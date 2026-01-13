Imports C1.C1Pdf
Public Class _PdfBase
    Property Pdf As C1PdfDocument
    Property Landscape As Boolean
    Property Font As Font
    Property Brush As Brush
    Property Pen As Pen
    Property X As Integer
    Property Y As Integer
    Property marginTop As Integer
    Property marginLeft As Integer
    Property marginRight As Integer
    Property marginBottom As Integer

    Property exs As New List(Of Exception)

    Protected PxToMm As Decimal = 25.4 / 100
    Protected MmToPx As Decimal = 1 / PxToMm



    Public Sub New(Optional paperKind As Printing.PaperKind = Printing.PaperKind.A4, Optional landscape As Boolean = False)
        _Pdf = New C1PdfDocument(paperKind, landscape)
        _Font = New Font("Helvetica", 8, FontStyle.Regular)
        _Brush = New SolidBrush(Color.Black)
        _Pen = New Pen(_Brush, 0.5)
    End Sub

    Public Sub SetMargins(Optional top As Integer = 0, Optional left As Integer = 0, Optional right As Integer = 0, Optional bottom As Integer = 0)
        Me.marginTop = top
        Me.marginLeft = left
        Me.marginRight = right
        Me.marginBottom = bottom
    End Sub

    Public Function Top() As Integer
        Return Me.Pdf.PageRectangle.Top + Me.marginTop
    End Function

    Public Function Right() As Integer
        Dim retval = Me.Pdf.PageRectangle.Right - Me.marginRight
        Return retval
    End Function

    Public Function Bottom() As Integer
        Return Me.Pdf.PageRectangle.Bottom - Me.Bottom
    End Function

    Public Function Left() As Integer
        Dim retval = Me.Pdf.PageRectangle.Left + Me.marginLeft
        Return retval
    End Function

    Public Sub NewPage()
        Me.Pdf.NewPage()
    End Sub

    Public Function Stream() As Byte()
        Dim oMemoryStream As New IO.MemoryStream
        _Pdf.Save(oMemoryStream)

        Dim retval As Byte() = oMemoryStream.ToArray
        Return retval
    End Function

    Public Sub DrawStringLine(sText As String)
        DrawString(sText)
        Y += _Font.Height
    End Sub

    Public Sub DrawString(sText As String, Optional font As Font = Nothing, Optional brush As Brush = Nothing, Optional rc As RectangleF = Nothing, Optional sF As StringFormat = Nothing)
        If font Is Nothing Then font = _Font
        If brush Is Nothing Then brush = _Brush
        If sF Is Nothing Then sF = New StringFormat
        If sText > "" Then
            Dim pt As PointF
            If rc = Nothing Then
                pt = New PointF(_marginLeft + X, _marginTop + Y)
            Else
                pt = New PointF(rc.Left, rc.Top)
            End If
            _Pdf.DrawString(sText, font, brush, pt, sF)
        End If
    End Sub

    Public Sub DrawCenteredString(sText As String)
        If sText > "" Then
            Dim iWidth As Integer = Me.Pdf.MeasureString(sText, _Font).Width
            Dim pt As New PointF(_marginLeft + X - iWidth / 2, _marginTop + Y)
            _Pdf.DrawString(sText, _Font, _Brush, pt)
        End If
    End Sub

    Public Sub DrawImage(oImage As Image, rc As RectangleF)
        Me.Pdf.DrawImage(oImage, rc)
    End Sub

    Public Sub DrawRectangle(iX As Integer, iY As Integer, Optional iWidth As Single = -1, Optional iHeight As Single = -1)
        If iWidth = -1 Then iWidth = Me.Pdf.PageRectangle.Width - iX - Me.marginLeft - Me.marginRight
        If iHeight = -1 Then iHeight = Me.Pdf.PageRectangle.Height - iY - Me.marginTop - Me.marginBottom
        Me.Pdf.DrawRectangle(_Pen, _marginLeft + iX, _marginTop + iY, iWidth, iHeight)
    End Sub

    Public Sub DrawPageRectangle()
        Dim rc As RectangleF = _Pdf.PageRectangle
        'rc.Inflate(-_marginLeft - _marginRight, -_marginTop - _marginBottom)
        rc.Inflate(-_marginLeft, -_marginTop)
        Me.Pdf.DrawRectangle(_Pen, rc)
    End Sub


    Public Function MarginsRectangle() As Rectangle
        Dim retval As New Rectangle(Me.marginLeft, Me.marginTop, Me.Pdf.PageRectangle.Width - Me.marginLeft - Me.marginRight, Me.Pdf.PageRectangle.Height - Me.marginTop - Me.marginBottom)
        Return retval
    End Function

    Public Function MeasureStringWidth(s) As Integer
        Dim retval As Integer = _Pdf.MeasureString(s, _Font).Width
        Return retval
    End Function

    Public Function GetAdjustedFont(ByVal GraphicString As String, ByVal ContainerWidth As Integer, ByVal MaxFontSize As Integer, ByVal MinFontSize As Integer, Optional ByVal SmallestOnFail As Boolean = False) As Font
        Dim testFont As Font = Nothing
        For AdjustedSize As Integer = MaxFontSize To MinFontSize Step -1
            testFont = New Font(_Font.Name, AdjustedSize, _Font.Style)
            Dim AdjustedSizeNew As SizeF = _Pdf.MeasureString(GraphicString, testFont)
            If ContainerWidth > Convert.ToInt32(AdjustedSizeNew.Width) Then
                Return testFont
            End If
        Next

        If SmallestOnFail Then
            Return testFont
        Else
            Return _Font
        End If
    End Function
End Class
