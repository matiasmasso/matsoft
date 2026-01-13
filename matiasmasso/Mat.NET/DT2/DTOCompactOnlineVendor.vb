Public Class DTOCompactOnlineVendor

    Property Guid As Guid

    Property Stocks As List(Of Stock)


    Public Class Stock
        Property Ean As String
        Property Qty As Integer
        Property Price As Decimal
    End Class


    Shared Sub LoadTestStocks(ByRef oVendor As DTOCompactOnlineVendor)
        oVendor.Stocks = New List(Of DTOCompactOnlineVendor.Stock)

        Dim item As New DTOCompactOnlineVendor.Stock
        With item
            .Ean = "4000984192421"
            .Qty = 100
            .Price = 260.5
        End With
        oVendor.Stocks.Add(item)

        item = New DTOCompactOnlineVendor.Stock
        With item
            .Ean = "8034135841919"
            .Qty = 55
            .Price = 311.0
        End With
        oVendor.Stocks.Add(item)

        item = New DTOCompactOnlineVendor.Stock
        With item
            .Ean = "5010415226075"
            .Qty = 55
            .Price = 311.0
        End With
        oVendor.Stocks.Add(item)

    End Sub

End Class
