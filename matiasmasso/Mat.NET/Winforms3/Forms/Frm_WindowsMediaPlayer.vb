Public Class Frm_WindowsMediaPlayer
    Public Sub New(oDocfile As DTODocFile)
        MyBase.New
        InitializeComponent()

        Dim url As String = oDocfile.DownloadUrl(True)
        AxWindowsMediaPlayer1.URL = url

    End Sub
End Class