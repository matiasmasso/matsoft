Public Class Menu_Mod145
    Inherits Menu_Base

    Private _Mod145s As List(Of DTOMod145)
    Private _Mod145 As DTOMod145

    Public Sub New(ByVal oMod145s As List(Of DTOMod145))
        MyBase.New()
        _Mod145s = oMod145s
        If _Mod145s IsNot Nothing Then
            If _Mod145s.Count > 0 Then
                _Mod145 = _Mod145s.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oMod145 As DTOMod145)
        MyBase.New()
        _Mod145 = oMod145
        _Mod145s = New List(Of DTOMod145)
        If _Mod145 IsNot Nothing Then
            _Mod145s.Add(_Mod145)
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
        oMenuItem.Enabled = _Mod145s.Count = 1
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
        Dim oContactDoc As New DTOContactDoc(_Mod145.Guid)
        Dim oFrm As New Frm_ContactDoc(oContactDoc)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest model?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            Dim oContactDoc As New DTOContactDoc(_Mod145s.First().Guid)
            If Await FEB.ContactDoc.Delete(oContactDoc, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el model 145")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

