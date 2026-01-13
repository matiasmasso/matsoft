Public Class NominaLoader

#Region "CRUD"

    Shared Function Find(oCca As DTOCca) As DTONomina
        Dim retval As DTONomina = Nothing
        Dim oNomina As New DTONomina(oCca)
        If Load(oNomina) Then
            retval = oNomina
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oNomina As DTONomina) As Boolean
        If Not oNomina.IsLoaded And Not oNomina.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Nomina.CcaGuid, Cca.Fch, Nomina.Staff, CliGral.RaoSocial ")
            sb.AppendLine(", Nomina.Devengat, Nomina.Dietes, Nomina.SegSocial, Nomina.IrpfBase, Nomina.Irpf, Nomina.Embargos, Nomina.Deutes, Nomina.Anticips, Nomina.Liquid ")
            sb.AppendLine(", Cca.Cca, Cca.Hash ")
            sb.AppendLine("FROM Nomina ")
            sb.AppendLine("INNER JOIN Cca ON Nomina.CcaGuid = Cca.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON Nomina.Staff = CliGral.Guid ")
            sb.AppendLine("WHERE Nomina.CcaGuid='" & oNomina.Cca.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oNomina
                    .Cca.Id = oDrd("Cca")
                    .Cca.Fch = oDrd("Fch")
                    .Staff = New DTOStaff(oDrd("Staff"))
                    .Staff.Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                    .Cca.DocFile = New DTODocFile(oDrd("Hash"))
                    .Devengat = DTOAmt.Factory(oDrd("Devengat"))
                    .Dietes = DTOAmt.Factory(oDrd("Dietes"))
                    .SegSocial = DTOAmt.Factory(oDrd("SegSocial"))
                    .IrpfBase = DTOAmt.Factory(oDrd("IrpfBase"))
                    .Irpf = DTOAmt.Factory(oDrd("Irpf"))
                    .Embargos = DTOAmt.Factory(oDrd("Embargos"))
                    .Deutes = DTOAmt.Factory(oDrd("Deutes"))
                    .Anticips = DTOAmt.Factory(oDrd("Anticips"))
                    .Liquid = DTOAmt.Factory(oDrd("Liquid"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oNomina.IsLoaded
        Return retval
    End Function

    Shared Function Update(oNomina As DTONomina, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oNomina, oTrans)
            oTrans.Commit()
            oNomina.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oNomina As DTONomina, oTrans As SqlTransaction)
        CcaLoader.Update(oNomina.Cca, oTrans)
        UpdateHeader(oNomina, oTrans)
        UpdateConceptes(oNomina, oTrans)
        UpdateItems(oNomina, oTrans)
    End Sub


    Shared Sub UpdateHeader(oNomina As DTONomina, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Nomina ")
        sb.AppendLine("WHERE Nomina.CcaGuid='" & oNomina.Cca.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("CcaGuid") = oNomina.Cca.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oNomina
            oRow("Staff") = .Staff.Guid
            oRow("Fch") = .Cca.Fch
            oRow("Devengat") = SQLHelper.NullableAmt(.Devengat)
            oRow("Dietes") = SQLHelper.NullableAmt(.Dietes)
            oRow("SegSocial") = SQLHelper.NullableAmt(.SegSocial)
            oRow("IrpfBase") = SQLHelper.NullableAmt(.IrpfBase)
            oRow("Irpf") = SQLHelper.NullableAmt(.Irpf)
            oRow("Embargos") = SQLHelper.NullableAmt(.Embargos)
            oRow("Deutes") = SQLHelper.NullableAmt(.Deutes)
            oRow("Anticips") = SQLHelper.NullableAmt(.Anticips)
            oRow("Liquid") = SQLHelper.NullableAmt(.Liquid)
            oRow("Iban") = SQLHelper.NullableString(.IbanDigits)
        End With

        oDA.Update(oDs)
    End Sub

    Protected Shared Sub UpdateConceptes(oNomina As DTONomina, oTrans As SqlTransaction)

        If oNomina.Items IsNot Nothing Then
            Dim oConceptes As List(Of DTONomina.Concepte) = NominaConceptesLoader.All(oTrans)

            Dim oDA As SqlDataAdapter = Nothing
            Dim oDs As DataSet = Nothing
            Dim oTb As DataTable = Nothing

            For Each oItem As DTONomina.Item In oNomina.Items
                If Not oConceptes.Exists(Function(x) x.Id = oItem.Concepte.Id) Then
                    If oDs Is Nothing Then
                        Dim SQL As String = "SELECT * FROM NominaConcepte"
                        oDA = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
                        oDs = New DataSet
                        oDA.Fill(oDs)
                        oTb = oDs.Tables(0)
                    End If
                    Dim oRow As DataRow = oTb.NewRow
                    oTb.Rows.Add(oRow)
                    oRow("Id") = oItem.Concepte.Id
                    oRow("Concepte") = oItem.Concepte.Name
                End If
            Next

            If oTb IsNot Nothing Then
                oDA.Update(oDs)
            End If
        End If
    End Sub

    Protected Shared Sub UpdateItems(oNomina As DTONomina, oTrans As SqlTransaction)
        If oNomina.Items IsNot Nothing Then
            'borra linies existents
            If Not oNomina.IsNew Then DeleteItems(oNomina, oTrans)

            Dim SQL As String = "SELECT * FROM NominaItem WHERE CcaGuid='" & oNomina.Cca.Guid.ToString & "'"
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)

            Dim iRow As Integer
            For Each oItem As DTONomina.Item In oNomina.Items
                If oItem.Price Is Nothing Then
                    'Stop
                Else
                    iRow += 1
                    Dim oRow As DataRow = oTb.NewRow
                    oRow("CcaGuid") = oNomina.Cca.Guid
                    oRow("Lin") = iRow
                    oRow("CodiConcepte") = oItem.Concepte.Id
                    oRow("Qty") = oItem.Qty
                    oRow("Preu") = oItem.Price.Eur
                    oTb.Rows.Add(oRow)
                End If
            Next

            oDA.Update(oDs)

        End If
    End Sub


    Shared Function Delete(oNomina As DTONomina, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oNomina, oTrans)
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


    Shared Sub Delete(oNomina As DTONomina, ByRef oTrans As SqlTransaction)
        DeleteItems(oNomina, oTrans)
        DeleteHeader(oNomina, oTrans)
        CcaLoader.Delete(oNomina.Cca, oTrans)
    End Sub

    Shared Sub DeleteItems(oNomina As DTONomina, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE NominaItem WHERE CcaGuid='" & oNomina.Cca.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oNomina As DTONomina, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Nomina WHERE CcaGuid='" & oNomina.Cca.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class
Public Class NominesLoader

    Shared Function All(Optional oExercici As DTOExercici = Nothing, Optional oStaff As DTOStaff = Nothing, Optional oUser As DTOUser = Nothing, Optional year As Integer = 0) As List(Of DTONomina)
        Dim retval As New List(Of DTONomina)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Nomina.CcaGuid, Cca.Fch, Nomina.Staff, CliGral.RaoSocial ")
        sb.AppendLine(", Nomina.Devengat, Nomina.Dietes, Nomina.SegSocial, Nomina.IrpfBase, Nomina.Irpf, Nomina.Embargos, Nomina.Deutes, Nomina.Anticips, Nomina.Liquid ")
        sb.AppendLine(", Cca.Cca, Cca.Hash ")
        sb.AppendLine(", Docfile.mime ")
        sb.AppendLine("FROM Nomina ")
        sb.AppendLine("INNER JOIN Cca ON Nomina.CcaGuid = Cca.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Docfile ON Cca.Hash = Docfile.Hash ")
        sb.AppendLine("INNER JOIN CliGral ON Nomina.Staff = CliGral.Guid ")
        If oExercici IsNot Nothing Then
            sb.AppendLine("WHERE Cca.Emp=" & oExercici.Emp.Id & " AND Year(Cca.Fch)=" & oExercici.Year & " ")
        ElseIf oStaff IsNot Nothing Then
            sb.AppendLine("WHERE Nomina.Staff = '" & oStaff.Guid.ToString & "' ")
            If year > 0 Then
                sb.AppendLine("AND Year(Cca.Fch)=" & year & " ")
            End If
        ElseIf oUser IsNot Nothing Then
            sb.AppendLine("INNER JOIN Email_Clis ON Nomina.Staff = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            If year > 0 Then
                sb.AppendLine("AND Year(Cca.Fch)=" & year & " ")
            End If
        End If
        sb.AppendLine("ORDER BY Cca.Fch DESC, CliGral.RaoSocial")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As New DTOCca(oDrd("CcaGuid"))
            Dim item As New DTONomina(oCca)
            With item
                .Cca.Id = oDrd("Cca")
                .Cca.Fch = oDrd("Fch")
                .Staff = New DTOStaff(oDrd("Staff"))
                .Staff.Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                If Not IsDBNull(oDrd("Hash")) Then
                    .Cca.DocFile = New DTODocFile(oDrd("Hash"))
                    .Cca.DocFile.Mime = SQLHelper.GetIntegerFromDataReader(oDrd("Mime"))
                End If
                .Devengat = DTOAmt.Factory(oDrd("Devengat"))
                .Dietes = DTOAmt.Factory(oDrd("Dietes"))
                .SegSocial = DTOAmt.Factory(oDrd("SegSocial"))
                .IrpfBase = DTOAmt.Factory(oDrd("IrpfBase"))
                .Irpf = DTOAmt.Factory(oDrd("Irpf"))
                .Embargos = DTOAmt.Factory(oDrd("Embargos"))
                .Deutes = DTOAmt.Factory(oDrd("Deutes"))
                .Anticips = DTOAmt.Factory(oDrd("Anticips"))
                .Liquid = DTOAmt.Factory(oDrd("Liquid"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
