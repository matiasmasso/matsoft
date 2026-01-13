Public Class Frm_PgcSaldo2s
    Private AllowEvents As Boolean
    Private Enum Period1
        PreviousMonth
        SameMonthPreviousYear
        PreviousQuarter
        SameQuarterPreviousYear
        Custom
    End Enum

    Private Enum Period2
        CurrentMonth
        LastMonth
        CurrentQuarter
        LastQuarter
        Custom
    End Enum

    Private Sub Frm_PgcSaldo2s_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetPeriods()
        SetFchs()
        AllowEvents = True
    End Sub

    Private Sub SetPeriods()
        ComboBox1.SelectedIndex = Period1.PreviousMonth
        ComboBox2.SelectedIndex = Period2.LastMonth
    End Sub

    Private Sub SetFchs()
        Select Case ComboBox2.SelectedIndex
            Case Period2.CurrentMonth
                DateTimePicker2a.Value = TimeHelper.FirstDayOfMonth(Today)
                DateTimePicker2b.Value = TimeHelper.LastDayOfMonth(Today)
                DateTimePicker2a.Enabled = False
                DateTimePicker2b.Enabled = False
            Case Period2.LastMonth
                DateTimePicker2a.Value = TimeHelper.FirstDayOfMonth(Today.AddMonths(-1))
                DateTimePicker2b.Value = TimeHelper.LastDayOfMonth(Today.AddMonths(-1))
                DateTimePicker2a.Enabled = False
                DateTimePicker2b.Enabled = False
            Case Period2.CurrentQuarter
                DateTimePicker2a.Value = TimeHelper.FirstDayOfQuarter(Today)
                DateTimePicker2b.Value = TimeHelper.LastDayOfQuarter(Today)
                DateTimePicker2a.Enabled = False
                DateTimePicker2b.Enabled = False
            Case Period2.LastQuarter
                DateTimePicker2a.Value = TimeHelper.FirstDayOfQuarter(Today.AddMonths(-3))
                DateTimePicker2b.Value = TimeHelper.LastDayOfQuarter(Today.AddMonths(-3))
                DateTimePicker2a.Enabled = False
                DateTimePicker2b.Enabled = False
            Case Period2.Custom
                DateTimePicker2a.Enabled = True
                DateTimePicker2b.Enabled = True
        End Select
        Select Case ComboBox1.SelectedIndex
            Case Period1.PreviousMonth
                DateTimePicker1a.Value = TimeHelper.FirstDayOfMonth(DateTimePicker2a.Value.AddMonths(-1))
                DateTimePicker1b.Value = TimeHelper.LastDayOfMonth(DateTimePicker2a.Value.AddMonths(-1))
                DateTimePicker1a.Enabled = False
                DateTimePicker1b.Enabled = False
            Case Period1.PreviousQuarter
                DateTimePicker1a.Value = TimeHelper.FirstDayOfQuarter(DateTimePicker2a.Value.AddMonths(-3))
                DateTimePicker1b.Value = TimeHelper.LastDayOfQuarter(DateTimePicker2a.Value.AddMonths(-3))
                DateTimePicker1a.Enabled = False
                DateTimePicker1b.Enabled = False
            Case Period1.SameMonthPreviousYear
                DateTimePicker1a.Value = TimeHelper.FirstDayOfMonth(DateTimePicker2a.Value.AddYears(-1))
                DateTimePicker1b.Value = TimeHelper.LastDayOfMonth(DateTimePicker2a.Value.AddYears(-1))
                DateTimePicker1a.Enabled = False
                DateTimePicker1b.Enabled = False
            Case Period1.SameQuarterPreviousYear
                DateTimePicker1a.Value = TimeHelper.FirstDayOfQuarter(DateTimePicker2a.Value.AddYears(-1))
                DateTimePicker1b.Value = TimeHelper.LastDayOfQuarter(DateTimePicker2a.Value.AddYears(-1))
                DateTimePicker1a.Enabled = False
                DateTimePicker1b.Enabled = False
            Case Period1.Custom
                DateTimePicker1a.Enabled = True
                DateTimePicker1b.Enabled = True
        End Select

        Dim items As List(Of DTOPgcSaldo2) = BLLPgcSaldo2.All(Current.Session.Emp, DateTimePicker1a.Value, DateTimePicker1b.Value, DateTimePicker2a.Value, DateTimePicker2b.Value)
        Xl_PgcSaldo2s1.Load(items)
    End Sub

    Private Sub Period_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _
        ComboBox1.SelectedIndexChanged,
         ComboBox2.SelectedIndexChanged

        If AllowEvents Then SetFchs()
    End Sub
End Class