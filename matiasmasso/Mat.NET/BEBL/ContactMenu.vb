Public Class ContactMenu
    Shared Function Find(oGuid As Guid) As DTOContactMenu
        Return ContactMenuLoader.Find(oGuid)
    End Function
End Class
