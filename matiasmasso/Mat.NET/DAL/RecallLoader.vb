Public Class RecallLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTORecall
        Dim retval As DTORecall = Nothing
        Dim oRecall As New DTORecall(oGuid)
        If Load(oRecall) Then
            retval = oRecall
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oRecall As DTORecall) As Boolean
        If Not oRecall.IsLoaded And Not oRecall.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Recall.Guid AS RecallGuid, Recall.Fch, Recall.Nom ")
            sb.AppendLine(", RecallCli.Guid AS RecallCliGuid, RecallCli.FchVivace, RecallCli.FchCreated, RecallCli.UsrCreated ")
            sb.AppendLine(", RecallCli.RegMuelle, RecallCli.PurchaseOrder, RecallCli.Delivery ")
            sb.AppendLine(", RecallCli.Customer, CliGral.FullNom, CliGral.RaoSocial ")
            sb.AppendLine(", Pdc.Pdc, Pdc.Fch AS PdcFch ")
            sb.AppendLine(", Alb.Alb, Alb.Fch AS AlbFch ")
            sb.AppendLine(", RecallProduct.Sku, RecallProduct.SerialNumber, VwSkuNom.* ")
            sb.AppendLine("FROM Recall ")
            sb.AppendLine("LEFT OUTER JOIN RecallCli ON Recall.Guid = RecallCli.Recall ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON RecallCli.Customer = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pdc ON RecallCli.PurchaseOrder = Pdc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Alb ON RecallCli.Delivery = Alb.Guid ")
            sb.AppendLine("LEFT OUTER JOIN RecallProduct ON RecallCli.Guid = RecallProduct.RecallCli ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON RecallProduct.Sku = VwSkuNom.SkuGuid ")
            sb.AppendLine("WHERE Recall.Guid='" & oRecall.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY RecallCli.FchCreated DESC, RecallCli.Guid, RecallProduct.SerialNumber ")

            Dim oRecallCli As New DTORecallCli
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                With oRecall
                    If Not .IsLoaded Then
                        .Nom = oDrd("Nom")
                        .Fch = oDrd("Fch")
                        .Clis = New List(Of DTORecallCli)
                        .IsLoaded = True
                    End If
                End With

                If Not oRecallCli.Guid.Equals(oDrd("RecallCliGuid")) Then
                    oRecallCli = New DTORecallCli(oDrd("RecallCliGuid"))
                    With oRecallCli
                        .Customer = New DTOCustomer(oDrd("Customer"))
                        .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                        .Customer.Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                        .FchVivace = SQLHelper.GetFchFromDataReader(oDrd("FchVivace"))
                        .RegMuelle = SQLHelper.GetStringFromDataReader(oDrd("RegMuelle"))
                        If Not IsDBNull(oDrd("PurchaseOrder")) Then
                            .PurchaseOrder = New DTOPurchaseOrder(oDrd("PurchaseOrder"))
                            With .PurchaseOrder
                                .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Pdc"))
                                .Fch = SQLHelper.GetFchFromDataReader(oDrd("PdcFch"))
                            End With
                        End If
                        If Not IsDBNull(oDrd("Delivery")) Then
                            .Delivery = New DTODelivery(oDrd("Delivery"))
                            With .Delivery
                                .Id = SQLHelper.GetIntegerFromDataReader(oDrd("Alb"))
                                .Fch = SQLHelper.GetFchFromDataReader(oDrd("AlbFch"))
                            End With
                        End If
                        .Products = New List(Of DTORecallProduct)
                        .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    End With
                    oRecall.Clis.Add(oRecallCli)
                End If

                If Not IsDBNull(oDrd("Sku")) Then
                    Dim item As New DTORecallProduct()
                    With item
                        .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                        .SerialNumber = SQLHelper.GetStringFromDataReader(oDrd("SerialNumber"))
                    End With
                    oRecallCli.Products.Add(item)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oRecall.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRecall As DTORecall, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRecall, oTrans)
            oTrans.Commit()
            oRecall.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oRecall As DTORecall, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Recall ")
        sb.AppendLine("WHERE Guid='" & oRecall.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oRecall.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oRecall
            oRow("Nom") = .Nom
            oRow("Fch") = .Fch
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oRecall As DTORecall, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRecall, oTrans)
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


    Shared Sub Delete(oRecall As DTORecall, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Recall WHERE Guid='" & oRecall.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class RecallsLoader

    Shared Function All() As List(Of DTORecall)
        Dim retval As New List(Of DTORecall)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Recall.Guid AS RecallGuid, Recall.Nom AS RecallNom, Recall.Fch AS RecallFch ")
        sb.AppendLine(", RecallCli.Guid AS RecallCliGuid, RecallProduct.Sku ")
        sb.AppendLine("FROM Recall ")
        sb.AppendLine("LEFT OUTER JOIN RecallCli ON Recall.Guid = RecallCli.Recall ")
        sb.AppendLine("LEFT OUTER JOIN RecallProduct ON RecallCli.Guid = RecallProduct.RecallCli ")
        sb.AppendLine("ORDER BY Recall.Nom")

        Dim oRecall As New DTORecall
        Dim oRecallCli As New DTORecallCli
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oRecall.Guid.Equals(oDrd("RecallGuid")) Then
                oRecall = New DTORecall(oDrd("RecallGuid"))
                With oRecall
                    .Nom = oDrd("RecallNom")
                    .Fch = oDrd("RecallFch")
                    .Clis = New List(Of DTORecallCli)
                End With
                retval.Add(oRecall)
            End If
            If Not oRecallCli.Guid.Equals(oDrd("RecallCliGuid")) Then
                oRecallCli = New DTORecallCli(oDrd("RecallCliGuid"))
                oRecallCli.Products = New List(Of DTORecallProduct)
                oRecall.Clis.Add(oRecallCli)
            End If
            If Not IsDBNull(oDrd("Sku")) Then
                Dim item As New DTORecallProduct()
                item.Sku = New DTOProductSku(oDrd("Sku"))
                oRecallCli.Products.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
