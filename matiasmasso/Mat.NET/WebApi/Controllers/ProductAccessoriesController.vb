Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductAccessoriesController

    Inherits _BaseController

    <HttpGet>
    <Route("api/ProductAccessories/Exist/{product}/{includeObsoletos}")>
    Public Function Exist(product As Guid, includeObsoletos As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim value = BEBL.ProductAccessories.Exist(oProduct, (includeObsoletos = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els accessoris")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductAccessories/Accessories/{product}/{includeObsoletos}/{allowInheritance}")>
    Public Function Accessories(product As Guid, includeObsoletos As Integer, allowInheritance As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.ProductAccessories.Accessories(oProduct, (includeObsoletos = 1), (allowInheritance = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els accessoris")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductAccessories/Spares/{product}/{includeObsoletos}/{allowInheritance}")>
    Public Function Spares(product As Guid, includeObsoletos As Integer, allowInheritance As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.ProductAccessories.Spares(oProduct, (includeObsoletos = 1), (allowInheritance = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els accessoris")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ProductAccessories/Accessories/{target}")>
    Public Function UpdateAccessories(target As Guid, <FromBody> values As List(Of DTOProductSku)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oTarget As New DTOProduct(target)
            If BEBL.ProductAccessories.UpdateAccessories(oTarget, values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar els accessoris")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar els accessoris")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ProductAccessories/Spares/{target}")>
    Public Function UpdateSpares(target As Guid, <FromBody> values As List(Of DTOProductSku)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oTarget As New DTOProduct(target)
            If BEBL.ProductAccessories.UpdateSpares(oTarget, values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar els recanvis")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar els recanvis")
        End Try
        Return retval
    End Function


End Class
