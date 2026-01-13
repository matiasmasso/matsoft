

Public Class TimeHelper

    Public Enum DateIntervals
        Year
        Quarter
        Month
        DayOfYear
        Day
        WeekOfYear
        WeekDay
        Hour
        Minute
        Second
    End Enum

    Shared Function fromISO8601(ByVal value As String) As TimeSpan
        Dim retval = System.Xml.XmlConvert.ToTimeSpan(value)
        Return retval
    End Function

    Shared Function ISO8601(ByVal value As TimeSpan) As String
        Dim retval = System.Xml.XmlConvert.ToString(value)
        Return retval
    End Function

    Shared Function ISO8601(ByVal minutes As Integer) As String
        Dim hours As Integer = minutes / 60
        Dim mins As Integer = minutes Mod 60
        Dim oTimeSpan As New TimeSpan(hours, mins, 0)
        Dim retval = System.Xml.XmlConvert.ToString(oTimeSpan)
        Return retval
    End Function

    Shared Function FirstDayOfMonth(DtFch As Date) As Date
        Dim retval As Date = DtFch.AddDays(-DtFch.Day + 1)
        Return retval
    End Function

    Shared Function LastDayOfMonth(DtFch As Date) As Date
        Dim DtFirstDayofMonth As Date = FirstDayOfMonth(DtFch)
        Dim retval As Date = DtFirstDayofMonth.AddMonths(1).AddDays(-1)
        Return retval
    End Function

    Shared Function Quarter(DtFch As DateTime) As Integer
        Dim retval = Math.Truncate((DtFch.Month + 2) / 3)
        Return retval
    End Function

    Shared Function FirstDayOfQuarter(DtFch As Date) As Date
        Dim retval As Date
        Dim q As Integer = Quarter(DtFch)
        Select Case q
            Case 1
                retval = New Date(DtFch.Year, 1, 1)
            Case 2
                retval = New Date(DtFch.Year, 4, 1)
            Case 3
                retval = New Date(DtFch.Year, 7, 1)
            Case 4
                retval = New Date(DtFch.Year, 10, 1)
        End Select
        Return retval
    End Function

    Shared Function LastDayOfQuarter(DtFch As Date) As Date
        Dim retval As Date
        Dim q As Integer = Quarter(DtFch)
        Select Case q
            Case 1
                retval = New Date(DtFch.Year, 3, 31)
            Case 2
                retval = New Date(DtFch.Year, 6, 30)
            Case 3
                retval = New Date(DtFch.Year, 9, 30)
            Case 4
                retval = New Date(DtFch.Year, 12, 31)
        End Select
        Return retval
    End Function

    Shared Sub GetLastQuarter(ByRef iYea As Integer, ByRef iQuarter As Integer, today As Date)
        Dim DtFch As Date = today
        iYea = DtFch.Year
        Select Case DtFch.Month
            Case 1 To 3
                iYea = iYea - 1
                iQuarter = 4
            Case 4 To 6
                iQuarter = 1
            Case 7 To 9
                iQuarter = 2
            Case 10 To 12
                iQuarter = 3
        End Select
    End Sub

    Shared Function FormatedQuarter(ByVal DtFch As DateTime) As Integer
        Dim retval As String = String.Format("{0}{1}", DtFch.Year, Quarter(DtFch))
        Return retval
    End Function

    Shared Function LastDayOfYear(DtFch As Date) As Date
        Dim FirstDayCurrentMonth As Date = DtFch.AddDays(-DtFch.Day + 1)
        Dim retval As New Date(DtFch.Year, 12, 31)
        Return retval
    End Function

    Shared Function Overlaps(SrcFrom As Date, SrcTo As Date, DestFrom As Date, DestTo As Date) As Boolean
        'explore next 3 scenarios:
        Dim ContainsStart As Boolean = SrcFrom >= DestFrom And SrcFrom <= DestTo
        Dim ContainsEnd As Boolean = SrcTo >= DestFrom And SrcTo <= DestTo
        Dim WrapsRange As Boolean = SrcFrom < DestFrom And SrcTo > DestTo

        Dim retval As Boolean = ContainsStart Or ContainsEnd Or WrapsRange
        Return retval
    End Function

    Shared Function DiesHabils(DtFch1 As Date, DtFch2 As Date) As Integer
        Dim retval As Integer
        Dim DtFch As Date = DtFch1.AddDays(1)
        Do Until DtFch = DtFch2
            Select Case DtFch.DayOfWeek
                Case DayOfWeek.Saturday, DayOfWeek.Sunday
                Case Else
                    retval += 1
            End Select
            DtFch = DtFch.AddDays(1)
        Loop
        Return retval
    End Function

    Shared Function AddDiasHabils(DtFch As Date, iDiasHabils As Integer) As Date
        Dim retval As Date = DtFch
        Do Until iDiasHabils = 0
            If iDiasHabils > 0 Then
                retval = retval.AddDays(1)
            Else
                retval = retval.AddDays(-1)
            End If
            Select Case retval.DayOfWeek
                Case DayOfWeek.Saturday, DayOfWeek.Sunday
                Case Else
                    If iDiasHabils > 0 Then
                        iDiasHabils -= 1
                    Else
                        iDiasHabils += 1
                    End If
            End Select
        Loop
        Return retval
    End Function

    Shared Function ParseFchSpain(src As String) As Date
        Dim retval As Date = Nothing
        If Not String.IsNullOrEmpty(src) Then
            Dim segments = src.Split("/")
            If segments.Length >= 3 Then
                Dim day = CInt(segments(0))
                Dim month = CInt(segments(1))
                Dim year = CInt(TextHelper.VbLeft(segments(2), 4))
                retval = New Date(year, month, day)
            End If
        End If
        Return retval
    End Function

    Shared Function bottomDate() As Date
        Dim retval As Date = Nothing
        Return retval
    End Function


    Shared Function monthsdiff(earlierDate As Date, laterDate As Date) As Integer
        Dim retval = daysdiff(earlierDate, laterDate) / (365.25 / 12)
        Return retval
    End Function

    Shared Function daysdiff(earlierDate As Date, laterDate As Date) As Integer
        Dim interval = laterDate - earlierDate
        Dim retval = interval.TotalDays
        Return retval
    End Function

    Shared Function hoursdiff(earlierDate As Date, laterDate As Date) As Integer
        Dim interval = laterDate - earlierDate
        Dim retval = interval.TotalHours
        Return retval
    End Function

    Shared Function WeekNumber(dtFch As Date) As Integer
        Dim ci = System.Globalization.CultureInfo.GetCultureInfo("es-ES")
        Dim retval = ci.Calendar.GetWeekOfYear(dtFch, Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)
        Return retval
    End Function

    Shared Function IsBetween(fchStart As Date, value As Date, fchEnd As Date) As Boolean
        Dim retval As Boolean = False
        If fchEnd = Nothing Then
            retval = fchStart <= value
        Else
            retval = (fchStart <= value And value <= fchEnd)
        End If
        Return retval
    End Function
End Class
