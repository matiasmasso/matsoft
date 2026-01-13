
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CustomerClusterController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CustomerCluster/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CustomerCluster.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CustomerCluster")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CustomerCluster")>
    Public Function Update(<FromBody> value As DTOCustomerCluster) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CustomerCluster.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CustomerCluster")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CustomerCluster")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/CustomerCluster/delete")>
    Public Function Delete(<FromBody> value As DTOCustomerCluster) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CustomerCluster.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CustomerCluster")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CustomerCluster")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/CustomerCluster/Children/{parent}")>
    Public Function All(parent As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oParent As New DTOCustomerCluster(parent)
            Dim values = BEBL.CustomerCluster.Children(oParent)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els membres del cluster")
        End Try
        Return retval
    End Function

End Class

Public Class CustomerClustersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CustomerClusters/{holding}")>
    Public Function All(holding As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oHolding As New DTOHolding(holding)
            Dim values = BEBL.CustomerClusters.All(oHolding)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els CustomerClusters")
        End Try
        Return retval
    End Function

End Class

