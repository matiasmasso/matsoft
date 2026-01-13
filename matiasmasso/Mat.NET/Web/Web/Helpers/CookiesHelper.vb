Public Class CookiesHelper

    Public Enum Cookies
        None
        User
        UserPersist
        Lang
    End Enum

    Private Shared Function Context() As HttpContext
        Return Web.HttpContext.Current
    End Function

    Shared Sub SetUserCookie(oUser As DTOUser, persist As Boolean)
        SetCookieValue(Cookies.User, oUser)
        If persist Then
            SetCookieValue(Cookies.UserPersist)
        Else
            RemoveCookie(Cookies.UserPersist)
        End If
    End Sub

    Shared Sub SetLangCookie(oLang As DTOLang)
        SetCookieValue(Cookies.Lang, oLang.Tag)
    End Sub

    Shared Function GetUser(oContext As HttpContext) As DTOUser
        Dim retval As DTOUser = Nothing
        Dim sGuid = GetCookieValue(Cookies.User)
        Dim oGuid = GuidHelper.GetGuid(sGuid)
        If Not oGuid.Equals(Guid.Empty) Then
            retval = New DTOUser(oGuid)
        End If
        Return retval
    End Function

    Shared Function GetLang() As DTOLang
        Dim sLang = GetCookieValue(Cookies.Lang)
        Dim retval = DTOLang.Factory(sLang)
        Return retval
    End Function

    Shared Function GetCookieValue(oCookieId As Cookies) As String
        Dim retval As String = ""
        Dim oCookie As HttpCookie = Context.Request.Cookies(oCookieId.ToString)
        If oCookie IsNot Nothing Then
            retval = oCookie.Value
        End If
        Return retval
    End Function

    Shared Sub SetCookieValue(oCookieId As DTOSession.CookieIds, oBaseGuid As DTOBaseGuid)
        If oBaseGuid IsNot Nothing Then
            SetCookieValue(oCookieId, oBaseGuid.Guid)
        End If
    End Sub

    Shared Sub SetCookieValue(oCookieId As DTOSession.CookieIds, oGuid As Guid)
        SetCookieValue(oCookieId, oGuid.ToString)
    End Sub

    Shared Sub SetCookieValue(oCookieId As Cookies, Optional sValue As String = "")
        Dim oCookie As HttpCookie = Context.Request.Cookies(oCookieId.ToString)
        If oCookie Is Nothing Then
            oCookie = New HttpCookie(oCookieId.ToString)
            Select Case oCookieId
                'Case DTOSession.CookieIds.LastProductBrowsed
                '   oCookie.Expires = DateTime.Now.AddSeconds(1)
                Case Else
                    oCookie.Expires = DateTime.Now.AddYears(50)
            End Select
            Context.Response.Cookies.Set(oCookie)
        End If
        oCookie.Value = sValue
    End Sub

    Shared Sub RemoveCookie(oCookieId As Cookies)
        If Context.Request.Cookies(oCookieId.ToString) IsNot Nothing Then
            Context.Response.Cookies(oCookieId.ToString).Expires = DateTime.Now.AddDays(-1)
        End If
    End Sub
End Class
