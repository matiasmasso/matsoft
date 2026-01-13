Public Class DTOMonthDay
    Property Month As Integer
    Property Day As Integer

    Public Sub New(Month As Integer, Day As Integer)
        MyBase.New
        _Month = Month
        _Day = Day
    End Sub
End Class
