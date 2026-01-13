Public Class WtbolBasketLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOWtbolBasket
        Dim retval As DTOWtbolBasket = Nothing
        Dim oWtbolBasket As New DTOWtbolBasket(oGuid)
        If Load(oWtbolBasket) Then
            retval = oWtbolBasket
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWtbolBasket As DTOWtbolBasket) As Boolean
        If Not oWtbolBasket.IsLoaded And Not oWtbolBasket.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT WtbolBasket.Site, WtbolBasket.Fch ")
            sb.AppendLine(", WtbolSite.MerchantId, WtbolSite.Web ")
            sb.AppendLine(", VwSkuNom.* ")
            sb.AppendLine(", WtbolBasketItem.Qty, WtbolBasketItem.Price ")
            sb.AppendLine("FROM WtbolBasket ")
            sb.AppendLine("INNER JOIN WtbolSite ON WtbolBasket.Site = WtbolSite.Guid ")
            sb.AppendLine("LEFT OUTER JOIN WtbolBasketItem ON WtbolBasket.Guid = WtbolBasketItem.Basket ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON WtbolBasketItem.Sku = VwSkuNom.SkuGuid ")
            sb.AppendLine("WHERE WtbolBasket.Guid='" & oWtbolBasket.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

            Do While oDrd.Read
                With oWtbolBasket
                    If Not .IsLoaded Then
                        .Site = New DTOWtbolSite(oDrd("Site"))
                        .Site.MerchantId = SQLHelper.GetStringFromDataReader(oDrd("MerchantId"))
                        .Site.Web = SQLHelper.GetStringFromDataReader(oDrd("Web"))
                        .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                        .Items = New List(Of DTOWtbolBasket.Item)
                        .IsLoaded = True
                    End If
                End With

                If Not IsDBNull(oDrd("SkuGuid")) Then
                    Dim item As New DTOWtbolBasket.Item
                    With item
                        .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                        .Qty = oDrd("Qty")
                        .Price = oDrd("Price")
                    End With
                    oWtbolBasket.Items.Add(item)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oWtbolBasket.IsLoaded
        Return retval
    End Function

    Shared Function Update(oWtbolBasket As DTOWtbolBasket, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWtbolBasket, oTrans)
            oTrans.Commit()
            oWtbolBasket.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oWtbolBasket As DTOWtbolBasket, ByRef oTrans As SqlTransaction)
        UpdateHeader(oWtbolBasket, oTrans)
        UpdateItems(oWtbolBasket, oTrans)
    End Sub

    Shared Sub UpdateHeader(oWtbolBasket As DTOWtbolBasket, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolBasket ")
        sb.AppendLine("WHERE Guid='" & oWtbolBasket.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWtbolBasket.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWtbolBasket
            oRow("Site") = .Site.Guid
            oRow("Fch") = .Fch
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oWtbolBasket As DTOWtbolBasket, ByRef oTrans As SqlTransaction)
        If Not oWtbolBasket.IsNew Then DeleteItems(oWtbolBasket, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolBasketItem ")
        sb.AppendLine("WHERE Basket='" & oWtbolBasket.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item In oWtbolBasket.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Basket") = oWtbolBasket.Guid
            With item
                oRow("Lin") = oTb.Rows.Count
                oRow("Sku") = SQLHelper.NullableBaseGuid(.Sku)
                oRow("Qty") = .Qty
                oRow("Price") = .Price
            End With
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oWtbolBasket As DTOWtbolBasket, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWtbolBasket, oTrans)
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


    Shared Sub Delete(oWtbolBasket As DTOWtbolBasket, ByRef oTrans As SqlTransaction)
        DeleteItems(oWtbolBasket, oTrans)
        DeleteHeader(oWtbolBasket, oTrans)
    End Sub

    Shared Sub DeleteHeader(oWtbolBasket As DTOWtbolBasket, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WtbolBasket WHERE Guid='" & oWtbolBasket.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oWtbolBasket As DTOWtbolBasket, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WtbolBasketItem WHERE Basket='" & oWtbolBasket.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


#End Region

End Class

Public Class WtbolBasketsLoader

    Shared Function All(Optional Site As DTOWtbolSite = Nothing) As List(Of DTOWtbolBasket)
        Dim retval As New List(Of DTOWtbolBasket)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WtbolBasket.Guid, WtbolBasket.Site, WtbolBasket.Fch ")
        sb.AppendLine(", WtbolSite.MerchantId, WtbolSite.Web ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine(", WtbolBasketItem.Qty, WtbolBasketItem.Price ")
        sb.AppendLine("FROM WtbolBasket ")
        sb.AppendLine("INNER JOIN WtbolSite ON WtbolBasket.Site = WtbolSite.Guid ")
        sb.AppendLine("LEFT OUTER JOIN WtbolBasketItem ON WtbolBasket.Guid = WtbolBasketItem.Basket ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON WtbolBasketItem.Sku = VwSkuNom.SkuGuid ")

        If Site IsNot Nothing Then
            sb.AppendLine("WHERE WtbolBasket.Site = '" & Site.Guid.ToString & "' ")
        End If

        sb.AppendLine("ORDER BY WtbolBasket.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oWtbolBasket As New DTOWtbolBasket
        Do While oDrd.Read
            If Not oWtbolBasket.Guid.Equals(oDrd("Guid")) Then
                oWtbolBasket = New DTOWtbolBasket(oDrd("Guid"))
                With oWtbolBasket
                    If Not .IsLoaded Then
                        .Site = New DTOWtbolSite(oDrd("Site"))
                        .Site.MerchantId = SQLHelper.GetStringFromDataReader(oDrd("MerchantId"))
                        .Site.Web = SQLHelper.GetStringFromDataReader(oDrd("Web"))
                        .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                        .Items = New List(Of DTOWtbolBasket.Item)
                        .IsLoaded = True
                    End If
                End With
                retval.Add(oWtbolBasket)
            End If

            If Not IsDBNull(oDrd("SkuGuid")) Then
                Dim item As New DTOWtbolBasket.Item
                With item
                    .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                    .Qty = oDrd("Qty")
                    .Price = oDrd("Price")
                End With
                oWtbolBasket.Items.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
