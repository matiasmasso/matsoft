Imports GMap.NET.WindowsForms
Imports GMap.NET

Public Class Xl_GoogleMaps

    Private _Map As DTOGoogleMap
    Private _mouseDownLocation As Point

    Public Shadows Sub Load(oMap As DTOGoogleMap)
        Dim markersOverlay As New GMapOverlay("markers")
        Dim marker As New Markers.GMarkerGoogle(New PointLatLng(oMap.Latitud, oMap.Longitud), Markers.GMarkerGoogleType.red_pushpin)
        markersOverlay.Markers.Add(marker)

        _Map = oMap
        With GMapControl1
            .MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance
            '.MapProvider = GMap.NET.MapProviders.GMapProviders.OpenStreetMap
            .MinZoom = oMap.MinZoom
            .MaxZoom = oMap.MaxZoom
            .Zoom = oMap.Zoom
            .Position = New GMap.NET.PointLatLng(oMap.Latitud, oMap.Longitud)
            .DragButton = MouseButtons.Left
            .Overlays.Clear()
            .Overlays.Add(markersOverlay)
        End With

    End Sub

    Private Sub GMapControl1_DoubleClick(sender As Object, e As EventArgs) Handles GMapControl1.DoubleClick
        Dim url As String = GoogleMapsHelper.Url(_Map.Latitud, _Map.Longitud)
        UIHelper.ShowHtml(url)
    End Sub



    Private Sub PictureBoxZoomIn_Click(sender As Object, e As EventArgs) Handles PictureBoxZoomIn.Click
        _Map.Zoom += 1
        If _Map.Zoom >= _Map.MaxZoom Then _Map.Zoom = _Map.MaxZoom
        GMapControl1.Zoom = _Map.Zoom
    End Sub

    Private Sub PictureBoxZoomOut_Click(sender As Object, e As EventArgs) Handles PictureBoxZoomOut.Click
        _Map.Zoom -= 1
        If _Map.Zoom <= _Map.MinZoom Then _Map.Zoom = _Map.MinZoom
        GMapControl1.Zoom = _Map.Zoom
    End Sub
End Class
