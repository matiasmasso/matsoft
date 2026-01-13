Public Class SkuController
    Inherits _BaseController

    <HttpPost>
    <Route("api/skus")>
    Public Function skusPerUser(userProduct As DUI.UserProduct) As List(Of DUI.Sku)
        Dim retval As New List(Of DUI.Sku)
        Dim oCategory As New DTOProductCategory(userProduct.Product.Guid)
        Dim oSkus As List(Of DTOProductSku) = BLL.BLLProductSkus.All(oCategory)

        For Each oSku As DTOProductSku In oSkus
            Dim item As New DUI.Sku
            With item
                .Guid = oCategory.Guid
                .Nom = oCategory.Nom
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/contact/skus")>
    Public Function skusPerContact(contactProduct As DUI.ContactProduct) As List(Of DUI.Sku)
        Dim retval As New List(Of DUI.Sku)

        Dim oCategory As DTOProductCategory = BLLProductCategory.Find(contactProduct.Product.Guid)
        Dim oSkus As List(Of DTOProductSku) = BLL.BLLProductStocks.CostAndInventory(oCategory)

        Dim oCliProductDtos As List(Of DTOCliProductDto) = Nothing
        Dim oCliProductsBlocked As List(Of DTOCliProductBlocked) = Nothing
        Dim oCustomPrices As List(Of DTOPricelistItemCustomer) = Nothing
        Dim DcDto As Decimal
        Dim oCustomer As DTOCustomer = Nothing
        If contactProduct.Contact IsNot Nothing Then
            oCustomer = BLLCustomer.Find(contactProduct.Contact.Guid)
            BLLContact.Load(oCustomer)
            oCustomPrices = BLLPricelistItemsCustomer.Active(oCustomer)
            'oProductsBlocked = BLLCliProductsBlocked.All(oCustomer)
            oCliProductsBlocked = BLLCliProductsBlocked.All(oCustomer)
            oCliProductDtos = BLLCliProductDtos.All(oCustomer)
            Dim oDtos As List(Of DTOCustomerTarifaDto) = BLL.BLLCustomerTarifaDtos.Active(oCustomer)
            Dim BlCostEnabled As Boolean = oDtos.Count > 0
            DcDto = BLL.BLLCustomerTarifaDto.FromProduct(oDtos, oCategory.Brand)
        End If


        For Each oSku As DTOProductSku In oSkus
            Dim Skip As Boolean = False
            Dim DcCost As Decimal
            If oCustomer IsNot Nothing Then
                If BLLCliProductsBlocked.IsAllowed(oCliProductsBlocked, oSku) Then
                    Dim oCustomPrice As DTOPricelistItemCustomer = oCustomPrices.Find(Function(x) x.Sku.Equals(oSku))
                    If oCustomPrice Is Nothing Then
                        DcCost = Math.Round(oSku.RRPP.Eur * (100 - DcDto) / 100, 2, MidpointRounding.AwayFromZero)
                    Else
                        DcCost = oCustomPrice.Retail.Eur
                    End If
                    If oCustomer.Tarifa = DTOCustomer.Tarifas.FiftyFifty Then
                        DcCost = Math.Round(DcCost / 2, 2, MidpointRounding.AwayFromZero)
                    End If
                    oSku.Price = BLLApp.GetAmt(DcCost)

                    Dim oCliProductDto As DTOCliProductDto = BLLProductSku.GetCliProductDto(oSku, oCliProductDtos)
                    If oCliProductDto IsNot Nothing Then
                        oSku.CustomerDto = oCliProductDto.Dto
                    End If
                Else
                    Skip = True
                End If
            End If

            If Not Skip Then
                Try
                    Dim item As New DUI.Sku
                    With item
                        .Guid = oSku.Guid
                        .Nom = oSku.NomCurt
                        If oSku.Price IsNot Nothing Then
                            .Price = oSku.Price.Eur
                        End If
                        .Dto = oSku.CustomerDto
                        .RRPP = oSku.RRPP.Eur
                        .Moq = BLLProductSku.Moq(oSku)

                        Dim iStock As Integer = oSku.Stock - oSku.Clients + oSku.ClientsAlPot + oSku.ClientsEnProgramacio
                        If iStock < 0 Then
                            .Stock = 0
                        Else
                            .Stock = iStock
                        End If

                    End With
                    retval.Add(item)

                Catch ex As Exception

                End Try

            End If

        Next

        Return retval
    End Function
End Class
