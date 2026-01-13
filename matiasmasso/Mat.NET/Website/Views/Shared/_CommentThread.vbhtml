@ModelType DTOPostComment.TreeModel.Item

<div class="Thread" data-idx="@Model.Idx">
    <a name="@Model.Guid.ToString()"></a>
    <div class="Comment" data-guid="@Model.Guid.ToString()" data-answerroot="@Model.AnswerRootGuid.ToString()">
        <div class="Avatar">
            <!--<img src="@@Model.Gravatar()" />-->
        </div>
        <div class="Text">
            <div class="Caption">
                @Model.Caption(ContextHelper.Lang)
            </div>

            <div class="ShortenedText">
                @Html.Raw(MatHelperStd.TextHelper.Html(Model.ShortenedText))
                @if Model.ShortenedText.EndsWith("...") Then
                    @<a href="#" class="ReadMore">
                        @ContextHelper.Tradueix("(leer más)", "(llegir-ne més)", "(read more)", "(ler mais)")
                    </a>
                End If
            </div>

            <div class="CompleteText">
                @Html.Raw(MatHelperStd.TextHelper.Html(Model.Text))
                <br />
                <a href="#" class="ReadLess">
                    @ContextHelper.Tradueix("(leer menos)", "(llegir-ne menys)", "(read less)", "(leia menos)")
                </a>
            </div>

            <div class="Feedback">
                <a href="#" class="CallToAnswer">
                    @ContextHelper.Tradueix("responder", "respon", "reply", "(leia menos)")
                </a>
            </div>

            <div class="Reply"></div>

            @For Each oComment In Model.Items()
                @Html.Partial("_CommentThread", oComment)
            Next
        </div>
    </div>

</div>

