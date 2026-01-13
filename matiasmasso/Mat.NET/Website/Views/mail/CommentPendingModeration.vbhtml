@ModelType DTOPostComment

@Code
    Layout = "~/Views/Mail/_Layout.vbhtml"
End Code


<p>
    @Model.Lang.Tradueix("Gracias por tu comentario.", "Gracies per el teu comentari.", "Thanks for your comment.")
    <br />
    @Model.Lang.Tradueix("Ha quedado pendiente de aprobación por parte del moderador.", "Ha quedat pendent de la aprobació per part del moderador", "It is currentlty waiting for moderator approval")
    <br />
    @Model.Lang.Tradueix("Esperamos publicarlo en breve.", "D'aquí poc el podras veure publicat", "You'll see it published at soonest")
</p>

<p><b>Comentario registrado:</b></p>
<p>
   @Html.Raw(Model.HtmlText())
</p>

@If Model.Answering IsNot Nothing Then
   @<div>
       <p>
           <b>
               @Model.Lang.Tradueix("en respuesta a", "en resposta a", "answering")
               @Model.Answering.User.NicknameForComments()
           </b>
       </p>
       <p>
           @Html.Raw(Model.Answering.HtmlText())
       </p>
   </div>
End If


<p><b>@Model.Lang.Tradueix("En el siguiente post:", "Al següent post:", "From next post:")</b></p>
<p>
   @Model.ParentTitle.Tradueix(Model.Lang)
</p>
<br/>
