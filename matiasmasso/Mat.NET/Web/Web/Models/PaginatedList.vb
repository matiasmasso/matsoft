Public Class PaginatedList(Of T As DTOBaseGuid)
    Inherits List(Of T)

    Property takeFrom As Integer
    Property takeCount As Integer
    Property itemsCount As Integer

    Public Sub New(ByVal items As List(Of T), takeFrom As Integer, takeCount As Integer)
        MyBase.New
        _takeFrom = takeFrom
        _takeCount = takeCount
        _itemsCount = items.Count

        For i As Integer = takeFrom To takeTo() - 1
            MyBase.Add(items(i))
        Next
    End Sub

    Public Function takeTo() As Integer
        Dim retval As Integer = Math.Min(_takeFrom + _takeCount, _itemsCount)
        Return retval
    End Function

    Public Function disablePrevious() As Boolean
        Dim retval As Boolean = _takeFrom + _takeCount >= _itemsCount
        Return retval
    End Function

    Public Function disableNext() As Boolean
        Dim retval As Boolean = _takeFrom <= 0
        Return retval
    End Function

    Public Function inRange(takeFrom) As Boolean
        Dim retval As Boolean
        If takeFrom < 0 Then retval = False
        If takeFrom > MyBase.Count Then retval = False
        Return retval
    End Function
End Class
