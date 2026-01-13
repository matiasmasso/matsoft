Public Class RepCliComLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTORepCliCom
        Dim retval As DTORepCliCom = Nothing
        Dim oRepCliCom As New DTORepCliCom(oGuid)
        If Load(oRepCliCom) Then
            retval = oRepCliCom
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oRepCliCom As DTORepCliCom) As Boolean
        If Not oRepCliCom.IsLoaded And Not oRepCliCom.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT RepCliCom.*, CliRep.Abr, CliGral.FullNom ")
            sb.AppendLine(", Email.Adr AS UsrCreatedEmail, Email.Nickname  AS UsrCreatedNickname")
            sb.AppendLine("FROM RepCliCom ")
            sb.AppendLine("INNER JOIN CliRep ON RepCliCom.Rep = CliRep.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON RepCliCom.Cli = CliGral.Guid ")
            sb.AppendLine("INNER JOIN Email ON RepCliCom.UsrCreated = Email.Guid ")
            sb.AppendLine("WHERE RepCliCom.Guid='" & oRepCliCom.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oRepCliCom
                    .Rep = New DTORep(oDrd("Rep"))
                    .Rep.NickName = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                    .Customer = New DTOCustomer(oDrd("Cli"))
                    .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .ComCod = oDrd("ComCod")
                    .Fch = oDrd("Fch")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .FchCreated = oDrd("FchCreated")
                    .UsrCreated = New DTOUser(DirectCast(oDrd("UsrCreated"), Guid))
                    With .UsrCreated
                        .EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("UsrCreatedEmail"))
                        .NickName = SQLHelper.GetStringFromDataReader(oDrd("UsrCreatedNickname"))
                    End With
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oRepCliCom.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRepCliCom As DTORepCliCom, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRepCliCom, oTrans)
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


    Shared Sub Update(oRepCliCom As DTORepCliCom, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM RepCliCom ")
        sb.AppendLine("WHERE Guid='" & oRepCliCom.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oRepCliCom.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oRepCliCom
            oRow("Rep") = .Rep.Guid
            oRow("Cli") = .Customer.Guid
            oRow("ComCod") = .ComCod
            oRow("Fch") = .Fch
            oRow("Obs") = SQLHelper.GetStringFromDataReader(.Obs)
            oRow("UsrCreated") = SQLHelper.NullableBaseGuid(.UsrCreated)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oRepCliCom As DTORepCliCom, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRepCliCom, oTrans)
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


    Shared Sub Delete(oRepCliCom As DTORepCliCom, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE RepCliCom WHERE Guid='" & oRepCliCom.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class RepCliComsLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTORepCliCom)
        Dim retval As New List(Of DTORepCliCom)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepCliCom.Guid, RepCliCom.Rep, RepCliCom.Cli, RepCliCom.Fch, RepCliCom.ComCod ")
        sb.AppendLine(", CliGral.FullNom ")
        sb.AppendLine(", CliRep.Abr ")

        sb.AppendLine("FROM RepCliCom ")
        sb.AppendLine("INNER JOIN CliRep ON RepCliCom.Rep = CliRep.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON RepCliCom.Cli = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY CliRep.Abr, CliGral.FullNom, RepCliCom.Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTORepCliCom(oDrd("Guid"))
            With item
                .Rep = New DTORep(oDrd("Rep"))
                .Rep.NickName = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                .Customer = New DTOCustomer(oDrd("Cli"))
                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .ComCod = oDrd("ComCod")
                .Fch = oDrd("Fch")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(Optional oRep As DTORep = Nothing) As List(Of DTORepCliCom)
        Dim retval As New List(Of DTORepCliCom)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepCliCom.Guid, RepCliCom.Rep, RepCliCom.Cli, RepCliCom.Fch, RepCliCom.ComCod ")
        sb.AppendLine(", CliGral.FullNom ")

        sb.AppendLine("FROM RepCliCom ")
        sb.AppendLine("INNER JOIN CliGral ON RepCliCom.Cli = CliGral.Guid ")

        sb.AppendLine("INNER JOIN ( ")
        sb.AppendLine("		SELECT Rep, Cli, MAX(Fch) AS LastFch FROM RepCliCom GROUP BY Rep, Cli ")
        sb.AppendLine("		) X ON RepCliCom.Fch=X.LastFch AND RepCliCom.Rep=X.Rep AND RepCliCom.Cli=X.Cli ")

        sb.AppendLine("WHERE RepCliCom.Rep = '" & oRep.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY CliGral.FullNom DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTORepCliCom(oDrd("Guid"))
            With item
                .Rep = oRep
                .Customer = New DTOCustomer(oDrd("Cli"))
                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .ComCod = oDrd("ComCod")
                .Fch = oDrd("Fch")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oRepCliComs As List(Of DTORepCliCom), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepCliComsLoader.Delete(oRepCliComs, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE RepCliCom ")
        sb.AppendLine("WHERE (")
        For Each item As DTORepCliCom In oRepCliComs
            If item.UnEquals(oRepCliComs.First) Then
                sb.Append("OR ")
            End If
            sb.Append("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Return retval
    End Function

End Class

