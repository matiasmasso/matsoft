Public Class IntrastatLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOIntrastat
        Dim retval As DTOIntrastat = Nothing
        Dim oIntrastat As New DTOIntrastat(oGuid)
        If Load(oIntrastat) Then
            retval = oIntrastat
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oIntrastat As DTOIntrastat) As Boolean
        Dim retval As Boolean
        Select Case oIntrastat.Flujo
            'Case DTOIntrastat.Flujos.Introduccion
             '   retval = LoadOld(oIntrastat)
            Case DTOIntrastat.Flujos.Introduccion, DTOIntrastat.Flujos.Expedicion
                If Not oIntrastat.IsLoaded And Not oIntrastat.IsNew Then
                    Dim sb As New Text.StringBuilder
                    sb.AppendLine("SELECT Intrastat.* ")
                    sb.AppendLine(", IntrastatPartida.* ")
                    sb.AppendLine(", Fra.Fra AS FraId, Fra.Fch AS FraFch, Fra.CliGuid AS FraCliGuid, Client.RaoSocial AS FraCliNom ")
                    sb.AppendLine(", Alb.Alb AS AlbId, Alb.Fch AS AlbFch, Alb.CliGuid AS AlbCliGuid , Proveidor.RaoSocial AS AlbCliNom ")
                    sb.AppendLine(", Country.Iso AS CountryIso, Provincia.Intrastat AS CodProvincia, MadeIn.Iso AS MadeinIso ")
                    sb.AppendLine(", Client.Nif AS NifClient, Proveidor.Nif AS NifProveidor ")
                    sb.AppendLine("FROM Intrastat ")
                    sb.AppendLine("LEFT OUTER JOIN IntrastatPartida ON Intrastat.Guid = IntrastatPartida.Intrastat ")
                    sb.AppendLine("LEFT OUTER JOIN Country ON IntrastatPartida.Country = Country.Guid ")
                    sb.AppendLine("LEFT OUTER JOIN Provincia ON IntrastatPartida.Provincia = Provincia.Guid ")
                    sb.AppendLine("LEFT OUTER JOIN Country AS MadeIn ON IntrastatPartida.MadeIn = MadeIn.Guid ")
                    sb.AppendLine("LEFT OUTER JOIN Fra ON IntrastatPartida.Tag = Fra.Guid ")
                    sb.AppendLine("LEFT OUTER JOIN Alb ON IntrastatPartida.Tag = Alb.Guid ")
                    sb.AppendLine("LEFT OUTER JOIN CliGral AS Client ON Fra.CliGuid = Client.Guid ")
                    sb.AppendLine("LEFT OUTER JOIN CliGral AS Proveidor ON Alb.CliGuid = Proveidor.Guid ")
                    sb.AppendLine("WHERE Intrastat.Guid='" & oIntrastat.Guid.ToString & "' ")
                    sb.AppendLine("ORDER BY IntrastatPartida.Lin ")

                    Dim SQL As String = sb.ToString

                    Dim oImportacio As New DTOImportacio()

                    Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
                    Do While oDrd.Read
                        With oIntrastat
                            If Not .IsLoaded Then
                                If .Emp Is Nothing Then .Emp = New DTOEmp(oDrd("Emp"))
                                .Flujo = CInt(oDrd("Flujo"))
                                .Yea = CInt(oDrd("Yea"))
                                .Mes = CInt(oDrd("Mes"))
                                .Ord = CInt(oDrd("Ord"))
                                .Csv = SQLHelper.GetStringFromDataReader(oDrd("Csv"))
                                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                                .Partidas = New List(Of DTOIntrastat.Partida)
                                .IsLoaded = True
                                .IsNew = False
                            End If
                        End With

                        If Not IsDBNull(oDrd("Lin")) Then
                            Dim oPartida As New DTOIntrastat.Partida
                            With oPartida
                                .Lin = oDrd("Lin")
                                If Not IsDBNull(oDrd("FraId")) Then
                                    Dim oInvoice As New DTOInvoice(oDrd("Tag"))
                                    With oInvoice
                                        .Num = oDrd("FraId")
                                        .Fch = oDrd("FraFch")
                                        .Customer = New DTOCustomer(oDrd("FraCliGuid"))
                                        .Customer.Nom = SQLHelper.GetStringFromDataReader(oDrd("FraCliNom"))
                                    End With
                                    .Tag = oInvoice
                                ElseIf Not IsDBNull(oDrd("AlbId")) Then
                                    Dim oDelivery As New DTODelivery(oDrd("Tag"))
                                    With oDelivery
                                        .Id = oDrd("AlbId")
                                        .Fch = oDrd("AlbFch")
                                        .Customer = New DTOCustomer(oDrd("AlbCliGuid"))
                                        .Customer.Nom = SQLHelper.GetStringFromDataReader(oDrd("AlbCliNom"))
                                    End With
                                    .Tag = oDelivery
                                End If
                                .Country = New DTOCountry(oDrd("Country"))
                                .Country.ISO = oDrd("CountryIso")
                                If Not IsDBNull(oDrd("Provincia")) Then
                                    .Provincia = New DTOAreaProvincia(oDrd("Provincia"))
                                    .Provincia.Intrastat = SQLHelper.GetStringFromDataReader(oDrd("CodProvincia"))
                                End If
                                .Incoterm = SQLHelper.GetIncotermFromDataReader(oDrd("Incoterm"))
                                .NaturalezaTransaccion = oDrd("NaturalezaTransaccion")
                                .CodiTransport = oDrd("CodiTransport")
                                .CodiMercancia = New DTOCodiMercancia(oDrd("CodiMercancia").ToString())
                                If Not IsDBNull(oDrd("MadeIn")) Then
                                    .madeIn = New DTOCountry(oDrd("MadeIn"))
                                    .madeIn.ISO = SQLHelper.GetStringFromDataReader(oDrd("MadeinIso"))
                                End If
                                .RegimenEstadistico = oDrd("RegimenEstadistico")
                                .UnidadesSuplementarias = SQLHelper.GetIntegerFromDataReader(oDrd("UnidadesSuplementarias"))
                                .Kg = oDrd("Kg")
                                .ImporteFacturado = oDrd("ImporteFacturado")
                                .ValorEstadistico = oDrd("ValorEstadistico")
                                .NifContraparte = IIf(oIntrastat.Flujo = DTOIntrastat.Flujos.expedicion, SQLHelper.GetStringFromDataReader(oDrd("NifClient")), SQLHelper.GetStringFromDataReader(oDrd("NifProveidor")))
                            End With
                            oIntrastat.Partidas.Add(oPartida)
                        End If
                    Loop

                End If
                retval = oIntrastat.IsLoaded
        End Select
        Return retval
    End Function


    Shared Function Update(oIntrastat As DTOIntrastat, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oIntrastat, oTrans)
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

    Shared Sub Update(oIntrastat As DTOIntrastat, ByRef oTrans As SqlTransaction)
        DocFileLoader.Update(oIntrastat.DocFile, oTrans)
        DeletePartidas(oIntrastat, oTrans)
        UpdateHeader(oIntrastat, oTrans)
        UpdatePartidas(oIntrastat, oTrans)
    End Sub


    Shared Sub UpdateHeader(oIntrastat As DTOIntrastat, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Intrastat ")
        sb.AppendLine("WHERE Guid='" & oIntrastat.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oIntrastat.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oIntrastat
            oRow("Emp") = .Emp.Id
            oRow("Flujo") = .Flujo
            oRow("Yea") = .Yea
            oRow("Mes") = .Mes
            oRow("Ord") = .Ord
            oRow("Csv") = .Csv
            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdatePartidas(oIntrastat As DTOIntrastat, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM IntrastatPartida ")
        sb.AppendLine("WHERE Intrastat='" & oIntrastat.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item As DTOIntrastat.Partida In oIntrastat.Partidas
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Intrastat") = oIntrastat.Guid
            oRow("Lin") = oIntrastat.Partidas.IndexOf(item)
            With item
                oRow("Tag") = SQLHelper.NullableBaseGuid(.Tag)
                oRow("Country") = .Country.Guid
                oRow("Provincia") = SQLHelper.NullableBaseGuid(.Provincia)
                oRow("Incoterm") = .Incoterm.Id.ToString
                oRow("NaturalezaTransaccion") = .NaturalezaTransaccion
                oRow("CodiTransport") = .CodiTransport
                oRow("CodiMercancia") = .CodiMercancia.Id
                oRow("MadeIn") = SQLHelper.NullableBaseGuid(.MadeIn)
                oRow("RegimenEstadistico") = .RegimenEstadistico
                oRow("UnidadesSuplementarias") = .UnidadesSuplementarias
                oRow("Kg") = .Kg
                oRow("ImporteFacturado") = .ImporteFacturado
                oRow("ValorEstadistico") = .ValorEstadistico
            End With
        Next
        oDA.Update(oDs)

    End Sub


    Shared Function Delete(oIntrastat As DTOIntrastat, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oIntrastat, oTrans)
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

    Shared Sub Delete(oIntrastat As DTOIntrastat, ByRef oTrans As SqlTransaction)
        DeletePartidas(oIntrastat, oTrans)
        DeleteHeader(oIntrastat, oTrans)
    End Sub


    Shared Sub DeletePartidas(oIntrastat As DTOIntrastat, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE IntrastatPartida WHERE Intrastat = '" & oIntrastat.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Sub DeleteHeader(oIntrastat As DTOIntrastat, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Intrastat WHERE Guid='" & oIntrastat.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function NextOrd(oEmp As DTOEmp, oFlujo As DTOIntrastat.Flujos, oYearMonth As DTOYearMonth) As Integer
        Dim SQL As String = "SELECT MAX(Ord) AS LastOrd FROM Intrastat WHERE Emp=" & oEmp.Id & " AND Yea = " & oYearMonth.Year & " AND Mes = " & oYearMonth.Month & " AND Flujo = " & oFlujo & " "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()
        Dim retval As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("LastOrd")) + 1
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class IntrastatsLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOIntrastat)
        Dim retval As New List(Of DTOIntrastat)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Intrastat.Guid, Intrastat.Yea, Intrastat.Mes, Intrastat.Flujo, Intrastat.Ord ")
        sb.AppendLine(", Intrastat.Csv, Intrastat.Hash ")
        sb.AppendLine(", COUNT(IntrastatPartida.Lin) AS Partidas ")
        sb.AppendLine(", SUM(IntrastatPartida.UnidadesSuplementarias) AS Units ")
        sb.AppendLine(", SUM(IntrastatPartida.Kg) AS Kgs ")
        sb.AppendLine(", SUM(IntrastatPartida.ImporteFacturado) AS Eurs ")
        sb.AppendLine("FROM Intrastat ")
        sb.AppendLine("LEFT OUTER JOIN IntrastatPartida ON Intrastat.Guid = IntrastatPartida.Intrastat ")
        sb.AppendLine("WHERE Intrastat.Emp = " & oEmp.Id & " ")
        sb.AppendLine("GROUP BY Intrastat.Guid, Intrastat.Yea, Intrastat.Mes, Intrastat.Flujo, Intrastat.Ord ")
        sb.AppendLine(", Intrastat.Csv, Intrastat.Hash ")
        sb.AppendLine("ORDER BY Intrastat.Yea DESC, Intrastat.Mes DESC, Intrastat.Flujo, Intrastat.Ord ")

        Dim SQL As String = sb.ToString

        Dim oIntrastat As New DTOIntrastat

        Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oIntrastat.Guid.Equals(oDrd("Guid")) Then
                oIntrastat = New DTOIntrastat(oDrd("Guid"))
                retval.Add(oIntrastat)
                With oIntrastat
                    If Not .IsLoaded Then
                        .Emp = oEmp
                        .Flujo = CInt(oDrd("Flujo"))
                        .Yea = CInt(oDrd("Yea"))
                        .Mes = CInt(oDrd("Mes"))
                        .Ord = CInt(oDrd("Ord"))
                        .PartidasCount = SQLHelper.GetIntegerFromDataReader(oDrd("Partidas"))
                        .Units = SQLHelper.GetIntegerFromDataReader(oDrd("Units"))
                        .Kg = SQLHelper.GetDecimalFromDataReader(oDrd("Kgs"))
                        .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Eurs"))
                        .Csv = SQLHelper.GetStringFromDataReader(oDrd("Csv"))
                        .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                        .Partidas = New List(Of DTOIntrastat.Partida)
                    End If
                End With
            End If


        Loop
        oDrd.Close()

        Return retval
    End Function


End Class
