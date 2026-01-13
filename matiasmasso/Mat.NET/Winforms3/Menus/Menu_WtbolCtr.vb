Public Class Menu_WtbolCtr
    Inherits Menu_Base

    Private _WtbolCtrs As List(Of DTOWtbolCtr)
    Private _WtbolCtr As DTOWtbolCtr

    Public Sub New(ByVal oWtbolCtrs As List(Of DTOWtbolCtr))
        MyBase.New()
        _WtbolCtrs = oWtbolCtrs
        If _WtbolCtrs IsNot Nothing Then
            If _WtbolCtrs.Count > 0 Then
                _WtbolCtr = _WtbolCtrs.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oWtbolCtr As DTOWtbolCtr)
        MyBase.New()
        _WtbolCtr = oWtbolCtr
        _WtbolCtrs = New List(Of DTOWtbolCtr)
        If _WtbolCtr IsNot Nothing Then
            _WtbolCtrs.Add(_WtbolCtr)
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
        oMenuItem.Enabled = _WtbolCtrs.Count = 1
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
        Dim oFrm As New Frm_WtbolCtr(_WtbolCtr)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.WtbolCtr.Delete(exs, _WtbolCtrs.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

