Public Class Frm_PostComments

    Private Async Sub Frm_PostComments_Load(sender As Object, e As EventArgs) Handles Me.Load
        ComboBoxStatus.SelectedIndex = DTOPostComment.StatusEnum.Pendent
        Await refresca()
    End Sub

    Private Async Sub Xl_UsuariComments1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_UsuariComments1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oStatus As DTOPostComment.StatusEnum = CurrentStatus()
        ProgressBar1.Visible = True
        Dim items = Await FEB.PostComments.All(exs, oStatus)
        Dim oBlogPosts = Await FEB.BlogPosts.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_UsuariComments1.Load(items, oBlogPosts)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function CurrentStatus() As DTOPostComment.StatusEnum
        Dim retval As DTOPostComment.StatusEnum = ComboBoxStatus.SelectedIndex
        Return retval
    End Function

    Private Async Sub ComboBoxStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxStatus.SelectedIndexChanged
        Await refresca()
    End Sub


    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_UsuariComments1.Filter = e.Argument
    End Sub

    Private Sub Xl_UsuariComments1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_UsuariComments1.RequestToToggleProgressBar
        ProgressBar1.Visible = e.Argument
    End Sub
End Class