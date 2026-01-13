Public Class PriceListSupplierLoader

    Shared Function Find(oGuid As Guid) As DTOPriceListSupplier
        Dim retval As DTOPriceListSupplier = Nothing
        Dim oPriceList As New DTOPriceListSupplier(oGuid)
        If Load(oPriceList) Then
            retval = oPriceList
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPriceList As DTOPriceListSupplier) As Boolean
        If Not oPriceList.IsLoaded And Not oPriceList.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT PriceList_Supplier.*, PriceListItem_Supplier.*, CliGral.FullNom, VwSkuNom.SkuId, VwSkuNom.SkuGuid, VwSkuNom.SkuRef, VwSkuNom.SkuNomLlargEsp ")
            sb.AppendLine(", (CASE WHEN PriceList_Supplier.Currency='EUR' THEN 1 ELSE CurExchangeRate.Rate END) AS Rate ")
            sb.AppendLine("FROM PriceList_Supplier ")
            sb.AppendLine("INNER JOIN CliGral ON PriceList_Supplier.Proveidor = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN PriceListItem_Supplier ON PriceList_Supplier.Guid = PriceListItem_Supplier.PriceList ")

            sb.AppendLine("LEFT OUTER JOIN (SELECT VwSkuNom.Proveidor, VwSkuNom.SkuRef, MAX(VwSkuNom.SkuId) AS SkuId FROM VwSkuNom GROUP BY VwSkuNom.Proveidor, VwSkuNom.SkuRef) X ")
            sb.AppendLine("     ON PriceList_Supplier.Proveidor = X.Proveidor AND PriceListItem_Supplier.Ref = X.SkuRef ")
            sb.AppendLine("     LEFT OUTER JOIN VwSkuNom ON PriceList_Supplier.Proveidor = VwSkuNom.Proveidor AND X.SkuId = VwSkuNom.SkuId ")

            sb.AppendLine("LEFT OUTER JOIN ( SELECT Q1.ISO, Q1.Fch, Q1.Rate FROM CurExchangeRate Q1 ")
            sb.AppendLine("                  INNER JOIN (SELECT ISO, Max(Fch) AS LastFch FROM CurExchangeRate GROUP BY ISO) Q2 ON Q1.ISO=Q2.ISO AND Q1.Fch=Q2.LastFch) ")
            sb.AppendLine("                 CurExchangeRate ON PriceList_Supplier.Currency=CurExchangeRate.ISO ")

            sb.AppendLine("WHERE PriceList_Supplier.Guid='" & oPriceList.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY PriceListItem_Supplier.Lin")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oPriceList.IsLoaded Then
                    Dim oProveidor As New DTOProveidor(oDrd("Proveidor"))
                    With oProveidor
                        .FullNom = oDrd("FullNom")
                    End With
                    With oPriceList
                        .Proveidor = oProveidor
                        .Fch = CDate(oDrd("Fch"))
                        .Concepte = oDrd("Concepte").ToString
                        .Cur = DTOCur.Factory(oDrd("Currency"))
                        .Discount_OnInvoice = CDec(oDrd("Discount_OnInvoice"))
                        .Discount_OffInvoice = CDec(oDrd("Discount_OffInvoice"))
                        .Items = New List(Of DTOPriceListItem_Supplier)
                        .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("PriceList")) Then
                    Dim oItem As New DTOPriceListItem_Supplier
                    With oItem
                        .Parent = oPriceList
                        .Ref = oDrd("Ref")
                        .EAN = oDrd("EAN").ToString
                        .Description = oDrd("Description").ToString
                        .Price = CDec(oDrd("Price"))
                        .Lin = oDrd("Lin")
                        If Not IsDBNull(oDrd("InnerPack")) Then
                            .InnerPack = CDec(oDrd("InnerPack"))
                        End If
                        If Not IsDBNull(oDrd("Retail")) Then
                            .Retail = CDec(oDrd("Retail"))
                        End If
                        If Not IsDBNull(oDrd("SkuGuid")) Then
                            Dim oSkuGuid As Guid = oDrd("SkuGuid")
                            Dim oSku As New DTOProductSku(oSkuGuid)
                            With oSku
                                .Id = CInt(oDrd("SkuId"))
                                .NomLlarg.Esp = oDrd("SkuNomLlargEsp")
                                .RefProveidor = oDrd("SkuRef")
                            End With

                            .Sku = oSku
                            .SkuGuid = oSkuGuid
                        End If
                    End With
                    oPriceList.Items.Add(oItem)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oPriceList.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPriceList As DTOPriceListSupplier, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oPriceList, oTrans)
            oTrans.Commit()
            oPriceList.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oPriceList As DTOPriceListSupplier, ByRef oTrans As SqlTransaction)
        DocFileLoader.Update(oPriceList.DocFile, oTrans)
        UpdateHeader(oPriceList, oTrans)
        If Not oPriceList.IsNew Then DeleteItems(oPriceList, oTrans)
        UpdateItems(oPriceList, oTrans)
    End Sub

    Protected Shared Sub UpdateHeader(oPriceList As DTOPriceListSupplier, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM PriceList_Supplier WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oPriceList.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPriceList.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPriceList
            oRow("Proveidor") = .Proveidor.Guid
            oRow("Fch") = .Fch
            oRow("Concepte") = .Concepte
            oRow("Currency") = .Cur.Tag
            oRow("Discount_OnInvoice") = .Discount_OnInvoice
            oRow("Discount_OffInvoice") = .Discount_OffInvoice
            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)
        End With

        oDA.Update(oDs)
    End Sub

    Protected Shared Sub UpdateItems(oPriceList As DTOPriceListSupplier, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM PriceListItem_Supplier WHERE PriceList = '" & oPriceList.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)


        Dim iLin As Integer
        For Each oItem As DTOPriceListItem_Supplier In oPriceList.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)

            oRow("PriceList") = oPriceList.Guid
            oRow("Lin") = iLin
            oRow("Ref") = oItem.Ref
            oRow("EAN") = oItem.EAN
            oRow("Description") = Left(oItem.Description, 50)
            oRow("Price") = oItem.Price
            If oItem.InnerPack = 0 Then
                oRow("InnerPack") = System.DBNull.Value
            Else
                oRow("InnerPack") = oItem.InnerPack
            End If
            If oItem.Retail = 0 Then
                oRow("Retail") = System.DBNull.Value
            Else
                oRow("Retail") = oItem.Retail
            End If
            iLin += 1
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function LastLin(oPriceList As DTOPriceListSupplier, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim SQL As String = "SELECT MAX(Lin) AS LastLin FROM PriceListItem_Supplier WHERE PriceList = @Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@GUID", oPriceList.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastLin")) Then
                retval = oRow("LastLin")
            End If
        End If
        Return retval
    End Function

    Shared Function Delete(oPriceList As DTOPriceListSupplier, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPriceList, oTrans)
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

    Shared Sub Delete(oPriceList As DTOPriceListSupplier, ByRef oTrans As SqlTransaction)
        DeleteItems(oPriceList, oTrans)
        DeleteHeader(oPriceList, oTrans)
        DocFileLoader.Delete(oPriceList.DocFile, oTrans)
    End Sub

    Protected Shared Sub DeleteItems(oPriceList As DTOPriceListSupplier, ByRef oTrans As SqlTransaction)
        With oPriceList
            Dim SQL As String = "DELETE PriceListItem_Supplier WHERE PriceList=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPriceList.Guid.ToString())
        End With
    End Sub

    Protected Shared Sub DeleteHeader(oPriceList As DTOPriceListSupplier, ByRef oTrans As SqlTransaction)
        With oPriceList
            Dim SQL As String = "DELETE PriceList_Supplier WHERE Guid=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPriceList.Guid.ToString())
        End With
    End Sub

End Class

Public Class PriceListsSupplierLoader

    Shared Function FromProveidor(oProveidor As DTOProveidor) As List(Of DTOPriceListSupplier)
        Dim retval As New List(Of DTOPriceListSupplier)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PriceList_Supplier.* ")
        sb.AppendLine(", Docfile.Mime ")
        sb.AppendLine("FROM PriceList_Supplier ")
        sb.AppendLine("LEFT OUTER JOIN DocFile ON PriceList_Supplier.Hash = Docfile.Hash ")
        sb.AppendLine("WHERE PriceList_Supplier.Proveidor='" & oProveidor.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY PriceList_Supplier.Fch DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oPriceList As New DTOPriceListSupplier(oDrd("Guid"))
            With oPriceList
                .Proveidor = oProveidor
                .Fch = CDate(oDrd("Fch"))
                .Concepte = oDrd("Concepte").ToString
                .Cur = DTOCur.Factory(oDrd("Currency"))
                .Discount_OnInvoice = CDec(oDrd("Discount_OnInvoice"))
                .Discount_OffInvoice = CDec(oDrd("Discount_OffInvoice"))
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                    .DocFile.Mime = SQLHelper.GetIntegerFromDataReader(oDrd("Mime"))
                End If
            End With
            retval.Add(oPriceList)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Costs(oProveidor As DTOProveidor) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM VwSkuCost ")
        sb.AppendLine("INNER JOIN VwSkuNom ON VwSkuCost.SkuGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE VwSkuCost.Proveidor='" & oProveidor.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.CategoryOrd, VwSkuNom.SkuNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With item
                .Cost = SQLHelper.GetAmtFromDataReader(oDrd("Price"))
                .SupplierDtoOnInvoice = SQLHelper.GetDecimalFromDataReader(oDrd("Discount_OnInvoice"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class PriceListItemSupplierLoader

    Shared Function Update(oPriceListItem As DTOPriceListItem_Supplier, exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oPriceListItem, oTrans)
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

    Shared Sub Update(oPriceListItem As DTOPriceListItem_Supplier, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM PriceListItem_Supplier WHERE PriceList ='" & oPriceListItem.Parent.Guid.ToString & "' AND Ref='" & oPriceListItem.Ref & "' "
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("PriceList") = oPriceListItem.Parent.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPriceListItem
            oRow("Ref") = .Ref
            If .Lin > 0 Then
                oRow("Lin") = .Lin
            Else
                Dim iLastLin As Integer = PriceListSupplierLoader.LastLin(oPriceListItem.Parent, oTrans)
                oRow("Lin") = iLastLin + 1
            End If
            oRow("EAN") = .EAN
            oRow("Description") = Left(.Description, 50)
            oRow("Price") = .Price
            If .InnerPack = 0 Then
                oRow("InnerPack") = System.DBNull.Value
            Else
                oRow("InnerPack") = .InnerPack
            End If
            If .Retail = 0 Then
                oRow("Retail") = System.DBNull.Value
            Else
                oRow("Retail") = .Retail
            End If
        End With


        oDA.Update(oDs)
        oPriceListItem.IsNew = False
    End Sub




    Shared Function Delete(oPriceListItem As DTOPriceListItem_Supplier, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPriceListItem, oTrans)
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

    Shared Sub Delete(oPriceListItem As DTOPriceListItem_Supplier, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PriceListItem_Supplier WHERE PriceList = @Guid AND Ref=@Ref"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPriceListItem.Parent.Guid.ToString, "@Ref", oPriceListItem.Ref)
    End Sub
End Class


Public Class PriceListItemsSupplierLoader

    Shared Function Vigent(oProveidor As DTOProveidor) As List(Of DTOPriceListItem_Supplier)
        Dim retval As New List(Of DTOPriceListItem_Supplier)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT *  ")
        sb.AppendLine("FROM VwSkuCost  ")
        sb.AppendLine("INNER JOIN VwSkuNom ON VwSkuCost.SkuGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE VwSkuCost.Proveidor = '" & oProveidor.Guid.ToString & "' ")
        sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")
        sb.AppendLine("ORDER BY VwSkuCost.PricelistFch DESC, VwSkuCost.PricelistGuid, VwSkuNom.BrandNom, VwSkuNom.CategoryNom, VwSkuNom.SkuNom ")

        Dim oTarifa As New DTOPriceListSupplier
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oTarifa.Guid.Equals(oDrd("PricelistGuid")) Then
                oTarifa = New DTOPriceListSupplier(oDrd("PricelistGuid"))
                With oTarifa
                    .Fch = oDrd("PricelistFch")
                    .Proveidor = oProveidor
                    .Cur = DTOCur.Factory(oDrd("Cur"))
                    .Discount_OnInvoice = CDec(oDrd("Discount_OnInvoice"))
                    .Items = New List(Of DTOPriceListItem_Supplier)
                    If Not IsDBNull(oDrd("Hash")) Then
                        .DocFile = New DTODocFile(oDrd("Hash").ToString())
                    End If
                End With
            End If
            Dim item As New DTOPriceListItem_Supplier()
            With item
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                .Ref = .Sku.RefProveidor
                .Description = .Sku.NomProveidor
                .Price = SQLHelper.GetDecimalFromDataReader(oDrd("Price"))
                .Parent = oTarifa
            End With
            oTarifa.Items.Add(item)
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp) As List(Of DTOPriceListItem_Supplier)
        Dim retval As New List(Of DTOPriceListItem_Supplier)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.SkuRef, VwSkuNom.BrandGuid, VwSkuNom.BrandNomEsp, VwSkuNom.CategoryGuid, VwSkuNom.CategoryNomEsp, VwSkuNom.SkuGuid, VwSkuNom.SkuNomEsp ")
        sb.AppendLine(", Q2.Price, Q1.Discount_OnInvoice, Q1.Discount_OffInvoice, Q1.Hash ")
        sb.AppendLine(", Q1.Proveidor, CliGral.FullNom, Q1.Guid AS Q1Guid, Q2Guid AS Q2Guid, Q1.Fch  ")
        sb.AppendLine(", Docfile.Mime ")
        sb.AppendLine("FROM PriceListItem_Supplier Q2  ")
        sb.AppendLine("INNER JOIN PriceList_Supplier Q1 ON Q2.PriceList=Q1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Docfile On Q1.Hash = Docfile.Hash ")
        sb.AppendLine("INNER JOIN CliGral ON Q1.Proveidor=CliGral.Guid ")
        sb.AppendLine("                 INNER JOIN VwSkuNom ON Q2.Ref=VwSkuNom.SkuRef AND VwSkuNom.SkuRef>'' AND VwSkuNom.Proveidor=Q1.Proveidor ")
        sb.AppendLine("                 INNER JOIN (SELECT MAX(PriceList_Supplier.Fch) AS Fch, PriceList_Supplier.Proveidor, PriceListItem_Supplier.Ref  ")
        sb.AppendLine("                 FROM PriceList_Supplier  ")
        sb.AppendLine("                 INNER JOIN PriceListItem_Supplier ON PriceList_Supplier.Guid = PriceListItem_Supplier.PriceList AND PriceList_Supplier.Fch <= GETDATE() ")
        sb.AppendLine("                 GROUP BY PriceList_Supplier.Proveidor, PriceListItem_Supplier.Ref) Q3 ON Q1.Fch=Q3.Fch AND Q1.Proveidor=Q3.Proveidor AND Q2.Ref=Q3.Ref ")
        sb.AppendLine("                 ORDER BY VwSkuNom.BrandNomEsp, VwSkuNom.CategoryNomEsp, VwSkuNom.SkuNomEsp ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProveidor As New DTOProveidor(oDrd("Proveidor"))
            oProveidor.FullNom = oDrd("FullNom")
            Dim oPriceList As New DTOPriceListSupplier(oDrd("Q1"))
            With oPriceList
                .Fch = oDrd("Fch")
                .Proveidor = oProveidor
                .Discount_OnInvoice = CDec(oDrd("Discount_OnInvoice"))
                .Discount_OffInvoice = CDec(oDrd("Discount_OffInvoice"))
                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                .Items = New List(Of DTOPriceListItem_Supplier)
            End With
            Dim oItem As New DTOPriceListItem_Supplier
            With oItem
                .Parent = oPriceList
                .Price = CDec(oDrd("Price"))
                If Not IsDBNull(oDrd("Retail")) Then
                    .Retail = CDec(oDrd("Retail"))
                End If
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
            End With
            oPriceList.Items.Add(oItem)
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oSku As DTOProductSku) As List(Of DTOPriceListItem_Supplier)
        Dim retval As New List(Of DTOPriceListItem_Supplier)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PriceList_Supplier.Fch, PriceList_Supplier.Proveidor, PriceList_Supplier.Concepte, PriceList_Supplier.Currency, PriceList_Supplier.Discount_OnInvoice, PriceList_Supplier.Discount_OffInvoice, PriceList_Supplier.Hash ")
        sb.AppendLine(", PriceListItem_Supplier.PriceList, PriceListItem_Supplier.EAN, PriceListItem_Supplier.Description ")
        sb.AppendLine(", PriceListItem_Supplier.Price, PriceListItem_Supplier.Retail, PriceListItem_Supplier.InnerPack ")
        sb.AppendLine(", Tpa.Proveidor, CliGral.FullNom ")
        sb.AppendLine(", DocFile.Mime ")
        sb.AppendLine("From Art ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category=Stp.Guid ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand=Tpa.Guid  ")
        sb.AppendLine("INNER JOIN CliGral ON Tpa.Proveidor=CliGral.Guid  ")
        sb.AppendLine("INNER JOIN PriceListItem_Supplier ON Art.Ref=PriceListItem_Supplier.Ref ")
        sb.AppendLine("INNER JOIN PriceList_Supplier ON PriceList_Supplier.Guid=PriceListItem_Supplier.PriceList AND Tpa.Proveidor=PriceList_Supplier.Proveidor ")
        sb.AppendLine("LEFT OUTER JOIN DocFile ON PriceList_Supplier.Hash = Docfile.Hash ")
        sb.AppendLine("WHERE ART.Guid='" & oSku.Guid.ToString & "' AND PriceList_Supplier.Proveidor=Tpa.Proveidor ")
        sb.AppendLine("ORDER BY PriceList_Supplier.Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProveidor As New DTOProveidor(oDrd("Proveidor"))
            oProveidor.FullNom = oDrd("FullNom")
            Dim oPriceList As New DTOPriceListSupplier(oDrd("PriceList"))
            With oPriceList
                .Fch = oDrd("Fch")
                .Concepte = oDrd("Concepte")
                .Proveidor = oProveidor
                .Cur = DTOCur.Factory(oDrd("Currency"))
                .Discount_OnInvoice = CDec(oDrd("Discount_OnInvoice"))
                .Discount_OffInvoice = CDec(oDrd("Discount_OffInvoice"))
                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                If .DocFile IsNot Nothing Then
                    .DocFile.Mime = SQLHelper.GetIntegerFromDataReader(oDrd("Mime"))
                End If
                .Items = New List(Of DTOPriceListItem_Supplier)
            End With
            Dim oItem As New DTOPriceListItem_Supplier
            With oItem
                .Parent = oPriceList
                .EAN = oDrd("EAN").ToString
                .Ref = oSku.RefProveidor
                .Description = oDrd("Description").ToString
                .Price = CDec(oDrd("Price"))
                If Not IsDBNull(oDrd("InnerPack")) Then
                    .InnerPack = CDec(oDrd("InnerPack"))
                End If
                If Not IsDBNull(oDrd("Retail")) Then
                    .Retail = CDec(oDrd("Retail"))
                End If
                .Sku = oSku
            End With
            oPriceList.Items.Add(oItem)
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(items As List(Of DTOPriceListItem_Supplier), exs As List(Of Exception)) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE PriceListItem_Supplier ")
        sb.AppendLine("WHERE PriceListItem_Supplier.PriceList = '" & items.First.Parent.Guid.ToString & "' ")
        sb.AppendLine("AND ( ")
        Dim firstItem As Boolean = True
        For Each item In items
            If firstItem Then
                firstItem = False
            Else
                sb.Append("OR ")
            End If
            sb.AppendLine("PriceListItem_Supplier.Ref = '" & item.Ref & "' ")
        Next
        sb.AppendLine(") ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function FromSku(oProveidor As DTOProveidor, sRef As String, DtFch As Date) As List(Of DTOPriceListItem_Supplier)
        Dim retval As New List(Of DTOPriceListItem_Supplier)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Select PriceList_Supplier.Fch, PriceList_Supplier.Proveidor, PriceList_Supplier.Currency, PriceList_Supplier.Discount_OnInvoice, PriceList_Supplier.Discount_OffInvoice, PriceList_Supplier.Hash ")
        sb.AppendLine(", VwCurExchange.Rate As CurExchangeRate, VwCurExchange.Fch As CurExchangeFch ")
        sb.AppendLine(", PriceListItem_Supplier.PriceList, PriceListItem_Supplier.EAN, PriceListItem_Supplier.Description ")
        sb.AppendLine(", PriceListItem_Supplier.Price, PriceListItem_Supplier.Retail, PriceListItem_Supplier.InnerPack ")
        sb.AppendLine(", VwSkuNom.SkuId, VwSkuNom.SkuGuid ")
        sb.AppendLine(", Docfile.Mime ")
        sb.AppendLine("FROM PriceList_Supplier ")
        sb.AppendLine("LEFT OUTER JOIN Docfile On PriceList_Supplier.Hash = Docfile.Hash ")
        sb.AppendLine("LEFT OUTER JOIN PriceListItem_Supplier On PriceList_Supplier.Guid = PriceListItem_Supplier.PriceList ")
        sb.AppendLine("LEFT OUTER JOIN VwCurExchange On PriceList_Supplier.Currency = VwCurExchange.Tag ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT VwSkuNom.Proveidor, VwSkuNom.SkuRef, MAX(VwSkuNom.SkuId) AS SkuId FROM VwSkuNom GROUP BY VwSkuNom.Proveidor, VwSkuNom.SkuRef) X ")
        sb.AppendLine("     ON PriceList_Supplier.Proveidor = X.Proveidor AND PriceListItem_Supplier.Ref = X.SkuRef ")
        sb.AppendLine("     LEFT OUTER JOIN VwSkuNom ON PriceList_Supplier.Proveidor = VwSkuNom.Proveidor AND X.SkuId = VwSkuNom.SkuId ")

        sb.AppendLine("WHERE PriceList_Supplier.Proveidor = '" & oProveidor.Guid.ToString & "' ")
        sb.AppendLine("AND PriceListItem_Supplier.Ref = '" & sRef & "' ")
        sb.AppendLine("ORDER BY PriceList_Supplier.Fch")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Fch", Format(DtFch, "yyyyMMdd"))
        Do While oDrd.Read
            Dim oPriceList As New DTOPriceListSupplier(oDrd("PriceList"))
            With oPriceList
                .Fch = oDrd("Fch")
                .Proveidor = oProveidor
                .Cur = DTOCur.Factory(oDrd("Currency")).Clon
                .Discount_OnInvoice = CDec(oDrd("Discount_OnInvoice"))
                .Discount_OffInvoice = CDec(oDrd("Discount_OffInvoice"))
                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                If .DocFile IsNot Nothing Then
                    .DocFile.Mime = SQLHelper.GetIntegerFromDataReader(oDrd("Mime"))
                End If
                .Items = New List(Of DTOPriceListItem_Supplier)
            End With
            Dim oItem As New DTOPriceListItem_Supplier
            With oItem
                .Parent = oPriceList
                .EAN = oDrd("EAN").ToString
                .Ref = sRef
                .Description = oDrd("Description").ToString
                .Price = CDec(oDrd("Price"))
                If Not IsDBNull(oDrd("InnerPack")) Then
                    .InnerPack = CDec(oDrd("InnerPack"))
                End If
                If Not IsDBNull(oDrd("Retail")) Then
                    .Retail = CDec(oDrd("Retail"))
                End If
                If Not IsDBNull(oDrd("SkuGuid")) Then
                    Dim oSkuGuid As Guid = oDrd("SkuGuid")
                    Dim oSku As New DTOProductSku(oSkuGuid)
                    oSku.Id = CInt(oDrd("SkuId"))
                    .Sku = oSku
                End If
            End With
            oPriceList.Items.Add(oItem)
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
