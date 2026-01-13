Public Class PdfRow
    Private mDoc As PdfCorporateNew = Nothing
    Private mLeft As Integer
    Private mTop As Integer
    Private mWidth As Integer
    Private mHeight As Integer
    Private mFont As System.Drawing.Font
    Private mBrush As Brush = Brushes.Black
    Private mBackgroundColor As Color = Color.White
    Private mCells As ArrayList
    Private mNoWidthCells As Boolean = False
    Private mStyle As PdfRowCell.Styles
    Private X, Y As Integer


    Public Sub New(ByVal oDoc As PdfCorporateNew, ByVal oStyle As PdfRowCell.Styles, ByVal iLeft As Integer, ByVal iWidth As Integer)
        MyBase.New()
        mDoc = oDoc
        SetStyle(oStyle)
        mLeft = iLeft
        mWidth = iWidth

        mCells = New ArrayList
    End Sub

    Private Sub SetStyle(ByVal oStyle As PdfRowCell.Styles)
        Select Case oStyle
            Case PdfRowCell.Styles.standard
                mFont = mDoc.Font
            Case PdfRowCell.Styles.smaller
                mFont = New Font(mDoc.Font.FontFamily, mDoc.Font.Size - 2)
            Case PdfRowCell.Styles.boldOnBlueBackground
                mBackgroundColor = Color.LightBlue
                mFont = New Font(mDoc.Font, FontStyle.Bold)
            Case PdfRowCell.Styles.docTitle
                mFont = New Font(mDoc.Font.FontFamily, 14, FontStyle.Bold)
            Case PdfRowCell.Styles.docSubtitle
                mFont = New Font(mDoc.Font.FontFamily, 14, FontStyle.Bold)
        End Select
        mHeight = mFont.Height
    End Sub

    Public ReadOnly Property Doc() As PdfCorporateNew
        Get
            Return mDoc
        End Get
    End Property

    Public Property Left() As Integer
        Get
            Return mLeft
        End Get
        Set(ByVal value As Integer)
            mLeft = value
        End Set
    End Property

    Public Property Width() As Integer
        Get
            Return mWidth
        End Get
        Set(ByVal value As Integer)
            mWidth = value
        End Set
    End Property

    Public ReadOnly Property Font() As Font
        Get
            Return mFont
        End Get
    End Property

    Public Property Brush() As Brush
        Get
            Return mBrush
        End Get
        Set(ByVal value As Brush)
            mBrush = value
        End Set
    End Property

    Public Property BackgroundColor() As Color
        Get
            Return mBackgroundColor
        End Get
        Set(ByVal value As Color)
            mBackgroundColor = value
        End Set
    End Property

    Public Property Height() As Integer
        Get
            Return mHeight
        End Get
        Set(ByVal value As Integer)
            mHeight = value
        End Set
    End Property


    Public Sub AddCell(Optional ByVal oType As PdfRowCell.Types = PdfRowCell.Types.TextLeftAligned, Optional ByVal iWidth As Integer = 0)
        Dim oCell As New PdfRowCell(Me, oType, iWidth)
        mCells.Add(oCell)
        If oCell.Width = 0 Then mNoWidthCells = True
    End Sub

    Public Sub Write(ByVal iTop As Integer, ByVal ParamArray oValues As Object())
        Dim iLeft As Integer = mDoc.LeftMargin + mLeft
        iLeft = mLeft
        If mNoWidthCells Then SetPendingWidths()
        Dim iValueIdx As Integer = 0

        For Each oCell As PdfRowCell In mCells
            If oCell.Type <> PdfRowCell.Types.Blank Then
                oCell.Write(iLeft, iTop, oValues(iValueIdx))
                iValueIdx += 1
                'perque es permet ometre valors nuls al final
                If iValueIdx >= oValues.Length Then Exit For
            End If
            iLeft += oCell.Width
        Next
    End Sub

    Private Sub SetPendingWidths()
        Dim oPendingWidthCells As New ArrayList
        Dim oCell As PdfRowCell = Nothing
        Dim iCellsWidth As Integer = 0
        For Each oCell In mCells
            If oCell.Width = 0 Then oPendingWidthCells.Add(oCell)
            iCellsWidth += oCell.Width
        Next
        Dim iRemainingWidth As Integer = mWidth - iCellsWidth
        Dim iCellWidth As Integer = iRemainingWidth / oPendingWidthCells.Count
        For Each oCell In oPendingWidthCells
            oCell.Width = iCellWidth
        Next
        mNoWidthCells = False
    End Sub
End Class

Public Class PdfRowCell
    Private mRow As PdfRow
    Private mType As Types
    Private mStringAlignment As StringAlignment
    Private mWidth As Integer

    Public Enum Types
        TextLeftAligned
        TextCenterAligned
        TextRightAligned
        Blank
        Int
        MoneySmall
        MoneyLarge
        Fch
    End Enum

    Public Enum Styles
        standard
        smaller
        docTitle
        docSubtitle
        boldOnBlueBackground
    End Enum

    Friend Sub New(ByVal oRow As PdfRow, ByVal oType As Types, ByVal iWidth As Integer)
        MyBase.New()
        mRow = oRow
        mType = oType
        mStringAlignment = GetStringAlignmentFromType(mType)
        mWidth = iWidth
        If iWidth = 0 Then
            mWidth = GetWidthFromType(mType)
        Else
            mWidth = iWidth
        End If
    End Sub

    Public ReadOnly Property Type() As Types
        Get
            Return mType
        End Get
    End Property

    Public Property Width() As Integer
        Get
            Return mWidth
        End Get
        Set(ByVal value As Integer)
            mWidth = value
        End Set
    End Property

    Public Sub Write(ByVal X As Integer, ByVal Y As Integer, ByVal oValue As Object)
        Dim oRc As New RectangleF(X, Y, mWidth, mRow.Height)
        If mRow.BackgroundColor <> Color.White Then
            mRow.Doc.FillRectangle(New SolidBrush(mRow.BackgroundColor), oRc)
        End If

        Dim sF As New StringFormat()
        sF.Alignment = mStringAlignment

        Dim s As String = GetParsedValueFromType(mType, oValue)
        mRow.Doc.DrawString(s, mRow.Font, mRow.Brush, oRc, sF)
    End Sub

    Private Function GetStringAlignmentFromType(ByVal otype As Types) As StringAlignment
        Dim oRetVal As StringAlignment = StringAlignment.Near
        Select Case otype
            Case Types.TextCenterAligned, Types.Fch
                oRetVal = StringAlignment.Center
            Case Types.TextRightAligned, Types.MoneySmall, Types.MoneyLarge, Types.Int
                oRetVal = StringAlignment.Far
        End Select
        Return oRetVal
    End Function

    Private Function GetWidthFromType(ByVal oType As Types) As Integer
        Dim iRetVal As Integer = 0
        Select Case oType
            Case Types.Fch
                iRetVal = MeasureString("00/00/00")
            Case Types.Int
                iRetVal = MeasureString("23.000")
            Case Types.MoneySmall
                iRetVal = MeasureString("99.999,00 €")
            Case Types.MoneyLarge
                iRetVal = MeasureString("999.999.999,00 €")
        End Select
        Return iRetVal
    End Function

    Private Function GetParsedValueFromType(ByVal oType As Types, ByVal oValue As Object) As String
        Dim sRetVal As String = ""
        Select Case oType
            Case Types.Fch
                sRetVal = Format(CDate(oValue), "dd/MM/yy")
            Case Types.Int
                sRetVal = Format(CInt(oValue), "#,###")
            Case Types.MoneySmall, Types.MoneyLarge
                If TypeOf (oValue) Is DTOAmt Then
                    sRetVal = DTOAmt.CurFormatted(DirectCast(oValue, DTOAmt))
                ElseIf IsNumeric(oValue) Then
                    Dim DcValue As Decimal = CDec(oValue)
                    If DcValue <> 0 Then
                        sRetVal = Format(CDec(oValue), "#,##0.00 €")
                    End If
                Else
                    sRetVal = oValue.ToString
                End If
            Case Else
                sRetVal = oValue.ToString
        End Select
        Return sRetVal
    End Function

    Private Function MeasureString(ByVal s As String) As Integer
        Dim oSize As SizeF = mRow.Doc.MeasureString(s, mRow.Font)
        Return oSize.Width
    End Function
End Class
