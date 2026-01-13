Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AmortizationItemController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AmortizationItem/{cca}")>
    Public Function Find(cca As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCca = BEBL.Cca.Find(cca)
            Dim value = BEBL.AmortizationItem.FromCca(oCca)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la AmortizationItem")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AmortizationItem/{user}")>
    Public Function Update(user As Guid, <FromBody> value As DTOAmortizationItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser = BEBL.User.Find(user)
            If BEBL.AmortizationItem.Update(oUser, value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la AmortizationItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la AmortizationItem")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/AmortizationItem/delete")>
    Public Function Delete(<FromBody> value As DTOAmortizationItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AmortizationItem.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la AmortizationItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la AmortizationItem")
        End Try
        Return retval
    End Function

End Class
