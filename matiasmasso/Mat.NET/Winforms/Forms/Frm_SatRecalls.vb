Public Class Frm_SatRecalls
    Private Async Sub Frm_StatRecalls_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim items = Await FEB2.SatRecalls.All(GlobalVariables.Emp, exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_SatRecalls1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_SatRecalls1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SatRecalls1.RequestToRefresh
        Await refresca()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_SatRecalls1.Filter = e.Argument
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim items = Xl_SatRecalls1.FilteredValues
        Dim oSheet = FEB2.SatRecalls.Excel(items)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class