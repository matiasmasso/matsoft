Public Class DTOProductRank

    Property Period As Periods
    Property Items As List(Of Item)
    Property Zonas As List(Of DTOGuidNom)

    Property Brands As List(Of DTOGuidNom)

    Property Unit As Units

    Property Lang As DTOLang

    Public Enum Periods
        Year
        Quarter
        Month
    End Enum

    Public Enum Units
        Units
        Eur
    End Enum

    Public Sub New(oPeriod As Periods)
        MyBase.New
        _Period = oPeriod
        _Items = New List(Of Item)
    End Sub

    Public Function YearMonthFrom() As DTOYearMonth
        Dim retval = DTOYearMonth.current
        Select Case _Period
            Case DTOProductRank.Periods.Year
                retval = DTOYearMonth.Previous(12)
            Case DTOProductRank.Periods.Quarter
                retval = DTOYearMonth.Previous(3)
            Case DTOProductRank.Periods.Month
                retval = DTOYearMonth.Previous()
        End Select
        Return retval
    End Function


    Public Function share(item As Item) As Decimal
        Dim retval As Decimal = 0
        Dim tot = _Items.Sum(Function(x) x.Value)
        If tot > 0 Then
            retval = item.Value / tot
        End If
        Return retval
    End Function

    Public Class Item
        Property Product As DTOProductCategory
        Property Value As Decimal
    End Class
End Class
