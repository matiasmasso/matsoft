Public Class MatGrid
    Inherits DataGridView
    Private mDirtyCell As Boolean

    Private Sub MatGrid_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles Me.CellBeginEdit
        mDirtyCell = True
    End Sub


    Private Sub MatGrid_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.CurrentCellChanged
        If mDirtyCell Then
            mDirtyCell = False
            Dim oGrid As DataGridView = Me
            If oGrid.CurrentRow.ReadOnly Then
                SendKeys.Send("{ENTER}")
            End If
            Dim iCurCol As Integer = oGrid.CurrentCell.ColumnIndex
            Dim iCol As Integer
            For iCol = iCurCol To oGrid.Columns.Count - 1
                If oGrid.Columns(iCol).ReadOnly = False Then Exit Sub
                SendKeys.Send("{TAB}")
            Next
            If oGrid.CurrentRow.ReadOnly Then
                SendKeys.Send("{ENTER}")
            End If
            For iCol = 0 To iCurCol - 1
                If oGrid.Columns(iCol).ReadOnly = False Then Exit Sub
                SendKeys.Send("{TAB}")
            Next
        End If
    End Sub

    Private Sub MatGrid_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles Me.EditingControlShowing
        'mDirtyCell = True
        'If Me.CurrentCell.ColumnIndex = 1 Then e.Control.TabStop = False
    End Sub

    Private Sub MatGrid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim oGrid As DataGridView = Me
        Select Case e.KeyCode

            Case Keys.Tab And e.Shift
                Dim oCurCol As DataGridViewColumn = oGrid.Columns(oGrid.CurrentCell.ColumnIndex)
                Dim oNextCol As DataGridViewColumn = Me.Columns.GetPreviousColumn(oCurCol, DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly)
                If oNextCol IsNot Nothing Then
                    Me.CurrentCell = Me.CurrentRow.Cells(oNextCol.Index)
                    e.Handled = True
                End If
            Case Keys.Tab
                Dim oCurCol As DataGridViewColumn = oGrid.Columns(oGrid.CurrentCell.ColumnIndex)
                Dim oNextCol As DataGridViewColumn = Me.Columns.GetNextColumn(oCurCol, DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly)
                If oNextCol IsNot Nothing Then
                    Me.CurrentCell = Me.CurrentRow.Cells(oNextCol.Index)
                    e.Handled = True
                End If
        End Select
    End Sub


End Class
