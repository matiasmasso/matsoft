@ModelType List(Of DTOProductSku)
@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"

    Dim sTitle As String = "Outlet profesional"
    ViewBag.Title = DTOWebPage.CorporateTitle(sTitle)
    Dim oUser = ContextHelper.FindUserSync()
    Dim oCustomers = FEB.User.CustomersForBasketSync(exs, oUser)
    Dim OrderEnabled As Boolean = (oUser.Rol.id = DTORol.Ids.CliFull Or oUser.Rol.id = DTORol.Ids.CliLite) And oCustomers.Count > 0
    Dim oCategory As New DTOProductCategory
    Dim oBrand As New DTOProductBrand

End Code


<div class="pagewrapper">
    <div class="PageTitle">@sTitle</div>
    @If Model.Count = 0 Then
        @<div>
            @ContextHelper.Tradueix("No disponemos de excedentes de stocks en estos momentos", "No disposem de excedents de stocks en aquests moments", "No overstocks available currently")
        </div>
    ElseIf Not OrderEnabled Then
        @<div>
            @ContextHelper.Tradueix("Su correo electrónico no está asociado a ninguna ficha activa de cliente. Por favor póngase en contacto con nuestras oficinas para actualizar sus datos.", "El seu correu electronic no está associat a cap fitxa de client en actiu. Si us plau posis en contacte amb les nostres oficines per actualitzar les seves dades.", "Sorry your email is not linked to any active customer account. Please contact our offices to update your profile.")
        </div>
    Else
        @<div class="DataCollection">
    @If (oUser.Rol.id = DTORol.Ids.CliFull Or oUser.Rol.id = DTORol.Ids.CliLite) Then
    @<p>
        @ContextHelper.Tradueix("Para cursar un pedido rellene las cantidades en la columna de la derecha y pulse el botón al final del listado", "Per cursar una comanda ompli les quantitats a la columna de la dreta i premi el botó que trobará al final del llistat", "Fill your quantities on right column and press the button at the bottom of the list to submit your order")
    </p>
    End If

    <div Class="Grid Outlet">

        <div Class="RowHdr">
            <div Class="CellTxt">
                @ContextHelper.Tradueix("Producto", "Producte", "Product")
            </div>
            <div Class="CellAmt" @IIf(OrderEnabled, "", "hidden='hidden'")>
                @ContextHelper.Tradueix("Coste", "Cost", "Cost")
            </div>
            <div Class="CellDto">
                @ContextHelper.Tradueix("Dto", "Dte", "Dct")
            </div>
            <div Class="CellNum">
                @ContextHelper.Tradueix("Min", "Min", "MOQ")
            </div>
            <div Class="CellAmt" @IIf(OrderEnabled, "", "hidden='hidden'")>
                @ContextHelper.Tradueix("Pedido", "Comanda", "Order")
            </div>
        </div>


        @For Each oSku As DTOProductSku In Model
            If oSku.Category.UnEquals(oCategory) Then
                If oSku.Category.Brand.UnEquals(oBrand) Then
                    oBrand = oSku.Category.Brand
                    @<div class="Row">
                        <div Class="CellTxt CellBrand">
                            @oSku.Category.Brand.Nom
                        </div>
                        <div Class="CellAmt" @IIf(OrderEnabled, "", "hidden='hidden'")>
                            &nbsp;
                        </div>
                        <div Class="CellDto">
                            &nbsp;
                        </div>
                        <div Class="CellNum">
                            &nbsp;
                        </div>
                        <div Class="CellAmt" @IIf(OrderEnabled, "", "hidden='hidden'")>
                            &nbsp;
                        </div>
                    </div>
                End If
                oCategory = oSku.Category
                @<div class="Row">
                    <div Class="CellTxt CellCategory">
                        @oSku.Category.Nom
                    </div>
                    <div Class="CellAmt" @IIf(OrderEnabled, "", "hidden='hidden'")>
                        &nbsp;
                    </div>
                    <div Class="CellDto">
                        &nbsp;
                    </div>
                    <div Class="CellNum">
                        &nbsp;
                    </div>
                    <div Class="CellAmt" @IIf(OrderEnabled, "", "hidden='hidden'")>
                        &nbsp;
                    </div>
                </div>
            End If

            @<div class="Row">

                <div Class="CellTxt CellSku">
                    <a href="@oSku.GetUrl(ContextHelper.Lang)" target="_blank">@oSku.Nom.Tradueix(ContextHelper.Lang())</a>
                </div>
                <div Class="CellAmt" @IIf(OrderEnabled, "", "hidden='hidden'")>
                    @If oSku.Price IsNot Nothing Then
                        @<span>@IIf(OrderEnabled, DTOAmt.CurFormatted(oSku.Price), "")</span>
                    End If
                </div>
    <div Class="CellDto">
                    @Format(oSku.OutletDto, "#\%")
                </div>
                <div Class="CellNum">
                    @FEB.Outlet.MOQ(oSku)
                </div>
                <div Class="CellAmt" @IIf(OrderEnabled, "", "hidden='hidden'")>
                    <input Class="Qty" type="number" data-sku="@oSku.Guid.ToString" data-price="0" min="@FEB.Outlet.MOQ(oSku)" max="@oSku.Stock" />
                </div>

            </div>
        Next
    </div>

    @If OrderEnabled Then
        @<div class="Submit" @IIf(OrderEnabled, "data-customer=" & oCustomers.First.Guid.ToString, "")>
            <input type="button" value='@ContextHelper.Tradueix("enviar pedido", "enviar comanda", "submit order")' />
        </div>
    End If
</div>

    @<div class="Thanks">
    </div>
    End If
</div>

@Section Styles
<style>
    .Row {
        height:40px;
    }
    .CellBrand {
        font-weight: 900;
    }

    .CellCategory {
        color:gray;
        font-weight: 600;
        padding-left: 20px;
    }

    .CellSku {
        padding-left: 40px;
    }

    .Qty {
        width: 60px;
    }
    .Submit {
        text-align:right;
        padding:5px 0 
    }
    .pagewrapper {
        max-width:500px;
    }
</style>
End Section

@Section Scripts
    <script>
        $(document).on('click', '.Submit input', function () {
            var orderLines = lines();
            if (orderLines.length == 0)
                alert('el pedido está vacío');
            else
                submitOrder(orderLines);
        });

        $(document).on('change', '.Qty', function () {
            var qty = parseInt($(this).val());
            var min = parseInt($(this).attr('min'));
            var max = parseInt($(this).attr('max'));
            if (qty < min) {
                alert('el pedido mínimo para descuento es de ' + min + ' unidades');
                $(this).val(min)
            }
            if (qty > max) {
                alert('máximo ' + max + ' unidades en existencia');
                $(this).val(max)
            }
        });

        function submitOrder(lines) {
            var data = JSON.stringify(lines);
            $('.loading').show();
            $.ajax({
                url: '/outlet/update',
                data: {customer: $('.Submit').data('customer'), lines: data },
                type: 'POST',
                dataType: "json",
                success: function (result) {
                    $('.loading').hide();
                    $('.DataCollection').hide();
                    $('.Thanks').show();

                    if (result.errors == 0) {
                        var data = JSON.stringify(result);
                        $('.loading').show();
                        $(".Thanks").load('/pedido/Thanks',
                            { data: data },
                            function () {
                                $('.DataCollection').hide();
                                $('.Thanks').show();
                                $('.loading').hide();
                                $.getJSON('/pedido/MailConfirmation', { orderGuid: result.guid });
                            });
                    } else {
                        alert('error: ' + result.message);
                    }
                }
            });
        };

        function lines() {
            var retval = [];
            $(".Qty").each(function () {
                if ($(this).val()) {
                    var data = {
                        qty: $(this).val(),
                        sku: $(this).data('sku'),
                    };
                    retval.push(data);
                }
            });
            return (retval);
        };
    </script>
End Section
