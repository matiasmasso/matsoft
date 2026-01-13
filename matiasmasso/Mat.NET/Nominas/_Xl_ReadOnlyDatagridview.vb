Imports MatHelperStd

Public Class _Xl_ReadOnlyDatagridview
    Inherits DataGridView


    Property DisplayObsolets As Boolean
    Property MouseIsDown As Boolean


    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToDelete(sender As Object, e As MatEventArgs)
    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)
    Public Event SortedColumnChange(sender As Object, e As DatagridviewSortedColumnChangedEventArgs)


    Protected Sub AddMenuItemObsolets(oContextMenu As ContextMenuStrip)
        Dim oMenuItem As New ToolStripMenuItem("mostrar obsolets", Nothing, AddressOf Do_DisplayObsolets)
        oMenuItem.Checked = _DisplayObsolets
        oMenuItem.CheckOnClick = True
        oContextMenu.Items.Add(oMenuItem)
    End Sub

    Protected Sub Do_DisplayObsolets()
        _DisplayObsolets = Not _DisplayObsolets
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    'Protected Sub RefreshRequest(sender As Object, e As MatEventArgs)
    'RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    'End Sub

    Protected Sub RefreshRequest(sender As Object, e As System.EventArgs)
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Sub DeleteRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToDelete(Me, e)
    End Sub

    Protected Sub ToggleProgressBarRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToToggleProgressBar(Me, e)
    End Sub


    Private Sub Control_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        _MouseIsDown = True
    End Sub

    Protected Function ExcelPastedCells() As DataTable
        Dim retval As New DataTable

        Dim objPresumablyExcel As IDataObject = Clipboard.GetDataObject()
        If (objPresumablyExcel.GetDataPresent(DataFormats.CommaSeparatedValue)) Then

            Dim srReadExcel As New System.IO.StreamReader(DirectCast(objPresumablyExcel.GetData(DataFormats.CommaSeparatedValue), System.IO.Stream))

            'The next task would be to read this stream of data one line at a time. A loop is the order of the day.
            While (srReadExcel.Peek() > 0)
                Dim sFormattedData As String = srReadExcel.ReadLine()
                Dim arrSplitData As Array = sFormattedData.Split(";")
                'The number of Array items is equivalent to the number of columns of data copied from the Excel Sheet.
                If retval.Columns.Count <= 0 Then
                    For iLoopCounter = 0 To arrSplitData.GetUpperBound(0)
                        retval.Columns.Add()
                    Next
                End If

                Dim oRow As DataRow = retval.NewRow()
                For iLoopCounter = 0 To arrSplitData.GetUpperBound(0)
                    oRow(iLoopCounter) = arrSplitData.GetValue(iLoopCounter)
                Next
                retval.Rows.Add(oRow)
            End While
        End If
        Return retval
    End Function


    Public Class DataGridViewImageCellBlank
        Inherits DataGridViewImageCell
        Public Sub New()
            MyBase.New()
        End Sub
        ' constructor
        Public Sub New(valueIsIcon As Boolean)
            MyBase.New(valueIsIcon)
        End Sub
        ' constructor
        Public Overrides ReadOnly Property DefaultNewRowValue() As Object
            Get
                ' RETURNS NULL, INSTEAD OF THE 'RED X'
                Return Nothing
            End Get
        End Property
    End Class

End Class
