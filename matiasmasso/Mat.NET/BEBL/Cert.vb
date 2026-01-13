Imports System.Security.Cryptography.X509Certificates
Imports C1.C1Pdf
Public Class Cert


#Region "CRUD"
    Shared Function FromContact(oContact As DTOContact) As DTOCert
        Return CertLoader.Find(oContact.Guid)
    End Function

    Shared Function Find(oGuid As Guid) As DTOCert
        Return CertLoader.Find(oGuid)
    End Function

    Shared Function Load(ByRef oCert As DTOCert) As Boolean
        Return CertLoader.Load(oCert)
    End Function

    Shared Function Image(ByRef oCert As DTOCert) As Byte()
        Return CertLoader.Image(oCert)
    End Function

    Shared Function Update(oCert As DTOCert, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CertLoader.Update(oCert, exs)
        Return retval
    End Function

    Shared Function Delete(oCert As DTOCert, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CertLoader.Delete(oCert, exs)
        Return retval
    End Function
#End Region

    Shared Function X509Certificate2(oContact As DTOContact) As X509Certificate2
        Dim oCert As DTOCert = FromContact(oContact)
        Dim retval As X509Certificate2 = Nothing
        If oCert IsNot Nothing Then
            retval = New X509Certificate2(oCert.Stream, oCert.Pwd, X509KeyStorageFlags.PersistKeySet And X509KeyStorageFlags.MachineKeySet)
        End If
        Return retval
    End Function

    Shared Function Sign(oCert As DTOCert, ByRef oPdf As C1PdfDocument, ByRef exs As List(Of Exception)) As Boolean
        Dim retVal As Boolean = False
        Dim oSignature As New C1.C1Pdf.PdfSignature

        Try
            Dim oX509Certificate As New X509Certificate2(oCert.Stream, oCert.Pwd, X509KeyStorageFlags.PersistKeySet And X509KeyStorageFlags.MachineKeySet)
            With oSignature
                .Certificate = oX509Certificate
                .Visibility = FieldVisibility.Hidden
                .Reason = "Certificar el origen y la integridad del documento para su validez a efectos fiscales"
            End With
            oPdf.AddField(oSignature, Nothing)
            retVal = True

        Catch ex3 As System.Security.Cryptography.CryptographicException
            exs.Add(New Exception("Error al signar document pdf a Cert.vb: " & ex3.Message))
        Catch ex2 As System.Security.SecurityException
            exs.Add(New Exception("Error al signar document pdf a Cert.vb: " & ex2.Message))
        Catch ex As Exception
            exs.Add(New Exception("Error al signar document pdf a Cert.vb: " & ex.Message))
        End Try

        Return retVal
    End Function

End Class

