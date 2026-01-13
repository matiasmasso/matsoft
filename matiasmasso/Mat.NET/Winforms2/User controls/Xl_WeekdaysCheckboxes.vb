Public Class Xl_WeekdaysCheckboxes
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public ReadOnly Property WeekDays As List(Of Boolean)
        Get
            Dim retval As New List(Of Boolean)
            retval.Add(CheckBox7.Checked) 'Base 0 sunday
            retval.Add(CheckBox1.Checked)
            retval.Add(CheckBox2.Checked)
            retval.Add(CheckBox3.Checked)
            retval.Add(CheckBox4.Checked)
            retval.Add(CheckBox5.Checked)
            retval.Add(CheckBox6.Checked)
            Return retval
        End Get
    End Property

    Public Shadows Sub Load(weekdays As List(Of Boolean))
        _AllowEvents = False
        CheckBox1.Checked = weekdays(1)
        CheckBox2.Checked = weekdays(2)
        CheckBox3.Checked = weekdays(3)
        CheckBox4.Checked = weekdays(4)
        CheckBox5.Checked = weekdays(5)
        CheckBox6.Checked = weekdays(6)
        CheckBox7.Checked = weekdays(0)
        _AllowEvents = True
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged,
            CheckBox2.CheckedChanged,
            CheckBox3.CheckedChanged,
            CheckBox4.CheckedChanged,
            CheckBox5.CheckedChanged,
            CheckBox6.CheckedChanged,
            CheckBox7.CheckedChanged

        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.WeekDays))
        End If
    End Sub
End Class
