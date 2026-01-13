@ModelType DTOSellOut

@Code
    ViewBag.Title = "Sell-out"
    Layout = "~/Views/Shared/_Layout_FullWidth.vbhtml"

    
    Dim exs As New List(Of Exception)

    DTOSellOut.SetLevelToExpand(Model, 1)
    Model.ExpandToLevel = 0

    Dim HideIfCustomerFilter As String = ""
    Dim CustomerGuid As String = ""
    If DTOSellOut.FilterValues(Model, DTOSellOut.Filter.Cods.Customer).Count > 0 Then
        HideIfCustomerFilter = "hidden='hidden'"
        CustomerGuid = DTOSellOut.FilterValues(Model, DTOSellOut.Filter.Cods.Customer).First.Guid.ToString
    End If

    Dim iYears As List(Of Integer) = FEB2.EciSalesReport.YearsSync(Model, exs)
    Dim oProductsFilter = DTOSellOut.FilterValues(Model, DTOSellOut.Filter.Cods.Product)
    Dim oAreasFilter = DTOSellOut.FilterValues(Model, DTOSellOut.Filter.Cods.Atlas)


    Dim oBrands = FEB2.EciSalesReport.BrandsSync(Model, exs)
    Dim oBrandFilter As DTOProductBrand = Nothing
    Dim oCategories As New List(Of DTOProductCategory)
    'If oProductsFilter.Count > 0 AndAlso TypeOf oProductsFilter.First Is DTOProductBrand Then
    'oBrandFilter = oProductsFilter.First
    'oCategories = oBrandFilter.Categories
    'End If

    Dim HideIfSingleBrand As String = ""
    If oBrands.Count = 1 Then
        HideIfSingleBrand = "hidden='hidden'"
        oCategories = oBrands.First.Categories
    End If

    Dim oCountries As List(Of DTOCountry) = FEB2.EciSalesReport.CountriesSync(Model, exs)
    Dim oZonas As New List(Of DTOZona)
    If oCountries.Count = 1 Then
        oZonas = oCountries.First.Zonas
    End If

End Code



<div id="pagewrapper">

    <section class="PageTitle ToolStrip">
        <a id="ExcelDownload" href='#' title='@(Mvc.ContextHelper.Tradueix("Descargar fichero Excel", "Descarregar fitxer Excel", "Download Excel file")) '>
            <img class="Download" src="~/Media/Img/48x48/Excel48.png" />
        </a>
        @Mvc.ContextHelper.Tradueix("Pedidos de clientes", "Comandes de clients", "Sell-out data")
    </section>



    <p class="miscellanea">
        @Html.Raw(Mvc.ContextHelper.Tradueix(
                                                                                        "Las cifras se refieren a unidades vendidas en el mes en que el que el cliente cursó el pedido, tanto si se le ha servido como si no.<br/>Estas cifras pueden verse afectadas por potenciales devoluciones, en cuyo caso se restan del pedido original.",
                                                                                        "Les xifres es refereixen a unitas venudes el mes que es va rebre la comanda, tant si es va servir llavors com si no.<br/>Les xifres poden variar en el futur per potencials devolucions, que serien deduides del mes en que van ser demanades.",
                                                                                        "Figures refer to units ordered by customers, in the month the order was logged, regardless if delivered or not.<br/>Potential returns may affect them in the future as they are deducted from the original order."))
    </p>

    <div class="divselectors">
        <select id="year">
            @For i As Integer = 0 To iYears.Count - 1
            @<option value="@iYears(i)" @IIf(i = 0, "selected='selected'", "")>@Html.Raw(iYears(i))</option>
            Next
        </select>

        <select id="conceptType" @HideIfCustomerFilter>
            @If Model.ConceptType = DTOSellOut.ConceptTypes.Product Then
            @<option value="@CInt(DTOSellOut.ConceptTypes.Product)" selected='selected'>@Mvc.ContextHelper.Tradueix("productos", "productes", "products", "produtos")</option>
            Else
            @<option value="@CInt(DTOSellOut.ConceptTypes.Product)">@Mvc.ContextHelper.Tradueix("productos", "productes", "products", "produtos")</option>
            End If

            @If Model.ConceptType = DTOSellOut.ConceptTypes.Geo Then
            @<option value="@CInt(DTOSellOut.ConceptTypes.Geo)" selected='selected'>@Mvc.ContextHelper.Tradueix("destinos", "destinacions", "destinations")</option>
            Else
            @<option value="@CInt(DTOSellOut.ConceptTypes.Geo)">@Mvc.ContextHelper.Tradueix("destinos", "destinacions", "destinations")</option>
            End If

            @If Model.ConceptType = DTOSellOut.ConceptTypes.Centres Then
            @<option value="@CInt(DTOSellOut.ConceptTypes.Centres)" selected='selected'>@Mvc.ContextHelper.Tradueix("centros", "centres", "stores")</option>
            Else
            @<option value="@CInt(DTOSellOut.ConceptTypes.Centres)">@Mvc.ContextHelper.Tradueix("centros", "centres", "stores")</option>
            End If
        </select>


        <select id="format">
            <option value="@CInt(DTOSellOut.Formats.Units)" @IIf(Model.Format = DTOSellOut.Formats.Units, "selected='selected'", "")> @Mvc.ContextHelper.Tradueix("unidades", "unitats", "units")</option>
            <option value="@CInt(DTOSellOut.Formats.Amounts)" @IIf(Model.Format = DTOSellOut.Formats.Amounts, "selected='selected'", "")>@Mvc.ContextHelper.Tradueix("importes", "imports", "amounts")</option>
        </select>



            <select id = "brand" @HideIfSingleBrand @HideIfCustomerFilter>
                <option value = "" >@Mvc.ContextHelper.Tradueix("(todas las marcas)", "(totes les marques)", "(all brands)")</option>
                    @If oBrands IsNot Nothing Then

                        For Each oBrand As DTOProductBrand In oBrands
                            Dim match As Boolean
                            If oProductsFilter.Count > 0 Then
                                match = oProductsFilter.Any(Function(x) x.Equals(oBrand))
                            End If
                            If match Then
                                @<option value="@oBrand.Guid.ToString" selected='selected'>@oBrand.Nom</option>
                            Else
                                @<option value="@oBrand.Guid.ToString">@oBrand.Nom</option>
                            End If
                        Next
                    End If
            </select>



            <select id = "category" @HideIfCustomerFilter>
                <option value="" selected="selected">@Mvc.ContextHelper.Tradueix("(todas las categorias)", "(totes les categories)", "(all categories)")</option>
                           
                            @For Each oCategory As DTOProductCategory In oCategories

                                Dim match As Boolean
                                If oProductsFilter.Count > 0 Then
                                    match = oProductsFilter.Any(Function(x) x.Equals(oCategory))
                                End If
                                If match Then
                                    @<option value="@oCategory.Guid.ToString" selected='selected'>@oCategory.Nom</option>
                                Else
                                    @<option value="@oCategory.Guid.ToString">@oCategory.Nom</option>
                                End If

                            Next
                    
            </select>


             <select id="country" @HideIfCustomerFilter hidden="hidden">
                <option value="" selected="selected">@Mvc.ContextHelper.Tradueix("(todos los paises)", "(tots els paisos)", "(all countries)")</option>
                @For Each oCountry As DTOCountry In oCountries
                    Dim match As Boolean = oAreasFilter.Count > 0 AndAlso oAreasFilter.Any(Function(x) x.Equals(oCountry))
                    If match Then
                        @<option value="@oCountry.Guid.ToString" selected='selected'>@DTOCountry.NomTraduit(oCountry, Mvc.ContextHelper.lang())</option>
                    Else
                        @<option value="@oCountry.Guid.ToString">@DTOCountry.NomTraduit(oCountry, Mvc.ContextHelper.lang())</option>
                    End If
                Next
            </select>


            <select id="zona" @HideIfCustomerFilter hidden="hidden">
                <option value="" selected="selected">@Mvc.ContextHelper.Tradueix("(todas las zonas)", "(totes les zones)", "(all zones)")</option>
                    @If oAreasFilter.Count > 0 AndAlso TypeOf oAreasFilter.First Is DTOCountry Then
                        Dim oCountry As DTOCountry = oAreasFilter.First
                        @For Each oZona As DTOZona In oCountry.Zonas
                            Dim match As Boolean = oAreasFilter.First.Equals(oZona)
                            If match Then
                                @<option value="@oZona.Guid.ToString" selected='selected'>@oZona.Nom)</option>
                            Else
                                @<option value="@oZona.Guid.ToString">@oZona.Nom)</option>
                            End If
                        Next
                    End If
            </select>

            <select id="location" @HideIfCustomerFilter hidden="hidden">
                <option value="" selected="selected">@Mvc.ContextHelper.Tradueix("(todas las poblaciones)", "(totes les poblacions)", "(all locations)")</option>
                @If oAreasFilter.Count > 0 AndAlso TypeOf oAreasFilter.First Is DTOZona Then

                    Dim oZona As DTOZona = oAreasFilter.First
                    If oZona IsNot Nothing Then

                        For Each oLocation As DTOLocation In oZona.Locations
                            Dim match As Boolean = oAreasFilter.First.Equals(oLocation)
                            If match Then
                                @<option value="@oLocation.Guid.ToString" selected='selected'>@oLocation.Nom)</option>
                            Else
                                @<option value="@oLocation.Guid.ToString">@oLocation.Nom)</option>
                            End If
                        Next
                    End If
                End If
            </select>

            <select id="contact" hidden="hidden" @HideIfCustomerFilter hidden="hidden">
                <option value="" selected="selected">@Mvc.ContextHelper.Tradueix("(todos los clientes)", "(tots els clients)", "(all customers)")</option>
                @If oAreasFilter.Count > 0 AndAlso TypeOf oAreasFilter.First Is DTOLocation Then
                    Dim oLocation As DTOLocation = oAreasFilter.First
                    If oLocation IsNot Nothing Then
                        @For Each oContact As DTOContact In DirectCast(oAreasFilter.First, DTOLocation).Contacts
                            Dim match As Boolean = oAreasFilter.First.Equals(oContact)
                            If match Then
                                @<option value="@oContact.Guid.ToString" selected='selected'>@oContact.Nom)</option>
                            Else
                                @<option value="@oContact.Guid.ToString">@oContact.Nom)</option>
                            End If
                        Next
                    End If
                End If
            </select>

    </div>


    <div id = "divItems" >
        @Html.Partial("Sellout_", Model)
    </div>

    <input type="hidden" id="HiddenCustomerGuid" value="@CustomerGuid" />
    <input type="hidden" id="HiddenGroupbyholding" value='@IIf(Model.GroupByHolding, "true", "false")' />
</div>


@Section Scripts
    <script src="~/Media/js/EciSalesReport.js"></script>
End Section

@section Styles
                                                                                                    <style>
                                                                                                        #pagewrapper {
                                                                                                            display: block;
                                                                                                            white-space: nowrap;
                                                                                                            text-align: left;
                                                                                                        }

                                                                                                        h2.pageTitle {
                                                                                                            font-size: 1em;
                                                                                                            margin-top: 0 0 15px 0;
                                                                                                            padding-top: 0;
                                                                                                            color: darkgray;
                                                                                                        }

                                                                                                        .Download {
                                                                                                            float: right;
                                                                                                            margin-right: 0px;
                                                                                                            margin-left: 20px;
                                                                                                        }

                                                                                                        p.miscellanea {
                                                                                                            font-size: 0.7em;
                                                                                                        }

                                                                                                        .Selloutrow {
                                                                                                            position: relative;
                                                                                                            display: block;
                                                                                                            text-align: left;
                                                                                                            left: 0px;
                                                                                                            height: 20px;
                                                                                                            vertical-align: top;
                                                                                                            font-size: 0.8em;
                                                                                                        }

                                                                                                        .rowlevel0 {
                                                                                                            font-weight: bold;
                                                                                                        }

                                                                                                        .rowlevel1 {
                                                                                                            background-color: #cdfbf8;
                                                                                                        }

                                                                                                        .SelloutConcept {
                                                                                                            display: inline-block;
                                                                                                            width: 280px;
                                                                                                            padding-left: 5px;
                                                                                                        }

                                                                                                        .SelloutValue {
                                                                                                            display: inline-block;
                                                                                                            /*width: 50px;*/
                                                                                                            width: 65px;
                                                                                                            text-align: right;
                                                                                                            border: 1px solid #d5d5d5;
                                                                                                        }

                                                                                                        .divselectors {
                                                                                                            height: 2em;
                                                                                                            margin: 15px 0 15px 0;
                                                                                                        }
                                                                                                    </style>
                                                                                                End Section
