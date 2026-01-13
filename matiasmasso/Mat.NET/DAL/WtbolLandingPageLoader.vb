Public Class WtbolLandingpageLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOWtbolLandingPage
        Dim retval As DTOWtbolLandingPage = Nothing
        Dim oWtbolLandingPage As New DTOWtbolLandingPage(oGuid)
        If Load(oWtbolLandingPage) Then
            retval = oWtbolLandingPage
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWtbolLandingPage As DTOWtbolLandingPage) As Boolean
        If Not oWtbolLandingPage.IsLoaded And Not oWtbolLandingPage.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT WtbolLandingPage.Site, WtbolLandingPage.Product, WtbolLandingPage.Url ")
            sb.AppendLine(", WtbolLandingPage.FchCreated, WtbolLandingPage.UsrCreated ")
            sb.AppendLine(", WtbolLandingPage.FchStatus, WtbolLandingPage.UsrStatus ")
            sb.AppendLine(", WtbolSite.Web, WtbolSite.Customer ")
            sb.AppendLine(", UsrCreated.Adr AS UsrCreatedAdr, UsrCreated.Nickname AS UsrCreatedNickname, UsrCreated.Nom AS UsrCreatedNom  ")
            sb.AppendLine(", UsrStatus.Adr AS UsrStatusAdr, UsrStatus.Nickname AS UsrStatusNickname, UsrStatus.Nom AS UsrStatusNom  ")
            sb.AppendLine(", VwProductNom.* ")
            sb.AppendLine("FROM WtbolLandingPage ")
            sb.AppendLine("INNER JOIN WtbolSite ON WtbolLandingPage.Site = WtbolSite.Guid ")
            sb.AppendLine("INNER JOIN VwProductNom ON WtbolLandingPage.Product = VwProductNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email UsrCreated ON WtbolLandingPage.UsrCreated= UsrCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email UsrStatus ON WtbolLandingPage.UsrStatus= UsrStatus.Guid ")
            sb.AppendLine("WHERE WtbolLandingPage.Guid='" & oWtbolLandingPage.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oWtbolLandingPage
                    .Site = New DTOWtbolSite(oDrd("Site"))
                    .Site.Web = oDrd("Web")
                    If Not IsDBNull(oDrd("Customer")) Then
                        .Site.Customer = New DTOCustomer(oDrd("Customer"))
                    End If
                    .Product = SQLHelper.GetProductFromDataReader(oDrd)
                    .Uri = New Uri(oDrd("Url").ToString())
                    .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
                    If Not IsDBNull(oDrd("UsrCreated")) Then
                        .UsrCreated = New DTOUser(oDrd("UsrCreated"))
                        With .UsrCreated
                            .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("UsrCreatedAdr"))
                            .nickName = SQLHelper.GetStringFromDataReader(oDrd("UsrCreatedNickname"))
                            .nom = SQLHelper.GetStringFromDataReader(oDrd("UsrCreatedNom"))
                        End With
                    End If
                    .FchStatus = SQLHelper.GetFchFromDataReader(oDrd("FchStatus"))
                    If Not IsDBNull(oDrd("UsrStatus")) Then
                        .UsrStatus = New DTOUser(oDrd("UsrStatus"))
                        With .UsrStatus
                            .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("UsrStatusAdr"))
                            .nickName = SQLHelper.GetStringFromDataReader(oDrd("UsrStatusNickname"))
                            .nom = SQLHelper.GetStringFromDataReader(oDrd("UsrStatusNom"))
                        End With
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oWtbolLandingPage.IsLoaded
        Return retval
    End Function


    Shared Function Update(oWtbolLandingPage As DTOWtbolLandingPage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWtbolLandingPage, oTrans)
            oTrans.Commit()
            oWtbolLandingPage.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oWtbolLandingPage As DTOWtbolLandingPage, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolLandingPage ")
        sb.AppendLine("WHERE Guid='" & oWtbolLandingPage.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWtbolLandingPage.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWtbolLandingPage
            oRow("Site") = SQLHelper.NullableBaseGuid(.Site)
            oRow("Url") = .Uri.AbsoluteUri
            oRow("Product") = .Product.Guid
            oRow("FchCreated") = SQLHelper.NullableFch(.FchCreated)
            oRow("UsrCreated") = SQLHelper.NullableBaseGuid(.UsrCreated)
            oRow("Status") = .Status
            oRow("FchStatus") = SQLHelper.NullableFch(.FchStatus)
            oRow("UsrStatus") = SQLHelper.NullableBaseGuid(.UsrStatus)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oWtbolLandingPage As DTOWtbolLandingPage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWtbolLandingPage, oTrans)
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


    Shared Sub Delete(oWtbolLandingPage As DTOWtbolLandingPage, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WtbolLandingPage WHERE Guid='" & oWtbolLandingPage.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region


End Class

Public Class WtbolLandingpagesLoader
    Shared Function All(oProduct As DTOProduct, Optional includeStocksFromMgz As DTOMgz = Nothing) As List(Of DTOWtbolLandingPage)
        Dim retval As New List(Of DTOWtbolLandingPage)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwWtbolLandingPage.Site, VwWtbolLandingPage.FchStatus, WtbolSite.Nom, WtbolSite.Web ")
        sb.AppendLine(", MIN(CAST(VwWtbolLandingPage.Url AS Varchar(MAX))) AS Url ")
        sb.AppendLine(", MIN(VwWtbolLandingPage.LandingPage) AS LandingPage ")
        sb.AppendLine(", SUM(VwWtbolLandingPage.Stock) AS SiteStock ")
        sb.AppendLine(", MIN(VwWtbolLandingPage.Price) AS Price ")
        sb.AppendLine(", MAX(VwWtbolLandingPage.StockFchCreated) AS FchLastStocks ")
        If includeStocksFromMgz IsNot Nothing Then
            sb.AppendLine(", SUM(VwSkuStocks.Stock) AS MgzStock ")
        End If
        sb.AppendLine(", VwWtbolInventory.Inventory ")
        sb.AppendLine("FROM VwWtbolLandingPage ")
        sb.AppendLine("INNER JOIN VwProductNom ON VwWtbolLandingPage.Product = VwProductNom.Guid ")
        If includeStocksFromMgz IsNot Nothing Then
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwProductNom.SkuGuid = VwSkuStocks.SkuGuid ")
        End If
        sb.AppendLine("LEFT OUTER JOIN VwWtbolInventory ON VwWtbolLandingPage.Site = VwWtbolInventory.Site ")
        sb.AppendLine("INNER JOIN WtbolSite ON VwWtbolLandingPage.Site = WtbolSite.Guid ")
        sb.AppendLine("WHERE VwProductNom.Guid = '" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("AND WtbolSite.Active <> 0 ")
        sb.AppendLine("GROUP BY VwWtbolLandingPage.Site, VwWtbolLandingPage.FchStatus, WtbolSite.Nom, WtbolSite.Web, VwWtbolInventory.Inventory ")
        sb.AppendLine("ORDER BY SiteStock DESC, VwWtbolInventory.Inventory DESC, WtbolSite.Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOWtbolLandingPage(oDrd("LandingPage"))
            With item
                .Site = New DTOWtbolSite(oDrd("Site"))
                .Site.Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Site.Web = oDrd("Web")
                .Uri = New Uri(oDrd("Url"))
                .FchStatus = SQLHelper.GetFchFromDataReader(oDrd("FchStatus"))
                If includeStocksFromMgz IsNot Nothing Then
                    .MgzStock = SQLHelper.GetIntegerFromDataReader(oDrd("MgzStock"))
                End If
                .RRPP = SQLHelper.GetAmtFromDataReader(oDrd("Price"))
                .Product = oProduct
                .Site.FchLastStocks = SQLHelper.GetFchFromDataReader(oDrd("FchLastStocks"))
                If .Site.FchLastStocks > DTO.GlobalVariables.Now().AddHours(-24) Then
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("SiteStock"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        retval = retval.OrderByDescending(Function(x) x.Stock).ToList
        Return retval
    End Function

    Shared Function All(oPremiumLine As DTOPremiumLine, Optional includeStocksFromMgz As DTOMgz = Nothing) As List(Of DTOWtbolLandingPage)
        Dim retval As New List(Of DTOWtbolLandingPage)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwWtbolLandingPage.Site, VwWtbolLandingPage.FchStatus, WtbolSite.Nom, WtbolSite.Web ")
        sb.AppendLine(", MIN(CAST(VwWtbolLandingPage.Url AS Varchar(MAX))) AS Url ")
        sb.AppendLine(", MIN(VwWtbolLandingPage.LandingPage) AS LandingPage ")
        sb.AppendLine(", SUM(VwWtbolLandingPage.Stock) AS SiteStock ")
        sb.AppendLine(", MIN(VwWtbolLandingPage.Price) AS Price ")
        sb.AppendLine(", MAX(VwWtbolLandingPage.StockFchCreated) AS FchLastStocks ")
        'If includeStocksFromMgz IsNot Nothing Then
        'sb.AppendLine(", SUM(VwSkuStocks.Stock) AS MgzStock ")
        'End If
        sb.AppendLine(", VwWtbolInventory.Inventory ")
        sb.AppendLine("FROM VwWtbolLandingPage ")
        sb.AppendLine("INNER JOIN VwProductNom ON VwWtbolLandingPage.Product = VwProductNom.Guid ")
        'If includeStocksFromMgz IsNot Nothing Then
        'sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwProductNom.SkuGuid = VwSkuStocks.SkuGuid ")
        'End If
        sb.AppendLine("LEFT OUTER JOIN VwWtbolInventory ON VwWtbolLandingPage.Site = VwWtbolInventory.Site ")
        sb.AppendLine("INNER JOIN WtbolSite ON VwWtbolLandingPage.Site = WtbolSite.Guid ")
        sb.AppendLine("WHERE VwProductNom.Guid = '" & oPremiumLine.Guid.ToString & "' ")
        sb.AppendLine("AND WtbolSite.Active <> 0 ")
        sb.AppendLine("GROUP BY VwWtbolLandingPage.Site, VwWtbolLandingPage.FchStatus, WtbolSite.Nom, WtbolSite.Web, VwWtbolInventory.Inventory ")
        sb.AppendLine("ORDER BY SiteStock DESC, VwWtbolInventory.Inventory DESC, WtbolSite.Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOWtbolLandingPage(oDrd("LandingPage"))
            With item
                .Site = New DTOWtbolSite(oDrd("Site"))
                .Site.Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Site.Web = oDrd("Web")
                .Uri = New Uri(oDrd("Url"))
                .FchStatus = SQLHelper.GetFchFromDataReader(oDrd("FchStatus"))
                'If includeStocksFromMgz IsNot Nothing Then
                '.MgzStock = SQLHelper.GetIntegerFromDataReader(oDrd("MgzStock"))
                'End If
                '.RRPP = SQLHelper.GetAmtFromDataReader(oDrd("Price"))
                '.Product = oProduct
                '.Site.FchLastStocks = SQLHelper.GetFchFromDataReader(oDrd("FchLastStocks"))
                'If .Site.FchLastStocks > DTO.GlobalVariables.Now().AddHours(-24) Then
                '.Stock = SQLHelper.GetIntegerFromDataReader(oDrd("SiteStock"))
                'End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        retval = retval.OrderByDescending(Function(x) x.Stock).ToList
        Return retval
    End Function

End Class
