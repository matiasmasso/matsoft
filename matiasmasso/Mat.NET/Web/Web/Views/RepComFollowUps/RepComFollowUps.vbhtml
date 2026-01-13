@ModelType DTORep
@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"

    Dim sTitle As String = Mvc.ContextHelper.Tradueix("Seguimiento de Comisiones", "Seguiment de Comisions", "Commission Follow up")
    ViewBag.Title = "M+O | " & sTitle

    Dim oOrders As List(Of DTOPurchaseOrder) = FEB2.Rep.ArchiveSync(exs, Model)
    Dim oMonths As List(Of DTORepComFollowUp) = DTORepComFollowUp.Months(oOrders, Mvc.ContextHelper.lang())
End Code


    <div class="PageTitle">@sTitle</div>


    <div class="DrillDownGridTitles">
        <div class="RightAlignedTotals">
            <div class="CellResult">@Mvc.ContextHelper.Tradueix("Pedido", "Demanat", "Ordered")</div>
            <div class="CellResult">@Mvc.ContextHelper.Tradueix("Servido", "Servit", "Delivered")</div>
            <div class="CellResult">@Mvc.ContextHelper.Tradueix("Facturado", "Facturat", "Invoiced")</div>
            <div class="CellResult">@Mvc.ContextHelper.Tradueix("Liquidado", "Liquidat", "Liquidated")</div>
        </div>
    </div>

    <div class="DrillDownGrid">

            @For Each oMonth As DTORepComFollowUp In oMonths
                @<div class='Collapsed truncate FollowUpRow'>
                    <a href="#" class="PlusMinus">&nbsp;</a>
                    <div class="Period">
                        @oMonth.Period

                        <div class="RightAlignedTotals">
                            <div class="CellResult">
                                @Format(oMonth.Ordered, "#,##0.00 €;-#,##0.00 €;#")
                            </div>
                            <div class="CellResult">
                                @Format(oMonth.Delivered, "#,##0.00 €;-#,##0.00 €;#")
                            </div>
                            <div class="CellResult">
                                @Format(oMonth.Invoiced, "#,##0.00 €;-#,##0.00 €;#")
                            </div>
                            <div class="CellResult">
                                @Format(oMonth.Liquid, "#,##0.00 €;-#,##0.00 €;#")
                            </div>
                        </div>
                    </div>

                    @For Each oDay As DTORepComFollowUp In DTORepComFollowUp.Days(oOrders, oMonth, Mvc.ContextHelper.lang())
                        @<div class='Collapsed truncate FollowUpRow'>
                            <a href="#" class="PlusMinus">&nbsp;</a>
                            <div class="Period LevelDay">
                                @oDay.Period

                                <div class="RightAlignedTotals">
                                    <div class="CellResult LevelDay">
                                        @Format(oDay.Ordered, "#,##0.00 €;-#,##0.00 €;#")
                                    </div>
                                    <div class="CellResult LevelDay">
                                        @Format(oDay.Delivered, "#,##0.00 €;-#,##0.00 €;#")
                                    </div>
                                    <div class="CellResult LevelDay">
                                        @Format(oDay.Invoiced, "#,##0.00 €;-#,##0.00 €;#")
                                    </div>
                                    <div class="CellResult LevelDay">
                                        @Format(oDay.Liquid, "#,##0.00 €;-#,##0.00 €;#")
                                    </div>
                                </div>
                            </div>

                            @For Each oOrder As DTORepComFollowUp In DTORepComFollowUp.Orders(oOrders, oDay)
                                @<div class='Collapsed truncate FollowUpRow'>
                                    <div class="Period">
                                        <div class="CellOrder truncate">
                                            <a href="@FEB2.PurchaseOrder.Url(oOrder.Source)" target="_blank">
                                                @oOrder.Period
                                            </a>
                                        </div>
                                        <div class="RightAlignedTotals">
                                            <div class="CellResult">
                                                <a href="@FEB2.PurchaseOrder.Url(oOrder.Source)" target="_blank">
                                                    @Format(oOrder.Ordered, "#,##0.00 €;-#,##0.00 €;#")
                                                    </a>
</div>
                                            <div class="CellResult">
                                                <a href="repsortides/@oOrder.Source.Guid.ToString" target="_blank">
                                                    @Format(oOrder.Delivered, "#,##0.00 €;-#,##0.00 €;#")
                                                    </a>
</div>
                                            <div class="CellResult">
                                                <a href="repsortides/@oOrder.Source.Guid.ToString" target="_blank">
                                                    @Format(oOrder.Invoiced, "#,##0.00 €;-#,##0.00 €;#")
                                                </a>
</div>
                                            <div class="CellResult">
                                                <a href="repsortides/@oOrder.Source.Guid.ToString" target="_blank">
                                                    @Format(oOrder.Liquid, "#,##0.00 €;-#,##0.00 €;#")
                                                </a>
</div>
                                        </div>
                                    </div>
                                </div>
                            Next
                        </div>
                    Next
                </div>
            Next

        <section>
            <p>Columna <b>Pedidos</b></p>
            <p>
                Resume la suma de los importes de los pedidos registrados en un periodo determinado, incluyendo los descuentos aplicables al pedido pero no los aplicables exclusivamente a los envíos como puedan ser los incentivos por volumen.<br />
                Los importes de los pedidos no incluyen tampoco los impuestos.<br />
                Estos importes pueden variar incluso finalizado el período, debido a devoluciones o cancelaciones de los clientes.
            </p>
            <p>Columna <b>Servido</b></p>
            <p>
                Importe de la mercancía de los pedidos de la primera columna que ha sido enviada a los clientes, dentro o fuera del periodo en que se cursó cada pedido.<br />
                Este importe tiende con el tiempo a ser igual que el de la columna pedidos, aunque puede ser inferior por los siguientes motivos:
                <ul>
                    <li>porque algunos pedidos se hallen programados para su entrega en fecha futura</li>
                    <li>porque algunos pedidos no hayan sido entregados, total o parcialmente, por falta de existencias</li>
                    <li>por incentivos al volumen aplicados en el envío</li>
                </ul>
            </p>
            <p>Columna <b>Facturado</b></p>
            <p>
                Importe de la mercancía facturada a los clientes sobre los pedidos de la primera columna.
                Este importe tiende a ser igual al de la columna Servido, aunque puede ser inferior por los siguientes motivos:
                <ul>
                    <li>puede haber un desfase temporal desde que se expide el albarán de entrega hasta que se expide la factura correspondiente (la normativa de Hacienda exige que la expedición de las facturas se realice antes del día 16 del mes siguiente a aquél en el curso del cual se hayan realizado las operaciones. El plazo de envío es de un mes a partir de la fecha de su expedición) </li>
                </ul>
            </p>
            <p>Columna <b>Liquidado</b></p>
            <p>
                Importe de las facturas liquidadas sobre los pedidos de la primera columna.<br />
                Este importe tiende a ser igual al de la columna Facturado, aunque puede ser inferior por los siguientes motivos:
                <ul>
                    <li>La liquidación no haya sido expedida aún. Se expide una liquidación cada final de mes</li>
                    <li>La factura no sea aún liquidable. Una factura es liquidable cuando ha sido cobrada efectivamente, o en caso de domiciliación bancaria, hayan pasado 20 días desde su vencimiento.</li>
                    <li>El cliente mantenga impagados pendientes. En este caso se suspende preventivamente la liquidación de sus facturas hasta que se regulariza el saldo.</li>
                </ul>
            </p>
            <p><b>Navegación</b></p>
            <p>
                La página presenta por defecto los importes agrupados de los últimos 12 meses enteros más el mes corriente y computados en tiempo real.<br />
                Estos importes pueden variar en cualquier momento, incluso noches y festivos por los pedidos que clientes y representantes puedan pasar por internet.<br />
                Accionando el icono a la izquierda de cada fila podemos profundizar en la información y obtener el mismo detalle por día.<br />
                El mismo icono en la fila de día nos dará acceso a los importes servidos, facturados y liquidados de cada pedido que se registró en esa fecha.<br />
                Cada fila de pedido está enlazada a la ficha de pedido, a la que podemos acceder por ejemplo seleccionando el nombre del cliente.<br />
                La ficha de pedido nos presenta el detalle de las unidades servidas y/o pendientes de entrega.<br />
                Seleccionando las unidades servidas accederemos al historial de cada linea de pedido, con mención de cuantas unidades se sirvieron en qué fechas, con qué albaranes y en qué facturas fueron facturadas.<br />
                Las menciones de albaranes y facturas llevan un enlace para acceder a la información y documento correspondientes.<br />
                La ficha de factura muestra en su última linea si la factura ha sido liquidada o no, y en su caso en qué liquidación con un enlace para acceder a una copia del documento.
            </p>
        </section>




@Section Styles
<link href="~/Media/Css/PlusMinus.css" rel="stylesheet" />
    <style>
        .ContentColumn {
            width: 700px;
            margin: auto;
        }
        .DrillDownGridTitles {
            position:relative;
            height:30px;
        }
        .DrillDownGrid {
        }
        .FollowUpRow {
            position:relative;
        }
        .LevelDay {
            color:gray
        }
        .Period {
            display:inline-block;
        }
        .CellOrder {
            position:absolute;
            left:0;
            top:0;
            width:150px;
        }
        .RightAlignedTotals {
            position:absolute;
            right:0;
            top:0;
            text-align:right;
        }
        .CellResult {
            display:inline-block;
            width:120px;
            text-align:right;
        }
        .FollowUpRow {
        }
    </style>
End Section

@Section Scripts
<script src="~/Media/js/PlusMinus.js"></script>
<script>
</script>
End Section