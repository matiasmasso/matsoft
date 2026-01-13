Public Class Frm_EdiversaDesadvs
    Private Async Sub Frm_EdiversaDesadvs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB.EdiversaDesadvs.All(exs)
        If exs.Count = 0 Then
            Xl_EdiversaDesadvs1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_EdiversaDesadvs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_EdiversaDesadvs1.RequestToRefresh
        Await refresca()
    End Sub
End Class