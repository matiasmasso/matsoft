Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductBrandController
    Inherits _BaseController

    <HttpGet>
    <Route("api/productBrand/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ProductBrand.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la marca")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productBrand/FromNom/{emp}")>
    Public Function FromNom(emp As DTOEmp.Ids, <FromBody> nom As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.ProductBrand.FromNom(oEmp, nom)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la marca")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/productBrand/logo/{guid}")>
    Public Function Logo(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(guid)
            Dim oImageMime = BEBL.ProductBrand.Logo(oBrand)
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descarregar el logo")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productBrand/LogoDistribuidorOficial/{guid}")>
    Public Function LogoDistribuidorOficial(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(guid)
            Dim oImage = BEBL.ProductBrand.LogoDistribuidorOficial(oBrand)
            retval = MyBase.HttpImageResponseMessage(oImage)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el logo de distribuidor oficial")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productBrand")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOProductBrand)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la ProductBrand")
            Else
                value.Logo = oHelper.GetImage("Logo")
                value.LogoDistribuidorOficial = oHelper.GetImage("LogoDistribuidorOficial")
                If DAL.ProductBrandLoader.Update(value, exs) Then
                    BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Brands)
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.ProductBrandLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.ProductBrandLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/productBrand/delete")>
    Public Function Delete(<FromBody> value As DTOProductBrand) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductBrand.Delete(value, exs) Then
                BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Brands)
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ProductBrand")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ProductBrand")
        End Try
        Return retval
    End Function




    <HttpGet>
    <Route("api/productbrand/deptFiltersBoxes/{brand}/{lang}")>
    Public Function deptFiltersBoxes(brand As Guid, lang As String) As HttpResponseMessage 'DEPRECATED
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim oLang = DTOLang.Factory(lang)
            Dim oDepts = BEBL.Depts.AllWithFilters(oBrand)
            Dim oFilters = BEBL.Filters.All()
            'Dim values As New List(Of DTOBox)
            For Each oDept In oDepts
                'Dim oDeptBox = DTOBox.Factory(oDept.LangNom.Tradueix(oLang), oDept.getUrl(), oDept.BannerUrl())
                'values.Add(oDeptBox)

                Dim oFilterItems = oDept.Categories.SelectMany(Function(x) x.filterItems).GroupBy(Function(y) y.Guid).Select(Function(z) z.First).ToList()
                For Each oFilter In oFilters
                    If oFilterItems.Any(Function(x) oFilter.Items.Any(Function(y) y.Equals(x))) Then
                        'Dim oFilterBox = DTOBox.Factory(oFilter.langText.Tradueix(oLang))
                        'oDeptBox.Children.Add(oFilterBox)

                        For Each item In oFilter.Items
                            If oFilterItems.Any(Function(x) x.Equals(item)) Then
                                'Dim oItemBox = DTOBox.Factory(item.langText.Tradueix(oLang), oDept.url(item))
                                'oFilterBox.Children.Add(oItemBox)
                            End If
                        Next
                    End If
                Next
            Next
            ' retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les marques comercials")
        End Try
        Return retval
    End Function

End Class


Public Class ProductBrandsController
    Inherits _BaseController




    <HttpGet>
    <Route("api/productbrands/RoutingConstraints/{emp}")>
    Public Function RoutingConstraints(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(emp)
            Dim values = BEBL.ProductBrands.RoutingConstraints(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les marques comercials")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productbrands/min/{emp}")>
    Public Function Minified(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(emp)
            Dim oBrands = BEBL.ProductBrands.All(oEmp, True)
            Dim values = oBrands.Select(Function(x) Models.Min.ProductBrand.Factory(x)).ToList()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les marques comercials")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productbrands/tree/{emp}")>
    Public Function Tree(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(emp)
            Dim values = BEBL.ProductBrands.Tree(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les marques comercials")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/brands/fromEmp/{emp}/{includeObsolets}")>
    Public Function FromEmp(emp As DTOEmp.Ids, includeObsolets As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(DTOEmp.Ids.MatiasMasso)
            Dim values = BEBL.ProductBrands.All(oEmp, (includeObsolets = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les marques comercials")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/brands/{user}")>
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As DTOUser = Nothing
            Dim oEmp = New DTOEmp(DTOEmp.Ids.MatiasMasso)
            If user <> Nothing Then
                oUser = BEBL.User.Find(user)
            End If
            Dim values = BEBL.ProductBrands.All(oEmp, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les marques comercials")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/brands/FromCustomer/{customer}")>
    Public Function FromCustomer(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.ProductBrands.All(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les marques comercials")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/brands/FromProveidor/{proveidor}")>
    Public Function FromProveidor(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.ProductBrands.FromProveidor(oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les marques comercials")
        End Try
        Return retval
    End Function


End Class