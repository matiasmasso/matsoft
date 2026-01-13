Public Class Logo
    Private mOverAllWidth As Integer
    Private mOverAllHeight As Integer
    Private mBackGroundStyle As BackGroundStyles

    Public Enum BackGroundStyles
        red
        ratlles
    End Enum

    Public Sub New(ByVal iSideLength As Integer)
        MyBase.New()
        mOverAllWidth = iSideLength
        mOverAllHeight = iSideLength
    End Sub

    Public ReadOnly Property width() As Integer
        Get
            Return mOverAllWidth
        End Get
    End Property

    Public Property BackGroundStyle() As BackGroundStyles
        Get
            Return mBackGroundStyle
        End Get
        Set(ByVal value As BackGroundStyles)
            mBackGroundStyle = value
        End Set
    End Property

    Public Sub DrawMe(ByVal oGraphics As Graphics, ByVal iOverAllX As Integer, ByVal iOverAllY As Integer, Optional ByVal BlDrawRaoSocial As Boolean = False)
        'exemple:
        'Dim hwnd As IntPtr = Me.Handle
        'Dim oGraphics As Graphics = Graphics.FromHwnd(hwnd)
        'Dim oLogo As New Logo(250)
        'oLogo.DrawMe(oGraphics, 30, 70)
        'oGraphics.Dispose()

        Dim oBackGroundBrush As Brush
        Select Case mBackGroundStyle
            Case BackGroundStyles.ratlles
                oBackGroundBrush = New SolidBrush(Color.FromArgb(224, 224, 224))
            Case Else
                oBackGroundBrush = Brushes.Red
        End Select

        Dim iCharWidth As Integer = mOverAllWidth * (16 / 60)
        Dim iCharHeight As Integer = mOverAllWidth * (17 / 60)
        Dim iCharX As Integer
        Dim iBackGroundHeight As Integer = IIf(BlDrawRaoSocial, mOverAllHeight * 52 / 60, mOverAllHeight)
        Dim iCharY As Integer = iOverAllY + (iBackGroundHeight - iCharHeight) / 2

        Dim oPenPath As New Pen(Color.DarkGray, 1)

        Dim oPathBackGround As Drawing2D.GraphicsPath = BackgroundPath(iOverAllX, iOverAllY, mOverAllWidth, iBackGroundHeight)
        oGraphics.FillPath(oBackGroundBrush, oPathBackGround)
        oGraphics.DrawPath(oPenPath, oPathBackGround)

        Dim oPathForeGround As New Drawing2D.GraphicsPath()
        iCharX = iOverAllX + (mOverAllWidth * 7 / 60)
        oPathForeGround.AddPath(Char_M_Path(iCharX, iCharY, iCharWidth, iCharHeight), True)
        oGraphics.DrawPath(oPenPath, (Char_M_Path(iCharX, iCharY, iCharWidth, iCharHeight)))

        iCharX += +iCharWidth
        oPathForeGround.AddPath(Char_MAS_Path(iCharX, iCharY, iCharWidth, iCharHeight), True)
        oGraphics.DrawPath(oPenPath, (Char_MAS_Path(iCharX, iCharY, iCharWidth, iCharHeight)))

        iCharX += +iCharWidth
        oPathForeGround.AddPath(Char_O_Path(iCharX, iCharY, iCharWidth, iCharHeight), True)
        oGraphics.DrawPath(oPenPath, (Char_O_Path(iCharX, iCharY, iCharWidth, iCharHeight)))

        oGraphics.FillPath(Brushes.White, oPathForeGround)


        Exit Sub

        If BlDrawRaoSocial Then

        End If

    End Sub

    Private Function BackgroundPath(ByVal X As Integer, ByVal Y As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer) As Drawing2D.GraphicsPath
        Dim oPath As New Drawing2D.GraphicsPath
        Dim oRectangle As New Rectangle(X, Y, iWidth, iHeight)
        oPath.AddRectangle(oRectangle)
        Return oPath
    End Function

    Private Function Char_M_Path(ByVal X As Integer, ByVal Y As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer) As Drawing2D.GraphicsPath
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

        Dim oPath As New Drawing2D.GraphicsPath
        For i As Integer = 1 To oPoint.Length - 1
            oPath.AddLine(oPoint(i - 1), oPoint(i))
        Next

        Return oPath
    End Function


    Private Function Char_MAS_Path(ByVal X As Integer, ByVal Y As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer)
        Dim iCreuWidth As Integer = iWidth * (12 / 15)
        Dim iCreuHeight As Integer = iHeight * (13 / 17)
        Dim iCreuGruix As Integer = iWidth * 4 / 15
        Dim oCenterP As New Point(X + (iWidth / 2), Y + (iHeight / 2))

        Dim Y0 As Integer = Y + (iHeight - iCreuHeight) / 2
        Dim Y1 As Integer = Y + (iHeight - iCreuGruix) / 2
        Dim Y2 As Integer = Y1 + iCreuGruix
        Dim Y3 As Integer = Y + (iHeight + iCreuHeight) / 2
        Dim X0 As Integer = X + (iWidth - iCreuWidth) / 2
        Dim X1 As Integer = X + (iWidth - iCreuGruix) / 2
        Dim X2 As Integer = X1 + iCreuGruix
        Dim X3 As Integer = X + (iWidth + iCreuWidth) / 2

        Dim oPoint(12) As PointF
        oPoint(0) = New PointF(X1, Y0)
        oPoint(1) = New PointF(X2, Y0)
        oPoint(2) = New PointF(X2, Y1)
        oPoint(3) = New PointF(X3, Y1)
        oPoint(4) = New PointF(X3, Y2)
        oPoint(5) = New PointF(X2, Y2)
        oPoint(6) = New PointF(X2, Y3)
        oPoint(7) = New PointF(X1, Y3)
        oPoint(8) = New PointF(X1, Y2)
        oPoint(9) = New PointF(X0, Y2)
        oPoint(10) = New PointF(X0, Y1)
        oPoint(11) = New PointF(X1, Y1)
        oPoint(12) = oPoint(0)

        Dim oPath As New Drawing2D.GraphicsPath
        For i As Integer = 1 To oPoint.Length - 1
            oPath.AddLine(oPoint(i - 1), oPoint(i))
        Next



        Return oPath
    End Function


    Private Function Char_O_Path(ByVal X As Integer, ByVal Y As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer) As Drawing2D.GraphicsPath
        Dim iInnerWidth As Integer = iWidth * (6 / 16)
        Dim iInnerX As Integer = (X + (iWidth - iInnerWidth) / 2)
        Dim iInnerHeight As Integer = iHeight / 2
        Dim iInnerY As Integer = (Y + (iHeight - iInnerHeight) / 2)
        Dim oRect As Rectangle = Nothing

        Dim oPath As New Drawing2D.GraphicsPath

        oRect = New Rectangle(X, Y, iWidth, iHeight)
        oPath.AddEllipse(oRect)

        oRect = New Rectangle(iInnerX, iInnerY, iInnerWidth, iInnerHeight)
        oPath.AddEllipse(oRect)

        Return oPath
    End Function


    Private Function NomPath(ByVal X As Integer, ByVal Y As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer) As Drawing2D.GraphicsPath
        Dim oPath As New Drawing2D.GraphicsPath
        Dim iStringWidth As Integer

        Dim s As String = "MATIAS MASSO, S.A."
        Dim oFont As Font
        For i As Integer = 6 To 72
            'iStringWidth = MyBase.MeasureString(s, New Font("Helvetica", i)).Width
            If iStringWidth > iWidth Then
                oFont = New Font("Helvetica", i - 1)
                Dim sF As New StringFormat()
                sF.Alignment = StringAlignment.Center
                Dim oRc As New Rectangle(X, Y + iHeight - oFont.Height, iWidth, oFont.Height)
                'MyBase.DrawString(s, oFont, Brushes.Black, oRc, sF)
                Exit For
            End If
        Next
        Return oPath
    End Function
End Class
