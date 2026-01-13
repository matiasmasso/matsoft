Public Class Menu_StaffPos

    Inherits Menu_Base

    Private _StaffPoss As List(Of DTOStaffPos)
    Private _StaffPos As DTOStaffPos


    Public Sub New(ByVal oStaffPoss As List(Of DTOStaffPos))
        MyBase.New()
        _StaffPoss = oStaffPoss
        If _StaffPoss IsNot Nothing Then
            If _StaffPoss.Count > 0 Then
                _StaffPos = _StaffPoss.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oStaffPos As DTOStaffPos)
        MyBase.New()
        _StaffPos = oStaffPos
        _StaffPoss = New List(Of DTOStaffPos)
        If _StaffPos IsNot Nothing Then
            _StaffPoss.Add(_StaffPos)
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
        oMenuItem.Enabled = _StaffPoss.Count = 1
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
        Dim oFrm As New Frm_StaffPos(_StaffPos)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.StaffPoss.Delete(exs, _StaffPoss) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


