Public Class BancSdo

    Shared Async Function Update(oBancSdo As DTOBancSdo, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOBancSdo)(oBancSdo, exs, "BancSdo")
        oBancSdo.IsNew = False
    End Function

End Class

Public Class BancSdos

    Shared Async Function Last(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOBancSdo))
        Return Await Api.Fetch(Of List(Of DTOBancSdo))(exs, "BancSdos/Last", oEmp.Id)
    End Function

End Class
