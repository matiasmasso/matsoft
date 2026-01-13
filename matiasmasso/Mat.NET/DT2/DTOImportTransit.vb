Public Class DTOImportTransit
    Inherits DTOImportacio
    Property YearMonthFras As DTOYearMonth
    Property YearMonthAlbs As DTOYearMonth

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
