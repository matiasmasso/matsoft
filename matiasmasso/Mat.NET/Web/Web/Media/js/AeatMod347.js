$(document).ready(function () {

    $(document).on("#ContactNom").change(function () {
            $("#ContactNif").val($("#ContactNom").val());
            $('.loading').show();
            $("#Mod347Details").load("/AeatMod347/FromCustomer", { guid: $("#ContactNom").val() }, function () { $('.loading').hide(); });
    });


})