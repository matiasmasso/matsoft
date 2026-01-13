Public Class InvoiceReceivedLoader
    Shared Function Find(oGuid As Guid) As DTOInvoiceReceived
        Dim retval As DTOInvoiceReceived = Nothing
        Dim oInvoiceReceived As New DTOInvoiceReceived(oGuid)
        If Load(oInvoiceReceived) Then
            retval = oInvoiceReceived
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oInvoiceReceived As DTOInvoiceReceived) As Boolean
        If Not oInvoiceReceived.IsLoaded And Not oInvoiceReceived.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT InvoiceReceivedHeader.* ")
            sb.AppendLine(", InvoiceReceivedItem.Guid AS ItemGuid, InvoiceReceivedItem.* ")
            sb.AppendLine(", VwSkuNom.* ")
            sb.AppendLine(", CliGral.FullNom ")
            sb.AppendLine(", Pdc.Pdc, Pdc.Pdd, Pdc.Fch as PdcFch, Pdc.Cod AS PdcCod ")
            sb.AppendLine(", Pnc.Qty AS PncQty, Pnc.Pts AS PncDiv, Pnc.Cur AS PncCur, Pnc.Eur AS PncEur, Pnc.Dto AS PncDto ")
            sb.AppendLine(", ImportHdr.Guid AS ImportGuid, ImportHdr.Yea AS ImportYea, ImportHdr.Id AS ImportId ")
            sb.AppendLine("FROM InvoiceReceivedHeader ")
            sb.AppendLine("LEFT OUTER JOIN CliGral on InvoiceReceivedHeader.Proveidor = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN ImportHdr on InvoiceReceivedHeader.Importacio = ImportHdr.Guid ")
            sb.AppendLine("LEFT OUTER JOIN InvoiceReceivedItem on InvoiceReceivedHeader.Guid = InvoiceReceivedItem.Parent ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom on InvoiceReceivedItem.Sku = VwSkuNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN Pdc on InvoiceReceivedItem.PurchaseOrder = Pdc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pnc on InvoiceReceivedItem.PurchaseOrderItem = Pnc.Guid ")
            sb.AppendLine("WHERE InvoiceReceivedHeader.Guid='" & oInvoiceReceived.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY InvoiceReceivedItem.Lin ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Dim oPurchaseOrder As DTOPurchaseOrder = Nothing
            Do While oDrd.Read

                With oInvoiceReceived
                    If Not .IsLoaded Then
                        .DocNum = SQLHelper.GetStringFromDataReader(oDrd("DocNum"))
                        .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                        .Proveidor = DTOGuidNom.Factory(oDrd("Proveidor"), SQLHelper.GetStringFromDataReader(oDrd("FullNom")))
                        '.Proveidor.lang = SQLHelper.GetLangFromDataReader(oDrd("LangId"))
                        .ProveidorEan = SQLHelper.GetEANFromDataReader(oDrd("ProveidorEan"))
                        .Cur = SQLHelper.GetCurFromDataReader(oDrd("Cur"))
                        If Not IsDBNull(oDrd("TaxBase")) Then
                            .TaxBase = DTOAmt.Factory(CDec(oDrd("TaxBase")))
                        End If
                        If Not IsDBNull(oDrd("NetTotal")) Then
                            .NetTotal = DTOAmt.Factory(CDec(oDrd("NetTotal")))
                        End If
                        If Not IsDBNull(oDrd("Importacio")) Then
                            Dim formattedImport = String.Format("{0:0000}/{1}", SQLHelper.GetIntegerFromDataReader(oDrd("ImportYea")), SQLHelper.GetIntegerFromDataReader(oDrd("ImportId")))
                            .Importacio = DTOGuidNom.Factory(oDrd("Importacio"), formattedImport)
                        End If
                        .ShipmentId = SQLHelper.GetStringFromDataReader(oDrd("ShipmentId"))
                        .Items = New List(Of DTOInvoiceReceived.Item)
                        .IsLoaded = True
                    End If
                    If Not IsDBNull(oDrd("Parent")) Then
                        Dim item = oInvoiceReceived.AddItem(oDrd("ItemGuid"))
                        With item
                            .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                            .QtyConfirmed = SQLHelper.GetIntegerFromDataReader(oDrd("QtyConfirmed"))
                            .Price = SQLHelper.GetDecimalFromDataReader(oDrd("Price"))
                            .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                            If Not IsDBNull(oDrd("Amount")) Then
                                .Amount = DTOAmt.Factory(oDrd("Amount"))
                            End If
                            .SkuEan = SQLHelper.GetEANFromDataReader(oDrd("SkuEan"))
                            .SkuRef = SQLHelper.GetStringFromDataReader(oDrd("SkuRef"))
                            .SkuNom = SQLHelper.GetStringFromDataReader(oDrd("SkuNom"))
                            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
                            If oSku IsNot Nothing Then
                                .Sku = DTOGuidNom.Factory(oSku.Guid, String.Format("{0:00000} {1}", oSku.Id, oSku.nomPrvAndRefOrMyd))
                            End If

                            If IsDBNull(oDrd("PurchaseOrder")) Then
                                oPurchaseOrder = Nothing
                            ElseIf oPurchaseOrder Is Nothing OrElse Not oPurchaseOrder.Guid.Equals(oDrd("PurchaseOrder")) Then
                                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PurchaseOrder"))
                                With oPurchaseOrder
                                    .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Pdc"))
                                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("PdcFch"))
                                    .Concept = SQLHelper.GetStringFromDataReader(oDrd("Pdd"))
                                    .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("PdcCod"))
                                End With
                            End If

                            If oPurchaseOrder IsNot Nothing Then
                                Dim concept = String.Format("Comanda {0} del {1:dd/MM/yy} {2}", oPurchaseOrder.Num, oPurchaseOrder.Fch, oPurchaseOrder.Concept)
                                .PurchaseOrder = DTOGuidNom.Factory(oPurchaseOrder.Guid, concept)
                            End If

                            .DeliveryNote = SQLHelper.GetStringFromDataReader(oDrd("DeliveryNote"))
                            .OrderConfirmation = SQLHelper.GetStringFromDataReader(oDrd("OrderConfirmation"))
                            .PurchaseOrderId = SQLHelper.GetStringFromDataReader(oDrd("PurchaseOrderId"))
                            If Not IsDBNull(oDrd("PurchaseOrderItem")) Then
                                .PurchaseOrderItem = New DTOPurchaseOrderItem(oDrd("PurchaseOrderItem"))
                                With .PurchaseOrderItem
                                    .Sku = oSku
                                    If Not IsDBNull(oDrd("PncEur")) Then
                                        .Price = DTOAmt.Factory(oDrd("PncEur"), oDrd("PncCur"), oDrd("PncDiv"))
                                        .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("PncDto"))
                                    End If
                                    .PurchaseOrder = oPurchaseOrder
                                End With
                            End If
                        End With
                    End If
                End With
            Loop

            oDrd.Close()
        End If

        LoadExceptions(oInvoiceReceived)
        Dim retval As Boolean = oInvoiceReceived.IsLoaded
        Return retval
    End Function

    Private Shared Sub LoadExceptions(oInvoiceReceived As DTOInvoiceReceived)
        Dim sb As New System.Text.StringBuilder
        'sb.AppendLine("Select X.Guid, X.Cod, X.Tag, X.Msg ")
        'sb.AppendLine("FROM ( ")
        sb.AppendLine("Select Exception.Guid, Exception.Cod, Exception.Tag, CAST(Exception.Msg As VARCHAR(MAX)) As Msg, Exception.Parent ")
        sb.AppendLine("FROM Exception ")
        sb.AppendLine("WHERE Exception.Parent = '" & oInvoiceReceived.Guid.ToString() & "'")
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT Exception.Guid, Exception.Cod, Exception.Tag, CAST(Exception.Msg AS VARCHAR(MAX)) AS Msg, Exception.Parent ")
        sb.AppendLine("FROM Exception ")
        sb.AppendLine("INNER JOIN InvoiceReceivedItem on Exception.Parent = InvoiceReceivedItem.Guid ")
        sb.AppendLine("WHERE InvoiceReceivedItem.Parent = '" & oInvoiceReceived.Guid.ToString() & "'")
        'sb.AppendLine(") X ")
        'sb.AppendLine("WHERE X.Parent = '" & oInvoiceReceived.Guid.ToString() & "'")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oParent = oDrd("Parent")
            Dim ex As New DTOException(oDrd("Guid"))
            With ex
                .Msg = SQLHelper.GetStringFromDataReader(oDrd("Msg"))
                .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cod"))
                If Not IsDBNull(oDrd("Tag")) Then
                    .Tag = New DTOBaseGuid(oDrd("Tag"))
                End If
            End With
            If oParent.Equals(oInvoiceReceived.Guid) Then
                oInvoiceReceived.Exceptions.Add(ex)
            Else
                Dim item = oInvoiceReceived.Items.FirstOrDefault(Function(x) x.Guid.Equals(oParent))
                If item IsNot Nothing Then
                    item.Exceptions.Add(ex)
                End If
            End If
        Loop
        oDrd.Close()
    End Sub

    Shared Function Update(oInvoiceReceived As DTOInvoiceReceived, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oInvoiceReceived, oTrans)
            oTrans.Commit()
            oInvoiceReceived.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oInvoiceReceived As DTOInvoiceReceived, ByRef oTrans As SqlTransaction)
        DeleteExceptions(oInvoiceReceived, oTrans)
        DeleteItems(oInvoiceReceived, oTrans)
        UpdateHeader(oInvoiceReceived, oTrans)
        UpdateItems(oInvoiceReceived, oTrans)
        UpdateExceptions(oInvoiceReceived, oTrans)
    End Sub

    Private Shared Sub UpdateHeader(oInvoiceReceived As DTOInvoiceReceived, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM InvoiceReceivedHeader ")
        sb.AppendLine("WHERE Guid='" & oInvoiceReceived.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oInvoiceReceived.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oInvoiceReceived
            oRow("DocNum") = SQLHelper.NullableString(.DocNum, 20)
            oRow("Fch") = SQLHelper.NullableFch(.Fch)
            oRow("Proveidor") = SQLHelper.NullableBaseGuid(.Proveidor)
            oRow("ProveidorEan") = SQLHelper.NullableEan(.ProveidorEan)
            oRow("Cur") = .Cur.Tag
            oRow("Importacio") = SQLHelper.NullableBaseGuid(.Importacio)
            oRow("ShipmentId") = SQLHelper.NullableString(.ShipmentId, 20)
            oRow("TaxBase") = SQLHelper.NullableAmt(.TaxBase)
            If .NetTotal IsNot Nothing Then
                oRow("NetTotal") = .NetTotal.Val
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Private Shared Sub UpdateItems(oInvoiceReceived As DTOInvoiceReceived, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM InvoiceReceivedItem ")
        sb.AppendLine("WHERE Parent='" & oInvoiceReceived.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item In oInvoiceReceived.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Parent") = oInvoiceReceived.Guid
            With item
                oRow("Guid") = .Guid
                oRow("Lin") = .Lin
                oRow("Qty") = SQLHelper.NullableInt(.Qty)
                oRow("QtyConfirmed") = SQLHelper.NullableInt(.QtyConfirmed)
                oRow("Price") = SQLHelper.NullableDecimal(.Price)
                oRow("Dto") = SQLHelper.NullableDecimal(.Dto)
                oRow("Amount") = SQLHelper.NullableAmt(.Amount)
                oRow("SkuEan") = SQLHelper.NullableEan(.SkuEan)
                oRow("Sku") = SQLHelper.NullableBaseGuid(.Sku)
                oRow("PurchaseOrder") = SQLHelper.NullableBaseGuid(.PurchaseOrder)
                oRow("PurchaseOrderItem") = SQLHelper.NullableBaseGuid(.PurchaseOrderItem)

                oRow("SkuNom") = SQLHelper.NullableString(.SkuNom, 60)
                oRow("SkuRef") = SQLHelper.NullableString(.SkuRef, 20)
                oRow("PurchaseOrderId") = SQLHelper.NullableString(.PurchaseOrderId, 20)
                oRow("OrderConfirmation") = SQLHelper.NullableString(.OrderConfirmation, 20)
                oRow("DeliveryNote") = SQLHelper.NullableString(.DeliveryNote, 20)

            End With
        Next

        oDA.Update(oDs)
    End Sub

    Private Shared Sub UpdateExceptions(oInvoiceReceived As DTOInvoiceReceived, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Exception ")
        sb.AppendLine("WHERE Parent='" & oInvoiceReceived.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each ex In oInvoiceReceived.Exceptions
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Parent") = oInvoiceReceived.Guid
            With ex
                oRow("Guid") = .Guid
                oRow("Msg") = SQLHelper.GetStringFromDataReader(.Msg)
                oRow("Cod") = SQLHelper.NullableInt(.Cod)
                oRow("Tag") = SQLHelper.NullableBaseGuid(.Tag)
            End With
        Next
        For Each item In oInvoiceReceived.Items
            For Each ex In item.Exceptions
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Parent") = item.Guid
                With ex
                    oRow("Guid") = .Guid
                    oRow("Msg") = SQLHelper.GetStringFromDataReader(.Msg)
                    oRow("Cod") = SQLHelper.NullableInt(.Cod)
                    oRow("Tag") = SQLHelper.NullableBaseGuid(.Tag)
                End With
            Next
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oInvoiceReceived As DTOInvoiceReceived, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oInvoiceReceived, oTrans)
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


    Shared Sub Delete(oInvoiceReceived As DTOInvoiceReceived, ByRef oTrans As SqlTransaction)
        RestoreEdiFile(oInvoiceReceived, oTrans)
        DeleteExceptions(oInvoiceReceived, oTrans)
        DeleteItems(oInvoiceReceived, oTrans)
        DeleteHeader(oInvoiceReceived, oTrans)
    End Sub

    Shared Sub DeleteHeader(oInvoiceReceived As DTOInvoiceReceived, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE InvoiceReceivedHeader WHERE Guid='" & oInvoiceReceived.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oInvoiceReceived As DTOInvoiceReceived, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE InvoiceReceivedItem WHERE Parent='" & oInvoiceReceived.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteExceptions(oInvoiceReceived As DTOInvoiceReceived, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE Exception ")
        sb.AppendLine("FROM Exception ")
        sb.AppendLine("INNER JOIN InvoiceReceivedItem ON Exception.Parent = InvoiceReceivedItem.Guid ")
        sb.AppendLine("WHERE InvoiceReceivedItem.Parent ='" & oInvoiceReceived.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)

        SQL = "DELETE Exception WHERE Parent ='" & oInvoiceReceived.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub RestoreEdiFile(oInvoiceReceived As DTOInvoiceReceived, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE Edi SET Result=0, ResultGuid=NULL WHERE ResultGuid='" & oInvoiceReceived.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class InvoicesReceivedLoader

    Shared Function All(oConfirmation As DTOImportacio.Confirmation) As List(Of DTOInvoiceReceived)
        Dim retval As New List(Of DTOInvoiceReceived)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each item In oConfirmation.Items
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", item.Guid.ToString())
            idx += 1
        Next

        sb.AppendLine("SELECT InvoiceReceivedHeader.Guid ")
        sb.AppendLine("FROM InvoiceReceivedHeader ")
        sb.AppendLine("INNER JOIN InvoiceReceivedItem on InvoiceReceivedHeader.Guid = InvoiceReceivedItem.Parent ")
        sb.AppendLine("INNER JOIN @Table X ON InvoiceReceivedItem.Guid = X.Guid ")
        sb.AppendLine("GROUP BY InvoiceReceivedHeader.Guid, InvoiceReceivedHeader.Fch, InvoiceReceivedHeader.DocNum ")
        sb.AppendLine("ORDER BY InvoiceReceivedHeader.Fch DESC, InvoiceReceivedHeader.DocNum DESC ")
        Dim SQL As String = sb.ToString
        Dim oInvoiceReceived As New DTOInvoiceReceived()
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            oInvoiceReceived = New DTOInvoiceReceived(oDrd("Guid"))
            retval.Add(oInvoiceReceived)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(Optional year As Integer = 0, Optional importacio As DTOImportacio = Nothing) As List(Of DTOInvoiceReceived)
        Dim retval As New List(Of DTOInvoiceReceived)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT InvoiceReceivedHeader.* ")
        sb.AppendLine(", HdrEx.Guid AS HdrExGuid, HdrEx.Msg AS HdrExMsg, ItemEx.Guid AS ItemExGuid, ItemEx.Msg AS ItemExMsg ")
        sb.AppendLine(", CliGral.FullNom ")
        sb.AppendLine(", ImportHdr.Guid AS ImportGuid, ImportHdr.Yea AS ImportYea, ImportHdr.Id AS ImportId ")
        sb.AppendLine("FROM InvoiceReceivedHeader ")
        sb.AppendLine("LEFT OUTER JOIN CliGral on InvoiceReceivedHeader.Proveidor = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN ImportHdr on InvoiceReceivedHeader.Importacio = ImportHdr.Guid ")
        sb.AppendLine("LEFT OUTER JOIN InvoiceReceivedItem on InvoiceReceivedHeader.Guid = InvoiceReceivedItem.Parent ")
        sb.AppendLine("LEFT OUTER JOIN Exception HdrEx on InvoiceReceivedHeader.Guid = HdrEx.Parent ")
        sb.AppendLine("LEFT OUTER JOIN Exception ItemEx on InvoiceReceivedItem.Guid = ItemEx.Parent ")
        If year > 0 Then
            sb.AppendLine("WHERE YEAR(InvoiceReceivedHeader.Fch)=" & year & " ")
        End If
        If importacio IsNot Nothing Then
            sb.AppendLine("WHERE InvoiceReceivedHeader.Importacio='" & importacio.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY InvoiceReceivedHeader.Fch DESC, InvoiceReceivedHeader.DocNum DESC, InvoiceReceivedHeader.Guid ")
        Dim SQL As String = sb.ToString
        Dim oInvoiceReceived As New DTOInvoiceReceived()
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("Guid").Equals(oInvoiceReceived.Guid) Then
                oInvoiceReceived = New DTOInvoiceReceived(oDrd("Guid"))
                With oInvoiceReceived
                    .DocNum = SQLHelper.GetStringFromDataReader(oDrd("DocNum"))
                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                    .Proveidor = DTOGuidNom.Factory(oDrd("Proveidor"), SQLHelper.GetStringFromDataReader(oDrd("FullNom")))
                    .Cur = SQLHelper.GetCurFromDataReader(oDrd("Cur"))
                    If Not IsDBNull(oDrd("TaxBase")) Then
                        .TaxBase = DTOAmt.Factory(CDec(oDrd("TaxBase")))
                    End If
                    If Not IsDBNull(oDrd("NetTotal")) Then
                        .NetTotal = DTOAmt.Factory(CDec(oDrd("NetTotal")))
                    End If
                    If Not IsDBNull(oDrd("Importacio")) Then
                        Dim formattedImport = String.Format("{0:0000}/{1}", SQLHelper.GetIntegerFromDataReader(oDrd("ImportYea")), SQLHelper.GetIntegerFromDataReader(oDrd("ImportId")))
                        .Importacio = DTOGuidNom.Factory(oDrd("Importacio"), formattedImport)
                    End If
                    .ShipmentId = SQLHelper.GetStringFromDataReader(oDrd("ShipmentId"))
                End With
                retval.Add(oInvoiceReceived)
            End If
            If Not IsDBNull(oDrd("HdrExGuid")) Then
                Dim ex As New DTOException(oDrd("HdrExGuid"))
                If Not oInvoiceReceived.Exceptions.Any(Function(x) x.Guid.Equals(ex.Guid)) Then
                    ex.Msg = SQLHelper.GetStringFromDataReader(oDrd("HdrExMsg"))
                    oInvoiceReceived.Exceptions.Add(ex)
                End If
            End If
            If Not IsDBNull(oDrd("ItemExGuid")) Then
                Dim ex As New DTOException(oDrd("ItemExGuid"))
                If Not oInvoiceReceived.Exceptions.Any(Function(x) x.Guid.Equals(ex.Guid)) Then
                    ex.Msg = SQLHelper.GetStringFromDataReader(oDrd("ItemExMsg"))
                    oInvoiceReceived.Exceptions.Add(ex)
                End If
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ClearConfirmation(exs As List(Of Exception), oImportacio As DTOImportacio) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE InvoiceReceivedItem ")
        sb.AppendLine("SET QtyConfirmed = 0 ")
        sb.AppendLine("FROM InvoiceReceivedItem ")
        sb.AppendLine("INNER JOIN InvoiceReceivedHeader ON InvoiceReceivedItem.Parent = InvoiceReceivedHeader.Guid ")
        sb.AppendLine("WHERE InvoiceReceivedHeader.Importacio = '" & oImportacio.Guid.ToString() & "' ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function

    Shared Function SetImportacio(exs As List(Of Exception), values As List(Of DTOInvoiceReceived), Optional oImportacio As DTOImportacio = Nothing) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each value In values
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", value.Guid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("UPDATE InvoiceReceivedHeader ")
        If oImportacio Is Nothing Then
            sb.AppendLine("SET InvoiceReceivedHeader.Importacio = NULL ")
        Else
            sb.AppendLine("SET InvoiceReceivedHeader.Importacio = '" & oImportacio.Guid.ToString() & "' ")
        End If
        sb.AppendLine("FROM InvoiceReceivedHeader ")
        sb.AppendLine("INNER JOIN @Table X ON InvoiceReceivedHeader.Guid = X.Guid ")
        Dim SQL As String = sb.ToString
        Dim i = SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function


    Shared Function Delete(values As List(Of DTOInvoiceReceived), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(values, oTrans)
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


    Shared Sub Delete(values As List(Of DTOInvoiceReceived), ByRef oTrans As SqlTransaction)
        RestoreEdiFile(values, oTrans)
        DeleteItemExceptions(values, oTrans)
        DeleteHeaderExceptions(values, oTrans)
        DeleteItems(values, oTrans)
        DeleteHeader(values, oTrans)
    End Sub

    Shared Sub DeleteHeader(values As List(Of DTOInvoiceReceived), ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each value In values
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", value.Guid.ToString())
            idx += 1
        Next

        sb.AppendLine("DELETE InvoiceReceivedHeader ")
        sb.AppendLine("FROM InvoiceReceivedHeader ")
        sb.AppendLine("INNER JOIN @Table X ON InvoiceReceivedHeader.Guid = X.Guid ")

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(values As List(Of DTOInvoiceReceived), ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each value In values
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", value.Guid.ToString())
            idx += 1
        Next

        sb.AppendLine("DELETE InvoiceReceivedItem ")
        sb.AppendLine("FROM InvoiceReceivedItem ")
        sb.AppendLine("INNER JOIN @Table X ON InvoiceReceivedItem.Parent = X.Guid ")

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeaderExceptions(values As List(Of DTOInvoiceReceived), ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each value In values
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", value.Guid.ToString())
            idx += 1
        Next

        sb.AppendLine("DELETE Exception ")
        sb.AppendLine("FROM Exception ")
        sb.AppendLine("INNER JOIN @Table X ON Exception.Parent = X.Guid ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItemExceptions(values As List(Of DTOInvoiceReceived), ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each value In values
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", value.Guid.ToString())
            idx += 1
        Next

        sb.AppendLine("DELETE Exception ")
        sb.AppendLine("FROM Exception ")
        sb.AppendLine("INNER JOIN InvoiceReceivedItem ON Exception.Parent = InvoiceReceivedItem.Guid ")
        sb.AppendLine("INNER JOIN @Table X ON InvoiceReceivedItem.Parent = X.Guid ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub RestoreEdiFile(values As List(Of DTOInvoiceReceived), ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each value In values
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", value.Guid.ToString())
            idx += 1
        Next

        sb.AppendLine("UPDATE Edi ")
        sb.AppendLine("SET Result=0, ResultGuid=NULL ")
        sb.AppendLine("FROM Edi ")
        sb.AppendLine("INNER JOIN @Table X ON Edi.ResultGuid = X.Guid ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class
