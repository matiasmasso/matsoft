Public Class ImportacioLoader
    Shared Function Find(oGuid As Guid) As DTOImportacio
        Dim retval As DTOImportacio = Nothing
        Dim oImportacio As New DTOImportacio(oGuid)
        If Load(oImportacio) Then
            retval = oImportacio
        End If
        Return retval
    End Function

    Shared Function FromDelivery(oDelivery As DTODelivery) As DTOImportacio
        Dim retVal As DTOImportacio = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT ImportDtl.HeaderGuid, ImportHdr.Yea, ImportHdr.Id ")
        sb.AppendLine(", ImportHdr.PrvGuid, CliGral.FullNom ")
        sb.AppendLine("From ImportDtl ")
        sb.AppendLine("INNER JOIN ImportHdr ON ImportDtl.HeaderGuid=ImportHdr.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON ImportHdr.PrvGuid = CliGral.Guid ")
        sb.AppendLine("WHERE ImportDtl.Guid = '" & oDelivery.Guid.ToString & "' ")
        sb.AppendLine("AND ImportDtl.SrcCod= " & CInt(DTOImportacioItem.SourceCodes.Alb) & " ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retVal = New DTOImportacio(oDrd("HeaderGuid"))
            With retVal
                .Yea = oDrd("Yea")
                .Id = oDrd("Id")
                .Proveidor = New DTOProveidor(oDrd("PrvGuid"))
                .Proveidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
            End With
        End If
        oDrd.Close()
        Return retVal
    End Function

    Shared Function Load(ByRef oImportacio As DTOImportacio) As Boolean
        If Not oImportacio.IsLoaded And Not oImportacio.IsNew Then
            Dim sb As New Text.StringBuilder
            sb.AppendLine("SELECT ImportHdr.Guid, ImportHdr.Emp, ImportHdr.Yea, ImportHdr.Id, ImportHdr.Week, ImportHdr.FchEtd, ImportHdr.Fch, ImportHdr.PrvGuid, ImportHdr.TrpGuid, ImportHdr.PlataformaDeCarga, ImportHdr.FchAvisTrp, ImportHdr.Matricula ")
            sb.AppendLine(", ImportHdr.Bultos, ImportHdr.Kg, ImportHdr.M3, ImportHdr.Arrived, ImportHdr.Obs, ImportHdr.Incoterms, ImportHdr.PaisOrigen, ImportHdr.Eur, ImportHdr.Cur, ImportHdr.Val, ImportHdr.Disabled ")
            sb.AppendLine(", X.Goods ")
            sb.AppendLine(", ImportDtl.Guid As DtlGuid, ImportDtl.SrcCod as DtlSrcCod, ImportDtl.Eur as DtlEur, ImportDtl.Cur as DtlCur, ImportDtl.Div as DtlDiv ")
            sb.AppendLine(", ImportDtl.Descripcio as DtlDescripcio, ImportDtl.Hash as DtlHash ")
            sb.AppendLine(", Prv.RaoSocial AS PrvNom, Prv.LangId AS PrvLang, Trp.RaoSocial AS TrpNom ")
            sb.AppendLine(", PlataformaDeCarga.FullNom AS PlataformaDeCargaNom ")
            sb.AppendLine(", VwAddress.* ")
            sb.AppendLine(", Alb.Alb AS AlbId, Alb.Fch as AlbFch ")
            sb.AppendLine(", Cca.Txt AS CcaTxt, Cca.Fch as CcaFch ")
            sb.AppendLine("FROM ImportHdr ")
            sb.AppendLine("INNER JOIN CliGral AS Prv ON ImportHdr.PrvGuid = Prv.Guid ")
            sb.AppendLine("INNER JOIN VwAddress ON ImportHdr.PrvGuid = VwAddress.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS Trp ON ImportHdr.TrpGuid = Trp.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS PlataformaDeCarga ON ImportHdr.PlataformaDeCarga = PlataformaDeCarga.Guid ")
            sb.AppendLine("LEFT OUTER JOIN ImportDtl ON ImportHdr.Guid = ImportDtl.HeaderGuid ")
            sb.AppendLine("LEFT OUTER JOIN (SELECT Arc.AlbGuid, ROUND(SUM(Arc.Qty*Arc.Eur*(100-Arc.Dto)/100),2) AS Goods FROM Arc GROUP BY AlbGuid) X ON ImportDtl.Guid = X.AlbGuid ")
            sb.AppendLine("LEFT OUTER JOIN Alb ON ImportDtl.Guid = Alb.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca ON ImportDtl.Guid = Cca.Guid ")
            sb.AppendLine("WHERE ImportHdr.Guid='" & oImportacio.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY ImportDtl.SrcCod")

            Dim SQL As String = sb.ToString

            Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oImportacio.IsLoaded Then
                    With oImportacio
                        .Emp = New DTOEmp(oDrd("Emp"))
                        .Yea = oDrd("Yea")
                        .Id = oDrd("Id")
                        .Proveidor = New DTOProveidor(DirectCast(oDrd("PrvGuid"), Guid))
                        .Proveidor.Nom = oDrd("PrvNom")
                        .proveidor.Address = SQLHelper.GetAddressFromDataReader(oDrd)
                        .proveidor.Lang = SQLHelper.GetLangFromDataReader(oDrd("PrvLang"))
                        If Not IsDBNull(oDrd("TrpGuid")) Then
                            .Transportista = New DTOTransportista(DirectCast(oDrd("TrpGuid"), Guid))
                            .Transportista.Nom = SQLHelper.GetStringFromDataReader(oDrd("TrpNom"))
                            .Transportista.FullNom = SQLHelper.GetStringFromDataReader(oDrd("TrpNom"))
                        End If

                        If Not IsDBNull(oDrd("PlataformaDeCarga")) Then
                            .PlataformaDeCarga = New DTOContact(oDrd("PlataformaDeCarga"))
                            .PlataformaDeCarga.FullNom = SQLHelper.GetStringFromDataReader(oDrd("PlataformaDeCargaNom"))
                        End If
                        .FchETD = SQLHelper.GetFchFromDataReader(oDrd("FchEtd"))
                        .FchETA = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                        .FchAvisTrp = SQLHelper.GetFchFromDataReader(oDrd("FchAvisTrp"))
                        .Bultos = oDrd("BULTOS")
                        .Kg = oDrd("KG")
                        .M3 = oDrd("M3")

                        .Week = oDrd("Week")
                        .Arrived = oDrd("Arrived")

                        .Obs = SQLHelper.GetStringFromDataReader(oDrd("OBS"))
                        .Disabled = oDrd("Disabled")


                        .Matricula = SQLHelper.GetStringFromDataReader(oDrd("Matricula"))
                        .incoTerm = SQLHelper.GetIncotermFromDataReader(oDrd("INCOTERMS"))

                        .CountryOrigen = New DTOCountry()
                        .CountryOrigen.ISO = oDrd("PAISORIGEN").ToString

                        .Amt = DTOAmt.Factory(oDrd("EUR"), oDrd("Cur").ToString, oDrd("VAL"))
                        .Goods = 0
                        .Items = New List(Of DTOImportacioItem)
                        .IsLoaded = True
                        .Exists = True
                    End With
                End If

                If Not IsDBNull(oDrd("DtlGuid")) Then
                    Dim item As New DTOImportacioItem(oDrd("DtlGuid"))
                    With item
                        .SrcCod = oDrd("DtlSrcCod")
                        .Parent = oImportacio

                        Select Case .SrcCod
                            Case DTOImportacioItem.SourceCodes.Fra, DTOImportacioItem.SourceCodes.FraTrp
                                .Amt = DTOAmt.Factory(CDec(oDrd("DtlEur")), oDrd("DtlCur").ToString, CDec(oDrd("DtlDiv")))
                            Case DTOImportacioItem.SourceCodes.Alb
                                Dim dcEur As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("Goods"))
                                .Amt = DTOAmt.Factory(SQLHelper.GetDecimalFromDataReader(dcEur))
                                oImportacio.Goods += dcEur
                        End Select

                        .Descripcio = SQLHelper.GetStringFromDataReader(oDrd("DtlDescripcio"))
                        .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("DtlHash"))

                        If Not IsDBNull(oDrd("AlbId")) Then
                            Dim oDelivery As New DTODelivery(oDrd("DtlGuid"))
                            With oDelivery
                                .Id = oDrd("AlbId")
                                .Fch = SQLHelper.GetStringFromDataReader(oDrd("AlbFch"))
                            End With
                            .Tag = oDelivery
                        End If

                        If Not IsDBNull(oDrd("CcaTxt")) Then
                            Dim oCca As New DTOCca(oDrd("DtlGuid"))
                            With oCca
                                .Concept = oDrd("CcaTxt")
                                .Fch = SQLHelper.GetStringFromDataReader(oDrd("CcaFch"))
                            End With
                            .Tag = oCca
                        End If
                    End With
                    oImportacio.Items.Add(item)

                End If
            Loop
            oDrd.Close()
            ImportPrevisionsLoader.Load(oImportacio)
        End If

        Dim retval As Boolean = oImportacio.IsLoaded
        Return retval
    End Function


    Shared Function Update(oImportacio As DTOImportacio, ByRef exs As List(Of Exception)) As Boolean
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

    Shared Function Entrada(oDelivery As DTODelivery, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            DeliveryLoader.Update(oDelivery, oTrans)

            Dim oImportacio = oDelivery.Importacio
            If oImportacio IsNot Nothing Then
                Load(oImportacio)
                Dim item = DTOImportacioItem.Factory(oImportacio, DTOImportacioItem.SourceCodes.alb, oDelivery.Guid)
                item.amt = oDelivery.Import
                oImportacio.items.Add(item)
                UpdateItems(oImportacio, oTrans)
            End If

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

    Shared Sub Update(oImportacio As DTOImportacio, oTrans As SqlTransaction)
        UpdateHeader(oImportacio, oTrans)
        UpdateItems(oImportacio, oTrans)
        If oImportacio.Previsions IsNot Nothing Then
            ImportPrevisionsLoader.Update(oImportacio, oTrans)
        End If
    End Sub

    Shared Function UpdateItems(oImportacio As DTOImportacio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            UpdateItems(oImportacio, oTrans)
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

    Shared Sub UpdateHeader(oImportacio As DTOImportacio, oTrans As SqlTransaction)
        With oImportacio
            If .Id = 0 Then .Id = LastId(oImportacio, oTrans) + 1

            Dim SQL As String = "SELECT * FROM IMPORTHDR WHERE Guid='" & .Guid.ToString & "'"
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow
            If oTb.Rows.Count = 0 Then
                oRow = oTb.NewRow
                oRow("Guid") = .Guid
                oTb.Rows.Add(oRow)
            Else
                oRow = oTb.Rows(0)
            End If

            oRow("Emp") = .Emp.Id
            oRow("YEA") = .Yea
            oRow("ID") = .Id
            oRow("PrvGuid") = .Proveidor.Guid
            oRow("PlataformaDeCarga") = SQLHelper.NullableBaseGuid(.PlataformaDeCarga)
            oRow("TrpGuid") = SQLHelper.NullableBaseGuid(.Transportista)
            oRow("Fch") = SQLHelper.NullableFch(.FchETA)
            oRow("FchETD") = SQLHelper.NullableFch(.FchETD)
            oRow("FchAvisTrp") = SQLHelper.NullableFch(.FchAvisTrp)
            oRow("Bultos") = .Bultos
            oRow("KG") = .Kg
            oRow("M3") = .M3
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("Disabled") = .Disabled

            oRow("Week") = .Week
            oRow("Arrived") = .Arrived

            Dim oSum = DTOAmt.Empty
            If .Items IsNot Nothing Then
                For Each item As DTOImportacioItem In .Items
                    If item.SrcCod = DTOImportacioItem.SourceCodes.Fra Then
                        If oSum Is Nothing Then
                            oSum = item.Amt
                        Else
                            oSum.Add(item.Amt)
                        End If
                    End If
                Next
            End If

            oRow("VAL") = oSum.Val
            oRow("CUR") = oSum.Cur.Tag
            oRow("EUR") = oSum.Eur

            oRow("Matricula") = SQLHelper.NullableString(.Matricula)
            oRow("INCOTERMS") = SQLHelper.NullableIncoterm(.incoTerm)

            If .CountryOrigen IsNot Nothing Then
                oRow("PAISORIGEN") = .CountryOrigen.ISO
            End If
            oDA.Update(oTb)

        End With

    End Sub

    Shared Function LastId(oImportacio As DTOImportacio, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim SQL As String = "SELECT TOP 1 Id AS LastId FROM ImportHdr " _
        & "WHERE Emp=" & oImportacio.Emp.Id & " " _
        & "AND Yea=" & oImportacio.Yea & " " _
        & "ORDER BY Id DESC"

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

    Shared Sub UpdateItems(oImportacio As DTOImportacio, oTrans As SqlTransaction)
        If oImportacio.Items IsNot Nothing Then
            DeleteItems(oImportacio, oTrans)

            Dim SQL As String = "SELECT * FROM IMPORTDTL WHERE HeaderGuid='" & oImportacio.Guid.ToString & "'"
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)

            For Each oItem As DTOImportacioItem In oImportacio.Items
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Guid") = oItem.Guid
                oRow("HeaderGuid") = oImportacio.Guid
                oRow("SrcCod") = oItem.SrcCod
                oRow("Descripcio") = SQLHelper.GetStringFromDataReader(oItem.Descripcio)
                If oItem.DocFile IsNot Nothing Then
                    oRow("Hash") = oItem.DocFile.Hash
                End If

                If oItem.Amt IsNot Nothing Then
                    oRow("Div") = oItem.Amt.Val
                    oRow("Cur") = oItem.Amt.Cur.Tag
                    oRow("Eur") = oItem.Amt.Eur
                End If
            Next
            oDA.Update(oTb)
        End If
    End Sub


    Shared Function Delete(oImportacio As DTOImportacio, ByRef exs As List(Of Exception)) As Boolean
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

    Shared Function RevertPrevisions(exs As List(Of Exception), oImportacio As DTOImportacio) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            ImportPrevisionsLoader.Delete(oImportacio, oTrans)

            Dim sb As New Text.StringBuilder()
            sb.AppendLine("UPDATE InvoiceReceivedHeader ")
            sb.AppendLine("SET InvoiceReceivedHeader.Importacio = NULL ")
            sb.AppendLine("WHERE InvoiceReceivedHeader.Importacio = '" & oImportacio.Guid.ToString() & "' ")
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

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

    Shared Function Confirm(exs As List(Of Exception), oConfirmacio As DTOImportacio.Confirmation) As Boolean
        Dim sb As New System.Text.StringBuilder

        sb = New Text.StringBuilder()
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	     Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("	     , QtyConfirmed Int NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid, QtyConfirmed) ")

        Dim idx As Integer = 0
        For Each item In oConfirmacio.Items
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}',{1}) ", item.Guid.ToString(), item.QtyConfirmed)
            idx += 1
        Next
        sb.AppendLine(" ")

        sb.AppendLine("UPDATE InvoiceReceivedItem ")
        sb.AppendLine("SET QtyConfirmed = X.QtyConfirmed ")
        sb.AppendLine("FROM InvoiceReceivedItem ")
        sb.AppendLine("INNER JOIN @Table X ON InvoiceReceivedItem.Guid = X.Guid ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function


    Shared Sub Delete(oImportacio As DTOImportacio, ByRef oTrans As SqlTransaction)
        DeleteItems(oImportacio, oTrans)
        DeleteHeader(oImportacio, oTrans)
    End Sub

    Shared Sub DeleteHeader(oImportacio As DTOImportacio, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ImportHdr WHERE Guid='" & oImportacio.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oImportacio As DTOImportacio, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ImportDtl WHERE HeaderGuid='" & oImportacio.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function LogAvisTrp(ByRef oImportacio As DTOImportacio, ByRef exs As List(Of Exception)) As Boolean
        Dim SQL As String = String.Format("UPDATE ImportHdr SET FchAvisTrp=GETDATE() WHERE Guid='{0}'", oImportacio.Guid.ToString())
        SQLHelper.ExecuteNonQuery(SQL, exs)
        oImportacio.FchAvisTrp = DTO.GlobalVariables.Now()
        Return exs.Count = 0
    End Function

    Shared Function ValidateCamion(oConfirmation As DTOImportacio.Confirmation, ByRef exs As List(Of Exception)) As Boolean
        Dim sb As New System.Text.StringBuilder

        sb = New Text.StringBuilder()
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	     Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each item In oConfirmation.Items
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", item.Guid.ToString())
            idx += 1
        Next
        sb.AppendLine(" ")

        sb.AppendLine("SELECT X.Guid, InvoiceReceivedItem.Guid AS InvGuid, Pnc.Guid AS PncGuid ")
        sb.AppendLine("FROM @Table X ")
        sb.AppendLine("LEFT OUTER JOIN InvoiceReceivedItem ON X.Guid = InvoiceReceivedItem.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pnc ON InvoiceReceivedItem.PurchaseOrderItem = Pnc.Guid ")
        sb.AppendLine("ORDER BY PncGuid ")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim sLin = oDrd("Guid").ToString().Replace("-", "").ToUpper()
            If IsDBNull(oDrd("InvGuid")) Then
                exs.Add(New Exception("Linia de factura no trobada amb identificador " & sLin))
            End If
            If IsDBNull(oDrd("PncGuid")) Then
                exs.Add(New Exception("Comanda no trobada a la linia amb identificador " & sLin))
            End If
        Loop
        oDrd.Close()
        Return exs.Count = 0
    End Function
End Class


Public Class ImportacionsLoader

    Shared Function Transits(oEmp As DTOEmp) As List(Of DTOImportTransit)
        Dim retval As New List(Of DTOImportTransit)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ImportHdr.Guid, ImportHdr.Id, ImportHdr.Eur, ImportHdr.PrvGuid, Prv.FullNom ")
        sb.AppendLine(",MIN(CAST(YEAR(Cca.Fch) AS VARCHAR(4))+CAST(MONTH(Cca.Fch) AS VARCHAR(4))) AS CcaFch ")
        sb.AppendLine(",MAX(CAST(YEAR(Alb.Fch) AS VARCHAR(4))+CAST(MONTH(Alb.Fch) AS VARCHAR(4))) AS AlbFch ")
        sb.AppendLine("from ImportHdr ")
        sb.AppendLine("INNER JOIN ImportDtl ON ImportHdr.Guid = ImportDtl.HeaderGuid ")
        sb.AppendLine("INNER JOIN CliGral AS Prv ON ImportHdr.PrvGuid=Prv.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca AS Done ON ImportHdr.Guid = Done.Ref ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON ImportDtl.Guid = Cca.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Alb ON ImportDtl.Guid = Alb.Guid ")
        sb.AppendLine("WHERE ImportHdr.Emp=" & oEmp.Id & " AND ImportHdr.Yea>=2016 AND ImportHdr.Id>130 AND ImportHdr.Eur>0 AND Done.Ref IS NULL ")
        sb.AppendLine("GROUP BY ImportHdr.Guid, ImportHdr.Yea, ImportHdr.Id, ImportHdr.Eur, ImportHdr.PrvGuid, Prv.FullNom ")
        sb.AppendLine("HAVING  MIN(CAST(YEAR(Cca.Fch) AS VARCHAR(4))+CAST(MONTH(Cca.Fch) AS VARCHAR(4))) <>MAX(CAST(YEAR(Alb.Fch) AS VARCHAR(4))+CAST(MONTH(Alb.Fch) AS VARCHAR(4))) ")
        sb.AppendLine("ORDER BY ImportHdr.Yea DESC, ImportHdr.Id DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oImportacio As New DTOImportTransit
        Do While oDrd.Read
            If Not oImportacio.Guid.Equals(oDrd("Guid")) Then
                oImportacio = New DTOImportTransit(oDrd("Guid"))
                With oImportacio
                    .Emp = oEmp
                    .Id = oDrd("Id")
                    .Proveidor = New DTOProveidor(oDrd("PrvGuid"))
                    .Proveidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Amt = DTOAmt.Factory(oDrd("Eur"))
                    .YearMonthFras = New DTOYearMonth(oDrd("CcaFch"))
                    .YearMonthAlbs = New DTOYearMonth(oDrd("AlbFch"))
                End With
                retval.Add(oImportacio)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, year As Integer, Optional oProveidor As DTOProveidor = Nothing) As List(Of DTOImportacio)
        Dim retval As New List(Of DTOImportacio)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ImportHdr.Guid, ImportHdr.Yea, ImportHdr.Id, ImportHdr.FchEtd, ImportHdr.Fch, ImportHdr.PrvGuid ")
        sb.AppendLine(", ImportHdr.Bultos, ImportHdr.Kg, ImportHdr.M3, ImportHdr.Arrived, ImportHdr.Incoterms, ImportHdr.Matricula, ImportHdr.PaisOrigen, ImportHdr.Eur, ImportHdr.Cur, ImportHdr.Val, ImportHdr.disabled ")
        sb.AppendLine(", X.Goods ")
        sb.AppendLine(", ImportDtl.Guid As DtlGuid, ImportDtl.SrcCod as DtlSrcCod, ImportDtl.Eur as DtlEur, ImportDtl.Cur as DtlCur, ImportDtl.Div as DtlDiv ")
        sb.AppendLine(", ImportDtl.Descripcio as DtlDescripcio, ImportDtl.Hash as DtlHash ")
        sb.AppendLine(", Prv.FullNom AS PrvNom ")
        sb.AppendLine("FROM ImportHdr ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS Prv ON ImportHdr.PrvGuid=Prv.Guid ")
        sb.AppendLine("LEFT OUTER JOIN ImportDtl ON ImportHdr.Guid = ImportDtl.HeaderGuid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT ImportDtl.HeaderGuid, ROUND(SUM(Arc.Qty*Arc.Eur*(100-Arc.Dto)/100),2) AS Goods FROM Arc INNER JOIN ImportDtl ON Arc.AlbGuid = ImportDtl.Guid GROUP BY ImportDtl.HeaderGuid) X ON ImportHdr.Guid = X.HeaderGuid ")
        sb.AppendLine("WHERE ImportHdr.Emp=" & oEmp.Id & " AND ImportHdr.Yea=" & year & " ")

        If oProveidor IsNot Nothing Then
            sb.AppendLine("AND ImportHdr.PrvGuid='" & oProveidor.Guid.ToString & "' ")
        End If

        sb.AppendLine("ORDER BY ImportHdr.Yea DESC, ImportHdr.Id DESC, ImportHdr.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oImportacio As New DTOImportacio
        Do While oDrd.Read
            'If oImportacio.Guid.ToString.ToUpper = "B78DF0DC-040A-49E3-9BBF-3B92FF5147A7" Then Stop
            If Not oImportacio.Guid.Equals(oDrd("Guid")) Then
                oImportacio = New DTOImportacio(oDrd("Guid"))
                With oImportacio
                    .Emp = oEmp
                    .Id = oDrd("Id")
                    .Yea = oDrd("Yea")
                    .FchETD = SQLHelper.GetFchFromDataReader(oDrd("FchEtd"))
                    .FchETA = SQLHelper.GetFchFromDataReader(oDrd("FCH"))
                    If Not IsDBNull(oDrd("PrvGuid")) Then
                        .Proveidor = New DTOProveidor(oDrd("PrvGuid"))
                        .Proveidor.Emp = oEmp
                        .Proveidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("PrvNom"))
                    End If
                    .Bultos = oDrd("BULTOS")
                    .Kg = oDrd("KG")
                    .M3 = oDrd("M3")

                    .Arrived = oDrd("Arrived")
                    .incoTerm = SQLHelper.GetIncotermFromDataReader(oDrd("INCOTERMS"))
                    .countryOrigen = New DTOCountry()
                    .CountryOrigen.ISO = oDrd("PAISORIGEN").ToString

                    .Amt = DTOAmt.Factory(oDrd("EUR"), oDrd("Cur").ToString, oDrd("VAL"))
                    .Goods = SQLHelper.GetDecimalFromDataReader(oDrd("Goods"))
                    .Matricula = SQLHelper.GetStringFromDataReader(oDrd("Matricula"))
                    .Disabled = oDrd("Disabled")
                    .Items = New List(Of DTOImportacioItem)
                    .Exists = True
                End With
                retval.Add(oImportacio)
            End If

            If Not IsDBNull(oDrd("DtlGuid")) Then
                Dim item As New DTOImportacioItem(oDrd("DtlGuid"))
                With item
                    .SrcCod = oDrd("DtlSrcCod")
                    .Parent = oImportacio
                    .Amt = DTOAmt.Factory(CDec(oDrd("DtlEur")), oDrd("DtlCur").ToString, CDec(oDrd("DtlDiv")))
                    .Descripcio = SQLHelper.GetStringFromDataReader(oDrd("DtlDescripcio"))
                    .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("DtlHash"))
                End With
                oImportacio.Items.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Weeks(oEmp As DTOEmp) As List(Of DTOImportacio)
        Dim retval As New List(Of DTOImportacio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ImportHdr.Guid, ImportHdr.Week, ImportHdr.Arrived, ImportHdr.Id, ImportHdr.Fch, ImportHdr.FchETD, ImportHdr.PrvGuid, CliGral.FullNom ")
        sb.AppendLine("FROM ImportHdr ")
        sb.AppendLine("INNER JOIN CliGral ON ImportHdr.PrvGuid=CliGral.Guid ")
        sb.AppendLine("WHERE ImportHdr.Fch > DATEADD(DAY,-7,GETDATE())")
        sb.AppendLine("AND ImportHdr.Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY YEAR(ImportHdr.Fch) DESC, Week DESC, ImportHdr.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOImportacio(oDrd("Guid"))
            With item
                .Week = oDrd("Week")
                .Arrived = oDrd("Arrived")
                .Id = oDrd("Id")
                .FchETA = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .FchETD = SQLHelper.GetFchFromDataReader(oDrd("FchETD"))
                .Proveidor = New DTOProveidor(oDrd("PrvGuid"))
                .Proveidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

