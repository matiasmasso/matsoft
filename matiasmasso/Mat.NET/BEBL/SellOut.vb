Imports Newtonsoft.Json.Linq

Public Class SellOut
    Shared Function Factory(oUser As DTOUser,
                            Optional oYearMonthTo As DTOYearMonth = Nothing,
                            Optional oConceptType As DTOSellOut.ConceptTypes = DTOSellOut.ConceptTypes.product,
                            Optional oFormat As DTOSellOut.Formats = DTOSellOut.Formats.amounts
                            ) As DTOSellOut

        If oYearMonthTo Is Nothing Then oYearMonthTo = New DTOYearMonth(DTO.GlobalVariables.Today().Year, 12)
        Dim retval As New DTOSellOut(DTOSellOut.ConceptTypes.cnaps, oUser.Lang)
        With retval
            .User = oUser
            .YearMonths = oYearMonthTo.Last12Yearmonths()
            .ConceptType = oConceptType
            .Format = oFormat
            .Filters = SellOutFilters.All(.User.Lang)
        End With

        Select Case oUser.Rol.id
            Case DTORol.Ids.manufacturer
                Dim oProveidor As DTOProveidor = User.GetProveidor(oUser)
                If oProveidor Is Nothing Then
                    retval = Nothing
                Else
                    AddFilter(retval, DTOSellOut.Filter.Cods.Provider, {oProveidor.ToGuidNom()})
                End If
            Case DTORol.Ids.cliLite, DTORol.Ids.cliFull
                Dim oCustomers = User.GetCustomers(oUser)
                If oCustomers.Count = 0 Then
                    retval = Nothing
                Else
                    Dim oGuidNoms As New List(Of DTOGuidNom)
                    For Each item In oCustomers
                        oGuidNoms.Add(item.ToGuidNom())
                    Next
                    AddFilter(retval, DTOSellOut.Filter.Cods.Customer, oGuidNoms)
                End If
            Case DTORol.Ids.comercial, DTORol.Ids.rep
                Dim oRep As DTORep = User.GetRep(oUser)
                If oRep Is Nothing Then
                    retval = Nothing
                Else
                    AddFilter(retval, DTOSellOut.Filter.Cods.Rep, {oRep.ToGuidNom()})
                End If
            Case DTORol.Ids.salesManager
                retval.ConceptType = DTOSellOut.ConceptTypes.repsGeo
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.operadora
            Case Else
                retval = Nothing
        End Select
        Return retval
    End Function

    Shared Sub AddFilter(oSellOut As DTOSellOut, oCod As DTOSellOut.Filter.Cods, oValues As IEnumerable(Of DTOGuidNom))
        If oSellOut.Filters IsNot Nothing Then
            Dim oFilter = oSellOut.Filters.FirstOrDefault(Function(x) x.Cod = oCod)
            If oFilter IsNot Nothing Then
                oFilter.Values.AddRange(oValues)
            End If
        End If
    End Sub


    Shared Sub ClearFilter(oSellOut As DTOSellOut, oCod As DTOSellOut.Filter.Cods)
        If oSellOut.Filters IsNot Nothing Then
            Dim oFilter = oSellOut.Filters.FirstOrDefault(Function(x) x.Cod = oCod)
            If oFilter IsNot Nothing Then
                oFilter.Values = New List(Of DTOGuidNom)
            End If
        End If
    End Sub



    Shared Function Factory(oUser As DTOUser, year As Integer, conceptType As DTOSellOut.ConceptTypes, format As DTOSellOut.ConceptTypes, brand As String, category As String, channel As String, country As String, zona As String, location As String, contact As String, groupbyholding As Boolean) As DTOSellOut
        'de controller via jquery
        Dim oYearMonthTo As New DTOYearMonth(year, 12)
        Dim retval As DTOSellOut = SellOut.Factory(oUser, oYearMonthTo, conceptType, format)

        With retval

            If MatHelperStd.GuidHelper.IsGuid(category) Then
                Dim oProduct = ProductCategoryLoader.Find(New Guid(category))
                Dim oFilter = Filter(retval, DTOSellOut.Filter.Cods.Product)
                Dim value = DTOProductCategory.ToGuidNom(oProduct)
                oFilter.Values.Add(value)
            ElseIf MatHelperStd.GuidHelper.IsGuid(brand) Then
                Dim oProduct = ProductBrandLoader.Find(New Guid(brand))
                Dim oFilter = Filter(retval, DTOSellOut.Filter.Cods.Product)
                Dim value = DTOProductBrand.ToGuidNom(oProduct)
                oFilter.Values.Add(value)
            End If

            If MatHelperStd.GuidHelper.IsGuid(channel) Then
                Dim oChannel = DistributionChannelLoader.Find(New Guid(channel))
                Dim oFilter = Filter(retval, DTOSellOut.Filter.Cods.Channel)
                Dim value = New DTOGuidNom(oChannel.Guid, oChannel.LangText.Tradueix(oUser.Lang))
                oFilter.Values.Add(value)
            End If

            If MatHelperStd.GuidHelper.IsGuid(contact) Then
                Dim value = CustomerLoader.Find(New Guid(contact))
                Dim oFilter = Filter(retval, DTOSellOut.Filter.Cods.Customer)
                oFilter.Values.Add(value)
                .GroupByHolding = groupbyholding
            ElseIf GuidHelper.IsGuid(location) Then
                Dim value = LocationLoader.Find(New Guid(location))
                Dim oFilter = Filter(retval, DTOSellOut.Filter.Cods.Atlas)
                oFilter.Values.Add(value)
            ElseIf GuidHelper.IsGuid(zona) Then
                Dim value = ZonaLoader.Find(New Guid(zona))
                Dim oFilter = Filter(retval, DTOSellOut.Filter.Cods.Atlas)
                oFilter.Values.Add(value)
            ElseIf GuidHelper.IsGuid(country) Then
                Dim value = CountryLoader.Find(New Guid(country))
                Dim oFilter = Filter(retval, DTOSellOut.Filter.Cods.Atlas)
                oFilter.Values.Add(value)
            End If

            If .User.Rol.id = DTORol.Ids.salesManager Then
                .ConceptType = DTOSellOut.ConceptTypes.repsGeo
            End If
        End With

        Return retval
    End Function

    Shared Sub SetCcx(ByRef oSellOut As DTOSellOut, oCcx As DTOCustomer)
        ClearFilter(oSellOut, DTOSellOut.Filter.Cods.Customer)
        AddFilter(oSellOut, DTOSellOut.Filter.Cods.Customer, {oCcx.ToGuidNom()})
        oSellOut.GroupByHolding = True
    End Sub


    Shared Sub Load(ByRef oSellout As DTOSellOut)
        SelloutLoader.Load(oSellout)
    End Sub

    Shared Function Years(oSellout As DTOSellOut) As List(Of Integer)
        Dim retval As List(Of Integer) = SelloutLoader.Years(oSellout)
        Return retval
    End Function


    Shared Function Reps(oSellout As DTOSellOut) As List(Of DTORep)
        Dim retval As List(Of DTORep) = SelloutLoader.Reps(oSellout)
        Return retval
    End Function

    Shared Function Proveidors(oSellOut As DTOSellOut) As List(Of DTOProveidor)
        Dim retval As List(Of DTOProveidor) = SelloutLoader.Proveidors(oSellOut)
        Return retval
    End Function

    Shared Sub ExpandToLevel(ByRef oSellout As DTOSellOut, iLevel As Integer)
        oSellout.ExpandToLevel = iLevel
        For Each item As DTOSelloutItem In oSellout.Items
            item.IsExpanded = (item.HasChildren And item.Level < iLevel)
        Next
    End Sub

    Shared Function Countries(ByVal oSellout As DTOSellOut) As List(Of DTOCountry)
        Dim retval As List(Of DTOCountry) = SelloutLoader.Countries(oSellout)
        Return retval
    End Function

    Shared Function Channels(ByVal oSellOut As DTOSellOut) As List(Of DTODistributionChannel)
        Dim retval As List(Of DTODistributionChannel) = SelloutLoader.DistributionChannels(oSellOut)
        Return retval
    End Function

    Shared Function Brands(ByVal oSellOut As DTOSellOut) As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = SelloutLoader.Brands(oSellOut)
        Return retval
    End Function

    Shared Function Customers(ByVal oSellOut As DTOSellOut) As List(Of DTOCustomer)
        oSellOut.ConceptType = DTOSellOut.ConceptTypes.channels
        SelloutLoader.Load(oSellOut)

        Dim retval As New List(Of DTOCustomer)
        For Each oChannelItem As DTOSelloutItem In oSellOut.Items
            For Each oCountryItem As DTOSelloutItem In oChannelItem.Items
                For Each oZonaItem As DTOSelloutItem In oCountryItem.Items
                    For Each oLocationItem As DTOSelloutItem In oZonaItem.Items
                        retval.AddRange(oLocationItem.Items)
                    Next
                Next
            Next
        Next
        Return retval
    End Function

    Shared Function FlattenItems(oSellOut As DTOSellOut) As List(Of DTOSelloutItem)
        Dim retval As List(Of DTOSelloutItem) = FlattenItems(oSellOut.Items)
        Return retval
    End Function

    Shared Function FlattenItems(items As List(Of DTOSelloutItem)) As List(Of DTOSelloutItem)
        Dim retval As New List(Of DTOSelloutItem)
        For Each item In items
            retval.Add(item)
            If item.Items.Count > 0 Then
                item.HasChildren = True
                retval.AddRange(FlattenItems(item.Items))
            End If
        Next
        Return retval
    End Function


    Shared Function RawDataLast12MonthsCsvUrl(Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Dox(AbsoluteUrl, DTODocFile.Cods.rawdatalast12monthscsv)
        Return retval
    End Function

    Shared Function RawExcel(ByRef oSellout As DTOSellOut) As MatHelper.Excel.Sheet
        Return SelloutLoader.RawExcel(oSellout)
    End Function

    Shared Function RawData(oUser As DTOUser, year As Integer) As DTO.Models.SellOutModel
        Return SelloutLoader.RawData(oUser, year)
    End Function

    Shared Function RawDataLast12Months(oProveidor As DTOProveidor) As List(Of DTOCustomerProduct)
        Return SelloutLoader.RawDataLast12Months(oProveidor)
    End Function

    Shared Function ExcelUrl(oSellout As DTOSellOut, Optional AbsoluteUrl As Boolean = False) As String
        Dim oParameters As Dictionary(Of String, String) = Parameters(oSellout)
        Dim retval As String = UrlHelper.Doc(DTODocFile.Cods.selloutexcel, oParameters, AbsoluteUrl)
        Return retval
    End Function

    Shared Function Filter(oSellout As DTOSellOut, oCod As DTOSellOut.Filter.Cods) As DTOSellOut.Filter
        Dim retval As DTOSellOut.Filter = Nothing
        If oSellout.Filters IsNot Nothing Then
            retval = oSellout.Filters.FirstOrDefault(Function(x) x.Cod = oCod)
        End If
        Return retval
    End Function

    Shared Function FilterValues(oSellout As DTOSellOut, oCod As DTOSellOut.Filter.Cods) As List(Of DTOGuidNom)
        Dim retval As New List(Of DTOGuidNom)
        Dim oFilter = Filter(oSellout, oCod)
        If oFilter IsNot Nothing Then
            retval = oFilter.Values
        End If
        Return retval
    End Function

    Shared Sub AddFilterValues(oSellout As DTOSellOut, oCod As DTOSellOut.Filter.Cods, values() As DTOGuidNom)
        Dim oFilter = Filter(oSellout, oCod)
        If oFilter IsNot Nothing Then
            oFilter.Values.AddRange(values)
        End If
    End Sub

    Shared Function Filtro(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        If oSellout.YearMonths IsNot Nothing AndAlso oSellout.YearMonths.Count > 0 Then
            sb.AppendFormat("{0}:{1} ", oSellout.Lang.Tradueix("Año", "Any", "Year"), oSellout.YearMonths.Last.Year)
        End If

        Dim oFilter = Filter(oSellout, DTOSellOut.Filter.Cods.Provider)
        If oFilter.Values.Count > 0 Then
            sb.AppendFormat(oSellout.Lang.Tradueix("Proveedor", "Proveïdor", "Supplier"))
            For Each value In oFilter.Values
                sb.Append(IIf(value.Equals(oFilter.Values.First), ": ", ", "))
                sb.AppendFormat(value.Nom)
                'ContactLoader.Load(value)
                'sb.AppendFormat(value.FullNom)
            Next
        End If

        oFilter = Filter(oSellout, DTOSellOut.Filter.Cods.Customer)
        If oFilter.Values.Count > 0 Then
            sb.AppendFormat(oSellout.Lang.Tradueix("Cliente", "Client", "Customer"))
            For Each value In oFilter.Values
                sb.Append(IIf(value.Equals(oFilter.Values.First), ": ", ", "))
                sb.AppendFormat(value.Nom)
                'ContactLoader.Load(value)
                'sb.AppendFormat(value.FullNom)
            Next
        End If

        oFilter = Filter(oSellout, DTOSellOut.Filter.Cods.Product)
        If oFilter.Values.Count > 0 Then
            sb.AppendFormat(oSellout.Lang.Tradueix("Producto", "Producte", "Product"))
            For Each value As DTOGuidNom In oFilter.Values
                sb.Append(IIf(value.Equals(oFilter.Values.First), ": ", ", "))
                sb.AppendFormat(value.Nom)
                'Product.Load(value)
                'sb.AppendFormat(Product.Nom(value))
            Next
        End If

        oFilter = Filter(oSellout, DTOSellOut.Filter.Cods.Channel)
        If oFilter.Values.Count > 0 Then
            sb.AppendFormat(oSellout.Lang.Tradueix("Canal", "Canal", "Channel"))
            For Each value As DTOGuidNom In oFilter.Values
                sb.Append(IIf(value.Equals(oFilter.Values.First), ": ", ", "))
                sb.AppendFormat(value.Nom)
                'sb.AppendFormat(value.LangText.Tradueix(oSellout.Lang))
            Next
        End If

        oFilter = Filter(oSellout, DTOSellOut.Filter.Cods.Atlas)
        If oFilter.Values.Count > 0 Then
            sb.AppendFormat(oSellout.Lang.Tradueix("Areas", "Areas", "Areas"))
            For Each value In oFilter.Values
                sb.Append(IIf(value.Equals(oFilter.Values.First), ": ", ", "))
                sb.AppendFormat(value.Nom)
            Next
        End If

        oFilter = Filter(oSellout, DTOSellOut.Filter.Cods.Rep)
        If oFilter.Values.Count > 0 Then
            sb.AppendFormat(oSellout.Lang.Tradueix("Reps", "Reps", "Reps"))
            For Each value In oFilter.Values
                sb.Append(IIf(value.Equals(oFilter.Values.First), ": ", ", "))
                sb.AppendFormat(value.Nom)
            Next
        End If

        oFilter = Filter(oSellout, DTOSellOut.Filter.Cods.CNap)
        If oFilter.Values.Count > 0 Then
            sb.AppendFormat(oSellout.Lang.Tradueix("Cnap", "Cnap", "Cnap"))
            For Each value In oFilter.Values
                sb.Append(IIf(value.Equals(oFilter.Values.First), ": ", ", "))
                sb.AppendFormat(value.Nom)
                'sb.AppendFormat(value.FullNom(oSellout.Lang))
            Next
        End If

        If oSellout.GroupByHolding Then
            sb.AppendFormat("{0}:{1} ", oSellout.Lang.Tradueix("Consolidado", "Consolidat", "Consolidated"), oSellout.Lang.Tradueix("Si", "Si", "True"))
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Private Shared Function AddFilterParameter(oDictionary As Dictionary(Of String, String), oSellOut As DTOSellOut, oCod As DTOSellOut.Filter.Cods, sParamName As String) As Boolean
        Dim retval As Boolean
        Dim oFilter = Filter(oSellOut, oCod)
        If oFilter IsNot Nothing AndAlso oFilter.Values.Count > 0 Then
            oDictionary.Add(sParamName, oFilter.Values.First.Guid.ToString())
            retval = True
        End If
        Return retval
    End Function

    Shared Function Parameters(oSellout As DTOSellOut) As Dictionary(Of String, String)
        Dim retval As New Dictionary(Of String, String)
        With retval
            AddFilterParameter(retval, oSellout, DTOSellOut.Filter.Cods.Provider, "Proveidor")
            AddFilterParameter(retval, oSellout, DTOSellOut.Filter.Cods.Customer, "Customer")
            AddFilterParameter(retval, oSellout, DTOSellOut.Filter.Cods.Product, "Product")
            AddFilterParameter(retval, oSellout, DTOSellOut.Filter.Cods.Channel, "Channel")
            AddFilterParameter(retval, oSellout, DTOSellOut.Filter.Cods.Atlas, "Area")
            AddFilterParameter(retval, oSellout, DTOSellOut.Filter.Cods.Rep, "Rep")
            If oSellout.YearMonths IsNot Nothing AndAlso oSellout.YearMonths.Count > 0 Then
                .Add("YearMonthTo", oSellout.YearMonths.Last.Tag)
            End If
            If oSellout.Lang.Tag <> "ESP" Then
                .Add("Lang", oSellout.Lang.Tag)
            End If
            If oSellout.Format <> DTOSellOut.Formats.units Then
                .Add("Format", CInt(oSellout.Format))
            End If
            If oSellout.ConceptType <> DTOSellOut.ConceptTypes.product Then
                .Add("ConceptType", CInt(oSellout.ConceptType))
            End If
            If oSellout.GroupByHolding Then
                .Add("GroupByHolding", "true")
            End If
        End With
        Return retval
    End Function

    Shared Function FromParameters(oParameters As Dictionary(Of String, String)) As DTOSellOut
        Dim oYearMonthTo As New DTOYearMonth(DTO.GlobalVariables.Today().Year, 12)
        If oParameters.ContainsKey("YearMonthTo") Then
            oYearMonthTo = DTOYearMonth.FromTag(oParameters("YearMonthTo"))
        End If

        Dim oFormat As DTOSellOut.Formats = DTOSellOut.Formats.units
        If oParameters.ContainsKey("Format") Then
            oFormat = oParameters("Format")
        End If

        Dim oConceptType As DTOSellOut.ConceptTypes = DTOSellOut.ConceptTypes.product
        If oParameters.ContainsKey("ConceptType") Then
            oConceptType = oParameters("ConceptType")
        End If

        Dim oLang As DTOLang = DTOLang.ESP
        If oParameters.ContainsKey("Lang") Then
            oLang = DTOLang.Factory(oParameters("Lang"))
        End If

        Dim BlGroupByHolding As Boolean
        If oParameters.ContainsKey("GroupByHolding") Then
            BlGroupByHolding = oParameters("GroupByHolding") = "true"
        End If

        Dim oUser As DTOUser = Nothing
        If oParameters.ContainsKey("User") Then
            oUser = UserLoader.Find(New Guid(oParameters("User")))
        End If

        Dim retval As DTOSellOut = SellOut.Factory(oUser, oYearMonthTo, oConceptType, oFormat)
        retval.GroupByHolding = BlGroupByHolding

        If oParameters.ContainsKey("Area") Then
            Dim value As DTOBaseGuid = AreaLoader.Find(New Guid(oParameters("Area")))
            AddFilterValues(retval, DTOSellOut.Filter.Cods.Atlas, {value})
        End If

        Dim oProducts As New List(Of DTOProduct)
        If oParameters.ContainsKey("Product") Then
            Dim value As DTOBaseGuid = ProductLoader.Find(New Guid(oParameters("Product")))
            AddFilterValues(retval, DTOSellOut.Filter.Cods.Product, {value})
        End If

        Dim oProveidors As New List(Of DTOProveidor)
        If oParameters.ContainsKey("Proveidor") Then
            Dim oProviderGuid = New Guid(oParameters("Proveidor"))
            If Not SellOut.FilterValues(retval, DTOSellOut.Filter.Cods.Provider).Any(Function(x) x.Guid.Equals(oProviderGuid)) Then
                Dim value As DTOBaseGuid = ProveidorLoader.Find(oProviderGuid)
                AddFilterValues(retval, DTOSellOut.Filter.Cods.Provider, {value})
            End If
        End If

        Dim oCustomers As New List(Of DTOCustomer)
        If oParameters.ContainsKey("Customer") Then
            Dim value As DTOBaseGuid = CustomerLoader.Find(New Guid(oParameters("Customer")))
            AddFilterValues(retval, DTOSellOut.Filter.Cods.Customer, {value})
        End If

        Dim oReps As New List(Of DTORep)
        If oParameters.ContainsKey("Rep") Then
            Dim value As DTOBaseGuid = RepLoader.Find(New Guid(oParameters("Rep")))
            AddFilterValues(retval, DTOSellOut.Filter.Cods.Rep, {value})
        End If

        Dim oChannels As New List(Of DTODistributionChannel)
        If oParameters.ContainsKey("Channel") Then
            Dim value As DTOBaseGuid = DistributionChannelLoader.Find(New Guid(oParameters("Channel")))
            AddFilterValues(retval, DTOSellOut.Filter.Cods.Channel, {value})
        End If

        Return retval
    End Function


    Shared Function Excel(oSellOut As DTOSellOut) As MatHelper.Excel.Sheet
        Dim sFilename As String = String.Format("M+O Sellout {0:yyyy.MM.dd}", DTO.GlobalVariables.Today())
        Dim sSheetname As String = SellOut.Filtro(oSellOut)
        Dim retval As New MatHelper.Excel.Sheet(sSheetname, sFilename)

        Dim items As List(Of DTOSelloutItem) = SellOut.FlattenItems(oSellOut)
        Dim maxLevel As Integer = items.Max(Function(x) x.Level)
        Dim numberFormat As MatHelper.Excel.Cell.NumberFormats = IIf(oSellOut.Format = DTOSellOut.Formats.amounts, MatHelper.Excel.Cell.NumberFormats.Euro, MatHelper.Excel.Cell.NumberFormats.Integer)

        Dim lastItem = items.Where(Function(x) x.Level = maxLevel).First
        Dim includeSkuRef As Boolean = TypeOf lastItem.Tag Is DTOProductSku


        With retval
            For j As Integer = 0 To maxLevel - 1
                .AddColumn("", MatHelper.Excel.Cell.NumberFormats.W50)
            Next
            If includeSkuRef Then
                .AddColumn("Ref", MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn(oSellOut.Lang.Tradueix("Producto", "Producte", "Product"), MatHelper.Excel.Cell.NumberFormats.W50)
            Else
                .AddColumn("", MatHelper.Excel.Cell.NumberFormats.W50)
            End If

            .AddColumn("Total", numberFormat)
            For m As Integer = 1 To 12
                .AddColumn(oSellOut.Lang.MesAbr(m), numberFormat)
            Next
        End With

        Dim levels(maxLevel + 1) As String
        For Each oItem As DTOSelloutItem In items
            If oItem.Level = maxLevel Then
                Dim oRow = retval.AddRow
                For j As Integer = 0 To maxLevel - 1
                    oRow.AddCell(levels(j))
                Next

                If includeSkuRef Then
                    Dim oSku As DTOProductSku = oItem.Tag
                    oRow.AddCell(oSku.RefProveidor, oSku.GetUrl(oSellOut.Lang, , True))
                End If

                oRow.AddCell(oItem.Concept)
                oRow.AddFormula("SUM(RC[1]:RC[12])")
                For m As Integer = 0 To 11
                    oRow.AddCell(oItem.Values(m))
                Next
            Else
                levels(oItem.Level) = oItem.Concept
            End If
        Next
        Return retval
    End Function


    Shared Function ExcelRepsGeo(oSellOut As DTOSellOut, exs As List(Of Exception)) As MatHelper.Excel.Sheet
        With oSellOut
            .ConceptType = DTOSellOut.ConceptTypes.repsGeo
            .Format = DTOSellOut.Formats.amounts
        End With

        SellOut.Load(oSellOut)

        Dim retval As New MatHelper.Excel.Sheet("Distribució x Rep")
        With retval
            .AddColumn("Rep", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Pais", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Zona", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Población", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Cliente", MatHelper.Excel.Cell.NumberFormats.PlainText)

            .AddColumn("Total", MatHelper.Excel.Cell.NumberFormats.Euro)
            For Each item In oSellOut.YearMonths
                .AddColumn(item.Formatted(oSellOut.Lang), MatHelper.Excel.Cell.NumberFormats.Euro)
            Next
            .DisplayTotals = True
        End With

        Dim iRowCount As Integer
        For Each itemRep In oSellOut.Items
            For Each itemCountry In itemRep.Items
                For Each itemZona In itemCountry.Items
                    For Each itemLocation In itemZona.Items
                        For Each itemCustomer In itemLocation.Items
                            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
                            oRow.AddCell(itemRep.Concept)
                            oRow.AddCell(itemCountry.Concept)
                            oRow.AddCell(itemZona.Concept)
                            oRow.AddCell(itemLocation.Concept)
                            oRow.AddCell(itemCustomer.Concept)
                            oRow.AddFormula("SUM(RC[+1]:Rc[+12])")
                            For Each value As Decimal In itemCustomer.Values
                                oRow.AddCell(value)
                            Next
                            iRowCount += 1
                        Next
                    Next
                Next
            Next
        Next

        Return retval
    End Function

    Shared Function ExcelRepsProduct(oSellOut As DTOSellOut, exs As List(Of Exception)) As MatHelper.Excel.Sheet
        With oSellOut
            .ConceptType = DTOSellOut.ConceptTypes.repsProduct
            .Format = DTOSellOut.Formats.amounts
        End With

        SellOut.Load(oSellOut)

        Dim retval As New MatHelper.Excel.Sheet("Producte x Rep")
        With retval
            .AddColumn("Rep", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Marca", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Categoría", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Ean", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Referencia", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Descripción", MatHelper.Excel.Cell.NumberFormats.PlainText)

            .AddColumn("Total", MatHelper.Excel.Cell.NumberFormats.Euro)
            For Each item In oSellOut.YearMonths
                .AddColumn(item.Formatted(oSellOut.Lang), MatHelper.Excel.Cell.NumberFormats.Euro)
            Next
            .DisplayTotals = True
        End With

        Dim iRowCount As Integer
        For Each itemRep In oSellOut.Items
            For Each itemBrand In itemRep.Items
                For Each itemCategory In itemBrand.Items
                    For Each itemSku In itemCategory.Items
                        Dim oRow As MatHelper.Excel.Row = retval.AddRow()
                        oRow.AddCell(itemRep.Concept)
                        oRow.AddCell(itemBrand.Concept)
                        oRow.AddCell(itemCategory.Concept)

                        Dim oSku As DTOProductSku = itemSku.Tag
                        oRow.AddCell(DTOProductSku.Ean(oSku))
                        oRow.AddCell(oSku.RefProveidor)
                        oRow.AddCell(oSku.NomProveidor)

                        oRow.AddFormula("SUM(RC[+1]:Rc[+12])")
                        For Each value As Decimal In itemSku.Values
                            oRow.AddCell(value)
                        Next
                        iRowCount += 1
                    Next
                Next
            Next
        Next

        Return retval
    End Function
    Shared Function ExcelCnaps(oSellOut As DTOSellOut, exs As List(Of Exception)) As MatHelper.Excel.Sheet
        With oSellOut
            .ConceptType = DTOSellOut.ConceptTypes.cnaps
            .Format = DTOSellOut.Formats.amounts
        End With

        SellOut.Load(oSellOut)
        Dim items As List(Of DTOSelloutItem) = FlattenItems(oSellOut)
        Dim retval As New MatHelper.Excel.Sheet("Cnap")
        With retval
            .AddColumn("Rep", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Marca", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Categoría", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Ean", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Referencia", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Descripción", MatHelper.Excel.Cell.NumberFormats.PlainText)

            .AddColumn("Total", MatHelper.Excel.Cell.NumberFormats.Euro)
            For Each item In oSellOut.YearMonths
                .AddColumn(item.Formatted(oSellOut.Lang), MatHelper.Excel.Cell.NumberFormats.Euro)
            Next
            .DisplayTotals = True
        End With

        For Each item In items
            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            oRow.AddCell(item.Concept)

            oRow.AddFormula("SUM(RC[+1]:Rc[+12])")
            For Each value As Decimal In item.Values
                oRow.AddCell(value)
            Next
        Next

        Return retval
    End Function




    Shared Function ExcelChannel(oSellOut As DTOSellOut, exs As List(Of Exception)) As MatHelper.Excel.Sheet
        With oSellOut
            .ConceptType = DTOSellOut.ConceptTypes.channels
            .Format = DTOSellOut.Formats.amounts
        End With

        SellOut.Load(oSellOut)

        Dim retval As New MatHelper.Excel.Sheet("Canales")
        With retval
            .AddColumn("Canal", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Pais", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Zona", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Población", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Cliente", MatHelper.Excel.Cell.NumberFormats.PlainText)

            .AddColumn("Total", MatHelper.Excel.Cell.NumberFormats.Euro)
            For Each item In oSellOut.YearMonths
                .AddColumn(item.Formatted(oSellOut.Lang), MatHelper.Excel.Cell.NumberFormats.Euro)
            Next
            .DisplayTotals = True
        End With

        Dim iRowCount As Integer
        For Each itemChannel In oSellOut.Items
            For Each itemCountry In itemChannel.Items
                For Each itemZona In itemCountry.Items
                    For Each itemLocation In itemZona.Items
                        For Each itemCustomer In itemLocation.Items
                            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
                            oRow.AddCell(itemChannel.Concept)
                            oRow.AddCell(itemCountry.Concept)
                            oRow.AddCell(itemZona.Concept)
                            oRow.AddCell(itemLocation.Concept)
                            oRow.AddCell(itemCustomer.Concept)
                            oRow.AddFormula("SUM(RC[+1]:Rc[+12])")
                            For Each value As Decimal In itemCustomer.Values
                                oRow.AddCell(value)
                            Next
                            iRowCount += 1
                        Next
                    Next
                Next
            Next
        Next

        Return retval
    End Function

    Shared Function ExcelProducts(ByVal oSellOut As DTOSellOut, oFormat As DTOSellOut.Formats, exs As List(Of Exception)) As MatHelper.Excel.Sheet
        With oSellOut
            .ConceptType = DTOSellOut.ConceptTypes.product
            .Format = oFormat
        End With
        SellOut.Load(oSellOut)

        Dim sCaption As String = IIf(oFormat = DTOSellOut.Formats.units, "Product (units)", "Product (amounts)")
        Dim retval As New MatHelper.Excel.Sheet(sCaption)
        With retval
            .AddColumn("Marca", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Categoría", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Ean", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Referencia", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Descripción", MatHelper.Excel.Cell.NumberFormats.PlainText)

            Dim oNumberFormat As MatHelper.Excel.Cell.NumberFormats = IIf(oFormat = DTOSellOut.Formats.units, MatHelper.Excel.Cell.NumberFormats.Integer, MatHelper.Excel.Cell.NumberFormats.Euro)

            .AddColumn("Total", oNumberFormat)
            For Each item In oSellOut.YearMonths
                .AddColumn(item.Formatted(oSellOut.Lang), oNumberFormat)
            Next
            .DisplayTotals = True
        End With

        Dim oRowTotal As MatHelper.Excel.Row = retval.AddRow()

        Dim iRowCount As Integer
        For Each itemBrand In oSellOut.Items
            For Each itemCategory In itemBrand.Items
                For Each itemSku In itemCategory.Items
                    Dim oRow As MatHelper.Excel.Row = retval.AddRow()
                    oRow.AddCell(itemBrand.Concept)
                    oRow.AddCell(itemCategory.Concept)

                    Dim oSku As DTOProductSku = itemSku.Tag
                    oRow.AddCell(DTOProductSku.Ean(oSku))
                    oRow.AddCell(oSku.RefProveidor)
                    oRow.AddCell(oSku.NomProveidor)

                    oRow.AddFormula("SUM(RC[+1]:Rc[+12])")
                    For Each value As Decimal In itemSku.Values
                        oRow.AddCell(value)
                    Next
                    iRowCount += 1
                Next
            Next
        Next

        Return retval
    End Function

    Shared Function RawDataLast12MonthsCsv(oProveidor As DTOProveidor) As DTOCsv
        Dim DtFchTo As Date = DTO.GlobalVariables.Today()

        Dim retval As New DTOCsv("M+O Sellout.csv")
        Dim oRow As DTOCsvRow = CsvHelper.AddRow(retval)
        CsvHelper.AddCell(oRow, "Country")
        CsvHelper.AddCell(oRow, "Area")
        CsvHelper.AddCell(oRow, "Location")
        CsvHelper.AddCell(oRow, "Customer")
        CsvHelper.AddCell(oRow, "Ref")
        CsvHelper.AddCell(oRow, "Product")
        For i As Integer = 12 To 0 Step -1
            Dim DtFch As Date = DtFchTo.AddMonths(-i)
            CsvHelper.AddCell(oRow, DTOLang.ENG.MesAbr(DtFch.Month))
        Next

        Dim items As List(Of DTOCustomerProduct) = RawDataLast12Months(oProveidor)
        Dim oSku As New DTOProductSku
        For Each item As DTOCustomerProduct In items
            If oSku.UnEquals(item.Sku) Then
                oSku = item.Sku
                oRow = CsvHelper.AddRow(retval)
                CsvHelper.AddCell(oRow, item.Customer.Address.Zip.Location.Zona.Country.LangNom.Eng)
                CsvHelper.AddCell(oRow, item.Customer.Address.Zip.Location.Zona.Nom)
                CsvHelper.AddCell(oRow, item.Customer.Address.Zip.Location.Nom)
                CsvHelper.AddCell(oRow, item.Customer.NomComercialOrDefault())
                CsvHelper.AddCell(oRow, oSku.RefProveidor)
                CsvHelper.AddCell(oRow, oSku.NomProveidor)
                For i = 0 To 12
                    CsvHelper.AddCell(oRow, 0)
                Next
            End If
            oRow.Cells(Column(DtFchTo, item)) = item.Qty
        Next
        Return retval
    End Function

    Private Shared Function Column(DtFchTo As Date, item As DTOCustomerProduct) As Integer
        Dim iMonthsCount As Integer = 13
        Dim iFirstMonthColumn As Integer = 6
        Dim oSecondYearMonth As New DTOYearMonth(DtFchTo.Year, DtFchTo.Month)
        Dim iDiff As Integer = DTOYearMonth.MonthsDiff(item.YearMonth, oSecondYearMonth)
        Dim retval As Integer = iFirstMonthColumn + iMonthsCount - iDiff - 1
        Return retval
    End Function



End Class

Public Class SellOutFilter

    Shared Function Caption(cod As DTOSellOut.Filter.Cods, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case cod
            Case DTOSellOut.Filter.Cods.Product
                retval = "producte"
            Case DTOSellOut.Filter.Cods.Atlas
                retval = "area"
            Case DTOSellOut.Filter.Cods.Channel
                retval = "canal"
            Case DTOSellOut.Filter.Cods.Customer
                retval = "client"
            Case DTOSellOut.Filter.Cods.Provider
                retval = "proveidor"
            Case DTOSellOut.Filter.Cods.Rep
                retval = "rep"
            Case DTOSellOut.Filter.Cods.CNap
                retval = "cnap"
        End Select
        Return retval
    End Function

    Shared Function ValueCaption(oFilter As DTOSellOut.Filter, oValue As DTOBaseGuid, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oFilter.Cod
            Case DTOSellOut.Filter.Cods.Product
                retval = DirectCast(oValue, DTOProduct).FullNom()
            Case DTOSellOut.Filter.Cods.Atlas
                retval = DTOArea.FullNom(oValue, oLang)
            Case DTOSellOut.Filter.Cods.Channel
                retval = DirectCast(oValue, DTODistributionChannel).LangText.Tradueix(oLang)
            Case DTOSellOut.Filter.Cods.Customer
                Dim oCustomer As DTOCustomer = DTOCustomer.FromContact(oValue)
                BEBL.Contact.Load(oCustomer)
                retval = oCustomer.FullNom()
            Case DTOSellOut.Filter.Cods.Provider
                Dim oProveidor As DTOProveidor = DTOProveidor.FromContact(oValue)
                retval = oProveidor.Nom
            Case DTOSellOut.Filter.Cods.Rep
                Dim oRep As DTORep = DTORep.FromContact(oValue)
                retval = oRep.NicknameOrNom()
            Case DTOSellOut.Filter.Cods.CNap
                Dim oCnap As DTOCnap = oValue
                retval = oCnap.FullNom(oLang)
        End Select
        Return retval
    End Function
End Class

Public Class SellOutFilters
    Shared Function All(oLang As DTOLang) As List(Of DTOSellOut.Filter)
        Dim retval As New List(Of DTOSellOut.Filter)
        For Each cod In [Enum].GetValues(GetType(DTOSellOut.Filter.Cods))
            Dim oFilter As New DTOSellOut.Filter(cod)
            retval.Add(oFilter)
        Next
        Return retval
    End Function
End Class
