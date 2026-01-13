Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DiariController
    Inherits _BaseController

    <HttpPost>
    <Route("api/Diari")>
    Public Function Load(value As DTODiari) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            BEBL.Diari.Load(value)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Diari")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Diari/LoadBrands")>
    Public Function LoadBrands(value As DTODiari) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            BEBL.Diari.LoadTopBrands(value)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Diari")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Diari/Years")>
    Public Function Years(oDiari As DTODiari) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Diari.Years(oDiari)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Diari")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Diari/Months")>
    Public Function Months(oDiari As DTODiari) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Diari.Months(oDiari)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Diari")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Diari/Days")>
    Public Function Days(oDiari As DTODiari) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Diari.Days(oDiari)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Diari")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Diari/Orders")>
    Public Function Orders(oDiari As DTODiari) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Diari.Orders(oDiari)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Diari")
        End Try
        Return retval
    End Function


End Class
