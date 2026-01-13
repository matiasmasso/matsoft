Public Class Menu_StaffHoliday

    Inherits Menu_Base

    Private _StaffHolidays As List(Of DTOStaffHoliday)
    Private _StaffHoliday As DTOStaffHoliday

    Public Sub New(ByVal oStaffHolidays As List(Of DTOStaffHoliday))
        MyBase.New()
        _StaffHolidays = oStaffHolidays
        If _StaffHolidays IsNot Nothing Then
            If _StaffHolidays.Count > 0 Then
                _StaffHoliday = _StaffHolidays.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oStaffHoliday As DTOStaffHoliday)
        MyBase.New()
        _StaffHoliday = oStaffHoliday
        _StaffHolidays = New List(Of DTOStaffHoliday)
        If _StaffHoliday IsNot Nothing Then
            _StaffHolidays.Add(_StaffHoliday)
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
        oMenuItem.Enabled = _StaffHolidays.Count = 1
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
        Dim oFrm As New Frm_StaffHoliday(_StaffHoliday)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.StaffHoliday.Delete(_StaffHolidays.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


