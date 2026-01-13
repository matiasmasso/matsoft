@ModelType List(Of DTOCredencial)
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang = ContextHelper.Lang
End Code

<div>
    <input type="text" class="SearchBox" />
</div>

<div class="Grid" data-contextmenu="Credencials">
    <div></div>
    @For Each item In Model
        @<div class="Row" data-guid="@item.Guid.ToString()" data-lowercase="@item.referencia.ToLower()">
            <div>@item.referencia</div>
        </div>
    Next

</div>

<div class="ContextMenu" data-grid="Credencials">
    <a href="#" data-url="/pro/proCredencial/index/{guid}">@lang.Tradueix("Ficha", "Fitxa", "Properties")</a>
    <a href="#" data-action="Browse" target="_blank">@lang.Tradueix("Navegar", "Navegar", "Browse")</a>
    <a href="#" data-action="CopyUser">@lang.Tradueix("Copiar usuario", "Copiar usuari", "Copy user name")</a>
    <a href="#" data-action="CopyPwd">@lang.Tradueix("Copiar contraseña", "Copiar clau de pas", "Copy password")</a>
    <hr />
    <a href="#" data-action="AddNew">@lang.Tradueix("Añadir nueva", "Afegir nova", "Add new")</a>
</div>

@Section Scripts
    <script src="~/Media/js/ContextMenu.js"></script>

    <script>
        var model = @Html.Raw(DTOBaseGuid.Serialized(Model))

           /*
        $(document).on('click', 'a', function (e) {
            event.preventDefault();
            var parent = $(this).parent();
            alert($(parent).data('menu'));
        });
        */


        $(document).on('keyup', '.SearchBox', function (e) {
            var searchKey = $(this).val().toLowerCase();
            if (searchKey == '') {
                $('.Grid [data-lowercase]').show();
            } else {
                $('.Grid [data-lowercase]').hide();
                $('.Grid').find('[data-lowercase*="' + searchKey + '"]').show();
            }
        });

 
        $(document).on('ContextMenuClick', function (e, argument) {
            var matches = model.filter(function (item) { return item.Guid === argument.guid; });
            var item = matches[0];
            switch (argument.action) {
                case "Browse":
                    var url = item.Url;
                    window.open(url, '_blank');
                    break;
                case "CopyUser":
                    copyToClipboard(item.Usuari);
                    break;
                case "CopyPwd":
                    copyToClipboard(item.Password);
                    break;
                case "AddNew":
                    window.location.href = "/pro/proCredencial/factory/";
                    break;
            }
        });


    </script>
End Section


@Section Styles
    <style>
        .MainContent {
            max-width: 600px;
        }

        .SearchBox {
            box-sizing: border-box;
            display: block;
            width: 300px;
            font-size: 1em;
            background-image: url('/Media/Img/Ico/magnifying-glass.jpg');
            background-repeat: no-repeat;
            background-position: right;
            border: 1px solid grey;
            padding: 4px 7px 2px 4px;
        }

        .Grid {
            display: grid;
            grid-template-rows: 1fr;
            margin-top: 20px;
        }

            .Grid a {
                display: block;
                padding: 4px 7px 2px 4px;
            }

                .Grid a:hover {
                    background-color: #167ac6;
                    color: white;
                    cursor: pointer;
                }
    </style>
End Section

