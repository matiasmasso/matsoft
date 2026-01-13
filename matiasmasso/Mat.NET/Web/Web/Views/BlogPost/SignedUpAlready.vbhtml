@ModelType Mvc.LoginViewModel
@Code
    Layout = "~/Views/shared/_Layout_Blog.vbhtml"
End Code

<div class="PageWrapperVertical">
    <h3>@ViewBag.Title</h3>


    <p>
        @Html.Raw(Mvc.ContextHelper.Tradueix("Confirmamos que ya estás registrado en este blog y deberias recibir las actualizaciones a: ",
                                                                 "Confirmem que ja estàs registrat en aquest blog i hauries de rebre les actualitzacions a:",
                                                                      "We are glad to confirm you are already registered and you should receive blog updates on:"))
        <a href="mailto:@Model.EmailAddress">@Model.EmailAddress</a>
    </p>

    <a href="@Model.ReturnUrl" class="Submit">@Mvc.ContextHelper.Tradueix("Volver al blog", "Tornar al blog", "Take me back", "de volta ao blog")</a>


</div>

@Section Styles
    <style>
        .PageWrapperVertical {
            max-width: 300px !important;
        }
    </style>

End Section