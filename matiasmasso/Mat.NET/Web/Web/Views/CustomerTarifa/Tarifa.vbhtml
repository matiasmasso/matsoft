@ModelType DTOCustomerTarifa
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)

    Dim DtoVisible As Boolean = Model.Skus.ToList.Exists(Function(x) x.customerDto <> 0)
    Dim GridClass = "Grid2Cols"
    If Model.CostEnabled Then GridClass = "Grid3Cols"
    If DtoVisible Then GridClass = "Grid4Cols"

End Code



    <section class="PageTitle">
        <h1>@Html.Raw(ViewBag.Title)</h1>
        
        <a href='@FEB2.CustomerTarifa.ExcelUrl(Model)' title='@(Mvc.ContextHelper.Tradueix("Descargar fichero Excel", "Descarregar fitxer Excel", "Download Excel file")) '>
            <img class="Excel" src="~/Media/Img/48x48/Excel48.png" />
        </a>
    </section>

    <div class="@GridClass">
        <div class="Row">
            <div class="CellSku">@Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</div>
            @If Model.CostEnabled Then
                @<div class="CellCost">@Mvc.ContextHelper.Tradueix("Coste", "Cost", "Cost")</div>

                @If DtoVisible Then
                    @<div class="CellDto">@Mvc.ContextHelper.Tradueix("Dto", "Dte", "Dct")</div>
                End If
            End If
            <div class="CellPvp">@Mvc.ContextHelper.Tradueix("PVP", "PVP", "RRPP")</div>
        </div>

        @For Each oBrand As DTOProductBrand In Model.Brands
            @<div class="Row">
                <div class="CellBrand"><a id="@oBrand.Guid.ToString">@oBrand.Nom.Tradueix(Mvc.ContextHelper.Lang)</a></div>
            </div>
            @For Each oCategory As DTOProductCategory In oBrand.Categories
                @<div class="Row">
                    <div class="CellCategory">@oCategory.Nom.Tradueix(Mvc.ContextHelper.Lang)</div>
                </div>
                For Each oSku As DTOProductSku In oCategory.Skus
                    @<div class="Row">
                        <div class="CellSku">
                            <a href="@oSku.GetUrl(Mvc.ContextHelper.Lang)">
                                @oSku.Nom.Tradueix(Mvc.ContextHelper.Lang())
                            </a>
                        </div>
                        @If Model.CostEnabled Then
                            @<div class="CellCost">
                                @DTOAmt.CurFormatted(oSku.price)
                            </div>
                            @If DtoVisible Then
                                @<div class="CellDto">@Format(oSku.customerDto, "0\%;-0\%;#")</div>
                            End If
                        End If
                        <div class="CellPvp">
                            @DTOAmt.CurFormatted(oSku.rrpp)
                        </div>
                    </div>
                Next
            Next
        Next

    </div>
    
    @If Model.CostEnabled Then
            @<p>@Mvc.ContextHelper.Tradueix("Los precios de coste no incluyen el IVA", "Els preus de cost no inclouen l'IVA", "Cost prices are VAT excluded")</p>
            @<p>@Mvc.ContextHelper.Tradueix("Los precios recomendados de venta al público son IVA incluido", "Els preus recomenats de venda al public son IVA inclos", "Retail prices are VAT included")</p>
        End If





@Section Styles
    <style>
        .ContentColumn {
            max-width: 500px;
            margin: auto;
        }

        .PageTitle {
            display:flex;
            justify-content: space-between;
            align-items: center;
        }

        .Grid2Cols {
            display: grid;
            grid-template-columns: auto 100px;
            margin-top: 20px;
        }

        .Grid3Cols {
            display: grid;
            grid-template-columns: auto 90px 90px;
            margin-top: 20px;
        }

        .Grid4Cols {
            display: grid;
            grid-template-columns: auto 80px 80px 80px;
            margin-top: 20px;
        }

        .Grid2Cols .CellBrand, .Grid2Cols .CellCategory {
            grid-column-end: span 2;
        }

        .Grid3Cols .CellBrand, .Grid3Cols .CellCategory {
            grid-column-end: span 3;
        }

        .Grid4Cols .CellBrand, .Grid4Cols .CellCategory {
            grid-column-end: span 4;
        }

        .CellBrand {
            text-align: center;
            font-weight: 700;
            padding: 15px 0 10px;
        }

        .CellCategory {
            font-weight: 600;
            padding: 15px 0 10px;
        }

        .CellSku {
            border-bottom: 1px solid #BBBBBB;
        }

            .CellSku a:hover {
                color: #167ac6;
            }

        .CellCost {
            text-align: right;
            border-bottom: 1px solid #BBBBBB;
        }

        .CellDto {
            text-align: right;
            border-bottom: 1px solid #BBBBBB;
        }

        .CellPvp {
            text-align: right;
            border-bottom: 1px solid #BBBBBB;
        }

        .Row {
            display: contents;
        }

        .Sku {
            padding-left: 20px;
            border: 1px solid blue;
        }

        .Excel {
            margin-right: 0px;
            margin-left: 20px;
        }
    </style>
End Section
