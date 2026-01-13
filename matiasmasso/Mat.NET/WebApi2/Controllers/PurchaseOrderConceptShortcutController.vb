Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PurchaseOrderConceptShortcutController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PurchaseOrderConceptShortcut/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PurchaseOrderConceptShortcut.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el shortcut dels conceptes de comanda")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PurchaseOrderConceptShortcut")>
    Public Function Update(<FromBody> value As DTOPurchaseOrder.ConceptShortcut) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PurchaseOrderConceptShortcut.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el shortcut dels conceptes de comanda")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el shortcut dels conceptes de comanda")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/PurchaseOrderConceptShortcut/delete")>
    Public Function Delete(<FromBody> value As DTOPurchaseOrder.ConceptShortcut) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PurchaseOrderConceptShortcut.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el shortcut dels conceptes de comanda")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el shortcut dels conceptes de comanda")
        End Try
        Return retval
    End Function

End Class

Public Class PurchaseOrderConceptShortcutLoadersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PurchaseOrderConceptShortcuts")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.PurchaseOrderConceptShortcuts.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els shortcuts dels conceptes de comanda")
        End Try
        Return retval
    End Function

End Class
