Imports System.ComponentModel
Public Class DatagridviewSortedColumnChangedEventArgs
    Inherits EventArgs

    Public Property ListSortDirection As ListSortDirection
    Public Property SortedColumnIndex As Integer

    Public Sub New(iSortColumnIndex As Integer, oListSortDirection As ListSortDirection)
        MyBase.New()
        _SortedColumnIndex = iSortColumnIndex
        _ListSortDirection = oListSortDirection
    End Sub

End Class
