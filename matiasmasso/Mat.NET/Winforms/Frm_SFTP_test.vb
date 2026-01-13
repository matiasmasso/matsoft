Imports System.Net

Public Class Frm_SFTP_test

    Private Sub Frm_SFTP_test_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBoxPKKFile.Text = GetSetting("Matsoft", "Mat.Net", "Amazon Key File")
    End Sub


    Private Sub ButtonBrowse_Click(sender As Object, e As EventArgs) Handles ButtonBrowse.Click
        SaveSetting("Matsoft", "Mat.Net", "Amazon Key File", TextBoxPKKFile.Text)
    End Sub

    Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click
        Dim request As FtpWebRequest = WebRequest.Create(TextBoxHost.Text)
        request.Method = WebRequestMethods.Ftp.DownloadFile
        request.EnableSsl = True ' Here you enabled request To use ssl instead Of clear text
        Dim response As WebResponse = request.GetResponse()
    End Sub
End Class