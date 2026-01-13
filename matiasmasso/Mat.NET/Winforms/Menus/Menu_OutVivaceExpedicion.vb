Public Class Menu_OutVivaceExpedicion
    Inherits Menu_Base

    Private _Expedicions As List(Of DTOOutVivaceLog.expedicion)
    Private _Expedicion As DTOOutVivaceLog.expedicion

    Public Sub New(ByVal oExpedicions As List(Of DTOOutVivaceLog.expedicion))
        MyBase.New()
        _Expedicions = oExpedicions
        If _Expedicions IsNot Nothing Then
            If _Expedicions.Count > 0 Then
                _Expedicion = _Expedicions.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oExpedicion As DTOOutVivaceLog.expedicion)
        MyBase.New()
        _Expedicion = oExpedicion
        _Expedicions = New List(Of DTOOutVivaceLog.expedicion)
        If _Expedicion IsNot Nothing Then
            _Expedicions.Add(_Expedicion)
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
        oMenuItem.Enabled = _Expedicions.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_OutVivaceExpedicio(_Expedicion)
        oFrm.Show()
    End Sub


End Class
