Imports C1.C1Pdf

Public Class PdfSpvInLabel
    Dim _Spv As DTOSpv

    Dim mPdf As C1PdfDocument
    Dim mPageRectangle As Rectangle

    Private mFontEpg As New Font("Calibri", 12, FontStyle.Bold)
    Private mFontDest As New Font("Calibri", 26, FontStyle.Bold)
    Private mFontRte As New Font("Calibri", 9, FontStyle.Bold)

    Private X As Integer
    Private Y As Integer
    Private XF As Integer

    Private Enum TextFormats
        epigraf
        Desti
        Remite
    End Enum


    Public Sub New(ByVal oSpv As DTOSpv)
        MyBase.New()
        _Spv = oSpv
    End Sub

    Public Function PdfStream(exs As List(Of Exception)) As Byte()
        Dim retval As Byte() = Nothing
        If mPdf Is Nothing Then MakePdf(exs)

        Dim oMemoryStream As New IO.MemoryStream
        mPdf.Save(oMemoryStream)

        retval = oMemoryStream.ToArray
        Return retval
    End Function


    Private Function MakePdf(exs As List(Of Exception)) As Boolean
        mPdf = New C1PdfDocument
        mPageRectangle = New Rectangle(mPdf.PageRectangle.X + 50, mPdf.PageRectangle.Y + 50, mPdf.PageRectangle.Width - 70, mPdf.PageRectangle.Height - 70)

        Dim oCustomer As DTOCustomer = _Spv.Customer
        Dim sText As String = ""
        Dim oSizeF As System.Drawing.SizeF = Nothing

        Dim oPen As New Pen(Color.Black, 1)
        Dim oBrush As Brush = Brushes.AliceBlue

        Dim X0 As Integer = mPageRectangle.Left
        Dim Y0 As Integer = mPageRectangle.Top
        XF = mPageRectangle.Right - 40
        Dim YF As Integer = mPageRectangle.Top + (mPageRectangle.Bottom - mPageRectangle.Top) / 2 - 50

        mPdf.DrawRectangle(oPen, X0, Y0, XF - X0, YF - Y0)


        Dim Y1 As Integer = Y0  'primer titol
        Dim Y2 As Integer = Y0 + 14 'ratlla sota primer titol
        Dim Y3 As Integer = Y2 + 30 'destino
        Dim Y4 As Integer = YF - 70 'ratlla sobre segon titol
        Dim Y5 As Integer = Y4  'segon titol
        Dim Y6 As Integer = Y4 + 14 'ratlla sota segon titol
        Dim Y7 As Integer = Y6 + 0 'remite


        Dim X1 As Integer = X0 + 20
        Dim X2 As Integer = X1 + 162
        Dim X3 As Integer = XF - 70

        mPdf.DrawLine(oPen, X0, Y2, XF, Y2) 'sota primer titol
        mPdf.DrawLine(oPen, X0, Y4, XF, Y4) 'sobre segon titol
        mPdf.DrawLine(oPen, X0, Y6, XF, Y6) 'sota segon titol
        mPdf.DrawLine(oPen, X3, Y4, X3, YF) 'vertical dreta
        mPdf.DrawLine(oPen, X0, YF + 50, XF, YF + 50) 'retallar

        mPdf.FillRectangle(oBrush, X0 + 1, Y0 + 1, XF - X0 - 2, Y2 - Y0 - 2)
        mPdf.FillRectangle(oBrush, X0 + 1, Y4 + 1, XF - X0 - 2, Y6 - Y4 - 2)

        X = X1
        Y = Y1
        Me.DrawText("destino:", TextFormats.epigraf)

        Y = Y3
        sText = "MATIAS MASSO, S.A."
        sText += vbCrLf & "Servicio de Asistencia Técnica"
        sText += vbCrLf & "Diagonal 403"
        sText += vbCrLf & "08008 Barcelona"
        oSizeF = mPdf.MeasureString(sText, mFontDest)
        X = XF - (XF - X0) / 2 - oSizeF.Width / 2
        Y = Y2 + (Y4 - Y2) / 2 - oSizeF.Height / 2
        DrawText(sText, TextFormats.Desti)

        X = X1
        Y = Y5
        Y = Y + Me.DrawText("remite:", TextFormats.epigraf)

        Y = Y5
        sText = "reparación:"
        oSizeF = mPdf.MeasureString(sText, mFontEpg)
        X = XF - (XF - X3) / 2 - oSizeF.Width / 2
        Y = Y + DrawText(sText, TextFormats.epigraf)

        If oCustomer.Logo IsNot Nothing Then
            Dim ms As New IO.MemoryStream(oCustomer.Logo)
            Dim oLogo As Image = Image.FromStream(ms)
            If oLogo IsNot Nothing Then
                X = X1
                Y = Y6 + (YF - Y6) / 2 - oLogo.Height / 2
                mPdf.DrawImage(oLogo, New RectangleF(X, Y, oLogo.Width, oLogo.Height))
            End If
        End If


        Dim sTel As String = _Spv.Tel
        sText = _Spv.Nom
        sText += vbCrLf & _Spv.Address.Text & " - " & DTOAddress.ZipyCit(_Spv.Address)
        If sTel > "" Then sText += vbCrLf & "tel.: " & sTel

        oSizeF = mPdf.MeasureString(sText, mFontRte)
        X = X2
        Y = YF - (YF - Y6) / 2 - oSizeF.Height / 2
        Me.DrawText(sText, TextFormats.Remite)

        sText = _Spv.Id
        oSizeF = mPdf.MeasureString(sText, mFontDest)
        X = XF - (XF - X3) / 2 - oSizeF.Width / 2
        Y = YF - (YF - Y6) / 2 - oSizeF.Height / 2
        Y = Y + DrawText(sText, TextFormats.Desti)
        Return exs.Count = 0
    End Function


    Private Function DrawText(ByVal sText As String, ByVal oFormat As TextFormats) As Integer
        Dim oFont As Font = Nothing
        Dim oBrush As Brush = Nothing

        Select Case oFormat
            Case TextFormats.epigraf
                oFont = mFontEpg
                oBrush = Brushes.Black
            Case TextFormats.Desti
                oFont = mFontDest
                oBrush = Brushes.Black
            Case TextFormats.Remite
                oFont = mFontRte
                oBrush = Brushes.Black
        End Select

        Dim oSizeF As System.Drawing.SizeF = mPdf.MeasureString(sText, oFont)
        'Dim oRc As New RectangleF(X, Y, oSizeF.Width, oSizeF.Height)
        Dim oRc As New RectangleF(X, Y, XF - X, oSizeF.Height)
        mPdf.DrawString(sText, oFont, oBrush, oRc)
        Return oSizeF.Height
    End Function

End Class
