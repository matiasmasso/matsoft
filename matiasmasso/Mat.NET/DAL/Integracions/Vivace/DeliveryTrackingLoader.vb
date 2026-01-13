Imports DTO.Integracions.Vivace

Public Class DeliveryTrackingLoader
    Shared Function Update(exs As List(Of Exception), oTracking As DeliveryTracking) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oTracking, oTrans)
            Update(oTracking, oTrans)
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


    Shared Sub Update(oTracking As DeliveryTracking, ByRef oTrans As SqlTransaction)


        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM DeliveryTracking ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each shipment In oTracking.shipments
            For Each package In shipment.items
                Dim oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                With oTracking
                    oRow("Guid") = Guid.NewGuid()
                    oRow("Emp") = CInt(DTOEmp.Ids.MatiasMasso)
                    oRow("Log") = SQLHelper.NullableBaseGuid(.Log)
                    oRow("Sender") = .sender
                    oRow("Fch") = .date
                End With
                With shipment
                    oRow("Delivery") = .delivery
                    oRow("Packages") = .packages
                    oRow("Forwarder") = .forwarder
                    oRow("Pallets") = .palets
                    oRow("Tracking") = .tracking
                    oRow("CubicKg") = .cubicKg
                    oRow("Weight") = .weight
                    oRow("Volume") = .volume
                    oRow("Cost") = .cost
                End With
                With package
                    oRow("Package") = .package
                    oRow("SSCC") = .SSCC
                    oRow("Packaging") = .packaging
                    oRow("Length") = .length
                    oRow("Width") = .width
                    oRow("Height") = .height
                End With
            Next
        Next

        oDA.Update(oDs)
    End Sub

    Shared Sub Delete(oTracking As DeliveryTracking, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder

        sb = New Text.StringBuilder()
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Sender VARCHAR(12) NOT NULL")
        sb.AppendLine("	    , Delivery VARCHAR(12) NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Sender, Delivery) ")
        Dim idx As Integer = 0
        For Each oShipment In oTracking.shipments
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}','{1}') ", oTracking.sender.Trim(), oShipment.delivery.Trim())
            idx += 1
        Next
        sb.AppendLine()

        sb.AppendLine("DELETE DeliveryTracking ")
        sb.AppendLine("FROM DeliveryTracking ")
        sb.AppendLine("INNER JOIN @Table X ON DeliveryTracking.Sender = X.Sender AND DeliveryTracking.Delivery = X.Delivery ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


End Class
