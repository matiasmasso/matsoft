Imports System.Web.Script.Serialization
Imports Ghostscript.NET
Imports Ghostscript.NET.Rasterizer
Imports Newtonsoft.Json
Imports SixLabors.ImageSharp

Public Class GhostScriptHelper

    Shared Function Rasterize(oByteArray As Byte(), iWidth As Integer, iHeight As Integer, exs As List(Of Exception)) As Pdf
        Dim oStream As New System.IO.MemoryStream(oByteArray)
        Return Rasterize(exs, oStream, iWidth, iHeight)
    End Function

    Shared Function Rasterize(exs As List(Of Exception), oStream As System.IO.MemoryStream, Optional maxWidth As Integer = 0, Optional maxHeight As Integer = 0) As Pdf
        Dim desired_x_dpi As Integer = 96
        Dim desired_y_dpi As Integer = 96
        Dim DcFactor As Decimal = 2.54 * 10 / 96 'cm/inch x mm/cm / dpi
        Dim oRasterizer As New GhostscriptRasterizer()
        Dim oLastInstalledVersion As GhostscriptVersionInfo = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL Or GhostscriptLicense.AFPL, GhostscriptLicense.GPL)
        oRasterizer.Open(oStream, oLastInstalledVersion, True)

        Dim retval As New Pdf
        With retval
            .Portrait = oRasterizer.GetPage(desired_x_dpi, desired_y_dpi, 1)
            If .Portrait Is Nothing Then
                exs.Add(New Exception("Ghostscript no ha estat capaç de renderitzar el Pdf\nProbar de imprimir el document com a Pdf"))
            Else
                .PageCount = oRasterizer.PageCount
                .Size = New Size(.Portrait.Width * DcFactor, .Portrait.Height * DcFactor)
                If maxWidth = 0 And maxHeight = 0 Then
                    .Thumbnail = .Portrait
                Else
                    .Thumbnail = ImageHelper.GetThumbnailToFill(.Portrait, maxWidth, maxHeight)
                End If
            End If
        End With

        Return retval
    End Function

    Shared Function Rasterize(oStream As System.IO.Stream, ByRef oPdf As Pdf, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oRasterizer As New GhostscriptRasterizer()

        Try
            Dim desired_x_dpi As Integer = 96
            Dim desired_y_dpi As Integer = 96
            Dim oLastInstalledVersion As GhostscriptVersionInfo = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL Or GhostscriptLicense.AFPL, GhostscriptLicense.GPL)


            oRasterizer.Open(oStream, oLastInstalledVersion, True)

            Dim DcFactor As Decimal = 2.54 * 10 / 96 'cm/inch x mm/cm / dpi
            oPdf = New Pdf
            With oPdf
                .PageCount = oRasterizer.PageCount
                .Portrait = oRasterizer.GetPage(desired_x_dpi, desired_y_dpi, 1)
                If .Portrait Is Nothing Then
                    exs.Add(New Exception("Ghostscript no ha estat capaç de renderitzar el Pdf\nProbar de imprimir el document com a Pdf"))
                Else
                    .Size = New Size(.Portrait.Width * DcFactor, .Portrait.Height * DcFactor)
                    retval = True
                End If
            End With

        Catch ex As GhostscriptLibraryNotInstalledException
            Dim sb As New System.Text.StringBuilder
            Dim sMsg As String = "Falta instal.lar el component Ghostscript. Cal baixar-lo de http://www.ghostscript.com/download/gsdnld.html"
            sb.Append(sMsg)
            Dim e As New System.Exception(sMsg)
            exs.Add(e)
        Catch e As SystemException
            exs.Add(e)
        Finally
            'oRasterizer.Close()
        End Try
        Return retval
    End Function



    Public Class Pdf
        Property PageCount As Integer
        Property Size As Size
        <JsonIgnore> Property Portrait As Image
        <JsonIgnore> Property Thumbnail As Image
    End Class
End Class
