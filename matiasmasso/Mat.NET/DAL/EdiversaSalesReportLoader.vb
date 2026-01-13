Public Class EdiversaSalesReportLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOEdiversaSalesReport
        Dim retval As DTOEdiversaSalesReport = Nothing
        Dim oEdiversaSalesReport As New DTOEdiversaSalesReport(oGuid)
        If Load(oEdiversaSalesReport) Then
            retval = oEdiversaSalesReport
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaSalesReport As DTOEdiversaSalesReport) As Boolean
        If Not oEdiversaSalesReport.IsLoaded And Not oEdiversaSalesReport.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT EdiversaSalesReportHeader.Id, EdiversaSalesReportHeader.Fch, EdiversaSalesReportHeader.Customer AS HeaderCustomer, EdiversaSalesReportHeader.Cur ")
            sb.AppendLine(", EdiversaSalesReportItem.Parent, EdiversaSalesReportItem.Customer as ItemCustomer ")
            sb.AppendLine(", EdiversaSalesReportItem.Dept, EdiversaSalesReportItem.Centro ")
            sb.AppendLine(", EdiversaSalesReportItem.Sku, EdiversaSalesReportItem.Qty ")
            sb.AppendLine(", EdiversaSalesReportItem.QtyBack, EdiversaSalesReportItem.Eur ")
            sb.AppendLine("FROM EdiversaSalesReportHeader ")
            sb.AppendLine("LEFT OUTER JOIN EdiversaSalesReportItem ON EdiversaSalesReportHeader.Guid = EdiversaSalesReportItem.Parent ")
            sb.AppendLine("WHERE EdiversaSalesReportHeader.Guid='" & oEdiversaSalesReport.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oEdiversaSalesReport
                    If Not .IsLoaded Then
                        .Id = oDrd("Id")
                        .Fch = oDrd("Fch")
                        .Customer = New DTOCustomer(oDrd("HeaderCustomer"))
                        .Cur = SQLHelper.GetCurFromDataReader(oDrd("Cur"))
                        .IsLoaded = True
                    End If
                    If Not IsDBNull(oDrd("Parent")) Then
                        Dim item As New DTOEdiversaSalesReport.Item()
                        With item
                            .Customer = New DTOCustomer(oDrd("ItemCustomer"))
                            .Dept = SQLHelper.GetStringFromDataReader(oDrd("Dept"))
                            .Centro = SQLHelper.GetStringFromDataReader(oDrd("Centro"))
                            .Sku = New DTOProductSku(oDrd("Sku"))
                            .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                            .QtyBack = SQLHelper.GetIntegerFromDataReader(oDrd("QtyBack"))
                            .Eur = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))
                        End With
                        .Items.Add(item)
                    End If
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oEdiversaSalesReport.IsLoaded
        Return retval
    End Function

    Shared Function Update(oEdiversaSalesReport As DTOEdiversaSalesReport, Optional oEdiFile As DTOEdiversaFile = Nothing, Optional ByRef exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEdiversaSalesReport, oEdiFile, oTrans)
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


    Shared Sub Update(oEdiversaSalesReport As DTOEdiversaSalesReport, Optional oEdiFile As DTOEdiversaFile = Nothing, Optional ByRef oTrans As SqlTransaction = Nothing)
        DeleteById(oEdiversaSalesReport.Id, oTrans)
        UpdateHeader(oEdiversaSalesReport, oTrans)
        UpdateItems(oEdiversaSalesReport, oTrans)
        If oEdiFile IsNot Nothing Then
            EdiversaFileLoader.Update(oEdiFile, oTrans)
        End If
    End Sub

    Shared Sub UpdateHeader(oEdiversaSalesReport As DTOEdiversaSalesReport, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EdiversaSalesReportHeader ")
        sb.AppendLine("WHERE Guid='" & oEdiversaSalesReport.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oEdiversaSalesReport.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oEdiversaSalesReport
            oRow("Id") = .Id
            oRow("Fch") = SQLHelper.NullableFch(.Fch)
            oRow("Customer") = SQLHelper.NullableBaseGuid(.Customer)
            oRow("Cur") = SQLHelper.NullableString(.Cur.Tag)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oEdiversaSalesReport As DTOEdiversaSalesReport, ByRef oTrans As SqlTransaction)
        DeleteItems(oEdiversaSalesReport, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EdiversaSalesReportItem ")
        sb.AppendLine("WHERE Parent='" & oEdiversaSalesReport.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOEdiversaSalesReport.Item In oEdiversaSalesReport.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Parent") = oEdiversaSalesReport.Guid
            oRow("Customer") = SQLHelper.NullableBaseGuid(item.Customer)
            oRow("Fch") = SQLHelper.NullableFch(item.Fch)
            oRow("Dept") = SQLHelper.NullableString(item.Dept)
            oRow("Centro") = SQLHelper.NullableString(item.Centro)
            oRow("Sku") = SQLHelper.NullableBaseGuid(item.Sku)
            oRow("Qty") = SQLHelper.NullableInt(item.Qty)
            oRow("QtyBack") = SQLHelper.NullableInt(item.QtyBack)
            oRow("Eur") = SQLHelper.NullableDecimal(item.Eur)
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oEdiversaSalesReport As DTOEdiversaSalesReport, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oEdiversaSalesReport, oTrans)
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


    Shared Sub DeleteById(Id As String, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE EdiversaSalesReportItem ")
        sb.AppendLine("FROM EdiversaSalesReportItem ")
        sb.AppendLine("INNER JOIN EdiversaSalesReportHeader ON EdiversaSalesReportItem.Parent = EdiversaSalesReportHeader.Guid ")
        sb.AppendLine("WHERE EdiversaSalesReportHeader.id='" & Id & "' ")
        Dim SQL As String = sb.ToString
        Dim rc = SQLHelper.ExecuteNonQuery(SQL, oTrans)

        sb = New Text.StringBuilder
        sb.AppendLine("DELETE EdiversaSalesReportHeader ")
        sb.AppendLine("WHERE EdiversaSalesReportHeader.id='" & Id & "' ")
        SQL = sb.ToString
        Dim rc2 = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub Delete(oEdiversaSalesReport As DTOEdiversaSalesReport, ByRef oTrans As SqlTransaction)
        DeleteItems(oEdiversaSalesReport, oTrans)
        DeleteHeader(oEdiversaSalesReport, oTrans)
    End Sub

    Shared Sub DeleteHeader(oEdiversaSalesReport As DTOEdiversaSalesReport, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaSalesReportHeader WHERE Guid='" & oEdiversaSalesReport.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oEdiversaSalesReport As DTOEdiversaSalesReport, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaSalesReportItem WHERE Parent='" & oEdiversaSalesReport.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class EdiversaSalesReportsLoader

    Shared Function Years(oEmp As DTOEmp) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT year(Fch) AS Year ")
        sb.AppendLine("FROM EdiversaSalesReportHeader ")
        sb.AppendLine("INNER JOIN CliGral ON EdiversaSalesReportHeader.Customer = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("GROUP BY year(Fch) ")
        sb.AppendLine("ORDER BY year(Fch) DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Year"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, year As Integer) As List(Of DTOEdiversaSalesReport)
        Dim retval As New List(Of DTOEdiversaSalesReport)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT EdiversaSalesReportHeader.Guid, EdiversaSalesReportHeader.Id, EdiversaSalesReportHeader.Fch ")
        sb.AppendLine(", EdiversaSalesReportHeader.Customer, CliGral.RaoSocial ")
        sb.AppendLine(", EdiversaSalesReportItem.Fch AS ItemFch, EdiversaSalesReportItem.Customer AS ItemCustomer, CliClient.Ref AS CustomerRef ")
        sb.AppendLine(", EdiversaSalesReportItem.Dept, EdiversaSalesReportItem.Centro ")
        sb.AppendLine(", EdiversaSalesReportItem.Sku, VwSkuNom.SkuNomLlargEsp ")
        sb.AppendLine(", EdiversaSalesReportItem.Qty, EdiversaSalesReportItem.QtyBack ")
        sb.AppendLine(", VwRetail.Retail,  EdiversaSalesReportItem.Eur ")
        sb.AppendLine("FROM EdiversaSalesReportHeader ")
        sb.AppendLine("INNER JOIN CliGral ON EdiversaSalesReportHeader.Customer = CliGral.Guid ")
        sb.AppendLine("INNER JOIN EdiversaSalesReportItem ON EdiversaSalesReportHeader.Guid = EdiversaSalesReportItem.Parent ")
        sb.AppendLine("INNER JOIN VwSkuNom ON EdiversaSalesReportItem.Sku = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN VwRetail ON EdiversaSalesReportItem.Sku = VwRetail.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON EdiversaSalesReportItem.Customer = CliClient.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND year(EdiversaSalesReportHeader.Fch) = " & year & " ")
        sb.AppendLine("ORDER BY EdiversaSalesReportHeader.Fch DESC, EdiversaSalesReportHeader.Guid ")
        Dim SQL As String = sb.ToString
        Dim value As New DTOEdiversaSalesReport
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("Guid").Equals(value.Guid) Then
                value = New DTOEdiversaSalesReport(oDrd("Guid"))
                With value
                    .Id = oDrd("Id")
                    .Fch = oDrd("fch")
                    .Customer = New DTOCustomer(oDrd("Customer"))
                    .Customer.Nom = oDrd("RaoSocial")
                End With
                retval.Add(value)
            End If
            If Not IsDBNull(oDrd("ItemFch")) Then
                Dim item As New DTOEdiversaSalesReport.Item()
                With item
                    .Fch = oDrd("ItemFch")
                    If Not IsDBNull(oDrd("ItemCustomer")) Then
                        .Customer = New DTOCustomer(oDrd("ItemCustomer"))
                        .Customer.Ref = SQLHelper.GetStringFromDataReader(oDrd("CustomerRef"))
                    End If
                    .Dept = SQLHelper.GetStringFromDataReader(oDrd("Dept"))
                    .Centro = SQLHelper.GetStringFromDataReader(oDrd("Centro"))
                    If Not IsDBNull(oDrd("Sku")) Then
                        .Sku = New DTOProductSku(oDrd("Sku"))
                        .Sku.NomLlarg.Esp = SQLHelper.GetStringFromDataReader(oDrd("SkuNomLlargEsp"))
                    End If
                    .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                    .QtyBack = SQLHelper.GetIntegerFromDataReader(oDrd("QtyBack"))
                    .Retail = SQLHelper.GetDecimalFromDataReader(oDrd("retail"))
                    .Eur = SQLHelper.GetDecimalFromDataReader(oDrd("eur"))
                End With
                value.Items.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

Public Class EdiversaSalesReportItemsLoader

    Shared Function SalesReport(exs As List(Of Exception), ByRef value As DTOSalesReport) As Boolean
        Dim sFchFrom As String = Format(value.SelectedExercici.FirstFch, "yyyyMMdd")
        Dim sFchTo As String = Format(value.SelectedExercici.LastFch, "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwEdiSalesReport.Fch, VwEdiSalesReport.Dept, VwEdiSalesReport.Centro ")
        sb.AppendLine(", VwEdiSalesReport.Customer, VwEdiSalesReport.CustomerRef ")
        sb.AppendLine(", VwEdiSalesReport.BrandGuid, VwEdiSalesReport.CategoryGuid, VwEdiSalesReport.SkuGuid ")
        sb.AppendLine(", VwEdiSalesReport.BrandNom, VwEdiSalesReport.CategoryNom, VwEdiSalesReport.SkuNom, VwEdiSalesReport.SkuRef, VwEdiSalesReport.SkuPrvNom ")
        sb.AppendLine(", VwEdiSalesReport.CountryGuid, VwEdiSalesReport.CountryISO, VwEdiSalesReport.ZonaGuid, VwEdiSalesReport.ZonaNom, VwEdiSalesReport.LocationGuid, VwEdiSalesReport.LocationNom, VwEdiSalesReport.ZipGuid ")
        sb.AppendLine(", SUM(VwEdiSalesReport.Eur) AS Eur  ")
        sb.AppendLine(", SUM(VwEdiSalesReport.Qty) AS Qty, SUM(VwEdiSalesReport.QtyBack) AS QtyBack ")
        sb.AppendLine("FROM VwEdiSalesReport ")
        sb.AppendLine("WHERE VwEdiSalesReport.Qty<>VwEdiSalesReport.QtyBack ")
        sb.AppendLine("AND VwEdiSalesReport.Holding ='" & value.SelectedHolding.Guid.ToString & "' ")
        If value.SelectedProveidor IsNot Nothing Then
            sb.AppendLine("AND VwEdiSalesReport.Proveidor='" & value.SelectedProveidor.Guid.ToString & "' ")
        End If
        sb.AppendLine("AND VwEdiSalesReport.Fch BETWEEN '" & sFchFrom & "' AND '" & sFchTo & "' ")
        sb.AppendLine("GROUP BY VwEdiSalesReport.Fch, VwEdiSalesReport.Dept, VwEdiSalesReport.Centro ")
        sb.AppendLine(", VwEdiSalesReport.Customer, VwEdiSalesReport.CustomerRef ")
        sb.AppendLine(", VwEdiSalesReport.BrandGuid, VwEdiSalesReport.CategoryGuid, VwEdiSalesReport.SkuGuid ")
        sb.AppendLine(", VwEdiSalesReport.BrandNom, VwEdiSalesReport.CategoryNom, VwEdiSalesReport.SkuNom, VwEdiSalesReport.SkuRef, VwEdiSalesReport.SkuPrvNom ")
        sb.AppendLine(", VwEdiSalesReport.CountryGuid, VwEdiSalesReport.CountryISO, VwEdiSalesReport.ZonaGuid, VwEdiSalesReport.ZonaNom, VwEdiSalesReport.LocationGuid, VwEdiSalesReport.LocationNom, VwEdiSalesReport.ZipGuid ")
        sb.AppendLine("ORDER BY VwEdiSalesReport.BrandNom, VwEdiSalesReport.BrandGuid, VwEdiSalesReport.CategoryNom, VwEdiSalesReport.CategoryGuid, VwEdiSalesReport.SkuNom, VwEdiSalesReport.SkuGuid, VwEdiSalesReport.Dept, VwEdiSalesReport.ZipGuid, VwEdiSalesReport.CustomerRef, VwEdiSalesReport.Fch  ")

        Dim SQL As String = sb.ToString
        Dim oSku As New DTOProductSku
        Dim oSkus As New List(Of DTOProductSku)
        Dim oCentro As New DTOSalesReport.Centro
        Dim oLocations As New List(Of DTOLocation)
        Dim oCustomers As New List(Of DTOCustomer)
        Dim oYearMonth As New DTOYearMonth
        Dim item As DTOSalesReport.Item = Nothing
        value.Items = New List(Of DTOSalesReport.Item)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("SkuGuid")) Then

                If Not oSku.Guid.Equals(oDrd("SkuGuid")) Then
                    oSku = SQLHelper.GetProductFromDataReader(oDrd)
                    oSkus.Add(oSku)
                End If

                Dim sCentroNom = SQLHelper.GetStringFromDataReader(oDrd("CustomerRef"))
                oCentro = value.Centros.FirstOrDefault(Function(x) x.Nom = sCentroNom)
                If oCentro Is Nothing Then
                    oCentro = value.AddCentro(sCentroNom)
                    If IsDBNull(oDrd("Customer")) Then
                        oCentro.Guid = New Guid
                    Else
                        Dim oLocation = oLocations.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("LocationGuid")))
                        If oLocation Is Nothing Then
                            oLocation = SQLHelper.GetLocationFromDataReader(oDrd)
                            oLocations.Add(oLocation)
                        End If

                        oCentro.Guid = oDrd("Customer")
                        oCentro.Location = New DTOCompactGuid(oLocation.Guid)
                    End If
                End If
                item = New DTOSalesReport.Item()
                With item
                    'If CDate(oDrd("Fch")).Month > 3 Then Stop
                    .Sku = New DTOCompactGuid(oSku.Guid)
                    .Centro = New DTOCompactGuid(oCentro.Guid)
                    .Dept = SQLHelper.GetStringFromDataReader(oDrd("Dept"))
                    .YearMonth = DTOYearMonth.FromFch(oDrd("Fch"))
                    .YearMonth.Eur = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))
                    .Qty = .Qty + oDrd("Qty") - oDrd("QtyBack")
                End With

                value.Items.Add(item)
            End If

        Loop
        oDrd.Close()

        Dim oGoyaItems = value.Items.Where(Function(a) a.Centro.Guid.Equals(New Guid("2E0EF079-FE59-435C-AF73-2423A1F4A09D"))).ToList
        Dim dcGoya = oGoyaItems.Sum(Function(x) CDec(x.YearMonth.Eur))
        'Stop

        value.Catalog = oSkus.GroupBy(Function(x) x.Category.Brand.Guid).Select(Function(y) y.First).Select(Function(z) z.Category.Brand).ToList
        Dim oCategories = oSkus.GroupBy(Function(x) x.Category.Guid).Select(Function(y) y.First).Select(Function(z) z.Category).ToList
        For Each oBrand In value.Catalog
            oBrand.Categories = oCategories.Where(Function(x) x.Brand.Equals(oBrand)).ToList
            For Each oCategory In oBrand.Categories
                oCategory.Skus = oSkus.Where(Function(x) x.Category.Equals(oCategory)).ToList
            Next
        Next

        For Each oBrand In value.Catalog
            For Each oCategory In oBrand.Categories
                oCategory.Brand = Nothing
                For Each oSku In oCategory.Skus
                    oSku.Category = Nothing
                Next
            Next
        Next

        value.IsLoaded = True
        Return True
    End Function



    Shared Function All(oStat As DTOStat) As List(Of DTOEdiversaSalesReport.Item)
        Dim retval As New List(Of DTOEdiversaSalesReport.Item)

        Dim iYear As Integer = oStat.Year
        Dim oProduct As DTOProduct = oStat.Product

        Dim FchFrom As New Date(iYear, 1, 1)
        Dim FchTo As New Date(iYear, 12, 31)
        Dim sFchFrom As String = Format(FchFrom, "yyyyMMdd")
        Dim sFchTo As String = Format(FchTo, "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwEdiSalesReport.Fch, VwEdiSalesReport.Dept, VwEdiSalesReport.Centro ")
        sb.AppendLine(", VwEdiSalesReport.Customer, VwEdiSalesReport.CustomerRef ")
        sb.AppendLine(", VwEdiSalesReport.BrandGuid, VwEdiSalesReport.CategoryGuid, VwEdiSalesReport.SkuGuid ")
        sb.AppendLine(", VwEdiSalesReport.BrandNom, VwEdiSalesReport.CategoryNom, VwEdiSalesReport.SkuNom, VwEdiSalesReport.SkuRef, VwEdiSalesReport.SkuPrvNom ")
        sb.AppendLine(", VwEdiSalesReport.Eur ")
        sb.AppendLine(", VwEdiSalesReport.Qty, VwEdiSalesReport.QtyBack ")
        sb.AppendLine(", VwEdiSalesReport.CountryGuid, VwEdiSalesReport.CountryISO, VwEdiSalesReport.ZonaGuid, VwEdiSalesReport.ZonaNom, VwEdiSalesReport.LocationGuid, VwEdiSalesReport.LocationNom, VwEdiSalesReport.ZipGuid ")
        sb.AppendLine("FROM VwEdiSalesReport ")
        If oStat.Product IsNot Nothing Then
            sb.AppendLine("INNER JOIN VwProductParent ON VwEdiSalesReport.SkuGuid = VwProductParent.Child AND VwProductParent.Parent='" & oProduct.Guid.ToString & "' ")
        End If
        sb.AppendLine("WHERE VwEdiSalesReport.Qty<>VwEdiSalesReport.QtyBack ")
        If oStat.Customer IsNot Nothing Then
            sb.AppendLine("AND VwEdiSalesReport.Ccx='" & oStat.Customer.Guid.ToString & "' ")
        End If
        If oStat.Proveidor IsNot Nothing Then
            sb.AppendLine("AND VwEdiSalesReport.Proveidor='" & oStat.Proveidor.Guid.ToString & "' ")
        End If

        'sb.AppendLine("AND Stp.Codi=" & CInt(DTOProductCategory.Codis.Standard) & " ")
        sb.AppendLine("AND VwEdiSalesReport.Fch BETWEEN '" & sFchFrom & "' AND '" & sFchTo & "' ")
        sb.AppendLine("ORDER BY VwEdiSalesReport.BrandNom, VwEdiSalesReport.BrandGuid, VwEdiSalesReport.CategoryNom, VwEdiSalesReport.CategoryGuid, VwEdiSalesReport.SkuNom, VwEdiSalesReport.SkuGuid, VwEdiSalesReport.CustomerRef ")



        Dim SQL As String = sb.ToString

        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory
        Dim oSku As New DTOProductSku

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("SkuGuid")) Then
                Dim item As New DTOEdiversaSalesReport.Item
                With item
                    If Not oSku.Guid.Equals(oDrd("SkuGuid")) Then
                        oSku = SQLHelper.GetProductFromDataReader(oDrd)
                    End If

                    .Fch = oDrd("Fch")
                    .Dept = SQLHelper.GetStringFromDataReader(oDrd("Dept"))
                    .Centro = SQLHelper.GetStringFromDataReader(oDrd("Centro"))
                    If Not IsDBNull(oDrd("Customer")) Then
                        .Customer = New DTOCustomer(oDrd("Customer"))
                        .Customer.Address = SQLHelper.GetAddressFromDataReader(oDrd)
                        .Customer.Nom = SQLHelper.GetStringFromDataReader(oDrd("CustomerRef"))
                    End If
                    .Sku = oSku
                    .Qty = oDrd("Qty")
                    .QtyBack = oDrd("QtyBack")
                    .Eur = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))

                End With

                retval.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Cataleg(iYear As Integer, Optional oCustomer As DTOCustomer = Nothing, Optional oProveidor As DTOProveidor = Nothing) As DTOProductCatalog
        Dim retval As New DTOProductCatalog
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.CategoryGuid, EdiversaSalesReportItem.Sku ")
        sb.AppendLine(", VwSkuNom.BrandNomEsp, VwSkuNom.CategoryNomEsp, VwSkuNom.SkuRef +' '+ VwSkuNom.SkuPrvNom AS SkuNom ")
        sb.AppendLine("FROM EdiversaSalesReportItem ")
        sb.AppendLine("INNER JOIN VwSkuNom ON EdiversaSalesReportItem.Sku=VwSkuNom.SkuGuid ")

        If oProveidor IsNot Nothing Then
            sb.AppendLine("AND VwSkuNom.Proveidor='" & oProveidor.Guid.ToString & "' ")
        End If

        If oCustomer IsNot Nothing Then
            sb.AppendLine("INNER JOIN EdiversaSalesReportHeader ON EdiversaSalesReportItem.Parent=EdiversaSalesReportHeader.Guid AND EdiversaSalesReportHeader.Customer='" & oCustomer.Guid.ToString & "' ")
        End If

        If oCustomer IsNot Nothing Then
            sb.AppendLine("WHERE EdiversaSalesReportHeader.Customer='" & oCustomer.Guid.ToString & "' ")
        End If

        sb.AppendLine("AND Qty<>QtyBack ")
        sb.AppendLine("AND VwSkuNom.CategoryCodi=" & CInt(DTOProductCategory.Codis.standard) & " ")
        sb.AppendLine("AND Year(EdiversaSalesReportItem.Fch)=" & iYear & " ")
        sb.AppendLine("GROUP BY VwSkuNom.BrandGuid, VwSkuNom.CategoryGuid, EdiversaSalesReportItem.Sku ")
        sb.AppendLine(", VwSkuNom.BrandNomEsp, VwSkuNom.CategoryNomEsp, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandNomEsp, VwSkuNom.BrandGuid, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryGuid, SkuNom, Sku ")
        Dim SQL As String = sb.ToString

        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNomEsp", "BrandNomEsp", "BrandNomEsp", "BrandNomEsp")
                oBrand.Categories = New List(Of DTOProductCategory)
                retval.Brands.Add(oBrand)
            End If
            If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                SQLHelper.LoadLangTextFromDataReader(oCategory.Nom, oDrd, "CategoryNomEsp", "CategoryNomEsp", "CategoryNomEsp", "CategoryNomEsp")
                oCategory.Skus = New List(Of DTOProductSku)
                oBrand.Categories.Add(oCategory)
            End If
            Dim oSku As New DTOProductSku(oDrd("Sku"))
            SQLHelper.LoadLangTextFromDataReader(oSku.Nom, oDrd, "SkuNom", "SkuNom", "SkuNom", "SkuNom")
            oCategory.Skus.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function StatItems(iYear As Integer, oLang As DTOLang, oProduct As DTOProduct, Optional oCustomer As DTOCustomer = Nothing, Optional oProveidor As DTOProveidor = Nothing) As DTOStat
        Dim retval As New DTOStat(DTOStat.ConceptTypes.Geo, oLang)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliClient.Guid AS CliGuid, (CASE WHEN CliClient.Ref IS NULL THEN EdiversaSalesReportItem.Centro ELSE CliClient.Ref END) AS CliRef ")
        sb.AppendLine(", Month(EdiversaSalesReportItem.Fch) AS Mes ")
        sb.AppendLine(", EdiversaSalesReportItem.Eur ")
        sb.AppendLine(", EdiversaSalesReportItem.Qty-EdiversaSalesReportItem.QtyBack AS Qty ")
        sb.AppendLine("FROM EdiversaSalesReportItem ")

        If oProduct IsNot Nothing Then
            sb.AppendLine("INNER JOIN VwProductParent ON EdiversaSalesReportItem.Sku=VwProductParent.Child AND VwProductParent.Parent = '" & oProduct.Guid.ToString & "' ")
        End If

        sb.AppendLine("INNER JOIN Art ON EdiversaSalesReportItem.Sku=Art.Guid ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category=Stp.Guid ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand=Tpa.Guid ")
        If oProveidor IsNot Nothing Then
            sb.AppendLine("AND Tpa.Proveidor='" & oProveidor.Guid.ToString & "' ")
        End If

        If oCustomer IsNot Nothing Then
            sb.AppendLine("INNER JOIN EdiversaSalesReportHeader ON EdiversaSalesReportItem.Parent=EdiversaSalesReportHeader.Guid AND EdiversaSalesReportHeader.Customer='" & oCustomer.Guid.ToString & "' ")
        End If

        sb.AppendLine("INNER JOIN CliClient ON EdiversaSalesReportItem.Customer=CliClient.Guid ")

        If oCustomer IsNot Nothing Then
            sb.AppendLine("WHERE EdiversaSalesReportHeader.Customer='" & oCustomer.Guid.ToString & "' ")
        End If

        sb.AppendLine("AND Qty<>QtyBack ")
        sb.AppendLine("AND Stp.Codi=" & CInt(DTOProductCategory.Codis.standard) & " ")
        sb.AppendLine("AND Year(EdiversaSalesReportItem.Fch)=" & iYear & " ")
        sb.AppendLine("ORDER BY CliRef, CliGuid, Month(EdiversaSalesReportItem.Fch) ")
        Dim SQL As String = sb.ToString

        Dim item As New DTOStatItem(retval, Guid.NewGuid, "")

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read
            Try
                If Not item.Key.Equals(oDrd("CliGuid")) Then
                    item = New DTOStatItem(retval, oDrd("CliGuid"), oDrd("CliRef"))
                    retval.Items.Add(item)
                End If
                Dim iMesIdx As Integer = oDrd("Mes") - 1
                Dim DcPrevious As Decimal = item.Values(iMesIdx)
                Dim iQty As Integer = oDrd("Qty")
                Dim DcPreu As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))
                item.Values(iMesIdx) += iQty * DcPreu

            Catch ex As Exception
            End Try
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function StatItems2(oUser As DTOUser, year As Integer, oHolding As DTOHolding) As DTOStat2
        Dim retval As New DTOStat2
        Dim oLang = DTOLang.ENG()
        Dim oSkus As New List(Of DTOProductSku)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliClient.Guid AS CliGuid, (CASE WHEN CliClient.Ref = '' THEN (CASE WHEN EdiversaSalesReportItem.Centro IS NULL THEN CliGral.RaoSocial END) ELSE CliClient.Ref END) AS CliRef ")
        sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNomEsp, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuRef, VwSkuNom.SkuGuid ")
        sb.AppendLine(", Month(EdiversaSalesReportItem.Fch) AS Mes ")
        sb.AppendLine(", SUM(EdiversaSalesReportItem.Eur) AS Eur ")
        sb.AppendLine(", SUM(EdiversaSalesReportItem.Qty-EdiversaSalesReportItem.QtyBack) AS Qty ")
        sb.AppendLine("FROM EdiversaSalesReportItem ")

        sb.AppendLine("INNER JOIN VwSkuNom ON EdiversaSalesReportItem.Sku=VwSkuNom.SkuGuid ")
        If oUser.Rol.id = DTORol.Ids.manufacturer Then
            sb.AppendLine("AND VwSkuNom.Proveidor='" & oUser.Contact.Guid.ToString & "' ")
        End If

        sb.AppendLine("INNER JOIN CliClient ON EdiversaSalesReportItem.Customer=CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliClient Ccx ON CliClient.CcxGuid = Ccx.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON CliClient.Guid = CliGral.Guid ")

        sb.AppendLine("WHERE Ccx.Holding = '" & oHolding.Guid.ToString() & "' ")
        sb.AppendLine("AND Qty<>QtyBack ")
        sb.AppendLine("AND Year(EdiversaSalesReportItem.Fch)=" & year & " ")
        sb.AppendLine("GROUP BY CliClient.Ref, CliClient.Guid, EdiversaSalesReportItem.Centro, CliGral.RaoSocial, VwSkuNom.SkuRef, VwSkuNom.SkuGuid ")
        sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNomEsp, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuRef, VwSkuNom.SkuGuid ")
        sb.AppendLine(", Month(EdiversaSalesReportItem.Fch) ")
        sb.AppendLine("ORDER BY VwSkuNom.SkuGuid, Month(EdiversaSalesReportItem.Fch) ")
        Dim SQL As String = sb.ToString
        Dim oSku As New DTOProductSku
        Dim oCustomer As New DTOGuidNom.Compact
        'Dim categoryNomField = oLang.Tradueix("CategoryNomEsp", "CategoryNomCat", "CategoryNomEng")
        'Dim skuNomField = oLang.Tradueix("SkuNomEsp", "SkuNomCat", "SkuNomEng")
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Try
                If Not oSku.Guid.Equals(oDrd("SkuGuid")) Then
                    'oSku = SQLHelper.GetProductFromDataReader(oDrd,,,, categoryNomField,, skuNomField)
                    oSku = SQLHelper.GetProductFromDataReader(oDrd)
                    oSkus.Add(oSku)
                End If
                If Not retval.Customers.Any(Function(x) x.Guid.Equals(oDrd("CliGuid"))) Then
                    oCustomer = New DTOGuidNom.Compact
                    oCustomer.Guid = oDrd("CliGuid")
                    oCustomer.Nom = SQLHelper.GetStringFromDataReader(oDrd("CliRef"))
                    retval.Customers.Add(oCustomer)
                End If
                Dim item As New DTOStat2.Item
                With item
                    .Sku = oSku.Guid
                    .Month = oDrd("Mes")
                    .Customer = oDrd("CliGuid")
                    .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                    .Eur = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))
                End With
                retval.Items.Add(item)

            Catch ex As Exception

            End Try
        Loop
        oDrd.Close()
        oSkus = oSkus.OrderBy(Function(x) x.Nom.Tradueix(oLang)).OrderBy(Function(y) y.Category.Nom.Tradueix(oLang)).OrderBy(Function(z) z.Category.Brand.Nom.Tradueix(oLang)).ToList()
        retval.Catalog = DTOCatalog.Factory(oSkus, oLang)
        retval.Customers = retval.Customers.OrderBy(Function(x) x.Nom).ToList()
        Return retval
    End Function


    Shared Function Excel(oUser As DTOUser, year As Integer, oHolding As DTOHolding, units As DTOStat2.Units, oBrand As DTOBaseGuid, oCategory As DTOBaseGuid, oSku As DTOBaseGuid) As MatHelper.Excel.Sheet
        Dim oLang As DTOLang = DTOLang.ENG
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT (CASE WHEN CliClient.Ref = '' THEN (CASE WHEN EdiversaSalesReportItem.Centro IS NULL THEN CliGral.RaoSocial END) ELSE CliClient.Ref END) AS CliRef ")
        sb.AppendLine(", VwSkuNom.BrandNom, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.SkuNomEsp, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuRef ")

        For i = 1 To 12
            If units = DTOStat2.Units.Eur Then
                sb.AppendLine(", SUM(CASE WHEN Month(EdiversaSalesReportItem.Fch) =" & i & " THEN EdiversaSalesReportItem.Eur ELSE 0 END) AS Eur" & i & " ")
            Else
                sb.AppendLine(", SUM(CASE WHEN Month(EdiversaSalesReportItem.Fch) =" & i & " THEN EdiversaSalesReportItem.Qty-EdiversaSalesReportItem.QtyBack ELSE 0 END) AS Qty" & i & " ")
            End If
        Next

        sb.AppendLine("FROM EdiversaSalesReportItem ")

        sb.AppendLine("INNER JOIN VwSkuNom ON EdiversaSalesReportItem.Sku=VwSkuNom.SkuGuid ")
        If oUser.Rol.id = DTORol.Ids.manufacturer Then
            sb.AppendLine("AND VwSkuNom.Proveidor='" & oUser.Contact.Guid.ToString & "' ")
        End If
        If oSku IsNot Nothing Then
            sb.AppendLine("AND VwSkuNom.SkuGuid='" & oSku.Guid.ToString & "' ")
        ElseIf oCategory IsNot Nothing Then
            sb.AppendLine("AND VwSkuNom.CategoryGuid='" & oCategory.Guid.ToString & "' ")
        ElseIf oBrand IsNot Nothing Then
            sb.AppendLine("AND VwSkuNom.BrandGuid='" & oBrand.Guid.ToString & "' ")
        End If

        sb.AppendLine("INNER JOIN CliClient ON EdiversaSalesReportItem.Customer=CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliClient Ccx ON CliClient.CcxGuid = Ccx.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON CliClient.Guid = CliGral.Guid ")

        sb.AppendLine("WHERE Ccx.Holding = '" & oHolding.Guid.ToString() & "' ")
        sb.AppendLine("AND Qty<>QtyBack ")
        sb.AppendLine("AND Year(EdiversaSalesReportItem.Fch)=" & year & " ")
        sb.AppendLine("GROUP BY CliClient.Ref, CliClient.Guid, EdiversaSalesReportItem.Centro, CliGral.RaoSocial, VwSkuNom.SkuRef, VwSkuNom.SkuGuid ")
        sb.AppendLine(", VwSkuNom.BrandNom, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.SkuNomEsp, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuRef ")
        sb.AppendLine("ORDER BY CliRef, CliClient.Guid, VwSkuNom.BrandNom, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.SkuNomEsp, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuGuid ")
        Dim SQL As String = sb.ToString

        Dim retval As New MatHelper.Excel.Sheet("M+O ECI SellOut")

        retval.DisplayTotals = True
        With retval
            .AddColumn("Customer")
            .AddColumn("Brand")
            .AddColumn("Category")
            .AddColumn("Product")
            .AddColumn("Code")
            For i = 1 To 12
                Select Case units
                    Case DTOStat2.Units.Eur
                        .AddColumn(oLang.MesAbr(i) & " amount", MatHelper.Excel.Cell.NumberFormats.Euro)
                    Case DTOStat2.Units.Units
                        .AddColumn(oLang.MesAbr(i) & " units", MatHelper.Excel.Cell.NumberFormats.Integer)
                End Select
            Next
        End With

        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRow = retval.AddRow
            oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("CliRef")))
            oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("BrandNom")))
            oRow.AddCell(SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng").Tradueix(oLang))
            oRow.AddCell(SQLHelper.GetLangTextFromDataReader(oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng").Tradueix(oLang))
            oRow.AddCell(oDrd("SkuRef"))
            For i = 1 To 12
                Select Case units
                    Case DTOStat2.Units.Eur
                        Dim fieldname = String.Format("Eur{0}", i)
                        If IsDBNull(oDrd(fieldname)) Then
                            oRow.AddCell()
                        Else
                            oRow.AddCell(oDrd(fieldname), "", MatHelper.Excel.Cell.NumberFormats.Euro)
                        End If
                    Case DTOStat2.Units.Units
                        Dim fieldname = String.Format("Qty{0}", i)
                        If IsDBNull(oDrd(fieldname)) Then
                            oRow.AddCell()
                        Else
                            oRow.AddCell(oDrd(fieldname), "", MatHelper.Excel.Cell.NumberFormats.Integer)
                        End If
                End Select
            Next

        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

