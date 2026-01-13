Public Class Frm_PostCommentAnswer

    Private _Src As DTOPostComment
    Private _AllowEvents As Boolean

    Public Sub New(src As DTOPostComment)
        InitializeComponent()
        _Src = src
        Me.Text = "Resposta a comentari de " & _Src.User.NicknameOrElse()
        _AllowEvents = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oAnswerRoot = _Src
        If oAnswerRoot Is Nothing Then oAnswerRoot = _Src
        Dim oAnswer As New DTOPostComment
        With oAnswer
            .Answering = _Src
            .AnswerRoot = If(_Src.AnswerRoot IsNot Nothing, _Src.AnswerRoot, _Src)
            .Fch = DTO.GlobalVariables.Now()
            .User = Current.Session.User
            .Lang = _Src.Lang
            .Parent = _Src.Parent
            .ParentSource = _Src.ParentSource
            .ParentTitle = _Src.ParentTitle
            .FchApproved = DTO.GlobalVariables.Now()
            .Text = TextBoxComment.Text
        End With

        UIHelper.ToggleProggressBar(PanelButtons, True)
        Dim exs As New List(Of Exception)
        If Await FEB.PostComment.Update(exs, oAnswer) Then
            If Await FEB.PostComment.EmailAnswer(exs, GlobalVariables.Emp, oAnswer) Then
                MsgBox("missatge enviat correctament", MsgBoxStyle.Information)
            Else
                UIHelper.WarnError(exs, "error al enviar el missatge")
            End If

            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TextBoxComment_TextChanged(sender As Object, e As EventArgs) Handles TextBoxComment.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub
End Class