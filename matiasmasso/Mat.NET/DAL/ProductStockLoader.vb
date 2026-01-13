Public Class ProductStocksLoader

    Shared Function CostAndInventory(oEmp As DTOEmp, oMgz As DTOMgz, Optional oCategory As DTOProductCategory = Nothing) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwRetail.Retail ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsBlockStock, VwSkuPncs.ClientsEnProgramacio ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwRetail ON VwSkuNom.SkuGuid = VwRetail.SkuGuid ")
        sb.AppendLine("WHERE VwSkuNom.Emp=" & oEmp.Id & " ")
        If oCategory IsNot Nothing Then
            sb.AppendLine("AND VwSkuNom.CategoryGuid='" & oCategory.Guid.ToString & "' ")
        End If
        sb.AppendLine("AND (VwSkuNom.Obsoleto = 0 OR VwSkuStocks.Stock > 0) ")
        sb.AppendLine("ORDER BY VwSkuNom.SkuNom ")

        If oCategory Is Nothing Then oCategory = New DTOProductCategory
        Dim oBrand As New DTOProductBrand
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)

            'Evita duplicar objectes parent
            If oSku.Category.Equals(oCategory) Then
                oSku.Category = oCategory
            Else
                oCategory = oSku.Category
                If oCategory.Brand.Equals(oBrand) Then
                    oCategory.Brand = oBrand
                Else
                    oBrand = oCategory.Brand
                End If
            End If

            With oSku
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
            End With
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromUserOrCustomer(oEmp As DTOEmp, oValue As DTOBaseGuid, oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT VwSkuNom.SkuGuid, VwSkuNom.Skuid, VwSkuNom.SkuNom, VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom, VwSkuNom.SkuRef, VwSkuNom.Ean13 ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        If TypeOf oValue Is DTOCustomer Then
            sb.AppendLine("INNER JOIN VwCustomerSkus ON VwCustomerSkus.SkuGuid = VwSkuNom.SkuGuid ")
            sb.AppendLine("WHERE VwCustomerSkus.Customer = '" & oValue.Guid.ToString & "' AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
            sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
        ElseIf TypeOf oValue Is DTOUser Then
            Dim oUser As DTOUser = oValue
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin
                Case DTORol.Ids.manufacturer
                    sb.AppendLine("INNER JOIN Email_Clis ON VwSkuNom.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oValue.Guid.ToString & "' ")
                Case DTORol.Ids.rep, DTORol.Ids.salesManager
                    sb.AppendLine("INNER JOIN VwSalesManagerSkus ON VwSkuNom.SkuGuid = VwSalesManagerSkus.SkuGuid ")
                    sb.AppendLine("INNER JOIN Email_Clis ON VwSalesManagerSkus.SalesManager = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oValue.Guid.ToString & "' ")
                Case DTORol.Ids.rep, DTORol.Ids.comercial
                    sb.AppendLine("INNER JOIN VwRepSkus ON VwSkuNom.SkuGuid = VwRepSkus.SkuGuid ")
                    sb.AppendLine("INNER JOIN Email_Clis ON VwRepSkus.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oValue.Guid.ToString & "' ")
                Case DTORol.Ids.cliLite, DTORol.Ids.cliFull
                    sb.AppendLine("INNER JOIN VwCustomerSkus ON VwCustomerSkus.SkuGuid = VwSkuNom.SkuGuid AND VwCustomerSkus.Obsoleto = 0 ")
                    sb.AppendLine("INNER JOIN Email_Clis ON VwCustomerSkus.Customer = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oValue.Guid.ToString & "' ")
            End Select

            sb.AppendLine("WHERE VwSkuNom.Emp = " & oEmp.Id & " ")
            sb.AppendLine("AND (VwSkuNom.CategoryCodi = " & DTOProductCategory.Codis.standard & " OR VwSkuNom.CategoryCodi = " & DTOProductCategory.Codis.accessories & ") ")
            sb.AppendLine("AND VwSkuNom.BrandNom <> 'Varios' ")
            sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")

            sb.AppendLine("GROUP BY VwSkuNom.SkuGuid, VwSkuNom.Skuid, VwSkuNom.SkuNom, VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
            sb.AppendLine(", VwSkuNom.CategoryOrd, VwSkuNom.BrandOrd ")
            sb.AppendLine(", VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom, VwSkuNom.SkuRef, VwSkuNom.Ean13 ")
            sb.AppendLine(", VwSkuStocks.Stock ")
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock ")
        Else
            sb.AppendLine("WHERE 1=0 ")
        End If

        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.BrandGuid ")
        sb.AppendLine(", VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryGuid ")
        sb.AppendLine(", VwSkuNom.SkuNom ")

        Dim SQL As String = sb.ToString

        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With item
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromProveidor(oProveidor As DTOProveidor, oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT VwSkuNom.SkuGuid, VwSkuNom.Skuid, VwSkuNom.SkuNom, VwSkuNom.SkuNomLlarg, VwSkuNom.SkuMoq, VwSkuNom.SkuForzarMoq ")
        sb.AppendLine(", VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom, VwSkuNom.SkuRef, VwSkuNom.Ean13 ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryMoq, VwSkuNom.CategoryForzarMoq ")
        sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuCost.Price AS Cost, VwSkuCost.Discount_OnInvoice ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuCost ON VwSkuNom.SkuGuid = VwSkuCost.SkuGuid AND VwSkuNom.Proveidor = VwSkuCost.Proveidor ")

        sb.AppendLine("WHERE VwSkuNom.Proveidor = '" & oProveidor.Guid.ToString & "' ")

        sb.AppendLine("GROUP BY VwSkuNom.SkuGuid, VwSkuNom.Skuid, VwSkuNom.SkuNom, VwSkuNom.SkuNomLlarg, VwSkuNom.SkuMoq, VwSkuNom.SkuForzarMoq ")
        sb.AppendLine(", VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom, VwSkuNom.SkuRef, VwSkuNom.Ean13 ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryMoq, VwSkuNom.CategoryForzarMoq, VwSkuNom.CategoryOrd ")
        sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom, VwSkuNom.BrandOrd ")
        sb.AppendLine(", VwSkuCost.Price, VwSkuCost.Discount_OnInvoice ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock ")

        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.BrandGuid ")
        sb.AppendLine(", VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryGuid ")
        sb.AppendLine(", VwSkuNom.SkuNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            Try
                With item
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    If Not IsDBNull(oDrd("Cost")) Then
                        .Cost = SQLHelper.GetAmtFromDataReader(oDrd("Cost"))
                        .SupplierDtoOnInvoice = SQLHelper.GetDecimalFromDataReader(oDrd("Discount_OnInvoice"))
                    End If
                End With

                retval.Add(item)
            Catch ex As Exception
                'Stop
            End Try

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Skus(oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT X.ArtGuid, Sum(X.Stock) AS Stock, SUM(X.Pn2) AS Pn2, SUM(X.Pn1) AS Pn1 ")
        sb.AppendLine("FROM (SELECT Arc.ArtGuid, SUM(CASE WHEN Cod<50 then Qty ELSE -Qty END) AS Stock,  0 AS pn2, 0 AS Pn1 ")
        sb.AppendLine("     FROM Arc WHERE MgzGuid='" & oMgz.Guid.ToString & "' GROUP BY ArtGuid HAVING SUM(CASE WHEN Cod<50 then Qty else -Qty END)>0 ")
        sb.AppendLine("     UNION ")
        sb.AppendLine("     SELECT ArtGuid, 0 as stock, sum(case when cod=2 and Pdc.Pot=0 then pn2 else 0 end) as pn2, sum(case when cod=1 then pn2 else 0 end) as pn1 ")
        sb.AppendLine("     FROM Pnc ")
        sb.AppendLine("     INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("     WHERE Pn2>0 ")
        sb.AppendLine("     GROUP BY Pnc.ArtGuid) X ")
        sb.AppendLine("GROUP BY X.ArtGuid ")
        sb.AppendLine("HAVING SUM(X.Stock)<>0 OR SUM(X.Pn2)<>0 OR SUM(X.Pn1)<>0 ")
        sb.AppendLine("ORDER BY X.ArtGuid ")

        Dim SQL As String = sb.ToString

        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As New DTOProductSku(oDrd("ArtGuid"))
            With oSku
                If Not IsDBNull(oDrd("Stock")) Then
                    .Stock = oDrd("Stock")
                End If
                If Not IsDBNull(oDrd("Pn2")) Then
                    .Clients = oDrd("Pn2")
                End If
                If Not IsDBNull(oDrd("Pn1")) Then
                    .Proveidors = oDrd("Pn1")
                End If
            End With

            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Custom(oCustomer As DTOCustomer, oMgz As DTOMgz) As List(Of DTOCustomerProduct)
        Dim retval As New List(Of DTOCustomerProduct)

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT ArtCustRef.Guid, ArtCustRef.ArtGuid, ArtCustRef.Ref AS CustRef, ArtCustRef.Dsc AS CustDsc, ArtCustRef.Color AS CustColor")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("INNER JOIN VwSkuNom ON ArtCustRef.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON ArtCustRef.ArtGuid = VwSkuStocks.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON ArtCustRef.ArtGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("WHERE ArtCustRef.CliGuid = '" & oCustomer.Guid.ToString & "' AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ArtCustRef.Ref ")

        Dim SQL As String = sb.ToString

        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomerProduct(oDrd("ArtGuid"))
            With item
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                With .Sku
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                End With
                .Ref = SQLHelper.GetStringFromDataReader(oDrd("CustRef"))
                .Dsc = SQLHelper.GetStringFromDataReader(oDrd("CustDsc"))
                .Color = SQLHelper.GetStringFromDataReader(oDrd("CustColor"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
