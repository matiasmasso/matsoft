Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class TaxController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Tax/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Tax.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Tax")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Tax")>
    Public Function Update(<FromBody> value As DTOTax) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Tax.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Tax")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Tax")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Tax/delete")>
    Public Function Delete(<FromBody> value As DTOTax) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Tax.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Tax")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Tax")
        End Try
        Return retval
    End Function

End Class


Public Class TaxesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/taxes")>
    Public Function GetTaxes() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Taxes.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els tipus impositius")
        End Try
        Return retval
    End Function
End Class
