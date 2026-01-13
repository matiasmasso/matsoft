Public Class Area
    Shared Function Find(oGuid As Guid) As DTOArea
        Dim retval As DTOArea = AreaLoader.Find(oGuid)
        Return retval
    End Function

End Class
