Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BancTransferPoolController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BancTransferPool/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.BancTransferPool.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BancTransferPool")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/BancTransferPool/FromCca/{cca}")>
    Public Function FromCca(cca As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCca As New DTOCca(cca)
            Dim value = BEBL.BancTransferPool.FromCca(oCca)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BancTransferPool")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/BancTransferPool")>
    Public Function Update(<FromBody> value As DTOBancTransferPool) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BancTransferPool.Update(value, exs) Then
                retval = Request.CreateResponse(Of Integer)(HttpStatusCode.OK, value.Cca.Id)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la BancTransferPool")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la BancTransferPool")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/BancTransferPool/saveref")>
    Public Function Saveref(<FromBody> value As DTOBancTransferPool) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BancTransferPool.SaveRef(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la referència")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la BancTransferPool")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/BancTransferPool/delete")>
    Public Function Delete(<FromBody> value As DTOBancTransferPool) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BancTransferPool.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la BancTransferPool")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la BancTransferPool")
        End Try
        Return retval
    End Function

End Class

Public Class BancTransferPoolsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BancTransferPools/{emp}/{banc}")>
    Public Function All(emp As DTOEmp.Ids, banc As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBanc As DTOBanc = Nothing
            If banc <> Nothing Then oBanc = New DTOBanc(banc)
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.BancTransferPools.All(oEmp, oBanc)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BancTransferPools")
        End Try
        Return retval
    End Function

End Class
