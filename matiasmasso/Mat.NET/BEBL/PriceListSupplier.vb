Public Class PriceListSupplier
    Shared Function Find(oGuid As Guid) As DTOPriceListSupplier
        Dim retval As DTOPriceListSupplier = PriceListSupplierLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oPriceList As DTOPriceListSupplier) As Boolean
        Dim retval As Boolean = PriceListSupplierLoader.Load(oPriceList)
        Return retval
    End Function

    Shared Function Update(oPriceList As DTOPriceListSupplier, exs As List(Of Exception)) As Boolean
        Return PriceListSupplierLoader.Update(oPriceList, exs)
    End Function

    Shared Function Delete(oPriceList As DTOPriceListSupplier, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PriceListSupplierLoader.Delete(oPriceList, exs)
        Return retval
    End Function


End Class


Public Class PriceListsSupplier

    Shared Function FromProveidor(oProveidor As DTOProveidor) As List(Of DTOPriceListSupplier)
        Dim retval As List(Of DTOPriceListSupplier) = PriceListsSupplierLoader.FromProveidor(oProveidor)
        Return retval
    End Function

    Shared Function Costs(oProveidor As DTOProveidor) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = PriceListsSupplierLoader.Costs(oProveidor)
        Return retval
    End Function

End Class