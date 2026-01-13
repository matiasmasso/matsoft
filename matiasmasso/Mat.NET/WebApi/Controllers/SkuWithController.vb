Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SkuWithsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/SkuWiths/{skuParent}/{mgz}/{fch}")>
    Public Function Find(SkuParent As Guid, mgz As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSkuParent As New DTOProductSku(SkuParent)
            Dim oMgz As New DTOMgz(mgz)
            Dim value = BEBL.SkuWiths.Find(oSkuParent, oMgz, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la SkuWith")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/SkuWiths/{parent}")>
    Public Function Update(parent As Guid, <FromBody> children As List(Of GuidQty)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.SkuWiths.Update(parent, children, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar SkuWith")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar SkuWith")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/SkuWiths/delete/{parent}")>
    Public Function Delete(parent As Guid, mgz As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.SkuWiths.Delete(parent, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la SkuWith")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la SkuWith")
        End Try
        Return retval
    End Function



End Class

