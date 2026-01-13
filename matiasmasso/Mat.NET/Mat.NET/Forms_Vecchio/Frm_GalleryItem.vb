Public Class Frm_GalleryItem
    Private _GalleryItem As DTOGalleryItem
    Private _allowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Public Sub New(oGalleryItem As DTOGalleryItem)
        MyBase.New()
        Me.InitializeComponent()
        _GalleryItem = oGalleryItem
        Refresca()
        _allowEvents = True
    End Sub

    Private Sub Refresca()
        With _GalleryItem
            TextBoxNom.Text = .Nom
            If .Image IsNot Nothing Then
                TextBoxFeatures.Text = BLL.BLLGalleryItem.MultilineText(_GalleryItem)
                Xl_Image1.Bitmap = .Image
            End If
            ButtonDel.Enabled = Not .IsNew
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _GalleryItem
            .Nom = TextBoxNom.Text
            .Image = Xl_Image1.Bitmap
            Dim exs as New List(Of exception)
            If BLL.BLLGalleryItem.Update(_GalleryItem, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_GalleryItem))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End With
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged

        If _allowevents Then
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If BLL.BLLGalleryItem.Delete(_GalleryItem, exs) Then
            RaiseEvent AfterUpdate(_GalleryItem, EventArgs.Empty)
            Me.Close()
        Else
            MsgBox(BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Xl_Image1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Image1.AfterUpdate

        With _GalleryItem
            .Image = Xl_Image1.Bitmap
            .Height = .Image.Height
            .Width = .Image.Width
            Dim oImageBytes As Byte() = BLL.ImageHelper.GetByteArrayFromImg(.Image)
            .Size = oImageBytes.Length / 1000
            .Mime = BLL.ImageHelper.GetMimeCodFromImage(.Image)
        End With
        TextBoxFeatures.Text = BLL.BLLGalleryItem.MultilineText(_GalleryItem)
    End Sub
End Class