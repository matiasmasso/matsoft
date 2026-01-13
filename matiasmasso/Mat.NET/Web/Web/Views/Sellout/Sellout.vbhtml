@ModelType DTOSellOut

@Code
    Dim exs As New List(Of Exception)
    ViewBag.Title = "Sell-out"
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)



    DTOSellOut.SetLevelToExpand(Model, 1)
    Model.ExpandToLevel = 0

    Dim HideIfSingleBrand As String = ""
    If Model.Items IsNot Nothing AndAlso Model.Items.Count = 1 Then
        HideIfSingleBrand = "hidden='hidden'"
    End If

    Dim HideIfCustomerFilter As String = ""
    Dim CustomerGuid As String = ""
    If FEB2.SellOut.FilterValues(Model, DTOSellOut.Filter.Cods.Customer).Count > 0 Then
        HideIfCustomerFilter = "hidden='hidden'"
        CustomerGuid = FEB2.SellOut.FilterValues(Model, DTOSellOut.Filter.Cods.Customer).First().Guid.ToString
    End If

    Dim iYears As List(Of Integer) = FEB2.SellOut.YearsSync(exs, Model)
    Dim oProductsFilter = FEB2.SellOut.FilterValues(Model, DTOSellOut.Filter.Cods.Product)
    Dim oChannelsFilter = FEB2.SellOut.FilterValues(Model, DTOSellOut.Filter.Cods.Channel)
    Dim oAreasFilter = FEB2.SellOut.FilterValues(Model, DTOSellOut.Filter.Cods.Atlas)


    Dim oBrands = FEB2.SellOut.BrandsSync(exs, Model)
    Dim oBrandFilter As DTOProductBrand = Nothing
    Dim oCategories As New List(Of DTOProductCategory)
    If oProductsFilter.Count > 0 Then
        Dim oProduct = FEB2.Product.FindSync(exs, oProductsFilter.First.Guid)
        If oProduct.SourceCod = DTOProduct.SourceCods.Brand Then
            oBrandFilter = oProduct
            oCategories = FEB2.ProductCategories.AllSync(exs, oProduct)
        End If
    End If

    Dim oCountries As List(Of DTOCountry) = FEB2.SellOut.CountriesSync(exs, Model)
    Dim oZonas As New List(Of DTOZona)
    If oCountries.Count = 1 Then
        oZonas = oCountries.First().Zonas
    End If

    Dim oChannels = FEB2.SellOut.ChannelsSync(exs, Model)
End Code

<section class="PageTitle ToolStrip">
            <a id="ExcelFullDownload" title='@(Mvc.ContextHelper.Tradueix("Descargar todos los datos del ejercicio", "Descarregar totes les dades de l'exercici", "Download full year data ")) '>
                <img class="Download" src="~/Media/Img/48x48/Excel48.png" />
            </a>

            @Mvc.ContextHelper.Tradueix("Pedidos de clientes", "Comandes de clients", "Sell-out data")
        </section>



        <p class="miscellanea">
            @Html.Raw(Mvc.ContextHelper.Tradueix("Las cifras se refieren a unidades vendidas en el mes en que el que el cliente cursó el pedido, tanto si se le ha servido como si no.<br/>Estas cifras pueden verse afectadas por potenciales devoluciones, en cuyo caso se restan del pedido original.",
                                                     "Les xifres es refereixen a unitas venudes el mes que es va rebre la comanda, tant si es va servir llavors com si no.<br/>Les xifres poden variar en el futur per potencials devolucions, que serien deduides del mes en que van ser demanades.",
                                                     "Figures refer to units ordered by customers, in the month the order was logged, regardless if delivered or not.<br/>Potential returns may affect them in the future as they are deducted from the original order."))
        </p>

        <div class="divselectors">
            <select id="year">
                @For i As Integer = 0 To iYears.Count - 1
                    @<option value="@iYears(i)" @iif(i = 0, "selected='selected'", "")>@Html.Raw(iYears(i))</option>
                Next
            </select>

            <select id="conceptType" @HideIfCustomerFilter>
                @If Model.conceptType = DTOSellOut.ConceptTypes.product Then
                    @<option value="@CInt(DTOSellOut.ConceptTypes.product)" selected='selected'>@Mvc.ContextHelper.Tradueix("productos", "productes", "products", "produtos")</option>
                Else
                    @<option value="@CInt(DTOSellOut.ConceptTypes.product)">@Mvc.ContextHelper.Tradueix("productos", "productes", "products", "produtos")</option>
                End If

                @If Model.conceptType = DTOSellOut.ConceptTypes.geo Then
                    @<option value="@CInt(DTOSellOut.ConceptTypes.geo)" selected='selected'>@Mvc.ContextHelper.Tradueix("destinos", "destinacions", "destinations")</option>
                Else
                    @<option value="@CInt(DTOSellOut.ConceptTypes.geo)">@Mvc.ContextHelper.Tradueix("destinos", "destinacions", "destinations")</option>
                End If

                @If Model.conceptType = DTOSellOut.ConceptTypes.channels Then
                    @<option value="@CInt(DTOSellOut.ConceptTypes.channels)" selected='selected'>@Mvc.ContextHelper.Tradueix("canales", "canals", "channels")</option>
                Else
                    @<option value="@CInt(DTOSellOut.ConceptTypes.channels)">@Mvc.ContextHelper.Tradueix("canales", "canals", "channels")</option>
                End If
            </select>


            <select id="format">
                <option value="@CInt(DTOSellOut.Formats.units)" @IIf(Model.format = DTOSellOut.Formats.units, "selected='selected'", "")> @Mvc.ContextHelper.Tradueix("unidades", "unitats", "units")</option>
                <option value="@CInt(DTOSellOut.Formats.amounts)" @IIf(Model.format = DTOSellOut.Formats.amounts, "selected='selected'", "")>@Mvc.ContextHelper.Tradueix("importes", "imports", "amounts")</option>
            </select>



            <select id="brand" @HideIfSingleBrand @HideIfCustomerFilter>
                <option value="">@Mvc.ContextHelper.Tradueix("(todas las marcas)", "(totes les marques)", "(all brands)")</option>
                @If oBrands IsNot Nothing Then

                    For Each oBrand As DTOProductBrand In oBrands
                        Dim match As Boolean
                        If oProductsFilter.Count > 0 Then
                            match = oProductsFilter.Any(Function(x) x.Equals(oBrand))
                        End If
                        If match Then
                            @<option value="@oBrand.Guid.ToString" selected='selected'>@oBrand.Nom.Tradueix(Mvc.ContextHelper.Lang)</option>
                        Else
                            @<option value="@oBrand.Guid.ToString">@oBrand.Nom.Tradueix(Mvc.ContextHelper.Lang)</option>
                        End If
                    Next
                End If
            </select>



            <select id="category" @HideIfCustomerFilter>
                <option value="" selected="selected">@Mvc.ContextHelper.Tradueix("(todas las categorias)", "(totes les categories)", "(all categories)")</option>

                @For Each oCategory As DTOProductCategory In oCategories

                    Dim match As Boolean
                    If oProductsFilter.Count > 0 Then
                        match = oProductsFilter.Any(Function(x) x.Equals(oCategory))
                    End If
                    If match Then
                        @<option value="@oCategory.Guid.ToString" selected='selected'>@oCategory.Nom.Tradueix(Mvc.ContextHelper.Lang)</option>
                    Else
                        @<option value="@oCategory.Guid.ToString">@oCategory.Nom.Tradueix(Mvc.ContextHelper.Lang)</option>
                    End If

                Next

            </select>


            <select id="channel" @HideIfCustomerFilter>
                <option value="" selected="selected">@Mvc.ContextHelper.Tradueix("(todos los canales)", "(tots els canals)", "(all channels)")</option>
                @For Each oChannel As DTODistributionChannel In oChannels

                    Dim match As Boolean
                    If oChannelsFilter.Count > 0 Then
                        match = oChannelsFilter.Any(Function(x) x.Equals(oChannel))
                    End If

                    If match Then
                        @<option value="@oChannel.Guid.ToString" selected='selected'>@oChannel.langText.Tradueix(Mvc.ContextHelper.lang())</option>
                    Else
                        @<option value="@oChannel.Guid.ToString">@oChannel.langText.Tradueix(Mvc.ContextHelper.lang())</option>
                    End If

                Next
            </select>


            <select id="country" @HideIfCustomerFilter>
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


            <select id="zona" @HideIfCustomerFilter>
                <option value="" selected="selected">@Mvc.ContextHelper.Tradueix("(todas las zonas)", "(totes les zones)", "(all zones)")</option>
                @If oAreasFilter.Count > 0 AndAlso TypeOf oAreasFilter.First Is DTOCountry Then
                    Dim oCountry As DTOCountry = oAreasFilter.First
                    @For Each oZona As DTOZona In oCountry.zonas
                        Dim match As Boolean = oAreasFilter.First.Equals(oZona)
                        If match Then
                            @<option value="@oZona.Guid.ToString" selected='selected'>@oZona.nom)</option>
                        Else
                            @<option value="@oZona.Guid.ToString">@oZona.nom)</option>
                        End If
                    Next
                End If
            </select>

            <select id="location" @HideIfCustomerFilter>
                <option value="" selected="selected">@Mvc.ContextHelper.Tradueix("(todas las poblaciones)", "(totes les poblacions)", "(all locations)")</option>
                @If oAreasFilter.Count > 0 AndAlso TypeOf oAreasFilter.First Is DTOZona Then

                    Dim oZona As DTOZona = oAreasFilter.First
                    If oZona IsNot Nothing Then

                        For Each oLocation As DTOLocation In oZona.locations
                            Dim match As Boolean = oAreasFilter.First.Equals(oLocation)
                            If match Then
                                @<option value="@oLocation.Guid.ToString" selected='selected'>@oLocation.nom)</option>
                            Else
                                @<option value="@oLocation.Guid.ToString">@oLocation.nom)</option>
                            End If
                        Next
                    End If
                End If
            </select>

            <select id="contact" hidden="hidden" @HideIfCustomerFilter>
                <option value="" selected="selected">@Mvc.ContextHelper.Tradueix("(todos los clientes)", "(tots els clients)", "(all customers)")</option>
                @If oAreasFilter.Count > 0 AndAlso TypeOf oAreasFilter.First Is DTOLocation Then
                    Dim oLocation As DTOLocation = oAreasFilter.First
                    If oLocation IsNot Nothing Then
                        @For Each oContact As DTOContact In DirectCast(oAreasFilter.First, DTOLocation).contacts
                            Dim match As Boolean = oAreasFilter.First.Equals(oContact)
                            If match Then
                                @<option value="@oContact.Guid.ToString" selected='selected'>@oContact.nom)</option>
                            Else
                                @<option value="@oContact.Guid.ToString">@oContact.nom)</option>
                            End If
                        Next
                    End If
                End If
            </select>

        </div>


        <div id="divItems">
            @Html.Partial("Sellout_", Model)
        </div>

        <input type="hidden" id="HiddenCustomerGuid" value="@CustomerGuid" />
        <input type="hidden" id="HiddenGroupbyholding" value='@IIf(Model.GroupByHolding, "true", "false")' />



@Section Scripts
    <script src="~/Media/js/SellOut.js"></script>
End Section

@section Styles
    <style>
        .ContentColumn {
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
            cursor: pointer;
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
