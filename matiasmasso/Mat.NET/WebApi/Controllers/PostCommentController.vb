Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PostCommentController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PostComment/{id}")>
    Public Function Find(id As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PostComment.Find(id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PostComment")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/PostComment")>
    Public Function Update(<FromBody> value As DTOPostComment) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PostComment.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la PostComment")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la PostComment")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/PostComment/delete")>
    Public Function Delete(<FromBody> value As DTOPostComment) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PostComment.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PostComment")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PostComment")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PostComment/emailAnswer/{emp}/{postComment}")>
    Public Async Function emailAnswer(emp As DTOEmp.Ids, postComment As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oPostComment = BEBL.PostComment.Find(postComment)
            Dim oMailMessage = BEBL.PostComment.AnsweredMailMessage(oEmp, oPostComment)
            If Await BEBL.MailMessageHelper.Send(oEmp, oMailMessage, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al enviar la resposta al comentari")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al enviar la resposta al comentari")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PostComment/emailPendingModeration/{emp}/{postComment}")>
    Public Async Function emailPendingModeration(emp As DTOEmp.Ids, postComment As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oPostComment = BEBL.PostComment.Find(postComment)
            Dim oMailMessage = BEBL.PostComment.PendingModerationMailMessage(oEmp, oPostComment)
            If Await BEBL.MailMessageHelper.Send(oEmp, oMailMessage, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al enviar la confirmacio de comentari rebut")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al enviar la confirmacio de comentari rebut")
        End Try
        Return retval
    End Function


End Class

Public Class PostCommentsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PostComments/{status}/{parent}")>
    Public Function All(status As DTOPostComment.StatusEnum, parent As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.PostComments.All(status, parent)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els comentaris")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PostComments/Tree/{target}/{lang}/{take}/{from}")>
    Public Function Tree(target As Guid, lang As String, take As Integer, from As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oTarget As New DTOBaseGuid(target)
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.PostComments.TreeModel(oTarget, oLang, take, from)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir l'arbre de comentaris")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PostComments/Tree/{comment}")>
    Public Function TreeToIncludeComment(comment As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oComment = BEBL.PostComment.Find(comment)
            Dim oTarget As New DTOBaseGuid(oComment.Parent)
            Dim value = BEBL.PostComments.TreeModel(oTarget, oComment.Lang, oIncludeComment:=oComment)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir l'arbre de comentaris")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PostComments/forFeed/{fchfrom}/{domainId}")>
    Public Function ForFeed(fchfrom As Date, domainId As DTOWebDomain.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDomain As New DTOWebDomain(domainId)
            Dim values = BEBL.PostComments.forfeed(fchfrom, oDomain)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir l'arbre de comentaris")
        End Try
        Return retval
    End Function


End Class
