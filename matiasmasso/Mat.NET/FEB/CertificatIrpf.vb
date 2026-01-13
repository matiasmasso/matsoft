Public Class CertificatIrpf
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOCertificatIrpf)
        Return Await Api.Fetch(Of DTOCertificatIrpf)(exs, "CertificatIrpf", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oCertificatIrpf As DTOCertificatIrpf) As Boolean
        If Not oCertificatIrpf.IsLoaded And Not oCertificatIrpf.IsNew Then
            Dim pCertificatIrpf = Api.FetchSync(Of DTOCertificatIrpf)(exs, "CertificatIrpf", oCertificatIrpf.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCertificatIrpf)(pCertificatIrpf, oCertificatIrpf, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Upload(exs As List(Of Exception), value As DTOCertificatIrpf) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "CertificatIrpf")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oCertificatIrpf As DTOCertificatIrpf) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCertificatIrpf)(oCertificatIrpf, exs, "CertificatIrpf")
    End Function
End Class

Public Class CertificatIrpfs
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOCertificatIrpf))
        Return Await Api.Fetch(Of List(Of DTOCertificatIrpf))(exs, "CertificatIrpfs/fromEmp", oEmp.Id)
    End Function

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOCertificatIrpf))
        Return Await Api.Fetch(Of List(Of DTOCertificatIrpf))(exs, "CertificatIrpfs/fromUser", oUser.Guid.ToString)
    End Function

    Shared Async Function All(exs As List(Of Exception), oContact As DTOContact) As Task(Of List(Of DTOCertificatIrpf))
        Return Await Api.Fetch(Of List(Of DTOCertificatIrpf))(exs, "CertificatIrpfs/fromContact", oContact.Guid.ToString())
    End Function

End Class
