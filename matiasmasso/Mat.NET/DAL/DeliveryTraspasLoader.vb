Public Class DeliveryTraspasLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTODeliveryTraspas
        Dim retval As DTODeliveryTraspas = Nothing
        Dim oDeliveryTraspas As New DTODeliveryTraspas(oGuid)
        If Load(oDeliveryTraspas) Then
            retval = oDeliveryTraspas
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oDeliveryTraspas As DTODeliveryTraspas) As Boolean
        If Not oDeliveryTraspas.IsLoaded And Not oDeliveryTraspas.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Alb.Alb, Alb.Fch ")
            sb.AppendLine(", Arc.Cod, Arc.Qty, Arc.Eur, VwSkuNom.* ")
            sb.AppendLine(", Alb.MgzGuid, Mgz.Nom as MgzAbr ")
            sb.AppendLine("FROM Alb ")
            sb.AppendLine("LEFT OUTER JOIN Arc ON Alb.Guid = Arc.AlbGuid ")
            sb.AppendLine("LEFT OUTER JOIN Mgz ON Alb.MgzGuid = Mgz.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON Arc.ArtGuid = VwSkuNom.SkuGuid ")
            sb.AppendLine("WHERE Alb.Guid='" & oDeliveryTraspas.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Arc.Lin ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

            Do While oDrd.Read
                If Not oDeliveryTraspas.IsLoaded Then
                    With oDeliveryTraspas
                        .Fch = oDrd("Fch")
                        .Id = oDrd("Alb")
                        .Items = New List(Of DTODeliveryItem)
                        .IsLoaded = True
                    End With
                End If
                If oDrd("Cod") = DTODeliveryItem.Cods.TraspasEntrada Then
                    Dim item As New DTODeliveryItem
                    With item
                        .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                        .Qty = oDrd("Qty")
                        .Price = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                    End With
                    oDeliveryTraspas.Items.Add(item)

                    If oDeliveryTraspas.MgzTo Is Nothing Then
                        oDeliveryTraspas.MgzTo = New DTOMgz(oDrd("MgzGuid"))
                        oDeliveryTraspas.MgzTo.Abr = oDrd("MgzAbr")
                    End If
                Else
                    If oDeliveryTraspas.MgzFrom Is Nothing Then
                        oDeliveryTraspas.MgzFrom = New DTOMgz(oDrd("MgzGuid"))
                        oDeliveryTraspas.MgzFrom.Abr = oDrd("MgzAbr")
                    End If
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oDeliveryTraspas.IsLoaded
        Return retval
    End Function

    Shared Function Update(oDeliveryTraspas As DTODeliveryTraspas, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oDeliveryTraspas, oTrans)
            oTrans.Commit()
            oDeliveryTraspas.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oDeliveryTraspas As DTODeliveryTraspas, ByRef oTrans As SqlTransaction)
        UpdateHeader(oDeliveryTraspas, oTrans)
        DeleteItems(oDeliveryTraspas, oTrans)
        UpdateItems(oDeliveryTraspas, oTrans)
    End Sub

    Shared Sub UpdateHeader(oDeliveryTraspas As DTODeliveryTraspas, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("WHERE Guid='" & oDeliveryTraspas.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oDeliveryTraspas.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        If oDeliveryTraspas.Id = 0 Then oDeliveryTraspas.Id = LastId(oDeliveryTraspas, oTrans) + 1

        With oDeliveryTraspas
            oRow("Emp") = .Emp.Id
            oRow("Yea") = .Fch.Year
            oRow("Alb") = .Id
            oRow("Cod") = DTOPurchaseOrder.Codis.Traspas
            oRow("Fch") = .Fch
            oRow("CliGuid") = .Emp.Org.Guid
            oRow("IvaExempt") = 1

            Dim oImport As DTOAmt = DTOAmt.Factory(.Items.Where(Function(y) y.Price IsNot Nothing).Sum(Function(x) x.Qty * x.Price.Eur))
            SQLHelper.SetNullableAmt(oImport, oRow, "Eur", "Cur", "Pts")
            oRow("MgzGuid") = .MgzFrom.Guid
            oRow("Facturable") = 0
            oRow("Nom") = .Emp.Org.Nom
            oRow("Adr") = .Emp.Org.Address.Text
            oRow("Zip") = SQLHelper.NullableBaseGuid(.Emp.Org.Address.Zip)
            SQLHelper.SetUsrLog(.UsrLog, oRow, UsrCreatedField:="UsrCreatedGuid", UsrLastEditedField:="UsrLastEditedGuid")
        End With

        oDA.Update(oDs)
    End Sub
    Shared Sub UpdateItems(oDeliveryTraspas As DTODeliveryTraspas, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("WHERE Arc.AlbGuid='" & oDeliveryTraspas.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim lin As Integer = 0
        For Each item In oDeliveryTraspas.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            lin += 1
            UpdateItem(oRow, oDeliveryTraspas, item, lin, DTODeliveryItem.Cods.TraspasSortida)

            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            lin += 1
            UpdateItem(oRow, oDeliveryTraspas, item, lin, DTODeliveryItem.Cods.TraspasEntrada)
        Next
        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItem(ByRef oRow As DataRow, oDeliveryTraspas As DTODeliveryTraspas, item As DTODeliveryItem, lin As Integer, oCod As DTODeliveryItem.Cods)
        oRow("Lin") = lin
        oRow("Cod") = oCod

        With oDeliveryTraspas
            oRow("AlbGuid") = .Guid
            oRow("Emp") = .Emp.Id
            oRow("Ye1") = .Fch.Year
            oRow("Alb") = .Id
            oRow("Fch") = .Fch
            Select Case oCod
                Case DTODeliveryItem.Cods.TraspasEntrada
                    oRow("MgzGuid") = .MgzTo.Guid
                Case DTODeliveryItem.Cods.TraspasSortida
                    oRow("MgzGuid") = .MgzFrom.Guid
            End Select
        End With
        With item
            oRow("Qty") = .Qty
            SQLHelper.SetNullableAmt(.Price, oRow, "Eur", "Cur", "Pts")
            oRow("ArtGuid") = .Sku.Guid
        End With
    End Sub

    Shared Function LastId(oDelivery As DTODeliveryTraspas, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim sb As New Text.StringBuilder
        sb.Append("SELECT TOP 1 Alb AS LastId ")
        sb.Append("FROM Alb ")
        sb.Append("WHERE Emp =" & oDelivery.Emp.Id & " ")
        sb.Append("AND Yea=" & oDelivery.Fch.Year & " ")
        sb.Append("ORDER BY Alb DESC")
        Dim SQL = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastId")) Then
                retval = CInt(oRow("LastId"))
            End If
        End If
        Return retval
    End Function


    Shared Function Delete(oDeliveryTraspas As DTODeliveryTraspas, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oDeliveryTraspas, oTrans)
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


    Shared Sub Delete(oDeliveryTraspas As DTODeliveryTraspas, ByRef oTrans As SqlTransaction)
        DeleteItems(oDeliveryTraspas, oTrans)
        DeleteHeader(oDeliveryTraspas, oTrans)
    End Sub
    Shared Sub DeleteHeader(oDeliveryTraspas As DTODeliveryTraspas, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Alb WHERE Alb.Guid='" & oDeliveryTraspas.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
    Shared Sub DeleteItems(oDeliveryTraspas As DTODeliveryTraspas, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Arc WHERE Arc.AlbGuid='" & oDeliveryTraspas.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region


End Class

Public Class DeliveryTraspassosLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTODeliveryTraspas)
        Dim retval As New List(Of DTODeliveryTraspas)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Alb.Guid, Alb.Alb, Alb.fch ")
        sb.AppendLine(", MAX(CASE WHEN Arc.Cod<50 THEN Alb.MgzGuid ELSE NULL END) AS MgzTo ")
        sb.AppendLine(", MAX(CASE WHEN Arc.Cod>=50 THEN Alb.MgzGuid ELSE NULL END) AS MgzFrom ")
        sb.AppendLine("FROM Alb  ")
        sb.AppendLine("INNER JOIN Arc ON Alb.Guid = Arc.AlbGuid ")
        sb.AppendLine("WHERE Alb.Cod=3 ")
        sb.AppendLine("AND Alb.MgzGuid IS NOT NULL ")
        sb.AppendLine("GROUP BY Alb.Guid, Alb.Alb, Alb.fch ")
        sb.AppendLine("HAVING( MAX(CASE WHEN Arc.Cod<50 THEN Alb.MgzGuid ELSE NULL END) IS NOT NULL ")
        sb.AppendLine("AND MAX(CASE WHEN Arc.Cod>=50 THEN Alb.MgzGuid ELSE NULL END) IS NOT NULL )")
        sb.AppendLine("ORDER BY Alb.Fch DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTODeliveryTraspas(oDrd("Guid"))
            With item
                .Id = oDrd("Alb")
                .Fch = oDrd("Fch")
                .MgzFrom = New DTOMgz(oDrd("MgzFrom"))
                .MgzTo = New DTOMgz(oDrd("MgzTo"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

