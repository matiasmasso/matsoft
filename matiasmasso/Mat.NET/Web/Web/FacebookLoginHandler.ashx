<%@ WebHandler Language="VB" Class="FacebookLoginHandler" %>

Imports System.Web
Imports System.Web.SessionState

Public Class FacebookLoginHandler
    Implements IHttpHandler, IRequiresSessionState

    ''' <summary>
    '''  You will need to configure this handler in the Web.config file of your 
    '''  web and register it with IIS before being able to use it. For more information
    '''  see the following link: https://go.microsoft.com/?linkid=8101007
    ''' </summary>
#Region "IHttpHandler Members"

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            ' Return false in case your Managed Handler cannot be reused for another request.
            ' Usually this would be false in case you have some state information preserved per request.
            Return True
        End Get
    End Property

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim exs As New List(Of Exception)
        Dim accessToken As String = context.Request("accessToken")
        If String.IsNullOrEmpty(accessToken) Then
            exs.Add(New Exception("Facebook no devolvió ningun token para este usuario"))
        Else
            Dim oFbProfile = UserProfile(accessToken)
            If oFbProfile Is Nothing Then
                exs.Add(New Exception("Facebook no devolvió ningun usuario para este token"))
            ElseIf oFbProfile.email = "" Then
                exs.Add(New Exception("Facebook no devolvió ningun email para este token"))
            Else
                Dim oUser = FEB2.User.FromEmailSync(exs, Mvc.GlobalVariables.Emp, oFbProfile.email)
                If oUser Is Nothing Then
                    exs.Add(New Exception(String.Format("Usuario {0} con correo {1} no registrado", oFbProfile.email, oFbProfile.name)))
                Else
                    Dim returnUrl As String = context.Request("returnUrl")
                    If returnUrl = "" Then returnUrl = "/pro" '/" & oUser.Guid.ToString

                    Dim oLang = Mvc.ContextHelper.Lang
                    context.Session("User") = oUser
                    Dim persist As Boolean = context.Request("persist")
                    Mvc.ContextHelper.SetUserCookie(oUser, persist)
                    Mvc.ContextHelper.SetNavViewModelSync(oUser)
                    context.Response.Redirect(returnUrl)
                    Exit Sub
                End If
            End If
        End If

        If exs.Count > 0 Then
            context.Response.Redirect("Error")
        End If

    End Sub

#End Region

    Shared Function UserProfile(accessToken As String) As DTO.DTOFacebook.UserProfile
        Dim fbClient As New Facebook.FacebookClient(accessToken)
        Dim oJObject = fbClient.Get("me", New With {.fields = "email, name, gender, first_name, last_name, locale"})
        Dim json = oJObject.ToString
        Dim retval = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DTO.DTOFacebook.UserProfile)(json)
        Return retval
    End Function
End Class

