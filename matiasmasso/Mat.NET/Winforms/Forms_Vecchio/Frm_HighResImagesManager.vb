Public Class Frm_HighResImagesManager

    Private Sub Frm_HighResImagesManager_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFolder As String = "ftp://ftp.matiasmasso.es/downloads"
        Dim oFtp As New BLL.FTPclient(sFolder, BLLApp.FtpClient.Username, BLLApp.FtpClient.Password)
        Xl_FtpExplorer1.Load(oFtp)

    End Sub

    Private Sub Xl_FtpExplorer1_NodeMouseClick(sender As Object, e As MatEventArgs) Handles Xl_FtpExplorer1.NodeMouseClick
        Dim sFolder As String = e.Argument
        Cursor = Cursors.WaitCursor
        Dim oHighResImages As List(Of DTOHighResImage) = BLLHighResImages.HighResImagesToAssignFromFtpFolder(sFolder)
        Xl_HighResImages1.Load(oHighResImages)
        Cursor = Cursors.Default
    End Sub
End Class