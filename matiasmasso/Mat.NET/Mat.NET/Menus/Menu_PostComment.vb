Public Class Menu_PostComment
    Private _PostComment As PostComment

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oPostComment As PostComment)
        MyBase.New()
        _PostComment = oPostComment
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
        MenuItem_Validate(), _
        MenuItem_MailAnswer(), _
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
        Dim oAnswering As PostComment = _PostComment.Answering
        If oAnswering IsNot Nothing Then
            CommentLoader.Load(_PostComment)
            If _PostComment.User.Rol.IsStaff Then
                oMenuItem.Enabled = True
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

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PostComment(_PostComment)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Validate(ByVal sender As Object, ByVal e As System.EventArgs)
        With _PostComment
            .FchApproved = Now
            .FchDeleted = Nothing
            .Update()
        End With
        RaiseEvent AfterUpdate(_PostComment, EventArgs.Empty)
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        With _PostComment
            .FchDeleted = Now
            .Update()
        End With
        RaiseEvent AfterUpdate(_PostComment, EventArgs.Empty)
    End Sub

    Private Sub Do_MailAnswer()
        Dim exs as New List(Of exception)
        If BLL_Comment.MailToUser(_PostComment, exs) Then
            MsgBox("missatge enviat correctament", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError( exs, "error al enviar el missatge")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
