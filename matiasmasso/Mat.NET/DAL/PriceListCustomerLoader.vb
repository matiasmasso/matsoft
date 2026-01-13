Public Class PriceListCustomerLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPricelistCustomer
        Dim retval As DTOPricelistCustomer = Nothing
        Dim candidate As New DTOPricelistCustomer(oGuid)
        If Load(candidate) Then
            retval = candidate
        End If
        Return retval
    End Function

    Shared Function Load(ByRef value As DTOPricelistCustomer, Optional ForceReload As Boolean = False) As Boolean
        If (Not value.IsLoaded And Not value.IsNew) Or ForceReload Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT PH.Customer, PH.Fch, PH.FchEnd, PH.Concepte, PH.Currency ")
            sb.AppendLine(", PD.Art, PD.Retail ")
            sb.AppendLine(", VwSkuNom.* ")
            sb.AppendLine(", CliGral.FullNom ")
            sb.AppendLine("FROM  PriceList_Customer PH ")
            sb.AppendLine("LEFT OUTER JOIN PricelistItem_Customer PD ON PH.Guid = PD.PriceList ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON PD.Art=VwSkuNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON PH.Customer=CliGral.Guid ")
            sb.AppendLine("WHERE PH.Guid = '" & value.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.BrandGuid ")
            sb.AppendLine(", VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryGuid ")
            sb.AppendLine(", VwSkuNom.SkuNom, VwSkuNom.SkuGuid ")

            value.Items = New List(Of DTOPricelistItemCustomer)

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not value.IsLoaded Then
                    With value
                        If Not IsDBNull(oDrd("Customer")) Then
                            If Not DirectCast(oDrd("Customer"), Guid).Equals(System.Guid.Empty) Then
                                .Customer = New DTOCustomer(DirectCast(oDrd("Customer"), Guid))
                                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                            End If
                        End If
                        If Not IsDBNull(oDrd("FchEnd")) Then
                            .FchEnd = CDate(oDrd("FchEnd"))
                        End If

                        .Fch = CDate(oDrd("Fch"))
                        .Concepte = oDrd("Concepte").ToString
                        .Cur = DTOCur.Factory(oDrd("Currency"))
                        .Items = New List(Of DTOPricelistItemCustomer)
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("Art")) Then
                    Dim oItem As New DTOPricelistItemCustomer(value)
                    With oItem
                        .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                        .Retail = SQLHelper.GetAmtFromDataReader2(oDrd, "retail")
                    End With
                    value.Items.Add(oItem)
                End If
            Loop
            oDrd.Close()
        End If

        Dim retval As Boolean = value.IsLoaded
        Return retval
    End Function

    Shared Function Update(value As DTOPricelistCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(value, oTrans)
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


    Shared Sub Update(value As DTOPricelistCustomer, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM PriceList_Customer WHERE Guid = @Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@GUID", value.Guid.ToString())
        Dim oDS As New DataSet

        oDA.Fill(oDS)

        Dim oTb As DataTable = oDS.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oRow("Guid") = value.Guid
            oTb.Rows.Add(oRow)
        Else
            oRow = oTb.Rows(0)
        End If

        With value
            If .Customer Is Nothing Then
                oRow("Customer") = System.DBNull.Value
            Else
                If .Customer.Guid.Equals(System.Guid.Empty) Then
                    oRow("Customer") = System.DBNull.Value
                Else
                    oRow("Customer") = .Customer.Guid
                End If
            End If

            If .FchEnd = Nothing Then
                oRow("FchEnd") = System.DBNull.Value
            Else
                oRow("FchEnd") = .FchEnd
            End If

            oRow("Fch") = .Fch
            oRow("Concepte") = .Concepte
            oRow("Currency") = .Cur.Tag

        End With

        oDA.Update(oDS)

        DeleteItems(value, oTrans)
        UpdateItems(value, oTrans)
    End Sub

    Shared Function Delete(value As DTOPricelistCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(value, oTrans)
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


    Shared Sub Delete(value As DTOPricelistCustomer, ByRef oTrans As SqlTransaction)
        DeleteItems(value, oTrans)
        deleteheader(value, oTrans)
    End Sub

    Shared Sub DeleteHeader(value As DTOPricelistCustomer, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PriceList_Customer WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", value.Guid.ToString())
    End Sub

    Shared Sub DeleteItems(value As DTOPricelistCustomer, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PricelistItem_Customer WHERE PriceList = @Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@GUID", value.Guid.ToString())
    End Sub

    Shared Sub UpdateItems(value As DTOPricelistCustomer, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM PricelistItem_Customer WHERE PriceList = @Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@GUID", value.Guid.ToString())
        Dim oDS As New DataSet
        oDA.Fill(oDS)
        Dim oTb As DataTable = oDS.Tables(0)

        For Each oItem As DTOPricelistItemCustomer In value.Items
            Dim oRow As DataRow = oTb.NewRow
            oRow("PriceList") = value.Guid

            With oItem
                oRow("Art") = .Sku.Guid

                If oItem.Retail Is Nothing Then
                    oRow("Retail") = System.DBNull.Value
                Else
                    oRow("Retail") = .Retail.Val
                End If

            End With
            oTb.Rows.Add(oRow)
        Next

        oDA.Update(oDS)
    End Sub

#End Region

End Class

Public Class PriceListsCustomerLoader

    Shared Function All(Optional oCustomer As DTOCustomer = Nothing) As List(Of DTOPricelistCustomer)
        Dim retval As New List(Of DTOPricelistCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pricelist_Customer.Guid,Pricelist_Customer.Fch,Pricelist_Customer.FchEnd ")
        sb.AppendLine(", Pricelist_Customer.Currency,Pricelist_Customer.Concepte,Pricelist_Customer.Customer ")
        sb.AppendLine(", CliGral.FullNom ")
        sb.AppendLine("FROM PriceList_Customer ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Pricelist_Customer.Customer=CliGral.Guid ")
        If oCustomer IsNot Nothing Then
            sb.AppendLine("INNER JOIN VwCcxOrMe ON PriceList_Customer.Customer = VwCcxOrMe.Ccx AND VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "' ")
        End If

        sb.AppendLine("ORDER BY Pricelist_Customer.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPricelistCustomer(oDrd("Guid"))
            With item
                .Fch = oDrd("Fch")
                If Not IsDBNull(oDrd("FchEnd")) Then
                    .FchEnd = oDrd("FchEnd")
                End If
                .Concepte = oDrd("Concepte")
                .Cur = DTOCur.Factory(oDrd("Currency"))
                If Not IsDBNull(oDrd("Customer")) Then
                    .Customer = New DTOCustomer(oDrd("Customer"))
                    .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Customers(oSku As DTOProductSku) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pricelist_Customer.Guid, CliGral.FullNom ")
        sb.AppendLine("FROM PriceList_Customer ")
        sb.AppendLine("INNER JOIN PriceListItem_Customer ON PriceList_Customer.Guid = PriceListItem_Customer.PriceList AND PriceListItem_Customer.Art='" & oSku.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Pricelist_Customer.Customer=CliGral.Guid ")
        sb.AppendLine("ORDER BY CliGral.FullNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOContact = Nothing
            If IsDBNull(oDrd("Guid")) Then
                item = New DTOContact(Guid.Empty)
                item.FullNom = "(tots els clients)"
            Else
                item = New DTOContact(oDrd("Guid"))
                item.FullNom = oDrd("FullNom")
            End If
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class PriceListItemCustomerLoader


    Shared Function GetCustomerCost(oCustomer As DTOCustomer, oSku As DTOProductSku, DtFch As Date) As DTOAmt
        Dim retval As DTOAmt = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PriceListItem_Customer.Retail ")
        sb.AppendLine("FROM       PriceList_Customer ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON PriceList_Customer.Customer=VwCcxOrMe.Ccx AND VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("INNER JOIN PriceListItem_Customer ON PriceList_Customer.Guid = PriceListItem_Customer.PriceList ")
        sb.AppendLine("WHERE PriceListItem_Customer.Art = '" & oSku.Guid.ToString & "' ")
        sb.AppendLine("AND PriceList_Customer.Fch <= '" & Format(DtFch, "yyyyMMdd") & "' ")
        sb.AppendLine("AND (PriceList_Customer.FchEnd IS NULL OR  PriceList_Customer.FchEnd >= '" & Format(DtFch, "yyyyMMdd") & "') ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = DTOAmt.Factory(CDec(oDrd("Retail")))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Search(oSku As DTOProductSku, Optional DtFch As Date = Nothing, Optional oCustomer As DTOContact = Nothing) As DTOPricelistItemCustomer
        Dim retval As DTOPricelistItemCustomer = Nothing
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT P1.Guid, P1.Fch, P1.Concepte, P2.Retail ")
        sb.AppendLine("FROM       PriceList_Customer AS P1 ")
        sb.AppendLine("INNER JOIN PriceListItem_Customer AS P2 ON P2.PriceList = P1.Guid ")
        sb.AppendLine("INNER JOIN (SELECT MAX(dbo.PriceList_Customer.Fch) AS FCH, dbo.PriceListItem_Customer.Art ")
        sb.AppendLine("            FROM       PriceList_Customer ")
        sb.AppendLine("            INNER JOIN PriceListItem_Customer ON dbo.PriceList_Customer.Guid = dbo.PriceListItem_Customer.PriceList ")
        sb.AppendLine("            WHERE PriceList_Customer.Fch <= '" & Format(DtFch, "yyyyMMdd") & "' ")
        If oCustomer Is Nothing Then
            sb.AppendLine("           AND PriceList_Customer.Customer IS NULL ")
        Else
            sb.AppendLine("           AND PriceList_Customer.Customer ='" & oCustomer.Guid.ToString & "' ")
        End If
        sb.AppendLine("            GROUP BY PriceListItem_Customer.Art) AS X ON P1.Fch = X.FCH AND P2.Art = X.Art ")
        sb.AppendLine("WHERE P2.Art = '" & oSku.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oPriceList As New DTOPricelistCustomer(oDrd("Guid"))
            With oPriceList
                .Fch = oDrd("Fch")
                .Concepte = oDrd("Concepte")
            End With
            retval = New DTOPricelistItemCustomer(oPriceList)
            With retval
                .Sku = oSku
                .Retail = DTOAmt.Factory(CDec(oDrd("Retail")))
            End With
        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function Find(oPriceList As DTOPricelistCustomer, oSku As DTOProductSku) As DTOPricelistItemCustomer
        Dim retval As DTOPricelistItemCustomer = Nothing
        Dim SQL As String = "SELECT * FROM PriceListItem_Customer WHERE PriceList=@PriceList and Art=@Art"
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL, "@PriceList", oPriceList.Guid.ToString, "@Art", oSku.Guid.ToString())
        If oDrd.Read Then
            Dim oParent As New DTOPricelistCustomer(DirectCast(oDrd("PriceList"), Guid))
            retval = New DTOPricelistItemCustomer(oParent)
            With retval
                .Parent = oPriceList
                .Sku = oSku
                If Not IsDBNull(oDrd("Retail")) Then
                    .Retail = DTOAmt.Factory(CDec(oDrd("Retail")))
                End If
            End With
        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(value As DTOPricelistItemCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(value, oTrans)
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

    Shared Sub Update(value As DTOPricelistItemCustomer, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM PriceListItem_Customer WHERE PriceList=@PriceList and Art=@Sku"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@PriceList", value.Parent.Guid.ToString, "@Sku", value.Sku.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("PriceList") = value.Parent.Guid
            oRow("Art") = value.Sku.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
        Else
            oRow = oTb.Rows(0)
        End If

        With value
            If .Retail Is Nothing Then
                oRow("Retail") = System.DBNull.Value
            Else
                oRow("Retail") = .Retail.Val
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(value As DTOPricelistItemCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(value, oTrans)
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


    Shared Function Delete(value As DTOPricelistItemCustomer, ByRef oTrans As SqlTransaction) As Boolean
        Dim SQL As String = "DELETE PriceListItem_Customer WHERE PriceList='" & value.Parent.Guid.ToString & "' and Art='" & value.Sku.Guid.ToString & "' "
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Dim retval As Boolean = (i > 0)
        Return retval
    End Function


    Shared Function GetPreuDeVenda(oSku As DTOProductSku, Optional oCustomer As DTOCustomer = Nothing, Optional DtFch As Date = Nothing) As DTOPricelistItemCustomer 'TO DEPRECATE
        Dim retval As DTOPricelistItemCustomer = Nothing
        Dim oClientGuid As Guid = Nothing
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT TOP 1 P1.Guid, P1.Currency,P2.Retail ")
        sb.AppendLine("FROM PriceList_Customer P1 ")
        sb.AppendLine("INNER JOIN PricelistItem_Customer P2 ON P2.PriceList=P1.Guid ")
        sb.AppendLine("WHERE P2.ART='" & oSku.Guid.ToString & "' ")
        sb.AppendLine("AND P1.FCH<='" & Format(DtFch, "yyyyMMdd") & "' ")
        sb.AppendLine("AND (P1.FchEnd IS NULL OR P1.FchEnd>='" & Format(DtFch, "yyyyMMdd") & "') ")
        If oCustomer Is Nothing Then
            sb.AppendLine("AND P1.Customer IS NULL ")
        Else
            sb.AppendLine("AND P1.Customer='" & oClientGuid.ToString & "' ")
            oClientGuid = oCustomer.Guid
        End If
        sb.AppendLine("ORDER BY P1.FCH DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oParent As New DTOPricelistCustomer(DirectCast(oDrd("Guid"), Guid))

            retval = New DTOPricelistItemCustomer(oParent)
            Dim oCur = DTOCur.Factory(oDrd("Currency"))
            With retval
                If Not IsDBNull(oDrd("Retail")) Then
                    .Retail = DTOAmt.Factory(oCur, CDec(oDrd("Retail")))
                End If
            End With
        Else
            oDrd.Close()
            'prova la primera data de tarifa, encara que sigui futura
            sb = New System.Text.StringBuilder
            sb.AppendLine("SELECT TOP 1 P1.Guid, P1.Currency,P2.Retail ")
            sb.AppendLine("FROM PriceList_Customer P1 ")
            sb.AppendLine("INNER JOIN PricelistItem_Customer P2 ON P2.PriceList=P1.Guid ")
            sb.AppendLine("WHERE P2.ART=@Art ")
            'sb.AppendLine("AND P1.FCH<=@Fch ")
            sb.AppendLine("AND (P1.FchEnd IS NULL OR P1.FchEnd>=@Fch) ")
            If oCustomer Is Nothing Then
                sb.AppendLine("AND P1.Customer IS NULL ")
            Else
                sb.AppendLine("AND P1.Customer='" & oClientGuid.ToString & "' ")
                oClientGuid = oCustomer.Guid
            End If
            sb.AppendLine("ORDER BY P1.FCH DESC")

            SQL = sb.ToString
            oDrd = SQLHelper.GetDataReader(SQL, "@Art", oSku.Guid.ToString, "@Fch", Format(DtFch, "yyyyMMdd"))
            If oDrd.Read Then
                Dim oParent As New DTOPricelistCustomer(DirectCast(oDrd("Guid"), Guid))

                retval = New DTOPricelistItemCustomer(oParent)
                Dim oCur As DTOCur = DTOCur.Factory(oDrd("Currency"))
                With retval
                    If Not IsDBNull(oDrd("Retail")) Then
                        .Retail = DTOAmt.Factory(oCur, CDec(oDrd("Retail")))
                    End If
                End With
            End If

        End If
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class PriceListItemsCustomerLoader

    Shared Function All(oProductSku As DTOProductSku, Optional oCustomer As DTOCustomer = Nothing) As List(Of DTOPricelistItemCustomer)
        Dim retval As New List(Of DTOPricelistItemCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT P1.Guid, P1.Currency, P1.Concepte, P1.Fch, P1.FchEnd, P1.Customer, P2.Retail ")
        sb.AppendLine(", CliGral.FullNom ")
        sb.AppendLine("FROM PriceList_Customer P1 ")
        sb.AppendLine("INNER JOIN PricelistItem_Customer P2 ON P2.PriceList = P1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON P1.Customer = CliGral.Guid ")
        sb.AppendLine("WHERE P2.ART = '" & oProductSku.Guid.ToString & "' ")
        If oCustomer Is Nothing Then
            sb.AppendLine("AND P1.Customer IS NULL ")
        Else
            sb.AppendLine("AND P1.Customer ='" & oCustomer.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY P1.FCH DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Sku", oProductSku.Guid.ToString())
        Do While oDrd.Read
            Dim oPriceList As New DTOPricelistCustomer(oDrd("Guid"))
            With oPriceList
                .Fch = oDrd("Fch")
                .Concepte = oDrd("Concepte")
                If Not IsDBNull(oDrd("FchEnd")) Then
                    .FchEnd = oDrd("FchEnd")
                End If
                .Cur = DTOCur.Factory(oDrd("Currency"))
                If Not IsDBNull(oDrd("Customer")) Then
                    .Customer = New DTOCustomer(oDrd("Customer"))
                    .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                End If
            End With

            Dim oItem As New DTOPricelistItemCustomer(oPriceList)
            With oItem
                .Sku = oProductSku
                If Not IsDBNull(oDrd("Retail")) Then
                    .Retail = DTOAmt.Factory(CDec(oDrd("Retail")))
                End If

            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval

    End Function


    Shared Function Old_Vigent() As List(Of DTOPricelistItemCustomer)
        Dim retval As New List(Of DTOPricelistItemCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwLastRetailPrice.PriceList, VwLastRetailPrice.Fch, VwLastRetailPrice.Retail ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM VwLastRetailPrice ")
        sb.AppendLine("INNER JOIN VwSkuNom ON VwLastRetailPrice.Art = VwSkuNom.SkuGuid  ")
        sb.AppendLine("WHERE VwSkuNom.Obsoleto = 0 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oPriceList As New DTOPricelistCustomer
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oPriceList.Guid.Equals(oDrd("PriceList")) Then
                oPriceList = New DTOPricelistCustomer(oDrd("PriceList"))
                oPriceList.Fch = oDrd("Fch")
            End If

            Dim oItem As New DTOPricelistItemCustomer(oPriceList)
            With oItem
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                .Retail = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Vigent(Optional DtFch As Date = Nothing) As List(Of DTOPricelistItemCustomer)
        Dim retval As New List(Of DTOPricelistItemCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PriceListItem_Customer.PriceList, PriceList_Customer.Fch, PriceListItem_Customer.Retail ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM PriceList_Customer ")
        sb.AppendLine("INNER JOIN PriceListItem_Customer ON PriceList_Customer.Guid = PriceListItem_Customer.PriceList AND PriceList_Customer.Customer IS NULL  ")
        sb.AppendLine("INNER JOIN (")
        sb.AppendLine("     SELECT Art, MAX(Fch) AS LastFch FROM VwRetailPrice")
        If DtFch = Nothing Then
            sb.AppendLine(" WHERE Fch <= GETDATE() ")
        Else
            sb.AppendLine(" WHERE Fch <= '" & Format(DtFch, "yyyyMMdd") & "' ")
        End If
        sb.AppendLine("     GROUP BY Art) AS X ON PriceListItem_Customer.Art = X.Art AND PriceList_Customer.Fch = X.LastFch ")
        sb.AppendLine("INNER JOIN VwSkuNom ON PriceListItem_Customer.Art = VwSkuNom.SkuGuid  ")
        sb.AppendLine("WHERE VwSkuNom.Obsoleto = 0 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oPriceList As New DTOPricelistCustomer
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oPriceList.Guid.Equals(oDrd("PriceList")) Then
                oPriceList = New DTOPricelistCustomer(oDrd("PriceList"))
                oPriceList.Fch = oDrd("Fch")
            End If

            Dim oItem As New DTOPricelistItemCustomer(oPriceList)
            With oItem
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                .brandNom = .sku.category.brand.nom.Esp
                .categoryNom = .sku.category.nom.Esp
                .productNom = .sku.nom.Esp
                .Retail = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Active(oCustomer As DTOCustomer, Optional DtFch As Date = Nothing) As List(Of DTOPricelistItemCustomer)
        Dim retval As New List(Of DTOPricelistItemCustomer)
        Dim sb As New System.Text.StringBuilder

        sb = New System.Text.StringBuilder
        sb.AppendLine("SELECT P3.Guid, P3.Currency, P4.Art, P4.Retail ")
        sb.AppendLine("FROM PriceList_Customer P3 ")
        sb.AppendLine("INNER JOIN PricelistItem_Customer P4 ON P4.PriceList = P3.Guid ")
        sb.AppendLine("INNER JOIN ")
        sb.AppendLine("(SELECT P1.Customer, P2.Art, MAX(P1.Fch) AS LastFch ")
        sb.AppendLine("     FROM PriceList_Customer P1 ")
        sb.AppendLine("     INNER JOIN PricelistItem_Customer P2 ON P2.PriceList = P1.Guid  ")
        sb.AppendLine("     INNER JOIN VwCcxOrMe ON P1.Customer = VwCcxOrMe.Ccx ")
        sb.AppendLine("     WHERE VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("     AND (P1.Fch IS NULL OR P1.Fch <='" & Format(DtFch, "yyyyMMdd") & "') ")
        sb.AppendLine("     AND (P1.FchEnd IS NULL OR P1.FchEnd >='" & Format(DtFch, "yyyyMMdd") & "') ")
        sb.AppendLine("     GROUP BY P1.Customer, P2.Art) X ON P3.Customer=X.Customer AND P4.Art = X.Art AND  P3.Fch = X.LastFch ")
        sb.AppendLine("ORDER BY P3.Guid, P4.Art ")

        Dim SQL As String = sb.ToString
        Dim oPriceList As New DTOPricelistCustomer
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oPriceList.Guid.Equals(oDrd("Guid")) Then
                oPriceList = New DTOPricelistCustomer(oDrd("Guid"))
                oPriceList.Cur = DTOCur.Factory(oDrd("Currency"))
            End If

            Dim oItem As New DTOPricelistItemCustomer(oPriceList)
            With oItem
                .Sku = New DTOProductSku(oDrd("Art"))
                .Retail = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval

    End Function

End Class
