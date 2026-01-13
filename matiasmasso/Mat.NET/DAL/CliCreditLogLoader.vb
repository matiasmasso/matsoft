Public Class CliCreditLogLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCliCreditLog
        Dim retval As DTOCliCreditLog = Nothing
        Dim oCliCreditLog As New DTOCliCreditLog(oGuid)
        If Load(oCliCreditLog) Then
            retval = oCliCreditLog
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCliCreditLog As DTOCliCreditLog) As Boolean
        If Not oCliCreditLog.IsLoaded And Not oCliCreditLog.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliCreditLog.*, CliGral.FullNom ")
            sb.AppendLine(", UserCreated.Adr AS UserCreatedAdr, UserCreated.Nickname AS UserCreatedNickname ")
            sb.AppendLine(", UserLastEdited.Adr AS UserLastEditedAdr, UserLastEdited.Nickname AS UserLastEditedNickname ")
            sb.AppendLine("FROM CliCreditLog ")
            sb.AppendLine("INNER JOIN CliGral ON CliCreditLog.CliGuid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email UserCreated ON CliCreditLog.UserCreated = UserCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email UserLastEdited ON CliCreditLog.UserLastEdited = UserLastEdited.Guid ")
            sb.AppendLine("WHERE CliCreditLog.Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oCliCreditLog.Guid.ToString())
            If oDrd.Read Then
                With oCliCreditLog
                    .Customer = New DTOCustomer(DirectCast(oDrd("CliGuid"), Guid))
                    .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("OBS"))
                    .Cod = DirectCast(oDrd("COD"), DTOCliCreditLog.Cods)

                    .FchCreated = CDate(oDrd("FchCreated"))
                    If Not IsDBNull(oDrd("UserCreated")) Then
                        .UsrCreated = New DTOUser(DirectCast(oDrd("UserCreated"), Guid))
                        With .UsrCreated
                            .Adr = SQLHelper.GetStringFromDataReader(oDrd("UserCreatedAdr"))
                            .NickName = SQLHelper.GetStringFromDataReader(oDrd("UserCreatedNickName"))
                        End With
                    End If

                    .FchLastEdited = CDate(oDrd("FchLastEdited"))
                    If Not IsDBNull(oDrd("UserLastEdited")) Then
                        .UsrLastEdited = New DTOUser(DirectCast(oDrd("UserLastEdited"), Guid))
                        With .UsrLastEdited
                            .Adr = SQLHelper.GetStringFromDataReader(oDrd("UserCreatedAdr"))
                            .NickName = SQLHelper.GetStringFromDataReader(oDrd("UserLastEditedNickName"))
                        End With
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCliCreditLog.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCliCreditLog As DTOCliCreditLog, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCliCreditLog, oTrans)
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


    Shared Sub Update(oCliCreditLog As DTOCliCreditLog, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliCreditLog ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oCliCreditLog.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCliCreditLog.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCliCreditLog
            oRow("CliGuid") = .Customer.Guid
            oRow("FchCreated") = .FchCreated
            oRow("UserCreated") = .UsrCreated.Guid
            oRow("FchLastEdited") = .FchLastEdited
            oRow("UserLastEdited") = .UsrLastEdited.Guid
            oRow("Eur") = .Amt.Eur
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("Cod") = .Cod
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCliCreditLog As DTOCliCreditLog, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oCliCreditLog, oTrans)
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

    Shared Sub Delete(oCliCreditLog As DTOCliCreditLog, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliCreditLog WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oCliCreditLog.Guid.ToString())
    End Sub

#End Region

End Class

Public Class CliCreditLogsLoader

    Shared Function All(oCcx As DTOCustomer) As List(Of DTOCliCreditLog)
        Dim retval As New List(Of DTOCliCreditLog)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliCreditLog.* ")
        sb.AppendLine(", UserCreated.Adr AS UserCreatedAdr, UserCreated.Nickname AS UserCreatedNickname ")
        sb.AppendLine(", UserLastEdited.Adr AS UserLastEditedAdr, UserLastEdited.Nickname AS UserLastEditedNickname ")
        sb.AppendLine("FROM CliCreditLog ")
        sb.AppendLine("LEFT OUTER JOIN Email UserCreated ON CliCreditLog.UserCreated = UserCreated.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email UserLastEdited ON CliCreditLog.UserLastEdited = UserLastEdited.Guid ")
        sb.AppendLine("WHERE CliCreditLog.CliGuid = '" & oCcx.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY FchCreated DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCliCreditLog(oDrd("Guid"))
            With item
                .Customer = oCcx
                .FchCreated = CDate(oDrd("FchCreated"))
                If Not IsDBNull(oDrd("UserCreated")) Then
                    .UsrCreated = New DTOUser(DirectCast(oDrd("UserCreated"), Guid))
                End If
                .FchLastEdited = CDate(oDrd("FchLastEdited"))
                If Not IsDBNull(oDrd("UserLastEdited")) Then
                    .UsrLastEdited = New DTOUser(DirectCast(oDrd("UserLastEdited"), Guid))
                End If
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("OBS"))
                .Cod = DirectCast(oDrd("COD"), DTOCliCreditLog.Cods)
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function PendentsDeCaducar(oUser As DTOUser) As List(Of DTOCliCreditLog)
        Dim retval As New List(Of DTOCliCreditLog)
        Dim MesosCaducitatCredit As Integer = 6
        Dim MesosCaducitatAutoritzacioRenovacio As Integer = 1
        Dim sFchMinAlb As String = Format(DTO.GlobalVariables.Today().AddMonths(-MesosCaducitatCredit), "yyyyMMdd")
        Dim sFchMinCredit As String = Format(DTO.GlobalVariables.Today().AddMonths(-MesosCaducitatAutoritzacioRenovacio), "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT C.CliGuid, LASTALBFCH, CliGral.FullNom ")
        sb.AppendLine("FROM CliCreditLog AS C ")
        sb.AppendLine("INNER JOIN CliGral ON C.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN (SELECT        CliCreditLog.CliGuid, MAX(CliCreditLog.FchCreated) AS LASTCREDFCH, MAX(ALB.fch) AS LASTALBFCH ")
        sb.AppendLine("FROM            CliCreditLog ")
        sb.AppendLine("INNER JOIN CliClient ON (CliCreditLog.CliGuid = CliClient.CcxGuid OR CliCreditLog.CliGuid = CliClient.Guid) ")
        sb.AppendLine("INNER JOIN ALB ON ALB.CliGuid = CliClient.Guid AND ALB.Cod = 2 AND ALB.Eur > 0 ")
        sb.AppendLine("GROUP BY CliCreditLog.CliGuid) AS X ON C.CliGuid = X.CliGuid AND C.FchCreated = X.LASTCREDFCH ")
        sb.AppendLine("WHERE CliGral.Emp =" & oUser.Emp.Id & "  ")
        sb.AppendLine("AND C.Eur > 0 ")
        sb.AppendLine("AND X.LASTALBFCH < '" & sFchMinAlb & "' ")
        sb.AppendLine("AND X.LASTCREDFCH < '" & sFchMinCredit & "' ")
        sb.AppendLine("ORDER BY X.LASTALBFCH")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim sFch As String = CDate(oDrd("LASTALBFCH")).ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
            Dim item As New DTOCliCreditLog
            With item
                .Customer = New DTOCustomer(DirectCast(oDrd("CliGuid"), Guid))
                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .Amt = DTOAmt.Empty
                .Obs = "Caducat per inactiu desde " & sFch
                .Cod = DTOCliCreditLog.Cods.Caducat
                .UsrCreated = oUser
                .UsrLastEdited = oUser
                .FchCreated = DTO.GlobalVariables.Now()
                .FchLastEdited = .FchCreated
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function CreditLastAlbs(oEmp As DTOEmp) As List(Of DTOCreditLastAlb)
        Dim retval As New List(Of DTOCreditLastAlb)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Z.CliGuid, Z.Eur, CliGral.FullNom, LastAlb.LastAlbFch, LastAlb.Eur AS Consum ")
        sb.AppendLine(",Iban.Guid AS IbanGuid, IBAN.Mandato_Fch ")
        sb.AppendLine("FROM  (SELECT Y.CliGuid, Y.Eur FROM (SELECT CliGuid, MAX(FchCreated) AS LastFch FROM CliCreditLog GROUP BY CliGuid) X ")
        sb.AppendLine("     INNER JOIN CliCreditLog Y ON X.CliGuid=Y.CliGuid AND X.LastFch=Y.FchCreated) Z ")
        sb.AppendLine("INNER JOIN CliGral ON Z.CliGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Iban ON Z.CliGuid = Iban.ContactGuid AND (IBAN.Caduca_Fch IS NULL OR IBAN.Caduca_Fch > GETDATE()) ")
        sb.AppendLine("LEFT OUTER JOIN  (SELECT VwCcxOrMe.Ccx, MAX(ALB.fch) AS LastAlbFch, SUM(CASE WHEN Alb.Fch> GETDATE() - 185 THEN Alb.Eur ELSE 0 END) AS Eur ")
        sb.AppendLine("     FROM Alb INNER JOIN VwCcxOrMe ON Alb.CliGuid = VwCcxOrMe.Guid GROUP BY VwCcxOrMe.Ccx) LastAlb ON Z.CliGuid=LastAlb.Ccx ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " AND Z.Eur>0 ")
        sb.AppendLine("GROUP BY Z.CliGuid, Z.Eur, CliGral.FullNom, LastAlb.LastAlbFch, LastAlb.Eur, Iban.Guid, IBAN.Mandato_Fch ")
        sb.AppendLine("ORDER BY LastAlb.LastAlbFch")


        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))
            oCustomer.Emp = oEmp
            oCustomer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))

            Dim oDelivery As New DTODelivery()
            oDelivery.Fch = SQLHelper.GetFchFromDataReader(oDrd("LastAlbFch"))
            Dim oIban As DTOIban = Nothing
            If Not IsDBNull(oDrd("IbanGuid")) Then
                oIban = New DTOIban(oDrd("IbanGuid"))
                oIban.FchFrom = SQLHelper.GetFchFromDataReader(oDrd("Mandato_Fch"))
            End If

            Dim item As New DTOCreditLastAlb
            With item
                .Customer = oCustomer
                .Credit = SQLHelper.GetAmtFromDataReader2(oDrd, "Eur")
                .LastDelivery = oDelivery
                .Consum = SQLHelper.GetAmtFromDataReader2(oDrd, "Consum")
                .Iban = oIban
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval

        Return retval
    End Function
End Class
