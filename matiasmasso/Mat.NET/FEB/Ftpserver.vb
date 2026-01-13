Public Class Ftpserver
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oContact As DTOContact) As Task(Of DTOFtpserver)
        Return Await Api.Fetch(Of DTOFtpserver)(exs, "Ftpserver", oContact.Guid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oFtpserver As DTOFtpserver) As Boolean
        If Not oFtpserver.IsLoaded Then
            Dim pFtpserver = Api.FetchSync(Of DTOFtpserver)(exs, "Ftpserver", oFtpserver.Owner.Guid.ToString())
            If exs.Count = 0 And pFtpserver IsNot Nothing Then
                oFtpserver = pFtpserver
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oFtpserver As DTOFtpserver) As Task(Of Boolean)
        Return Await Api.Update(Of DTOFtpserver)(oFtpserver, exs, "Ftpserver")
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oFtpserver As DTOFtpserver) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOFtpserver)(oFtpserver, exs, "Ftpserver")
    End Function

    Shared Async Function Send(exs As List(Of Exception), oContact As DTOContact, oCod As DTOFtpserver.Path.Cods, fileContents As Byte(), remoteFilename As String) As Threading.Tasks.Task(Of String)
        Dim retval As String = ""
        Dim oFtpServer = Await Find(exs, oContact)
        If exs.Count = 0 Then
            If oFtpServer Is Nothing Then
                exs.Add(New Exception("Client sense configuració Ftp"))
            Else
                Dim remoteFolder = oFtpServer.Paths.FirstOrDefault(Function(x) x.cod = oCod)
                If remoteFolder Is Nothing Then
                    exs.Add(New Exception("el servidor ftp no te configurada una ruta per aquest proposit"))
                Else
                    Dim remoteFolderPath = If(remoteFolder.value.EndsWith("/"), remoteFolder.value, remoteFolder.value & "/")
                    Dim remotePath = remoteFolderPath & If(remoteFilename.StartsWith("/"), remoteFilename.Substring(1), remoteFilename)
                    retval = MatHelperStd.FtpHelper.SendFtp(exs, oFtpServer.Servername, oFtpServer.Username, oFtpServer.Password, fileContents, remotePath)
                End If
            End If
        End If
        Return retval
    End Function
End Class

Public Class Ftpservers
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOFtpserver))
        Return Await Api.Fetch(Of List(Of DTOFtpserver))(exs, "Ftpservers", oEmp.Id)
    End Function

End Class

