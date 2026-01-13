Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CliProductDtoController
    Inherits _BaseController



End Class
Public Class CliProductDtosController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CliProductDtos/{customer}")>
    Public Function All(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = BEBL.Customer.Find(customer)
            Dim values = BEBL.CliProductDtos.All(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CliProductDtos")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/CliProductDtos")>
    Public Function Update(<FromBody> oCustomer As DTOCustomer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CliProductDtos.Update(oCustomer, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CliProductDto")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CliProductDto")
        End Try
        Return retval
    End Function

End Class
