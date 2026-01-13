Public Class CertificatIrpf


    Shared Function Find(oGuid As Guid) As DTOCertificatIrpf
        Return CertificatIrpfLoader.Find(oGuid)
    End Function

    Shared Function Update(oCertificatIrpf As DTOCertificatIrpf, exs As List(Of Exception)) As Boolean
        Return CertificatIrpfLoader.Update(oCertificatIrpf, exs)
    End Function

    Shared Function Delete(oCertificatIrpf As DTOCertificatIrpf, exs As List(Of Exception)) As Boolean
        Return CertificatIrpfLoader.Delete(oCertificatIrpf, exs)
    End Function

End Class



Public Class CertificatIrpfs
    Shared Function All(oContact As DTOContact) As List(Of DTOCertificatIrpf)
        Dim retval As List(Of DTOCertificatIrpf) = CertificatIrpfsLoader.All(oContact)
        Return retval
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTOCertificatIrpf)
        Dim retval As List(Of DTOCertificatIrpf) = CertificatIrpfsLoader.All(oUser)
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp) As List(Of DTOCertificatIrpf)
        Dim retval As List(Of DTOCertificatIrpf) = CertificatIrpfsLoader.All(oEmp)
        Return retval
    End Function
End Class
