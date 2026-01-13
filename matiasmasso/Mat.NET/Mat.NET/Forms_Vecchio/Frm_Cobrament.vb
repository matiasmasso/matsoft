

Public Class Frm_Cobrament

    '----------------------------------------------vigilar SaveCca especialment al cobrar impagats
    Private mClient As Contact
    Private mEmp as DTOEmp
    Private mLastTabIdx As Integer
    Private _Cca As Cca

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(oEdiRemadv As EdiRemadv)
        MyBase.New()
        Me.InitializeComponent()
        mClient = oEdiRemadv.EmisorPago
        mEmp = mClient.Emp
        TextBoxNom.Text = mClient.Nom
        PictureBoxLogo.Image = mClient.Img48
        Xl_Pnds_Select1.Codi = Pnd.Codis.Deutor
        Xl_Pnds_Select1.Load(PndsLoader.FromContact(mClient), Pnd.Codis.Deutor)
        Xl_Pnds_Select1.Load(New Client(mClient.Guid).Impagats)
        Xl_AmtDespeses.Amt = New MaxiSrvr.Amt(0, MaxiSrvr.Cur.Eur, 0)
        Xl_Cobrament1.PagadorNom = mClient.Nom_o_NomComercial
        Try
            Xl_Cobrament1.XecIBAN = mClient.Client.FormaDePago.Iban
        Catch ex As Exception
        End Try

        Dim oPnds As New Pnds
        For Each oItem As EdiRemadvItem In oEdiRemadv.Items
            If oItem.Pnd IsNot Nothing Then
                oPnds.Add(oItem.Pnd)
            End If
        Next
        Xl_Pnds_Select1.SetCheckedItems(oPnds)
        Xl_Cobrament1.loadPagare(oEdiRemadv.DocRef, oEdiRemadv.FchVto)

        EnableButtons()
    End Sub

    Public Sub New(oContact As Contact, Optional oPnds As Pnds = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mClient = oContact
        mEmp = mClient.Emp
        TextBoxNom.Text = mClient.Nom
        PictureBoxLogo.Image = mClient.Img48
        Xl_Pnds_Select1.Load(PndsLoader.FromContact(mClient), Pnd.Codis.Deutor)
        Xl_Pnds_Select1.Load(New Client(mClient.Guid).Impagats)
        Xl_AmtDespeses.Amt = New MaxiSrvr.Amt(0, MaxiSrvr.Cur.Eur, 0)
        Xl_Cobrament1.PagadorNom = mClient.Nom_o_NomComercial
        Try
            Xl_Cobrament1.XecIBAN = mClient.Client.FormaDePago.Iban
        Catch ex As Exception
        End Try

        If oPnds IsNot Nothing Then
            Xl_Pnds_Select1.SetCheckedItems(oPnds)
            EnableButtons()
        End If

    End Sub


    Public WriteOnly Property Impagats() As Impagats
        Set(ByVal oImpagats As Impagats)
            If oImpagats IsNot Nothing Then
                If oImpagats.Count > 0 Then
                    mClient = oImpagats(0).Csb.Client
                    mEmp = mClient.Emp
                    TextBoxNom.Text = mClient.Nom
                    PictureBoxLogo.Image = mClient.Img48
                    'chequeijar els impagats passats
                    '...
                    Xl_Pnds_Select1.Codi = Pnd.Codis.Deutor
                    Xl_Pnds_Select1.Load(PndsLoader.FromContact(mClient), Pnd.Codis.Deutor)
                    Xl_Pnds_Select1.Load(New Client(mClient.Guid).Impagats)
                    Xl_AmtDespeses.Amt = New maxisrvr.Amt(0, MaxiSrvr.Cur.Eur, 0)
                    Xl_Cobrament1.PagadorNom = mClient.Nom_o_NomComercial
                End If
            End If
        End Set
    End Property

    Private Function Save(ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oPnds As Pnds = Xl_Pnds_Select1.Pnds
        Dim oImpagats As Impagats = Xl_Pnds_Select1.Impagats

        Select Case Xl_Cobrament1.CodiFpg
            Case Xl_Cobrament.Fpgs.Cash, Xl_Cobrament.Fpgs.Transfer
                retval = SaveCca(oPnds, oImpagats, exs)
            Case Xl_Cobrament.Fpgs.Xec, Xl_Cobrament.Fpgs.Pagare
                retval = SaveXec( exs)
        End Select
        Return retval
    End Function



    Private Function SaveCca(oPnds As Pnds, oImpagats As Impagats, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlClient.SqlConnection = Current.SQLConnection(True)
        Dim oTrans As SqlClient.SqlTransaction = oConn.BeginTransaction
        Try
            Dim oCca As Cca = GetCca(oPnds, oImpagats)
            oCca.Update(oTrans)
            For Each oPnd As Pnd In oPnds
                oPnd.SetStatus(oTrans, Pnd.StatusCod.saldat)
            Next

            For Each oImpagat As Impagat In oImpagats
                Dim oACompte As MaxiSrvr.Amt = oImpagat.PagatACompte.Clone
                oACompte.Add(oImpagat.Gastos)
                oACompte.Add(oImpagat.Pendent)
                oImpagat.PagatACompte = oACompte.Clone
                oImpagat.Status = Impagat.StatusCodes.Saldat
                oImpagat.Update(oTrans)
            Next

            oTrans.Commit()
            _Cca = oCca
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function




    Private Function SaveXec(ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim DtFch As Date = DateTimePicker1.Value
        Dim oLliurador As Contact = mClient
        Dim oIban As DTOIban = Xl_Cobrament1.XecIBAN
        Dim sXecNum As String = Xl_Cobrament1.XecNum
        Dim DtVto As Date = Xl_Cobrament1.XecVto
        Dim oPnds As Pnds = Xl_Pnds_Select1.Pnds
        Dim oImpagats As Impagats = Xl_Pnds_Select1.Impagats
        Dim oDespesesImpagats As Amt = Xl_AmtDespeses.Amt

        Dim oXec As New Xec(oLliurador, oIban, sXecNum, oPnds, oImpagats, DtVto)
        oXec.FchRecepcio = DtFch

        If XecLoader.UpdateXecRebut(oXec, exs) Then
            TextBoxEnd.Text = "xec registrat i pendent de ingressar"
            retval = True
        End If

        _Cca = oXec.CcaRebut

        Return retval
    End Function


    Private Function GetCca(oPnds As Pnds, oImpagats As Impagats) As Cca
        Dim oSum As New MaxiSrvr.Amt()

        Dim oPlan As PgcPlan = PgcPlan.FromYear(DateTimePicker1.Value.Year)
        Dim oCtaImpagats As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.impagats)
        Dim oCtaIngresosDespesesImpagats As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.ImpagosRecuperacioDespeses)

        Dim oPnd As Pnd
        Dim oImpagat As Impagat
        Dim CcbCod As DTOCcb.DhEnum
        Dim oEmptyContact As Contact = Nothing

        Dim oCca As new cca(BLL.BLLApp.emp)
        With oCca
            .Ccd = DTOCca.CcdEnum.Cobro
            .fch = DateTimePicker1.Value
            .Txt = Xl_Cobrament1.Concepte

            For Each oPnd In oPnds
                Select Case oPnd.Cod
                    Case Pnd.Codis.Deutor
                        CcbCod = DTOCcb.DhEnum.Haber
                        oSum.Add(oPnd.Amt)
                    Case Pnd.Codis.Creditor
                        CcbCod = DTOCcb.DhEnum.Debe
                        oSum.Substract(oPnd.Amt)
                End Select
                .ccbs.Add(New Ccb(oCca, oPnd.Cta, oPnd.Contact, oPnd.Amt, CcbCod, , oPnd))
            Next

            For Each oImpagat In oImpagats
                CcbCod = DTOCcb.DhEnum.Haber
                oSum.Add(oImpagat.Pendent)
                .ccbs.Add(New Ccb(oCca, oCtaImpagats, oImpagat.Csb.Client, oImpagat.Pendent.Clone, CcbCod))
            Next

            If Xl_AmtDespeses.Amt.Eur <> 0 Then
                Dim oAmt As MaxiSrvr.Amt = Xl_AmtDespeses.Amt
                .ccbs.Add(New Ccb(oCtaIngresosDespesesImpagats, oEmptyContact, oAmt, Pnd.Codis.Creditor))
                oSum.Add(oAmt)
            End If

            .ccbs.Add(New Ccb(oCca, Xl_Cobrament1.Cta, Xl_Cobrament1.SubCta, oSum, DTOCcb.DhEnum.Debe))
        End With
        Return oCca
    End Function


    Private Sub EnableButtons()
        Dim BlEnableNext As Boolean = True
        Dim BlEnablePrevious As Boolean = True
        Dim BlEnableEnd As Boolean = False
        Dim sCaptionEnd As String = "FI >>"

        mLastTabIdx = TabControl1.SelectedIndex
        Select Case TabControl1.SelectedTab.Text
            Case TabPagePndSel.Text
                BlEnablePrevious = False
                BlEnableNext = (Xl_Pnds_Select1.Pnds.Count + Xl_Pnds_Select1.Impagats.Count > 0)
            Case TabPageFpg.Text
                BlEnableNext = False
                BlEnableEnd = Xl_Cobrament1.CheckComplete
            Case TabPageEnd.Text
                BlEnablePrevious = False
                BlEnableNext = False
                BlEnableEnd = True
                sCaptionEnd = "SORTIDA"
        End Select

        ButtonPrevious.Enabled = BlEnablePrevious
        ButtonNext.Enabled = BlEnableNext
        ButtonEnd.Enabled = BlEnableEnd
        ButtonEnd.Text = sCaptionEnd
    End Sub

    Private Sub ButtonPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPrevious.Click
        Dim Idx As Integer = TabControl1.SelectedIndex
        TabControl1.SelectedTab = TabControl1.TabPages(Idx - 1)
        EnableButtons()
        'Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNext.Click
        Dim Idx As Integer = TabControl1.SelectedIndex
        TabControl1.SelectedTab = TabControl1.TabPages(Idx + 1)
        EnableButtons()
        'Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEnd.Click
        Select Case TabControl1.SelectedTab.Text
            Case TabPageEnd.Text
                'si ja estaba a l'ultim tab, surt.
                Me.Close()
            Case Else
                Dim exs as New List(Of exception)
                Dim oCca As Cca = Nothing
                If Save( exs) Then
                    TextBoxEnd.Text = "cobrament registrat correctament"
                    TabControl1.SelectedTab() = TabPageEnd
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cca))
                Else
                    UIHelper.WarnError( exs, "error al registrar el cobrament")
                End If
        End Select

    End Sub


    Private Sub xl_pnd_select1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Pnds_Select1.ItemCheckedChange
        Dim oNominal As MaxiSrvr.Amt = Xl_Pnds_Select1.GetTotal(Xl_Pnd_Select.Totals.AllChecked)
        Dim oGastos As New MaxiSrvr.Amt

        Dim oImpagats As Impagats = Xl_Pnds_Select1.Impagats
        For Each oImpagat As Impagat In oImpagats
            oGastos.Add(oImpagat.Gastos)
        Next

        Dim oAmt As MaxiSrvr.Amt = oNominal.Clone
        Xl_AmtNominal.Amt = oAmt
        Xl_AmtDespeses.Amt = oGastos
        oAmt.Add(oGastos)
        Xl_AmtLiquid.Amt = oAmt
        CheckBoxDespeses.Checked = (oGastos.Eur <> 0)
        EnableButtons()
    End Sub

    Private Sub Xl_Cobrament1_AfterUpdate() Handles Xl_Cobrament1.AfterUpdate
        EnableButtons()
    End Sub



    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim AllowEvent As Boolean = False
        'ButtonPrevious.Enabled = True

        Select Case TabControl1.SelectedIndex
            Case TabControl1.TabPages.Count - 1
                AllowEvent = ButtonEnd.Enabled
            Case Is > mLastTabIdx
                AllowEvent = ButtonNext.Enabled
            Case Is < mLastTabIdx
                AllowEvent = ButtonPrevious.Enabled
        End Select

        If AllowEvent Then
            If TabControl1.SelectedIndex = TabControl1.TabPages.Count - 1 Then

                'Save() nomes permetem save per el boto "End"
            End If
            EnableButtons()
        Else
            TabControl1.SelectedTab = TabControl1.TabPages(mLastTabIdx)
        End If
    End Sub

    Private Sub CheckBoxDespeses_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDespeses.CheckedChanged
        GroupBoxDespeses.Visible = CheckBoxDespeses.Checked
    End Sub

    Private Sub Xl_AmtDespeses_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtDespeses.AfterUpdate
        Dim oNominal As MaxiSrvr.Amt = Xl_AmtNominal.Amt
        Dim oAmtToAdd As Amt = sender
        If oNominal Is Nothing Then oNominal = New Amt
        oNominal.Add(oAmtToAdd)
        Xl_AmtLiquid.Amt = oNominal
    End Sub

    Private Sub Xl_AmtLiquid_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtLiquid.AfterUpdate
        Dim oNominal As maxisrvr.Amt = Xl_AmtNominal.Amt
        Dim oLiquid As maxisrvr.Amt = Xl_AmtLiquid.Amt
        Dim oDespeses As maxisrvr.Amt = oLiquid.Clone
        oDespeses.Substract(oNominal)
        Xl_AmtDespeses.Amt = oDespeses
    End Sub

    Private Sub Xl_Pnds_Select1_ItemCheckedChange(sender As Object, e As MatEventArgs) Handles Xl_Pnds_Select1.ItemCheckedChange
        EnableButtons()
    End Sub
End Class