Public Class DTOYearMonth
    Property year As Integer
    Property month As Months

    Property Eur As Decimal

    Public Enum Months
        NotSet
        January
        February
        March
        April
        May
        June
        July
        August
        September
        October
        November
        December
    End Enum

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(s4DigitsChar As String)
        MyBase.New()
        If IsNumeric(s4DigitsChar) Then
            If s4DigitsChar.Length = 4 Then
                _Year = s4DigitsChar.Substring(0, 2)
                _Month = s4DigitsChar.Substring(2, 2)
            ElseIf s4DigitsChar.Length = 6 Then
                _Year = s4DigitsChar.Substring(0, 4)
                _Month = s4DigitsChar.Substring(4, 2)
            End If

        End If
    End Sub

    Public Sub New(iYear As Integer, iMonth As Months, Optional Eur As Decimal = 0)
        MyBase.New()
        _Year = iYear
        _Month = iMonth
        _Eur = Eur
    End Sub

    Public Shadows Function Equals(oCandidate As Object) As Boolean
        Dim retval As Boolean
        If oCandidate IsNot Nothing Then
            If TypeOf oCandidate Is DTOYearMonth Then
                retval = (CType(oCandidate, DTOYearMonth).Year = _Year And CType(oCandidate, DTOYearMonth).Month = _Month)
            End If
        End If
        Return retval
    End Function

    Public Function MonthNum() As Integer
        Return CInt(_Month)
    End Function

    Public Function Tag() As String
        Dim sYea As String = Format(_Year, "0000")
        Dim sMes As String = Format(CInt(_Month), "00")
        Dim retval As String = String.Format("Y{0}M{1}", sYea, sMes)
        Return retval
    End Function

    Public Function RawTag() As String
        Dim retval As String = String.Format("{0:0000}{1:00}", _Year, CInt(_Month))
        Return retval
    End Function

    Public Function Caption(oLang As DTOLang) As String
        Return String.Format("{0:0000} {1}", _year, oLang.MesAbr(_month))
    End Function

    Shared Function Current() As DTOYearMonth
        Dim retval As DTOYearMonth = FromFch(Today)
        Return retval
    End Function
    Shared Function Previous(Optional months As Integer = 1) As DTOYearMonth
        Dim retval As DTOYearMonth = FromFch(Today.AddMonths(-months))
        Return retval
    End Function

    Shared Function FromTag(sTag As String) As DTOYearMonth
        Dim retval As DTOYearMonth = Nothing
        If sTag.Length = 8 Then
            Dim sYea As String = sTag.Substring(1, 4)
            Dim sMes As String = sTag.Substring(6, 2)
            If IsNumeric(sYea) And IsNumeric(sMes) Then
                Dim iMes As Integer = CInt(sMes)
                If iMes >= 1 And iMes <= 12 Then
                    Dim iYea As Integer = CInt(sYea)
                    Dim oMonth As Months = CType(iMes, DTOYearMonth.Months)
                    retval = New DTOYearMonth(iYea, oMonth)
                End If
            End If
        ElseIf sTag.Length = 6 Then
            Dim sYea As String = sTag.Substring(0, 4)
            Dim sMes As String = sTag.Substring(4, 2)
            If IsNumeric(sYea) And IsNumeric(sMes) Then
                Dim iMes As Integer = CInt(sMes)
                If iMes >= 1 And iMes <= 12 Then
                    Dim iYea As Integer = CInt(sYea)
                    Dim oMonth As Months = CType(iMes, DTOYearMonth.Months)
                    retval = New DTOYearMonth(iYea, oMonth)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function FromFch(DtFch As Date) As DTOYearMonth
        Dim retval As New DTOYearMonth(DtFch.Year, DtFch.Month)
        Return retval
    End Function



    Public Function FchFrom() As Date
        Dim retval As New Date(_Year, _Month, 1)
        Return retval
    End Function

    Public Function FchTo() As Date
        Dim retval As Date = FchFrom.AddMonths(1).AddDays(-1)
        Return retval
    End Function

    Public Function AddMonths(iMonthsToAdd As Integer) As DTOYearMonth
        Dim retval As DTOYearMonth = Me
        If iMonthsToAdd > 0 Then
            For i As Integer = 1 To iMonthsToAdd
                retval = retval.NextMonth
            Next
        Else
            For i As Integer = 1 To Math.Abs(iMonthsToAdd)
                retval = retval.PreviousMonth
            Next
        End If
        Return retval
    End Function

    Public Function NextMonth() As DTOYearMonth
        Dim retval As DTOYearMonth
        If _Month < Months.December Then
            retval = New DTOYearMonth(_Year, _Month + 1)
        Else
            retval = New DTOYearMonth(_Year + 1, Months.January)
        End If
        Return retval
    End Function

    Public Function PreviousMonth() As DTOYearMonth
        Dim retval As DTOYearMonth
        If _Month > Months.January Then
            retval = New DTOYearMonth(_Year, _Month - 1)
        Else
            retval = New DTOYearMonth(_Year - 1, Months.December)
        End If
        Return retval
    End Function

    Public Function IsOutdated6MonthsOrMore() As Boolean
        Dim DtFch As Date = LastFch()
        Dim mesos As Integer = DateDiff(DateInterval.Month, DtFch, Today)
        Dim retval As Integer = mesos > 6
        Return retval
    End Function

    Public Function FirstFch() As Date
        Dim retval As New Date(_Year, _Month, 1)
        Return retval
    End Function

    Public Function LastFch() As Date
        Dim DtFch As Date = FirstFch()
        Dim retval As Date = DtFch.AddMonths(1).AddDays(-1)
        Return retval
    End Function

    Shared Function HasFch(oYearMonth As DTOYearMonth, DtFch As Date) As Boolean
        Dim retval As Boolean
        If DtFch.Year = oYearMonth.Year AndAlso DtFch.Month = oYearMonth.Month Then retval = True
        Return retval
    End Function

    Public Function IsInRange(minFch As Date, maxFch As Date) As Boolean
        Dim oMin As New DTOYearMonth(minFch.Year, minFch.Month)
        Dim oMax As New DTOYearMonth(maxFch.Year, maxFch.Month)
        Dim myTag As String = Me.Tag
        Dim retval As Boolean = myTag >= oMin.Tag AndAlso myTag <= oMax.Tag
        Return retval
    End Function

    Public Function IsInRange(oRange As List(Of DTOYearMonth)) As Boolean
        Dim retval As Boolean = oRange.Any(Function(x) x.Equals(Me))
        Return retval
    End Function

    Shared Function Range(oYearMonthFrom As DTOYearMonth, oYearMonthTo As DTOYearMonth) As List(Of DTOYearMonth)
        Dim retval As New List(Of DTOYearMonth)
        Dim item = New DTOYearMonth(oYearMonthFrom.year, oYearMonthFrom.month)
        Do While item.IsLowerOrEqualThan(oYearMonthTo)
            retval.Add(item)
            item = item.AddMonths(1)
        Loop
        Return retval
    End Function

    Shared Function Range(tagFrom As String, tagTo As String) As List(Of DTOYearMonth)
        Dim retval As New List(Of DTOYearMonth)
        Dim itemTo = DTOYearMonth.FromTag(tagTo)
        Dim item = DTOYearMonth.FromTag(tagFrom)
        Do While item.IsLowerOrEqualThan(itemTo)
            retval.Add(item)
            item = item.NextMonth()
        Loop
        Return retval
    End Function

    Public Function IsLowerThan(oCandidate As DTOYearMonth) As Boolean
        Dim retval As Boolean
        If _Year < oCandidate.Year Then
            retval = True
        ElseIf _Year = oCandidate.Year And _Month < oCandidate.Month Then
            retval = True
        End If
        Return retval
    End Function

    Public Function IsGreaterThan(oCandidate As DTOYearMonth) As Boolean
        Dim retval As Boolean
        If _Year > oCandidate.Year Then
            retval = True
        ElseIf _Year = oCandidate.Year And _Month > oCandidate.Month Then
            retval = True
        End If
        Return retval
    End Function

    Public Function IsLowerOrEqualThan(oCandidate As DTOYearMonth) As Boolean
        Dim retval As Boolean
        If _Year < oCandidate.Year Then
            retval = True
        ElseIf _Year = oCandidate.Year And _Month <= oCandidate.Month Then
            retval = True
        End If
        Return retval
    End Function

    Public Function IsGreaterOrEqualThan(oCandidate As DTOYearMonth) As Boolean
        Dim retval As Boolean
        If _Year > oCandidate.Year Then
            retval = True
        ElseIf _Year = oCandidate.Year And _Month >= oCandidate.Month Then
            retval = True
        End If
        Return retval
    End Function

    Public Function DaysInMonth() As Integer
        Dim retval As Integer = System.DateTime.DaysInMonth(_Year, _Month)
        Return retval
    End Function

    Public Function DaysToEndMonth() As Integer
        Dim retval As Integer = (FchTo() - Today).Days
        Return retval
    End Function

    Public Function Last12YearMonths() As List(Of DTOYearMonth)
        Dim retval As New List(Of DTOYearMonth)
        Dim item As DTOYearMonth = Me
        For i As Integer = 1 To 12
            retval.Insert(0, item)
            item = item.PreviousMonth
        Next
        Return retval
    End Function

    Public Function Formatted(oLang As DTOLang) As String
        Dim retval As String = String.Format("{0} {1}", oLang.MesAbr(_Month), _Year)
        Return retval
    End Function

    Shared Function Formatted(oYearMonth As DTOYearMonth, oLang As DTOLang) As String
        Dim retval As String = String.Format("{0} {1}", oLang.MesAbr(oYearMonth.Month), oYearMonth.Year)
        Return retval
    End Function

    Shared Function MonthsDiff(oFirstYearMonth As DTOYearMonth, oSecondYearMonth As DTOYearMonth) As Integer
        Dim iFirstMonth As Integer = 12 * oFirstYearMonth.Year + oFirstYearMonth.Month
        Dim iSecondMonth As Integer = 12 * oSecondYearMonth.Year + oSecondYearMonth.Month
        Dim retval As Integer = iSecondMonth - iFirstMonth
        Return retval
    End Function

    Shared Function Max(values As IEnumerable(Of DTOYearMonth)) As DTOYearMonth
        Dim year = values.Max(Function(x) x.year)
        Dim month = values.Where(Function(x) x.year = year).Max(Function(y) y.month)
        Dim retval = values.FirstOrDefault(Function(x) x.year = year And x.month = month)
        Return retval
    End Function

    Shared Function Min(values As IEnumerable(Of DTOYearMonth)) As DTOYearMonth
        Dim year = values.Min(Function(x) x.year)
        Dim month = values.Where(Function(x) x.year = year).Min(Function(y) y.month)
        Dim retval = values.FirstOrDefault(Function(x) x.year = year And x.month = month)
        Return retval
    End Function
End Class
