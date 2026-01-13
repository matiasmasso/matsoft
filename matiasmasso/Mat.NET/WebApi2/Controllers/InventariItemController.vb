Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class InventariItemController
    Inherits _BaseController

    <HttpGet>
    <Route("api/inventariItem/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.InventariItem.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la InventariItem")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/InventariItem")>
    Public Function Update(<FromBody> value As DTOImmoble.InventariItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.InventariItem.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la InventariItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la InventariItem")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/InventariItem/delete")>
    Public Function Delete(<FromBody> value As DTOImmoble.InventariItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.InventariItem.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la InventariItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la InventariItem")
        End Try
        Return retval
    End Function

End Class

Public Class InventariItemsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/InventariItems/{immoble}")>
    Public Function All(immoble As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImmoble = New DTOImmoble(immoble)
            Dim values = BEBL.InventariItems.All(oImmoble)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les InventariItems")
        End Try
        Return retval
    End Function

End Class

