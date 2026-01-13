

Public Class Frm_Pagament
    Private mProveidor As Contact
    Private mLastTabIdx As Integer
    Private mEmp As DTOEmp
    Private mAllowEvents As Boolean
    Private mCca As Cca = Nothing

    Public WriteOnly Property Proveidor() As Contact
        Set(ByVal Value As Contact)
            Dim oDefaultAmt As maxisrvr.Amt = MaxiSrvr.DefaultAmt
            mProveidor = Value
            mEmp = mProveidor.Emp
            TextBoxNom.Text = mProveidor.Nom
            DateTimePicker1.Value = Today
            PictureBoxLogo.Image = mProveidor.Img48
            xl_pnd_select1.Codi = Pnd.Codis.Creditor
            xl_pnd_select1.Pnds = PndsLoader.FromContact(mProveidor)
            Xl_Pagaments1.BeneficiariNom = mProveidor.Nom_o_NomComercial
            Xl_AmtCurDifCanvi.Amt = oDefaultAmt
            Xl_AmtCurGts.Amt = oDefaultAmt
            Xl_AmtCurLiq.Amt = oDefaultAmt
            mAllowEvents = True
        End Set
    End Property

    Public WriteOnly Property Fch() As Date
        Set(ByVal value As Date)
            Me.DateTimePicker1.Value = value
        End Set
    End Property


    Public WriteOnly Property DocFile() As DTODocFile
        Set(ByVal value As DTODocFile)
            If value IsNot Nothing Then
                Xl_DocFile1.Load(value)
            End If
        End Set
    End Property

    Public Shadows Function Update(ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlClient.SqlConnection = Current.SQLConnection(True)
        Dim oTrans As SqlClient.SqlTransaction = oConn.BeginTransaction
        Try
            Dim oPnds As Pnds = xl_pnd_select1.Pnds
            mCca = GetCca(oPnds)
            mCca.Update(oTrans)
            For Each oPnd As Pnd In oPnds
                oPnd.SetStatus(oTrans, Pnd.StatusCod.saldat)
            Next
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Private Function GetCca(oPnds As Pnds) As Cca

        Dim oPlan As PgcPlan = PgcPlan.FromYear(DateTimePicker1.Value.Year)

        Dim oSum As New MaxiSrvr.Amt(0, Nothing, 0)
        Dim oPnd As Pnd
        Dim CcbCod As DTOCcb.DhEnum

        Dim oCca As New Cca(BLL.BLLApp.Emp)
        With oCca
            .Ccd = DTOCca.CcdEnum.Pagament
            .fch = DateTimePicker1.Value
            .Txt = Xl_Pagaments1.Concepte

            For Each oPnd In oPnds
                Select Case oPnd.Cod
                    Case Pnd.Codis.Creditor
                        CcbCod = DTOCcb.DhEnum.Debe
                        oSum.Add(oPnd.Amt)
                    Case Pnd.Codis.Deutor
                        CcbCod = DTOCcb.DhEnum.Haber
                        oSum.Substract(oPnd.Amt)
                End Select
                .ccbs.Add(New Ccb(oCca, oPnd.Cta, oPnd.Contact, oPnd.Amt, CcbCod, , oPnd))
            Next

            If Xl_AmtCurDifCanvi.Amt.Val <> 0 Then
                Dim oCtaDifCanvi As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.DiferenciesDeCanvi)
                If Xl_AmtCurDifCanvi.Amt.IsPositive Then
                    .ccbs.Add(New Ccb(oCca, oCtaDifCanvi, Nothing, Xl_AmtCurDifCanvi.Amt, DTOCcb.DhEnum.Haber))
                Else
                    .ccbs.Add(New Ccb(oCca, oCtaDifCanvi, Nothing, Xl_AmtCurDifCanvi.Amt.Absolute, DTOCcb.DhEnum.Debe))
                End If
            End If

            If CheckBoxGts.Checked Then
                If Xl_AmtCurGts.Amt.Val <> 0 Then
                    Dim oCtaGts As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.despesesPagament)
                    .ccbs.Add(New Ccb(oCca, oCtaGts, Xl_Pagaments1.SubCta, Xl_AmtCurGts.Amt, DTOCcb.DhEnum.Debe))
                End If
            End If

            .ccbs.Add(New Ccb(oCca, Xl_Pagaments1.Cta, Xl_Pagaments1.SubCta, Xl_AmtCurLiq.Amt, DTOCcb.DhEnum.Haber))

            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If

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
                BlEnableNext = (xl_pnd_select1.Pnds.Count > 0)
            Case TabPageFpg.Text
                BlEnableNext = False
                BlEnableEnd = Xl_Pagaments1.CheckComplete
            Case TabPageEnd.Text
                BlEnablePrevious = False
                BlEnableNext = False
                BlEnableEnd = True
                sCaptionEnd = "SORTIDA"
        End Select

        Dim BlTransfer As Boolean = Xl_Pagaments1.RadioButtonTransfer.Checked
        CheckBoxEmail.Enabled = BlTransfer
        CheckBoxEmail.Checked = BlTransfer
        ComboBoxMails.Enabled = BlTransfer

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
                If CheckBoxEmail.Checked Then
                    Dim oProveidor As New DTOContact(mProveidor.Guid)
                    Dim oIban As DTOIban = BLL.BLLIban.FromContact(oProveidor) '  New Proveidor(mProveidor.Guid).FormaDePago.Iban
                    If oIban Is Nothing Then
                        MsgBox("falta assignar compte corrent al proveidor", MsgBoxStyle.Exclamation)
                    Else
                        MatOutlook.RemittanceAdvice(mCca)
                        Me.Close()
                    End If
                Else
                    Me.Close()
                End If

            Case Else
                Dim exs as New List(Of exception)
                If Update( exs) Then
                    TabControl1.SelectedTab() = TabPageEnd
                    TextBoxEnd.Text = "pagament registrat correctament"
                Else
                    UIHelper.WarnError( exs, "error al registrar el cobrament")
                End If
        End Select
        'Wizard_AfterTabSelect()
    End Sub

    Private Function AltresDivises() As Boolean
        Dim BlEur As Boolean = xl_pnd_select1.Amt.Cur.Equals(MaxiSrvr.Cur.Eur)
        Return Not BlEur
    End Function

    Private Sub xl_pnd_select1_AfterUpdate() Handles xl_pnd_select1.AfterUpdate
        EnableButtons()
        Dim oAmt As maxisrvr.Amt = xl_pnd_select1.Amt
        Xl_AmtCurSel.Amt = oAmt
        Xl_AmtCurContraValor.Amt = New maxisrvr.Amt


        If AltresDivises() Then
            Xl_AmtCurSel.Enabled = False
            Xl_AmtCurSelEur.Amt = New maxisrvr.Amt(oAmt.Eur, MaxiSrvr.Cur.Eur, oAmt.Eur)
            Xl_AmtCurSelEur.Visible = True

            LabelContraValor.Visible = True
            Xl_AmtCurContraValor.Visible = True

            LabelDifCanvi.Visible = True
            Xl_AmtCurDifCanvi.Visible = True
        Else
            Xl_AmtCurSel.Enabled = True
            Xl_AmtCurSelEur.Amt = New maxisrvr.Amt()
            Xl_AmtCurSelEur.Visible = False

            LabelContraValor.Visible = False
            Xl_AmtCurContraValor.Visible = False

            LabelDifCanvi.Visible = False
            Xl_AmtCurDifCanvi.Visible = False
        End If

        RefrescaDespeses()
    End Sub

    Private Sub RefrescaDespeses()
        Dim BlShowLiq As Boolean = False
        Dim oSum As maxisrvr.Amt = New maxisrvr.Amt

        Xl_AmtCurGts.Visible = (CheckBoxGts.Checked)

        If AltresDivises() Then
            oSum = Xl_AmtCurContraValor.Amt.Clone
            BlShowLiq = True
        Else
            oSum = Xl_AmtCurSel.Amt.Clone
            BlShowLiq = CheckBoxGts.Checked
        End If

        If CheckBoxGts.Checked Then
            oSum.Add(Xl_AmtCurGts.Amt)
            BlShowLiq = True
        End If

        LabelLiq.Visible = BlShowLiq
        Xl_AmtCurLiq.Visible = BlShowLiq
        Xl_AmtCurLiq.Amt = oSum
    End Sub

    Private Sub Xl_Pagaments1_AfterUpdate() Handles Xl_Pagaments1.AfterUpdate
        EnableButtons()
    End Sub



    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim AllowEvent As Boolean = False

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
                'Save() commentem per no duplicar amb l'acció de clicar el botó
            End If
            EnableButtons()
        Else
            TabControl1.SelectedTab = TabControl1.TabPages(mLastTabIdx)
        End If
    End Sub



    Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_AmtCurGts.Changed, _
     CheckBoxGts.CheckedChanged
        If mAllowEvents Then
            RefrescaDespeses()
        End If
    End Sub


    Private Sub Proveidor_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNom.DoubleClick, _
    PictureBoxLogo.DoubleClick
        root.ShowContact(mProveidor)
    End Sub


 


    Private Sub Xl_AmtCurContraValor_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtCurContraValor.AfterUpdate
        Dim oSum As maxisrvr.Amt = Xl_AmtCurContraValor.Amt.Clone
        oSum.Add(Xl_AmtCurGts.Amt)
        Xl_AmtCurLiq.Amt = oSum

        Dim oDif As maxisrvr.Amt = Xl_AmtCurSelEur.Amt.Clone
        oDif.Substract(Xl_AmtCurContraValor.Amt.Clone())
        Xl_AmtCurDifCanvi.Amt = oDif
    End Sub
End Class