Public Class JScriptDate
    Public Property Src As String
    Public Property Fch As Date
    Public Enum Fields
        Weekday
        Month
        Day
        Year
        HHmmss
        Offset
    End Enum
    Public Sub New(src As String)
        _Src = src
        Loadfields()
    End Sub

    Shared Function ToNetDate(src As String) As Date
        Dim o As New JScriptDate(src)
        Return o.Fch()
    End Function

    Private Sub LoadFields()
        Dim segments = Src.Split(" ")
        Dim months = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}.ToList()
        Dim day As Integer = segments(Fields.Day)
        Dim month = months.IndexOf(segments(Fields.Month))
        Dim year = segments(Fields.Year)
        _Fch = New Date(year, month, day)
    End Sub
End Class
