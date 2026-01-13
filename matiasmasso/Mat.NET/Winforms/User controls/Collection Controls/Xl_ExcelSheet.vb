Imports MatHelperStd

Public Class Xl_ExcelSheet
    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As ExcelHelper.Sheet
    Private _ColumnHeaders As List(Of String)

    Public Shadows Sub Load(value As ExcelHelper.Sheet, Optional sColumnHeaders As List(Of String) = Nothing)
        _Value = value
        _ColumnHeaders = sColumnHeaders

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        MyBase.Rows.Clear()

        For Each oSrcRow As ExcelHelper.Row In _Value.Rows
            Dim oCellValuesList As New List(Of String)
            For Each oSrcCell As ExcelHelper.Cell In oSrcRow.Cells
                Try
                    oCellValuesList.Add(oSrcCell.Content)
                Catch ex As Exception
                    oCellValuesList.Add(Nothing)
                End Try
            Next

            Dim row As String() = oCellValuesList.ToArray
            MyBase.Rows.Add(row)
        Next

    End Sub

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True


        Dim iColumnsCount As Integer = _Value.Rows.First.Cells.Count
        For j As Integer = 0 To iColumnsCount - 1
            Dim iCol As Integer = MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            If _ColumnHeaders IsNot Nothing And iCol < _ColumnHeaders.Count Then
                MyBase.Columns(iCol).HeaderText = _ColumnHeaders(iCol)
            End If
        Next
    End Sub


End Class


