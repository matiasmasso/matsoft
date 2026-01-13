Public Class SessionHelper

    Shared Async Function AddSession(oEmpId As DTOEmp.Ids, oAppType As DTOApp.AppTypes, LangId As DTOLang.Ids, oCurId As DTOCur.Ids, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean = True

        If SessionHelper.IsPersisted() Then
            If Not Await GetFromPreviousSession(exs) Then
                GetFromLogin(oEmpId)
            End If
        Else
            GetFromLogin(oEmpId)
        End If

        Return retval
    End Function

    Shared Async Function GetFromPreviousSession(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Current.Session = Await FEB.Session.NextSession(SessionHelper.PreviousSession(), exs)
        If Current.Session IsNot Nothing Then
            SessionHelper.Persist(True)
            retval = True
        End If
        Return retval
    End Function


    Shared Sub GetFromLogin(oEmpId As DTOEmp.Ids)
        Dim oFrm As New Frm_Login(oEmpId)
        oFrm.ShowDialog()
        If oFrm.User IsNot Nothing Then
            Current.Session = New DTOSession
            With Current.Session
                .User = oFrm.User
                .Contact = .User.contact
                '.Culture = PreviousSession.Culture
                '.Cur = DTOCur.Eur
                .Lang = .User.Lang
                .FchFrom = DTO.GlobalVariables.Now()
            End With

            Dim exs As New List(Of Exception)
            FEB.Session.UpdateSync(exs, Current.Session)
            If exs.Count = 0 Then
                Persist(oFrm.Persist)
            Else
                UIHelper.WarnError(exs)
            End If
        End If

    End Sub

    Shared Function PreviousSession() As DTOSession
        Dim retval As DTOSession = Nothing
        Dim sSessionGuid As String = GetSetting("MatSoft", "Mat.NET", DTOSession.CookieSessionName)
        If GuidHelper.IsGuid(sSessionGuid) Then
            retval = New DTOSession(New Guid(sSessionGuid))
        End If
        Return retval
    End Function

    Shared Function IsPersisted() As Boolean
        Dim retval As Boolean = GetSetting("MatSoft", "Mat.NET", DTOSession.CookiePersistName) = "1"
        Return retval
    End Function

    Shared Sub Persist(Optional persist As Boolean = False)
        SaveSetting("MatSoft", "MAT.NET", DTOSession.CookieSessionName, Current.Session.Guid.ToString())
        SaveSetting("MatSoft", "MAT.NET", DTOSession.CookiePersistName, "1")
    End Sub

End Class
