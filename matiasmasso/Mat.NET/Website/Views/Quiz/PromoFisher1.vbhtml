@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim exs As New List(Of Exception)

    Dim oUser = ContextHelper.FindUserSync()
    Dim oCustomers = FEB.User.GetCustomersSync(oUser, exs)
End Code

<div class="pagewrapper">
    <div class="PageTitle">Quincena del 10% en Fisher-Price</div>
    <p>
        Del 8 al 22 de Mayo, cursando un pedido de los siguientes artículos que llegue a los 300,00 € (antes de IVA y después del descuento) obtendrá un 10% de descuento en todos ellos.
    </p>
    <p>
        Entrega inmediata, pedidos no fraccionables.<br/>
        Descuento no acumulable a otras promociones o descuentos.
    </p>

    <select class="customer" data-test='@ViewData("test")'>
        @For Each oCustomer As DTOCustomer In oCustomers
            @<option value="@oCustomer.Guid.ToString">@Left(oCustomer.FullNom, 60)</option>
        Next
    </select>

    <div class="rowSku">
        <div>unidades</div>
        <div>&nbsp;</div>
        <div>producto</div>
        <div>coste</div>
    </div>

    <div class="rowSku" data-sku="aed5b20c-8991-4678-a08e-3e6a726f0024" data-price="49.6">
        <div>
            <select>
                <option selected>0</option>
                <option>2</option>
                <option>4</option>
                <option>6</option>
                <option>8</option>
                <option>10</option>
                <option>12</option>
                <option>14</option>
                <option>16</option>
                <option>18</option>
                <option>20</option>
            </select>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/hamacas/cbf52_hamaca_crece_conmigo_monitos_divertidos" target="_blank">
                <img src="/img/5/aed5b20c-8991-4678-a08e-3e6a726f0024">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/hamacas/cbf52_hamaca_crece_conmigo_monitos_divertidos" target="_blank">
                CBF52 Hamaca Crece Conmigo Monitos Divertidos
            </a>
        </div>
        <div>
            49,60 €
        </div>
    </div>
    <div class="rowSku" data-sku="dcd6da44-0bfb-4e4a-ba3a-b4bd08c14016" data-price="49.6">
        <div>
            <select>
                <option selected>0</option>
                <option>2</option>
                <option>4</option>
                <option>6</option>
                <option>8</option>
                <option>10</option>
                <option>12</option>
                <option>14</option>
                <option>16</option>
                <option>18</option>
                <option>20</option>
            </select>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/hamacas/y8184_crece_rosa" target="_blank">
                <img src="/img/5/dcd6da44-0bfb-4e4a-ba3a-b4bd08c14016">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/hamacas/y8184_crece_rosa" target="_blank">
                Y8184 Hamaca Crece Conmigo Rosa
            </a>
        </div>
        <div>
            49,60 €
        </div>
    </div>
    <div class="rowSku" data-sku="e8921c6c-aa60-4823-b017-ea8eeae36cd7" data-price="36.4">
        <div>
            <select>
                <option selected>0</option>
                <option>2</option>
                <option>4</option>
                <option>6</option>
                <option>8</option>
                <option>10</option>
                <option>12</option>
                <option>14</option>
                <option>16</option>
                <option>18</option>
                <option>20</option>
            </select>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/ba%c3%b1o/p4325_orinal_aprendo" target="_blank">
                <img src="/img/5/e8921c6c-aa60-4823-b017-ea8eeae36cd7">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/ba%c3%b1o/p4325_orinal_aprendo" target="_blank">
                P4325 Orinal Aprendo y me Divierto
            </a>
        </div>
        <div>
            36,40 €
        </div>
    </div>
    <div class="rowSku" data-sku="5498876a-bc31-4c6b-8da2-db9de6e20d68" data-price="30.85">
        <div>
            <select>
                <option selected>0</option>
                <option>2</option>
                <option>4</option>
                <option>6</option>
                <option>8</option>
                <option>10</option>
                <option>12</option>
                <option>14</option>
                <option>16</option>
                <option>18</option>
                <option>20</option>
            </select>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/ba%c3%b1o/t6211_orinal_cu%c3%a1_cu%c3%a1_3_en_1" target="_blank">
                <img src="/img/5/5498876a-bc31-4c6b-8da2-db9de6e20d68">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/ba%c3%b1o/t6211_orinal_cu%c3%a1_cu%c3%a1_3_en_1" target="_blank">
                T6211 Orinal Cuá Cuá 3 en 1
            </a>
        </div>
        <div>
            30,85 €
        </div>
    </div>

<div id="valueinfo">
    pedido vacío
</div>  

    <div id="submit">
        <input type="button" value="enviar" disabled="disabled" />
    </div>
    <div id="thanks" hidden="hidden" style="color:blue;">
        Gracias por su pedido. Acabamos de enviarle un correo de confirmación.
    </div>

</div>

@Section Scripts
    <script>
    $(document).ready(function(){
        $(document).on('click', '#submit input[type="button"]', function (e) {
            var isTest = $('.customer').data('test');
            if (isTest == "1")
                alert('promoción reservada exclusivamente a distribuidores oficiales');
            else
                update();
        });

        $(document).on('change', 'div[data-sku] select', function (e) {
            var orderValue = valor();
            var netOrderValue = valor() * 0.9;
            if (orderValue == 0) {
                var txt = 'pedido vacío';
                $('#valueinfo').html(txt);
                $('#valueinfo').css('color', 'red');
            }
            else if (orderValue >= 366.66) {
                var txt = 'importe del pedido con descuento: ' + netOrderValue.toFixed(2).toLocaleString() + ' €';
                $('#valueinfo').html(txt);
                $('#valueinfo').css('color', 'blue');
                $('#submit input[type="button"]').prop("disabled", false);
            } else {
                var txt = 'le faltan ' + (366.66 - orderValue).toFixed(2).toLocaleString() + ' € para llegar al 10% de descuento';
                $('#valueinfo').html(txt);
                $('#valueinfo').css('color', 'red');
                $('#submit input[type="button"]').prop("disabled", true);
            }
        });


        function valor() {
            var retval = 0;
            $('div[data-sku]').each(function (index, item) {
                var qty = parseInt($(item).find('select').val());
                var price = parseFloat($(item).attr("data-price"));
                retval += (qty * price)
            })
            return retval;
        }

        function update() {
            $('.loading').show();

            var formdata = new FormData();
            formdata.append('customer', $('.customer').val());
            $('div[data-sku]').each(function (i) {
                formdata.append('product.' + i + '.guid', $(this).attr("data-sku"));
                formdata.append('product.' + i + '.qty', $(this).find("select").val());
            });

            var xhr = new XMLHttpRequest();
            var url = 'PromoFisher1';
            xhr.open('POST', url);
            xhr.send(formdata);
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    $('.loading').hide();
                    var response = $.parseJSON(xhr.response);
                    if (response.result == 1) {
                        $('#POnumber').text(response.id);
                        $('#submit').hide();
                        $('#thanks').show();
                        $('div[data-sku] select').prop('disabled', 'disabled');
                        $.getJSON('MailConfirmation', { template: response.template, param1: response.param1 });
                    }
                    else
                        alert(response.message);
                }
            }
        }
    });
    </script>
End Section
@Section Styles
    <style>
        select.customer {
            margin-bottom: 10px;
        }

        .rowSku div {
            display: inline-block;
        }

            .rowSku div:nth-child(1) {
                width: 70px;
                vertical-align: middle;
            }

            .rowSku div select {
                width: 100%;
            }

            .rowSku div:nth-child(2) {
                width: 30px;
                vertical-align: middle;
            }

                .rowSku div:nth-child(2) a img {
                    width: 100%;
                }

            .rowSku div:nth-child(3) {
                width: 300px;
                vertical-align: middle;
            }

            .rowSku div:nth-child(4) {
                width: 70px;
                vertical-align: middle;
                text-align: right;
            }

        #submit {
            text-align: right;
        }

        #thanks, #valueinfo {
            padding: 10px 0;
        }
    </style>
End Section