Public Class Xl_ImageCrop

    Private _ThumbnailSource As Bitmap
    Private _rectangle As Rectangle
    Private _mouseLocation As Point

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oImage As Image)
        _ThumbnailSource = BLL.ImageHelper.GetThumbnailToFit(oImage, PictureBox1.Width, PictureBox1.Height)
        _rectangle = New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height)
        refresca()
        SetContextMenu()
    End Sub

    Public ReadOnly Property Value As Bitmap
        Get
            Return PictureBox1.Image
        End Get
    End Property

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        _mouseLocation = e.Location
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left And _ThumbnailSource IsNot Nothing Then
            MoveRectangle(_mouseLocation.X - e.X, _mouseLocation.Y - e.Y)
            refresca()
            _mouseLocation = e.Location
            RaiseEvent ValueChanged(Me, New MatEventArgs(PictureBox1.Image))
        End If
    End Sub

    Private Sub refresca()
        PictureBox1.Image = _ThumbnailSource.Clone(_rectangle, _ThumbnailSource.PixelFormat)
    End Sub

    Private Sub MoveRectangle(DeltaX As Integer, DeltaY As Integer)
        If (_rectangle.X + _rectangle.Width + DeltaX) > _ThumbnailSource.Width Then DeltaX = 0
        If (_rectangle.X + DeltaX) <= 0 Then DeltaX = 0
        If (_rectangle.Y + _rectangle.Height + DeltaY) > _ThumbnailSource.Height Then DeltaY = 0
        If (_rectangle.Y + DeltaY) <= 0 Then DeltaY = 0

        Dim X As Integer = _rectangle.X + DeltaX
        Dim Y As Integer = _rectangle.Y + DeltaY
        Dim iWidth As Integer = _rectangle.Width
        Dim iHeight As Integer = _rectangle.Height
        _rectangle = New Rectangle(X, Y, iWidth, iHeight)
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If Me.Value IsNot Nothing Then
            oContextMenu.Items.Add(New ToolStripMenuItem("refrescar", My.Resources.refresca, AddressOf Do_Refresh))
            'oContextMenu.Items.Add("-")
        End If

        PictureBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Refresh()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

End Class
