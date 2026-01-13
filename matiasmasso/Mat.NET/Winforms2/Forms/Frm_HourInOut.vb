Public Class Frm_HourInOut
    Private _value As DTOHourInOut
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToDelete(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOHourInOut)
        MyBase.New()
        InitializeComponent()
        _value = value
    End Sub

    Private Sub Frm_StaffLogTemplateItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePickerFrom.Value = New Date(2000, 1, 1, _value.HourFrom, _value.MinuteFrom, 0)
        DateTimePickerTo.Value = New Date(2000, 1, 1, _value.HourTo, _value.MinuteTo, 0)
    End Sub


    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles DateTimePickerFrom.ValueChanged,
            DateTimePickerTo.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
            ButtonDel.Enabled = False
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        RaiseEvent RequestToDelete(Me, New MatEventArgs(_value))
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _value
            .HourFrom = DateTimePickerFrom.Value.Hour
            .MinuteFrom = DateTimePickerFrom.Value.Minute
            .HourTo = DateTimePickerTo.Value.Hour
            .MinuteTo = DateTimePickerTo.Value.Minute
        End With
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
        Me.Close()
    End Sub
End Class