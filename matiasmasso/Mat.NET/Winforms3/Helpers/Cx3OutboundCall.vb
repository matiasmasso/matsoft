Public Class Cx3OutboundCall

    Shared Sub MakeCall(telnum As String)
        Dim url As String = MmoUrl.Factory(True, "cx3/makecall", telnum)
        Process.Start("chrome.exe", url)
    End Sub

End Class
