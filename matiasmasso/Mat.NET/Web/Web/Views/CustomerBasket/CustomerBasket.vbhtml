@ModelType List(Of DTOCustomer)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)

    Dim sToday As String = Format(Today, "yyyy-MM-dd")
    Dim sMaxFch As String = Format(Today.AddDays(60), "yyyy-MM-dd")
End Code


<h1>@ViewBag.Title</h1>

<div class="Spinner Loading" hidden="hidden"></div>

<div class="DataCollection">

    <section class="CustomerProductSelection">

        <section id="CustomerSelection">
            @Html.Partial("_UsuariCustomerSelection", Model)
        </section>

        <a href="#" class="AdvancedOptions">@Mvc.ContextHelper.Tradueix("Opciones Avanzadas", "Opcions Avançades", "Advanced Options")</a>
        <table class="AdvancedOptions" hidden="hidden">

            <tr>
                <td>@Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</td>
                <td align="right"><input type="text" id="Concept" value="@Mvc.ContextHelper.Tradueix("via web", "via web", "via website")" /></td>
            </tr>
            <tr>
                <td>@Mvc.ContextHelper.Tradueix("Observaciones", "Observacions", "Comments")</td>
                <td><input type="text" id="Obs" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="checkbox" id="StockOnly" checked="checked" />
                    @Mvc.ContextHelper.Tradueix("Solo producto en stock", "Nomes producte en stock", "Just product in stock")
                </td>
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
                                @Mvc.ContextHelper.Tradueix("Servicio", "Servei", "Shipping")
                            </td>
                            <td align="right"><input type="date" id="FchMin" min="@sToday" max=@sMaxFch hidden="hidden" value="@sToday" data-fchminwarn='@Mvc.ContextHelper.Tradueix("La fecha de servicio debe estar dentro de los próximos 60 días", "La data de servei ha de estar dins els propers 60 dies", "Shipping must take place within next 50 days")' /></td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="UploadLabel">
                        <a href="#" class="UploadStickers">
                            @Mvc.ContextHelper.Tradueix("clic aquí para subir etiquetas personalizadas para el transportista", "clic aquí per pujar etiquetes personalitzades per el transportista", "click here to upload forwarder stickers")
                        </a>
                    </div>

                    <input id="fileBoxStickers" type="file" />
                    <div id="stickers" class="control">

                    </div>
                </td>
            </tr>
        </table>



        <section class="ProductSelection" hidden="hidden">
            <div>
                <select id="Brand">
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

    <section class="Grid">
        <div class="RowHdr">
            <div class="CellTxt">@Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</div>
            <div class="CellNum">@Mvc.ContextHelper.Tradueix("Cant", "Quant", "Quant")</div>
            <div class="CellAmt">@Mvc.ContextHelper.Tradueix("Precio", "Preu", "Price", "Preço")</div>
            <div class="CellDto" hidden="hidden">@Mvc.ContextHelper.Tradueix("Dto", "Dte", "Dct")</div>
            <div class="CellAmt">@Mvc.ContextHelper.Tradueix("Importe", "Import", "Amount")</div>
            <div class="CellIco">&nbsp;</div>
        </div>
        <div class="Row" id="BasketTotal">
            <div class="CellTxt">total</div>
            <div class="CellNum">&nbsp;</div>
            <div class="CellAmt">&nbsp;</div>
            <div class="CellDto" hidden="hidden">&nbsp;</div>
            <div class="CellAmt">&nbsp;</div>
            <div class="CellIco">&nbsp;</div>
        </div>
    </section>

    <section class="SubmitButtons" hidden="hidden">
        <div class="Spinner Updating" hidden="hidden"></div>
        <span id="CheckBoxConditions">
            <input type="checkbox" />
            @Mvc.ContextHelper.Tradueix("He leido y acepto las", "He llegit i accepto les", "I have read and accept the")
            <a href="/condiciones">@Mvc.ContextHelper.Tradueix("condiciones de venta", "condicions de venda", "sales conditions")</a>
        </span>

        <input type="button" id="ButtonSubmit" value='@Mvc.ContextHelper.Tradueix("Enviar", "Enviar", "Submit")' />
    </section>

</div>

<div class="Thanks"></div>

<input type="hidden" id="User" value="@Mvc.ContextHelper.GetUser().Guid.ToString" />
<input type="hidden" id="ApiCatalogUrl" value="@MmoUrl.apiUrl("customerBasket/catalog", Mvc.ContextHelper.GetUser().Guid.ToString())" />
<input type="hidden" id="MmoSkuUrl" value='@MmoUrl.Factory(False, "product")' />
<input type="hidden" id="MmoSkuThumbnailUrl" value='@MmoUrl.Factory(False, "img", DTO.Defaults.ImgTypes.art150)' />
<input type="hidden" id="MmoBasketUpdateUrl" value='@Url.Action("Update", "customerBasket")' />
<input type="hidden" id="MmoBlankOrder" value='@Url.Action("Index", "customerBasket")' />

@Section Styles
    <link href="~/Media/Css/Basket.css?id=1" rel="stylesheet" />
    <link href="~/Media/Css/CustomerBasket.css?id=1" rel="stylesheet" />
    <link href="~/Media/Css/tables.css" rel="stylesheet" />
    <style scoped>
    </style>
End Section

@Section Scripts
    <script src="~/Media/js/Basket.js"></script>
    <script src="~/Media/js/CustomerBasket.js"></script>
    <script src="~/Media/js/UsuariCustomerSelection.js"></script>
End Section