Public Class Menu_EciTransmGroup
    Inherits Menu_Base

    Private _ECITransmGroups As List(Of DTOECITransmGroup)
    Private _ECITransmGroup As DTOECITransmGroup

    Public Sub New(ByVal oECITransmGroups As List(Of DTOECITransmGroup))
        MyBase.New()
        _ECITransmGroups = oECITransmGroups
        If _ECITransmGroups IsNot Nothing Then
            If _ECITransmGroups.Count > 0 Then
                _ECITransmGroup = _ECITransmGroups.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oECITransmGroup As DTOECITransmGroup)
        MyBase.New()
        _ECITransmGroup = oECITransmGroup
        _ECITransmGroups = New List(Of DTOECITransmGroup)
        If _ECITransmGroup IsNot Nothing Then
            _ECITransmGroups.Add(_ECITransmGroup)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _ECITransmGroups.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_EciTransmGroup(_ECITransmGroup)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub



End Class
