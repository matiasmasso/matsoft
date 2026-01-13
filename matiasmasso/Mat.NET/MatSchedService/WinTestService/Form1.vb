Public Class Form1

    Private _LogLevel As LogLevels = LogLevels.Standard

    Private Enum LogLevels
        Standard
        Intensive
    End Enum

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim exs As New List(Of Exception)
        If BLL.BLLApp.Initialize(DTO.DTOEmp.Ids.MatiasMasso, DTO.DTOSession.AppTypes.MatSched, DTOLang.Ids.ESP, DTO.DTOCur.Ids.EUR, exs) Then
            _LogLevel = IIf(BLL.BLLDefault.EmpValue(DTO.DTODefault.Codis.MatschedLogLevel), LogLevels.Intensive, LogLevels.Standard)
            LogEvent("Matsched App.Initialized", LogLevels.Intensive)
            LogEvent("App.Current.Emp = " & BLLApp.Emp.Id.ToString, LogLevels.Intensive)

            Dim oUser As DTO.DTOUser = Nothing
            Try
                oUser = BLL.BLLUser.FromEmail("info@matiasmasso.es")
                If oUser Is Nothing Then
                    LogEvent("MatSched User not Found", LogLevels.Intensive)
                Else
                    If BLLSession.CreateWindowsSession(oUser, exs) Then
                        LogEvent("MatSched User:" & BLLSession.Current.User.EmailAddress & vbCrLf & "Guid: " & oUser.Guid.ToString, LogLevels.Intensive)

                        Dim oSession As DTO.DTOSession = BLL.BLLSession.Current
                        LogEvent("MatSched Sessio:" & oSession.Guid.ToString, LogLevels.Intensive)

                        If _LogLevel = LogLevels.Standard Then
                            Dim sb As New System.Text.StringBuilder
                            sb.AppendLine("Matsched App.Initialized")
                            sb.AppendLine("App.Current.Emp = " & BLLApp.Emp.Id.ToString)
                            sb.AppendLine("MatSched Usuari:" & BLLSession.Current.User.EmailAddress & vbCrLf & "Guid: " & BLLSession.Current.User.Guid.ToString)
                            sb.AppendLine("MatSched Sessio:" & oSession.Guid.ToString)
                        End If

                        Stop
                    Else
                        LogEvent("MatSchedService.CreateWindowsSession(oUser,exs) failed", LogLevels.Standard)
                    End If
                End If
            Catch ex As Exception
                LogEvent("MatSched User not Found" & vbCrLf & ex.Message, LogLevels.Standard)
            End Try


        Else
            LogEvent("Matsched App. could not be Initialized", LogLevels.Standard)
        End If

    End Sub

    Private Sub LogEvent(ByVal s As String, ByVal iLogLevel As LogLevels)
        Dim BlLog As Boolean = iLogLevel = LogLevels.Standard
        If _LogLevel = LogLevels.Intensive Then BlLog = True

        If BlLog Then
            Debug.Print("MATSCHED: " & s)
        End If
    End Sub
End Class
