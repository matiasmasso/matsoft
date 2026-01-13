Public Class Frm_Emps
    Public Event SelectedItemChanged(sender As Object, e As MatEventArgs)

    Private Sub Frm_Emps_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oEmps As List(Of DTOEmp) = Await FEB.Emps.All(exs, Current.Session.User)
        If exs.Count = 0 Then
            Xl_Emps1.Load(oEmps, Current.Session.Emp, DTO.Defaults.SelectionModes.Selection)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Emps1_SelectedItemChanged(sender As Object, e As MatEventArgs) Handles Xl_Emps1.SelectedItemChanged
        Dim oEmp As DTOEmp = e.Argument
        Dim exs As New List(Of Exception)
        RaiseEvent SelectedItemChanged(sender, e)
        Me.Close()
    End Sub

    Private Sub Xl_Emps1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Emps1.RequestToAddNew
        Dim oEmp As New DTOEmp()
        Dim oFrm As New Frm_Emp(oEmp)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub


End Class