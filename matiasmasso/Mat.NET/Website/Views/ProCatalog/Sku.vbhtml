@ModelType DTOProductSku.Model
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang As DTOLang = ContextHelper.Lang
End Code

<div class="SkuContainer">
    <div class="Properties">
        <div>
            @lang.Tradueix("M+O id")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.Id</span>
            <input type="text" data-prop="Id" class="EditControl" value="@Model.Id" />
        </div>
        <div>
            @lang.Tradueix("Marca", "Marca", "Brand name")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.Brand.nom</span>
            <input type="text" data-prop="Brand" class="EditControl" value="@Model.Brand.nom" />
        </div>
        <div>
            @lang.Tradueix("Categoría", "Categoria", "Category")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.Category.nom</span>
            <input type="text" data-prop="Category" class="EditControl" value="@Model.Category.nom" />
        </div>
        <div>
            @lang.Tradueix("Producto", "Producte", "Product")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.NomEsp</span>
            <input type="text" data-prop="NomEsp" class="EditControl" value="@Model.NomEsp" />
        </div>
        <div>
            @lang.Tradueix("Nombre completo", "Nom complert", "Full name")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.NomLlarg</span>
            <input type="text" data-prop="NomLlarg" class="EditControl" value="@Model.NomLlarg" />
        </div>
        <div>
            @lang.Tradueix("EAN")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.Ean</span>
            <input type="text" data-prop="Ean" class="EditControl" value="@Model.Ean" />
        </div>
        <div>
            @lang.Tradueix("Ref fabr.", "Ref fabr.", "Manuf.code")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.Ref</span>
            <input type="text" data-prop="Ref" class="EditControl" value="@Model.Ref" />
        </div>
        <div>
            @lang.Tradueix("Disponibilidad", "Disponibilitat", "Availability")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.ManufacturerAvailabilityText(ContextHelper.Lang)</span>
            <input type="text" data-prop="NomEsp" Class="EditControl" value="@Model.FchObsoleto" />
        </div>


        <div class="SubmitRow">
            <div>
                <input type="button" class="Cancel" value='@lang.Tradueix("Cancelar", "Cancel·lar", "Cancel")' />
                <input type="button" class="Delete" value='@lang.Tradueix("Eliminar", "Eliminar", "Delete")' disabled />
                <input type="button" class="Submit" value='@lang.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' disabled />
            </div>
        </div>

    </div>
    <div class="ImgContainer">
        <img src="@Model.ImageUrl()" />
    </div>
</div>

@Section Scripts
    <script src="~/Media/js/ContextMenu.js"></script>
    <script>
        var model = @Html.Raw(System.Web.Helpers.Json.Encode(Model));

        $(document).ready(function () {
            if (model.IsNew) {
                $('.EditButton').css('visibility','hidden');
                $('.EditButton').next().children().toggle();
            }
            if (model.ReadOnly) {
                $('.EditButton').css('visibility', 'hidden');
            }
        })

        $(document).on('click', '.EditButton', function () {
            $(this).next().children().toggle();
        });

        $(document).on('click', '.SubmitRow .Submit', function () {
            Update();
        });

        $(document).on('click', '.SubmitRow .Delete', function () {
            Delete();
        });

        $(document).on('click', '.SubmitRow .Cancel', function () {
            location.href = '/pro/proCatalog';
        });

        $(document).on('change', '.EditControl', function () {
            $('.SubmitRow .Submit').attr("disabled", false);
        });

        function Update() {
            $('.SubmitRow .Submit').toggle();
            $('.SubmitRow').append(spinner20);

            model.Referencia = $('.Properties').find('[data-prop=Referencia]').val();
            model.Url = $('.Properties').find('[data-prop=Url]').val();
            model.Usuari = $('.Properties').find('[data-prop=Usuari]').val();
            model.Password = $('.Properties').find('[data-prop=Password]').val();
            model.Obs = $('.Properties').find('[data-prop=Obs]').val();


            var url = "/pro/proCatalog/update";
            $.post(url, model, function (result) {
            })
                .done(function (result) {
                    spinner20.remove();
                    if (result.success) {
                        location.href = '/pro/proCatalog';
                    } else {
                        $('.SubmitRow .Submit').toggle();
                        alert('failed: ' + result.message);
                    }
                })
                .fail(function (xhr, textStatus, errorThrown) {
                    spinner20.remove();
                    $('.SubmitRow .Submit').toggle();
                    alert('failed: ' + xhr.responseText);
                })
        }

        function Delete() {
            $('.SubmitRow .Delete').toggle();
            $('.SubmitRow .Delete').after(spinner20);

            var url = "/pro/proCredencial/delete";
            $.post(url, model, function (result) {
            })
                .done(function (result) {
                    spinner20.remove();
                    if (result.success) {
                        location.href = '/pro/proCredencials';
                    } else {
                        $('.SubmitRow .Delete').toggle();
                        alert('failed: ' + result.message);
                    }
                })
                .fail(function (xhr, textStatus, errorThrown) {
                    spinner20.remove();
                    $('.SubmitRow .Delete').toggle();
                    alert('failed: ' + xhr.responseText);
                })
        }

        function IsOwner() {
            var currentUserGuid = '@ContextHelper.GetUser().Guid.ToString()';
            var retval = model.Owners.includes(currentUserGuid);
            return retval;
        }


    </script>
End Section

@Section Styles
    <style>
        /*
        .MainContent {
            max-width: 400px;
            margin:0 auto;
        }
            */

        .SkuContainer {
            display: flex;
            flex-direction: row;
            justify-content: space-between;
            align-items: flex-start;
        }

        .Properties {
            display: grid;
            grid-template-columns: auto 16px 1fr;
            grid-gap: 10px;
        }

        .EditButton {
            width: 16px;
            height: 16px;
            background-image: url('/Media/Img/Ico/edit.png');
            cursor: pointer;
        }

        .EditButton_Disabled {
            width: 16px;
            height: 16px;
            background-image: url('/Media/Img/Ico/edit_Disabled.png');
            cursor: pointer;
        }

        .EditButton:Hover {
            background-image: url('/Media/Img/Ico/edit_Active.png');
        }

        .EditControl {
            display: none;
        }

        .SubmitRow {
            grid-column: 1 / 4; /* span from grid column line 1 to 3 (i.e., span 2 columns) */
            column-gap: 20px;
        }

        .ImgContainer img {
            width:100%;
            height:auto;
        }
    </style>
End Section