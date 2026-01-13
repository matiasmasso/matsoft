Public Class Xl_StreetView

    Private _StreetView As DTOStreetView
    Private _mouseDownLocation As Point

    Public Shadows Sub Load(oAddress As DTOAddress)
        Dim exs As New List(Of Exception)
        _StreetView = DTOStreetView.Factory(oAddress, PictureBoxView.Size.Width, PictureBoxView.Size.Height)
        Dim bytes = FEB.FetchBinarySync(exs, _StreetView.Url())
        PictureBoxView.Image = LegacyHelper.ImageHelper.FromBytes(bytes)
    End Sub

    Private Sub Picturebox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBoxView.MouseDown
        Select Case e.Button
            Case MouseButtons.Left
                _mouseDownLocation = New Point(e.X, e.Y)
        End Select
    End Sub

    Private Sub Picturebox1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBoxView.MouseMove
        Dim exs As New List(Of Exception)
        'comentat el 27/9/22 perque es penja
        'If _mouseDownLocation <> Nothing Then
        '    If e.Button = MouseButtons.Left Then
        '        Dim deltaX As Integer = 10 * (_mouseDownLocation.X - e.X) / PictureBoxView.Width
        '        Dim iHeading As Integer = _StreetView.Heading + deltaX
        '        If iHeading > 360 Then iHeading -= 360
        '        If iHeading < 0 Then iHeading += 360
        '        _StreetView.Heading = iHeading

        '        Dim deltaY As Integer = -10 * (_mouseDownLocation.Y - e.Y) / PictureBoxView.Height
        '        Dim iPitch As Integer = _StreetView.Pitch + deltaY
        '        If iPitch > 90 Then iPitch = 90
        '        If iPitch < -90 Then iPitch = -90
        '        _StreetView.Pitch = iPitch

        '        Dim bytes = FEB.FetchImage(exs, _StreetView.Url()).Result
        '        PictureBoxView.Image = LegacyHelper.ImageHelper.FromBytes(bytes)
        '    Else
        '        _mouseDownLocation = Nothing
        '    End If
        'End If
    End Sub

    Private Sub Xl_StreetView_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        _mouseDownLocation = Nothing
    End Sub

    Private Sub Xl_StreetView_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        _mouseDownLocation = Nothing
    End Sub

    Private Sub PictureBoxZoomIn_Click(sender As Object, e As EventArgs) Handles PictureBoxZoomIn.Click
        Dim exs As New List(Of Exception)
        _mouseDownLocation = Nothing
        _StreetView.Zoom -= 10
        Dim bytes = FEB.FetchImage(exs, _StreetView.Url()).Result
        PictureBoxView.Image = LegacyHelper.ImageHelper.FromBytes(bytes)
    End Sub

    Private Sub PictureBoxZoomOut_Click(sender As Object, e As EventArgs) Handles PictureBoxZoomOut.Click
        Dim exs As New List(Of Exception)
        _mouseDownLocation = Nothing
        _StreetView.Zoom += 10
        If _StreetView.Zoom < 1 Then _StreetView.Zoom = 1
        Dim bytes = FEB.FetchImage(exs, _StreetView.Url()).Result
        PictureBoxView.Image = LegacyHelper.ImageHelper.FromBytes(bytes)
    End Sub
End Class

