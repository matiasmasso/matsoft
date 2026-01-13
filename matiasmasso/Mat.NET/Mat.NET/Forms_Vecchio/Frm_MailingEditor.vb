Public Class Frm_MailingEditor

    Private _Mailing As Mailing
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As Mailing)
        MyBase.New()
        Me.InitializeComponent()
        _Mailing = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Mailing
            If Not .IsNew And Not .IsLoaded Then MailingLoader.Load(_Mailing)
            DateTimePicker1.Value = .Fch
            TextBoxSubject.Text = .Subject
            TextBoxBody.Text = .Body
            Xl_MailingParameters1.Load(.Parameters)
            Xl_MailingRecipients1.Load(.Recipients)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles _
         DateTimePicker1.ValueChanged, _
          TextBoxSubject.TextChanged, _
           TextBoxBody.TextChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        With _Mailing
            .Fch = DateTimePicker1.Value
            .Subject = TextBoxSubject.Text
            .Body = TextBoxBody.Text
            .Parameters = Xl_MailingParameters1.values
            .Recipients = Xl_MailingRecipients1.values
        End With
        If Mailingloader.Update(_Mailing, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Mailing))
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar la editorial")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("eliminem el mailing?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Mailingloader.Delete(_Mailing, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Mailing))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar la editorial")
            End If
        End If
    End Sub
End Class



