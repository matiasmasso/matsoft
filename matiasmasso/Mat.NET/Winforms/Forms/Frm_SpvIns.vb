Public Class Frm_SpvIns
    Private Async Sub Frm_SpvIns_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await Refresca()
    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim items = Await FEB2.SpvIns.All(Current.Session.Emp, exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_SpvIns1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_SpvIns1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SpvIns1.RequestToRefresh
        Await Refresca()
    End Sub
End Class