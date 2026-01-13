Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SubscriptionController
    Inherits _BaseController

    <HttpGet>
    <Route("api/subscription/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Subscription.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la subscripció")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Subscription")>
    Public Function Update(<FromBody> value As DTOSubscription) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Subscription.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Subscription")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Subscription")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Subscription/delete")>
    Public Function Delete(<FromBody> value As DTOSubscription) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Subscription.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Subscription")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Subscription")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/subscription/{subscription}/subscriptors")>
    Public Function GetSubscriptors(subscription As Guid) As List(Of DTOSubscriptor)
        Dim oSubscription As New DTOSubscription(subscription)
        Dim retval = BEBL.Subscription.Subscriptors(oSubscription)
        Return retval
    End Function

    <HttpGet>
    <Route("api/subscription/{subscription}/subscriptors/{contact}")>
    Public Function GetSubscriptors(subscription As Guid, contact As Guid) As List(Of DTOSubscriptor)
        Dim oSubscription As New DTOSubscription(subscription)
        Dim oContact As New DTOContact(contact)
        Dim retval = BEBL.Subscription.Subscriptors(oSubscription, oContact)
        Return retval
    End Function

    <HttpGet>
    <Route("api/subscription/{subscription}/SubscriptorsWithManufacturer")>
    Shared Function SubscriptorsWithManufacturer(subscription As Guid) As List(Of DTOSubscriptor)
        Dim oSubscription As New DTOSubscription(subscription)
        Dim retval = BEBL.Subscription.SubscriptorsWithManufacturer(oSubscription)
        Return retval
    End Function
End Class

Public Class SubscriptionsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Subscriptions/{user}")>
    Public Function FromUser(user As Guid) As HttpResponseMessage 'iMat 3.3 
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Subscriptions.All(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Subscriptions")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Subscriptions/FromEmpUser/{emp}/{user}")>
    Public Function FromEmpUser(emp As DTOEmp.Ids, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oUser = DTOBaseGuid.opcional(Of DTOUser)(user)
            Dim values = BEBL.Subscriptions.All(oEmp, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Subscriptions")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Subscriptions/FromEmpRol/{emp}/{rol}")>
    Public Function FromEmpRol(emp As DTOEmp.Ids, rol As DTORol.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oRol As New DTORol(rol)
            Dim values = BEBL.Subscriptions.All(oEmp, oRol)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Subscriptions")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Subscriptions/{user}")>
    Public Function Update(user As Guid, <FromBody> values As List(Of DTOSubscription)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser As New DTOUser(user)
            If BEBL.Subscriptions.Update(oUser, values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar les Subscriptions")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar les Subscriptions")
        End Try
        Return retval
    End Function
End Class