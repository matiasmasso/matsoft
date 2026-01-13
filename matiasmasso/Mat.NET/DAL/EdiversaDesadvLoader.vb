Public Class EdiversaDesadvLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOEdiversaDesadv
        Dim retval As DTOEdiversaDesadv = Nothing
        Dim oEdiFile As New DTOEdiversaFile(oGuid)
        Dim oEdiversaDesadv As New DTOEdiversaDesadv(oEdiFile)
        If Load(oEdiversaDesadv) Then
            retval = oEdiversaDesadv
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaDesadv As DTOEdiversaDesadv) As Boolean
        If Not oEdiversaDesadv.IsLoaded And Not oEdiversaDesadv.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT EdiversaDesadvHeader.*, EdiversaDesadvItem.*, VwSkuNom.* ")
            sb.AppendLine(", NomProveidor.FullNom AS ProveidorNom, NomEntrega.FullNom AS EntregaNom ")
            sb.AppendLine(", Pdc.Pdc, Pdc.Fch AS PdcFch, Pdc.Pdd ")
            sb.AppendLine("FROM EdiversaDesadvHeader ")
            sb.AppendLine("LEFT OUTER JOIN EdiversaDesadvItem ON EdiversaDesadvHeader.Guid = EdiversaDesadvItem.Desadv ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS NomProveidor ON EdiversaDesadvHeader.Proveidor = NomProveidor.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS NomEntrega ON EdiversaDesadvHeader.Entrega = NomEntrega.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pdc ON EdiversaDesadvHeader.PurchaseOrder = Pdc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON EdiversaDesadvItem.Sku = VwSkuNom.SkuGuid ")
            sb.AppendLine("WHERE EdiversaDesadvHeader.Guid='" & oEdiversaDesadv.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oEdiversaDesadv
                    If Not .IsLoaded Then
                        .Bgm = SQLHelper.GetStringFromDataReader(oDrd("Bgm"))
                        .FchDoc = SQLHelper.GetFchFromDataReader(oDrd("FchDoc"))
                        .FchShip = SQLHelper.GetFchFromDataReader(oDrd("FchShip"))
                        .Rff = SQLHelper.GetStringFromDataReader(oDrd("Rff"))
                        .NadBy = SQLHelper.GetEANFromDataReader(oDrd("NadBy"))
                        .NadSu = SQLHelper.GetEANFromDataReader(oDrd("NadSu"))
                        .NadDp = SQLHelper.GetEANFromDataReader(oDrd("NadDp"))
                        If Not IsDBNull(oDrd("Proveidor")) Then
                            .Proveidor = New DTOProveidor(oDrd("Proveidor"))
                            .Proveidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("ProveidorNom"))
                        End If
                        If Not IsDBNull(oDrd("Entrega")) Then
                            .Entrega = New DTOContact(oDrd("Entrega"))
                            .Entrega.FullNom = SQLHelper.GetStringFromDataReader(oDrd("EntregaNom"))
                        End If
                        If Not IsDBNull(oDrd("PurchaseOrder")) Then
                            .PurchaseOrder = New DTOPurchaseOrder(oDrd("PurchaseOrder"))
                            With .PurchaseOrder
                                .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Pdc"))
                                .Fch = SQLHelper.GetFchFromDataReader(oDrd("PdcFch"))
                                .Concept = SQLHelper.GetStringFromDataReader(oDrd("Pdd"))
                            End With
                        End If
                        .Items = New List(Of DTOEdiversaDesadv.Item)
                        .IsLoaded = True
                    End If

                    If Not IsDBNull(oDrd("Lin")) Then
                        Dim item As New DTOEdiversaDesadv.Item
                        With item
                            .Lin = oDrd("Lin")
                            .Ean = SQLHelper.GetEANFromDataReader(oDrd("Ean"))
                            .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                            .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                            .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                            .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                        End With
                        .Items.Add(item)
                    End If
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oEdiversaDesadv.IsLoaded
        Return retval
    End Function

    Shared Function Update(oEdiversaDesadv As DTOEdiversaDesadv, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEdiversaDesadv, oTrans)
            oTrans.Commit()
            oEdiversaDesadv.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oEdiversaDesadv As DTOEdiversaDesadv, ByRef oTrans As SqlTransaction)
        UpdateHeader(oEdiversaDesadv, oTrans)
        UpdateItems(oEdiversaDesadv, oTrans)
        UpdateExceptions(oEdiversaDesadv, oTrans)
    End Sub

    Shared Sub UpdateHeader(oEdiversaDesadv As DTOEdiversaDesadv, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EdiversaDesadvHeader ")
        sb.AppendLine("WHERE Guid='" & oEdiversaDesadv.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oEdiversaDesadv.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oEdiversaDesadv
            oRow("Bgm") = SQLHelper.NullableString(.Bgm)
            oRow("FchDoc") = SQLHelper.NullableFch(.FchDoc)
            oRow("FchShip") = SQLHelper.NullableFch(.FchShip)
            oRow("Rff") = SQLHelper.NullableString(.Rff)
            oRow("NadBy") = SQLHelper.NullableEan(.NadBy)
            oRow("NadSu") = SQLHelper.NullableEan(.NadSu)
            oRow("NadDp") = SQLHelper.NullableEan(.NadDp)
            oRow("Proveidor") = SQLHelper.NullableBaseGuid(.Proveidor)
            oRow("Entrega") = SQLHelper.NullableBaseGuid(.Entrega)
            oRow("PurchaseOrder") = SQLHelper.NullableBaseGuid(.PurchaseOrder)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oEdiversaDesadv As DTOEdiversaDesadv, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EdiversaDesadvItem ")
        sb.AppendLine("WHERE Desadv='" & oEdiversaDesadv.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item In oEdiversaDesadv.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Desadv") = oEdiversaDesadv.Guid
            oRow("Lin") = oEdiversaDesadv.Items.IndexOf(item) + 1
            oRow("Ean") = SQLHelper.NullableEan(item.Ean)
            oRow("Ref") = SQLHelper.NullableString(item.Ref)
            oRow("Dsc") = SQLHelper.NullableString(item.Dsc)
            oRow("Qty") = SQLHelper.NullableInt(item.Qty)
            oRow("Sku") = SQLHelper.NullableBaseGuid(item.Sku)
        Next

        oDA.Update(oDs)
    End Sub

    Protected Shared Sub UpdateExceptions(oEdiversaDesadv As DTOEdiversaDesadv, ByRef oTrans As SqlTransaction)
        DeleteExceptions(oEdiversaDesadv, oTrans)

        Dim SQL As String = "SELECT * FROM EdiversaExceptions WHERE Parent='" & oEdiversaDesadv.Guid.ToString & "' "
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each ex As DTOEdiversaException In oEdiversaDesadv.Exceptions
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Parent") = oEdiversaDesadv.Guid

            With ex
                oRow("Cod") = .Cod
                oRow("TagGuid") = SQLHelper.NullableBaseGuid(.Tag)
                oRow("TagCod") = .TagCod
                oRow("Msg") = SQLHelper.NullableString(.Msg)
            End With
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oEdiversaDesadv As DTOEdiversaDesadv, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oEdiversaDesadv, oTrans)
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


    Shared Sub Delete(oEdiversaDesadv As DTOEdiversaDesadv, ByRef oTrans As SqlTransaction)
        DeleteItems(oEdiversaDesadv, oTrans)
        DeleteExceptions(oEdiversaDesadv, oTrans)
        DeleteHeader(oEdiversaDesadv, oTrans)
    End Sub

    Shared Sub DeleteHeader(oEdiversaDesadv As DTOEdiversaDesadv, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaDesadvHeader WHERE Guid='" & oEdiversaDesadv.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oEdiversaDesadv As DTOEdiversaDesadv, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaDesadvItem WHERE Desadv='" & oEdiversaDesadv.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteExceptions(oEdiversaDesadv As DTOEdiversaDesadv, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE EdiversaExceptions ")
        sb.AppendLine("WHERE EdiversaExceptions.Parent ='" & oEdiversaDesadv.Guid.ToString & "' ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class EdiversaDesadvsLoader

    Shared Function All() As List(Of DTOEdiversaDesadv)
        Dim retval As New List(Of DTOEdiversaDesadv)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT EdiversaDesadvHeader.* ")
        sb.AppendLine(", NomProveidor.FullNom AS ProveidorNom, NomEntrega.FullNom AS EntregaNom ")
        sb.AppendLine(", Pdc.Pdc, Pdc.Fch AS PdcFch, Pdc.Pdd ")
        sb.AppendLine("FROM EdiversaDesadvHeader ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS NomProveidor ON EdiversaDesadvHeader.Proveidor = NomProveidor.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS NomEntrega ON EdiversaDesadvHeader.Entrega = NomEntrega.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pdc ON EdiversaDesadvHeader.PurchaseOrder = Pdc.Guid ")
        sb.AppendLine("ORDER BY FchDoc DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oEdiFile As New DTOEdiversaFile(oDrd("Guid"))
            Dim item As New DTOEdiversaDesadv(oEdiFile)
            With item
                .Bgm = SQLHelper.GetStringFromDataReader(oDrd("Bgm"))
                .FchDoc = SQLHelper.GetFchFromDataReader(oDrd("FchDoc"))
                .FchShip = SQLHelper.GetFchFromDataReader(oDrd("FchShip"))
                .Rff = SQLHelper.GetStringFromDataReader(oDrd("Rff"))
                .NadBy = SQLHelper.GetEANFromDataReader(oDrd("NadBy"))
                .NadSu = SQLHelper.GetEANFromDataReader(oDrd("NadSu"))
                .NadDp = SQLHelper.GetEANFromDataReader(oDrd("NadDp"))
                If Not IsDBNull(oDrd("Proveidor")) Then
                    .Proveidor = New DTOProveidor(oDrd("Proveidor"))
                    .Proveidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("ProveidorNom"))
                End If
                If Not IsDBNull(oDrd("Entrega")) Then
                    .Entrega = New DTOContact(oDrd("Entrega"))
                    .Entrega.FullNom = SQLHelper.GetStringFromDataReader(oDrd("EntregaNom"))
                End If
                If Not IsDBNull(oDrd("PurchaseOrder")) Then
                    .PurchaseOrder = New DTOPurchaseOrder(oDrd("PurchaseOrder"))
                    With .PurchaseOrder
                        .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Pdc"))
                        .Fch = SQLHelper.GetFchFromDataReader(oDrd("PdcFch"))
                        .Concept = SQLHelper.GetStringFromDataReader(oDrd("Pdd"))
                    End With
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
