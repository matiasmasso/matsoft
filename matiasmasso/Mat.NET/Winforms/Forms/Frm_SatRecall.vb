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
        If FEB2.SatRecall.Load(_SatRecall, exs) Then
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
                If .CreditFch <> Nothing Or .CreditNum > "" Then
                    CheckBoxCredit.Checked = True
                    TextBoxCreditNum.Visible = True
                    DateTimePickerCredit.Visible = True
                    TextBoxCreditNum.Text = .CreditNum
                    DateTimePickerCredit.Value = .CreditFch
                End If
                TextBoxPickupRef.Text = .PickupRef
                If .Address IsNot Nothing Then
                    TextBoxAdr.Text = .Address.Text
                    Xl_LookupZip1.Load(.Address.Zip)
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

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _SatRecall
            .Defect = TextBoxDefect.Text
            .PickupFrom = ComboBoxPickupFrom.SelectedIndex
            .FchCustomer = IIf(CheckBoxFchCustomer.Checked, DateTimePickerCustomer.Value, Nothing)
            .FchManufacturer = IIf(CheckBoxFchManufacturer.Checked, DateTimePickerManufacturer.Value, Nothing)
            .ContactPerson = TextBoxContactPerson.Text
            .Tel = TextBoxTel.Text
            .PickupRef = TextBoxPickupRef.Text
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
        If Await FEB2.SatRecall.Update(_SatRecall, exs) Then
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
            If Await FEB2.SatRecall.Delete(_SatRecall, exs) Then
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
        LegacyHelper.SatRecallLegacyHelper.FillForm(_SatRecall, recallFormFilename, exs)
        If exs.Count = 0 Then
            Dim oMailMessage As DTOMailMessage = Await FEB2.SatRecall.MailMessageToCustomer(_SatRecall, recallFormFilename, exs)
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
    Private Async Sub EmailAlFabricantToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmailAlFabricantToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim recallFormFilename As String = ""
        LegacyHelper.SatRecallLegacyHelper.FillForm(_SatRecall, recallFormFilename, exs)
        If exs.Count = 0 Then
            Dim oMailMessage As DTOMailMessage = Await FEB2.SatRecall.MailMessageToManufacturer(GlobalVariables.Emp, _SatRecall, recallFormFilename, exs)
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
        FEB2.Contact.Load(_SatRecall.Incidencia.Customer, exs)
        If exs.Count = 0 Then
            If LegacyHelper.SatRecallLegacyHelper.FillForm(_SatRecall, sFilename, exs) Then
                Process.Start(sFilename)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class


