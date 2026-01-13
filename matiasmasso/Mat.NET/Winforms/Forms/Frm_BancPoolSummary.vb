Public Class Frm_BancPoolSummary
    Private Sub Frm_BancPoolSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_BancPoolSummary1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BancPoolSummary1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim values As List(Of DTOBancPool) = BLLBancPools.All(, DateTimePicker1.Value)
        Xl_BancPoolSummary1.Load(values)
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        refresca()
    End Sub
End Class