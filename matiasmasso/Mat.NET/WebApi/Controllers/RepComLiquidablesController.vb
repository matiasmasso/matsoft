Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RepcomLiquidableController
    Inherits _BaseController

    <HttpPost>
    <Route("api/repcomliquidable")>
    Public Function Update(<FromBody> value As DTORepComLiquidable) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RepComLiquidable.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la repcomliquidable")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la repcomliquidable")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/repcomliquidable/Descarta/{guid}")>
    Public Function Descarta(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value As New DTORepComLiquidable(guid)
            If BEBL.RepComLiquidable.Descarta(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al descartar la comisió")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descartar la comisió")
        End Try
        Return retval
    End Function
End Class

Public Class RepcomLiquidablesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/repcomliquidables/Sincronitza")>
    Public Function Sincronitza() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RepComLiquidables.Sincronitza(exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la repcomliquidable")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la repcomliquidable")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/repcomliquidables/pendents")>
    Public Function GetPendents() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.RepComLiquidables.PendentsDeLiquidar
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les repcomliquidables")
        End Try
        Return retval
    End Function


End Class
