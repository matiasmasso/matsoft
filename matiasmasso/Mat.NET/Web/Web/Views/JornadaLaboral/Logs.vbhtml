@Code
    Layout = "~/Views/shared/_Layout_Minimal.vbhtml"
    Dim lang As DTOLang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim oUser = Mvc.ContextHelper.GetUser()
    Dim caption = lang.Tradueix("Registro de Jornada Laboral", "Registre de Jornada Laboral", "Work day logs")
End Code

<div class="PageWrapper">
    <h2>@caption</h2>
    <h2 id="StaffNom" class="Truncate"></h2>

    <noscript>
        <div id="NoScript">
            Este dispositivo tiene deshabilitado JavaScript, que es imprescindible para fichar.<br/>
            Puedes habilitarlo en la cofiguración del navegador
        </div>
    </noscript>

    <div id="Loading" hidden="hidden">
        <div>
            @lang.Tradueix("Fichando...", "Fitxant...", "Logging...")
        </div>
        <img src="~/Media/Img/preloaders/Spinner64px.gif" />
    </div>

    <div class="Grid"></div>

    <a href="#" id="RemoveLast" hidden="hidden">
        <span>
            <img src="~/Media/Img/Ico/bin-white-20.png" />
            @lang.Tradueix("retroceder el último registro", "retrocedeix el darrer registre", "remove last log")
        </span>
    </a>

    <input type="hidden" id="DataUrl" value="@MmoUrl.ApiUrl("JornadesLaborals/fromUser", oUser.Guid.ToString())" />
    <input type="hidden" id="RemoveLastUrl" value="@MmoUrl.ApiUrl("JornadesLaborals/removeLast", oUser.Guid.ToString())" />
    <input type="hidden" id="Lang" value="@lang.Tag" />
</div>

@Section Scripts
    <script>
        var model = {}; //Models.JornadesLaboralsModel
        var months = [];
        var weekdays = [];
        var year = -1;
        var month = -1;
        var visibleYear = 0;
        var visibleMonth = 0;

        $(document).ready(function () {
            loadLangTexts();
            var url = $('#DataUrl').val();
            loadData(url);
        });

        $(document).on('click', '#RemoveLast', function (e) {
            var url = $('#RemoveLastUrl').val();
            loadData(url);
        });


        $(document).on('click', '[data-lin="year"] a', function (e) {
            var row = $(this).closest('.Row');
            var year = $(row).data('year');
            $('.Grid .Row').addClass('Hidden');

            if ($(row).hasClass('Collapsed')) {
                $(row).removeClass('Collapsed');
                var yearRows = $('.Grid .Row[data-lin="year"][data-year="' + year + '"]');
                var monthRows = $('.Grid .Row[data-lin="month"][data-year="' + year + '"]');
                $(yearRows).removeClass('Hidden');
                $(monthRows).removeClass('Hidden');
                $(monthRows).addClass('Collapsed');
            } else {
                $(row).addClass('Collapsed');
                var rows = $('.Grid .Row[data-lin="year"]');
                $(rows).removeClass('Hidden');
            }
        });

        $(document).on('click', '[data-lin="month"] a', function (e) {
            var row = $(this).closest('.Row');
            var year = $(row).data('year');
            var month = $(row).data('month');
            var yearRows = $('.Grid .Row[data-lin="year"][data-year="' + year + '"]');
            $('.Grid .Row').addClass('Hidden');

            if ($(row).hasClass('Collapsed')) {
                $(row).removeClass('Collapsed');
                var rows = $('.Grid .Row[data-year="' + year + '"][data-month="' + month + '"]');
                $(yearRows).removeClass('Hidden');
                $(rows).removeClass('Hidden');

            } else {
                $(row).addClass('Collapsed');
                var monthRows = $('.Grid .Row[data-lin="month"][data-year="' + year + '"]');
                $(yearRows).removeClass('Hidden');
                $(monthRows).removeClass('Hidden');
            }
        });

        function loadGrid() {
            var staff = model.Staffs[0];
            $('#StaffNom').html(staff.Nom);

            var items = staff.Items;
            visibleYear = getYear(items[0]);
            visibleMonth = getMonth(items[0]);
            year = -1;
            month = -1;
            $('.Grid .Row').remove();

            $.each(items, function (index, item) {
                addYearIfNeeded(item);
                addMonthIfNeeded(item);
                addDay(item);
            });
        }


        function addYearIfNeeded(item) {
            if (getYear(item) != year) {
                year = getYear(item);
                var row = $('<div class="Row" data-lin="year" data-year="' + year + '"></div>');
                if (isHidden(item))
                    $(row).addClass('Hidden Collapsed');
                $('.Grid').append(row);
                row.append('<a href="#">' + year + '</a>');
                row.append('<div></div>');
                row.append('<div></div>');
            }
        }

        function addMonthIfNeeded(item) {
            if (getMonth(item) != month) {
                month = getMonth(item);
                var row = $('<div class="Row" data-lin="month" data-year="' + year + '" data-month="' + month + '"></div>');
                if (isHidden(item))
                    $(row).addClass('Hidden Collapsed');

                $('.Grid').append(row);
                row.append('<a href="#">' + months[month] + '</a>');
                row.append('<div></div>');
                row.append('<div></div>');
            }
        }

        function addDay(item) {
            var row = $('<div class="Row"  data-lin="item" data-year="' + year + '" data-month="' + month + '" data-guid="' + item.Guid + '"></div>');
            if (isHidden(item))
                $(row).addClass('Hidden');
            $('.Grid').append(row);
            row.append('<div>' + formattedDay(item.FchFrom) + '</div>');
            row.append('<div>' + formattedTime(item.FchFrom) + '</div>');
            row.append('<div>' + formattedTime(item.FchTo) + '</div>');
        }

        function isHidden(item) {
            retval = true;
            var itemYear = getYear(item);
            var itemMonth = getMonth(item);
            if (itemYear === visibleYear && itemMonth === visibleMonth)
                retval = false;
            return retval;
        }

        function loadData(url) {

            $('body').removeClass('Success Warn');
            $('#Loading').show();
            $('#RemoveLast').hide();
            var jqxhr = $.getJSON(url, function (result) {
                model = result;
            })
                .done(function () {
                    loadGrid();
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#Errors').html('<div class="Error">' + textStatus + '<br/>' + errorThrown + '</div>');
                })
                .always(function () {
                   $('#Loading').hide();
                    if (isMissingFch()) {
                        $('body').addClass('Warn');
                        $('body').removeClass('Success');
                    }
                    else {
                        $('body').removeClass('Warn');
                        $('body').addClass('Success');
                    }
                   $('#RemoveLast').show();
                });
        }


        function loadLangTexts() {
            switch ($('#Lang').val()) {
                case 'CAT':
                    months = ['Gener', 'Febrer', 'Març', 'Abril', 'Maig', 'Juny', 'Juliol', 'Agost', 'Setembre', 'Octubre', 'Novembre', 'Desembre'];
                    weekdays = ['diumenge', 'dilluns', 'dimarts', 'dimecres', 'dijous', 'divendres', 'dissabte'];
                    break;
                case 'ENG':
                    months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
                    weekdays = ['sunday', 'monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];
                    break;
                default:
                    months = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];
                    weekdays = ['domingo', 'lunes', 'martes', 'miercoles', 'jueves', 'viernes', 'sábado'];
                    break;
            };
        }


        function isMissingFch() {
            var retval = false;
            var staff = model.Staffs[0];
            var items = staff.Items;
            $.each(items, function (index, item) {
                if (isEmptyFch(item.FchTo) && !isToday(item.FchFrom)) {
                    retval = true;
                    return false;
                }
            });
            return retval;
        }

        function getYear(item) {
            var date = new Date(item.FchFrom);
            var retval = date.getFullYear();
            return retval;
        }

        function getMonth(item) {
            var date = new Date(item.FchFrom);
            var retval = date.getMonth();
            return retval;
        }

        function formattedDay(fch) {
            var date = new Date(fch);
            var day = date.getDate();
            var weekday = date.getDay();
            var retval = padLeadingZeros(day, 2) + ' ' + weekdays[weekday];
            return retval;
        }

        function formattedTime(fch) {
            var retval = '';
            if (!isEmptyFch(fch)) {
                var date = new Date(fch);
                retval = padLeadingZeros(date.getHours(), 2) + ':' + padLeadingZeros(date.getMinutes(),2);
            }
            return retval;
        }

        function isEmptyFch(fch) {
            return (fch == '0001-01-01T00:00:00')
        }

        function isToday(fch) {
            var today = new Date().toDateString();
            var date = new Date(fch).toDateString();
            var retval = (date === today);
            return retval;
        }

        function padLeadingZeros(num, size) {
            var s = num + "";
            while (s.length < size) s = "0" + s;
            return s;
        }
    </script>
End Section

@Section Styles
    <style scoped>
         body.Success {
            background-color: lightgreen;
        }

            body.Warn {
                background-color: lightsalmon;
            }

        .PageWrapper {
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
            max-width: 320px;
            margin: auto;
        }

        #Loading {
            padding: 10px 20px 10px 20px;
            display: flex;
            align-content: center;
            justify-content: center;
            gap: 10px;
        }
            #Loading div {
                margin: auto 0;
            }

        .Grid {
            display: grid;
            grid-template-columns: minmax(110px, 1fr) 60px 60px;
            margin-bottom: 20px;
        }

        .Row {
            display: contents;
        }

            .Row div, .Row a {
                display: block;
                width: 100%;
                border-bottom: 1px solid black;
                padding: 10px 7px 10px 4px;
            }

            .Row.Hidden div, .Row.Hidden a {
                display: none;
            }

        #RemoveLast {
            margin-top: 50px;
            padding: 10px 20px 10px 20px;
            background-color: red;
            color: white;
            border-radius: 5px;
        }
        #RemoveLast span {
            display: flex;
            justify-content: center;
            gap: 10px;
        }

        .Truncate {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
    </style>
End Section



