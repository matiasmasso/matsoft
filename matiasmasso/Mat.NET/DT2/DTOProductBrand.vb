
Public Class DTOProductBrand
    Inherits DTOProduct

    Public Const LogoWidth = 150
    Public Const LogoHeight = 48

    Property emp As DTOEmp
    Property id As Integer
    Shadows Property proveidor As DTOProveidor
    Property codDist As CodDists
    <JsonIgnore> Property logo As Image
    <JsonIgnore> Property logoDistribuidorOficial As Image
    Property showAtlas As Boolean
    Property enabledxConsumer As Boolean
    Property enabledxPro As Boolean

    Property title_Esp As String
    Property title_Cat As String
    Property title_Eng As String

    Property tagline_Esp As String
    Property tagline_Cat As String
    Property tagline_Eng As String

    Property restrictAtlasToPremiumLine As DTOPremiumLine

    Property fchLastEdited As DateTime 'per sitemap.xml

    Property categories As List(Of DTOProductCategory)

    Property cliProductBlockedCodi As DTOCliProductBlocked.Codis

    Property cnap As DTOCnap

    Property madeIn As DTOCountry
    Shadows Property codiMercancia As DTOCodiMercancia

    Property webAtlasDeadline As Integer 'Max Days from last order to be published as distributor
    Property webAtlasRafflesDeadline As Integer 'Max Days from last order to be published as distributor

    Public Enum CodDists
        Free
        DistribuidorsOficials
    End Enum

    Public Enum CodStks
        NotSet
        Intern 'nosaltres estoquem. Entra quan comprem, surt quan venem
        Extern 'ells estoquen; entra automáticament tot allo i nomes allo que surt
    End Enum

    Public Enum wellknowns
        Britax
        Romer
        Bob
        Inglesina
        FourMoms
        TommeeTippee
        FisherPrice
        JBimbi
        Gro
        Varios
    End Enum

    Public Sub New()
        MyBase.New()
        MyBase.SourceCod = SourceCods.Brand
        _Categories = New List(Of DTOProductCategory)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        MyBase.SourceCod = SourceCods.Brand
        _Categories = New List(Of DTOProductCategory)
    End Sub

    Shared Function wellknown(id As DTOProductBrand.wellknowns) As DTOProductBrand
        Dim retval As DTOProductBrand = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case DTOProductBrand.wellknowns.Inglesina
                sGuid = "B1A0FB03-0C18-4607-9091-DF5A6A635BB0"
            Case DTOProductBrand.wellknowns.Britax
                sGuid = "D56CE172-3C98-48E0-A378-8718BE8622F7"
            Case DTOProductBrand.wellknowns.Romer
                sGuid = "D4C2BC59-046D-42D3-86E3-BDCA91FB473F"
            Case DTOProductBrand.wellknowns.Bob
                sGuid = "63F67FDB-812F-49F9-B06C-023EE8A984EC"
            Case DTOProductBrand.wellknowns.FisherPrice
                sGuid = "7C097674-233E-4899-92A7-37F37DD6D1F4"
            Case DTOProductBrand.wellknowns.FourMoms
                sGuid = "67058F90-1FD6-4AE6-82ED-78447779B358"
            Case DTOProductBrand.wellknowns.TommeeTippee
                sGuid = "B55B006D-3322-4E41-8CF7-9A02C3503A09"
            Case wellknowns.Gro
                sGuid = "7b5dd269-62ea-481e-8be3-6e4c9d772117"
            Case DTOProductBrand.wellknowns.Varios
                sGuid = "93995799-2865-46E0-A0EC-05DB6A80C7B4"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOProductBrand(oGuid)
        End If
        Return retval
    End Function

    Public Function Clon() As DTOProductBrand
        Dim exs As New List(Of Exception)
        Dim retval As New DTOProductBrand
        DTOBaseGuid.CopyPropertyValues(Of DTOProductBrand)(Me, retval, exs)
        Return retval
    End Function


    Shared Shadows Function Factory(oEmp As DTOEmp) As DTOProductBrand
        Dim retval As New DTOProductBrand
        retval.Emp = oEmp
        Return retval
    End Function


    Public Function getUrl(Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = MmoUrl.Factory(AbsoluteUrl, DTOProductBrand.UrlSegment(Me))

        If oTab <> DTOProduct.Tabs.general Then
            retval = retval & "/" & oTab.ToString
        End If

        Return retval
    End Function

    Overloads Shared Function UrlSegment(oBrand As DTOProductBrand) As String
        Dim retval As String = ""
        If oBrand IsNot Nothing Then
            retval = oBrand.UrlSegment
        End If
        Return retval
    End Function

    Public Overloads Function UrlSegment(Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general) As String
        Dim retval As String = ""
        If Me.Equals(DTOProductBrand.wellknown(wellknowns.Romer)) Then
            retval = "britax-roemer"
        Else
            retval = MatHelperStd.UrlHelper.EncodedUrlSegment(MyBase.Nom)
        End If

        If oTab <> DTOProduct.Tabs.general Then
            retval = String.Format("{0}/{1}", retval, oTab.ToString())
        End If

        Return retval
    End Function


    Public Function UrlSegmentSalePointsPerArea(AreaNom As String) As String
        Return String.Format("/{0}/{1}", UrlSegment(), AreaNom)
    End Function

    Shared Function ToGuidNom(oBrand As DTOProductBrand) As DTOGuidNom
        Dim retval As New DTOGuidNom(oBrand.Guid, oBrand.Nom)
        Return retval
    End Function

    Shared Function ToGuidNoms(oBrands As IEnumerable(Of DTOProductBrand)) As List(Of DTOGuidNom)
        Dim retval As New List(Of DTOGuidNom)
        For Each oBrand In oBrands
            Dim item = DTOProductBrand.ToGuidNom(oBrand)
            retval.Add(item)
        Next
        Return retval
    End Function

    Public Function DeptsSprite(oDepts As List(Of DTODept), oLang As DTOLang) As SpriteHelper.Sprite
        Dim url = MmoUrl.ApiUrl("depts/sprite", MyBase.Guid.ToString())
        Dim retval = SpriteHelper.Factory(url, DTODept.width, DTODept.height)
        For Each oDept In oDepts
            retval.addItem(oDept.LangNom.tradueix(oLang), oDept.url)
        Next
        Return retval
    End Function
End Class
