Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RepCliComController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RepCliCom/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.RepCliCom.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la RepCliCom")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/RepCliCom")>
    Public Function Update(<FromBody> value As DTORepCliCom) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RepCliCom.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la RepCliCom")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la RepCliCom")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/RepCliCom/delete")>
    Public Function Delete(<FromBody> value As DTORepCliCom) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RepCliCom.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la RepCliCom")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la RepCliCom")
        End Try
        Return retval
    End Function

End Class

Public Class RepCliComsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RepCliComs/FromRep/{rep}")>
    Public Function All(rep As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRep As New DTORep(rep)
            Dim values = BEBL.RepCliComs.All(oRep)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCliComs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepCliComs/FromEmp/{emp}")>
    Public Function All(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.RepCliComs.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCliComs")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/RepCliComs/delete")>
    Public Function Delete(<FromBody> values As List(Of DTORepCliCom)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RepCliComs.Delete(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les RepCliCom")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les RepCliCom")
        End Try
        Return retval
    End Function

End Class
