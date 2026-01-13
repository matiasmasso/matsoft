Public Class CommentViewModel
    Property Target As Guid
    Property TargetSrc As DTOPostComment.ParentSources
    Property AnswerRoot As Guid
    Property Answering As Guid
    Property EmailAddress As String
    Property Password As String
    Property Nickname As String
    Property Text As String
    Property Result As Results

    Public Enum Results
        NotSet
        Success
        EmptyEmail
        WrongEmail
        EmailNotFound
        EmailDelivered
        PasswordRequest
        WrongPassword
        NicknameRequest
        SysError
    End Enum

    Function PostComment(user As DTOUser, lang As DTOLang) As DTOPostComment
        Dim retval As New DTOPostComment
        With retval
            .Parent = Target
            .ParentSource = TargetSrc
            .User = user
            .Text = Text
            .Fch = Now
            .Lang = lang
            If Answering <> Nothing Then
                .Answering = New DTOPostComment(Answering)
            End If
            If AnswerRoot <> Nothing Then
                .AnswerRoot = New DTOPostComment(AnswerRoot)
            End If
        End With
        Return retval
    End Function
End Class
