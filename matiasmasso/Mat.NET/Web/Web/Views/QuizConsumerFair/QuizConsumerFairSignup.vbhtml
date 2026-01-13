@Code
    ViewData("Title") = "QuizConsumerFairSignup"
    Layout = "~/Views/shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    ViewData("Title") = "MATIAS MASSO, S.A. - " & oWebSession.Tradueix("Participación en Feria de Consumo", "Participació en Fira de Consum", "Fair participation")
    Dim oUser As DTO.DTOUser = oWebsession.User
    Dim oActiveCustomers As List(Of DTO.DTOCustomer) = BLL.BLLUser.GetCustomers(oWebsession.User).FindAll(Function(x) x.Obsoleto = False)
    Dim SingleCustomerGuid As String = ""
    If oActiveCustomers.Count = 1 Then
        SingleCustomerGuid = oActiveCustomers(0).Guid.ToString
    End If

End Code

<style>
    .pagewrapper {
        margin:auto;
        width:550px;
    }

    div[data-brand] div {
        display:inline-block;
        width:100px;
        text-align:center;
    }

    div[data-brand] div:nth-child(1) {
        text-align:left;
    }

    .Tpv {
        margin:20px 0 20px 0;
        text-align:center;
    }

    .Submit {
        margin-top:20px;
        text-align:right;
    }
    .Warn {
        color:red;
        margin-bottom:20px;
    }

</style>


<div class="pagewrapper">
    <h2>Participación en Bebes y Mamas Abril 2015</h2>

    @If oActiveCustomers.Count = 0 Then
        @<div class="Warn">
            <p>
                @Html.Raw(oWebSession.Tradueix("El usuario <b>" & oWebSession.Usuari.EmailAddress & "</b> no consta vinculado a ninguno de nuestros clientes, por lo que no es posible registrar su participación por este medio.", _
                                  "L'usuari <b>" & oWebSession.Usuari.EmailAddress & "</b> no consta vinculat a cap client, per el que no ens es possible registrar la seva participació per aquest mitjá.", _
                                  "User <b>" & oWebSession.Usuari.EmailAddress & "</b> is not linked to any of our customers, hence we cannot register you as a candidate."))
            </p><p>
                @Html.Raw(oWebSession.Tradueix("Si Vd. no es <b>" & oWebSession.Usuari.EmailAddress & "</b>, por favor cierre la sesión (arriba a la derecha 'perfil' -> 'desconectar'), seleccione 'acceder' e identifiquese con sus credenciales.", _
                                  "Si vosté no es <b>" & oWebSession.Usuari.EmailAddress & "</b>, si us plau tanqui la sessió (a dalt a ma dreta, 'perfil' -> 'desconnectar'), cliqui 'accedir' e identifiqui's amb les seves credencials.", _
                                  "If you are not <b>" & oWebSession.Usuari.EmailAddress & "</b>, please close your session (top right menu 'profile' -> 'close') and click on 'login' to identify yourself."))
            </p><p>
                @Html.Raw(oWebSession.Tradueix("Si Vd. es <b>" & oWebSession.Usuari.EmailAddress & "</b> y cree que deberíamos haberle identificado como cliente por favor contacte con nuestras oficinas.", _
                                  "Si vosté es <b>" & oWebSession.Usuari.EmailAddress & "</b> y creu que hariem de haver-lo identificat com a client si us plau contacti amb les nostres oficines.", _
                                  "If you are <b>" & oWebSession.Usuari.EmailAddress & "</b> and you feel we should have identified you as an active customer please contact our offices."))
            </p>
        </div>
        
    Else
        @<div id="Customer">
            @Html.Partial("_UsuariCustomerSelection")
        </div>
    End If


    <section id="Details" data-customer='@SingleCustomerGuid' @IIf(oActiveCustomers.Count > 1, "hidden='hidden'", "")>

        <p>Por favor confirme si puede aportar un mínimo de 2 personas y un terminal Tpv por stand y franja horaria seleccionados. No es imprescindible pero sí recomendable y se valorará en caso de franjas horarias con mucha demanda:</p>

        <div class="Tpv">
            <input type="checkbox" value="1" />Dispongo de 2 personas y TPV por stand y franja horaria
        </div>

        <p>Por favor seleccione aquellas marcas y franjas horarias en las que esté interesado en participar:</p>

        <div data-brand="">
            <div>&nbsp;</div>
            <div>Sábado 11<br />mañana<br />(10:00-15:00)</div>
            <div>Sábado 11<br />tarde<br />(15:00-20:00)</div>
            <div>Domingo 12<br />mañana<br />(10:00-15:00)</div>
            <div>Domingo 12<br />tarde<br />(15:00-20:00)</div>
        </div>

        <div data-brand="d4c2bc59-046d-42d3-86e3-bdca91fb473f">
            <div>Britax Römer</div>
            <div data-franja="0">
                <input type="checkbox" />
            </div>
            <div data-franja="1">
                <input type="checkbox" />
            </div>
            <div data-franja="2">
                <input type="checkbox" />
            </div>
            <div data-franja="3">
                <input type="checkbox" />
            </div>
        </div>

        <div data-brand="b1a0fb03-0c18-4607-9091-df5a6a635bb0">
            <div>Inglesina</div>
            <div data-franja="0">
                <input type="checkbox" />
            </div>
            <div data-franja="1">
                <input type="checkbox" />
            </div>
            <div data-franja="2">
                <input type="checkbox" />
            </div>
            <div data-franja="3">
                <input type="checkbox" />
            </div>
        </div>

        <div data-brand="67058f90-1fd6-4ae6-82ed-78447779b358">
            <div>4moms</div>
            <div data-franja="0">
                <input type="checkbox" />
            </div>
            <div data-franja="1">
                <input type="checkbox" />
            </div>
            <div data-franja="2">
                <input type="checkbox" />
            </div>
            <div data-franja="3">
                <input type="checkbox" />
            </div>
        </div>

        <div data-brand="63f67fdb-812f-49f9-b06c-023ee8a984ec">
            <div>Bob</div>
            <div data-franja="0">
                <input type="checkbox" />
            </div>
            <div data-franja="1">
                <input type="checkbox" />
            </div>
            <div data-franja="2">
                <input type="checkbox" />
            </div>
            <div data-franja="3">
                <input type="checkbox" />
            </div>
        </div>

        <div data-brand="">
            <div></div>
            <div>
            </div>
            <div>
            </div>
            <div>
            </div>
            <div class="Submit">
                <input type="button" value="enviar" @iif(oActiveCustomers.Count=0,"disabled ='disabled'","") />
            </div>
        </div>
    </section>

    <section id="Thanks" hidden="hidden">
        <p>Acabamos de enviarle un correo de confirmación con los datos registrados.</p>
        <p>Puede Vd. modificarlos antes de la fecha límite volviendo a entrar desde el mismo enlace.</p>
        <p>Esperamos confirmar su participación a partir del 15 de marzo según disponibilidad.</p>
        <p><b>Gracias por su participación.</b></p>
    </section>

</div>

@Section Scripts
<script src="~/Media/js/UsuariCustomerSelection.js"></script>

    <script>
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
                    $.getJSON('Load', { customer: customer }, function (response) { loadGrid(response) });
                }
            }

            function loadGrid(json) {
                $("div[data-brand] div[data-franja] input").prop('checked', false);
                json.forEach(function (obj) {
                    $(".Tpv input").prop('checked', obj.tpv2pax);
                    $("div[data-brand=" + obj.brand + "] div[data-franja=" + obj.franja + "] input").prop('checked', true);
                });
            };

            $(".Submit input").click(function () {
                var json = [];
                $("div[data-franja] input:checked").each(function (index) {
                    json.push({
                        "contact": $('#Details').data("customer"),
                        "brand": $(this).closest("div[data-brand]").data("brand"),
                        "tpv2pax": $('.Tpv input').is(':checked'),
                        "franja": $(this).closest("div[data-franja]").data("franja")
                    });
                });

                if (json.length === 0)
                    alert('¡no ha seleccionado ninguna franja horaria!');
                else {
                    $('.loading').show();
                    $.post('Save', { data: JSON.stringify(json) }, function (response) { afterUpdate(response) });
                }
            });

            function afterUpdate(response) {
                $('.loading').hide();
                $('#Details').hide();
                $('#Thanks').show();
                $.getJSON('/Quiz/MailConfirmation', { template: response.template, param1: response.param1 });
            };
        });
    </script>
End Section