Public Class BreadCrumb

    Shared Function FromProduct(oProductBase As DTOProduct, oLang As DTOLang, oTab As DTOProduct.Tabs, Optional oLocation As DTOLocation = Nothing) As DTOBreadCrumb
        Dim retval As New DTOBreadCrumb(oLang, "Productos", "Productes", "Products", "Produtos")
        Dim exs As New List(Of Exception)
        With retval
            Select Case oProductBase.SourceCod
                Case DTOProduct.SourceCods.Brand
                    Dim oBrand As DTOProductBrand = oProductBase
                    FEB2.ProductBrand.Load(oBrand, exs)
                    .AddItem(oBrand.Nom.tradueix(oLang), If(oTab = DTOProduct.Tabs.general, "", oBrand.GetUrl(oLang)))
                Case DTOProduct.SourceCods.Dept
                    Dim oDept As DTODept = oProductBase
                    If FEB2.Dept.Load(oDept, False, exs) Then
                        .AddItem(oDept.brand.nom.tradueix(oLang), oDept.brand.GetUrl(oLang)))
                        .AddItem(oDept.Nom.tradueix(oLang), If(oTab = DTOProduct.Tabs.general, "", oDept.GetUrl(oLang)))
                    End If
                Case DTOProduct.SourceCods.Category
                    Dim oCategory As DTOProductCategory = oProductBase
                    FEB2.ProductCategory.Load(oCategory, exs)
                    .AddItem(oCategory.brand.nom.tradueix(oLang), oCategory.brand.GetUrl(oLang)))
                    .AddItem(oCategory.Nom.tradueix(oLang), If(oTab = DTOProduct.Tabs.general, "", oCategory.GetUrl(oLang)))
                Case DTOProduct.SourceCods.Sku
                    Dim oSKU As DTOProductSku = oProductBase
                    FEB2.ProductSku.Load(oSKU, exs)
                    .AddItem(oSKU.category.brand.nom.tradueix(oLang), oSKU.category.brand.GetUrl(oLang)))
                    .AddItem(oSKU.category.nom.tradueix(oLang), oSKU.category.GetUrl(oLang)))
                    .AddItem(oSKU.Nom.tradueix(oLang), If(oTab = DTOProduct.Tabs.general, "", oSKU.GetUrl(oLang)))
            End Select

            Select Case oTab
                Case DTOProduct.Tabs.general
                Case DTOProduct.Tabs.distribuidores
                    If oLocation Is Nothing Then
                        .AddItem(DTOProduct.TabCaption(oTab, oLang))
                    Else
                        .AddItem(DTOProduct.TabCaption(oTab, oLang), oProductBase.GetUrl(oLang, oTab)))
                        .AddItem(oLocation.Nom)
                    End If
                Case Else
                    .AddItem(DTOProduct.TabCaption(oTab, oLang))
            End Select

        End With
        Return retval
    End Function

End Class

