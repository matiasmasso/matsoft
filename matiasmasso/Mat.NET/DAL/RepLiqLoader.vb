Public Class RepLiqLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTORepLiq
        Dim retval As DTORepLiq = Nothing
        Dim oRepLiq As New DTORepLiq(oGuid)
        If Load(oRepLiq) Then
            retval = oRepLiq
        End If
        Return retval
    End Function

    Shared Function Find(ByVal oRep As DTORep, oInvoice As DTOInvoice) As DTORepLiq
        Dim retVal As DTORepLiq = Nothing
        Dim SQL As String = "SELECT Rps.RepLiqGuid FROM Rps WHERE Rps.FraGuid='" & oInvoice.Guid.ToString & "' AND Rps.RepGuid='" & oRep.Guid.ToString & "' "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("RepLiqGuid")) Then
                Dim oGuid As Guid = oDrd("RepLiqGuid")
                retVal = New DTORepLiq(oGuid)
            End If
        End If
        oDrd.Close()
        Return retVal
    End Function


    Shared Function Pdf(ByVal oRepLiq As DTORepLiq) As Byte()
        Dim retVal = (New List(Of Byte)).ToArray()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT DocFile.Doc ")
        sb.AppendLine("FROM RepLiq ")
        sb.AppendLine("INNER JOIN Cca ON RepLiq.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN DocFile ON Cca.Hash = DocFile.Hash ")
        sb.AppendLine("WHERE RepLiq.Guid = '" & oRepLiq.Guid.ToString() & "'")
        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("Doc")) Then
                retVal = oDrd("Doc")
            End If
        End If
        oDrd.Close()
        Return retVal
    End Function

    Shared Function Load(ByRef oRepLiq As DTORepLiq) As Boolean
        If Not oRepLiq.IsLoaded And Not oRepLiq.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT RepLiq.Id, RepLiq.Fch, RepLiq.RepGuid, CliRep.Abr, RepLiq.BaseFras, RepLiq.ComisioEur,RepLiq.IVAPct, RepLiq.IRPFPct, RepLiq.CcaGuid, Cca.Hash ")
            sb.AppendLine(", Rps.Guid As RpsGuid, Rps.FraGuid, Rps.Bas, Rps.ComVal, Rps.Liquidable, Rps.Obs ")
            sb.AppendLine(", CliGral.Guid AS Ccx, CliGral.LangId, CliGral.RaoSocial, VwAddress.* ")
            sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
            sb.AppendLine(", Fra.Fra, Fra.Fch AS FraFch, Fra.CliGuid, Customer.FullNom AS CustomerNom ")
            sb.AppendLine("FROM RepLiq ")
            sb.AppendLine("INNER JOIN CliRep ON RepLiq.RepGuid = CliRep.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca ON RepLiq.CcaGuid = Cca.Guid ")
            sb.AppendLine("INNER JOIN Rps ON Rps.RepLiqGuid = RepLiq.Guid ")
            sb.AppendLine("INNER JOIN Fra ON Rps.FraGuid = Fra.Guid ")
            sb.AppendLine("INNER JOIN CliGral Customer ON Fra.CliGuid = Customer.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON (CASE WHEN CliRep.CcxGuid IS NULL THEN CliRep.Guid ELSE CliRep.CcxGuid END) = CliGral.Guid ")
            sb.AppendLine("INNER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
            sb.AppendLine("WHERE RepLiq.Guid='" & oRepLiq.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Fra.Fch, Fra.Fra")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oRepLiq.IsLoaded Then

                    Dim oRep As New DTORep(oDrd("RepGuid"))
                    With oRep
                        .Nom = oDrd("Abr")
                        .NickName = oDrd("Abr")
                        .RaoSocialFacturacio = New DTOProveidor(oDrd("Ccx"))
                        With .RaoSocialFacturacio
                            .Nom = oDrd("RaoSocial")
                            .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                            .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                            .Lang = SQLHelper.GetLangFromDataReader(oDrd("LangId"))
                        End With
                    End With
                    With oRepLiq
                        .Id = oDrd("Id")
                        .Fch = oDrd("Fch")
                        .Rep = oRep
                        .BaseFras = Defaults.AmtOrNothing(oDrd("BaseFras"))
                        .BaseImponible = Defaults.AmtOrNothing(oDrd("ComisioEur"))
                        .IvaPct = Defaults.DecimalOrZero(oDrd("IvaPct"))
                        .IrpfPct = Defaults.DecimalOrZero(oDrd("IRPFPct"))
                        .IvaAmt = .BaseImponible.Percent(.IvaPct)
                        .IrpfAmt = .BaseImponible.Percent(.IrpfPct)
                        If Not IsDBNull(oDrd("CcaGuid")) Then
                            .Cca = New DTOCca(oDrd("CcaGuid"))
                            If Not IsDBNull(oDrd("Hash")) Then
                                .Cca.DocFile = New DTODocFile
                                .Cca.DocFile.Hash = oDrd("Hash")
                            End If
                        End If
                        .Items = New List(Of DTORepComLiquidable)
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull("RpsGuid") Then
                    Dim oCustomer As DTOCustomer = Nothing
                    If Not IsDBNull(oDrd("CliGuid")) Then
                        oCustomer = New DTOCustomer(oDrd("CliGuid"))
                        With oCustomer
                            .FullNom = oDrd("CustomerNom")
                        End With
                    End If
                    Dim item As New DTORepComLiquidable(oDrd("RpsGuid"))
                    With item
                        If Not IsDBNull(oDrd("Fra")) Then
                            .Fra = New DTOInvoice(oDrd("FraGuid"))
                            With .Fra
                                .Num = oDrd("Fra")
                                .Fch = oDrd("FraFch")
                                .Customer = oCustomer
                            End With
                            .Rep = oRepLiq.Rep
                            .Liquidable = oDrd("Liquidable")
                            .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                        End If
                        .BaseFras = SQLHelper.GetAmtFromDataReader(oDrd("Bas"))
                        .Comisio = SQLHelper.GetAmtFromDataReader(oDrd("ComVal"))
                    End With
                    oRepLiq.Items.Add(item)
                End If
            Loop

            oDrd.Close()

            If oRepLiq.Rep.RaoSocialFacturacio IsNot Nothing Then
                ProveidorLoader.Load(oRepLiq.Rep.RaoSocialFacturacio)
            End If
        End If

        Dim retval As Boolean = oRepLiq.IsLoaded
        Return retval
    End Function


    Shared Function Update(exs As List(Of Exception), oRepLiq As DTORepLiq) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRepLiq, oTrans)
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

    Shared Function GetDocFile(exs As List(Of Exception), oRepLiq As DTORepLiq, Optional oCert As DTOCert = Nothing) As DTODocFile
        Dim oPdf As New LegacyHelper.PdfRepLiq(oRepLiq)
        Dim oBytes() As Byte = oPdf.Stream(exs, oCert)
        Dim retval = LegacyHelper.DocfileHelper.Factory(exs, oBytes)
        Return retval
    End Function

    Shared Sub Update(oRepLiq As DTORepLiq, oTrans As SqlTransaction)
        Dim exs As New List(Of Exception)
        If oRepLiq.IsNew Then
            oRepLiq.Id = LastId(oRepLiq, oTrans) + 1
            oRepLiq.Cca.Concept = String.Format(oRepLiq.Cca.Concept, oRepLiq.Id)
            'RepLoader.Load(oRepLiq.Rep) 'recull dades per redactar la liquidació
            'oRepLiq.Cca.DocFile = GetDocFile(exs, oRepLiq, oCert) deprecated due To problems On hosting GhostScript In Azure
        End If

        CcaLoader.Update(oRepLiq.Cca, oTrans)
        UpdateHeader(oRepLiq, oTrans)

        If oRepLiq.Items IsNot Nothing Then
            UpdateItems(oRepLiq, oTrans)
        End If

        'UpdatePnd(oRepLiq, oTrans)
    End Sub

    Shared Sub UpdateHeader(oRepLiq As DTORepLiq, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM RepLiq ")
        sb.AppendLine("WHERE Guid='" & oRepLiq.Guid.ToString() & "'")
        Dim SQL As String = sb.ToString

        If oRepLiq.Items IsNot Nothing Then
            With oRepLiq
                .baseFras = DTOAmt.Factory(.items.Sum(Function(x) x.baseFras.Eur))
                .BaseImponible = DTOAmt.Factory(.Items.Sum(Function(x) x.Comisio.Eur))
                .IRPFamt = .BaseImponible.Percent(.IRPFpct)
                .IVAamt = .BaseImponible.Percent(.IVApct)
            End With
        End If

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With oRepLiq
                oRow("Guid") = .Guid
            End With
        Else
            oRow = oTb.Rows(0)
        End If

        With oRepLiq
            oRow("RepGuid") = .Rep.Guid
            oRow("Yea") = .Fch.Year
            oRow("Id") = .Id
            oRow("Fch") = .Fch
            oRow("IVApct") = .IVApct
            oRow("IRPFpct") = .IRPFpct

            oRow("BaseFras") = .BaseFras.Eur
            oRow("ComisioDivisa") = .BaseImponible.Val
            oRow("ComisioEur") = .BaseImponible.Eur
            oRow("Cur") = .BaseImponible.Cur.Tag

            If .Cca IsNot Nothing Then
                oRow("CcaGuid") = .Cca.Guid
            End If

        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oRepLiq As DTORepLiq, ByRef oTrans As SqlTransaction)
        If Not oRepLiq.IsNew Then
            RestoreArcs(oRepLiq, oTrans)
            DeleteRpss(oRepLiq, oTrans)
        End If

        For Each oItem As DTORepComLiquidable In oRepLiq.Items
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("INSERT INTO Rps(Guid, RepGuid, FraGuid, RepLiqGuid, Bas, ComVal) ")
            sb.Append("VALUES( ")
            sb.AppendFormat("'{0}'", oItem.Guid.ToString())
            sb.AppendFormat(", '{0}'", oItem.Rep.Guid.ToString())
            sb.AppendFormat(", '{0}'", oItem.Fra.Guid.ToString())
            sb.AppendFormat(", '{0}'", oRepLiq.Guid.ToString())
            sb.AppendFormat(", {0}", oItem.baseFras.eur.ToString(System.Globalization.CultureInfo.InvariantCulture))
            sb.AppendFormat(", {0}", oItem.Comisio.Eur.ToString(System.Globalization.CultureInfo.InvariantCulture))
            sb.AppendLine(") ")
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            For Each oArc In oItem.Items
                sb = New System.Text.StringBuilder
                sb.AppendLine("UPDATE Arc SET RepComLiquidable = '" & oItem.Guid.ToString & "' ")
                sb.AppendLine("WHERE Guid = '" & oArc.Guid.ToString & "' ")
            Next
        Next
    End Sub

    Shared Sub RestoreArcs(oRepLiq As DTORepLiq, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Arc ")
        sb.AppendLine("SET Arc.RepComLiquidable = NULL ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Rps ON Arc.RepcomLiquidable = Rps.Guid ")
        sb.AppendLine("WHERE Rps.RepLiqGuid='" & oRepLiq.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteRpss(oRepLiq As DTORepLiq, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE Rps ")
        sb.AppendLine("WHERE Rps.RepLiqGuid='" & oRepLiq.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function LastId(oRepLiq As DTORepLiq, oTrans As SqlTransaction) As Integer
        Dim retval As Integer = 0

        Dim SQL As String = "SELECT TOP 1 Id AS LastId FROM RepLiq WHERE " _
        & "RepGuid='" & oRepLiq.Rep.Guid.ToString & "' AND Yea=" & oRepLiq.Fch.Year & " " _
        & "ORDER BY Id DESC"

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            retval = oRow("LastId")
        End If

        Return retval
    End Function

    Shared Function Delete(oRepLiq As DTORepLiq, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRepLiq, oTrans)
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


    Shared Sub Delete(oRepLiq As DTORepLiq, ByRef oTrans As SqlTransaction)
        RestoreArcs(oRepLiq, oTrans)
        DeleteRpss(oRepLiq, oTrans)
        DeleteHeader(oRepLiq, oTrans)
        If oRepLiq.Cca IsNot Nothing Then
            CcaLoader.Delete(oRepLiq.Cca, oTrans)
        End If
    End Sub

    Shared Sub DeleteHeader(oRepLiq As DTORepLiq, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE RepLiq WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oRepLiq.Guid.ToString())
    End Sub

#End Region

End Class

Public Class RepLiqsLoader



    Shared Function Headers(Optional oEmp As DTOEmp = Nothing, Optional oInvoice As DTOInvoice = Nothing, Optional oRep As DTORep = Nothing, Optional oUser As DTOUser = Nothing) As List(Of DTORepLiq)
        Dim retval As New List(Of DTORepLiq)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepLiq.Guid, RepLiq.Id, RepLiq.Fch, RepLiq.RepGuid, CliRep.Abr, RepLiq.CcaGuid, Cca.Hash ")
        sb.AppendLine(", BaseFras, ComisioEur, IvaPct, IRpfPct ")
        sb.AppendLine(", CliRep.Abr, CliGral.FullNom ")
        sb.AppendLine("FROM RepLiq ")
        sb.AppendLine("INNER JOIN CliRep ON RepLiq.RepGuid = CliRep.Guid ")
        If oUser IsNot Nothing Then
            sb.AppendLine("INNER JOIN Email_Clis ON RepLiq.RepGuid = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString() & "' ")
        End If
        sb.AppendLine("INNER JOIN CliGral ON RepLiq.RepGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Rps ON Rps.RepLiqGuid = RepLiq.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON RepLiq.CcaGuid=Cca.Guid ")

        If oInvoice Is Nothing Then
            If oRep Is Nothing Then
                If oEmp IsNot Nothing Then
                    sb.AppendLine("WHERE Cca.Emp='" & oEmp.Id & "' ")
                End If
            Else
                sb.AppendLine("WHERE RepLiq.RepGuid='" & oRep.Guid.ToString & "' ")
            End If
        Else
            sb.AppendLine("WHERE Rps.FraGuid='" & oInvoice.Guid.ToString & "' ")
            If oRep IsNot Nothing Then
                sb.AppendLine("AND RepLiq.RepGuid='" & oRep.Guid.ToString & "' ")
            End If
        End If
        sb.AppendLine("GROUP BY RepLiq.Guid, RepLiq.Id, RepLiq.Fch, RepLiq.RepGuid, CliRep.Abr, RepLiq.CcaGuid, Cca.Hash ")
        sb.AppendLine(", BaseFras, ComisioEur, IvaPct, IRpfPct, CliRep.Abr, CliGral.FullNom ")

        sb.AppendLine("ORDER BY RepLiq.Fch DESC, Clirep.abr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim item As New DTORepLiq(oDrd("Guid"))
            With item
                .Id = oDrd("Id")
                .Fch = oDrd("Fch")
                .Rep = New DTORep(oDrd("RepGuid"))
                .Rep.FullNom = oDrd("FullNom")
                .Rep.NickName = oDrd("Abr")
                .BaseFras = DTOAmt.Factory(oDrd("BaseFras"))
                .BaseImponible = DTOAmt.Factory(oDrd("ComisioEur"))
                .IvaPct = SQLHelper.GetDecimalFromDataReader(oDrd("IvaPct"))
                .IrpfPct = SQLHelper.GetDecimalFromDataReader(oDrd("IRpfPct"))
                .IvaAmt = .BaseImponible.percent(.IvaPct)
                .IrpfAmt = .BaseImponible.percent(.IrpfPct)
                If Not IsDBNull(oDrd("CcaGuid")) Then
                    .Cca = New DTOCca(oDrd("CcaGuid"))
                    If Not IsDBNull(oDrd("Hash")) Then
                        .Cca.docFile = New DTODocFile
                        .Cca.docFile.Hash = oDrd("Hash")
                    End If
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Model(oUser As DTOUser) As Models.RepLiqsModel
        Dim retval As New Models.RepLiqsModel
        Dim oRep As New Models.RepLiqsModel.Rep

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepLiq.RepGuid, RepLiq.Guid, RepLiq.Fch, RepLiq.BaseFras, RepLiq.ComisioEur, RepLiq.IvaPct, RepLiq.IrpfPct ")
        sb.AppendLine(", CliRep.Abr, CliGral.RaoSocial ")
        sb.AppendLine("FROM RepLiq ")
        sb.AppendLine("LEFT OUTER JOIN CliRep ON RepLiq.RepGuid = CliRep.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON RepLiq.RepGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON CliRep.Guid = Email_Clis.ContactGuid ")
        sb.AppendLine("WHERE RepLiq.Cur ='EUR' ")

        Select Case oUser.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts
                sb.AppendLine("AND CliGral.Emp =" & oUser.Emp.Id & " ")
            Case DTORol.Ids.rep
                sb.AppendLine("AND Email_Clis.EmailGuid ='" & oUser.Guid.ToString & "' ")
        End Select
        sb.AppendLine("GROUP BY RepLiq.RepGuid, RepLiq.Guid, RepLiq.Fch, RepLiq.BaseFras, RepLiq.ComisioEur, RepLiq.IvaPct, RepLiq.IrpfPct ")
        sb.AppendLine(", CliRep.Abr, CliGral.RaoSocial ")
        sb.AppendLine("ORDER BY RepLiq.RepGuid, RepLiq.Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oRep.Guid.Equals(oDrd("RepGuid")) Then
                oRep = New Models.RepLiqsModel.Rep
                oRep.Guid = oDrd("RepGuid")
                oRep.Nom = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                If String.IsNullOrEmpty(oRep.Nom) Then oRep.Nom = oDrd("RaoSocial")
                retval.Reps.Add(oRep)
            End If
            Dim item As New Models.RepLiqsModel.Item()
            With item
                .Guid = oDrd("Guid")
                .Fch = oDrd("Fch")
                .Base = SQLHelper.GetDecimalFromDataReader(oDrd("ComisioEur"))
                .Iva = SQLHelper.GetDecimalFromDataReader(oDrd("IvaPct"))
                .Irpf = SQLHelper.GetDecimalFromDataReader(oDrd("IRPFPct"))
            End With
            oRep.Items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
    Shared Function All(oEmp As DTOEmp, Optional oRep As DTORep = Nothing) As List(Of DTORepLiq)
        Dim retval As New List(Of DTORepLiq)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepLiq.*, CliRep.Abr, Cca.Hash ")
        sb.AppendLine("FROM RepLiq ")
        sb.AppendLine("INNER JOIN CliRep ON RepLiq.RepGuid = CliRep.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON RepLiq.CcaGuid=Cca.Guid ")
        If oRep IsNot Nothing Then
            sb.AppendLine("WHERE RepGuid='" & oRep.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim item As New DTORepLiq(oDrd("Guid"))
            With item
                .Id = oDrd("Id")
                .Fch = oDrd("Fch")
                If oRep Is Nothing Then
                    .Rep = New DTORep(oDrd("RepGuid"))
                    .Rep.Nom = oDrd("Abr")
                Else
                    .Rep = oRep
                End If
                .BaseFras = Defaults.AmtOrNothing(oDrd("BaseFras"))
                .BaseImponible = Defaults.AmtOrNothing(oDrd("ComisioEur"))
                .IvaPct = Defaults.DecimalOrZero(oDrd("IvaPct"))
                .IrpfPct = Defaults.DecimalOrZero(oDrd("IRPFPct"))
                If Not IsDBNull(oDrd("CcaGuid")) Then
                    .Cca = New DTOCca(oDrd("CcaGuid"))
                    If Not IsDBNull(oDrd("Hash")) Then
                        .Cca.DocFile = New DTODocFile
                        .Cca.DocFile.Hash = oDrd("Hash")
                    End If
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, Optional iYea As Integer = 0) As List(Of DTORepLiq)
        Dim retval As New List(Of DTORepLiq)
        If iYea = 0 Then iYea = DTO.GlobalVariables.Today().Year

        Dim SQL As String = "SELECT RepLiq.Guid,RepLiq.RepGuid,RepLiq.Fch,RepLiq.Id,RepLiq.Yea, " _
                            & "RepLiq.BaseFras,RepLiq.ComisioEur,RepLiq.Cur,RepLiq.ComisioDivisa,RepLiq.IVAPct as IVA,RepLiq.IRPFPct as Irpf,RepLiq.CcaGuid, " _
                            & "CliRep.Abr, Cca.Hash " _
                            & "FROM RepLiq " _
                            & "INNER JOIN CliGral ON CliRep.Guid=Cli Gral.Guid " _
                            & "LEFT OUTER JOIN CliRep ON RepLiq.RepGuid=CliRep.Guid " _
                            & "LEFT OUTER JOIN Cca ON RepLiq.CcaGuid=Cca.Guid " _
                            & "WHERE CliGral.Emp=" & oEmp.Id & " AND RepLiq.Yea=@Yea " _
                            & "ORDER BY RepLiq.Fch DESC, CliRep.ABR"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Yea", iYea)
        Do While oDrd.Read
            Dim oRep As New DTORep(DirectCast(oDrd("RepGuid"), Guid))
            oRep.NickName = oDrd("Abr").ToString

            Dim oRepLiq As New DTORepLiq(DirectCast(oDrd("Guid"), Guid))
            With oRepLiq
                .Rep = oRep
                .Fch = oDrd("Fch")
                .Id = oDrd("Id")
                .BaseFras = DTOAmt.Factory(CDec(oDrd("BaseFras")))
                .BaseImponible = DTOAmt.Factory(CDec(oDrd("ComisioEur")), oDrd("Cur").ToString, CDec(oDrd("ComisioDivisa")))
                .IRPFpct = oDrd("Irpf")
                .IVApct = oDrd("IVA")
                If Not IsDBNull(oDrd("CcaGuid")) Then
                    .Cca = New DTOCca(DirectCast(oDrd("CcaGuid"), Guid))
                End If
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash"))
                End If
            End With
            retval.Add(oRepLiq)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Delete(oRepLiqs As List(Of DTORepLiq), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            For Each item As DTORepLiq In oRepLiqs
                RepLiqLoader.Delete(item, oTrans)
            Next

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
End Class

