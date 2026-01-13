Public Class Xl_StaffSchedItems
    Private _value As DTOStaffSched
    Private _cod As DTOStaffSched.Item.Cods
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Periods
        Matins
        Tardes
    End Enum

    Public Shadows Sub Load(value As DTOStaffSched, oCod As DTOStaffSched.Item.Cods)
        _value = value
        _cod = oCod
        SetEventHandlers()
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOStaffSched.Item)
        Get
            Dim retval As New List(Of DTOStaffSched.Item)
            For iWeekday As Integer = 1 To 7
                If CheckboxWD(iWeekday).Checked Then
                    Dim item = GetItem(iWeekday, Periods.Matins)
                    If item.HasValue Then retval.Add(item)

                    If _cod = DTOStaffSched.Item.Cods.Ordinari Then
                        item = GetItem(iWeekday, Periods.Tardes)
                        If item.HasValue Then retval.Add(item)
                    End If
                End If
            Next
            Return retval
        End Get
    End Property

    Private Sub Refresca()
        If _cod = DTOStaffSched.Item.Cods.Intensiu Then GroupBoxTardes.Visible = False
        For i As Integer = 1 To 7
            Dim iWeekday As Integer = i
            Dim checkboxName = String.Format("CheckboxWD{0}", iWeekday)
            Dim checkboxWd = DirectCast(Me.Controls(checkboxName), CheckBox)
            Dim items = _value.Items.Where(Function(x) x.Cod = _cod And x.weekDay = iWeekday).ToList
            Select Case items.Count
                Case 0
                    checkboxWd.Checked = False
                Case 1
                    checkboxWd.Checked = True
                    Dim item As DTOStaffSched.Item = items.First
                    If item.Matins Then
                        LoadItem(item, Periods.Matins)
                    Else
                        LoadItem(item, Periods.Tardes)
                    End If
                Case Else
                    checkboxWd.Checked = True
                    LoadItem(items(0), Periods.Matins)
                    LoadItem(items(1), Periods.Tardes)
            End Select
        Next
        _AllowEvents = True
    End Sub

    Private Sub LoadItem(item As DTOStaffSched.Item, oPeriod As Periods)
        Dim sKey = IIf(oPeriod = Periods.Matins, "M", "T")
        GetNumericUpDown(item.weekDay, sKey & "EH").Value = item.FromHour
        GetNumericUpDown(item.weekDay, sKey & "EM").Value = item.FromMinutes
        GetNumericUpDown(item.weekDay, sKey & "SH").Value = item.ToHour
        GetNumericUpDown(item.weekDay, sKey & "SM").Value = item.ToMinutes
    End Sub


    Private Function GetItem(iWeekDay As Integer, operiod As Periods) As DTOStaffSched.Item
        Dim retval = DTOStaffSched.Item.Factory(_cod)
        Dim sKey = IIf(operiod = Periods.Matins, "M", "T")
        With retval
            .weekDay = iWeekDay
            .FromHour = GetNumericUpDown(iWeekDay, sKey & "EH").Value
            .FromMinutes = GetNumericUpDown(iWeekDay, sKey & "EM").Value
            .ToHour = GetNumericUpDown(iWeekDay, sKey & "SH").Value
            .ToMinutes = GetNumericUpDown(iWeekDay, sKey & "SM").Value
        End With
        Return retval
    End Function

    Private Function CheckboxWD(iWeekday As Integer) As CheckBox
        Dim controlName = String.Format("CheckBoxWD{0}", iWeekday)
        Dim retval = DirectCast(Me.Controls(controlName), CheckBox)
        Return retval
    End Function

    Private Function GetNumericUpDown(iWeekDay As Integer, key As String) As NumericUpDown
        Dim controlName = String.Format("NumericUpDown{0}{1}", iWeekDay, key)
        Dim oGroupBox = IIf(key.StartsWith("M"), GroupBoxMatins, GroupBoxTardes)
        Dim retval = DirectCast(oGroupBox.Controls(controlName), NumericUpDown)
        Return retval
    End Function


    Private Sub CheckBoxWD1_CheckedChanged(sender As Object, e As EventArgs) Handles _
            CheckBoxWD1.CheckedChanged, CheckBoxWD2.CheckedChanged, CheckBoxWD3.CheckedChanged, CheckBoxWD4.CheckedChanged, CheckBoxWD5.CheckedChanged, CheckBoxWD6.CheckedChanged, CheckBoxWD7.CheckedChanged
        Dim oCheckbox As CheckBox = sender
        Dim lastLetter As String = oCheckbox.Name(oCheckbox.Name.Length - 1)
        Dim iWeekday = CInt(lastLetter)
        GetNumericUpDown(iWeekday, "MEH").Visible = CheckboxWD(iWeekday).Checked
        GetNumericUpDown(iWeekday, "MEM").Visible = CheckboxWD(iWeekday).Checked
        GetNumericUpDown(iWeekday, "MSH").Visible = CheckboxWD(iWeekday).Checked
        GetNumericUpDown(iWeekday, "MSM").Visible = CheckboxWD(iWeekday).Checked
        GetNumericUpDown(iWeekday, "TEH").Visible = CheckboxWD(iWeekday).Checked
        GetNumericUpDown(iWeekday, "TEM").Visible = CheckboxWD(iWeekday).Checked
        GetNumericUpDown(iWeekday, "TSH").Visible = CheckboxWD(iWeekday).Checked
        GetNumericUpDown(iWeekday, "TSM").Visible = CheckboxWD(iWeekday).Checked

        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Values))
        End If
    End Sub

    Private Sub SetEventHandlers()
        For Each oControl In Me.Controls
            If TypeOf oControl Is NumericUpDown Then
                AddHandler DirectCast(oControl, NumericUpDown).ValueChanged, AddressOf Control_Changed
            End If
        Next
    End Sub

    Private Sub Control_Changed()
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub
End Class
