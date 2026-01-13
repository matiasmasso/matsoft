Public Class LogoMmO
    Private mDoc As C1.C1Pdf.C1PdfDocument
    Protected mColor As Color = Color.FromArgb(255, 51, 51) 'FF3333
    Private mRectangle As RectangleF

    Public Sub New(ByVal oDoc As C1.C1Pdf.C1PdfDocument)
        mDoc = oDoc
    End Sub

    Public ReadOnly Property Rectangle() As RectangleF
        Get
            Return mRectangle
        End Get
    End Property

    Public Property Color() As Color
        Get
            Return mColor
        End Get
        Set(ByVal value As Color)
            mColor = value
        End Set
    End Property

    Public ReadOnly Property Brush() As SolidBrush
        Get
            Return New SolidBrush(mColor)
        End Get
    End Property

    Public Sub Draw(ByVal oCenter As Point, ByVal iSideLength As Integer, Optional ByVal BlDrawRaoSocial As Boolean = False)
        Dim iOverAllX As Integer = oCenter.X - iSideLength / 2
        Dim iOverAllY As Integer = oCenter.Y - iSideLength / 2
        Draw(iOverAllX, iOverAllY, iSideLength, BlDrawRaoSocial)
    End Sub

    Public Sub Draw(ByVal iOverAllX As Integer, ByVal iOverAllY As Integer, ByVal iSideLength As Integer, Optional ByVal BlDrawRaoSocial As Boolean = False)
        Dim iCharWidth As Integer = iSideLength * (16 / 60)
        Dim iCharHeight As Integer = iSideLength * (17 / 60)
        Dim iCharX As Integer
        Dim iBackGroundHeight As Integer = IIf(BlDrawRaoSocial, iSideLength * 52 / 60, iSideLength)
        Dim iCharY As Integer = iOverAllY + (iBackGroundHeight - iCharHeight) / 2

        'quadrat
        mRectangle = New RectangleF(iOverAllX, iOverAllY, iSideLength, iBackGroundHeight)
        mDoc.FillRectangle(New SolidBrush(mColor), mRectangle)

        'M
        iCharX = iOverAllX + (iSideLength * 7 / 60)
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
            For i As Integer = 40 To 720
                iStringWidth = mDoc.MeasureString(s, New Font("Helvetica", i / 10)).Width
                If iStringWidth > iSideLength Then
                    oFont = New Font("Helvetica", i / 10)
                    'oFont.verticalalignment = System.Drawing.ver
                    Dim sF As New StringFormat(StringFormatFlags.FitBlackBox)
                    sF.Alignment = StringAlignment.Near

                    Dim iHeight As Integer = oFont.Size
                    Dim oRc As New Rectangle(iOverAllX, iOverAllY + iSideLength - iHeight, 2 * iSideLength, iHeight)
                    mDoc.DrawString(s, oFont, Brushes.Black, oRc, sF)
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

        mDoc.FillPolygon(Brushes.White, oPoint)
    End Sub

    Protected Sub DrawChar_MAS(ByVal X As Integer, ByVal Y As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer)
        Dim iRectWidth As Integer = iWidth * (12 / 15)
        Dim iRectHeight As Integer = iHeight * (13 / 17)
        Dim iGruix As Integer = iWidth * 4 / 15
        Dim oCenterP As New Point(X + (iWidth / 2), Y + (iHeight / 2))
        Dim oRectVert As New Rectangle(oCenterP.X - iGruix / 2, oCenterP.Y - iRectHeight / 2, iGruix, iRectHeight)
        mDoc.FillRectangle(Brushes.White, oRectVert)
        Dim oRectHoriz As New Rectangle(oCenterP.X - iRectWidth / 2, oCenterP.Y - iGruix / 2, iRectWidth, iGruix)
        mDoc.FillRectangle(Brushes.White, oRectHoriz)
    End Sub

    Protected Sub DrawChar_O(ByVal X As Integer, ByVal Y As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer)
        Dim iInnerWidth As Integer = iWidth * (6 / 16)
        Dim iInnerX As Integer = (X + (iWidth - iInnerWidth) / 2)
        Dim iInnerHeight As Integer = iHeight / 2
        Dim iInnerY As Integer = (Y + (iHeight - iInnerHeight) / 2)

        mDoc.FillEllipse(Brushes.White, X, Y, iWidth, iHeight)
        mDoc.FillEllipse(New SolidBrush(mColor), iInnerX, iInnerY, iInnerWidth, iInnerHeight)
    End Sub

End Class


