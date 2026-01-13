Public Class Menu_BlogPost
    Inherits Menu_Base

    Private _BlogPosts As List(Of DTOBlogPost)
    Private _BlogPost As DTOBlogPost

    Public Sub New(ByVal oBlogPosts As List(Of DTOBlogPost))
        MyBase.New()
        _BlogPosts = oBlogPosts
        If _BlogPosts IsNot Nothing Then
            If _BlogPosts.Count > 0 Then
                _BlogPost = _BlogPosts.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oBlogPost As DTOBlogPost)
        MyBase.New()
        _BlogPost = oBlogPost
        _BlogPosts = New List(Of DTOBlogPost)
        If _BlogPost IsNot Nothing Then
            _BlogPosts.Add(_BlogPost)
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
        oMenuItem.Enabled = _BlogPosts.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Navegar"
        oMenuItem.DropDownItems.Add(MenuItem_BrowseEs)
        oMenuItem.DropDownItems.Add(MenuItem_BrowsePt)
        Return oMenuItem
    End Function

    Private Function MenuItem_BrowseEs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "ES"
        AddHandler oMenuItem.Click, AddressOf Do_BrowseEs
        Return oMenuItem
    End Function

    Private Function MenuItem_BrowsePt() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "PT"
        AddHandler oMenuItem.Click, AddressOf Do_BrowsePt
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.DropDownItems.Add(MenuItem_CopyLinkEs)
        oMenuItem.DropDownItems.Add(MenuItem_CopyLinkPt)
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLinkEs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "ES"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLinkEs
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLinkPt() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "PT"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLinkPt
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
        Dim oFrm As New Frm_BlogPost(_BlogPost)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_BrowseES(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = _BlogPost.Url.AbsoluteUrl(DTOLang.ESP)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_BrowsePT(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = _BlogPost.Url.AbsoluteUrl(DTOLang.POR)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_CopyLinkES(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = _BlogPost.Url.AbsoluteUrl(DTOLang.ESP)
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub Do_CopyLinkPT(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = _BlogPost.Url.AbsoluteUrl(DTOLang.POR)
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.BlogPost.Delete(exs, _BlogPosts.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

