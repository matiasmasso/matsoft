Imports Ghostscript.NET
Imports Ghostscript.NET.Rasterizer
Imports Newtonsoft.Json

Public Class GhostScriptHelper
    Shared CallBack As New Image.GetThumbnailImageAbort(AddressOf MycallBack)

    Shared Function MycallBack() As Boolean
        Return False
    End Function



    Shared Function Rasterize(oByteArray As Byte(), iWidth As Integer, iHeight As Integer, exs As List(Of Exception)) As Pdf
        Dim oStream As New System.IO.MemoryStream(oByteArray)
        Return Rasterize(exs, oStream, iWidth, iHeight)
    End Function

    Shared Function Rasterize(exs As List(Of Exception), oStream As System.IO.MemoryStream, Optional maxWidth As Integer = 0, Optional maxHeight As Integer = 0) As Pdf
        Dim retval As New Pdf
        Dim dpi As Integer = 96
        Dim DcFactor As Decimal = 2.54 * 10 / 96 'cm/inch x mm/cm / dpi
        Dim oRasterizer As New GhostscriptRasterizer()
        Try
            Dim oLastInstalledVersion As GhostscriptVersionInfo = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL Or GhostscriptLicense.AFPL, GhostscriptLicense.GPL)
            oRasterizer.Open(oStream, oLastInstalledVersion, True)
            Dim oPortrait = oRasterizer.GetPage(dpi, 1)

            With retval
                .Portrait = ImageHelper.GetByteArrayFromImg(oPortrait) ' oPortrait.Bytes()
                If .Portrait Is Nothing Then
                    exs.Add(New Exception("Ghostscript no ha estat capaç de renderitzar el Pdf\nProbar de imprimir el document com a Pdf"))
                Else
                    .PageCount = oRasterizer.PageCount
                    .Width = oPortrait.Width * DcFactor
                    .Height = oPortrait.Height * DcFactor
                    If maxWidth = 0 And maxHeight = 0 Then
                        .Thumbnail = .Portrait
                    Else
                        .Thumbnail = ImageHelper.GetThumbnailToFill(oPortrait, maxWidth, maxHeight).Bytes()
                    End If
                End If
            End With

        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function


    Shared Function Pdf2Jpg(oFileBytes As Byte()) As Image
        Dim retval As Image = Nothing
        Dim oRasterizer As New GhostscriptRasterizer()

        SyncLock oRasterizer
            Dim dpi As Integer = 96

            Try
                Dim oLastInstalledVersion As GhostscriptVersionInfo = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL Or GhostscriptLicense.AFPL, GhostscriptLicense.GPL)
                Dim oStream As New System.IO.MemoryStream(oFileBytes)
                oRasterizer.Open(oStream, oLastInstalledVersion, True)
                Dim oPortrait = oRasterizer.GetPage(dpi, 1)
                retval = ImageHelper.ResizeImage(oPortrait, 350, 400)

            Catch ex As GhostscriptLibraryNotInstalledException
                Dim sb As New System.Text.StringBuilder
                Throw New Exception("Falta instal.lar el component Ghostscript. Cal baixar-lo de http://www.ghostscript.com/download/gsdnld.html")
            Catch e As SystemException
                Throw New Exception(e.Message)
            Finally
                oRasterizer.Close()
            End Try

        End SyncLock
        Return retval
    End Function

    Shared Function Pdf2Docfile(oFileBytes As Byte()) As DTODocFile.Pdf
        Dim retval As New DTODocFile.Pdf()
        Dim oRasterizer As New GhostscriptRasterizer()
        Dim dpi As Integer = 96

        Try
            Dim oLastInstalledVersion As GhostscriptVersionInfo = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL Or GhostscriptLicense.AFPL, GhostscriptLicense.GPL)
            Dim oStream As New System.IO.MemoryStream(oFileBytes)
            oRasterizer.Open(oStream, oLastInstalledVersion, True)
            Dim oPortrait = oRasterizer.GetPage(dpi, 1)
            Dim oThumbnailBytes = ImageHelper.ResizeImage(oPortrait, 350, 400).Bytes()
            With retval
                .ThumbnailDataUrl = String.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String(oThumbnailBytes))
                .Pags = oRasterizer.PageCount
                .Width = oPortrait.Width
                .Height = oPortrait.Height
            End With

        Catch ex As GhostscriptLibraryNotInstalledException
            Dim sb As New System.Text.StringBuilder
            Throw New Exception("Falta instal.lar el component Ghostscript. Cal baixar-lo de http://www.ghostscript.com/download/gsdnld.html")
        Catch e As SystemException
            Throw New Exception(e.Message)
        Finally
            oRasterizer.Close()
        End Try
        Return retval
    End Function

    Shared Function Pdf2Img(oFileBytes As Byte()) As Pdf
        Dim retval As Pdf = Nothing
        Dim oRasterizer As New GhostscriptRasterizer()
        Dim dpi As Integer = 96

        Try
            Dim oLastInstalledVersion As GhostscriptVersionInfo = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL Or GhostscriptLicense.AFPL, GhostscriptLicense.GPL)
            Dim oStream As New System.IO.MemoryStream(oFileBytes)
            oRasterizer.Open(oStream, oLastInstalledVersion, True)
            Dim oPortrait = oRasterizer.GetPage(dpi, 1)

            Dim DcFactor As Decimal = 2.54 * 10 / 96 'cm/inch x mm/cm / dpi
            retval = New Pdf
            With retval
                .PageCount = oRasterizer.PageCount
                .Portrait = oPortrait.Bytes()
                If .Portrait Is Nothing Then
                    Throw (New Exception("Ghostscript no ha estat capaç de renderitzar el Pdf\nProbar de imprimir el document com a Pdf"))
                Else
                    .Width = oPortrait.Width * DcFactor
                    .Height = oPortrait.Height * DcFactor
                End If
            End With

        Catch ex As GhostscriptLibraryNotInstalledException
            Dim sb As New System.Text.StringBuilder
            Throw New Exception("Falta instal.lar el component Ghostscript. Cal baixar-lo de http://www.ghostscript.com/download/gsdnld.html")
        Catch e As SystemException
            Throw New Exception(e.Message)
        Finally
            oRasterizer.Close()
        End Try
        Return retval
    End Function

    Shared Function Rasterize(oStream As System.IO.Stream, ByRef oPdf As Pdf, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oRasterizer As New GhostscriptRasterizer()

        Try
            Dim dpi As Integer = 96
            'Dim desired_x_dpi As Integer = 96
            'Dim desired_y_dpi As Integer = 96
            Dim oLastInstalledVersion As GhostscriptVersionInfo = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL Or GhostscriptLicense.AFPL, GhostscriptLicense.GPL)


            oRasterizer.Open(oStream, oLastInstalledVersion, True)
            Dim oPortrait = oRasterizer.GetPage(dpi, 1)

            Dim DcFactor As Decimal = 2.54 * 10 / 96 'cm/inch x mm/cm / dpi
            oPdf = New Pdf
            With oPdf
                .PageCount = oRasterizer.PageCount
                .Portrait = oPortrait.Bytes()
                '.Portrait = ImageHelper.Converter(oRasterizer.GetPage(desired_x_dpi, desired_y_dpi, 1))
                If .Portrait Is Nothing Then
                    exs.Add(New Exception("Ghostscript no ha estat capaç de renderitzar el Pdf\nProbar de imprimir el document com a Pdf"))
                Else
                    .Width = oPortrait.Width * DcFactor
                    .Height = oPortrait.Height * DcFactor
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
        Property Width As Integer
        Property Height As Integer
        <JsonIgnore> Property Portrait As Byte()
        <JsonIgnore> Property Thumbnail As Byte()
    End Class


End Class
