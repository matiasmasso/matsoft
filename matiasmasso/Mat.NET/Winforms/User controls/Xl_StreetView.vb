Public Class Xl_StreetView

    Private _StreetView As DTOStreetView
    Private _mouseDownLocation As Point

    Public Shadows Sub Load(oAddress As DTOAddress)
        _StreetView = DTOStreetView.Factory(oAddress)
        _StreetView.Size = New SixLabors.ImageSharp.Size(PictureBoxView.Size.Width, PictureBoxView.Size.Height)
        PictureBoxView.Image = LegacyHelper.ImageHelper.Converter(DTOStreetView.Image(_StreetView))
    End Sub

    Private Sub Picturebox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBoxView.MouseDown
        Select Case e.Button
            Case MouseButtons.Left
                _mouseDownLocation = New Point(e.X, e.Y)
        End Select
    End Sub

    Private Sub Picturebox1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBoxView.MouseMove
        If _mouseDownLocation <> Nothing Then
            If e.Button = MouseButtons.Left Then
                Dim deltaX As Integer = 10 * (_mouseDownLocation.X - e.X) / PictureBoxView.Width
                Dim iHeading As Integer = _StreetView.Heading + deltaX
                If iHeading > 360 Then iHeading -= 360
                If iHeading < 0 Then iHeading += 360
                _StreetView.Heading = iHeading

                Dim deltaY As Integer = -10 * (_mouseDownLocation.Y - e.Y) / PictureBoxView.Height
                Dim iPitch As Integer = _StreetView.Pitch + deltaY
                If iPitch > 90 Then iPitch = 90
                If iPitch < -90 Then iPitch = -90
                _StreetView.Pitch = iPitch

                PictureBoxView.Image = LegacyHelper.ImageHelper.Converter(DTOStreetView.Image(_StreetView))
            Else
                _mouseDownLocation = Nothing
            End If
        End If
    End Sub

    Private Sub Xl_StreetView_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        _mouseDownLocation = Nothing
    End Sub

    Private Sub Xl_StreetView_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        _mouseDownLocation = Nothing
    End Sub

    Private Sub PictureBoxZoomIn_Click(sender As Object, e As EventArgs) Handles PictureBoxZoomIn.Click
        _mouseDownLocation = Nothing
        _StreetView.Zoom -= 10
        PictureBoxView.Image = LegacyHelper.ImageHelper.Converter(DTOStreetView.Image(_StreetView))
    End Sub

    Private Sub PictureBoxZoomOut_Click(sender As Object, e As EventArgs) Handles PictureBoxZoomOut.Click
        _mouseDownLocation = Nothing
        _StreetView.Zoom += 10
        If _StreetView.Zoom < 1 Then _StreetView.Zoom = 1
        PictureBoxView.Image = LegacyHelper.ImageHelper.Converter(DTOStreetView.Image(_StreetView))
    End Sub
End Class

