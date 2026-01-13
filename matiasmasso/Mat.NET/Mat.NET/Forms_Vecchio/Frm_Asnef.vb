

Public Class Frm_Asnef
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Tabs
        Consultes
        Registres
    End Enum

    Private Sub Frm_Asnef_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Xl_Asnef_Consultas1.Emp = mEmp
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Registres
                Static LoadedRegs As Boolean
                If LoadedRegs Then Exit Sub
                LoadedRegs = True
                Xl_Asnef_Registres1.Emp = mEmp
        End Select
    End Sub
End Class