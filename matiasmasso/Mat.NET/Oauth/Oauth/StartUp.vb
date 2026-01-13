Imports Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Infrastructure
Imports System.Web.Http
Imports Microsoft.Owin.Security.OAuth
Imports System.Security.Claims


Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        Dim oauthProvider = New OAuthAuthorizationServerProvider() With {
            .OnGrantResourceOwnerCredentials = Function(context)
                                                   If context.UserName = "matias" AndAlso context.Password = "1234" Then
                                                       Dim claimsIdentity = New ClaimsIdentity(context.Options.AuthenticationType)
                                                       claimsIdentity.AddClaim(New Claim("user", context.UserName))
                                                       context.Validated(claimsIdentity)
                                                   Else
                                                       context.Rejected()
                                                   End If

                                               End Function,
            .OnValidateClientAuthentication = Function(context)
                                                  Dim clientId As String
                                                  Dim clientSecret As String
                                                  If context.TryGetBasicCredentials(clientId, clientSecret) Then
                                                      If clientId = "rajeev" AndAlso clientSecret = "secretKey" Then
                                                          context.Validated()
                                                      End If
                                                  End If

                                              End Function
        }

        Dim oauthOptions = New OAuthAuthorizationServerOptions() With {
            .AllowInsecureHttp = True,
            .TokenEndpointPath = New PathString("/accesstoken"),
            .Provider = oauthProvider,
            .AuthorizationCodeExpireTimeSpan = TimeSpan.FromMinutes(1),
            .AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(3),
            .SystemClock = New SystemClock()
        }
        app.UseOAuthAuthorizationServer(oauthOptions)
        app.UseOAuthBearerAuthentication(New OAuthBearerAuthenticationOptions())

        Dim config = New HttpConfiguration()
        config.MapHttpAttributeRoutes()
        app.UseWebApi(config)
    End Sub
End Class
