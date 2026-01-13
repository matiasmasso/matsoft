Public Class Frm_Login

    Public Enum Icons
        Info
        Warning
    End Enum

    Private Sub Frm_Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        LabelVersion.Text = version()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim sEmail As String = TextBoxEmail.Text
        Dim sPassword As String = TextBoxPwd.Text
        Dim oUser As DTOUser = Nothing
        Dim oResult As BLL.BLLUser.ValidationResults = BLL.BLLUser.ValidatePassword(sEmail, sPassword, oUser)

        Select Case oResult
            Case BLL_Usuari.ValidationResults.EmptyEmail
                WarnUser(Icons.Warning, "si us plau omple la casella de correu")
            Case BLL_Usuari.ValidationResults.WrongEmail
                WarnUser(Icons.Warning, "l'email no está escrit correctament")
            Case BLL_Usuari.ValidationResults.EmailNotRegistered
                WarnUser(Icons.Warning, "email no registrat")
            Case BLL_Usuari.ValidationResults.EmptyPassword
                WarnUser(Icons.Info, "password enviat a " & sEmail)
            Case BLL_Usuari.ValidationResults.WrongPassword
                WarnUser(Icons.Warning, "password incorrecte")
            Case BLL_Usuari.ValidationResults.Success
                Session.Initialize(oUser, CheckBoxPersist.Checked)
                Me.Close()
        End Select

    End Sub

    Public Function version() As String
        Dim retval As String = ""
        Try
            If My.Application.IsNetworkDeployed Then
                With My.Application.Deployment.CurrentVersion
                    retval = .Major & "." & .Minor & "." & .Revision
                End With
            Else
                retval = "(versió de desenvolupador)"
            End If
        Catch ex As Exception

        End Try
        Return retval
    End Function

    Private Sub WarnUser(oIcon As Icons, sMessage As String)
        PictureBoxWarn.Visible = True
        LabelWarnUser.Visible = True
        LabelWarnUser.Text = sMessage

        Select Case oIcon
            Case Icons.Info
                PictureBoxWarn.Image = My.Resources.info
                LabelWarnUser.ForeColor = Color.Navy
            Case Icons.Warning
                PictureBoxWarn.Image = My.Resources.warn
                LabelWarnUser.ForeColor = Color.Red
        End Select
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Application.Exit()
    End Sub

    Private Sub LinkLabelRemindPassword_Click(sender As Object, e As EventArgs) Handles LinkLabelRemindPassword.Click
        Dim sEmail As String = TextBoxEmail.Text
        If sEmail = "" Then
            WarnUser(Icons.Warning, "si us plau omple la casella de correu")
        Else
            Dim oUser As User = User.FromEmailAddress(sEmail)
            If oUser Is Nothing Then
                WarnUser(Icons.Warning, "email no registrat")
            Else
                oUser.EmailPwd()
                WarnUser(Icons.Info, "password enviat a " & sEmail)
            End If
        End If
    End Sub

    Private Sub TextBoxEmail_TextChanged(sender As Object, e As EventArgs) Handles TextBoxEmail.TextChanged
        Dim value As String = TextBoxEmail.Text
        If MaxiSrvr.IsValidEmailAddress(value) Then
            TextBoxPwd.Enabled = True
            CheckBoxPersist.Enabled = True
            ButtonOk.Enabled = True
        Else
            TextBoxPwd.Enabled = False
            CheckBoxPersist.Enabled = False
            ButtonOk.Enabled = False
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        UIHelper.ShowHtml("http://www.matiasmasso.es")
    End Sub


End Class