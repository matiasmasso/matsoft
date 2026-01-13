Public Class IncentiuLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOIncentiu
        Dim retval As DTOIncentiu = Nothing
        Dim oIncentiu As New DTOIncentiu(oGuid)
        If Load(oIncentiu) Then
            retval = oIncentiu
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oIncentiu As DTOIncentiu) As Boolean
        If Not oIncentiu.IsLoaded And Not oIncentiu.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT  Incentiu.* ")
            sb.AppendLine(", VwProductNom.Cod AS ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
            sb.AppendLine(", X.Product AS ItemProduct, X.ProductCod AS ItemProductCod, X.BrandGuid AS ItemBrandGuid, X.BrandNom AS ItemBrandNom, X.CategoryGuid AS ItemCategoryGuid, X.CategoryNom AS ItemCategoryNom, X.SkuNom AS ItemSkuNom ")
            sb.AppendLine(", X.Qty, X.Dto, X.FreeUnits ")
            sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
            sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
            sb.AppendLine(", LangBases.Esp AS BasesEsp, LangBases.Cat AS BasesCat, LangBases.Eng AS BasesEng, LangBases.Por AS BasesPor ")
            sb.AppendLine("FROM Incentiu ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Incentiu.Product=VwProductNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN (")
            sb.AppendLine("     SELECT Incentiu_Product.Incentiu, Incentiu_Product.Product ")
            sb.AppendLine("     , VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom")
            sb.AppendLine("     , Incentiu_QtyDto.Qty, Incentiu_QtyDto.Dto, Incentiu_QtyDto.FreeUnits ")
            sb.AppendLine("     FROM Incentiu_Product ")
            sb.AppendLine("     INNER JOIN Incentiu_QtyDto ON Incentiu_Product.Incentiu = Incentiu_QtyDto.Incentiu  ")
            sb.AppendLine("     INNER JOIN VwProductNom ON Incentiu_Product.Product=VwProductNom.Guid ")
            sb.AppendLine("                ) X ON Incentiu.Guid = X.Incentiu ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Incentiu.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.IncentiuTitle & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Incentiu.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.IncentiuExcerpt & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangBases ON Incentiu.Guid = LangBases.Guid AND LangBases.Src = " & DTOLangText.Srcs.IncentiuBases & " ")
            sb.AppendLine("WHERE Incentiu.Guid='" & oIncentiu.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY X.Product, X.Qty")

            Dim oProduct As New DTOProduct
            Dim iLastQty As Integer
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oIncentiu.IsLoaded Then
                    With oIncentiu
                        SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                        SQLHelper.LoadLangTextFromDataReader(.excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                        SQLHelper.LoadLangTextFromDataReader(.bases, oDrd, "BasesEsp", "BasesCat", "BasesEng", "BasesPor")

                        .manufacturerContribution = SQLHelper.GetStringFromDataReader(oDrd("ManufacturerContribution"))
                        .fchFrom = oDrd("FchFrom")
                        .fchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                        .onlyInStk = oDrd("OnlyInStk")
                        .cod = oDrd("Cod")
                        .cliVisible = oDrd("CliVisible")
                        .repVisible = oDrd("RepVisible")
                        '.Thumbnail = oDrd("Thumbnail")

                        If Not IsDBNull(oDrd("Product")) Then
                            .product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Product"), oDrd("SkuNom"))
                        End If

                        If Not IsDBNull(oDrd("Evento")) Then
                            .evento = New DTOEvento(oDrd("Evento"))
                        End If

                        .products = New List(Of DTOProduct)
                        .qtyDtos = New List(Of DTOQtyDto)
                        .IsLoaded = True
                    End With
                    oProduct = New DTOProduct
                    iLastQty = 0
                End If

                If Not IsDBNull(oDrd("ItemProduct")) Then
                    If Not oDrd("ItemProduct").Equals(oProduct.Guid) Then
                        oProduct = ProductLoader.NewProduct(CInt(oDrd("ItemProductCod")), DirectCast(oDrd("ItemBrandGuid"), Guid), oDrd("ItemBrandNom").ToString, oDrd("ItemCategoryGuid"), oDrd("ItemCategoryNom"), oDrd("ItemProduct"), oDrd("ItemSkuNom"))
                        oIncentiu.Products.Add(oProduct)
                    End If
                End If

                If Not IsDBNull(oDrd("Qty")) Then
                    If oDrd("Qty") > iLastQty Then
                        Dim oQtyDto As New DTOQtyDto
                        With oQtyDto
                            .Qty = oDrd("Qty")
                            .Dto = oDrd("Dto")
                            .FreeUnits = oDrd("FreeUnits")
                            iLastQty = .Qty
                        End With
                        oIncentiu.QtyDtos.Add(oQtyDto)
                    End If
                End If
            Loop

            oDrd.Close()

            oIncentiu.Channels = DistributionChannelsLoader.All(oIncentiu, DTOLang.ESP)
        End If

        Dim retval As Boolean = oIncentiu.IsLoaded
        Return retval
    End Function

    Shared Function Update(oIncentiu As DTOIncentiu, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oIncentiu, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oIncentiu As DTOIncentiu, ByRef oTrans As SqlTransaction)
        UpdateHeader(oIncentiu, oTrans)
        UpdateProducts(oIncentiu, oTrans)
        UpdateQtyDtos(oIncentiu, oTrans)
        LangTextLoader.Update(oIncentiu.title, oTrans)
        LangTextLoader.Update(oIncentiu.excerpt, oTrans)
        LangTextLoader.Update(oIncentiu.bases, oTrans)

        If oIncentiu.Channels IsNot Nothing Then
            UpdateChannels(oIncentiu, oTrans)
        End If
    End Sub

    Shared Sub UpdateChannels(oIncentiu As DTOIncentiu, ByRef oTrans As SqlTransaction)
        If oIncentiu.Products IsNot Nothing Then
            If Not oIncentiu.IsNew Then DeleteChannels(oIncentiu, oTrans)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Incentiu, DistributionChannel ")
            sb.AppendLine("FROM Incentiu_Channels ")
            sb.AppendLine("WHERE Incentiu ='" & oIncentiu.Guid.ToString & "'")
            Dim SQL As String = sb.ToString

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            For Each item As DTODistributionChannel In oIncentiu.Channels
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Incentiu") = oIncentiu.Guid
                oRow("DistributionChannel") = item.Guid
            Next
            oDA.Update(oDs)
        End If
    End Sub

    Shared Sub UpdateProducts(oIncentiu As DTOIncentiu, ByRef oTrans As SqlTransaction)
        If oIncentiu.Products IsNot Nothing Then
            If Not oIncentiu.IsNew Then DeleteProducts(oIncentiu, oTrans)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Incentiu, Product ")
            sb.AppendLine("FROM Incentiu_Product ")
            sb.AppendLine("WHERE Incentiu ='" & oIncentiu.Guid.ToString & "'")
            Dim SQL As String = sb.ToString

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            For Each item As DTOProduct In oIncentiu.Products
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Incentiu") = oIncentiu.Guid
                oRow("Product") = item.Guid
            Next
            oDA.Update(oDs)
        End If
    End Sub

    Shared Sub UpdateQtyDtos(oIncentiu As DTOIncentiu, ByRef oTrans As SqlTransaction)
        If oIncentiu.QtyDtos IsNot Nothing Then
            If Not oIncentiu.IsNew Then DeleteQtyDtos(oIncentiu, oTrans)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Incentiu_QtyDto ")
            sb.AppendLine("WHERE Incentiu = @Guid")
            Dim SQL As String = sb.ToString

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oIncentiu.Guid.ToString())
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            For Each item As DTOQtyDto In oIncentiu.QtyDtos
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Incentiu") = oIncentiu.Guid
                oRow("Qty") = item.Qty
                oRow("Dto") = item.Dto
                oRow("FreeUnits") = item.FreeUnits
            Next
            oDA.Update(oDs)
        End If
    End Sub

    Shared Sub UpdateHeader(oIncentiu As DTOIncentiu, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Incentiu ")
        sb.AppendLine("WHERE Guid='" & oIncentiu.Guid.ToString() & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oIncentiu.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oIncentiu
            'oRow("NomEsp") = .Title.Esp
            'oRow("NomCat") = Defaults.NullOrValue(.Title.Cat)
            'oRow("NomEng") = Defaults.NullOrValue(.Title.Eng)
            'oRow("NomPor") = Defaults.NullOrValue(.Title.Por)
            'oRow("ExcerptEsp") = Defaults.NullOrValue(.Excerpt.Esp)
            'oRow("ExcerptCat") = Defaults.NullOrValue(.Excerpt.Cat)
            'oRow("ExcerptEng") = Defaults.NullOrValue(.Excerpt.Eng)
            'oRow("ExcerptPor") = Defaults.NullOrValue(.Excerpt.Por)
            'oRow("DscEsp") = Defaults.NullOrValue(.Bases.Esp)
            'oRow("DscCat") = Defaults.NullOrValue(.Bases.Cat)
            'oRow("DscEng") = Defaults.NullOrValue(.Bases.Eng)
            'oRow("DscPor") = Defaults.NullOrValue(.Bases.Por)
            oRow("ManufacturerContribution") = Defaults.NullOrValue(.ManufacturerContribution)
            oRow("FchFrom") = .FchFrom
            oRow("FchTo") = Defaults.NullOrValue(.FchTo)
            oRow("OnlyInStk") = .OnlyInStk
            oRow("Cod") = .Cod
            oRow("CliVisible") = .CliVisible
            oRow("RepVisible") = .RepVisible
            oRow("Product") = Defaults.NullOrValue(.Product)
            oRow("Evento") = Defaults.NullOrValue(.Evento)
            oRow("Thumbnail") = .Thumbnail
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oIncentiu As DTOIncentiu, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oIncentiu, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Delete(oIncentiu As DTOIncentiu, ByRef oTrans As SqlTransaction)
        DeleteChannels(oIncentiu, oTrans)
        DeleteQtyDtos(oIncentiu, oTrans)
        DeleteProducts(oIncentiu, oTrans)
        DeleteHeader(oIncentiu, oTrans)
    End Sub

    Shared Sub DeleteChannels(oIncentiu As DTOIncentiu, ByRef oTrans As SqlTransaction)
        If oIncentiu.Channels IsNot Nothing Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("DELETE Incentiu_Channels ")
            sb.AppendLine("WHERE Incentiu ='" & oIncentiu.Guid.ToString & "'")
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If
    End Sub
    Shared Sub DeleteQtyDtos(oIncentiu As DTOIncentiu, ByRef oTrans As SqlTransaction)
        If oIncentiu.QtyDtos IsNot Nothing Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("DELETE Incentiu_QtyDto ")
            sb.AppendLine("WHERE Incentiu ='" & oIncentiu.Guid.ToString & "'")
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If
    End Sub

    Shared Sub DeleteProducts(oIncentiu As DTOIncentiu, ByRef oTrans As SqlTransaction)
        If oIncentiu.Products IsNot Nothing Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("DELETE Incentiu_Product ")
            sb.AppendLine("WHERE Incentiu ='" & oIncentiu.Guid.ToString & "'")
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If
    End Sub


    Shared Sub DeleteHeader(oIncentiu As DTOIncentiu, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Incentiu WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oIncentiu.Guid.ToString())
    End Sub

#End Region

    Shared Function Participants(oIncentiu As DTOIncentiu) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)

        Dim oIncentius As New List(Of DTOIncentiu)
        oIncentius.Add(oIncentiu)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", VwPostalAddress.CountryGuid, VwPostalAddress.CountryEsp, VwPostalAddress.CEE ")
        sb.AppendLine(", VwPostalAddress.ProvinciaGuid, VwPostalAddress.ProvinciaNom ")
        sb.AppendLine(", VwPostalAddress.LocationGuid, VwPostalAddress.LocationNom ")
        sb.AppendLine(", VwPostalAddress.ZonaGuid, VwPostalAddress.ZipGuid, VwPostalAddress.adr ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
        sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass= ContactClass.Guid ")
        'sb.AppendLine("INNER JOIN Incentiu_Channels ON ContactClass.DistributionChannel=Incentiu_Channels.DistributionChannel AND Incentiu_Channels.Incentiu='" & oIncentiu.Guid.ToString & "' ")
        sb.AppendLine("INNER JOIN VwPostalAddress ON CliGral.Guid = VwPostalAddress.SrcGuid ")
        sb.AppendLine("INNER JOIN ")
        sb.AppendLine("(SELECT (CASE WHEN CliClient.CcxGuid IS NULL THEN Pdc.CliGuid ELSE CliClient.CcxGuid END) AS Ccx ")
        sb.AppendLine("		FROM Pdc ")
        sb.AppendLine("		INNER JOIN CliClient ON Pdc.CliGuid = CliClient.Guid ")
        sb.AppendLine("     WHERE (")
        For Each item As DTOIncentiu In oIncentius
            If item.UnEquals(oIncentius.First) Then
                sb.Append(" OR ")
            End If
            sb.Append("     Pdc.Promo ='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine("		)) Ccx ON (CliClient.CcxGuid = Ccx.Ccx OR CliClient.Guid = Ccx.Ccx) ")
        sb.AppendLine("WHERE CliGral.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", VwPostalAddress.CountryGuid, VwPostalAddress.CountryEsp, VwPostalAddress.CEE ")
        sb.AppendLine(", VwPostalAddress.ProvinciaGuid, VwPostalAddress.ProvinciaNom ")
        sb.AppendLine(", VwPostalAddress.LocationGuid, VwPostalAddress.LocationNom ")
        sb.AppendLine(", VwPostalAddress.ZonaGuid, VwPostalAddress.ZipGuid, VwPostalAddress.adr ")
        sb.AppendLine("ORDER BY VwPostalAddress.CountryEsp, VwPostalAddress.ProvinciaNom, VwPostalAddress.LocationNom, CliGral.NomCom+CliGral.RaoSocial, CliGral.Guid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOContact(oDrd("Guid"))
            With item
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function DeliveryAddresses(oIncentiu As DTOIncentiu) As MatHelper.Excel.Sheet
        Dim retval As MatHelper.Excel.Sheet = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT LangTitle.Esp AS TitleEsp ")
        sb.AppendLine(", CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", A1.adr AS Adr1, A1.LocationNom as LocationNom1, A1.ZipCod AS ZipCod1, A1.ProvinciaNom AS ProvinciaNom1, A1.CountryEsp AS CountryNom1 ")
        sb.AppendLine(", A2.adr AS Adr2, A2.LocationNom as LocationNom2, A2.ZipCod AS ZipCod2, A2.ProvinciaNom AS ProvinciaNom2, A2.CountryEsp AS CountryNom2 ")
        sb.AppendLine("FROM Incentiu ")
        sb.AppendLine("LEFT OUTER JOIN Pdc ON Pdc.Promo = Incentiu.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Incentiu.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.IncentiuTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwAddressBase A1 ON Pdc.CliGuid = A1.SrcGuid AND A1.Cod=1 ")
        sb.AppendLine("LEFT OUTER JOIN VwAddressBase A2 ON Pdc.CliGuid = A2.SrcGuid AND A2.Cod=3 ")
        sb.AppendLine("WHERE Incentiu.Guid ='" & oIncentiu.Guid.ToString() & "' ")
        sb.AppendLine("GROUP BY LangTitle.Esp, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", A1.adr, A1.LocationNom, A1.ZipCod, A1.ProvinciaNom, A1.CountryEsp ")
        sb.AppendLine(", A2.adr, A2.LocationNom, A2.ZipCod, A2.ProvinciaNom, A2.CountryEsp ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If retval Is Nothing Then
                retval = New MatHelper.Excel.Sheet("adreces entrega", SQLHelper.GetStringFromDataReader(oDrd("TitleEsp")))
                With retval
                    .AddColumn("Destinatari", MatHelper.Excel.Cell.NumberFormats.PlainText)
                    .AddColumn("Adreça", MatHelper.Excel.Cell.NumberFormats.PlainText)
                    .AddColumn("Població", MatHelper.Excel.Cell.NumberFormats.PlainText)
                    .AddColumn("Codi postal", MatHelper.Excel.Cell.NumberFormats.PlainText)
                    .AddColumn("Provincia", MatHelper.Excel.Cell.NumberFormats.PlainText)
                    .AddColumn("Pais", MatHelper.Excel.Cell.NumberFormats.PlainText)
                End With
            End If

            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            If String.IsNullOrEmpty(SQLHelper.GetStringFromDataReader(oDrd("NomCom"))) Then
                oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("RaoSocial")))
            Else
                oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("NomCom")))
            End If
            If IsDBNull(oDrd("Adr2")) Then
                oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("Adr1")))
                oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("LocationNom1")))
                oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("ZipCod1")))
                If IsDBNull(oDrd("ProvinciaNom1")) Then
                    oRow.AddCell()
                Else
                    oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("ProvinciaNom1")))
                End If
                oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("CountryNom1")))
            Else
                oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("Adr2")))
                oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("LocationNom2")))
                oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("ZipCod2")))
                If IsDBNull(oDrd("ProvinciaNom2")) Then
                    oRow.AddCell()
                Else
                    oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("ProvinciaNom2")))
                End If
                oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("CountryNom2")))
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function PncProducts(oIncentiu As DTOIncentiu) As List(Of DTOProductSkuQtyEur)
        Dim retval As New List(Of DTOProductSkuQtyEur)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNom, VwSkuNom.SkuPrvNom, VwSkuNom.SkuRef ")
        sb.AppendLine(", SUM(Pnc.Qty) AS Qty, SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) AS Eur ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE Pdc.Promo='" & oIncentiu.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNom, VwSkuNom.SkuPrvNom, VwSkuNom.SkuRef ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBrand As New DTOProductBrand(oDrd("BrandGuid"))
            SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
            Dim oCategory As New DTOProductCategory(oDrd("CategoryGuid"))
            With oCategory
                .Brand = oBrand
                .Nom.Esp = oDrd("CategoryNom")
                '.Nom = oDrd("CategoryNom")
            End With
            Dim item As New DTOProductSkuQtyEur(oDrd("SkuGuid"))
            With item
                .Category = oCategory
                SQLHelper.LoadLangTextFromDataReader(.nom, oDrd, "SkuNom", "SkuNom", "SkuNom", "SkuNom")
                .refProveidor = SQLHelper.GetStringFromDataReader(oDrd("SkuRef"))
                .NomProveidor = SQLHelper.GetStringFromDataReader(oDrd("SkuPrvNom"))
                .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
    Shared Function ExcelResults(oIncentiu As DTOIncentiu) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("Resultats incentiu")
        With retval
            .AddColumn("Brand name")
            .AddColumn("Category name")
            .AddColumn("Sku name")
            .AddColumn("Manufacturer Sku name")
            .AddColumn("ref")
            .AddColumn("order")
            .AddColumn("date", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("customer")
            .AddColumn("qty", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("price", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("discount", MatHelper.Excel.Cell.NumberFormats.Percent)
            .AddColumn("amount", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("contribution", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryNom ")
        sb.AppendLine(", VwSkuNom.SkuNom, VwSkuNom.SkuPrvNom, VwSkuNom.SkuRef ")
        sb.AppendLine(", Pdc.Pdc, Pdc.Fch, CliGral.FullNom ")
        sb.AppendLine(", Pnc.Qty, Pnc.Eur, Pnc.Dto ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE Pdc.Promo='" & oIncentiu.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Pdc.Fch DESC, Pnc.Lin ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRow = retval.AddRow
            oRow.AddCell(oDrd("BrandNom"))
            oRow.AddCell(oDrd("CategoryNom"))
            oRow.AddCell(oDrd("SkuNom"))
            oRow.AddCell(oDrd("SkuPrvNom"))
            oRow.AddCell(oDrd("SkuRef"))
            oRow.AddCell(oDrd("Pdc"))
            oRow.AddCell(oDrd("Fch"))
            oRow.AddCell(oDrd("FullNom"))
            oRow.AddCell(oDrd("Qty"))
            oRow.AddCell(oDrd("Eur"))
            oRow.AddCell(oDrd("Dto"))
            oRow.AddFormula("RC[-3]*RC[-2]*(100-RC[-1])/100")
            oRow.AddFormula("RC[-4]*RC[-3]*RC[-2]/100")
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

Public Class IncentiusLoader
    Shared Function Headers(oUser As DTOUser, BlIncludeObsolets As Boolean, Optional BlIncludeFutureIncentius As Boolean = False) As List(Of DTOIncentiu)
        Dim retval As New List(Of DTOIncentiu)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT  Incentiu.Guid, Incentiu.fchFrom, Incentiu.fchTo, Incentiu.Cod, Incentiu.CliVisible, Incentiu.RepVisible, Pdcs.Pdcs ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
        sb.AppendLine("FROM Incentiu ")

        Select Case oUser.Rol.Id
            Case DTORol.Ids.manufacturer
                sb.AppendLine("INNER JOIN VwProductParent ON Incentiu.Product = VwProductParent.Child ")
                sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent = Tpa.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                sb.AppendLine("INNER JOIN Incentiu_Channels ON Incentiu.Guid = Incentiu_Channels.Incentiu ")
                sb.AppendLine("INNER JOIN ContactClass ON Incentiu_Channels.DistributionChannel = ContactClass.DistributionChannel ")
                sb.AppendLine("INNER JOIN CliGral ON ContactClass.Guid = CliGral.ContactClass ")
                sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = CliGral.Guid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN Incentiu_Channels ON Incentiu.Guid = Incentiu_Channels.Incentiu ")
                sb.AppendLine("INNER JOIN RepProducts ON Incentiu_Channels.DistributionChannel = RepProducts.DistributionChannel AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>=GETDATE() ) ")
                sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = RepProducts.Rep AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select

        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Incentiu.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.IncentiuTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Incentiu.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.IncentiuExcerpt & " ")

        sb.AppendLine("LEFT OUTER JOIN ( ")
        sb.AppendLine("SELECT Pdc.Promo, COUNT(DISTINCT Pdc) AS Pdcs ")
        sb.AppendLine("FROM Pdc ")
        Select Case oUser.Rol.Id
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Pnc.RepGuid = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select
        sb.AppendLine("GROUP BY Pdc.Promo ")
        sb.AppendLine(") Pdcs ON Incentiu.Guid = Pdcs.Promo ")

        Dim BlWhere As Boolean
        If Not BlIncludeObsolets Then
            sb.AppendLine("WHERE (Incentiu.FchTo IS NULL OR Incentiu.FchTo> GETDATE()) ")
            BlWhere = True
        End If

        If Not BlIncludeFutureIncentius Then
            sb.Append(IIf(BlWhere, "AND ", "WHERE "))
            sb.AppendLine("(Incentiu.FchFrom IS NULL OR Incentiu.FchFrom<= GETDATE()) ")
        End If

        Select Case oUser.Rol.Id
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.Append(IIf(BlWhere, "AND ", "WHERE "))
                sb.AppendLine("Incentiu.RepVisible<>0 ")
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                sb.Append(IIf(BlWhere, "AND ", "WHERE "))
                sb.AppendLine("Incentiu.CliVisible<>0 AND Incentiu.RepVisible<>0 ")
        End Select

        sb.AppendLine("GROUP BY Incentiu.Guid, Incentiu.fchFrom, Incentiu.fchTo, Incentiu.Cod, Incentiu.CliVisible, Incentiu.RepVisible, Pdcs.Pdcs ")
        sb.AppendLine(", CAST(LangTitle.Esp AS VARCHAR(MAX)), CAST(LangTitle.Cat AS VARCHAR(MAX)), CAST(LangTitle.Eng AS VARCHAR(MAX)), CAST(LangTitle.Por AS VARCHAR(MAX)) ")
        sb.AppendLine(", CAST(LangExcerpt.Esp AS VARCHAR(MAX)), CAST(LangExcerpt.Cat AS VARCHAR(MAX)), CAST(LangExcerpt.Eng AS VARCHAR(MAX)), CAST(LangExcerpt.Por AS VARCHAR(MAX)) ")

        sb.AppendLine("ORDER BY Incentiu.FchFrom DESC, Incentiu.FchTo DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oIncentiu As New DTOIncentiu(oDrd("Guid"))
            With oIncentiu
                SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                SQLHelper.LoadLangTextFromDataReader(.bases, oDrd, "BasesEsp", "BasesCat", "BasesEng", "BasesPor")


                .FchFrom = oDrd("FchFrom")
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                .Cod = oDrd("Cod")
                .CliVisible = oDrd("CliVisible")
                .RepVisible = oDrd("RepVisible")
                .PdcsCount = SQLHelper.GetIntegerFromDataReader(oDrd("Pdcs"))
            End With
            retval.Add(oIncentiu)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oUser As DTOUser, BlIncludeObsolets As Boolean, Optional BlIncludeFutureIncentius As Boolean = False) As List(Of DTOIncentiu)
        Dim retval As New List(Of DTOIncentiu)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT  Incentiu.Guid, Incentiu.fchFrom, Incentiu.fchTo, Incentiu.Cod, Incentiu.CliVisible, Incentiu.RepVisible, Incentiu.OnlyInStk, Pdcs.Pdcs ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
        sb.AppendLine(", LangBases.Esp AS BasesEsp, LangBases.Cat AS BasesCat, LangBases.Eng AS BasesEng, LangBases.Por AS BasesPor ")
        sb.AppendLine(",Incentiu_Product.Product, VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        sb.AppendLine(",Incentiu_QtyDto.Qty, Incentiu_QtyDto.Dto, Incentiu_QtyDto.FreeUnits ")
        sb.AppendLine("FROM Incentiu ")
        sb.AppendLine("LEFT OUTER JOIN Incentiu_Product ON Incentiu.Guid = Incentiu_Product.Incentiu ")
        sb.AppendLine("LEFT OUTER JOIN Incentiu_QtyDto ON Incentiu.Guid = Incentiu_QtyDto.Incentiu ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Incentiu_Product.Product=VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Incentiu.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.IncentiuTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Incentiu.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.IncentiuExcerpt & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangBases ON Incentiu.Guid = LangBases.Guid AND LangBases.Src = " & DTOLangText.Srcs.IncentiuBases & " ")



        Select Case oUser.Rol.Id
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN ( ")
                sb.AppendLine("SELECT Incentiu_Channels.Incentiu ")
                sb.AppendLine("FROM Incentiu_Channels ")
                sb.AppendLine("INNER JOIN RepProducts ON Incentiu_Channels.DistributionChannel = RepProducts.DistributionChannel ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo >= GETDATE()) ")
                sb.AppendLine("GROUP BY Incentiu_Channels.Incentiu ")
                sb.AppendLine(") X ON Incentiu.Guid = X.Incentiu ")
        End Select


        sb.AppendLine("LEFT OUTER JOIN ( ")
        sb.AppendLine("SELECT Pdc.Promo, COUNT(DISTINCT Pdc) AS Pdcs ")
        sb.AppendLine("FROM Pdc ")
        Select Case oUser.Rol.Id
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Pnc.RepGuid = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                sb.AppendLine("INNER JOIN Incentiu_Channels ON Pdc.Promo = Incentiu_Channels.Incentiu ")
                sb.AppendLine("INNER JOIN ContactClass ON Incentiu_Channels.DistributionChannel = ContactClass.DistributionChannel ")
                sb.AppendLine("INNER JOIN CliGral ON ContactClass.Guid = CliGral.ContactClass ")
                sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = CliGral.Guid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select
        sb.AppendLine("GROUP BY Pdc.Promo ")
        sb.AppendLine(") Pdcs ON Incentiu.Guid = Pdcs.Promo ")

        Dim BlWhere As Boolean
        If Not BlIncludeObsolets Then
            sb.Append(IIf(BlWhere, "AND ", "WHERE "))
            BlWhere = True
            sb.AppendLine("(FchTo IS NULL OR FchTo> GETDATE()) ")
        End If
        If Not BlIncludeFutureIncentius Then
            sb.Append(IIf(BlWhere, "AND ", "WHERE "))
            BlWhere = True
            sb.AppendLine("(FchFrom IS NULL OR FchFrom<= GETDATE()) ")
        End If
        Select Case oUser.Rol.Id
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.Append(IIf(BlWhere, "AND ", "WHERE "))
                sb.AppendLine("RepVisible<>0 ")
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                sb.Append(IIf(BlWhere, "AND ", "WHERE "))
                sb.AppendLine("CliVisible<>0 AND RepVisible<>0 ")
        End Select
        sb.AppendLine("ORDER BY FchFrom DESC,Incentiu.Guid,VwProductNom.BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNom, Incentiu_QtyDto.Qty")
        Dim oIncentiu As New DTOIncentiu
        Dim oProduct As New DTOProduct
        Dim iLastQty As Integer
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("Guid").Equals(oIncentiu.Guid) Then
                oIncentiu = New DTOIncentiu(oDrd("Guid"))
                With oIncentiu
                    SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                    SQLHelper.LoadLangTextFromDataReader(.excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                    SQLHelper.LoadLangTextFromDataReader(.bases, oDrd, "BasesEsp", "BasesCat", "BasesEng", "BasesPor")

                    .FchFrom = oDrd("FchFrom")
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .OnlyInStk = oDrd("OnlyInStk")
                    .Cod = oDrd("Cod")
                    .CliVisible = oDrd("CliVisible")
                    .RepVisible = oDrd("RepVisible")
                    .PdcsCount = SQLHelper.GetIntegerFromDataReader(oDrd("Pdcs"))
                    .Products = New List(Of DTOProduct)
                    .QtyDtos = New List(Of DTOQtyDto)
                End With
                retval.Add(oIncentiu)
                oProduct = New DTOProduct
                iLastQty = 0
            End If
            If Not oDrd("Product").Equals(oProduct.Guid) Then
                If IsDBNull(oDrd("ProductCod")) Then
                    oProduct = New DTOProduct
                Else
                    oProduct = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Product"), oDrd("SkuNom"))
                    oIncentiu.Products.Add(oProduct)
                End If
            End If
            If Not IsDBNull(oDrd("Qty")) Then

                If oDrd("Qty") > iLastQty Then
                    Dim oQtyDto As New DTOQtyDto
                    With oQtyDto
                        .Qty = oDrd("Qty")
                        .Dto = oDrd("Dto")
                        .FreeUnits = oDrd("FreeUnits")
                        iLastQty = .Qty
                    End With
                    oIncentiu.QtyDtos.Add(oQtyDto)
                End If
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
