Imports System.Dynamic
Imports Facebook

Public Class Frm_FbLogin
    Private ReadOnly _loginUrl As Uri
    Protected _fb As FacebookClient
    Public Property FacebookOAuthResult As FacebookOAuthResult

    Public Sub New()
        Dim appId As String = "489736407757151"
        Dim extendedPermissions As String = "email" 'comma separated list of permissions
        _fb = New FacebookClient()
        _loginUrl = GenerateLoginUrl(appId, extendedPermissions)
        InitializeComponent()
    End Sub

    Private Function GenerateLoginUrl(ByVal appId As String, ByVal extendedPermissions As String) As Uri
        Dim parameters As Object = New ExpandoObject()
        parameters.client_id = appId
        parameters.redirect_uri = "https://www.facebook.com/connect/login_success.html"
        parameters.response_type = "token"
        parameters.display = "popup"
        If Not String.IsNullOrWhiteSpace(extendedPermissions) Then parameters.scope = extendedPermissions
        Return _fb.GetLoginUrl(parameters)
    End Function

    Private Sub FacebookLoginDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        WebBrowser1.Navigate(_loginUrl.AbsoluteUri)
    End Sub

    Private Sub webBrowser1_Navigated(ByVal sender As Object, ByVal e As WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
        Dim oauthResult As FacebookOAuthResult = Nothing

        If _fb.TryParseOAuthCallbackUrl(e.Url, oauthResult) Then
            FacebookOAuthResult = oauthResult
            Dim dtExpires As Date = Nothing
            If Date.TryParse(oauthResult.Expires, dtExpires) Then
                If dtExpires > Now Then
                End If
            End If
            Dim sErrorReason = oauthResult.ErrorReason
            Dim sErrorDescription = oauthResult.ErrorDescription
                DialogResult = If(FacebookOAuthResult.IsSuccess, DialogResult.OK, DialogResult.No)
            Else
                FacebookOAuthResult = Nothing
        End If
    End Sub
End Class