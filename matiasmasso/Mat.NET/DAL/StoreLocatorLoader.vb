Public Class StoreLocatorLoader

    Shared Function Fetch(product As DTOProduct, oLang As DTOLang, IncludeBlocked As Boolean) As DTOStoreLocator3
        Dim retval As New DTOStoreLocator3

        Dim sCountryNom As String = oLang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng", "Country.Nom_Por")
        Dim sProductGuid As String = product.Guid.ToString

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Web.Client, Web.Nom, Web.Adr, Web.Tel, Web.Raffles ")
        sb.AppendLine(", Web.Country, " & sCountryNom & " AS CountryNom ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod ")
        sb.AppendLine(", SUM(Web.Val) AS Sales, SUM(ValHistoric) AS SalesHistoric, SUM(CASE WHEN SalePointsCount>0 THEN Web.SumCcxVal/Web.SalePointsCount ELSE 0 END) AS SalesCcx ")
        sb.AppendLine(", MAX(Web.LastFch) AS LastFch ")
        sb.AppendLine(", Web.Obsoleto, Web.Impagat, Web.Blocked ")

        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN Country ON Web.Country=Country.Guid ")

        Dim oPremiumLine = PremiumLineLoader.FromProduct(product)
        If oPremiumLine Is Nothing Then
            sb.AppendLine("AND (Web.Category ='" & sProductGuid & "' OR Web.Brand ='" & sProductGuid & "') ")
        Else
            sb.AppendLine(" AND Web.PremiumLine = '" & oPremiumLine.Guid.ToString() & "' ")
        End If

        If Not IncludeBlocked Then
            sb.AppendLine("WHERE Web.Obsoleto = 0 ")
            sb.AppendLine("AND Web.Impagat = 0 ")
            sb.AppendLine("AND Web.Blocked = 0 ")
        End If

        sb.AppendLine("GROUP BY Web.Client, Web.Nom, Web.Adr, Web.Tel, Web.Raffles ")
        sb.AppendLine(", Web.Country, " & sCountryNom & " ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod, Web.ConsumerPriority ")
        sb.AppendLine(", Web.Obsoleto, Web.Impagat, Web.Blocked ")

        sb.AppendLine("ORDER BY CountryNom, Web.Country, Web.AreaNom, Web.AreaGuid, Web.Cit, Web.Location ")
        sb.AppendLine(", Web.Obsoleto, Web.Impagat, Web.Blocked ")
        sb.AppendLine(", Web.ConsumerPriority, SUM(Web.Val) DESC, SUM(Web.ValHistoric) DESC, SUM(CASE WHEN SalePointsCount>0 THEN Web.SumCcxVal/Web.SalePointsCount ELSE 0 END) DESC")
        Dim oCountry As New DTOStoreLocator3.Country
        Dim oZona As New DTOStoreLocator3.Zona
        Dim oLocation As New DTOStoreLocator3.Location

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("Country")) Then
                oCountry = New DTOStoreLocator3.Country(oDrd("Country"))
                oCountry.Nom = SQLHelper.GetStringFromDataReader(oDrd("CountryNom"))
                retval.Offline.Countries.Add(oCountry)
            End If

            If Not oZona.Guid.Equals(oDrd("AreaGuid")) Then
                oZona = New DTOStoreLocator3.Zona(oDrd("AreaGuid"))
                oZona.Nom = oDrd("AreaNom")
                oCountry.Zonas.Add(oZona)
            End If

            If Not oLocation.Guid.Equals(oDrd("Location")) Then
                oLocation = New DTOStoreLocator3.Location(oDrd("Location"))
                oLocation.Nom = oDrd("Cit")
                oZona.Locations.Add(oLocation)
            End If

            Dim oDistributor As New DTOStoreLocator3.Distributor(oDrd("Client"))
            With oDistributor
                .Nom = oDrd("Nom")
                .Adr = oDrd("Adr")
                .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                .Sales = SQLHelper.GetDecimalFromDataReader(oDrd("Sales"))
                .SalesHistoric = SQLHelper.GetDecimalFromDataReader(oDrd("SalesHistoric"))
                .SalesCcx = SQLHelper.GetDecimalFromDataReader(oDrd("SalesCcx"))
                .LastFch = SQLHelper.GetFchFromDataReader(oDrd("LastFch"))

                If IncludeBlocked Then
                    Dim blImpagat As Boolean = oDrd("Impagat")
                    Dim blObsoleto As Boolean = oDrd("Obsoleto")
                    Dim blBlocked As Boolean = oDrd("Blocked")
                    If blImpagat Then
                        .Status = DTOStoreLocator3.Distributor.Statuses.Impagat
                    ElseIf blBlocked Then
                        .Status = DTOStoreLocator3.Distributor.Statuses.Blocked
                    ElseIf blObsoleto Then
                        .Status = DTOStoreLocator3.Distributor.Statuses.Obsolet
                    Else
                        .Status = DTOStoreLocator3.Distributor.Statuses.Success
                    End If
                Else
                    .Status = DTOStoreLocator3.Distributor.Statuses.Success
                End If
            End With
            oLocation.Distributors.Add(oDistributor)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Fetch(oPremiumLine As DTOPremiumLine, oLang As DTOLang) As DTOStoreLocator3
        Dim retval As New DTOStoreLocator3

        Dim sCountryNom As String = oLang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng", "Country.Nom_Por")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Web.Client, Web.Nom, Web.Adr, Web.Tel, Web.Raffles ")
        sb.AppendLine(", Web.Country, " & sCountryNom & " AS CountryNom ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod ")
        sb.AppendLine(", SUM(Web.Val) AS Sales, SUM(ValHistoric) AS SalesHistoric, SUM(CASE WHEN SalePointsCount>0 THEN Web.SumCcxVal/Web.SalePointsCount ELSE 0 END) AS SalesCcx ")
        sb.AppendLine(", MAX(Web.LastFch) AS LastFch ")

        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN Country ON Web.Country=Country.Guid ")
        sb.AppendLine("WHERE Web.Obsoleto = 0 ")
        sb.AppendLine("AND Web.Impagat = 0 ")
        sb.AppendLine("AND Web.Blocked = 0 ")

        sb.AppendLine(" AND Web.PremiumLine = '" & oPremiumLine.Guid.ToString() & "' ")

        sb.AppendLine("GROUP BY Web.Client, Web.Nom, Web.Adr, Web.Tel, Web.Raffles ")
        sb.AppendLine(", Web.Country, " & sCountryNom & " ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod, Web.ConsumerPriority ")

        sb.AppendLine("ORDER BY CountryNom, Web.Country, Web.AreaNom, Web.AreaGuid, Web.Cit, Web.Location ")
        sb.AppendLine(", Web.ConsumerPriority, SUM(Web.Val) DESC, SUM(Web.ValHistoric) DESC, SUM(CASE WHEN SalePointsCount>0 THEN Web.SumCcxVal/Web.SalePointsCount ELSE 0 END) DESC")
        Dim oCountry As New DTOStoreLocator3.Country
        Dim oZona As New DTOStoreLocator3.Zona
        Dim oLocation As New DTOStoreLocator3.Location

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("Country")) Then
                oCountry = New DTOStoreLocator3.Country(oDrd("Country"))
                oCountry.Nom = SQLHelper.GetStringFromDataReader(oDrd("CountryNom"))
                retval.Offline.Countries.Add(oCountry)
            End If

            If Not oZona.Guid.Equals(oDrd("AreaGuid")) Then
                oZona = New DTOStoreLocator3.Zona(oDrd("AreaGuid"))
                oZona.Nom = oDrd("AreaNom")
                oCountry.Zonas.Add(oZona)
            End If

            If Not oLocation.Guid.Equals(oDrd("Location")) Then
                oLocation = New DTOStoreLocator3.Location(oDrd("Location"))
                oLocation.Nom = oDrd("Cit")
                oZona.Locations.Add(oLocation)
            End If

            Dim oDistributor As New DTOStoreLocator3.Distributor(oDrd("Client"))
            With oDistributor
                .Nom = oDrd("Nom")
                .Adr = oDrd("Adr")
                .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                .Sales = SQLHelper.GetDecimalFromDataReader(oDrd("Sales"))
                .SalesHistoric = SQLHelper.GetDecimalFromDataReader(oDrd("SalesHistoric"))
                .SalesCcx = SQLHelper.GetDecimalFromDataReader(oDrd("SalesCcx"))
                .LastFch = SQLHelper.GetFchFromDataReader(oDrd("LastFch"))
            End With
            oLocation.Distributors.Add(oDistributor)

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Fetch(oRaffle As DTORaffle, Optional oLang As DTOLang = Nothing) As DTOStoreLocator3
        Dim retval As New DTOStoreLocator3
        RaffleLoader.Load(oRaffle)
        'Dim sCountryNom As String = oLang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng", "Country.Nom_Por")
        'Dim sProductGuid As String = oRaffle.Brand.Guid.ToString

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Web.Client, Web.Nom, Web.Adr, Web.Tel ")
        sb.AppendLine(", Web.Country, Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod ")
        sb.AppendLine(", SUM(Web.Val) AS Sales, SUM(ValHistoric) AS SalesHistoric, SUM(CASE WHEN SalePointsCount>0 THEN Web.SumCcxVal/Web.SalePointsCount ELSE 0 END) AS SalesCcx ")
        sb.AppendLine(", MAX(Web.LastFch) AS LastFch ")
        sb.AppendLine(", Sorteos.Lang ")
        sb.AppendLine(", Tpa.WebAtlasRafflesDeadline ")
        sb.AppendLine("FROM Sorteos ")
        sb.AppendLine("INNER JOIN VwProductParent ON Sorteos.Art = VwProductParent.Child ")
        sb.AppendLine("INNER JOIN Web ON VwProductParent.Parent = Web.Brand AND Sorteos.Country = Web.Country ")
        sb.AppendLine("INNER JOIN Country ON Web.Country=Country.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Tpa ON Web.Brand=Tpa.Guid ")
        sb.AppendLine("WHERE Sorteos.Guid = '" & oRaffle.Guid.ToString() & "' ")
        sb.AppendLine("AND Web.Country = Sorteos.Country ")
        sb.AppendLine("AND Web.Obsoleto = 0 ")
        sb.AppendLine("AND Web.Impagat = 0 ")
        sb.AppendLine("AND Web.Blocked = 0 ")
        sb.AppendLine("AND Web.Raffles = 1 ")

        Dim oPremiumLine = PremiumLineLoader.FromProduct(oRaffle.Brand)
        If oPremiumLine IsNot Nothing Then
            sb.AppendLine(" AND Web.PremiumLine = '" & oPremiumLine.Guid.ToString() & "' ")
        End If

        sb.AppendLine("GROUP BY Web.Client, Web.Nom, Web.Adr, Web.Tel ")
        sb.AppendLine(", Web.Country, Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod, Web.ConsumerPriority, Sorteos.Lang ")
        sb.AppendLine(", Tpa.WebAtlasRafflesDeadline ")

        sb.AppendLine("ORDER BY Country.Nom_Esp, Web.Country, Web.AreaNom, Web.AreaGuid, Web.Cit, Web.Location ")
        sb.AppendLine(", Web.ConsumerPriority, SUM(Web.Val) DESC, SUM(Web.ValHistoric) DESC, SUM(CASE WHEN SalePointsCount>0 THEN Web.SumCcxVal/Web.SalePointsCount ELSE 0 END) DESC")
        Dim oCountry As New DTOStoreLocator3.Country
        Dim oZona As New DTOStoreLocator3.Zona
        Dim oLocation As New DTOStoreLocator3.Location

        Dim isExcluded As Boolean = False
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim lastOrdersWithinDeadline As Boolean = True
            Dim lastFch = SQLHelper.GetFchFromDataReader(oDrd("LastFch"))
            If Not IsDBNull(oDrd("WebAtlasRafflesDeadline")) Then
                Dim deadline = DTO.GlobalVariables.Today().AddDays(-oDrd("WebAtlasRafflesDeadline"))
                If lastFch < deadline Then lastOrdersWithinDeadline = False
            End If

            If lastOrdersWithinDeadline Then
                If Not oCountry.Guid.Equals(oDrd("Country")) Then
                    Dim oLangTextNom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom_Esp", "Nom_Cat", "Nom_Eng", "Nom_Por")
                    If oLang Is Nothing Then oLang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                    oCountry = New DTOStoreLocator3.Country(oDrd("Country"))
                    oCountry.Nom = oLangTextNom.Tradueix(oLang)
                    'oCountry.Nom = SQLHelper.GetStringFromDataReader(oDrd("CountryNom"))
                    retval.Offline.Countries.Add(oCountry)
                End If

                If Not oZona.Guid.Equals(oDrd("AreaGuid")) Then
                    oZona = New DTOStoreLocator3.Zona(oDrd("AreaGuid"))
                    isExcluded = oZona.IsExcluded()
                    If Not isExcluded Then
                        oZona.Nom = oDrd("AreaNom")
                        oCountry.Zonas.Add(oZona)
                    End If
                End If
                If Not isExcluded Then
                    If Not oLocation.Guid.Equals(oDrd("Location")) Then
                        oLocation = New DTOStoreLocator3.Location(oDrd("Location"))
                        oLocation.Nom = oDrd("Cit")
                        oZona.Locations.Add(oLocation)
                    End If

                    Dim oDistributor As New DTOStoreLocator3.Distributor(oDrd("Client"))
                    With oDistributor
                        .Nom = oDrd("Nom")
                        .Adr = oDrd("Adr")
                        .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                        .Sales = SQLHelper.GetDecimalFromDataReader(oDrd("Sales"))
                        .SalesHistoric = SQLHelper.GetDecimalFromDataReader(oDrd("SalesHistoric"))
                        .SalesCcx = SQLHelper.GetDecimalFromDataReader(oDrd("SalesCcx"))
                        .LastFch = SQLHelper.GetFchFromDataReader(oDrd("LastFch"))
                    End With
                    oLocation.Distributors.Add(oDistributor)
                End If

            End If
        Loop
        oDrd.Close()
        Return retval
    End Function




    Shared Function Distributors(oLang As DTOLang, Optional product As DTOProduct = Nothing, Optional oArea As DTOArea = Nothing, Optional proveidor As DTOProveidor = Nothing, Optional includeItems As Boolean = False) As List(Of DTOProductDistributor)
        Dim retval As New List(Of DTOProductDistributor)

        Dim sCountryNom As String = oLang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng", "Country.Nom_Por")
        Dim sProductGuid As String = product.Guid.ToString

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Web.Client, Web.Nom, Web.Adr, Web.Tel, Web.Raffles ")
        sb.AppendLine(", Web.Country, " & sCountryNom & " AS CountryNom, Country.ISO AS CountryISO, Country.PrefixeTelefonic ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod ")
        sb.AppendLine(", SUM(Web.Val) AS Sales, SUM(ValHistoric) AS SalesHistoric, SUM(CASE WHEN SalePointsCount>0 THEN Web.SumCcxVal/Web.SalePointsCount ELSE 0 END) AS SalesCcx ")
        sb.AppendLine(", MAX(Web.LastFch) AS LastFch ")
        If includeItems Then
            sb.AppendLine(", Web.SkuRef ")
        End If

        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN Country ON Web.Country=Country.Guid ")
        sb.AppendLine("WHERE Web.Obsoleto = 0 ")
        sb.AppendLine("AND Web.Impagat = 0 ")
        sb.AppendLine("AND Web.Blocked = 0 ")

        If product IsNot Nothing Then
            Dim oPremiumLine = PremiumLineLoader.FromProduct(product)
            If oPremiumLine Is Nothing Then
                sb.AppendLine("AND (Web.Category ='" & sProductGuid & "' OR Web.Brand ='" & sProductGuid & "') ")
            Else
                sb.AppendLine(" AND Web.PremiumLine = '" & oPremiumLine.Guid.ToString() & "' ")
            End If
        End If

        If oArea IsNot Nothing Then
            Dim sAreaGuid As String = oArea.Guid.ToString
            sb.AppendLine("AND (Web.Country='" & sAreaGuid & "' OR Web.AreaGuid ='" & sAreaGuid & "' OR Web.Location ='" & sAreaGuid & "') ")
        End If

        If proveidor IsNot Nothing Then
            sb.AppendLine("AND Web.Proveidor = '" & proveidor.Guid.ToString & "' ")
            If includeItems Then
                sb.AppendLine("AND Web.SkuRef >'' ")
            End If
        End If

        sb.AppendLine("GROUP BY Web.Client, Web.Nom, Web.Adr, Web.Tel, Web.Raffles ")
        sb.AppendLine(", Web.Country, " & sCountryNom & ", Country.ISO, Country.PrefixeTelefonic ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod, Web.ConsumerPriority ")
        If includeItems Then
            sb.AppendLine(", Web.SkuRef ")
        End If

        sb.AppendLine("ORDER BY CountryNom, Web.Country, Web.AreaNom, Web.AreaGuid, Web.Cit, Web.Location ")
        sb.AppendLine(", Web.ConsumerPriority, SUM(Web.Val) DESC, SUM(Web.ValHistoric) DESC, SUM(CASE WHEN SalePointsCount>0 THEN Web.SumCcxVal/Web.SalePointsCount ELSE 0 END) DESC")
        If includeItems Then
            sb.AppendLine(", Web.SkuRef ")
        End If

        Dim oCountry As New DTOArea
        Dim oZona As New DTOArea
        Dim pLocation As New DTOArea
        Dim oProductDistributor As New DTOProductDistributor

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("Country")) Then
                oCountry = New DTOCountry(oDrd("Country"))
                oCountry.nom = SQLHelper.GetStringFromDataReader(oDrd("CountryNom"))
                CType(oCountry, DTOCountry).ISO = SQLHelper.GetStringFromDataReader(oDrd("CountryISO"))
                CType(oCountry, DTOCountry).prefixeTelefonic = SQLHelper.GetStringFromDataReader(oDrd("PrefixeTelefonic"))
            End If

            If Not oZona.Guid.Equals(oDrd("AreaGuid")) Then
                oZona = DTOArea.Factory(oDrd("AreaGuid"), DTOArea.Cods.Zona, oDrd("AreaNom"))
            End If

            If Not pLocation.Guid.Equals(oDrd("Location")) Then
                pLocation = DTOArea.Factory(oDrd("Location"), DTOArea.Cods.location, oDrd("Cit"))
            End If
            If Not oProductDistributor.Guid.Equals(oDrd("Client")) Then
                oProductDistributor = New DTOProductDistributor(oDrd("Client"))
                With oProductDistributor
                    .Nom = oDrd("Nom")
                    .Adr = oDrd("Adr")
                    .ZipCod = SQLHelper.GetStringFromDataReader(oDrd("ZipCod"))
                    .Country = oCountry
                    .Zona = oZona
                    .Location = pLocation
                    If Not IsDBNull(oDrd("Tel")) Then
                        Dim sTelNum As String = oDrd("Tel")
                        .Tel = New DTOContactTel()
                        .Tel.Country = oCountry
                        .Tel.value = sTelNum
                    End If

                    .Sales = SQLHelper.GetDecimalFromDataReader(oDrd("Sales"))
                    .SalesHistoric = SQLHelper.GetDecimalFromDataReader(oDrd("SalesHistoric"))
                    .SalesCcx = SQLHelper.GetDecimalFromDataReader(oDrd("SalesCcx"))

                    .LastFch = SQLHelper.GetFchFromDataReader(oDrd("LastFch"))
                    .Raffles = SQLHelper.GetBooleanFromDatareader(oDrd("Raffles"))
                    If includeItems Then
                        .Items = New List(Of String)
                    End If
                End With
                retval.Add(oProductDistributor)
            End If

            If includeItems Then
                Dim ref As String = SQLHelper.GetStringFromDataReader(oDrd("SkuRef"))
                If ref > "" Then
                    oProductDistributor.Items.Add(ref)
                End If
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Distributors(oProveidor As DTOProveidor, oLang As DTOLang, Optional includeItems As Boolean = False) As List(Of DTOProductDistributor)
        Dim retval As New List(Of DTOProductDistributor)

        Dim sCountryNom As String = oLang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng", "Country.Nom_Por")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Web.Client, Web.Nom, Web.Adr, Web.Tel, Web.Raffles ")
        sb.AppendLine(", Web.Country, " & sCountryNom & " AS CountryNom, Country.ISO AS CountryISO, Country.PrefixeTelefonic ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod ")
        sb.AppendLine(", CliAdr.Longitud, CliAdr.Latitud ")
        sb.AppendLine(", SUM(Web.Val) AS Sales, SUM(ValHistoric) AS SalesHistoric, SUM(CASE WHEN SalePointsCount>0 THEN Web.SumCcxVal/Web.SalePointsCount ELSE 0 END) AS SalesCcx ")
        sb.AppendLine(", MAX(Web.LastFch) AS LastFch ")
        If includeItems Then
            sb.AppendLine(", Web.SkuRef ")
        End If

        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN CliAdr ON Web.Client=CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
        sb.AppendLine("INNER JOIN Country ON Web.Country=Country.Guid ")
        sb.AppendLine("WHERE Web.Obsoleto = 0 ")
        sb.AppendLine("AND Web.Impagat = 0 ")
        sb.AppendLine("AND Web.Blocked = 0 ")
        sb.AppendLine("AND Web.Proveidor = '" & oProveidor.Guid.ToString & "' ")

        If includeItems Then
            sb.AppendLine("AND Web.SkuRef >'' ")
        End If

        sb.AppendLine("GROUP BY Web.Client, Web.Nom, Web.Adr, Web.Tel, Web.Raffles ")
        sb.AppendLine(", Web.Country, " & sCountryNom & ", Country.ISO, Country.PrefixeTelefonic ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod, Web.ConsumerPriority ")
        sb.AppendLine(", CliAdr.Longitud, CliAdr.Latitud ")

        If includeItems Then
            sb.AppendLine(", Web.SkuRef ")
        End If

        sb.AppendLine("ORDER BY CountryNom, Web.Country, Web.AreaNom, Web.AreaGuid, Web.Cit, Web.Location ")
        If includeItems Then
            sb.AppendLine(", Web.Nom, Web.Client ")
        End If
        sb.AppendLine(", Web.ConsumerPriority, SUM(Web.Val) DESC, SUM(Web.ValHistoric) DESC, SUM(CASE WHEN SalePointsCount>0 THEN Web.SumCcxVal/Web.SalePointsCount ELSE 0 END) DESC")
        If includeItems Then
            sb.AppendLine(", Web.SkuRef ")
        End If

        Dim oCountry As New DTOArea
        Dim oZona As New DTOArea
        Dim pLocation As New DTOArea
        Dim oProductDistributor As New DTOProductDistributor

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("Country")) Then
                oCountry = New DTOCountry(oDrd("Country"))
                oCountry.nom = SQLHelper.GetStringFromDataReader(oDrd("CountryNom"))
                DirectCast(oCountry, DTOCountry).ISO = SQLHelper.GetStringFromDataReader(oDrd("CountryISO"))
                DirectCast(oCountry, DTOCountry).prefixeTelefonic = SQLHelper.GetStringFromDataReader(oDrd("PrefixeTelefonic"))
            End If
            If Not oZona.Guid.Equals(oDrd("AreaGuid")) Then
                oZona = DTOArea.Factory(oDrd("AreaGuid"), DTOArea.Cods.Zona, oDrd("AreaNom"))
            End If
            If Not pLocation.Guid.Equals(oDrd("Location")) Then
                pLocation = DTOArea.Factory(oDrd("Location"), DTOArea.Cods.location, oDrd("Cit"))
            End If
            If Not oProductDistributor.Guid.Equals(oDrd("Client")) Then
                oProductDistributor = New DTOProductDistributor(oDrd("Client"))
                With oProductDistributor
                    .Nom = oDrd("Nom")
                    .Adr = oDrd("Adr")
                    .ZipCod = SQLHelper.GetStringFromDataReader(oDrd("ZipCod"))
                    .Country = oCountry
                    .Zona = oZona
                    .Location = pLocation
                    If Not IsDBNull(oDrd("Tel")) Then
                        Dim sTelNum As String = oDrd("Tel")
                        .Tel = New DTOContactTel()
                        .Tel.Country = oCountry
                        .Tel.value = sTelNum
                    End If
                    .Latitud = SQLHelper.GetDecimalFromDataReader(oDrd("Latitud"))
                    .Longitud = SQLHelper.GetDecimalFromDataReader(oDrd("Longitud"))

                    .Sales = SQLHelper.GetDecimalFromDataReader(oDrd("Sales"))
                    .SalesHistoric = SQLHelper.GetDecimalFromDataReader(oDrd("SalesHistoric"))
                    .SalesCcx = SQLHelper.GetDecimalFromDataReader(oDrd("SalesCcx"))

                    .LastFch = SQLHelper.GetFchFromDataReader(oDrd("LastFch"))
                    .Raffles = SQLHelper.GetBooleanFromDatareader(oDrd("Raffles"))
                    If includeItems Then
                        .Items = New List(Of String)
                    End If
                End With
                retval.Add(oProductDistributor)
            End If
            If includeItems Then
                Dim ref As String = SQLHelper.GetStringFromDataReader(oDrd("SkuRef"))
                If ref > "" Then
                    oProductDistributor.Items.Add(ref)
                End If
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
