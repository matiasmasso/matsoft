Public Class EdiversaExceptions
    Shared Function All(oParent As DTOBaseGuid) As List(Of DTOEdiversaException)
        Return EdiversaExceptionsLoader.All(oParent)
    End Function

End Class
