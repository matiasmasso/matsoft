@ModelType DTODiari
@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"

    Dim exs As New List(Of Exception)
    Dim sFch As String = ContextHelper.lang().WeekDay(Model.Fch) & " " & Model.Fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
    ViewBag.Title = ContextHelper.Tradueix("Diario de pedidos", "Diari de Comandes", "Diary orders") & " " & sFch
End Code

<div class="PageTitle">@sFch</div>

<div class="Grid">

    <div class="RowHdr">
        <div class="CellNum">
            @Model.Lang.Tradueix("Hora", "Hora", "Time")
        </div>
        <div class="CellIco">
            &nbsp;
        </div>
        <div class="CellNum">
            @Model.Lang.Tradueix("Pedido", "Comanda", "Order")
        </div>

        <div class="CellTxt">
            @Model.Lang.Tradueix("Cliente", "Client", "Customer")
        </div>

        @If Model.DisplayableBrandsCount > 0 Then
            @<div class="CellAmt">
                @Model.Lang.Tradueix("Total", "Total", "Total")
            </div>
            @For Each oBrand As DTOProductBrand In Model.DisplayableBrands
                @<div class="CellAmt">
                    @oBrand.Nom.Tradueix(ContextHelper.Lang)
                </div>
            Next
            @<div class="CellAmt">
                @Model.Lang.Tradueix("Otras", "Altres", "Others")
            </div>
        End If

    </div>

    @For Each item As DTODiariItem In FEB.Diari.Orders(Model, EXS)
        Dim oOrder As DTOPurchaseOrder = item.PurchaseOrder '.Source

        @<div class="Row">
            <div class="CellNum">
                @Format(oOrder.UsrLog.FchCreated, "HH:mm")
            </div>
            <div class="CellIco">
                <img src="~/Media/Img/Ico/PdcSrc/@(CInt(oOrder.Source).ToString()).gif" />
            </div>
            <div class="CellNum">
                <a href="@FEB.PurchaseOrder.Url(oOrder)">
                    @oOrder.Num
                </a>
            </div>
            <div class="CellTxt">
                <a href="@FEB.Contact.Url(oOrder.Customer)">
                    @oOrder.Customer.FullNom
                </a>
            </div>


            @If Model.DisplayableBrandsCount > 0 Then
                @<div class="CellAmt">
                    @DTOAmt.CurFormatted(DTOAmt.Factory(item.Total))
                </div>
                @For j As Integer = 0 To Model.DisplayableBrandsCount - 1
                    @<div class="CellAmt">
                        @DTOAmt.CurFormatted(DTOAmt.Factory(item.Values(j)))
                    </div>
                Next
                @<div class="CellAmt">
                    @DTOAmt.CurFormatted(DTOAmt.Factory(item.Resto))
                </div>
            End If

        </div>
    Next
</div>

@Section styles
    <link href="~/Media/Css/Tables.Css" rel="stylesheet" />
End Section


