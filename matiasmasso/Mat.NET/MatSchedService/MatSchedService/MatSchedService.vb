Public Class MatSchedService

    Private _WorkingThread As System.Threading.Thread
    Private _Timer As System.Timers.Timer
    Private _Seconds As Integer

    Protected Overrides Sub OnStart(ByVal args() As String)
        LogEvent("onStart", EventLogEntryType.Information)

        'FEB.App.Initialize("https://matiasmasso-api.azurewebsites.net")
        FEB.App.Initialize("https://api.matiasmasso.es")

        Dim ts As System.Threading.ThreadStart
        ts = AddressOf DoWork
        _WorkingThread = New System.Threading.Thread(ts)
        _WorkingThread.Start()

    End Sub

    Private Sub DoWork()
        _Timer = New System.Timers.Timer()
        With _Timer
            .Interval = 1 * 60 * 1000 'un minut en milisegons
            AddHandler .Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Me.OnTimerElapsed)
            .Enabled = True
        End With
    End Sub

    Private Sub OnTimerElapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        ServiceHelper.TimeElapsed()
    End Sub

    Protected Overrides Sub OnStop()
        _Timer.Stop()
        _Timer.Close()
        _Timer = Nothing
    End Sub

    Shared Sub LogEvent(msg As String, entryType As EventLogEntryType)
        Try
            System.Diagnostics.EventLog.WriteEntry("MatScheD", msg, entryType)
        Catch ex As Exception

        End Try
    End Sub

End Class
