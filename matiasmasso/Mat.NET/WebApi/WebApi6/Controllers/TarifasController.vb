Public Class TarifasController
    Inherits _BaseController

    <HttpPost>
    <Route("api/tarifas")>
    Public Function Tarifas(query As DUI.Query) As List(Of DUI.Category)
        Dim retval As New List(Of DUI.Category)
        Dim oCustomer As DTOCustomer = BLLCustomer.Find(query.key1.Guid)
        Dim oCategory As DTOProductCategory = Nothing
        Dim oSku As DTOProductSku = Nothing

        Dim oTarifa As DTOCustomerTarifa = BLL.BLLCustomerTarifa.Load(oCustomer)

        Dim oBrand As DTOProductBrand = oTarifa.Brands.FirstOrDefault(Function(x) x.Guid.Equals(query.key2.Guid))
        If oBrand IsNot Nothing Then
            For Each oCategory In oBrand.Categories
                Select Case oCategory.Codi
                    Case DTOProductCategory.Codis.Standard, DTOProductCategory.Codis.Accessories

                        If BLL.BLLCliProductsBlocked.IsAllowed(oTarifa.CliProductsBlocked, oCategory) Then
                            Dim duiCategory As New DUI.Category
                            duiCategory.Guid = oCategory.Guid
                            duiCategory.Nom = oCategory.Nom
                            duiCategory.Skus = New List(Of DUI.Sku)
                            retval.Add(duiCategory)

                            For Each oSku In oCategory.Skus
                                If BLL.BLLCliProductsBlocked.IsAllowed(oTarifa.CliProductsBlocked, oSku) Then
                                    Dim duiSku As New DUI.Sku
                                    duiSku.Guid = oSku.Guid
                                    duiSku.Nom = oSku.NomCurt
                                    duiSku.RRPP = oSku.RRPP.Eur
                                    duiSku.Price = oSku.Price.Eur
                                    duiSku.Stock = BLLProductSku.StockAvailable(oSku)
                                    duiSku.Moq = BLLProductSku.Moq(oSku)
                                    duiCategory.Skus.Add(duiSku)
                                End If
                            Next
                        End If
                End Select
            Next
        End If

        Return retval
    End Function

End Class
