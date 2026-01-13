Public Class Menu_InventariItem
    Inherits Menu_Base

    Private _InventariItems As List(Of DTOImmoble.InventariItem)
    Private _InventariItem As DTOImmoble.InventariItem

    Public Sub New(ByVal oInventariItems As List(Of DTOImmoble.InventariItem))
        MyBase.New()
        _InventariItems = oInventariItems
        If _InventariItems IsNot Nothing Then
            If _InventariItems.Count > 0 Then
                _InventariItem = _InventariItems.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oInventariItem As DTOImmoble.InventariItem)
        MyBase.New()
        _InventariItem = oInventariItem
        _InventariItems = New List(Of DTOImmoble.InventariItem)
        If _InventariItem IsNot Nothing Then
            _InventariItems.Add(_InventariItem)
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
        oMenuItem.Enabled = _InventariItems.Count = 1
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
        Dim oFrm As New Frm_InventariItem(_InventariItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.InventariItem.Delete(exs, _InventariItems.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class


