Public Class HoldingLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOHolding
        Dim retval As DTOHolding = Nothing
        Dim oHolding As New DTOHolding(oGuid)
        If Load(oHolding) Then
            retval = oHolding
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oHolding As DTOHolding) As Boolean
        If Not oHolding.IsLoaded And Not oHolding.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Holding.Emp, Holding.Nom ")
            sb.AppendLine(", CliGral.Guid AS CustomerGuid, CliGral.FullNom AS CustomerFullNom ")
            sb.AppendLine("FROM Holding ")
            sb.AppendLine("LEFT OUTER JOIN CliClient ON Holding.Guid = CliClient.Holding ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON CliClient.Guid = CliGral.Guid ")
            sb.AppendLine("WHERE Holding.Guid='" & oHolding.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                With oHolding
                    If Not oHolding.IsLoaded Then
                        .emp = New DTOEmp(oDrd("Emp"))
                        .Nom = oDrd("Nom")
                        .IsLoaded = True
                    End If
                    If Not IsDBNull(oDrd("CustomerGuid")) Then
                        Dim oCustomer As New DTOCustomer(oDrd("CustomerGuid"))
                        oCustomer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("CustomerFullNom"))
                        .Companies.Add(oCustomer)
                    End If
                End With
            Loop

            oDrd.Close()

            oHolding.Clusters = Clusters(oHolding)
        End If

        Dim retval As Boolean = oHolding.IsLoaded
        Return retval
    End Function

    Shared Function Update(oHolding As DTOHolding, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oHolding, oTrans)
            oTrans.Commit()
            oHolding.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oHolding As DTOHolding, ByRef oTrans As SqlTransaction)
        UpdateHeader(oHolding, oTrans)
        UpdateItems(oHolding, oTrans)
    End Sub

    Shared Sub UpdateHeader(oHolding As DTOHolding, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Holding ")
        sb.AppendLine("WHERE Guid='" & oHolding.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oHolding.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oHolding
            oRow("Emp") = .Emp.Id
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oHolding As DTOHolding, ByRef oTrans As SqlTransaction)
        ClearItems(oHolding, oTrans)
        For Each item In oHolding.Companies
            Dim SQL As String = "UPDATE CliClient SET Holding='" & oHolding.Guid.ToString & "' WHERE Guid='" & item.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Next
    End Sub

    Shared Function Delete(oHolding As DTOHolding, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oHolding, oTrans)
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


    Shared Sub Delete(oHolding As DTOHolding, ByRef oTrans As SqlTransaction)
        ClearItems(oHolding, oTrans)
        DeleteHeader(oHolding, oTrans)
    End Sub

    Shared Sub DeleteHeader(oHolding As DTOHolding, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Holding WHERE Guid='" & oHolding.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub ClearItems(oHolding As DTOHolding, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE CliClient SET Holding=NULL WHERE Holding='" & oHolding.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Clusters(oHolding As DTOHolding) As List(Of DTOCustomerCluster)
        Dim retval As New List(Of DTOCustomerCluster)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CustomerCluster.Guid AS ClusterGuid, CustomerCluster.Nom AS ClusterNom, CliGral.FullNom, CliClient.Guid AS SalePointGuid, CliClient.Ref AS SalePointNom ")
        sb.AppendLine("FROM CliClient ")
        sb.AppendLine("LEFT OUTER JOIN CliClient Ccx ON CliClient.CcxGuid = Ccx.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CustomerCluster ON CliClient.CustomerCluster = CustomerCluster.Guid And CustomerCluster.Holding = Ccx.Holding ")
        sb.AppendLine("INNER JOIN CliGral ON CliClient.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE Ccx.Holding = '" & oHolding.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ClusterNom, ClusterGuid, SalePointNom, CliGral.FullNom ")
        Dim SQL As String = sb.ToString

        Dim oCluster As New DTOCustomerCluster()
        Dim oNoCluster As New DTOCustomerCluster(Guid.Empty)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If IsDBNull(oDrd("ClusterGuid")) Then
                oCluster = oNoCluster
            ElseIf Not oCluster.Guid.Equals(oDrd("ClusterGuid")) Then
                oCluster = New DTOCustomerCluster(oDrd("ClusterGuid"))
                oCluster.Nom = oDrd("ClusterNom")
                retval.Add(oCluster)
            End If
            Dim item As New DTOCustomer(oDrd("SalePointGuid"))
            With item
                If IsDBNull(oDrd("SalePointNom")) OrElse oDrd("SalePointNom") = "" Then
                    .Ref = oDrd("FullNom")
                Else
                    .Ref = oDrd("SalePointNom")
                End If
            End With
            oCluster.Customers.Add(item)
        Loop
        oDrd.Close()

        If oNoCluster.Customers.Count > 0 Then
            oNoCluster.Nom = "(sense classificar)"
            retval.Add(oNoCluster)
        End If
        Return retval

    End Function
#End Region

    Shared Function PendingPurchaseOrders(oHolding As DTOHolding) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Guid AS PdcGuid, Pdc.Pdc, Pdc.Fch, Pdc.FchMin, Pdc.Pdd ")
        sb.AppendLine(", Pdc.CliGuid, CliClient.Ref, VwHoldingCustomRefs.CustomRef ")
        sb.AppendLine(", Pnc.Guid AS PncGuid, VwSkuNom.*, Pnc.Pn2, Pnc.Eur, Pnc.Dto ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN CliClient ON Pdc.CliGuid = CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliClient Ccx ON CliClient.CcxGuid = Ccx.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwHoldingCustomRefs ON VwSkuNom.SkuGuid = VwHoldingCustomRefs.SkuGuid AND Ccx.Holding = VwHoldingCustomRefs.Holding ")
        sb.AppendLine("WHERE Ccx.Holding = '" & oHolding.Guid.ToString & "' ")
        sb.AppendLine("AND Pdc.Cod = 2 AND Pnc.Pn2 > 0 ")
        sb.AppendLine("ORDER BY FchMin, Pdc.Fch, Pdc.Pdc, Pnc.Lin ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oOrder As New DTOPurchaseOrder
        Do While oDrd.Read
            If Not oOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oOrder
                    .num = oDrd("Pdc")
                    .fch = oDrd("Fch")
                    .fchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("FchMin"))
                    .concept = SQLHelper.GetStringFromDataReader(oDrd("Pdd"))
                    .customer = New DTOCustomer(oDrd("CliGuid"))
                    With .customer
                        .Ref = SQLHelper.GetStringFromDataReader("Ref") & " " & SQLHelper.GetStringFromDataReader("CustomRef")
                    End With
                    retval.Add(oOrder)
                End With
            End If

            Dim item As New DTOPurchaseOrderItem(oDrd("PncGuid"))
            With item
                .sku = SQLHelper.GetProductFromDataReader(oDrd)
                .pending = oDrd("Pn2")
                .price = DTOAmt.Factory(oDrd("Eur"))
                .dto = oDrd("Dto")
            End With
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function PendingExcel(oHolding As DTOHolding) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("M+O Pendiente de entrega", "M+O Pendiente de entrega.xlsx")
        With retval
            .addColumn("n/ref.pedido")
            .addColumn("fecha", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .addColumn("punto de venta")
            .addColumn("s/ref.pedido")
            .addColumn("entrega", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .addColumn("producto")
            .addColumn("ref.cliente")
            .addColumn("unidades", MatHelper.Excel.Cell.NumberFormats.Integer)
        End With
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Pdc, Pdc.Fch, CliClient.Ref, Pdc.Pdd, Pdc.fchMin, VwSkuNom.SkuNomLlarg, VwHoldingCustomRefs.CustomRef, Pnc.Pn2 ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN CliClient ON Pdc.CliGuid = CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliClient Ccx ON CliClient.CcxGuid = Ccx.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwHoldingCustomRefs ON VwSkuNom.SkuGuid = VwHoldingCustomRefs.SkuGuid AND Ccx.Holding = VwHoldingCustomRefs.Holding ")
        sb.AppendLine("WHERE Ccx.Holding = '" & oHolding.Guid.ToString & "' ")
        sb.AppendLine("AND Pdc.Cod = 2 AND Pnc.Pn2>0 ")
        sb.AppendLine("ORDER BY FchMin, Pdc.Fch, Pdc.Pdc, Pnc.Lin ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRow = retval.addRow
            oRow.addCell(oDrd("Pdc"))
            oRow.addCell(oDrd("Fch"))
            oRow.addCell(oDrd("Ref"))
            oRow.addCell(oDrd("Pdd"))
            oRow.addCell(oDrd("fchMin"))
            oRow.addCell(oDrd("SkuNomLlarg"))
            oRow.addCell(oDrd("CustomRef"))
            oRow.addCell(oDrd("Pn2"))
        Loop
        oDrd.Close()
        For i = retval.columns.Count - 1 To 0 Step -1
            Dim colIdx As Integer = i
            Dim values = retval.rows.Where(Function(x) Not x.Equals(retval.rows.First)).Select(Function(y) y.cells(colIdx).content).ToList
            If values.All(Function(x) IsDBNull(x)) Then
                retval.columns.RemoveAt(i)
                For Each oRow In retval.rows
                    oRow.cells.RemoveAt(i)
                Next
            End If
        Next

        Return retval
    End Function


    Shared Function ComandesECIDuplicades() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT COUNT(distinct Pdc.Guid) as PdcCount, max(pdc) as lastPdc, max(fch) as lastFch, pdd from pdc ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON Pdc.CliGuid = VwCcxOrMe.Guid ")
        sb.AppendLine("INNER JOIN CliClient ON VwCcxOrMe.Ccx = CliClient.Guid ")
        sb.AppendLine("INNER JOIN Holding ON CliClient.Holding= Holding.Guid ")
        sb.AppendLine("WHERE PDD LIKE '%/prov.01-030825' AND YEAR(Pdc.Fch)>2020 ")
        sb.AppendLine("group by Holding.Nom, pdd ")
        sb.AppendLine("HAVING count(distinct Pdc.Guid) >1 ")
        sb.AppendLine("order by max(Pdc.FchCreated) DESC ")
        Dim SQL As String = sb.ToString
        sb = New Text.StringBuilder
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            sb.Append(String.Format("El pedido {0} ({1}) del {2:dd/MM/yy} está {3} veces<br/>", oDrd("LastPdc"), oDrd("Pdd"), oDrd("LastFch"), oDrd("PdcCount")))
        Loop
        oDrd.Close()
        Return sb.ToString
    End Function
End Class

Public Class HoldingsLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOHolding)
        Dim retval As New List(Of DTOHolding)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Nom ")
        sb.AppendLine("FROM Holding ")
        sb.AppendLine("WHERE Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOHolding(oDrd("Guid"))
            With item
                .Emp = oEmp.Trimmed
                .Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

