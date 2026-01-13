@ModelType DTOTpvRedsysPasarela
@Code
    
End Code

<!DOCTYPE html>

<html lang="@Choose(ContextHelper.lang().Id, "es", "ca", "en")">
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

        <input type="hidden" name="Ds_MerchantParameters" value="@Model.Ds_MerchantParameters" />
        <input type="hidden" name="Ds_Signature" value="@Model.Ds_Signature" />
        <input type="hidden" name="Ds_SignatureVersion" value="@Model.Ds_SignatureVersion" />
    </form>

</body>
</html>

