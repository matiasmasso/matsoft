Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class SubscriptorController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Subscriptor/{sscGuid}/{user}")>
    Public Function Search(sscGuid As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSubscription As New DTOSubscription(sscGuid)
            Dim value = BEBL.Subscriptor.Find(user, oSubscription)
            retval = Request.CreateResponse(Of DTOSubscriptor)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al verificar la subscripcio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Subscriptor/search/{sscId}/{user}")>
    Public Function Search(sscId As DTOSubscription.Wellknowns, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSubscription = DTOSubscription.Wellknown(sscId)
            Dim value = BEBL.Subscriptor.Find(user, oSubscription)
            retval = Request.CreateResponse(Of DTOSubscriptor)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al verificar la subscripcio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Subscriptor/IsSubscribed/{ssc}/{user}")>
    Public Function IsSubscribed(ssc As DTOSubscription.Wellknowns, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSubscription = DTOSubscription.Wellknown(ssc)
            Dim oSubscriptor As New DTOSubscriptor(user, oSubscription)
            Dim value = BEBL.Subscriptors.isSubscribed(oSubscriptor)
            retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al verificar la subscripcio")
        End Try
        Return retval
    End Function
End Class

Public Class SubscriptorsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Subscriptors/{subscription}/{contact}")>
    Public Function All(subscription As Guid, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSubscription As New DTOSubscription(subscription)
            Dim oContact = DTOBaseGuid.opcional(Of DTOContact)(contact)
            Dim values = BEBL.Subscriptors.All(oSubscription, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els emails dels subscriptors")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Subscriptors/recipients/{subscription}/{contact}")>
    Public Function Recipients(subscription As Guid, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSubscription As New DTOSubscription(subscription)
            Dim oContact = DTOBaseGuid.opcional(Of DTOContact)(contact)
            Dim values = BEBL.Subscriptors.Recipients(oSubscription, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els emails dels subscriptors")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Subscriptors/Subscribe")>
    Public Function Subscribe(<FromBody> values As List(Of DTOSubscriptor)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Subscriptors.Subscribe(exs, values) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al Subscriure")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al Subscriure")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Subscriptors/Unsubscribe")>
    Public Function Unsubscribe(<FromBody> values As List(Of DTOSubscriptor)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Subscriptors.Unsubscribe(exs, values) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al cancelar la Subscripcio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cancelar la Subscripcio")
        End Try
        Return retval
    End Function

End Class
