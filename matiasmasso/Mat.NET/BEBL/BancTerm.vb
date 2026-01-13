Public Class BancTerm
    Shared Function Update(oBancTerm As DTOBancTerm, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancTermLoader.Update(oBancTerm, exs)
        Return retval
    End Function

    Shared Function Delete(oBancTerm As DTOBancTerm, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancTermLoader.Delete(oBancTerm, exs)
        Return retval
    End Function

End Class

Public Class BancTerms

    Shared Function All(oEmp As DTOEmp, Optional oBanc As DTOBanc = Nothing) As List(Of DTOBancTerm)
        Dim retval As List(Of DTOBancTerm) = BancTermsLoader.All(oEmp, oBanc)
        Return retval
    End Function

End Class
