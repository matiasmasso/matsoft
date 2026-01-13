

Public Class Frm_fiscal_Mod349
    Private _AllowEvents As Boolean

    Private Sub Frm_fiscal_Mod349_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadYeas()
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        Dim items As List(Of DTOModel349) = BLL.BLLModel349.All(Xl_Years1.Value)
        Xl_Model3491.Load(items)
    End Sub

    Private Sub LoadYeas()
        Dim oYears As List(Of Integer) = BLL.BLLModel349.Years
        Xl_Years1.Load(oYears)
    End Sub


    Private Sub ToolStripButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        Dim oSheet As DTOExcelSheet = BLL.BLLModel349.ExcelSheet(Xl_Years1.Value)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        If _AllowEvents Then
            Refresca()
        End If
    End Sub
End Class