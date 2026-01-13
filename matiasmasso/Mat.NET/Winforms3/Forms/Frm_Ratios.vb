Public Class Frm_Ratios
    Private Async Sub Frm_Ratios_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePicker1.Value = DTO.GlobalVariables.Today()
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim dtFch As Date = DateTimePicker1.Value
        Dim oRatios As List(Of DTORatio) = Await FEB.Balance.Ratios(Current.Session.Emp, dtFch)
        Xl_Ratios1.Load(oRatios)
    End Function

    Private Async Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Await refresca()
    End Sub
End Class