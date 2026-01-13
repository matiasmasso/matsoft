Public Class DTOKpi
    Property YearMonths As List(Of DTOYearMonth)
    Property Caption As String
    Property YFactor As Decimal
    Property Id As Ids

    Public Enum Formats
        Eur
        [Decimal]
    End Enum

    Public Enum Ids
        Activo_Corriente
        Pasivo_Corriente
        Comandes_Proveidors
        Comandes_Clients
        Efectes_Vençuts
        Efectes_Impagats
        Index_Impagats
    End Enum



    Shared Function Factory(id As Ids) As DTOKpi
        Dim retval As New DTOKpi
        retval.Id = id
        retval.YearMonths = New List(Of DTOYearMonth)
        Return retval
    End Function

    Public Function format() As Formats
        Dim retval As Formats = Formats.Eur
        Select Case _Id
            Case Ids.Index_Impagats
                retval = Formats.Decimal
        End Select
        Return retval
    End Function

    Public Function MaxValue() As Decimal
            Dim retval = 0
            If _YearMonths.Count > 0 Then
                retval = _YearMonths.Max(Function(x) x.Eur)
            End If
            Return retval
        End Function

        Public Function YearMonthFrom() As DTOYearMonth
            Return DTOYearMonth.Min(_YearMonths)
        End Function

        Public Function YearMonthTo() As DTOYearMonth
            Return DTOYearMonth.Max(_YearMonths)
        End Function

    Public Function MonthsCount() As Integer
        Return DTOYearMonth.MonthsDiff(YearMonthFrom, YearMonthTo)
    End Function

    Public Function AddYearMonth(year As Integer, month As Integer, eur As Decimal) As DTOYearMonth
        Dim retval As New DTOYearMonth(year, month, eur)
        _YearMonths.Add(retval)
        Return retval
    End Function
End Class

