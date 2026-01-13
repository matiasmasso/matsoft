@ModelType DTO.DTOUser
@Code
    ViewData("Title") = "QuizConsumerFairSignup"
    Layout = "~/Views/shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    ViewData("Title") = "MATIAS MASSO, S.A. - " & oWebSession.Tradueix("Participación en Feria de Consumo", "Participació en Fira de Consum", "Fair participation")
    Dim oActiveCustomers As List(Of DTO.DTOCustomer) = BLL.BLLQuizAdvansafix.Customers(Model)
    Dim SingleCustomerGuid As String = ""
    If oActiveCustomers.Count = 1 Then
        SingleCustomerGuid = oActiveCustomers(0).Guid.ToString
    End If

End Code




<div class="narrowForm">
    <h2>Advansafix II en stock</h2>

    @If oActiveCustomers.Count = 0 Then
        @<div class="Warn">
            <p>
                @Html.Raw("El usuario <b>" & Model.EmailAddress & "</b> no consta vinculado a ningun cliente con Advansafix II adquiridas antes del 2 de Noviembre de 2015.")
            </p><p>
                @Html.Raw("Si Vd. no es <b>" & Model.EmailAddress & "</b>, por favor cierre la sesión (arriba a la derecha 'perfil' -> 'desconectar'), seleccione 'acceder' e identifiquese con sus credenciales.")
            </p><p>
                @Html.Raw("Si Vd. es <b>" & Model.EmailAddress & "</b> y cree que esta información es incorrecta por favor contacte con nuestras oficinas.")
            </p>
        </div>

    Else
        @<div id="Customer">
            @Html.Partial("_UsuariCustomerSelection", oActiveCustomers)
        </div>
    End If

    <section id="Details" data-user="@Model.Guid.ToString" data-customer='@SingleCustomerGuid' @IIf(oActiveCustomers.Count > 1, "hidden='hidden'", "")>
        
    </section>

    <section id = "Thanks" hidden="hidden">
        <p>Acabamos de enviarle un correo de confirmación con los datos registrados.</p>
        <p>Puede Vd. modificarlos antes de la fecha límite del 10/11/2015 volviendo a entrar desde el mismo enlace.</p>
        <p><b>Gracias por su participación.</b></p>
    </section>

</div>

@Section Scripts
<script src="~/Media/js/UsuariCustomerSelection.js"></script>

    <script>

        $(document).on('change', 'select', function (event) {
            /* $('.editor-submit button').prop('disabled', false);*/
        });

        $(document).on('click', '.editor-submit button', function (event) {
            
                $('.loading').show();
                var url = '@Url.Action("Save")';
                var user = $('#Details').data("user");
                var customer = $('#Details').data("customer");

                var noSICT = 0;
                if ($('#NoSICT select').val())
                    noSICT = $('#NoSICT select').val();

                var SICT = 0;
                if ($('#SICT select').val())
                    SICT = $('#SICT select').val();

                $.post(url, { user: user, customer: customer, noSICT: noSICT, SICT: SICT }, function (response) {
                    $('.loading').hide();
                    $('#Details').hide();
                    $('#Thanks').show();
                    $.getJSON('@Url.Action("MailConfirmation")', { user: user, customer: customer });
                });
            
        });


        $(document).ready(function () {
            loadDetails();
            
            $(document).on('CustomerSelected', function (e, argument) {
                $('#Details').data("customer", argument.guid);

                if (argument.guid == '')
                    $('#Details').hide();
                else {
                    loadDetails();
                    $('#Details').show();
                }
            });


            function loadDetails() {
                var customer = $('#Details').data("customer");
                if (customer) {
                    var url = '@Url.Action("LoadCustomer")';
                    $('#Details').load(url, { guid: customer });
                }
            }

        });


    </script>
End Section

@Section Styles
<style>
        .narrowForm {
            width: 320px;
            margin: auto;
        }

        .editor-label {
            margin: 15px 7px 2px 4px;
            color:darkgray;
        }

        .editor-field {
            margin: 4px 7px 2px 30px;
            text-align: right;
        }

            .editor-field input, .editor-field textarea {
                width: 100%;
            }

        .editor-submit {
            text-align: right;
            margin: 20px 7px 2px 30px;
        }

        .FchConfirmed {
            margin-top:15px;
        }

        .Outdated {
            color:red;
        }
    </style>
End Section
