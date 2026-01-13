@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)


    Dim oContacts As New List(Of DTOContact)
    Dim oUser = Mvc.ContextHelper.FindUserSync()
    For Each oCustomer As DTOCustomer In FEB2.User.CustomersRaonsSocialsSync(exs, oUser)
        oContacts.Add(oCustomer)
    Next

    Dim sPersonNom As String = ""
    Dim sPersonDni As String = ""
    Dim sIban1, sIban2, sIban3, sIban4, sIban5, sIban6 As String

    If oContacts.Count = 1 Then
        Dim oIbans = FEB2.Ibans.PendingUploadsSync(exs, oUser)
        If oIbans.Count > 0 Then
            Dim oIban As DTOIban = oIbans.Last
            sPersonNom = oIban.PersonNom
            sPersonDni = oIban.PersonDni
            Dim sIbans As List(Of String) = DTOIban.SegmentedDigits(oIban)
            If sIbans.Count > 0 Then sIban1 = sIbans(0)
            If sIbans.Count > 1 Then sIban2 = sIbans(1)
            If sIbans.Count > 2 Then sIban3 = sIbans(2)
            If sIbans.Count > 3 Then sIban4 = sIbans(3)
            If sIbans.Count > 4 Then sIban5 = sIbans(4)
            If sIbans.Count > 5 Then sIban6 = sIbans(5)
        End If
    End If
End Code


    <h1>
        @ViewBag.Title
    </h1>

    @Html.Partial("_Select_Contact", oContacts)

    <div id="Iban" @IIf(oContacts.Count > 1, "hidden='hidden'", "")>
        <p>
            @Mvc.ContextHelper.Tradueix("Por favor rellene los datos de la persona con poderes que va a firmar el mandato, los dígitos (IBAN) de su cuenta bancaria y pulse el botón 'verificar'",
                                                           "Si us plau entri les dades de la persona amb poders que firmará el document, els digits (IBAN) del seu compte bancari i premi el botó 'verificar'",
                                                           "Please enter the details of the person who signs, the IBAN digits of your bank account and press 'validate' button:")
        </p>
        <div class="Query">
            @Mvc.ContextHelper.Tradueix("Nombre de la persona que firma", "Nom de la persona que firma", "Signature name")
            <div c><input id="PersonNom" type="text" maxlength="50" value="@Html.Raw(sPersonNom)"/></div>
        </div>
        <div class="Query">
            @Mvc.ContextHelper.Tradueix("DNI", "DNI", "Id document number")
            <div><input id="PersonDni" type="text" maxlength="12"  value="@Html.Raw(sPersonDni)"/></div>
        </div>
        <div class="Query">
            @Mvc.ContextHelper.Tradueix("dígitos del IBAN", "digits del IBAN", "IBAN digits")
            <div id="IbanDigits">
                <input type="text" class="autotabbed" maxlength="4" value='@IIf(sIban1 > "", sIban1, "ES")' />
                <input type="text" class="autotabbed" maxlength="4" value="@sIban2" />
                <input type="text" class="autotabbed" maxlength="4" value="@sIban3" />
                <input type="text" class="autotabbed" maxlength="4" value="@sIban4" />
                <input type="text" class="autotabbed" maxlength="4" value="@sIban5" />
                <input type="text" class="autotabbed" maxlength="4" value="@sIban6" />
            </div>
        </div>

        <div class="Query">
            <input type="button" value='@Mvc.ContextHelper.Tradueix("verificar", "verificar", "validate")' />
        </div>
    </div>

    <input type = "hidden" id="titular" value='@IIf(oContacts.Count = 1, oContacts(0).Guid.ToString, "")' />

    <div id = "IbanValidated" >
    </div>
                

    <!-- ---------------------------Begin WarningMessage ---------------------------------------------------- -->
    <div id = "WarningMessage" hidden="hidden">
        <img src="~/Media/Img/Ico/warn.gif" />
        <span></span>
    </div>
    <!-- ---------------------------End WarningMessage ---------------------------------------------------- -->


    <!-- ---------------------------Begin Download ---------------------------------------------------- -->
    <div id = "Download" hidden="hidden">

        <div>
            <a href = "#" >
                @Mvc.ContextHelper.Tradueix("Descargar mandato para su firma",
                                     "Descarregar mandato per la seva signatura",
                                     "Download mandate to be signed")
            </a>
        </div>
    </div>
    <!-- ---------------------------End Download ---------------------------------------------------- -->

    <!-- ---------------------------Begin Upload ---------------------------------------------------- -->
    <div id = "Upload" hidden="hidden">
        <p>
            @Mvc.ContextHelper.Tradueix("Gracias por su descarga.",
                                  "Gracies per la seva descarrega.",
                                  "Thanks for downloading.")
            <br/>
            <ul>
                <li>Si tiene certificado para firma digital:
                <ul><li>firme el documento electrónicamente</li></ul>
                </li>
                <li>
                    Si no tiene certificado:
                    <ul><li>fírmelo a mano</li>
                        <li>escanéelo</li>
                    </ul>
                </li>
            </ul>

            @Mvc.ContextHelper.Tradueix("Cuando esté listo puede subirlo mediante el siguiente enlace:  ", _
                                  "", _
                                  "")
        </p>
        <p>
            <a href = "#" >
                @Mvc.ContextHelper.Tradueix("Subir mandato firmado",
                                  "Pujar mandat signat",
                                  "Upload signed mandate")
            </a>
            <input id = "FileBox" type="file" />
        </p>

    </div>
    <!-- ---------------------------End Upload ---------------------------------------------------- -->

    <!-- ---------------------------Behin Thanks ---------------------------------------------------- -->
    <div id = "Thanks" hidden="hidden">
        @Mvc.ContextHelper.Tradueix("Gracias por subir el documento.", _
                                  "Gracies per pujar el document", _
                                  "Thanks for loading the document")
        <br/>
        @Mvc.ContextHelper.Tradueix("El mandato ha quedado a la espera de su validación.", _
                                  "El mandat ha quedat a la espera de la seva validació", _
                                  "The mandate is now waiting for validation")


    </div>
    <!-- ---------------------------End Thanks ---------------------------------------------------- -->




@Section Scripts
    <script src="~/Media/js/jquery.autotab.min.js"></script>
<script>
        $(document).ready(function () {
            $('.autotabbed').autotab();

            $('.SelectContact .Contact a').click(function () {
                var guid = $(this).data('guid');
                var html = $(this).html();
                $('.SelectContact').hide();
                $('.SelectedContact div').html(html);
                $('.SelectedContact').data('guid', guid);
                $('.SelectedContact').show();
                $(document).trigger('onContactSelected', guid)
            });

        });

        $(document).on('onContactSelected', function (e, argument) {
            $('#titular').val(argument);
            $('#Iban').show();
        });

        $(document).on('click', '#Iban input[type=button]', function (e, argument) {
            $('.loading').show();
            var titular = $('#titular').val();

            var digits1 = $('#IbanDigits input:nth-child(1)').val();
            var digits2 = $('#IbanDigits input:nth-child(2)').val();
            var digits3 = $('#IbanDigits input:nth-child(3)').val();
            var digits4 = $('#IbanDigits input:nth-child(4)').val();
            var digits5 = $('#IbanDigits input:nth-child(5)').val();
            var digits6 = $('#IbanDigits input:nth-child(6)').val();
            var digits = digits1.concat(digits2, digits3, digits4, digits5, digits6)

            var personNom = $('#PersonNom').val();
            var personDni = $('#PersonDni').val();
            var url = '@Url.Action("Save", "Iban")';
            $.getJSON(url,
                { digits: digits, titular: titular, personNom: personNom, personDni: personDni },
                function (result) {
                    $('.loading').hide();
                    if (result.status == 0) {
                        $('#SysError').show();
                    }
                    else if (result.status == 1) {
                        $('#Iban').hide();
                        $('#IbanValidated').show();
                        $('#IbanValidated').html(result.iban);
                        $('#Upload a').data('guid', result.guid);
                        $('#Download a').attr('href', result.url);
                        $('#Download').show();
                    }
                    else if (result.status == 2) {
                        $('#WarningMessage span').html(result.message);
                        $('#WarningMessage').show();
                    }
                });
        });

        $('#Download a').click(function () {
            $('#Download').hide();
            $('#Upload').show();
        });

        $('#Upload a').click(function () {
            event.preventDefault();
            $('#FileBox').trigger('click');
        });

        $("#FileBox").change(function () {
            upload();
        });


        function upload() {
            $('.loading').show();
            var url = '@Url.Action("Upload", "Iban")';
            var formdata = new FormData();
            formdata.append('guid', $('#Upload a').data('guid'));
            for (i = 0; i < document.getElementById('FileBox').files.length; i++) {
                formdata.append(document.getElementById('FileBox').files[i].name, document.getElementById('FileBox').files[i]);
            }

            var xhr = new XMLHttpRequest();
            xhr.open('POST', url);
            xhr.send(formdata);
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    $('.loading').hide();
                    var Response = $.parseJSON(xhr.response);
                    if (Response.result == 1) {
                        $('.loading').hide();
                        $('#Upload').hide();
                        $('#Thanks').show();
                    }
                    else
                        if (Response.Status == 'UNKNOWN')
                            alert('Formato de imagen desconocido. Por favor suba el mandato en formato PDF, JPEG, TIFF, GIF o PNG');
                        else if (response.status == 'DUPLICATED')
                            alert('Ya ha subido este mandato anteriormente.');
                        else if (response.status == 'NOFILES')
                            alert('No se ha subido ningun fichero.');
                        else
                            alert('Se ha producido un error de sistema. Por favor vuélvelo a probar más tarde');
                }
            }
        }


</script>

End Section

@Section Styles
    <style>
        .ContentColumn {
            max-width: 400px;
            margin: 0 auto;
        }

        .PageTitle {
            font-size: 1.2em;
            font-weight: 600;
            margin-bottom: 20px;
        }

        #PersonDni {
            width: 240px;
        }
        #PersonNom {
            width: 240px;
        }

        .Query {
            margin: 20px 0 0 0px;
        }

        #IbanDigits input {
            display:inline-block;
            width:45px;
            margin:2px;
        }

        #IbanValidated {
            margin: 20px 0 0 0px;
        }

        #WarningMessage span {
            vertical-align: top;
            color:red;
        }

        #WarningMessage img {
            vertical-align: top;
        }

        #Download div {
            margin: 20px 0 0 20px;
            text-align: left;
        }

        #FileBox {
            opacity: 0.0;
        }

        #Thanks {
            padding-top: 1em;
        }
    </style>
End Section


