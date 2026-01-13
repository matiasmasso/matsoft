Public Class Frm_JsonLogs
    Private Async Sub Frm_JsonLogs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await Refresca()
    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oJsonLogs = Await FEB.JsonLogs.All(exs, Xl_TextboxSearch1.Value)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_JsonLogs1.Load(oJsonLogs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_JsonLogs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_JsonLogs1.RequestToRefresh
        Await Refresca()
    End Sub



    Private Async Sub Xl_TextboxSearch1_Validated(sender As Object, e As EventArgs) Handles Xl_TextboxSearch1.Validated
        Await Refresca()
    End Sub
End Class