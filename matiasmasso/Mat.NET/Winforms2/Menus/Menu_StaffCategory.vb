Public Class Menu_StaffCategory

    Inherits Menu_Base

    Private _StaffCategories As List(Of DTOStaffCategory)
    Private _StaffCategory As DTOStaffCategory

    Public Sub New(ByVal oStaffCategories As List(Of DTOStaffCategory))
        MyBase.New()
        _StaffCategories = oStaffCategories
        If _StaffCategories IsNot Nothing Then
            If _StaffCategories.Count > 0 Then
                _StaffCategory = _StaffCategories.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oStaffCategory As DTOStaffCategory)
        MyBase.New()
        _StaffCategory = oStaffCategory
        _StaffCategories = New List(Of DTOStaffCategory)
        If _StaffCategory IsNot Nothing Then
            _StaffCategories.Add(_StaffCategory)
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
        oMenuItem.Enabled = _StaffCategories.Count = 1
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
        Dim oFrm As New Frm_StaffCategory(_StaffCategory)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.StaffCategory.Delete(exs, _StaffCategories.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


