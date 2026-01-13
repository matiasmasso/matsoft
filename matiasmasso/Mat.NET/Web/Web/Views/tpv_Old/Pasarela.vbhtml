@ModelType MaxiSrvr.TpvPayment

@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim s As String = Model.Ds_Merchant_Amount
    
End Code

<!DOCTYPE html>

<html lang="@Choose(oWebSession.Lang.Id, "es", "ca", "en")">
<head>
    <title>pasarela tpv</title>

    <script type="text/javascript">
        function submitOnLoad() {
            var frm = document.getElementById("creditcardform");
            frm.submit();
        }
    </script>

</head>
<body onload="submitOnLoad();">

    <form id="creditcardform" action="@Model.Ds_URL_Tpv" method="post">

        conectando con la entidad bancaria....

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

</body>
</html>

