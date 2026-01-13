Public Class Frm_Tasks
    Private Sub Frm_Tasks_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Async Sub Xl_Tasks1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Xl_Tasks1.ItemCheck
        Dim oTask As DTOTask = Xl_Tasks1.Value
        Dim exs As New List(Of Exception)
        If FEB2.Task.Load(oTask, exs) Then
            oTask.Enabled = (e.NewValue = CheckState.Checked)
            If Await FEB2.Task.Update(oTask, exs) Then
                refresca()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim oTasks As List(Of DTOTask) = Await FEB2.Tasks.All(exs)
        Xl_Tasks1.Load(oTasks)
    End Sub

    Private Sub Xl_Tasks1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Tasks1.RequestToAddNew
        Dim oTask As New DTOTask
        Dim oFrm As New Frm_Task(oTask)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Tasks1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Tasks1.RequestToRefresh
        refresca()
    End Sub
End Class