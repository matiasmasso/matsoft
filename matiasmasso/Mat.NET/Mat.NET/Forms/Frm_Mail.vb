Public Class Frm_Mail

    Private _Mail As DTOCorrespondencia
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCorrespondencia)
        MyBase.New()
        Me.InitializeComponent()
        _Mail = value
        BLL.BLLMail.Load(_Mail)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Mail
            DateTimePicker1.Value = .Fch
            TextBoxAtn.Text = .Atn
            TextBoxSubject.Text = .Subject
            Select Case .Cod
                Case DTO.DTOCorrespondencia.Cods.Enviat
                    RadioButtonSent.Checked = True
                Case Else
                    RadioButtonReceived.Checked = True
            End Select
            Xl_Contacts1.Load(.Contacts)
            Xl_DocFile1.Load(.DocFile)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxAtn.TextChanged, _
         TextBoxSubject.TextChanged, _
          DateTimePicker1.ValueChanged, _
           RadioButtonSent.CheckedChanged, _
            RadioButtonReceived.CheckedChanged, _
             ButtonAddContact.Click, _
              Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Mail
            .Fch = DateTimePicker1.Value
            .Atn = TextBoxAtn.Text
            .Subject = TextBoxSubject.Text
            .Cod = IIf(RadioButtonSent.Checked, DTO.DTOCorrespondencia.Cods.Enviat, DTO.DTOCorrespondencia.Cods.Rebut)
            .DocFile = Xl_DocFile1.Value
            .Contacts = Xl_Contacts1.Values
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLMail.Update(_Mail, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Mail))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLMail.Delete(_Mail, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Mail))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


