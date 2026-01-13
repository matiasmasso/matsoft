Public Class Menu_SegSocialGrup

    Inherits Menu_Base

    Private _SegSocialGrups As List(Of DTOSegSocialGrup)
    Private _SegSocialGrup As DTOSegSocialGrup

    Public Sub New(ByVal oSegSocialGrups As List(Of DTOSegSocialGrup))
        MyBase.New()
        _SegSocialGrups = oSegSocialGrups
        If _SegSocialGrups IsNot Nothing Then
            If _SegSocialGrups.Count > 0 Then
                _SegSocialGrup = _SegSocialGrups.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSegSocialGrup As DTOSegSocialGrup)
        MyBase.New()
        _SegSocialGrup = oSegSocialGrup
        _SegSocialGrups = New List(Of DTOSegSocialGrup)
        If _SegSocialGrup IsNot Nothing Then
            _SegSocialGrups.Add(_SegSocialGrup)
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
        oMenuItem.Enabled = _SegSocialGrups.Count = 1
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
        Dim oFrm As New Frm_SegSocialGrup(_SegSocialGrup)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.SegSocialGrup.Delete(_SegSocialGrups.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

