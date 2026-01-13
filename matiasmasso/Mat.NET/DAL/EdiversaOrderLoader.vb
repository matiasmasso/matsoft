Public Class EdiversaOrderLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOEdiversaOrder
        Dim retval As DTOEdiversaOrder = Nothing
        Dim oEDiversaOrder As New DTOEdiversaOrder(oGuid)
        If Load(oEDiversaOrder) Then
            retval = oEDiversaOrder
        End If
        Return retval
    End Function


    Shared Function Load(ByRef oEDiversaOrder As DTOEdiversaOrder) As Boolean
        Dim retval As Boolean
        If Not oEDiversaOrder.IsLoaded And Not oEDiversaOrder.IsNew Then


            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT EDiversaOrderHeader.Guid, EDiversaOrderHeader.DocNum ")
            sb.AppendLine(", EDiversaOrderHeader.FchDoc, EDiversaOrderHeader.FchDelivery, EDiversaOrderHeader.FchLast ")
            sb.AppendLine(", EDiversaOrderHeader.Tipo, EDiversaOrderHeader.Funcion, EDiversaOrderHeader.Cur ")
            sb.AppendLine(", EDiversaOrderHeader.Nadms, EDiversaOrderHeader.Proveedor, EDiversaOrderHeader.Comprador, EDiversaOrderHeader.FacturarA, EDiversaOrderHeader.ReceptorMercancia ")
            sb.AppendLine(", EDiversaOrderHeader.CompradorEAN, EDiversaOrderHeader.FacturarAEAN, EDiversaOrderHeader.ReceptorMercanciaEAN ")
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
            sb.AppendLine(", FacturarA.FullNom AS FacturarANom ")
            sb.AppendLine(", VwSkuNom.* ")
            sb.AppendLine(", Ex1.Guid AS Ex1Guid, Ex1.Cod as Ex1Cod, Ex1.TagGuid AS Ex1TagGuid, Ex1.TagCod AS Ex1TagCod, Ex1.Msg AS Ex1Msg ")
            sb.AppendLine(", Ex2.Guid AS Ex2Guid, Ex2.Cod as Ex2Cod, Ex2.TagGuid AS Ex2TagGuid, Ex2.TagCod AS Ex2TagCod, Ex2.Msg AS Ex2Msg ")
            sb.AppendLine("FROM EDiversaOrderHeader ")
            sb.AppendLine("INNER JOIN Edi ON EDiversaOrderHeader.Guid = Edi.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwCcxOrMe ON EDiversaOrderHeader.Comprador=VwCcxOrMe.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS Comprador ON EDiversaOrderHeader.Comprador=Comprador.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS FacturarA ON EDiversaOrderHeader.FacturarA=FacturarA.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS ReceptorMercancia ON EDiversaOrderHeader.ReceptorMercancia=ReceptorMercancia.Guid ")
            sb.AppendLine("LEFT OUTER JOIN EdiversaOrderItem ON EDiversaOrderHeader.Guid=EdiversaOrderItem.Parent ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON EdiversaOrderItem.Sku = VwSkuNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN EdiversaExceptions AS Ex1 ON EdiversaOrderHeader.Guid = Ex1.Parent ")
            sb.AppendLine("LEFT OUTER JOIN EdiversaExceptions AS Ex2 ON EdiversaOrderItem.Guid = Ex2.Parent ")
            sb.AppendLine("WHERE EdiversaOrderHeader.Guid='" & oEDiversaOrder.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY EdiversaOrderItem.Lin ")

            Dim SQL As String = sb.ToString
            Dim oItem As New DTOEdiversaOrderItem
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oEDiversaOrder.IsLoaded Then
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
                        .Cur = SQLHelper.GetCurFromDataReader(oDrd("Cur"))

                        If Not IsDBNull(oDrd("Customer")) Then
                            .Customer = New DTOCustomer(oDrd("Customer"))
                        End If
                        .Proveedor = New DTOContact(DirectCast(oDrd("Proveedor"), Guid))

                        If Not IsDBNull(oDrd("FacturarA")) Then
                            .FacturarA = New DTOCustomer(oDrd("FacturarA"))
                            .FacturarA.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FacturarANom"))
                        End If

                        .NADMS = SQLHelper.GetEANFromDataReader(oDrd("NADMS"))
                        .FacturarAEAN = SQLHelper.GetEANFromDataReader(oDrd("FacturarAEAN"))
                        .CompradorEAN = DTOEan.Factory(oDrd("CompradorEAN"))
                        .ReceptorMercanciaEAN = DTOEan.Factory(oDrd("ReceptorMercanciaEAN"))

                        .Centro = SQLHelper.GetStringFromDataReader(oDrd("Centro"))
                        .Departamento = SQLHelper.GetStringFromDataReader(oDrd("Departamento"))
                        If Not IsDBNull(oDrd("Comprador")) Then
                            Dim oMatchingComprador As DTOEdiversaOrder = Nothing
                            .Comprador = New DTOCustomer(DirectCast(oDrd("Comprador"), Guid))
                            .Comprador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("CompradorNom"))
                        End If

                        If Not IsDBNull(oDrd("ReceptorMercancia")) Then
                            Dim oMatchingReceptor As DTOEdiversaOrder = Nothing
                            .ReceptorMercancia = New DTOContact(DirectCast(oDrd("ReceptorMercancia"), Guid))
                            .ReceptorMercancia.FullNom = SQLHelper.GetStringFromDataReader(oDrd("ReceptorMercanciaNom"))
                        End If

                        .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                        .EdiversaFile = New DTOEdiversaFile(oDrd("Guid"))
                        .EdiversaFile.Result = oDrd("Status")
                        If Not IsDBNull(oDrd("Result")) Then
                            .Result = New DTOPurchaseOrder(oDrd("Result"))
                        End If
                        .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                        .IsLoaded = True
                    End With
                End If


                If Not IsDBNull(oDrd("Ex1Guid")) Then
                    If Not oEDiversaOrder.Exceptions.Any(Function(x) x.Guid.Equals(oDrd("Ex1Guid"))) Then
                        Dim ex = DTOEdiversaException.Factory(oDrd("Ex1Cod"), Nothing, oDrd("Ex1Msg"))
                        With ex
                            .Guid = oDrd("Ex1Guid")
                            .Cod = oDrd("Ex1Cod")
                            .TagCod = oDrd("Ex1TagCod")
                            If Not IsDBNull(oDrd("Ex1TagGuid")) Then
                                .Tag = New DTOBaseGuid(oDrd("Ex1TagGuid"))
                            End If
                        End With
                        oEDiversaOrder.Exceptions.Add(ex)
                    End If
                End If

                If Not IsDBNull(oDrd("ItemGuid")) Then
                    If Not oItem.Guid.Equals(oDrd("ItemGuid")) Then
                        oItem = New DTOEdiversaOrderItem(oDrd("ItemGuid"))
                        With oItem
                            .Parent = oEDiversaOrder
                            .Lin = oDrd("Lin")
                            .Ean = DTOEan.Factory(oDrd("Ean"))
                            .RefProveidor = SQLHelper.GetStringFromDataReader(oDrd("RefProveidor"))
                            .RefClient = SQLHelper.GetStringFromDataReader(oDrd("RefClient"))
                            .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
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
                            oEDiversaOrder.Items.Add(oItem)
                        End With
                    End If
                End If

                If Not IsDBNull(oDrd("Ex2Guid")) Then
                    If Not oEDiversaOrder.Exceptions.Any(Function(x) x.Guid.Equals(oDrd("Ex2Guid"))) Then
                        Dim ex = DTOEdiversaException.Factory(oDrd("Ex2Cod"), Nothing, oDrd("Ex2Msg"))
                        With ex
                            .Guid = oDrd("Ex2Guid")
                            .Cod = oDrd("Ex2Cod")
                            .TagCod = oDrd("Ex2TagCod")
                            If Not IsDBNull(oDrd("Ex2TagGuid")) Then
                                .Tag = New DTOBaseGuid(oDrd("Ex2TagGuid"))
                            End If
                        End With
                        oItem.Exceptions.Add(ex)
                    End If
                End If
            Loop
            oDrd.Close()
        End If
        retval = oEDiversaOrder.IsLoaded
        Return retval
    End Function


    Shared Function searchByDocNum(docnum As String) As List(Of DTOEdiversaOrder)
        Dim retval As New List(Of DTOEdiversaOrder)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT EDiversaOrderHeader.Guid, EDiversaOrderHeader.DocNum ")
        sb.AppendLine(", EDiversaOrderHeader.FchDoc, EDiversaOrderHeader.FchDelivery, EDiversaOrderHeader.FchLast ")
        sb.AppendLine(", EDiversaOrderHeader.Tipo, EDiversaOrderHeader.Funcion, EDiversaOrderHeader.Cur ")
        sb.AppendLine(", EDiversaOrderHeader.Proveedor, EDiversaOrderHeader.Comprador, EDiversaOrderHeader.FacturarA, EDiversaOrderHeader.ReceptorMercancia ")
        sb.AppendLine(", EDiversaOrderHeader.NADMS, EDiversaOrderHeader.CompradorEAN, EDiversaOrderHeader.ReceptorMercanciaEAN ")
        sb.AppendLine(", EDiversaOrderHeader.Centro, EDiversaOrderHeader.Departamento ")
        sb.AppendLine(", EDiversaOrderHeader.Eur, EDiversaOrderHeader.Result, EDiversaOrderHeader.Obs ")
        sb.AppendLine(", Edi.Result AS Status ")
        sb.AppendLine(", Comprador.FullNom AS CompradorNom, ReceptorMercancia.FullNom AS ReceptorMercanciaNom, VwCcxOrMe.Ccx as Customer ")
        sb.AppendLine(", FacturarA.FullNom AS FacturarANom ")
        sb.AppendLine("FROM EDiversaOrderHeader ")
        sb.AppendLine("INNER JOIN Edi ON EDiversaOrderHeader.Guid = Edi.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwCcxOrMe ON EDiversaOrderHeader.Comprador=VwCcxOrMe.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS Comprador ON EDiversaOrderHeader.Comprador=Comprador.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS FacturarA ON EDiversaOrderHeader.FacturarA=FacturarA.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS ReceptorMercancia ON EDiversaOrderHeader.ReceptorMercancia=ReceptorMercancia.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pdc ON EDiversaOrderHeader.Result = Pdc.Guid ")
        sb.AppendLine("WHERE EdiversaOrderHeader.DocNum='" & docnum & "' ")
        sb.AppendLine("AND EDiversaOrderHeader.Result  is not null AND Pdc.Guid IS NULL ")
        sb.AppendLine("ORDER BY EDiversaOrderHeader.FchDoc DESC ")

        Dim SQL As String = sb.ToString
        Dim oItem As New DTOEdiversaOrderItem
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oOrder As New DTOEdiversaOrder
        Do While oDrd.Read
            If Not oOrder.Guid.Equals(oDrd("Guid")) Then
                oOrder = New DTOEdiversaOrder(oDrd("Guid"))
                With oOrder
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
                    .Cur = SQLHelper.GetCurFromDataReader(oDrd("Cur"))

                    If Not IsDBNull(oDrd("Customer")) Then
                        .Customer = New DTOCustomer(oDrd("Customer"))
                    End If
                    .Proveedor = New DTOContact(DirectCast(oDrd("Proveedor"), Guid))

                    .NADMS = DTOEan.Factory(oDrd("NADMS"))
                    .CompradorEAN = DTOEan.Factory(oDrd("CompradorEAN"))
                    .Centro = SQLHelper.GetStringFromDataReader(oDrd("Centro"))
                    .Departamento = SQLHelper.GetStringFromDataReader(oDrd("Departamento"))
                    If Not IsDBNull(oDrd("Comprador")) Then
                        Dim oMatchingComprador As DTOEdiversaOrder = Nothing
                        .Comprador = New DTOCustomer(DirectCast(oDrd("Comprador"), Guid))
                        .Comprador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("CompradorNom"))
                    End If

                    If Not IsDBNull(oDrd("ReceptorMercancia")) Then
                        .ReceptorMercanciaEAN = DTOEan.Factory(oDrd("ReceptorMercanciaEAN"))
                        Dim oMatchingReceptor As DTOEdiversaOrder = Nothing
                        .ReceptorMercancia = New DTOContact(DirectCast(oDrd("ReceptorMercancia"), Guid))
                        .ReceptorMercancia.FullNom = SQLHelper.GetStringFromDataReader(oDrd("ReceptorMercanciaNom"))
                    End If

                    If Not IsDBNull(oDrd("FacturarA")) Then
                        .FacturarA = New DTOCustomer(oDrd("FacturarA"))
                        .FacturarA.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FacturarANom"))
                    End If

                    .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                    .EdiversaFile = New DTOEdiversaFile(oDrd("Guid"))
                    .EdiversaFile.result = oDrd("Status")
                    If Not IsDBNull(oDrd("Result")) Then
                        .Result = New DTOPurchaseOrder(oDrd("Result"))
                    End If
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .IsLoaded = True
                    retval.Add(oOrder)
                End With
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oEDiversaOrder As DTOEdiversaOrder, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEDiversaOrder, oTrans)
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

    Shared Function Update(oEDiversaOrder As DTOEdiversaOrder) As DTOTaskResult
        Dim retval As New DTOTaskResult

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEDiversaOrder, oTrans)
            oTrans.Commit()
            retval.Succeed("Comanda Edi desada correctament")
        Catch ex As Exception
            oTrans.Rollback()
            retval.Fail("Error al desar la comanda Edi")
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Function Update(oEDiversaOrder As DTOEdiversaOrder, oEdiversaFile As DTOEdiversaFile, oPurchaseOrder As DTOPurchaseOrder, ByRef exs As List(Of DTOEdiversaException)) As Boolean
        Dim retval As Boolean

        'es fa servir quan processem un fitxer de comanda de Edi

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            PurchaseOrderLoader.Update(oPurchaseOrder, oTrans)
            EdiversaFileLoader.Update(oEdiversaFile, oTrans)
            Update(oEDiversaOrder, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, oEDiversaOrder, ex.Message))
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oEDiversaOrder As DTOEdiversaOrder, ByRef oTrans As SqlTransaction)
        DeleteExceptions(oEDiversaOrder, oTrans)
        UpdateHeader(oEDiversaOrder, oTrans)
        UpdateItems(oEDiversaOrder, oTrans)
        UpdateExceptions(oEDiversaOrder, oTrans)
    End Sub

    Shared Sub DeleteExceptions(oEdiversaOrder As DTOEdiversaOrder, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE EdiversaExceptions ")
        sb.AppendLine("FROM EdiversaExceptions ")
        sb.AppendLine("INNER JOIN EdiversaOrderItem ON EdiversaExceptions.Parent = EdiversaOrderItem.Guid ")
        sb.AppendLine("WHERE EdiversaOrderItem.Parent ='" & oEdiversaOrder.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)

        sb.AppendLine("DELETE EdiversaExceptions ")
        sb.AppendLine("WHERE EdiversaExceptions.Parent ='" & oEdiversaOrder.Guid.ToString & "' ")
        SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub UpdateHeader(oEDiversaOrder As DTOEdiversaOrder, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EdiversaOrderHeader ")
        sb.AppendLine("WHERE Guid='" & oEDiversaOrder.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oEDiversaOrder.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oEDiversaOrder
            oRow("DocNum") = .DocNum
            oRow("FchDoc") = .FchDoc
            oRow("FchDelivery") = .FchDeliveryMin
            oRow("FchLast") = .FchDeliveryMax
            oRow("Tipo") = .Tipo
            oRow("Funcion") = .Funcion
            oRow("Proveedor") = SQLHelper.NullableBaseGuid(.Proveedor)
            oRow("FacturarA") = SQLHelper.NullableBaseGuid(.FacturarA)
            oRow("Comprador") = SQLHelper.NullableBaseGuid(.Comprador)
            oRow("ReceptorMercancia") = SQLHelper.NullableBaseGuid(.ReceptorMercancia)
            oRow("Centro") = SQLHelper.NullableString(.Centro)
            oRow("Departamento") = SQLHelper.NullableString(.Departamento)
            oRow("NADMS") = SQLHelper.NullableEan(.NADMS)
            oRow("CompradorEAN") = SQLHelper.NullableEan(.CompradorEAN)
            oRow("FacturarAEAN") = SQLHelper.NullableEan(.FacturarAEAN)
            oRow("ReceptorMercanciaEAN") = SQLHelper.NullableEan(.ReceptorMercanciaEAN)
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            If .Amt IsNot Nothing Then
                oRow("Cur") = .Amt.Cur.Tag
                oRow("Val") = .Amt.Val
                oRow("Eur") = .Amt.Eur
            End If
            oRow("Result") = SQLHelper.NullableBaseGuid(.Result)
        End With

        oDA.Update(oDs)


    End Sub



    Shared Function SetResult(exs As List(Of Exception), oEdiOrder As DTOEdiversaOrder, oPurchaseOrder As DTOPurchaseOrder) As Boolean
        Dim SQL As String = "UPDATE EdiversaOrderHeader SET Result='" & oPurchaseOrder.Guid.ToString & "' WHERE Guid = '" & oEdiOrder.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, exs)
        SQL = "UPDATE Edi SET Result = " & DTOEdiversaFile.Results.processed & ", ResultGuid='" & oPurchaseOrder.Guid.ToString & "' WHERE Guid = '" & oEdiOrder.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function

    Protected Shared Sub UpdateItems(oEDiversaOrder As DTOEdiversaOrder, ByRef oTrans As SqlTransaction)
        If Not oEDiversaOrder.IsNew Then DeleteItems(oEDiversaOrder, oTrans)

        Dim SQL As String = "SELECT * FROM EdiversaOrderItem WHERE Parent='" & oEDiversaOrder.Guid.ToString & "' "
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each oItem As DTOEdiversaOrderItem In oEDiversaOrder.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Parent") = oEDiversaOrder.Guid
            oRow("Lin") = oItem.Lin

            With oItem
                oRow("Guid") = .Guid
                oRow("Ean") = .Ean.Value
                oRow("RefProveidor") = .RefProveidor
                oRow("RefClient") = .RefClient
                oRow("Dsc") = .Dsc
                oRow("Qty") = .Qty
                oRow("Preu") = SQLHelper.NullableAmt(If(.Preu, .PreuNet))
                oRow("Dto") = .Dto
                oRow("Sku") = SQLHelper.NullableBaseGuid(.Sku)
                oRow("SkipPreuValidationUser") = SQLHelper.NullableBaseGuid(.SkipPreuValidationUser)
                oRow("SkipDtoValidationUser") = SQLHelper.NullableBaseGuid(.SkipDtoValidationUser)
                oRow("SkipItemUser") = SQLHelper.NullableBaseGuid(.SkipItemUser)
                oRow("SkipPreuValidationFch") = SQLHelper.NullableFch(.SkipPreuValidationFch)
                oRow("SkipDtoValidationFch") = SQLHelper.NullableFch(.SkipDtoValidationFch)
                oRow("SkipItemFch") = SQLHelper.NullableFch(.SkipItemFch)
            End With

        Next

        oDA.Update(oDs)
    End Sub

    Protected Shared Sub UpdateExceptions(oEDiversaOrder As DTOEdiversaOrder, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM EdiversaExceptions WHERE Parent='" & Guid.NewGuid.ToString & "' "
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each ex As DTOEdiversaException In oEDiversaOrder.Exceptions
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Parent") = oEDiversaOrder.Guid

            With ex
                oRow("Cod") = .Cod
                oRow("TagGuid") = SQLHelper.NullableBaseGuid(.Tag)
                oRow("TagCod") = .TagCod
                oRow("Msg") = SQLHelper.NullableString(.Msg)
            End With
        Next

        For Each item In oEDiversaOrder.Items
            For Each ex As DTOEdiversaException In item.Exceptions
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Parent") = item.Guid

                With ex
                    oRow("Cod") = .Cod
                    oRow("TagGuid") = SQLHelper.NullableBaseGuid(.Tag)
                    oRow("TagCod") = .TagCod
                    oRow("Msg") = SQLHelper.NullableString(.Msg)
                End With
            Next
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oEDiversaOrder As DTOEdiversaOrder, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oEDiversaOrder, oTrans)
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


    Shared Sub Delete(oEDiversaOrder As DTOEdiversaOrder, ByRef oTrans As SqlTransaction)
        DeleteItems(oEDiversaOrder, oTrans)
        DeleteHeader(oEDiversaOrder, oTrans)
    End Sub

    Shared Sub DeleteHeader(oEDiversaOrder As DTOEdiversaOrder, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaOrderHeader WHERE Guid='" & oEDiversaOrder.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oEDiversaOrder As DTOEdiversaOrder, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaOrderItem WHERE Parent='" & oEDiversaOrder.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function Duplicates(oEDiversaOrder As DTOEdiversaOrder) As List(Of DTOEdiversaOrder)
        Dim retval As New List(Of DTOEdiversaOrder)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT EDiversaOrderHeader.Guid, EDiversaOrderHeader.Result, EDiversaOrderHeader.FchDoc, EDiversaOrderHeader.DocNum, EDiversaOrderHeader.Comprador, CliGral.FullNom, EDiversaOrderHeader.Eur ")
        sb.AppendLine(", Pdc.Pdc, Pdc.FchCreated ")
        sb.AppendLine("FROM EDiversaOrderHeader ")
        sb.AppendLine("INNER JOIN Edi ON EdiversaOrderHeader.Guid = Edi.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON EDiversaOrderHeader.Comprador=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pdc ON EDiversaOrderHeader.Result=Pdc.Guid ")
        With oEDiversaOrder
            sb.AppendLine("WHERE EdiversaOrderHeader.Docnum = '" & .DocNum & "' ")
            If .CompradorEAN IsNot Nothing Then
                sb.AppendLine("AND EdiversaOrderHeader.CompradorEAN = '" & .CompradorEAN.Value & "' ")
            End If
            If .ReceptorMercanciaEAN IsNot Nothing Then
                sb.AppendLine("AND EdiversaOrderHeader.ReceptorMercanciaEAN = '" & .ReceptorMercanciaEAN.Value & "' ")
            End If
            sb.AppendLine("AND EdiversaOrderHeader.Guid <> '" & .Guid.ToString & "' ")
            sb.AppendLine("AND Edi.Result <> " & DTOEdiversaFile.Results.Deleted & " ")
        End With

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOEdiversaOrder(oDrd("Guid"))
            With item
                .FchDoc = SQLHelper.GetFchFromDataReader(oDrd("FchDoc"))
                .DocNum = SQLHelper.GetStringFromDataReader(oDrd("DocNum"))
                If IsDBNull(oDrd("Comprador")) Then
                    .Comprador = New DTOContact()
                    .Comprador.FullNom = "(comprador desconegut)"
                Else
                    .Comprador = New DTOContact(oDrd("Comprador"))
                    .Comprador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                End If
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                If Not IsDBNull(oDrd("Result")) Then
                    .Result = New DTOPurchaseOrder(oDrd("Result"))
                    With .Result
                        .Num = oDrd("Pdc")
                        .UsrLog.FchCreated = oDrd("FchCreated")
                    End With
                End If

                .EdiversaFile = New DTOEdiversaFile(oDrd("Guid"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

Public Class EDiversaOrdersLoader

    Shared Function Headers(oEmp As DTOEmp, year As Integer) As List(Of DTOEdiversaOrder)
        Dim retval As New List(Of DTOEdiversaOrder)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT EDiversaOrderHeader.Guid, EDiversaOrderHeader.DocNum, EDiversaOrderHeader.FchDoc ")
        sb.AppendLine(", EDiversaOrderHeader.NADMS, EDiversaOrderHeader.CompradorEAN, EDiversaOrderHeader.Result ")
        sb.AppendLine(", EDiversaOrderHeader.Comprador, Comprador.FullNom AS CompradorNom, ReceptorMercancia.FullNom AS ReceptorMercanciaNom ")
        sb.AppendLine("FROM EDiversaOrderHeader ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS Comprador ON EDiversaOrderHeader.Comprador=Comprador.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS ReceptorMercancia ON EDiversaOrderHeader.ReceptorMercancia=ReceptorMercancia.Guid ")
        sb.AppendLine("WHERE YEAR(EDiversaOrderHeader.FchDoc)=" & year & " ")
        sb.AppendLine("ORDER BY EDiversaOrderHeader.FchDoc DESC, Comprador.FullNom, EDiversaOrderHeader.DocNum DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOEdiversaOrder(oDrd("Guid"))
            With item
                .NADMS = SQLHelper.GetEANFromDataReader(oDrd("NADMS"))
                .CompradorEAN = SQLHelper.GetEANFromDataReader(oDrd("CompradorEan"))
                .DocNum = oDrd("DocNum")
                .FchDoc = SQLHelper.GetFchFromDataReader(oDrd("FchDoc"))
                If Not IsDBNull(oDrd("Result")) Then
                    .Result = New DTOPurchaseOrder(oDrd("Result"))
                End If
                If Not IsDBNull(oDrd("Comprador")) Then
                    .Comprador = New DTOCustomer(oDrd("Comprador"))
                    Dim sNaddp = SQLHelper.GetStringFromDataReader(oDrd("ReceptorMercanciaNom"))
                    If sNaddp.StartsWith("EL CORTE INGLES") Then sNaddp = sNaddp.Replace("EL CORTE INGLES", "").Trim()
                    .Comprador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("CompradorNom")) & " - " & sNaddp
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(Optional OnlyOpenFiles As Boolean = True) As List(Of DTOEdiversaOrder)
        Dim retval As New List(Of DTOEdiversaOrder)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT EDiversaOrderHeader.Guid, EDiversaOrderHeader.DocNum ")
        sb.AppendLine(", EDiversaOrderHeader.FchDoc, EDiversaOrderHeader.FchDelivery, EDiversaOrderHeader.FchLast ")
        sb.AppendLine(", EDiversaOrderHeader.Tipo, EDiversaOrderHeader.Funcion, EDiversaOrderHeader.Cur ")
        sb.AppendLine(", EDiversaOrderHeader.Proveedor, EDiversaOrderHeader.Comprador, EDiversaOrderHeader.ReceptorMercancia, EDiversaOrderHeader.FacturarA ")
        sb.AppendLine(", EDiversaOrderHeader.NADMS, EDiversaOrderHeader.CompradorEAN, EDiversaOrderHeader.ReceptorMercanciaEAN, EDiversaOrderHeader.FacturarAEAN ")
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
        sb.AppendLine(", Ex1.Guid AS Ex1Guid, Ex1.Cod as Ex1Cod, Ex1.TagGuid AS Ex1TagGuid, Ex1.TagCod AS Ex1TagCod, Ex1.Msg AS Ex1Msg ")
        sb.AppendLine(", Ex2.Guid AS Ex2Guid, Ex2.Cod as Ex2Cod, Ex2.TagGuid AS Ex2TagGuid, Ex2.TagCod AS Ex2TagCod, Ex2.Msg AS Ex2Msg ")
        sb.AppendLine("FROM EDiversaOrderHeader ")
        sb.AppendLine("INNER JOIN Edi ON EDiversaOrderHeader.Guid = Edi.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwCcxOrMe ON EDiversaOrderHeader.Comprador=VwCcxOrMe.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS Comprador ON EDiversaOrderHeader.Comprador=Comprador.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS ReceptorMercancia ON EDiversaOrderHeader.ReceptorMercancia=ReceptorMercancia.Guid ")
        sb.AppendLine("LEFT OUTER JOIN EdiversaOrderItem ON EDiversaOrderHeader.Guid=EdiversaOrderItem.Parent ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON EdiversaOrderItem.Sku = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN EdiversaExceptions AS Ex1 ON EdiversaOrderHeader.Guid = Ex1.Parent ")
        sb.AppendLine("LEFT OUTER JOIN EdiversaExceptions AS Ex2 ON EdiversaOrderItem.Guid = Ex2.Parent ")
        If OnlyOpenFiles Then
            sb.AppendLine("WHERE EDi.Result =0 ")
            sb.AppendLine("AND EDi.ResultGuid IS NULL ")
        End If
        sb.AppendLine("ORDER BY Customer, EDiversaOrderHeader.FchDoc DESC, EDiversaOrderHeader.Guid, EdiversaOrderItem.Lin ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oOrder As New DTOEdiversaOrder(Guid.NewGuid)
        Dim oItem As New DTOEdiversaOrderItem
        Dim oCustomer As New DTOCustomer
        Dim oFacturarA As New DTOCustomer
        Do While oDrd.Read
            If Not oOrder.Guid.Equals(oDrd("Guid")) Then
                oOrder = New DTOEdiversaOrder(oDrd("Guid"))
                With oOrder
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
                    .Cur = SQLHelper.GetCurFromDataReader(oDrd("Cur"))

                    If Not IsDBNull(oDrd("Customer")) Then
                        If Not oCustomer.Guid.Equals(oDrd("Customer")) Then
                            oCustomer = New DTOCustomer(oDrd("Customer"))
                        End If
                        .Customer = oCustomer
                    End If

                    If Not IsDBNull(oDrd("FacturarA")) Then
                        If Not oFacturarA.Guid.Equals(oDrd("FacturarA")) Then
                            oFacturarA = New DTOCustomer(oDrd("FacturarA"))
                        End If
                        .FacturarA = oFacturarA
                    End If

                    If Not IsDBNull(oDrd("Proveedor")) Then
                        .Proveedor = New DTOContact(oDrd("Proveedor"))
                    End If

                    .NADMS = DTOEan.Factory(oDrd("NADMS"))
                    .CompradorEAN = DTOEan.Factory(oDrd("CompradorEAN"))
                    .FacturarAEAN = DTOEan.Factory(oDrd("FacturarAEAN"))
                    .Centro = SQLHelper.GetStringFromDataReader(oDrd("Centro"))
                    .Departamento = SQLHelper.GetStringFromDataReader(oDrd("Departamento"))
                    If Not IsDBNull(oDrd("Comprador")) Then
                        Dim oMatchingComprador As DTOEdiversaOrder = Nothing
                        If retval.Count > 0 Then
                            oMatchingComprador = retval.FindAll(Function(x) x.Comprador IsNot Nothing).Find(Function(x) x.Comprador.Guid.Equals(oDrd("Comprador")))
                        End If
                        If oMatchingComprador Is Nothing Then
                            .Comprador = New DTOCustomer(oDrd("Comprador"))
                            .Comprador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("CompradorNom"))
                        Else
                            .Comprador = oMatchingComprador.Comprador
                        End If
                    End If

                    If Not IsDBNull(oDrd("ReceptorMercancia")) Then
                        .ReceptorMercanciaEAN = DTOEan.Factory(oDrd("ReceptorMercanciaEAN"))
                        Dim oMatchingReceptor As DTOEdiversaOrder = Nothing
                        If retval.Count > 0 Then
                            oMatchingReceptor = retval.FindAll(Function(x) x.ReceptorMercancia IsNot Nothing).Find(Function(x) x.ReceptorMercancia.Guid.Equals(oDrd("ReceptorMercancia")))
                        End If
                        If oMatchingReceptor Is Nothing Then
                            .ReceptorMercancia = New DTOContact(DirectCast(oDrd("ReceptorMercancia"), Guid))
                            .ReceptorMercancia.FullNom = SQLHelper.GetStringFromDataReader(oDrd("ReceptorMercanciaNom"))
                        Else
                            .ReceptorMercancia = oMatchingReceptor.ReceptorMercancia
                        End If
                    End If

                    .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                    .EdiversaFile = New DTOEdiversaFile(oDrd("Guid"))
                    .EdiversaFile.Result = oDrd("Status")
                    If Not IsDBNull(oDrd("Result")) Then
                        .Result = New DTOPurchaseOrder(oDrd("Result"))
                    End If
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    retval.Add(oOrder)
                End With
            End If
            If Not IsDBNull(oDrd("Ex1Guid")) Then
                If Not oOrder.Exceptions.Any(Function(x) x.Guid.Equals(oDrd("Ex1Guid"))) Then
                    Dim ex = DTOEdiversaException.Factory(oDrd("Ex1Cod"), Nothing, oDrd("Ex1Msg"))
                    With ex
                        .Guid = oDrd("Ex1Guid")
                        .Cod = oDrd("Ex1Cod")
                        .TagCod = oDrd("Ex1TagCod")
                        If Not IsDBNull(oDrd("Ex1TagGuid")) Then
                            .Tag = New DTOBaseGuid(oDrd("Ex1TagGuid"))
                        End If
                    End With
                    oOrder.Exceptions.Add(ex)
                End If
            End If
            If Not IsDBNull(oDrd("ItemGuid")) Then
                If Not oItem.Guid.Equals(oDrd("ItemGuid")) Then
                    oItem = New DTOEdiversaOrderItem(oDrd("ItemGuid"))
                    With oItem
                        .Parent = oOrder
                        .Lin = oDrd("Lin")
                        .Ean = DTOEan.Factory(oDrd("Ean"))
                        .RefProveidor = SQLHelper.GetStringFromDataReader(oDrd("RefProveidor"))
                        .RefClient = SQLHelper.GetStringFromDataReader(oDrd("RefClient"))
                        .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
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
                        oOrder.Items.Add(oItem)
                    End With
                End If
            End If
            If Not IsDBNull(oDrd("Ex2Guid")) Then
                If Not oOrder.Exceptions.Any(Function(x) x.Guid.Equals(oDrd("Ex2Guid"))) Then
                    Dim ex = DTOEdiversaException.Factory(oDrd("Ex2Cod"), Nothing, oDrd("Ex2Msg"))
                    With ex
                        .Guid = oDrd("Ex2Guid")
                        .Cod = oDrd("Ex2Cod")
                        .TagCod = oDrd("Ex2TagCod")
                        If Not IsDBNull(oDrd("Ex2TagGuid")) Then
                            .Tag = New DTOBaseGuid(oDrd("Ex2TagGuid"))
                        End If
                    End With
                    oItem.Exceptions.Add(ex)
                End If
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function DeleteDuplicates() As DTOTaskResult
        Dim retval As New DTOTaskResult
        Dim SQL As String = ""
        Dim rc As Integer
        Dim sbResult As New Text.StringBuilder

        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE EdiversaOrderItem ")
        sb.AppendLine("FROM EdiversaOrderItem ")
        sb.AppendLine("INNER JOIN Edi ON EdiversaOrderItem.parent = Edi.Guid ")
        sb.AppendLine("WHERE EDi.Result =0 ")
        sb.AppendLine("AND EDi.ResultGuid IS NULL ")
        SQL = sb.ToString
        rc = SQLHelper.ExecuteNonQuery(SQL, retval.Exceptions)
        If rc = 0 Then
            sbResult.AppendLine("No s'han trobat linies de comanda per esborrar")
        Else
            sbResult.AppendLine(String.Format("Esborrades {0} linies de comanda de EdiversaOrderItem", rc))
        End If

        sb.AppendLine("DELETE EdiversaOrderheader ")
        sb.AppendLine("FROM EdiversaOrderheader ")
        sb.AppendLine("INNER JOIN Edi ON EdiversaOrderheader.Guid = Edi.Guid ")
        sb.AppendLine("WHERE EDi.Result =0 ")
        sb.AppendLine("AND EDi.ResultGuid IS NULL ")
        SQL = sb.ToString
        rc = SQLHelper.ExecuteNonQuery(SQL, retval.Exceptions)
        If rc = 0 Then
            sbResult.AppendLine("No s'han trobat capçaleres de comanda per esborrar")
        Else
            sbResult.AppendLine(String.Format("Esborrades {0} capçaleres de comanda de EdiversaOrderHeader", rc))
        End If

        sb.AppendLine("SELECT Guid, Filename from Edi ")
        sb.AppendLine("WHERE Edi.Result =0 ")
        sb.AppendLine("AND Edi.tag='ORDERS_D_96A_UN_EAN008' ")
        sb.AppendLine("AND Edi.ResultGuid IS NULL ")
        sb.AppendLine("ORDER BY Filename ")
        SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oGuids As New List(Of Guid)
        Dim sFilename As String = "xx"
        Do While oDrd.Read
            If oDrd("Filename") = sFilename Then
                oGuids.Add(oDrd("Guid"))
            Else
                sFilename = oDrd("Filename")
            End If
        Loop
        oDrd.Close()

        If oGuids.Count = 0 Then
            sbResult.AppendLine("No s'han trobat comandes per recuperar")
        Else
            sbResult.AppendLine(String.Format("Trobades {0} comandes a Edi per recuperar", oGuids.Count))
        End If


        Dim exs As New List(Of Exception)
        Dim i As Integer
        For Each oGuid In oGuids
            i += 1
            SQL = "DELETE Edi WHERE Guid='" & oGuid.ToString & "'"
            Dim rc2 = SQLHelper.ExecuteNonQuery(SQL, exs)
            'Stop
        Next
        sbResult.AppendLine(String.Format("Eliminades {0} comandes Edi duplicades", oGuids.Count))
        retval.Msg = sbResult.ToString

        Return retval
    End Function
    Shared Function Descarta(oEDiversaOrders As List(Of DTOEdiversaOrder), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Descarta(oEDiversaOrders, oTrans)
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

    Shared Sub Descarta(oEDiversaOrders As List(Of DTOEdiversaOrder), ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Edi SET Result=" & CInt(DTOEdiversaFile.Results.Deleted) & " WHERE (")
        For Each item As DTOEdiversaOrder In oEDiversaOrders
            If item.UnEquals(oEDiversaOrders.First) Then sb.AppendLine("OR ")
            sb.AppendLine("Edi.Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL As String = sb.ToString
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function ConfirmationPending() As List(Of DTOEdiversaOrder)
        Dim retval As New List(Of DTOEdiversaOrder)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT EdiversaOrderHeader.Guid, EdiversaOrderHeader.FchDoc, EdiversaOrderHeader.DocNum, EdiversaOrderHeader.Comprador, CliGral.FullNom, EdiversaOrderHeader.Eur ")
        sb.AppendLine("FROM EdiversaOrderHeader ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON EdiversaOrderHeader.Comprador = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN EdiversaOrdrSpHeader ON EdiversaOrderHeader.Result = EdiversaOrdrSpHeader.EdiversaOrder AND EdiversaOrdrSpHeader.Guid IS NULL ")
        sb.AppendLine("ORDER BY EdiversaOrderHeader.FchDoc DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOEdiversaOrder(oDrd("Guid"))
            With item
                .FchDoc = SQLHelper.GetFchFromDataReader(oDrd("FchDoc"))
                .DocNum = SQLHelper.GetStringFromDataReader(oDrd("DocNum"))
                If IsDBNull(oDrd("Comprador")) Then
                    .Comprador = New DTOContact()
                    .Comprador.FullNom = "(comprador desconegut)"
                Else
                    .Comprador = New DTOContact(oDrd("Comprador"))
                    .Comprador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                End If
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                .EdiversaFile = New DTOEdiversaFile(oDrd("Guid"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
