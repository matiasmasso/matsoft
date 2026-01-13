

Public Class Frm_TaskSchedule
    Private mSchedule As TaskSchedule
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterDelete(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oSchedule As TaskSchedule)
        MyBase.New()
        Me.InitializeComponent()
        mSchedule = oSchedule
        With mSchedule
            LabelNom.Text = TaskSchedule.GetNomFromWeekDays(.WeekDays)
            CheckBoxEnabled.Checked = .Enabled

            If .Mode = TaskSchedule.Modes.GivenTime Then
                GroupBoxGivenTime.Enabled = True
                GroupBoxInterval.Enabled = False
                RadioButtonGivenTime.Checked = True
                RadioButtonIntervals.Checked = False

                NumericUpDownHour.Value = .TimeSpan.Hours
                NumericUpDownMinutes.Value = .TimeSpan.Minutes

                Dim BlWholeWeek As Boolean = True
                For i As Integer = 0 To 6
                    CheckedListBoxWeekDays.SetItemChecked(i, .WeekDays(i))
                    If Not .WeekDays(i) Then BlWholeWeek = False
                Next
                CheckBoxAllWeekDays.Checked = BlWholeWeek
                CheckedListBoxWeekDays.Visible = Not BlWholeWeek
            ElseIf .Mode = TaskSchedule.Modes.Interval Then
                GroupBoxGivenTime.Enabled = False
                GroupBoxInterval.Enabled = True
                RadioButtonGivenTime.Checked = False
                RadioButtonIntervals.Checked = True

                NumericUpDownInterval.Value = .Interval
            Else
                RadioButtonGivenTime.Checked = False
                RadioButtonIntervals.Checked = False
                GroupBoxGivenTime.Enabled = False
                GroupBoxInterval.Enabled = False
            End If

            ButtonDel.Enabled = Not .Guid.Equals(Guid.Empty)
        End With
        mAllowEvents = True
    End Sub



    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        RaiseEvent AfterDelete(sender, e)
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mSchedule
            .TimeSpan = New TimeSpan(NumericUpDownHour.Value, NumericUpDownMinutes.Value, 0)
            .Enabled = CheckBoxEnabled.Checked
            .WeekDays = GetWeekDaysFromForm()
            If RadioButtonIntervals.Checked Then
                .Interval = NumericUpDownInterval.Value
            Else
                .Interval = 0
            End If
        End With

        RaiseEvent AfterUpdate(mSchedule, e)
        Me.Close()

    End Sub

    Private Function GetWeekDaysFromForm() As Boolean()
        Dim oWeekDays(7) As Boolean
        For i As Integer = 0 To 6
            If CheckBoxAllWeekDays.Checked Then
                oWeekDays(i) = True
            Else
                oWeekDays(i) = CheckedListBoxWeekDays.GetItemChecked(i)
            End If
        Next
        Return oWeekDays
    End Function

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     NumericUpDownHour.ValueChanged, _
      NumericUpDownMinutes.ValueChanged, _
       CheckBoxEnabled.CheckedChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub CheckedListBoxWeekDays_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBoxWeekDays.ItemCheck
        If mAllowEvents Then
            Dim oWeekDays As Boolean() = GetWeekDaysFromForm()
            oWeekDays(e.Index) = e.NewValue
            LabelNom.Text = TaskSchedule.GetNomFromWeekDays(oWeekDays)
        End If
        ButtonOk.Enabled = True
    End Sub


    Private Sub CheckBoxAllWeekDays_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxAllWeekDays.CheckedChanged
        If mAllowEvents Then
            CheckedListBoxWeekDays.Visible = Not CheckBoxAllWeekDays.Checked
        End If
    End Sub

    Private Sub RadioButton_Click(sender As Object, e As System.EventArgs) Handles _
        RadioButtonGivenTime.Click, RadioButtonIntervals.Click

        If RadioButtonGivenTime.Checked Then
            GroupBoxGivenTime.Enabled = True
            GroupBoxInterval.Enabled = False
        Else
            GroupBoxGivenTime.Enabled = False
            GroupBoxInterval.Enabled = True
        End If

        ButtonOk.Enabled = True
    End Sub
End Class