Imports System.Web.Http
Imports System.Net
Imports System.Net.Http

Public Class TarifasController
    Inherits _BaseController

    <HttpPost>
    <Route("api/tarifas")>
    Public Function Tarifas(query As DUI.Query) As List(Of DUI.Category)
        Dim retval As New List(Of DUI.Category)
        Dim oCustomer As DTOCustomer = BEBL.Customer.Find(query.key1.Guid)
        Dim oCategory As DTOProductCategory = Nothing
        Dim oSku As DTOProductSku = Nothing

        Dim oTarifa As DTOCustomerTarifa = BEBL.CustomerTarifa.Load(oCustomer)

        Dim oBrand As DTOProductBrand = oTarifa.Brands.FirstOrDefault(Function(x) x.Guid.Equals(query.key2.Guid))
        If oBrand IsNot Nothing Then
            For Each oCategory In oBrand.Categories
                Select Case oCategory.Codi
                    Case DTOProductCategory.Codis.Standard, DTOProductCategory.Codis.Accessories

                        Dim duiCategory As New DUI.Category
                        duiCategory.Guid = oCategory.Guid
                        duiCategory.Nom = oCategory.nom.Esp
                        duiCategory.Skus = New List(Of DUI.Sku)
                            retval.Add(duiCategory)

                        For Each oSku In oCategory.Skus.Where(Function(x) x.NoWeb = False)
                            Dim duiSku As New DUI.Sku
                            duiSku.Guid = oSku.Guid
                            duiSku.Nom = oSku.nom.Esp
                            duiSku.RRPP = oSku.RRPP.Eur
                            duiSku.Price = oSku.Price.Eur
                            duiSku.Stock = oSku.StockAvailable()
                            duiSku.Moq = DTOProductSku.Moq(oSku)
                            duiCategory.Skus.Add(duiSku)
                        Next
                End Select
            Next
        End If

        Return retval
    End Function

End Class
