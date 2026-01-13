Public Class PriceListItemCustomer
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oPriceList As DTOPricelistCustomer, oSku As DTOProductSku) As Task(Of DTOPricelistItemCustomer)
        Return Await Api.Fetch(Of DTOPricelistItemCustomer)(exs, "PriceListItemCustomer", oPriceList.Guid.ToString, oSku.Guid.ToString())
    End Function

    Shared Async Function Search(exs As List(Of Exception), oSku As DTOProductSku, Optional DtFch As Date = Nothing) As Task(Of DTOPricelistItemCustomer)
        Return Await Api.Fetch(Of DTOPricelistItemCustomer)(exs, "PriceListItemCustomer/Search", oSku.Guid.ToString, FormatFch(DtFch))
    End Function

    Shared Async Function Update(exs As List(Of Exception), oPriceListItemCustomer As DTOPricelistItemCustomer) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPricelistItemCustomer)(oPriceListItemCustomer, exs, "PriceListItemCustomer")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oPriceListItemCustomer As DTOPricelistItemCustomer) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPricelistItemCustomer)(oPriceListItemCustomer, exs, "PriceListItemCustomer")
    End Function

    Shared Async Function AddFromPriceListItemSupplier(oPriceListCustomer As DTOPricelistCustomer, item As DTOPriceListItem_Supplier, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oSku = Await FEB2.ProductSku.FromProveidor(exs, item.Parent.Proveidor, item.Ref)
        If oSku Is Nothing Then
            Dim ex As New KeyNotFoundException(item.Ref & " " & item.Description & " no s'ha trobat al catáleg")
            exs.Add(ex)
        Else
            Dim value As DTOPricelistItemCustomer = oPriceListCustomer.Items.FirstOrDefault(Function(x) x.Sku.Equals(oSku))
            If value Is Nothing Then
                value = New DTOPricelistItemCustomer(oPriceListCustomer)
                oPriceListCustomer.Items.Add(value)
            End If
            With value
                .Sku = oSku
                .Retail = DTOAmt.Factory(item.Retail)
            End With
            retval = True
        End If
        Return retval
    End Function
End Class
Public Class PriceListItemsCustomer
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oProductSku As DTOProductSku) As Task(Of List(Of DTOPricelistItemCustomer))
        Return Await Api.Fetch(Of List(Of DTOPricelistItemCustomer))(exs, "PriceListItemsCustomer", oProductSku.Guid.ToString())
    End Function

    Shared Async Function Active(exs As List(Of Exception), oCustomer As DTOCustomer, Optional DtFch As Date = Nothing) As Task(Of List(Of DTOPricelistItemCustomer))
        Return Await Api.Fetch(Of List(Of DTOPricelistItemCustomer))(exs, "PriceListItemsCustomer/Active", oCustomer.Guid.ToString, FormatFch(DtFch))
    End Function

    Shared Async Function Vigent(exs As List(Of Exception), Optional DtFch As Date = Nothing) As Task(Of List(Of DTOPricelistItemCustomer))
        Return Await Api.Fetch(Of List(Of DTOPricelistItemCustomer))(exs, "PriceListItemsCustomer/Vigent", FormatFch(DtFch))
    End Function

End Class
