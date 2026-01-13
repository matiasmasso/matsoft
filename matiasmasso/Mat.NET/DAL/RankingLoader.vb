Public Class RankingLoader

    Shared Sub Load(ByRef oRanking As DTORanking)
        LoadProveidors(oRanking)
        'LoadCatalog(oRanking)
        LoadReps(oRanking)
        'If oRanking.Atlas Is Nothing Then LoadAtlas(oRanking)
        LoadItems(oRanking)
    End Sub

    Shared Sub LoadReps(ByRef oRanking As DTORanking)
        oRanking.Reps = New List(Of DTORep)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT RepGuid, RepNom ")
        sb.Append(SQLFROM(oRanking))
        sb.Append(SQLWHERE(oRanking))
        sb.Append("GROUP BY RepGuid, RepNom ")
        sb.Append("ORDER BY RepNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("RepGuid")) AndAlso Not IsDBNull(oDrd("RepNom")) Then
                Dim oRep As New DTORep(oDrd("RepGuid"))
                oRep.NickName = oDrd("RepNom")
                oRanking.Reps.Add(oRep)
            End If
        Loop
        oDrd.Close()
    End Sub

    Shared Sub LoadProveidors(ByRef oRanking As DTORanking)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT ProveidorGuid, ProveidorNom ")
        sb.Append(SQLFROM(oRanking))
        sb.Append(SQLWHERE(oRanking))
        sb.Append("GROUP BY ProveidorGuid, ProveidorNom ")
        sb.Append("ORDER BY ProveidorNom")
        Dim SQL As String = sb.ToString
        oRanking.Proveidors = New List(Of DTOContact)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("ProveidorGuid")) Then
                Dim oContact As New DTOContact(DirectCast(oDrd("ProveidorGuid"), Guid))
                oContact.Nom = oDrd("ProveidorNom").ToString
                oRanking.Proveidors.Add(oContact)
            End If
        Loop
        oDrd.Close()
    End Sub


    Shared Sub LoadCatalog(ByRef oRanking As DTORanking)

        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT BrandGuid, BrandNomEsp ")
        sb.AppendLine(", CategoryGuid, CategoryNomEsp, CategoryNomCat, CategoryNomEng, CategoryNomPor ")
        sb.AppendLine(", SkuGuid, SkuNomEsp, SkuNomCat, SkuNomEng, SkuNomPor ")
        sb.AppendLine(", ProveidorGuid ")

        sb.Append(SQLFROM(oRanking))
        sb.Append(SQLWHERE(oRanking))
        sb.Append("GROUP BY BrandGuid, BrandNomEsp ")
        sb.AppendLine(", CategoryGuid, CategoryNomEsp, CategoryNomCat, CategoryNomEng, CategoryNomPor ")
        sb.AppendLine(", SkuGuid, SkuNomEsp, SkuNomCat, SkuNomEng, SkuNomPor ")
        sb.AppendLine(", ProveidorGuid ")

        sb.Append("ORDER BY BrandGuid, BrandNomEsp ")
        sb.AppendLine(", CategoryGuid, CategoryNomEsp")
        sb.AppendLine(", SkuGuid, SkuNomEsp ")

        oRanking.Catalog = New DTOProductCatalog
        Dim oBrand As New DTOProductBrand(System.Guid.NewGuid)
        Dim oCategory As New DTOProductCategory(System.Guid.NewGuid)
        Dim oSku As New DTOProductSku(System.Guid.NewGuid)

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("BrandGuid").Equals(oBrand.Guid) Then
                oBrand = New DTOProductBrand(DirectCast(oDrd("BrandGuid"), Guid))
                SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNomEsp", "BrandNomEsp", "BrandNomEsp", "BrandNomEsp")
                oBrand.Categories = New List(Of DTOProductCategory)
                If Not IsDBNull(oDrd("ProveidorGuid")) Then
                    oBrand.Proveidor = New DTOProveidor(DirectCast(oDrd("ProveidorGuid"), Guid))
                End If
                oRanking.Catalog.Brands.Add(oBrand)
            End If

            If Not oDrd("CategoryGuid").Equals(oCategory.Guid) Then
                    oCategory = New DTOProductCategory(DirectCast(oDrd("CategoryGuid"), Guid))
                    oCategory.Brand = oBrand
                    SQLHelper.LoadLangTextFromDataReader(oCategory.Nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                    oCategory.Skus = New List(Of DTOProductSku)
                    oBrand.Categories.Add(oCategory)
                End If

                oSku = New DTOProductSku(DirectCast(oDrd("SkuGuid"), Guid))
                oSku.Category = oCategory
                SQLHelper.LoadLangTextFromDataReader(oSku.Nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                oCategory.Skus.Add(oSku)
        Loop
        oDrd.Close()

    End Sub


    Shared Sub LoadItems(ByRef oRanking As DTORanking)
        Dim sb As New System.Text.StringBuilder
        sb.Append(SQLSELECT(oRanking))
        sb.Append(SQLFROM(oRanking))
        sb.Append(SQLWHERE(oRanking))
        sb.Append(SQLGROUPBY(oRanking))
        sb.Append(SQLORDERBY(oRanking))
        Dim SQL As String = sb.ToString
        'oRanking.Items = New List(Of RankingItem)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Select Case oRanking.ConceptType
            Case DTORanking.ConceptTypes.Geo
                LoadGeo(oDrd, oRanking)
            Case DTORanking.ConceptTypes.Product
                'LoadProducts(oDrd, oRanking)
        End Select
        oDrd.Close()
    End Sub


    Shared Function SQLSELECT(oRanking As DTORanking) As String
        Dim sb As New System.Text.StringBuilder
        Select Case oRanking.ConceptType
            Case DTORanking.ConceptTypes.Geo
                If oRanking.GroupCcx Then
                    sb.Append("SELECT VwAddress.CountryGuid as CountryGuid, VwAddress.CountryEsp AS CountryNom ")
                    sb.Append(", VwAddress.ZonaGuid, VwAddress.ZonaNom ")
                    sb.Append(", VwAddress.LocationGuid, VwAddress.LocationNom ")
                    sb.Append(", VwSellOut2.Ccx as CliGuid, CliGral.RaoSocial, CliGral.NomCom AS NomCom ")
                Else
                    sb.Append("SELECT VwSellOut2.CountryGuid, VwSellOut2.CountryEsp AS CountryNom ")
                    sb.Append(", VwSellOut2.ZonaGuid, VwSellOut2.ZonaNom ")
                    sb.Append(", VwSellOut2.LocationGuid, VwSellOut2.LocationNom ")
                    sb.Append(", VwSellOut2.CustomerGuid as CliGuid, VwSellOut2.CustomerNom AS RaoSocial, VwSellOut2.CustomerNomCom AS NomCom ")
                End If
        End Select

        sb.Append(", SUM(Qty) AS Units ")
        sb.Append(", SUM(Eur) as Amt ")

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SQLFROM(oRanking As DTORanking) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("FROM VwSellout2 ")
        If oRanking.GroupCcx Then
            sb.Append("LEFT OUTER JOIN CliGral ON VwSellOut2.Ccx = CliGral.Guid ")
            sb.Append("LEFT OUTER JOIN VwAddress ON VwSellOut2.Ccx = VwAddress.SrcGuid ")
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SQLWHERE(oRanking As DTORanking) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("WHERE VwSellout2.Emp=" & oRanking.User.Emp.Id & " ")
        sb.Append("AND VwSellout2.Cod = " & DTOPurchaseOrder.Codis.client & " ")
        sb.Append("AND VwSellout2.Year = " & oRanking.Year & " ")
        sb.Append("AND VwSellout2.Month BETWEEN " & oRanking.MonthFrom & " AND " & oRanking.MonthTo & " ")

        If oRanking.Rep IsNot Nothing Then
            sb.Append("AND VwSellout2.RepGuid = '" & oRanking.Rep.Guid.ToString & "' ")
        End If


        If oRanking.Product IsNot Nothing Then
            Select Case oRanking.Product.SourceCod
                Case DTOProduct.SourceCods.Brand
                    sb.Append("AND VwSellout2.BrandGuid = '" & oRanking.Product.Guid.ToString & "' ")
                Case DTOProduct.SourceCods.Category
                    sb.Append("AND VwSellout2.CategoryGuid = '" & oRanking.Product.Guid.ToString & "' ")
                Case DTOProduct.SourceCods.Sku
                    sb.Append("AND VwSellout2.SkuGuid = '" & oRanking.Product.Guid.ToString & "' ")
            End Select
        End If


        If oRanking.Channel IsNot Nothing Then
            sb.Append("AND VwSellout2.ChannelGuid = '" & oRanking.Channel.Guid.ToString & "' ")
        End If

        If oRanking.Area IsNot Nothing Then
            Select Case oRanking.Area.GetCod()
                Case DTOArea.Cods.Country
                    sb.Append("AND VwSellout2.CountryGuid = '" & oRanking.Area.Guid.ToString & "' ")
                Case DTOArea.Cods.Zona
                    sb.Append("AND VwSellout2.ZonaGuid = '" & oRanking.Area.Guid.ToString & "' ")
                Case DTOArea.Cods.Location
                    sb.Append("AND VwSellout2.LocationGuid = '" & oRanking.Area.Guid.ToString & "' ")
            End Select
        End If

        If oRanking.Proveidor IsNot Nothing Then
            sb.Append("AND VwSellout2.ProveidorGuid = '" & oRanking.Proveidor.Guid.ToString & "' ")
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SQLGROUPBY(oRanking As DTORanking) As String
        Dim sb As New System.Text.StringBuilder

        Select Case oRanking.ConceptType
            Case DTORanking.ConceptTypes.Geo
                If oRanking.GroupCcx Then
                    sb.Append("GROUP BY VwAddress.CountryGuid, VwAddress.CountryEsp ")
                    sb.Append(", VwAddress.ZonaGuid, VwAddress.ZonaNom ")
                    sb.Append(", VwAddress.LocationGuid, VwAddress.LocationNom ")
                    sb.Append(", VwSellOut2.Ccx, CliGral.RaoSocial, CliGral.NomCom ")
                Else

                    sb.Append("GROUP BY VwSellOut2.CountryGuid , VwSellOut2.CountryEsp ")
                    sb.Append(", VwSellOut2.ZonaGuid, VwSellOut2.ZonaNom ")
                    sb.Append(", VwSellOut2.LocationGuid, VwSellOut2.LocationNom ")
                    sb.Append(", VwSellOut2.CustomerGuid, VwSellOut2.CustomerNom, VwSellOut2.CustomerNomCom ")
                End If
        End Select

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SQLORDERBY(oRanking As DTORanking) As String
        Dim sb As New System.Text.StringBuilder
        Select Case oRanking.ConceptType
            Case DTORanking.ConceptTypes.Geo
                sb.Append("ORDER BY SUM(Eur) DESC ")
        End Select

        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Sub LoadGeo(oDrd As SqlDataReader, ByRef oRanking As DTORanking)
        oRanking.Items = New List(Of DTORankingItem)
        Dim index As Integer = 1
        Do While oDrd.Read
            Dim oCountry As New DTOCountry(DirectCast(oDrd("CountryGuid"), Guid))
            oCountry.LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryNom")

            Dim oZona As New DTOZona(DirectCast(oDrd("ZonaGuid"), Guid))
            oZona.Country = oCountry
            oZona.Nom = oDrd("ZonaNom")

            Dim oLocation As New DTOLocation(DirectCast(oDrd("LocationGuid"), Guid))
            oLocation.Zona = oZona
            oLocation.Nom = oDrd("LocationNom")

            Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))
            Dim sb As New System.Text.StringBuilder
            sb.Append(oDrd("RaoSocial"))
            If oDrd("NomCom").ToString > "" Then
                sb.Append(" '" & oDrd("NomCom").ToString & "'")
            End If
            oCustomer.Nom = sb.ToString

            Dim oItem As New DTORankingItem
            With oItem
                .Customer = oCustomer
                .Location = oLocation
                .Order = index
                .Units = CInt(oDrd("Units"))
                .Amt = DTOAmt.Factory(CDec(oDrd("Amt")))
            End With
            oRanking.Items.Add(oItem)
            index += 1
        Loop
        oDrd.Close()
    End Sub



End Class
