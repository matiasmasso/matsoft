Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PremiumLineController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PremiumLine/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PremiumLine.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PremiumLine")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PremiumLine")>
    Public Function Update(<FromBody> value As DTOPremiumLine) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PremiumLine.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la PremiumLine")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la PremiumLine")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PremiumLine/delete")>
    Public Function Delete(<FromBody> value As DTOPremiumLine) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PremiumLine.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PremiumLine")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PremiumLine")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PremiumLine/EmailRecipients/{premiumLine}")>
    Public Function EmailRecipients(premiumLine As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPremiumLine As New DTOPremiumLine(premiumLine)
            Dim values = BEBL.PremiumLine.EmailRecipients(oPremiumLine)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PremiumLines")
        End Try
        Return retval
    End Function
End Class

Public Class PremiumLinesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PremiumLines")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.PremiumLines.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PremiumLines")
        End Try
        Return retval
    End Function

End Class
