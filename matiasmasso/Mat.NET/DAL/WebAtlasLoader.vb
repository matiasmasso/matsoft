Public Class WebAtlasLoader

    Shared Property FchMin = DTO.GlobalVariables.Today().AddMonths(-24) 'Descarta els que no han comprat després d'aquesta data

#Region "Loader"
    Shared Function Distributors(Optional product As DTOProduct = Nothing, Optional proveidor As DTOProveidor = Nothing, Optional oProvinciaOrZona As DTOArea = Nothing, Optional lang As DTOLang = Nothing, Optional includeItems As Boolean = False, Optional oLocation As DTOLocation = Nothing) As List(Of DTOProductDistributor)
        Dim retval As New List(Of DTOProductDistributor)
        If lang Is Nothing AndAlso proveidor IsNot Nothing Then lang = proveidor.lang
        If lang Is Nothing Then lang = DTOLang.ESP

        Dim oPremiumLine = PremiumLineLoader.FromProduct(product)

        Dim sCountryNom As String = lang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng", "Country.Nom_Por")

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

        If oPremiumLine IsNot Nothing Then
            sb.AppendLine(" AND Web.PremiumLine = '" & oPremiumLine.Guid.ToString() & "' ")
        ElseIf product IsNot Nothing Then
            Dim sProductGuid As String = product.Guid.ToString
            sb.AppendLine("AND (Web.Category ='" & sProductGuid & "' OR Web.Brand ='" & sProductGuid & "') ")
        End If

        If proveidor IsNot Nothing Then
            sb.AppendLine("AND Web.Proveidor = '" & proveidor.Guid.ToString & "' ")
        End If
        If includeItems Then
            sb.AppendLine("AND Web.SkuRef >'' ")
        End If
        If oProvinciaOrZona IsNot Nothing Then
            sb.AppendLine("AND Web.AreaGuid ='" & oProvinciaOrZona.Guid.ToString & "' ")
        End If
        If oLocation IsNot Nothing Then
            sb.AppendLine("AND Web.Location ='" & oLocation.Guid.ToString & "' ")
        End If
        sb.AppendLine("GROUP BY Web.Client, Web.Nom, Web.Adr, Web.Tel, Web.Raffles ")
        sb.AppendLine(", Web.Country, " & sCountryNom & ", Country.ISO, Country.PrefixeTelefonic ")
        sb.AppendLine(", Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine(", Web.Location, Web.Cit, Web.ZipCod, Web.ConsumerPriority ")
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
                oCountry.Nom = SQLHelper.GetStringFromDataReader(oDrd("CountryNom"))
                DirectCast(oCountry, DTOCountry).ISO = SQLHelper.GetStringFromDataReader(oDrd("CountryISO"))
                DirectCast(oCountry, DTOCountry).PrefixeTelefonic = SQLHelper.GetStringFromDataReader(oDrd("PrefixeTelefonic"))
            End If
            If Not oZona.Guid.Equals(oDrd("AreaGuid")) Then
                oZona = DTOArea.Factory(oDrd("AreaGuid"), DTOArea.Cods.Zona, oDrd("AreaNom"))
            End If
            If Not pLocation.Guid.Equals(oDrd("Location")) Then
                pLocation = DTOArea.Factory(oDrd("Location"), DTOArea.Cods.Location, oDrd("Cit"))
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


    Shared Function NearestNeighbours(oProduct As DTOProduct, oCoordenadas As GeoHelper.Coordenadas, oLang As DTOLang, Optional iCount As Integer = 7) As List(Of DTONeighbour)
        Dim retval As New List(Of DTONeighbour)
        Dim WKT As String = String.Format("POINT({0} {1})", oCoordenadas.Longitud, oCoordenadas.Latitud).Replace(",", ".")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP(" & iCount & ") CliAdr.Geo.Lat AS Lat, CliAdr.Geo.Long AS Lng, CliAdr.Geo.STDistance('" & WKT & "') AS Distance ")
        sb.AppendLine(", X.Client, X.Nom, X.Tel ")
        sb.AppendLine(", VwAddress.* ")
        sb.AppendLine("FROM ( ")
        sb.AppendLine("     SELECT Web.Client, Web.Nom, Web.Tel ")
        sb.AppendLine("     FROM Web ")
        sb.AppendLine("     WHERE (Web.Category ='" & oProduct.Guid.ToString & "' OR Web.Brand ='" & oProduct.Guid.ToString & "') ")
        sb.AppendLine("     AND Web.Impagat = 0 ")
        sb.AppendLine("     AND Web.Obsoleto = 0 ")
        sb.AppendLine("     AND Web.Blocked = 0 ")
        sb.AppendLine("     GROUP BY Web.Client, Web.Nom, Web.Tel ")
        sb.AppendLine("     ) X ")
        sb.AppendLine("INNER JOIN CliGral ON X.Client=CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON X.Client=VwAddress.SrcGuid ")
        sb.AppendLine("INNER JOIN CliAdr ON X.Client=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
        sb.AppendLine("AND CliAdr.Geo.STDistance('" & WKT & "') IS NOT NULL ")
        sb.AppendLine("ORDER BY Distance")

        Dim oCountry As New DTOGuidNom
        Dim oZona As New DTOGuidNom
        Dim oLocation As New DTOGuidNom

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTONeighbour()
            With item
                .Guid = oDrd("Client")
                .Nom = oDrd("Nom")
                .Telefon = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                .Distance = oDrd("Distance")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

#End Region


#Region "Update"

    Shared Function UpdateWebAtlas(oEmp As DTOEmp, exs As List(Of Exception)) As Boolean
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Dim sb As New System.Text.StringBuilder
        Dim mystep As Integer
        Try
            mystep += 1
            Reset(oTrans)
            mystep += 1
            UpdateXarxa(oTrans)
            mystep += 1
            AssignaFacturarA(oTrans)
            mystep += 1
            AddSalePoints(oTrans)
            mystep += 1
            RemoveBlockedSalePoints(oTrans)
            mystep += 1
            SalePointsAggregation(oTrans)
            mystep += 1
            SalePointsPriority(oTrans)
            mystep += 1
            SetDetails(oTrans)
            mystep += 1
            SetAddress(oTrans)
            mystep += 1
            SetTelephone(oTrans)
            mystep += 1
            SetImpagats(oEmp, oTrans)
            mystep += 1
            SetChannelDetails(oTrans)
            mystep += 1
            BlockNoWebClients(oTrans)
            mystep += 1
            RemoveExcludedAreas(oTrans)
            mystep += 1
            SetPremiumLines(oTrans)

            oTrans.Commit()
        Catch ex As Exception
            exs.Add(New Exception(String.Format("Error al pas " & mystep & " al actualitzar els punts de venda")))
            exs.Add(ex)
        Finally
            If oConn IsNot Nothing Then
                oConn.Close()
            End If
        End Try

        Return exs.Count = 0
    End Function

    Private Shared Sub Reset(oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Web"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Private Shared Sub UpdateXarxa(oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("INSERT INTO Web (Brand, Category, Sku, SkuRef, Proveidor, Client, Val, ValHistoric, LastFch)  ")
        sb.AppendLine("SELECT VwWebAtlas.BrandGuid, VwWebAtlas.CategoryGuid, VwWebAtlas.SkuGuid, VwWebAtlas.SkuRef, VwWebAtlas.Proveidor ")
        sb.AppendLine(", VwWebAtlas.CliGuid, VwWebAtlas.Val, VwWebAtlas.ValHistoric, VwWebAtlas.LastFch ")
        sb.AppendLine("FROM VwWebAtlas  ")

        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Private Shared Sub AssignaFacturarA(oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Web ")
        sb.AppendLine("SET Web.Ccx = (Case When CliClient.CcxGuid Is NULL Then Web.Client Else CliClient.CcxGuid End) ")
        sb.AppendLine("FROM Web LEFT OUTER JOIN CliClient On Web.Client = CliClient.Guid")
        Dim SQL = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Private Shared Sub AddSalePoints(oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("INSERT INTO WEB (Brand, Category, Sku, SkuRef, Proveidor, Client, Ccx, ConsumerPriority, LastFch) ")
        sb.AppendLine("Select Web.Brand, Web.Category, Web.Sku, Web.SkuRef, Web.Proveidor, CliClient.Guid, CliClient.CcxGuid, Web.ConsumerPriority, MAX(Web2.LastFch) ")
        sb.AppendLine("     FROM Web ")
        sb.AppendLine("     INNER JOIN CliClient On Web.Ccx = CliClient.CcxGuid ")
        sb.AppendLine("     INNER JOIN CliGral On CliClient.Guid = CliGral.Guid ")
        sb.AppendLine("     LEFT OUTER JOIN Web As Web2 On CliClient.Guid = Web2.Client And Web.Client = Web2.Client And Web.Category = Web2.Category And Web.Brand = Web2.Brand ")
        sb.AppendLine("     WHERE CliGral.Obsoleto=0 And (Web2.Guid Is NULL) And CliClient.NoWeb = 0 ")
        sb.AppendLine("     GROUP BY Web.Brand, Web.Category, Web.Sku, Web.SkuRef, Web.Proveidor, CliClient.Guid, CliClient.CcxGuid, Web.ConsumerPriority")
        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Private Shared Sub RemoveExcludedAreas(oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE WEB ")
        sb.AppendLine("FROM Web  ")
        sb.AppendLine("INNER JOIN Tpa On Web.Brand=TPA.Guid ")
        sb.AppendLine("WHERE Web.Guid Not In ")
        sb.AppendLine("		(Select Web.Guid ")
        sb.AppendLine("		FROM Web ")
        sb.AppendLine("		INNER JOIN VwAreaParent On Web.Location = VwAreaParent.ChildGuid ")
        sb.AppendLine("		INNER JOIN BrandArea On VwAreaParent.ParentGuid = BrandArea.Area ")
        sb.AppendLine("		INNER JOIN VwProductParent On Web.Sku= VwProductParent.Child And BrandArea.Brand = VwProductParent.Parent) ")
        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Private Shared Sub RemoveBlockedSalePoints(oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Web  " _
        & "FROM Web " _
        & "INNER JOIN CliTpa On Web.Client = CliTpa.CliGuid And (Web.Brand = CliTpa.ProductGuid Or Web.Category = CliTpa.ProductGuid Or Web.Sku = CliTpa.ProductGuid) " _
        & "INNER JOIN Stp On (CliTpa.ProductGuid=Stp.Guid Or CliTpa.ProductGuid=Stp.Brand) " _
        & "INNER JOIN Tpa On Stp.Brand = Tpa.Guid " _
        & "WHERE (CliTpa.Cod = " & CInt(DTOCliProductBlocked.Codis.Exclos) & " Or CliTpa.Cod = " & CInt(DTOCliProductBlocked.Codis.Standard) & ") " _
        & "And TPA.CodDist = 1 "
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Private Shared Sub BlockNoWebClients(oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Web ")
        sb.AppendLine("Set Web.Blocked = 1 ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN CliClient On Web.Client = CliClient.Guid And CliClient.NoWeb = 1 ")
        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Private Shared Sub SetChannelDetails(oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Web ")
        sb.AppendLine("Set Web.Blocked = (Case When ContactClass.SalePoint = 1 Then 0 Else 1 End) ")
        sb.AppendLine("  , Web.ConsumerPriority = DistributionChannel.ConsumerPriority ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN CliGral On Web.Client = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN ContactClass On CliGral.ContactClass = ContactClass.Guid ")
        sb.AppendLine("LEFT OUTER JOIN DistributionChannel On ContactClass.DistributionChannel = DistributionChannel.Guid ")
        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub



    Private Shared Sub SalePointsAggregation(oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Web ")
        sb.AppendLine("Set Web.SalePointsCount=X.Clis ")
        sb.AppendLine("  , Web.SumCcxVal=X.SumCcxVal ")
        sb.AppendLine("  , Web.LastFch=X.LastFch ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN (Select Ccx, Sku, COUNT(DISTINCT Client) As CLIS, SUM(Val) As SumCcxVal, MAX(Web.LastFch) As LastFch FROM Web GROUP BY Ccx, Sku) X On WEB.CCX = X.CCX And Web.Sku = X.Sku ")
        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Private Shared Sub SalePointsPriority(oTrans As SqlTransaction)
        'aquells la central dels quals està marcada amb CliClient.Webatlaspriority=1 sel's assignarà a compres el valor resultant de dividir les compres de la central per el numero de punts de venda a no ser que el volum de les seves compres sigui superior
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Web ")
        sb.AppendLine("Set Val=(Case When Web.Val > SumCcxVal/SalePointsCount Then Web.Val Else SumCcxVal/SalePointsCount End) ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN CliClient On Web.Client=CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliClient Ccx On CliClient.CcxGuid = Ccx.Guid ")
        sb.AppendLine("WHERE SalePointsCount>0 And Ccx.webatlaspriority=1 ")
        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Private Shared Sub SetDetails(oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Web ")
        sb.AppendLine("Set Web.Nom = (Case When CliGral.NomCom >'' THEN CliGral.NomCom ELSE CliGral.RaoSocial END) ")
        sb.AppendLine("  , Web.Raffles = (CASE WHEN CliClient.NoRaffles = 1 THEN 0 ELSE ContactClass.Raffles END) ")
        sb.AppendLine("  , Web.Obsoleto=CliGral.Obsoleto ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN CliGral ON Web.Client = CliGral.Guid ")
        sb.AppendLine("INNER JOIN CliClient ON Web.Client = CliClient.Guid ")
        sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass=ContactClass.Guid ")
        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Private Shared Sub SetAddress(oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Web SET Web.Location = Zip.Location, Web.ZipCod = Zip.ZipCod, Web.Adr = CliAdr.Adr, Web.Cit = Location.Nom ")
        sb.AppendLine(", Web.AreaGuid = (CASE WHEN Provincia.Nom IS NULL THEN Zona.Guid ELSE Provincia.Guid END) ")
        sb.AppendLine(", Web.AreaNom = (CASE WHEN Provincia.Nom IS NULL THEN Zona.Nom ELSE Provincia.Nom END) ")
        sb.AppendLine(", Web.Country = Country.Guid ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN CliAdr ON Web.Client=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
        sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip = Zip.Guid ")
        sb.AppendLine("INNER JOIN Location ON Zip.Location = Location.Guid ")
        sb.AppendLine("INNER JOIN Zona ON Location.Zona = Zona.Guid ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country = Country.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Provincia ON Zona.Provincia = Provincia.Guid ")
        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Private Shared Sub SetTelephone(oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE Web SET tel=VwTelDefault.TelNum from Web inner join VwTelDefault on Web.Client=VwTelDefault.Contact"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Private Shared Sub SetImpagats(oEmp As DTOEmp, oTrans As SqlTransaction)
        Dim oExercici As New DTOExercici(oEmp, DTO.GlobalVariables.Today().Year)
        Dim oCtaImpagats As DTOPgcCta = PgcCtaLoader.FromCod(DTOPgcPlan.Ctas.impagats, oExercici)
        Dim SQL As String = "UPDATE Web SET Impagat=1 " _
        & "WHERE Web.Ccx IN (" _
            & "SELECT ContactGuid FROM Ccb " _
            & "INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid " _
            & "WHERE Cca.Yea = @Yea AND Ccb.CtaGuid = @CtaGuid " _
            & "GROUP BY Ccb.ContactGuid " _
            & "HAVING SUM(CASE WHEN Ccb.DH = 1 THEN Ccb.EUR ELSE - Ccb.EUR END) > 0)"

        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Yea", DTO.GlobalVariables.Today().Year, "@CtaGuid", oCtaImpagats.Guid.ToString())
    End Sub

    Private Shared Sub SetPremiumLines(oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Web SET Web.PremiumLine = PremiumLine.Guid ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN VwProductParent ON Web.Sku = VwProductParent.Child ")
        sb.AppendLine("INNER JOIN PremiumProduct ON VwProductParent.Parent = PremiumProduct.Product ")
        sb.AppendLine("INNER JOIN PremiumLine ON PremiumProduct.PremiumLine = PremiumLine.Guid AND PremiumLine.Codi = " & DTOPremiumLine.Codis.defaultExclude & " ")
        sb.AppendLine("INNER JOIN PremiumCustomer ON PremiumLine.Guid = PremiumCustomer.PremiumLine AND (PremiumCustomer.Customer = Web.Ccx OR PremiumCustomer.Customer = Web.Client) AND PremiumCustomer.Codi =" & DTOPremiumCustomer.Codis.included & " ")
        Dim SQL = sb.ToString()
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


#End Region

End Class
