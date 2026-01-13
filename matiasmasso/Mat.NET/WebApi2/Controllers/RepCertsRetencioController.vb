Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class RepCertsRetencioController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RepCertsRetencio/{rep}/{year}/{quarter}")>
    Public Function All(rep As Guid, year As Integer, quarter As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRep = BEBL.Rep.Find(rep)
            Dim values = BEBL.RepCertsRetencio.All(oRep, year, quarter)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCertsRetencio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepCertsRetencio/fromUser/{user}")>
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim values = BEBL.RepCertsRetencio.All(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els certificats de retencio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepCertsRetencio/fromRep/{rep}")>
    Public Function FromRep(rep As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRep As New DTORep(rep)
            Dim values = BEBL.RepCertsRetencio.All(oRep)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els certificats de retencio")
        End Try
        Return retval
    End Function

End Class

