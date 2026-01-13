Public Class Frm_Last_Albs2

    Private Sub Frm_LastAlbs2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oEmp as DTOEmp = BLL.BLLApp.Emp
        Dim oAlbs As Albs = AlbsLoader.Last(oEmp)
        Xl_Albs1.Load(oAlbs)
    End Sub
End Class