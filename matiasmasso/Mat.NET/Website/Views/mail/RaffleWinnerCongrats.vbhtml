@ModelType DTORaffleParticipant

@Code
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim oLang As DTOLang = Model.User.Lang
    If oLang Is Nothing Then oLang = DTOApp.current.lang
End Code

<p>
    @oLang.Format("¡Enhorabuena, {0}!", "¡Enhorabona, {0}!", "¡Congratulations, {0}!", "¡Está de parabéns, {0}!", Model.User.Nom)
</p>
<p>
    @oLang.Format("Nos complace confirmarte que has ganado el premio en el siguiente sorteo:",
                                                                                                                                                       "Ens complau confirmar-te que has sortit guanyador del següent sorteig:",
                                                                                                                                                       "We are glad to confirm you as the winner of the raffle:",
                                                                                                                                                       "Temos o prazer de confirmar que ganhou o prémio do seguinte sorteio:")
    <br/>
<a href = "@FEB.Raffle.ZoomUrl(Model.Raffle, True)" >@Model.Raffle.Title</a>
</p>
<p>

    @oLang.Tradueix("A partir de aquí, y siguiendo las ", "A partir d'aquí, y seguint les ", "Following ", "A partir de aqui, e seguindo as ")
    <a href="@FEB.Raffle.BasesUrl(Model.Raffle, True)">
        @oLang.Tradueix("bases del sorteo", "bases del sorteig", "raffle terms", "bases do sorteio")
    </a>, 
    @oLang.Tradueix("el procedimiento es el siguiente.", "el procediment es el següent.", "procedure is as follows.", "o procedimento é o seguinte.")
<ol>
    <li>
        @oLang.Tradueix("Indícanos cuál es la cuenta que has utilizado para seguirnos en Facebook o Instagram",
                                                                                                                           "Indicans quin compte has fet servir per seguir-nos a Facebook o Instagram",
                                                                                                                           "Please tell us which account did you use to follow us in Facebook or Instagram",
                                                                                                                           "Indica-nos qual é a conta, com a que nos segues em Facebook ou Instagram")
        </li>
    <li>
        @oLang.Tradueix("Confírmanos por favor respondiendo a este correo tu disposición a:",
                                                                              "Confirman's si us plau responent aquest correu la teva disposició a:",
                                                                              "Please answer this message confirming your availability to:",
                                                                              "Confirme-nos por favor respondendo a este correio eletrónico a sua disponibilidade para")
        <ul>
            <li>
                @oLang.Tradueix("recoger el premio en el establecimiento del distribuidor que has seleccionado",
                                                                                               "recollir el premi a l'establiment del distribuidor que has escullit",
                                                                                               "pick up the prize at the shop you selected",
                                                                                               "recolher o prémio no estabelecimento do distribuidor que selecionou"):<br />
                @Html.Raw(FEB.RaffleParticipant.DistributorNameAndAddressHtml(Model))
            </li>
            <li>
                @oLang.Tradueix("participar en la foto de la entrega", "participar en la foto de la entrega", "to participate on a picture of the delivery", "o	participar na foto do acto de entrega")
            </li>
            <li>
                @oLang.Tradueix("tu permiso para publicarla", "el teu permis per publicar-la", "your permission to publish it", "o	permissão para publicá-la")
            </li>
        </ul>
    </li>
    <li>
        @oLang.Tradueix("Al recibir tu correo nos pondremos en contacto con el distribuidor para enviarle tu premio", "Al rebre el teu correu ens posarem en contacte amb el distribuidor per enviar-li el premi", "We will ship the prize to the distributor as soon as we get your answer", "Ao receber a sua resposta contactaremos o distribuidor para enviar-lhe o seu prémio")
    </li>
    <li>
        @oLang.Tradueix("Una vez que el punto de venta haya recibido el producto, se pondrán en contacto contigo para realizar la entrega.Por favor, facilítanos un número de teléfono.",
                                                                              "Un cop que el punt de venda hagi rebut el producte, es posarà en contacte amb tu per organitzar la entrega. Si us plau, donan's un numero de telefon.",
                                                                              "Once the sale point receives the product they'll contact you to arrange the delivery. Please tell us a phone contact number.",
                                                                              "Uma vez que o ponto de venda tenha recebido o produto, se porá em contacto consigo para realizar a entrega. Facilita-nos por favor, tu número de contacto.")
    </li>
</ol>
</p>
<p>
    @oLang.Tradueix("Quedamos a la espera de tus noticias", "Quedem a la espera de les teves noticies", "We look forward your news", "Aguardamos as suas noticias").
    <br />
    @oLang.Tradueix("Recibe un cordial saludo", "Salutacions", "Best regards", "Com os nossos cumprimentos"),
</p>
<p>
MATIAS MASSO, S.A.<br />
Diagonal 403 - 08008 Barcelona<br />
info@matiasmasso.es
</p>
