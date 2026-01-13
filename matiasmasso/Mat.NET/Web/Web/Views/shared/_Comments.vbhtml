@ModelType List(Of DTOPostComment)
@Code
    Dim oUser = Mvc.ContextHelper.FindUserSync()
End Code


@For Each oComment As DTOPostComment In Model

    @<div class='CommentWrapper'>
        @If oComment.Answering IsNot Nothing Then
            @<div class='CommentArrow'></div>
        End If

        <div class='CommentBox' itemtype='http://schema.org/Comment' itemscope='itemscope'>

            <!--================================================================================================ -->
            <!--Header -->
            <!--================================================================================================ -->

            <!--' add the avatar-->
            <img src='@LegacyHelper.GravatarHelper.Url(oComment.User.emailAddress, 36)' style='float:left;margin-right:5px;' />

            <!--' add the name-->
            <a name ='@oComment.Guid.ToString' itemprop='author'>@DTOPostComment.UserNickname(oComment)</a><br />

            <!--' add the time-->
            <time content='@Format(oComment.Fch, "yyyy-MM-dd")' 
                  datetime='@Format(oComment.Fch, "yyyy-MM-dd")' 
                  itemprop='dateCreated'>
                    @Format(oComment.Fch, "dd/MM/yy HH:mm")
            </time><br />

            <br />

            <!--================================================================================================ -->
            <!--Body -->
            <!--================================================================================================ -->

            <div itemprop='text'>@Html.Raw(DTOPostComment.HtmlText(oComment))</div>

            <!--================================================================================================ -->
            <!--Resposta -->
            <!--================================================================================================ -->
            <p>
                <a href='#' class="AnswerRequest" data-answering="@oComment.Guid.ToString">responder a este comentario</a>
            </p>
            <br />

            <div class="SISU"
                 data-lang="@Mvc.ContextHelper.lang().Tag"
                 data-email="@DTOUser.GetEmailAddress(oUser)"
                 data-isauthenticated="@DTOUser.IsAuthenticated(oUser).ToString.ToLower"
                 data-hideafterauthenticate="true"
                 data-guid="@oComment.Guid.ToString"
                 data-src="@CInt(DTOUser.Sources.WebComment)"></div>

            <div class="AnswerInfo" data-answering="@oComment.Guid.ToString" hidden>
                Gracias por tu respuesta.<br />Ha quedado pendiente de aprobación por parte del moderador.<br />Esperamos publicarlo en breve
            </div>
            <div class="AnswerForm" data-answering="@oComment.Guid.ToString" hidden>
                <br />
                <h3>Responder al comentario</h3>
                <textarea class="AnswerTextarea" data-answering="@oComment.Guid.ToString" rows="10"></textarea>

                <div class="formSubmitDiv" data-answering="@oComment.Guid.ToString">
                    <input class="AnswerSubmit" data-answering="@oComment.Guid.ToString" type="button" value='Publicar' />
                </div>
            </div>

        </div>

        @For Each oAnswer As DTOPostComment In oComment.Answers
                @Html.Partial("_Comments", oComment.Answers)
        Next


    </div>

Next






