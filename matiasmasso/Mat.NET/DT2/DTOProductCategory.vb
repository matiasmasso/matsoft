
Public Class DTOProductCategory
    Inherits DTOProduct

    Shadows Property Brand As DTOProductBrand

    Property nom2 As DTOLangText
    Property id As Integer
    Property dsc_PropagateToChildren As Boolean
    Property bloqEShops As Boolean
    Property innerPack As Integer
    Property outerPack As Integer
    Property forzarInnerPack As Boolean
    Shadows Property codiMercancia As DTOCodiMercancia
    Property skus As List(Of DTOProductSku)
    Property codi As Codis
    Property cNap As DTOCnap
    Property enabledxConsumer As Boolean
    Property enabledxPro As Boolean
    Property noStk As Boolean

    Property ord As Integer
    Shadows Property description As DTOLangText '<AllowHtml>
    Shadows Property excerpt As DTOLangText '<AllowHtml>

    Property noDimensions As Boolean
    Property dimensionL As Integer
    Property dimensionW As Integer
    Property dimensionH As Integer
    Property kgBrut As Decimal
    Property kgNet As Decimal
    Property volumeM3 As Decimal
    Property packageEan As DTOEan
    Property isBundle As Boolean
    Property madeIn As DTOCountry


    Property fchLastEdited As Date


    <JsonIgnore> Property thumbnail As Image
    Shadows Property launchment As DTOYearMonth
    Property hideUntil As Date

    Property usrLog As DTOUsrLog

    Public Enum wellknowns
        rockaRoo
        dualfix_iSize
    End Enum


    Public Enum Codis
        Standard
        Accessories
        Spareparts
        POS
        Others
    End Enum

    Public Enum SortOrders
        Alfabetic
        Custom
    End Enum

    Public Sub New()
        MyBase.New()
        MyBase.SourceCod = SourceCods.Category
        _Skus = New List(Of DTOProductSku)
        _Excerpt = New DTOLangText
        _Description = New DTOLangText
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        MyBase.SourceCod = SourceCods.Category
        _Skus = New List(Of DTOProductSku)
        _Excerpt = New DTOLangText
        _Description = New DTOLangText
    End Sub

    Shared Shadows Function Factory(oBrand As DTOProductBrand) As DTOProductCategory
        Dim retval As New DTOProductCategory
        With retval
            .Brand = oBrand
        End With
        Return retval
    End Function

    Shared Function wellknown(id As DTOProductCategory.wellknowns) As DTOProductCategory
        Dim retval As DTOProductCategory = Nothing
        Select Case id
            Case wellknowns.dualfix_iSize
                retval = New DTOProductCategory(New Guid("7318FF90-5847-4D73-9B5B-4DAFC168810B"))
            Case wellknowns.rockaRoo
                retval = New DTOProductCategory(New Guid("FDCAD204-4EF1-49AE-90A9-537AC04FBD19"))
        End Select
        Return retval
    End Function

    Public Function Clon() As DTOProductCategory
        Dim exs As New List(Of Exception)
        Dim retval As New DTOProductCategory
        DTOBaseGuid.CopyPropertyValues(Of DTOProductCategory)(Me, retval, exs)
        Return retval
    End Function

    Shared Shadows Function FullNom(oCategory As DTOProductCategory) As String
        Dim retval As String = ""
        If oCategory.Brand Is Nothing Then
            retval = oCategory.Nom
        Else
            retval = String.Format("{0} {1}", oCategory.Brand.Nom, oCategory.Nom)
        End If
        Return retval
    End Function

    Public Function getUrl(Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = MmoUrl.Factory(AbsoluteUrl, DTOProductBrand.UrlSegment(Me.Brand), DTOProductCategory.UrlSegment(Me))

        If oTab <> DTOProduct.Tabs.general Then
            retval = retval & "/" & oTab.ToString
        End If

        Return retval
    End Function

    Overloads Shared Function UrlSegment(oCategory As DTOProductCategory) As String
        Dim retval As String = ""
        If oCategory IsNot Nothing Then
            retval = oCategory.UrlSegment()
        End If
        Return retval
    End Function

    Public Overloads Function UrlSegment() As String
        Return MatHelperStd.UrlHelper.EncodedUrlSegment(MyBase.Nom)
    End Function

    Public Function UrlFullSegment(Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general) As String
        Dim retval = String.Format("{0}/{1}", _Brand.UrlSegment, UrlSegment)

        If oTab <> DTOProduct.Tabs.general Then
            retval = String.Format("{0}/{1}", retval, oTab.ToString())
        End If

        Return retval
    End Function




    Shared Function CodiMercanciaOrInherited(oCategory As DTOProductCategory) As DTOCodiMercancia
        Dim retval As DTOCodiMercancia = Nothing
        If oCategory.CodiMercancia Is Nothing Then
            retval = oCategory.Brand.CodiMercancia
        Else
            retval = oCategory.CodiMercancia
        End If
        Return retval
    End Function

    Shared Function SelfOrInheritedCnapFullNom(oCategory As DTOProductCategory, oLang As DTOLang) As String
        Dim retval As String = ""
        If oCategory IsNot Nothing Then
            If oCategory.CNap Is Nothing Then
                If oCategory.Brand IsNot Nothing AndAlso oCategory.Brand.Cnap IsNot Nothing Then
                    retval = oCategory.Brand.Cnap.FullNom(oLang)
                End If
            Else
                retval = oCategory.CNap.FullNom(oLang)
            End If
        End If
        Return retval
    End Function

    Shared Function ExcerptOrShortDescription(oCategory As DTOProductCategory, ByVal oLang As DTOLang, Optional ByVal MaxLen As Integer = 0, Optional BlAppendEllipsis As Boolean = True) As String
        Dim retval As String = ""
        If oCategory.Excerpt IsNot Nothing Then
            retval = oCategory.Excerpt.tradueix(oLang)
        End If
        If retval = "" Then
            Dim sText As String = oCategory.Description.tradueix(oLang)
            retval = TextHelper.Excerpt(sText, MaxLen, BlAppendEllipsis)
        End If
        Return retval
    End Function

    Shared Function MadeInOrInherited(oCategory As DTOProductCategory) As DTOCountry
        Dim retval As DTOCountry = Nothing
        If oCategory Is Nothing Then
        Else
            If oCategory.MadeIn Is Nothing Then
                If oCategory.Brand Is Nothing Then
                Else
                    retval = oCategory.Brand.MadeIn
                End If
            Else
                retval = oCategory.MadeIn
            End If
        End If
        Return retval
    End Function

    Shared Function ToGuidNom(oCategory As DTOProductCategory) As DTOGuidNom
        Dim retval As New DTOGuidNom(oCategory.Guid, DTOProductCategory.FullNom(oCategory))
        Return retval
    End Function

    Shared Function ToGuidNoms(oCategories As IEnumerable(Of DTOProductCategory)) As List(Of DTOGuidNom)
        Dim retval As New List(Of DTOGuidNom)
        For Each oCategory In oCategories
            Dim item = DTOProductCategory.ToGuidNom(oCategory)
            retval.Add(item)
        Next
        Return retval
    End Function

    Public Function PluginCollection() As String
        Return String.Format("<iframe src='https://www.matiasmasso.es/plugin/skucolors/{0}' style='border:0;' width='100%' height='205px'></iframe><br/>", MyBase.Guid.ToString())
    End Function

    Public Function PluginRelatedSkus() As String
        'Return String.Format("<iframe src='https://www.matiasmasso.es/plugin/RelatedSkus/{0}' style='border:0;' width='100%' height='205px'></iframe><br/>", MyBase.Guid.ToString())
        Return String.Format("<iframe src='https://www.matiasmasso.es/plugin/RelatedProducts/{0}' style='border:0;' width='100%' height='205px'></iframe><br/>", MyBase.Guid.ToString())
    End Function

    Public Function SpriteSkuColorsUrl(width As Integer, height As Integer) As String
        Return MmoUrl.ApiUrl("ProductCategory/skuColors/sprite", MyBase.Guid.ToString, width, height)
    End Function
End Class
