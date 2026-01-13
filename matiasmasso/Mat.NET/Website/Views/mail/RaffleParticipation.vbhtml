@ModelType DTORaffleParticipant

@Code
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim oLang As DTOLang = Model.User.Lang
    If oLang Is Nothing Then oLang = DTOApp.current.lang
    Dim oBrand = FEB.Product.BrandSync(Model.Raffle.Product)
End Code

<table cellpadding="20" style="font-family: arial, sans-serif; font-size: 12pt;">
    <tr>
        <td colspan=" 2">
            <font face="arial, sans-serif">
                @Html.Raw(oLang.Tradueix("Confirmamos tu participación en el siguiente sorteo:", "Confirmem la teva participació en el següent sorteig:", "We confirm your participation on our next raffle:", "Confirmamos a tu participação no sorteio:"))
            </font>
        <td>
    </tr>
    <tr>
        <td>
            <table cellpadding="0" style="font-family: arial, sans-serif; font-size: 12pt;">
                <tr>
                    <td>@Html.Raw(oLang.Tradueix("participación", "participació", "share number", "participação"))</td>
                    <td><b>@Html.Raw(Format(Model.Num, "00000"))</b></td>
                </tr>
                <tr>
                    <td>@Html.Raw(oLang.Tradueix("fecha", "data", "date", "data"))</td>
                    <td><b>@Html.Raw(Format(Model.Fch, "dd/MM/yy HH:mm"))</b></td>
                </tr>
                <tr>
                    <td>@Html.Raw(oLang.Tradueix("nombre", "nom", "name", "nome"))</td>
                    <td><b>@Html.Raw(DTORaffleParticipant.FullNom(Model))</b></td>
                </tr>
                <tr>
                    <td style="width:120px">@Html.Raw(oLang.Tradueix("teléfono", "telèfon", "phone number", "telefone"))</td>
                    <td><b>@Html.Raw(Model.User.Tel)</b></td>
                </tr>
                <tr>
                    <td>@Html.Raw(oLang.Tradueix("concepto", "concepte", "concept", "conceito"))</td>
                    <td><b>@Html.Raw(Model.Raffle.Title)</b></td>
                </tr>
                <tr>
                    <td>@Html.Raw(oLang.Tradueix("bases", "bases", "terms", "bases"))</td>
                    <td><a href="@Html.Raw(FEB.Raffle.BasesUrl(Model.Raffle, True))">@Html.Raw(oLang.Tradueix("(ver bases del sorteo)", "(veure bases del sorteig)", "(see raffle terms)", "(ver bases do sorteio)"))</a></td>
                </tr>
                @if Model.Raffle.RightAnswer > 0 Then
                    @<tr>
                        <td style="text-wrap:none">@Html.Raw(oLang.Tradueix("respuesta", "resposta", "answer", "resposta"))</td>
                        <td><b>@Html.Raw(Model.Raffle.Answers(Model.Answer))</b></td>
                    </tr>
                    @<tr>
                        <td style="text-wrap:none;vertical-align:top">@Html.Raw(oLang.Tradueix("distribuidor", "distribuïdor", "distributor", "distribuidor"))</td>
                        <td><b>@Html.Raw(FEB.Contact.HtmlNameComercialAndAddress(Model.Distribuidor))</b></td>
                    </tr>
                End If
            </table>
        </td>
    </tr>


    <tr>
        <td colspan=" 2">
                <span>
                     @Html.Raw(oLang.Tradueix("Recuerda que este es un sorteo exclusivo para seguidores de nuestros perfiles en Facebook y/o Instagram. Si aun no lo eres puedes seguirnos haciendo clic en los botones <b>Me gusta</b> (Facebook) o <b>Seguir</b> (Instagram)  en las siguientes páginas:",
                                                                "Recorda que aquest és un sorteig exclusiu per seguidors dels nostres perfils a Facebook i/o Instagram. Si encara no ens segueixes, fes clic als botons <b>M'agrada</b> (Facebook) o <b>Seguir</b>(Instagram) a les següents pàgines:",
                                                                "Please note this is a raffle restricted to our Facebook/Instagram followers. If you are not following us yet, you may clic <b>Like button</b> (Facebook) or <b>Follow</b> (Instagram) on next pages:",
                                                                "Recorda que este é um sorteio exclusivo para os seguidores dos nossos perfis em Facebook/Instagram. Se ainda não o és podes seguir-nos fazendo clic no botão <b>Gosto</b> (Facebook) ou <b>Seguir</b> (Instagram) nas seguintes páginas:"))

                </span>
      

        </td>
    </tr>


    <tr>
        <td colspan=" 2">
            @If oLang.IsPor() Then
                @<a href="https://www.facebook.com/matiasmasso.sa.pt" target="_blank">www.facebook.com/matiasmasso.sa.pt</a>
                @<br />
                @<a href="https://www.instagram.com/matiasmasso.sa_pt" target="_blank">www.instagram.com/matiasmasso.sa_pt</a>
            Else
                @<a href="https://www.facebook.com/matiasmasso.sa" target="_blank">www.facebook.com/matiasmasso.sa</a>
                @<br />
                @<a href="https://www.instagram.com/matiasmasso.sa" target="_blank">www.instagram.com/matiasmasso.sa</a>
            End If


            @If (oBrand.Is4moms() Or oBrand.IsBritaxRoemer) Then
                If oLang.IsPor() Then
                    If oBrand.IsBritaxRoemer Then
                        @<br />
                        @<a href="https://www.facebook.com/BritaxPT" target="_blank">www.facebook.com/BritaxPT</a>
                        @<br />
                        @<a href="https://www.instagram.com/britaxroemer_pt" target="_blank">www.instagram.com/britaxroemer_pt</a>
                    ElseIf oBrand.Is4moms Then
                        @<br />
                        @<a href="https://www.facebook.com/4moms.pt" target="_blank">www.facebook.com/4moms.pt</a>
                        @<br />
                        @<a href="https://www.instagram.com/4moms_pt" target="_blank">www.instagram.com/4moms_pt</a>
                    End If
                Else
                    If oBrand.IsBritaxRoemer Then
                        @<br />
                        @<a href="@FEB.Raffle.FacebookPage(Model.Raffle, oLang)">@FEB.Raffle.FacebookPageLabel(Model.Raffle, oLang)</a>
                        @<br />
                        @<a href="https://www.instagram.com/britaxroemer_es" target="_blank">www.instagram.com/britaxroemer_es</a>
                    ElseIf oBrand.Is4moms Then
                        @<br />
                        @<a href="https://www.facebook.com/4moms.es" target="_blank">www.facebook.com/4moms.es</a>
                        @<br />
                        @<a href="https://www.instagram.com/4moms_es" target="_blank">www.instagram.com/4moms_es</a>
                    End If
                End If
            End If

        </td>
    </tr>



    <tr>
        <td colspan = " 2" >
            <p>
        @Html.Raw(oLang.Tradueix("Puedes verificar el resultado a partir de:", "Pots verificar el resultat a partir de:", "Don't miss to check the result from:", "Recorda-te de verificar o resultado a partir das:"))
                        <br/>
        @Html.Raw(oLang.Tradueix("las " & Format(Model.Raffle.FchTo, "HH:mm") & " horas del", "les " & Format(Model.Raffle.FchTo, "HH:mm") & " hores del", "" & Format(Model.Raffle.FchTo, "HH:mm") & "", Format(Model.Raffle.FchTo, "HH:mm") & " horas de"))
                        <b>
        @Html.Raw(oLang.WeekDay(Model.Raffle.FchTo))
        @Html.Raw(Format(Model.Raffle.FchTo, "dd/MM/yy"))
                        </b>
                        <br/>

        @Html.Raw(oLang.Tradueix("en", "a", "at", "em"))
                        <a href="@FEB.Raffles.Url(oLang, True)">@FEB.Raffles.Url(oLang, True)</a>
                                    </p>
                                    <p>
                        @Html.Raw(oLang.Tradueix("Si te toca el premio, tienes 30 días para ponerte en contacto con nosotros en", "Si et toca el premi, tens 30 dies per contactar amb nosaltres a", "If you get awarded, you must contact us during next 30 days at", "Se te toca o prémio, tens 30 dias para nos contactar, através do email"))
                                        <a href ='mailto:@Html.Raw(oLang.Tradueix("marketing@matiasmasso.es", "marketing@matiasmasso.es", "marketing@matiasmasso.es", "marketing@matiasmasso.pt"))'>
                                        @Html.Raw(oLang.Tradueix("marketing@matiasmasso.es", "marketing@matiasmasso.es", "marketing@matiasmasso.es", "marketing@matiasmasso.pt"))
                                                        </a>
                                                    </p>
                                                    <p>
                                        @Html.Raw(oLang.Tradueix("¡Gracias por tu participación!", "Gràcies per participar-hi!", "Thanks for participating!", "Agradecemos a tua participação!"))
                                                    </p>
                                                <td>
                                            </tr>
</table>

