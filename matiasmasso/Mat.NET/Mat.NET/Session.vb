Public Class Session
    Shared _Session As DTO.DTOSession

    Shared ReadOnly Property Guid As Guid
        Get
            Return _Session.Guid
        End Get
    End Property

    Shared ReadOnly Property User As DTO.DTOUser
        Get
            Return _Session.User
        End Get
    End Property

    Shared Function Initialize() As Boolean
        Dim retval As Boolean
        Dim IsPersisted As Boolean = GetSetting("MatSoft", "Mat.NET", DTOSession.CookiePersistName) = "1"
        If IsPersisted Then
            Dim sSessionGuid As String = GetSetting("MatSoft", "Mat.NET", DTOSession.CookieSessionName)
            If GuidHelper.IsGuid(sSessionGuid) Then
                Dim oGuid As New Guid(sSessionGuid)
                Dim oLastSession As DTOSession = BLL.BLLSession.Find(oGuid)
                If oLastSession IsNot Nothing Then
                    BLL.BLLUser.Load(oLastSession.User)
                    _Session = BLL.BLLSession.GetNewSession(, oLastSession.User)
                    retval = True
                End If
            End If
        End If
        Return retval
    End Function

    Shared Sub Initialize(oUser As DTOUser, BlPersist As Boolean)
        _Session = BLL.BLLSession.GetNewSession(, oUser)
        SaveSetting("MatSoft", "Mat.NET", DTOSession.CookieSessionName, _Session.Guid.ToString)
        SaveSetting("MatSoft", "Mat.NET", DTOSession.CookiePersistName, IIf(BlPersist, "1", "0"))
    End Sub

End Class
