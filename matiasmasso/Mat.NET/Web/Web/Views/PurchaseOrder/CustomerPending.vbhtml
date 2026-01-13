@ModelType List(Of DTOPurchaseOrderItem)

@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    ViewBag.Title = "CustomerPending"

    Dim oOrderGuid As Guid = Guid.NewGuid
    Dim oCustomerGuid As Guid = Guid.NewGuid
    Dim FirstLine As Boolean = True

    Dim ShowCustomers As Boolean
    Dim oCod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.Client
    If Model.Count > 0 Then
        Dim iCount As Integer = Model.Select(Function(x) x.PurchaseOrder.Contact.Guid).Distinct().Count()
        ShowCustomers = iCount > 1
        oCod = Model(0).PurchaseOrder.Cod
    End If

End Code


<div class="title">
        <h3>@Mvc.ContextHelper.Tradueix("Pendiente de entrega", "Pendent d'entrega", "Open Orders")</h3>
        @If Model.Count > 0 And Not ShowCustomers Then
            @<a href='@FEB2.PurchaseOrders.ExcelUrl(Model(0).PurchaseOrder.Contact)' title='@(Mvc.ContextHelper.Tradueix("Descargar fichero Excel", "Descarregar fitxer Excel", "Download Excel file")) '>
                <img class="Excel" src="~/Media/Img/48x48/Excel48.png" />
            </a>
        End If
    </div>

    <div class="Grid">

            <span class="CellTxt">@Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</span>
            <span class="CellQty">@Mvc.ContextHelper.Tradueix("Cant", "Quant", "Qty")</span>
            <span class="CellIco">Stock</span>

        @For Each item As DTOPurchaseOrderItem In Model

                If ShowCustomers Then
                    If Not item.PurchaseOrder.Contact.Guid.Equals(oCustomerGuid) Then
                        oCustomerGuid = item.PurchaseOrder.Contact.Guid
                        If FirstLine Then
                            FirstLine = False
                        Else
                            @<span Class="CellEpigraf">&nbsp;</span>
                            @<span Class="CellNum"></span>
                            @<span Class="CellIco"></span>
                    End If
                        @<span class="CellEpigraf">
                            <a href="@FEB2.Contact.Url(item.PurchaseOrder.Contact)" title='@Mvc.ContextHelper.Tradueix("ver perfil de cliente", "veure perfil de client", "see customer profile")'>
                                @item.PurchaseOrder.Contact.FullNom
                            </a>
                        </span>
                        @<span class="CellNum"></span>
                        @<span class="CellIco"></span>
                End If
            End If

            If Not item.PurchaseOrder.Guid.Equals(oOrderGuid) Then
                oOrderGuid = item.PurchaseOrder.Guid
                    @<span class="CellEpigraf">&nbsp;</span>
                    @<span class="CellNum">&nbsp;</span>
                    @<span class="CellIco"></span>

                    @<span Class="CellPO">
                        <a href = "@FEB2.PurchaseOrder.Url(item.purchaseOrder)" title='@Mvc.ContextHelper.Tradueix("ver pedido original", "veure comanda original", "see original order")'>
                            @item.purchaseOrder.caption(Mvc.ContextHelper.Lang())
                        </a>
                    </span>
                    @<span class="CellNum"></span>
                    @<span class="CellIco"></span>
            End If

                @<span class="CellSku">
                    <a href="@item.sku.GetUrl(Mvc.ContextHelper.Lang)" title='@Mvc.ContextHelper.Tradueix("ver ficha de producto", "veure fitxa del producte", "see product information")' target="_blank">
                        @if oCod = DTOPurchaseOrder.Codis.Proveidor Then
                            @DTOProductSku.RefYNomPrv(item.Sku)
                        Else
                            @item.sku.nomLlarg.Tradueix(Mvc.ContextHelper.Lang)
                        End If
                    </a>
                </span>
                @<span class="CellNum">
                    @item.pending.ToString("N0")
                </span>

            If item.purchaseOrder.fchDeliveryMin > Today Then
                    @<span class="CellWait" title='@String.Format("{0} {1:dd/MM/yy}", Mvc.ContextHelper.Tradueix("programado para", "programat per", "scheduled for"), item.purchaseOrder.fchDeliveryMin)'>&nbsp</span>
                 ElseIf item.sku.isAvailable Then
                    @<span class="CellOk" title='@Mvc.ContextHelper.Tradueix("en stock", "en stock", "in stock")'>&nbsp;</span>
                 Else
                    @<span class="CellKo" title='@Mvc.ContextHelper.Tradueix("agotado", "exhaurit", "out of stock")'>&nbsp;</span>
                 End If


        Next
    </div>


@Section Styles

    <style>
        .ContentColumn {
            max-width: 600px;
            margin: 0 auto;
        }

        .title {
            display:flex;
            justify-content: space-between;
        }

        .Grid {
            display: grid;
            grid-template-columns: 1fr 70px 50px;
            border-top: 1px solid gray;
            border-right: 1px solid gray;
        }

            .Grid > span {
                padding: 8px 4px 2px 4px;
                border-left: 1px solid gray;
                border-bottom: 1px solid gray;
            }

                .Grid > span:nth-child(6n+4),
                .Grid > span:nth-child(6n+5),
                .Grid > span:nth-child(6n+6) {
                    background-color: #EFEFEF;
                }

        .CellPO {
            font-weight: 700;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
        .CellSku {
            padding-left: 10px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
        .CellNum {
            text-align:right;
        }

        .CellOk {
            text-align: center;
            padding-top: 2px;
            background-image: url('/Media/Img/Ico/ok.png');
            background-repeat: no-repeat;
            background-position: center;
            cursor: pointer;
        }

        .CellKo {
            text-align: center;
            padding-top: 2px;
            background-image: url('/Media/Img/Ico/aspa.png');
            background-repeat: no-repeat;
            background-position: center;
            cursor: pointer;
        }

        .CellWait {
            text-align: center;
            padding-top: 2px;
            background-image: url('/Media/Img/Ico/alarmClock.png');
            background-repeat: no-repeat;
            background-position: center;
            cursor: pointer;
        }
        
        .Excel {
            float:right;
            margin-right:0px;
            margin-left:20px;
        }
    </style>
End Section
