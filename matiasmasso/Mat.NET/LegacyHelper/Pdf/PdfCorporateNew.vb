Public Class PdfCorporateNew
    Inherits C1PdfHelper.Document

    Private mLeftMargin As Integer = 100
    Private mRightMargin As Integer = 70
    Private mTopMargin As Integer = 20
    Private mBottomMargin As Integer = 50

    Private mFont As New Font("Arial", 10, FontStyle.Regular)
    Private mDisplayDadesRegistrals As Boolean = True
    Private mPageCount As Integer = 1

    Public X As Integer
    Public Y As Integer


    Public Sub New()
        MyBase.new()
        X = mLeftMargin
        Y = mTopMargin
    End Sub

    Public Property LeftMargin() As Integer
        Get
            Return mLeftMargin
        End Get
        Set(ByVal value As Integer)
            mLeftMargin = value
        End Set
    End Property

    Public Property TopMargin() As Integer
        Get
            Return mTopMargin
        End Get
        Set(ByVal value As Integer)
            mTopMargin = value
        End Set
    End Property

    Public Property RightMargin() As Integer
        Get
            Return mRightMargin
        End Get
        Set(ByVal value As Integer)
            mRightMargin = value
        End Set
    End Property

    Public Property BottomMargin() As Integer
        Get
            Return mBottomMargin
        End Get
        Set(ByVal value As Integer)
            mBottomMargin = value
        End Set
    End Property

    Public Function Width() As Integer
        Dim iWidth As Integer = MyBase.PageRectangle.Width - mLeftMargin - mRightMargin
        Return iWidth
    End Function

    Public Function Bottom() As Integer
        Dim iBottom As Integer = MyBase.PageRectangle.Bottom - mBottomMargin
        Return iBottom
    End Function

    Public Property Font() As Font
        Get
            Return mFont
        End Get
        Set(ByVal value As Font)
            mFont = value
        End Set
    End Property

    Public ReadOnly Property PageCount() As Integer
        Get
            Return mPageCount
        End Get
    End Property

    Public WriteOnly Property displayDadesRegistrals() As Boolean
        Set(ByVal value As Boolean)
            mDisplayDadesRegistrals = value
        End Set
    End Property

    Public Function toByteArray(Optional ByVal BlSigned As Boolean = False) As Byte()
        Dim oMemStream As New IO.MemoryStream
        MyBase.Save(oMemStream)
        Dim oRetVal As Byte() = oMemStream.ToArray
        Return oRetVal
    End Function

    Public Overridable Sub onNewPage()
        MyBase.NewPage()
        mPageCount += 1
        Y = mTopMargin
    End Sub

    Public Function NewRowTemplate(Optional ByVal oStyle As PdfRowCell.Styles = PdfRowCell.Styles.standard, Optional ByVal iLeftMargin As Integer = 0, Optional ByVal iWidth As Integer = 0) As PdfRow
        If iLeftMargin = 0 Then iLeftMargin = mLeftMargin
        If iWidth = 0 Then iWidth = Me.Width + mLeftMargin - iLeftMargin
        Dim oRow As New PdfRow(Me, oStyle, iLeftMargin, iWidth)
        Return oRow
    End Function

    Public Sub WriteRow(ByVal oRowTemplate As PdfRow, ByVal ParamArray oValues() As Object)
        If LinesLeftToEndPage(oRowTemplate) = 0 Then '   If (Y + oRowTemplate.Height) > Me.bottom Then
            onNewPage()
        End If
        oRowTemplate.Write(Y, oValues)
        Y += oRowTemplate.Height
    End Sub

    Public Function LinesLeftToEndPage(ByVal oRowTemplate As PdfRow) As Integer
        Dim iGap As Integer = Me.Bottom - Y
        Dim iLines As Integer = iGap / oRowTemplate.Height
        Return iLines
    End Function

    Public Sub WriteBlankRow(ByVal oRowTemplate As PdfRow)
        Y += oRowTemplate.Height
    End Sub

    Public Sub WritePageFooter(ByVal s As String)
        Dim oFont As New Font("Arial", 8, FontStyle.Regular)
        Dim oBrush As Brush = Brushes.Gray
        Dim iStringWidth As Integer = MyBase.MeasureString(s, oFont).Width
        Dim oRc As RectangleF = MyBase.PageRectangle
        oRc.Y = oRc.Height - oFont.Height
        oRc.Height = oFont.Height
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center
        MyBase.DrawString(s, oFont, oBrush, oRc, sF)
    End Sub

    Protected Sub DrawDadesRegistrals()
        Dim oFont As New Font("Arial", 8, FontStyle.Regular)
        Dim oBrush As Brush = Brushes.Gray
        Dim s As String = "Reg.Mercantil de Barcelona, Tomo 6403, Libro 5689, Sección 2ª, Folio 167, Hoja 76326, Inscripción 1ª, NIF ES-A58007857"
        Dim iStringWidth As Integer = MyBase.MeasureString(s, oFont).Width
        Dim oRc As RectangleF = MyBase.PageRectangle
        oRc.Width = 10
        oRc.X = 10
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center
        sF.FormatFlags = StringFormatFlags.DirectionVertical
        MyBase.DrawString(s, oFont, oBrush, oRc, sF)
    End Sub
End Class


