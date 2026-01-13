@ModelType DTORaffle.HeadersModel

<div class="Item">

    <div class="Image-container NextRaffle">
        <img src="~/Media/Img/Misc/nextraffle.png"
             width="178"
             height="125"
             alt='@ContextHelper.Tradueix("Próximo sorteo", "Proper sorteig", "Next raffle")' />

        <!--@@Html.Partial("_countdown")-->
    </div>

    <div class="Text-container">

        <div>
            <span>
                @ContextHelper.Tradueix("Próximo sorteo en menos de", "Proper sorteig en menys de", "Next raffle in less than")
            </span>
            <span>
                @Model.TimeToNextLessThan(ContextHelper.Lang)
            </span>
        </div>

        <div class="Properties">
            <div>
                @ContextHelper.Tradueix("fecha de inicio:", "data de inici:", "start up date:", "Data de inicio;")
            </div>
            <div>
                @Model.NextFch.ToString("dd/MM/yy")
            </div>
        </div>

        <div class="actioncall">
            <p>
                @ContextHelper.Tradueix("¡mejor será que no te lo pierdas!", "sera millor que no te'l perdis!", "you better don't miss it!", "O melhor será que não deixes passar a oportunidade!")
            </p>
        </div>

    </div>
</div>


