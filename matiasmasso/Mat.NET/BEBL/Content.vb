Public Class Content


    Shared Function Find(oGuid As Guid) As DTOContent
        Return ContentLoader.Find(oGuid)
    End Function

    Shared Function SearchByUrl(sUrlFriendlySegment As String) As DTOContent
        Dim retval As DTOContent = ContentLoader.SearchByUrl(sUrlFriendlySegment)
        Return retval
    End Function

    Shared Function Update(oContent As DTOContent, exs As List(Of Exception)) As Boolean
        Return ContentLoader.Update(oContent, exs)
    End Function

    Shared Function Delete(oContent As DTOContent, exs As List(Of Exception)) As Boolean
        Return ContentLoader.Delete(oContent, exs)
    End Function

End Class



Public Class Contents
    Shared Function All() As List(Of DTOContent)
        Dim retval As List(Of DTOContent) = ContentsLoader.All()
        Return retval
    End Function
End Class
