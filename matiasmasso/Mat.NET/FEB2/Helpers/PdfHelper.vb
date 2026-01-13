Public Class PdfHelper

    Shared Function LoadDocfile(ByRef oDocFile As DTODocFile, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oPdf As DTOPdf = Nothing
        If Load(oPdf, oDocFile.Stream, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT, exs) Then
            With oDocFile
                .Size = oPdf.Size
                .Pags = oPdf.PageCount
                .Thumbnail = oPdf.Thumbnail
            End With
            retval = True
        End If
        Return retval
    End Function

    Shared Function LoadMediaResource(ByRef oMediaResource As DTOMediaResource, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oPdf As DTOPdf = Nothing
        If Load(oPdf, oMediaResource.Stream, DTOMediaResource.THUMBWIDTH, DTOMediaResource.THUMBHEIGHT, exs) Then
            With oMediaResource
                .Size = oPdf.Size
                .Pags = oPdf.PageCount
                .Thumbnail = oPdf.Thumbnail
            End With
            retval = True
        End If
        Return retval
    End Function

    Shared Function Thumbnail(oFileBytes As Byte(), iMaxWidth As Integer, iMaxHeight As Integer) As System.Drawing.Image
        Dim oPdf As DTOPdf = Nothing
        Dim exs As New List(Of Exception)
        Dim retval As System.Drawing.Image = Nothing
        If Load(oPdf, oFileBytes, iMaxWidth, iMaxHeight, exs) Then
            retval = oPdf.Thumbnail
        End If
        Return retval
    End Function

    Shared Function Thumbnail(oStream As System.IO.MemoryStream, iMaxWidth As Integer, iMaxHeight As Integer) As System.Drawing.Image
        Dim retval = MatHelper.GhostScriptHelper.PdfThumbnail()
        Dim oPdf As DTOPdf = Nothing
        Dim exs As New List(Of Exception)
        Dim retval As System.Drawing.Image = Nothing
        If Load(oPdf, oStream, iMaxWidth, iMaxHeight, exs) Then
            retval = oPdf.Thumbnail
        End If
        Return retval
    End Function

    Shared Function Load(sFilename As String, oByteArray As Byte(), maxWidth As Integer, MaxHeight As Integer, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If FileSystemHelper.GetStreamFromFile(sFilename, oByteArray, exs) Then
            Dim oPdf As New DTOPdf
            retval = Load(oPdf, oByteArray, maxWidth, MaxHeight, exs)
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPdf As DTOPdf, oByteArray() As Byte, maxWidth As Integer, maxHeight As Integer, exs As List(Of Exception)) As Boolean
        Dim oStream As New System.IO.MemoryStream(oByteArray)
        Dim retval As Boolean = Load(oPdf, oStream, maxWidth, maxHeight, exs)
        Return retval
    End Function

    Shared Function Load(ByRef oPdf As DTOPdf, sFilename As String, maxWidth As Integer, maxHeight As Integer, exs As List(Of Exception)) As Boolean
        Dim oStream As System.IO.Stream = New IO.FileStream(sFilename, IO.FileMode.Open, IO.FileAccess.Read)
        Dim retval As Boolean = Load(oPdf, oStream, maxWidth, maxHeight, exs)
        Return retval
    End Function

    Shared Function Load(ByRef oPdf As DTOPdf, oStream As System.IO.Stream, maxWidth As Integer, maxHeight As Integer, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = GhostscriptHelper.Rasterize(oStream, oPdf, exs)
        If retval Then
            If maxWidth = 0 And maxHeight = 0 Then
                oPdf.Thumbnail = oPdf.Portrait
            Else
                oPdf.Thumbnail = ImageHelper.GetThumbnailToFitAndFill(oPdf.Portrait, maxWidth, maxHeight)
            End If
        End If
        Return retval
    End Function

End Class
