Public Class PdfEtq
    Private mPdf As C1.C1Pdf.C1PdfDocument
    Private mCols As Integer = 3
    Private mRows As Integer = 7
    Private mIdx As Integer
    Private mDs As DataSet
    Private mLeftMargin As Integer
    Private mTopMargin As Integer
    Private mEtqWidth As Integer
    Private mEtqHeight As Integer
    Private mBottomMargin As Integer
    Private mRightMargin As Integer
    Private mFont As New Font("Helvetica", 10, FontStyle.Regular)
    Private mBrush As SolidBrush = Brushes.Black
    Private mFirstEtq As Integer

    Public Sub New(ByVal oDs As DataSet, Optional ByVal iFirstEtq As Integer = 1)
        MyBase.new()
        mDs = oDs
        mFirstEtq = iFirstEtq
    End Sub

    Private Function Pdf() As C1.C1Pdf.C1PdfDocument
        SetDefaults()
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            If mIdx >= NumEtqsPerPagina() Then
                mIdx = 0
                mPdf.NewPage()
            End If
            DrawEtq(oRow)
            mIdx += 1
        Next
        Return mPdf
    End Function

    Public Function Stream() As Byte()
        Dim oPdfStream As New IO.MemoryStream

        Me.Pdf.Save(oPdfStream)
        Dim oBuffer As Byte() = oPdfStream.ToArray
        Return oBuffer
    End Function

    Private Sub SetDefaults()
        mPdf = New C1.C1Pdf.C1PdfDocument
        Dim rc As RectangleF = mPdf.PageRectangle
        mIdx = mFirstEtq - 1
        mTopMargin = 16
        mBottomMargin = rc.Bottom ' - 10
        mLeftMargin = 20
        mRightMargin = rc.Right '- 10
        mEtqWidth = 200 '(mRightMargin - mLeftMargin) / mCols
        mEtqHeight = 116 '(mBottomMargin - mTopMargin) / mRows
    End Sub

    Private Sub DrawEtq(ByVal oRow As DataRow)
        Dim X As Integer
        Dim Y As Integer
        Dim rc As RectangleF
        SetCoordenadas(mIdx, X, Y)
        If Not IsDBNull(oRow("ADR1")) Then
            rc = New RectangleF(X, Y, mEtqWidth, mEtqHeight)
            mPdf.DrawString(oRow("ADR1"), mFont, mBrush, rc)
            Y += mFont.Height
        End If
        If Not IsDBNull(oRow("ADR2")) Then
            rc = New RectangleF(X, Y, mEtqWidth, mEtqHeight)
            mPdf.DrawString(oRow("ADR2"), mFont, mBrush, rc)
            Y += mFont.Height
        End If
        If Not IsDBNull(oRow("ADR3")) Then
            rc = New RectangleF(X, Y, mEtqWidth, mEtqHeight)
            mPdf.DrawString(oRow("ADR3"), mFont, mBrush, rc)
            Y += mFont.Height
        End If
        If Not IsDBNull(oRow("ADR4")) Then
            rc = New RectangleF(X, Y, mEtqWidth, mEtqHeight)
            mPdf.DrawString(oRow("ADR4"), mFont, mBrush, rc)
        End If
    End Sub

    Private Sub SetCoordenadas(ByVal iIdx As Integer, ByRef iX As Integer, ByRef iY As Integer)
        Dim iCol As Integer = mIdx Mod mCols
        Dim iRow As Integer = Int(mIdx / mCols)
        'If iRow = 0 Then Stop
        iX = mLeftMargin + iCol * mEtqWidth
        iY = mTopMargin + iRow * mEtqHeight
    End Sub

    Public Function Fits(ByVal sTxt As String) As Boolean
        Dim retval As Boolean
        If mEtqWidth = 0 Then SetDefaults()
        Dim iHeight As Integer = mPdf.MeasureString(sTxt, mFont, mEtqWidth).Height
        If iHeight <= mFont.Height Then retval = True
        Return retval
    End Function

    Public Function NumEtqsPerPagina() As Integer
        Return mCols * mRows
    End Function
End Class
