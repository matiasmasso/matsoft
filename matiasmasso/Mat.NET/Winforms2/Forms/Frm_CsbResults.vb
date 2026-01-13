Public Class Frm_CsbResults
    Private Async Sub Frm_CsbResults_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim values = Await FEB.Csbs.CsbResults(exs, GlobalVariables.Emp)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_CsbResults1.Load(values)
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub
End Class