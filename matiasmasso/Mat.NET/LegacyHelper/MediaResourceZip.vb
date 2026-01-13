Public Class MediaResourceZip
    Shared Function Zip(items As List(Of DTOMediaResource), exs As List(Of Exception)) As IO.Stream
        Dim oFiles As New List(Of ZipHelper.File)
        For Each item As DTOMediaResource In items
            'Dim oFile = ZipHelper.File.Factory(item.Nom, MediaResourceFtpHelper.GetStream(item))
            'oFiles.Add(oFile)
        Next
        Dim retval As IO.MemoryStream = ZipHelper.Zip(oFiles, exs)
        Return retval
    End Function

End Class
