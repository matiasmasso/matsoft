@Code
    Layout = "~/Views/shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    ViewData("Title") = oWebSession.Tradueix("Compradores Invitados Feria Puericultura", "Compradors Convidats Fira Puericultura", "Signup for Trade Fair Invited Buyers")
    Dim exs As New List(Of Exception)
    Dim oUser As DTO.DTOUser = oWebsession.User
    Dim oContact As DTO.DTOContact = Nothing
    Dim oContacts As List(Of DTO.DTOCustomer) = BLL.BLLUser.CustomersRaonsSocials(oUser)
    If oContacts.Count > 0 Then
        oContact = oContacts.First
    End If

    BLL.BLLContact.Load(oContact)
    Dim oAddress As DTO.DTOAddress = oContact.Address
    Dim ShortDistance As Boolean = BLL.BLLZona.IsCloserThan200KmFromMadrid(oAddress)
    Dim outdated As Boolean = (Today >= New Date(2016, 6, 10))
End Code


<div class="pagewrapper">
    <div class="PageTitle">@oWebSession.Tradueix("Programa de Compradores Invitados Ifema", "Programa de Compradors Convidats Ifema", "Signup for Ifema Trade Fair Invited Buyers")</div>

    @If outdated Then
        @<div class="outdated">
            @(oWebsession.Tradueix("Este registro caducó el ", "Aquest registre va caducar el ", "This form is outdated since ") & "9/6/2016")
            <br/>
            @oWebSession.Tradueix("Rogamos disculpe las molestias", "Preguem disculpin les molesties", "Sorry for any unconveniences")
    </div>
    Else
        @<div>

            <p>
                @oWebSession.Tradueix("Por favor revise los siguientes datos y complete los que falten:", "Si us plau revisi les següents dades i ompli les que faltin", "Please check next data and fill up the empty boxes")
            </p>
            <div class="FormRow">
                <div class="FormLabel">
                    @oWebsession.Tradueix("Nombre", "Nom", "First name", "Nome")
                </div>
                <div class="FormControl">
                    <input type="Text" id="firstname" maxlength="50" />
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebsession.Tradueix("Apellidos", "Cognoms", "Last name", "Apelidos")
                </div>
                <div class="FormControl">
                    <input type="Text" id="lastname" maxlength="60" />
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebSession.Tradueix("Cargo", "Carrec", "Position")
                </div>
                <div class="FormControl">
                    <input type="Text" id="position" maxlength="50" />
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebSession.Tradueix("NIF de la empresa", "NIF de la empresa", "Company VAT Number")
                </div>
                <div class="FormControl">
                    @If oContact Is Nothing Then
                        @<input type="Text" id="nif" />
                    Else
                        @<input type="Text" id="nif" value="@oContact.Nif" maxlength="12" />
                    End If
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebSession.Tradueix("Nombre de la empresa", "Nom de la empresa", "Company name")
                </div>
                <div class="FormControl">
                    @If oContact Is Nothing Then
                        @<input type="Text" id="raosocial" />
                    Else
                        @<input type="Text" id="raosocial" value="@oContact.Nom" maxlength="60" />
                    End If
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebSession.Tradueix("Actividad", "Activitat", "Activity")
                </div>
                <div class="FormControl">
                    <select id="activitycode">
                        <option selected value="1">@oWebSession.Tradueix("Tienda física", "Botiga física", "Offline shop")</option>
                        <option value="2">@oWebSession.Tradueix("Tienda online", "Botiga online", "e-commerce")</option>
                        <option value="3">@oWebSession.Tradueix("Compradores de Franquicias o grandes almacenes", "Compradors de Franquicies o Grans Magatzems", "Purchase Managers from Joint Ventures or Depart.Stores")</option>
                        <option value="4">@oWebSession.Tradueix("Otros", "Altres", "Others")</option>
                    </select>
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebSession.Tradueix("Dirección", "Adreça", "Address")
                </div>
                <div class="FormControl">
                    @If oAddress Is Nothing Then
                        @<input type="Text" id="address" maxlength="60" />
                    Else
                        @<input type="Text" id="address" value="@oAddress.Text" maxlength="60" />
                    End If
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebSession.Tradueix("Código postal", "Codi postal", "zip code")
                </div>
                <div class="FormControl">
                    @If oAddress Is Nothing Then
                        @<input type="Text" id="zip" />
                    Else
                    If oAddress.Zip Is Nothing Then
                        @<input type="Text" id="zip" maxlength="5" />
                    Else
                        @<input type="Text" id="zip" value="@oAddress.zip.zipCod" maxlength="5" />
                    End If
                    End If
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebSession.Tradueix("Población", "Població", "Location")
                </div>
                <div class="FormControl">
                    @If oAddress Is Nothing Then
                        @<input type="Text" id="location" maxlength="60" />
                    Else
                    If oAddress.Zip Is Nothing Then
                        @<input type="Text" id="location" maxlength="60" />
                    Else
                    If oAddress.Zip.Location Is Nothing Then
                        @<input type="Text" id="location" maxlength="60" />
                    Else
                        @<input type="Text" id="location" value="@oAddress.zip.Location.Nom" maxlength="60" />
                    End If
                    End If
                    End If

                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebSession.Tradueix("Pais", "Pais", "Country")
                </div>
                <div class="FormControl">
                    <select id="country">
                        @For Each oCountry As DTO.DTOCountry In FEBL.Countries.AllSync(oWebsession.Lang, exs)
                            @<option @IIf(oCountry.ISO = "ES", "selected", "") value='@oCountry.Guid.ToString'>@BLL.BLLCountry.Nom(oCountry, oWebsession.Lang)</option>
                        Next
                    </select>
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebSession.Tradueix("Teléfono", "Telèfon", "Phone number")
                </div>
                <div class="FormControl">
                    <input type="Text" id="phone" maxlength="60" />
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    @oWebsession.Tradueix("Movil", "Mobil", "Cell phone")
                </div>
                <div class="FormControl">
                    <input type="Text" id="cellphone" maxlength="50" />
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    Fax()
                </div>
                <div class="FormControl">
                    <input type="Text" id="fax" maxlength="50" />
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    Email()
                </div>
                <div class="FormControl">
                    <input type="Text" id="email" value="@BLL.BLLSession.UserEmailAddress(oWebsession)" maxlength="100" />
                </div>
            </div>

            <div class="FormRow">
                <div class="FormLabel">
                    Página Web
                </div>
                <div class="FormControl">
                    <input type="Text" id="web" value="@oContact.Website" maxlength="100" />
                </div>
            </div>
            <br />
            <div class="FormRow">
                <div class="FormLabel">
                    @oWebSession.Tradueix("Residencia", "Residència", "Residence")
                </div>
                <div class="FormControl">
                    <label><input type="radio" name="distance" value="1" @IIf(ShortDistance, "checked='checked'", "") /> @oWebSession.Tradueix("Resido a menos de 200Km de Madrid", "Resideixo a menys de 200Km de Madrid", "I live within 200Km from Madrid")</label>
                    <br />
                    <label><input type="radio" name="distance" value="2" @IIf(ShortDistance, "", "checked='checked'") /> @oWebSession.Tradueix("Resido a más de 200Km de Madrid", "Resideixo a mes de 200Km de Madrid", "I live more than 200Km away from Madrid")</label>
                </div>
            </div>

            <div class="FormRow">
                <span id="errors">&nbsp;</span>
            </div>

            <div class="SubmitRow">
                <input type="Button" id="submit" value='@oWebSession.Tradueix("Aceptar", "Acceptar", "Submit")' />
            </div>

            <div id="thanks" hidden="hidden">
                @oWebSession.Tradueix("Gracias por registrarse. Le hemos enviado un correo de confirmación. La organización de la Feria se pondrá en contacto directamente con Vd.", _
                              "Gracies per registrar-se. Acabem de enviar-li un correu de confirmació. La organització de la Fira es posará en contacte directament amb vostés.", _
                              "Thanks for signing up. We have just emailed you a confirmation message. You'll be directly contacted by the Fair organization")
            </div>
        </div>
    End If
</div>

    @Section Scripts
        <script>

            $(document).on('click', '#submit', function () {
                var errors = validationerrors();
                if (errors.length == 0) {
                    $('#errors').html('');
                    update();
                }
                else
                    $('#errors').html(errors.join('<br/>'));
            });

            function update() {
                $('.loading').show();
                var data = getdata();

                var formdata = new FormData();
                formdata.append('firstname', data.firstname);
                formdata.append('lastname', data.lastname);
                formdata.append('position', data.position);
                formdata.append('nif', data.nif);
                formdata.append('raosocial', data.raosocial);
                formdata.append('activitycode', data.activitycode);
                formdata.append('address', data.address);
                formdata.append('zip', data.zip);
                formdata.append('location', data.location);
                formdata.append('country', data.country);
                formdata.append('phone', data.phone);
                formdata.append('cellphone', data.cellphone);
                formdata.append('fax', data.fax);
                formdata.append('email', data.email);
                formdata.append('web', data.web);
                formdata.append('distance', data.distance);

                var xhr = new XMLHttpRequest();
                var url = '/FairGuest/Update';
                xhr.open('POST', url);
                xhr.send(formdata);
                xhr.onreadystatechange = function () {
                    if (xhr.readyState == 4 && xhr.status == 200) {
                        $('.loading').hide();
                        var response = $.parseJSON(xhr.response);
                        if (response.result == 1) {
                            $('#submit').hide();
                            $('#thanks').show();
                            $.getJSON('/Quiz/MailConfirmation', { template: response.template, param1: response.param1 });
                        }
                        else
                            alert(response.message);
                    }
                }
            }

            function validationerrors() {
                var errors = [];
                var data = getdata();
                if (!data.firstname)
                    errors.push('El Nombre no puede quedar vacío');
                if (!data.lastname)
                    errors.push('La casilla de apellidos no puede quedar vacía');
                if (!data.position)
                    errors.push('Por favor indique su cargo en la empresa');
                if (!data.nif)
                    errors.push('Por favor rellene el NIF en la empresa');
                if (!data.raosocial)
                    errors.push('El nombre de la empresa no puede quedar vacío');
                if (data.activitycode == 0)
                    errors.push('Debe seleccionar alguna actividad');
                if (!data.zip)
                    errors.push('Por favor rellene el código postal de su población');
                if (!data.location)
                    errors.push('La población no puede quedar en blanco');
                if (!data.phone)
                    errors.push('Por favor indíquenos un teléfono de contacto');
                else if (data.phone.length < 9)
                    errors.push('telefono no válido')
                if (!data.cellphone)
                    errors.push('Por favor indíquenos un teléfono móvil de contacto');
                else if (data.phone.length < 9)
                    errors.push('número de móvil no válido')
                if (!data.email)
                    errors.push('Por favor indíquenos un email de contacto');
                return errors;
            }

            function getdata() {
                var retval = {
                    firstname: $("#firstname").val(),
                    lastname: $("#lastname").val(),
                    position: $("#position").val(),
                    nif: $("#nif").val(),
                    raosocial: $("#raosocial").val(),
                    activitycode: $("#activitycode").val(),
                    address: $("#address").val(),
                    zip: $("#zip").val(),
                    location: $("#location").val(),
                    country: $("#country").val(),
                    phone: $("#phone").val(),
                    cellphone: $("#cellphone").val(),
                    fax: $("#fax").val(),
                    email: $("#email").val(),
                    web: $("#web").val(),
                    distance: $("input[name=distance]:checked").val()
                }
                return retval;
            }


        </script>

    End Section

    @Section Styles
        <style>
            .pagewrapper {
                max-width: 530px;
                margin: auto;
            }

            .outdated {
                color:red;
                margin:20px 0
            }

            .FormLabel {
                display: inline-block;
                vertical-align: top;
                width: 200px;
            }

            .FormControl {
                display: inline-block;
                width: 300px;
            }

                .FormControl input[type=text] {
                    width: 100%;
                }

            select {
                width: 100%;
            }

            #zip, #phone, #cellphone, #fax {
                width: 100px;
            }

            #errors {
                color: red;
            }

            #thanks {
                margin-top: 15px;
                color: blue;
            }

            .SubmitRow {
                text-align: right;
            }
        </style>
    End Section
