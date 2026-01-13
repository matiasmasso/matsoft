Public Class DTOPagination
    Property ItemsCount As Integer
    Property PageSize As Integer
    Property PageIndex As Integer
    Property Guid As Guid

    Public Sub New(iItemsCount As Integer, iPageSize As Integer, iPageIndex As Integer, Optional oGuid As Guid = Nothing)
        MyBase.New()
        _ItemsCount = iItemsCount
        _PageSize = iPageSize
        _PageIndex = iPageIndex
        _Guid = oGuid
    End Sub
End Class
