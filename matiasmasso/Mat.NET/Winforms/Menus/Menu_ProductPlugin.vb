Public Class Menu_ProductPlugin
    Inherits Menu_Base

    Private _ProductPlugins As List(Of DTOProductPlugin)
    Private _ProductPlugin As DTOProductPlugin

    Public Sub New(ByVal oProductPlugins As List(Of DTOProductPlugin))
        MyBase.New()
        _ProductPlugins = oProductPlugins
        If _ProductPlugins IsNot Nothing Then
            If _ProductPlugins.Count > 0 Then
                _ProductPlugin = _ProductPlugins.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oProductPlugin As DTOProductPlugin)
        MyBase.New()
        _ProductPlugin = oProductPlugin
        _ProductPlugins = New List(Of DTOProductPlugin)
        If _ProductPlugin IsNot Nothing Then
            _ProductPlugins.Add(_ProductPlugin)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Snippet())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _ProductPlugins.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Snippet() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar codi"
        oMenuItem.Enabled = _ProductPlugins.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Snippet
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
        Dim oFrm As New Frm_ProductPlugin(_ProductPlugin)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Snippet(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim src As String = _ProductPlugin.snippet
        Clipboard.SetDataObject(src, True)
        MsgBox("Plugin copiat al portapapers", MsgBoxStyle.Information, "Mat.Net")
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.ProductPlugin.Delete(exs, _ProductPlugins.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

