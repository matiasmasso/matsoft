Public Class Frm_GalleryItem
    Private _GalleryItem As DTOGalleryItem
    Private _allowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Public Sub New(oGalleryItem As DTOGalleryItem)
        MyBase.New()
        Me.InitializeComponent()
        _GalleryItem = oGalleryItem
    End Sub

    Private Async Sub Frm_GalleryItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.GalleryItem.Load(_GalleryItem, exs) Then
            Await Refresca()
            _allowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        With _GalleryItem
            TextBoxNom.Text = .Nom
            If Not .IsNew Then
                Xl_Image1.Bitmap = LegacyHelper.ImageHelper.Converter(Await FEB2.GalleryItem.Image(.Guid, exs))
                TextBoxFeatures.Text = DTOGalleryItem.MultilineText(_GalleryItem)
            End If
            If exs.Count = 0 Then
                ButtonDel.Enabled = Not .IsNew
            Else
                UIHelper.WarnError(exs)
            End If
        End With
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _GalleryItem
            .Nom = TextBoxNom.Text
            .Image = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB2.GalleryItem.Update(_GalleryItem, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_GalleryItem))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
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

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.GalleryItem.Delete(_GalleryItem, exs) Then
            RaiseEvent AfterUpdate(_GalleryItem, EventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Image1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Image1.AfterUpdate

        With _GalleryItem
            .Image = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
            .Height = .Image.Height
            .Width = .Image.Width
            Dim oImageBytes As Byte() = MatHelperStd.ImageHelper.GetByteArrayFromImg(.Image)
            .Size = oImageBytes.Length / 1000
            .Mime = Xl_Image1.MimeCod() '= LegacyHelper.ImageHelper.GetImageMimeCod(.Image)
        End With
        TextBoxFeatures.Text = DTOGalleryItem.MultilineText(_GalleryItem)
    End Sub


End Class