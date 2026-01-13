Public Class Menu_FeedbackSource

    Inherits Menu_Base

    Private _FeedbackSources As List(Of DTOFeedback.SourceClass)
    Private _FeedbackSource As DTOFeedback.SourceClass

    Public Sub New(ByVal oFeedbackSources As List(Of DTOFeedback.SourceClass))
        MyBase.New()
        _FeedbackSources = oFeedbackSources
        If _FeedbackSources IsNot Nothing Then
            If _FeedbackSources.Count > 0 Then
                _FeedbackSource = _FeedbackSources.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oFeedbackSource As DTOFeedback.SourceClass)
        MyBase.New()
        _FeedbackSource = oFeedbackSource
        _FeedbackSources = New List(Of DTOFeedback.SourceClass)
        If _FeedbackSource IsNot Nothing Then
            _FeedbackSources.Add(_FeedbackSource)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_CopyGuid())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _FeedbackSources.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyGuid() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar Guid"
        oMenuItem.Enabled = _FeedbackSources.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyGuid
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
        Dim oFrm As New Frm_FeedbackSource(_FeedbackSource)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyGuid(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_FeedbackSource.Guid.ToString())
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.FeedbackSource.Delete(exs, _FeedbackSources.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

