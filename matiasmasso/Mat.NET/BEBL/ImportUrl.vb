Public Class ImportUrl
    Shared Function Import(oUser As DTOUser, url As String, exs As List(Of Exception)) As DTOImportUrl
        Dim retval As DTOImportUrl = Nothing
        Dim oStream As System.IO.MemoryStream = Nothing
        If DTO.AmazonSellerOrder.MatchUrl(url) Then
            retval = BEBL.AmazonSellerOrder.ImportUrl(oUser, url, exs)
        Else
            exs.Add(New Exception("fitxer no reconegut"))
        End If
        Return retval
    End Function
End Class
