Public Class OutVivaceLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOOutVivaceLog
        Dim retval As DTOOutVivaceLog = Nothing
        Dim oOutVivaceExpedicio As New DTOOutVivaceLog(oGuid)
        If Load(oOutVivaceExpedicio) Then
            retval = oOutVivaceExpedicio
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oOutVivaceExpedicio As DTOOutVivaceLog) As Boolean
        If Not oOutVivaceExpedicio.IsLoaded And Not oOutVivaceExpedicio.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM OutVivaceExpedicio ")
            sb.AppendLine("WHERE Guid='" & oOutVivaceExpedicio.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oOutVivaceExpedicio
                    '.Nom = oDrd("Nom")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oOutVivaceExpedicio.IsLoaded
        Return retval
    End Function

    Shared Function Update(oOutVivaceExpedicio As DTOOutVivaceLog, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oOutVivaceExpedicio, oTrans)
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


    Shared Sub Update(oOutVivaceExpedicio As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM OutVivaceExpedicio ")
        sb.AppendLine("WHERE Guid='" & oOutVivaceExpedicio.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oOutVivaceExpedicio.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oOutVivaceExpedicio
            'oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oOutVivaceExpedicio As DTOOutVivaceLog, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oOutVivaceExpedicio, oTrans)
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


    Shared Sub Delete(oOutVivaceExpedicio As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE OutVivaceExpedicio WHERE Guid='" & oOutVivaceExpedicio.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function SkusQtyEur(oOutVivaceExpedicio As DTOOutVivaceLog.expedicion) As List(Of DTOProductSkuQtyEur)
        Dim retval As New List(Of DTOProductSkuQtyEur)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.ArtGuid, VwSkuNom.SkuNomLlarg, Arc.Qty, Arc.Eur, Arc.Dto ")
        sb.appendline(", VwSkuNom.HeredaDimensions, VwSkuNom.SkuDimensionL, VwSkuNom.SkuDimensionW, VwSkuNom.SkuDimensionH ")
        sb.AppendLine(", VwSkuNom.CategoryDimensionL, VwSkuNom.CategoryDimensionH, VwSkuNom.CategoryDimensionW ")
        sb.appendline(", VwSkuNom.SkuMoq, VwSkuNom.CategoryMoq ")
        sb.AppendLine(", VwSkuNom.SkuForzarMoq, VwSkuNom.CategoryForzarMoq ")
        sb.AppendLine("FROM OutVivaceAlbExp ")
        sb.appendline("INNER JOIN Arc ON OutVivaceAlbExp.Alb=Arc.AlbGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE OutVivaceAlbExp.Expedicio='AEB04266-5C06-47B7-B45F-336EFE1218A9' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductSkuQtyEur(oDrd("ArtGuid"))
            With item
                .NomLlarg = SQLHelper.GetStringFromDataReader(oDrd("SkuNomLlarg"))
                .Qty = oDrd("Qty")
                .Price = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                .CustomerDto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .HeredaDimensions = SQLHelper.GetBooleanFromDatareader(oDrd("HeredaDimensions"))
                .DimensionL = SQLHelper.GetDecimalFromDataReader(oDrd("SkuDimensionL"))
                .DimensionH = SQLHelper.GetDecimalFromDataReader(oDrd("SkuDimensionH"))
                .DimensionW = SQLHelper.GetDecimalFromDataReader(oDrd("SkuDimensionW"))
            End With
        Loop
        oDrd.Close()
        Return retval

    End Function

End Class

Public Class OutVivaceExpedicionsLoader

    Shared Function All() As List(Of DTOOutVivaceLog.expedicion)
        Dim retval As New List(Of DTOOutVivaceLog.expedicion)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT OutVivaceLog.Guid AS LogGuid, OutVivaceLog.Fch AS LogFch, OutVivaceLog.FchCreated AS LogFchCreated ")
        sb.AppendLine(", OutVivaceExpedicio.Guid AS ExpGuid, OutVivaceExpedicio.id AS ExpId, OutVivaceExpedicio.Bultos, OutVivaceExpedicio.Kg, OutVivaceExpedicio.M3 ")
        sb.AppendLine(", OutVivaceExpedicio.TranspNif, Trp.TrpGuid, Trp.TrpNom ")
        sb.AppendLine(", OutVivaceAlbExp.Alb AS AlbGuid, Alb.Alb, Alb.Fch AS AlbFch, Alb.Nom AS AlbNom, Alb.Eur, Alb.Adr, Alb.Zip, VwZip.* ")
        sb.AppendLine(", C1.Guid AS C1Guid, C1.Qty AS C1Qty, C1.Ref AS C1Ref, C1.Price AS C1Price ")
        sb.AppendLine(", C2.Guid AS C2Guid, C2.Qty AS C2Qty, C2.Ref AS C2Ref, C2.Price AS C2Price ")
        sb.AppendLine("FROM OutVivaceLog ")
        sb.AppendLine("INNER JOIN OutVivaceExpedicio ON OutVivaceLog.Guid=OutVivaceExpedicio.Log ")
        sb.AppendLine("LEFT OUTER JOIN OutVivaceAlbExp ON OutVivaceExpedicio.Guid = OutVivaceAlbExp.Expedicio ")
        sb.AppendLine("LEFT OUTER JOIN OutVivaceCarrec C1 ON OutVivaceExpedicio.Guid = C1.Parent ")
        sb.AppendLine("LEFT OUTER JOIN OutVivaceCarrec C2 ON OutVivaceAlbExp.Alb = C2.Parent ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT CliGral.NIF, MAX(CliGral.Guid) AS TrpGuid, MAX(CliGral.RaoSocial) AS TrpNom FROM CliGral GROUP BY CliGral.NIF) Trp ON OutVivaceExpedicio.TranspNif = Trp.Nif ")
        sb.AppendLine("INNER JOIN Alb ON OutVivaceAlbExp.Alb = Alb.Guid ")
        sb.AppendLine("INNER JOIN VwZip ON Alb.Zip = VwZip.ZipGuid ")
        sb.AppendLine("ORDER BY Alb.Alb DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oLog As New DTOOutVivaceLog
        Dim oExp As New DTOOutVivaceLog.expedicion
        Dim oAlb As New DTOOutVivaceLog.albaran
        Dim oTrp As New DTOGuidNom
        Do While oDrd.Read
            If Not oLog.Guid.Equals(oDrd("LogGuid")) Then
                oLog = New DTOOutVivaceLog(oDrd("LogGuid"))
                With oLog
                    .fchCreated = oDrd("LogFchCreated")
                    .fecha = oDrd("LogFch")
                End With
            End If
            If Not oExp.Guid.Equals(oDrd("ExpGuid")) Then
                oExp = New DTOOutVivaceLog.expedicion(oDrd("ExpGuid"))
                With oExp
                    .log = oLog
                    .vivace = oDrd("ExpId")
                    .bultos = oDrd("Bultos")
                    .kg = oDrd("kg")
                    .m3 = oDrd("m3")
                    If oTrp.Guid.Equals(oDrd("TrpGuid")) Then
                        .Trp = oTrp
                    ElseIf oDrd("TranspNif") = "" Then
                        .Trp = Nothing
                        oTrp = New DTOGuidNom(Guid.NewGuid)
                    ElseIf IsDBNull(oDrd("TrpGuid")) Then
                        .Trp = Nothing
                        oTrp = New DTOGuidNom(Guid.NewGuid)
                    Else
                        oTrp = New DTOGuidNom
                        oTrp.Guid = oDrd("TrpGuid")
                        oTrp.Nom = oDrd("TrpNom")
                        .Trp = oTrp
                    End If
                    .albaranes = New List(Of DTOOutVivaceLog.albaran)
                    .cargos = New List(Of DTOOutVivaceLog.cargo)
                End With
                retval.Add(oExp)
            End If
            If Not oAlb.guid.Equals(oDrd("AlbGuid")) Then
                oAlb = New DTOOutVivaceLog.albaran()
                With oAlb
                    .guid = oDrd("AlbGuid")
                    .numero = oDrd("Alb")
                    .delivery = New DTODelivery(oDrd("AlbGuid"))
                    With .delivery
                        .Id = oDrd("Alb")
                        .Fch = oDrd("AlbFch")
                        .Nom = oDrd("AlbNom")
                        .Address = New DTOAddress()
                        With .Address
                            .Text = oDrd("Adr")
                            .Zip = SQLHelper.GetZipFromDataReader(oDrd)
                        End With
                        .Import = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                    End With
                    .cargos = New List(Of DTOOutVivaceLog.cargo)
                End With
                oExp.albaranes.Add(oAlb)
            End If

            If Not IsDBNull(oDrd("C1Guid")) Then
                If Not oExp.cargos.Exists(Function(x) x.Guid.Equals(oDrd("C1Guid"))) Then
                    Dim oCargo As New DTOOutVivaceLog.cargo
                    With oCargo
                        .Guid = oDrd("C1Guid")
                        .unidades = oDrd("C1Qty")
                        .precio = oDrd("C1Price")
                        .codigo = oDrd("C1Ref")
                        .src = oExp
                    End With
                    oExp.cargos.Add(oCargo)
                End If
            End If

            If Not IsDBNull(oDrd("C2Guid")) Then
                If Not oAlb.cargos.Exists(Function(x) x.Guid.Equals(oDrd("C2Guid"))) Then
                    Dim oCargo As New DTOOutVivaceLog.cargo
                    With oCargo
                        .Guid = oDrd("C2Guid")
                        .unidades = oDrd("C2Qty")
                        .precio = oDrd("C2Price")
                        .codigo = oDrd("C2Ref")
                        .src = oAlb
                    End With
                    oAlb.cargos.Add(oCargo)
                End If
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
