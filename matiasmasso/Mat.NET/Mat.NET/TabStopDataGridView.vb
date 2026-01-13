Public Class TabStopDataGridView
    Inherits DataGridView

    'Prevents user from stop on ReadOnly Columns

    Protected Overrides Function ProcessDialogKey(keyData As System.Windows.Forms.Keys) As Boolean
        If keyData = Keys.Tab Then
            If IsNothing(MyBase.CurrentCell) Then
                Return True
            End If
            Dim nextColumn As DataGridViewColumn
            nextColumn = MyBase.Columns.GetNextColumn(MyBase.Columns(MyBase.CurrentCell.ColumnIndex), DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly)
            If Not IsNothing(nextColumn) Then
                Try
                    Dim oRow As DataGridViewRow = MyBase.Rows(MyBase.CurrentCell.RowIndex)
                    Dim oNextCell = oRow.Cells(nextColumn.Index)

                    MyBase.CurrentCell = oNextCell ' MyBase.Rows(MyBase.CurrentCell.RowIndex).Cells(nextColumn.Index)

                Catch ex As InvalidOperationException
                    'peta quan no valida la cel.la (e.cancel=true)
                End Try
            Else
                nextColumn = MyBase.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly)
                If (MyBase.CurrentCell.RowIndex + 1) = MyBase.Rows.Count Then
                    MyBase.CurrentCell = MyBase.Rows(0).Cells(nextColumn.Index)
                Else
                    Try
                        MyBase.CurrentCell = MyBase.Rows(MyBase.CurrentCell.RowIndex + 1).Cells(nextColumn.Index)
                    Catch ex As Exception
                        'peta quan evitem que canvii de cel.la perque les dades entrades no son valides
                    End Try
                End If
            End If
            Return True
        End If
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Protected Overrides Function ProcessDataGridViewKey(e As System.Windows.Forms.KeyEventArgs) As Boolean
        If e.KeyData = Keys.Tab And e.Shift = False Then
            If IsNothing(MyBase.CurrentCell) Then
                Return True
            End If
            Dim nextColumn As DataGridViewColumn
            nextColumn = MyBase.Columns.GetNextColumn(MyBase.Columns(MyBase.CurrentCell.ColumnIndex), DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly)
            If Not IsNothing(nextColumn) Then
                MyBase.CurrentCell = MyBase.Rows(MyBase.CurrentCell.RowIndex).Cells(nextColumn.Index)
            Else
                nextColumn = MyBase.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly)
                If (MyBase.CurrentCell.RowIndex + 1) = MyBase.Rows.Count Then
                    MyBase.CurrentCell = MyBase.Rows(0).Cells(nextColumn.Index)
                Else
                    MyBase.CurrentCell = MyBase.Rows(MyBase.CurrentCell.RowIndex + 1).Cells(nextColumn.Index)
                End If
            End If
            Return True
        End If
        If e.KeyData = 65545 And e.Shift = True Then
            Dim nextColumn As DataGridViewColumn
            Dim priorColumn As DataGridViewColumn
            nextColumn = MyBase.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly)
            priorColumn = nextColumn
            If nextColumn.DisplayIndex >= MyBase.Columns(MyBase.CurrentCell.ColumnIndex).DisplayIndex Then
                If MyBase.CurrentCell.RowIndex > 0 Then
                    MyBase.CurrentCell = MyBase.Rows(MyBase.CurrentCell.RowIndex - 1).Cells(MyBase.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly).Index)
                Else
                    MyBase.CurrentCell = MyBase.Rows(MyBase.Rows.Count - 1).Cells(MyBase.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly).Index)
                End If
            Else
                'Same Row
                Do Until nextColumn.DisplayIndex >= MyBase.Columns(MyBase.CurrentCell.ColumnIndex).DisplayIndex
                    priorColumn = nextColumn
                    nextColumn = MyBase.Columns.GetNextColumn(MyBase.Columns(nextColumn.Index), DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly)
                Loop
                MyBase.CurrentCell = MyBase.Rows(MyBase.CurrentCell.RowIndex).Cells(priorColumn.Index)
            End If
            Return True
        End If
        Return MyBase.ProcessDataGridViewKey(e)
    End Function

End Class
