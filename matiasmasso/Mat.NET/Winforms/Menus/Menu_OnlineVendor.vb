Public Class Menu_OnlineVendor

    Inherits Menu_Base

    Private _OnlineVendors As List(Of DTOOnlineVendor)
    Private _OnlineVendor As DTOOnlineVendor

    Public Sub New(ByVal oOnlineVendors As List(Of DTOOnlineVendor))
        MyBase.New()
        _OnlineVendors = oOnlineVendors
        If _OnlineVendors IsNot Nothing Then
            If _OnlineVendors.Count > 0 Then
                _OnlineVendor = _OnlineVendors.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oOnlineVendor As DTOOnlineVendor)
        MyBase.New()
        _OnlineVendor = oOnlineVendor
        _OnlineVendors = New List(Of DTOOnlineVendor)
        If _OnlineVendor IsNot Nothing Then
            _OnlineVendors.Add(_OnlineVendor)
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
        oMenuItem.Enabled = _OnlineVendors.Count = 1
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
        Dim oFrm As New Frm_OnlineVendor(_OnlineVendor)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLOnlineVendor.Delete(_OnlineVendors.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

