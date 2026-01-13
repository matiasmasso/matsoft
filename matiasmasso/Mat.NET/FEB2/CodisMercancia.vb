Public Class CodiMercancia

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCodiMercancia)
        Return Await Api.Fetch(Of DTOCodiMercancia)(exs, "CodiMercancia", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oCodiMercancia As DTOCodiMercancia, exs As List(Of Exception)) As Boolean
        Dim pCodiMercancia = Api.FetchSync(Of DTOCodiMercancia)(exs, "CodiMercancia", oCodiMercancia.Id)
        If exs.Count = 0 And pCodiMercancia IsNot Nothing Then
            DTOBaseGuid.CopyPropertyValues(Of DTOCodiMercancia)(pCodiMercancia, oCodiMercancia, exs)
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCodiMercancia As DTOCodiMercancia, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCodiMercancia)(oCodiMercancia, exs, "CodiMercancia")
    End Function


    Shared Async Function Delete(oCodiMercancia As DTOCodiMercancia, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCodiMercancia)(oCodiMercancia, exs, "CodiMercancia")
    End Function

    Shared Async Function Products(oCodiMercancia As DTOCodiMercancia, exs As List(Of Exception)) As Task(Of List(Of DTOProduct))
        Return Await Api.Fetch(Of List(Of DTOProduct))(exs, "CodiMercancia/products", oCodiMercancia.Id)
    End Function
End Class

Public Class CodisMercancia

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOCodiMercancia))
        Return Await Api.Fetch(Of List(Of DTOCodiMercancia))(exs, "CodisMercancia")
    End Function

End Class
