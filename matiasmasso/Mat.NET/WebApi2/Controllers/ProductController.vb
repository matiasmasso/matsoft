Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductController
    Inherits _BaseController


    <HttpGet>
    <Route("api/product/{product}")>
    Public Function Find(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Product.Find(product)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la RepCustomer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/product/brand/{product}")>
    Public Function Brand(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            If oProduct Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage(String.Format("producte '" & product.ToString & "' desconegut"))
            Else
                Dim value = DTOProduct.Brand(oProduct)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la RepCustomer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/product/FraccionarTemporalment/{product}/{user}")>
    Public Function FraccionarTemporalment(product As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oProduct As New DTOProduct(product)
            Dim oUser As New DTOUser(user)
            If BEBL.Product.FraccionarTemporalment(exs, oProduct, oUser) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al fraccionar el producte")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al fraccionar el producte")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/product/relateds/Exist/{cod}/{target}")>
    Public Function RelatedsExist(cod As DTOProduct.Relateds, target As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oTarget As New DTOProduct(target)
            Dim value = BEBL.Product.RelatedsExist(cod, oTarget)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els articles relacionats")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/product/relateds/{cod}/{target}/{mgz}/{IncludeObsoletos}/{AllowInheritance}")>
    Public Function Relateds(cod As DTOProduct.Relateds, target As Guid, mgz As Guid, IncludeObsoletos As Integer, AllowInheritance As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim oTarget As New DTOProductSku(target)
            Dim values = BEBL.Product.Relateds(cod, oTarget, oMgz, (IncludeObsoletos = 1), (AllowInheritance = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els articles relacionats")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/product/relateds/{cod}/{target}")>
    Public Function Relateds(cod As DTOProduct.Relateds, target As Guid, <FromBody> oSkus As List(Of DTOProductSku)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oTarget As New DTOProductSku(target)
            If BEBL.Product.UpdateRelateds(exs, cod, oTarget, oSkus) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar els articles relacionats")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar els articles relacionats")
        End Try
        Return retval
    End Function

End Class


Public Class ProductsController
    Inherits _BaseController


    <HttpPost>
    <Route("api/products/FromNom/{brand}")>
    Public Function FromNom(brand As Guid, <FromBody> nom As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim values = BEBL.Products.FromNom(oBrand, nom)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els productes per nom")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/products/ForSitemap/{emp}")>
    Public Function ForSitemap(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.Products.ForSitemap(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els productes del sitemap")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productSkus/fromCnap/")>
    Public Function fromCnap(<FromBody> cnap As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Products.FromCnap(cnap)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Sku")
        End Try
        Return retval
    End Function


End Class