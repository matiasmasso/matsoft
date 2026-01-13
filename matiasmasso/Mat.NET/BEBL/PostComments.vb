Public Class PostComment
    Shared Function Find(oGuid As Guid) As DTOPostComment
        Dim retval As DTOPostComment = PostCommentLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oPostComment As DTOPostComment) As Boolean
        Dim retval As Boolean = PostCommentLoader.Load(oPostComment)
        Return retval
    End Function

    Shared Function Update(oPostComment As DTOPostComment, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PostCommentLoader.Update(oPostComment, exs)
        Return retval
    End Function

    Shared Function Delete(oPostComment As DTOPostComment, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PostCommentLoader.Delete(oPostComment, exs)
        Return retval
    End Function

    Shared Function AnsweredMailMessage(oEmp As DTOEmp, oPostComment As DTOPostComment) As DTOMailMessage
        BEBL.PostComment.Load(oPostComment.Answering)
        Dim oUser As DTOUser = oPostComment.Answering.User

        Dim retval = DTOMailMessage.Factory(oUser.EmailAddress)
        With retval
            '.to = {"matias@matiasmasso.es"}.ToList
            .bcc = {"matias@matiasmasso.es", "marketing@matiasmasso.es"}.ToList
            .subject = oUser.lang.Tradueix("Aviso de respuesta a su comentario", "Avis de resposta al seu comentari", "Your comment has just been answered")
            .BodyUrl = BEBL.Mailing.BodyUrl(DTODefault.MailingTemplates.CommentAnswered, oPostComment.Answering.Guid.ToString())
        End With
        Return retval
    End Function

    Shared Function PendingModerationMailMessage(oEmp As DTOEmp, oPostComment As DTOPostComment) As DTOMailMessage
        Dim oUser = oPostComment.User
        Dim retval = DTOMailMessage.Factory(oUser.EmailAddress)
        With retval
            '.bcc = {"matias@matiasmasso.es"}.ToList
            .bcc = {"matias@matiasmasso.es", "marketing@matiasmasso.es"}.ToList
            .subject = oUser.lang.Tradueix("Confirmación de registro de comentario en ", "Confirmació de registre de comentari a ", "Comment log on ")
            .subject = .subject & oPostComment.ParentTitle.Tradueix(oUser.lang)
            .bodyUrl = BEBL.Mailing.BodyUrl(DTODefault.MailingTemplates.commentPendingModeration, oPostComment.Guid.ToString())
        End With
        Return retval
    End Function

End Class

Public Class PostComments
    Shared Function All(oStatus As DTOPostComment.StatusEnum, Optional oParentGuid As Guid = Nothing) As List(Of DTOPostComment)
        Dim retval As List(Of DTOPostComment) = PostCommentsLoader.All(oStatus, oParentGuid)
        Return retval
    End Function
    Shared Function ForFeed(fchFrom As Date, oDomain As DTOWebDomain) As List(Of DTOPostComment)
        Dim retval As List(Of DTOPostComment) = PostCommentsLoader.forFeed(fchFrom, oDomain)
        Return retval
    End Function

    Shared Function TreeModel(oTarget As DTOBaseGuid, oLang As DTOLang, Optional take As Integer = 0, Optional from As Integer = 0, Optional oIncludeComment As DTOPostComment = Nothing) As DTOPostComment.TreeModel
        Return PostCommentsLoader.TreeModel(oTarget, oLang, take, from, oIncludeComment)
    End Function

End Class
