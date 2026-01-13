Public Class TimeHelper

    Shared Function FirstDayOfMonth(DtFch As Date) As Date
        Dim retval As Date = DtFch.AddDays(-DtFch.Day + 1)
        Return retval
    End Function

    Shared Function LastDayOfMonth(DtFch As Date) As Date
        Dim DtFirstDayofMonth As Date = FirstDayOfMonth(DtFch)
        Dim retval As Date = DtFirstDayofMonth.AddMonths(1).AddDays(-1)
        Return retval
    End Function


    Shared Function FirstDayOfQuarter(DtFch As Date) As Date
        Dim retval As Date
        Dim q As Integer = DatePart(DateInterval.Quarter, DtFch)
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
        Dim q As Integer = DatePart(DateInterval.Quarter, DtFch)
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

    Shared Sub GetLastQuarter(ByRef iYea As Integer, ByRef iQuarter As Integer)
        Dim DtFch As Date = Today
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


End Class
