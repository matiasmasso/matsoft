Public Class PostComment
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOPostComment)
        Return Await Api.Fetch(Of DTOPostComment)(exs, "PostComment", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oPostComment As DTOPostComment) As Boolean
        If Not oPostComment.IsLoaded And Not oPostComment.IsNew Then
            Dim pPostComment = Api.FetchSync(Of DTOPostComment)(exs, "PostComment", oPostComment.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPostComment)(pPostComment, oPostComment, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oPostComment As DTOPostComment) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPostComment)(oPostComment, exs, "PostComment")
        oPostComment.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oPostComment As DTOPostComment) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPostComment)(oPostComment, exs, "PostComment")
    End Function

    Shared Function Url(oPostComment As DTOPostComment, Optional BlAbsoluteUrl As Boolean = False) As String
        Dim exs As New List(Of Exception)
        Dim oNoticia As DTONoticia = Noticia.FindSync(exs, oPostComment.Parent)
        Dim retval = Noticia.UrlFriendly(oNoticia, BlAbsoluteUrl) & "#" & oPostComment.Guid.ToString
        Return retval
    End Function

    Shared Async Function EmailAnswer(exs As List(Of Exception), oEmp As DTOEmp, oPostComment As DTOPostComment) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "PostComment/emailAnswer", oEmp.Id, oPostComment.Guid.ToString())
    End Function

    Shared Async Function EmailPendingModeration(exs As List(Of Exception), oEmp As DTOEmp, oPostComment As DTOPostComment) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "PostComment/EmailPendingModeration", oEmp.Id, oPostComment.Guid.ToString())
    End Function

End Class

Public Class PostComments
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oStatus As DTOPostComment.StatusEnum, Optional oParentGuid As Guid = Nothing) As Task(Of List(Of DTOPostComment))
        Return Await Api.Fetch(Of List(Of DTOPostComment))(exs, "PostComments", oStatus, oParentGuid.ToString())
    End Function

    Shared Async Function ForFeed(exs As List(Of Exception), fchFrom As Date, domain As Integer) As Task(Of List(Of DTOPostComment))
        Return Await Api.Fetch(Of List(Of DTOPostComment))(exs, "PostComments/forFeed", fchFrom.ToString("yyyy-MM-dd"), domain)
    End Function

    Shared Async Function Tree(exs As List(Of Exception), oTarget As DTOBaseGuid, targetSrc As DTOPostComment.ParentSources, oLang As DTOLang, take As Integer, Optional from As Integer = 0) As Task(Of DTOPostComment.TreeModel)
        Dim retval = Await Api.Fetch(Of DTOPostComment.TreeModel)(exs, "PostComments/tree", oTarget.Guid.ToString(), oLang.Tag, take, from)
        If retval IsNot Nothing Then
            retval.From += retval.NextCount
            retval.Target = oTarget
            retval.TargetSrc = targetSrc
        End If
        Return retval
    End Function

    Shared Async Function Tree(exs As List(Of Exception), oPostComment As DTOPostComment) As Task(Of DTOPostComment.TreeModel)
        Dim retval = Await Api.Fetch(Of DTOPostComment.TreeModel)(exs, "PostComments/tree", oPostComment.Guid.ToString())
        If retval IsNot Nothing Then
            retval.Target = New DTOBaseGuid(oPostComment.Parent)
            retval.TargetSrc = oPostComment.ParentSource
            retval.Take = 15
            retval.From = retval.Items.Count
        End If
        Return retval
    End Function

    Shared Function AllSync(exs As List(Of Exception), oStatus As DTOPostComment.StatusEnum, Optional oParentGuid As Guid = Nothing) As List(Of DTOPostComment)
        Return Api.FetchSync(Of List(Of DTOPostComment))(exs, "PostComments", oStatus, oParentGuid.ToString())
    End Function

End Class

