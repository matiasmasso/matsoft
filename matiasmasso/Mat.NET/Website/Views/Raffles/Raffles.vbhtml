@ModelType DTORaffle.HeadersModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
    Dim domainLang = ContextHelper.Domain.DefaultLang()
End Code


@Section AdditionalMetaTags
    @If Model.AnyActiveRaffle() Then
        @<meta property="og:image" content="@DTORaffle.ImgCallAction500Url(Model.ActiveRaffle().Guid)" />
        @<meta Property="og:image:width" content="600" />
        @<meta Property="og:image:height" content="300" />
        @<meta Property="og:image:type" content="image/jpeg">
    End If

    <meta property="og:title" content=@domainLang.Tradueix("Sorteos",,, "Sorteios") />
    <meta property="og:type" content="website" />
    <meta property="og:site_name" content="M+O MATIAS MASSO, S.A." />
    <meta property="og:url" content='@DTORaffle.Collection.LangUrl(domainLang)' />
    <meta property="og:description" content='@lang.tradueix("Participa en nuestros sorteos semanales y consulta los ganadores de los sorteos anteriores")' />
    <meta property="fb:app_id" content="@FEB.Facebook.AppId(FEB.Facebook.AppIds.MatiasMasso)" />
    <meta property="og:locale" content="@ContextHelper.Domain.TopLevelTag" />
    <meta property="article:publisher" content="https://www.facebook.com/matiasmasso.sa" />

End Section

<h1 class="Title">@ViewBag.Title</h1>

<p >

    <div class="Warning">
        <img src="~/Media/Img/Ico/warn.gif" width="16" height="16" />
        @lang.tradueix("Advertencia", "Advertència", "Warning"):
    </div>
    <div class="Warning">
        @lang.tradueix("Se han detectado perfiles falsos en Instagram, con una estética y un nombre muy similares a los de Matias Massó, S.A. y sus marcas representadas, que envían mensajes indicando que han sido el ganador/a de nuestro sorteo y solicitando sus datos personales, entre ellos el número de tarjeta bancaria. En los sorteos organizados por Matias Massó, S.A. el nombre del ganador se publica en https://www.matiasmasso.es/sorteos y nunca pedimos datos bancarios.",
                                "S'han detectat perfils falsos en Instagram, amb una estètica i nom molt similars als de Matias Masso, S.A. i les seves marques representades, que envien missatges per solicitar dades personals com el numero de targeta bancària indicant que ha guanyat el sorteig. En els sortejos organitzats per Matias Massó, S.A. el nom del guanyador es publica a https://www.matiasmasso.es/sorteos i no demanem mai dades bancaries.",
                                "We have detected fake profiles in Instagram with names and looking very close to those of Matias Masso, S.A. and represented brands, who send messages to consumers confirming they won the raffle and asking forpersonal data as credit card number. In all raffles organized by Matias Masso, S.A., the winner is publish at https://www.matiasmasso.es/sorteos and we never ask for any credit card or bank details.",
                                "Foram detetados perfis falsos em Instagram, com uma estética e nome muito idênticos aos de Matias Massó, S.A. e as marcas que representa. Enviam mensagens indicando que o usuário foi o ganhador/a do nosso sorteio e a solicitar dados pessoais, entre os quais o número bancário. Se participaste nos sorteios organizados por Matias Massó, S.A. deves consultar o nome do ganhador em https://www.matiasmasso.pt/sorteos, nunca escrevemos pessoalmente ao ganhador ou a qualquer outro seguidor do nosso perfil (a não ser que este nos tenha consultado por algum motivo) e jamais pedimos dados bancários. ")
    </div>
</p>

<div class="Raffles">
    @If Model.AnyActiveRaffle() Then
        @Html.Partial("Raffles_Active_", Model.ActiveRaffle())
    ElseIf Model.AnyNextRaffle() Then
        @Html.Partial("Raffles_Next_", Model)
    End If

    @Html.Partial("Raffles_", Model)

    @Html.Partial("Raffles_More_", Model)
</div>


<div hidden class="Error">
    <img src="~/Media/Img/Ico/warn.gif" />
    @ContextHelper.Tradueix("Error al descargar los sorteos", "Error al descarregar els sortejos", "Error downloading raffles")
</div>


@Section Styles
    <link href="~/Styles/Site.css" rel="stylesheet" />
    <link href="~/Media/Css/Raffles.css" rel="stylesheet" />
    <style scoped>

        .ContentColumn {
            width: 100%;
        }

        .Warning {
            font-style: italic;
            font-size: smaller;
        }
        .ShareButtonsModal {
            border: 1px solid gray;
            padding: 20px 30px;
            position: fixed; /* Stay in place */
            z-index: 1; /* Stay on top */
            top: 50%; /* Stay at the top */
            left: 50%;
            background-color: #fff; /* White*/
            transition: 0.5s; /* 0.5 second transition effect to slide in the sidenav */
            box-shadow: 20px 20px 10px grey;
        }

            .ShareButtonsModal .CloseModal {
                position: absolute;
                top: 0;
                right: 0;
                cursor: pointer;
            }

            .ShareButtonsModal .CopyLink {
                display: inline-block;
                border: 1px solid gray;
                padding: 4px 15px 2px 15px;
                margin: 10px;
                cursor: pointer;
            }

            .ShareButtonsModal .EmailMe {
                display: inline-block;
                border: 1px solid gray;
                padding: 4px 15px 2px 15px;
                margin: 10px;
                cursor: pointer;
            }

            .ShareButtonsModal a.EmailMe:hover, .ShareButtonsModal a.CopyLink:hover {
                background-color: #167ac6;
                color: white;
            }
    </style>
End Section

@Section Scripts
    <script src="~/Scripts/CountDown.js"></script>
    <script>

        $(document).on('click', '.ShareButtons .ShareIt', function (event) {
            event.preventDefault();

            if (navigator.share)
                NativeShare();
            else
                TraditionalShare();
        });

        $(document).on('click', '.CopyLink', function (event) {
            event.preventDefault();
            var me = event.target;
            var modal = me.closest('.ShareButtonsModal');
            CopyLink(me);
            SaveFeedback();
            $(modal).hide();
        });

        $(document).on('click', '.EmailMe', function (event) {
            event.preventDefault();
            var me = event.target;
            var modal = $me.closest('.ShareButtonsModal');
            var url = window.location.href;
            var title = document.title;
            window.open('mailto:?subject=' + title + '&body=' + url);
            SaveFeedback();
            $(modal).hide();
        });

        $(document).on('click', '.ShareButtonsModal .CloseModal', function (event) {
            event.preventDefault();
            var modal = $(this).closest('.ShareButtonsModal');
            $(modal).hide();
        })

        function TraditionalShare() {
            $('.ShareButtonsModal').show();
        }

        function NativeShare() {
            var url = window.location.href;
            var title = document.title;
            var text = url;
            navigator.share({ title: title, text: text, url: url })
                .then(() => {
                    SaveFeedback();
                }, error => {
                    console.log('Error sharing:', error)
                });
        }

        function SaveFeedback() {
            var modal = $('.Raffles .Active .ShareButtonsModal');
            var raffle = modal.data('guid');
            var url = '/raffle/share';
            var data = { raffle: raffle };
            $.post(url, data)
                .done(function (result) { console.log('shared') })
                .fail(function () { console.log('error') })
        }


        function CopyLink(me) {
            var windowLoc = window.location.href;
            $(me).after('<div class="temp-text" style="display:none;">' + windowLoc + '</div>');
            var $temp = $("<input>");
            $("body").append($temp);
            $temp.val($(me).siblings('.temp-text').text()).select();
            document.execCommand("copy");
            $temp.remove();
            $(me).siblings('.temp-text').remove();
        }

        $(document).on('click', '.Raffles .ReadMore', function () {
            event.preventDefault();
            var anchor = $(this);
            $(anchor).append(spinner20);

            var take = parseInt(anchor.data('take'), 10);
            var takeFrom = parseInt(anchor.data('takefrom'), 10);
            var totalCount = parseInt(anchor.data('totalcount'), 10);

            var moreToTakeFrom = takeFrom + take;
            var moreToTake = take;
            if (moreToTakeFrom + take > totalCount) {
                moreToTake = totalCount - moreToTakeFrom();
                $(anchor).data('take', moreToTake.toString());
            }
            $(anchor).data('takefrom', moreToTakeFrom.toString());

            var url = '/Raffles/partialRaffles';
            var data = { take: moreToTake, takeFrom: moreToTakeFrom };
            $.post(url, data)
                .done(function (result) {
                    spinner20.remove();
                    $(anchor).before(result);
                    $(anchor).find('.Take').text(moreToTake.toString());
                    $(anchor).find('.LeftCount').text((totalCount - moreToTakeFrom - moreToTake).toString());
                    $('.Raffles .Error').css('display', 'flex');
                })
                .fail(function () {
                    spinner20.remove();
                    $('.Raffles .Error').css('display', 'flex');
                })
        });

        var fchFrom;

        $(document).ready(function () {
            fchFrom = new Date('@Model.NextFch.ToString("yyyy-MM-ddTHH:mm:ssZ")');
            var div = $('.Raffles .Countdown');
            //InitCountdown($(div), fchFrom);
            //div.startCountDown();
            //div.fadeIn(3000);
        });

            $(document).on('onCountdownComplete', function (e, argument) {
                location.reload();
            });


        function InitCountdown(div, fch) {
            div.countDown({
                targetOffset: {
                    'day': 0,
                    'month': 0,
                    'year': 0,
                    'hour': 0,
                    'min': 0,
                    'sec': 0
                }
            });

            div.setCountDown({
                targetDate: {
                    'day': fchFrom.getDay(),
                    'month': fchFrom.getMonth(),
                    'year': fchFrom.getFullYear(),
                    'hour': fchFrom.getHours(),
                    'min': fchFrom.getMinutes(),
                    'sec': fchFrom.getSeconds()
                },
                onComplete: function () {
                    $(document).trigger('onCountdownComplete', 'argument');
                }
            });
        }



    </script>

End Section
