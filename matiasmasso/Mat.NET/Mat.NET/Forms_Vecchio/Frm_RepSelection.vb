Public Class Frm_RepSelection
    Public Event AfterSelect(sender As Object, e As MatEventArgs)

    Private Sub Frm_RepSelection_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oEmp as DTOEmp = BLL.BLLApp.Emp
        Dim oReps As Reps = App.Current.emp.Reps
        Xl_RepsSelectionGrid1.Load(oReps, bll.dEFAULTS.SelectionModes.Selection)
    End Sub

    Private Sub Xl_RepsSelectionGrid1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_RepsSelectionGrid1.ValueChanged
        RaiseEvent AfterSelect(Me, e)
        Me.Close()
    End Sub
End Class