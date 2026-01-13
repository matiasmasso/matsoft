Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BancPoolController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BancPool/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.BancPool.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BancPool")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/BancPool")>
    Public Function Update(<FromBody> value As DTOBancPool) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BancPool.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la BancPool")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la BancPool")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/BancPool/delete")>
    Public Function Delete(<FromBody> value As DTOBancPool) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BancPool.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la BancPool")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la BancPool")
        End Try
        Return retval
    End Function

End Class

Public Class BancPoolsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BancPools/{banc}/{fch}")>
    Public Function All(banc As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBanc As DTOBanc = Nothing
            If banc <> Nothing Then oBanc = New DTOBanc(banc)
            Dim values = BEBL.BancPools.All(oBanc, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BancPools")
        End Try
        Return retval
    End Function

End Class
