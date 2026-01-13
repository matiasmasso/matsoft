@ModelType List(Of DTOTransmisio)
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang As DTOLang = Mvc.ContextHelper.Lang

End Code

<div class="ContentWrapper">


    <div class="Grid">
        <div class="Row">
            <div>@lang.Tradueix("Transmisión", "Transmisió", "Transmission")</div>
            <div>@lang.Tradueix("Fecha", "Data", "Date")</div>
        </div>

        @For Each item In Model
            @<div class="Row" data-guid="@item.Guid.ToString()">
                <div><input type="checkbox" class="Transmisions" data-tag="@item.Guid.ToString()" />&nbsp;@item.Id</div>
                <div>@item.Fch.ToString("dd/MM/yy HH:mm")</div>
            </div>
        Next
    </div>

    <div class="CallToAction">
        <div class="Info">@lang.Tradueix("Procedimiento", "Procediment", "Procedure"):</div>
        <ol>
            <li>@lang.Tradueix("Seleccionar las transmisiones afectadas en la lista anterior", "Marcar les transmisions afectades a la llista anterior", "Check involved transmissions on previous list")</li>
            <li>
                @lang.Tradueix("Introducir la fecha correcta en la siguiente casilla", "Introduir la nova data a la casella següent", "Enter updated date on next box"):
                <div>
                    <input type="date" id="Fch" value='@Today.ToString("yyyy-MM-dd")'>
                </div>

            </li>
            <li>
                @lang.Tradueix("Descargar el Excel de la plantilla pulsando el siguiente botón", "Descarregar l'Excel de la plantilla pulsant el següent botó", "Download Excel template from next button"):
                <div>
                    <input type="button" id="SubmitButton" value='@lang.Tradueix("Descargar Excel", "Descarregar Excel", "Download Excel")' />
                </div>

            </li>
        </ol>
    </div>
</div>


@Section Scripts
    <script>

        $(document).on('click', '#SubmitButton', function (e) {
            var checkboxes = $(".Transmisions:checked");
            if (checkboxes.length == 0) {
                alert('@lang.Tradueix("Selecciona primero las transmisiones afectadas", "Selecciona primer les transmisions afectades", "You must first check involved transmisions")');
            } else {
                downloadExcel(checkboxes);
            }
        });

        function downloadExcel(checkboxes) {
            startPreloader();
            var url = '/EciTransmTemplate/Excel';
            var data = formData(checkboxes);
            fetch(url, {method:'POST', body:data})
                .then(response => {
                    if (response.ok) {
                        return response.blob();
                    } else {
                        //throw error to be catched
                        throw new Error('Something went wrong');
                    }
                })
                .then(blob => {
                    const url = window.URL.createObjectURL(blob);
                    const a = document.createElement('a');
                    a.style.display = 'none';
                    a.href = url;
                    a.download = 'M+O-Plantilla ECI modificacion fecha de entrega.xlsx';
                    document.body.appendChild(a);
                    a.click();
                    window.URL.revokeObjectURL(url);
                })
                .then(() => {
                    stopPreloader();
                })
                .catch(() => {
                    stopPreloader();
                    alert('@lang.Tradueix("error al descargar el fichero Excel", "Error al descarregarel fitxer Excel", "Error on downloading the Excel file")');
                });

        }

        function formData(checkboxes) {
            var retval = new FormData();
            retval.append('fch', $('#Fch').val());
            $(checkboxes).each(function () {
                retval.append('transms[]', $(this).data("tag"));
            });
            return retval;
        }

        function startPreloader() {
            $('#SubmitButton').hide();
            $('#Fch').append(spinner20);
        }

        function stopPreloader() {
            spinner.remove();
            $('#SubmitButton').show();
        }

    </script>
End Section



@Section Styles
    <style scoped>
        .MainContent {
            max-width: 600px;
            margin: auto;
        }

        .ContentWrapper {
            display: flex;
            justify-content: space-around;
        }

        .Info {
            padding-top: 20px;
        }

        li {
            padding-bottom: 20px;
        }

        #Fch, #SubmitButton {
            margin-top: 15px;
        }

        .Grid {
            grid-template-columns: 100px 150px;
        }

            .Grid .Row div {
                padding: 4px 7px 2px 4px;
                text-overflow: ellipsis;
                white-space: nowrap;
                overflow: hidden;
            }

            .Grid .Row .Icon.Enabled {
                background-image: url('/Media/Img/Ico/ok.png');
                padding-right: 10px;
            }

        @@media screen and (max-width: 500px) {
            .ContentWrapper {
                flex-direction: column;
                justify-content: flex-start;
            }
        }
    </style>
End Section
