@ModelType DTOPurchaseOrder
@Code
    ViewBag.Title = "Pedido de Cliente"
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
End Code

<div class="pagewrapper">
    <section class="PageTitle ToolStrip">
        @If Model.DocFile IsNot Nothing Then
            @<a href='@FEB.PurchaseOrder.PdfUrl(Model)' title='@(ContextHelper.Tradueix("Descargar fichero Pdf", "Descarregar fitxer Pdf", "Download Pdf file")) '>
                <img class="Download" src="~/Media/Img/48x48/Pdf48.png" />
            </a>
        End If
        <a href='@FEB.PurchaseOrder.ExcelUrl(Model)' title='@(ContextHelper.Tradueix("Descargar fichero Excel", "Descarregar fitxer Excel", "Download Excel file")) '>
            <img src="~/Media/Img/48x48/Excel48.png" />
        </a>

        @If Model.Cod = DTOPurchaseOrder.Codis.proveidor Then
            @ContextHelper.Lang().Tradueix("Pedido a Proveedor", "Comanda a Proveidor", "Purchase Order to supplier")
        ElseIf Model.Cod = DTOPurchaseOrder.Codis.client Then
            @ContextHelper.Lang().Tradueix("Pedido de Cliente", "Comanda de Client", "Customer Purchase Order")
        End If
    </section>


    <div>
        <a href="@FEB.Contact.Url(Model.Contact)">
            @Model.Contact.FullNom
        </a>
    </div>

    <div>
        @Model.caption(ContextHelper.Lang())
    </div>

    @If Model.Cod = DTOPurchaseOrder.Codis.client Then
        @<div>
            @ContextHelper.Tradueix("Confirmación de pedido", "Confirmació de comanda", "Purchase Order Confirmation") @Model.Num
        </div>
    End If

    @If Model.FchDeliveryMin <> Nothing Then
        @<div>
            @ContextHelper.Tradueix("Fecha de servicio:", "Data de servei:", "Delivery date:") @Model.FchDeliveryMin.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
        </div>
    End If

    <div Class="Grid">
        <div Class="RowHdr">
            <div Class="CellNum">
                @ContextHelper.Tradueix("Pedido", "Demanat", "Ordered")
            </div>
            <div class="CellTxt">
                @ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
            </div>
            <div class="CellAmt">
                @ContextHelper.Tradueix("Precio", "Preu", "Price", "Preço")
            </div>
            @If DTOPurchaseOrder.discountsExist(Model) Then
                @<div Class="CellDto">
                    @ContextHelper.Tradueix("Descto", "Descte", "Discount")
                </div>
            End If
            <div Class="CellAmt">
                @ContextHelper.Tradueix("Importe", "Import", "Amount")
            </div>
            <div class="CellNum Servido">
                @ContextHelper.Tradueix("Servido", "Servit", "Shipped")
            </div>
            <div class="CellNum Pendiente">
                @ContextHelper.Tradueix("Pendiente", "Pendent", "Pending")
            </div>
        </div>
        @For Each item As DTOPurchaseOrderItem In Model.Items
            @<div class="Row">
                <div class="CellNum">@item.Qty</div>
                <div class="CellTxt">
                    <a href="@item.Sku.GetUrl(ContextHelper.Lang)" target="_blank">
                        @item.Sku.RefYNomLlarg.Tradueix(ContextHelper.Lang)
                    </a>
                </div>

                <div class="CellAmt Precio">@DTOAmt.CurFormatted(item.Price)</div>

                @If DTOPurchaseOrder.discountsExist(Model) Then
                    @<div Class="CellDto">
                        @if item.Dto = 0 Then
                            @<span>&nbsp;</span>
                        Else
                            @<span>@Format(item.Dto, "0\%")</span>
                        End If
                    </div>
                End If

                <div class="CellAmt ">@DTOAmt.CurFormatted(item.Amount)</div>

                <div class="CellNum Servido">
                    <a href="@FEB.PurchaseOrderItem.Url(item)" target="_blank">
                        @If item.Qty <> item.Pending Then
                            @Html.Raw(item.Qty - item.Pending)
                        End If
                    </a>
                </div>

                <div class="CellNum Pendiente">@IIf(item.Pending = 0, Html.Raw("&nbsp;"), item.Pending)</div>
            </div>
        Next

        <div class="Row">
            <div class="CellNum">&nbsp;</div>
            <div class="CellTxt">
                @ContextHelper.lang().Tradueix("Suma de importes", "Suma de imports", "Sum")
            </div>

            <div class="CellAmt Precio">&nbsp;</div>

            @If DTOPurchaseOrder.DiscountsExist(Model) Then
                @<div Class="CellDto">&nbsp;</div>
            End If

            <div class="CellAmt">@DTOAmt.CurFormatted(Model.SumaDeImportes())</div>

            <div class="CellNum Servido">&nbsp;</div>
            <div class="CellNum Pendiente">&nbsp;</div>
        </div>

        @If DTOPurchaseOrder.DevengaIva(Model) Then
            @<div class="Row">
                <div class="CellNum">&nbsp;</div>
                <div class="CellTxt">
                    @ContextHelper.lang().Tradueix("IVA", "IVA", "VAT")
                    @Format(DTOPurchaseOrder.IvaPct(Model), "0\%")
                </div>

                <div class="CellAmt Precio">&nbsp;</div>

                @If DTOPurchaseOrder.DiscountsExist(Model) Then
                    @<div Class="CellDto">&nbsp;</div>
                End If

                <div class="CellAmt">@DTOAmt.CurFormatted(DTOPurchaseOrder.IvaAmt(Model))</div>

                <div class="CellNum Servido">&nbsp;</div>
                <div class="CellNum Pendiente">&nbsp;</div>
            </div>


            @If DTOPurchaseOrder.DevengaReq(Model) Then
                @<div class="Row">
                    <div class="CellNum">&nbsp;</div>
                    <div class="CellTxt">
                        @ContextHelper.lang().Tradueix("Recargo de equivalencia", "Recarrec d'equivalencia", "VAT")
                        @Format(DTOPurchaseOrder.ReqPct(Model), "0\%")
                    </div>

                    <div class="CellAmt Precio">&nbsp;</div>

                    @If DTOPurchaseOrder.DiscountsExist(Model) Then
                        @<div Class="CellDto">&nbsp;</div>
                    End If

                    <div class="CellAmt">@DTOAmt.CurFormatted(DTOPurchaseOrder.ReqAmt(Model))</div>

                    <div class="CellNum Servido">&nbsp;</div>
                    <div class="CellNum Pendiente">&nbsp;</div>
                </div>

            End If


            @<div class="Row">
                <div class="CellNum">&nbsp;</div>
                <div class="CellTxt">
                    @ContextHelper.lang().Tradueix("Total IVA incluido", "Total IVA inclos", "VAT Included")
                </div>

                <div class="CellAmt Precio">&nbsp;</div>

                @If DTOPurchaseOrder.DiscountsExist(Model) Then
                    @<div Class="CellDto">&nbsp;</div>
                End If

                <div class="CellAmt">@DTOAmt.CurFormatted(DTOPurchaseOrder.TotalIvaInclos(Model))</div>

                <div class="CellNum Servido">&nbsp;</div>
                <div class="CellNum Pendiente">&nbsp;</div>
            </div>



        End If




    </div>

    <div>
        <a href="@FEB.PurchaseOrders.Url(Model.Contact)">
            @ContextHelper.Tradueix("Ver el historial de pedidos de", "Veure l'historial de comandes de", "Browse order history from")
            @Model.Contact.FullNom
        </a>
    </div>
</div>



@Section Styles
    <link href="~/Media/Css/Tables.css" rel="stylesheet" />
    <link href="~/Media/Css/SinglePurchaseOrder.css" rel="stylesheet" />
End Section

