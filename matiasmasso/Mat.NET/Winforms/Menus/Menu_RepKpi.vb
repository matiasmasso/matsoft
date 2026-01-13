Public Class Menu_RepKpi

    Inherits Menu_Base

    Private _RepKpis As List(Of DTORepKpi)
    Private _RepKpi As DTORepKpi

    Public Sub New(ByVal oRepKpis As List(Of DTORepKpi))
        MyBase.New()
        _RepKpis = oRepKpis
        If _RepKpis IsNot Nothing Then
            If _RepKpis.Count > 0 Then
                _RepKpi = _RepKpis.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oRepKpi As DTORepKpi)
        MyBase.New()
        _RepKpi = oRepKpi
        _RepKpis = New List(Of DTORepKpi)
        If _RepKpi IsNot Nothing Then
            _RepKpis.Add(_RepKpi)
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
        oMenuItem.Enabled = _RepKpis.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_RepKpi(_RepKpi)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


End Class


