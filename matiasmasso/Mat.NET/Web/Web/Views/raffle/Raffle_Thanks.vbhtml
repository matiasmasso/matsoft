@ModelType DTORaffle
@Code
    Dim lang As DTOLang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim oBrand = Model.Brand
End Code

<div class="Contingut">
    <div>
        <p>
            @lang.Tradueix("Enhorabuena, has completado correctamente el proceso de inscripción", "Enhorabona, has completat correctament el procés de inscripció", "Congratulations, you successfully completed the subscription process", "Parabéns, completas-te corretamente o processo de inscrição.")
        </p>
        <p>
            @lang.Tradueix("Acabamos de enviarte un correo electrónico confirmando tu participación.", "Acabem de enviar-te un email confirmant la teva participació.", "We've just sent you an email confirming your enrollment.", "Acabamos de te enviar um correio eletrónico a confirmar a tua participação.")
        </p>
        <p>
            @lang.Tradueix("Una vez celebrado el sorteo, publicaremos el ganador en", "Un cop celebrat el sorteig, publicarem el guanyador a", "Once the raffle is over, we will publish the winner at", "Uma vez se celebre o sorteio, publicaremos o nome do ganhador/a em ")
            &nbsp;<a href='@FEB2.Raffles.Url(lang)'>@FEB2.Raffles.Url(lang, True)</a>
        </p>
        <p>
            @lang.Tradueix("Si te toca el premio, tienes 30 días para ponerte en contacto con nosotros en ", "Si et toca el premi, tens 30 dies per posar-te en contacte amb nosaltres a ", "If your name becomes published, you have 30 days to confirm your acceptance at ", "Se te toca o prémio, tens 30 dias para nos contactar em ")
            <a href='mailto:marketing@matiasmasso.@lang.Tradueix("es", "es", "es", "pt")'>marketing@matiasmasso.@lang.Tradueix("es", "es", "es", "pt")</a>
        </p>
        <p class="FbWarn">
            <span>
                @Html.Raw(lang.Tradueix("Recuerda que este es un sorteo exclusivo para seguidores de nuestros perfiles en Facebook y/o Instagram. Si aún no lo eres puedes seguirnos haciendo clic en los botones <b>Me gusta</b> (Facebook) o <b>Seguir</b> (Instagram)  en las siguientes páginas:",
                                                                         "Recorda que aquest és un sorteig exclusiu per seguidors dels nostres perfils a Facebook i/o Instagram. Si encara no ens segueixes, fes clic als botons <b>M'agrada</b> (Facebook) o <b>Seguir</b>(Instagram) a les següents pàgines:",
                                                                         "Please note this is a raffle restricted to our Facebook/Instagram followers. If you are not following us yet, you may clic <b>Like button</b> (Facebook) or <b>Follow</b> (Instagram) on next pages:",
                                                                         "Recorda que este sorteio é exclusivo para os seguidores dos nossos perfis em Facebook/Instagram. Se ainda não o és, podes seguir-nos clicando no botão <b>Gosto</b> (Facebook) ou <b>Seguir</b> (Instagram) nas seguintes páginas:"))

            </span>
        </p>
        <p>
            @If lang.IsPor() Then
                @<a href="https://www.facebook.com/matiasmasso.sa.pt" rel="noopener" target="_blank">www.facebook.com/matiasmasso.sa.pt</a>
                @<br />
                @<a href="https://www.instagram.com/matiasmasso.sa_pt" rel="noopener" target="_blank">www.instagram.com/matiasmasso.sa_pt</a>
            Else
                @<a href="https://www.facebook.com/matiasmasso.sa" rel="noopener" target="_blank">www.facebook.com/matiasmasso.sa</a>
                @<br />
                @<a href="https://www.instagram.com/matiasmasso.sa" rel="noopener" target="_blank">www.instagram.com/matiasmasso.sa</a>
            End If


            @If (oBrand.Is4moms() Or oBrand.IsBritaxRoemer) Then
                @<br />
                @<a href="@FEB2.Raffle.FacebookPage(Model, lang)" target="_blank">
                    @FEB2.Raffle.FacebookPageLabel(Model, lang)
                </a>
                @If lang.Equals(DTOLang.POR) Then
                    If oBrand.IsBritaxRoemer Then
                        @<br />
                        @<a href="https://www.instagram.com/britaxroemer_pt" rel="noopener" target="_blank">www.instagram.com/britaxroemer_pt</a>
                    ElseIf oBrand.Is4moms Then
                        @<br />
                        @<a href="https://www.instagram.com/4moms_pt" rel="noopener" target="_blank">www.instagram.com/4moms_pt</a>
                    End If
                Else
                    If oBrand.IsBritaxRoemer Then
                        @<br />
                        @<a href="https://www.instagram.com/britaxroemer_es" rel="noopener" target="_blank">www.instagram.com/britaxroemer_es</a>
                    ElseIf oBrand.Is4moms Then
                        @<br />
                        @<a href="https://www.instagram.com/4moms_es" rel="noopener" target="_blank">www.instagram.com/4moms_es</a>
                    End If
                End If
            End If
        </p>

        <p>
            @lang.Tradueix("Gracias por participar", "Gracies per participar", "Thanks for participating", "Agradecemos a tua participação.")
        </p>
    </div>

</div>




