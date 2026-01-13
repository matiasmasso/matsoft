Public Class ForecastsLoader


    Shared Function All(Optional oProveidor As DTOProveidor = Nothing,
                        Optional oCustomer As DTOCustomer = Nothing,
                        Optional oBrand As DTOProductBrand = Nothing,
                        Optional oCategory As DTOProductCategory = Nothing) As List(Of DTOForecast)

        Dim retval As New List(Of DTOForecast)

        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom, VwSkuNom.Proveidor ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.Skuid, VwSkuNom.SkuNom, VwSkuNom.SkuNomLlarg, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom ")
        sb.AppendLine(", (CASE WHEN VwSkuNom.HeredaDimensions=1 THEN VwSkuNom.CategoryDimensionL*VwSkuNom.CategoryDimensionW*VwSkuNom.CategoryDimensionH ELSE VwSkuNom.SkuDimensionL*VwSkuNom.SkuDimensionW*VwSkuNom.SkuDimensionH END) AS VolumeM3 ")
        sb.AppendLine(", (CASE WHEN VwSkuNom.HeredaDimensions=1 THEN VwSkuNom.CategoryMoq ELSE VwSkuNom.SkuMoq END) AS Moq ")
        sb.AppendLine(", (CASE WHEN VwSkuNom.HeredaDimensions=1 THEN VwSkuNom.CategoryForzarMoq ELSE VwSkuNom.SkuForzarMoq END) AS ForzarMoq ")
        sb.AppendLine(", VwSkuNom.Obsoleto, VwSkuNom.LastProduction ")
        sb.AppendLine(", VwSkuStocks.Stock, VwSkuPncs.Clients, VwSkuPncs.Pn1 ")
        sb.AppendLine(", VwSkuCost.Price AS Cost, VwSkuCost.Discount_OnInvoice ")
        sb.AppendLine(", X.Yea, X.Mes, X.Qty, X.Sold ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='88A2C2F3-9E14-421A-B727-7647ECD07165' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuCost ON VwSkuNom.SkuGuid = VwSkuCost.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN (")
        sb.AppendLine("SELECT Y.Sku, Y.Yea, Y.Mes, SUM(Y.Qty) AS Qty, SUM(Y.Sold) AS Sold ")
        sb.AppendLine("FROM ( ")
        sb.AppendLine("SELECT VwForecast.Sku, VwForecast.Yea, VwForecast.Mes, VwForecast.Qty, 0 AS Sold ")
        sb.AppendLine("FROM VwForecast ")
        If oCustomer Is Nothing Then
            sb.AppendLine("WHERE VwForecast.Customer IS NULL ")
        Else
            sb.AppendLine("WHERE VwForecast.Customer = '" & oCustomer.Guid.ToString & "' ")
        End If
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT Pnc.ArtGuid, YEAR(Pdc.Fch), MONTH(Pdc.Fch), 0 AS Qty, SUM(Pnc.Qty) ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("WHERE Pdc.Cod = 2 ")
        sb.AppendLine("GROUP BY Pnc.ArtGuid, YEAR(Pdc.Fch), MONTH(Pdc.Fch) ")
        sb.AppendLine(") Y ")
        sb.AppendLine("WHERE Y.Yea>YEAR(GETDATE())-2 ")
        sb.AppendLine("GROUP BY Y.Sku, Y.Yea, Y.Mes ")
        sb.AppendLine(") X ON VwSkuNom.SkuGuid = X.Sku ")
        sb.AppendLine("WHERE (X.Qty>0 OR X.Sold>0 OR VwSkuNom.Obsoleto=0) ")
        sb.AppendLine("AND VwSkuNom.CategoryCodi<2 ")

        If oProveidor IsNot Nothing Then
            sb.AppendLine("AND VwSkuNom.Proveidor = '" & oProveidor.Guid.ToString & "' ")
        End If
        If oBrand IsNot Nothing Then
            sb.AppendLine("AND VwSkuNom.BrandGuid = '" & oBrand.Guid.ToString & "' ")
        End If
        If oCategory IsNot Nothing Then
            sb.AppendLine("AND VwSkuNom.CategoryGuid = '" & oCategory.Guid.ToString & "' ")
        End If

        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.BrandGuid, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryGuid, VwSkuNom.SkuNom, X.Yea, X.Mes ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oBrand = New DTOProductBrand
        oCategory = New DTOProductCategory
        Dim oSku As New DTOProductSku
        Do While oDrd.Read
            If Not oSku.Guid.Equals(oDrd("SkuGuid")) Then
                If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                    If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                        oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                        With oBrand
                            SQLHelper.LoadLangTextFromDataReader(.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                            If Not IsDBNull(oDrd("Proveidor")) Then
                                .proveidor = New DTOProveidor(oDrd("Proveidor"))
                            End If
                        End With
                    End If

                    oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                    With oCategory
                        .Brand = oBrand
                        SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNom", "CategoryNom", "CategoryNom", "CategoryNom")
                        .ord = SQLHelper.GetIntegerFromDataReader(oDrd("CategoryOrd"))
                        .Codi = SQLHelper.GetIntegerFromDataReader(oDrd("CategoryCodi"))
                    End With
                End If

                oSku = New DTOProductSku(oDrd("SkuGuid"))
                'If oDrd("SkuGuid").Equals(New Guid("B1E94685-EF2D-4F8F-B590-76B115090F0D")) Then Stop

                With oSku
                    .Id = SQLHelper.GetIntegerFromDataReader(oDrd("SkuId"))
                    .Category = oCategory
                    SQLHelper.LoadLangTextFromDataReader(.nom, oDrd, "SkuNom", "SkuNom", "SkuNom", "SkuNom")
                    SQLHelper.LoadLangTextFromDataReader(.nomLlarg, oDrd, "SkuNomLlarg", "SkuNomLlarg", "SkuNomLlarg", "SkuNomLlarg")
                    .refProveidor = SQLHelper.GetStringFromDataReader(oDrd("SkuRef"))
                    .NomProveidor = SQLHelper.GetStringFromDataReader(oDrd("SkuPrvNom"))
                    .VolumeM3 = SQLHelper.GetDecimalFromDataReader(oDrd("VolumeM3")) / 1000000000
                    .InnerPack = SQLHelper.GetIntegerFromDataReader(oDrd("Moq"))
                    .ForzarInnerPack = SQLHelper.GetIntegerFromDataReader(oDrd("ForzarMoq"))
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                    .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                    .Cost = SQLHelper.GetAmtFromDataReader(oDrd("Cost"))
                    .CustomerDto = SQLHelper.GetIntegerFromDataReader(oDrd("Discount_OnInvoice"))
                    .LastProduction = SQLHelper.GetBooleanFromDatareader(oDrd("LastProduction"))
                    .Obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("Obsoleto"))
                End With
            End If


            Dim item As New DTOForecast
            With item
                .Sku = oSku
                If Not IsDBNull(oDrd("Yea")) Then
                    .YearMonth = New DTOYearMonth(oDrd("Yea"), oDrd("Mes"))
                    .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                    .Sold = SQLHelper.GetIntegerFromDataReader(oDrd("Sold"))
                Else
                    .YearMonth = New DTOYearMonth(0, 0)
                End If
            End With
            retval.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function

    Shared Function Insert(oForecasts As List(Of DTOForecast), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Insert(oForecasts, oTrans)
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


    Shared Sub Insert(oForecasts As List(Of DTOForecast), ByRef oTrans As SqlTransaction)
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
        For Each item As DTOForecast In oForecasts
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With item
                oRow("Customer") = SQLHelper.NullableBaseGuid(.Customer)
                oRow("Sku") = .Sku.Guid
                oRow("Yea") = .YearMonth.Year
                oRow("Mes") = .YearMonth.Month
                oRow("Qty") = .Qty
                oRow("UsrCreated") = SQLHelper.NullableBaseGuid(.UserCreated)
                oRow("FchCreated") = DtFch
            End With
        Next

        oDA.Update(oDs)
    End Sub

End Class
