Public Class Frm_SatRecall

    Private _SatRecall As DTOSatRecall
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOSatRecall)
        MyBase.New()
        Me.InitializeComponent()
        _SatRecall = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.SatRecall.Load(_SatRecall, exs) Then
            With _SatRecall
                TextBoxIncidencia.Text = DTOIncidencia.MultilineText(.Incidencia)
                TextBoxDefect.Text = .Defect
                TextBoxContactPerson.Text = .ContactPerson
                TextBoxTel.Text = .Tel
                ComboBoxPickupFrom.SelectedIndex = .PickupFrom
                If .FchCustomer <> Nothing Then
                    CheckBoxFchCustomer.Checked = True
                    DateTimePickerCustomer.Visible = True
                    DateTimePickerCustomer.Value = .FchCustomer
                End If
                If .FchManufacturer <> Nothing Then
                    CheckBoxFchManufacturer.Checked = True
                    DateTimePickerManufacturer.Visible = True
                    DateTimePickerManufacturer.Value = .FchManufacturer
                End If
                If .Mode = DTOSatRecall.Modes.PerAbonar Then
                    If .CreditFch <> Nothing Or .CreditNum > "" Then
                        CheckBoxCredit.Checked = True
                        TextBoxCreditNum.Visible = True
                        DateTimePickerCredit.Visible = True
                        TextBoxCreditNum.Text = .CreditNum
                        DateTimePickerCredit.Value = .CreditFch
                    End If
                Else
                    CheckBoxCredit.Visible = False
                    TextBoxCreditNum.Visible = False
                    DateTimePickerCredit.Visible = False
                    LabelCreditNum.Visible = False
                    LabelCreditFch.Visible = False
                End If
                TextBoxPickupRef.Text = .PickupRef
                If .Address IsNot Nothing Then
                    TextBoxAdr.Text = .Address.Text
                    Xl_LookupZip1.Load(.Address.Zip)
                End If
                RadioButtonRepara.Checked = .Mode = DTOSatRecall.Modes.PerReparar
                RadioButtonAbona.Checked = .Mode = DTOSatRecall.Modes.PerAbonar
                If Not String.IsNullOrEmpty(.ReturnRef) Or .ReturnFch > DateTimePickerReturn.MinDate Or .Carrec = True Then
                    CheckBoxReturned.Checked = True
                    TextBoxReturnRef.Text = .ReturnRef
                    If .ReturnFch > DateTimePickerReturn.MinDate Then
                        DateTimePickerReturn.Value = .ReturnFch
                    End If
                    CheckBoxCarrec.Checked = .Carrec
                End If
                TextBoxObs.Text = .Obs
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles ComboBoxPickupFrom.SelectedIndexChanged,
              DateTimePickerCustomer.ValueChanged,
                DateTimePickerManufacturer.ValueChanged,
                 TextBoxPickupRef.TextChanged,
                  CheckBoxReturned.CheckedChanged,
                   TextBoxReturnRef.TextChanged,
                    DateTimePickerReturn.ValueChanged,
                     CheckBoxCarrec.CheckedChanged,
                  TextBoxCreditNum.TextChanged,
                   DateTimePickerCredit.ValueChanged,
                    TextBoxDefect.TextChanged,
                    TextBoxObs.TextChanged,
                     TextBoxAdr.TextChanged,
                      Xl_LookupZip1.AfterUpdate,
                       TextBoxContactPerson.TextChanged,
                        TextBoxTel.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub RadioButtonRepara_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonRepara.CheckedChanged
        CheckBoxCredit.Visible = RadioButtonAbona.Checked
        TextBoxCreditNum.Visible = RadioButtonAbona.Checked And CheckBoxCredit.Checked
        DateTimePickerCredit.Visible = RadioButtonAbona.Checked And CheckBoxCredit.Checked
        LabelCreditNum.Visible = RadioButtonAbona.Checked And CheckBoxCredit.Checked
        LabelCreditFch.Visible = RadioButtonAbona.Checked And CheckBoxCredit.Checked

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _SatRecall
            .Defect = TextBoxDefect.Text
            .PickupFrom = ComboBoxPickupFrom.SelectedIndex
            .FchCustomer = IIf(CheckBoxFchCustomer.Checked, DateTimePickerCustomer.Value, Nothing)
            .FchManufacturer = IIf(CheckBoxFchManufacturer.Checked, DateTimePickerManufacturer.Value, Nothing)
            .ContactPerson = TextBoxContactPerson.Text
            .Tel = TextBoxTel.Text
            .PickupRef = TextBoxPickupRef.Text
            .Mode = IIf(RadioButtonRepara.Checked, DTOSatRecall.Modes.PerReparar, DTOSatRecall.Modes.PerAbonar)
            .ReturnRef = IIf(CheckBoxReturned.Checked, TextBoxReturnRef.Text, "")
            .ReturnFch = IIf(CheckBoxReturned.Checked, DateTimePickerReturn.Value, Nothing)
            .Carrec = IIf(CheckBoxReturned.Checked, CheckBoxCarrec.Checked, False)
            .CreditFch = IIf(CheckBoxCredit.Checked, DateTimePickerCredit.Value, Nothing)
            .CreditNum = IIf(CheckBoxCredit.Checked, TextBoxCreditNum.Text, "")
            .Address = New DTOAddress
            With .Address
                .Text = TextBoxAdr.Text
                .Zip = Xl_LookupZip1.Zip
            End With
            .Obs = TextBoxObs.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.SatRecall.Update(_SatRecall, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_SatRecall))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.SatRecall.Delete(_SatRecall, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_SatRecall))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub



    Private Sub CheckBoxCredit_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCredit.CheckedChanged
        If _AllowEvents Then
            LabelCreditNum.Visible = CheckBoxCredit.Checked
            TextBoxCreditNum.Visible = CheckBoxCredit.Checked
            LabelCreditFch.Visible = CheckBoxCredit.Checked
            DateTimePickerCredit.Visible = CheckBoxCredit.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxFchCustomer_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchCustomer.CheckedChanged
        If _AllowEvents Then
            DateTimePickerCustomer.Visible = CheckBoxFchCustomer.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxFchManufacturer_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchManufacturer.CheckedChanged
        If _AllowEvents Then
            DateTimePickerManufacturer.Visible = CheckBoxFchManufacturer.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub EmailAlClientToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmailAlClientToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim recallFormFilename As String = ""
        If FEB.SatRecall.Load(_SatRecall, exs) Then
            LegacyHelper.SatRecallLegacyHelper.FillForm(_SatRecall, recallFormFilename, exs)
            If exs.Count = 0 Then
                Dim oMailMessage As DTOMailMessage = Await FEB.SatRecall.MailMessageToCustomer(_SatRecall, recallFormFilename, exs)
                If exs.Count = 0 Then
                    If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                        UIHelper.WarnError(exs, "error al enviar el missatge")
                    End If
                Else
                    UIHelper.WarnError(exs, "error al redactar el missatge")
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub
    Private Async Sub EmailAlFabricantToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmailAlFabricantToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim recallFormFilename As String = ""
        LegacyHelper.SatRecallLegacyHelper.FillForm(_SatRecall, recallFormFilename, exs)
        If exs.Count = 0 Then
            Dim oMailMessage As DTOMailMessage = Await FEB.SatRecall.MailMessageToManufacturer(GlobalVariables.Emp, _SatRecall, recallFormFilename, exs)
            If exs.Count = 0 Then
                If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                    UIHelper.WarnError(exs, "error al enviar el missatge")
                End If
            Else
                UIHelper.WarnError(exs, "error al redactar el missatge")
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub


    Private Sub FormulariFabricantToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FormulariFabricantToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim sFilename As String = ""
        If FEB.SatRecall.Load(_SatRecall, exs) Then
            If LegacyHelper.SatRecallLegacyHelper.FillForm(_SatRecall, sFilename, exs) Then
                Process.Start(sFilename)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub CheckBoxReturned_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxReturned.CheckedChanged
        LabelReturnRef.Visible = CheckBoxReturned.Checked
        TextBoxReturnRef.Visible = CheckBoxReturned.Checked
        LabelReturnFch.Visible = CheckBoxReturned.Checked
        DateTimePickerReturn.Visible = CheckBoxReturned.Checked
        CheckBoxCarrec.Visible = CheckBoxReturned.Checked
    End Sub


End Class


