Public Class DTOCatalog
    Inherits List(Of Brand)


    Shared Function Factory(oTarifa As DTOCustomerTarifa) As DTOCatalog
        Dim retval As New DTOCatalog
        For Each oBrand In oTarifa.brands
            Dim pBrand As New DTOCatalog.Brand(oBrand.Guid, oBrand.nom)
            retval.Add(pBrand)
            For Each oCategory In oBrand.categories
                Dim pCategory = New DTOCatalog.Category(oCategory.Guid, oCategory.nom)
                pBrand.categories.Add(pCategory)
                For Each oSku In oCategory.skus
                    Dim pSku = New DTOCatalog.Sku(oSku.Guid, oSku.nomCurtOrNom())
                    With pSku
                        .id = oSku.id
                        .ref = oSku.refProveidor
                        .nomPrv = oSku.nomProveidor
                        .ean = oSku.ean13
                        .price = CompactAmt.Factory(oSku.price)
                        .customerDto = oSku.customerDto
                        .obsoleto = oSku.obsoleto
                    End With
                    pCategory.skus.Add(pSku)
                Next
            Next
        Next
        Return retval
    End Function

    Shared Function Factory(oBrands As List(Of DTOProductBrand), oLang As DTOLang) As DTOCatalog
        Dim retval As New DTOCatalog
        For Each oBrand In oBrands
            Dim pBrand As New DTOCatalog.Brand(oBrand.Guid, oBrand.nom)
            retval.Add(pBrand)
            For Each oCategory In oBrand.categories
                Dim pCategory = New DTOCatalog.Category(oCategory.Guid, oCategory.nom)
                pBrand.categories.Add(pCategory)
                For Each oSku In oCategory.skus
                    Dim pSku = New DTOCatalog.Sku(oSku.Guid, oSku.nomCurtOrNom())
                    With pSku
                        .id = oSku.id
                        .ref = oSku.refProveidor
                        .nomPrv = oSku.nomProveidor
                        .nomCurt = oSku.nomCurtLangText.tradueix(oLang)
                        .ean = oSku.ean13
                        .stock = oSku.stockAvailable()
                        .price = CompactAmt.Factory(oSku.price)
                        .customerDto = oSku.customerDto
                        .obsoleto = oSku.obsoleto
                    End With
                    pCategory.skus.Add(pSku)
                Next
            Next
        Next
        Return retval
    End Function


    Shared Function Factory(oSkus As List(Of DTOProductSku), oLang As DTOLang) As DTOCatalog
        Dim retval As New DTOCatalog
        Dim oBrand As New DTOCatalog.Brand(Guid.NewGuid)
        Dim oCategory As New DTOCatalog.Category(Guid.NewGuid)
        For Each oSku In oSkus
            If Not oSku.category.Guid.Equals(oCategory.Guid) Then
                If Not oSku.category.Brand.Guid.Equals(oBrand.Guid) Then
                    oBrand = New DTOCatalog.Brand(oSku.category.Brand.Guid)
                    oBrand.nom = oSku.category.Brand.nom
                    retval.Add(oBrand)
                End If
                oCategory = New DTOCatalog.Category(oSku.category.Guid)
                oCategory.nom = oSku.category.nom
                oBrand.categories.Add(oCategory)
            End If
            Dim pSku = New DTOCatalog.Sku(oSku.Guid, oSku.NomCurtOrNom())
            With pSku
                .id = oSku.id
                .ref = oSku.refProveidor
                .nomPrv = oSku.nomProveidor
                .ean = oSku.ean13
                .price = CompactAmt.Factory(oSku.price)
                .customerDto = oSku.customerDto
                .stock = oSku.StockAvailable()
                .obsoleto = oSku.obsoleto
            End With
            oCategory.skus.Add(pSku)
        Next
        Return retval
    End Function

    Public Function toProductBrands() As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        For Each oCompactBrand In Me
            Dim oBrand As New DTOProductBrand(oCompactBrand.Guid)
            oBrand.nom = oCompactBrand.nom
            retval.Add(oBrand)
            For Each oCompactCategory In oCompactBrand.categories
                Dim oCategory As New DTOProductCategory(oCompactCategory.Guid)
                With oCategory
                    .Brand = oBrand
                    .nom = oCompactCategory.nom
                End With
                oBrand.categories.Add(oCategory)
                For Each oCompactSku In oCompactCategory.skus
                    Dim oSku As New DTOProductSku(oCompactSku.Guid)
                    With oSku
                        .category = oCategory
                        .id = oCompactSku.id
                        .ean13 = oCompactSku.ean
                        .refProveidor = oCompactSku.ref
                        .nomProveidor = oCompactSku.nomPrv
                        .nomCurt = oCompactSku.nomCurt
                        .obsoleto = oCompactSku.obsoleto
                    End With
                    oCategory.skus.Add(oSku)
                Next
            Next
        Next
        Return retval
    End Function

    Public Class Brand
        Property Guid As Guid
        Property nom As String
        Property CodDist As DTOProductBrand.CodDists
        Property categories As List(Of Category)
        Property obsoleto As Boolean


        Public Sub New(Optional oGuid As Guid = Nothing, Optional sNom As String = "")
            MyBase.New
            If oGuid = Nothing Then oGuid = New Guid
            _Guid = oGuid
            _Nom = sNom
            _Categories = New List(Of Category)
        End Sub


    End Class

    Public Class Category
        Property Guid As Guid
        Property nom As String
        Property isBundle As Boolean
        Property skus As List(Of Sku)
        Property obsoleto As Boolean


        Public Sub New(Optional oGuid As Guid = Nothing, Optional sNom As String = "")
            MyBase.New
            If oGuid = Nothing Then oGuid = New Guid
            _Guid = oGuid
            _Nom = sNom
            _Skus = New List(Of Sku)
        End Sub
    End Class

    Public Class Sku
        Property Guid As Guid
        Property id As Integer
        Property nomCurt As String
        Property ref As String
        Property nomPrv As String
        Property ean As DTOEan

        Property stock As Integer

        Property price As CompactAmt
        Property customerDto As Decimal
        Property obsoleto As Boolean

        Public Sub New(Optional oGuid As Guid = Nothing, Optional sNom As String = "")
            MyBase.New
            If oGuid = Nothing Then oGuid = New Guid
            _Guid = oGuid
            _NomCurt = sNom
        End Sub

    End Class

    Public Class CompactAmt
        Property eur As Decimal
        Property cur As CompactCur

        Shared Function Factory(oAmt As DTOAmt) As CompactAmt
            Dim retval As CompactAmt = Nothing
            If oAmt IsNot Nothing Then
                retval = New CompactAmt
                With retval
                    .eur = oAmt.eur
                    .cur = New CompactCur With {.tag = oAmt.cur.Tag}
                End With
            End If
            Return retval
        End Function

    End Class

    Public Class CompactCur
        Property tag As String = "EUR"
    End Class

End Class
