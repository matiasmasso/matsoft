Public Class Tax

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOTax)
        Return Await Api.Fetch(Of DTOTax)(exs, "Tax", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oTax As DTOTax, exs As List(Of Exception)) As Boolean
        If Not oTax.IsLoaded And Not oTax.IsNew Then
            Dim pTax = Api.FetchSync(Of DTOTax)(exs, "Tax", oTax.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOTax)(pTax, oTax, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oTax As DTOTax, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOTax)(oTax, exs, "Tax")
        oTax.IsNew = False
    End Function

    Shared Async Function Delete(oTax As DTOTax, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOTax)(oTax, exs, "Tax")
    End Function
End Class


Public Class Taxes

    Shared Async Function AllAsync(exs As List(Of Exception)) As Task(Of List(Of DTOTax))
        Return Await Api.Fetch(Of List(Of DTOTax))(exs, "taxes")
    End Function

    Shared Function AllSync(exs As List(Of Exception)) As List(Of DTOTax)
        Return Api.FetchSync(Of List(Of DTOTax))(exs, "taxes")
    End Function

End Class
