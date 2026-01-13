@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    ViewData("Title") = "MATIAS MASSO, S.A. - " & oWebSession.Tradueix("Registro de incidencia postventa", "Registre de incidencia postvenda", "Post sales incidence form")
    Dim exs As New List(Of Exception)
    Dim oUser As DTO.DTOUser = oWebsession.User
    Dim oActiveCustomers As List(Of DTO.DTOCustomer) = BLL.BLLUser.GetCustomers(oWebsession.User).FindAll(Function(x) x.Obsoleto = False)
End Code

<style>
    .pagewrapper {
        margin:auto;
        width:400px;
        text-align:center;
    }

    .Caption {
        font-weight:600;
        text-align:left;
        margin-bottom:20px;
    }

    .ProductBox {
        display:inline-block;
        width:155px;
        margin:20px 5px 10px 5px;
        text-align:center;
    }

    .ProductBox select {
        width:100%;
    }
    
    #Thanks {
        margin:20px 0 20px 0;
        color:navy;
        text-align:left;
    }

    .Outdated {
        color:red;
        font-weight:600;
        margin:20px auto;
    }
</style>

<div class="pagewrapper">


    <div class="Caption">Promoción Römer Dual-Fix en Mi Bebe y Yo</div>
    <div class="Outdated">El plazo para registrarse caducó el 15/02/2015</div>

    <!--
    <div id="Customer">
        @@Html.Partial("_UsuariCustomerSelection")
    </div>
        -->
    <div id="Details" @IIf(oActiveCustomers.Count = 1, "", "hidden = 'hidden'") data-customer='@IIf(oActiveCustomers.Count=1,oActiveCustomers(0).Guid.ToString,"")'>
        <div>
            Por favor seleccione los colores de las dos unidades de Römer Dual-Fix que forman el pedido de implantación:
        </div>

        <div class="ProductBox">
            <select id="Product1">
                <option value="7A55F9E6-5C1D-4D2E-A166-49BBCFC9896E" selected="selected">Black Thunder</option>
                <option value="AD44ABF8-9215-40F3-8C15-2BACB9602749">Chili Pepper</option>
                <option value="BDD000E9-6FB8-4719-A5D8-7D1773790F58">Stone Grey</option>
                <option value="89CA1822-7EBB-43AC-9753-981CF7377683">Dark Grape</option>
                <option value="D650EE27-BEAE-4C8C-A4EE-D92F54E0958A">Crown Blue</option>
                <option value="3A029A00-6C08-4B1E-B462-FE21E2D46782">Smart Zebra</option>
            </select>
            <div class="ImageBox">
                <img id="Img1" src="/img/5/7a55f9e6-5c1d-4d2e-a166-49bbcfc9896e" />
            </div>
        </div>
        <div class="ProductBox">
            <select id="Product2">
                <option value="7A55F9E6-5C1D-4D2E-A166-49BBCFC9896E">Black Thunder</option>
                <option value="AD44ABF8-9215-40F3-8C15-2BACB9602749" selected="selected">Chili Pepper</option>
                <option value="BDD000E9-6FB8-4719-A5D8-7D1773790F58">Stone Grey</option>
                <option value="89CA1822-7EBB-43AC-9753-981CF7377683">Dark Grape</option>
                <option value="D650EE27-BEAE-4C8C-A4EE-D92F54E0958A">Crown Blue</option>
                <option value="3A029A00-6C08-4B1E-B462-FE21E2D46782">Smart Zebra</option>
            </select>
            <div class="ImageBox">
                <img id="Img2" src="/img/5/ad44abf8-9215-40f3-8c15-2bacb9602749" />
            </div>
        </div>

            <div class="right">
                <input type="button" id="Submit" value="Aceptar" disabled="disabled" />
            </div>
        </div>

    <div id="Thanks" hidden="hidden">
        <p>Su pedido ha sido registrado correctamente con el número <span id="POnumber"></span>.</p>
        <p>Le hemos enviado un correo de confirmación a @oWebSession.Usuari.EmailAddress</p>
        <p>Gracias por su participación</p>
    </div>
</div>

@Section Scripts
    <script src="~/Media/js/UsuariCustomerSelection.js"></script>


<script>
    $(document).ready(function () {

        $(document).on('CustomerSelected', function (e, argument) {
            $('#Details').data("customer", argument.guid);

            if (argument.guid == '')
                $('#Details').hide();
            else {
                $('#Details').show();
            }
        });


        $('#Product1').change(function () {
            var guid = $(this).val();
            $('#Img1').attr('src', '/img/5/' + guid);
        });


        $('#Product2').change(function () {
            var guid = $(this).val();
            $('#Img2').attr('src', '/img/5/' + guid);
        });


        $('#Submit').click(function () {
            update();

        });


        function update() {
            $('.loading').show();

            var formdata = new FormData();
            formdata.append('customer', $('#Details').data("customer"));
            formdata.append('product1', $('#Product1').val());
            formdata.append('product2', $('#Product2').val());

            var xhr = new XMLHttpRequest();
            var url = '/quiz/PromoDualFix';
            xhr.open('POST', url);
            xhr.send(formdata);
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    $('.loading').hide();
                    var response = $.parseJSON(xhr.response);
                    if (response.result == 1) {
                        $('#POnumber').text(response.id);
                        $('#Submit').hide();
                        $('#Thanks').show();
                        $.getJSON('/Quiz/MailConfirmation', { template: response.template, param1: response.param1 });
                        }
                    else
                        alert(response.message);
                }
            }
        }

    });
</script>

End Section