Public Class Frm_Iban
    Private _Iban As DTOIban
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOIban)
        MyBase.New()
        Me.InitializeComponent()
        _Iban = value
       BLL.BLLIban.Load(_Iban)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        UIHelper.LoadComboFromEnum(ComboBoxFormat, GetType(DTOIban.Formats))
        With _Iban
            TextBoxGuid.Text = .Guid.ToString
            Xl_Contact21.Contact = .Titular
            Xl_IbanTextbox1.Value = _Iban.Digits
            Xl_Lookup_BankBranch1.Load(_Iban)
            ComboBoxFormat.SelectedIndex = .Format
            DateTimePickerFchFrom.MinDate = New Date(1985, 5, 28)
            If .FchFrom <= DateTimePickerFchFrom.MinDate Then
                DateTimePickerFchFrom.Value = DateTimePickerFchFrom.MinDate
            Else
                DateTimePickerFchFrom.Value = .FchFrom
            End If
            If .FchTo > DateTimePickerFchTo.MinDate Then
                CheckBoxFchTo.Checked = True
                DateTimePickerFchTo.Visible = True
                DateTimePickerFchTo.Value = .FchTo
            End If

            If .UsrDownloaded Is Nothing Then
                LabelDownloaded.Text = "pendent de descarregar"
                LabelUploaded.Visible = False
                LabelApproved.Visible = False
                ButtonApprove.Visible = False
            Else
                LabelDownloaded.Text = "descarregat a " & Format(.FchDownloaded, "dd/MM/yy HH:mm") & " per " & BLL.BLLUser.NicknameOrElse(.UsrDownloaded)
                LabelUploaded.Visible = True

                If .UsrUploaded Is Nothing Then
                    LabelUploaded.Text = "pendent de signar"
                    LabelApproved.Visible = False
                    ButtonApprove.Visible = False
                Else
                    LabelUploaded.Text = "signat a " & Format(.FchUploaded, "dd/MM/yy HH:mm") & " per " & BLL.BLLUser.NicknameOrElse(.UsrUploaded)
                    LabelApproved.Visible = False

                    If .UsrApproved Is Nothing Then
                        LabelApproved.Text = "pendent de validar"
                        ButtonApprove.Visible = True
                    Else
                        LabelApproved.Text = "validat a " & Format(.FchUploaded, "dd/MM/yy HH:mm") & " per " & BLL.BLLUser.NicknameOrElse(.UsrUploaded)
                        ButtonApprove.Visible = False
                    End If
                End If
            End If



            Xl_DocFile1.Load(.DocFile)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_Contact21.AfterUpdate, _
         Xl_Lookup_BankBranch1.AfterUpdate, _
          ComboBoxFormat.SelectedIndexChanged, _
           DateTimePickerFchFrom.ValueChanged, _
            DateTimePickerFchTo.ValueChanged, _
              Xl_DocFile1.AfterFileDropped

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        save()
    End Sub

    Private Sub Save()
        Dim exs as New List(Of exception)
        With _Iban
            .Titular = Xl_Contact21.Contact
            .Digits = BLL.BLLIbanStructure.CleanCcc(Xl_IbanTextbox1.Value)
            .BankBranch = Xl_Lookup_BankBranch1.Branch
            .Format = ComboBoxFormat.SelectedIndex
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxFchTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Date.MinValue
            End If
            .DocFile = Xl_DocFile1.Value
        End With

        Dim sWarningMessage As String = ""
        if BLL.BLLIban.CheckIfOthersMatchingTimeSpan(_Iban, sWarningMessage) Then
            MsgBox("error al desar la domiciliació:" & vbCrLf & sWarningMessage, MsgBoxStyle.Exclamation)
        Else
            if BLL.BLLIban.Update(_Iban, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Iban))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al desar la domiciliació")
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLIban.Delete(_Iban, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Iban))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_IbanTextbox1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_IbanTextbox1.ValueChanged
        If _AllowEvent Then
            ButtonOk.Enabled = True
            'Dim oBankBranch As DTOBankBranch = BLL.BLLBankBranch.FromDigits(Xl_IbanTextbox1.Value)
            Xl_Lookup_BankBranch1.Load(Xl_IbanTextbox1.Value)
        End If

    End Sub

    Private Sub CheckBoxFchTo_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchTo.CheckedChanged
        If _AllowEvent Then
            DateTimePickerFchTo.Visible = CheckBoxFchTo.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonApprove_Click(sender As Object, e As EventArgs) Handles ButtonApprove.Click
        With _Iban
            .UsrApproved = BLL.BLLSession.Current.User
            .FchApproved = Now
        End With

        Save()
    End Sub
End Class