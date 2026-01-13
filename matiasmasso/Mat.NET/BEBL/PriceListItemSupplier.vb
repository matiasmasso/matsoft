Public Class PriceListItemSupplier


    Shared Function Update(oPriceListItemSupplier As DTOPriceListItem_Supplier, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PriceListItemSupplierLoader.Update(oPriceListItemSupplier, exs)
        Return retval
    End Function

    Shared Function Delete(oPriceListItemSupplier As DTOPriceListItem_Supplier, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PriceListItemSupplierLoader.Delete(oPriceListItemSupplier, exs)
        Return retval
    End Function

    Shared Function FromSku(oSku As DTOProductSku, Optional DtFch As Date = Nothing) As DTOPriceListItem_Supplier
        Dim retval As DTOPriceListItem_Supplier = Nothing
        If oSku IsNot Nothing Then
            If oSku.RefProveidor > "" And oSku.Category IsNot Nothing Then
                If oSku.Category.Brand IsNot Nothing Then
                    If oSku.Category.Brand.Proveidor IsNot Nothing Then
                        retval = FromSku(oSku.Category.Brand.Proveidor, oSku.RefProveidor, DtFch)
                    End If
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function FromSku(oProveidor As DTOProveidor, sRef As String, Optional DtFch As Date = Nothing) As DTOPriceListItem_Supplier
        Dim retval As DTOPriceListItem_Supplier = Nothing
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Dim oItems As List(Of DTOPriceListItem_Supplier) = PriceListItemsSupplierLoader.FromSku(oProveidor, sRef, DtFch)
        Select Case oItems.Count
            Case 0
            Case 1
                retval = oItems(0)
            Case Else
                'busca la tarifa anterior mes recent
                retval = oItems.Where(Function(x) x.Parent.Fch <= DtFch).OrderByDescending(Function(x) x.Parent.Fch).FirstOrDefault()
                If retval Is Nothing Then
                    'si no, la primera que hi hagi encara que no sigui vigent
                    retval = oItems.Where(Function(x) x.Parent.Fch > DtFch).OrderBy(Function(x) x.Parent.Fch).FirstOrDefault()
                End If
        End Select
        Return retval
    End Function

End Class

Public Class PriceListItemsSupplier
    Shared Function Vigent(oProveidor As DTOProveidor) As List(Of DTOPriceListItem_Supplier)
        Return PriceListItemsSupplierLoader.Vigent(oProveidor)
    End Function

    Shared Function All(oSku As DTOProductSku) As List(Of DTOPriceListItem_Supplier)
        Return PriceListItemsSupplierLoader.All(oSku)
    End Function

    Shared Function Delete(items As List(Of DTOPriceListItem_Supplier), exs As List(Of Exception)) As Boolean
        Return PriceListItemsSupplierLoader.Delete(items, exs)
    End Function

End Class
