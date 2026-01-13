Public Class SpvIn

    Shared Async Function FromId(oEmp As DTOEmp, iYea As Integer, iNum As Integer, exs As List(Of Exception)) As Task(Of DTOSpvIn)
        Return Await Api.Fetch(Of DTOSpvIn)(exs, "spvIn", oEmp.Id, iYea, iNum)
    End Function

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOSpvIn)
        Return Await Api.Fetch(Of DTOSpvIn)(exs, "SpvIn", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oSpvIn As DTOSpvIn, exs As List(Of Exception)) As Boolean
        If Not oSpvIn.IsLoaded And Not oSpvIn.IsNew Then
            Dim pSpvIn = Api.FetchSync(Of DTOSpvIn)(exs, "SpvIn", oSpvIn.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOSpvIn)(pSpvIn, oSpvIn, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(oSpvIn As DTOSpvIn, exs As List(Of Exception)) As Task(Of Integer)
        Return Await Api.Update(Of DTOSpvIn, Integer)(oSpvIn, exs, "spvIn")
    End Function

    Shared Async Function Delete(oSpvIn As DTOSpvIn, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOSpvIn)(oSpvIn, exs, "spvIn")
    End Function

End Class

Public Class SpvIns
    Shared Async Function All(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOSpvIn))
        Dim retval = Await Api.Fetch(Of List(Of DTOSpvIn))(exs, "SpvIns", oEmp.Id)
        Return retval
    End Function

End Class
