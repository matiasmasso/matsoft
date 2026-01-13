Public Class ProductSkuForecastLoader
    Shared Function All(oMgz As DTOMgz, Optional oProveidor As DTOProveidor = Nothing, Optional oProduct As DTOProduct = Nothing, Optional FromNowOn As Boolean = False) As DTOProductSkuForecast.Collection

        Dim retval As New DTOProductSkuForecast.Collection

        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom, VwSkuNom.Proveidor ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.Skuid, VwSkuNom.SkuNom, VwSkuNom.SkuNomLlarg, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom ")
        sb.AppendLine(", (CASE WHEN VwSkuNom.HeredaDimensions=1 THEN VwSkuNom.CategoryDimensionL*VwSkuNom.CategoryDimensionW*VwSkuNom.CategoryDimensionH ELSE VwSkuNom.SkuDimensionL*VwSkuNom.SkuDimensionW*VwSkuNom.SkuDimensionH END) AS VolumeM3 ")
        sb.AppendLine(", (CASE WHEN VwSkuNom.HeredaDimensions=1 THEN VwSkuNom.CategoryMoq ELSE VwSkuNom.SkuMoq END) AS Moq ")
        sb.AppendLine(", (CASE WHEN VwSkuNom.HeredaDimensions=1 THEN VwSkuNom.CategoryForzarMoq ELSE VwSkuNom.SkuForzarMoq END) AS ForzarMoq ")
        sb.AppendLine(", VwSkuNom.Obsoleto, VwSkuNom.LastProduction, VwSkuNom.SecurityStock ")
        sb.AppendLine(", VwSkuStocks.Stock, VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock, VwSkuPncs.Pn1 ")
        sb.AppendLine(", VwSkuCost.Price AS Cost, VwSkuCost.Discount_OnInvoice ")
        sb.AppendLine(", XFcast.Yea, XFcast.Mes, XFcast.Target, XFcast.Sold, XFcast.FchCreated ")

        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuCost ON VwSkuNom.SkuGuid = VwSkuCost.SkuGuid ")

        If oProduct IsNot Nothing Then
            sb.AppendLine("INNER JOIN VwProductParent ON VwSkuNom.SkuGuid = VwProductParent.Child AND VwProductParent.Parent='" & oProduct.Guid.ToString & "' ")
        End If

        sb.AppendLine("LEFT OUTER JOIN (")
        sb.AppendLine(" SELECT Y.Sku, Y.Yea, Y.Mes, SUM(Y.Qty) AS Target, SUM(Y.Sold) AS Sold, MAX(Y.FchCreated) AS FchCreated ")
        sb.AppendLine(" FROM ( ")
        sb.AppendLine("     SELECT VwForecast.Sku, VwForecast.Yea, VwForecast.Mes, VwForecast.Qty, 0 AS Sold, VwForecast.FchCreated ")
        sb.AppendLine("     FROM VwForecast ")
        sb.AppendLine("     WHERE VwForecast.Customer IS NULL ")
        sb.AppendLine("         UNION ")
        sb.AppendLine("     SELECT Pnc.ArtGuid, YEAR(Pdc.Fch), MONTH(Pdc.Fch), 0 AS Qty, SUM(Pnc.Qty), NULL ")
        sb.AppendLine("     FROM Pnc ")
        sb.AppendLine("     INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("     WHERE Pdc.Cod = 2 ")
        sb.AppendLine("     GROUP BY Pnc.ArtGuid, YEAR(Pdc.Fch), MONTH(Pdc.Fch) ")
        sb.AppendLine("     ) Y ")
        If FromNowOn Then
            sb.AppendLine(" WHERE (Y.Yea>YEAR(GETDATE()) OR (Y.Yea=YEAR(GETDATE()) AND Y.Mes>=MONTH(GETDATE()))) ")
        Else
            sb.AppendLine(" WHERE Y.Yea>YEAR(GETDATE())-2 ")
        End If
        sb.AppendLine(" GROUP BY Y.Sku, Y.Yea, Y.Mes ")
        sb.AppendLine(") XFcast ON VwSkuNom.SkuGuid = XFcast.Sku ")

        sb.AppendLine("WHERE VwSkuNom.CategoryCodi<2 ")
        sb.AppendLine("AND VwSkuNom.IsBundle = 0 ")
        sb.AppendLine("AND VwSkuNom.CategoryObsoleto = 0 ")

        If oProveidor IsNot Nothing Then
            sb.AppendLine("And VwSkuNom.Proveidor = '" & oProveidor.Guid.ToString & "' ")
        End If

        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.BrandGuid, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryGuid, VwSkuNom.SkuNom, XFcast.Yea, XFcast.Mes ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oBrand = New DTOProductBrand
        Dim oCategory = New DTOProductCategory
        Dim oSku As New DTOProductSkuForecast
        Do While oDrd.Read
            If Not oSku.Guid.Equals(oDrd("SkuGuid")) Then
                If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                    If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                        oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                        With oBrand
                            SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                            If Not IsDBNull(oDrd("Proveidor")) Then
                                .proveidor = New DTOProveidor(oDrd("Proveidor"))
                            End If
                        End With
                    End If

                    oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                    With oCategory
                        .Brand = oBrand
                        SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNom", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                        .ord = SQLHelper.GetIntegerFromDataReader(oDrd("CategoryOrd"))
                        .Codi = SQLHelper.GetIntegerFromDataReader(oDrd("CategoryCodi"))
                    End With
                End If

                oSku = New DTOProductSkuForecast(oDrd("SkuGuid"))
                retval.Add(oSku)

                With oSku
                    .Id = SQLHelper.GetIntegerFromDataReader(oDrd("SkuId"))
                    .Category = oCategory
                    SQLHelper.LoadLangTextFromDataReader(oSku.nom, oDrd, "SkuNom", "SkuNom", "SkuNom", "SkuNom")
                    SQLHelper.LoadLangTextFromDataReader(oSku.nomLlarg, oDrd, "SkuNomLlarg", "SkuNomLlarg", "SkuNomLlarg", "SkuNomLlarg")
                    .refProveidor = SQLHelper.GetStringFromDataReader(oDrd("SkuRef"))
                    .NomProveidor = SQLHelper.GetStringFromDataReader(oDrd("SkuPrvNom"))
                    .VolumeM3 = SQLHelper.GetDecimalFromDataReader(oDrd("VolumeM3")) / 1000000000
                    .InnerPack = SQLHelper.GetIntegerFromDataReader(oDrd("Moq"))
                    .ForzarInnerPack = SQLHelper.GetIntegerFromDataReader(oDrd("ForzarMoq"))
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1")) ' - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Cost = SQLHelper.GetAmtFromDataReader(oDrd("Cost"))
                    .customerDto = SQLHelper.GetIntegerFromDataReader(oDrd("Discount_OnInvoice"))
                    .SecurityStock = SQLHelper.GetIntegerFromDataReader(oDrd("SecurityStock"))
                    .LastProduction = SQLHelper.GetBooleanFromDatareader(oDrd("LastProduction"))
                    .Obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("Obsoleto"))
                End With
            End If


            If Not IsDBNull(oDrd("Yea")) Then
                Dim item As New DTOProductSkuForecast.Forecast
                With item
                    .YearMonth = New DTOYearMonth(oDrd("Yea"), oDrd("Mes"))
                    .Target = SQLHelper.GetIntegerFromDataReader(oDrd("Target"))
                    .Sold = SQLHelper.GetIntegerFromDataReader(oDrd("Sold"))
                    .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
                End With
                oSku.Forecasts.Add(item)
            End If
        Loop

        oDrd.Close()
        Return retval
    End Function


    Shared Function Insert(oSkus As DTOProductSkuForecast.Collection, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Insert(oSkus, oTrans)
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


    Shared Sub Insert(oSkus As DTOProductSkuForecast.Collection, ByRef oTrans As SqlTransaction)
        Dim DtFch As Date = DTO.GlobalVariables.Now()
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Forecast ")
        sb.AppendLine("WHERE Yea=0")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oSku In oSkus

            For Each item In oSku.Forecasts
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                With item
                    oRow("Sku") = oSku.Guid
                    oRow("Yea") = .YearMonth.Year
                    oRow("Mes") = .YearMonth.Month
                    oRow("Qty") = .Target
                    oRow("UsrCreated") = SQLHelper.NullableBaseGuid(.UserCreated)
                    oRow("FchCreated") = DtFch
                End With
            Next
        Next

        oDA.Update(oDs)
    End Sub
End Class
