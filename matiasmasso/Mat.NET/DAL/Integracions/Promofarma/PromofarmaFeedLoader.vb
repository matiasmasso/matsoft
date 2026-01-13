Public Class PromofarmaFeedLoader

    Public Shared Function Feed(oMgz As DTOMgz) As DTO.Integracions.Promofarma.Feed
        Dim retval As New DTO.Integracions.Promofarma.Feed
        Dim IVA = DTOTax.closestTipus(DTOTax.Codis.iva_Standard)
        Dim oPromofarma = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.promofarma)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwCustomerSkus.SkuGuid, VwCustomerSkus.EAN13, VwCustomerSkus.BrandNom, VwCustomerSkus.CategoryNomEsp, VwCustomerSkus.SkuNomLlargEsp ")
        sb.AppendLine(", VwCustomerSkus.SkuImageExists, VwCustomerSkus.SkuKg, VwCustomerSkus.CategoryKg, VwCustomerSkus.HeredaDimensions ")
        sb.AppendLine(", VwSkuRetail.Retail ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", LangText.Text ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsBlockStock, VwSkuPncs.ClientsEnProgramacio ")
        sb.AppendLine(", PromofarmaFeed.Id, PromofarmaFeed.[Enabled] ")
        sb.AppendLine("FROM VwCustomerSkus ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuRetail ON VwCustomerSkus.SkuGuid = VwSkuRetail.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuStocks ON VwCustomerSkus.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuPncs ON VwCustomerSkus.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN LangText ON VwCustomerSkus.SkuGuid = LangText.Guid AND LangText.Lang='ESP' AND LangText.Src=" & DTOLangText.Srcs.ProductExcerpt & " ")
        sb.AppendLine("LEFT OUTER JOIN PromofarmaFeed ON VwCustomerSkus.SkuGuid = PromofarmaFeed.Sku AND VwCustomerSkus.Customer = PromofarmaFeed.Customer ")
        sb.AppendLine("WHERE VwCustomerSkus.Customer='" & oPromofarma.Guid.ToString & "' ")
        sb.AppendLine("AND VwCustomerSkus.CodExclusio IS NULL ")
        sb.AppendLine("ORDER BY VwCustomerSkus.BrandNom, VwCustomerSkus.CategoryNomEsp, VwCustomerSkus.SkuNomLlargEsp ")
        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTO.Integracions.Promofarma.Feed.Item(oDrd("SkuGuid"))
            Dim oSku As New DTOProductSku(oDrd("SkuGuid"))
            With oSku
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
            End With

            With item
                .EAN13 = SQLHelper.GetStringFromDataReader(oDrd("EAN13"))
                .Marca = SQLHelper.GetStringFromDataReader(oDrd("BrandNom"))
                .Category = SQLHelper.GetStringFromDataReader(oDrd("CategoryNomEsp"))
                .NombreProducto = SQLHelper.GetStringFromDataReader(oDrd("SkuNomLlargEsp"))
                .Pvp = SQLHelper.GetDecimalFromDataReader(oDrd("Retail"))
                .IVAPct = IVA
                .Stock = oSku.stockAvailable()
                .IdPromofarma = SQLHelper.GetStringFromDataReader(oDrd("Id"))
                If SQLHelper.GetBooleanFromDatareader(oDrd("SkuImageExists")) Then
                    'Promofarma requests all image urls to display proper mime extension
                    .UrlImagen = MmoUrl.image(DTO.Defaults.ImgTypes.art, item.Guid, True) & ".jpg"
                End If
                If Not IsDBNull(oDrd("Enabled")) Then
                    'default is true if
                    .Enabled = oDrd("Enabled")
                End If
                If SQLHelper.GetBooleanFromDatareader(oDrd("HeredaDimensions")) Then
                    .Peso = SQLHelper.GetDecimalFromDataReader(oDrd("CategoryKg"))
                Else
                    .Peso = SQLHelper.GetDecimalFromDataReader(oDrd("SkuKg"))
                End If
                .Descripion = SQLHelper.GetStringFromDataReader(oDrd("Text"))
                .IsLoaded = True
            End With
            retval.Items.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function



    Shared Function Find(oGuid As Guid) As DTO.Integracions.Promofarma.Feed.Item
        Dim retval As DTO.Integracions.Promofarma.Feed.Item = Nothing
        Dim oItem As New DTO.Integracions.Promofarma.Feed.Item(oGuid)
        If Load(oItem) Then
            retval = oItem
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oItem As DTO.Integracions.Promofarma.Feed.Item) As Boolean
        If Not oItem.IsLoaded And Not oItem.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM PromofarmaFeed ")
            sb.AppendLine("WHERE Sku='" & oItem.Guid.ToString & "' ")
            sb.AppendLine("AND Customer='" & DTOCustomer.Wellknown(DTOCustomer.Wellknowns.promofarma).Guid.ToString() & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oItem
                    .IdPromofarma = SQLHelper.GetStringFromDataReader(oDrd("Id"))
                    .Enabled = oDrd("Enabled")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
            End If

            Dim retval As Boolean = oItem.IsLoaded
            Return retval
    End Function

    Shared Function Update(oItem As DTO.Integracions.Promofarma.Feed.Item, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oItem, oTrans)
            oTrans.Commit()
            oItem.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oItem As DTO.Integracions.Promofarma.Feed.Item, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PromofarmaFeed ")
        sb.AppendLine("WHERE Sku='" & oItem.Guid.ToString & "' ")
        sb.AppendLine("AND Customer='" & DTOCustomer.Wellknown(DTOCustomer.Wellknowns.promofarma).Guid.ToString() & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Sku") = oItem.Guid
            oRow("Marketplace") = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.promofarma).Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oItem
            oRow("Id") = SQLHelper.NullableString(.IdPromofarma)
            oRow("Enabled") = .Enabled
        End With

        oDA.Update(oDs)
    End Sub


End Class

Public Class PromofarmaFeedsLoader
    Shared Function Enable(oItems As List(Of DTO.Integracions.Promofarma.Feed.Item), blEnabled As Boolean, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Enable(oItems, blEnabled, oTrans)
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

    Shared Sub Enable(oItems As List(Of DTO.Integracions.Promofarma.Feed.Item), blEnabled As Boolean, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	     Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table (Guid) ")

        Dim idx As Integer = 0
        For Each oItem In oItems
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oItem.Guid.ToString())
            idx += 1
        Next
        Dim SQL1 As String = sb.ToString

        sb.AppendLine()
        sb.AppendLine("INSERT INTO PromofarmaFeed (Sku, Customer) ")
        sb.AppendLine("SELECT X.Guid, '" & DTOCustomer.Wellknown(DTOCustomer.Wellknowns.promofarma).Guid.ToString() & "' ")
        sb.AppendLine("FROM @Table X ")
        sb.AppendLine("LEFT OUTER JOIN PromofarmaFeed ON X.Guid = PromofarmaFeed.Sku AND PromofarmaFeed.Customer = '" & DTOCustomer.Wellknown(DTOCustomer.Wellknowns.promofarma).Guid.ToString() & "' ")
        sb.AppendLine("WHERE PromofarmaFeed.Sku IS NULL ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)

        sb = New Text.StringBuilder(SQL1)
        sb.AppendLine()
        sb.AppendLine("UPDATE PromofarmaFeed ")
        sb.AppendLine("SET Enabled = " & If(blEnabled, "1", "0") & " ")
        sb.AppendLine("FROM @Table X ")
        sb.AppendLine("INNER JOIN PromofarmaFeed ON X.Guid = PromofarmaFeed.Sku AND PromofarmaFeed.Customer = '" & DTOCustomer.Wellknown(DTOCustomer.Wellknowns.promofarma).Guid.ToString() & "' ")
        SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


End Class


