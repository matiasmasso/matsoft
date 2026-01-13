Public Class EdiversaOrderItemLoader

    Shared Function Find(oGuid As Guid) As DTOEdiversaOrderItem
        Dim retval As DTOEdiversaOrderItem = Nothing
        Dim oEdiversaOrderItem As New DTOEdiversaOrderItem(oGuid)
        If Load(oEdiversaOrderItem) Then
            retval = oEdiversaOrderItem
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaOrderItem As DTOEdiversaOrderItem) As Boolean
        If Not oEdiversaOrderItem.IsLoaded And Not oEdiversaOrderItem.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT EDiversaOrderHeader.Guid, EDiversaOrderHeader.DocNum ")
            sb.AppendLine(", EDiversaOrderHeader.FchDoc, EDiversaOrderHeader.FchDelivery, EDiversaOrderHeader.FchLast ")
            sb.AppendLine(", EDiversaOrderHeader.Tipo, EDiversaOrderHeader.Funcion, EDiversaOrderHeader.Cur ")
            sb.AppendLine(", EDiversaOrderHeader.Proveedor, EDiversaOrderHeader.Comprador, EDiversaOrderHeader.ReceptorMercancia ")
            sb.AppendLine(", EDiversaOrderHeader.CompradorEAN, EDiversaOrderHeader.ReceptorMercanciaEAN ")
            sb.AppendLine(", EDiversaOrderHeader.Centro, EDiversaOrderHeader.Departamento ")
            sb.AppendLine(", EDiversaOrderHeader.Eur, EDiversaOrderHeader.Result, EDiversaOrderHeader.Obs ")
            sb.AppendLine(", EdiversaOrderItem.Guid AS ItemGuid, EdiversaOrderItem.Lin, EdiversaOrderItem.Ean ")
            sb.AppendLine(", EdiversaOrderItem.RefProveidor, EdiversaOrderItem.RefClient, EdiversaOrderItem.Dsc ")
            sb.AppendLine(", EdiversaOrderItem.Qty, EdiversaOrderItem.Preu, EdiversaOrderItem.Dto, EdiversaOrderItem.Sku ")
            sb.AppendLine(", EdiversaOrderItem.SkipPreuValidationUser, EdiversaOrderItem.SkipPreuValidationFch ")
            sb.AppendLine(", EdiversaOrderItem.SkipDtoValidationUser, EdiversaOrderItem.SkipDtoValidationFch ")
            sb.AppendLine(", EdiversaOrderItem.SkipItemUser, EdiversaOrderItem.SkipItemFch ")
            sb.AppendLine(", Edi.Result AS Status ")
            sb.AppendLine(", Comprador.FullNom AS CompradorNom, ReceptorMercancia.FullNom AS ReceptorMercanciaNom, VwCcxOrMe.Ccx as Customer ")
            sb.AppendLine(", VwSkuNom.* ")
            sb.AppendLine("FROM EDiversaOrderHeader ")
            sb.AppendLine("INNER JOIN Edi ON EDiversaOrderHeader.Guid = Edi.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwCcxOrMe ON EDiversaOrderHeader.Comprador=VwCcxOrMe.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS Comprador ON EDiversaOrderHeader.Comprador=Comprador.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS ReceptorMercancia ON EDiversaOrderHeader.ReceptorMercancia=ReceptorMercancia.Guid ")
            sb.AppendLine("INNER JOIN EdiversaOrderItem ON EDiversaOrderHeader.Guid=EdiversaOrderItem.Parent ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON EdiversaOrderItem.Sku = VwSkuNom.SkuGuid ")
            sb.AppendLine("WHERE EdiversaOrderItem.Guid='" & oEdiversaOrderItem.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oEDiversaOrder As New DTOEdiversaOrder(oDrd("Guid"))
                With oEDiversaOrder
                    .DocNum = oDrd("DocNum")
                    .FchDoc = oDrd("FchDoc")
                    If Not IsDBNull(oDrd("FchDelivery")) Then
                        .FchDeliveryMin = oDrd("FchDelivery")
                    End If
                    If Not IsDBNull(oDrd("FchLast")) Then
                        .FchDeliveryMax = oDrd("FchLast")
                    End If
                    .Tipo = oDrd("Tipo")
                    .Funcion = oDrd("Funcion")
                    .Cur = DTOCur.Factory(oDrd("Cur"))

                    If Not IsDBNull(oDrd("Customer")) Then
                        .Customer = New DTOCustomer(oDrd("Customer"))
                    End If
                    .Proveedor = New DTOContact(oDrd("Proveedor"))

                    .CompradorEAN = DTOEan.Factory(oDrd("CompradorEAN"))
                    .Centro = SQLHelper.GetStringFromDataReader(oDrd("Centro"))
                    .Departamento = SQLHelper.GetStringFromDataReader(oDrd("Departamento"))
                    If Not IsDBNull(oDrd("Comprador")) Then
                        .Comprador = New DTOCustomer(oDrd("Comprador"))
                        .Comprador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("CompradorNom"))
                    End If

                    .ReceptorMercanciaEAN = DTOEan.Factory(oDrd("ReceptorMercanciaEAN"))
                    If Not IsDBNull(oDrd("ReceptorMercancia")) Then
                        .ReceptorMercancia = New DTOContact(DirectCast(oDrd("ReceptorMercancia"), Guid))
                        .ReceptorMercancia.FullNom = SQLHelper.GetStringFromDataReader(oDrd("ReceptorMercanciaNom"))
                    End If

                    .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                    .EdiversaFile = New DTOEdiversaFile(oDrd("Guid"))
                    .EdiversaFile.Result = oDrd("Status")
                    If Not IsDBNull(oDrd("Result")) Then
                        .Result = oDrd("Result")
                    End If
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .Items = New List(Of DTOEdiversaOrderItem)

                    .IsLoaded = True
                End With

                With oEdiversaOrderItem
                    .Parent = oEDiversaOrder
                    .Lin = oDrd("Lin")
                    .Ean = DTOEan.Factory(oDrd("Ean"))
                    .RefProveidor = SQLHelper.GetStringFromDataReader(oDrd("RefProveidor"))
                    .RefClient = SQLHelper.GetStringFromDataReader(oDrd("RefClient"))
                    .Dsc = oDrd("Dsc")
                    .Qty = oDrd("Qty")
                    .Preu = SQLHelper.GetAmtFromDataReader(oDrd("Preu"))
                    .Dto = oDrd("Dto")
                    .Sku = SQLHelper.GetProductFromDataReader(oDrd)

                    If Not IsDBNull(oDrd("SkipPreuValidationUser")) Then
                        .SkipPreuValidationUser = New DTOUser(DirectCast(oDrd("SkipPreuValidationUser"), Guid))
                    End If
                    If Not IsDBNull(oDrd("SkipDtoValidationUser")) Then
                        .SkipDtoValidationUser = New DTOUser(DirectCast(oDrd("SkipDtoValidationUser"), Guid))
                    End If
                    If Not IsDBNull(oDrd("SkipItemUser")) Then
                        .SkipItemUser = New DTOUser(DirectCast(oDrd("SkipItemUser"), Guid))
                    End If

                    .SkipPreuValidationFch = SQLHelper.GetFchFromDataReader(oDrd("SkipPreuValidationFch"))
                    .SkipDtoValidationFch = SQLHelper.GetFchFromDataReader(oDrd("SkipDtoValidationFch"))
                    .SkipItemFch = SQLHelper.GetFchFromDataReader(oDrd("SkipItemFch"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oEdiversaOrderItem.IsLoaded
        Return retval
    End Function


    Shared Function Update(oEdiversaOrderItem As DTOEdiversaOrderItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEdiversaOrderItem, oTrans)
            oTrans.Commit()
            oEdiversaOrderItem.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oEdiversaOrderItem As DTOEdiversaOrderItem, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EdiversaOrderItem ")
        sb.AppendLine("WHERE Guid='" & oEdiversaOrderItem.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oEdiversaOrderItem.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oEdiversaOrderItem
            oRow("Parent") = .Parent.Guid
            oRow("Lin") = .Lin
            oRow("Ean") = SQLHelper.NullableEan(.Ean)
            oRow("RefProveidor") = SQLHelper.NullableString(.RefProveidor)
            oRow("RefClient") = SQLHelper.NullableString(.RefClient)
            oRow("Dsc") = .Dsc
            oRow("Qty") = .Qty
            oRow("Preu") = SQLHelper.NullableAmt(.Preu)
            oRow("Dto") = .Dto
            oRow("Sku") = SQLHelper.NullableBaseGuid(.Sku)
            oRow("SkipPreuValidationUser") = SQLHelper.NullableBaseGuid(.SkipPreuValidationUser)
            oRow("SkipDtoValidationUser") = SQLHelper.NullableBaseGuid(.SkipDtoValidationUser)
            oRow("SkipItemUser") = SQLHelper.NullableBaseGuid(.SkipItemUser)

            oRow("SkipPreuValidationFch") = SQLHelper.NullableFch(.SkipPreuValidationFch)
            oRow("SkipDtoValidationFch") = SQLHelper.NullableFch(.SkipDtoValidationFch)
            oRow("SkipItemFch") = SQLHelper.NullableFch(.SkipItemFch)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oEdiversaOrderItem As DTOEdiversaOrderItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oEdiversaOrderItem, oTrans)
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


    Shared Sub Delete(oEdiversaOrderItem As DTOEdiversaOrderItem, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaOrderItem WHERE Guid='" & oEdiversaOrderItem.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class
