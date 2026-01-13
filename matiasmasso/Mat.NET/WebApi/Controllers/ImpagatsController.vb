Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class ImpagatController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Impagat/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Impagat.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'Impagat")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Impagat")>
    Public Function Update(<FromBody> value As DTOImpagat) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Impagat.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Impagat")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Impagat")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Impagat/delete")>
    Public Function Delete(<FromBody> value As DTOImpagat) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Impagat.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Impagat")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Impagat")
        End Try
        Return retval
    End Function
End Class

Public Class ImpagatsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Impagats/FromUser/{user}")>
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Impagats.All(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Impagats")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Impagats/FromContact/{emp}/{contact}")>
    Public Function FromContact(emp As Integer, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact = New DTOContact(contact)
            Dim values = BEBL.Impagats.All(oEmp, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Impagats")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Impagats/FromRep/{rep}")>
    Public Function FromRep(rep As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRep = New DTORep(rep)
            Dim values = BEBL.Impagats.All(oRep)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Impagats")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Impagats/Kpis/{emp}/{fromYear}")>
    Public Function Kpis(emp As DTOEmp.Ids, fromYear As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.Impagats.Kpis(oEmp, fromYear)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Impagats")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Impagats")>
    Public Function Update(<FromBody> value As DTOImpagats) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCca = BEBL.Impagats.Update(exs, value.Impagats, value.Cca)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(Of DTOCca)(HttpStatusCode.OK, oCca)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Impagat")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Impagat")
        End Try
        Return retval
    End Function

End Class
