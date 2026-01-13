Imports System.Security.Cryptography.X509Certificates
Imports C1.C1Pdf

Public Class C1PdfHelper


    Public Class Document
        Inherits C1.C1Pdf.C1PdfDocument

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(oPaperKind As System.Drawing.Printing.PaperKind)
            MyBase.New(oPaperKind)
        End Sub

        Public Function Stream(exs As List(Of Exception)) As Byte()
            Dim oMemoryStream As New IO.MemoryStream
            Try
                'pot petar per manca de Ghostscript instal.lat
                MyBase.Save(oMemoryStream)

            Catch ex As Exception
                exs.Add(New Exception("error en C1PdfHelper.Stream"))
                exs.Add(ex)
            End Try

            Return oMemoryStream.ToArray
        End Function

        Public Function SignedStream(exs As List(Of Exception), oCertificateStream As Byte(), sCertificatePassword As String) As Byte()
            If Not Sign(oCertificateStream, sCertificatePassword, exs) Then
                exs.Add(New Exception("Error de signatura pdf"))
            End If
            Return Stream(exs)
        End Function

        Public Function Sign(oCertificateStream As Byte(), sCertificatePassword As String, ByRef exs As List(Of Exception)) As Boolean
            Dim retVal As Boolean = False
            Dim oSignature As New C1.C1Pdf.PdfSignature

            Try
                Dim oX509Certificate As New X509Certificate2(oCertificateStream, sCertificatePassword, X509KeyStorageFlags.PersistKeySet And X509KeyStorageFlags.MachineKeySet)
                With oSignature
                    .Certificate = oX509Certificate
                    .Visibility = FieldVisibility.Hidden
                    .Reason = "Certificar el origen y la integridad del documento para su validez a efectos fiscales"
                End With

                MyBase.AddField(oSignature, Nothing)
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
End Class
