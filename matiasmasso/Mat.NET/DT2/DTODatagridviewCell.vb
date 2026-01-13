Public Class DTODatagridviewCell

    Property FirstDisplayedScrollingRowIndex As Integer
    Property RowIndex As Integer
    Property ColumnIndex As Integer
    Property SortedColumn As Integer
    Property SortOrder As SortOrders

    Public Enum SortOrders
        None
        Ascending
        Descending
    End Enum
End Class
