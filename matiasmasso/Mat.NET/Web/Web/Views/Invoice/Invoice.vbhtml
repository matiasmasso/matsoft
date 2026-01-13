@ModelType DTOInvoice
@Code
    Layout = "~/Views/shared/_Layout.vbhtml"

    ViewBag.Title = Mvc.ContextHelper.Tradueix("factura", "factura", "invoice")
    ViewData("DiscountExists") = DTOInvoice.DiscountExists(Model)

    Dim exs As New List(Of Exception)
    Dim oRepLiq As DTORepLiq = Nothing
    Dim oUser = Mvc.ContextHelper.FindUserSync()
    If oUser.Rol.id = DTORol.Ids.Rep Then
        Dim oRep = FEB2.User.GetRepSync(oUser, exs)
        oRepLiq = FEB2.Repliq.HeaderSync(exs, oUser.Emp, Model, oRep)
    End If
End Code

<div class="pagewrapper">

    <section class="PageTitle">
        <a href='@FEB2.Invoice.PdfUrl(Model)' title='@(Mvc.ContextHelper.Tradueix("Descargar fichero Pdf", "Descarregar fitxer Pdf", "Download Pdf file")) '>
            <img class="Download" src="~/Media/Img/48x48/Pdf48.png" />
        </a>
        
        <p>
            @DTOInvoice.Caption(Model, Mvc.ContextHelper.lang())
            <br/>
            <a href="@FEB2.Contact.Url(Model.Customer)">@Model.Customer.Nom</a>
            <br />
            Nif: @Model.Customer.Nifs.PrimaryNifValue()
        </p>



    </section>


    <div class="Grid">
        @Html.Partial("_RowTitles")

        @For Each oDelivery As DTODelivery In Model.Deliveries

            @<div class="Row Delivery">
                <div class="CellEpigraf">
                    <a href="@FEB2.Delivery.Url(oDelivery)">
                        @DTODelivery.Caption(oDelivery, Mvc.ContextHelper.lang())
                    </a>
                </div>

                <div class="CellNum"></div>

                <div class="CellPrice"></div>

                @If ViewData("DiscountExists") Then
                    @<div class="CellDto"></div>
                End If

                <div class="CellAmt"></div>

            </div>

            Dim oOrder As New DTOPurchaseOrder
            For Each item As DTODeliveryItem In oDelivery.Items
                If Not oOrder.Equals(item.PurchaseOrderItem.PurchaseOrder) Then
                    oOrder = item.PurchaseOrderItem.PurchaseOrder

                    @<div class="Row Order">

                        <div class="CellEpigraf">
                            <a href="@FEB2.PurchaseOrder.Url(oOrder)">
                                @oOrder.Caption(Mvc.ContextHelper.lang())
                            </a>
                        </div>

                        <div class="CellNum"></div>

                        <div class="CellPrice"></div>

                        @If ViewData("DiscountExists") Then
                            @<div class="CellDto"></div>
                        End If

                        <div class="CellAmt"></div>

                    </div>


                End If
            @<div class="Row Item">

                <div class="CellTxt">
                    <a href="@item.purchaseOrderItem.sku.GetUrl(Mvc.ContextHelper.Lang)">
                        @item.purchaseOrderItem.sku.NomLlarg.Tradueix(Mvc.ContextHelper.Lang)
                    </a>
                </div>

                <div class="CellNum">
                    @Microsoft.VisualBasic.Format(item.Qty, "#,###")
                </div>

                <div class="CellPrice">
                    @DTOAmt.CurFormatted(item.Price)
                </div>

                @If ViewData("DiscountExists") Then
                    @<div class="CellDto">
                        @If item.Dto <> 0 Then
                        @<span>@Microsoft.VisualBasic.FormatPercent(item.Dto / 100, 0)</span>
                        End If
                    </div>
                End If

                <div class="CellAmt">
                    @DTOAmt.CurFormatted(item.Import)
                </div>

            </div>
            Next


        Next


        <div class="Row Item">
            <div class="CellTxt">
                @If Model.Iva = 0 Then
                    @Html.Raw("total")
                Else
                    @Mvc.ContextHelper.Tradueix("Base Imponible", "Base Imponible", "Taxable amount")
                End If
            </div>
             <div class="CellNum"></div>
             <div class="CellPrice"></div>
             @If ViewData("DiscountExists") Then
                 @<div class="CellDto"></div>
             End If
                                    <div class="CellAmt">
                @DTOAmt.CurFormatted(DTOInvoice.GetBaseImponible(Model))
            </div>
        </div>

        @If Model.Iva > 0 Then

            @<div class="Row Item">
                <div class="CellTxt">
                    @DTOInvoice.TaxText(Model, DTOTax.Codis.Iva_Standard)
                </div>
                <div class="CellNum"></div>
                <div class="CellPrice"></div>
                @If ViewData("DiscountExists") Then
                    @<div class="CellDto"></div>
                End If
                                                    <div class="CellAmt">
                    @DTOAmt.CurFormatted(DTOInvoice.TaxAmt(Model, DTOTax.Codis.Iva_Standard))
                </div>
            </div>

            If Model.Req > 0 Then

                @<div class="Row Item">
                    <div class="CellTxt">
                        @DTOInvoice.TaxText(Model, DTOTax.Codis.Recarrec_Equivalencia_Standard)
                    </div>
                    <div class="CellNum"></div>
                    <div class="CellPrice"></div>
                    @If ViewData("DiscountExists") Then
                        @<div class="CellDto"></div>
                    End If
                                                                    <div class="CellAmt">
                        @DTOAmt.CurFormatted(DTOInvoice.TaxAmt(Model, DTOTax.Codis.Recarrec_Equivalencia_Standard))
                    </div>
                </div>

            End If

            @<div class="Row Item">
                <div class="CellTxt">
                    Total
                </div>
                <div class="CellNum"></div>
                <div class="CellPrice"></div>
                @If ViewData("DiscountExists") Then
                    @<div class="CellDto"></div>
                End If
                                                                                    <div class="CellAmt">
                    @DTOAmt.CurFormatted(DTOInvoice.GetTotal(Model))
                </div>
            </div>

        End If

    </div>

    <section Class="RepLiq">
        @If oUser.Rol.Id = DTORol.Ids.Rep Then
            If oRepLiq Is Nothing Then
                @<span>Factura pendiente de liquidación</span>
            Else
                @<span>
                    Factura liquidada en 
                    <a href="@FEB2.DocFile.DownloadUrl(oRepLiq.Cca.DocFile, False)" target="_blank">
                        liquidación num. @oRepLiq.Id del @Microsoft.VisualBasic.Format(oRepLiq.Fch, "dd/MM/yy")
                    </a>
                </span>
            End If
                                                                                            End If
    </section>

</div>

@Section Styles

    <style>
        .pagewrapper {
            max-width: 550px;
            margin: auto;
        }
        .Download {
            float:  right;
            margin-right: 0px;
            margin-left: 20px;
        }
        .Delivery .CellEpigraf {
            font-weight: 700;
        }

        .Order .CellEpigraf {
            font-style: italic;
            padding-left:10px;
        }

        .Item .CellTxt {
            padding-left: 20px;
        }
    </style>
End Section

