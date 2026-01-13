@ModelType DTOCustomerTarifa
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim oUser = Mvc.ContextHelper.GetUser()
    Dim sToday As String = Format(Today, "yyyy-MM-dd")
End Code


<h1 class="PageTitle">@ViewBag.Title</h1>

<div class="Spinner Loading"></div>

<div class="DataCollection">



    <section Class="CustomerProductSelection">


        <section Class="CustomerSelection" hidden="hidden">
            <div>
                <select id="Country" disabled='disabled'>
                    <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar país", "Sel·leccionar país", "Select country")</option>
                </select>
            </div>
            <div>
                <select id="Zona" disabled='disabled'>
                    <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar zona", "Sel·leccionar zona", "Select area")</option>
                </select>
            </div>
            <div>
                <select id="Location" disabled='disabled'>
                    <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar población", "Sel·leccionar població", "Select location")</option>
                </select>
            </div>
            <div>
                <select id="Customer" disabled='disabled'>
                    <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar cliente", "Sel·leccionar client", "Select customer")</option>
                </select>
            </div>
        </section>



        <section Class="CustomerSelected" hidden="hidden">
            <a href="#" target="_blank">

            </a>
        </section>


        <a href="#" Class="AdvancedOptions" hidden="hidden">@Mvc.ContextHelper.Tradueix("Opciones Avanzadas", "Opcions Avançades", "Advanced Options")</a>
        <table class="AdvancedOptions" hidden="hidden">

            <tr>
                <td>@Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</td>
                <td style="padding-left:5px;" align="right"><input type="text" id="Concept" value="por mediación de representante" /></td>
            </tr>
            <tr>
                <td>@Mvc.ContextHelper.Tradueix("Observaciones", "Observacions", "Comments")</td>
                <td style="padding-left:5px;"><input type="text" id="Obs" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="checkbox" id="TotJunt" />
                    @Mvc.ContextHelper.Tradueix("Servir todo junto", "Servir tot junt", "Ship all together")
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%">
                        <tr>
                            <td align="left" wrap="nowrap">
                                <input type="checkbox" id="CheckboxFchMin" />
                                @Mvc.ContextHelper.Tradueix("Fecha Servicio", "Data Servei", "Shipping Date")
                            </td>
                            <td align="right"><input type="date" id="FchMin" min="@sToday" hidden="hidden" value="@sToday" /></td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>

        <section class="ProductSelection" hidden="hidden">
            <div>
                <select id="Brand" disabled="disabled">
                    <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar marca comercial", "Sel·leccionar marca comercial", "Select brand")</option>
                </select>
            </div>
            <div>
                <select id="Category" disabled="disabled">
                    <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar categoría", "Sel·leccionar categoría", "Select category")</option>
                </select>
            </div>
            <div>
                <select id="Sku" disabled="disabled">
                    <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar producto", "Sel·leccionar producte", "Select product")</option>
                </select>
            </div>
        </section>
    </section>

    <section class="QtySelection">
        <div class="SkuThumbnail">
            <a href="#" target="_blank">
                <img />
            </a>
        </div>

        <div class="SkuDetails">
            <input type="number" id="Qty" data-moq="0" data-moqwarn='@Mvc.ContextHelper.Tradueix("La cantidad debe ser múltiplo de ", "La quantitat ha de ser multiplo de ", "Quantity for this product must match multiple of ")' value="" />
            <input type="button" id="ButtonAdd" value='@Mvc.ContextHelper.Tradueix("Añadir", "Afegir", "Add")' disabled="disabled" />
        </div>

    </section>

    <section class="Grid" hidden="hidden">
        <div class="RowHdr">
            <div class="CellTxt">@Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</div>
            <div class="CellNum">@Mvc.ContextHelper.Tradueix("Cant", "Quant", "Quant")</div>
            <div class="CellAmt">@Mvc.ContextHelper.Tradueix("Precio", "Preu", "Price", "Preço")</div>
            <div class="CellDto" hidden="hidden">@Mvc.ContextHelper.Tradueix("Dto", "Dte", "Dct")</div>
            <div class="CellAmt">@Mvc.ContextHelper.Tradueix("Importe", "Import", "Amount")</div>
            <div class="CellIco">&nbsp;</div>
        </div>
    </section>

    <section class="SubmitButtons" hidden="hidden">
        <span id="CheckBoxMail">
            <input type="checkbox" />
            @Mvc.ContextHelper.Tradueix("Enviarme email de confirmación", "Enviar-me email de confirmació", "Send me an email confirmation")
        </span>
        <input type="button" id="ButtonCancel" value='@Mvc.ContextHelper.Tradueix("Cancelar", "Cancel·lar", "Cancel")' />
        &nbsp;
        <input type="button" id="ButtonSubmit" value='@Mvc.ContextHelper.Tradueix("Enviar", "Enviar", "Submit", "Aceitar")' />
    </section>
</div>

<div class="Thanks" hidden="hidden">

</div>

<input type="hidden" id="Rep" value="@oUser.Guid.ToString" />
<input type="hidden" id="User" value="@Mvc.ContextHelper.GetUser().Guid.ToString" />
<input type="hidden" id="ApiAtlasUrl" value="@MmoUrl.apiUrl("repCustomers/atlas", oUser.Guid.ToString())" />
<input type="hidden" id="ApiCatalogUrl" value="@MmoUrl.apiUrl("customerBasket/catalog", oUser.Guid.ToString())" />
<input type="hidden" id="MmoSkuUrl" value='@DTO.MmoUrl.Factory(False, "product")' />
<input type="hidden" id="MmoSkuThumbnailUrl" value='@DTO.MmoUrl.Factory(False, "img", DTO.Defaults.ImgTypes.Art150)' />
<input type="hidden" id="MmoBasketUpdateUrl" value='@Url.Action("Update", "repBasket")' />
<input type="hidden" id="MmoBlankOrder" value='@Url.Action("Index", "repBasket")' />





@Section Styles
    <link href="~/Media/Css/Basket.css" rel="stylesheet" />
    <link href="~/Media/Css/RepBasket.css" rel="stylesheet" />
    <link href="~/Media/Css/tables.css" rel="stylesheet" />
End Section

@Section Scripts
    <script>
       var tarifa = @IIf(Model Is Nothing, "null", Html.Raw(System.Web.Helpers.Json.Encode(Model)));
    </script>
    <script src="~/Media/js/Basket.js"></script>
    <script src="~/Media/js/RepBasket.js"></script>

End Section