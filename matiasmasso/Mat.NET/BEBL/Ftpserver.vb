Public Class Ftpserver

    Shared Function Find(oOwner As DTOBaseGuid) As DTOFtpserver
        Return FtpserverLoader.Find(oOwner)
    End Function

    Shared Function Update(oFtpserver As DTOFtpserver, exs As List(Of Exception)) As Boolean
        Return FtpserverLoader.Update(oFtpserver, exs)
    End Function

    Shared Function Delete(oFtpserver As DTOFtpserver, exs As List(Of Exception)) As Boolean
        Return FtpserverLoader.Delete(oFtpserver, exs)
    End Function

    Shared Function Send(exs As List(Of Exception), oContact As DTOBaseGuid, oCod As DTOFtpserver.Path.Cods, fileContents As Byte(), remoteFilename As String) As Boolean
        Dim oFtpServer = Find(oContact)
        If oFtpServer Is Nothing Then
            exs.Add(New Exception("Client sense configuració Ftp"))
        Else
            Dim remoteFolder = oFtpServer.Paths.FirstOrDefault(Function(x) x.cod = oCod)
            If remoteFolder Is Nothing Then
                exs.Add(New Exception("el servidor ftp no te configurada una ruta per aquest proposit"))
            Else
                Dim remoteFolderPath = IIf(remoteFolder.value.EndsWith("/"), remoteFolder.value, remoteFolder.value & "/")
                Dim remotePath = remoteFolderPath & IIf(remoteFilename.StartsWith("/"), remoteFilename.Substring(1), remoteFilename)
                MatHelperStd.FtpHelper.SendFtp(exs, oFtpServer.Servername, oFtpServer.Username, oFtpServer.Password, fileContents, remotePath)
            End If
        End If
        Return exs.Count = 0
    End Function

End Class



Public Class Ftpservers
    Shared Function All(oEmp As DTOEmp) As List(Of DTOFtpserver)
        Dim retval As List(Of DTOFtpserver) = FtpserversLoader.All(oEmp)
        Return retval
    End Function
End Class


