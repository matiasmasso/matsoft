@ModelType DTOFeedback

<div id="Feedback" data-cod="@CInt(Model.Source.Cod)" 
                   data-source="@Model.Source.Guid.ToString()"
                   data-nom="@Model.Source.Nom"
                   data-guid="@Guid.Empty.ToString()">

    <div class="Request">
        @Mvc.ContextHelper.Tradueix("¿Te ha resultado útil esta información?", "Ha estat d'ultilitat aquesta informació?", "Did this information help you?")
    </div>
    <div class="Icons" data-guid="@Model.ToString()">
        <a href="#" class="Positive" data-score="1">
            <img src="~/Media/Img/Ico/feedback_positive.png" width="21" border="0" alt="Positive" title="Positive" />
        </a>
        <a href="#" class="Neutral" data-score="2">
            <img src="~/Media/Img/Ico/feedback_neutral.png" width="21" border="0" alt="Neutral" title="Neutral" />
        </a>
        <a href="#" class="Negative" data-score="3">
            <img src="~/Media/Img/Ico/feedback_negative.png" width="21" border="0" alt="Negative" title="Negative" />
        </a>
    </div>

    <div class="Spinner64"></div>
    <div class="Comment" hidden="hidden">
        <p>@Mvc.ContextHelper.Tradueix("Gracias por registrar su opinión.")</p>
        <p>@Mvc.ContextHelper.Tradueix("Si desea añadir algun comentario que nos permita mejorar puede hacerlo a continuación:")</p>
        <textarea></textarea>
        <div class="SubmitDiv">
            <input type="button" value=@Mvc.ContextHelper.Tradueix("enviar") />
        </div>
    </div>
</div>
