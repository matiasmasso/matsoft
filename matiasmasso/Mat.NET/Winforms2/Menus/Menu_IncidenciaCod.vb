Public Class Menu_IncidenciaCod
    Inherits Menu_Base

    Private _IncidenciaCods As List(Of DTOIncidenciaCod)
    Private _IncidenciaCod As DTOIncidenciaCod

    Public Sub New(ByVal oIncidenciaCods As List(Of DTOIncidenciaCod))
        MyBase.New()
        _IncidenciaCods = oIncidenciaCods
        If _IncidenciaCods IsNot Nothing Then
            If _IncidenciaCods.Count > 0 Then
                _IncidenciaCod = _IncidenciaCods.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oIncidenciaCod As DTOIncidenciaCod)
        MyBase.New()
        _IncidenciaCod = oIncidenciaCod
        _IncidenciaCods = New List(Of DTOIncidenciaCod)
        If _IncidenciaCod IsNot Nothing Then
            _IncidenciaCods.Add(_IncidenciaCod)
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
        oMenuItem.Enabled = _IncidenciaCods.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        oMenuItem.Enabled = _IncidenciaCods.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_IncidenciaCod(_IncidenciaCod)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.IncidenciaCod.Delete(_IncidenciaCods.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


