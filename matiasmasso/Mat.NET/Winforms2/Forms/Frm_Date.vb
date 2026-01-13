Public Class Frm_Date

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(Optional DtFch As Date = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        If DtFch = Nothing Then
            DateTimePicker1.Value = DTO.GlobalVariables.Today()
            ButtonOk.Enabled = True
        Else
            DateTimePicker1.Value = DtFch
        End If
        _AllowEvents = True
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If _AllowEvents Then
            DateTimePicker1.Visible = CheckBox1.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim DtFch As Date = Date.MinValue
        If CheckBox1.Checked Then DtFch = DateTimePicker1.Value
        RaiseEvent AfterUpdate(Me, New MatEventArgs(DtFch))
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class