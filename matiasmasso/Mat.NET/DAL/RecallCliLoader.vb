Public Class RecallCliLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTORecallCli
        Dim retval As DTORecallCli = Nothing
        Dim oRecallCli As New DTORecallCli(oGuid)
        If Load(oRecallCli) Then
            retval = oRecallCli
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oRecallCli As DTORecallCli) As Boolean
        If Not oRecallCli.IsLoaded And Not oRecallCli.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Recall.Nom, Recall.Fch ")
            sb.AppendLine(", RecallCli.* ")
            sb.AppendLine(", CliGral.RaoSocial, CliGral.NomCom ")
            sb.AppendLine(", Country.Nom_Esp AS CountryEsp ")
            sb.AppendLine(", Pdc.Pdc, Pdc.Fch AS PdcFch ")
            sb.AppendLine(", Alb.Alb, Alb.Fch AS AlbFch ")
            sb.AppendLine(", VwSkuNom.*, RecallProduct.SerialNumber, RecallProduct.Sku ")
            sb.AppendLine("FROM RecallCli ")
            sb.AppendLine("INNER JOIN Recall ON RecallCli.Recall = Recall.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON RecallCli.Customer = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Country ON RecallCli.Country = Country.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pdc ON RecallCli.PurchaseOrder = Pdc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Alb ON RecallCli.Delivery = Alb.Guid ")
            sb.AppendLine("LEFT OUTER JOIN RecallProduct ON RecallCli.Guid = RecallProduct.RecallCli ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON RecallProduct.Sku = VwSkuNom.SkuGuid ")
            sb.AppendLine("WHERE RecallCli.Guid='" & oRecallCli.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY RecallProduct.SerialNumber ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oRecallCli.IsLoaded Then
                    With oRecallCli
                        .Recall = New DTORecall(oDrd("Recall"))
                        With .Recall
                            .Fch = oDrd("Fch")
                            .Nom = oDrd("Nom")
                        End With
                        .ContactNom = SQLHelper.GetStringFromDataReader(oDrd("ContactNom"))
                        .ContactTel = SQLHelper.GetStringFromDataReader(oDrd("ContactTel"))
                        .ContactEmail = SQLHelper.GetStringFromDataReader(oDrd("ContactEmail"))
                        If Not IsDBNull(oDrd("Customer")) Then
                            .Customer = New DTOCustomer(oDrd("Customer"))
                            .Customer.Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                            .Customer.NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                        End If
                        .Address = SQLHelper.GetStringFromDataReader(oDrd("Address"))
                        .Zip = SQLHelper.GetStringFromDataReader(oDrd("Zip"))
                        .Location = SQLHelper.GetStringFromDataReader(oDrd("Location"))
                        If Not IsDBNull(oDrd("Country")) Then
                            .Country = New DTOCountry(oDrd("Country"))
                            .Country.LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp")
                        End If
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
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("SerialNumber")) Then
                    Dim oProduct As New DTORecallProduct
                    oProduct.Sku = SQLHelper.GetProductFromDataReader(oDrd)
                    oProduct.SerialNumber = oDrd("SerialNumber")
                    oRecallCli.Products.Add(oProduct)
                End If
            Loop

            oDrd.Close()
        End If
        Dim retval As Boolean = oRecallCli.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRecallCli As DTORecallCli, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRecallCli, oTrans)
            oTrans.Commit()
            oRecallCli.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oRecallCli As DTORecallCli, ByRef oTrans As SqlTransaction)
        UpdateHeader(oRecallCli, oTrans)
        DeleteProducts(oRecallCli, oTrans)
        UpdateProducts(oRecallCli, oTrans)
    End Sub


    Shared Sub UpdateHeader(oRecallCli As DTORecallCli, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM RecallCli ")
        sb.AppendLine("WHERE RecallCli.Guid='" & oRecallCli.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oRecallCli.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oRecallCli
            oRow("Recall") = SQLHelper.NullableBaseGuid(.Recall)
            oRow("ContactNom") = SQLHelper.NullableString(.ContactNom)
            oRow("ContactTel") = SQLHelper.NullableString(.ContactTel)
            oRow("ContactEmail") = SQLHelper.NullableString(.ContactEmail)
            oRow("Customer") = SQLHelper.NullableBaseGuid(.Customer)
            oRow("Address") = SQLHelper.NullableString(.Address)
            oRow("Zip") = SQLHelper.NullableString(.Zip)
            oRow("Location") = SQLHelper.NullableString(.Location)
            oRow("Country") = SQLHelper.NullableBaseGuid(.Country)
            oRow("FchVivace") = SQLHelper.NullableFch(.FchVivace)
            oRow("RegMuelle") = SQLHelper.NullableString(.RegMuelle)
            oRow("PurchaseOrder") = SQLHelper.NullableBaseGuid(.PurchaseOrder)
            oRow("Delivery") = SQLHelper.NullableBaseGuid(.Delivery)
            SQLHelper.SetUsrLog(.UsrLog, oRow)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateProducts(oRecallCli As DTORecallCli, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM RecallProduct ")
        sb.AppendLine("WHERE RecallCli='" & oRecallCli.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oProduct As DTORecallProduct In oRecallCli.Products
            Dim oRow As DataRow = oTb.NewRow
            oRow("RecallCli") = SQLHelper.NullableBaseGuid(oRecallCli)
            oRow("Sku") = SQLHelper.NullableBaseGuid(oProduct.Sku)
            oRow("SerialNumber") = SQLHelper.GetStringFromDataReader(oProduct.SerialNumber)
            oTb.Rows.Add(oRow)
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oRecallCli As DTORecallCli, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRecallCli, oTrans)
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

    Shared Sub Delete(oRecallCli As DTORecallCli, ByRef oTrans As SqlTransaction)
        DeleteProducts(oRecallCli, oTrans)
        DeleteHeader(oRecallCli, oTrans)
    End Sub


    Shared Sub DeleteProducts(oRecallCli As DTORecallCli, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE RecallProduct WHERE RecallProduct.RecallCli='" & oRecallCli.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oRecallCli As DTORecallCli, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE RecallCli WHERE RecallCli.Guid='" & oRecallCli.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class RecallClisLoader

    Shared Function All(oRecall As DTORecall) As List(Of DTORecallCli)
        Dim retval As New List(Of DTORecallCli)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RecallCli.* ")
        sb.AppendLine(", Pdc.Pdc, Pdc.Fch AS PdcFch ")
        sb.AppendLine(", Alb.Alb, Alb.Fch AS AlbFch ")
        sb.AppendLine("FROM RecallCli ")
        sb.AppendLine("LEFT OUTER JOIN Pdc ON RecallCli.PurchaseOrder = Pdc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Alb ON RecallCli.Delivery = Alb.Guid ")
        sb.AppendLine("WHERE RecallCli.Recall = '" & oRecall.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY RecallCli.FchCreated DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTORecallCli(oDrd("Guid"))
            With item
                .Recall = oRecall
                .ContactNom = SQLHelper.GetStringFromDataReader(oDrd("ContactNom"))
                .ContactTel = SQLHelper.GetStringFromDataReader(oDrd("ContactTel"))
                .ContactEmail = SQLHelper.GetStringFromDataReader(oDrd("ContactEmail"))
                If Not IsDBNull(oDrd("Customer")) Then
                    .Customer = New DTOCustomer(oDrd("Customer"))
                End If
                .Address = SQLHelper.GetStringFromDataReader(oDrd("Address"))
                .Zip = SQLHelper.GetStringFromDataReader(oDrd("Zip"))
                .Location = SQLHelper.GetStringFromDataReader(oDrd("Location"))
                If Not IsDBNull(oDrd("Country")) Then
                    .Country = New DTOCountry(oDrd("Country"))
                End If
                .FchVivace = SQLHelper.GetFchFromDataReader(oDrd("FchVivace"))
                .RegMuelle = SQLHelper.GetStringFromDataReader("RegMuelle")
                If Not IsDBNull(oDrd("PurchaseOrder")) Then
                    .PurchaseOrder = New DTOPurchaseOrder(oDrd("PurchaseOrder"))
                    With .PurchaseOrder
                        .Num = oDrd("Pdc")
                        .Fch = SQLHelper.GetFchFromDataReader(oDrd("PdcFch"))
                    End With
                End If
                If Not IsDBNull(oDrd("Delivery")) Then
                    .Delivery = New DTODelivery(oDrd("Delivery"))
                    With .Delivery
                        .Id = oDrd("Alb")
                        .Fch = SQLHelper.GetFchFromDataReader(oDrd("AlbFch"))
                    End With
                End If
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class