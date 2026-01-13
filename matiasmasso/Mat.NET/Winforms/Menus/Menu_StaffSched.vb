Public Class Menu_StaffSched

    Inherits Menu_Base

    Private _StaffScheds As List(Of DTOStaffSched)
    Private _StaffSched As DTOStaffSched

    Public Sub New(ByVal oStaffScheds As List(Of DTOStaffSched))
        MyBase.New()
        _StaffScheds = oStaffScheds
        If _StaffScheds IsNot Nothing Then
            If _StaffScheds.Count > 0 Then
                _StaffSched = _StaffScheds.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oStaffSched As DTOStaffSched)
        MyBase.New()
        _StaffSched = oStaffSched
        _StaffScheds = New List(Of DTOStaffSched)
        If _StaffSched IsNot Nothing Then
            _StaffScheds.Add(_StaffSched)
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
        oMenuItem.Enabled = _StaffScheds.Count = 1
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
        Dim oFrm As New Frm_StaffSched(_StaffSched)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.StaffSched.Delete(_StaffScheds.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

