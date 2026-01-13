Public Class Menu_Content
    Inherits Menu_Base

    Private _Contents As List(Of DTOContent)
    Private _Content As DTOContent

    Public Sub New(ByVal oContents As List(Of DTOContent))
        MyBase.New()
        _Contents = oContents
        If _Contents IsNot Nothing Then
            If _Contents.Count > 0 Then
                _Content = _Contents.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oContent As DTOContent)
        MyBase.New()
        _Content = oContent
        _Contents = New List(Of DTOContent)
        If _Content IsNot Nothing Then
            _Contents.Add(_Content)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Browse())
        MyBase.AddMenuItem(MenuItem_CopyLink())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Contents.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Navegar"
        AddHandler oMenuItem.Click, AddressOf Do_Browse
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
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
        Dim oFrm As New Frm_Content(_Content)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = _Content.Url.AbsoluteUrl(Current.Session.Lang)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = _Content.Url.AbsoluteUrl(Current.Session.Lang)
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Content.Delete(exs, _Contents.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

