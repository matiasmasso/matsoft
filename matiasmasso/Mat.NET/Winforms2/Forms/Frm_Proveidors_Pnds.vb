Public Class Frm_Proveidors_Pnds
    Dim _Values As List(Of DTOPnd)

    Private Async Sub Frm_Proveidors_Pnds_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Do_Excel()
    End Sub

    Private Sub Do_Excel()
        Dim oSheet = DTOPnd.Excel(_Values, "Pendent de pago a proveidors", DTOPnd.Codis.Creditor, Current.Session.Lang)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Proveidors_Pnds1.Filter = e.Argument
    End Sub

    Private Async Sub Xl_Proveidors_Pnds1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Proveidors_Pnds1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        _Values = Await FEB.Pnds.All(exs, GlobalVariables.Emp, oContact:=Nothing, cod:=DTOPnd.Codis.Creditor)
        If exs.Count = 0 Then
            Xl_Proveidors_Pnds1.Load(_Values)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_Proveidors_Pnds1_RequestToExcel(sender As Object, e As MatEventArgs) Handles Xl_Proveidors_Pnds1.RequestToExcel
        Do_Excel()
    End Sub
End Class