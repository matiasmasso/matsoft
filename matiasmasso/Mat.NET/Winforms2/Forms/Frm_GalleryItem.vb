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
        UIHelper.ToggleProggressBar(Panel1, True)
        If FEB.GalleryItem.Load(_GalleryItem, exs) Then
            Await Refresca()
            _allowEvents = True
            UIHelper.ToggleProggressBar(Panel1, False)
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        With _GalleryItem
            TextBoxNom.Text = .Nom
            If Not .IsNew Then
                Dim oImageBytes = Await FEB.GalleryItem.Image(.Guid, exs)
                If oImageBytes IsNot Nothing Then
                    Xl_ImageMime1.Load(oImageBytes, _GalleryItem.Mime)
                End If
                TextBoxFeatures.Text = DTOGalleryItem.MultilineText(_GalleryItem)
            End If
            If exs.Count = 0 Then
                ButtonDel.Enabled = Not .IsNew
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
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
            If Xl_ImageMime1.Value Is Nothing Then
                .Image = Nothing
            Else
                Dim ms = New IO.MemoryStream
                .Image = Xl_ImageMime1.Value.ByteArray
                .Mime = Xl_ImageMime1.Value.Mime
            End If

            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB.GalleryItem.Update(_GalleryItem, exs) Then
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
        If Await FEB.GalleryItem.Delete(_GalleryItem, exs) Then
            RaiseEvent AfterUpdate(_GalleryItem, EventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ImageMime1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ImageMime1.AfterUpdate
        With _GalleryItem
            If Xl_ImageMime1.Value Is Nothing Then
                .Image = Nothing
            Else
                Dim ms As New IO.MemoryStream(Xl_ImageMime1.Value.ByteArray)
                Dim oImage = Image.FromStream(ms)
                .Image = Xl_ImageMime1.Value.ByteArray
                .Mime = Xl_ImageMime1.Value.Mime
                .Height = oImage.Height
                .Width = oImage.Width
                .Size = Xl_ImageMime1.Value.ByteArray.Length / 1000
            End If
        End With
        TextBoxFeatures.Text = DTOGalleryItem.MultilineText(_GalleryItem)
        ButtonOk.Enabled = True
    End Sub
End Class