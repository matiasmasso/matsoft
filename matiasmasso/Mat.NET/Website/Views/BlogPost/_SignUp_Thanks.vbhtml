@ModelType String

    <p>
        @Html.Raw(ContextHelper.Tradueix("Gracias por registrar tus datos en nuestro blog",
                                                                                  "Gracies per registrar les seves dades al nostre blog.",
                                                                                  "Thanks for signing up on our blog."))
    </p>

    <a href="@Model" class="Submit">@ContextHelper.Tradueix("Volver al blog", "Tornar al blog", "Take me back", "de volta ao blog")</a>


