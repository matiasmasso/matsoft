Public Class CliReturnLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCliReturn
        Dim retval As DTOCliReturn = Nothing
        Dim oCliReturn As New DTOCliReturn(oGuid)
        If Load(oCliReturn) Then
            retval = oCliReturn
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCliReturn As DTOCliReturn) As Boolean
        If Not oCliReturn.IsLoaded And Not oCliReturn.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliReturn.Fch, CliReturn.RefMgz, CliReturn.Bultos, CliReturn.Auth, CliReturn.Obs ")
            sb.AppendLine(", CliReturn.Cli, CliGral.FullNom ")
            sb.AppendLine(", CliReturn.Mgz, Mgz.Nom AS MgzAbr ")
            sb.AppendLine(", CliReturn.Entrada, Alb.Alb, Alb.Fch AS AlbFch ")
            sb.AppendLine(", Alb.FraGuid, Fra.Fra, Fra.Fch AS FraFch ")
            sb.AppendLine(", CliReturn.FchCreated, CliReturn.UsrCreated, UsrCreated.Nickname AS UsrCreatedNickname ")
            sb.AppendLine(", CliReturn.FchLastEdited, CliReturn.UsrLastEdited, UsrLastEdited.Nickname AS UsrLastEditedNickname ")
            sb.AppendLine("FROM CliReturn ")
            sb.AppendLine("INNER JOIN CliGral ON CliReturn.Cli = CliGral.Guid ")
            sb.AppendLine("INNER JOIN Mgz ON CliReturn.Mgz = Mgz.Guid ")
            sb.AppendLine("INNER JOIN Email UsrCreated ON CliReturn.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("INNER JOIN Email UsrLastEdited ON CliReturn.UsrLastEdited = UsrLastEdited.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Alb ON CliReturn.Entrada = Alb.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")

            sb.AppendLine("WHERE CliReturn.Guid='" & oCliReturn.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oCliReturn
                    .Customer = New DTOCustomer(oDrd("Cli"))
                    .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Mgz = New DTOMgz(oDrd("Mgz"))
                    .Mgz.FullNom = SQLHelper.GetStringFromDataReader(oDrd("MgzAbr"))
                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                    .RefMgz = SQLHelper.GetStringFromDataReader(oDrd("RefMgz"))
                    .Bultos = oDrd("Bultos")
                    If Not IsDBNull(oDrd("Entrada")) Then
                        .Entrada = New DTODelivery(oDrd("Entrada"))
                        .Entrada.Id = SQLHelper.GetIntegerFromDataReader(oDrd("Alb"))
                        .Entrada.Fch = SQLHelper.GetFchFromDataReader(oDrd("AlbFch"))
                        If Not IsDBNull(oDrd("FraGuid")) Then
                            .Entrada.Invoice = New DTOInvoice(oDrd("FraGuid"))
                            .Entrada.Invoice.Num = SQLHelper.GetIntegerFromDataReader(oDrd("Fra"))
                            .Entrada.Invoice.Fch = SQLHelper.GetFchFromDataReader(oDrd("FraFch"))
                        End If
                    End If
                    .Auth = SQLHelper.GetStringFromDataReader(oDrd("Auth"))
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCliReturn.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCliReturn As DTOCliReturn, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCliReturn, oTrans)
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


    Shared Sub Update(oCliReturn As DTOCliReturn, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliReturn ")
        sb.AppendLine("WHERE Guid='" & oCliReturn.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCliReturn.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCliReturn
            oRow("Cli") = .Customer.Guid
            oRow("Mgz") = .Mgz.Guid
            oRow("Fch") = .Fch
            oRow("RefMgz") = SQLHelper.NullableString(.RefMgz)
            oRow("Bultos") = .Bultos
            oRow("Entrada") = SQLHelper.NullableBaseGuid(.Entrada)
            oRow("Auth") = .Auth
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("UsrCreated") = SQLHelper.NullableBaseGuid(.UsrLog.UsrCreated)
            oRow("FchCreated") = SQLHelper.NullableFch(.UsrLog.FchCreated)
            oRow("UsrLastEdited") = SQLHelper.NullableBaseGuid(.UsrLog.UsrLastEdited)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCliReturn As DTOCliReturn, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCliReturn, oTrans)
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


    Shared Sub Delete(oCliReturn As DTOCliReturn, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliReturn WHERE Guid='" & oCliReturn.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class CliReturnsLoader

    Shared Function All() As List(Of DTOCliReturn)
        Dim retval As New List(Of DTOCliReturn)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliReturn.Guid, CliReturn.Fch, CliReturn.RefMgz, CliReturn.Bultos, CliReturn.Auth, CliReturn.Obs ")
        sb.AppendLine(", CliReturn.Cli, CliGral.FullNom ")
        sb.AppendLine(", CliReturn.Mgz, Mgz.Nom AS MgzAbr ")
        sb.AppendLine(", CliReturn.Entrada, Alb.Alb, Alb.Fch AS AlbFch ")
        sb.AppendLine(", Alb.FraGuid, Fra.Fra, Fra.Fch AS FraFch ")
        sb.AppendLine(", CliReturn.FchCreated, CliReturn.UsrCreated, UsrCreated.Nickname AS UsrCreatedNickname ")
        sb.AppendLine(", CliReturn.FchLastEdited, CliReturn.UsrLastEdited, UsrLastEdited.Nickname AS UsrLastEditedNickname ")
        sb.AppendLine("FROM CliReturn ")
        sb.AppendLine("INNER JOIN CliGral ON CliReturn.Cli = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Mgz ON CliReturn.Mgz = Mgz.Guid ")
        sb.AppendLine("INNER JOIN Email UsrCreated ON CliReturn.UsrCreated = UsrCreated.Guid ")
        sb.AppendLine("INNER JOIN Email UsrLastEdited ON CliReturn.UsrLastEdited = UsrLastEdited.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Alb ON CliReturn.Entrada = Alb.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("ORDER BY CliReturn.FchCreated DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCliReturn(oDrd("Guid"))
            With item
                .Customer = New DTOCustomer(oDrd("Cli"))
                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .Mgz = New DTOMgz(oDrd("Mgz"))
                .Mgz.FullNom = SQLHelper.GetStringFromDataReader(oDrd("MgzAbr"))
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .RefMgz = SQLHelper.GetStringFromDataReader(oDrd("RefMgz"))
                .Bultos = oDrd("Bultos")
                If Not IsDBNull(oDrd("Entrada")) Then
                    .Entrada = New DTODelivery(oDrd("Entrada"))
                    .Entrada.Id = SQLHelper.GetIntegerFromDataReader(oDrd("Alb"))
                    .Entrada.Fch = SQLHelper.GetFchFromDataReader(oDrd("AlbFch"))
                    If Not IsDBNull(oDrd("FraGuid")) Then
                        .Entrada.Invoice = New DTOInvoice(oDrd("FraGuid"))
                        .Entrada.Invoice.Num = SQLHelper.GetIntegerFromDataReader(oDrd("Fra"))
                        .Entrada.Invoice.Fch = SQLHelper.GetFchFromDataReader(oDrd("FraFch"))
                    End If
                End If
                .Auth = oDrd("Auth")
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

