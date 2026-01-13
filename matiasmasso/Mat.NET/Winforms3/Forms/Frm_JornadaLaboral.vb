Public Class Frm_JornadaLaboral

    Private _JornadaLaboral As DTOJornadaLaboral
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOJornadaLaboral)
        MyBase.New()
        Me.InitializeComponent()
        _JornadaLaboral = value
    End Sub

    Private Sub Frm_JornadaLaboral_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.JornadaLaboral.Load(exs, _JornadaLaboral) Then
            With _JornadaLaboral
                Me.Text = "Registre Jornada Laboral " & .Staff.Nom
                DateTimePickerFch.Value = .FchFrom.Date()
                DateTimePickerHourFrom.Value = .FchFrom
                If Not .IsOpen Then
                    DateTimePickerHourTo.Visible = True
                    DateTimePickerHourTo.Value = .FchTo
                    CheckBoxSortida.Checked = True
                End If
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
        DateTimePickerFch.ValueChanged,
         DateTimePickerHourFrom.ValueChanged,
          DateTimePickerHourTo.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _JornadaLaboral
            .FchFrom = New DateTime(DateTimePickerFch.Value.Year, DateTimePickerFch.Value.Month, DateTimePickerFch.Value.Day, DateTimePickerHourFrom.Value.Hour, DateTimePickerHourFrom.Value.Minute, 0)
            If CheckBoxSortida.Checked Then
                .FchTo = New DateTime(DateTimePickerFch.Value.Year, DateTimePickerFch.Value.Month, DateTimePickerFch.Value.Day, DateTimePickerHourTo.Value.Hour, DateTimePickerHourTo.Value.Minute, 0)
            Else
                .FchTo = DateTime.MinValue
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.JornadaLaboral.Update(exs, _JornadaLaboral) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_JornadaLaboral))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
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
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.JornadaLaboral.Delete(exs, _JornadaLaboral) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_JornadaLaboral))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub CheckBoxSortida_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSortida.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
            DateTimePickerHourTo.Visible = CheckBoxSortida.Checked
        End If
    End Sub
End Class


