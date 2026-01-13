Imports System.ComponentModel

Public Class DTOProduct
    Inherits DTOBaseGuid

    Property sourceCod As SourceCods
    Property nom As String
    Property obsoleto As Boolean


    Public Enum SourceCods
        NotSet
        Catalog
        Brand
        Category
        SKU
        Dept
    End Enum

    'important en minuscules:
    Public Enum Tabs
        general
        coleccion
        distribuidores
        galeria
        descargas
        accesorios
        videos
        bloggerposts
        descripcion
    End Enum

    Public Enum Relateds
        NotSet
        Accessories
        Spares
        Relateds
    End Enum

    Public Enum SelectionModes
        Browse
        SelectAny
        SelectBrand
        SelectCategory
        SelectSku
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oGuid As Guid, oCod As DTOProduct.SourceCods, Optional sNom As String = "") As DTOProduct
        Dim retval As DTOProduct = Nothing
        Select Case oCod
            Case 1 'SourceCods.Brand
                retval = New DTOProductBrand(oGuid)
                retval.Nom = sNom
            Case 2 'SourceCods.Category
                retval = New DTOProductCategory(oGuid)
                retval.Nom = sNom
            Case 3 'SourceCods.SKU
                retval = New DTOProductSku(oGuid)
                retval.Nom = sNom
        End Select
        Return retval
    End Function


    Shared Function FromObject(oObject As Object) As DTOProduct
        Dim retval As DTOProduct = Nothing
        If oObject IsNot Nothing Then
            If oObject.GetType().IsSubclassOf(GetType(DTOProduct)) Then
                retval = oObject
            ElseIf TypeOf oObject Is DTOProduct Then
                retval = oObject
            Else
                Dim oProduct As DTOProduct = oObject.toobject(Of DTOProduct)
                Select Case oProduct.SourceCod
                    Case DTOProduct.SourceCods.SKU
                        retval = oObject.toobject(Of DTOProductSku)
                    Case DTOProduct.SourceCods.Category
                        retval = oObject.toobject(Of DTOProductCategory)
                    Case DTOProduct.SourceCods.Brand
                        retval = oObject.toobject(Of DTOProductBrand)
                    Case Else
                        retval = oProduct
                End Select
            End If
        End If
        Return retval
    End Function

    Public Function ToDerivedClass() As DTOProduct
        Dim retval As DTOProduct = Nothing
        Select Case _SourceCod
            Case DTOProduct.SourceCods.Brand
                retval = New DTOProductBrand(MyBase.Guid)
                retval.Nom = _Nom
            Case DTOProduct.SourceCods.Category
                retval = New DTOProductCategory(MyBase.Guid)
                retval.Nom = _Nom
            Case DTOProduct.SourceCods.SKU
                retval = New DTOProductSku(MyBase.Guid)
                retval.Nom = _Nom
        End Select
        Return retval
    End Function

    Public Function FullNom(Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Dim retval As String = ""
        If TypeOf Me Is DTOProductBrand Then
            Dim oBrand As DTOProductBrand = CType(Me, DTOProductBrand)
            retval = oBrand.Nom
        ElseIf TypeOf Me Is DTODept Then
            Dim oDept As DTODept = CType(Me, DTODept)
            retval = String.Format("{0} {1}", oDept.Brand.Nom, oDept.LangNom.tradueix(oLang))
        ElseIf TypeOf Me Is DTOProductCategory Then
            Dim oCategory As DTOProductCategory = CType(Me, DTOProductCategory)
            retval = String.Format("{0} {1}", oCategory.Brand.Nom, oCategory.Nom)
        ElseIf TypeOf Me Is DTOProductSku Then
            Dim oSku As DTOProductSku = CType(Me, DTOProductSku)
            retval = String.Format("{0} {1} {2}", oSku.Category.Brand.Nom, oSku.Category.Nom, oSku.NomCurt)
        Else
            retval = _Nom
        End If
        Return retval
    End Function

    Public Function NomCurt(Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Dim retval As String = ""
        If TypeOf Me Is DTOProductBrand Then
            Dim oBrand As DTOProductBrand = CType(Me, DTOProductBrand)
            retval = oBrand.Nom
        ElseIf TypeOf Me Is DTODept Then
            Dim oDept As DTODept = CType(Me, DTODept)
            retval = oDept.LangNom.tradueix(oLang)
        ElseIf TypeOf Me Is DTOProductCategory Then
            Dim oCategory As DTOProductCategory = CType(Me, DTOProductCategory)
            retval = oCategory.Nom
        ElseIf TypeOf Me Is DTOProductSku Then
            Dim oSku As DTOProductSku = CType(Me, DTOProductSku)
            retval = oSku.NomCurt
        End If
        Return retval
    End Function

    Shared Function GetNom(src As DTOProduct) As String
        Dim retval As String = ""
        If TypeOf src Is DTOProductBrand Then
            retval = CType(src, DTOProductBrand).Nom
        ElseIf TypeOf src Is DTOProductCategory Then
            retval = CType(src, DTOProductCategory).Nom
        ElseIf TypeOf src Is DTOProductSku Then
            retval = CType(src, DTOProductSku).Nom
        End If
        Return retval
    End Function

    Shared Function Brand(oProduct As DTOProduct) As DTOProductBrand
        Dim retval As DTOProductBrand = Nothing
        If oProduct IsNot Nothing Then
            Select Case oProduct.SourceCod
                Case SourceCods.Brand
                    retval = oProduct
                Case SourceCods.Category
                    Dim oCategory As DTOProductCategory = oProduct
                    retval = oCategory.Brand
                Case SourceCods.SKU
                    Dim oSku As DTOProductSku = oProduct
                    retval = oSku.Category.Brand
            End Select
        End If
        Return retval
    End Function

    Shared Function CategoryOrBrand(oProduct As DTOProduct) As DTOProduct
        Dim retval As DTOProduct = oProduct
        If TypeOf retval Is DTOProductSku Then
            retval = CType(oProduct, DTOProductSku).category
        End If
        Return retval
    End Function

    Public Function Is4moms() As Boolean
        Dim o4moms = DTOProductBrand.wellknown(DTOProductBrand.wellknowns.FourMoms)
        Dim retval As Boolean = o4moms.Guid.Equals(MyBase.Guid)
        Return retval
    End Function

    Shared Function BrandNom(src As DTOProduct) As String
        Dim retval As String = ""
        Dim oBrand = DTOProduct.Brand(src)
        If oBrand IsNot Nothing Then
            retval = oBrand.Nom
        End If
        Return retval
    End Function

    Shared Function BrandCodDist(src As DTOProduct) As DTOProductBrand.CodDists
        Dim retval = DTOProductBrand.CodDists.Free
        Dim oBrand = DTOProduct.Brand(src)
        If oBrand IsNot Nothing Then
            retval = oBrand.codDist
        End If
        Return retval
    End Function

    Shared Function Category(src As DTOProduct) As DTOProductCategory
        Dim retval As DTOProductCategory = Nothing
        If src IsNot Nothing Then
            If TypeOf src Is DTOProductCategory Then
                retval = src
            ElseIf TypeOf src Is DTOProductSku Then
                retval = CType(src, DTOProductSku).Category
            End If
        End If
        Return retval
    End Function

    Shared Function CategoryNom(src As DTOProduct) As String
        Dim retval As String = ""
        Dim oCategory As DTOProductCategory = Category(src)
        If oCategory IsNot Nothing Then
            retval = oCategory.Nom
        End If
        Return retval
    End Function

    Shared Function Sku(src As DTOProduct) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        If TypeOf src Is DTOProductSku Then
            retval = CType(src, DTOProductSku)
        End If
        Return retval
    End Function

    Shared Function SkuNom(src As DTOProduct) As String
        Dim retval As String = ""
        Dim oSku As DTOProductSku = Sku(src)
        If oSku IsNot Nothing Then
            retval = oSku.Nom
            If retval = "" Then retval = oSku.NomCurt
            If retval = "" Then retval = oSku.NomLlarg
        End If
        Return retval
    End Function

    Shared Function SkuCostEur(src As DTOProduct) As Decimal
        Dim retval As Decimal = 0
        Dim oSku As DTOProductSku = Sku(src)
        If oSku IsNot Nothing Then
            If oSku.Cost IsNot Nothing Then
                retval = oSku.Cost.Eur
            End If
        End If
        Return retval
    End Function

    Shared Function TabCaption(oTab As DTOProduct.Tabs, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oTab
            Case DTOProduct.Tabs.accesorios
                retval = oLang.tradueix("Accesorios", "Accessoris", "Accessories")
            Case DTOProduct.Tabs.coleccion
                retval = oLang.tradueix("Colección", "Col.lecció", "Collection")
            Case DTOProduct.Tabs.descargas
                retval = oLang.tradueix("Descargas", "Descárregues", "Downloads")
            Case DTOProduct.Tabs.distribuidores
                retval = oLang.tradueix("Puntos de venta", "Punts de venda", "Sale points", "Pontos de venda")
            Case DTOProduct.Tabs.galeria
                retval = oLang.tradueix("Galería de imágenes", "Galería de imatges", "Image gallery", "Galeria de imágenes")
            Case DTOProduct.Tabs.videos
                retval = oLang.tradueix("Galería de videos", "Galería de videos", "Video gallery")
        End Select
        Return retval
    End Function

    Shared Function CodiMercancia(src As DTOProduct) As DTOCodiMercancia
        Dim retval As DTOCodiMercancia = Nothing
        If src IsNot Nothing Then
            If TypeOf src Is DTOProductBrand Then
                Dim oBrand As DTOProductBrand = src
                retval = oBrand.CodiMercancia
            ElseIf TypeOf src Is DTOProductCategory Then
                Dim oCategory As DTOProductCategory = src
                retval = DTOProductCategory.CodiMercanciaOrInherited(oCategory)
            ElseIf TypeOf src Is DTOProductSku Then
                Dim oSku As DTOProductSku = src
                retval = DTOProductSku.CodiMercanciaOrInherited(oSku)
            End If
        End If
        Return retval
    End Function

    Shared Function WebPageTitle(oProductBase As DTOProduct, oLang As DTOLang, Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional oLocation As DTOLocation = Nothing) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("M+O | ")

        If oTab <> DTOProduct.Tabs.general Then
            sb.Append(TabCaption(oTab, oLang) & " ")
        End If

        Select Case oProductBase.sourceCod
            Case DTOProduct.SourceCods.Brand
                Dim oBrand As DTOProductBrand = oProductBase
                sb.Append(oBrand.nom)
            Case DTOProduct.SourceCods.Category
                Dim oCategory As DTOProductCategory = oProductBase
                sb.Append(oCategory.Brand.nom)
                sb.Append(" ")
                sb.Append(oCategory.nom)
            Case DTOProduct.SourceCods.SKU
                Dim oSKU As DTOProductSku = oProductBase
                sb.Append(oSKU.category.Brand.nom)
                sb.Append(" ")
                sb.Append(oSKU.category.nom)
                sb.Append(" ")
                sb.Append(oSKU.nomCurt)
        End Select

        If oLocation IsNot Nothing Then
            sb.Append(oLang.tradueix(" en ", " a ", " on "))
            sb.Append(oLocation.nom)
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Function Excerpt(oProductBase As DTOProduct, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oProductBase.sourceCod
            Case DTOProduct.SourceCods.Brand
                Dim oBrand As DTOProductBrand = oProductBase
                retval = oLang.tradueix(oBrand.tagline_Esp, oBrand.tagline_Cat, oBrand.tagline_Eng)
            Case DTOProduct.SourceCods.Category
                Dim oCategory As DTOProductCategory = oProductBase
                retval = oCategory.excerpt.tradueix(oLang)
            Case DTOProduct.SourceCods.SKU
                Dim oSKU As DTOProductSku = oProductBase
                retval = oSKU.excerpt.tradueix(oLang)
        End Select
        Return retval
    End Function

    Shared Function Description(oProductBase As DTOProduct, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oProductBase.sourceCod
            Case DTOProduct.SourceCods.Brand
                Dim oBrand As DTOProductBrand = oProductBase
                retval = oBrand.title_Esp
            Case DTOProduct.SourceCods.Category
                Dim oCategory As DTOProductCategory = oProductBase
                retval = oCategory.description.tradueix(oLang)
            Case DTOProduct.SourceCods.SKU
                Dim oSKU As DTOProductSku = oProductBase
                retval = oSKU.description.tradueix(oLang)
        End Select
        Return retval
    End Function

    Shared Function BrandDownloadsRef(iQty As Integer, oSrc As DTOProductDownload.Srcs, oLang As DTOLang) As String
        Dim retval As String = ""
        If iQty = 1 Then
            Select Case oSrc
                Case DTOProductDownload.Srcs.Catalogos
                    retval = String.Format("{0} {1}", iQty, oLang.tradueix("catálogo", "cataleg", "catalog"))
                Case DTOProductDownload.Srcs.Instrucciones
                    retval = String.Format("{0} {1}", iQty, oLang.tradueix("manual", "manual", "user manual"))
                Case DTOProductDownload.Srcs.Compatibilidad
                    retval = String.Format("{0} {1}", iQty, oLang.tradueix("lista", "llista", "list"))
            End Select
        Else
            Select Case oSrc
                Case DTOProductDownload.Srcs.Catalogos
                    retval = String.Format("{0} {1}", iQty, oLang.tradueix("catálogos", "catalegs", "catalogues"))
                Case DTOProductDownload.Srcs.Instrucciones
                    retval = String.Format("{0} {1}", iQty, oLang.tradueix("manuales", "manuals", "user manuals"))
                Case DTOProductDownload.Srcs.Compatibilidad
                    retval = String.Format("{0} {1}", iQty, oLang.tradueix("listas", "llistes", "lists"))
            End Select
        End If
        Return retval
    End Function

    Shared Function Launchment(oProduct As DTOProduct, oLang As DTOLang) As String
        Dim retval As String = ""
        If oProduct IsNot Nothing Then
            Try
                If oLang Is Nothing Then oLang = DTOLang.ESP
                Dim oYearMonth As DTOYearMonth = Nothing
                Select Case oProduct.SourceCod
                    Case DTOProduct.SourceCods.Category
                        Dim oCategory As DTOProductCategory = oProduct
                        oYearMonth = oCategory.Launchment
                    Case DTOProduct.SourceCods.SKU
                        Dim oSku As DTOProductSku = oProduct
                        oYearMonth = oSku.Category.Launchment
                End Select

                If oYearMonth IsNot Nothing Then
                    If DTOYearMonth.Current.Tag < oYearMonth.Tag Then
                        retval = String.Format("{0} {1} {2}", oLang.tradueix("disponible a partir de", "disponible a partir de", "availability"), oLang.Mes(oYearMonth.Month), oYearMonth.Year)
                    Else
                        If oYearMonth.IsOutdated6MonthsOrMore Then
                        Else
                            retval = String.Format("{0} {1} {2}", oLang.tradueix("novedad", "novetat", "new from"), oLang.Mes(oYearMonth.Month), oYearMonth.Year)
                        End If
                    End If
                End If

            Catch ex As Exception
            End Try
        End If
        Return retval
    End Function

    Shared Function Proveidor(src As DTOProduct) As DTOProveidor
        Dim retval As DTOProveidor = Nothing
        Dim oBrand As DTOProductBrand = Brand(src)
        If oBrand IsNot Nothing Then
            retval = oBrand.Proveidor
        End If
        Return retval
    End Function

    Shared Function EncodedUrlSegment(nom As String) As String
        Dim retval As String = nom.ToLower
        retval = retval.Replace(" ", "_")
        retval = retval.Replace("&", "|")
        retval = retval.Replace("á", "a")
        retval = retval.Replace("é", "e")
        retval = retval.Replace("í", "i")
        retval = retval.Replace("ó", "o")
        retval = retval.Replace("ö", "o")
        retval = retval.Replace("ú", "u")
        retval = retval.Replace("ü", "u")
        Return retval
    End Function

    Shared Function DecodedUrlSegment(encodedNom As String) As String
        Dim retval As String = encodedNom
        retval = retval.Replace("_", " ")
        retval = retval.Replace("|", "&")
        Return retval
    End Function

    Public Function url(Optional oTab As Tabs = DTOProduct.Tabs.general) As String
        Dim retval As String = ""
        Select Case _sourceCod
            Case SourceCods.Brand
                retval = CType(Me, DTOProductBrand).getUrl(oTab)
            Case SourceCods.Dept
                retval = CType(Me, DTODept).getUrl(oTab)
            Case SourceCods.Category
                retval = CType(Me, DTOProductCategory).getUrl(oTab)
            Case SourceCods.SKU
                retval = MmoUrl.Factory(False, "sku", Me.Guid.ToString())
                'retval = CType(Me, DTOProductSku).getUrl(oTab)
        End Select
        Return retval
    End Function

    Public Function thumbnailUrl() As String
        Dim retval As String = ""
        Select Case _sourceCod
            Case SourceCods.Brand
                retval = CType(Me, DTOProductBrand).thumbnailUrl()
            Case SourceCods.Category
                retval = CType(Me, DTOProductCategory).thumbnailUrl()
            Case SourceCods.SKU
                retval = MmoUrl.Image(Defaults.ImgTypes.Art150, MyBase.Guid)
                'retval = CType(Me, DTOProductSku).ThumbnailUrl()
        End Select
        Return retval
    End Function
End Class
