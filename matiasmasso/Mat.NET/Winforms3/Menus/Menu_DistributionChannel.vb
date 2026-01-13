Public Class Menu_DistributionChannel
    Inherits Menu_Base

    Private _DistributionChannels As List(Of DTODistributionChannel)
    Private _DistributionChannel As DTODistributionChannel

    Public Sub New(ByVal oDistributionChannels As List(Of DTODistributionChannel))
        MyBase.New()
        _DistributionChannels = oDistributionChannels
        If _DistributionChannels IsNot Nothing Then
            If _DistributionChannels.Count > 0 Then
                _DistributionChannel = _DistributionChannels.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oDistributionChannel As DTODistributionChannel)
        MyBase.New()
        _DistributionChannel = oDistributionChannel
        _DistributionChannels = New List(Of DTODistributionChannel)
        If _DistributionChannel IsNot Nothing Then
            _DistributionChannels.Add(_DistributionChannel)
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
        oMenuItem.Enabled = _DistributionChannels.Count = 1
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
        Dim oFrm As New Frm_DistributionChannel(_DistributionChannel)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.DistributionChannel.Delete(_DistributionChannel, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

