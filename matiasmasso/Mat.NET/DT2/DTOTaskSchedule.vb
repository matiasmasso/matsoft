Public Class DTOTaskSchedule
    Inherits DTOBaseGuid

    Property Task As DTOTask
    Property Enabled As Boolean
    Property WeekDays As Boolean()
    Property Mode As Modes
    <JsonIgnore> Property TimeInterval As TimeSpan

    'versió de TimeInterval per enviar en JSON per la Api
    Property ISO8601
        Get
            Return TimeHelper.ISO8601(_timeInterval)
        End Get
        Set(value)
            _timeInterval = TimeHelper.fromISO8601(value)
        End Set
    End Property

    Public Enum Modes
        givenTime
        interval
    End Enum


    Public Sub New()
        MyBase.New()
        _weekDays = {True, True, True, True, True, True, True}
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _weekDays = {True, True, True, True, True, True, True}
    End Sub



    Public Function FrequencyText() As String
        Dim retval As String = ""
        Select Case Me.Mode()
            Case DTOTaskSchedule.Modes.Interval
                retval = GetIntervalText()
            Case Else
                retval = GetWeekDaysText()
        End Select
        Return retval
    End Function

    Private Function GetIntervalText() As String
        Dim retval As String = ""
        If _timeInterval.TotalMinutes = 1 Then
            retval = "cada minuto"
        Else
            retval = "cada " & _timeInterval.TotalMinutes.ToString & " minutos"
        End If
        Return retval
    End Function

    Private Function GetWeekDaysText() As String
        Dim oLang As New DTOLang(DTOLang.Ids.CAT)
        Dim DaysEnabled As New ArrayList
        Dim DaysDisabled As New ArrayList

        For i As Integer = System.DayOfWeek.Sunday To System.DayOfWeek.Saturday
            If _WeekDays(i) Then
                DaysEnabled.Add(i)
            Else
                DaysDisabled.Add(i)
            End If
        Next

        Dim sb As New System.Text.StringBuilder
        If DaysEnabled.Count >= DaysDisabled.Count Then
            If DaysDisabled.Count = 0 Then
                sb.Append("tots els dies de la setmana")
            Else
                sb.Append("tots els dies menys ")
                sb.Append(oLang.WeekDay(DaysDisabled(0)))
                If DaysDisabled.Count > 1 Then
                    If DaysDisabled.Count = 2 Then sb.Append(" i ") Else sb.Append(", ")
                    sb.Append(oLang.WeekDay(DaysDisabled(1)))
                    If DaysDisabled.Count > 2 Then
                        sb.Append(" i ")
                        sb.Append(oLang.WeekDay(DaysDisabled(2)))
                    End If
                End If
            End If
        Else
            If DaysEnabled.Count = 0 Then
                sb.Append("(cap dia)")
            Else
                sb.Append("cada ")
                sb.Append(oLang.WeekDay(DaysEnabled(0)))
                If DaysEnabled.Count > 1 Then
                    If DaysEnabled.Count = 2 Then sb.Append(" i ") Else sb.Append(", ")
                    sb.Append(oLang.WeekDay(DaysEnabled(1)))
                    If DaysEnabled.Count > 2 Then
                        sb.Append(" i ")
                        sb.Append(oLang.WeekDay(DaysEnabled(2)))
                    End If
                End If
            End If
        End If

        Dim retVal As String = sb.ToString
        Return retVal
    End Function



    Shared Function SpanText(DtFch As DateTimeOffset, oLang As DTOLang) As String
        Dim retval As String = ""
        If DtFch <> Nothing Then
            Dim oTimeSpan As TimeSpan = DtFch - DateTimeOffset.Now
            retval = SpanText(oTimeSpan, oLang)
        End If
        Return retval
    End Function


    Shared Function SpanText(oTimeSpan As TimeSpan, oLang As DTOLang) As String
        Dim retval As String = ""
        If oTimeSpan <> Nothing Then
            If oTimeSpan.Days > 0 Then
                retval = String.Format("d'aqui a {0} dies {1} hores {2} min {3}", oTimeSpan.Days, oTimeSpan.Hours, oTimeSpan.Minutes, oTimeSpan.Seconds)
            ElseIf oTimeSpan.Hours > 2 Then
                retval = String.Format("d'aqui a {0} hores {1} min {2}", oTimeSpan.Hours, oTimeSpan.Minutes, oTimeSpan.Seconds)
            ElseIf oTimeSpan.Hours > 1 Then
                retval = String.Format("d'aqui a 1 hora {0} min {1}", oTimeSpan.Minutes, oTimeSpan.Seconds)
            ElseIf oTimeSpan.Minutes > 0 Then
                retval = String.Format("d'aqui a {0} minuts {1}", oTimeSpan.Minutes, oTimeSpan.Seconds)
            ElseIf oTimeSpan.Seconds > 0 Then
                retval = String.Format("d'aqui a {0} segons", oTimeSpan.Seconds)
            Else
                retval = "inminent"
            End If
        End If
        Return retval
    End Function

    Public Function EncodedWeekdays() As String
        Dim sb As New System.Text.StringBuilder
        For Each item As Boolean In _WeekDays
            sb.Append(If(item, "1", "0"))
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function DecodedWeekdays(src As String) As Boolean()
        Dim retval As Boolean() = {(src.Substring(0, 1) = "1"),
            (src.Substring(1, 1) = "1"),
            (src.Substring(2, 1) = "1"),
            (src.Substring(3, 1) = "1"),
            (src.Substring(4, 1) = "1"),
            (src.Substring(5, 1) = "1"),
            (src.Substring(6, 1) = "1")}
        Return retval
    End Function
End Class
