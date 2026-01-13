Public Class MatDataGridView

    Inherits DataGridView

    Public Sub New()
        Me.InitializeComponent()
    End Sub

    Private components As System.ComponentModel.IContainer = Nothing

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso Not Me.components Is Nothing Then
            Me.components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
    End Sub

    Private Function GetFirstColumnIndex() As Integer
        Dim column As DataGridViewColumn = Me.Columns.GetFirstColumn(DataGridViewElementStates.Visible)
        Dim index As Integer = Convert.ToInt32(IIf(column Is Nothing, -1, column.Index))
        Return index
    End Function

    Private Function GetFirstRowIndex() As Integer
        Return Me.Rows.GetFirstRow(DataGridViewElementStates.Visible)
    End Function

    Private Function GetLastColumnIndex() As Integer
        Dim column As DataGridViewColumn = Me.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None)
        Dim index As Integer = Convert.ToInt32(IIf(column Is Nothing, -1, column.Index))
        Return index
    End Function

    Private Function GetLastRowIndex() As Integer
        Return Me.Rows.GetLastRow(DataGridViewElementStates.Visible)
    End Function

    Private Function GetNextTabPosition(ByVal currentCellAddress As Point) As Point
        Dim nextCellAddress As Point = New Point(-1, -1)
        Dim nextColumnIndex As Integer = -1

        If Me.CurrentCellAddress.X <> -1 Then
            Dim column As DataGridViewColumn = Me.Columns.GetNextColumn(Me.Columns(currentCellAddress.X), DataGridViewElementStates.Visible, DataGridViewElementStates.None)
            If Not column Is Nothing Then
                nextColumnIndex = column.Index
            End If
        End If
        If nextColumnIndex = -1 Then
            nextCellAddress.X = Me.GetFirstColumnIndex()
            'nextCellAddress.Y = Me.Rows.GetNextRow(currentCellAddress.Y, DataGridViewElementStates.Visible)
            nextCellAddress.Y = Me.Rows.GetNextRow(currentCellAddress.Y, DataGridViewElementStates.Visible And Not DataGridViewElementStates.ReadOnly)
            If nextCellAddress.Y = -1 Then
                nextCellAddress.Y = Me.GetFirstRowIndex()
            End If
        Else
            nextCellAddress.X = nextColumnIndex
            nextCellAddress.Y = currentCellAddress.Y
        End If
        Return nextCellAddress
    End Function

    Private Function CanTabToNextCell() As Boolean
        Dim found As Boolean = False
        Dim currentCellAddress As Point = Me.CurrentCellAddress

        Do While (Not found)
            Dim nextCellAddress As Point = Me.GetNextTabPosition(currentCellAddress)
            If TypeOf MyBase.Rows(nextCellAddress.Y).Cells(nextCellAddress.X) Is ISupportTabStop Then
                Dim tabstopcell As ISupportTabStop = _
                    DirectCast(Me.Rows(nextCellAddress.Y).Cells(nextCellAddress.X), ISupportTabStop)
                If tabstopcell.TabStop Then found = True
            Else
                found = True
            End If
            If Not found AndAlso _
               nextCellAddress.X <= currentCellAddress.X AndAlso _
               nextCellAddress.Y <= currentCellAddress.Y Then
                Return False
            End If
            currentCellAddress = nextCellAddress
        Loop
        Return True
    End Function

    Private Function TabToNextCell(ByVal keyData As Keys) As Boolean
        If Me.GetFirstColumnIndex() = -1 OrElse Me.GetFirstRowIndex() = -1 Then Return False
        If Not Me.CanTabToNextCell() Then Return False
        Return Me.TabToCell(keyData)
    End Function

    Private Function GetPreviousTabPosition(ByVal currentCellAddress As Point) As Point
        Dim previousCellAddress As Point = New Point(-1, -1)
        Dim previousColumnIndex As Integer = -1

        If Me.CurrentCellAddress.X <> -1 Then
            Dim column As DataGridViewColumn = Me.Columns.GetPreviousColumn(Me.Columns(currentCellAddress.X), _
            DataGridViewElementStates.Visible, DataGridViewElementStates.None)
            If Not column Is Nothing Then
                previousColumnIndex = column.Index
            End If
        End If
        If previousColumnIndex = -1 Then
            previousCellAddress.X = Me.GetLastColumnIndex()
            previousCellAddress.Y = Me.Rows.GetPreviousRow(currentCellAddress.Y, _
                                    DataGridViewElementStates.Visible)
            If previousCellAddress.Y = -1 Then
                previousCellAddress.Y = Me.GetLastRowIndex()
            End If
        Else
            previousCellAddress.X = previousColumnIndex
            previousCellAddress.Y = currentCellAddress.Y
        End If
        Return previousCellAddress
    End Function

    Private Function CanTabToPreviousCell() As Boolean
        Dim found As Boolean = False
        Dim currentCellAddress As Point = Me.CurrentCellAddress

        Do While (Not found)
            Dim previousCellAddress As Point = Me.GetPreviousTabPosition(currentCellAddress)
            If TypeOf MyBase.Rows(previousCellAddress.Y).Cells(previousCellAddress.X) Is ISupportTabStop Then
                Dim tabstopcell As ISupportTabStop = _
                DirectCast(Me.Rows(previousCellAddress.Y).Cells(previousCellAddress.X), ISupportTabStop)
                If tabstopcell.TabStop Then found = True
            Else
                found = True
            End If
            If Not found AndAlso _
               previousCellAddress.X >= currentCellAddress.X AndAlso _
               previousCellAddress.Y >= currentCellAddress.Y Then
                Return False
            End If
            currentCellAddress = previousCellAddress
        Loop
        Return True
    End Function

    Private Function TabToPreviousCell(ByVal keyData As Keys) As Boolean
        If Me.GetFirstColumnIndex() = -1 OrElse Me.GetFirstRowIndex() = -1 Then Return False
        If Not Me.CanTabToPreviousCell() Then Return False
        Return Me.TabToCell(keyData)
    End Function

    Private Function TabToCell(ByVal keyData As Keys) As Boolean
        Dim found As Boolean = False
        Dim currentCellAddress As Point = Me.CurrentCellAddress

        Do While (Not found)
            Dim processed As Boolean = MyBase.ProcessTabKey(keyData)
            If Not processed Then
                MyBase.Rows(currentCellAddress.Y).Cells(currentCellAddress.X).Selected = True
                Return False
            End If

            If TypeOf Me.CurrentCell Is ISupportTabStop Then
                Dim tabstopcell As ISupportTabStop = DirectCast(Me.CurrentCell, ISupportTabStop)
                If tabstopcell.TabStop Then Return True
            Else
                Return processed
            End If
        Loop
        Return False
    End Function

    Protected Overrides Function ProcessDataGridViewKey(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
        Select Case e.KeyData
            Case Keys.Tab, Keys.Enter
                If Me.TabToNextCell(e.KeyData) Then
                    Return True
                Else
                    Return MyBase.ProcessDialogKey(e.KeyData Or Keys.Control)
                End If
            Case Keys.Shift Or Keys.Tab
                If Me.TabToPreviousCell(e.KeyData) Then
                    Return True
                Else
                    Return MyBase.ProcessDialogKey(e.KeyData Or Keys.Control)
                End If
        End Select
        Return MyBase.ProcessDataGridViewKey(e)
    End Function

    Protected Overrides Function ProcessDialogKey( _
            ByVal keyData As Keys) As Boolean
        If (keyData And Keys.KeyCode) = Keys.Enter Then
            Return Me.ProcessTabKey(keyData)
        End If
        Return MyBase.ProcessDialogKey(keyData)
    End Function



End Class
