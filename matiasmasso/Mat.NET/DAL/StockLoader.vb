Public Class StockLoader
    Shared Function Find(oSku As DTOProductSku, oMgz As DTOMgz) As DTOStock
        Dim retval As New DTOStock
        retval.Sku = oSku

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT sum(X.Stock) AS Stock, sum(X.Pn2) AS Pn2 ")
        sb.AppendLine("FROM (")
        sb.AppendLine("         SELECT  Arc.ArtGuid, SUM(CASE WHEN Arc.Cod < 50 THEN Arc.Qty ELSE - Arc.Qty END) AS Stock, 0 AS Pn2 ")
        sb.AppendLine("         FROM Arc ")
        sb.AppendLine("         WHERE Arc.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("         GROUP BY Arc.ArtGuid ")
        sb.AppendLine("    UNION ")
        sb.AppendLine("         SELECT Pnc.ArtGuid, 0 AS Stock, Pnc.Pn2 ")
        sb.AppendLine("         FROM Pnc ")
        sb.AppendLine("         INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("         WHERE Pnc.Pn2>0 AND Pdc.Pot=0 AND Pdc.Cod=2 ")
        sb.AppendLine(") X ")
        sb.AppendLine("WHERE X.ArtGuid ='" & oSku.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With retval
                .UnitsInStock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .UnitsInClients = SQLHelper.GetIntegerFromDataReader(oDrd("Pn2"))
            End With
        End If
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class StocksLoader


    Shared Function All(oUser As DTOUser, oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.EAN13 ")
        'sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine(", VwSkuStocks.Stock, VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsBlockStock, VwSkuPncs.ClientsEnProgramacio ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("INNER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Operadora, DTORol.Ids.SalesManager
                sb.AppendLine("WHERE VwSkuStocks.Stock>0 ")
            Case DTORol.Ids.Rep
                sb.AppendLine("INNER JOIN VwRepSkus ON VwSkuNom.SkuGuid = VwRepSkus.SkuGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwRepSkus.Rep=Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND VwSkuStocks.Stock>0 ")
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                sb.AppendLine("INNER JOIN VwCustomerSkus ON VwSkuNom.SkuGuid = VwCustomerSkus.SkuGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwCustomerSkus.Customer=Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
                sb.AppendLine("AND VwSkuStocks.Stock>0 AND (VwCustomerSkus.CodExclusio IS NULL OR VwCustomerSkus.CodExclusio =" & DTOProductSku.CodisExclusio.Inclos & ") ")
        End Select
        sb.AppendLine("ORDER BY VwSkuNom.SkuId ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim oSku As New DTOProductSku(oDrd("SkuGuid"))
            'Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With oSku
                .Id = oDrd("SkuId")
                .Ean13 = SQLHelper.GetEANFromDataReader(oDrd("EAN13"))
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
            End With

            retval.Add(oSku)


        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oMgz As DTOMgz, Optional oCategory As DTOProductCategory = Nothing, Optional related As DTOProduct.Relateds = DTOProduct.Relateds.NotSet, Optional IncludeObsoletos As Boolean = False) As List(Of DTOStock)
        Dim retval As New List(Of DTOStock)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Art.Guid, Art.Art,  ")
        sb.AppendLine("SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor, ")
        sb.AppendLine("Art.LastProduction, ")
        sb.AppendLine("VwSkuStocks.Stock, ")
        sb.AppendLine("VwSkuPncs.Clients,  ")
        sb.AppendLine("VwSkuPncs.ClientsAlPot,  ")
        sb.AppendLine("(CASE WHEN Art.Image IS NULL THEN 0 ELSE 1 END) AS ImageExists,  ")
        sb.AppendLine("VwSkuPncs.Pn1,  ")
        sb.AppendLine("Previsio.Prv, VwSkuRetail.Retail,  ")
        sb.AppendLine("Art.Obsoleto AS EX  ")
        sb.AppendLine("FROM Art  ")
        sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuStocks ON ART.Guid=VwSkuStocks.SkuGuid AND (VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' OR ArtStock.MgzGuid IS NULL)  ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuPncs ON Art.Guid=VwSkuPncs.SkuGuid  ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT ArtGuid,SUM(Qty) AS PRV FROM DELIVERY GROUP BY ArtGuid) AS PREVISIO ON ART.Guid=PREVISIO.ArtGuid  ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuRetail ON Art.Guid=VwSkuRetail.SkuGuid  ")

        If related = DTOProduct.Relateds.NotSet Then
            sb.AppendLine("WHERE Art.Category = '" & oCategory.Guid.ToString & "' ")
        Else
            sb.AppendLine("INNER JOIN ArtSpare ON Art.Guid=ArtSpare.ProductGuid AND ArtSpare.Cod=" & CInt(related) & " ")
            sb.AppendLine("WHERE ArtSpare.TargetGuid = '" & oCategory.Guid.ToString & "' ")
        End If

        If Not IncludeObsoletos Then
            sb.AppendLine("AND (VwSkuStocks.Stock<>0 OR VwSkuPncs.Clients<>0 OR VwSkuPncs.Pn1<>0 OR Art.Obsoleto=0)  ")
        End If
        sb.AppendLine("ORDER BY Art.Obsoleto, SkuNom.Esp, Art.Art ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As New DTOProductSku(oDrd("Guid"))
            With oSku
                .Category = oCategory
                .Id = oDrd("Art")
                SQLHelper.LoadLangTextFromDataReader(oSku.nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                .lastProduction = CBool(oDrd("LastProduction"))
                .ImageExists = oDrd("ImageExists")
                .RRPP = Defaults.AmtOrNothing(oDrd("Retail"))
                .Obsoleto = CBool(oDrd("Ex"))
            End With

            Dim oItem As New DTOStock
            With oItem
                .Sku = oSku
                If Not IsDBNull(oDrd("stock")) Then
                    .UnitsInStock = oDrd("Stock")
                End If
                If Not IsDBNull(oDrd("Clients")) Then
                    .UnitsInClients = oDrd("Clients")
                End If
                If Not IsDBNull(oDrd("ClientsAlPot")) Then
                    .UnitsInPot = oDrd("ClientsAlPot")
                End If

                If Not IsDBNull(oDrd("Pn1")) Then
                    .UnitsInProveidor = oDrd("Pn1")
                End If
                If Not IsDBNull(oDrd("Prv")) Then
                    .UnitsInPrevisio = oDrd("Prv")
                End If
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
