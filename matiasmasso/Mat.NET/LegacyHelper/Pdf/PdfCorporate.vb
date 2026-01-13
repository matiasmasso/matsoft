Imports C1.C1Pdf

Public Class PdfCorporate

    Private mPdf As C1PdfHelper.Document

    Protected mPen As Pen = Pens.Gray
    Protected mFont As New Font("Arial", 10, FontStyle.Regular)
    Protected mTableTitleFont As New Font("Arial", 10, FontStyle.Underline)
    Protected mLineHeight As Integer = 13
    Protected mLeft As Integer = 60
    Protected mRight As Integer
    Protected mBottom As Integer
    Protected Y As Integer
    Protected mTable As MatTable
    Private mDefaultTableLeft As Integer = 0
    Private mDefaultTableTop As Integer = 60
    Private mDefaultTableFont As Font = mFont
    Private mLang As DTOLang

    Public Event NewPage(ByVal iPage As Integer)

    Public Sub New(Optional ByVal oLang As DTOLang = Nothing)
        MyBase.New()
        mPdf = New PdfCorpTemplateDiagonal(PdfCorpTemplateDiagonal.Templates.logo)
        'mPdf = New C1PdfDocument(Printing.PaperKind.A4)
        mRight = mPdf.PageRectangle.Width - 20
        mBottom = mPdf.PageRectangle.Bottom - 20
        If oLang Is Nothing Then
            mLang = DTOApp.current.lang
        Else
            mLang = oLang
        End If
    End Sub

    Protected Function Tradueix(ByVal sEsp As String, ByVal sCat As String, ByVal sEng As String) As String
        Return mLang.Tradueix(sEsp, sCat, sEng)
    End Function

    Public ReadOnly Property Pdf() As C1PdfDocument
        Get
            Return mPdf
        End Get
    End Property

    Public Function Stream(Optional ByVal BlSigned As Boolean = False) As Byte()
        Dim oMemStream As New IO.MemoryStream
        mPdf.Save(oMemStream)
        Dim oBuffer As Byte() = oMemStream.ToArray
        Return oBuffer
    End Function

    Public Sub Save(ByVal sFileName As String)
        mPdf.Save(sFileName)
    End Sub

    Public Property Table() As MatTable
        Get
            If mTable Is Nothing Then
                mTable = New MatTable(mDefaultTableLeft, mDefaultTableTop, mDefaultTableFont, mPdf.PageRectangle.Height - mDefaultTableTop - 80)
            End If
            Return mTable
        End Get
        Set(ByVal value As MatTable)
            mTable = value
        End Set
    End Property

    Protected Sub DrawAdr(ByVal oAdr As ArrayList)
        Dim oRc As RectangleF
        Dim X As Integer = mLeft + 10
        Y = 120
        Dim s As String
        For Each s In oAdr
            oRc = New RectangleF(X, Y, mPdf.PageRectangle.Width, mFont.Height)
            mPdf.DrawString(s, mFont, Brushes.Black, oRc)
            Y += mFont.GetHeight
        Next
    End Sub


    Protected Sub DrawText(ByVal sText As String, ByVal iXinicial As Integer, ByVal iXfinal As Integer, Optional ByVal oAlign As StringAlignment = StringAlignment.Near, Optional ByVal oBackgroundColor As Integer = 0)
        Dim sF As New StringFormat()
        sF.Alignment = oAlign
        Dim oRc As New RectangleF(iXinicial, Y, iXfinal - iXinicial, mFont.Height)
        If oBackgroundColor <> 0 Then
            Dim oPen As New Pen(Color.FromArgb(oBackgroundColor))
            Dim oBrush As Brush = oPen.Brush
            mPdf.FillRectangle(oBrush, oRc)
        End If
        mPdf.DrawString(sText, mFont, Brushes.Black, oRc, sF)
    End Sub

    Public Sub DrawTableHeader(ByVal oTable As MatTable)
        Static BlDone As Boolean

        Dim oCell As MatCell
        If BlDone Then
            Y = 100
        Else
            Y = oTable.Top 'primera pagina
            BlDone = True
        End If

        If oTable.HeaderRow.Visible Then
            For Each oCell In oTable.HeaderRow.Cells
                DrawTableCell(oCell)
            Next
            Y += (oTable.HeaderRow.Height + oTable.CellSpacing)
        End If
    End Sub

    Public Sub DrawTable(Optional ByVal oFont As System.Drawing.Font = Nothing)
        Dim oRow As MatRow
        Dim oCell As MatCell
        If oFont IsNot Nothing Then mTable.Font = oFont

        Dim iCurrentPage As Integer = mTable.Rows(0).Page

        'RaiseEvent NewPage(iCurrentPage)
        DrawTableHeader(mTable)

        For Each oRow In mTable.Rows
            If Y >= mBottom - 20 Then
                RaiseEvent NewPage(iCurrentPage)
            End If
            If oRow.Page <> iCurrentPage Then
                mPdf.NewPage()
                iCurrentPage = oRow.Page
                RaiseEvent NewPage(iCurrentPage)
                DrawTableHeader(mTable)
            End If

            For Each oCell In oRow.Cells
                DrawTableCell(oCell)
            Next

            Y += (oRow.Height + mTable.CellSpacing)
        Next

        If mTable.FooterRow.Visible Then
            For Each oCell In mTable.FooterRow.Cells
                If oCell.Value IsNot Nothing Then
                    DrawTableCell(oCell)
                End If
            Next
        End If
    End Sub

    Public Sub DrawTableTitle(ByVal sText As String, Optional ByVal oFont As Font = Nothing)
        If oFont Is Nothing Then oFont = mTableTitleFont
        'Dim sF As New StringFormat
        'sF.Alignment = StringAlignment.Center
        Dim oRc As New Rectangle(mLeft, Me.Table.Top - 2 * oFont.GetHeight, mRight, oFont.Height)
        mPdf.DrawString(sText, oFont, Brushes.Gray, oRc)
    End Sub


    Private Sub DrawTableCell(ByVal oCell As MatCell)
        Dim oRc As New RectangleF(oCell.Column.Offset + mLeft, Y, oCell.Column.Width, oCell.Row.Height)
        Dim oPen As New Pen(oCell.ForeColor)
        Dim oBrush As Brush = oPen.Brush
        Dim sF As New StringFormat
        sF.Alignment = oCell.Column.Alignment
        Dim sText As String = ""
        Select Case oCell.Column.type
            Case MatColumn.Types.amt
                If TypeOf (oCell.Value) Is DTOAmt Then
                    sText = DirectCast(oCell.Value, DTOAmt).Formatted
                Else
                    If oCell.Value IsNot Nothing Then
                        sText = oCell.Value.ToString
                    End If
                End If
            Case MatColumn.Types.date
                If IsDate(oCell.Value) Then
                    sText = DirectCast(oCell.Value, Date).ToShortDateString
                Else
                    If oCell.Value IsNot Nothing Then
                        sText = oCell.Value.ToString
                    End If
                End If
            Case MatColumn.Types.percent
                If IsNumeric(oCell.Value) Then
                    sText = DirectCast(oCell.Value, Decimal).ToString & "%"
                Else
                    If oCell.Value IsNot Nothing Then
                        sText = oCell.Value.ToString
                    End If
                End If
            Case MatColumn.Types.integer
                If oCell.Value IsNot Nothing Then
                    sText = oCell.Value.ToString
                End If
            Case MatColumn.Types.string
                If oCell.Value IsNot Nothing Then
                    sText = oCell.Value.ToString
                End If
        End Select

        Dim oBackPen As New Pen(oCell.BackColor)
        'Dim oBackPen As New Pen(Color.Yellow)
        Dim oBackBrush As SolidBrush = oBackPen.Brush
        mPdf.FillRectangle(oBackBrush, oRc)

        mPdf.DrawString(sText, oCell.Font, Brushes.Black, oRc, sF)

    End Sub


End Class
