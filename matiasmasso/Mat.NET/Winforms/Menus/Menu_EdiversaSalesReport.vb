Public Class Menu_EdiversaSalesReport
    Inherits Menu_Base

    Private _EdiversaSalesReports As List(Of DTOEdiversaSalesReport)
    Private _EdiversaSalesReport As DTOEdiversaSalesReport

    Public Sub New(ByVal oEdiversaSalesReports As List(Of DTOEdiversaSalesReport))
        MyBase.New()
        _EdiversaSalesReports = oEdiversaSalesReports
        If _EdiversaSalesReports IsNot Nothing Then
            If _EdiversaSalesReports.Count > 0 Then
                _EdiversaSalesReport = _EdiversaSalesReports.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oEdiversaSalesReport As DTOEdiversaSalesReport)
        MyBase.New()
        _EdiversaSalesReport = oEdiversaSalesReport
        _EdiversaSalesReports = New List(Of DTOEdiversaSalesReport)
        If _EdiversaSalesReport IsNot Nothing Then
            _EdiversaSalesReports.Add(_EdiversaSalesReport)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _EdiversaSalesReports.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_EdiversaSalesReport(_EdiversaSalesReport)
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.EdiversaSalesReport.Delete(exs, _EdiversaSalesReports.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class


