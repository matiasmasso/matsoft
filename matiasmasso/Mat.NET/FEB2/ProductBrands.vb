Imports Newtonsoft.Json.Linq

Public Class ProductBrand
    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOProductBrand)
        Return Await Api.Fetch(Of DTOProductBrand)(exs, "ProductBrand", oGuid.ToString())
    End Function

    Shared Async Function FromNom(exs As List(Of Exception), oEmp As DTOEmp, sNom As String) As Task(Of DTOProductBrand)
        Dim retval = DTOProductBrand.FromNom(sNom)
        If retval Is Nothing Then
            retval = Await Api.Execute(Of String, DTOProductBrand)(sNom, exs, "ProductBrand/FromNom", oEmp.Id)
        End If
        Return retval
    End Function

    Shared Function FindSync(oGuid As Guid, exs As List(Of Exception)) As DTOProductBrand
        Return Api.FetchSync(Of DTOProductBrand)(exs, "ProductBrand", oGuid.ToString())
    End Function
    '
    Shared Async Function Logo(exs As List(Of Exception), oGuid As Guid) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "ProductBrand/Logo", oGuid.ToString())
    End Function

    Shared Function LogoSync(exs As List(Of Exception), oGuid As Guid) As Byte() 
        Return Api.FetchImageSync(exs, "ProductBrand/Logo", oGuid.ToString())
    End Function

    Shared Function LogoDistribuidorOficialSync(exs As List(Of Exception), oGuid As Guid) As Byte() 
        Return Api.FetchImageSync(exs, "ProductBrand/Logo", oGuid.ToString())
    End Function

    Shared Async Function LogoDistribuidorOficial(exs As List(Of Exception), oGuid As Guid) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "ProductBrand/LogoDistribuidorOficial", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oProductBrand As DTOProductBrand, exs As List(Of Exception), Optional IncludeImages As Boolean = False) As Boolean
        If Not oProductBrand.IsLoaded And Not oProductBrand.IsNew Then
            Dim pProductBrand = Api.FetchSync(Of DTOProductBrand)(exs, "ProductBrand", oProductBrand.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOProductBrand)(pProductBrand, oProductBrand, exs)
                oProductBrand.Logo = FEB2.ProductBrand.LogoSync(exs, oProductBrand.Guid)
                oProductBrand.LogoDistribuidorOficial = FEB2.ProductBrand.LogoDistribuidorOficialSync(exs, oProductBrand.Guid)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOProductBrand, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("logo", value.Logo)
            oMultipart.AddFileContent("LogoDistribuidorOficial", value.LogoDistribuidorOficial)
            retval = Await Api.Upload(oMultipart, exs, "ProductBrand")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oProductBrand As DTOProductBrand, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOProductBrand)(oProductBrand, exs, "ProductBrand")
    End Function

    Shared Function Url(oBrand As DTOProductBrand, Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oBrand IsNot Nothing Then
            If oTab = DTOProduct.Tabs.general Then
                retval = UrlHelper.Factory(AbsoluteUrl, DTOProductBrand.urlSegment(oBrand))
            Else
                retval = UrlHelper.Factory(AbsoluteUrl, DTOProductBrand.urlSegment(oBrand), oTab.ToString())
            End If
        End If
        Return retval
    End Function

    Shared Function LogoUrl(oBrand As DTOProductBrand, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.tpalogo, oBrand.Guid, AbsoluteUrl)
    End Function

    Shared Function LogoDistribuidorOficialUrl(oBrand As DTOProductBrand) As String
        Return UrlHelper.Factory(DTO.Defaults.ImgTypes.tpalogodistribuidoroficial, oBrand.Guid.ToString())
    End Function

    Shared Function LogoDistribuidorOficialAnchor(oBrand As DTOProductBrand, oCustomer As DTOCustomer) As String
        Return UrlHelper.Factory(False, "LogoDistribuidorOficial", oBrand.Guid.ToString, oCustomer.Guid.ToString())
    End Function


    'Shared Async Function DeptFiltersBoxes(exs As List(Of Exception), oBrand As DTOProductBrand, oLang As DTOLang) As Task(Of List(Of DTOBox))
    'Dim retval = Await Api.Fetch(Of List(Of DTOBox))(exs, "productbrand/DeptFiltersBoxes", oBrand.Guid.ToString, oLang.Tag)
    'Return retval
    'End Function
End Class

Public Class ProductBrands
    Inherits _FeblBase

    Shared Async Function Minified(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOProductBrand))
        Dim min = Await Api.Fetch(Of List(Of JObject))(exs, "productbrands/min", oEmp.Id)
        Dim retval = min.Select(Function(x) Models.Min.ProductBrand.Expand(x)).ToList()
        Return retval
    End Function

    Shared Async Function Tree(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOProductBrand))
        Return Await Api.Fetch(Of List(Of DTOProductBrand))(exs, "productbrands/tree", oEmp.Id)
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, Optional IncludeObsolets As Boolean = False) As Task(Of List(Of DTOProductBrand))
        Return Await Api.Fetch(Of List(Of DTOProductBrand))(exs, "brands/fromEmp", oEmp.Id, OpcionalBool(IncludeObsolets))
    End Function

    Shared Function AllSync(exs As List(Of Exception), oEmp As DTOEmp, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductBrand)
        Return Api.FetchSync(Of List(Of DTOProductBrand))(exs, "brands/fromEmp", oEmp.Id, OpcionalBool(IncludeObsolets))
    End Function

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOProductBrand))
        Return Await Api.Fetch(Of List(Of DTOProductBrand))(exs, "brands", OpcionalGuid(oUser))
    End Function

    Shared Function AllSync(exs As List(Of Exception), oUser As DTOUser) As List(Of DTOProductBrand)
        Return Api.FetchSync(Of List(Of DTOProductBrand))(exs, "brands", OpcionalGuid(oUser))
    End Function

    Shared Async Function All(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOProductBrand))
        Return Await Api.Fetch(Of List(Of DTOProductBrand))(exs, "brands/FromCustomer", oCustomer.Guid.ToString())
    End Function

    Shared Async Function FromProveidor(exs As List(Of Exception), oProveidor As DTOProveidor) As Task(Of List(Of DTOProductBrand))
        Return Await Api.Fetch(Of List(Of DTOProductBrand))(exs, "brands/FromProveidor", oProveidor.Guid.ToString())
    End Function

    Shared Function RoutingConstraint(oEmp As DTOEmp) As String
        Dim exs As New List(Of Exception)
        Dim nomsList = Api.FetchSync(Of List(Of String))(exs, "productbrands/RoutingConstraints", oEmp.Id)
        Dim retval As String = "(" & String.Join("|", nomsList.ToArray) & ")"
        Return retval
    End Function
End Class
