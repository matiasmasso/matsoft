Public Class WtbolStocksLoader

    Shared Function All(oSite As DTOWtbolSite) As List(Of DTOWtbolStock)
        Dim retval As New List(Of DTOWtbolStock)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.*, WtbolStock.Stock, WtbolStock.Price, WtbolLandingPage.Url, X.LastFch ")
        sb.AppendLine("FROM WtbolStock ")
        sb.AppendLine("INNER JOIN (SELECT Site, MAX(FchCreated) AS LastFch ")
        sb.AppendLine("             FROM WtbolStock ")
        sb.AppendLine("             GROUP BY Site) X ON WtbolStock.Site=X.Site AND WtbolStock.FchCreated=X.LastFch ")
        sb.AppendLine("INNER JOIN VwSkuNom ON WtbolStock.Sku=VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN WtbolLandingPage ON WtbolStock.Site=WtbolLandingPage.Site AND WtbolStock.Sku=WtbolLandingPage.Product ")
        sb.AppendLine("WHERE Wtbolstock.Site='" & oSite.Guid.ToString & "' ")
        sb.AppendLine("AND Wtbolstock.Stock > 0 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandNom, VwSkuNom.CategoryNom, VwSkuNom.SkuNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("Url")) Then
                Dim item As New DTOWtbolStock()
                With item
                    .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                    .Stock = oDrd("Stock")
                    .Price = SQLHelper.GetAmtFromDataReader(oDrd("Price"))
                    .Uri = New Uri(oDrd("Url").ToString())
                    .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("LastFch"))
                End With
                retval.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Merged(oSite As DTOWtbolSite, oMgz As DTOMgz) As List(Of DTOWtbolStock)
        Dim retval As New List(Of DTOWtbolStock)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine(", (CASE WHEN VwWtbolLandingPage.Stock IS NULL THEN 0 ELSE VwWtbolLandingPage.Stock END) + (CASE WHEN VwSkuStocks.Stock IS NULL THEN 0 ELSE VwSkuStocks.Stock END) AS Stock ")
        sb.AppendLine(", (CASE WHEN VwWtbolLandingPage.Price IS NULL THEN VwRetail.Retail ELSE VwWtbolLandingPage.Price END) AS Price ")
        sb.AppendLine(", VwWtbolLandingPage.Url ")
        sb.AppendLine("FROM VwWtbolLandingPage ")
        sb.AppendLine("INNER JOIN VwSkuNom ON VwWtbolLandingPage.Product=VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN VwRetail ON VwWtbolLandingPage.Product=VwRetail.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("WHERE VwWtbolLandingPage.Site='" & oSite.Guid.ToString & "' ")
        sb.AppendLine("AND (CASE WHEN VwWtbolLandingPage.Stock IS NULL THEN 0 ELSE VwWtbolLandingPage.Stock END) + (CASE WHEN VwSkuStocks.Stock IS NULL THEN 0 ELSE VwSkuStocks.Stock END)  > 0 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandNom, VwSkuNom.CategoryNom, VwSkuNom.SkuNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("Url")) Then
                Dim item As New DTOWtbolStock()
                With item
                    .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                    .Stock = oDrd("Stock")
                    .Price = SQLHelper.GetAmtFromDataReader(oDrd("Price"))
                    .Uri = New Uri(oDrd("Url").ToString())
                End With
                retval.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oSite As DTOWtbolSite, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oSite, oTrans)

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

    Shared Sub Update(oSite As DTOWtbolSite, oTrans As SqlTransaction)
        Dim DtFchCreated As Date = DTO.GlobalVariables.Now()
        Dim sFchCreated As String = Format(DtFchCreated, "yyyy-MM-dd HH:mm:ss")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolStock")
        sb.AppendLine("WHERE Site = '" & oSite.Guid.ToString & "'")
        sb.AppendLine("AND FchCreated = '" & sFchCreated & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each value In oSite.Stocks
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With value
                oRow("Site") = .Site.Guid
                oRow("Sku") = .Sku.Guid
                oRow("Stock") = .Stock
                oRow("Price") = .Price.Eur
                oRow("FchCreated") = DtFchCreated
            End With
        Next
        oDA.Update(oDs)
    End Sub

End Class
