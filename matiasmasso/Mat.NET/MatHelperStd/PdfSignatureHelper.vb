Imports System.IO
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.X509
Imports iText.Kernel.Geom
Imports iText.Kernel.Pdf
Imports iText.Signatures
Imports Org.BouncyCastle.Pkcs


Public Class PdfSignatureHelper

    Public Shared Function Sign(exs As List(Of Exception), srcFilename As String, destFilename As String, oCertStream As Stream, password As String, Optional rect As System.Drawing.Rectangle = Nothing, Optional imageUri As Uri = Nothing) As Boolean
        Dim oPkcs12Store As Pkcs12Store
        Dim reader As PdfReader = Nothing
        Try
            oPkcs12Store = New Pkcs12Store(oCertStream, password)
            Dim sPrivateKeyEntry As String = PrivateKeyEntry(oPkcs12Store)
            Dim oPrivateKey As ICipherParameters = oPkcs12Store.GetKey(sPrivateKeyEntry).Key
            Dim oChain = CertificatesChain(oPkcs12Store, sPrivateKeyEntry)

            reader = New PdfReader(srcFilename)

            Dim signer As PdfSigner = New PdfSigner(reader, New FileStream(destFilename, FileMode.CreateNew), New StampingProperties())
            Dim height = signer.GetDocument.GetFirstPage.GetCropBox.GetHeight
            Dim rectangle As New Rectangle(rect.X, rect.Y, rect.Width, rect.Height)
            Dim appearance As PdfSignatureAppearance = signer.GetSignatureAppearance()
            'appearance.SetReason("reason").SetLocation("location").SetReuseAppearance(False).SetPageRect(rectangle).SetPageNumber(1)
            appearance.SetLocation("Barcelona").SetReuseAppearance(False).SetPageRect(rectangle).SetPageNumber(1)
            If imageUri Is Nothing Then
                appearance.SetRenderingMode(PdfSignatureAppearance.RenderingMode.DESCRIPTION)
            Else
                Dim iTextImage = iText.IO.Image.ImageDataFactory.CreateJpeg(imageUri)
                appearance.SetSignatureGraphic(iTextImage)
                appearance.SetRenderingMode(PdfSignatureAppearance.RenderingMode.GRAPHIC)
            End If

            signer.SetFieldName("sig")
            Dim pks As IExternalSignature = New PrivateKeySignature(oPrivateKey, DigestAlgorithms.SHA256)
            signer.SignDetached(pks, oChain, Nothing, Nothing, Nothing, 0, PdfSigner.CryptoStandard.CADES)

        Catch ex As Exception
            exs.Add(ex)
        Finally
            If reader IsNot Nothing Then reader.Close()
        End Try
        Return exs.Count = 0
    End Function


    Private Shared Function PrivateKeyEntry(oPkcs12Store As Pkcs12Store) As String
        Dim retval As String = ""

        For Each a In oPkcs12Store.Aliases
            If oPkcs12Store.IsKeyEntry(CStr(a)) Then
                retval = CStr(a)
            End If
        Next
        Return retval
    End Function

    Private Shared Function CertificatesChain(oPkcs12Store As Pkcs12Store, privateKeyEntry As String) As X509Certificate()
        Dim ce As X509CertificateEntry() = oPkcs12Store.GetCertificateChain(privateKeyEntry)
        Dim retval As X509Certificate() = New X509Certificate(ce.Length - 1) {}

        For k As Integer = 0 To ce.Length - 1
            retval(k) = ce(k).Certificate
        Next
        Return retval
    End Function


End Class
