Public Class Frm_PropertyGrid

    Private Async Sub Frm_PropertyGrid_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oCustomer = Await FEB2.Customer.Find(exs, New Guid("938AA55C-2AD5-451D-A14A-6F5AF1D1A888"))
        If exs.Count = 0 Then
            PropertyGrid1.SelectedObject = oCustomer
            PropertyGrid1.ToolbarVisible = False
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class