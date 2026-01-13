Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RepProductController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RepProduct/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.RepProduct.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la RepProduct")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepProduct/{area}/{product}/{channel}/{fch}")>
    Public Function Find1(area As Guid, product As Guid, channel As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oArea As New DTOArea(area)
            Dim oProduct As New DTOProduct(product)
            Dim oChannel As New DTODistributionChannel(channel)
            Dim value = BEBL.RepProduct.GetRepProduct(oArea, oProduct, oChannel, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la RepProduct")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/RepProduct")>
    Public Function Update(<FromBody> value As DTORepProduct) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            value.RestoreObjects()
            If BEBL.RepProduct.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la RepProduct")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la RepProduct")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/RepProduct/delete")>
    Public Function Delete(<FromBody> value As DTORepProduct) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RepProduct.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la RepProduct")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la RepProduct")
        End Try
        Return retval
    End Function

End Class

Public Class RepProductsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RepProducts/FromRep/{emp}/{rep}/{includeObsoletos}")>
    Public Function FromRepToDeprecateOldIMat(emp As Integer, rep As Guid, includeObsoletos As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oRep = DTOBaseGuid.Opcional(Of DTORep)(rep)
            Dim values = BEBL.RepProducts.All(oEmp, oRep, (includeObsoletos = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els RepProducts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepProducts/FromEmp/{emp}/{includeObsoletos}")>
    Public Function FromEmp(emp As Integer, includeObsoletos As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.RepProducts.All(oEmp, (includeObsoletos = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els RepProducts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepProducts/FromRep/{rep}/{includeObsoletos}")>
    Public Function FromRep(rep As Guid, includeObsoletos As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRep = DTOBaseGuid.Opcional(Of DTORep)(rep)
            Dim values = BEBL.RepProducts.All(oRep, (includeObsoletos = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els RepProducts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepProducts/FromProduct/{product}/{includeObsoletos}")>
    Public Function FromProduct(product As Guid, includeObsoletos As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = New DTOProduct(product)
            Dim values = BEBL.RepProducts.All(oProduct, (includeObsoletos = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els RepProducts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepProducts/{channel}/{area}/{product}/{fch}")>
    Public Function All3(channel As Guid, area As Guid, product As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oArea As New DTOArea(area)
            Dim oProduct As New DTOProduct(product)
            Dim oChannel As New DTODistributionChannel(channel)
            Dim values = BEBL.RepProducts.All(oChannel, oArea, oProduct, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els RepProducts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepProducts/FromArea/{area}/{includeObsoletos}")>
    Public Function FromArea(area As Guid, includeObsoletos As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oArea = New DTOArea(area)
            Dim values = BEBL.RepProducts.All(oArea, (includeObsoletos = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els RepProducts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepProducts/Catalogue/FromRep/{rep}")>
    Public Function Catalogue(rep As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRep = New DTORep(rep)
            Dim values = BEBL.RepProducts.Catalogue(oRep)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els RepProducts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepProducts/CatalogueTree/FromRep/{rep}")>
    Public Function CatalogueTree(rep As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRep = New DTORep(rep)
            Dim values = BEBL.RepProducts.CatalogueTree(oRep)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els RepProducts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepProducts/Customers/{user}")>
    Public Function Customers(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.RepProducts.Customers(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els clients")
        End Try
        Return retval
    End Function

    '
    '

    <HttpGet>
    <Route("api/RepProducts/RepsxAreaWithMobiles/{includeObsolets}")>
    Public Function RepsxAreaWithMobiles(includeObsolets As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.RepProducts.RepsxAreaWithMobiles(includeObsolets = 1)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els reps")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/RepProducts")>
    Public Function Update(<FromBody> values As List(Of DTORepProduct)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            For Each value In values
                value.RestoreObjects()
            Next
            If BEBL.RepProducts.Update(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar els RepProducts")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar els RepProducts")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/RepProducts/delete")>
    Public Function Delete(<FromBody> value As List(Of DTORepProduct)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RepProducts.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar els RepProducts")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar els RepProducts")
        End Try
        Return retval
    End Function

End Class
