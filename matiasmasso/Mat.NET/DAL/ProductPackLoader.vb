Public Class ProductPackLoader

    Shared Sub Load(oSku As DTOProductSku, oMgz As DTOMgz)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ProductPack.Sku, ProductPack.Qty, Art.Myd, Stk.Stock, Pnx.Pn2, Preus.Retail ")
        sb.AppendLine(", Art.Category, Stp.Brand ")
        sb.AppendLine("FROM ProductPack ")
        sb.AppendLine("INNER JOIN Art ON ProductPack.Sku = Art.Guid ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category = Stp.Guid ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand = Tpa.Guid ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT MgzGuid, ArtGuid, SUM(CASE WHEN Cod < 50 THEN Qty ELSE - Qty END) AS Stock FROM Arc GROUP BY MgzGuid, ArtGuid) Stk ON Stk.MgzGuid ='" & oMgz.Guid.ToString & "' AND Stk.ArtGuid = Art.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT ArtGuid, SUM(Pn2) AS Pn2 FROM PNC WHERE Cod = 2 AND pn3 = 0 GROUP BY ArtGuid HAVING SUM(Pn2) <> 0) Pnx ON Pnx.ArtGuid = Art.Guid ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT P2.Art, P2.Retail ")
        sb.AppendLine("                 FROM PriceListItem_Customer AS P2 ")
        sb.AppendLine("                 INNER JOIN PriceList_Customer AS P1 ON P2.PriceList = P1.Guid ")
        sb.AppendLine("                 INNER JOIN (SELECT MAX(dbo.PriceList_Customer.Fch) AS FCH, dbo.PriceListItem_Customer.Art FROM PriceList_Customer INNER JOIN PriceListItem_Customer ON dbo.PriceList_Customer.Guid = dbo.PriceListItem_Customer.PriceList AND PriceList_Customer.Fch <= GETDATE() AND PriceList_Customer.Customer IS NULL ")
        sb.AppendLine("                 GROUP BY PriceListItem_Customer.Art) AS P3 ON P1.Fch = P3.FCH And P3.Art = P2.Art) Preus ON Preus.Art=Art.Guid ")


        sb.AppendLine("WHERE ProductPack.Parent ='" & oSku.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ProductPack.Lin")

        Dim SQL As String = sb.ToString
        oSku.PackItems = New List(Of DTOProductPackItem)
        oSku.RRPP = DTOAmt.Empty

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBrand As New DTOProductBrand(oDrd("Brand"))
            Dim oCategory As New DTOProductCategory(oDrd("Category"))
            oCategory.Brand = oBrand

            Dim oProductPackItem As New DTOProductPackItem(oDrd("Sku"))
            With oProductPackItem
                .Category = oCategory
                .Qty = oDrd("Qty")
                .NomLlarg = SQLHelper.GetStringFromDataReader(oDrd("Myd"))
                .RRPP = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Pn2"))

                If oSku.PackItems.Count = 0 Then
                    oSku.Stock = oProductPackItem.Stock
                ElseIf oSku.Stock > oProductPackItem.Stock Then
                    oSku.Stock = oProductPackItem.Stock
                End If

                If oSku.Clients < oProductPackItem.Clients Then oSku.Clients = oProductPackItem.Clients
                oSku.RRPP.Add(oProductPackItem.RRPP.Times(oProductPackItem.Qty))
            End With
            oSku.PackItems.Add(oProductPackItem)
        Loop
        oDrd.Close()
    End Sub

    Shared Function Update(oSku As DTOProductSku, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSku, oTrans)
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


    Shared Sub Update(oSku As DTOProductSku, ByRef oTrans As SqlTransaction)
        Delete(oSku, oTrans)
        ProductSkuLoader.Update(oSku, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ProductPack ")
        sb.AppendLine("WHERE Parent='" & oSku.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim lin As Integer = 0
        For Each item As DTOProductPackItem In oSku.PackItems
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)

            oRow("Parent") = oSku.Guid
            oRow("Sku") = item.Guid
            oRow("Qty") = item.Qty
            oRow("Lin") = lin
            lin += 1
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSku As DTOProductSku, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSku, oTrans)
            ProductSkuLoader.Delete(oSku, oTrans)
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


    Shared Sub Delete(oSku As DTOProductSku, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ProductPack WHERE Parent='" & oSku.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class ProductPackItemsLoader
    Shared Function All(oSku As DTOProductSku, oMgz As DTOMgz) As List(Of DTOProductPackItem)
        Dim retval As New List(Of DTOProductPackItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Sku, Qty, Art.Myd, Stk.Stock, Pnx.Pn2, Preus.Retail ")
        sb.AppendLine("FROM ProductPack ")
        sb.AppendLine("LEFT OUTER JOIN Art ON ProductPack.Sku = Art.Guid ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT MgzGuid, ArtGuid, SUM(CASE WHEN Cod < 50 THEN Qty ELSE - Qty END) AS Stock FROM Arc GROUP BY MgzGuid, ArtGuid) Stk ON Stk.MgzGuid ='" & oMgz.Guid.ToString & "' AND Stk.ArtGuid = Art.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT ArtGuid, SUM(Pn2) AS Pn2 FROM PNC WHERE Cod = 2 AND pn3 = 0 GROUP BY ArtGuid HAVING SUM(Pn2) <> 0) Pnx ON Pnx.ArtGuid = Art.Guid ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT P2.Art, P2.Retail ")
        sb.AppendLine("                 FROM PriceListItem_Customer AS P2 ")
        sb.AppendLine("                 INNER JOIN PriceList_Customer AS P1 ON P2.PriceList = P1.Guid ")
        sb.AppendLine("                 INNER JOIN (SELECT MAX(dbo.PriceList_Customer.Fch) AS FCH, dbo.PriceListItem_Customer.Art FROM PriceList_Customer INNER JOIN PriceListItem_Customer ON dbo.PriceList_Customer.Guid = dbo.PriceListItem_Customer.PriceList AND PriceList_Customer.Fch <= GETDATE() AND PriceList_Customer.Customer IS NULL ")
        sb.AppendLine("                 GROUP BY PriceListItem_Customer.Art) AS P3 ON P1.Fch = P3.FCH And P3.Art = P2.Art) Preus ON Preus.Art=Art.Guid ")

        sb.AppendLine("WHERE ProductPack.Parent ='" & oSku.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ProductPack.Lin")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProductPackItem As New DTOProductPackItem(oDrd("Sku"))
            With oProductPackItem
                .Qty = oDrd("Qty")
                .NomLlarg = SQLHelper.GetStringFromDataReader(oDrd("Myd"))
                .RRPP = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Pn2"))
            End With
            retval.Add(oProductPackItem)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
