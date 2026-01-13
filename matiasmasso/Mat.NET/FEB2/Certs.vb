Imports System.Security.Cryptography.X509Certificates

Public Class Cert

    Shared Async Function Find(oContact As DTOContact, exs As List(Of Exception)) As Task(Of DTOCert)
        Dim retval = Await Api.Fetch(Of DTOCert)(exs, "Cert", oContact.Guid.ToString())
        If exs.Count = 0 Then
            retval.Stream = Await Stream(oContact, exs)
        End If
        Return retval
    End Function

    Shared Async Function Stream(oContact As DTOContact, exs As List(Of Exception)) As Task(Of Byte())
        Return Await Api.FetchBinary(exs, "Cert/stream", oContact.Guid.ToString())
    End Function

    Shared Async Function Image(oContact As DTOContact, exs As List(Of Exception)) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "Cert/image", oContact.Guid.ToString())
    End Function

    Shared Async Function X509Certificate2(oContact As DTOContact, exs As List(Of Exception)) As Task(Of X509Certificate2)
        Dim retval As X509Certificate2 = Nothing
        Dim oCert = Await Find(oContact, exs)
        If oCert IsNot Nothing Then
            retval = New X509Certificate2(oCert.Stream, oCert.Pwd, X509KeyStorageFlags.PersistKeySet And X509KeyStorageFlags.MachineKeySet)
        End If
        Return retval
    End Function

    Shared Async Function Update(value As DTOCert, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.Stream IsNot Nothing Then
                oMultipart.AddFileContent("stream", value.Stream)
                oMultipart.AddFileContent("image", value.Image)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Cert")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oCert As DTOCert, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCert)(oCert, exs, "Cert")
    End Function

    Shared Function ImportFromFile(oCert As DTOCert, ByVal sFilename As String, ByVal sPwd As String, ByVal FchCaduca As Date, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If FileSystemHelper.GetStreamFromFile(sFilename, oCert.Stream, exs) Then
            With oCert
                .Pwd = sPwd
                .Ext = TextHelper.VbMid(sFilename, sFilename.LastIndexOf(".") + 2).ToUpper
                .Caduca = FchCaduca
            End With
            retval = True
        End If
        Return retval
    End Function
End Class
