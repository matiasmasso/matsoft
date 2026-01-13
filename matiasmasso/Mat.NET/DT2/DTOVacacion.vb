Public Class DTOVacacion
    Property MonthDayFrom As DTOMonthDay
    Property MonthDayTo As DTOMonthDay
    Property MonthDayResult As DTOMonthDay

    Public Enum Segments
        FromMes
        FromDia
        UntilMes
        UntilDia
        ForwardMes
        ForwardDia
    End Enum

    Shared Function Match(oVacacion As DTOVacacion, SrcVto As Date) As Boolean
        Dim FchFrom As New Date(SrcVto.Year, oVacacion.MonthDayFrom.Month, oVacacion.MonthDayFrom.Day)
        Dim FchTo As New Date(SrcVto.Year, oVacacion.MonthDayTo.Month, oVacacion.MonthDayTo.Day)
        If FchTo < FchFrom Then FchTo = FchTo.AddYears(1)
        Dim retval As Boolean = (SrcVto >= FchFrom AndAlso SrcVto <= FchTo)
        Return retval
    End Function

    Shared Function Text(item As DTOVacacion, oLang As DTOLang) As String
        Dim sb As New Text.StringBuilder
        sb.AppendFormat("{0} {1:00}/{2:00}", oLang.tradueix("del", "del", "from"), item.MonthDayFrom.Day, item.MonthDayFrom.Month)
        sb.AppendFormat(" {0} {1:00}/{2:00}", oLang.tradueix("al", "al", "to"), item.MonthDayTo.Day, item.MonthDayTo.Month)
        If item.MonthDayResult.Month = 0 And item.MonthDayResult.Day = 0 Then
            sb.Append(oLang.tradueix(" aplaza 30 dias", " aplaça 30 dies", " add 30 days"))
        Else
            sb.AppendFormat(" {0} {1:00}/{2:00}", oLang.tradueix("aplaza al", "aplaça al", "delay to"), item.MonthDayResult.Day, item.MonthDayResult.Month)
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Function Result(items As List(Of DTOVacacion), SrcVto As Date) As Date
        Dim retval As Date = SrcVto
        If items IsNot Nothing Then
            For Each item As DTOVacacion In items
                Dim FchFrom As New Date(SrcVto.Year, item.MonthDayFrom.Month, item.MonthDayFrom.Day)
                Dim FchTo As New Date(SrcVto.Year, item.MonthDayTo.Month, item.MonthDayTo.Day)
                If FchTo < FchFrom Then FchTo = FchTo.AddYears(1)
                If SrcVto >= FchFrom AndAlso SrcVto <= FchTo Then
                    If item.MonthDayResult.Month = 0 Then
                        If item.MonthDayResult.Day = 0 Then
                            'default: aplaza un mes
                            retval = SrcVto.AddMonths(1)
                        Else
                            'mismo dia del mes indicado
                            retval = New Date(FchTo.Year, item.MonthDayResult.Month, SrcVto.Day)
                        End If
                    Else
                        retval = New Date(FchTo.Year, item.MonthDayResult.Month, item.MonthDayResult.Day)
                    End If
                    Exit For
                End If
            Next
        End If
        Return retval
    End Function
End Class
