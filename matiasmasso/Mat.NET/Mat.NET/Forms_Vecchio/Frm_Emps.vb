Public Class Frm_Emps
    Public Event SelectedItemChanged(sender As Object, e As MatEventArgs)

    Public Sub New(oEmp as DTOEmp)
        MyBase.New()
        Me.InitializeComponent()

        Dim oEmps As List(Of DTOEmp) = BLL.BLLEmps.All(BLL.BLLSession.Current.User)
        Xl_Emps1.Load(oEmps,BLL.BLLApp.Emp, BLL.Defaults.SelectionModes.Selection)
    End Sub

    Private Sub Xl_Emps1_SelectedItemChanged(sender As Object, e As MatEventArgs) Handles Xl_Emps1.SelectedItemChanged
        Dim oEmp As DTOEmp = e.Argument
        BLL.BLLApp.SetEmp(oEmp.Id)
        App.Current.emp = New MaxiSrvr.Emp(oEmp.Id)
        RaiseEvent SelectedItemChanged(sender, e)
        Me.Close()
    End Sub
End Class