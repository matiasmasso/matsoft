Public Class Menu_WebPageAlias

    Inherits Menu_Base

    Private _WebPagesAlias As List(Of DTOWebPageAlias)
    Private _WebPageAlias As DTOWebPageAlias


    Public Sub New(ByVal oWebPagesAlias As List(Of DTOWebPageAlias))
        MyBase.New()
        _WebPagesAlias = oWebPagesAlias
        If _WebPagesAlias IsNot Nothing Then
            If _WebPagesAlias.Count > 0 Then
                _WebPageAlias = _WebPagesAlias.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oWebPageAlias As DTOWebPageAlias)
        MyBase.New()
        _WebPageAlias = oWebPageAlias
        _WebPagesAlias = New List(Of DTOWebPageAlias)
        If _WebPageAlias IsNot Nothing Then
            _WebPagesAlias.Add(_WebPageAlias)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Browse())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _WebPagesAlias.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function
    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Prova-ho"
        oMenuItem.Enabled = _WebPagesAlias.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Browse
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
        Dim oFrm As New Frm_WebPageAlias(_WebPageAlias)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Browse()
        Dim url As String = _WebPageAlias.FullUrl()
        UIHelper.ShowHtml(url)
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.WebPageAlias.Delete(_WebPagesAlias.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


