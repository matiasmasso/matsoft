@ModelType DTORecallCli
@Code
    Layout = "~/Views/shared/_Layout.vbhtml"
    
    Dim sTitle As String = Mvc.ContextHelper.Tradueix("Retirada de producto", "Retirada de Producte", "Product Recall")
    ViewBag.Title = "M+O | " & sTitle
    Dim exs As New List(Of Exception)
    Dim sToday As String = Format(Today, "yyyy-MM-dd")
End Code


<div class="pagewrapper">
    <div class="PageTitle">@sTitle</div>

    <div>
        <div class="label">Persona de contacto</div>
        <div class="inputbox"><input type="text" id="ContactNom" value="@Model.UsrLog.UsrCreated.NickName" maxlength="50"/></div>

        <div class="label">Teléfono de contacto</div>
        <div class="inputbox"><input type="text" id="ContactTel" value="@FEB2.Contact.TelSync(exs, Model.Customer)" maxlength="50" /></div>

        <div class="label">Email de contacto</div>
        <div class="inputbox"><input type="text" id="ContactEmail" value="@Model.ContactEmail" maxlength="200"/></div>

        <div class="label">Cliente</div>
        <div class="inputbox"><input type="text" data-customer="@Model.Customer.Guid.ToString" id="Customer" value="@Model.Customer.Nom" /></div>

        <div class="label">Dirección de recogida</div>
        <div class="inputbox"><input type="text" id="Address" value="@Model.Customer.Address.Text" maxlength="200" /></div>

        <div class="label">Código postal</div>
        <div class="inputbox"><input type="text"  id="Zip" value="@Model.Customer.Address.Zip.ZipCod"  maxlength="10" /></div>

        <div class="label">Población</div>
        <div class="inputbox"><input type="text" id="Location"  value="@Model.Customer.Address.Zip.Location.Nom" maxlength="50"  /></div>

        <div class="label">Pais</div>
        <div class="inputbox">
            <select id="Pais">
                <option value="">(seleccionar pais)</option>
                <option value="AE3E6755-8FB7-40A5-A8B3-490ED2C44061" @IIf(Model.Country.ISO = "AD", "selected='selected'", "")>Andorra</option>
                <option value="AEEA6300-DE1D-4983-9AA2-61B433EE4635" @IIf(Model.Country.ISO = "ES", "selected='selected'", "")>España</option>
                <option value="631B1258-9761-4254-8ED9-25B9E42FD6D1" @IIf(Model.Country.ISO = "PT", "selected='selected'", "")>Portugal</option>
            </select>
        </div>

        <div id="products">
            <div class="epgRow">
                <div class="epgColor label">Color</div>
                <div class="epgSerial label">Num.serie</div>
            </div>

            <div class="productRow">
                <select>
                    <option value="">(escoge uno)</option>
                    <option value="E7F1C22C-7FE0-4795-900D-E90FC3010953">Cosmos Black</option>
                    <option value="7D4365FF-86C6-4360-9F67-1BFBD6A1B96C">Red Flame</option>
                    <option value="70A12901-7AB6-46F0-BE14-EB566A45E958">Moonlight Blue</option>
                    <option value="4292E69C-7678-4ADA-87AD-8D6CA0C640CC">Storm Gray</option>
                    <option value="364B9856-57A6-47AF-9FE1-AF630718C953">Black Marble</option>
                    <option value="E9B829FA-B720-4D95-A80B-258CF4CA618F">Wine Rose</option>
                </select>
                <input type="text" class="serialNumber" placeholder="M101A..." pattern="^M101A[0-9]{14}$" />
                <button class="buttonAdd">Añadir</button>
            </div>
            </div>
        </div>

    <div class="submit">
        <button id="submit">Enviar</button>
    </div>

    <div class="Thanks" hidden="hidden">
        Gracias por su registro.<br/>
        Acabamos de enviarle las etiquetas a su correo electrónico para identificar los bultos.
    </div>
</div>

@Section Scripts
<script>
    $(document).on('click', '.buttonAdd', function (e) {
        if (checkLastValue() == true) {
            var $lastRow = $("div.productRow:last");
            var serial = $lastRow.children('.serialNumber').val();
            var sku = $lastRow.children('select').val();
            var idx = 0;

            idx += 1;
            var $nextRow = $lastRow.clone();
                $nextRow.children('.serialNumber').val('');
                $nextRow.children('.serialNumber').data('row', idx);
                $nextRow.children('select').data('row', idx);
                $nextRow.insertAfter($lastRow);
                $lastRow.children('.buttonAdd').hide();
        }
    });

    $(document).on('click', '#submit', function (e) {
        var $lastRow = $("div.productRow:last");
        var serial = $lastRow.children('.serialNumber').val();
        var sku = $lastRow.children('select').val();

        if ($('#products > .productRow').length == 1 && sku == '' && serial == '') {
            alert('no ha seleccionado ningun producto');
        } else {
            if (sku > '' && serial == '') {
                alert('por favor indique el numero de serie');
            } else {
                if (sku == '' && serial > '') {
                    alert('por favor seleccione el color del producto');
                } else {
                    update();
                }
            }
        }
    });

    function checkLastValue() {
        var $lastRow = $("div.productRow:last");
        var serial = $lastRow.children('.serialNumber').val();
        var sku = $lastRow.children('select').val();

        var retval = false;
        if (sku == '') {
            alert('por favor seleccione el color del producto');
        } else {
            if (serial == '') {
                alert('por favor rellene la casilla de Número de Serie');
            } else {
                if (serial.match("^M101A[0-9]{14}$")) {
                    retval = true;
                } else {
                    alert('Numero de serie incorrecto\ndebe empezar por M101A seguido de 14 digitos')
                }
            }
        }
        return (retval);
    }

    function update() {
        $('.loading').show();

        var formdata = new FormData();
        formdata.append('ContactNom', $('#ContactNom').val());
        formdata.append('ContactTel', $('#ContactTel').val());
        formdata.append('ContactEmail', $('#ContactEmail').val());
        formdata.append('Customer', $('#Customer').data("customer"));
        formdata.append('Address', $('#Address').val());
        formdata.append('Zip', $('#Zip').val());
        formdata.append('Location', $('#Location').val());
        formdata.append('Pais', $('#Pais').val());

        var products = '';
        $('#products > .productRow').each(function ($pr) {
            var serial = $(this).children(":first").val();
            var sku = $(this).children(":nth-child(2)").val();
            if (sku > '') {
                if (products > '') products += ';';
                products += serial + ',' + sku;
            }
        });
        formdata.append('products', products);

        var xhr = new XMLHttpRequest();
        var url = '/recall/save';
        xhr.open('POST', url);
        xhr.send(formdata);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                $('.loading').hide();
                var response = $.parseJSON(xhr.response);
                if (response.result == 1)
                    $(document).trigger('afterUpdate', response);
                else
                    alert(response.message);
            }
        }
    }

    $(document).on('afterUpdate', function (e) {
        $('.submit').hide();
        $('.Thanks').show();
    });

</script>
End Section

@Section Styles
    <style>
        .pagewrapper {
            margin: auto;
            max-width:320px;
        }
        .label {
            font-size: small;
            color: gray;
        }
        .inputbox {
            margin-left: 20px;
            margin-bottom:5px;
        }
        .inputbox input, .inputbox select {
            width: 100%;
        }
        #products {
            margin-top: 10px;
        }
        .productRow {
            margin-bottom: 5px;
        }
        .productRow select, .productRow input, .productRow button, .epgRow div {
            display: inline-block;
        }
            .productRow select, .epgColor {
                width: 125px;
            }
        .serialNumber, .epgSerial {
            width:110px;
        }
        .buttonAdd {
            height:30px;
        }
        div.submit {
            margin-top: 20px;
            text-align: right;
            width: 100%
        }
        #submit {
            height: 30px;
            width: 170px;
        }
        .Thanks {
            color:blue;
        }
    </style>
End Section

