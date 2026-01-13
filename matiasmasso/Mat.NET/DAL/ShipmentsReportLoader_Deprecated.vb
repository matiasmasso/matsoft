Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports DTO.Integracions.Vivace

Public Class ShipmentsReportLoader_Deprecated


    Shared Function Merge(exs As List(Of Exception), ByRef oShipmentsReport As DTO.Integracions.Vivace.ShipmentsReport_Deprecated) As Boolean ' TO DEPRECATE
        Dim sDeliveryIds = oShipmentsReport.shipments.Select(Function(x) x.delivery).ToList()
        If sDeliveryIds.Count > 0 Then

            Dim sb As New System.Text.StringBuilder

            sb = New Text.StringBuilder()
            sb.AppendLine("DECLARE @Table TABLE( ")
            sb.AppendLine("	      Yea int NOT NULL")
            sb.AppendLine("	    , Alb int NOT NULL ")
            sb.AppendLine("        ) ")
            sb.AppendLine("INSERT INTO @Table(Yea, Alb) ")

            Dim idx As Integer = 0
            For Each sDeliveryId In sDeliveryIds
                sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
                sb.AppendFormat("({0},{1}) ", DTODelivery.YearFromFormattedId(sDeliveryId), DTODelivery.IdFromFormattedId(sDeliveryId))
                idx += 1
            Next
            sb.AppendLine()

            sb.AppendLine("SELECT Arc.Guid, Arc.Lin, Arc.Qty ")
            sb.AppendLine(", Arc.AlbGuid, Alb.Fch, Alb.Alb ")
            sb.AppendLine(", Arc.ArtGuid, VwSkuNom.SkuId, VwSkuNom.SkuNomLlargEsp ")
            sb.AppendLine("FROM Alb ")
            sb.AppendLine("INNER JOIN Arc ON Alb.Guid = Arc.AlbGuid ")
            sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid = VwSkuNom.SkuGuid ")
            sb.AppendLine("INNER JOIN @Table X ON Alb.Yea = X.Yea AND Alb.Alb = X.Alb ")
            sb.AppendLine("WHERE Alb.Emp = " & CInt(DTOEmp.Ids.MatiasMasso) & " ")
            sb.AppendLine("ORDER BY X.Yea, X.Alb, Arc.Lin")

            Dim SQL = sb.ToString
            Dim oDrd = SQLHelper.GetDataReader(SQL)
            Dim oDeliveries As New List(Of DTODelivery)
            Dim oDelivery As New DTODelivery
            Do While oDrd.Read
                If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                    oDelivery = New DTODelivery(oDrd("AlbGuid"))
                    With oDelivery
                        .Id = oDrd("Alb")
                        .Fch = oDrd("Fch")
                        .Items = New List(Of DTODeliveryItem)
                    End With
                    oDeliveries.Add(oDelivery)
                End If
                Dim oItem As New DTODeliveryItem(oDrd("Guid"))
                With oItem
                    .qty = oDrd("Qty")
                    .lin = oDrd("Lin")
                    .sku = New DTOProductSku(oDrd("ArtGuid"))
                    With .sku
                        .Id = oDrd("SkuId")
                        .NomLlarg.Esp = oDrd("SkuNomLlargEsp")
                    End With
                    oDelivery.Items.Add(oItem)
                End With
            Loop
            oDrd.Close()

            For Each oShipment In oShipmentsReport.shipments
                Dim year = DTODelivery.YearFromFormattedId(oShipment.delivery)
                Dim id = DTODelivery.IdFromFormattedId(oShipment.delivery)
                oDelivery = oDeliveries.FirstOrDefault(Function(x) x.Fch.Year = year And x.Id = id)
                If oDelivery Is Nothing Then
                    exs.Add(New Exception(String.Format("Albarà {0} no registrat", oShipment.delivery)))
                Else
                    'per cada bulto de l'enviament 
                    For Each sItem In oShipment.items

                        'crea una nova llista linia/producte/quantitat on assignar les linies de l'albarà a que es refereix
                        sItem.DeliveryItems = New List(Of DTODeliveryItem)

                        'quantitat del producte a repartir entre les diferents linies embalades en aquest bulto
                        Dim sItemQty = sItem.qty

                        If sItem.lines Is Nothing Then
                            exs.Add(New Exception(String.Format("linea sin especificar en SSCC {0}", sItem.SSCC)))
                        Else
                            'per cada linia de aquest producte en aquest bulto
                            For Each iLine In sItem.lines

                                'troba la linia corresponent de l'albará
                                Dim oArc = oDelivery.Items.FirstOrDefault(Function(x) x.lin = iLine)
                                If oArc Is Nothing Then
                                    exs.Add(New Exception(String.Format("L'Albarà {0} no te cap linia {1}", oShipment.delivery, iLine)))
                                ElseIf oArc.sku.Id <> sItem.sku Then
                                    exs.Add(New Exception(String.Format("La linia {0} de l'albarà {1} correspon al producte {2} {3} i no al {4}", iLine, oShipment.delivery, iLine, oArc.sku.Id, sItem.sku)))
                                Else
                                    'crea el nou element linia/producte
                                    Dim dItem = New DTODeliveryItem(oArc.Guid)
                                    dItem.sku = oArc.sku
                                    dItem.lin = iLine

                                    'assigna-li la quantitat d'aquesta linia que va en el bulto
                                    If sItemQty >= oArc.qty Then
                                        dItem.qty = oArc.qty 'assigna totes les unitats de la linia a aquest bulto
                                        oArc.qty -= dItem.qty 'resta-les a la linia de l'albarà
                                        sItemQty -= dItem.qty 'resta-les de les que queden per assignar a la resta de linies
                                    Else
                                        dItem.qty = sItemQty 'omple el bulto amb part de la linia
                                        oArc.qty -= dItem.qty 'resta-les a la linia de l'albarà
                                        sItemQty = 0 'ja hem assignat totes les unitats del bulto
                                    End If

                                    'afegeix l'element al bulto
                                    sItem.DeliveryItems.Add(dItem)
                                End If

                            Next
                        End If
                    Next
                End If
            Next

        End If
        Return exs.Count = 0
    End Function


    Shared Function Shipments(exs As List(Of Exception), ByRef oShipmentsReport As DTO.Integracions.Vivace.ShipmentsReport_Deprecated) As DTODelivery.Shipment.Collection
        Dim retval As New DTODelivery.Shipment.Collection
        Dim sDeliveryIds = oShipmentsReport.shipments.Select(Function(x) x.delivery).ToList()
        If sDeliveryIds.Count > 0 Then

            Dim sb As New System.Text.StringBuilder

            sb = New Text.StringBuilder()
            sb.AppendLine("DECLARE @Table TABLE( ")
            sb.AppendLine("	      Yea int NOT NULL")
            sb.AppendLine("	    , Alb int NOT NULL ")
            sb.AppendLine("        ) ")
            sb.AppendLine("INSERT INTO @Table(Yea, Alb) ")

            Dim idx As Integer = 0
            For Each sDeliveryId In sDeliveryIds
                sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
                sb.AppendFormat("({0},{1}) ", DTODelivery.YearFromFormattedId(sDeliveryId), DTODelivery.IdFromFormattedId(sDeliveryId))
                idx += 1
            Next
            sb.AppendLine()

            sb.AppendLine("SELECT Arc.Guid, Arc.Lin, Arc.Qty ")
            sb.AppendLine(", Arc.AlbGuid, Alb.Fch, Alb.Alb ")
            sb.AppendLine(", Arc.ArtGuid, VwSkuNom.SkuId, VwSkuNom.SkuNomLlargEsp ")
            sb.AppendLine("FROM Alb ")
            sb.AppendLine("INNER JOIN Arc ON Alb.Guid = Arc.AlbGuid ")
            sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid = VwSkuNom.SkuGuid ")
            sb.AppendLine("INNER JOIN @Table X ON Alb.Yea = X.Yea AND Alb.Alb = X.Alb ")
            sb.AppendLine("WHERE Alb.Emp = " & CInt(DTOEmp.Ids.MatiasMasso) & " ")
            sb.AppendLine("ORDER BY X.Yea, X.Alb, Arc.Lin")

            Dim SQL = sb.ToString
            Dim oDrd = SQLHelper.GetDataReader(SQL)
            Dim oDeliveries As New List(Of DTODelivery)
            Dim oDelivery As New DTODelivery
            Do While oDrd.Read
                If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                    oDelivery = New DTODelivery(oDrd("AlbGuid"))
                    With oDelivery
                        .Id = oDrd("Alb")
                        .Fch = oDrd("Fch")
                        .Items = New List(Of DTODeliveryItem)
                    End With
                    oDeliveries.Add(oDelivery)
                End If
                Dim oItem As New DTODeliveryItem(oDrd("Guid"))
                With oItem
                    .qty = oDrd("Qty")
                    .lin = oDrd("Lin")
                    .sku = New DTOProductSku(oDrd("ArtGuid"))
                    With .sku
                        .Id = oDrd("SkuId")
                        .NomLlarg.Esp = oDrd("SkuNomLlargEsp")
                    End With
                    oDelivery.Items.Add(oItem)
                End With
            Loop
            oDrd.Close()

            Dim oShipments = DTO.DTODelivery.Shipment.Collection.Factory(oShipmentsReport, oDeliveries)

            For Each oShipment In oShipmentsReport.shipments
                Dim year = DTODelivery.YearFromFormattedId(oShipment.delivery)
                Dim id = DTODelivery.IdFromFormattedId(oShipment.delivery)
                oDelivery = oDeliveries.FirstOrDefault(Function(x) x.Fch.Year = year And x.Id = id)
                If oDelivery Is Nothing Then
                    exs.Add(New Exception(String.Format("Albarà {0} no registrat", oShipment.delivery)))
                Else
                    Dim oShipment = DTODelivery.Shipment.Factory(oDelivery)
                    'per cada bulto de l'enviament 
                    For Each sItem In oShipment.items

                        'crea una nova llista linia/producte/quantitat on assignar les linies de l'albarà a que es refereix
                        sItem.DeliveryItems = New List(Of DTODeliveryItem)

                        'quantitat del producte a repartir entre les diferents linies embalades en aquest bulto
                        Dim sItemQty = sItem.qty

                        If sItem.lines Is Nothing Then
                            exs.Add(New Exception(String.Format("linea sin especificar en SSCC {0}", sItem.SSCC)))
                        Else
                            'per cada linia de aquest producte en aquest bulto
                            For Each iLine In sItem.lines

                                'troba la linia corresponent de l'albará
                                Dim oArc = oDelivery.Items.FirstOrDefault(Function(x) x.lin = iLine)
                                If oArc Is Nothing Then
                                    exs.Add(New Exception(String.Format("L'Albarà {0} no te cap linia {1}", oShipment.delivery, iLine)))
                                ElseIf oArc.sku.Id <> sItem.sku Then
                                    exs.Add(New Exception(String.Format("La linia {0} de l'albarà {1} correspon al producte {2} {3} i no al {4}", iLine, oShipment.delivery, iLine, oArc.sku.Id, sItem.sku)))
                                Else
                                    'crea el nou element linia/producte
                                    Dim dItem = New DTODeliveryItem(oArc.Guid)
                                    dItem.sku = oArc.sku
                                    dItem.lin = iLine

                                    'assigna-li la quantitat d'aquesta linia que va en el bulto
                                    If sItemQty >= oArc.qty Then
                                        dItem.qty = oArc.qty 'assigna totes les unitats de la linia a aquest bulto
                                        oArc.qty -= dItem.qty 'resta-les a la linia de l'albarà
                                        sItemQty -= dItem.qty 'resta-les de les que queden per assignar a la resta de linies
                                    Else
                                        dItem.qty = sItemQty 'omple el bulto amb part de la linia
                                        oArc.qty -= dItem.qty 'resta-les a la linia de l'albarà
                                        sItemQty = 0 'ja hem assignat totes les unitats del bulto
                                    End If

                                    'afegeix l'element al bulto
                                    sItem.DeliveryItems.Add(dItem)
                                End If

                            Next
                        End If
                    Next
                End If
            Next

        End If
        Return exs.Count = 0
    End Function





    Shared Function Update(exs As List(Of Exception), oShipmentsReport As ShipmentsReport_Deprecated) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

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


    Shared Sub Update(oShipmentsReport As ShipmentsReport_Deprecated, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ShipmentsReport ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oShipment In oShipmentsReport.shipments
            For Each item In oShipment.items
                For Each linia In item.DeliveryItems
                    Dim oRow = oTb.NewRow
                    oTb.Rows.Add(oRow)
                    With oShipmentsReport
                        oRow("Log") = SQLHelper.NullableBaseGuid(.Log)
                        oRow("Fch") = .date
                    End With
                    With oShipment
                        oRow("Expedition") = .expedition
                        oRow("Delivery") = .delivery
                    End With
                    With item 'bulto/producto
                        oRow("SSCC") = .SSCC
                        oRow("Pallet") = .pallet
                        oRow("Sku") = .sku
                    End With
                    With linia
                        oRow("Arc") = linia.Guid
                        oRow("Line") = linia.lin
                        oRow("Qty") = linia.qty
                    End With
                Next
            Next
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Deliveries(oShipmentsReportLog As DTOJsonLog) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT AlbGuid, Alb, Fch, CliGuid, Clx ")
        sb.AppendLine("FROM VwShipmentsReport ")
        sb.AppendLine("WHERE VwShipmentsReport.Log = '" & oShipmentsReportLog.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY AlbGuid, Alb, Fch, CliGuid, Clx ")
        sb.AppendLine("ORDER BY Fch, Alb")
        Dim SQL As String = sb.ToString

        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDelivery As New DTODelivery(oDrd("AlbGuid"))
            With oDelivery
                .Fch = oDrd("Fch")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("Clx"))
            End With
            retval.Add(oDelivery)
        Loop

        oDrd.Close()
        Return retval
    End Function
End Class
