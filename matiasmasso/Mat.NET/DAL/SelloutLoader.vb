Imports DTO.Models

Public Class SelloutLoader

    Shared Sub Load(ByRef oSellout As DTOSellOut)
        Select Case oSellout.ConceptType
            Case DTOSellOut.ConceptTypes.channels
                LoadChannels(oSellout)
            Case DTOSellOut.ConceptTypes.geo
                LoadGeo(oSellout)
            Case DTOSellOut.ConceptTypes.product
                LoadProducts(oSellout)
            Case DTOSellOut.ConceptTypes.repsGeo
                LoadRepsGeo(oSellout)
            Case DTOSellOut.ConceptTypes.repsProduct
                LoadRepsProduct(oSellout)
            Case DTOSellOut.ConceptTypes.yeas
                LoadYearsProduct(oSellout)
            Case DTOSellOut.ConceptTypes.cnaps
                LoadCnaps(oSellout)
            Case DTOSellOut.ConceptTypes.centres
                LoadCentres(oSellout)
        End Select
    End Sub



    Shared Sub LoadCnaps(ByRef oSellout As DTOSellOut)
        oSellout.Items = New List(Of DTOSelloutItem)
        Dim oCnaps As List(Of DTOCnap) = CnapsLoader.All()
        Dim items As New List(Of DTOSelloutItem)

        Dim oPreviousItems As New List(Of DTOSelloutItem)
        Dim oFlatItems As New List(Of DTOSelloutItem)
        Dim item As DTOSelloutItem = Nothing
        For i As Integer = 1 To 5
            item = New DTOSelloutItem(oSellout)
            item.Tag = New DTOCnap()
            oPreviousItems.Add(item)
        Next

        For Each oCnap In oCnaps
            Dim sConcept As String = String.Format("{0} {1}", oCnap.Id, oCnap.NomShort.Tradueix(oSellout.Lang))
            item = Nothing
            For i As Integer = 4 To 0 Step -1
                Dim oPreviousItem As DTOSelloutItem = oPreviousItems(i)
                Dim previousId As String = If(DirectCast(oPreviousItem.Tag, DTOCnap).Id, "ZZ")
                If oCnap.Id.StartsWith(previousId) Then
                    item = oPreviousItem.AddItem(sConcept)
                    Exit For
                End If
            Next

            If item Is Nothing Then
                item = New DTOSelloutItem(oSellout, sConcept)
                oSellout.Items.Add(item)
            End If

            item.Tag = oCnap
            oFlatItems.Add(item)

            Dim Idx As Integer = oCnap.Id.Length - 1
            oPreviousItems(Idx) = item
        Next

        Dim oNoCnap As New DTOCnap(Guid.Empty)
        oNoCnap.NomLong = New DTOLangText("(pendent de codificar)")
        Dim oNoItem As New DTOSelloutItem(oSellout, oNoCnap.NomLong.Esp)
        oNoItem.Tag = oNoCnap

        Dim SQL As String = SelloutLoader.SQL(oSellout)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim oCnap As New DTOCnap
            If IsDBNull(oDrd("CnapGuid")) Then
                item = oNoItem
            Else
                item = oFlatItems.FirstOrDefault(Function(x) DirectCast(x.Tag, DTOCnap).Guid.Equals(oDrd("CnapGuid")))
                If item Is Nothing Then
                    item = oNoItem
                End If
            End If

            For monthIdx As Integer = 1 To 12
                Select Case oSellout.Format
                    Case DTOSellOut.Formats.units
                        item.Values(monthIdx - 1) = SQLHelper.GetIntegerFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                    Case DTOSellOut.Formats.amounts
                        item.Values(monthIdx - 1) = SQLHelper.GetDecimalFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                End Select
            Next

        Loop
        oDrd.Close()

        If Not oNoItem.IsEmpty Then
            oSellout.Items.Add(item)
            oFlatItems.Add(item)
        End If
        'arrossega els totals
        For Each item In oSellout.Items
            AggregateChildrenValues(item)
        Next

    End Sub


    Shared Sub LoadGeo(ByRef oSellout As DTOSellOut)
        oSellout.Items = New List(Of DTOSelloutItem)
        Dim itemCountry As New DTOSelloutItem(oSellout)
        Dim itemZona As New DTOSelloutItem(oSellout)
        Dim itemLocation As New DTOSelloutItem(oSellout)
        Dim itemCustomer As DTOSelloutItem = Nothing
        Dim SQL As String = SelloutLoader.SQL(oSellout)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("CountryGuid").Equals(DirectCast(itemCountry.Tag, DTOBaseGuid).Guid) Then
                Dim oCountry As DTOCountry = SQLHelper.GetCountryFromDataReader(oDrd)
                itemCountry = New DTOSelloutItem(oSellout, oCountry.LangNom.Tradueix(oSellout.Lang))
                itemCountry.Tag = oCountry
                oSellout.Items.Add(itemCountry)
            End If
            If Not oDrd("ZonaGuid").Equals(DirectCast(itemZona.Tag, DTOBaseGuid).Guid) Then
                Dim oZona As New DTOZona(oDrd("ZonaGuid"))
                oZona.Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                oZona.Country = itemCountry.Tag
                itemZona = itemCountry.AddItem(oZona.Nom)
                itemZona.Tag = oZona
            End If
            If Not oDrd("LocationGuid").Equals(DirectCast(itemLocation.Tag, DTOBaseGuid).Guid) Then
                Dim oLocation As New DTOLocation(oDrd("LocationGuid"))
                oLocation.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                oLocation.Zona = itemZona.Tag
                itemLocation = itemZona.AddItem(oLocation.Nom)
                itemLocation.Tag = oLocation
            End If

            Dim sCaption As String = ""
            Dim oCustomer As New DTOCustomer(oDrd("CustomerGuid"))
            With oCustomer
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("CustomerNom"))
                .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("CustomerNomCom"))
                If .Nom > "" Then
                    If .NomComercial > "" Then
                        sCaption = String.Format("{0} '{1}'", .Nom, .NomComercial)
                    Else
                        sCaption = .Nom
                    End If
                Else
                    sCaption = .NomComercial
                End If
            End With
            itemCustomer = itemLocation.AddItem(sCaption)
            itemCustomer.Tag = oCustomer

            For monthIdx As Integer = 1 To 12
                Select Case oSellout.Format
                    Case DTOSellOut.Formats.units
                        itemCustomer.Values(monthIdx - 1) = SQLHelper.GetIntegerFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                    Case DTOSellOut.Formats.amounts
                        itemCustomer.Values(monthIdx - 1) = SQLHelper.GetDecimalFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                End Select
            Next

        Loop
        oDrd.Close()

        'arrossega els totals
        For Each item In oSellout.Items
            AggregateChildrenValues(item)
        Next

        For Each itemCountry In oSellout.Items
            For Each itemZona In itemCountry.Items
                For Each itemLocation In itemZona.Items
                    'endreça passant del Zip:
                    itemLocation.Items = itemLocation.Items.OrderBy(Function(x) x.Concept).ToList
                Next
            Next
        Next
    End Sub

    Shared Sub LoadRepsGeo(ByRef oSellout As DTOSellOut)
        oSellout.Items = New List(Of DTOSelloutItem)
        Dim itemRep As New DTOSelloutItem(oSellout)
        Dim itemCountry As New DTOSelloutItem(oSellout)
        Dim itemZona As New DTOSelloutItem(oSellout)
        Dim itemLocation As New DTOSelloutItem(oSellout)
        Dim itemCustomer As New DTOSelloutItem(oSellout)
        Dim SQL As String = SelloutLoader.SQL(oSellout)
        Dim resetChildren As Boolean
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRepGuid As Guid = Guid.Empty
            If Not IsDBNull(oDrd("RepGuid")) Then oRepGuid = oDrd("RepGuid")
            If Not oRepGuid.Equals(DirectCast(itemRep.Tag, DTOBaseGuid).Guid) Then
                Dim oRep As New DTORep(oRepGuid)
                If oRep.Guid = Nothing Then oRep.Nom = "(s/rep)" Else oRep.Nom = SQLHelper.GetStringFromDataReader(oDrd("RepNom"))
                itemRep = New DTOSelloutItem(oSellout, oRep.Nom)
                itemRep.Tag = oRep

                oSellout.Items.Add(itemRep)
                resetChildren = True
            End If
            If resetChildren Or Not oDrd("CountryGuid").Equals(DirectCast(itemCountry.Tag, DTOBaseGuid).Guid) Then
                Dim oCountry As DTOCountry = SQLHelper.GetCountryFromDataReader(oDrd)
                itemCountry = itemRep.AddItem(oCountry.LangNom.Tradueix(oSellout.Lang))
                itemCountry.Tag = oCountry

                resetChildren = True
            End If
            If resetChildren Or Not oDrd("ZonaGuid").Equals(DirectCast(itemZona.Tag, DTOBaseGuid).Guid) Then
                Dim oZona As New DTOZona(oDrd("ZonaGuid"))
                oZona.Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                oZona.Country = itemCountry.Tag
                itemZona = itemCountry.AddItem(oZona.Nom)
                itemZona.Tag = oZona

                resetChildren = True
            End If
            If resetChildren Or Not oDrd("LocationGuid").Equals(DirectCast(itemLocation.Tag, DTOBaseGuid).Guid) Then
                Dim oLocation As New DTOLocation(oDrd("LocationGuid"))
                oLocation.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                oLocation.Zona = itemZona.Tag
                itemLocation = itemZona.AddItem(oLocation.Nom)
                itemLocation.Tag = oLocation

                resetChildren = True
            End If

            Dim sCaption As String = ""
            Dim oCustomer As New DTOCustomer(oDrd("CustomerGuid"))
            With oCustomer
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("CustomerNom"))
                .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("CustomerNomCom"))
                If .Nom > "" Then
                    If .NomComercial > "" Then
                        sCaption = String.Format("{0} '{1}'", .Nom, .NomComercial)
                    Else
                        sCaption = .Nom
                    End If
                Else
                    sCaption = .NomComercial
                End If
            End With

            itemCustomer = itemLocation.AddItem(sCaption)
            itemCustomer.Tag = oCustomer
            resetChildren = False

            For monthIdx As Integer = 1 To 12
                Select Case oSellout.Format
                    Case DTOSellOut.Formats.units
                        itemCustomer.Values(monthIdx - 1) = SQLHelper.GetIntegerFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                    Case DTOSellOut.Formats.amounts
                        itemCustomer.Values(monthIdx - 1) = SQLHelper.GetDecimalFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                End Select
            Next

            'If oCustomer.Guid.ToString.ToUpper = "0FA0A2E6-9666-4EEA-9F88-4BD535732969" Then Stop
        Loop
        oDrd.Close()

        'arrossega els totals
        For Each item In oSellout.Items
            AggregateChildrenValues(item)
        Next

        For Each itemRep In oSellout.Items
            For Each itemCountry In oSellout.Items
                For Each itemZona In itemCountry.Items
                    For Each itemLocation In itemZona.Items
                        'endreça passant del Zip:
                        itemLocation.Items = itemLocation.Items.OrderBy(Function(x) x.Concept).ToList
                    Next
                Next
            Next
        Next
    End Sub

    Shared Sub LoadRepsProduct(ByRef oSellout As DTOSellOut)
        oSellout.Items = New List(Of DTOSelloutItem)
        Dim itemRep As New DTOSelloutItem(oSellout)
        Dim itemBrand As New DTOSelloutItem(oSellout)
        Dim itemCategory As New DTOSelloutItem(oSellout)
        Dim itemSku As New DTOSelloutItem(oSellout)
        Dim SQL As String = SelloutLoader.SQL(oSellout)
        Dim resetChildren As Boolean
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRepGuid As Guid = Guid.Empty
            If Not IsDBNull(oDrd("RepGuid")) Then oRepGuid = oDrd("RepGuid")
            If Not oRepGuid.Equals(DirectCast(itemRep.Tag, DTOBaseGuid).Guid) Then
                Dim oRep As New DTORep(oRepGuid)
                If oRep.Guid = Nothing Then oRep.Nom = "(s/rep)" Else oRep.Nom = SQLHelper.GetStringFromDataReader(oDrd("RepNom"))
                itemRep = New DTOSelloutItem(oSellout, oRep.Nom)
                itemRep.Tag = oRep

                oSellout.Items.Add(itemRep)
                resetChildren = True
            End If

            If resetChildren Or Not oDrd("BrandGuid").Equals(DirectCast(itemBrand.Tag, DTOBaseGuid).Guid) Then
                Dim oBrand As New DTOProductBrand(oDrd("BrandGuid"))
                SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                itemBrand = itemRep.AddItem(oBrand.Nom.Esp)
                itemBrand.Tag = oBrand
                resetChildren = True
            End If

            If resetChildren Or Not oDrd("CategoryGuid").Equals(DirectCast(itemCategory.Tag, DTOBaseGuid).Guid) Then
                Dim oCategory As New DTOProductCategory(oDrd("CategoryGuid"))
                SQLHelper.LoadLangTextFromDataReader(oCategory.Nom, oDrd, "CategoryNom", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                itemCategory = itemBrand.AddItem(oCategory.Nom.Tradueix(oSellout.Lang))
                itemCategory.Tag = oCategory
            End If

            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            SQLHelper.LoadLangTextFromDataReader(oSku.Nom, oDrd, "SkuNom", "SkuNomCat", "SkuNomEng", "SkuNomPor")
            itemSku = itemCategory.AddItem(oSku.Nom.Tradueix(oSellout.Lang))
            itemSku.Tag = oSku

            resetChildren = False

            For monthIdx As Integer = 1 To 12
                Select Case oSellout.Format
                    Case DTOSellOut.Formats.units
                        itemSku.Values(monthIdx - 1) = SQLHelper.GetIntegerFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                    Case DTOSellOut.Formats.amounts
                        itemSku.Values(monthIdx - 1) = SQLHelper.GetDecimalFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                End Select
            Next

        Loop
        oDrd.Close()

        'arrossega els totals
        For Each item In oSellout.Items
            AggregateChildrenValues(item)
        Next

        For Each itemRep In oSellout.Items
            For Each itemCountry In oSellout.Items
                For Each itemZona In itemCountry.Items
                    For Each itemLocation In itemZona.Items
                        'endreça passant del Zip:
                        itemLocation.Items = itemLocation.Items.OrderBy(Function(x) x.Concept).ToList
                    Next
                Next
            Next
        Next
    End Sub

    Shared Sub LoadCentres(ByRef oSellout As DTOSellOut)
        oSellout.Items = New List(Of DTOSelloutItem)
        Dim itemCentre As New DTOSelloutItem(oSellout)
        Dim itemBrand As New DTOSelloutItem(oSellout)
        Dim itemCategory As New DTOSelloutItem(oSellout)
        Dim itemSku As New DTOSelloutItem(oSellout)
        Dim SQL As String = SelloutLoader.SQL(oSellout)
        Dim resetChildren As Boolean
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCentreGuid As Guid = Guid.Empty
            If Not IsDBNull(oDrd("CustomerGuid")) Then oCentreGuid = oDrd("CustomerGuid")
            If Not oCentreGuid.Equals(DirectCast(itemCentre.Tag, DTOBaseGuid).Guid) Then
                Dim oCustomer As New DTOCustomer(oCentreGuid)
                itemCentre = New DTOSelloutItem(oSellout, oDrd("CliNom"))
                itemCentre.Tag = oCustomer

                oSellout.Items.Add(itemCentre)
                resetChildren = True
            End If

            If resetChildren Or Not oDrd("BrandGuid").Equals(DirectCast(itemBrand.Tag, DTOBaseGuid).Guid) Then
                Dim oBrand As New DTOProductBrand(oDrd("BrandGuid"))
                SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                itemBrand = itemCentre.AddItem(oBrand.Nom.Esp)
                itemBrand.Tag = oBrand
                resetChildren = True
            End If

            If resetChildren Or Not oDrd("CategoryGuid").Equals(DirectCast(itemCategory.Tag, DTOBaseGuid).Guid) Then
                Dim oCategory As New DTOProductCategory(oDrd("CategoryGuid"))
                SQLHelper.LoadLangTextFromDataReader(oCategory.Nom, oDrd, "CategoryNom", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                itemCategory = itemBrand.AddItem(oCategory.Nom.Tradueix(oSellout.Lang))
                itemCategory.Tag = oCategory
            End If

            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            itemSku = itemCategory.AddItem(oSku.Nom.Tradueix(oSellout.Lang))
            itemSku.Tag = oSku

            resetChildren = False

            For monthIdx As Integer = 1 To 12
                Select Case oSellout.Format
                    Case DTOSellOut.Formats.units
                        itemSku.Values(monthIdx - 1) = SQLHelper.GetIntegerFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                    Case DTOSellOut.Formats.amounts
                        itemSku.Values(monthIdx - 1) = SQLHelper.GetDecimalFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                End Select
            Next

        Loop
        oDrd.Close()

        'arrossega els totals
        For Each item In oSellout.Items
            AggregateChildrenValues(item)
        Next

        For Each itemRep In oSellout.Items
            For Each itemCountry In oSellout.Items
                For Each itemZona In itemCountry.Items
                    For Each itemLocation In itemZona.Items
                        'endreça passant del Zip:
                        itemLocation.Items = itemLocation.Items.OrderBy(Function(x) x.Concept).ToList
                    Next
                Next
            Next
        Next
    End Sub

    Shared Sub LoadYearsProduct(ByRef oSellout As DTOSellOut)
        oSellout.Items = New List(Of DTOSelloutItem)
        Dim itemYear As New DTOSelloutItem(oSellout)
        itemYear.Tag = New DTOYear
        Dim SQL As String = SelloutLoader.SQL(oSellout)
        Dim resetChildren As Boolean
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If oDrd("Year") <> (DirectCast(itemYear.Tag, DTOYear).Year) Then
                Dim oYear As New DTOYear()
                oYear.Year = oDrd("Year")
                itemYear = New DTOSelloutItem(oSellout, oYear.Year)
                itemYear.Tag = oYear

                oSellout.Items.Add(itemYear)
                resetChildren = True
            End If

            For monthIdx As Integer = 1 To 12
                Select Case oSellout.Format
                    Case DTOSellOut.Formats.units
                        itemYear.Values(monthIdx - 1) = SQLHelper.GetIntegerFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                    Case DTOSellOut.Formats.amounts
                        itemYear.Values(monthIdx - 1) = SQLHelper.GetDecimalFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                End Select
            Next

        Loop
        oDrd.Close()

    End Sub

    Shared Sub LoadChannels(ByRef oSellout As DTOSellOut)
        oSellout.Items = New List(Of DTOSelloutItem)
        Dim itemChannel As New DTOSelloutItem(oSellout)
        Dim itemCountry As New DTOSelloutItem(oSellout)
        Dim itemZona As New DTOSelloutItem(oSellout)
        Dim itemLocation As New DTOSelloutItem(oSellout)
        Dim itemCustomer As DTOSelloutItem = Nothing
        Dim SQL As String = SelloutLoader.SQL(oSellout)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("ChannelGuid").Equals(DirectCast(itemChannel.Tag, DTOBaseGuid).Guid) Then
                Dim oChannel As New DTODistributionChannel(oDrd("ChannelGuid"))
                oChannel.LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ChannelNom", "ChannelNomCat", "ChannelNomEng", "ChannelNomPor")
                itemChannel = New DTOSelloutItem(oSellout, oChannel.LangText.Tradueix(oSellout.Lang))
                itemChannel.Tag = oChannel
                oSellout.Items.Add(itemChannel)
                itemCountry = New DTOSelloutItem(oSellout)
                itemZona = New DTOSelloutItem(oSellout)
                itemLocation = New DTOSelloutItem(oSellout)
                itemCustomer = Nothing
            End If

            If Not oDrd("CountryGuid").Equals(DirectCast(itemCountry.Tag, DTOBaseGuid).Guid) Then
                Dim oCountry As DTOCountry = SQLHelper.GetCountryFromDataReader(oDrd)
                itemCountry = itemChannel.AddItem(oCountry.LangNom.Tradueix(oSellout.Lang))
                itemCountry.Tag = oCountry
            End If
            If Not oDrd("ZonaGuid").Equals(DirectCast(itemZona.Tag, DTOBaseGuid).Guid) Then
                Dim oZona As New DTOZona(oDrd("ZonaGuid"))
                oZona.Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                oZona.Country = itemCountry.Tag
                itemZona = itemCountry.AddItem(oZona.Nom)
                itemZona.Tag = oZona
            End If
            If Not oDrd("LocationGuid").Equals(DirectCast(itemLocation.Tag, DTOBaseGuid).Guid) Then
                Dim oLocation As New DTOLocation(oDrd("LocationGuid"))
                oLocation.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                oLocation.Zona = itemZona.Tag
                itemLocation = itemZona.AddItem(oLocation.Nom)
                itemLocation.Tag = oLocation
            End If

            Dim sCaption As String = ""
            Dim oCustomer As New DTOCustomer(oDrd("CustomerGuid"))
            With oCustomer
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("CustomerNom"))
                .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("CustomerNomCom"))
                If .Nom > "" Then
                    If .NomComercial > "" Then
                        sCaption = String.Format("{0} '{1}'", .Nom, .NomComercial)
                    Else
                        sCaption = .Nom
                    End If
                Else
                    sCaption = .NomComercial
                End If
            End With

            itemCustomer = itemLocation.AddItem(sCaption)
            itemCustomer.Tag = oCustomer

            For monthIdx As Integer = 1 To 12
                Select Case oSellout.Format
                    Case DTOSellOut.Formats.units
                        itemCustomer.Values(monthIdx - 1) = SQLHelper.GetIntegerFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                    Case DTOSellOut.Formats.amounts
                        itemCustomer.Values(monthIdx - 1) = SQLHelper.GetDecimalFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                End Select
            Next

        Loop
        oDrd.Close()

        'arrossega els totals
        For Each item In oSellout.Items
            AggregateChildrenValues(item)
        Next

    End Sub

    Shared Sub LoadProducts(ByRef oSellout As DTOSellOut)
        oSellout.Items = New List(Of DTOSelloutItem)
        Dim itemBrand As New DTOSelloutItem(oSellout)
        Dim itemCategory As New DTOSelloutItem(oSellout)
        Dim itemSku As New DTOSelloutItem(oSellout)
        Dim SQL As String = SelloutLoader.SQL(oSellout)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("BrandGuid").Equals(DirectCast(itemBrand.Tag, DTOBaseGuid).Guid) Then
                Dim oBrand As New DTOProductBrand(oDrd("BrandGuid"))
                SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNomEsp", "BrandNomEsp", "BrandNomEsp", "BrandNomEsp")
                itemBrand = New DTOSelloutItem(oSellout, oBrand.Nom.Esp)
                itemBrand.Tag = oBrand
                oSellout.Items.Add(itemBrand)
            End If

            If Not oDrd("CategoryGuid").Equals(DirectCast(itemCategory.Tag, DTOBaseGuid).Guid) Then
                Dim oCategory As New DTOProductCategory(oDrd("CategoryGuid"))
                SQLHelper.LoadLangTextFromDataReader(oCategory.Nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                itemCategory = itemBrand.AddItem(oCategory.Nom.Tradueix(oSellout.Lang))
                itemCategory.Tag = oCategory
            End If

            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            itemSku = itemCategory.AddItem(oSku.Nom.Tradueix(oSellout.Lang))
            itemSku.Tag = oSku

            For monthIdx As Integer = 1 To 12
                Select Case oSellout.Format
                    Case DTOSellOut.Formats.units
                        itemSku.Values(monthIdx - 1) = SQLHelper.GetIntegerFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                    Case DTOSellOut.Formats.amounts
                        itemSku.Values(monthIdx - 1) = SQLHelper.GetDecimalFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                End Select
            Next

        Loop
        oDrd.Close()

        For Each item In oSellout.Items
            AggregateChildrenValues(item)
        Next

    End Sub

    Shared Function RawExcel(ByRef oSellout As DTOSellOut) As MatHelper.Excel.Sheet
        Dim iYear As Integer = oSellout.YearMonths.First.Year
        Dim retval As New MatHelper.Excel.Sheet(iYear.ToString, Format("M+O Sellout data {0}.xlsx", iYear))
        With retval
            .AddColumn(oSellout.Lang.Tradueix("Canal", "Canal", "Channel"))
            .AddColumn(oSellout.Lang.Tradueix("Pais", "Pais", "Country"))
            .AddColumn(oSellout.Lang.Tradueix("Zona", "Zona", "Zone"))
            .AddColumn(oSellout.Lang.Tradueix("Población", "Població", "Location"))
            .AddColumn(oSellout.Lang.Tradueix("Cliente", "Client", "Customer"))
            .AddColumn(oSellout.Lang.Tradueix("Marca", "Marca", "Brand"))
            .AddColumn(oSellout.Lang.Tradueix("Categoría", "Categoria", "Category"))
            .AddColumn(oSellout.Lang.Tradueix("Producto", "Producte", "Product"))
            .AddColumn(oSellout.Lang.Tradueix("Ref", "Ref", "Ref"))
            .AddColumn(oSellout.Lang.Tradueix("Ean 13", "Ean 13", "Ean 13"))
            .AddColumn("Total unidades", MatHelper.Excel.Cell.NumberFormats.Integer)
            For i = 1 To 12
                .AddColumn(oSellout.Lang.MesAbr(i), MatHelper.Excel.Cell.NumberFormats.Integer)
            Next
            .AddColumn("Total importes", MatHelper.Excel.Cell.NumberFormats.Euro)
            For i = 1 To 12
                .AddColumn(oSellout.Lang.MesAbr(i), MatHelper.Excel.Cell.NumberFormats.Euro)
            Next
            .DisplayTotals = True
        End With

        Dim SQL As String = SelloutLoader.SQL(oSellout)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Try
                Dim sCustomerNom = SQLHelper.GetStringFromDataReader(oDrd("CustomerNom"))
                Dim sCustomerNomCom = SQLHelper.GetStringFromDataReader(oDrd("CustomerNomCom"))
                Dim sClientRef = SQLHelper.GetStringFromDataReader(oDrd("ClientRef"))
                Dim sNom = sCustomerNom
                If sCustomerNomCom > "" And Not sCustomerNom.Contains(sCustomerNomCom) Then
                    sNom = sNom & " (" & sCustomerNomCom.Trim & ")"
                End If
                If sClientRef > "" Then
                    sNom = sNom & " (" & sClientRef.Trim & ")"
                End If

                Dim oRow = retval.AddRow
                oRow.AddCell(oSellout.Lang.Tradueix(oDrd("ChannelNom"), oDrd("ChannelNomCat"), oDrd("ChannelNomEng"), oDrd("ChannelNomPor")))
                oRow.AddCell(oSellout.Lang.Tradueix(oDrd("CountryEsp"), oDrd("CountryCat"), oDrd("CountryEng"), oDrd("CountryPor")))
                oRow.AddCell(oDrd("ZonaNom"))
                oRow.AddCell(oDrd("LocationNom"))
                oRow.AddCell(sNom)
                oRow.AddCell(oDrd("BrandNomEsp"))
                oRow.AddCell(oSellout.Lang.Tradueix(oDrd("CategoryNomEsp"), oDrd("CategoryNomCat"), oDrd("CategoryNomEng"), oDrd("CategoryNomPor")))
                If oSellout.User.Rol.id = DTORol.Ids.manufacturer Then
                    oRow.AddCell(oDrd("SkuPrvNom"))
                Else
                    oRow.AddCell(oSellout.Lang.Tradueix(oDrd("SkuNomEsp"), oDrd("SkuyNomCat"), oDrd("SkuNomEng"), oDrd("SkuNomPor")))
                End If
                oRow.AddCell(oDrd("SkuRef"))
                oRow.AddCell(oDrd("Ean13"))

                oRow.AddFormula("SUM(RC[1]:RC[12])")
                For monthIdx As Integer = 1 To 12
                    Dim value = SQLHelper.GetDecimalFromDataReader(oDrd("Q" & Format(monthIdx, "00")))
                    oRow.AddCell(value)
                Next
                oRow.AddFormula("SUM(RC[1]:RC[12])")
                For monthIdx As Integer = 1 To 12
                    Dim value = SQLHelper.GetDecimalFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                    oRow.AddCell(value)
                Next


            Catch ex As Exception
            End Try
        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function RawData(oUser As DTOUser, year As Integer) As SellOutModel
        Dim retval As New SellOutModel
        Dim oYearMonthTo As New DTOYearMonth(year, 12)

        Dim oSellOut = DTOSellOut.Factory(oUser, oYearMonthTo, DTOSellOut.ConceptTypes.full)
        Select Case oUser.Rol.id
            Case DTORol.Ids.manufacturer
                Dim oFilter = oSellOut.Filters.FirstOrDefault(Function(x) x.Cod = DTOSellOut.Filter.Cods.Provider)
                oFilter.Values.Add(New DTOGuidNom(oUser.Contact.Guid))
            Case DTORol.Ids.comercial, DTORol.Ids.rep
                Dim oFilter = oSellOut.Filters.FirstOrDefault(Function(x) x.Cod = DTOSellOut.Filter.Cods.Rep)
                oFilter.Values.Add(New DTOGuidNom(oUser.Contact.Guid))
            Case DTORol.Ids.comercial, DTORol.Ids.rep
                Dim oFilter = oSellOut.Filters.FirstOrDefault(Function(x) x.Cod = DTOSellOut.Filter.Cods.Customer)
                oFilter.Values.Add(New DTOGuidNom(oUser.Contact.Guid))
        End Select

        Dim SQL As String = SelloutLoader.SQL(oSellOut)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read

            Dim oChannel = retval.Channels.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("ChannelGuid")))
            If oChannel Is Nothing Then
                oChannel = New SellOutModel.Channel
                oChannel.Guid = oDrd("ChannelGuid")
                oChannel.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "ChannelNom", "ChannelNomCat", "ChannelNomEng", "ChannelNomPor").Tradueix(oSellOut.Lang)
                retval.Channels.Add(oChannel)
            End If

            Dim oCountry = retval.Countries.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("CountryGuid")))
            If oCountry Is Nothing Then
                oCountry = New SellOutModel.Country
                oCountry.Guid = oDrd("CountryGuid")
                oCountry.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp", "CountryCat", "CountryEng", "CountryPor").Tradueix(oSellOut.Lang)
                retval.Countries.Add(oCountry)
            End If

            Dim oZona = oCountry.Zonas.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("ZonaGuid")))
            If oZona Is Nothing Then
                oZona = New SellOutModel.Zona
                oZona.Guid = oDrd("ZonaGuid")
                oZona.Nom = oDrd("ZonaNom")
                oCountry.Zonas.Add(oZona)
            End If

            Dim oLocation = oZona.Locations.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("LocationGuid")))
            If oLocation Is Nothing Then
                oLocation = New SellOutModel.Location
                oLocation.Guid = oDrd("LocationGuid")
                oLocation.Nom = oDrd("LocationNom")
                oZona.Locations.Add(oLocation)
            End If

            Dim oCustomer = oLocation.Customers.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("CustomerGuid")))
            If oCustomer Is Nothing Then
                Dim sCustomerNom = SQLHelper.GetStringFromDataReader(oDrd("CustomerNom"))
                Dim sCustomerNomCom = SQLHelper.GetStringFromDataReader(oDrd("CustomerNomCom"))
                Dim sClientRef = SQLHelper.GetStringFromDataReader(oDrd("ClientRef"))
                Dim sNom = sCustomerNom
                If sCustomerNomCom > "" And Not sCustomerNom.Contains(sCustomerNomCom) Then
                    sNom = sNom & " (" & sCustomerNomCom.Trim & ")"
                End If
                If sClientRef > "" Then
                    sNom = sNom & " (" & sClientRef.Trim & ")"
                End If
                oCustomer = New SellOutModel.Customer
                oCustomer.Guid = oDrd("CustomerGuid")
                oCustomer.Nom = sNom
                oCustomer.Channel = oDrd("ChannelGuid")
                oLocation.Customers.Add(oCustomer)
            End If

            Dim oBrand = retval.Brands.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("BrandGuid")))
            If oBrand Is Nothing Then
                oBrand = New SellOutModel.Brand
                oBrand.Guid = oDrd("BrandGuid")
                oBrand.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "BrandNomEsp", "BrandNomCat", "BrandNomEng", "BrandNomPor").Tradueix(oSellOut.Lang)
                retval.Brands.Add(oBrand)
            End If

            Dim oCategory = oBrand.Categories.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("CategoryGuid")))
            If oCategory Is Nothing Then
                oCategory = New SellOutModel.Category
                oCategory.Guid = oDrd("CategoryGuid")
                oCategory.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor").Tradueix(oSellOut.Lang)
                oBrand.Categories.Add(oCategory)
            End If

            Dim oSku = oCategory.Skus.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("SkuGuid")))
            If oSku Is Nothing Then
                oSku = New SellOutModel.Sku
                oSku.Guid = oDrd("SkuGuid")
                oSku.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor").Tradueix(oSellOut.Lang)
                oSku.Ean = SQLHelper.GetStringFromDataReader(oDrd("Ean13"))
                oSku.Ref = SQLHelper.GetStringFromDataReader(oDrd("SkuRef"))
                ' oSku.Cnap = oDrd("CnapGuid")
                oCategory.Skus.Add(oSku)
            End If

            Dim oMonths As New List(Of SellOutModel.Month)
            For monthIdx As Integer = 1 To 12
                Dim qty = SQLHelper.GetDecimalFromDataReader(oDrd("Q" & Format(monthIdx, "00")))
                Dim amt = SQLHelper.GetDecimalFromDataReader(oDrd("M" & Format(monthIdx, "00")))
                If qty <> 0 Or amt <> 0 Then
                    Dim month As New SellOutModel.Month
                    month.Id = monthIdx
                    month.Qty = qty
                    month.Amt = amt
                    oMonths.Add(month)
                End If
            Next

            Dim item As New SellOutModel.Item
            item.Sku = oSku.Guid
            item.Customer = oCustomer.Guid
            item.Months = oMonths
            retval.Items.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Sub AggregateChildrenValues(ByRef item As DTOSelloutItem)
        For Each oChild In item.Items
            AggregateChildrenValues(oChild)
        Next
        If item.Items.Count > 0 Then
            For i As Integer = 1 To 12
                Dim monthIdx As Integer = i - 1
                For Each oChild In item.Items
                    item.Values(monthIdx) += oChild.Values(monthIdx)
                Next
            Next
        End If
    End Sub


    Shared Function SQL(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        sb.Append(SQLSELECT(oSellout))
        sb.Append(SQLFROM(oSellout))
        sb.Append(SQLWHERE(oSellout))
        sb.Append(SQLGROUPBY(oSellout))
        sb.Append(SQLORDERBY(oSellout))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SQLSELECT(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        Select Case oSellout.ConceptType
            Case DTOSellOut.ConceptTypes.yeas
                sb.AppendLine("SELECT VwSellout2.Year ")
            Case DTOSellOut.ConceptTypes.cnaps
                sb.AppendLine("SELECT VwSellout2.CnapGuid, VwSellout2.CnapId, VwSellout2.CnapNom ")
            Case DTOSellOut.ConceptTypes.channels
                sb.AppendLine("SELECT VwSellout2.ChannelGuid, VwSellout2.ChannelNom, VwSellout2.ChannelNomCat, VwSellout2.ChannelNomEng, VwSellout2.ChannelNomPor ")
                sb.AppendLine(", VwSellout2.CountryGuid, VwSellout2.CountryEsp, VwSellout2.CountryCat, VwSellout2.CountryEng, VwSellout2.CountryPor ")
                sb.AppendLine(", VwSellout2.ZonaGuid, VwSellout2.ZonaNom, VwSellout2.LocationGuid, VwSellout2.LocationNom, VwSellout2.CustomerGuid, VwSellout2.CustomerNom, VwSellout2.CustomerNomCom ")
            Case DTOSellOut.ConceptTypes.geo
                sb.AppendLine("SELECT VwSellout2.CountryGuid, VwSellout2.CountryEsp, VwSellout2.CountryCat, VwSellout2.CountryEng, VwSellout2.CountryPor ")
                sb.AppendLine(", VwSellout2.ZonaGuid, VwSellout2.ZonaNom, VwSellout2.LocationGuid, VwSellout2.LocationNom, VwSellout2.CustomerGuid, VwSellout2.CustomerNom, VwSellout2.CustomerNomCom ")
            Case DTOSellOut.ConceptTypes.product
                sb.AppendLine("SELECT VwSellout2.ProveidorGuid, VwSellout2.ProveidorNom ")
                sb.AppendLine(", VwSellout2.BrandGuid, VwSellout2.BrandNomEsp ")
                sb.AppendLine(", VwSellout2.CategoryGuid, VwSellout2.CategoryNomEsp, VwSellout2.CategoryNomCat, VwSellout2.CategoryNomEng, VwSellout2.CategoryNomPor ")
                sb.AppendLine(", VwSellout2.SkuGuid, VwSellout2.SkuNomEsp, VwSellout2.SkuNomCat, VwSellout2.SkuNomEng, VwSellout2.SkuNomPor ")
                sb.AppendLine(", VwSellout2.Ean13, VwSellout2.SkuRef, VwSellout2.SkuPrvNom ")
            Case DTOSellOut.ConceptTypes.repsGeo
                sb.AppendLine("SELECT VwSellout2.RepGuid, VwSellout2.RepNom ")
                sb.AppendLine(", VwSellout2.CountryGuid, VwSellout2.CountryEsp, VwSellout2.CountryCat, VwSellout2.CountryEng, VwSellout2.CountryPor ")
                sb.AppendLine(", VwSellout2.ZonaGuid, VwSellout2.ZonaNom, VwSellout2.LocationGuid, VwSellout2.LocationNom, VwSellout2.CustomerGuid, VwSellout2.CustomerNom, VwSellout2.CustomerNomCom ")
            Case DTOSellOut.ConceptTypes.repsProduct
                sb.AppendLine("SELECT VwSellout2.RepGuid, VwSellout2.RepNom ")
                sb.AppendLine(", VwSellout2.BrandGuid, VwSellout2.BrandNom ")
                sb.AppendLine(", VwSellout2.CategoryGuid, VwSellout2.CategoryNomEsp, VwSellout2.CategoryNomCat, VwSellout2.CategoryNomEng, VwSellout2.CategoryNomPor ")
                sb.AppendLine(", VwSellout2.SkuGuid, VwSellout2.SkuNomEsp, VwSellout2.SkuNomCat, VwSellout2.SkuNomEng, VwSellout2.SkuNomPor ")
                sb.AppendLine(", VwSellout2.Ean13, VwSellout2.SkuRef, VwSellout2.SkuPrvNom ")
            Case DTOSellOut.ConceptTypes.centres
                sb.AppendLine("SELECT VwSellout2.CustomerGuid, (CASE WHEN VwSellout2.ClientRef >'' THEN  VwSellout2.ClientRef ELSE VwSellout2.LocationNom END) AS CliNom ")
                sb.AppendLine(", VwSellout2.BrandGuid, VwSellout2.BrandNom ")
                sb.AppendLine(", VwSellout2.CategoryGuid, VwSellout2.CategoryNomEsp, VwSellout2.CategoryNomCat, VwSellout2.CategoryNomEng, VwSellout2.CategoryNomPor ")
                sb.AppendLine(", VwSellout2.SkuGuid, VwSellout2.SkuNomEsp, VwSellout2.SkuNomCat, VwSellout2.SkuNomEng, VwSellout2.SkuNomPor ")
                sb.AppendLine(", VwSellout2.Ean13, VwSellout2.SkuRef, VwSellout2.SkuPrvNom ")
            Case DTOSellOut.ConceptTypes.full
                sb.AppendLine("SELECT VwSellout2.ChannelGuid, VwSellout2.ChannelNom, VwSellout2.ChannelNomCat, VwSellout2.ChannelNomEng, VwSellout2.ChannelNomPor ")
                sb.AppendLine(", VwSellout2.CountryGuid, VwSellout2.CountryEsp, VwSellout2.CountryCat, VwSellout2.CountryEng, VwSellout2.CountryPor ")
                sb.AppendLine(", VwSellout2.ZonaGuid, VwSellout2.ZonaNom, VwSellout2.LocationGuid, VwSellout2.LocationNom, VwSellout2.CustomerGuid, VwSellout2.CustomerNom, VwSellout2.CustomerNomCom, VwSellout2.ClientRef ")
                sb.AppendLine(", VwSellout2.BrandGuid, VwSellout2.BrandNomEsp ")
                sb.AppendLine(", VwSellout2.CategoryGuid, VwSellout2.CategoryNomEsp, VwSellout2.CategoryNomCat, VwSellout2.CategoryNomEng, VwSellout2.CategoryNomPor ")
                sb.AppendLine(", VwSellout2.SkuGuid, VwSellout2.SkuNomEsp, VwSellout2.SkuNomCat, VwSellout2.SkuNomEng, VwSellout2.SkuNomPor ")
                sb.AppendLine(", VwSellout2.Ean13, VwSellout2.SkuRef, VwSellout2.SkuPrvNom ")
        End Select

        If oSellout.ConceptType = DTOSellOut.ConceptTypes.yeas Then
            For monthIdx As Integer = 1 To 12
                Dim oYearMonth As DTOYearMonth = oSellout.YearMonths(monthIdx - 1)
                Select Case oSellout.Format
                    Case DTOSellOut.Formats.units
                        sb.AppendLine(", SUM(CASE WHEN VwSellout2.Month = " & oYearMonth.Month & " Then VwSellout2.Qty Else 0 End) As M" & Format(monthIdx, "00") & " ")
                    Case DTOSellOut.Formats.amounts
                        sb.AppendLine(", SUM(CASE WHEN VwSellout2.Month = " & oYearMonth.Month & " Then VwSellout2.Eur Else 0 End) As M" & Format(monthIdx, "00") & " ")
                End Select
            Next
        ElseIf oSellout.ConceptType = DTOSellOut.ConceptTypes.full Then
            For monthIdx As Integer = 1 To 12
                Dim oYearMonth As DTOYearMonth = oSellout.YearMonths(monthIdx - 1)
                sb.AppendLine(", SUM(CASE WHEN VwSellout2.Month = " & oYearMonth.Month & " Then VwSellout2.Qty Else 0 End) As Q" & Format(monthIdx, "00") & " ")
            Next
            For monthIdx As Integer = 1 To 12
                Dim oYearMonth As DTOYearMonth = oSellout.YearMonths(monthIdx - 1)
                sb.AppendLine(", SUM(CASE WHEN VwSellout2.Month = " & oYearMonth.Month & " Then VwSellout2.Eur Else 0 End) As M" & Format(monthIdx, "00") & " ")
            Next
        Else
            For monthIdx As Integer = 1 To 12
                Dim oYearMonth As DTOYearMonth = oSellout.YearMonths(monthIdx - 1)
                Select Case oSellout.Format
                    Case DTOSellOut.Formats.units
                        sb.AppendLine(", SUM(CASE WHEN YEAR(VwSellout2.FchCreated) = " & oYearMonth.Year & " And Month(VwSellout2.FchCreated) = " & oYearMonth.Month & " Then VwSellout2.Qty Else 0 End) As M" & Format(monthIdx, "00") & " ")
                    Case DTOSellOut.Formats.amounts
                        sb.AppendLine(", SUM(Case When YEAR(VwSellout2.FchCreated)= " & oYearMonth.Year & " And Month(VwSellout2.FchCreated) = " & oYearMonth.Month & " Then VwSellout2.Eur Else 0 End) As M" & Format(monthIdx, "00") & " ")
                End Select
            Next
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SQLFROM(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("FROM VwSellout2 ")
        If oSellout.User.Rol.id = DTORol.Ids.salesManager Then
            sb.AppendLine("INNER JOIN SalesManager ")
            sb.AppendLine("     ON (VwSellout2.BrandGuid = SalesManager.Brand OR VwSellout2.CategoryGuid = SalesManager.Brand) ")
            sb.AppendLine("     AND SalesManager.Channel = VwSellout2.ChannelGuid ")
            sb.AppendLine("     AND (SalesManager.Area = VwSellout2.CountryGuid OR SalesManager.Area = VwSellout2.ZonaGuid OR SalesManager.Area = VwSellout2.LocationGuid) ")
            sb.AppendLine("INNER JOIN Email_Clis ON SalesManager.Contact = Email_Clis.ContactGuid ")
            sb.AppendLine("AND Email_Clis.EmailGuid = '" & oSellout.User.Guid.ToString & "' ")
        End If
        If oSellout.Filters.Any(Function(x) x.Cod = DTOSellOut.Filter.Cods.Holding And x.Values.Count > 0) Then
            sb.AppendLine("INNER JOIN CliClient ON VwSellout2.Ccx = CliClient.Guid ")
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SQLWHERE(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        sb.Append(SqlWhereEmp(oSellout))
        sb.Append(SqlWhereYear(oSellout))
        sb.Append(SqlWhereCnaps(oSellout))
        sb.Append(SqlWhereReps(oSellout))
        sb.Append(SqlWhereProducts(oSellout))
        sb.Append(SqlWhereAreas(oSellout))
        sb.Append(SqlWhereCustomers(oSellout))
        sb.Append(SqlWhereHoldings(oSellout))
        sb.Append(SqlWhereProveidors(oSellout))
        sb.Append(SqlWhereChannels(oSellout))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SQLGROUPBY(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        Select Case oSellout.ConceptType
            Case DTOSellOut.ConceptTypes.yeas
                sb.Append("GROUP BY VwSellout2.Year ")
            Case DTOSellOut.ConceptTypes.cnaps
                sb.Append("GROUP BY VwSellout2.CnapGuid, VwSellout2.CnapId, VwSellout2.CnapNom ")
            Case DTOSellOut.ConceptTypes.channels
                sb.Append("GROUP BY VwSellout2.ChannelGuid, VwSellout2.ChannelOrd, VwSellout2.ChannelNom, VwSellout2.ChannelNomCat, VwSellout2.ChannelNomEng, VwSellout2.ChannelNomPor ")
                sb.Append(", VwSellout2.CountryGuid, VwSellout2.CountryEsp, VwSellout2.CountryCat, VwSellout2.CountryEng, VwSellout2.CountryPor ")
                sb.AppendLine(", VwSellout2.ZonaGuid, VwSellout2.ZonaNom, VwSellout2.LocationGuid, VwSellout2.LocationNom, VwSellout2.CustomerGuid, VwSellout2.CustomerNom, VwSellout2.CustomerNomCom ")
            Case DTOSellOut.ConceptTypes.geo
                sb.Append("GROUP BY VwSellout2.CountryGuid, VwSellout2.CountryEsp, VwSellout2.CountryCat, VwSellout2.CountryEng, VwSellout2.CountryPor ")
                sb.Append(", VwSellout2.ZonaGuid, VwSellout2.ZonaNom, VwSellout2.LocationGuid, VwSellout2.LocationNom, VwSellout2.CustomerGuid, VwSellout2.CustomerNom, VwSellout2.CustomerNomCom ")
            Case DTOSellOut.ConceptTypes.product
                sb.Append("GROUP BY VwSellout2.ProveidorGuid, VwSellout2.ProveidorNom ")
                sb.AppendLine(", VwSellout2.BrandGuid, VwSellout2.BrandNomEsp, VwSellout2.BrandOrd ")
                sb.AppendLine(", VwSellout2.CategoryGuid, VwSellout2.CategoryNomEsp, VwSellout2.CategoryNomCat, VwSellout2.CategoryNomEng, VwSellout2.CategoryNomPor, VwSellout2.CategoryOrd ")
                sb.AppendLine(", VwSellout2.SkuGuid, VwSellout2.SkuNomEsp, VwSellout2.SkuNomCat, VwSellout2.SkuNomEng, VwSellout2.SkuNomPor ")
                sb.AppendLine(", VwSellout2.Ean13, VwSellout2.SkuRef, VwSellout2.SkuPrvNom ")
            Case DTOSellOut.ConceptTypes.repsGeo
                sb.Append("GROUP BY VwSellout2.RepGuid, VwSellout2.RepNom ")
                sb.AppendLine(", VwSellout2.CountryGuid, VwSellout2.CountryEsp, VwSellout2.CountryCat, VwSellout2.CountryEng, VwSellout2.CountryPor ")
                sb.AppendLine(", VwSellout2.ZonaGuid, VwSellout2.ZonaNom, VwSellout2.LocationGuid, VwSellout2.LocationNom, VwSellout2.CustomerGuid, VwSellout2.CustomerNom, VwSellout2.CustomerNomCom ")
            Case DTOSellOut.ConceptTypes.repsProduct
                sb.Append("GROUP BY VwSellout2.RepGuid, VwSellout2.RepNom ")
                sb.AppendLine(", VwSellout2.BrandGuid, VwSellout2.BrandNomEsp, VwSellout2.BrandOrd ")
                sb.AppendLine(", VwSellout2.CategoryGuid, VwSellout2.CategoryNomEsp, VwSellout2.CategoryNomCat, VwSellout2.CategoryNomEng, VwSellout2.CategoryNomPor, VwSellout2.CategoryOrd ")
                sb.AppendLine(", VwSellout2.SkuGuid, VwSellout2.SkuNomEsp, VwSellout2.SkuNomCat, VwSellout2.SkuNomEng, VwSellout2.SkuNomPor ")
                sb.AppendLine(", VwSellout2.Ean13, VwSellout2.SkuRef, VwSellout2.SkuPrvNom ")
            Case DTOSellOut.ConceptTypes.centres
                sb.Append("GROUP BY VwSellout2.CustomerGuid, VwSellout2.ClientRef, VwSellout2.LocationNom ")
                sb.AppendLine(", VwSellout2.BrandGuid, VwSellout2.BrandNomEsp, VwSellout2.BrandOrd ")
                sb.AppendLine(", VwSellout2.CategoryGuid, VwSellout2.CategoryNomEsp, VwSellout2.CategoryNomCat, VwSellout2.CategoryNomEng, VwSellout2.CategoryNomPor, VwSellout2.CategoryOrd ")
                sb.AppendLine(", VwSellout2.SkuGuid, VwSellout2.SkuNomEsp, VwSellout2.SkuNomCat, VwSellout2.SkuNomEng, VwSellout2.SkuNomPor ")
                sb.AppendLine(", VwSellout2.Ean13, VwSellout2.SkuRef, VwSellout2.SkuPrvNom ")
            Case DTOSellOut.ConceptTypes.full
                Dim sCategoryNom = oSellout.Lang.Tradueix("VwSellout2.CategoryNomEsp", "VwSellout2.CategoryNomCat", "VwSellout2.CategoryNomEng", "VwSellout2.CategoryNomPor")
                Dim sSkuNom = oSellout.Lang.Tradueix("VwSellout2.SkuNomEsp", "VwSellout2.SkuNomCat", "VwSellout2.SkuNomEng", "VwSellout2.SkuNomPor")
                sb.AppendLine("GROUP BY VwSellout2.ChannelGuid, VwSellout2.ChannelNom, VwSellout2.ChannelNomCat, VwSellout2.ChannelNomEng, VwSellout2.ChannelNomPor ")
                sb.AppendLine(", VwSellout2.CountryGuid, VwSellout2.CountryEsp, VwSellout2.CountryCat, VwSellout2.CountryEng, VwSellout2.CountryPor ")
                sb.AppendLine(", VwSellout2.ZonaGuid, VwSellout2.ZonaNom, VwSellout2.LocationGuid, VwSellout2.LocationNom, VwSellout2.CustomerGuid, VwSellout2.CustomerNom, VwSellout2.CustomerNomCom, VwSellout2.ClientRef ")
                sb.AppendLine(", VwSellout2.BrandGuid, VwSellout2.BrandNomEsp ")
                sb.AppendLine(", VwSellout2.CategoryGuid, VwSellout2.CategoryNomEsp, VwSellout2.CategoryNomCat, VwSellout2.CategoryNomEng, VwSellout2.CategoryNomPor ")
                sb.AppendLine(", VwSellout2.SkuGuid, VwSellout2.SkuNomEsp, VwSellout2.SkuNomCat, VwSellout2.SkuNomEng, VwSellout2.SkuNomPor ")
                sb.AppendLine(", VwSellout2.Ean13, VwSellout2.SkuRef, VwSellout2.SkuPrvNom ")
        End Select
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SQLORDERBY(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        Select Case oSellout.ConceptType
            Case DTOSellOut.ConceptTypes.yeas
                sb.Append("ORDER BY VwSellout2.Year DESC ")
            Case DTOSellOut.ConceptTypes.cnaps
                sb.Append("ORDER BY (Case When VwSellout2.CnapGuid Is NULL Then 1 Else 0 End), VwSellout2.CnapId ")
            Case DTOSellOut.ConceptTypes.channels
                sb.Append("ORDER BY VwSellout2.ChannelOrd, VwSellout2.ChannelNom, VwSellout2.ChannelNomCat, VwSellout2.ChannelNomEng, VwSellout2.ChannelNomPor ")
                sb.Append(", VwSellout2.CountryEsp, VwSellout2.ZonaNom, VwSellout2.LocationNom, VwSellout2.CustomerNom ")
            Case DTOSellOut.ConceptTypes.geo
                sb.Append("ORDER BY VwSellout2.CountryEsp, VwSellout2.ZonaNom, VwSellout2.LocationNom, VwSellout2.CustomerNom ")
            Case DTOSellOut.ConceptTypes.product
                Dim sSkuOrd = oSellout.Lang.Tradueix("VwSellout2.SkuNomEsp", "VwSellout2.SkuNomCat", "VwSellout2.SkuNomEng", "VwSellout2.SkuNomPor")
                sb.Append("ORDER BY VwSellout2.ProveidorNom, VwSellout2.BrandOrd, VwSellout2.BrandNomEsp, VwSellout2.CategoryOrd, VwSellout2.CategoryNomEsp, " & sSkuOrd & " ")
            Case DTOSellOut.ConceptTypes.repsGeo
                sb.Append("ORDER BY VwSellout2.RepNom ")
                sb.Append(", VwSellout2.CountryEsp, VwSellout2.ZonaNom, VwSellout2.LocationNom, VwSellout2.CustomerNom ")
            Case DTOSellOut.ConceptTypes.repsProduct
                Dim sSkuOrd = oSellout.Lang.Tradueix("VwSellout2.SkuNomEsp", "VwSellout2.SkuNomCat", "VwSellout2.SkuNomEng", "VwSellout2.SkuNomPor")
                sb.Append("ORDER BY VwSellout2.RepNom ")
                sb.Append(", VwSellout2.BrandOrd, VwSellout2.BrandNomEsp, VwSellout2.CategoryOrd, VwSellout2.CategoryNomEsp, " & sSkuOrd & " ")
            Case DTOSellOut.ConceptTypes.centres
                Dim sSkuOrd = oSellout.Lang.Tradueix("VwSellout2.SkuNomEsp", "VwSellout2.SkuNomCat", "VwSellout2.SkuNomEng", "VwSellout2.SkuNomPor")
                sb.Append("ORDER BY CliNom ")
                sb.Append(", VwSellout2.BrandOrd, VwSellout2.BrandNomEsp, VwSellout2.CategoryOrd, VwSellout2.CategoryNomEsp, " & sSkuOrd & " ")
            Case DTOSellOut.ConceptTypes.full
                Dim sCategoryNom = oSellout.Lang.Tradueix("VwSellout2.CategoryNomEsp", "VwSellout2.CategoryNomCat", "VwSellout2.CategoryNomEng", "VwSellout2.CategoryNomPor")
                Dim sSkuNom = oSellout.Lang.Tradueix("VwSellout2.SkuNomEsp", "VwSellout2.SkuNomCat", "VwSellout2.SkuNomEng", "VwSellout2.SkuNomPor")
                sb.AppendLine("ORDER BY VwSellout2.ChannelNom, VwSellout2.CountryEsp, VwSellout2.ZonaNom, VwSellout2.LocationNom, VwSellout2.CustomerNom, VwSellout2.CustomerNomCom, VwSellout2.ClientRef ")
                sb.AppendLine(", VwSellout2.BrandNomEsp, " & sCategoryNom & " ")
                sb.AppendLine(", " & sSkuNom & " ")
                sb.AppendLine(", VwSellout2.Ean13, VwSellout2.SkuRef, VwSellout2.SkuPrvNom ")
        End Select
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhereChannels(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        If oSellout.Filters IsNot Nothing Then
            Dim oFilter = oSellout.Filters.FirstOrDefault(Function(x) x.Cod = DTOSellOut.Filter.Cods.Channel)
            If oFilter IsNot Nothing AndAlso oFilter.Values IsNot Nothing AndAlso oFilter.Values.Count > 0 Then
                sb.Append("AND ( ")
                For Each item In oFilter.Values
                    If Not item.Equals(oFilter.Values.First) Then
                        sb.Append("OR ")
                    End If
                    sb.Append("VwSellout2.ChannelGuid = '" & item.Guid.ToString & "' ")
                Next
                sb.Append(") ")
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhereProveidors(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        If oSellout.Filters IsNot Nothing Then
            If oSellout.Filters.Any(Function(x) x.Cod = DTOSellOut.Filter.Cods.Provider) Then
                Dim oFilter = oSellout.Filters.First(Function(x) x.Cod = DTOSellOut.Filter.Cods.Provider)
                If oFilter.Values.Count > 0 Then
                    sb.Append("AND ( ")
                    For Each item In oFilter.Values
                        If oFilter.Values.IndexOf(item) > 0 Then
                            sb.Append("OR ")
                        End If
                        sb.Append("VwSellout2.ProveidorGuid = '" & item.Guid.ToString & "' ")
                    Next
                    sb.Append(") ")
                End If
            End If
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhereCustomers(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        If oSellout.Filters IsNot Nothing Then
            Dim oFilter = oSellout.Filters.FirstOrDefault(Function(x) x.Cod = DTOSellOut.Filter.Cods.Customer)
            If oFilter IsNot Nothing AndAlso oFilter.Values IsNot Nothing AndAlso oFilter.Values.Count > 0 Then
                sb.Append("AND ( ")
                For Each item In oFilter.Values
                    If Not item.Equals(oFilter.Values.First) Then
                        sb.Append("OR ")
                    End If
                    If oSellout.GroupByHolding Then
                        sb.Append("VwSellout2.Ccx = '" & item.Guid.ToString & "' ")
                    Else
                        sb.Append("VwSellout2.CustomerGuid = '" & item.Guid.ToString & "' ")
                    End If
                Next
                sb.Append(") ")
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhereHoldings(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        If oSellout.Filters IsNot Nothing Then
            Dim oFilter = oSellout.Filters.FirstOrDefault(Function(x) x.Cod = DTOSellOut.Filter.Cods.Holding)
            If oFilter IsNot Nothing AndAlso oFilter.Values IsNot Nothing AndAlso oFilter.Values.Count > 0 Then
                sb.Append("AND ( ")
                For Each item In oFilter.Values
                    If Not item.Equals(oFilter.Values.First) Then
                        sb.Append("OR ")
                    End If
                    sb.Append("CliClient.Holding = '" & item.Guid.ToString & "' ")
                Next
                sb.Append(") ")
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhereAreas(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        If oSellout.Filters IsNot Nothing Then
            If oSellout.Filters.Any(Function(x) x.Cod = DTOSellOut.Filter.Cods.Atlas) Then
                Dim oFilter = oSellout.Filters.First(Function(x) x.Cod = DTOSellOut.Filter.Cods.Atlas)
                If oFilter.Values.Count > 0 Then
                    sb.Append("AND ( ")
                    For Each item In oFilter.Values
                        If Not item.Equals(oFilter.Values.First) Then
                            sb.Append("OR ")
                        End If
                        sb.Append("( ")
                        sb.Append("VwSellout2.CountryGuid = '" & item.Guid.ToString & "' ")
                        sb.Append("OR VwSellout2.ZonaGuid = '" & item.Guid.ToString & "' ")
                        sb.Append("OR VwSellout2.LocationGuid = '" & item.Guid.ToString & "' ")
                        'sb.Append("OR VwSellout2.ZipGuid = '" & item.Guid.ToString & "' ")
                        sb.AppendLine(") ")
                    Next
                    sb.Append(") ")
                End If
            End If
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhereProducts(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        If oSellout.Filters IsNot Nothing Then
            If oSellout.Filters.Any(Function(x) x.Cod = DTOSellOut.Filter.Cods.Product) Then
                Dim oFilter = oSellout.Filters.First(Function(x) x.Cod = DTOSellOut.Filter.Cods.Product)
                If oFilter.Values.Count > 0 Then
                    sb.Append("AND ( ")
                    For Each item In oFilter.Values
                        If Not item.Equals(oFilter.Values.First) Then
                            sb.Append("OR ")
                        End If
                        sb.Append("( ")
                        sb.Append("VwSellout2.BrandGuid = '" & item.Guid.ToString & "' ")
                        sb.Append("OR VwSellout2.CategoryGuid = '" & item.Guid.ToString & "' ")
                        sb.Append("OR VwSellout2.SkuGuid = '" & item.Guid.ToString & "' ")
                        sb.AppendLine(") ")
                    Next
                    sb.Append(") ")
                End If
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhereReps(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        If oSellout.Filters IsNot Nothing Then
            Dim oFilter = oSellout.Filters.FirstOrDefault(Function(x) x.Cod = DTOSellOut.Filter.Cods.Rep)
            If oFilter IsNot Nothing AndAlso oFilter.Values IsNot Nothing AndAlso oFilter.Values.Count > 0 Then
                sb.Append("AND ( ")
                For Each item In oFilter.Values
                    If Not item.Equals(oFilter.Values.First) Then
                        sb.Append("OR ")
                    End If
                    sb.Append("VwSellout2.RepGuid = '" & item.Guid.ToString & "' ")
                Next
                sb.Append(") ")
            End If
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhereCnaps(oSellout As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        If oSellout.Filters IsNot Nothing Then
            Dim oFilter = oSellout.Filters.FirstOrDefault(Function(x) x.Cod = DTOSellOut.Filter.Cods.CNap)
            If oFilter IsNot Nothing AndAlso oFilter.Values IsNot Nothing AndAlso oFilter.Values.Count > 0 Then
                sb.Append("AND ( ")
                For Each item As DTOBaseGuid In oFilter.Values
                    If Not item.Equals(oFilter.Values.First) Then
                        sb.Append("OR ")
                    End If
                    sb.Append("VwSellout2.CnapId LIKE '" & DirectCast(item, DTOCnap).Id & "%' ")
                Next
                sb.Append(") ")
            End If
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhereYear(oSellout As DTOSellOut) As String
        Dim DtFchFrom As Date = oSellout.YearMonths.First.FchFrom
        Dim DtFchTo As Date = oSellout.YearMonths.Last.FchTo
        Dim sb As New Text.StringBuilder
        If oSellout.ConceptType <> DTOSellOut.ConceptTypes.yeas Then
            sb.Append("AND (VwSellout2.FchCreated ")
            sb.Append("BETWEEN '" & Format(DtFchFrom, "yyyyMMdd") & "' ")
            sb.Append("AND  '" & Format(DtFchTo, "yyyyMMdd") & "' ")
            sb.AppendLine(") ")
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhereEmp(oSellOut As DTOSellOut) As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("WHERE VwSellout2.Emp=" & oSellOut.User.Emp.Id & " ")
        sb.AppendLine("AND VwSellout2.Cod=" & DTOPurchaseOrder.Codis.client & " ")
        sb.AppendLine("AND VwSellout2.IsBundle=" & IIf(oSellOut.IsBundle, "1", "0") & " ")
        sb.AppendLine("AND VwSellout2.CustomerGuid <> '" & oSellOut.User.Emp.Org.Guid.ToString & "' ")
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function Years(oSellout As DTOSellOut) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Year(VwSellout2.FchCreated) AS Yea ")
        sb.AppendLine("FROM VwSellout2 ")
        sb.AppendLine(SqlWhereEmp(oSellout))
        sb.AppendLine("GROUP BY Year(VwSellout2.FchCreated) ")
        sb.AppendLine("ORDER BY Year(VwSellout2.FchCreated) DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As Integer = oDrd("Yea")
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Cnaps(oSellout As DTOSellOut) As List(Of DTOCnap)
        Dim retval As New List(Of DTOCnap)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSellout2.CnapGuid, VwSellout2.id, VwSellout2.CnapNom ")
        sb.AppendLine("FROM VwSellout2 ")
        sb.AppendLine("WHERE " & SQLWHERE(oSellout) & " ")
        sb.AppendLine("GROUP BY VwSellout2.CnapGuid, VwSellout2.id, VwSellout2.CnapNom ")
        sb.AppendLine("ORDER BY VwSellout2.id ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCnap(oDrd("CnapGuid"))
            With item
                .Id = oDrd("CnapId")
                .NomLong = SQLHelper.GetLangTextFromDataReader(oDrd, "CnapNom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Reps(ByVal oSellout As DTOSellOut) As List(Of DTORep)
        Dim retval As New List(Of DTORep)
        Dim pSellout = oSellout.Clone
        pSellout.ClearFilter(DTOSellOut.Filter.Cods.Rep)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSellout2.RepGuid, VwSellout2.RepNom ")
        sb.AppendLine("FROM VwSellout2 ")
        sb.AppendLine(SQLWHERE(pSellout))
        sb.AppendLine("GROUP BY VwSellout2.RepGuid, VwSellout2.RepNom ")
        sb.AppendLine("ORDER BY VwSellout2.RepNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTORep = Nothing
            If IsDBNull(oDrd("RepGuid")) Then
                item = New DTORep(Guid.Empty)
                item.Nom = "(s/rep)"
            Else
                item = New DTORep(oDrd("RepGuid"))
                item.Nom = SQLHelper.GetStringFromDataReader(oDrd("RepNom"))
            End If
            retval.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function

    Shared Function Proveidors(ByVal oSellout As DTOSellOut) As List(Of DTOProveidor)
        Dim retval As New List(Of DTOProveidor)
        Dim pSellout = oSellout.Clone
        pSellout.ClearFilter(DTOSellOut.Filter.Cods.Provider)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSellout2.ProveidorGuid, VwSellout2.ProveidorNom ")
        sb.AppendLine("FROM VwSellout2 ")
        sb.AppendLine(SQLWHERE(pSellout))
        sb.AppendLine("GROUP BY VwSellout2.ProveidorGuid, VwSellout2.ProveidorNom ")
        sb.AppendLine("ORDER BY VwSellout2.ProveidorNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read
            Dim item As DTOProveidor = Nothing
            If IsDBNull(oDrd("ProveidorGuid")) Then
                item = New DTOProveidor(Guid.Empty)
                item.Nom = "(s/proveidor)"
            Else
                item = New DTOProveidor(oDrd("ProveidorGuid"))
                item.Nom = SQLHelper.GetStringFromDataReader(oDrd("ProveidorNom"))
            End If
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Countries(ByVal oSellout As DTOSellOut) As List(Of DTOCountry)
        Dim retval As New List(Of DTOCountry)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSellout2.CountryGuid, VwSellout2.CountryEsp, VwSellout2.CountryCat, VwSellout2.CountryEng, VwSellout2.CountryPor ")
        sb.AppendLine(", VwSellout2.ZonaGuid, VwSellout2.ZonaNom ")
        sb.AppendLine(", VwSellout2.LocationGuid, VwSellout2.LocationNom ")
        sb.AppendLine("FROM VwSellout2 ")
        sb.AppendLine(SQLWHERE(oSellout))
        sb.AppendLine("GROUP BY VwSellout2.CountryGuid, VwSellout2.CountryEsp, VwSellout2.CountryCat, VwSellout2.CountryEng, VwSellout2.CountryPor ")
        sb.AppendLine(", VwSellout2.ZonaGuid, VwSellout2.ZonaNom ")
        sb.AppendLine(", VwSellout2.LocationGuid, VwSellout2.LocationNom ")
        Dim SQL As String = sb.ToString
        Dim oCountry As New DTOCountry
        Dim oZona As New DTOZona
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                oCountry = New DTOCountry(oDrd("CountryGuid"))
                With oCountry
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp", "CountryCat", "CountryEng", "CountryPor")
                    .Zonas = New List(Of DTOZona)
                End With
                retval.Add(oCountry)
            End If

            If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then
                oZona = New DTOZona(oDrd("ZonaGuid"))
                oZona.Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                oZona.Locations = New List(Of DTOLocation)
                oCountry.Zonas.Add(oZona)
            End If

            Dim oLocation As New DTOLocation(oDrd("LocationGuid"))
            oLocation.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
            oZona.Locations.Add(oLocation)
        Loop
        oDrd.Close()
        retval = retval.OrderBy(Function(x) x.LangNom.Tradueix(oSellout.Lang)).ToList()
        Return retval
    End Function

    Shared Function DistributionChannels(ByVal oSellout As DTOSellOut) As List(Of DTODistributionChannel)
        Dim retval As New List(Of DTODistributionChannel)
        Dim pSellout = oSellout.Clone
        pSellout.ClearFilter(DTOSellOut.Filter.Cods.Channel)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSellout2.ChannelGuid, VwSellout2.ChannelNom, VwSellout2.ChannelNomCat, VwSellout2.ChannelNomEng, VwSellout2.ChannelNomPor ")
        sb.AppendLine("FROM VwSellout2 ")
        sb.AppendLine(SQLWHERE(pSellout))
        sb.AppendLine("GROUP BY VwSellout2.ChannelGuid, VwSellout2.ChannelNom, VwSellout2.ChannelNomCat, VwSellout2.ChannelNomEng, VwSellout2.ChannelNomPor, VwSellout2.ChannelOrd ")
        sb.AppendLine("ORDER BY VwSellout2.ChannelOrd, VwSellout2.ChannelNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTODistributionChannel = Nothing
            If IsDBNull(oDrd("ChannelGuid")) Then
                item = New DTODistributionChannel(Guid.Empty)
                item.LangText.Esp = "(s/canal)"
            Else
                item = New DTODistributionChannel(oDrd("ChannelGuid"))
                item.LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ChannelNom", "ChannelNomCat", "ChannelNomEng", "ChannelNomPor")
            End If
            retval.Add(item)

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Brands(ByVal oSellOut As DTOSellOut) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSellout2.BrandGuid, VwSellout2.BrandNom ")
        sb.AppendLine(", VwSellout2.CategoryGuid, VwSellout2.CategoryNomEsp, VwSellout2.CategoryNomCat, VwSellout2.CategoryNomEng, VwSellout2.CategoryNomPor ")
        sb.AppendLine("FROM VwSellout2 ")
        sb.AppendLine(SQLWHERE(oSellOut))
        sb.AppendLine("GROUP BY VwSellout2.BrandOrd, VwSellout2.BrandGuid, VwSellout2.BrandNom ")
        sb.AppendLine(", VwSellout2.CategoryOrd, VwSellout2.CategoryGuid, VwSellout2.CategoryNomEsp, VwSellout2.CategoryNomCat, VwSellout2.CategoryNomEng, VwSellout2.CategoryNomPor ")
        sb.AppendLine("ORDER BY VwSellout2.BrandOrd, VwSellout2.BrandNomEsp, VwSellout2.BrandGuid ")
        sb.AppendLine(", VwSellout2.CategoryOrd, VwSellout2.CategoryNomEsp, VwSellout2.CategoryGuid ")
        Dim SQL As String = sb.ToString
        Dim oBrand As New DTOProductBrand
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                With oBrand
                    SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                    .Categories = New List(Of DTOProductCategory)
                End With
                retval.Add(oBrand)
            End If
            Dim item As New DTOProductCategory(oDrd("CategoryGuid"))
            With item
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "CategoryNom", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
            End With
            oBrand.Categories.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function RawDataLast12Months(oProveidor As DTOProveidor) As List(Of DTOCustomerProduct)
        'product sold x month and customer (csv for Guillermo)
        Dim retval As New List(Of DTOCustomerProduct)

        Dim DtFch As New Date(DTO.GlobalVariables.Today().AddMonths(-12).Year, DTO.GlobalVariables.Today().AddMonths(-12).Month, 1)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Year(Pdc.FchCreated) AS Year, Month(Pdc.FchCreated) AS Month ")
        sb.AppendLine(", Country.Guid AS CountryGuid, Country.Nom_Esp AS CountryEsp, Country.Nom_Cat AS CountryCat, Country.Nom_Eng AS CountryEng, Country.Nom_Por AS CountryPor ")
        sb.AppendLine(", Zona.Guid AS ZonaGuid, Zona.Nom AS ZonaNom ")
        sb.AppendLine(", Location.Guid AS LocationGuid, Location.Nom AS LocationNom ")
        sb.AppendLine(", Zip.Guid AS ZipGuid ")
        sb.AppendLine(", Pdc.CliGuid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", Pnc.ArtGuid, Art.Ref, (CASE WHEN Art.RefPrv>'' THEN Art.RefPrv ELSE VwSkuNom.SkuNomLlargEsp END) AS SkuNom ")
        sb.AppendLine(", SUM(Pnc.Qty) AS Qty ")

        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category=Stp.Guid ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand = Tpa.Guid ")
        sb.AppendLine("INNER JOIN Pnc ON Art.Guid = Pnc.ArtGuid ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
        sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
        sb.AppendLine("INNER JOIN Location ON Zip.Location=Location.Guid ")
        sb.AppendLine("INNER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country=Country.Guid ")

        sb.AppendLine("WHERE Tpa.Proveidor='" & oProveidor.Guid.ToString & "' ")
        sb.AppendLine("And Pdc.Cod=2 ")
        sb.AppendLine(" And Stp.Codi < 2 ")
        sb.AppendLine(" And CliGral.Rol <> " & CInt(DTORol.Ids.comercial) & " ")
        sb.AppendLine(" And CliGral.Rol <> " & CInt(DTORol.Ids.rep) & " ")
        sb.AppendLine(" And Pdc.FchCreated>= '" & Format(DtFch, "yyyyMMdd") & "' ")

        sb.AppendLine("GROUP BY Year(Pdc.FchCreated), Month(Pdc.FchCreated) ")
        sb.AppendLine(", Country.Guid, Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por ")
        sb.AppendLine(", Zona.Guid, Zona.Nom, Location.Guid, Location.Nom, Zip.Guid ")
        sb.AppendLine(", Pdc.CliGuid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", Pnc.ArtGuid, Art.Ref, Art.RefPrv, VwSkuNom.SkuNomLlargEsp ")

        sb.AppendLine("ORDER BY Country.Nom_Eng, Zona.Nom, Location.Nom, CliGral.RaoSocial ")
        sb.AppendLine(", Art.Ref, Art.RefPrv, Pnc.ArtGuid, Year(Pdc.FchCreated), Month(Pdc.FchCreated)")

        Dim oCountry As New DTOCountry
        Dim oZona As New DTOZona
        Dim oLocation As New DTOLocation
        Dim oZip As New DTOZip
        Dim oCustomer As New DTOCustomer

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCustomer.Guid.Equals(oDrd("CliGuid")) Then

                If Not oZip.Guid.Equals(oDrd("ZipGuid")) Then
                    If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then
                        If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then
                            If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                                oCountry = New DTOCountry(oDrd("CountryGuid"))
                                oCountry.LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp", "CountryCat", "CountryEng", "CountryPor")
                            End If
                            oZona = New DTOZona(oDrd("ZonaGuid"))
                            With oZona
                                .Country = oCountry
                                .Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                            End With
                        End If
                        oLocation = New DTOLocation(oDrd("LocationGuid"))
                        With oLocation
                            .Zona = oZona
                            .Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                        End With
                    End If
                    oZip = New DTOZip(oDrd("ZipGuid"))
                    oZip.Location = oLocation
                End If

                oCustomer = New DTOCustomer(oDrd("CliGuid"))
                With oCustomer
                    .Nom = oDrd("RaoSocial")
                    .NomComercial = oDrd("NomCom")
                    .Address = New DTOAddress()
                    .Address.Zip = oZip
                End With
            End If

            Dim oSku As New DTOProductSku(oDrd("ArtGuid"))
            With oSku
                .RefProveidor = oDrd("Ref")
                .NomProveidor = oDrd("SkuNom")
            End With

            Dim item As New DTOCustomerProduct
            With item
                .Customer = oCustomer
                .Sku = oSku
                .YearMonth = New DTOYearMonth(oDrd("Year"), oDrd("Month"))
                .Qty = oDrd("Qty")
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
