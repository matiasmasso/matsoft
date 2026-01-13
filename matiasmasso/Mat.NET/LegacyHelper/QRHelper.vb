Imports QRCoder
Public Class QRHelper
    Shared Function Factory(src As String, Optional pixelsPerModule As Integer = 20) As Image
        Dim qrGenerator As New QRCodeGenerator()
        Dim qrCodeData = qrGenerator.CreateQrCode(src, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrCodeData)
        Dim retval = qrCode.GetGraphic(pixelsPerModule)
        Return retval
    End Function
End Class
