Public Class BancSdo
    Shared Function Update(oBancSdo As DTOBancSdo, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancSdoLoader.Update(oBancSdo, exs)
        Return retval
    End Function

End Class

Public Class BancSdos
    Shared Function Last(oEmp As DTOEmp) As List(Of DTOBancSdo)
        Dim retval As List(Of DTOBancSdo) = BancSdosLoader.Last(oEmp)
        Return retval
    End Function
End Class
