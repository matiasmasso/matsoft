Public Class AppUsrLog

    Shared Function Log(exs As List(Of Exception), oRequest As DTOAppUsrLog.Request) As DTOAppUsrLog.Response
        Dim retval As New DTOAppUsrLog.Response
        Try
            AppUsrLogLoader.Log(exs, oRequest)
            retval.Rol = AppUsrLogLoader.UserRol(oRequest)
            Dim oApp = AppLoader.Find(oRequest.AppId)
            If oApp IsNot Nothing Then
                retval.AppMinVersion = oApp.MinVersion
                retval.AppLastVersion = oApp.LastVersion
            End If

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Function LogExit(exs As List(Of Exception), oGuid As Guid) As Boolean
        Return AppUsrLogLoader.LogExit(exs, oGuid)
    End Function
End Class

Public Class AppUsrLogs

    Shared Function All() As List(Of DTOAppUsrLog.Request)
        Return AppUsrLogsLoader.all()
    End Function

    Shared Function lastLogs(AppId As DTOApp.AppTypes) As List(Of DTOAppUsrLog.Request) 'Mat.Net
        Return AppUsrLogsLoader.lastLogs(AppId)
    End Function

    Shared Function lastLogs(AppId As DTOApp.AppTypes, oUser As DTOUser) As List(Of DTOAppUsrLog) 'Mat.Net
        Return AppUsrLogsLoader.lastLogs(AppId, oUser)
    End Function
End Class
