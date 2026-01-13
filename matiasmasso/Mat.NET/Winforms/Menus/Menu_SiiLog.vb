Public Class Menu_SiiLog

    Inherits Menu_Base

    Private _SiiLogs As List(Of DTOSiiLog)
    Private _SiiLog As DTOSiiLog

    Public Sub New(ByVal oSiiLogs As List(Of DTOSiiLog))
        MyBase.New()
        _SiiLogs = oSiiLogs
        If _SiiLogs IsNot Nothing Then
            If _SiiLogs.Count > 0 Then
                _SiiLog = _SiiLogs.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSiiLog As DTOSiiLog)
        MyBase.New()
        _SiiLog = oSiiLog
        _SiiLogs = New List(Of DTOSiiLog)
        If _SiiLog IsNot Nothing Then
            _SiiLogs.Add(_SiiLog)
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
        oMenuItem.Enabled = _SiiLogs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_SiiLog(_SiiLog)
        'oFrm.Show()
    End Sub



End Class


