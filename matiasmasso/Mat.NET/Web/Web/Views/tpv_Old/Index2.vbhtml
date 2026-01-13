@ModelType MaxiSrvr.TpvPayment

@Code
    ViewData("Title") = "Tpv"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oWebSession As MVC.WebSession = Session("WebSession")
End Code



<style>
    .pagewrapper {
        min-width: 320px;
        max-width: 510px;
        margin: 0 auto 20px;
        text-align: left;
        min-height: 400px;
    }

    h2 {
        margin-top:0;
        padding: 0 0 20px 0;
        font-size: 1.1em;
        color: darkgray;
    }

    .Row {
        margin-bottom: 15px;
    }

    .Label {
        vertical-align: top;
        display: inline-block;
        width: 150px;
    }


    .Control {
        margin-left: 20px;
        display: inline-block;
        width: 300px;
    }


    .Right {
        text-align: right;
    }

    .Center {
        text-align: center;
    }

    .FullWidth {
        width:100%;
    }

    .Width150 {
        width:150px;
    }


    #submit {
        border: 1px solid red;
        margin-bottom: 20px;
    }

    input[type=text], input[type=number], input[type=email], input[type=password], select {
        padding: 5px;
        border: 2px solid #ccc;
        -webkit-border-radius: 5px;
        border-radius: 5px;
    }

        input[type=text]:focus {
            border-color: #333;
        }

        
    input[type=submit], input[type=button] {
        padding: 5px 15px;
        background: #ccc;
        border: 1px solid #ccc;
        cursor: pointer;
        -webkit-border-radius: 5px;
        border-radius: 5px;
    }

    input[type=submit]:hover, input[type=button]:hover {
        background: #ddd;
        border: 1px solid black;
    }

</style>



<div class="pagewrapper">
    <h2>@Html.Raw(Model.Lang.Tradueix("Pagos mediante tarjeta de crédito", "Pagaments per tarja de crèdit", "Credit card payments"))</h2>

    <p>
        @Html.Raw(Model.Lang.Tradueix("al pulsar 'Siguiente' será reenviado a la página segura de nuestra entidad bancaria", _
                                       "al premer 'Següent' será reenviat a la página segura de la nostre entitat bancaria", _
                                       "on clicking 'Next' you'll be forwarded to a safe page on our bank website"))
    </p>

    <div class="Row">
        <div class="Label">
            @Model.Lang.Tradueix("concepto del pago", "concepte del pago", "payment concept")
        </div>
        <div class="Control">
            <input type="text" id="concepte" value='@model.hisconcepte' maxlength="50" @IIf(Model.Mode = MaxiSrvr.TpvPayment.Modes.Free, "", "disabled='disabled'") class="FullWidth" />
        </div>
    </div>

    <div class="Row">
        <div class="Label">
            @Model.Lang.Tradueix("importe", "import", "amount")
        </div>
        <div class="Control Right">
            @If Model.Mode = MaxiSrvr.TpvPayment.Modes.Free Then
                @<input type="number" id="eur" value='' maxlength="50" class="Width150 Right" />
            Else
                @<input type="text" id="eur" value='@Model.FormatedEur' maxlength="50" disabled='disabled' class="Width150 Right" />
            End If
        </div>
    </div>

    <div class="Row">
        <div class="Label">

        </div>
        <div class="Control Right">
            <input id="submit" type="button" class="Width150 Center" value='@Model.Lang.Tradueix("Siguiente", "Següent", "Next")' />
        </div>
    </div>
</div>

<input type="hidden" id="ref" value="@Model.Ref" />
<input type="hidden" id="mode" value="@CInt(Model.Mode)" />

<form id="pasarela" action="@Model.Ds_URL_Tpv" method="post">

    <input type="hidden" name="Ds_Merchant_Amount" value="@Model.Ds_Merchant_Amount" />
    <input type="hidden" name="Ds_Merchant_Currency" value="@Model.Ds_Merchant_Currency" />
    <input type="hidden" name="Ds_Merchant_Order" value="@Model.Ds_Merchant_Order" />
    <input type="hidden" name="Ds_Merchant_ProductDescription" value="@Model.Ds_Merchant_ProductDescription" />
    <input type="hidden" name="Ds_Merchant_MerchantData" value="@Model.Ds_Merchant_MerchantData" />
    <input type="hidden" name="Ds_Merchant_MerchantCode" value="@Model.Ds_Merchant_MerchantCode" />
    <input type="hidden" name="Ds_Merchant_TransactionType" value="@Model.Ds_Merchant_TransactionType" />
    <input type="hidden" name="Ds_Merchant_MerchantURL" value="@Model.Ds_Merchant_MerchantURL" />
    <input type="hidden" name="Ds_Merchant_MerchantSignature" value="@Model.Ds_Merchant_MerchantSignature" />
    <input type="hidden" name="Ds_Merchant_Terminal" value="@Model.Ds_Merchant_Terminal" />
    <input type="hidden" name="Ds_Merchant_ConsumerLanguage" value="@Model.Ds_Merchant_ConsumerLanguage" />
    <input type="hidden" name="Ds_Merchant_UrlOK" value="@Model.Ds_Merchant_UrlOK" />
    <input type="hidden" name="Ds_Merchant_UrlKO" value="@Model.Ds_Merchant_UrlKO" />

</form>

@Section Scripts
<script>
    $(document).ready(function () {

        $(document).on('click', '#submit', function (event) {
            $('.loading').show();
            var data = {
                mode: $('#mode').val(),
                ref: $('#ref').val(),
                concepte: $('#concepte').val(),
                eur: $('#eur').val()
            };
            $.getJSON('/tpv/LoadPasarela', data, function (response) { submitPasarela(response) });
        });


        function submitPasarela(formdata) {
            $('input [name="Ds_Merchant_Amount"]').val(formdata.Ds_Merchant_Amount);
            $('input [name="Ds_Merchant_Currency"]').val(formdata.Ds_Merchant_Currency);
            $('input [name="Ds_Merchant_Order"]').val(formdata.Ds_Merchant_Order);
            $('input [name="Ds_Merchant_ProductDescription"]').val(formdata.Ds_Merchant_ProductDescription);
            $('input [name="Ds_Merchant_MerchantData"]').val(formdata.Ds_Merchant_MerchantData);
            $('input [name="Ds_Merchant_MerchantCode"]').val(formdata.Ds_Merchant_MerchantCode);
            $('input [name="Ds_Merchant_TransactionType"]').val(formdata.Ds_Merchant_TransactionType);
            $('input [name="Ds_Merchant_MerchantURL"]').val(formdata.Ds_Merchant_MerchantURL);
            $('input [name="Ds_Merchant_MerchantSignature"]').val(formdata.Ds_Merchant_MerchantSignature);
            $('input [name="Ds_Merchant_Terminal"]').val(formdata.Ds_Merchant_Terminal);
            $('input [name="Ds_Merchant_ConsumerLanguage"]').val(formdata.Ds_Merchant_ConsumerLanguage);
            $('input [name="Ds_Merchant_UrlOK"]').val(formdata.Ds_Merchant_UrlOK);
            $('input [name="Ds_Merchant_UrlKO"]').val(formdata.Ds_Merchant_UrlKO);

            $('pasarela').submit();
        }
    });

</script>


End Section

