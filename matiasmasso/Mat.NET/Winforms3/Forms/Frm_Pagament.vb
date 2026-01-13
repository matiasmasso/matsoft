Public Class Frm_Pagament

    Private _PreviousTab As Tabs
    Private _Proveidor As DTOProveidor
    Private _Fch As Date
    Private _DocFile As DTODocFile
    Private _BancTransferPool As DTOBancTransferPool
    Private _resultmsg As String
    Private _AllowEvents As Boolean


    Private Enum Tabs
        Pnd
        Fpg
        Fin
    End Enum

    Private Enum PaymentModes
        Visa
        DirectDebit
        CreditTransfer
    End Enum

    Public Sub New(oProveidor As DTOProveidor, Optional DtFch As Date = Nothing, Optional oDocFile As DTODocFile = Nothing)
        MyBase.New
        InitializeComponent()
        _Proveidor = oProveidor
        _Fch = IIf(DtFch = Nothing, DTO.GlobalVariables.Today(), DtFch)
        _DocFile = oDocFile

    End Sub

    Private Async Sub Frm_Pagament_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        FEB.Contact.Load(_Proveidor, exs)
        If FEB.Proveidor.Load(_Proveidor, exs) Then
            Me.Text = "Pagament a " & _Proveidor.FullNom

            If _DocFile Is Nothing Then
                MyBase.Size = New Drawing.Size(Me.Size.Width - SplitContainer1.Panel1.Size.Width, MyBase.Height)
                SplitContainer1.Panel1Collapsed = True
            Else
                Xl_DocFile1.Load(_DocFile)
            End If

            Dim oPnds = Await FEB.Pnds.All(exs, GlobalVariables.Emp, _Proveidor)
            For Each oPnd In oPnds
                oPnd.Contact = _Proveidor
            Next

            If exs.Count = 0 Then
                Xl_Pnds_Select1.Load(oPnds, DTOPnd.Codis.Creditor)
                SetNavButtons()

                Select Case _Proveidor.PaymentTerms.Cod
                    Case DTOPaymentTerms.CodsFormaDePago.DomiciliacioBancaria
                        RadioButtonDirectDebit.Checked = True
                    Case DTOPaymentTerms.CodsFormaDePago.Transferencia
                        RadioButtonCreditTransfer.Checked = True
                End Select

                If Await Xl_BancsComboBox1.LoadDefaultsFor(DTODefault.Codis.BancNominaTransfers, exs) Then
                    If Await LoadIban(exs) Then
                        SetPaymentTerms()
                        SetCcaConcept()
                        SetMenu()
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
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub


    Private Sub Xl_Pnds_Select1_ItemCheckedChange(sender As Object, e As MatEventArgs) Handles Xl_Pnds_Select1.ItemCheckedChange
        SetImports()
        SetTransferBeneficiariConcept()
        SetNavButtons()
    End Sub

    Private Sub Xl_EurContravalor_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_EurContravalor.AfterUpdate
        If _AllowEvents Then
            SetDiferenciesDeCanvi()
            SetLiquid()
        End If
    End Sub
    Private Sub Xl_EurDespeses_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_EurDespeses.AfterUpdate
        If _AllowEvents Then
            SetLiquid()
        End If
    End Sub

    Private Sub SetImports()
        Dim oPnds As List(Of DTOPnd) = Xl_Pnds_Select1.Pnds
        Dim oAmt As DTOAmt = DTOPnd.Sum(oPnds, DTOPnd.Codis.Creditor)
        Dim DtFch As Date = DateTimePicker1.Value
        Dim oRate As DTOCurExchangeRate = Nothing
        Dim oCur As DTOCur = oAmt.Cur

        Xl_AmountCur1.Amt = oAmt
        Xl_EurContravalor.Amt = DTOAmt.Factory(oAmt.Eur)
        If oCur.isEur Then
            LabelDivisa.Visible = False
            Xl_AmountCur1.Visible = False
            LabelExchange.Text = "Total sel.leccionat"
            LabelDiferenciesDeCanvi.Visible = False
            Xl_EurDiferenciesDeCanvi.Visible = False
        Else
            LabelExchange.Text = String.Format("Contravalor a {0}", oCur.ExchangeText())
        End If

        SetDiferenciesDeCanvi()
        SetLiquid()
    End Sub

    Private Sub SetDiferenciesDeCanvi()
        Dim Seleccionat As Decimal = Xl_AmountCur1.Amt.Eur
        Dim Contravalor As Decimal = Xl_EurContravalor.Amt.Eur
        Xl_EurDiferenciesDeCanvi.Amt = DTOAmt.Factory(Seleccionat - Contravalor)
    End Sub

    Private Sub SetLiquid()
        Dim Contravalor As Decimal = Xl_EurContravalor.Amt.Eur
        Dim Despeses As Decimal = Xl_EurDespeses.Amt.Eur
        Xl_EurLiquid.Amt = DTOAmt.Factory(Contravalor + Despeses)
    End Sub

    Private Sub SetPaymentTerms()
        LabelTitularVisa.Visible = RadioButtonVisa.Checked
        Xl_LookupVisaCard1.Visible = RadioButtonVisa.Checked
        LabelBancEmissor.Visible = RadioButtonDirectDebit.Checked Or RadioButtonCreditTransfer.Checked
        Xl_BancsComboBox1.Visible = RadioButtonDirectDebit.Checked Or RadioButtonCreditTransfer.Checked
        GroupBoxCreditTransfer.Visible = RadioButtonCreditTransfer.Checked
        GroupBoxTransferResult.Visible = RadioButtonCreditTransfer.Checked
    End Sub

    Private Sub SetCcaConcept()
        Dim s As String = ""
        Dim bancnom = ""
        If (CurrentBanc() IsNot Nothing) Then
            bancnom = CurrentBanc.Abr
        End If
        Select Case CurrentMode()
            Case PaymentModes.CreditTransfer
                s = String.Format("{0} - transferència {1}", bancnom, _Proveidor.Nom)
            Case PaymentModes.DirectDebit
                s = String.Format("{0} - efecte {1}", bancnom, _Proveidor.Nom)
            Case PaymentModes.Visa
                s = String.Format("{1} - Pagament amb Visa {0}", bancnom, _Proveidor.Nom)
        End Select
        TextBoxCcaConcept.Text = Microsoft.VisualBasic.Left(s, 60)
    End Sub

    Private Sub SetTransferBeneficiariConcept()
        Dim sb As New Text.StringBuilder

        sb.Append("M+O - ")
        sb.Append(_Proveidor.Lang.Tradueix("Facturas", "Factures", "Invoices"))
        sb.Append(" ")
        Dim oPnds As List(Of DTOPnd) = Xl_Pnds_Select1.Pnds
        For Each oPnd As DTOPnd In oPnds
            If oPnd.FraNum > "" Then
                If oPnd.UnEquals(oPnds.First) Then sb.Append(", ")
                sb.Append(oPnd.FraNum)
            End If
        Next
        Dim s As String = sb.ToString
        TextBoxTransferConcept.Text = Microsoft.VisualBasic.Left(s, 140)
    End Sub

    Private Function CurrentMode() As PaymentModes
        Dim retval As PaymentModes
        If RadioButtonVisa.Checked Then
            retval = PaymentModes.Visa
        ElseIf RadioButtonDirectDebit.Checked Then
            retval = PaymentModes.DirectDebit
        ElseIf RadioButtonCreditTransfer.Checked Then
            retval = PaymentModes.CreditTransfer
        End If
        Return retval
    End Function

    Private Function CurrentBanc() As DTOBanc
        Dim retval As DTOBanc = Nothing
        If Xl_BancsComboBox1.SelectedItem IsNot Nothing Then
            retval = Xl_BancsComboBox1.SelectedItem
        End If
        Return retval
    End Function

    Private Function CtaDebit() As DTOPgcCta
        Dim retval As DTOPgcCta = Nothing
        Dim exs As New List(Of Exception)
        Dim oCur As DTOCur = Xl_AmountCur1.Amt.Cur
        If oCur.isUSD Then
            retval = FEB.PgcCta.FromCodSync(DTOPgcPlan.Ctas.ProveidorsUsd, Current.Session.Emp, exs)
        Else
            retval = FEB.PgcCta.FromCodSync(DTOPgcPlan.Ctas.ProveidorsEur, Current.Session.Emp, exs)
        End If
        Return retval
    End Function

    Private Async Function LoadIban(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oIban As DTOIban = Await FEB.Iban.FromContact(exs, _Proveidor, DTOIban.Cods.Proveidor)
        If exs.Count = 0 Then
            Await Xl_IbanDigits1.Load(oIban)
            retval = True
        End If
        Return retval
    End Function

    Private Sub SetMenu()
        ArxiuToolStripMenuItem.DropDownItems.Add("importar justificant", Nothing, AddressOf DisplayDocFile)
    End Sub

    Private Sub DisplayDocFile()
        If SplitContainer1.Panel1Collapsed Then
            SplitContainer1.Panel1Collapsed = False
            MyBase.Size = New Drawing.Size(Me.Size.Width + SplitContainer1.Panel1.Size.Width, MyBase.Height)
        End If
    End Sub

#Region "Navegacio"
    Private Function CurrentTab() As Tabs
        Dim retval As Tabs = TabControl1.SelectedIndex
        Return retval
    End Function

    Private Sub SetNavButtons()
        Select Case CurrentTab()
            Case Tabs.Pnd
                ButtonPrevious.Enabled = False
                ButtonNext.Enabled = Xl_Pnds_Select1.Pnds.Count > 0
                ButtonEnd.Enabled = False
            Case Tabs.Fpg
                ButtonPrevious.Enabled = True
                ButtonNext.Enabled = True
                ButtonEnd.Enabled = False
            Case Tabs.Fin
                ButtonCancel.Enabled = False
                ButtonPrevious.Enabled = False
                ButtonNext.Enabled = False
                ButtonEnd.Enabled = True
        End Select

    End Sub

    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        Select Case e.TabPageIndex
            Case _PreviousTab + 1
                e.Cancel = Not ButtonNext.Enabled
            Case _PreviousTab - 1
                e.Cancel = Not ButtonPrevious.Enabled
            Case Else
                e.Cancel = True
        End Select
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        If _AllowEvents Then
            _PreviousTab = TabControl1.SelectedIndex
            SetNavButtons()

            Select Case TabControl1.SelectedIndex
                Case Tabs.Fin
                    If Await Save(exs) Then
                        TextBoxResult.Text = _resultmsg
                        PictureBoxResult.Image = My.Resources.vb
                    Else
                        TextBoxResult.Text = _resultmsg
                        PictureBoxResult.Image = My.Resources.warn
                        ButtonPrevious.Enabled = True
                        UIHelper.WarnError(exs)
                    End If
            End Select
        End If
    End Sub

    Private Async Function Save(exs As List(Of Exception)) As Task(Of Boolean)
        Select Case CurrentMode()
            Case PaymentModes.Visa
                Await SaveVisa(exs)
            Case PaymentModes.DirectDebit
                Await SaveDirectDebit(exs)
            Case PaymentModes.CreditTransfer
                Await SaveCreditTransfer(exs)
        End Select
        Return exs.Count = 0
    End Function

    Private Async Function SaveVisa(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oCtaCredit = Await FEB.PgcCta.FromCod(DTOPgcPlan.Ctas.VisasPagadas, Current.Session.Emp, exs)
        Dim oAmt As DTOAmt = Xl_EurLiquid.Amt
        Dim oVisa As DTOVisaCard = Xl_LookupVisaCard1.VisaCard
        Dim oPnds As List(Of DTOPnd) = Xl_Pnds_Select1.Pnds
        Dim oCca As DTOCca = DTOCca.Factory(DateTimePicker1.Value, Current.Session.User, DTOCca.CcdEnum.Pagament)
        oCca.Concept = TextBoxCcaConcept.Text
        oCca.DocFile = Xl_DocFile1.Value
        oCca.AddCredit(oAmt, oCtaCredit, oVisa.Titular)
        oCca.AddSaldo(CtaDebit, _Proveidor)

        Dim pCca = Await FEB.Proveidor.SaveFactura(exs, oCca, oPnds)
        If exs.Count = 0 Then
            oCca = pCca
            _resultmsg = "Assentament " & oCca.Id & " registrat satisfactoriament"
            retval = True
        Else
            _resultmsg = ExceptionsHelper.ToFlatString(exs)
        End If
        Return retval
    End Function

    Private Async Function SaveDirectDebit(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oCtaCredit = Await FEB.PgcCta.FromCod(DTOPgcPlan.Ctas.bancs, Current.Session.Emp, exs)
        Dim oAmt As DTOAmt = Xl_EurLiquid.Amt
        Dim oCca As DTOCca = DTOCca.Factory(DateTimePicker1.Value, Current.Session.User, DTOCca.CcdEnum.Pagament)
        oCca.Concept = TextBoxCcaConcept.Text
        oCca.DocFile = Xl_DocFile1.Value
        oCca.AddCredit(oAmt, oCtaCredit, CurrentBanc)
        oCca.AddSaldo(CtaDebit, _Proveidor)

        Dim oPnds As List(Of DTOPnd) = Xl_Pnds_Select1.Pnds
        For Each oPnd In oPnds
            oPnd.CcaVto = oCca.Trimmed
            oPnd.Status = DTOPnd.StatusCod.saldat
        Next

        Dim pCca = Await FEB.Proveidor.SaveFactura(exs, oCca, oPnds)
        If exs.Count = 0 Then
            oCca = pCca
            _resultmsg = "Assentament " & oCca.Id & " registrat satisfactoriament"
            retval = True
        Else
            _resultmsg = ExceptionsHelper.ToFlatString(exs)
        End If
        Return retval
    End Function

    Private Async Function SaveCreditTransfer(exs As List(Of Exception)) As Task(Of Boolean)
        Dim oAmt As DTOAmt = Xl_EurLiquid.Amt
        Dim oPnds As List(Of DTOPnd) = Xl_Pnds_Select1.Pnds

        Dim oBankBranch As DTOBankBranch = Xl_IbanDigits1.BankBranch
        If oBankBranch Is Nothing Then
            Dim oIban = DTOIban.Factory(Xl_IbanDigits1.Value)
            oBankBranch = Await FEB.Iban.GetBankBranchFromDigits(oIban, exs)
        End If
        If oBankBranch Is Nothing Then
            _resultmsg = "falten les dades del banc"
        Else
            _BancTransferPool = DTOBancTransferPool.Factory(
             Current.Session.User,
            DateTimePicker1.Value,
            CurrentBanc,
            Xl_EurDespeses.Amt
            )


            For Each item As DTOPnd In Xl_Pnds_Select1.Pnds

                DTOBancTransferPool.AddPnd(
                    _BancTransferPool,
                    item,
                    oBankBranch,
                    Xl_IbanDigits1.Value,
                    item.Contact.Lang)

            Next

            Dim CcaId = Await FEB.BancTransferPool.Save(exs, _BancTransferPool)
            If exs.Count = 0 Then
                _BancTransferPool.Cca.Id = CcaId
                _resultmsg = "Assentament " & CcaId & " registrat satisfactoriament"
            Else
                _resultmsg = ExceptionsHelper.ToFlatString(exs)
            End If
        End If

        Return exs.Count = 0
    End Function


    Private Sub ButtonPrevious_Click(sender As Object, e As EventArgs) Handles ButtonPrevious.Click
        TabControl1.SelectedIndex -= 1
    End Sub

    Private Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        TabControl1.SelectedIndex += 1
    End Sub

    Private Sub ButtonEnd_Click(sender As Object, e As EventArgs) Handles ButtonEnd.Click
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles _
        RadioButtonVisa.CheckedChanged,
         RadioButtonDirectDebit.CheckedChanged,
          RadioButtonCreditTransfer.CheckedChanged

        If _AllowEvents Then
            SetPaymentTerms()
            SetCcaConcept()
        End If

    End Sub

    Private Sub Xl_BancsComboBox1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_BancsComboBox1.ValueChanged
        If _AllowEvents Then
            SetCcaConcept()
        End If
    End Sub

    Private Async Sub ButtonSepaCreditTransfer_Click(sender As Object, e As EventArgs) Handles ButtonSepaCreditTransfer.Click
        Dim exs As New List(Of Exception)
        Dim sFilename As String = _BancTransferPool.DefaultFilename(_Proveidor.Nom)
        Dim XmlSource As String = Await FEB.SepaCreditTransfer.XML(Current.Session.Emp, _BancTransferPool, exs)
        If exs.Count = 0 Then
            If UIHelper.SaveXmlFileDialog(XmlSource, sFilename) Then
                TextBoxSepaCreditTransfer.Text = sFilename
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonEmail_Click(sender As Object, e As EventArgs) Handles ButtonEmail.Click
        Dim exs As New List(Of Exception)
        If Not Await MatOutlook.RemittanceAdvice(_BancTransferPool.Beneficiaris.First, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonSaveTransferRef_Click(sender As Object, e As EventArgs) Handles ButtonSaveTransferRef.Click
        Dim exs As New List(Of Exception)
        _BancTransferPool.Ref = TextBoxBancTransferPoolRef.Text
        If Await FEB.BancTransferPool.SaveRef(_BancTransferPool, exs) Then
            ButtonSaveTransferRef.Enabled = False
            TextBoxBancTransferPoolRef.ReadOnly = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TextBoxBancTransferPoolRef_TextChanged(sender As Object, e As EventArgs) Handles TextBoxBancTransferPoolRef.TextChanged
        ButtonSaveTransferRef.Enabled = True
    End Sub



#End Region

End Class