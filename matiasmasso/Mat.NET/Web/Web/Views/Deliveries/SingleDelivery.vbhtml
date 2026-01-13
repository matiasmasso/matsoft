@ModelType DTODelivery
@Code
    'Layout = "~/Views/shared/_Layout.vbhtml"
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    ViewBag.Title = Mvc.ContextHelper.Tradueix("Albarán de entrega", "Albará d'entrega", "Delivery note")
    Dim DiscountExists As Boolean
    If Model.Items IsNot Nothing Then
        DiscountExists = DTODelivery.DiscountExists(Model)
    End If

    Dim oOrder As New DTOPurchaseOrder
End Code

<div class="pagewrapper">

    <section class="PageTitle">
        <a href='@FEB2.Delivery.PdfUrl(Model)' title='@(Mvc.ContextHelper.Tradueix("Descargar fichero Pdf", "Descarregar fitxer Pdf", "Download Pdf file")) '>
            <img class="Download" src="~/Media/Img/48x48/Pdf48.png" />
        </a>

        @DTODelivery.Caption(Model, Mvc.ContextHelper.lang())
    </section>

    <a href="@FEB2.Contact.Url(Model.Customer)">@Model.Nom</a>
    <br />
    @Model.Address.Text
    <br />
    @Model.Address.Zip.Location.FullNom(Model.Contact.Lang)
    <br />

    @If Model.Invoice Is Nothing Then
        If Model.Facturable Then
            Html.Raw(Mvc.ContextHelper.lang().Tradueix("pendiente de facturar", "pendent de facturar", "invoice pending"))
        Else
            Html.Raw(Mvc.ContextHelper.lang().Tradueix("no facturable", "no facturable", "not invoiceable"))
        End If
    Else
        @<a href="@FEB2.Invoice.Url(Model.Invoice)">
            @(Mvc.ContextHelper.lang().Tradueix("factura ", "factura ", "invoice ") & Model.Invoice.Num & Mvc.ContextHelper.lang().Tradueix(" del ", " del ", " from ") & String.Format(Model.Invoice.Fch, "dd/MM/yy"))
        </a>
    End If



    <div class="Grid">
        <div class="RowHdr">
            <div class="CellTxt">
                @Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
            </div>
            <div class="CellNum">
                @Mvc.ContextHelper.Tradueix("Cantidad", "Quantitat", "Quantity")
            </div>
            <div class="CellAmt">
                @Mvc.ContextHelper.Tradueix("Precio", "Preu", "Price", "Preço")
            </div>

            @If DiscountExists Then
                @<div class="CellDto">
                    @Mvc.ContextHelper.Tradueix("Dto.", "Dte.", "Disc.")
                </div>
            End If

            <div class="CellAmt">
                @Mvc.ContextHelper.Tradueix("Importe", "Import", "Amount")
            </div>
        </div>

        @If Model.Items IsNot Nothing Then
            For Each item As DTODeliveryItem In Model.Items

                If Not item.PurchaseOrderItem.PurchaseOrder.Guid.Equals(oOrder.Guid) Then
                    oOrder = item.PurchaseOrderItem.PurchaseOrder
                    @<div class="Row Order">
                        <div class="CellEpigraf">
                            <a href="@FEB2.PurchaseOrder.Url(oOrder)">
                                @oOrder.Caption(Mvc.ContextHelper.lang())
                            </a>
                        </div>

                        <div class="CellNum">&nbsp;</div>
                        <div class="CellAmt">&nbsp;</div>
                        @If DiscountExists Then
                            @<div class="CellDto">&nbsp;</div>
                        End If
                        <div class="CellAmt"></div>


                    </div>
                End If

                @<div class="Row Item">
                    <div class="CellTxt">
                        <a href="@item.purchaseOrderItem.sku.GetUrl(Mvc.ContextHelper.Lang))" target="_blank">
                            @item.purchaseOrderItem.sku.NomLlarg.Tradueix(Mvc.ContextHelper.Lang)
                        </a>
                    </div>

                    <div class="CellNum">@item.Qty</div>
                    <div class="CellAmt">@DTOAmt.CurFormatted(item.Price)</div>

                    @If DiscountExists Then
                        @<div class="CellDto">
                            @If item.Dto <> 0 Then
                                @(item.Dto & "%")
                            End If
                        </div>
                    End If

                    <div class="CellAmt">@DTOAmt.CurFormatted(item.Import)</div>
                </div>

            Next

            @<div Class="Row Item">
                <div Class="CellEpigraf">
                    @Mvc.ContextHelper.Tradueix("Base imponible", "Base imponible", "Taxable amount")
                </div>
                <div Class="CellNum">&nbsp;</div>
                <div Class="CellAmt">&nbsp;</div>
                @If DiscountExists Then
                    @<div class="CellDto">&nbsp;</div>
                End If
                <div Class="CellAmt">
                    @DTOAmt.CurFormatted(DTODelivery.BaseImponible(Model))
                </div>
            </div>

        End If

    </div>
</div>

@Section Styles
    <link href="/Styles/Tables.css" rel="stylesheet" />
    <style>
        .pagewrapper {
            max-width: 550px;
            margin: auto;
        }

        .Download {
            float: right;
            margin-right: 0px;
            margin-left: 20px;
        }

        .Order .CellEpigraf {
            font-style: italic;
            padding-left: 10px;
        }

        .Item .CellTxt {
            padding-left: 20px;
        }
    </style>
End Section

