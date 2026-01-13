Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DeliveryTraspasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/DeliveryTraspas/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DeliveryTraspas.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la DeliveryTraspas")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/DeliveryTraspas/FullTraspas/{user}/{mgzFrom}/{mgzTo}")>
    Public Function FullTraspas(user As Guid, mgzFrom As Guid, mgzTo As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser = BEBL.User.Find(user)
            oUser.Emp = MyBase.GetEmp(oUser.Emp.Id)
            Dim oMgzFrom As New DTOMgz(mgzFrom)
            Dim oMgzTo As New DTOMgz(mgzTo)
            If BEBL.DeliveryTraspas.FullTraspas(exs, oUser, oMgzFrom, oMgzTo) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la DeliveryTraspas")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la DeliveryTraspas")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/DeliveryTraspas")>
    Public Function Update(<FromBody> value As DTODeliveryTraspas) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.DeliveryTraspas.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la DeliveryTraspas")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la DeliveryTraspas")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/DeliveryTraspas/delete")>
    Public Function Delete(<FromBody> value As DTODeliveryTraspas) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.DeliveryTraspas.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la DeliveryTraspas")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la DeliveryTraspas")
        End Try
        Return retval
    End Function

End Class

Public Class DeliveryTraspassosController
    Inherits _BaseController

    <HttpGet>
    <Route("api/DeliveryTraspassos/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.DeliveryTraspassos.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les DeliveryTraspassos")
        End Try
        Return retval
    End Function

End Class




