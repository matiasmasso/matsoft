Public Class TransmisioLoader


    Shared Function Find(oGuid As Guid) As DTOTransmisio
        Dim retval As DTOTransmisio = Nothing
        Dim oTransmisio As New DTOTransmisio(oGuid)
        If Load(oTransmisio) Then
            retval = oTransmisio
        End If
        Return retval
    End Function

    Shared Function FromNum(oEmp As DTOEmp, iYea As Integer, iId As Integer) As DTOTransmisio
        Dim retval As DTOTransmisio = Nothing
        If iYea = 0 Then iYea = DTO.GlobalVariables.Today().Year
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Transm.Guid ")
        sb.AppendLine("FROM Transm ")
        sb.AppendLine("WHERE Transm.Emp=" & oEmp.Id & " AND Transm.Yea=" & iYea & " AND Transm.transm=" & iId & " ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOTransmisio(oDrd("Guid"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oTransmisio As DTOTransmisio) As Boolean
        If Not oTransmisio.IsLoaded And Not oTransmisio.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Transm.Emp, Transm.Yea,Transm.transm,Transm.Fch,Transm.MgzGuid, Mgz.Nom AS MgzAbr ")
            sb.AppendLine(", Alb.Guid as AlbGuid, Alb.Yea as AlbYea, Alb.Alb as AlbId, Alb.Fch as AlbFch, Alb.CliGuid, CliGral.FullNom, Alb.Eur, Alb.CashCod, Alb.Facturable ")
            sb.AppendLine(", Alb.Nom, Alb.Adr, Alb.Zip, Alb.TrpGuid, Alb.CustomerDocURL, Alb.EtiquetesTransport ")
            sb.AppendLine(", Fra.Guid AS FraGuid, Fra.Fra, Fra.Fch AS FraFch ")
            sb.AppendLine(", VwZip.* ")
            sb.AppendLine("FROM Alb ")
            sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid=CliGral.Guid ")
            sb.AppendLine("INNER JOIN Transm ON Alb.TransmGuid=Transm.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Mgz ON Alb.MgzGuid=Mgz.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwZip ON Alb.Zip=VwZip.ZipGuid ")
            sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid=Fra.Guid ")
            sb.AppendLine("WHERE Transm.Guid='" & oTransmisio.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Alb.Yea, Alb.Alb ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oTransmisio.IsLoaded Then
                    With oTransmisio
                        .Emp = New DTOEmp(oDrd("Emp"))
                        .Id = oDrd("transm")
                        .Fch = oDrd("Fch")
                        .Mgz = New DTOMgz(oDrd("MgzGuid"))
                        .Mgz.Abr = SQLHelper.GetStringFromDataReader(oDrd("MgzAbr"))
                        .Deliveries = New List(Of DTODelivery)
                        .IsLoaded = True
                    End With
                End If

                Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))
                oCustomer.FullNom = oDrd("FullNom")

                Dim oDelivery As New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Id = oDrd("AlbId")
                    .Fch = oDrd("AlbFch")
                    .Customer = oCustomer
                    .Import = DTOAmt.Factory(CDec(oDrd("Eur")))
                    .CashCod = oDrd("CashCod")
                    .Transmisio = oTransmisio
                    .Nom = oDrd("Nom")
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    '.Address.Zip = ZipLoader.NewZip(oDrd("Zip"), oDrd("ZipCod"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"))

                    .CustomerDocURL = SQLHelper.GetStringFromDataReader(oDrd("CustomerDocURL"))
                    .EtiquetesTransport = SQLHelper.GetDocFileFromDataReaderHash(oDrd("EtiquetesTransport"))
                    If Not IsDBNull(oDrd("TrpGuid")) Then
                        .Transportista = New DTOTransportista(oDrd("TrpGuid"))
                    End If

                    .Facturable = SQLHelper.GetBooleanFromDatareader(oDrd("Facturable"))
                    If Not IsDBNull(oDrd("FraGuid")) Then
                        .Invoice = New DTOInvoice(oDrd("FraGuid"))
                        .Invoice.Num = SQLHelper.GetIntegerFromDataReader(oDrd("Fra"))
                        .Invoice.Fch = SQLHelper.GetFchFromDataReader(oDrd("FraFch"))
                    End If
                End With

                oTransmisio.Deliveries.Add(oDelivery)
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oTransmisio.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTransmisio, oTrans)
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


    Shared Sub Update(oTransmisio As DTOTransmisio, ByRef oTrans As SqlTransaction)
        UpdateHeader(oTransmisio, oTrans)
        UpdateDeliveries(oTransmisio, oTrans)
    End Sub

    Shared Sub UpdateHeader(oTransmisio As DTOTransmisio, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Transm ")
        sb.AppendLine("WHERE Guid='" & oTransmisio.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oTransmisio.Guid
            If oTransmisio.Id = 0 Then oTransmisio.Id = NextId(oTransmisio, oTrans)
        Else
            oRow = oTb.Rows(0)
        End If

        With oTransmisio
            oRow("Emp") = .Emp.Id
            oRow("Yea") = .Fch.Year
            oRow("Transm") = .Id
            oRow("Fch") = .Fch
            oRow("MgzGuid") = .Mgz.Guid
            oRow("Albs") = .Deliveries.Count

            Dim oAmt As DTOAmt = DTOAmt.Factory(.Deliveries.Sum(Function(x) x.Import.Eur))
            oRow("Eur") = oAmt.Eur
            oRow("Cur") = oAmt.Cur.Tag
            oRow("Val") = oAmt.Val
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateDeliveries(oTransmisio As DTOTransmisio, ByRef oTrans As SqlTransaction)
        If Not oTransmisio.IsNew Then
            RollBackDeliveries(oTransmisio, oTrans)
        End If

        For Each oDelivery As DTODelivery In oTransmisio.Deliveries
            Dim SQL As String = "UPDATE ALB SET TransmGuid=@TransmGuid WHERE Guid=@AlbGuid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@AlbGuid", oDelivery.Guid.ToString, "@TransmGuid", oTransmisio.Guid.ToString, "@TransmYea", oTransmisio.Fch.Year, "@TransmId", oTransmisio.Id)
        Next
    End Sub

    Private Shared Function NextId(oTransmisio As DTOTransmisio, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer = 1
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT MAX(Transm) AS LastId FROM Transm WHERE Emp=" & oTransmisio.Emp.Id & " AND Yea=" & oTransmisio.Fch.Year & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = oTb.Rows(0)
        If Not IsDBNull(oRow("LastId")) Then
            retval = CInt(oRow("LastId")) + 1
        End If
        Return retval
    End Function

    Shared Function Delete(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oTransmisio, oTrans)
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


    Shared Sub Delete(oTransmisio As DTOTransmisio, ByRef oTrans As SqlTransaction)
        RollBackDeliveries(oTransmisio, oTrans)
        Dim SQL As String = "DELETE Transm WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oTransmisio.Guid.ToString())
    End Sub

    Shared Sub RollBackDeliveries(oTransmisio As DTOTransmisio, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE ALB SET TransmGuid=NULL WHERE TransmGuid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oTransmisio.Guid.ToString())
    End Sub



End Class

Public Class TransmisionsLoader

    Shared Function Headers(oMgz As DTOMgz) As List(Of DTOTransmisio)
        Dim retval As New List(Of DTOTransmisio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Transm.Emp, Transm.Guid, Transm.Transm, Transm.Fch, Transm.Eur ")
        sb.AppendLine(", COUNT(Alb.Guid) AS Albs ")
        sb.AppendLine(", SUM(CASE WHEN Alb.FraGuid IS NULL THEN 0 ELSE 1 END) AS InvoicedAlbs ")
        sb.AppendLine(", SUM(CASE WHEN Alb.Facturable=1 THEN 0 ELSE 1 END) AS NoFacturables ")
        sb.AppendLine(", SUM(X.Qty) AS Uds ")
        sb.AppendLine(", SUM(X.Lines) AS Lines ")
        sb.AppendLine("FROM Transm ")
        sb.AppendLine("INNER JOIN Alb ON Transm.Guid = Alb.TransmGuid ")
        sb.AppendLine("INNER JOIN ")
        sb.AppendLine("(SELECT Arc.AlbGuid, SUM(Arc.Qty) AS Qty, COUNT(Arc.Guid) AS Lines FROM Arc GROUP BY Arc.AlbGuid) X ON Alb.Guid = X.AlbGuid ")
        sb.AppendLine("WHERE Transm.MgzGuid ='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Transm.Emp, Transm.Guid, Transm.Transm, Transm.Fch, Transm.Eur ")
        sb.AppendLine("ORDER BY Year(Transm.Fch) DESC, Transm.Transm DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTransmisio(oDrd("Guid"))
            With item
                .Emp = New DTOEmp(oDrd("Emp"))
                .Id = oDrd("Transm")
                .Fch = SQLHelper.GetDateTimeOffsetFromDataReader(oDrd("Fch"))
                .Amt = DTOAmt.Factory(oDrd("Eur"))
                .DeliveriesCount = SQLHelper.GetIntegerFromDataReader(oDrd("Albs"))
                .InvoicedDeliveriesCount = SQLHelper.GetIntegerFromDataReader(oDrd("InvoicedAlbs"))
                .NoFacturablesCount = SQLHelper.GetIntegerFromDataReader(oDrd("NoFacturables"))
                .LinesCount = SQLHelper.GetIntegerFromDataReader(oDrd("Lines"))
                .UnitsCount = SQLHelper.GetIntegerFromDataReader(oDrd("Uds"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function HoldingHeaders(oHolding As DTOHolding, daysFrom As Integer) As List(Of DTOTransmisio)
        Dim retval As New List(Of DTOTransmisio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Transm.Guid, Transm.transm, Transm.Fch ")
        sb.AppendLine("FROM Transm ")
        sb.AppendLine("INNER JOIN Alb ON Transm.Guid = Alb.TransmGuid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON Alb.CliGuid = VwCcxOrMe.Guid ")
        sb.AppendLine("INNER JOIN CliClient ON VwCcxOrMe.Ccx=CliClient.Guid ")
        sb.AppendLine("WHERE CliClient.Holding='" & oHolding.Guid.ToString & "' AND Transm.Fch > DATEADD(day, -" & daysFrom & ",GETDATE()) ")
        sb.AppendLine("GROUP BY Transm.Guid, Transm.transm, Transm.Fch ")
        sb.AppendLine("ORDER BY YEAR(Transm.Fch) DESC, Transm.Transm DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTransmisio(oDrd("Guid"))
            With item
                .Id = oDrd("Transm")
                .Fch = SQLHelper.GetDateTimeOffsetFromDataReader(oDrd("Fch"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Orders(transmGuids As List(Of Guid)) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim firstTime As Boolean = True
        sb.AppendLine("VALUES ")
        For Each oGuid In transmGuids
            If firstTime Then
                firstTime = False
            Else
                sb.Append(", ")
            End If
            sb.AppendLine("('" & oGuid.ToString & "') ")
        Next
        sb.AppendLine()
        sb.AppendLine("SELECT Pdc.Guid, Pdc.Pdd, Pdc.FchMin ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN Arc ON Pnc.Guid = Arc.PncGuid ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN @Table X ON Alb.TransmGuid = X.Guid ")
        sb.AppendLine("GROUP BY Pdc.Guid, Pdc.Pdd, Pdc.FchMin ")
        sb.AppendLine("ORDER BY Pdc.Pdd ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPurchaseOrder(oDrd("Guid"))
            With item
                .Concept = SQLHelper.GetStringFromDataReader(oDrd("Pdd"))
                .FchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("FchMin"))
                .FchDeliveryMax = SQLHelper.GetFchFromDataReader(oDrd("FchMin")) 'to be changed to FchMax when implemented
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

