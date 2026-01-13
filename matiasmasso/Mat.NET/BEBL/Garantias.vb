Public Class Garantias
    Shared Function all(oEmp As DTOEmp, FchFrom As Date, FchTo As Date) As List(Of DTODeliveryItem)
        Dim retval As List(Of DTODeliveryItem) = DeliveryItemsloader.Garantias(oEmp, FchFrom, FchTo)
        Return retval
    End Function




End Class
