@ModelType  DTOCustomerRanking
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    ViewBag.Title = "CustomerRanking"

    Dim exs As New List(Of Exception)
    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oLang As DTOLang = oUser.lang
    Dim sFchFrom As String = Format(Model.FchFrom, "yyyy-MM-dd")
    Dim sFchTo As String = Format(Model.FchTo, "yyyy-MM-dd")
    Dim oCountries As List(Of DTOCountry) = FEB2.Countries.AllSync(oUser, exs)
    Dim sTitle As String = Mvc.ContextHelper.Tradueix("Ranking clientes", "Ranking clients", "Customers ranking")
End Code



    <section class="title">
        <h1>@Html.Raw(sTitle)</h1>
        <a id="Csv" href='@FEB2.CustomerRanking.CsvUrl(Model)' title='@(Mvc.ContextHelper.Tradueix("Descargar fichero Excel", "Descarregar fitxer Excel", "Download Excel file")) '>
            <img class="Excel" src="~/Media/Img/48x48/Excel48.png" />
        </a>
    </section>

    <div class="filters">
        <div>
            @Mvc.ContextHelper.Tradueix("Desde", "Des de", "From")
            <input id="FchFrom" type="date" value="@sFchFrom" />
        </div>
        <div>
            @Mvc.ContextHelper.Tradueix("Hasta", "Fins", "To")
            <input id="FchTo" type="date" value="@sFchTo" />
        </div>

        <div @IIf(oCountries.Count = 1, "hidden='hidden'", "")>
            <select id="Country">
                @If oCountries.Count = 1 Then
                    @<option value="@oCountries(0).Guid.ToString" selected></option>
                Else
                    @<option value="@Guid.Empty.ToString">@Mvc.ContextHelper.Tradueix("Seleccionar país", "Sel·leccionar país", "Select country")</option>
                    @For Each oCountry As DTOCountry In oCountries
                        @<option value="@oCountry.Guid.ToString">@oCountry.LangNom.Tradueix(oLang)</option>
                    Next
                End If
            </select>
        </div>

        <div hidden='hidden' class="Zona">
            <select id="Zona">
                <option value="@Guid.Empty.ToString">@Mvc.ContextHelper.Tradueix("Seleccionar zona", "Sel·leccionar zona", "Select area")</option>
            </select>
        </div>

        <input type="button" id="Submit" value="@Mvc.ContextHelper.Tradueix("Actualizar", "Actualitzar", "Update")">
    </div>


    <div id="Detail">
        @Html.Partial("_CustomerRanking", Model)
    </div>


@Section Scripts
    <script>
        $(document).on('change', '#Country', function (event) {
            $('.loading').show();

            $.getJSON(
                 '@Url.Action("Zonas")',
                 { country: $('#Country').val() },
                 function (result) {
                     $('.loading').hide();

                     $('#Zona').children('option:not(:first)').remove();
                     $.each(result, function (key, item) {
                         $('#Zona').append('<option value=' + item.Guid + '>' + item.Nom + '</option>');
                     });

                     $('.Zona').show();
                 })
        });

        $(document).on('click', '#Submit', function (event) {
            $('.loading').show();
            var data;

            if ($('#Zona').val() == '@Guid.Empty.ToString') 
                data = {
                    FchFrom: $('#FchFrom').val(),
                    FchTo: $('#FchTo').val(),
                    Area: $('#Country').val()
                };
            
            else
                data = {
                    FchFrom: $('#FchFrom').val(),
                    FchTo: $('#FchTo').val(),
                    Area: $('#Zona').val()
                };

            var json = JSON.stringify(data);

            $('#Detail').load(
                '@Url.Action("reload")',
                {data: json}
                )

            $.getJSON(
                '@Url.Action("csvUrl")',
                {data: json},
                function (response) {
                    $('#Csv').attr('href',response.result)
                    $('.loading').hide();
                })

        })
    </script>
End Section


@Section Styles
    <style scoped>

        .title {
            display: flex;
            flex-direction: row;
            justify-content: space-between;
            vertical-align: middle;
        }

        .filters {
            display: flex;
            flex-direction: row;
            justify-content: flex-start;
            column-gap: 15px;
            margin-bottom: 20px;
            align-items: center;
        }

        .filters select {
            height: 24px;
            font-size: 1em;
        }

        .Grid {
            display: grid;
            grid-template-columns: 80px 10fr 120px 70px 70px;
            border-top: 1px solid gray;
            border-right: 1px solid gray;
        }

            .Grid > span, .Grid > .Row > span {
                padding: 8px 4px 2px 4px;
                border-left: 1px solid gray;
                border-bottom: 1px solid gray;
            }

            /*(2 x num.de columnes n + num.de columnes+1
                .Grid > .Row > span:nth-child(10n+6),
                .Grid > .Row > span:nth-child(10n+7),
                .Grid > .Row > span:nth-child(10n+8),
                .Grid > .Row > span:nth-child(10n+9),
                .Grid > .Row > span:nth-child(10n+10) {
                    background-color: #EFEFEF;
                }
                    */

            .Grid > .Row:nth-child(2n+1) {
                background-color: #EFEFEF;
            } 

        .Grid .Row {
            display: contents;
        }

            .Grid .Row:hover span {
                background-color: #dbeaf4;
                cursor: pointer;
            }

        .CellId {
            text-align: center;
        }

        .CellAmt, .CellPct {
            text-align: right;
        }

        .CellNom {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            width:100%;
        }

        @@media(max-width:600px) {
            .Grid {
                grid-template-columns: 80px 10fr 120px;
            }

            .Grid {
                border-right: none;
            }

                .Grid > span, .Grid > .Row > span {
                    border-left: none;
                }


           .CellPct {
                display: none;
            }
        }
        @@media(max-width:400px) {
            .Grid {
                grid-template-columns: 80px 10fr;
            }

            .Row.Total {
                display: none;
            }

            .CellAmt {
                display: none;
            }
        }
    </style>
End Section



