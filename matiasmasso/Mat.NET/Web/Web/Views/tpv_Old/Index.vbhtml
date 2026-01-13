@ModelType Maxisrvr.TpvPayment

@Code
    ViewData("Title") = "Tpv"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
End Code


@Using (Html.BeginForm())

    @<div class="pagewrapper">

    <h1>@Html.Raw(Model.Lang.Tradueix("Pagos mediante tarjeta de crédito", "Pagaments per tarja de crèdit", "Credit card payments"))</h1>

    <p>
        @Html.Raw(Model.Lang.Tradueix("al pulsar 'Siguiente' será reenviado a la página segura de nuestra entidad bancaria", _
                                       "al premer 'Següent' será reenviat a la página segura de la nostre entitat bancaria", _
                                       "on clicking 'Next' you'll be forwarded to a safe page on our bank website"))
    </p>

    @Html.Partial("_ValidationSummary", ViewData.ModelState)

    <div class="row">
        <div class='label'>
            @Model.Lang.Tradueix("concepto del pago", "concepte del pago", "payment concept")
        </div>
        <div class="control  WidthMed">
            @Html.TextBoxFor(Function(Model) Model.HisConcepte, New With {.maxLength = "50"})
        </div>
    </div>

    <div class="row">
        <div class='label'>
            @Model.Lang.Tradueix("importe", "import", "amount")
        </div>

        <div class="control WidthMed">
            @If Model.Mode = MaxiSrvr.TpvPayment.Modes.Free Then
                @Html.TextBoxFor(Function(Model) Model.FormatedEur, New With {.style = "text-align:right;"})
            Else
                @Html.TextBoxFor(Function(Model) Model.FormatedEur, New With {.style = "text-align:right;", .disabled = "disabled"})
            End If
        </div>
    </div>

    <div id="submit" class="row">
        <div class='label'>
        </div>
        <div class="control WidthMed right">
            <input type="submit" value='@Model.Lang.Tradueix("Siguiente", "Següent", "Next")' />
        </div>
    </div>

    @Html.HiddenFor(Function(Model) Model.Ds_Merchant_MerchantData)
    @Html.HiddenFor(Function(Model) Model.Eur)
    @Html.HiddenFor(Function(Model) Model.Mode)


</div>

End Using

