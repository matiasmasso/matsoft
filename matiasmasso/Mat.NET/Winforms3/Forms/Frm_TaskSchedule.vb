Public Class Frm_TaskSchedule
    Private _TaskSchedule As DTOTaskSchedule
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOTaskSchedule)
        MyBase.New()
        Me.InitializeComponent()
        _TaskSchedule = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.TaskSchedule.Load(_TaskSchedule, exs) Then
            With _TaskSchedule

                TextBoxTask.Text = .Task.nom
                CheckBoxEnabled.Checked = .enabled
                If .mode = DTOTaskSchedule.Modes.interval Then
                    RadioButtonInterval.Checked = True
                    NumericUpDownInterval.Value = .timeInterval.TotalMinutes
                    NumericUpDownInterval.Enabled = True
                    NumericUpDownHours.Enabled = False
                    NumericUpDownMinutes.Enabled = False
                Else
                    RadioButtonDaily.Checked = True
                    NumericUpDownHours.Value = .timeInterval.Hours
                    NumericUpDownMinutes.Value = .timeInterval.Minutes
                    NumericUpDownInterval.Enabled = False
                    NumericUpDownHours.Enabled = True
                    NumericUpDownMinutes.Enabled = True
                End If

                'GroupBoxWeekdays.Enabled = .Mode = DTOTaskSchedule.Modes.GivenTime
                CheckBoxMon.Checked = .WeekDays(1)
                CheckBoxTue.Checked = .WeekDays(2)
                CheckBoxWed.Checked = .WeekDays(3)
                CheckBoxThu.Checked = .WeekDays(4)
                CheckBoxFri.Checked = .WeekDays(5)
                CheckBoxSat.Checked = .WeekDays(6)
                CheckBoxSun.Checked = .WeekDays(0)

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        CheckBoxEnabled.CheckedChanged,
         RadioButtonInterval.CheckedChanged,
          RadioButtonDaily.CheckedChanged,
           NumericUpDownInterval.ValueChanged,
            NumericUpDownHours.ValueChanged,
             NumericUpDownMinutes.ValueChanged,
             CheckBoxMon.CheckedChanged,
              CheckBoxTue.CheckedChanged,
               CheckBoxWed.CheckedChanged,
                CheckBoxThu.CheckedChanged,
                 CheckBoxFri.CheckedChanged,
                  CheckBoxSat.CheckedChanged,
                   CheckBoxSun.CheckedChanged
        If _AllowEvents Then
            UpdateWithFormValues()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Delegate Sub DelegateSetNextRun(sFch As String, sTimeSpan As String)

    Private Sub SetNextRun(sFch As String, sTimeSpan As String)
        If InvokeRequired Then
            Try
                Invoke(Sub() TextBoxNextRunFch.Text = sFch)
                Invoke(Sub() LabelTimeSpan.Text = sTimeSpan)
            Catch ex As Exception

            End Try
        Else
            TextBoxNextRunFch.Text = sFch
            LabelTimeSpan.Text = sTimeSpan
        End If

    End Sub


    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles _
            RadioButtonInterval.CheckedChanged

        If _AllowEvents Then
            NumericUpDownInterval.Enabled = RadioButtonInterval.Checked
            NumericUpDownHours.Enabled = RadioButtonDaily.Checked
            NumericUpDownMinutes.Enabled = RadioButtonDaily.Checked
            'GroupBoxWeekdays.Enabled = RadioButtonDaily.Checked
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UpdateWithFormValues()

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.TaskSchedule.Update(_TaskSchedule, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_TaskSchedule))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub UpdateWithFormValues()
        With _TaskSchedule
            .Enabled = CheckBoxEnabled.Checked
            .WeekDays = {
                    CheckBoxSun.Checked,
                    CheckBoxMon.Checked,
                    CheckBoxTue.Checked,
                    CheckBoxWed.Checked,
                    CheckBoxThu.Checked,
                    CheckBoxFri.Checked,
                    CheckBoxSat.Checked}
            If RadioButtonInterval.Checked Then
                .mode = DTOTaskSchedule.Modes.interval
                .timeInterval = New TimeSpan(CInt(NumericUpDownInterval.Value / 60), CInt(NumericUpDownInterval.Value Mod 60), 0)
            Else
                .mode = DTOTaskSchedule.Modes.givenTime
                .timeInterval = New TimeSpan(NumericUpDownHours.Value, NumericUpDownMinutes.Value, 0)
            End If
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.TaskSchedule.Delete(_TaskSchedule, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_TaskSchedule))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub
End Class


