Public Class MediaResourceFtpHelper
    Shared Function FtpClient() As FTPclient
        Dim retval As New FTPclient(DTOMediaResource.FTPPATH, DTOMediaResource.FTPUSER, DTOMediaResource.FTPPWD)
        Return retval
    End Function

    Shared Function GetStream(oMediaResource As DTOMediaResource) As Byte()
        Dim retval As Byte() = Nothing
        Dim sTargetFilename As String = DTOMediaResource.TargetFilename(oMediaResource)
        Dim sUrl = DTOMediaResource.FTPPATH & "/" & sTargetFilename
        Dim oFtp = FtpClient()
        retval = oFtp.Download(sUrl)
        Return retval
    End Function
End Class
