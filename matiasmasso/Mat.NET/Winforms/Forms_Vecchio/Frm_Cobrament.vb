Public Class Frm_Cobrament

    '----------------------------------------------vigilar SaveCca especialment al cobrar impagats
    Private _Source As Sources
    Private _EdiversaRemadv As DTOEdiversaRemadv
    Private _Pnds As List(Of DTOPnd)
    Private _Contact As DTOContact
    Private _Cca As DTOCca
    Private _AllowEvents As Boolean
    Private _PreviousTab As Tabs

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Sources
        EdiRemadv
        Pnds
    End Enum

    Private Enum Tabs
        PndSel
        Fpg
        Final
    End Enum

    Public Sub New(oEdiversaRemadv As DTOEdiversaRemadv)
        MyBase.New()
        Me.InitializeComponent()
        _EdiversaRemadv = oEdiversaRemadv
        _Contact = oEdiversaRemadv.EmisorPago
        _Source = Sources.EdiRemadv
    End Sub

    Public Sub New(oContact As DTOContact, Optional oPnds As List(Of DTOPnd) = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Pnds = oPnds
        _Contact = oContact
        _Source = Sources.Pnds
    End Sub

    Private Async Sub Frm_Cobrament_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Contact.Load(_Contact, exs) Then
            TextBoxNom.Text = _Contact.FullNom
            PictureBoxLogo.Image = LegacyHelper.ImageHelper.Converter(_Contact.Logo)
            Dim oAllPnds = Await FEB2.Pnds.All(exs, GlobalVariables.Emp, _Contact)
            If exs.Count = 0 Then
                Xl_Pnds_Select1.Codi = DTOPnd.Codis.Deutor
                Xl_Pnds_Select1.Load(oAllPnds, DTOPnd.Codis.Deutor)
                Dim oImpagats = Await FEB2.Impagats.All(exs, Current.Session.Emp, _Contact)
                If exs.Count = 0 Then
                    Xl_Pnds_Select1.Load(oImpagats)
                    Xl_AmtDespeses.Amt = DTOAmt.Empty
                    Xl_Cobrament1.SetPagadorNom(_Contact.NomComercialOrDefault())
                    Try
                        Xl_Cobrament1.XecIBAN = Await FEB2.Iban.FromContact(exs, _Contact, DTOIban.Cods.Client)
                    Catch ex As Exception
                        exs.Add(ex)
                    End Try
                    Select Case _Source
                        Case Sources.EdiRemadv
                            Dim oPnds As New List(Of DTOPnd)
                            For Each oItem As DTOEdiversaRemadvItem In _EdiversaRemadv.Items
                                If oItem.Pnd IsNot Nothing Then
                                    oPnds.Add(oItem.Pnd)
                                End If
                            Next
                            Xl_Pnds_Select1.SetCheckedItems(oPnds)
                            Xl_Cobrament1.LoadPagare(_EdiversaRemadv.DocRef, _EdiversaRemadv.FchVto)
                        Case Sources.Pnds
                            If _Pnds IsNot Nothing Then
                                Xl_Pnds_Select1.SetCheckedItems(_Pnds)
                            End If
                    End Select
                    EnableButtons()
                    _AllowEvents = True
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function Save(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oPnds As List(Of DTOPnd) = Xl_Pnds_Select1.Pnds
        Dim oImpagats As List(Of DTOImpagat) = Xl_Pnds_Select1.Impagats
        'For Each oImpagat As DTOImpagat In oImpagats
        'oImpagat.FchSdo = DateTimePicker1.Value
        'Next
        Select Case Xl_Cobrament1.CodiFpg
            Case Xl_Cobrament.Fpgs.Cash, Xl_Cobrament.Fpgs.Transfer
                _Cca = GetCca(oPnds, oImpagats)
                Dim oCobrament As New DTOCobrament
                With oCobrament
                    .Cca = _Cca
                    .Pnds = oPnds
                    .Impagats = oImpagats
                End With
                retval = Await FEB2.Cobrament.Update(exs, oCobrament)
            Case Xl_Cobrament.Fpgs.Xec, Xl_Cobrament.Fpgs.Pagare
                retval = Await SaveXec(exs)
        End Select
        Return retval
    End Function


    Private Async Function SaveXec(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim DtFch As Date = DateTimePicker1.Value
        Dim oLliurador As DTOContact = _Contact
        Dim oIban As DTOIban = Xl_Cobrament1.XecIBAN
        Dim sXecNum As String = Xl_Cobrament1.XecNum
        Dim DtVto As Date = Xl_Cobrament1.XecVto
        Dim oPnds As List(Of DTOPnd) = Xl_Pnds_Select1.Pnds
        Dim oImpagats As List(Of DTOImpagat) = Xl_Pnds_Select1.Impagats
        Dim oDespesesImpagats As DTOAmt = Xl_AmtDespeses.Amt

        'Dim oXec As New Xec(oLliurador, oIban, sXecNum, oPnds, oImpagats, DtVto)

        Dim oXec = DTOXec.Factory(Current.Session.Emp)
        With oXec
            .Lliurador = oLliurador
            .Iban = oIban
            .XecNum = sXecNum
            .Pnds = oPnds
            .Impagats = IIf(oImpagats Is Nothing, New List(Of DTOImpagat), oImpagats)
            .Vto = DtVto
            .FchRecepcio = DtFch
        End With

        If Await FEB2.Xec.Cobrament(exs, Current.Session.User, oXec) Then
            TextBoxEnd.Text = "xec registrat i pendent de ingressar"
            _Cca = oXec.CcaRebut
            retval = True
        Else
            UIHelper.WarnError(exs)
        End If


        Return retval
    End Function


    Private Function GetCca(oPnds As List(Of DTOPnd), oImpagats As List(Of DTOImpagat)) As DTOCca
        Dim exs As New List(Of Exception)
        Dim oSum = DTOAmt.Empty
        Dim oCtaImpagats = FEB2.PgcCta.FromCodSync(DTOPgcPlan.Ctas.impagats, Current.Session.Emp, exs)
        Dim oCtaIngresosDespesesImpagats = FEB2.PgcCta.FromCodSync(DTOPgcPlan.Ctas.ImpagosRecuperacioDespeses, Current.Session.Emp, exs)

        Dim oPnd As DTOPnd
        Dim oImpagat As DTOImpagat
        Dim CcbCod As DTOCcb.DhEnum

        Dim oCca As DTOCca = DTOCca.Factory(DateTimePicker1.Value, Current.Session.User, DTOCca.CcdEnum.Cobro)
        With oCca
            .Concept = Xl_Cobrament1.Concepte

            For Each oPnd In oPnds
                Select Case oPnd.Cod
                    Case DTOPnd.Codis.Deutor
                        CcbCod = DTOCcb.DhEnum.Haber
                        oSum.Add(oPnd.Amt)
                    Case DTOPnd.Codis.Creditor
                        CcbCod = DTOCcb.DhEnum.Debe
                        oSum.Substract(oPnd.Amt)
                End Select

                oCca.AddCredit(oPnd.Amt, oPnd.Cta, oPnd.Contact)
            Next

            For Each oImpagat In oImpagats
                CcbCod = DTOCcb.DhEnum.Haber
                oSum.Add(oImpagat.Nominal)
                oCca.AddCredit(oImpagat.Nominal.Clone, oCtaImpagats, oImpagat.Csb.Contact)
            Next

            If Xl_AmtDespeses.Amt.Eur <> 0 Then
                Dim oAmt As DTOAmt = Xl_AmtDespeses.Amt
                oCca.AddCredit(oAmt, oCtaIngresosDespesesImpagats)
                oSum.Add(oAmt)
            End If

            oCca.AddSaldo(Xl_Cobrament1.Cta)
        End With
        Return oCca
    End Function


    Private Sub EnableButtons()
        Dim BlEnableNext As Boolean = True
        Dim BlEnablePrevious As Boolean = True
        Dim BlEnableEnd As Boolean = False

        Select Case TabControl1.SelectedIndex
            Case Tabs.PndSel
                BlEnablePrevious = False
                BlEnableNext = (Xl_Pnds_Select1.Pnds.Count + Xl_Pnds_Select1.Impagats.Count > 0)
                BlEnableEnd = False
            Case Tabs.Fpg
                BlEnablePrevious = True
                BlEnableNext = Xl_Cobrament1.CheckComplete
                BlEnableEnd = False
            Case Tabs.Final
                'BlEnablePrevious = False
                BlEnableNext = False
                BlEnableEnd = True
        End Select

        ButtonPrevious.Enabled = BlEnablePrevious
        ButtonNext.Enabled = BlEnableNext
        ButtonEnd.Enabled = BlEnableEnd
    End Sub

    Private Sub ButtonPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPrevious.Click
        Dim Idx As Integer = TabControl1.SelectedIndex
        TabControl1.SelectedTab = TabControl1.TabPages(Idx - 1)
        'EnableButtons() ja s'activa al canviar de Tab
        'Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNext.Click
        Dim Idx As Integer = TabControl1.SelectedIndex
        TabControl1.SelectedTab = TabControl1.TabPages(Idx + 1)
        'EnableButtons() ja s'activa al canviar de Tab
        'Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEnd.Click
        Me.Close()
    End Sub


    Private Sub xl_pnd_select1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Pnds_Select1.ItemCheckedChange
        If _AllowEvents Then
            Dim oNominal As DTOAmt = Xl_Pnds_Select1.GetTotal(Xl_Pnds_Select.Totals.AllChecked)
            Dim oGastos = DTOAmt.empty

            Dim oImpagats As List(Of DTOImpagat) = Xl_Pnds_Select1.Impagats
            For Each oImpagat As DTOImpagat In oImpagats
                oGastos.add(oImpagat.gastos)
            Next

            Dim oAmt As DTOAmt = oNominal.clone
            Xl_AmtNominal.Amt = oAmt
            Xl_AmtDespeses.Amt = oGastos
            oAmt.add(oGastos)
            Xl_AmtLiquid.Amt = oAmt
            CheckBoxDespeses.Checked = (oGastos.Eur <> 0)
            EnableButtons()
        End If
    End Sub

    Private Sub Xl_Cobrament1_AfterUpdate() Handles Xl_Cobrament1.AfterUpdate
        If _AllowEvents Then
            EnableButtons()
        End If
    End Sub





    Private Sub CheckBoxDespeses_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDespeses.CheckedChanged
        GroupBoxDespeses.Visible = CheckBoxDespeses.Checked
    End Sub

    Private Sub Xl_AmtDespeses_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtDespeses.AfterUpdate
        Dim oNominal As DTOAmt = Xl_AmtNominal.Amt
        Dim oAmtToAdd As DTOAmt = sender
        If oNominal Is Nothing Then oNominal = DTOAmt.Empty
        oNominal.Add(oAmtToAdd)
        Xl_AmtLiquid.Amt = oNominal
    End Sub

    Private Sub Xl_AmtLiquid_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtLiquid.AfterUpdate
        Dim oNominal As DTOAmt = Xl_AmtNominal.Amt
        Dim oLiquid As DTOAmt = Xl_AmtLiquid.Amt
        Dim oDespeses As DTOAmt = oLiquid.Clone
        oDespeses.Substract(oNominal)
        Xl_AmtDespeses.Amt = oDespeses
    End Sub

    Private Sub Xl_Pnds_Select1_ItemCheckedChange(sender As Object, e As MatEventArgs) Handles Xl_Pnds_Select1.ItemCheckedChange
        If _AllowEvents Then
            'EnableButtons() (ja actua per afterupdate
        End If
    End Sub

    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        Select Case e.TabPageIndex
            Case _PreviousTab + 1
                e.Cancel = Not ButtonNext.Enabled
            Case _PreviousTab - 1
                e.Cancel = Not ButtonPrevious.Enabled
        End Select
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If _AllowEvents Then
            _PreviousTab = TabControl1.SelectedIndex

            If TabControl1.SelectedIndex = Tabs.Final Then
                Dim exs As New List(Of Exception)
                If Await Save(exs) Then
                    TextBoxEnd.Text = "cobrament registrat correctament"
                    EnableButtons()
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cca))
                Else
                    TextBoxEnd.Text = "error al registrar el cobrament"
                    UIHelper.WarnError(exs, "error al registrar el cobrament")
                End If
            End If
        End If
    End Sub


End Class