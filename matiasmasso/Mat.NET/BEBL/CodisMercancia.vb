Public Class CodiMercancia
    Shared Function Find(Id As String) As DTOCodiMercancia
        Dim retval As DTOCodiMercancia = CodiMercanciaLoader.Find(Id)
        Return retval
    End Function

    Shared Function Load(ByRef oCodiMercancia As DTOCodiMercancia) As Boolean
        Dim retval As Boolean = CodiMercanciaLoader.Load(oCodiMercancia)
        Return retval
    End Function

    Shared Function Update(oCodiMercancia As DTOCodiMercancia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CodiMercanciaLoader.Update(oCodiMercancia, exs)
        Return retval
    End Function

    Shared Function Delete(oCodiMercancia As DTOCodiMercancia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CodiMercanciaLoader.Delete(oCodiMercancia, exs)
        Return retval
    End Function

    Shared Function Products(oCodiMercancia As DTOCodiMercancia) As List(Of DTOProduct)
        Dim retval As List(Of DTOProduct) = CodiMercanciaLoader.Products(oCodiMercancia)
        Return retval
    End Function

End Class

Public Class CodisMercancia

    Shared Function All() As List(Of DTOCodiMercancia)
        Dim retval As List(Of DTOCodiMercancia) = CodisMercanciaLoader.All()
        Return retval
    End Function

End Class
