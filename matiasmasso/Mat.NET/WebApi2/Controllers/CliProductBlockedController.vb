Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CliProductBlockedController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CliProductBlocked/{customer}/{product}")>
    Public Function Find(customer As Guid, product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim oProduct As New DTOProduct(product)
            Dim value = BEBL.CliProductBlocked.Find(oCustomer, oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CliProductBlocked")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CliProductBlocked/AltresEnExclusiva/{customer}/{product}")>
    Public Function AltresEnExclusiva(customer As Guid, product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.CliProductBlocked.AltresEnExclusiva(oCustomer, oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CliProductBlocked")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CliProductBlocked")>
    Public Function Update(<FromBody> value As DTOCliProductBlocked) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            value.RestoreObjects()

            If BEBL.CliProductBlocked.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CliProductBlocked")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CliProductBlocked")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CliProductBlocked/delete")>
    Public Function Delete(<FromBody> value As DTOCliProductBlocked) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            value.RestoreObjects()
            If BEBL.CliProductBlocked.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CliProductBlocked")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CliProductBlocked")
        End Try
        Return retval
    End Function

End Class



Public Class CliProductsBlockedController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CliProductsBlocked/{customer}")>
    Public Function All(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.CliProductsBlocked.All(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CliProductsBlocked")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CliProductsBlocked/DistribuidorsOficialsActiveEmails/{brand}")>
    Public Function DistribuidorsOficialsActiveEmails(brand As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim values = BEBL.CliProductsBlocked.DistribuidorsOficialsActiveEmails(oBrand)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CliProductsBlocked")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CliProductsBlocked/delete")>
    Public Function Delete(<FromBody> values As List(Of DTOCliProductBlocked)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            For Each value In values
                value.RestoreObjects()
            Next
            If BEBL.CliProductsBlocked.Delete(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les CliProductBlocked")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les CliProductBlocked")
        End Try
        Return retval
    End Function
End Class
