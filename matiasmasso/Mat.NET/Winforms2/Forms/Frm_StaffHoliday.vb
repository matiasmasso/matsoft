Public Class Frm_StaffHoliday

    Private _StaffHoliday As DTOStaffHoliday
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOStaffHoliday)
        MyBase.New()
        Me.InitializeComponent()
        _StaffHoliday = value

    End Sub

    Private Sub Frm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.StaffHoliday.Load(_StaffHoliday, exs) Then
            UIHelper.LoadComboFromEnum(ComboBoxCod, GetType(DTOStaffHoliday.Cods))
            With _StaffHoliday
                TextBoxTitularNom.Text = .TitularNom
                ComboBoxCod.SelectedValue = .Cod
                DateTimePickerFchFrom.Value = .FchFrom.Date
                If .HasSpecificHourFrom Then
                    CheckBoxHourFrom.Checked = True
                    DateTimePickerHourFrom.Visible = True
                    DateTimePickerHourFrom.Value = .FchFrom
                End If
                If .SeveralDays Then
                    CheckBoxMoreDays.Checked = True
                    ToggleSeveralDays()
                    DateTimePickerFchTo.Value = .FchTo.Date
                    If .HasSpecificHourTo Then
                        CheckBoxHourTo.Checked = True
                        DateTimePickerHourTo.Visible = True
                        DateTimePickerHourTo.Value = .FchFrom
                    End If
                End If
                TextBoxObs.Text = .Obs
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
             ComboBoxCod.SelectedIndexChanged,
              DateTimePickerFchFrom.ValueChanged,
                DateTimePickerHourFrom.ValueChanged,
                 CheckBoxMoreDays.CheckedChanged,
                  DateTimePickerFchTo.ValueChanged,
                    DateTimePickerHourTo.ValueChanged,
                     TextBoxObs.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxHoraFrom_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxHourFrom.CheckedChanged
        If _AllowEvents Then
            DateTimePickerHourFrom.Visible = CheckBoxHourFrom.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxHourTo_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxHourTo.CheckedChanged
        If _AllowEvents Then
            DateTimePickerHourTo.Visible = CheckBoxHourTo.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Function CurrentFchFrom() As DateTime
        Dim retval As DateTime = Nothing
        Dim DtFch = DateTimePickerFchFrom.Value
        If CheckBoxHourFrom.Checked Then
            Dim Hora = DateTimePickerHourFrom.Value
            retval = New DateTime(DtFch.Year, DtFch.Month, DtFch.Day, Hora.Hour, Hora.Minute, 0)
        Else
            retval = New DateTime(DtFch.Year, DtFch.Month, DtFch.Day, 0, 0, 0)
        End If
        Return retval
    End Function

    Private Function CurrentFchTo() As DateTime
        Dim retval As DateTime = Nothing
        If CheckBoxMoreDays.Checked Then
            Dim DtFch = DateTimePickerFchTo.Value
            If CheckBoxHourFrom.Checked Then
                Dim Hora = DateTimePickerHourTo.Value
                retval = New DateTime(DtFch.Year, DtFch.Month, DtFch.Day, Hora.Hour, Hora.Minute, 0)
            Else
                retval = New DateTime(DtFch.Year, DtFch.Month, DtFch.Day, 23, 59, 59)
            End If
        Else
            Dim DtFch = DateTimePickerFchFrom.Value
            retval = New DateTime(DtFch.Year, DtFch.Month, DtFch.Day, 23, 59, 59)
        End If
        Return retval
    End Function

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _StaffHoliday
            .Cod = ComboBoxCod.SelectedIndex
            .FchFrom = CurrentFchFrom()
            .FchTo = CurrentFchTo()
            .Obs = TextBoxObs.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.StaffHoliday.Update(_StaffHoliday, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_StaffHoliday))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.StaffHoliday.Delete(_StaffHoliday, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_StaffHoliday))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub CheckBoxMoreDays_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxMoreDays.CheckedChanged
        If _AllowEvents Then
            ToggleSeveralDays()
        End If
    End Sub

    Private Sub ToggleSeveralDays()
        LabelFchTo.Visible = CheckBoxMoreDays.Checked
        DateTimePickerFchTo.Visible = CheckBoxMoreDays.Checked
        CheckBoxHourTo.Visible = CheckBoxMoreDays.Checked
        DateTimePickerHourTo.Visible = CheckBoxHourTo.Checked
    End Sub


End Class