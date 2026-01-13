@ModelType DTOPostComment.TreeModel
@Code
    Dim oNewCommentGuid = Guid.NewGuid()
    Dim oUser = Mvc.ContextHelper.FindUserSync()
End Code

<div class="CommentsWrapper">



    <div class="CommentThreads Contingut"
         data-target="@Model.Target.Guid.ToString()"
         data-targetsrc="@CInt(Model.TargetSrc).ToString()">


        <div class="Comment">
            @If Model.Items.Count = 0 Then
                @<div>
                    <label for="CommentFormTextArea">
                        @Mvc.ContextHelper.Tradueix("No hay comentarios aún.", "Encara no hi ha cap comentari.", "No comments yet.", "Não há comentários.")
                        @Mvc.ContextHelper.Tradueix("¡Sé el primero en colgar uno!", "Sigues el primer a deixar-ne!", "Be the first to post one!", "Sê o primeiro, a enviar-nos o teu!")
                    </label>
                </div>
            Else
                @<div>
                    <label for="CommentFormTextArea">
                        @If Model.Items.Count = 1 Then
                            @Html.Raw(Mvc.ContextHelper.Tradueix("Un comentario publicado hasta ahora.", "Un comentari publicat fins ara.", "One comment so far."))
                        Else
                            @Html.Raw(String.Format("{0:N0} {1}.", Model.RootItemsCount, Mvc.ContextHelper.Tradueix("comentarios publicados hasta ahora.", "comentaris publicats fins ara.", "comments so far.")))
                        End If
                        @Html.Raw(Mvc.ContextHelper.Tradueix("¡Deja el tuyo a continuación!", "Deixa el teu a continuació!", "Leave yours here!"))
                    </label>
                </div>
            End If

            <div class="MainThreadForm">
                @Html.Partial("_CommentForm")
            </div>

            <div class="Reply"></div>
        </div>

        <br />
        <br />

        @Html.Partial("_CommentThreads", Model)
    </div>

    @If Model.MoreItems() Then
        @<a href="#"
            Class="ReadAllComments"
            data-take="@Model.Take"
            data-from="@(Model.From)"
            data-target="@Model.Target.Guid.ToString()"
            data-targetsrc="@CInt(Model.TargetSrc).ToString()">

            @Html.Raw(Mvc.ContextHelper.Tradueix("Ver", "Veure", "See", "ver mais"))
            <span class='NextCount'>
                @Html.Raw(Model.NextCount)
            </span>
            @Html.Raw(Mvc.ContextHelper.Tradueix("comentarios más", "comentaris més", "more comments", "comentários"))

            @Html.Raw(Mvc.ContextHelper.Tradueix("de los", "dels", "from"))
            <span class='LeftCount'>
                @Html.Raw(Model.RootItemsCount - Model.From)
            </span>
            @String.Format(Mvc.ContextHelper.Tradueix("restantes", "restants", "left"))
        </a>
    End If


</div>



