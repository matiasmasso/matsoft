Public Class Session

    Shared Function Find(oGuid As Guid) As DTOSession
        Dim retval = SessionLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oSession As DTOSession) As Boolean
        Dim retval As Boolean = SessionLoader.Load(oSession)
        Return retval
    End Function

    Shared Function Update(oSession As DTOSession, exs As List(Of Exception)) As Boolean
        Dim retval = SessionLoader.Update(oSession, exs)
        Return retval
    End Function

    Shared Function Delete(oSession As DTOSession, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SessionLoader.Delete(oSession, exs)
        Return retval
    End Function

    Shared Function Log(exs As List(Of Exception), oSrcGuid As Guid, Optional oUser As DTOUser = Nothing) As Boolean
        Dim retval As Boolean = SessionLoader.Log(exs, oSrcGuid, oUser)
        Return retval
    End Function

    Shared Function Close(oSession As DTOSession, exs As List(Of Exception)) As Boolean
        oSession.FchTo = DTO.GlobalVariables.Now()
        Dim retval = SessionLoader.Update(oSession, exs)
        Return retval
    End Function


    Shared Function NextSession(oLastSession As DTOSession, exs As List(Of Exception)) As DTOSession
        Dim retval As DTOSession = Nothing
        Dim previousSession As DTOSession = BEBL.Session.Find(oLastSession.Guid)
        If previousSession IsNot Nothing Then
            retval = New DTOSession
            With retval
                .AppType = oLastSession.AppType
                .AppVersion = oLastSession.AppVersion
                .User = previousSession.User
                If .User IsNot Nothing Then
                    .Rol = .User.Rol 'així refresca el rol no sigui que l'haguem actualitzat
                End If
                .Contact = previousSession.Contact
                .Culture = previousSession.Culture
                .Cur = previousSession.Cur
                .Lang = previousSession.Lang
                .FchFrom = DTO.GlobalVariables.Now()
            End With
        End If

        BEBL.Session.Update(retval, exs)
        Dim oApp = AppLoader.Find(oLastSession.AppType)
        retval.LastVersion = oApp.LastVersion
        retval.MinVersion = oApp.MinVersion
        Return retval
    End Function

    Shared Function NextSession(lastToken As Guid, exs As List(Of Exception)) As DTOSession
        Dim oSession As DTOSession = Nothing
        Dim previousSession As DTOSession = BEBL.Session.Find(lastToken)
        If previousSession IsNot Nothing Then
            oSession = New DTOSession
            With oSession
                .AppType = previousSession.AppType
                .User = previousSession.User
                If .User IsNot Nothing Then
                    .Rol = .User.Rol 'així refresca el rol no sigui que l'haguem actualitzat
                End If
                .Contact = previousSession.Contact
                .Culture = previousSession.Culture
                .Cur = previousSession.Cur
                .Lang = previousSession.Lang
                .FchFrom = DTO.GlobalVariables.Now()
            End With
        End If

        Dim retval As DTOSession = Nothing
        If BEBL.Session.Update(oSession, exs) Then
            retval = oSession
        End If
        Return retval
    End Function


End Class
