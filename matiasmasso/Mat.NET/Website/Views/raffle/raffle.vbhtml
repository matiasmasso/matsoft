@Modeltype DTORaffle
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)

    ViewBag.Title = Model.Title
    Dim exs As New List(Of Exception)
    FEB.RaffleParticipant.Load(exs, Model.Winner)
    Dim oWinner = Model.Winner.User
    If oWinner.Nom = "" Then FEB.User.Load(exs, oWinner)

End Code

@Section AdditionalMetaTags
    <meta property="og:title" content="¡Participa en este fantástico sorteo!" />
    <meta property="og:site_name" content="M+O Sorteos y concursos" />
    <meta property="og:url" content="@FEB.Raffle.PlayOrZoomUrl(Model, True)" />
    <meta property="og:description" content="@Model.Title" />
    <meta property="fb:app_id" content="@FEB.Facebook.AppId(FEB.Facebook.AppIds.MatiasMasso)" />
    <meta property="og:locale" content="es_ES" />
    <meta property="article:publisher" content="https://www.facebook.com/matiasmasso.sa" />
    <meta property="og:image" content="@FEB.Raffle.ImgCallAction500(Model)" />
End Section



<div class="banner">
    <img src="@FEB.Raffle.ImgBanner600Url(Model)" width="600" height="200" />
</div>

<h1 class="Title">@ViewBag.Title</h1>

<div>
    <div class="caption">
        @ContextHelper.Tradueix("fecha inicio", "data inici", "start up", "data inicio"):
    </div>
    <div class="data">
        @Model.FchFrom.ToString("dd/MM/yy")
    </div>
</div>

<div>
    <div class="caption">
        @ContextHelper.Tradueix("fecha finalización", "data finalització", "deadline", "data finalização"):
    </div>
    <div class="data">
        @Model.FchTo.ToString("dd/MM/yy")
    </div>
</div>

<br />

<div>
    <div class="caption">
        @ContextHelper.Tradueix("producto sorteado", "producte sortejat", "prize", "produto sorteado"):
    </div>
    <div class="data">
        <a href="@Model.Product.GetUrl(ContextHelper.Lang)">@Model.Product.FullNom()</a>
    </div>
</div>


@If Model.Winner IsNot Nothing Then
    @<div>
        <div class="caption">
            @ContextHelper.Tradueix("participación", "participació", "engagement", "participação"):
        </div>
        <div class="data">
            @(Format(Model.ParticipantsCount, "#,##0") & " " & ContextHelper.Tradueix("participantes", "participants", "players", "participantes"))
        </div>
    </div>  End If


@If Model.Answers IsNot Nothing Then
    If Model.Answers.Count > 0 And Model.RightAnswer > 0 Then
        @<div>
            <div class="caption">
                @ContextHelper.Tradueix("respuesta correcta", "resposta correcte", "right answer", "resposta correcta"):
            </div>
            <div class="data truncate">
                @If Model.Answers.Count >= Model.RightAnswer Then
                    @Model.Answers(Model.RightAnswer - 1)
                End If
            </div>
        </div>
    End If
End If

<br />

<div>
    <div class="caption">
        @ContextHelper.Tradueix("ganador", "guanyador", "winner", "ganhador"):
    </div>
    <div class="data truncate">
        @If Model.Winner IsNot Nothing Then
            @DTORaffleParticipant.FormattedNumAndNom(Model.Winner)
        End If
    </div>
</div>

@If Model.Winner IsNot Nothing Then
    If Model.Winner.Distribuidor IsNot Nothing Then

        @<div>
            <div class="caption">
                @ContextHelper.Tradueix("distribuidor", "distribuïdor", "distributor", "distribuidor"):
            </div>
            <div class="data truncate">
                @If Model.Winner.Distribuidor IsNot Nothing Then
                    @Html.Raw(FEB.RaffleParticipant.DistributorNameAndAddressHtml(Model.Winner))
                End If
            </div>
        </div>

    End If
End If


<div>
    @If Model.ImageWinner IsNot Nothing Then
        @<div class="ImageWinner">
            <img src="@FEB.Raffle.ImgWinnerUrl(Model)" />
        </div>
    End If
</div>


<p>
    <a href="@FEB.Raffles.Url(ContextHelper.lang())">(@ContextHelper.Tradueix("ver todos los sorteos", "veure tots els sortejos", "see all raffles", "ver todos os sorteios"))</a>
</p>




@Section Styles
    <style>
        .ContentColumn {
            max-width: 600px;
            margin: 0 auto;
        }


        .banner img {
            width: 100%;
            height: auto;
            margin-bottom: 20px;
        }

        .caption {
            display: inline-block;
            width: 150px;
            vertical-align: top;
        }

        .data {
            display: inline-block;
            vertical-align: top;
            width: 300px;
            margin-left: 20px;
        }

        .ImageWinner {
            margin-top: 20px;
        }

            .ImageWinner img {
                width: 100%;
            }
    </style>
End Section
