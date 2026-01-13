Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SocialMediaWidgetController
    Inherits _BaseController

    <HttpGet>
    <Route("api/SocialMediaWidget/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.SocialMediaWidget.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la SocialMediaWidget")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/SocialMediaWidget/{user}/{platform}/{product}")>
    Public Function Find(user As Guid, platform As DTOSocialMediaWidget.Platforms, product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oProduct As New DTOProduct(product)
            Dim value = BEBL.SocialMediaWidget.Widget(oUser, platform, oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la SocialMediaWidget")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/SocialMediaWidget")>
    Public Function Update(<FromBody> value As DTOSocialMediaWidget) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.SocialMediaWidget.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la SocialMediaWidget")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la SocialMediaWidget")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/SocialMediaWidget/delete")>
    Public Function Delete(<FromBody> value As DTOSocialMediaWidget) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.SocialMediaWidget.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la SocialMediaWidget")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la SocialMediaWidget")
        End Try
        Return retval
    End Function

End Class

Public Class SocialMediaWidgetsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/SocialMediaWidgets")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.SocialMediaWidgets.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les SocialMediaWidgets")
        End Try
        Return retval
    End Function

End Class
