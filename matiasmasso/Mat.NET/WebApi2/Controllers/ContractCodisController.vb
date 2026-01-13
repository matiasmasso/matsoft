Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ContractCodiController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ContractCodi/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ContractCodi.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la ContractCodi")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ContractCodi")>
    Public Function Update(<FromBody> value As DTOContractCodi) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ContractCodi.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la ContractCodi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la ContractCodi")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ContractCodi/delete")>
    Public Function Delete(<FromBody> value As DTOContractCodi) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ContractCodi.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ContractCodi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ContractCodi")
        End Try
        Return retval
    End Function

End Class

Public Class ContractCodisController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ContractCodis")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ContractCodis.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les categories de Contractes")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ContractCodis/delete")>
    Public Function Delete(<FromBody> value As List(Of DTOContractCodi)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ContractCodis.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ContractCodi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ContractCodi")
        End Try
        Return retval
    End Function


End Class
