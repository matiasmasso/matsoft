Public Class PdfTemplateMembrete
    Inherits _BaseC1PdfDocument


    Protected mColor As Color = Color.FromArgb(255, 51, 51) 'FF3333
    Protected mLogoBrush As New SolidBrush(mColor)
    Protected mFontTemplate As New Font("Arial", 8, FontStyle.Regular)
    Protected mTextBrush As Brush = Brushes.Gray

    Public Sub New(oLang As DTOLang, Optional sFilename As String = "")
        MyBase.New(sFilename)
        MyBase.Lang = oLang
        DrawPage()
    End Sub

    Public Sub DrawPage()
        Dim iPageWidth As Integer = MyBase.PageRectangle.Width
        Dim iLogoWidth As Integer = 70
        DrawLogo((iPageWidth - iLogoWidth) / 2, 10, iLogoWidth, iLogoWidth)
        DrawAddress()
        DrawDadesRegistrals()
    End Sub

    Public Sub DrawPageNoLogo()
        Dim iPageWidth As Integer = MyBase.PageRectangle.Width
        Dim iLogoWidth As Integer = 70
        'DrawLogo((iPageWidth - iLogoWidth) / 2, 10, iLogoWidth, iLogoWidth)
        DrawAddress()
        DrawDadesRegistrals()
    End Sub

    Protected Sub DrawAddress()
        Dim oRc As New Rectangle(0, MyBase.PageRectangle.Height - 20, MyBase.PageRectangle.Width, 12)
        Dim sTxt As String = "MATIAS MASSO, S.A. - Diagonal 403 - 08008 Barcelona - tel.: 932.541.522 - fax: 932.541.521 - www.matiasmasso.es"
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center
        MyBase.DrawString(sTxt, mFontTemplate, mTextBrush, oRc, sF)
    End Sub

    Protected Sub DrawDadesRegistrals()
        Dim s As String = "Reg.Mercantil de Barcelona, Tomo 6403, Libro 5689, Sección 2ª, Folio 167, Hoja 76326, Inscripción 1ª, NIF ES-A58007857"
        Dim iStringWidth As Integer = MyBase.MeasureString(s, mFontTemplate).Width
        Dim oRc As RectangleF = MyBase.PageRectangle
        oRc.Width = 10
        oRc.X = 10
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center
        sF.FormatFlags = StringFormatFlags.DirectionVertical
        MyBase.DrawString(s, mFontTemplate, mTextBrush, oRc, sF)
    End Sub

    Protected Sub DrawLogo(ByVal iOverAllX As Integer, ByVal iOverAllY As Integer, ByVal iOverAllWidth As Integer, ByVal iOverAllHeight As Integer, Optional ByVal BlDrawRaoSocial As Boolean = False)
        Dim iCharWidth As Integer = iOverAllWidth * (16 / 60)
        Dim iCharHeight As Integer = iOverAllWidth * (17 / 60)
        Dim iCharX As Integer
        Dim iBackGroundHeight As Integer = IIf(BlDrawRaoSocial, iOverAllHeight * 52 / 60, iOverAllHeight)
        Dim iCharY As Integer = iOverAllY + (iBackGroundHeight - iCharHeight) / 2

        'quadrat
        MyBase.FillRectangle(mLogoBrush, iOverAllX, iOverAllY, iOverAllWidth, iBackGroundHeight)

        'M
        iCharX = iOverAllX + (iOverAllWidth * 7 / 60)
        DrawChar_M(iCharX, iCharY, iCharWidth, iCharHeight)

        '+
        iCharX += +iCharWidth 'iOverAllX + (iOverAllWidth - iCharWidth) / 2 + 2
        DrawChar_MAS(iCharX, iCharY, iCharWidth, iCharHeight)

        'O
        iCharX += +iCharWidth ' iOverAllX + (iOverAllWidth * 38 / 60)
        DrawChar_O(iCharX, iCharY, iCharWidth, iCharHeight)

        If BlDrawRaoSocial Then
            Dim iStringWidth As Integer

            Dim s As String = "MATIAS MASSO, S.A."
            Dim oFont As Font
            For i As Integer = 6 To 72
                iStringWidth = MyBase.MeasureString(s, New Font("Helvetica", i)).Width
                If iStringWidth > iOverAllWidth Then
                    oFont = New Font("Helvetica", i - 1)
                    Dim sF As New StringFormat()
                    sF.Alignment = StringAlignment.Center
                    Dim oRc As New Rectangle(iOverAllX, iOverAllY + iOverAllHeight - oFont.Height, iOverAllWidth, oFont.Height)
                    MyBase.DrawString(s, oFont, Brushes.Black, oRc, sF)
                    Exit Sub
                End If
            Next

        End If

    End Sub

    Protected Sub DrawChar_M(ByVal X As Integer, ByVal Y As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer)
        Dim Y0 As Integer = Y + 1
        Dim Y1 As Integer = Y + iHeight * 1 / 2
        Dim Y2 As Integer = Y + iHeight * 2 / 2 - 1
        Dim X0 As Integer = X
        Dim X1 As Integer = X + iWidth * 1 / 4
        Dim X2 As Integer = X + iWidth * 2 / 4
        Dim X3 As Integer = X + iWidth * 3 / 4
        Dim X4 As Integer = X + iWidth * 4 / 4

        Dim oPoint(12) As PointF
        oPoint(0) = New PointF(X0, Y0)
        oPoint(1) = New PointF(X1, Y0)
        oPoint(2) = New PointF(X2, Y1 + 1)
        oPoint(3) = New PointF(X3, Y0)
        oPoint(4) = New PointF(X4, Y0)
        oPoint(5) = New PointF(X4, Y2)
        oPoint(6) = New PointF(X3, Y2)
        oPoint(7) = New PointF(X3, Y1)
        oPoint(8) = New PointF(X2, Y2 + 1)
        oPoint(9) = New PointF(X1, Y1)
        oPoint(10) = New PointF(X1, Y2)
        oPoint(11) = New PointF(X0, Y2)
        oPoint(12) = oPoint(0)

        MyBase.FillPolygon(Brushes.White, oPoint)
    End Sub

    Protected Sub DrawChar_MAS(ByVal X As Integer, ByVal Y As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer)
        Dim iRectWidth As Integer = iWidth * (12 / 15)
        Dim iRectHeight As Integer = iHeight * (13 / 17)
        Dim iGruix As Integer = iWidth * 4 / 15
        Dim oCenterP As New Point(X + (iWidth / 2), Y + (iHeight / 2))
        'Dim iCompensa As Integer = iWidth / 20
        Dim oRectVert As New Rectangle(oCenterP.X - iGruix / 2, oCenterP.Y - iRectHeight / 2, iGruix, iRectHeight)
        MyBase.FillRectangle(Brushes.White, oRectVert)
        Dim oRectHoriz As New Rectangle(oCenterP.X - iRectWidth / 2, oCenterP.Y - iGruix / 2, iRectWidth, iGruix)
        MyBase.FillRectangle(Brushes.White, oRectHoriz)
    End Sub

    Protected Sub DrawChar_O(ByVal X As Integer, ByVal Y As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer)
        Dim iInnerWidth As Integer = iWidth * (6 / 16)
        Dim iInnerX As Integer = (X + (iWidth - iInnerWidth) / 2)
        Dim iInnerHeight As Integer = iHeight / 2
        Dim iInnerY As Integer = (Y + (iHeight - iInnerHeight) / 2)

        MyBase.FillEllipse(Brushes.White, X, Y, iWidth, iHeight)
        MyBase.FillEllipse(mLogoBrush, iInnerX, iInnerY, iInnerWidth, iInnerHeight)
    End Sub

End Class


