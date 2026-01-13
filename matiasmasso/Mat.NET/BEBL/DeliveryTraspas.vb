Public Class DeliveryTraspas


    Shared Function Find(oGuid As Guid) As DTODeliveryTraspas
        Dim retval As DTODeliveryTraspas = DeliveryTraspasLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FullTraspas(exs As List(Of Exception), oUser As DTOUser, oMgzFrom As DTOMgz, oMgzTo As DTOMgz, Optional DtFch As Date = Nothing) As Boolean
        Dim oStocks = MgzLoader.RawStocks(oMgzFrom)
        Dim oTraspas = DTODeliveryTraspas.Factory(oUser, oMgzFrom, oMgzTo, DtFch)
        For Each oSku In oStocks
            Dim item As New DTODeliveryItem()
            item.Sku = oSku
            item.Qty = oSku.Stock
            item.Price = DTOAmt.Factory(oSku.Pmc)
            oTraspas.Items.Add(item)
        Next
        Dim retval = Update(oTraspas, exs)
        Return retval
    End Function

    Shared Function Update(oTraspas As DTODeliveryTraspas, exs As List(Of Exception)) As Boolean
        Return DeliveryTraspasLoader.Update(oTraspas, exs)
    End Function

    Shared Function Delete(oTraspas As DTODeliveryTraspas, exs As List(Of Exception)) As Boolean
        Return DeliveryTraspasLoader.Delete(oTraspas, exs)
    End Function
End Class

Public Class DeliveryTraspassos
    Shared Function All(oEmp As DTOEmp) As List(Of DTODeliveryTraspas)
        Return DeliveryTraspassosLoader.all(oEmp)
    End Function
End Class
