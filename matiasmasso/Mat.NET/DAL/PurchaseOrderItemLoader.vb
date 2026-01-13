Public Class PurchaseOrderItemLoader
    Shared Function DeliveryItems(ByRef value As DTOPurchaseOrderItem) As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.Guid AS ArcGuid, Arc.Qty, Arc.Eur, Arc.Cur, Arc.Pts, Arc.Dto, Arc.Cod, Arc.RepGuid, Arc.Com ")
        sb.AppendLine(", Arc.AlbGuid, Alb.Alb as AlbNum, Alb.Fch as AlbFch ")
        sb.AppendLine(", Alb.FraGuid, Fra.Fra, Fra.Fch as FraFch ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("WHERE Arc.PncGuid = '" & value.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Alb.fch")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oInvoice As DTOInvoice = Nothing
            If Not IsDBNull(oDrd("FraGuid")) Then
                oInvoice = New DTOInvoice(oDrd("FraGuid"))
                With oInvoice
                    .Num = oDrd("Fra")
                    .Fch = oDrd("FraFch")
                End With
            End If

            Dim oDelivery As New DTODelivery(oDrd("AlbGuid"))
            With oDelivery
                .Id = oDrd("AlbNum")
                .Fch = oDrd("AlbFch")
                .Invoice = oInvoice
            End With

            Dim oRepComLiquidable As DTORepComLiquidable = Nothing
            If Not IsDBNull(oDrd("RepGuid")) Then
                oRepComLiquidable = New DTORepComLiquidable
                With oRepComLiquidable
                    .Rep = New DTORep(oDrd("RepGuid"))
                    .Comisio = DTOAmt.Factory(CDec(oDrd("Com")))
                End With
            End If

            Dim item As New DTODeliveryItem(oDrd("ArcGuid"))
            With item
                .Qty = oDrd("Qty")
                .PurchaseOrderItem = value
                .Delivery = oDelivery
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                .Dto = oDrd("Dto")
                .RepComLiquidable = oRepComLiquidable
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function



    Shared Function UpdateRepCom(value As DTOPurchaseOrderItem, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Dim SQL As String = "SELECT * FROM Pnc WHERE Guid='" & value.Guid.ToString & "'"

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow = Nothing

            If oTb.Rows.Count = 0 Then
                exs.Add(New Exception("no s'ha trobat la linia de comanda"))
            Else
                oRow = oTb.Rows(0)
            End If

            If value.RepCom Is Nothing Then
                oRow("RepGuid") = System.DBNull.Value
                oRow("Com") = 0
                oRow("RepCustom") = False
            Else
                With value.RepCom
                    oRow("RepGuid") = SQLHelper.NullableBaseGuid(.Rep)
                    oRow("Com") = .Com
                    oRow("RepCustom") = .RepCustom
                End With
            End If

            oDA.Update(oDs)
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

    Shared Function ResetPendingQty(value As DTOPurchaseOrderItem, exs As List(Of Exception)) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Pnc ")
        sb.AppendLine("SET Pnc.Pn2 = Pnc.Qty - (CASE WHEN X.Sortides IS NULL THEN 0 ELSE X.Sortides END) ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT Arc.PncGuid, SUM(Qty) AS Sortides FROM Arc GROUP BY Arc.PncGuid) X ON Pnc.Guid = X.PncGuid ")
        sb.AppendLine("WHERE Pnc.Guid='" & value.Guid.ToString & "'")

        Dim SQL As String = sb.ToString
        Dim rc As Integer = SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function UnitatsSortides(value As DTOPurchaseOrderItem) As Integer
        Dim retval As Integer

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SUM(Arc.Qty) AS Qty ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("WHERE PncGuid = '" & value.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("Qty")) Then
                retval = oDrd("Qty")
            End If
        End If
        oDrd.Close()
        Return retval
    End Function

End Class

Public Class PurchaseOrderItemsLoader

    Shared Function All(oContact As DTOContact) As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Guid AS PdcGuid, Pdc.Fch, Pdc.Pdc, Pnc.Qty, Pnc.Pn2 ")
        sb.AppendLine(", Pnc.Guid AS PncGuid, Pnc.ArtGuid, Pnc.Eur, Pnc.Dto ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE Pdc.CliGuid = '" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Pdc.Cod=" & CInt(DTOPurchaseOrder.Codis.Client) & " ")
        sb.AppendLine("AND Pnc.ErrCod=" & DTOPurchaseOrderItem.ErrCods.Success & " ")
        sb.AppendLine("ORDER BY Pdc.fch DESC, Pdc.Pdc DESC, Pnc.Lin")
        Dim SQL As String = sb.ToString

        Dim oPurchaseOrder As New DTOPurchaseOrder
        Dim oCustomer As New DTOCustomer(oContact.Guid)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Num = oDrd("Pdc")
                    .Fch = oDrd("Fch")
                    .Customer = oCustomer
                    .Cod = DTOPurchaseOrder.Codis.Client
                End With
            End If


            Dim item As New DTOPurchaseOrderItem(oDrd("PncGuid"))
            With item
                .PurchaseOrder = oPurchaseOrder
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                .Qty = oDrd("Qty")
                .Pending = oDrd("Pn2")
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = oDrd("Dto")
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oProduct As DTOProduct) As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT  Pnc.Guid AS PncGuid, Pnc.Qty, Pnc.Eur, Pnc.Dto ")
        sb.AppendLine(", Pdc.Guid AS PdcGuid, Pdc.Fch ")
        sb.AppendLine(", Pdc.CliGuid, CliGral.FullNom ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwProductParent ON Pnc.ArtGuid = VwProductParent.Child ")
        sb.AppendLine("WHERE VwProductParent.Parent = '" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("AND Pdc.Cod=" & CInt(DTOPurchaseOrder.Codis.Client) & " ")
        sb.AppendLine("AND Pnc.ErrCod=" & DTOPurchaseOrderItem.ErrCods.Success & " ")
        sb.AppendLine("ORDER BY CliGral.FullNom, CliGral.Guid, Pdc.Fch")
        Dim SQL As String = sb.ToString

        Dim oPurchaseOrder As New DTOPurchaseOrder
        Dim oCustomer As New DTOCustomer()
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            If Not oCustomer.Guid.Equals(oDrd("CliGuid")) Then
                oCustomer = New DTOCustomer(oDrd("PdcGuid"))
                oCustomer.FullNom = oDrd("FullNom")
            End If

            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Customer = oCustomer
                    .Fch = oDrd("Fch")
                End With
            End If

            Dim item As New DTOPurchaseOrderItem(oDrd("PncGuid"))
            With item
                .PurchaseOrder = oPurchaseOrder
                .Qty = oDrd("Qty")
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = oDrd("Dto")
            End With

            oPurchaseOrder.Items.Add(item)
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function



    Shared Function Pending(oSku As DTOProductSku, oCod As DTOPurchaseOrder.Codis, Optional oMgz As DTOMgz = Nothing) As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Guid AS PdcGuid, Pdc.Fch, Pdc.Pdc, Pdc.Pdd, Pdc.FchMin, Pdc.FchMax, Pdc.CliGuid, Pdc.BlockStock ")
        sb.AppendLine(", Pnc.Guid AS PncGuid, Pnc.Pn2, Pdc.pot, Pnc.FchConfirm ")
        sb.AppendLine(", Pnc.Eur, Pnc.Dto ")
        sb.AppendLine(", CliGral.FullNom ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("AND Pnc.Pn2<>0 ")
        sb.AppendLine("AND Pdc.cod=" & oCod & " ")
        sb.AppendLine("AND Pnc.ArtGuid='" & oSku.Guid.ToString & "' ")
        sb.AppendLine("AND Pnc.ErrCod=" & DTOPurchaseOrderItem.ErrCods.Success & " ")
        sb.AppendLine("ORDER BY Pdc.fch, Pdc.Pdc, Pnc.Lin")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim oPurchaseOrder As New DTOPurchaseOrder(oDrd("PdcGuid"))
            With oPurchaseOrder
                .Num = oDrd("Pdc")
                .Fch = oDrd("Fch")
                .fchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("FchMin"))
                .fchDeliveryMax = SQLHelper.GetFchFromDataReader(oDrd("FchMax"))
                Select Case oCod
                    Case DTOPurchaseOrder.Codis.proveidor
                        .proveidor = New DTOProveidor(oDrd("CliGuid"))
                        .Proveidor.FullNom = oDrd("FullNom")
                    Case Else
                        .customer = New DTOCustomer(oDrd("CliGuid"))
                        .Customer.FullNom = oDrd("FullNom")
                End Select
                .Concept = oDrd("Pdd")
                .Cod = oCod
                .Pot = oDrd("Pot")
                .BlockStock = oDrd("BlockStock")
            End With

            Dim item As New DTOPurchaseOrderItem(oDrd("PncGuid"))
            With item
                .PurchaseOrder = oPurchaseOrder
                .Sku = oSku
                .Pending = oDrd("Pn2")
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = oDrd("Dto")
                .ETD = SQLHelper.GetFchFromDataReader(oDrd("FchConfirm"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Pending(oContact As DTOBaseGuid, oCod As DTOPurchaseOrder.Codis, oMgz As DTOMgz, oLevel As DTOCustomer.GroupLevels) As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnc.Guid, Pdc.Guid AS PdcGuid, Pdc.Fch, Pdc.Pdc, Pdc.Pdd, Pdc.FchMin, Pdc.FchMax, Pnc.Qty, Pnc.Pn2, Pnc.Eur, Pnc.Cur, Pnc.Pts, Pnc.Dto, Pnc.FchConfirm ")
        sb.AppendLine(", Pnc.ArtGuid, VwSkuNom.*, VwSkuStocks.Stock, VwSkuPncs.Clients ")
        sb.AppendLine(", Pdc.CliGuid, CliClient.Ref AS CliRef, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON Pnc.ArtGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON Pnc.ArtGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON Pdc.CliGuid = CliClient.Guid ")
        Select Case oLevel
            Case DTOCustomer.GroupLevels.Single
                sb.AppendLine("WHERE Pdc.CliGuid = '" & oContact.Guid.ToString & "' ")
            Case DTOCustomer.GroupLevels.Chain
                'sb.AppendLine("INNER JOIN CliClient ON Pdc.CliGuid = CliClient.Guid ")
                sb.AppendLine("WHERE (CliClient.CcxGuid = '" & oContact.Guid.ToString & "' OR CliClient.Guid = '" & oContact.Guid.ToString & "') ")
            Case DTOCustomer.GroupLevels.Holding
                'sb.AppendLine("INNER JOIN CliClient ON Pdc.CliGuid = CliClient.Guid ")
                sb.AppendLine("LEFT OUTER JOIN CliClient Ccx ON CliClient.CcxGuid = Ccx.Guid ")
                sb.AppendLine("WHERE Ccx.Holding = '" & oContact.Guid.ToString & "' ")
        End Select


        sb.AppendLine("AND Pdc.Cod=" & CInt(oCod) & " ")
        sb.AppendLine("AND Pnc.Pn2<>0 ")
        sb.AppendLine("AND Pnc.ErrCod=" & DTOPurchaseOrderItem.ErrCods.Success & " ")
        sb.AppendLine("ORDER BY Pdc.fch DESC, Pdc.Pdc DESC, Pnc.Lin")
        Dim SQL As String = sb.ToString

        Dim oPurchaseOrder As New DTOPurchaseOrder

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .num = oDrd("Pdc")
                    .fch = oDrd("Fch")
                    .fchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("FchMin"))
                    .fchDeliveryMax = SQLHelper.GetFchFromDataReader(oDrd("FchMax"))
                    Select Case oCod
                        Case DTOPurchaseOrder.Codis.proveidor
                            .proveidor = DTOProveidor.FromContact(oContact)
                        Case Else
                            .customer = New DTOCustomer(oDrd("CliGuid"))
                            With .customer
                                .nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                                .nomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                                .Ref = SQLHelper.GetStringFromDataReader(oDrd("CliRef"))
                            End With
                    End Select


                    .concept = oDrd("Pdd")
                    .cod = oCod
                End With
            End If

            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With oSku
                If oSku.UnEquals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ReferenciaEspecial)) Then
                    .stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                    .clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                End If
            End With

            Dim item As New DTOPurchaseOrderItem(oDrd("Guid"))
            With item
                .purchaseOrder = oPurchaseOrder
                .sku = oSku
                .qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                .Pending = SQLHelper.GetIntegerFromDataReader(oDrd("Pn2"))

                Dim eur = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))
                Dim cur = SQLHelper.GetCurFromDataReader(oDrd("Cur"))
                Dim val = SQLHelper.GetDecimalFromDataReader(oDrd("Pts"))
                .Price = DTOAmt.Factory(eur, cur, val)

                .dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .ETD = SQLHelper.GetFchFromDataReader(oDrd("FchConfirm"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function PendingCcx(oCcx As DTOCustomer) As List(Of DTOPurchaseOrderItem) '-----DEPRECATED
        Dim retval As New List(Of DTOPurchaseOrderItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnc.Guid, Pdc.Guid AS PdcGuid, Pdc.CliGuid, Pdc.Fch, Pdc.Pdc, Pdc.Pdd, Pdc.FchMin, Pdc.FchMax ")
        sb.AppendLine(", Pnc.ArtGuid, Pnc.Qty, Pnc.Pn2, Pnc.Eur, Pnc.Dto ")
        sb.AppendLine(", CliClient.Ref AS CliRef, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON Pdc.CliGuid=CliClient.Guid ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON VwCcxOrMe.Guid = Pdc.CliGuid AND VwCcxOrMe.Ccx = '" & oCcx.Guid.ToString & "' ")
        sb.AppendLine("WHERE Pdc.Cod = 2 AND Pnc.Pn2<>0 ")
        sb.AppendLine("AND Pnc.ErrCod=" & DTOPurchaseOrderItem.ErrCods.Success & " ")
        sb.AppendLine("ORDER BY CliClient.Ref, CliGral.RaoSocial, CliGral.NomCom, Pdc.fch DESC, Pdc.Pdc, Pdc.Guid, Pnc.Lin")
        Dim SQL As String = sb.ToString

        Dim oPurchaseOrder As New DTOPurchaseOrder
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .num = oDrd("Pdc")
                    .fch = oDrd("Fch")
                    .concept = SQLHelper.GetStringFromDataReader(oDrd("Pdd"))
                    .fchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("FchMin"))
                    .fchDeliveryMax = SQLHelper.GetFchFromDataReader(oDrd("FchMax"))
                    .customer = New DTOCustomer(oDrd("CliGuid"))
                    With .customer
                        .nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                        .nomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                        .ref = SQLHelper.GetStringFromDataReader(oDrd("CliRef"))
                    End With
                    .concept = oDrd("Pdd")
                    .cod = DTOPurchaseOrder.Codis.client
                End With
            End If

            Dim item As New DTOPurchaseOrderItem(oDrd("Guid"))
            With item
                .purchaseOrder = oPurchaseOrder
                .sku = SQLHelper.GetProductFromDataReader(oDrd)
                .qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                .pending = SQLHelper.GetIntegerFromDataReader(oDrd("Pn2"))
                .price = SQLHelper.GetAmtFromDataReader(CDec(oDrd("Eur")))
                .dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Pending(oUser As DTOUser, oCod As DTOPurchaseOrder.Codis, oMgz As DTOMgz) As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Select Pnc.Guid, Pdc.Guid As PdcGuid, Pdc.Pdc, Pdc.Fch, Pdc.FchMin, Pdc.FchMax, Pdc.CliGuid, Pdc.Pdd, Pnc.Qty, Pnc.Pn2, Pnc.Eur, Pnc.Dto, Pnc.ArtGuid, VwSkuStocks.Stock, VwSkuPncs.Clients, VwSkuPncs.Pn1 ")
        sb.AppendLine(", CliGral.FullNom ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN CliGral On Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN Pnc On Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom On Pnc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuStocks ON Pnc.ArtGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuPncs ON Pnc.ArtGuid = VwSkuPncs.SkuGuid ")

        Select Case oUser.Rol.Id
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                sb.AppendLine("INNER JOIN Email_Clis On Pdc.CliGuid=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                sb.AppendLine("INNER JOIN Email_Clis On Pnc.RepGuid=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Manufacturer
                sb.AppendLine("INNER JOIN Email_Clis On VwSkuNom.Proveidor=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select

        sb.AppendLine("WHERE Pdc.cod=" & oCod & " ")
        sb.AppendLine("AND Pnc.Pn2>0 ")
        sb.AppendLine("AND Pnc.ErrCod=" & DTOPurchaseOrderItem.ErrCods.Success & " ")
        sb.AppendLine("ORDER BY CliGral.FullNom, Pdc.fch DESC, Pdc.Pdc DESC, Pnc.Lin")
        Dim SQL As String = sb.ToString

        Dim oContact As New DTOContact
        Dim oPurchaseOrder As New DTOPurchaseOrder

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            If Not oContact.Guid.Equals(oDrd("CliGuid")) Then
                oContact = New DTOContact(oDrd("CliGuid"))
                oContact.FullNom = oDrd("FullNom")
                oPurchaseOrder = New DTOPurchaseOrder
            End If

            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Num = oDrd("Pdc")
                    .Fch = oDrd("Fch")
                    .fchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("FchMin"))
                    .fchDeliveryMax = SQLHelper.GetFchFromDataReader(oDrd("FchMax"))
                    .concept = oDrd("Pdd")
                    .Cod = oCod
                    Select Case oCod
                        Case DTOPurchaseOrder.Codis.Proveidor
                            .Proveidor = DTOProveidor.FromContact(oContact)
                        Case Else
                            .Customer = DTOCustomer.FromContact(oContact)
                    End Select
                End With
            End If

            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With oSku
                If oSku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ReferenciaEspecial)) Then
                    'Stop
                Else
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                    .Clients = oDrd("Clients")
                    .Proveidors = oDrd("Pn1")
                End If
            End With

            Dim item As New DTOPurchaseOrderItem(oDrd("Guid"))
            With item
                .PurchaseOrder = oPurchaseOrder
                .Sku = oSku
                If oSku.UnEquals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ReferenciaEspecial)) Then
                    .Pending = oDrd("Pn2")
                End If
                .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                .Price = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Descuadres(oUser As DTOUser) As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnc.PdcGuid, Pdc.Pdc, Pdc.Fch, Pdc.CliGuid, CliGral.FullNom, Pdc.Cod ")
        sb.AppendLine(", Pnc.Guid, Pnc.Lin, Pnc.Qty, Pnc.Pn2 ")
        sb.AppendLine(", X.Sortides ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN CliGral On Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN Pnc On Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom On Pnc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT Arc.PncGuid, SUM(Qty) AS Sortides FROM Arc GROUP BY Arc.PncGuid) X ON Pnc.Guid = X.PncGuid ")
        sb.AppendLine("WHERE VwSkuNom.Emp=" & oUser.Emp.Id & " ")
        sb.AppendLine("AND Pnc.Pn2<>Pnc.Qty-(CASE WHEN X.Sortides IS NULL THEN 0 ELSE X.Sortides END) ")

        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts
            Case Else
                sb.AppendLine("AND Pdc.Cod = 2 ")
        End Select

        sb.AppendLine("ORDER BY Pdc.FchCreated DESC, Pnc.Lin")
        Dim SQL As String = sb.ToString

        Dim oCustomer As New DTOCustomer
        Dim oPurchaseOrder As New DTOPurchaseOrder

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            If Not oCustomer.Guid.Equals(oDrd("CliGuid")) Then
                oCustomer = New DTOCustomer(oDrd("CliGuid"))
                With oCustomer
                    .FullNom = oDrd("FullNom")
                End With
                oPurchaseOrder = New DTOPurchaseOrder
            End If

            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Num = oDrd("Pdc")
                    .Fch = oDrd("Fch")
                    .Cod = oDrd("Cod")
                    .Customer = oCustomer
                End With
            End If

            Dim item As New DTOPurchaseOrderItem(oDrd("Guid"))
            With item
                .PurchaseOrder = oPurchaseOrder
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                .Pending = oDrd("Pn2")
                .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                .DeliveredQty = SQLHelper.GetIntegerFromDataReader(oDrd("Sortides"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval

    End Function

    Shared Function PendentsDeLiquidacioRep(oEmp As DTOEmp) As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * FROM VwRepPncsLiqPending  ")
        sb.AppendLine("WHERE Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Year(Fch), PdcNum")

        Dim SQL As String = sb.ToString
        Dim oOrder As New DTOPurchaseOrder

        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oPdcGuid As Guid = oDrd("PdcGuid")
            If Not oPdcGuid.Equals(oOrder.Guid) Then
                Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))
                With oCustomer
                    .FullNom = oDrd("FullNom")
                    '.Emp = oEmp
                    .NoRep = oDrd("NoRep")
                    If Not IsDBNull(oDrd("CcxGuid")) Then
                        .Ccx = New DTOCustomer(oDrd("CcxGuid"))
                    End If
                    If Not IsDBNull(oDrd("ContactClass")) Then
                        .ContactClass = New DTOContactClass(oDrd("ContactClass"))
                        If Not IsDBNull(oDrd("DistributionChannel")) Then
                            .ContactClass.DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                        End If
                    End If
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                End With

                oOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oOrder
                    .Customer = oCustomer
                    '.Emp = oEmp
                    .Num = CInt(oDrd("PdcNum"))
                    .Fch = oDrd("Fch")
                End With
            End If

            Dim item As New DTOPurchaseOrderItem(oDrd("Guid"))
            With item
                .PurchaseOrder = oOrder
                .Lin = oDrd("Lin")
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                If IsDBNull(oDrd("RepGuid")) Then
                    If SQLHelper.GetBooleanFromDatareader(oDrd("RepCustom")) Then
                        .RepCom = New DTORepCom
                        .RepCom.RepCustom = True
                    End If
                Else
                    Dim oRep As New DTORep(oDrd("RepGuid"))
                    oRep.Nom = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                    Dim oRepCom As New DTORepCom
                    With oRepCom
                        .Rep = oRep
                        .Com = SQLHelper.GetDecimalFromDataReader(oDrd("Com"))
                        .RepCustom = SQLHelper.GetBooleanFromDatareader(oDrd("RepCustom"))
                    End With
                    .RepCom = oRepCom
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function RecuperaLiniesDeSortides(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Integer
        Dim retval As Integer
        PurchaseOrderLoader.Load(oPurchaseOrder)

        'posa-ho tot pendent com si no hagues sortit cap linia a compte d'aquesta comanda
        Dim items As List(Of DTOPurchaseOrderItem) = oPurchaseOrder.Items
        For Each item As DTOPurchaseOrderItem In items
            item.Pending = item.Qty
        Next

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PncGuid, SUM(Qty) AS Qty, Ln2, ArtGuid, MIN(EUR) AS EUR,MIN(CUR) As Cur ,MIN(PTS) AS PTS,MIN(DTO) AS DTO,MIN(RepGuid) AS RepGuid,MIN(COM) AS COM ")
        sb.AppendLine("FROM ARC WHERE PdcGuid='" & oPurchaseOrder.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY PncGuid, Ln2, ArtGuid ")
        sb.AppendLine("ORDER BY Ln2")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oPncGuid As Guid = oDrd("PncGuid")
            Dim item As DTOPurchaseOrderItem = items.Find(Function(x) x.Guid.Equals(oPncGuid))
            If item Is Nothing Then
                item = New DTOPurchaseOrderItem(oPncGuid)
                With item
                    .PurchaseOrder = oPurchaseOrder
                    .Lin = oDrd("Ln2")
                    .Sku = New DTOProductSku(oDrd("ArtGuid"))
                    .Qty = oDrd("Qty")
                    .Price = DTOAmt.Factory(oDrd("Eur"), oDrd("Cur"), oDrd("Pts"))
                    .Dto = oDrd("Dto")
                    If Not IsDBNull(oDrd("RepGuid")) Then
                        .RepCom = New DTORepCom()
                        With .RepCom
                            .Rep = New DTORep(oDrd("RepGuid"))
                            .Com = oDrd("Com")
                        End With
                    End If
                End With
                items.Add(item)
                retval += 1
            Else
                Dim iSortides As Integer = oDrd("Qty")
                If item.Qty < iSortides Then item.Qty = iSortides
                item.Pending = item.Qty - iSortides
            End If

        Loop
        oDrd.Close()

        Dim oSortedItems As List(Of DTOPurchaseOrderItem) = items.OrderBy(Function(x) x.Lin).ToList
        oPurchaseOrder.Items = oSortedItems

        PurchaseOrderLoader.Update(oPurchaseOrder, exs)
        Return retVal
    End Function

    Shared Function UpdateEtd(exs As List(Of Exception), items As List(Of DTOPurchaseOrderItem)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            For Each item As DTOPurchaseOrderItem In items
                Dim sb As New System.Text.StringBuilder
                If item.ETD = Nothing Then
                    sb.AppendLine("UPDATE Pnc SET FchConfirm=NULL ")
                Else
                    sb.AppendLine("UPDATE Pnc SET FchConfirm='" & Format(item.ETD, "yyyyMMdd") & "' ")
                End If
                sb.AppendLine("WHERE Guid='" & item.Guid.ToString & "' ")
                Dim SQL As String = sb.ToString
                SQLHelper.ExecuteNonQuery(SQL, oTrans)
            Next
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

    Shared Function Delete(items As List(Of DTOPurchaseOrderItem), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim itemsToDelete As List(Of DTOPurchaseOrderItem) = items.Where(Function(x) x.Pending = x.Qty).ToList
            Dim itemsToUpdate As List(Of DTOPurchaseOrderItem) = items.Where(Function(x) x.Pending <> x.Qty).ToList

            If itemsToDelete.Count > 0 Then
                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("DELETE Pnc ")
                sb.AppendLine("WHERE ( ")
                Dim firstrec As Boolean = True
                For Each item In itemsToDelete
                    If firstrec Then firstrec = False Else sb.Append("OR ")
                    sb.AppendLine("Guid = '" & item.Guid.ToString & "' ")
                Next
                sb.AppendLine(") ")
                Dim SQL As String = sb.ToString
                SQLHelper.ExecuteNonQuery(SQL, oTrans)
            End If

            For Each item In itemsToUpdate
                Dim SQL As String = "UPDATE Pnc SET Qty=Qty-Pn2, Pn2=0 WHERE Guid = '" & item.Guid.ToString & "' "
                SQLHelper.ExecuteNonQuery(SQL, oTrans)
            Next

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


    Shared Function Kpis(oEmp As DTOEmp, yearFrom As Integer) As List(Of DTOKpi)
        Dim oKpi1 = DTOKpi.Factory(DTOKpi.Ids.Comandes_Proveidors)
        Dim oKpi2 = DTOKpi.Factory(DTOKpi.Ids.Comandes_Clients)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT TOP 27 YEAR(Pdc.FchCreated) AS Year, MONTH(Pdc.FchCreated) AS Month ")
        sb.AppendLine(", SUM(CASE WHEN Pdc.Cod = 1 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS Input ")
        sb.AppendLine(", SUM(CASE WHEN Pdc.Cod = 2 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS Output ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE VwSkuNom.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND YEAR(Pdc.FchCreated) >= " & yearFrom & " ")
        sb.AppendLine("AND VwSkuNom.BrandGuid <> '" & DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Varios).Guid.ToString & "' ")
        sb.AppendLine("AND Pnc.ErrCod = " & DTOPurchaseOrderItem.ErrCods.Success & " ")
        sb.AppendLine("GROUP BY YEAR(Pdc.FchCreated), MONTH(Pdc.FchCreated) ")
        sb.AppendLine("ORDER BY YEAR(Pdc.FchCreated) DESC, MONTH(Pdc.FchCreated) DESC ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            oKpi1.YearMonths.Add(New DTOYearMonth(oDrd("Year"), oDrd("Month"), oDrd("Input")))
            oKpi2.YearMonths.Add(New DTOYearMonth(oDrd("Year"), oDrd("Month"), oDrd("Output")))
        Loop
        oDrd.Close()
        Dim retval As New List(Of DTOKpi)
        retval.Add(oKpi1)
        retval.Add(oKpi2)
        Return retval
    End Function


    'Search original Edi order message to get the product Ean  the customer asked for
    'since some customers use specific custom eans (Wells) and they want to be invoiced and labeled with them
    Shared Function PncCustomSkuEans(oPncGuids As List(Of Guid)) As List(Of DTOEdiSku)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Idx int NOT NULL")
        sb.AppendLine("	    , Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Idx,Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oPncGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("({0},'{1}') ", idx, oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("SELECT Pnc.Guid, EdiversaOrderItem.Ean, EdiversaOrderItem.RefClient ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN @Table X ON Pnc.Guid = X.Guid ")
        sb.AppendLine("INNER JOIN EdiversaOrderHeader ON Pnc.PdcGuid = EdiversaOrderHeader.Result ")
        sb.AppendLine("INNER JOIN EdiversaOrderItem ON EdiversaOrderHeader.Guid = EdiversaOrderItem.Parent AND Pnc.CustomLin = EdiversaOrderItem.Lin ")
        sb.AppendLine("ORDER BY X.Idx")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim retval As New List(Of DTOEdiSku)
        Do While oDrd.Read
            Dim item As New DTOEdiSku
            item.PncGuid = oDrd("Guid")
            item.Ean = SQLHelper.GetStringFromDataReader(oDrd("Ean"))
            item.Ref = SQLHelper.GetStringFromDataReader(oDrd("RefClient"))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
