Imports DTO.Integracions.Vivace

Public Class ShipmentsReportLoader



    Shared Function Deliveries(oShipmentsReportLog As DTOJsonLog) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Alb.Guid AS AlbGuid, Alb.alb, Alb.fch, Alb.CliGuid, CliGral.FullNom ")
        sb.AppendLine("FROM DeliveryShipment ")
        sb.AppendLine("INNER JOIN Alb ON Alb.Emp = DeliveryShipment.Emp AND Year(Alb.Fch) = CAST(SUBSTRING(DeliveryShipment.Delivery,1,4) AS Int) AND Alb.Alb = CAST(SUBSTRING(DeliveryShipment.Delivery,5,12) AS Int) ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE DeliveryShipment.[Log] = '" & oShipmentsReportLog.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Alb.Guid, Alb.Alb, Alb.Fch, Alb.CliGuid, CliGral.FullNom ")
        sb.AppendLine("ORDER BY Alb.Fch, Alb.Alb")
        Dim SQL As String = sb.ToString

        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDelivery As New DTODelivery(oDrd("AlbGuid"))
            With oDelivery
                .Id = oDrd("Alb")
                .Fch = oDrd("Fch")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
            End With
            retval.Add(oDelivery)
        Loop

        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(exs As List(Of Exception), oShipmentsReport As ShipmentsReport) As Boolean
        Dim retval As Boolean
        AllocateDeliveryLines(oShipmentsReport, exs)

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oShipmentsReport, oTrans)
            Update(oShipmentsReport, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As SqlException
            oTrans.Rollback()
            If ex.Number = 2627 Then
                exs.Add(New Exception("Duplicated record not registered"))
            Else
                exs.Add(ex)
            End If
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oShipmentsReport As ShipmentsReport, ByRef oTrans As SqlTransaction)


        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM DeliveryShipment ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each shipment In oShipmentsReport.shipments
            For Each item In shipment.items
                If item.lineQties IsNot Nothing Then
                    For Each line In item.lineQties
                        Dim oRow = oTb.NewRow
                        oTb.Rows.Add(oRow)
                        With oShipmentsReport
                            oRow("Guid") = Guid.NewGuid()
                            oRow("Emp") = CInt(DTOEmp.Ids.MatiasMasso)
                            oRow("Log") = SQLHelper.NullableBaseGuid(.Log)
                            oRow("Sender") = .sender
                            oRow("Fch") = .date
                        End With
                        With shipment
                            oRow("Expedition") = .expedition
                            oRow("Delivery") = .delivery
                            oRow("Packages") = .packages
                        End With
                        With item 'bulto/producto
                            oRow("Pallet") = .pallet
                            oRow("Package") = .package
                            oRow("SSCC") = .SSCC
                            oRow("Sku") = .sku
                            oRow("Qty") = line.qty
                            oRow("Line") = line.line
                        End With
                    Next
                End If
            Next
        Next

        oDA.Update(oDs)
    End Sub

    Shared Sub Delete(oShipmentsReport As ShipmentsReport, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder

        sb = New Text.StringBuilder()
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Sender VARCHAR(12) NOT NULL")
        sb.AppendLine("	    , Expedition VARCHAR(12) NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Sender, Expedition) ")

        Dim idx As Integer = 0
        For Each oShipment In oShipmentsReport.shipments
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}','{1}') ", oShipmentsReport.sender.Trim(), oShipment.expedition.Trim())
            idx += 1
        Next
        sb.AppendLine()

        sb.AppendLine("DELETE DeliveryShipment ")
        sb.AppendLine("FROM DeliveryShipment ")
        sb.AppendLine("INNER JOIN @Table X ON DeliveryShipment.Sender = X.Sender AND DeliveryShipment.Expedition = X.Expedition ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub



    Shared Sub AllocateDeliveryLines(oShipmentsReport As DTO.Integracions.Vivace.ShipmentsReport, exs As List(Of Exception))
        Dim oDeliveriesLineQties = DeliveriesLineQties(oShipmentsReport.shipments)
        For Each oShipment In oShipmentsReport.shipments
            Dim oShipmentLineQties = oDeliveriesLineQties.Where(Function(x) x.Delivery = oShipment.delivery).ToList()
            AllocateDeliveryLines(oShipment, oShipmentLineQties, exs)
        Next
    End Sub

    Shared Function AllocateDeliveryLines(oShipment As DTO.Integracions.Vivace.ShipmentsReport.Shipment, oDeliveryLineQties As IEnumerable(Of ShipmentsReport.AlbLineQty), exs As List(Of Exception)) As Boolean
        'allocates each units of sku in package to the delivery line it belongs
        Dim deliveryCheckSum = oDeliveryLineQties.Sum(Function(x) x.qty)

        'set a copy to prevent modifying the original delivery line/qties
        Dim oRemainingDeliveryLineQties As New List(Of ShipmentsReport.AlbLineQty)
        For Each oDeliveryLineQty In oDeliveryLineQties
            Dim o = New ShipmentsReport.AlbLineQty()
            o.line = oDeliveryLineQty.line
            o.qty = oDeliveryLineQty.qty
            oRemainingDeliveryLineQties.Add(o)
        Next

        Dim shippedExceedLines As New List(Of Integer)

        For Each packageSku In oShipment.items

            Dim packageSkuQty = packageSku.qty
            packageSku.lineQties = New List(Of DTO.Integracions.Vivace.ShipmentsReport.LineQty)
            If packageSku.lines IsNot Nothing Then

                'reportar error s'estan empaquetant mes unitats de les declarades en la linia line
                Dim remainingQty = oRemainingDeliveryLineQties.Where(Function(x) packageSku.lines.Any(Function(y) y = x.line)).Sum(Function(x) x.qty)
                If remainingQty < packageSku.qty Then shippedExceedLines.AddRange(packageSku.lines)

                For Each line In packageSku.lines
                    Dim oRemainingDeliveryLineQty = oRemainingDeliveryLineQties.FirstOrDefault(Function(x) x.line = line)
                    If oRemainingDeliveryLineQty IsNot Nothing AndAlso oRemainingDeliveryLineQty.qty > 0 Then
                        Dim oLineQty As New DTO.Integracions.Vivace.ShipmentsReport.LineQty
                        oLineQty.line = line
                        'If oRemainingDeliveryLineQty.qty < packageSku.qty Then shippedExceedLines.Add(line)
                        oLineQty.qty = Math.Min(packageSkuQty, oRemainingDeliveryLineQty.qty)
                        If oLineQty.qty <> 0 Then packageSku.lineQties.Add(oLineQty)
                        oRemainingDeliveryLineQty.qty -= oLineQty.qty
                        packageSkuQty -= oLineQty.qty
                    End If
                Next
            End If
        Next

        Dim sbErr As New Text.StringBuilder
        If shippedExceedLines.Count > 0 Then
            exs.Add(New Exception("S'ha enviat mes producte del declarat a les següents linies:"))
            For Each line In shippedExceedLines
                Try
                    Dim oDeliveryLine = oDeliveryLineQties.FirstOrDefault(Function(x) x.line = line)
                    Dim oPackages = oShipment.items.Where(Function(x) x.lines.Contains(line)).ToList()
                    Dim sku = oPackages.FirstOrDefault().sku
                    Dim oPackagedQty = oPackages.Sum(Function(x) x.qty)
                    Dim oBultos = oPackages.Select(Function(x) String.Format("bulto {0} unitats {1}", x.package, x.qty))
                    Dim demanat = If(oDeliveryLine Is Nothing, 0, oDeliveryLine.qty)
                    Dim report = String.Format("albarà:{0} linia:{1} producte:{2} demanat:{3} sortit:{4} a {5}", oShipment.delivery, line, sku, demanat, oPackagedQty, String.Join(", ", oBultos))
                    exs.Add(New Exception(report))

                Catch ex As Exception

                End Try
            Next
        End If

        If oRemainingDeliveryLineQties.Any(Function(x) x.qty > 0) Then
            exs.Add(New Exception("S'ha enviat menys producte del declarat a les següents linies:"))
            For Each oLineQty In oRemainingDeliveryLineQties
                Dim oDeliveryLine = oDeliveryLineQties.FirstOrDefault(Function(x) x.line = oLineQty.line)
                Dim oPackages = oShipment.items.Where(Function(x) x.lines.Any(Function(y) y = oLineQty.line)).ToList()
                If oPackages.Count > 0 Then
                    Dim sku = oPackages.FirstOrDefault().sku
                    Dim iShippedQty = oPackages.SelectMany(Function(x) x.lineQties).Where(Function(y) y.line = oLineQty.line).Sum(Function(x) x.qty)
                    If oDeliveryLine.qty <> iShippedQty Then
                        Dim oBultos As New List(Of String)
                        For Each oPackage In oPackages
                            oBultos.Add(oPackage.lineQties.Where(Function(x) x.line = oLineQty.line).Select(Function(y) String.Format("bulto {0} SSCC {1} unitats {2}", oPackage.package, oPackage.SSCC, y.qty)).FirstOrDefault)
                        Next
                        Dim report As String
                        If oBultos.Count = 0 Then
                            report = String.Format("albarà:{0} linia:{1} producte:{2} demanat:{3} no hi surt a cap bulto", oShipment.delivery, oLineQty.line, sku, oDeliveryLine.qty)
                        Else
                            report = String.Format("albarà:{0} linia:{1} producte: {2} demanat: {3} sortit: {4} a {5}", oShipment.delivery, oLineQty.line, sku, oDeliveryLine.qty, iShippedQty, String.Join(", ", oBultos))
                        End If
                        exs.Add(New Exception(report))

                    End If
                End If
            Next
        End If

        If oShipment.items.Any(Function(x) x.lines Is Nothing) Then
            exs.Add(New Exception("Els següents bultos contenen mercancia sense asignar a cap linia de l'albarà:"))
            Dim oPackages = oShipment.items.Where(Function(x) x.lines Is Nothing).ToList()
            For Each oPackage In oPackages
                Dim report = String.Format("albarà:{0} bulto:{1} producte:{2} sortit:{3}", oShipment.delivery, oPackage.package, oPackage.sku, oPackage.qty)
                exs.Add(New Exception(report))
            Next
        End If

        Dim packagesCheckSum = oShipment.items.Sum(Function(x) x.qty)
        If packagesCheckSum <> deliveryCheckSum Then
            Dim oCsvShipment As New DTOCsv
            Dim oRow = oCsvShipment.AddRow()
            oRow.AddCell("package")
            oRow.AddCell("pallet SSCC")
            oRow.AddCell("package SSCC")
            oRow.AddCell("sku")
            oRow.AddCell("qty")
            oRow.AddCell("lines")
            oRow.AddCell("lineQties")
            For Each item In oShipment.items
                Try
                    oRow = oCsvShipment.AddRow()
                    oRow.AddCell(item.package)
                    oRow.AddCell(item.pallet)
                    oRow.AddCell(item.SSCC)
                    oRow.AddCell(item.sku)
                    oRow.AddCell(item.qty)
                    If item.lines IsNot Nothing Then
                        oRow.AddCell(String.Join("-", item.lines))
                    End If
                    If item.lineQties IsNot Nothing Then
                        oRow.AddCell(String.Join("-", item.lineQties.Select(Function(x) String.Format("line:{0} qty:{1}", x.line, x.qty))))
                    End If

                Catch ex As Exception
                End Try
            Next
            Dim sCsvShipment = oCsvShipment.ToString()

            exs.Add(New Exception(String.Format("albarà {0} amb {1} unitats pero bultos amb {2} unitats", oShipment.delivery, deliveryCheckSum, packagesCheckSum)))
        End If

        Return exs.Count = 0
    End Function

    Shared Function DeliveriesLineQties(oShipments As List(Of ShipmentsReport.Shipment)) As List(Of ShipmentsReport.AlbLineQty)
        Dim retval As New List(Of ShipmentsReport.AlbLineQty)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Declare @Table TABLE( ")
        sb.AppendLine("	      Delivery VARCHAR(12) Not NULL")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Delivery) ")

        Dim idx As Integer = 0
        For Each oShipment In oShipments
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oShipment.delivery)
            idx += 1
        Next

        sb.AppendLine("SELECT X.Delivery, Arc.Lin, Arc.Qty ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid AND (Arc.Bundle IS NULL OR Arc.Guid = Arc.Bundle) ")
        sb.AppendLine("INNER JOIN @Table X ")
        sb.AppendLine("ON Alb.Emp = " & CInt(DTOEmp.Ids.MatiasMasso).ToString() & " ")
        sb.AppendLine("AND Year(Alb.Fch) = CAST(SUBSTRING(X.Delivery,1,4) AS Int)  ")
        sb.AppendLine("AND Alb.Alb = CAST(SUBSTRING(X.Delivery,5,12) AS Int) ")
        sb.AppendLine("INNER JOIN Art ON Arc.ArtGuid = Art.Guid AND Art.NoStk = 0 ")
        sb.AppendLine("ORDER BY X.Delivery, Arc.Lin")
        Dim SQL As String = sb.ToString

        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New ShipmentsReport.AlbLineQty
            item.Delivery = oDrd("Delivery")
            item.line = oDrd("Lin")
            item.qty = oDrd("Qty")
            retval.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function
End Class

Public Class ShipmentsReportsLoader


    Shared Function Rebuild(exs As List(Of Exception)) 'for testController
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            SQLHelper.ExecuteNonQuery("DELETE DeliveryShipment", oTrans)
            Dim oAllJson = JsonLogsLoader.All("shipments", True)
            For Each oJson In oAllJson
                Dim oObj = JsonHelper.DeSerialize(Of ShipmentsReport)(oJson.Json, exs)

                Dim oShipmentsReport As DTO.Integracions.Vivace.ShipmentsReport = oObj
                oShipmentsReport.Log = oJson
                If Not ShipmentsReportLoader.Update(exs, oShipmentsReport) Then
                    'Stop
                End If
            Next
            oTrans.Commit()
            retval = True
        Catch ex As SqlException
            oTrans.Rollback()
            If ex.Number = 2627 Then
                exs.Add(New Exception("Duplicated record not registered"))
            Else
                exs.Add(ex)
            End If
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval


    End Function
End Class