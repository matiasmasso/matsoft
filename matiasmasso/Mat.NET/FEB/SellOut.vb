Public Class SellOut

    Shared Async Function Load(exs As List(Of Exception), oSellout As DTOSellOut) As Task(Of DTOSellOut)
        Dim retval = Await Api.Execute(Of DTOSellOut, DTOSellOut)(oSellout, exs, "Sellout")
        Return retval
    End Function

    Shared Async Function Years(exs As List(Of Exception), oSellout As DTOSellOut) As Task(Of List(Of Integer))
        Return Await Api.Execute(Of DTOSellOut, List(Of Integer))(oSellout, exs, "Sellout/years")
    End Function

    Shared Function YearsSync(exs As List(Of Exception), oSellout As DTOSellOut) As List(Of Integer)
        Return Api.ExecuteSync(Of DTOSellOut, List(Of Integer))(oSellout, exs, "Sellout/years")
    End Function

    Shared Async Function Reps(exs As List(Of Exception), oSellout As DTOSellOut) As Task(Of List(Of DTORep))
        Return Await Api.Execute(Of DTOSellOut, List(Of DTORep))(oSellout, exs, "Sellout/reps")
    End Function

    Shared Async Function Proveidors(exs As List(Of Exception), oSellout As DTOSellOut) As Task(Of List(Of DTOProveidor))
        Return Await Api.Execute(Of DTOSellOut, List(Of DTOProveidor))(oSellout, exs, "Sellout/Proveidors")
    End Function

    Shared Async Function Countries(exs As List(Of Exception), oSellout As DTOSellOut) As Task(Of List(Of DTOCountry))
        Return Await Api.Execute(Of DTOSellOut, List(Of DTOCountry))(oSellout, exs, "Sellout/countries")
    End Function

    Shared Function CountriesSync(exs As List(Of Exception), oSellout As DTOSellOut) As List(Of DTOCountry)
        Return Api.ExecuteSync(Of DTOSellOut, List(Of DTOCountry))(oSellout, exs, "Sellout/countries")
    End Function

    Shared Async Function Channels(exs As List(Of Exception), oSellout As DTOSellOut) As Task(Of List(Of DTODistributionChannel))
        Return Await Api.Execute(Of DTOSellOut, List(Of DTODistributionChannel))(oSellout, exs, "Sellout/channels")
    End Function

    Shared Function ChannelsSync(exs As List(Of Exception), oSellout As DTOSellOut) As List(Of DTODistributionChannel)
        Return Api.ExecuteSync(Of DTOSellOut, List(Of DTODistributionChannel))(oSellout, exs, "Sellout/channels")
    End Function

    Shared Function BrandsSync(exs As List(Of Exception), oSellout As DTOSellOut) As List(Of DTOProductBrand)
        Return Api.ExecuteSync(Of DTOSellOut, List(Of DTOProductBrand))(oSellout, exs, "Sellout/brands")
    End Function

    Shared Async Function RawDataLast12Months(exs As List(Of Exception), oProveidor As DTOProveidor) As Task(Of List(Of DTOCustomerProduct))
        Return Await Api.Fetch(Of List(Of DTOCustomerProduct))(exs, "Sellout/RawDataLast12Months", oProveidor.Guid.ToString())
    End Function

    Shared Async Function Factory(exs As List(Of Exception),
                                  oUser As DTOUser,
                        Optional oYearMonthTo As DTOYearMonth = Nothing,
                        Optional oConceptType As DTOSellOut.ConceptTypes = DTOSellOut.ConceptTypes.product,
                        Optional oFormat As DTOSellOut.Formats = DTOSellOut.Formats.amounts
                        ) As Task(Of DTOSellOut)

        If oYearMonthTo Is Nothing Then oYearMonthTo = New DTOYearMonth(DTO.GlobalVariables.Today().Year, 12)
        Dim retval As New DTOSellOut(DTOSellOut.ConceptTypes.cnaps, oUser.Lang)
        With retval
            .User = oUser
            .YearMonths = oYearMonthTo.Last12Yearmonths()
            .ConceptType = oConceptType
            .Format = oFormat
            .Filters = DTOSellOut.AllFilters(.User.Lang)
        End With

        Select Case oUser.Rol.id
            Case DTORol.Ids.manufacturer
                Dim oProveidor = Await User.GetProveidor(oUser, exs)
                If oProveidor Is Nothing Then
                    retval = Nothing
                Else
                    AddFilter(retval, DTOSellOut.Filter.Cods.Provider, {oProveidor})
                End If
            Case DTORol.Ids.cliLite, DTORol.Ids.cliFull
                Dim oCustomers = Await User.GetCustomers(oUser, exs)
                If oCustomers.Count = 0 Then
                    retval = Nothing
                Else
                    AddFilter(retval, DTOSellOut.Filter.Cods.Customer, oCustomers.ToArray)
                End If
            Case DTORol.Ids.comercial, DTORol.Ids.rep
                Dim oRep = Await User.GetRep(oUser, exs)
                If oRep Is Nothing Then
                    retval = Nothing
                Else
                    AddFilter(retval, DTOSellOut.Filter.Cods.Rep, {oRep})
                End If
            Case DTORol.Ids.salesManager
                retval.ConceptType = DTOSellOut.ConceptTypes.repsGeo
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.operadora
            Case Else
                retval = Nothing
        End Select
        Return retval
    End Function

    Shared Async Function Factory(oUser As DTOUser, year As Integer, conceptType As DTOSellOut.ConceptTypes, format As DTOSellOut.ConceptTypes, brand As String, category As String, channel As String, country As String, zona As String, location As String, contact As String, groupbyholding As Boolean) As Task(Of DTOSellOut)
        'de controller via jquery
        Dim exs As New List(Of Exception)
        Dim oYearMonthTo As New DTOYearMonth(year, 12)
        Dim retval = Await SellOut.Factory(exs, oUser, oYearMonthTo, conceptType, format)

        With retval

            If GuidHelper.IsGuid(category) Then
                Dim oProduct = Await ProductCategory.Find(exs, New Guid(category))
                Dim value = New DTOGuidNom(oProduct.Guid, oProduct.Nom.Esp)
                Dim oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Product)
                oFilter.Values.Add(value)
            ElseIf GuidHelper.IsGuid(brand) Then
                Dim oProduct = Await ProductBrand.Find(exs, New Guid(brand))
                Dim value = New DTOGuidNom(oProduct.Guid, oProduct.Nom.Esp)
                Dim oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Product)
                oFilter.Values.Add(value)
            End If

            If GuidHelper.IsGuid(channel) Then
                Dim oChannel = Await DistributionChannel.Find(New Guid(channel), exs)
                Dim value = New DTOGuidNom(oChannel.Guid, oChannel.LangText.Tradueix(oUser.Lang))
                Dim oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Channel)
                oFilter.Values.Add(value)
            End If

            If GuidHelper.IsGuid(contact) Then
                Dim value = Await Customer.Find(exs, New Guid(contact))
                Dim oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Customer)
                oFilter.Values.Add(value)
                .GroupByHolding = groupbyholding
            ElseIf GuidHelper.IsGuid(location) Then
                Dim value = Await FEB.Location.Find(New Guid(location), exs)
                Dim oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Atlas)
                oFilter.Values.Add(value)
            ElseIf GuidHelper.IsGuid(zona) Then
                Dim value = Await FEB.Zona.Find(New Guid(zona), exs)
                Dim oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Atlas)
                oFilter.Values.Add(value)
            ElseIf GuidHelper.IsGuid(country) Then
                Dim oCountry = Await FEB.Country.Find(New Guid(country), exs)
                Dim oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Atlas)
                Dim Value = New DTOGuidNom(oCountry.Guid, oCountry.LangNom.Tradueix(retval.Lang))
                oFilter.Values.Add(Value)
            End If

            If .User.Rol.id = DTORol.Ids.salesManager Then
                .ConceptType = DTOSellOut.ConceptTypes.repsGeo
            End If
        End With

        Return retval
    End Function


    Shared Function FilterValues(oSellout As DTOSellOut, oCod As DTOSellOut.Filter.Cods) As List(Of DTOGuidNom)
        Dim retval As New List(Of DTOGuidNom)
        Dim oFilter = oSellout.GetFilter(oCod)
        If oFilter IsNot Nothing Then
            retval = oFilter.Values
        End If
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

    Shared Sub AddFilterValues(oSellout As DTOSellOut, oCod As DTOSellOut.Filter.Cods, values() As DTOGuidNom)
        Dim oFilter = oSellout.GetFilter(oCod)
        If oFilter IsNot Nothing Then
            oFilter.Values.AddRange(values)
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

    Shared Function RawDataLast12MonthsCsvUrl(Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Dox(AbsoluteUrl, DTODocFile.Cods.rawdatalast12monthscsv)
        Return retval
    End Function

    Shared Sub SetCcx(ByRef oSellOut As DTOSellOut, oCcx As DTOCustomer)
        SellOut.ClearFilter(oSellOut, DTOSellOut.Filter.Cods.Customer)
        SellOut.AddFilter(oSellOut, DTOSellOut.Filter.Cods.Customer, {oCcx})
        oSellOut.GroupByHolding = True
    End Sub

    Shared Function ExcelUrl(oSellout As DTOSellOut, Optional AbsoluteUrl As Boolean = False) As String
        Dim oParameters As Dictionary(Of String, String) = Parameters(oSellout)
        Dim retval As String = UrlHelper.Factory(DTODocFile.Cods.selloutexcel, oParameters, AbsoluteUrl)
        Return retval
    End Function

    Shared Async Function ExcelRawData(exs As List(Of Exception), year As Integer, oUser As DTOUser) As Task(Of MatHelper.Excel.Sheet)
        Dim retval = Await Api.Fetch(Of MatHelper.Excel.Sheet)(exs, "Sellout/Excel", year, oUser.Guid.ToString())
        Return retval
    End Function

    Shared Async Function RawData(exs As List(Of Exception), oUser As DTOUser, year As Integer) As Task(Of MatHelper.Excel.Sheet)
        Dim retval = Await Api.Fetch(Of MatHelper.Excel.Sheet)(exs, "sellout/data", oUser.Guid.ToString(), year)
        Return retval
    End Function

    Private Shared Function AddFilterParameter(oDictionary As Dictionary(Of String, String), oSellOut As DTOSellOut, oCod As DTOSellOut.Filter.Cods, sParamName As String) As Boolean
        Dim retval As Boolean
        Dim oFilter = oSellOut.GetFilter(oCod)
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

    Shared Async Function FromParameters(exs As List(Of Exception), oParameters As Dictionary(Of String, String)) As Task(Of DTOSellOut)
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
            oUser = Await User.Find(New Guid(oParameters("User")), exs)
        End If

        Dim retval As DTOSellOut = Await SellOut.Factory(exs, oUser, oYearMonthTo, oConceptType, oFormat)
        retval.GroupByHolding = BlGroupByHolding

        If oParameters.ContainsKey("Area") Then
            Dim value As DTOGuidNom = Area.FindSync(New Guid(oParameters("Area")), exs)
            SellOut.AddFilterValues(retval, DTOSellOut.Filter.Cods.Atlas, {value})
        End If

        Dim oProducts As New List(Of DTOProduct)
        If oParameters.ContainsKey("Product") Then
            Dim oProduct = Await Product.Find(exs, New Guid(oParameters("Product")))
            Dim value As New DTOGuidNom(oProduct.Guid, oProduct.Nom.Esp)
            SellOut.AddFilterValues(retval, DTOSellOut.Filter.Cods.Product, {value})
        End If

        Dim oProveidors As New List(Of DTOProveidor)
        If oParameters.ContainsKey("Proveidor") Then
            Dim oProviderGuid = New Guid(oParameters("Proveidor"))
            If Not SellOut.FilterValues(retval, DTOSellOut.Filter.Cods.Provider).Any(Function(x) x.Guid.Equals(oProviderGuid)) Then
                Dim value As DTOGuidNom = Await Proveidor.Find(oProviderGuid, exs)
                SellOut.AddFilterValues(retval, DTOSellOut.Filter.Cods.Provider, {value})
            End If
        End If

        Dim oCustomers As New List(Of DTOCustomer)
        If oParameters.ContainsKey("Customer") Then
            Dim value As DTOGuidNom = Await Customer.Find(exs, New Guid(oParameters("Customer")))
            SellOut.AddFilterValues(retval, DTOSellOut.Filter.Cods.Customer, {value})
        End If

        Dim oReps As New List(Of DTORep)
        If oParameters.ContainsKey("Rep") Then
            Dim value As DTOGuidNom = Await Rep.Find(New Guid(oParameters("Rep")), exs)
            SellOut.AddFilterValues(retval, DTOSellOut.Filter.Cods.Rep, {value})
        End If

        Dim oChannels As New List(Of DTODistributionChannel)
        If oParameters.ContainsKey("Channel") Then
            Dim oChannel = Await DistributionChannel.Find(New Guid(oParameters("Channel")), exs)
            Dim value As New DTOGuidNom(oChannel.Guid, oChannel.LangText.Tradueix(DTOLang.ESP))
            SellOut.AddFilterValues(retval, DTOSellOut.Filter.Cods.Channel, {value})
        End If

        Return retval
    End Function



End Class
