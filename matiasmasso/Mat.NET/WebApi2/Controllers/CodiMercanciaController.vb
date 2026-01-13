Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CodiMercanciaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CodiMercancia/{id}")>
    Public Function Find(id As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CodiMercancia.Find(id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CodiMercancia")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CodiMercancia")>
    Public Function Update(<FromBody> value As DTOCodiMercancia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CodiMercancia.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CodiMercancia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CodiMercancia")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CodiMercancia/delete")>
    Public Function Delete(<FromBody> value As DTOCodiMercancia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CodiMercancia.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CodiMercancia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CodiMercancia")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/CodiMercancia/products/{id}")>
    Public Function All(id As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCodiMercancia As New DTOCodiMercancia(id)
            Dim values = BEBL.CodiMercancia.Products(oCodiMercancia)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir lels productes del CodisMercancia")
        End Try
        Return retval
    End Function
End Class

Public Class CodisMercanciaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CodisMercancia")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.CodisMercancia.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CodisMercancia")
        End Try
        Return retval
    End Function

End Class

