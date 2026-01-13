Public Class Frm_ProveidorVtos
    Private _values As List(Of DTOPnd)

    Private Async Sub Frm_ProveidorVtos_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _values = Await FEB2.Pnds.All(exs, GlobalVariables.Emp, oContact:=Nothing, onlyPendents:=True)
        If exs.Count = 0 Then
            Xl_ProveidorVtos1.Load(_values)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = Xl_ProveidorVtos1.Excel("Proveidors pendents de pagament", Current.Session.Lang)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_ProveidorVtos1.Filter = e.Argument
    End Sub
End Class