Public Class Frm_Perfil

    Private Sub Frm_Perfil_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oUser As DTOUser = Current.Session.User
        TextBoxEmail.Text = oUser.EmailAddress
        TextBoxRol.Text = oUser.Rol.Id.ToString

        PictureBoxGravatar.Image = LegacyHelper.ImageHelper.Converter(GravatarHelper.Image(TextBoxEmail.Text, ""))
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class