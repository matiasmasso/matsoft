Public Class Frm_JsonLogs
    Private Async Sub Frm_JsonLogs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oJsonLogs = Await FEB2.JsonLogs.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_JsonLogs1.Load(oJsonLogs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class