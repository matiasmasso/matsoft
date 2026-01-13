Public Class Frm_OldHighResImage
    Private _HighResImage As HighResImage
    Private _DirtyThumbnail As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oValue As HighResImage)
        MyBase.New()
        Me.InitializeComponent()
        _HighResImage = oValue

        With _HighResImage
            TextBoxUrl.Text = .Url
            TextBoxFeatures.Text = .Features
            Xl_ImageCrop1.Load(.Thumbnail)
            Xl_Products1.Load(HighResImageLoader.Products(_HighResImage))
            ButtonDel.Enabled = .IsLoaded
        End With

    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        _HighResImage.Products = Xl_Products1.Values
        If _DirtyThumbnail Then _HighResImage.Thumbnail = Xl_ImageCrop1.Value

        Dim exs As New List(Of exception)
        If HighResImageLoader.Update(_HighResImage, exs) Then
            RaiseEvent afterupdate(Me, New MatEventArgs(_HighResImage))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar la imatge")
        End If
    End Sub

    Private Sub Xl_Products1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Products1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of exception)
        If HighResImageLoader.Delete(_HighResImage, exs) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al eliminar la imatge")
        End If
    End Sub

    Private Sub Xl_ImageCrop1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ImageCrop1.RequestToRefresh
        Cursor = Cursors.WaitCursor
        Dim exs As New List(Of exception)
        Dim oImage As Image = BLL.ImageHelper.DownloadFromFtpsite(_HighResImage.Url, App.Current.FtpClient)
        Cursor = Cursors.Default

        Xl_ImageCrop1.Load(oImage)
        _DirtyThumbnail = True
    End Sub

    Private Sub Xl_ImageCrop1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ImageCrop1.ValueChanged
        _DirtyThumbnail = True
        ButtonOk.Enabled = True
    End Sub
End Class