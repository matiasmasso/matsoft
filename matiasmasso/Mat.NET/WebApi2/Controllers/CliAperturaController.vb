Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CliAperturaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CliApertura/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CliApertura.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CliApertura")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CliApertura")>
    Public Function Update(<FromBody> value As DTOCliApertura) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CliApertura.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CliApertura")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CliApertura")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/CliApertura/UpdateStatus/{guid}")>
    Public Function DeprecatedFurtherImat33(guid As Guid, <FromBody> repobs As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCliApertura As New DTOCliApertura(guid)
            If BEBL.CliApertura.UpdateStatus(exs, oCliApertura, DTOCliApertura.CodsTancament.StandBy, repobs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CliApertura")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CliApertura")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/CliApertura/UpdateStatus/{guid}/{status}")>
    Public Function UpdateStatus(guid As Guid, status As DTOCliApertura.CodsTancament, <FromBody> repobs As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim o = HttpContext.Current.Request()
            Dim oCliApertura As New DTOCliApertura(guid)
            If BEBL.CliApertura.UpdateStatus(exs, oCliApertura, status, repobs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CliApertura")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CliApertura")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/CliApertura/delete")>
    Public Function Delete(<FromBody> value As DTOCliApertura) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CliApertura.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CliApertura")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CliApertura")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CliApertura/send/{emp}/{cliapertura}")>
    Public Async Function Send(emp As Integer, cliapertura As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.CliApertura.Find(cliapertura)
            If Await BEBL.CliApertura.Send(oEmp, value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CliApertura")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CliApertura")
        End Try
        Return retval
    End Function

End Class

Public Class CliAperturasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CliAperturas/{user}")>
    Public Function All(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Usuari desconegut")
            Else
                Dim values = BEBL.CliAperturas.All(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CliAperturas")
        End Try
        Return retval
    End Function

End Class

