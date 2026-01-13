Public Class Frm_PropertyGrid

    Private Sub Frm_PropertyGrid_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oCustomer As DTOCustomer = BLL.BLLCustomer.Find(New Guid("938AA55C-2AD5-451D-A14A-6F5AF1D1A888"))
        PropertyGrid1.SelectedObject = oCustomer
        PropertyGrid1.ToolbarVisible = False
    End Sub
End Class