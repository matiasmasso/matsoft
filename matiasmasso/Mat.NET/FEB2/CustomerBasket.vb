Public Class CustomerBasket
    Inherits _FeblBase

    Shared Async Function Model(exs As List(Of Exception), oUser As DTOUser, oCustomer As DTOCustomer) As Task(Of Models.CustomerBasket)
        Dim retval = Await Api.Fetch(Of Models.CustomerBasket)(exs, "CustomerBasket", oCustomer.Guid.ToString())

        Dim oCache = Await FEB2.Cache.Fetch(exs, oUser)
        If exs.Count = 0 Then
            Dim oBrands = oCache.Brands.Where(Function(x) x.obsoleto = False).ToList()
            Dim oBrandVarios = DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Varios)
            For Each oBrand In oBrands ' And x.EnabledxPro = True)
                If Not oBrand.Guid.Equals(oBrandVarios.Guid) Then
                    Dim pBrand As New Models.CatalogModel.Brand
                    pBrand.Guid = oBrand.Guid
                    pBrand.Nom = oBrand.Nom.Tradueix(oUser.Lang)
                    retval.Catalog.Brands.Add(pBrand)
                    Dim oCategories = oBrand.Categories.Where(Function(x) x.obsoleto = False).
                        Where(Function(y) y.Codi < 2).
                        OrderBy(Function(z) z.Nom.Tradueix(oUser.Lang)).
                        OrderBy(Function(t) t.Codi).
                        Reverse().
                        ToList()

                    For Each oCategory In oCategories
                        Dim pCategory As New Models.CatalogModel.Category
                        pCategory.Guid = oCategory.Guid
                        pCategory.Nom = oCategory.Nom.Tradueix(oUser.Lang)
                        pBrand.Categories.Add(pCategory)
                        Dim oSkus = oCategory.Skus.Where(Function(x) x.obsoleto = False).
                        OrderBy(Function(y) y.Nom.Tradueix(oUser.Lang)).
                        ToList()

                        For Each oSku In oSkus
                            Dim pSku As New Models.CatalogModel.SkuExtended
                            pSku.Guid = oSku.Guid
                            pSku.Nom = oSku.Nom.Tradueix(oUser.Lang)
                            pSku.Moq = oSku.Moq
                            pSku.Rrpp = oCache.RetailPrice(oSku.Guid)
                            pSku.Price = Math.Round(pSku.Rrpp * (100 - DiscountOnRrpp(oSku, retval.TarifaDtos)) / 100, 2, MidpointRounding.AwayFromZero)
                            pCategory.Skus.Add(pSku)
                        Next

                    Next
                End If
            Next
        Else
        End If
        Return retval
    End Function

    Shared Function DiscountOnRrpp(oSku As DTOProductSku, oTarifaDtos As Dictionary(Of String, Decimal)) As Decimal
        Dim retval As Decimal = 0
        If oTarifaDtos.ContainsKey(oSku.Guid.ToString()) Then
            retval = oTarifaDtos(oSku.Guid.ToString())
        ElseIf oTarifaDtos.ContainsKey(oSku.Category.Guid.ToString()) Then
            retval = oTarifaDtos(oSku.Category.Guid.ToString())
        ElseIf oTarifaDtos.ContainsKey(oSku.Category.Brand.Guid.ToString()) Then
            retval = oTarifaDtos(oSku.Category.Brand.Guid.ToString())
        ElseIf oTarifaDtos.ContainsKey(Guid.Empty.Tostring()) Then
            retval = oTarifaDtos(Guid.Empty.ToString())
        End If
        Return retval
    End Function


    Shared Async Function Tarifa(exs As List(Of Exception), oUser As DTOUser, oCustomer As DTOCustomer) As Task(Of DTOCustomerTarifa)
        Return Await Fetch(Of DTOCustomerTarifa)(exs, "CustomerBasket/Catalog", oUser.Guid.ToString, oCustomer.Guid.ToString())
    End Function

    Shared Async Function Update(exs As List(Of Exception), oOrder As DTOPurchaseOrder) As Task(Of Integer)
        Return Await Api.Update(Of DTOPurchaseOrder, Integer)(oOrder, exs, "CustomerBasket/update")
    End Function




End Class
