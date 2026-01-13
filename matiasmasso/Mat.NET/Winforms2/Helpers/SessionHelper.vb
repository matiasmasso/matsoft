Imports Facebook

Public Class SessionHelper

    Shared Async Function AddSession(oEmp As DTOEmp, oAppType As DTOApp.AppTypes, LangId As DTOLang.Ids, oCurId As DTOCur.Ids, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean = True

        If SessionHelper.IsPersisted() Then
            If Not Await GetFromPreviousSession(exs) Then
                Await GetFromLogin(oEmp, exs)
            End If
        Else
            Await GetFromLogin(oEmp, exs)
        End If

        Return retval
    End Function

    Shared Async Function GetFromPreviousSession(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oPreviousSession = Await SessionHelper.PreviousSession(exs)
        If oPreviousSession IsNot Nothing Then
            Current.Session = Await FEB.Session.NextSession(oPreviousSession, exs)
            If Current.Session IsNot Nothing Then
                SessionHelper.Persist(True)
                retval = True
            End If

        End If
        Return retval
    End Function


    Shared Async Function GetFromLogin(oEmp As DTOEmp, exs As List(Of Exception)) As Task
        Dim oFrm As New Frm_Login(oEmp)
        Dim rc = oFrm.ShowDialog()
        'If rc = DialogResult.Cancel Then
        'exs.Add(New Exception("l'usuari ha rebutjat identificar-se"))
        'Else
        If oFrm.RequestToFbLogin Then
                Dim oFrmFb As New Frm_FbLogin()
                oFrmFb.ShowDialog()
                Dim oOauthResult = oFrmFb.FacebookOAuthResult

                If oOauthResult.IsSuccess Then
                    Dim oAccesstoken = oOauthResult.AccessToken
                    Dim fb As New FacebookClient(oAccesstoken)

                    Dim result = fb.Get("me?fields=first_name,last_name,email")
                    If result IsNot Nothing Then
                        Dim emailAdddress As String = result.email
                        Dim oUser = Await FEB.User.FromEmail(exs, oEmp.Trimmed, emailAdddress)
                        oFrm.User = oUser
                    End If
                Else
                    Stop
                End If

            End If

            If oFrm.User IsNot Nothing Then
                Current.Session = New DTOSession
                With Current.Session
                    .User = oFrm.User
                    .Contact = .User.Contact
                    '.Culture = PreviousSession.Culture
                    '.Cur = DTOCur.Eur
                    .Lang = .User.Lang
                .FchFrom = DTO.GlobalVariables.Now()
                .AppType = DTOApp.AppTypes.matNet
                    .AppVersion = AppVersion()
                End With

                'FEB.Session.UpdateSync(exs, Current.Session)
                Await FEB.Session.Update(exs, Current.Session)
                If exs.Count = 0 Then
                    Persist(oFrm.Persist)
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        ' End If

    End Function

    Shared Function AppVersion() As String
        Dim retval As String = ""
        Try
            If System.Diagnostics.Debugger.IsAttached Then
                retval = "Debug mode"
            Else
                retval = My.Application.Deployment.CurrentVersion.ToString
            End If
        Catch ex As Exception
            retval = "(n/a)"
        End Try
        Return retval
    End Function

    Shared Async Function PreviousSession(exs As List(Of Exception)) As Task(Of DTOSession)
        Dim retval As DTOSession = Nothing
        'SaveSetting(DTOSession.CookieSessionName, "")
        Dim sSessionGuid As String = GetSetting(DTOSession.CookieSessionName)
        If GuidHelper.IsGuid(sSessionGuid) Then
            'retval = New DTOSession(New Guid(sSessionGuid))
            Dim oPreviousSession = Await FEB.Session.Find(New Guid(sSessionGuid), exs)
            If exs.Count = 0 AndAlso oPreviousSession IsNot Nothing Then
                retval = oPreviousSession
                With retval
                    .AppType = DTOApp.AppTypes.matNet
                    .AppVersion = AppVersion()
                End With
            End If
        End If
        Return retval
    End Function

    Shared Function IsPersisted() As Boolean
        ' Dim retval As Boolean = GetSetting("MatSoft", "Mat.NET", DTOSession.CookiePersistName) = "1"
        Dim retval As Boolean = GetSetting(DTOSession.CookiePersistName) = "1"
        Return retval
    End Function

    Shared Sub Persist(Optional persist As Boolean = False)
        'SaveSetting("MatSoft", "MAT.NET", DTOSession.CookieSessionName, Current.Session.Guid.ToString())
        'SaveSetting("MatSoft", "MAT.NET", DTOSession.CookiePersistName, "1")
        SaveSettingString(DTOSession.CookieSessionName, Current.Session.Guid.ToString())
        SaveSettingString(DTOSession.CookiePersistName, "1")
    End Sub

End Class
