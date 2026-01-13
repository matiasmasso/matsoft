Public Class ImportPrevisioLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOImportPrevisio
        Dim retval As DTOImportPrevisio = Nothing
        Dim oImportPrevisio As New DTOImportPrevisio(oGuid)
        If Load(oImportPrevisio) Then
            retval = oImportPrevisio
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oImportPrevisio As DTOImportPrevisio) As Boolean
        If Not oImportPrevisio.IsLoaded And Not oImportPrevisio.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM ImportPrevisio ")
            sb.AppendLine("WHERE Guid='" & oImportPrevisio.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oImportPrevisio
                    .Importacio = New DTOImportacio(oDrd("Importacio"))
                    .Lin = oDrd("Lin")
                    .NumComandaProveidor = oDrd("NumComandaProveidor")
                    If Not IsDBNull(oDrd("PurchaseOrderItem")) Then
                        .purchaseOrderItem = New DTOPurchaseOrderItem(oDrd("PurchaseOrderItem"))
                    End If
                    If Not IsDBNull(oDrd("InvoiceReceivedItem")) Then
                        .invoiceReceivedItem = New DTOInvoiceReceived.Item(oDrd("InvoiceReceivedItem"))
                    End If
                    If Not IsDBNull(oDrd("Sku")) Then
                        .sku = New DTOProductSku(oDrd("Sku"))
                    End If
                    .SkuRef = oDrd("Ref")
                    .SkuNom = oDrd("Nom")
                    .Qty = oDrd("Qty")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oImportPrevisio.IsLoaded
        Return retval
    End Function

    Shared Function Update(oImportPrevisio As DTOImportPrevisio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oImportPrevisio, oTrans)
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


    Shared Sub Update(oImportPrevisio As DTOImportPrevisio, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ImportPrevisio ")
        sb.AppendLine("WHERE Guid='" & oImportPrevisio.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oImportPrevisio.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oImportPrevisio
            oRow("Importacio") = .Importacio.Guid
            oRow("Lin") = .Lin
            oRow("NumComandaProveidor") = SQLHelper.NullableString(.NumComandaProveidor)
            oRow("PurchaseOrderItem") = SQLHelper.NullableBaseGuid(.purchaseOrderItem)
            oRow("InvoiceReceivedItem") = SQLHelper.NullableBaseGuid(.invoiceReceivedItem)
            oRow("Sku") = SQLHelper.NullableBaseGuid(.sku)
            oRow("Ref") = SQLHelper.NullableString(.SkuRef)
            oRow("Nom") = SQLHelper.NullableString(.SkuNom)
            oRow("Qty") = .Qty
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oImportPrevisio As DTOImportPrevisio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oImportPrevisio, oTrans)
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


    Shared Sub Delete(oImportPrevisio As DTOImportPrevisio, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ImportPrevisio WHERE Guid='" & oImportPrevisio.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class ImportPrevisionsLoader

    Shared Function All(oSku As DTOProductSku) As List(Of DTOImportPrevisio)
        Dim retval As New List(Of DTOImportPrevisio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ImportPrevisio.Guid, ImportPrevisio.NumComandaProveidor, ImportPrevisio.Ref, ImportPrevisio.Qty, ImportPrevisio.Nom, ImportPrevisio.PurchaseOrderItem, ImportPrevisio.InvoiceReceivedItem ")
        sb.AppendLine(", ImportPrevisio.Importacio, ImportHdr.Yea, ImportHdr.Id, ImportHdr.PrvGuid, ImportHdr.Week, CliGral.FullNom ")
        sb.AppendLine("FROM ImportPrevisio ")
        sb.AppendLine("INNER JOIN ImportHdr ON ImportPrevisio.Importacio=ImportHdr.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON ImportHdr.PrvGuid=CliGral.Guid ")
        sb.AppendLine("WHERE ImportPrevisio.Sku='" & oSku.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ImportHdr.Yea, ImportHdr.Id ")

        Dim oImportacio As New DTOImportacio(Guid.NewGuid)
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOImportPrevisio(oDrd("Guid"))
            With item
                If Not oDrd("Importacio").Equals(oImportacio.Guid) Then
                    oImportacio = New DTOImportacio(oDrd("Importacio"))
                    With oImportacio
                        .Yea = oDrd("Yea")
                        .Id = oDrd("Id")
                        .Week = oDrd("Week")
                        If Not IsDBNull(oDrd("PrvGuid")) Then
                            .Proveidor = New DTOProveidor(oDrd("PrvGuid"))
                            .Proveidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                        End If
                    End With
                End If
                .Importacio = oImportacio
                .NumComandaProveidor = SQLHelper.GetStringFromDataReader(oDrd("NumComandaProveidor"))
                .skuRef = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                .Qty = oDrd("Qty")
                .Sku = oSku
                If Not IsDBNull(oDrd("PurchaseOrderItem")) Then
                    .PurchaseOrderItem = New DTOPurchaseOrderItem(oDrd("PurchaseOrderItem"))
                End If
                If Not IsDBNull(oDrd("InvoiceReceivedItem")) Then
                    .invoiceReceivedItem = New DTOInvoiceReceived.Item(oDrd("InvoiceReceivedItem"))
                End If
            End With
            retval.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function

    Shared Sub Load(oImportacio As DTOImportacio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ImportPrevisio.Guid, ImportPrevisio.NumComandaProveidor, ImportPrevisio.Ref, ImportPrevisio.Qty, ImportPrevisio.Nom, ImportPrevisio.PurchaseOrderItem, ImportPrevisio.InvoiceReceivedItem ")
        sb.AppendLine(", VwSkuPncs.Pn1, Pnc.PdcGuid, Pdc.Pdd ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM ImportPrevisio ")
        sb.AppendLine("LEFT OUTER JOIN Pnc ON ImportPrevisio.PurchaseOrderItem = Pnc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN InvoiceReceivedItem ON ImportPrevisio.InvoiceReceivedItem = InvoiceReceivedItem.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON ImportPrevisio.Sku = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuPncs ON ImportPrevisio.Sku = VwSkuPncs.SkuGuid ")
        sb.AppendLine("WHERE Importacio='" & oImportacio.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ImportPrevisio.Lin ")

        oImportacio.Previsions = New List(Of DTOImportPrevisio)
        Dim oOrder As New DTOPurchaseOrder
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOImportPrevisio(oDrd("Guid"))
            With item
                .Importacio = oImportacio
                .NumComandaProveidor = SQLHelper.GetStringFromDataReader(oDrd("NumComandaProveidor"))
                .SkuRef = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                .Qty = oDrd("Qty")
                If IsDBNull(oDrd("SkuGuid")) Then
                    .skuNom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    .errors.Add(DTOImportPrevisio.ValidationErrors.skuNotFound)
                Else
                    .sku = SQLHelper.GetProductFromDataReader(oDrd)
                    .Sku.Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
                    .SkuNom = .Sku.RefYNomLlarg.Esp
                End If
                If IsDBNull(oDrd("PurchaseOrderItem")) Then
                    ' .Errors.Add(DTOImportPrevisio.ValidationErrors.OrderNotFound)
                Else
                    .PurchaseOrderItem = New DTOPurchaseOrderItem(oDrd("PurchaseOrderItem"))
                    If IsDBNull(oDrd("PdcGuid")) Then
                    Else
                        If oOrder.Guid.Equals(oDrd("PdcGuid")) Then
                        Else
                            oOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                            oOrder.Concept = oDrd("Pdd")
                        End If
                        .PurchaseOrderItem.PurchaseOrder = oOrder
                    End If
                End If
                If Not IsDBNull(oDrd("InvoiceReceivedItem")) Then
                    .invoiceReceivedItem = New DTOInvoiceReceived.Item(oDrd("InvoiceReceivedItem"))
                End If
            End With
            oImportacio.Previsions.Add(item)
        Loop

        oDrd.Close()

    End Sub

    Shared Function Update(oImportacio As DTOImportacio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oImportacio, oTrans)
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


    Shared Sub Update(oImportacio As DTOImportacio, ByRef oTrans As SqlTransaction)
        Delete(oImportacio, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ImportPrevisio ")
        sb.AppendLine("WHERE Importacio='" & oImportacio.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Lin ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim lin As Integer
        For Each item As DTOImportPrevisio In oImportacio.Previsions
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            lin += 1
            oRow("Guid") = item.Guid
            oRow("Importacio") = oImportacio.Guid
            oRow("NumComandaProveidor") = item.NumComandaProveidor
            oRow("lin") = lin
            oRow("Ref") = item.SkuRef
            oRow("Qty") = item.Qty
            oRow("Sku") = SQLHelper.NullableBaseGuid(item.Sku)
            oRow("Nom") = item.skuNom
            oRow("PurchaseOrderItem") = SQLHelper.NullableBaseGuid(item.purchaseOrderItem)
            oRow("InvoiceReceivedItem") = SQLHelper.NullableBaseGuid(item.invoiceReceivedItem)
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function SetSkus(oImportacio As DTOImportacio, exs As List(Of Exception)) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE ImportPrevisio SET ImportPrevisio.Sku=VwSkuNom.SkuGuid ")
        sb.AppendLine("FROM ImportPrevisio ")
        sb.AppendLine("INNER JOIN ImportHdr ON ImportPrevisio.Importacio=ImportHdr.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON ImportHdr.PrvGuid=VwSkuNom.Proveidor AND ImportPrevisio.Ref=VwSkuNom.SkuRef ")
        sb.AppendLine("WHERE ImportPrevisio.Importacio='" & oImportacio.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function Delete(oPrevisions As List(Of DTOImportPrevisio), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oPrevisions, oTrans)
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


    Shared Sub Delete(oPrevisions As List(Of DTOImportPrevisio), ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE ImportPrevisio ")
        sb.AppendLine("WHERE ( ")
        For Each Item As DTOImportPrevisio In oPrevisions
            If Item.UnEquals(oPrevisions.First) Then sb.Append(" OR ")
            sb.AppendLine("Guid='" & Item.Guid.ToString & "' ")
        Next
        sb.AppendLine(" ) ")

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Delete(oImportacio As DTOImportacio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oImportacio, oTrans)
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


    Shared Sub Delete(oImportacio As DTOImportacio, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE ImportPrevisio ")
        sb.AppendLine("WHERE Importacio='" & oImportacio.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class
