Public Class DTOCsvPOBebitus
    Inherits DTOCsv

    Public Enum Cols
        Lin
        Ean
        Ref
        CategoryNom
        SkuNom
        Qty
        Eur
        Fch
    End Enum

    Shared Function IsPurchaseOrder(oCsv As CsvHelper.CsvFile) As Boolean
        Dim retval As Boolean = oCsv.HeaderRow = "Número de línea;Código de artículo;Externo;Nombre del producto;Texto;Cantidad;Importe neto;Fecha de entrega"
        Return retval
    End Function


    Shared Function PurchaseOrder(exs As List(Of Exception),
                                  oUser As DTOUser,
                                  oCsv As CsvHelper.CsvFile,
                                  oCustomer As DTOCustomer,
                                  oTarifaDtos As List(Of DTOCustomerTarifaDto),
                                  oCustomCosts As List(Of DTOPricelistItemCustomer),
                                  oCustomerSkus As List(Of DTOProductSku),
                                  oCliProductsBlocked As List(Of DTOCliProductBlocked),
                                  oCliProductDtos As List(Of DTOCliProductDto)) As DTOPurchaseOrder

        Dim sConcept = oCsv.Filename.Replace(".csv", "")
        Dim retval = DTOPurchaseOrder.Factory(oCustomer, oUser, Today, DTOPurchaseOrder.Sources.eMail, sConcept)
        For Each oRow In oCsv.ItemRows
            Dim item = PurchaseOrderItem(exs, oRow, oCustomer, oTarifaDtos, oCustomCosts, oCustomerSkus, oCliProductsBlocked, oCliProductDtos)
            If item.Sku IsNot Nothing Then
                retval.Items.Add(item)
            End If
        Next
        Return retval
    End Function

    Shared Function PurchaseOrderItem(exs As List(Of Exception),
                                  oRow As CsvHelper.CsvRow,
                                  oCustomer As DTOCustomer,
                                  oTarifaDtos As List(Of DTOCustomerTarifaDto),
                                  oCustomCosts As List(Of DTOPricelistItemCustomer),
                                  oCustomerSkus As List(Of DTOProductSku),
                                  oCliProductsBlocked As List(Of DTOCliProductBlocked),
                                  oCliProductDtos As List(Of DTOCliProductDto)) As DTOPurchaseOrderItem

        Dim retval As New DTOPurchaseOrderItem()
        Dim exs2 As New List(Of Exception)
        Dim oSku = Sku(exs2, oRow, oTarifaDtos, oCustomCosts, oCustomerSkus, oCliProductsBlocked, oCliProductDtos)
        If exs2.Count = 0 Then
            With retval
                .Sku = oSku
                .Qty = oRow.Cells(Cols.Qty)
                .Pending = .Qty
            End With

            Dim oCost As DTOAmt = DTOProductSku.GetCustomerCost(oSku, oCustomCosts, oTarifaDtos)
            If oCost Is Nothing Then
                retval.Price = DTOAmt.Factory(0)
                retval.Dto = 0
            Else
                retval.Price = oCost

                Dim oDto As DTOCliProductDto = oSku.CliProductDto(oCliProductDtos)
                If oDto IsNot Nothing Then retval.Dto = oDto.Dto
                If oCustomer.GlobalDto > retval.Dto Then retval.Dto = oCustomer.GlobalDto

            End If
            '.RepCom = Await febl.RepCom.GetRepCom(Current.Session.Emp, _Order.Customer, .Sku, _Fch)
        Else
            exs.AddRange(exs2)
        End If
        Return retval
    End Function

    Shared Function Sku(exs As List(Of Exception), oRow As CsvHelper.CsvRow,
                                  oTarifaDtos As List(Of DTOCustomerTarifaDto),
                                  oCustomCosts As List(Of DTOPricelistItemCustomer),
                                  oCustomerSkus As List(Of DTOProductSku),
                                  oCliProductsBlocked As List(Of DTOCliProductBlocked),
                                  oCliProductDtos As List(Of DTOCliProductDto)) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim sEan As String = oRow.Cells(Cols.Ean)
        If sEan > "" Then
            retval = oCustomerSkus.FirstOrDefault(Function(x) x.Ean13 IsNot Nothing AndAlso x.Ean13.Value = sEan)
        End If
        Dim sRef As String = oRow.Cells(Cols.Ref)
        If retval Is Nothing And Not String.IsNullOrEmpty(sRef) Then
            retval = oCustomerSkus.FirstOrDefault(Function(x) x.RefProveidor = sRef)
        End If
        If retval Is Nothing Then
            exs.Add(New Exception(String.Format("Ean {0} no trobat al catàleg", sEan)))

        End If
        Return retval
    End Function
End Class
