Public Class Cx3Helper
    Shared Sub MakeCall(telnum As String)
        Process.Start("chrome.exe", "https://www.matiasmasso.es/cx3/makecall/" & telnum)
    End Sub
End Class
