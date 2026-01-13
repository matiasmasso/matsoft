Public Class Menu_PortsCondicio
    Inherits Menu_Base

    Private _PortsCondicios As List(Of DTOPortsCondicio)
    Private _PortsCondicio As DTOPortsCondicio

    Public Sub New(ByVal oPortsCondicios As List(Of DTOPortsCondicio))
        MyBase.New()
        _PortsCondicios = oPortsCondicios
        If _PortsCondicios IsNot Nothing Then
            If _PortsCondicios.Count > 0 Then
                _PortsCondicio = _PortsCondicios.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oPortsCondicio As DTOPortsCondicio)
        MyBase.New()
        _PortsCondicio = oPortsCondicio
        _PortsCondicios = New List(Of DTOPortsCondicio)
        If _PortsCondicio IsNot Nothing Then
            _PortsCondicios.Add(_PortsCondicio)
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
        oMenuItem.Enabled = _PortsCondicios.Count = 1
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
        Dim oFrm As New Frm_PortsCondicio(_PortsCondicio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.PortsCondicio.Delete(exs, _PortsCondicios.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class


