@ModelType DTORaffle

@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim oLang As DTOLang = Model.Winner.Distribuidor.Lang
End Code

<p>
    @oLang.Tradueix("Apreciado distribuidor,", "", "", "Apreciado distribuidor,")
</p>
<p>
    @oLang.Tradueix("Dentro de nuestras actividades de promoción de producto, llevamos a cabo un sorteo semanal entre los consumidores, en el que se pide al participante que seleccione un distribuidor donde recoger el premio si resulta agraciado.",
                                                                             "", "",
                                                                             "Dentro das nossas atividades de promoção de produto, levamos a cabo um sorteio semanal entre os consumidores, no qual se pede ao participante que selecione um distribuidor onde ir recolher o prémio se resulta ganhador.")
</p>
<p>
    @oLang.Tradueix("Nuestra intención al involucrar al distribuidor en la entrega del premio es la de publicitar su establecimiento.",
                                                                                                     "",
                                                                                                     "",
                                                                                                     "A nossa intenção ao envolver ao distribuidor na entrega do prémio é publicitar o seu estabelecimento.")
    
</p>
<p>
    @Html.Raw(oLang.Tradueix("El ganador del último sorteo <b>ha seleccionado su establecimiento</b>, por lo que si está Vd. de acuerdo en participar <b>le remitiremos el producto sin cargo</b> para que pueda efectuar la entrega.",
                                                                                                      "",
                                                                                                      "",
                                                                                                      "O ganhador do último sorteio selecionou o seu estabelecimento, pelo que se está de acordo em participar <b>remitimos-lhe o produto livre de cargos</b> para que possa efetuar a entrega. "))
    
</p>

@Html.Raw(oLang.Tradueix("Por favor responda a este mensaje dando su conformidad dentro de las <b>próximas 48 horas</b>:",
                                                                                                                     "",
                                                                                                                     "",
                                                                                                                     "Por favor responda a esta mensagem dando a sua conformidade dentro das <b>próximas 48 horas</b>:"))

<ul>
    <li>
        @oLang.Tradueix("a recibir el producto para entregarlo en su establecimiento a la persona indicada",
                                                                                         "",
                                                                                         "",
                                                                                         "a receber o produto para entregá-lo no seu estabelecimento à pessoa indicada")
       
    </li>
    <li>
        @oLang.Tradueix("a enviarnos una fotografía del acto de entrega para su publicación",
                                                                       "",
                                                                       "",
                                                                       "a enviar-nos uma fotografia do ato de entrega para a sua publicação")
    </li>
</ul>
<p>
    @oLang.Tradueix("Agradeceremos que, una vez recibido el premio, se pongan en contacto con la persona ganadora para realizar la entrega.",
                                                                                       "",
                                                                                       "",
                                                                                       "Agradeceremos que, uma vez recebido o prémio, contacte a pessoa ganhadora para realizar a entrega.")
    
</p>
<p>
    @oLang.Tradueix(" Por favor, comunique a su personal el funcionamiento de esta actividad e indíquenos el nombre de la persona o personas que se encargarán de efectuar la entrega del premio.",
                                                                           "",
                                                                           "",
                                                                           "Por favor, comunique ao seu pessoal o funcionamento desta atividade e indique-nos o nome da pessoa ou pessoas que se encarregarão de efetuar a entrega do prémio. ")
   
</p>
<p>
    @oLang.Tradueix(" A vuelta de correo le remitiremos el producto sin cargo a portes pagados",
                                                                           "",
                                                                           "",
                                                                           "Após receber a sua resposta a este correio eletrónico enviaremos o produto sem cargo com portes pagos. ")
   
</p>

@oLang.Tradueix("Recomendaciones para la fotografía del acto de entrega:",
                                                                                 "",
                                                                                 "",
                                                                                 "Recomendações para a fotografia do ato de entrega:")
<ul>
    <li>
        @oLang.Tradueix("Debe salir por lo menos la persona ganadora y el producto.",
                                                                  "",
                                                                  "",
                                                                  "Deve sair pelo menos a pessoa ganhadora e o produto.")
    </li>
    <li>
        @oLang.Tradueix("Sugerimos que salga también alguien del establecimiento, como el dueño, el encargado de la tienda o los dependientes",
                                                                  "",
                                                                  "",
                                                                  "Sugerimos que esteja também alguém do estabelecimento, como o dono, o encarregado da loja ou os empregados")
    </li>
    <li>
        @oLang.Tradueix("Vamos a publicarla en nuestra web y en nuestras redes sociales, es una buena oportunidad para hacerse publicidad. Procúrese un fondo con el nombre del establecimiento, un letrero o una bolsa con el logotipo.",
                                                                  "",
                                                                  "",
                                                                  "Publicaremos a fotografia na nossa web e nas nossas redes sociais, é uma boa oportunidade para fazer publicidade. Procure um fundo com o nome do estabelecimento, um letreiro ou uma bolsa com o logotipo.")
    </li>
    <li>
        @oLang.Tradueix("El público al que va destinado es fiel seguidor de nuestras marcas. Si en la imagen se aprecian sus logotipos o sus productos reforzará su imagen como Distribuidor Oficial.",
                                                                  "",
                                                                  "",
                                                                  "O público ao qual vai destinado é fiel seguidor das nossas marcas. Se na imagem se apreciam os seus logotipos ou os seus produtos reforçará a sua imagem como Distribuidor Oficial.")
    </li>
</ul>
<p>
    @oLang.Tradueix("Los datos de la persona ganadora del sorteo son los siguientes:",
                                                                      "",
                                                                      "",
                                                                      "Os dados da pessoa ganhadora do sorteio são os seguintes:")
</p>
<p>
    <b>@Html.Raw(FEB.Raffle.WinnerNomSync(exs, Model))</b><br />
    email: @Html.Raw(Model.Winner.User.EmailAddress)<br />
    tel.: @Html.Raw(Model.Winner.User.Tel)<br />
</p>
<p>
    @oLang.Tradueix("Los datos del sorteo están publicados en el siguiente enlace:",
                                                                                              "",
                                                                                              "",
                                                                                              "Os dados do sorteio estão publicados no seguinte link:")
    <br />
    <a href="@FEB.Raffle.ZoomUrl(Model, True)">@Model.Title</a>
</p>
<p>
    @oLang.Tradueix("Quedamos a la espera de recibir su confirmación.",
                                                                          "",
                                                                          "",
                                                                          "Aguardamos receber a sua confirmação.")
    <br />
    @oLang.Tradueix("Reciba un cordial saludo,",
                                                          "",
                                                          "",
                                                          "Receba os nossos cumprimentos, ")
</p>
