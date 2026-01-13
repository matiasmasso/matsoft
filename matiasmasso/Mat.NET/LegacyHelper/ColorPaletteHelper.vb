Public Class ColorPaletteHelper
    'not compatible with .NET Standard due to reference to legacy system.drawing

    Public Enum Palettes
        None
        GrayScale
        [Default]
        Excel
        colorsBrightPastel
        Fire
    End Enum

    Shared Function Colors(Optional oPalette As Palettes = Palettes.Default) As List(Of Color)
        'mes a https://referencesource.microsoft.com/#System.Web.DataVisualization/Common/Utilities/ColorPalette.cs,b2797498e3fdc9a8,references
        Dim retval As New List(Of Color)
        Select Case oPalette
            Case Palettes.GrayScale
                For i As Integer = 0 To 15
                    Dim colorValue As Integer = 200 - i * (180 / 16)
                    retval.Add(Color.FromArgb(colorValue, colorValue, colorValue))
                Next
            Case Palettes.Default
                retval = {Color.Green, Color.Blue, Color.Purple, Color.Lime, Color.Fuchsia, Color.Teal, Color.Yellow, Color.Gray, Color.Aqua, Color.Navy, Color.Maroon, Color.Red, Color.Olive, Color.Silver, Color.Tomato, Color.Moccasin}.ToList

            Case Palettes.Excel
                retval = {Color.FromArgb(153, 153, 255), Color.FromArgb(153, 51, 102), Color.FromArgb(255, 255, 204), Color.FromArgb(204, 255, 255), Color.FromArgb(102, 0, 102), Color.FromArgb(255, 128, 128), Color.FromArgb(0, 102, 204), Color.FromArgb(204, 204, 255), Color.FromArgb(0, 0, 128), Color.FromArgb(255, 0, 255), Color.FromArgb(255, 255, 0), Color.FromArgb(0, 255, 255), Color.FromArgb(128, 0, 128), Color.FromArgb(128, 0, 0), Color.FromArgb(0, 128, 128), Color.FromArgb(0, 0, 255)}.ToList
            Case Palettes.colorsBrightPastel
                retval = {Color.FromArgb(65, 140, 240), Color.FromArgb(252, 180, 65), Color.FromArgb(224, 64, 10), Color.FromArgb(5, 100, 146), Color.FromArgb(191, 191, 191), Color.FromArgb(26, 59, 105), Color.FromArgb(255, 227, 130), Color.FromArgb(18, 156, 221), Color.FromArgb(202, 107, 75), Color.FromArgb(0, 92, 219), Color.FromArgb(243, 210, 136), Color.FromArgb(80, 99, 129), Color.FromArgb(241, 185, 168), Color.FromArgb(224, 131, 10), Color.FromArgb(120, 147, 190)}.ToList
            Case Palettes.Fire
                retval = {Color.Gold, Color.Red, Color.DeepPink, Color.Crimson, Color.DarkOrange, Color.Magenta, Color.Yellow, Color.OrangeRed, Color.MediumVioletRed, Color.FromArgb(221, 226, 33)}.ToList
        End Select
        Return retval
    End Function

    Private Shared Function InitializeGrayScaleColors() As Color()
        Dim grayScale As Color() = New Color(15) {}
        For i As Integer = 0 To grayScale.Length - 1
            Dim colorValue As Integer = 200 - i * (180 / 16)
            grayScale(i) = Color.FromArgb(colorValue, colorValue, colorValue)
        Next

        Return grayScale
    End Function
End Class
