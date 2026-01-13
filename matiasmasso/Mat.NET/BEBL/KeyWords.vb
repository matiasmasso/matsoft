Public Class KeyWords
    Shared Function all(oSrcGuid As Guid) As List(Of String)
        Dim retval As List(Of String) = KeywordsLoader.FromSrc(oSrcGuid)
        Return retval
    End Function

End Class
