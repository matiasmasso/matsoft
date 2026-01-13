Public Class DTOUsrLog
    Property usrCreated As DTOUser
    Property usrLastEdited As DTOUser
    Property fchCreated As Date
    Property fchLastEdited As Date

    Shared Function Factory(Optional oUser As DTOUser = Nothing) As DTOUsrLog
        Dim retval As New DTOUsrLog
        With retval
            .UsrCreated = oUser
            .UsrLastEdited = oUser
            '.FchCreated = Now
            '.FchLastEdited = .FchCreated
        End With
        Return retval
    End Function

    Public Function Text() As String
        Dim retval As String = ""
        Dim sameUser = _UsrCreated IsNot Nothing AndAlso _UsrCreated.Equals(_UsrLastEdited)
        Dim sameFch = Math.Abs((_FchCreated - FchLastEdited).TotalSeconds) < 2
        Dim sameUserFch = sameUser And sameFch
        If _UsrLastEdited Is Nothing Or sameUserFch Then
            If _FchCreated = Nothing Then
                retval = String.Format("Registrat per {0}", DTOUser.NicknameOrElse(_UsrCreated))
            Else
                retval = String.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}", DTOUser.NicknameOrElse(_UsrCreated), _FchCreated)
            End If
        Else
            If _FchLastEdited = Nothing Then

                retval = String.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}  i modificat per {2} ", DTOUser.NicknameOrElse(_UsrCreated), _FchCreated, DTOUser.NicknameOrElse(_UsrLastEdited))
            Else
                retval = String.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}  i modificat per {2} el {3:dd/MM/yy HH:mm}", DTOUser.NicknameOrElse(_UsrCreated), _FchCreated, DTOUser.NicknameOrElse(_UsrLastEdited), FchLastEdited)
            End If
        End If
        Return retval
    End Function

    Shared Function LogText(oUserCreated As DTOUser, 'Per compatibilitat amb antics
                            Optional oUserLastEdited As DTOUser = Nothing,
                            Optional DtFchCreated As Date = Nothing,
                            Optional DtFchLastEdited As Date = Nothing) As String

        Dim oUsrLog = DTOUsrLog.Factory(oUserCreated)
        With oUsrLog
            .UsrLastEdited = oUserLastEdited
            .FchCreated = DtFchCreated
            .FchLastEdited = DtFchLastEdited
        End With
        Return oUsrLog.Text
    End Function


End Class
