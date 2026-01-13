Public Class Cnap

    Shared Function Find(oGuid As Guid) As DTOCnap
        Dim retval As DTOCnap = CnapLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oCnap As DTOCnap, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CnapLoader.Update(oCnap, exs)
        Return retval
    End Function

    Shared Function Delete(oCnap As DTOCnap, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CnapLoader.Delete(oCnap, exs)
        Return retval
    End Function

End Class

Public Class Cnaps
    Shared Function All(Optional searchkey As String = "") As List(Of DTOCnap)
        Return CnapsLoader.All(searchkey)
    End Function
End Class
