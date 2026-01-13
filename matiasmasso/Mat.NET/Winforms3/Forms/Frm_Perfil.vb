Imports LegacyHelper

Public Class Frm_Perfil

    Private Sub Frm_Perfil_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oUser As DTOUser = Current.Session.User
        TextBoxEmail.Text = oUser.EmailAddress
        TextBoxRol.Text = oUser.Rol.id.ToString

        Dim exs As New List(Of Exception)
        Dim url = GravatarHelper.Url(TextBoxEmail.Text)
        Dim oImgBytes = FEB.FetchImage(exs, url).Result
        PictureBoxGravatar.Image = LegacyHelper.ImageHelper.FromBytes(oImgBytes)

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class