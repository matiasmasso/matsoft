Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BookFraController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BookFra/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.BookFra.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BookFra")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/BookFra")>
    Public Function Update(<FromBody> value As DTOBookFra) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BookFra.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la BookFra")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la BookFra")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/BookFra/delete")>
    Public Function Delete(<FromBody> value As DTOBookFra) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BookFra.Delete(exs, value) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la BookFra")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la BookFra")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/BookFra/LogSii")>
    Public Function LogSii(<FromBody> value As DTOBookFra) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BookFra.LogSii(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al BookFra.LogSii")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al BookFra.LogSii")
        End Try
        Return retval
    End Function


End Class

Public Class BookFrasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BookFras/{mode}/{emp}/{year}/{mes}/{contact}")>
    Public Function All(mode As DTOBookFra.Modes, emp As DTOEmp.Ids, year As Integer, mes As Integer, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim oContact = DTOBaseGuid.Opcional(Of DTOContact)(contact)
            Dim values = BEBL.BookFras.All(mode, oExercici, mes, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BookFras")
        End Try
        Return retval
    End Function


End Class
