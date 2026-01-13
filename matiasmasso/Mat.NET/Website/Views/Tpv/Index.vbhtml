@ModelType DTOTpvRequest

@Code
    ViewBag.Title = "Tpv"
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

<div class="LeftSide">
    <h1>@ViewBag.Title</h1>

    <p>
        @Html.Raw(ContextHelper.Tradueix("al pulsar 'Siguiente' será reenviado a la página segura de nuestra entidad bancaria",
                                                         "al premer 'Següent' será reenviat a la página segura de la nostre entitat bancaria",
                                                         "on clicking 'Next' you'll be forwarded to a safe page on our bank website"))
    </p>


    @Using (Html.BeginForm())
        @Html.Partial("_ValidationSummary", ViewData.ModelState)


        @<div class="row">
            <div Class='label'>
                @ContextHelper.Tradueix("concepto del pago", "concepte del pago", "payment concept")
            </div>
            <div Class="control">
                @Html.TextBoxFor(Function(Model) Model.Concept, New With {.maxLength = "50"})
            </div>
        </div>

        @<div Class="row">
            <div Class='label'>
                @ContextHelper.Tradueix("importe", "import", "amount")
            </div>

            <div Class="control">

                @If Model.Mode = DTOTpvRequest.Modes.Free And Model.Eur = 0 Then
                    @Html.TextBoxFor(Function(Model) Model.Eur, New With {.style = "text-align:right;", .Value = String.Format("{0:N2} €", Model.Eur)})
                Else
                    @*@Html.TextBoxFor(Function(Model) Model.Eur, New With {.style = "text-align:right;", .Value = DTOAmt.Factory(Model.Eur).CurFormatted(), .disabled = "disabled"})*@
                    @Html.TextBoxFor(Function(Model) Model.Eur, New With {.style = "text-align:right;", .Value = String.Format("{0:N2} €", Model.Eur), .disabled = "disabled"})
                End If

            </div>
        </div>

        @<div id="submit" Class="SubmitRow">
            <input type="submit" Class="Submit" value='@ContextHelper.Tradueix("Siguiente", "Següent", "Next")' />
        </div>


        @Html.HiddenFor(Function(Model) Model.Eur)
        @Html.HiddenFor(Function(Model) Model.Mode)
        @Html.HiddenFor(Function(Model) Model.Ref)
    End Using

</div>

<div class="RightSide">
    <div class="Noticias"></div>
    <div class="readmore">
        <a href="/noticias">
            @lang.Tradueix("ver todas las noticias...", "veure totes les noticies...", "see all news...")
        </a>
    </div>
</div>

@Section Styles
    <link href="~/Media/Css/Noticias.css" rel="stylesheet" />
    <style>
        .ContentColumn {
            display:flex;
            flex-direction: row;
            justify-content: space-between;
            column-gap: 30px;
            max-width:100%;
        }

        .LeftSide {
            max-width: 400px;
        }

        .LeftSide input[type=text] {
            box-sizing: border-box;
        }

        .RightSide {
            max-width: 280px;
            max-height: 450px;
            overflow-y: hidden;
        }


        .row {
            display: flex;
        }

        .label {
            padding: 4px 7px 2px 4px;
            width: 200px;
        }

        .control input {
            padding: 4px 7px 2px 4px;
            font-size: 1em;
            max-width: 100%;
            width: 100%;
        }

        @@media screen and (max-width: 1000px) {

            .RightSide {
                display:none;
            }
        }
    </style>
End Section

@Section Scripts
    <script src="~/Media/js/Comments.js"></script>
    <Script src="~/Media/js/Noticias.js"></Script>

    <Script>
        $(document).ready(function () {
            loadLastNews();
        })

        function loadLastNews() {
            var postsDiv = $('.RightSide .Noticias');
            var url = '/Noticias/PartialNoticias/@lang.Tag';
            postsDiv.load(url, function () { })
        }

    </Script>
End Section

