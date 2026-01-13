@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim exs As New List(Of Exception)

    Dim oUser = ContextHelper.FindUserSync()
    Dim oCustomers = FEB.User.GetCustomersSync(oUser, exs)
End Code

<div class="pagewrapper">
    <div class="PageTitle">Fisher-Price devuelve el 50% del importe este verano</div>
    <p>
        ¡Aproveche esta oportunidad para animar las ventas!</p>
    <p>
        Si desea participar en esta promoción, puede registrarse enviando el pedido que figura a continuación para disponer de producto.
    </p>

    <select class="customer" data-test='@ViewData("test")'>
        @For Each oCustomer As DTOCustomer In oCustomers
            @<option value="@oCustomer.Guid.ToString">@Left(oCustomer.FullNom, 60)</option>
        Next
    </select>

    <div class="skuRowHeader">
        <div>unidades</div>
        <div>&nbsp;</div>
        <div>producto</div>
        <div>PVP</div>
    </div>

    <div data-sku="f88ef169-9b2e-4f3d-8416-98f3be281ca6">
        <div>
            <select>
                <option>0</option>
                <option selected>2</option>
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
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_0_meses/cdn41_m%c3%b3vil_ositos" target="_blank">
                <img src="/img/5/f88ef169-9b2e-4f3d-8416-98f3be281ca6">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_0_meses/cdn41_m%c3%b3vil_ositos" target="_blank">
                CDN41 móvil ositos
            </a>
        </div>
        <div>
            50,00 €
        </div>
    </div>

    <div data-sku="da91eff7-9a55-48d0-a280-b072a49bde84">
        <div>
            <select>
                <option>0</option>
                <option selected>3</option>
                <option>6</option>
                <option>9</option>
                <option>12</option>
                <option>15</option>
                <option>18</option>
                <option>21</option>
                <option>24</option>
                <option>27</option>
                <option>30</option>
            </select>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_0_meses/bmh49_gimnasio-piano_pataditas" target="_blank">
                <img src="/img/5/da91eff7-9a55-48d0-a280-b072a49bde84">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_0_meses/bmh49_gimnasio-piano_pataditas" target="_blank">
                BMH49 Gimnasio-Piano Pataditas
            </a>
        </div>
        <div>
            78,00 €
        </div>
    </div>
    <div data-sku="f73129ea-58c8-4f97-bf6a-4fdf9febdd96">
        <div>
            <select>
                <option>0</option>
                <option selected>4</option>
                <option>8</option>
                <option>12</option>
                <option>16</option>
                <option>20</option>
                <option>24</option>
                <option>28</option>
                <option>32</option>
                <option>36</option>
                <option>40</option>
            </select>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_0_meses/chp85_gimnasio_musical_animalitos_de_la_selva" target="_blank">
                <img src="/img/5/f73129ea-58c8-4f97-bf6a-4fdf9febdd96">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_0_meses/chp85_gimnasio_musical_animalitos_de_la_selva" target="_blank">
                CHP85 Gimnasio Musical Animalitos de la Selva
            </a>
        </div>
        <div>
            51,00 €
        </div>
    </div>

    <div data-sku="5d1d41b5-62da-4de9-92e3-3158ecac83f1">
        <div>
            <select>
                <option>0</option>
                <option selected>4</option>
                <option>8</option>
                <option>12</option>
                <option>16</option>
                <option>20</option>
                <option>24</option>
                <option>28</option>
                <option>32</option>
                <option>36</option>
                <option>40</option>
            </select>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_6_meses/71050_piramide" target="_blank">
                <img src="/img/5/5d1d41b5-62da-4de9-92e3-3158ecac83f1">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_6_meses/71050_piramide" target="_blank">
71050:      Pirámide balanceante
            </a>
        </div>
        <div>
            10,00 €
        </div>
    </div>

    <div data-sku="da6467e3-a161-41b2-8213-71b95a59c5d6">
        <div>
            <select>
                <option>0</option>
                <option selected>4</option>
                <option>8</option>
                <option>12</option>
                <option>16</option>
                <option>20</option>
                <option>24</option>
                <option>28</option>
                <option>32</option>
                <option>36</option>
                <option>40</option>
            </select>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_6_meses/k7167_bloques_inf" target="_blank">
                <img src="/img/5/da6467e3-a161-41b2-8213-71b95a59c5d6">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_6_meses/k7167_bloques_inf" target="_blank">
                K7167 Bloques Infantiles
            </a>
        </div>
        <div>
            12,00 €
        </div>
    </div>

    <div data-sku="a4961112-c990-49c7-a6ff-403a1ecc39a3">
        <div>
            <select>
                <option>0</option>
                <option selected>2</option>
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
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_6_meses/y9854_le%c3%b3n_andador_musical" target="_blank">
                <img src="/img/5/a4961112-c990-49c7-a6ff-403a1ecc39a3">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_6_meses/y9854_le%c3%b3n_andador_musical" target="_blank">
                Y9854 León Andador Musical
            </a>
        </div>
        <div>
            50,00 €
        </div>
    </div>

    <div data-sku="b8f557d3-ee71-469b-ad04-17d0d4d2345b">
        <div>
            <select>
                <option>0</option>
                <option selected>2</option>
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
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_6_meses/cdl25_perrito__primeros_descubrim." target="_blank">
                <img src="/img/5/b8f557d3-ee71-469b-ad04-17d0d4d2345b">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/mas_de_6_meses/cdl25_perrito__primeros_descubrim." target="_blank">
                CDL25 Perrito Primeros Descubrimientos
            </a>
        </div>
        <div>
            33,50 €
        </div>
    </div>
    <div data-sku="fca915e1-31c0-49ed-a314-8958e14655ee">
        <div>
            <select>
                <option>0</option>
                <option selected>4</option>
                <option>8</option>
                <option>12</option>
                <option>16</option>
                <option>20</option>
                <option>24</option>
                <option>28</option>
                <option>32</option>
                <option>36</option>
                <option>40</option>
            </select>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/de_6_a_36_meses/h8173_libro_interac_aprendizaje" target="_blank">
                <img src="/img/5/fca915e1-31c0-49ed-a314-8958e14655ee">
            </a>
        </div>
        <div>
            <a href="https://www.matiasmasso.es/fisher-price/de_6_a_36_meses/h8173_libro_interac_aprendizaje" target="_blank">
                H8173 Libro interactivo Aprendizaje
            </a>
        </div>
        <div>
            20,50 €
        </div>
    </div>

    <div id="submit">
        <input type="button" value="enviar" />
    </div>
    <div id="thanks" hidden="hidden" style="color:blue;">
        Gracias por su pedido. Acabamos de enviarle un correo de confirmación.
    </div>

<div>
    <p>Para recibir la devolución del importe, el consumidor deberá:</p>
    <ol>
        <li>Rellenar un cupón (que podrá descargar de nuestra página web)</li>
        <li>Adjuntar fotocopia de un único ticket de compra con uno o varios de los productos relacionados por un importe superior a 25,00 €</li>
        <li>Adjuntar el código de barras recortado de los productos mencionados</li>
        <li>Enviarlo a Mattel (Ref. programa Pequeños Descubrimientos)-Aptdo de Correos 2589, 08080 Barcelona</li>
    </ol>
    Promoción válida para compras realizadas entre el 1 de Junio y el 2 de Agosto de 2015 en territorio Español. La fecha límite para recibir la documentación completa es el 8 de Agosto de 2015. Los abonos del 50% (hasta un máximo de 40,00 €) se realizarán por transferencia directamente al consumidor antes del 30 de Septiembre de 2015.
</div>
    </div>

@Section Scripts
    <script>
    $(document).ready(function(){
        $(document).on('click', '#submit input[type="button"]', function (e) {
            var isTest = $('.customer').data('test');
            if (isTest == "1")
                alert('reservado exclusivamente a distribuidores oficiales');
            else
                update();
        });

        function update() {
            $('.loading').show();

            var formdata = new FormData();
            formdata.append('customer', $('.customer').val());
            $('div[data-sku]').each(function (i) {
                formdata.append('product.' + i + '.guid', $(this).attr("data-sku"));
                formdata.append('product.' + i + '.qty', $(this).find("select").val());
            });

            var xhr = new XMLHttpRequest();
            var url = 'PromoFisher';
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
            margin-bottom:10px;
        }
        div[data-sku] div, .skuRowHeader div {
            display:inline-block;
        }
        div[data-sku] div:nth-child(1), .skuRowHeader div:nth-child(1) {
            width:70px;
            vertical-align:middle;
        }
        div[data-sku] div select {
            width:100%;
        }
        div[data-sku] div:nth-child(2), .skuRowHeader div:nth-child(2) {
            width:30px;
            vertical-align:middle;
        }
        div[data-sku] div:nth-child(2) a img{
            width:100%;
        }
        div[data-sku] div:nth-child(3), .skuRowHeader div:nth-child(3) {
            width:300px;
            vertical-align:middle;
        }
        div[data-sku] div:nth-child(4), .skuRowHeader  div:nth-child(4){
            width:70px;
            vertical-align:middle;
            text-align:right;
        }
        #submit{
            text-align:right;
        }
        #thanks{
            padding:10px 0;
        }
</style>
End Section