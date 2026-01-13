Public Class Session
    Inherits _FeblBase

    Shared Function FindSync(oGuid As Guid) As DTOSession
        Dim exs As New List(Of Exception)
        Return Api.FetchSync(Of DTOSession)(exs, "session", oGuid.ToString())
    End Function

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOSession)
        Return Await Api.Fetch(Of DTOSession)(exs, "session", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oSession As DTOSession) As Boolean
        If Not oSession.IsLoaded And Not oSession.IsNew Then
            Dim pSession = Api.FetchSync(Of DTOSession)(exs, "Session", oSession.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOSession)(pSession, oSession, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function NextSession(previousSession As DTOSession, exs As List(Of Exception)) As Task(Of DTOSession)
        Return Await Api.Execute(Of DTOSession, DTOSession)(previousSession, exs, "session/next")
    End Function

    Shared Function NextSessionSync(previousSession As DTOSession, exs As List(Of Exception)) As DTOSession
        Return Api.FetchSync(Of DTOSession)(exs, "session/next", previousSession.Guid.ToString())
    End Function



    Shared Function Factory(oLang As DTOLang) As DTOSession
        Dim retval As New DTOSession(DTOEmp.Ids.MatiasMasso)
        With retval
            .AppType = DTOApp.AppTypes.web
            .FchFrom = DTO.GlobalVariables.Now()
            .Lang = oLang
            .Rol = New DTORol(DTORol.Ids.Unregistered)
        End With
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOSession) As Task(Of DTOSession)
        Return Await Api.Execute(Of DTOSession, DTOSession)(value, exs, "session")
    End Function

    Shared Function UpdateSync(exs As List(Of Exception), value As DTOSession) As DTOSession
        Return Api.ExecuteSync(Of DTOSession, DTOSession)(value, exs, "session")
    End Function

    Shared Async Function Close(exs As List(Of Exception), oSession As DTOSession) As Task(Of Boolean)
        If oSession Is Nothing Then
            Return True
        Else
            Return Await Api.Fetch(Of Boolean)(exs, "session/close", oSession.Guid.ToString())
        End If
    End Function

    Shared Function CloseSync(exs As List(Of Exception), oSession As DTOSession) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "session/close", oSession.Guid.ToString())
    End Function


    Shared Function UserEmailAddress(oSession As DTOSession) As String
        Dim retval As String = ""
        Dim oUser As DTOUser = oSession.User
        If oUser IsNot Nothing Then
            retval = oUser.EmailAddress
        End If
        Return retval
    End Function

    Shared Async Function Contact(exs As List(Of Exception), oSession As DTOSession) As Task(Of DTOContact)
        Dim retval As DTOContact = Nothing
        Dim oUser As DTOUser = oSession.User
        If oUser IsNot Nothing Then
            If oUser.Contact Is Nothing Then
                Dim oContacts = Await Contacts.All(exs, oUser)
                If exs.Count = 0 AndAlso oContacts.Count > 0 Then
                    retval = oContacts.First
                End If
            Else
                retval = oUser.Contact
            End If
        End If
        Return retval
    End Function

    Shared Async Function Log(exs As List(Of Exception), oUser As DTOUser, oSrcGuid As Guid) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "session/log", oSrcGuid.ToString, OpcionalGuid(oUser))
    End Function


End Class
