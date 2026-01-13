Public Class Menu_UserTaskId

    Inherits Menu_Base

    Private _UserTaskIds As List(Of DTOUserTaskId)
    Private _UserTaskId As DTOUserTaskId

    Public Sub New(ByVal oUserTaskIds As List(Of DTOUserTaskId))
        MyBase.New()
        _UserTaskIds = oUserTaskIds
        If _UserTaskIds IsNot Nothing Then
            If _UserTaskIds.Count > 0 Then
                _UserTaskId = _UserTaskIds.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oUserTaskId As DTOUserTaskId)
        MyBase.New()
        _UserTaskId = oUserTaskId
        _UserTaskIds = New List(Of DTOUserTaskId)
        If _UserTaskId IsNot Nothing Then
            _UserTaskIds.Add(_UserTaskId)
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
        oMenuItem.Enabled = _UserTaskIds.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_UserTaskId(_UserTaskId)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub



End Class


