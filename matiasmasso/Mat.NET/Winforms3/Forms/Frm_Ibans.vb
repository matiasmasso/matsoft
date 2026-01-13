Public Class Frm_Ibans
    Private _AllowEvents As Boolean

    Private Async Sub Frm_Ibans_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Dim oIbans = Await FEB.Ibans.Clients(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            ToolStripStatusLabelCount.Text = Format(oIbans.Count, "#,###") & " mandats"
            Xl_Ibans1.Load(oIbans)
        Else
            UIHelper.WarnError(exs)
        End If
        Cursor = Cursors.Default
    End Function



    Private Async Sub Xl_Ibans1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Ibans1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Ibans1.Filter = e.Argument
        Await refresca()
    End Sub
End Class