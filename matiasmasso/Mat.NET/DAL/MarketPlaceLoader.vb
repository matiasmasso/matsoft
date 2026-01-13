Public Class MarketPlaceLoader

    Shared Function Find(oGuid As Guid) As DTOMarketPlace
        Dim retval As DTOMarketPlace = Nothing
        Dim oMarketPlace As New DTOMarketPlace(oGuid)
        If Load(oMarketPlace) Then
            retval = oMarketPlace
        End If
        Return retval
    End Function

    Shared Function FindSku(marketplace As Guid, sku As Guid) As DTOMarketplaceSku
        Dim retval As New DTOMarketplaceSku()
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Marketplace.Nom, VwSkuNom.SkuNomLlargEsp, MarketPlaceSku.Id AS CustomId, MarketPlaceSku.Active ")
        sb.AppendLine("FROM MarketPlaceSku ")
        sb.AppendLine("INNER JOIN Marketplace ON MarketPlaceSku.Marketplace = Marketplace.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON MarketPlaceSku.Sku = VwSkuNom.SkuGuid  ")
        sb.AppendLine("WHERE MarketPlace='" & marketplace.ToString & "' ")
        sb.AppendLine("AND Sku='" & marketplace.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With retval
                .MarketPlace = New DTOGuidNom(marketplace, oDrd("Nom"))
                .Sku = New DTOGuidNom(sku, oDrd("SkuNomLlargEsp"))
                .CustomId = SQLHelper.GetStringFromDataReader(oDrd("CustomId"))
                .Enabled = oDrd("Active")
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oMarketPlace As DTOMarketPlace) As Boolean
        If Not oMarketPlace.IsLoaded And Not oMarketPlace.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliGral.RaoSocial, MarketPlace.Web, MarketPlace.Nom, MarketPlace.Commission ")
            sb.AppendLine("FROM MarketPlace ")
            sb.AppendLine("INNER JOIN CliGral ON MarketPlace.Guid = CliGral.Guid ")
            sb.AppendLine("WHERE MarketPlace.Guid='" & oMarketPlace.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oMarketPlace
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    .Web = SQLHelper.GetStringFromDataReader(oDrd("Web"))
                    .Commission = SQLHelper.GetDecimalFromDataReader(oDrd("Commission"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oMarketPlace.IsLoaded
        Return retval
    End Function

    Shared Function Update(oMarketPlace As DTOMarketPlace, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oMarketPlace, oTrans)
            oTrans.Commit()
            oMarketPlace.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oMarketPlace As DTOMarketPlace, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM MarketPlace ")
        sb.AppendLine("WHERE Guid='" & oMarketPlace.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oMarketPlace.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oMarketPlace
            oRow("Nom") = SQLHelper.NullableString(.Nom)
            oRow("Web") = SQLHelper.NullableString(.Web)
            oRow("Commission") = SQLHelper.NullableDecimal(.Commission)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function UpdateSku(oMarketPlaceSku As DTOMarketplaceSku, exs As List(Of Exception)) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM MarketPlaceSku ")
        sb.AppendLine("WHERE Sku='" & oMarketPlaceSku.Sku.Guid.ToString & "' ")
        sb.AppendLine("AND MarketPlace='" & oMarketPlaceSku.MarketPlace.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oConn As SqlConnection = SQLHelper.SQLConnection()
        Try
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oConn)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow
            If oTb.Rows.Count = 0 Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Sku") = oMarketPlaceSku.Sku.Guid
                oRow("Marketplace") = oMarketPlaceSku.MarketPlace.Guid
            Else
                oRow = oTb.Rows(0)
            End If

            With oMarketPlaceSku
                oRow("Id") = SQLHelper.NullableString(.CustomId)
                oRow("Active") = .Enabled
            End With

            oDA.Update(oDs)
        Catch ex As Exception
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try
        Return exs.Count = 0
    End Function

    Shared Function Delete(oMarketPlace As DTOMarketPlace, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oMarketPlace, oTrans)
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


    Shared Sub Delete(oMarketPlace As DTOMarketPlace, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE MarketPlace WHERE Guid='" & oMarketPlace.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Public Shared Function Catalog(oMarketplace As DTOMarketPlace) As List(Of DTOMarketplaceSku)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwCustomerSkus.SkuGuid, VwCustomerSkus.SkuNomLlargEsp ")
        sb.AppendLine(", MarketplaceSku.Id AS CustomId, MarketplaceSku.Active ")
        sb.AppendLine(", (CASE WHEN VwProductText.ContentEsp IS NULL THEN 0 ELSE 1 END) AS HasTxt ")
        sb.AppendLine(", VwCustomerSkus.SkuImageExists AS HasImg ")
        sb.AppendLine("FROM VwCustomerSkus ")
        sb.AppendLine("LEFT OUTER JOIN VwProductText ON VwCustomerSkus.SkuGuid = VwProductText.Guid ")
        sb.AppendLine("LEFT OUTER JOIN MarketplaceSku ON VwCustomerSkus.Customer = MarketplaceSku.Marketplace And MarketplaceSku.Sku = VwCustomerSkus.SkuGuid ")
        sb.AppendLine("WHERE VwCustomerSkus.Customer='" & oMarketplace.Guid.ToString & "' ")
        sb.AppendLine("AND VwCustomerSkus.CodExclusio IS NULL ")
        sb.AppendLine("ORDER BY VwCustomerSkus.BrandNom, VwCustomerSkus.CategoryNomEsp, VwCustomerSkus.SkuNomLlargEsp ")

        Dim SQL = sb.ToString()
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim retval As New List(Of DTOMarketplaceSku)
        Do While oDrd.Read
            Dim item As New DTOMarketplaceSku
            item.Sku = New DTOGuidNom(oDrd("SkuGuid"), oDrd("SkuNomLlargEsp"))
            item.MarketPlace = New DTOGuidNom(oMarketplace.Guid, oMarketplace.Nom)
            item.CustomId = SQLHelper.GetStringFromDataReader(oDrd("CustomId"))
            item.Enabled = SQLHelper.GetBooleanFromDatareader(oDrd("Active"))
            item.HasImg = SQLHelper.GetBooleanFromDatareader(oDrd("HasImg"))
            item.HasTxt = oDrd("HasTxt")
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function EnableSkus(oItems As List(Of DTOMarketplaceSku), blEnabled As Boolean, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            EnableSkus(oItems, blEnabled, oTrans)
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

    Shared Sub EnableSkus(oItems As List(Of DTOMarketplaceSku), blEnabled As Boolean, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	     Marketplace uniqueidentifier NOT NULL ")
        sb.AppendLine("	     , Sku uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table (Marketplace, Sku) ")

        Dim idx As Integer = 0
        For Each oItem In oItems
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}','{1}') ", oItem.MarketPlace.Guid.ToString(), oItem.Sku.Guid.ToString())
            idx += 1
        Next
        Dim SQL1 As String = sb.ToString

        sb.AppendLine()
        sb.AppendLine("INSERT INTO MarketplaceSku (Marketplace, Sku) ")
        sb.AppendLine("SELECT X.Marketplace, X.Sku ")
        sb.AppendLine("FROM @Table X ")
        sb.AppendLine("LEFT OUTER JOIN MarketplaceSku ON X.Marketplace = MarketplaceSku.Marketplace AND x.Sku = MarketplaceSku.Sku ")
        sb.AppendLine("WHERE MarketplaceSku.Sku IS NULL ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)

        sb = New Text.StringBuilder(SQL1)
        sb.AppendLine()
        sb.AppendLine("UPDATE MarketplaceSku ")
        sb.AppendLine("SET Active = " & If(blEnabled, "1", "0") & " ")
        sb.AppendLine("FROM @Table X ")
        sb.AppendLine("INNER JOIN MarketplaceSku ON X.Marketplace = MarketplaceSku.Marketplace AND x.Sku = MarketplaceSku.Sku ")
        SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Offers(oMarketPlace As DTOMarketPlace) As List(Of DTOOffer)
        Dim retval As New List(Of DTOOffer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Sku, Price ")
        sb.AppendLine("FROM Offers ")
        sb.AppendLine("WHERE Parent = '" & oMarketPlace.Guid.ToString() & "' ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim offer As New DTOOffer
            offer.Parent = oMarketPlace.Guid
            offer.Sku = oDrd("Sku")
            offer.Price = oDrd("Price")
            retval.Add(offer)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

Public Class MarketPlacesLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOMarketPlace)
        Dim retval As New List(Of DTOMarketPlace)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT MarketPlace.Guid, CliGral.RaoSocial, MarketPlace.Nom ")
        sb.AppendLine("FROM MarketPlace ")
        sb.AppendLine("INNER JOIN CliGral ON MarketPlace.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY CliGral.RaoSocial")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOMarketPlace(oDrd("Guid"))
            With item
                If IsDBNull(oDrd("Nom")) Then
                    .Nom = oDrd("RaoSocial")
                Else
                    .Nom = oDrd("Nom")
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

