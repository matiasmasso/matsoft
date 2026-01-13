Public Class DTOProductSku
    Inherits DTOProduct

    Shadows Property category As DTOProductCategory
    Property id As Integer
    Shadows Property nomCurt As String
    Property nomCurtLangText As DTOLangText
    Property nomLlarg As String

    Property refCustomer As String 'TO DEPRECATE to CustomerProduct
    Property customerProduct As DTOCustomerProduct
    Property refProveidor As String
    Property ean13 As DTOEan
    Property packageEan As DTOEan
    Property cnap As DTOCnap
    Property nomProveidor As String
    Property hereda As Boolean


    Property innerPack As Integer
    Property forzarInnerPack As Boolean
    Property heredaDimensions As Boolean

    <JsonIgnore> Property image As Image
    Property imageFch As DateTime
    Property imageExists As Boolean
    Property lastProduction As Boolean

    Property price As DTOAmt
    Property customerDto As Decimal
    Property supplierDtoOnInvoice As Decimal
    Property cost As DTOAmt
    Property lastPurchaseDate As Date

    Property dtoSobreRRPP As DTOCustomerTarifaDto
    Property rrpp As DTOAmt
    Property pmc As Decimal 'Preu mig de cost en euros

    Shadows Property excerpt As DTOLangText '<AllowHtml>

    Shadows Property description As DTOLangText '<AllowHtml>

    Property ivaCod As DTOTax.Codis = DTOTax.Codis.Iva_Standard

    Property stock As Integer
    Property noStk As Boolean
    Property noWeb As Boolean
    Property noPro As Boolean
    Property clients As Integer 'totes les unitats pendents de servir a clients
    Property clientsAlPot As Integer 'unitats de comandes en standby a la espera indefinida de confirmació
    Property clientsEnProgramacio As Integer 'unitats en programació a mes de una setmana vista
    Property clientsBlockStock As Integer 'unitats de comandes amb stock reservat
    Property proveidors As Integer
    Property previsions As Integer

    Property dimensionL As Integer
    Property dimensionW As Integer
    Property dimensionH As Integer
    Property kgBrut As Decimal
    Property volumeM3 As Decimal

    Property madeIn As DTOCountry

    Shadows Property codiMercancia As DTOCodiMercancia

    Shadows Property url As String
    Property virtual As Boolean
    'Property PackItems As List(Of DTOProductPackItem)

    Property skuWiths As List(Of DTOSkuWith)

    Property keys As New List(Of String)

    Property outlet As Boolean
    Property outletDto As Decimal
    Property outletQty As Integer
    Property fchLastEdited As Date

    Property codExclusio As CodisExclusio = CodisExclusio.Inclos

    '000=====================================
    Property noTarifa As Boolean
    Property hideUntil As Date
    Property noMgz As Boolean
    Property noDimensions As Boolean
    Property outerPack As Integer
    Property kgNet As Decimal
    Property isBundle As Boolean
    Property bundleDto As Decimal
    Property bundleSkus As List(Of DTOSkuBundle)
    Property usrLog As DTOUsrLog


    Public Enum CodisExclusio
        Inclos
        Canal
        Exclusives
        PremiumLine
        OutOfCatalog
    End Enum

    Public Enum wellknowns
        none
        mamaRoo
        kidfix
        ManoDeObraSinCargo
        ManoDeObra
        MaterialEmpleado
        Embalaje
        Transport
        LogisticCharges
        UnknownSku
        ReferenciaEspecial
    End Enum

    Public Enum BundleCods
        none
        parent
        child
    End Enum



    Public Sub New()
        MyBase.New()
        MyBase.sourceCod = SourceCods.SKU
        _nomCurtLangText = New DTOLangText
        _excerpt = New DTOLangText
        _description = New DTOLangText
        _bundleSkus = New List(Of DTOSkuBundle)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        MyBase.sourceCod = SourceCods.SKU
        _nomCurtLangText = New DTOLangText
        _excerpt = New DTOLangText
        _description = New DTOLangText
        _bundleSkus = New List(Of DTOSkuBundle)
    End Sub


    Shared Shadows Function Factory(oUser As DTOUser, oCategory As DTOProductCategory) As DTOProductSku
        Dim retval As New DTOProductSku
        With retval
            .category = oCategory
            .ivaCod = DTOTax.Codis.Iva_Standard
            .usrLog = DTOUsrLog.Factory(oUser)
        End With
        Return retval
    End Function

    Shared Function wellknown(owellknown As DTOProductSku.wellknowns) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Select Case owellknown
            Case DTOProductSku.wellknowns.mamaRoo
                retval = New DTOProductSku(New Guid("135FE602-D139-4013-8F2B-ACA69D7BC5FF"))
            Case DTOProductSku.wellknowns.kidfix
                retval = New DTOProductSku(New Guid("C0CAEF04-7D25-42FF-B1CB-7277E2EA1258"))
            Case DTOProductSku.wellknowns.ManoDeObraSinCargo
                retval = New DTOProductSku(New Guid("F6445139-4BD3-426F-90AF-95C0CE13D413"))
            Case DTOProductSku.wellknowns.ManoDeObra
                retval = New DTOProductSku(New Guid("21D9ADAD-06F2-408C-824F-C4622F6EE362"))
            Case DTOProductSku.wellknowns.MaterialEmpleado
                retval = New DTOProductSku(New Guid("08B96F83-1C07-43F8-B2F5-3D8AA26F1C1F"))
            Case DTOProductSku.wellknowns.Embalaje
                retval = New DTOProductSku(New Guid("B2C01147-8048-4430-A9B7-74FF3E4EC0C3"))
            Case DTOProductSku.wellknowns.Transport
                retval = New DTOProductSku(New Guid("210EE215-A1D4-44F0-A134-7E372F5F5A26"))
            Case DTOProductSku.wellknowns.LogisticCharges
                retval = New DTOProductSku(New Guid("63456678-9AF3-4330-8CCC-6A7927A79BE1"))
            Case DTOProductSku.wellknowns.UnknownSku
                retval = New DTOProductSku(New Guid("2FFE0A89-82ED-4A79-8D7A-CFC53693AEF3"))
            Case wellknowns.ReferenciaEspecial
                retval = New DTOProductSku(New Guid("72BE21F1-4B37-4C4C-BEBC-72C417401D28"))
        End Select
        Return retval
    End Function

    Public Function NomLlargNoRef() As String
        Dim retval As String = _nomLlarg
        If Not String.IsNullOrEmpty(_refProveidor) Then
            If retval.StartsWith(_refProveidor) Then
                retval = retval.Replace(_refProveidor, "").Trim
            End If
        End If
        Return retval
    End Function

    Shared Function BackColor(oSku As DTOProductSku) As Color
        Dim retval As Color = Color.Transparent
        If oSku.stock > 0 Then
            If oSku.stock > oSku.clients Then
                retval = Color.LightGreen
            Else
                retval = Color.Yellow
            End If
        Else
            If oSku.obsoleto Then
                retval = Color.LightGray
            Else
                retval = Color.LightSalmon
            End If
        End If
        Return retval
    End Function

    Shared Function BackColor(stock As Integer, clients As Integer) As Color
        Dim retval As Color = Color.Transparent
        If stock > 0 Then
            If stock > clients Then
                retval = Color.LightGreen
            Else
                retval = Color.Yellow
            End If
        Else
            retval = Color.LightSalmon
        End If
        Return retval
    End Function

    Shared Function Moq(oSku As DTOProductSku) As Integer
        Dim retval As Integer = 1
        If oSku.heredaDimensions Then
            If oSku.category.forzarInnerPack Then
                retval = oSku.category.innerPack
            End If
        Else
            If oSku.forzarInnerPack Then
                If oSku.innerPack > 0 Then
                    retval = oSku.innerPack
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function GetCliProductDto(oSku As DTOProductSku, oDtos As List(Of DTOCliProductDto)) As DTOCliProductDto
        'If oSku.Category Is Nothing Then Stop

        Dim retval As DTOCliProductDto = Nothing
        If oSku IsNot Nothing And oDtos.Count > 0 Then
            retval = oDtos.Find(Function(x) x.Product.Equals(oSku))
            If retval Is Nothing And oSku.category IsNot Nothing Then
                retval = oDtos.Find(Function(x) x.Product.Equals(oSku.category))
                If retval Is Nothing And oSku.category.Brand IsNot Nothing Then
                    retval = oDtos.Find(Function(x) x.Product.Equals(oSku.category.Brand))

                End If
            End If
        End If
        Return retval
    End Function

    Public Function StockAvailable() As Integer
        Dim retval As Integer = _stock - (_clients - _clientsAlPot - _clientsEnProgramacio)
        If retval < 0 Then retval = 0
        Return retval
    End Function

    Shared Function Ean(oSku As DTOProductSku) As String
        Dim retval As String = ""
        If oSku IsNot Nothing Then
            If oSku.ean13 IsNot Nothing Then
                retval = oSku.ean13.value
            End If
        End If
        Return retval
    End Function

    Public Function getUrl(Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = _url
        If String.IsNullOrEmpty(retval) Then
            If Me.category IsNot Nothing AndAlso Me.category.Brand IsNot Nothing Then
                retval = MmoUrl.Factory(AbsoluteUrl, DTOProductBrand.UrlSegment(Me.category.Brand), DTOProductCategory.UrlSegment(Me.category), DTOProductSku.UrlSegment(Me))
            Else
                retval = MmoUrl.Factory(AbsoluteUrl, "sku", MyBase.Guid.ToString())
            End If
        End If

        If oTab <> DTOProduct.Tabs.general Then
            retval = retval & "/" & oTab.ToString
        End If

        Return retval
    End Function

    Shared Shadows Function UrlSegment(oSku As DTOProductSku) As String
        Dim retval As String = ""
        If oSku IsNot Nothing Then
            If Not String.IsNullOrEmpty(oSku.NomCurtOrNom) Then
                retval = MatHelperStd.UrlHelper.EncodedUrlSegment(oSku.NomCurtOrNom.ToLower)
            End If
        End If
        Return retval
    End Function

    Public Function DefaultUrl() As String
        Dim retval As String = ""
        If _category IsNot Nothing AndAlso _category.Brand IsNot Nothing Then
            retval = MmoUrl.Factory(True, _category.Brand.UrlSegment, _category.UrlSegment, UrlSegment(Me))
        End If
        Return retval
    End Function

    Public Function UrlOrDefault() As String
        Dim retval As String = IIf(String.IsNullOrEmpty(url), DefaultUrl(), url)
        Return retval
    End Function

    Public Function ImageUrl(Optional AbsoluteUrl As Boolean = False) As String
        Return MmoUrl.Image(Defaults.ImgTypes.Art, MyBase.Guid, AbsoluteUrl)
    End Function

    Public Shadows Function ThumbnailUrl(Optional AbsoluteUrl As Boolean = False) As String
        Return MmoUrl.Image(Defaults.ImgTypes.Art150, MyBase.Guid, AbsoluteUrl)
    End Function


    Public Function NomCurtOrNom() As String
        Dim retval As String = _nomCurt
        If retval = "" Then retval = MyBase.nom
        Return retval
    End Function

    Shared Function NomPrvOrMyd(oSku As DTOProductSku) As String
        Dim retval As String = oSku.nomProveidor
        If retval = "" Then retval = oSku.nomLlarg
        Return retval
    End Function

    Shared Function RefYNomPrv(oSku As DTOProductSku) As String
        Dim sNom As String = oSku.nomProveidor
        Dim sRef As String = oSku.refProveidor
        Dim sb As New System.Text.StringBuilder
        If sRef > "" Then
            If Not sNom.Contains(sRef) Then
                sb.Append(sRef)
                sb.Append(" ")
            End If
        End If
        sb.Append(sNom)
        If sb.Length = 0 Then
            sb.Append(DTOProductSku.NomPrvOrMyd(oSku))
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Public Function CliProductDto(oDtos As List(Of DTOCliProductDto)) As DTOCliProductDto
        'If oSku.Category Is Nothing Then Stop

        Dim retval As DTOCliProductDto = Nothing
        If oDtos.Count > 0 Then
            retval = oDtos.Find(Function(x) x.Product.Guid.Equals(MyBase.Guid))
            If retval Is Nothing And _category IsNot Nothing Then
                retval = oDtos.Find(Function(x) x.Product.Equals(_category))
                If retval Is Nothing And _category.Brand IsNot Nothing Then
                    retval = oDtos.Find(Function(x) x.Product.Equals(_category.Brand))

                End If
            End If
        End If
        Return retval
    End Function

    Public Shadows Function BrandNom() As String
        Dim retval As String = ""
        If _category IsNot Nothing AndAlso _category.Brand IsNot Nothing Then
            retval = _category.Brand.nom
        End If
        Return retval
    End Function

    Public Shadows Function CategoryNom() As String
        Dim retval As String = ""
        If _category IsNot Nothing Then
            retval = _category.nom
        End If
        Return retval
    End Function


    Public Function RefYNomPrv() As String
        Dim sNom As String = _nomProveidor
        Dim sRef As String = _refProveidor
        Dim sb As New System.Text.StringBuilder
        If sRef > "" Then
            If Not sNom.Contains(sRef) Then
                sb.Append(sRef)
                sb.Append(" ")
            End If
        End If
        sb.Append(sNom)
        If sb.Length = 0 Then
            sb.Append(NomPrvOrMyd())
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Shadows Function FullNom(oSku As DTOProductSku) As String
        Dim retval As String = oSku.nomLlarg
        If retval = "" Then
            If oSku.category IsNot Nothing Then
                Dim sCategory = oSku.category.nom
                If oSku.category.Brand IsNot Nothing Then
                    Dim sBrand As String = oSku.category.Brand.nom
                    retval = String.Format("{0} {1} {2}", sBrand, sCategory, oSku.nomCurt)
                End If
            End If
        End If
        If retval = "" Then retval = oSku.nom
        If retval = "" Then retval = DTOProductSku.RefYNomPrv(oSku)
        Return retval
    End Function


    Public Function NomPrvOrMyd() As String
        Dim retval As String = _nomProveidor
        If retval = "" Then retval = _nomLlarg
        Return retval
    End Function

    Public Function InnerPackOrInherited() As Integer
        Dim retval As Decimal = _innerPack
        If _heredaDimensions And _category IsNot Nothing Then
            retval = _category.innerPack
        End If
        If retval < 1 Then retval = 1
        Return retval
    End Function

    Shared Function InnerPackOrInherited(oSku As DTOProductSku) As Integer
        Dim retval As Decimal
        If oSku IsNot Nothing Then retval = oSku.InnerPackOrInherited
        Return retval
    End Function

    Shared Function SelfOrInheritedForzarInnerPack(oSku As DTOProductSku) As Boolean
        Dim retval As Boolean
        If oSku.heredaDimensions Then
            retval = oSku.category.forzarInnerPack
        Else
            retval = oSku.forzarInnerPack
        End If
        Return retval
    End Function


    Shared Function OuterPackOrInherited(oSku As DTOProductSku) As Integer
        Dim retval As Decimal
        If oSku IsNot Nothing Then retval = oSku.OuterPackOrInherited
        Return retval
    End Function

    Public Function OuterPackOrInherited() As Integer
        Dim retval As Decimal = _innerPack 'temporary *********************************************
        If _heredaDimensions And _category IsNot Nothing Then
            retval = _category.innerPack
        End If
        If retval < 1 Then retval = 1
        Return retval
    End Function

    Public Function MadeInOrInherited() As DTOCountry
        Dim retval As DTOCountry = _madeIn
        If retval Is Nothing And _category IsNot Nothing Then
            retval = _category.madeIn
            If retval Is Nothing And _category.Brand IsNot Nothing Then
                retval = _category.Brand.madeIn
            End If
        End If
        Return retval
    End Function
    Public Function MadeInOrInheritedISO() As String
        Dim oCountry As DTOCountry = _madeIn
        If oCountry Is Nothing And _category IsNot Nothing Then
            oCountry = _category.madeIn
            If oCountry Is Nothing And _category.Brand IsNot Nothing Then
                oCountry = _category.Brand.madeIn
            End If
        End If
        Dim retval As String = ""
        If oCountry IsNot Nothing Then retval = oCountry.ISO
        Return retval
    End Function

    Shared Function MadeInOrInheritedISO(oSku As DTOProductSku) As String
        Dim retval As String = ""
        If oSku IsNot Nothing Then
            retval = oSku.MadeInOrInheritedISO
        End If
        Return retval
    End Function

    Public Function DimensionLOrInherited() As Integer
        Dim retval As Integer = IIf(_heredaDimensions, _category.dimensionL, _dimensionL)
        Return retval
    End Function

    Public Function DimensionHOrInherited() As Integer
        Dim retval As Integer = IIf(_heredaDimensions, _category.dimensionH, _dimensionH)
        Return retval
    End Function

    Public Function DimensionWOrInherited() As Integer
        Dim retval As Integer = IIf(_heredaDimensions, _category.dimensionW, _dimensionW)
        Return retval
    End Function
    Public Function VolumeM3OrInherited() As Decimal
        Dim retval As Decimal
        retval = _volumeM3
        If _heredaDimensions Then
            If _category IsNot Nothing Then
                Dim oCategory As DTOProductCategory = _category
                retval = oCategory.volumeM3
                If retval = 0 Then retval = oCategory.dimensionL * oCategory.dimensionW * oCategory.dimensionH / 1000000000
                If oCategory.innerPack > 1 Then
                    retval = retval / oCategory.innerPack
                End If
            End If
        Else
            retval = _volumeM3
            If retval = 0 Then retval = _dimensionL * _dimensionW * _dimensionH / 1000000000
            If _innerPack > 1 Then
                retval = retval / _innerPack
            End If
        End If

        Return retval
    End Function

    Shared Function VolumeM3OrInherited(oSku As DTOProductSku) As Decimal
        Dim retval As Decimal
        If oSku IsNot Nothing Then
            retval = oSku.VolumeM3OrInherited
        End If
        Return retval
    End Function


    Public Function WeightKgOrInherited() As Integer
        Dim retval As Decimal = _kgBrut
        If _heredaDimensions And _category IsNot Nothing Then
            retval = _category.kgBrut
        End If
        Return retval
    End Function

    Shared Function WeightKgOrInherited(oSku As DTOProductSku) As Decimal
        Dim retval As Decimal
        If oSku IsNot Nothing Then
            retval = oSku.WeightKgOrInherited
        End If
        Return retval
    End Function


    Shared Function KgNetOrInheritedOrBrut(oSku As DTOProductSku) As Decimal
        Dim retval As Decimal = DTOProductSku.KgNetOrInherited(oSku)
        If retval = 0 Then retval = oSku.WeightKgOrInherited
        Return retval
    End Function

    Shared Function KgNetOrInherited(oSku As DTOProductSku) As Decimal
        Dim retval As Decimal = IIf(oSku.heredaDimensions, oSku.category.kgNet, oSku.kgNet)
        Return retval
    End Function

    Shared Function MadeInOrInherited(oSku As DTOProductSku) As DTOCountry
        Dim retval As DTOCountry = oSku.madeIn
        If retval Is Nothing And oSku.category IsNot Nothing Then
            retval = oSku.category.madeIn
            If retval Is Nothing And oSku.category.Brand IsNot Nothing Then
                retval = oSku.category.Brand.madeIn
            End If
        End If
        Return retval
    End Function



    Public Function IncludeCategoryWithAccessoryNom() As Boolean
        Dim retval As Boolean
        If _category IsNot Nothing Then
            If _category.codi = DTOProductCategory.Codis.Accessories Then
                If Not _category.nom.ToLower.StartsWith("acces") Then
                    retval = True
                End If
            End If
        End If
        Return retval
    End Function

    Public Function CodiMercanciaOrInherited() As DTOCodiMercancia
        Dim retval As DTOCodiMercancia = Nothing
        If _codiMercancia Is Nothing OrElse Not IsNumeric(_codiMercancia.Id) Then
            retval = _category.codiMercancia
        Else
            retval = _codiMercancia
        End If
        Return retval
    End Function

    Public Function CodiMercanciaNomOrInherited() As String
        Dim retval As String = ""
        Dim oCodiMercancia = CodiMercanciaOrInherited()
        If oCodiMercancia IsNot Nothing Then
            retval = oCodiMercancia.Dsc
        End If
        Return retval
    End Function

    Public Function CodiMercanciaIdOrInherited() As String
        Dim retval As String = ""
        Dim oCodiMercancia = CodiMercanciaOrInherited()
        If oCodiMercancia IsNot Nothing Then
            retval = oCodiMercancia.Id
        End If
        Return retval
    End Function

    Shared Function CodiMercanciaOrInherited(oSku As DTOProductSku) As DTOCodiMercancia
        Dim retval As DTOCodiMercancia = Nothing
        If oSku.codiMercancia Is Nothing OrElse Not IsNumeric(oSku.codiMercancia.Id) Then
            retval = DTOProductCategory.CodiMercanciaOrInherited(oSku.category)
        Else
            retval = oSku.codiMercancia
        End If
        Return retval
    End Function

    Shared Function IsTruncatedStock(oSku As DTOProductSku, oRol As DTORol) As Boolean
        Dim retval As Boolean
        Dim iDisplayableClients As Integer = oSku.clients - oSku.clientsAlPot - oSku.clientsEnProgramacio
        Dim iAvailableStock As Integer = IIf(oSku.stock > iDisplayableClients, oSku.stock - iDisplayableClients, 0)
        Select Case oRol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.SalesManager, DTORol.Ids.Rep, DTORol.Ids.Comercial, DTORol.Ids.Manufacturer
            Case Else
                retval = (iAvailableStock > 10)
        End Select
        Return retval
    End Function

    Public Function IsAvailable() As Boolean
        Dim retval As Boolean
        If MyBase.Guid.Equals(DTOProductSku.wellknown(wellknowns.ReferenciaEspecial).Guid) Then
        ElseIf Me.stock > 0 Then
            retval = Me.stock > Me.clients
        End If
        Return retval
    End Function

    Shared Function StockAvailable(oSku As DTOProductSku) As Integer
        Dim retval As Integer = oSku.stock - (oSku.clients - oSku.clientsAlPot - oSku.clientsEnProgramacio)
        If retval < 0 Then retval = 0
        Return retval
    End Function

    Shared Function TruncatedStockValue(oSku As DTOProductSku, oRol As DTORol) As String
        Dim retval As String = ""
        Dim iStk As Integer = TruncatedStock(oSku, oRol)

        If DTOProductSku.IsTruncatedStock(oSku, oRol) Then
            retval = String.Format("{0}+", iStk)
        Else
            retval = iStk.ToString
        End If

        Return retval
    End Function

    Shared Function TruncatedStock(oSku As DTOProductSku, oRol As DTORol) As Integer
        Dim retval As Integer
        Dim iDisplayableClients As Integer = oSku.clients - oSku.clientsAlPot - oSku.clientsEnProgramacio
        Dim iAvailableStock As Integer = IIf(oSku.stock > iDisplayableClients, oSku.stock - iDisplayableClients, 0)

        Select Case oRol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.SalesManager, DTORol.Ids.Rep, DTORol.Ids.Comercial, DTORol.Ids.Manufacturer
                retval = iAvailableStock
            Case Else
                retval = IIf(iAvailableStock > 10, 10, iAvailableStock)
        End Select
        Return retval
    End Function


    Shared Function Incentius(oProduct As DTOProduct, oSource As List(Of DTOIncentiu)) As List(Of DTOIncentiu)
        Dim retval As List(Of DTOIncentiu) = (From i In oSource
                                              From p In i.Products
                                              Where p.Equals(oProduct)
                                              Select i).ToList
        Return retval
    End Function

    Shared Function IsHidden(oSku As DTOProductSku) As Boolean
        'Amaga-ho al consumidor i a les comandes de professional fins a la data HideUntil mes llunyana
        Dim SkuHideUntil As Date = oSku.hideUntil
        Dim CategoryHideUntil As Date = oSku.category.hideUntil
        Dim retval As Boolean = SkuHideUntil > Today
        If CategoryHideUntil > Today Then retval = True
        Return retval
    End Function

    Shared Function SelfOrInheritedCnapFullNom(oSku As DTOProductSku, oLang As DTOLang) As String
        Dim retval As String = ""
        If oSku IsNot Nothing Then
            If oSku.cnap Is Nothing Then
                retval = DTOProductCategory.SelfOrInheritedCnapFullNom(oSku.category, oLang)
            Else
                retval = oSku.cnap.FullNom(oLang)
            End If
        End If
        Return retval
    End Function

    Shared Function Categories(oSkus As IEnumerable(Of DTOProductSku)) As List(Of DTOProductCategory)
        Dim retval As New List(Of DTOProductCategory)
        If oSkus IsNot Nothing Then
            retval = oSkus.GroupBy(Function(x) x.category.Guid).Select(Function(y) y.First).Select(Function(z) z.category).ToList
        End If
        Return retval
    End Function

    Shared Function GetCustomerCost(oSku As DTOProductSku, oDtos As List(Of DTOCustomerTarifaDto), Optional DtFch As Date = Nothing) As DTOAmt
        Dim retval As DTOAmt = Nothing

        If oSku.rrpp IsNot Nothing Then
            If DtFch = Nothing Then DtFch = Today
            Dim DcDto As Decimal = DTOCustomerTarifaDto.ProductDto(oDtos, oSku) ' oSku.Category.Brand)
            Dim DcCost As Decimal = Math.Round(oSku.rrpp.eur * (100 - DcDto) / 100, 2, MidpointRounding.AwayFromZero)
            retval = DTOAmt.factory(DcCost)
        End If
        Return retval
    End Function

    Shared Function GetCustomerCost(oSku As DTOProductSku, oCustomCosts As List(Of DTOPricelistItemCustomer), oDtos As List(Of DTOCustomerTarifaDto)) As DTOAmt
        Dim retval As DTOAmt = Nothing

        'prova si existeix tarifa personalitzada d'aquest client per aquest article
        Dim oCustomCost As DTOPricelistItemCustomer = oCustomCosts.Find(Function(x) x.Sku.Equals(oSku))
        If oCustomCost Is Nothing Then
            'si no n'hi ha, busca a la tarifa general i dedueix el descompte que pugui tenir

            Dim DcCost As Decimal

            If oSku.rrpp IsNot Nothing Then
                Dim RRPP As Decimal = oSku.rrpp.eur
                oSku.dtoSobreRRPP = Nothing
                Dim oDto As DTOCustomerTarifaDto = GetCustomerDto(oDtos, oSku)
                If oDto Is Nothing Then
                    DcCost = RRPP
                Else
                    oSku.dtoSobreRRPP = oDto
                    DcCost = Math.Round(RRPP * (100 - oSku.dtoSobreRRPP.Dto) / 100, 2, MidpointRounding.AwayFromZero)
                End If
            End If

            retval = DTOAmt.factory(DcCost)
        Else
            retval = oCustomCost.Retail
        End If

        Return retval
    End Function

    Shared Function GetCustomerDto(oDtos As List(Of DTOCustomerTarifaDto), oSku As DTOProductSku) As DTOCustomerTarifaDto
        Dim retval As DTOCustomerTarifaDto = oDtos.Find(Function(x) oSku.Equals(x.Product))
        If retval Is Nothing Then
            retval = oDtos.Find(Function(x) oSku.category.Equals(x.Product))
            If retval Is Nothing Then
                retval = oDtos.Find(Function(x) oSku.category.Brand.Equals(x.Product))
                If retval Is Nothing Then
                    retval = oDtos.Find(Function(x) x.Product Is Nothing)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function ToGuidNom(oSku As DTOProductSku) As DTOGuidNom
        Dim retval As New DTOGuidNom(oSku.Guid, DTOProductSku.FullNom(oSku))
        Return retval
    End Function

    Shared Function ToGuidNoms(oSkus As IEnumerable(Of DTOProductSku)) As List(Of DTOGuidNom)
        Dim retval As New List(Of DTOGuidNom)
        For Each oSku In oSkus
            Dim item = DTOProductSku.ToGuidNom(oSku)
            retval.Add(item)
        Next
        Return retval
    End Function

    Public Function MatchesFilter(searchkey As String) As Boolean
        Dim retval As Boolean
        searchkey = searchkey.ToLower
        If Not String.IsNullOrEmpty(_nomCurt) AndAlso _nomCurt.ToLower.Contains(searchkey) Then
            retval = True
        ElseIf Not String.IsNullOrEmpty(_nomLlarg) AndAlso _nomLlarg.ToLower.Contains(searchkey) Then
            retval = True
        ElseIf Not String.IsNullOrEmpty(_refProveidor) AndAlso _refProveidor.ToLower.Contains(searchkey) Then
            retval = True
        ElseIf Not String.IsNullOrEmpty(_nomProveidor) AndAlso _nomProveidor.ToLower.Contains(searchkey) Then
            retval = True
        ElseIf _id.ToString.Contains(searchkey) Then
            retval = True
        ElseIf _ean13 IsNot Nothing AndAlso _ean13.value.Contains(searchkey) Then
            retval = True
        End If
        Return retval
    End Function

    Shared Function Url2(oSku As DTOProductSku, oDomain As DTOWebPageAlias.domains, Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general) As String
        Dim retval As String = ""
        'Dim exs As New List(Of Exception) 
        ' If Load(oSku, exs) Then -----------RALENTEIX MOLT A TARIFAS

        If oSku IsNot Nothing AndAlso oSku.category IsNot Nothing AndAlso oSku.category.Brand IsNot Nothing Then
            retval = MmoUrl.Factory2(oDomain, DTOProductBrand.UrlSegment(oSku.category.Brand), DTOProductCategory.UrlSegment(oSku.category), DTOProductSku.UrlSegment(oSku))
            If oTab <> DTOProduct.Tabs.general Then
                retval = String.Format("{0}/{1}", retval, oTab.ToString())
            End If
        End If

        'End If
        Return retval
    End Function


    Public Class Switch
        Inherits DTOBaseGuid
        Property SkuFrom As DTOProductSku
        Property SkuTo As DTOProductSku
        Property Fch As Date
    End Class
End Class
