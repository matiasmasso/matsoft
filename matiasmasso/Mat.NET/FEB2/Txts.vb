Public Class Txt

    Shared Async Function Find(id As DTOTxt.Ids, exs As List(Of Exception)) As Task(Of DTOTxt)
        Return Await Api.Fetch(Of DTOTxt)(exs, "txt", CInt(id).ToString())
    End Function

    Shared Function FindSync(id As DTOTxt.Ids, exs As List(Of Exception)) As DTOTxt
        Return Api.FetchSync(Of DTOTxt)(exs, "txt", CInt(id).ToString())
    End Function

    Shared Function Load(ByRef oTxt As DTOTxt, exs As List(Of Exception)) As Boolean
        If Not oTxt.IsLoaded Then
            Dim pTxt = Api.FetchSync(Of DTOTxt)(exs, "Txt", oTxt.Id.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOTxt)(pTxt, oTxt, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oTxt As DTOTxt, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOTxt)(oTxt, exs, "Txt")
    End Function


    Shared Async Function Delete(oTxt As DTOTxt, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOTxt)(oTxt, exs, "Txt")
    End Function
End Class
