Public Class Frm_Ftpserver
    Private _Ftpserver As DTOFtpserver
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOFtpserver)
        MyBase.New()
        Me.InitializeComponent()
        _Ftpserver = value
    End Sub

    Private Async Sub Frm_Ftpserver_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Ftpserver.Load(exs, _Ftpserver) Then
            With _Ftpserver
                TextBoxServerName.Text = .Servername
                Await Xl_Contact21.Load(.Owner)
                TextBoxUsername.Text = .Username
                TextBoxPassword.Text = .Password
                TextBoxPort.Text = .Port
                CheckBoxSsl.Checked = .SSL
                CheckBoxPassiveMode.Checked = .PassiveMode
                Xl_FtpserverPaths1.Load(.Paths)
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxServerName.TextChanged,
        Xl_Contact21.AfterUpdate,
        TextBoxUsername.TextChanged,
        TextBoxPassword.TextChanged,
        TextBoxPort.TextChanged,
        CheckBoxSsl.CheckedChanged,
        CheckBoxPassiveMode.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Ftpserver
            .Servername = TextBoxServerName.Text
            .Owner = Xl_Contact21.Contact
            .Username = TextBoxUsername.Text
            .Password = TextBoxPassword.Text
            .Port = TextBoxPort.Text
            .SSL = CheckBoxSsl.Checked
            .PassiveMode = CheckBoxPassiveMode.Checked
            .Paths = Xl_FtpserverPaths1.values
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB2.Ftpserver.Update(exs, _Ftpserver) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Ftpserver))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar el servidor Ftp")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB2.Ftpserver.Delete(exs, _Ftpserver) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Ftpserver))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


