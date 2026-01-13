Public Class EdiversaOrdrSpLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOEdiversaOrdrsp
        Dim retval As DTOEdiversaOrdrsp = Nothing
        Dim oEdiversaOrdrSp As New DTOEdiversaOrdrsp(oGuid)
        If Load(oEdiversaOrdrSp) Then
            retval = oEdiversaOrdrSp
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaOrdrSp As DTOEdiversaOrdrsp) As Boolean
        If Not oEdiversaOrdrSp.IsLoaded And Not oEdiversaOrdrSp.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT EdiversaOrdrSpHeader.Fch AS SpFch, EdiversaOrdrSpHeader.FchCreated AS SpFchCreated ")
            sb.AppendLine(", EDiversaOrderHeader.Guid AS OrderGuid, EDiversaOrderHeader.DocNum, EDiversaOrderHeader.FchDoc ")
            sb.AppendLine(", EDiversaOrderHeader.Comprador, Comprador.FullNom AS CompradorNom ")
            sb.AppendLine(", EdiversaOrdrSpItem.Qty AS SpQty ")
            sb.AppendLine(", EdiversaOrderItem.Guid AS OrderItemGuid, EdiversaOrderItem.Qty AS OrderQty, EdiversaOrderItem.Sku ")
            sb.AppendLine(", VwSkuNom.* ")
            sb.AppendLine("FROM EdiversaOrdrSpHeader ")
            sb.AppendLine("INNER JOIN  EDiversaOrderHeader ON EdiversaOrdrSpHeader.EdiversaOrder = EDiversaOrderHeader.Guid")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS Comprador ON EDiversaOrderHeader.Comprador=Comprador.Guid ")
            sb.AppendLine("INNER JOIN EdiversaOrderItem ON EDiversaOrderHeader.Guid=EdiversaOrderItem.Parent ")
            sb.AppendLine("INNER JOIN EdiversaOrdrSpItem ON EdiversaOrderItem.Guid = EdiversaOrdrSpItem.OrderItem ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON EdiversaOrderItem.Sku = VwSkuNom.SkuGuid ")
            sb.AppendLine("WHERE EdiversaOrdrSpHeader.Guid='" & oEdiversaOrdrSp.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oEdiversaOrdrSp.IsLoaded Then
                    With oEdiversaOrdrSp
                        .Order = New DTOEdiversaOrder(oDrd("OrderGuid"))
                        With .Order
                            .DocNum = oDrd("DocNum")
                            .FchDoc = oDrd("FchDoc")
                            If Not IsDBNull(oDrd("Comprador")) Then
                                .Comprador = New DTOContact(DirectCast(oDrd("Comprador"), Guid))
                                .Comprador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("CompradorNom"))
                            End If
                        End With
                        .Fch = oDrd("SpFch")
                        .FchCreated = oDrd("SpFchCreated")
                        .Items = New List(Of DTOEdiversaOrdrspItem)

                        .IsLoaded = True
                    End With
                End If



                Dim item As New DTOEdiversaOrdrspItem()
                With item
                    .OrderItem = New DTOEdiversaOrderItem(oDrd("OrderItemGuid"))
                    With .OrderItem
                        .Parent = oEdiversaOrdrSp.Order
                        .Qty = oDrd("OrderQty")
                        .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                    End With
                    .Qty = oDrd("SpQty")
                End With
            Loop
            oDrd.Close()
        End If
        Dim retval As Boolean = oEdiversaOrdrSp.IsLoaded
        Return retval
    End Function

    Shared Function Update(oEdiversaOrdrSp As DTOEdiversaOrdrsp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEdiversaOrdrSp, oTrans)
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

    Shared Sub Update(oEdiversaOrdrSp As DTOEdiversaOrdrsp, ByRef oTrans As SqlTransaction)
        UpdateHeader(oEdiversaOrdrSp, oTrans)
        UpdateItems(oEdiversaOrdrSp, oTrans)
    End Sub

    Shared Sub UpdateHeader(oEdiversaOrdrSp As DTOEdiversaOrdrsp, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EdiversaOrdrSpHeader ")
        sb.AppendLine("WHERE Guid='" & oEdiversaOrdrSp.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oEdiversaOrdrSp.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oEdiversaOrdrSp
            oRow("EdiversaOrder") = .Order.Guid
            oRow("Fch") = .Fch
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oEdiversaOrdrSp As DTOEdiversaOrdrsp, ByRef oTrans As SqlTransaction)
        DeleteItems(oEdiversaOrdrSp, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EdiversaOrdrSpItem ")
        sb.AppendLine("WHERE Parent='" & oEdiversaOrdrSp.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item As DTOEdiversaOrdrspItem In oEdiversaOrdrSp.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Parent") = oEdiversaOrdrSp.Guid
            oRow("OrderItem") = item.OrderItem.Guid
            oRow("Qty") = item.Qty
        Next

        oDA.Update(oDs)
    End Sub



    Shared Function Delete(oEdiversaOrdrSp As DTOEdiversaOrdrsp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oEdiversaOrdrSp, oTrans)
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

    Shared Sub Delete(oEdiversaOrdrSp As DTOEdiversaOrdrsp, ByRef oTrans As SqlTransaction)
        DeleteItems(oEdiversaOrdrSp, oTrans)
        DeleteHeader(oEdiversaOrdrSp, oTrans)
    End Sub

    Shared Sub DeleteItems(oEdiversaOrdrSp As DTOEdiversaOrdrsp, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaOrdrSpItem WHERE Parent='" & oEdiversaOrdrSp.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oEdiversaOrdrSp As DTOEdiversaOrdrsp, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaOrdrSpHeader WHERE Guid='" & oEdiversaOrdrSp.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class EdiversaOrdrSpsLoader

    Shared Function Headers() As List(Of DTOEdiversaOrdrsp)
        Dim retval As New List(Of DTOEdiversaOrdrsp)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT EdiversaOrdrSpHeader.Guid, EdiversaOrdrSpHeader.Fch, EdiversaOrdrSpHeader.EdiversaOrder ")
        sb.AppendLine(", EDiversaOrderHeader.DocNum, EDiversaOrderHeader.Comprador, CliGral.FullNom ")
        sb.AppendLine("FROM EdiversaOrdrSpHeader ")
        sb.AppendLine("INNER JOIN EdiversaOrderHeader ON EdiversaOrdrSpHeader.EdiversaOrder = EdiversaOrderHeader.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON EdiversaOrderHeader.Comprador = CliGral.Guid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOEdiversaOrdrsp(oDrd("Guid"))
            With item
                .Fch = oDrd("Fch")
                .Order = New DTOEdiversaOrder(oDrd("EdiversaOrder"))
                With .Order
                    .DocNum = oDrd("DocNum")
                    .Comprador = New DTOContact(oDrd("Comprador"))
                    .Comprador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                End With
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
