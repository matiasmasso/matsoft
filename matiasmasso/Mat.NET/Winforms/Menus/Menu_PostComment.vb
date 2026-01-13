Public Class Menu_PostComment
    Private _PostComment As DTOPostComment

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oPostComment As DTOPostComment)
        MyBase.New()
        _PostComment = oPostComment
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(),
        MenuItem_Validate(),
        MenuItem_MailAnswer(),
        MenuItem_Copy(),
        MenuItem_Browse(),
        MenuItem_Delete()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Validate() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Aprovar"
        AddHandler oMenuItem.Click, AddressOf Do_Validate
        Return oMenuItem
    End Function

    Private Function MenuItem_MailAnswer() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "notificar la resposta"
        oMenuItem.Enabled = False
        Dim oAnswering As DTOPostComment = _PostComment.Answering
        If oAnswering IsNot Nothing Then
            Dim exs As New List(Of Exception)
            If FEB2.PostComment.Load(exs, _PostComment) Then
                If _PostComment.User.Rol.IsStaff Then
                    oMenuItem.Enabled = True
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
        AddHandler oMenuItem.Click, AddressOf Do_MailAnswer
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "paperera"
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function

    Private Function MenuItem_Copy() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar..."
        oMenuItem.DropDownItems.Add("enllaç", Nothing, AddressOf Do_CopyLink)
        oMenuItem.DropDownItems.Add("Guid", Nothing, AddressOf Do_CopyGuid)
        Return oMenuItem
    End Function
    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "navegar al post"
        AddHandler oMenuItem.Click, AddressOf Do_Browse
        Return oMenuItem
    End Function

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PostComment(_PostComment)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Validate(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.PostComment.Load(exs, _PostComment) Then
            With _PostComment
                .FchApproved = Now
                .FchDeleted = Nothing
            End With
            If Await FEB2.PostComment.Update(exs, _PostComment) Then
                RaiseEvent AfterUpdate(sender, New MatEventArgs(_PostComment))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.PostComment.Load(exs, _PostComment) Then
            _PostComment.FchDeleted = Now
            If Await FEB2.PostComment.Delete(exs, _PostComment) Then
                RaiseEvent AfterUpdate(sender, New MatEventArgs(_PostComment))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_MailAnswer()
        Dim exs As New List(Of Exception)
        If Await FEB2.PostComment.EmailAnswer(exs, GlobalVariables.Emp, _PostComment) Then
            MsgBox("missatge enviat correctament", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs, "error al enviar el missatge")
        End If
    End Sub

    Private Sub Do_CopyLink()
        Dim oLang = Current.Session.Lang
        Dim oDomain = DTOWebDomain.Factory(oLang, True)
        Dim url As String = oDomain.Url("PostComment", _PostComment.Guid.ToString)
        UIHelper.CopyToClipboard(url, "Enllaç a la pagina del comentari copiat")
    End Sub

    Private Sub Do_CopyGuid()
        Dim exs As New List(Of Exception)
        UIHelper.CopyToClipboard(_PostComment.Guid.ToString(), "Guid identificador del comentari copiat")
    End Sub

    Private Async Sub Do_Browse()
        Dim exs As New List(Of Exception)
        Select Case _PostComment.ParentSource
            Case DTOPostComment.ParentSources.Blog
                Dim oBlogPost = Await FEB2.Blog2Post.Find(exs, _PostComment.Parent)
                If exs.Count = 0 Then
                    If oBlogPost Is Nothing Then
                        UIHelper.WarnError("No s'ha trobat la pàgina")
                    Else
                        UIHelper.ShowHtml(oBlogPost.Url.AbsoluteUrl(DTOLang.ESP))
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If

            Case DTOPostComment.ParentSources.News, DTOPostComment.ParentSources.Noticia
                Dim oContent = Await FEB2.Content.Find(exs, _PostComment.Parent)
                If exs.Count = 0 Then
                    UIHelper.ShowHtml(DTOWebDomain.Factory().Url("content", _PostComment.Guid.ToString))
                Else
                    UIHelper.WarnError(exs)
                End If
        End Select
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
