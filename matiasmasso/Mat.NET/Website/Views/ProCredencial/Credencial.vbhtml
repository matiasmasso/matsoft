@ModelType DTOCredencial.ViewModel
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang As DTOLang = ContextHelper.Lang
End Code

        <div class="Properties">
            <div>
                @lang.Tradueix("Referencia", "Referència", "Reference")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.referencia</span>
                <input type="text" data-prop="Referencia" class="EditControl" value="@Model.referencia" />
            </div>

            <div>
                @lang.Tradueix("Url")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.url</span>
                <input type="text" data-prop="Url" class="EditControl" value="@Model.url" />
            </div>

            <div>
                @lang.Tradueix("Usuario", "Usuari", "User name")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.usuari</span>
                <input type="text" data-prop="Usuari" class="EditControl" value="@Model.usuari" />
            </div>

            <div>
                @lang.Tradueix("Contraseña", "Clau de pas", "Password")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@(If(String.IsNullOrEmpty(Model.password), "", New String("*", Model.password.Length)))</span>
                <input type="password" data-prop="Password" class="EditControl" value="@Model.password" />
            </div>

            <div>
                @lang.Tradueix("Observaciones", "Observacions", "Comments")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.obs</span>
                <textarea data-prop="Obs" class="EditControl">@Model.obs</textarea>
            </div>


            <div class="SubmitRow">
                <div>
                    <input type="button" class="Cancel" value='@lang.Tradueix("Cancelar", "Cancel·lar", "Cancel")' />
                    <input type="button" class="Delete" value='@lang.Tradueix("Eliminar", "Eliminar", "Delete")' disabled />
                    <input type="button" class="Submit" value='@lang.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' disabled />
                </div>
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
            if (IsOwner()) {
                $('.SubmitRow .Delete').attr("disabled", false);
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
            location.href = '/pro/proCredencials';
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


            var url = "/pro/proCredencial/update";
            $.post(url, model, function (result) {
            })
                .done(function (result) {
                    spinner20.remove();
                    if (result.success) {
                        location.href = '/pro/proCredencials';
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

        .MainContent {
            max-width: 400px;
            margin:0 auto;
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

        textarea, input[type=text] {
            box-sizing: border-box;
            max-width: 400px;
            width:100%;
        }

        .SubmitRow {
            grid-column: 1 / 4; /* span from grid column line 1 to 3 (i.e., span 2 columns) */
            column-gap: 20px;
        }
    </style>
End Section