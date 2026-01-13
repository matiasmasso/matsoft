Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WinMenuItemController
    Inherits _BaseController


    <HttpGet>
    <Route("api/WinMenuItem/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WinMenuItem.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WinMenuItem")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/WinMenuItem/icon/{guid}")>
    Public Function GetIcon(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WinMenuItem.Icon(guid)
            retval = MyBase.HttpImageMimeResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'icona del WinMenuItem")
        End Try

        Return retval
    End Function


    <HttpPost>
    <Route("api/WinMenuItem/upload")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)

        Dim resultHash As String = ""
        Dim result As HttpResponseMessage = Nothing
        Dim httpRequest = HttpContext.Current.Request

        Try

            If oHelper.FileValues.Count = 0 Then
                result = Request.CreateResponse(HttpStatusCode.BadRequest)
                result.ReasonPhrase = "No ha arribat cap fitxer al servidor"
            Else
                Dim exs As New List(Of Exception)
                Dim json As String = oHelper.GetValue("serialized")
                Dim oWinMenuItem = ApiHelper.Client.DeSerialize(Of DTOWinMenuItem)(json, exs)
                If oWinMenuItem Is Nothing Then
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el departament")
                Else
                    oWinMenuItem.Icon = oHelper.GetImage("icon")
                    If DAL.WinMenuItemLoader.Update(oWinMenuItem, exs) Then
                        result = Request.CreateResponse(HttpStatusCode.OK)
                    Else
                        result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el banner a DAL.WinMenuItemLoader.Upload")
                    End If
                End If

            End If
        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.DocFileLoader.Upload")
        End Try

        Return result
    End Function

    <HttpPost>
    <Route("api/WinMenuItem/delete")>
    Public Function Delete(<FromBody> value As DTOWinMenuItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WinMenuItem.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar WinMenuItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar WinMenuItem")
        End Try
        Return retval
    End Function
End Class

Public Class WinMenuItemsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WinMenuItems/{user}")> 'for Mat.NET
    Public Function all(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("usuari no reconegut")
            Else
                Dim values = BEBL.WinMenuItems.All(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els winmenuitems")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/WinMenuItems/Sprite/{user}")>
    Public Function GetSprite(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                Throw New HttpResponseException(HttpStatusCode.Forbidden)
            Else
                Dim oImage = BEBL.WinMenuItems.Sprite(oUser)
                retval = MyBase.HttpImageResponseMessage(oImage)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els iconos del menu")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/WinMenuItems/Sprite")>
    Public Function Sprite(<FromBody()> ByVal oGuids As Guid()) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            If oGuids.Count > 0 Then
                Dim oSprite = BEBL.WinMenuItems.Sprite(oGuids.ToList)
                retval = MyBase.HttpImageResponseMessage(oSprite)
            Else
                retval = MyBase.HttpImageResponseMessage(Nothing)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descarregar el sprite")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/WinMenuItems/Sprite/{localSpriteHash}")>
    Public Function SpriteWithCheckControl(localSpriteHash As String, <FromBody()> ByVal oGuids As Guid()) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSprite = BEBL.WinMenuItems.Sprite(oGuids.ToList)
            Dim serverHash = CryptoHelper.HashMD5(oSprite)
            Dim localHash = CryptoHelper.FromUrFriendlyBase64(localSpriteHash)
            If serverHash = localHash Then
                retval = MyBase.HttpImageResponseMessage(Nothing)
            Else
                retval = MyBase.HttpImageResponseMessage(oSprite)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descarregar el sprite")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WinMenuItems/SaveOrder")>
    Public Function SaveOrder(<FromBody()> ByVal values As List(Of DTOWinMenuItem)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WinMenuItems.SaveOrder(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar l'ordre dels menus")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar l'ordre dels menus")
        End Try
        Return retval
    End Function

End Class
