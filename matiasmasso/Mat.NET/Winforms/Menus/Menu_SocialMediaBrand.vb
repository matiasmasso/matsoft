Public Class Menu_SocialMediaWidget
    Inherits Menu_Base

    Private _SocialMediaWidgets As List(Of DTOSocialMediaWidget)
    Private _SocialMediaWidget As DTOSocialMediaWidget

    Public Sub New(ByVal oSocialMediaWidgets As List(Of DTOSocialMediaWidget))
        MyBase.New()
        _SocialMediaWidgets = oSocialMediaWidgets
        If _SocialMediaWidgets IsNot Nothing Then
            If _SocialMediaWidgets.Count > 0 Then
                _SocialMediaWidget = _SocialMediaWidgets.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSocialMediaWidget As DTOSocialMediaWidget)
        MyBase.New()
        _SocialMediaWidget = oSocialMediaWidget
        _SocialMediaWidgets = New List(Of DTOSocialMediaWidget)
        If _SocialMediaWidget IsNot Nothing Then
            _SocialMediaWidgets.Add(_SocialMediaWidget)
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
        oMenuItem.Enabled = _SocialMediaWidgets.Count = 1
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
        Dim oFrm As New Frm_SocialMediaWidget(_SocialMediaWidget)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.SocialMediaWidget.Delete(exs, _SocialMediaWidget) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub



End Class
