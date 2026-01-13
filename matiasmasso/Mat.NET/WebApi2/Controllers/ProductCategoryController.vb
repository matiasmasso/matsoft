Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductCategoryController
    Inherits _BaseController


    <HttpGet>
    <Route("api/productCategory/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ProductCategory.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la ProductCategory")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productCategory/fromNom/{brand}")>
    Public Function FromNom(brand As Guid, <FromBody> nom As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim value = BEBL.ProductCategory.FromNom(oBrand, nom)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la ProductCategory")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductCategory/thumbnail/{guid}")>
    Public Function Thumbnail(guid As Guid) As HttpResponseMessage ' TO DEPRECATE (/image)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCategory As New DTOProductCategory(guid)
            Dim oImage = BEBL.ProductCategory.Thumbnail(oCategory)
            retval = MyBase.HttpImageResponseMessage(oImage)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la image de la categoria")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductCategory/image/{guid}")>
    Public Function Image(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.productCategory)
            If oImageMime Is Nothing Then
                oImageMime = BEBL.ProductCategory.Image(guid)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.productCategory, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la image de la categoria")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductCategory/skuColors/sprite/{category}/{width}/{height}")>
    Public Function SkuColorsSprite(category As Guid, width As Integer, height As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCategory As New DTOProductCategory(category)
            Dim value = BEBL.ProductCategory.SkuColorsSprite(oCategory, width, height)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'sprite de la colecció")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/ProductCategory")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOProductCategory)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la ProductCategory")
            Else
                value.thumbnail = oHelper.GetImage("thumbnail")
                value.Image = oHelper.GetImage("Image")

                If BEBL.ProductCategory.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.ProductCategoryLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.ProductCategoryLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/ProductCategory/delete")>
    Public Function Delete(<FromBody> value As DTOProductCategory) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductCategory.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ProductCategory")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ProductCategory")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/ProductCategory/SortSkus/{guid}")>
    Public Function SortSkus(guid As Guid, <FromBody> oDict As Dictionary(Of Guid, Integer)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing

        Dim exs As New List(Of Exception)
        Try
            If BEBL.ProductCategory.SortSkus(exs, guid, oDict) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al reordenar els productes")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al reordenar els productes")
        End Try
        Return retval
    End Function


End Class
Public Class ProductCategoriesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/productcategories/min/{emp}")>
    Public Function Minified(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(emp)
            Dim oCategories = BEBL.ProductCategories.All(oEmp)
            Dim values = oCategories.Select(Function(x) Models.Min.ProductCategory.Factory(x)).ToList()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les marques comercials")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productcategories/{emp}")>
    Public Function All(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(emp)
            Dim values = BEBL.ProductCategories.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les marques comercials")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productCategories/{brand}/{mgz}/{IncludeObsolets}/{sortOrder}/{skipEmptyCategories}")>
    Public Function All(brand As Guid, mgz As Guid, IncludeObsolets As Integer, sortOrder As Integer, skipEmptyCategories As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand = BEBL.ProductBrand.Find(brand)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.ProductCategories.All(oBrand, oMgz, (IncludeObsolets = 1), sortOrder, (skipEmptyCategories = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les ProductCategory")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productCategories/fromCustomer/{customer}")>
    Public Function All(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.ProductCategories.All(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les ProductCategory")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productCategories/fromDept/{dept}")>
    Public Function fromDept(dept As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDept = BEBL.Dept.Find(dept)
            Dim values = oDept.Categories
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les categories")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productCategories/compactTree/{emp}/{lang}")>
    Public Function compactTree(emp As DTOEmp.Ids, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.ProductCategories.CompactTree(oEmp, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el cataleg de categories")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productCategories/move")>
    Public Function Move(<FromBody> oCategories As List(Of DTOProductCategory)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductCategories.Move(oCategories, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al moure les ProductCategory")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al moure les ProductCategory")
        End Try
        Return retval
    End Function
End Class