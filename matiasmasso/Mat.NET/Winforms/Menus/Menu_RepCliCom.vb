Public Class Menu_RepCliCom

    Inherits Menu_Base

    Private _RepCliComs As List(Of DTORepCliCom)
    Private _RepCliCom As DTORepCliCom


    Public Sub New(ByVal oRepCliComs As List(Of DTORepCliCom))
        MyBase.New()
        _RepCliComs = oRepCliComs
        If _RepCliComs IsNot Nothing Then
            If _RepCliComs.Count > 0 Then
                _RepCliCom = _RepCliComs.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oRepCliCom As DTORepCliCom)
        MyBase.New()
        _RepCliCom = oRepCliCom
        _RepCliComs = New List(Of DTORepCliCom)
        If _RepCliCom IsNot Nothing Then
            _RepCliComs.Add(_RepCliCom)
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
        oMenuItem.Enabled = _RepCliComs.Count = 1
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
        Dim oFrm As New Frm_RepCliCom(_RepCliCom)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.RepCliComs.Delete(exs, _RepCliComs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


