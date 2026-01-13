Public Class ContractLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOContract
        Dim retval As DTOContract = Nothing
        Dim oContract As New DTOContract(oGuid)
        If Load(oContract) Then
            retval = oContract
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oContract As DTOContract) As Boolean
        If Not oContract.IsLoaded And Not oContract.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliGral.Emp, Contract.* ")
            sb.AppendLine(", CliGral.FullNom, Contract_Codis.Nom AS CodiNom, Contract_Codis.Amortitzable ")
            sb.AppendLine("FROM Contract ")
            sb.AppendLine("INNER JOIN Contract_Codis ON Contract.CodiGuid=Contract_Codis.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON Contract.ContactGuid=CliGral.Guid ")
            sb.AppendLine("WHERE Contract.Guid='" & oContract.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oContract
                    .Codi = New DTOContractCodi(oDrd("CodiGuid"))
                    .Codi.Nom = SQLHelper.GetStringFromDataReader(oDrd("CodiNom"))
                    .Codi.Amortitzable = SQLHelper.GetBooleanFromDatareader(oDrd("Amortitzable"))
                    .Nom = oDrd("Nom").ToString
                    .Num = oDrd("Num").ToString
                    .Contact = New DTOContact(oDrd("ContactGuid"))
                    .Contact.Emp = New DTOEmp(oDrd("Emp"))
                    .Contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Privat = CBool(oDrd("Privat"))
                    .fchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                    .fchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oContract.IsLoaded
        Return retval
    End Function

    Shared Function Update(oContract As DTOContract, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oContract, oTrans)
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


    Shared Sub Update(oContract As DTOContract, ByRef oTrans As SqlTransaction)
        If oContract.DocFile IsNot Nothing Then
            DocFileLoader.Update(oContract.DocFile, oTrans)
        End If

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Contract ")
        sb.AppendLine("WHERE Contract.Guid='" & oContract.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oContract.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oContract
            oRow("CodiGuid") = .Codi.Guid
            oRow("Nom") = .Nom
            oRow("Num") = .Num
            oRow("ContactGuid") = .Contact.Guid
            oRow("Privat") = .Privat
            oRow("FchFrom") = SQLHelper.NullableFch(.fchFrom)
            oRow("FchTo") = SQLHelper.NullableFch(.fchTo)
            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oContract As DTOContract, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oContract, oTrans)
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


    Shared Sub Delete(oContract As DTOContract, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Contract WHERE Guid='" & oContract.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class ContractsLoader

    Shared Function All(Optional oUser As DTOUser = Nothing, Optional oCodi As DTOContractCodi = Nothing, Optional oContact As DTOContact = Nothing) As List(Of DTOContract)
        Dim retval As New List(Of DTOContract)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Contract.* ")
        sb.AppendLine(", CliGral.FullNom, Contract_Codis.Nom AS CodiNom, Contract_Codis.Amortitzable ")
        sb.AppendLine("FROM Contract ")
        sb.AppendLine("INNER JOIN CliGral ON Contract.ContactGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Contract_Codis ON Contract.CodiGuid=Contract_Codis.Guid ")
        If oUser IsNot Nothing Then
            sb.AppendLine("AND CliGral.Emp = " & oUser.Emp.Id & " ")
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts, DTORol.Ids.auditor
                Case Else
                    sb.AppendLine("INNER JOIN Email_Clis ON Contract.ContactGuid = Email_Clis.ContactGuid AND Email_Clis.EmailGuid ='" & oUser.Guid.ToString & "' ")
            End Select
        End If
        sb.AppendLine("WHERE 1 = 1 ")
        If oCodi IsNot Nothing Then
            sb.AppendLine("AND Contract.CodiGuid = '" & oCodi.Guid.ToString & "' ")
        End If
        If oContact IsNot Nothing Then
            sb.AppendLine("AND Contract.ContactGuid = '" & oContact.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY (CASE WHEN Contract.FchTo IS NULL THEN 0 ELSE 1 END), Contract_Codis.Nom, Contract.FchFrom DESC, Contract.Nom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOContract(oDrd("Guid"))
            With item
                .Codi = New DTOContractCodi(oDrd("CodiGuid"))
                .Codi.Nom = SQLHelper.GetStringFromDataReader(oDrd("CodiNom"))
                .Codi.Amortitzable = SQLHelper.GetBooleanFromDatareader(oDrd("Amortitzable"))
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Num = SQLHelper.GetStringFromDataReader(oDrd("Num"))
                .Contact = New DTOContact(oDrd("ContactGuid"))
                .Contact.Emp = oUser.Emp
                .Contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .Privat = CBool(oDrd("Privat"))
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oContracts As List(Of DTOContract), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContractsLoader.Delete(oContracts, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE Contract ")
        sb.AppendLine("WHERE (")
        For Each item As DTOContract In oContracts
            If item.UnEquals(oContracts.First) Then
                sb.Append("OR ")
            End If
            sb.Append("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Return retval
    End Function

End Class
