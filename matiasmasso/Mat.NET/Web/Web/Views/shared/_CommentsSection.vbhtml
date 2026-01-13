@ModelType Guid

@Code
    Dim exs As New List(Of Exception)
    Dim oComments = FEB2.PostComments.AllSync(exs, DTOPostComment.StatusEnum.Aprobat, Model)
    Dim iCommentsCount As Integer = oComments.Count
    Dim oCommentsTree = DTOPostComment.Tree(oComments)

    Dim oLang As DTOLang = Mvc.ContextHelper.lang()
    Dim oNewComment = DTOPostComment.NewFromParentSource(Model)
    Dim oNewCommentGuid As Guid = Guid.Empty
    Dim oUser = Mvc.ContextHelper.FindUserSync()
End Code

<style scoped>
    .AnswerLoginEmail {
        width:80%;
    }
</style>


<div class="CommentsWrapper">
    <div>
        <div class="AnswerForm" data-answering="@oNewCommentGuid.ToString" hidden>
            <br />
            <h3>Publicar nuevo comentario</h3>
            <textarea class="AnswerTextarea" data-answering="@oNewCommentGuid.ToString" rows="10"></textarea>

            <div class="formSubmitDiv" data-answering="@oNewCommentGuid.ToString">
                <input class="AnswerSubmit" data-answering="@oNewCommentGuid.ToString" type="button" value='Publicar' />
            </div>
        </div>

        <div class="AnswerInfo" data-answering="@oNewCommentGuid.ToString" hidden>
            Gracias por tu comentario.<br />Ha quedado pendiente de aprobación por parte del moderador.<br />Esperamos publicarlo en breve
        </div>
    </div>

    <br />

    @If iCommentsCount = 0 Then
        @Html.Raw(oLang.tradueix("No hay comentarios aún.", "Encara no hi ha cap comentari.", "No comments yet."))
        @<a href="#" class="AnswerRequest" data-answering="@oNewCommentGuid.ToString">
            @Html.Raw(oLang.tradueix("¡Sé el primero en colgar uno!", "Sigues el primer a deixar-ne!", "Be the first to post one!"))
        </a>
    Else
        @Html.Raw(iCommentsCount.ToString & " comentarios.")
        @<a href="#" class="AnswerRequest" data-answering="@oNewCommentGuid.ToString">
            @Html.Raw(oLang.tradueix("¡Pincha aquí para dejar el tuyo!", "Clic aquí per deixar el teu!", "Click here to leave yours"))
        </a>
    End If

    <br />
    <br />

    <div class="SISU"
         data-guid="@oNewCommentGuid.ToString"
         data-lang="@Mvc.ContextHelper.lang().Tag"
         data-email="@DTOUser.GetEmailAddress(oUser)"
         data-isauthenticated="@DTOUser.IsAuthenticated(oUser).ToString.ToLower"
         data-hideafterauthenticate="true"
         data-src="@CInt(DTOUser.Sources.WebComment)"></div>


    <input type="hidden" id="HiddenParent" value="@Model" />
    <input type="hidden" id="HiddenParentLang" value="@oLang.Tag" />

    @Html.Partial("_Comments", oCommentsTree)
</div>


