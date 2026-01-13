Public Class MatTable
    Private mLeft As Integer = 0
    Private mTop As Integer = 200
    Private mHeight As Decimal = 300

    Private mRows As MatRows
    Private mColumns As MatColumns
    Private mFont As New Font("Arial", 10, FontStyle.Regular)
    Private mForeColor As Color = Color.Black
    Private mBackColor As Color = Color.LightGray
    Private mHeaderRow As MatHeaderRow
    Private mFooterRow As MatFooterRow
    Private mCellSpacing As Integer = 2
    Private mLinesPerPage As Integer

    Public Sub New(Optional ByVal iLeft As Integer = -1, Optional ByVal iTop As Integer = -1, Optional ByVal oFont As Font = Nothing, Optional ByVal iHeight As Decimal = -1)
        MyBase.new()
        If iLeft <> -1 Then mLeft = iLeft
        If iTop <> -1 Then mTop = iTop
        If iHeight <> -1 Then mHeight = iHeight
        If oFont IsNot Nothing Then mFont = oFont

        mHeaderRow = New MatHeaderRow(Me)

        mFooterRow = New MatFooterRow(Me)

        mHeaderRow.Font = mFont
        mFooterRow.Font = mFont
        mFooterRow.Height = mFont.GetHeight
        mHeaderRow.Height = mFont.GetHeight
        mLinesPerPage = (mHeight) / (mFont.GetHeight + mCellSpacing)
    End Sub

    Public ReadOnly Property HeaderRow() As MatHeaderRow
        Get
            Return mHeaderRow
        End Get
    End Property

    Public ReadOnly Property FooterRow() As MatFooterRow
        Get
            Return mFooterRow
        End Get
    End Property

    Public ReadOnly Property Left() As Integer
        Get
            Return mLeft
        End Get
    End Property

    Public ReadOnly Property Top() As Integer
        Get
            Return mTop
        End Get
    End Property

    Public Property CellSpacing() As Integer
        'espai entre celdes (no inclos a width)
        Get
            Return mCellSpacing
        End Get
        Set(ByVal value As Integer)
            mCellSpacing = value
        End Set
    End Property


    Public Property Columns() As MatColumns
        Get
            If mColumns Is Nothing Then mColumns = New MatColumns(Me)
            Return mColumns
        End Get
        Set(ByVal value As MatColumns)
            mColumns = value
        End Set
    End Property

    Public Property Rows() As MatRows
        Get
            If mRows Is Nothing Then mRows = New MatRows
            Return mRows
        End Get
        Set(ByVal value As MatRows)
            mRows = value
        End Set
    End Property

    Public Property Font() As Font
        Get
            Return mFont
        End Get
        Set(ByVal value As Font)
            mFont = value
            mHeaderRow.Font = mFont
            mFooterRow.Font = mFont
            mFooterRow.Height = mFont.GetHeight
            mHeaderRow.Height = mFont.GetHeight
            mLinesPerPage = (mHeight) / (mFont.GetHeight + mCellSpacing)
        End Set
    End Property

    Public Property ForeColor() As Color
        Get
            Return mForeColor
        End Get
        Set(ByVal value As Color)
            mForeColor = value
        End Set
    End Property

    Public Property BackColor() As Color
        Get
            Return mBackColor
        End Get
        Set(ByVal value As Color)
            mBackColor = value
        End Set
    End Property

    Public Function NewRow() As MatRow
        Dim oRow As New MatRow(Me)
        Return oRow
    End Function

    Public Property LinesPerPage() As Integer
        Get
            Return mLinesPerPage
        End Get
        Set(ByVal value As Integer)
            mLinesPerPage = value
        End Set
    End Property


End Class

Public Class MatColumn
    Private mTable As MatTable
    Private mCaption As String
    Private mWidth As Integer
    Private mOffset As Integer
    Private mType As Types
    Private mAlign As StringAlignment
    Private mSumAmt = DTOAmt.Empty
    Private mSumDbl As Decimal
    Private mFooterVisible As Boolean

    Public Enum Types
        [integer]
        amt
        [string]
        [date]
        percent
    End Enum

    Public Sub New(ByVal oTable As MatTable, ByVal sCaption As String, ByVal iWidth As Integer, ByVal oType As Types)
        MyBase.New()
        mTable = oTable
        mCaption = sCaption
        mWidth = iWidth
        mType = oType
        Select Case mType
            Case Types.date
                mAlign = StringAlignment.Center
            Case Types.string
                mAlign = StringAlignment.Near
            Case Types.integer, Types.amt, Types.percent
                mAlign = StringAlignment.Far
        End Select
    End Sub

    Public ReadOnly Property Table() As MatTable
        Get
            Return mTable
        End Get
    End Property

    Public ReadOnly Property Alignment() As StringAlignment
        Get
            Return mAlign
        End Get
    End Property

    Public ReadOnly Property Caption() As String
        Get
            Return mCaption
        End Get
    End Property

    Public ReadOnly Property Width() As Integer
        Get
            Return mWidth
        End Get
    End Property

    Public ReadOnly Property [type]() As Types
        Get
            Return mType
        End Get
    End Property

    Public Property Offset() As Integer
        Get
            Return mOffset
        End Get
        Set(ByVal value As Integer)
            mOffset = value
        End Set
    End Property

    Public Property FooterVisible() As Boolean
        Get
            Return mFooterVisible
        End Get
        Set(ByVal value As Boolean)
            mFooterVisible = value
        End Set
    End Property

    Public ReadOnly Property SumAmt() as DTOAmt
        Get
            Return mSumAmt
        End Get
    End Property

    Public Property SumDbl() As Decimal
        Get
            Return mSumDbl
        End Get
        Set(ByVal value As Decimal)
            mSumDbl = value
        End Set
    End Property
End Class


Public Class MatRow
    Private mTable As MatTable
    Private mCells As MatCells
    Private mForeColor As Color = Color.Black
    Private mBackColor As Color = Color.FromArgb(245, 245, 245)
    Private mHeight As Integer
    Private mFont As Font
    Private mPage As Integer

    Friend Sub New(ByVal oTable As MatTable)
        mTable = oTable
        mFont = oTable.Font
        mHeight = oTable.Font.GetHeight()
        mCells = New MatCells()
        Dim oColumn As MatColumn
        Dim oCell As MatCell
        For Each oColumn In oTable.Columns
            oCell = New MatCell(Me, oColumn)
            oCell.Font = mTable.Font
            mCells.Add(oCell)
        Next
    End Sub

    Public ReadOnly Property Table() As MatTable
        Get
            Return mTable
        End Get
    End Property

    Public ReadOnly Property Cells() As MatCells
        Get
            Return mCells
        End Get
    End Property

    Public Property Font() As Font
        Get
            Return mFont
        End Get
        Set(ByVal value As Font)
            mFont = value
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

    Public Property ForeColor() As Color
        Get
            Return mForeColor
        End Get
        Set(ByVal value As Color)
            mForeColor = value
        End Set
    End Property

    Public Property BackColor() As Color
        Get
            Return mBackColor
        End Get
        Set(ByVal value As Color)
            mBackColor = value
        End Set
    End Property

    Public Property Page() As Integer
        Get
            Return mPage
        End Get
        Set(ByVal value As Integer)
            mPage = value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

Public Class MatCell
    Private mColumn As MatColumn
    Private mRow As MatRow
    Private mForeColor As Color
    Private mBackColor As Color
    Private mValue As Object
    Private mFont As Font

    Public Sub New(ByVal oRow As MatRow, ByVal oColumn As MatColumn)
        mRow = oRow
        mColumn = oColumn
        mFont = oRow.Font
        mForeColor = oRow.ForeColor
        mBackColor = oRow.backcolor
    End Sub

    Public Property Value() As Object
        Get
            Return mValue
        End Get
        Set(ByVal oValue As Object)
            mValue = oValue
            If Not TypeOf (mRow) Is MatFooterRow Then
                Select Case mColumn.type
                    Case MatColumn.Types.integer
                        If IsNumeric(mValue) Then
                            mColumn.SumDbl += mValue
                        End If
                    Case MatColumn.Types.amt
                        If TypeOf (mValue) Is DTOAmt Then
                            mColumn.SumAmt.Add(mValue)
                        End If
                End Select
            End If
        End Set
    End Property

    Public ReadOnly Property Column() As MatColumn
        Get
            Return mColumn
        End Get
    End Property

    Public ReadOnly Property Row() As MatRow
        Get
            Return mRow
        End Get
    End Property

    Public Property ForeColor() As Color
        Get
            Return mForeColor
        End Get
        Set(ByVal value As Color)
            mForeColor = value
        End Set
    End Property

    Public Property BackColor() As Color
        Get
            Return mBackColor
        End Get
        Set(ByVal value As Color)
            mBackColor = value
        End Set
    End Property

    Public Property Font() As Font
        Get
            Return mFont
        End Get
        Set(ByVal value As Font)
            mFont = value
        End Set
    End Property
End Class

Public Class MatHeaderRow
    Inherits MatRow
    Private mVisible As Boolean = True

    Public Sub New(ByVal oTable As MatTable)
        MyBase.New(oTable)
        BackColor = Color.FromArgb(255, 204, 153)
    End Sub

    Public Property Visible() As Boolean
        Get
            Return mVisible
        End Get
        Set(ByVal value As Boolean)
            mVisible = value
        End Set
    End Property
End Class

Public Class MatFooterRow
    Inherits MatRow

    Private mVisible As Boolean = True

    Public Sub New(ByVal oTable As MatTable)
        MyBase.New(oTable)
        BackColor = Color.FromArgb(235, 235, 235)
    End Sub

    Public Property Visible() As Boolean
        Get
            Return mVisible
        End Get
        Set(ByVal value As Boolean)
            mVisible = value
        End Set
    End Property

End Class

Public Class MatRows
    Inherits System.Collections.CollectionBase

    Public Sub Add(ByVal NewObjMember As MatRow)
        List.Add(NewObjMember)
    End Sub

    Public Sub Remove(ByVal index As Integer)
        If index > Count - 1 Or index < 0 Then Exit Sub
        List.RemoveAt(index)
    End Sub

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As MatRow
        Get
            Item = List.Item(vntIndexKey)
        End Get
    End Property
End Class

Public Class MatCells
    Inherits System.Collections.CollectionBase
    Private mRow As MatRow

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub Add(ByVal oCell As MatCell)
        List.Add(oCell)
    End Sub

    Public Sub Remove(ByVal index As Integer)
        If index > Count - 1 Or index < 0 Then Exit Sub
        List.RemoveAt(index)
    End Sub

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As MatCell
        Get
            Item = List.Item(vntIndexKey)
        End Get
    End Property
End Class

Public Class MatColumns
    Inherits System.Collections.CollectionBase

    Private mTable As MatTable
    Private mLastOffset As Integer

    Public Sub New(ByVal oTable As MatTable)
        MyBase.New()
        mTable = oTable
    End Sub

    Public Sub Add(ByVal sCaption As String, ByVal iWidth As Integer, ByVal oType As MatColumn.Types)
        Dim oColumn As New MatColumn(mTable, sCaption, iWidth, oType)
        oColumn.Offset = mLastOffset
        mLastOffset += (iWidth + mTable.CellSpacing)
        List.Add(oColumn)

        Dim oHeaderCell As New MatCell(mTable.HeaderRow, oColumn)
        oHeaderCell.Value = oColumn.Caption
        mTable.HeaderRow.Cells.Add(oHeaderCell)

        Dim oFooterCell As New MatCell(mTable.FooterRow, oColumn)
        mTable.FooterRow.Cells.Add(oFooterCell)
    End Sub

    Public Sub Remove(ByVal index As Integer)
        If index > Count - 1 Or index < 0 Then Exit Sub
        List.RemoveAt(index)
    End Sub

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As MatColumn
        Get
            Item = List.Item(vntIndexKey)
        End Get
    End Property
End Class
