Public Class Frm_BancSdos
    Private Async Sub Frm_BancSdos_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub Xl_BancSdos1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_BancSdos1.AfterUpdate
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB2.BancSdos.Last(Current.Session.Emp, exs)
        If exs.Count = 0 Then
            Xl_BancSdos1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub ToolStripButtonExcel_Click(sender As Object, e As EventArgs) Handles ToolStripButtonExcel.Click
        Dim items As List(Of DTOBancSdo) = Xl_BancSdos1.Values
        Dim oSheet = DTOBancSdo.Excel(items)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class