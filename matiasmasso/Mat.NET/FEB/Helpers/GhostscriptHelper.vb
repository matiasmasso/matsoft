Imports Ghostscript.NET
Imports Ghostscript.NET.Rasterizer
Imports System.Drawing

Public Module GhostscriptHelper

    Public Function Rasterize(oStream As System.IO.Stream, ByRef oPdf As DTOPdf, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oRasterizer As New GhostscriptRasterizer()

        Try
            Dim desired_x_dpi As Integer = 96
            Dim desired_y_dpi As Integer = 96
            Dim oLastInstalledVersion As GhostscriptVersionInfo = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL Or GhostscriptLicense.AFPL, GhostscriptLicense.GPL)


            oRasterizer.Open(oStream, oLastInstalledVersion, True)

            Dim DcFactor As Decimal = 2.54 * 10 / 96 'cm/inch x mm/cm / dpi
            oPdf = New DTOPdf
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
            'BLLWinBug.Log(sb.ToString)
        Catch e As SystemException
            exs.Add(e)
            'BLLWinBug.Log(e.Message)
        Finally
            'oRasterizer.Close()
        End Try
        Return retval
    End Function

End Module

