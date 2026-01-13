Public Class CorrespondenciaLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCorrespondencia
        Dim retval As DTOCorrespondencia = Nothing
        Dim oMail As New DTOCorrespondencia(oGuid)
        If Load(oMail) Then
            retval = oMail
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oMail As DTOCorrespondencia) As Boolean
        If Not oMail.IsLoaded And Not oMail.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Crr.*, CrrCli.CliGuid, CliGral.FullNom ")
            sb.AppendLine(", UCreated.Adr AS UCAdr, UCreated.Nickname AS UCNickname, ULastEdited.Adr AS ULAdr, ULastEdited.Nickname AS ULNickname ")
            sb.AppendLine("FROM Crr ")
            sb.AppendLine("LEFT OUTER JOIN CrrCli ON Crr.Guid=CrrCli.MailGuid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON CrrCli.CliGuid=CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email AS UCreated ON Crr.UsrCreated = UCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email AS ULastEdited ON Crr.UsrLastEdited = ULastEdited.Guid ")
            sb.AppendLine("WHERE Crr.Guid='" & oMail.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                With oMail
                    If Not .IsLoaded Then
                        If .Emp Is Nothing Then .Emp = New DTOEmp(oDrd("Emp"))
                        .Fch = oDrd("Fch")
                        .Id = oDrd("Crr")
                        .Subject = oDrd("Dsc")
                        .Atn = oDrd("Atn")
                        .Cod = oDrd("rt")
                        .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                        .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
                        .Contacts = New List(Of DTOContact)
                        .IsLoaded = True
                    End If
                End With
                If Not IsDBNull(oDrd("CliGuid")) Then
                    Dim oContact As New DTOContact(oDrd("CliGuid"))
                    With oContact
                        .Emp = oMail.Emp
                        .FullNom = oDrd("FullNom")
                    End With
                    oMail.Contacts.Add(oContact)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oMail.IsLoaded
        Return retval
    End Function

    Shared Function Update(oMail As DTOCorrespondencia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oMail, oTrans)
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

    Shared Sub Update(oMail As DTOCorrespondencia, ByRef oTrans As SqlTransaction)
        DocFileLoader.Update(oMail.DocFile, oTrans)
        UpdateHeader(oMail, oTrans)
        UpdateContacts(oMail, oTrans)
    End Sub

    Shared Sub UpdateContacts(oMail As DTOCorrespondencia, ByRef oTrans As SqlTransaction)
        If Not oMail.IsNew Then DeleteContacts(oMail, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CrrCli ")
        sb.AppendLine("WHERE CliGuid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oMail.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOContact In oMail.Contacts
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("MailGuid") = oMail.Guid
            oRow("CliGuid") = item.Guid
        Next
        oDA.Update(oDs)

    End Sub

    Shared Sub UpdateHeader(oMail As DTOCorrespondencia, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Crr ")
        sb.AppendLine("WHERE Guid='" & oMail.Guid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oMail.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oMail
            If .Id = 0 Then .Id = NextId(oMail, oTrans)

            oRow("Emp") = oMail.Emp.Id
            oRow("Yea") = .Fch.Year
            oRow("Crr") = .Id
            oRow("Fch") = .Fch
            oRow("rt") = .Cod
            oRow("Atn") = .Atn
            oRow("Dsc") = .Subject
            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)

            SQLHelper.SetUsrLog(.UsrLog, oRow)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function NextId(oMail As DTOCorrespondencia, oTrans As SqlTransaction) As Integer
        Dim SQL As String = "SELECT MAX(Crr) AS LastId FROM Crr WHERE Emp=" & oMail.Emp.Id & " AND Yea=" & oMail.Fch.Year

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = oTb.Rows(0)

        Dim LastId As Integer = Defaults.IntOrZeroIfNull(oRow("LastId"))
        Dim retval As Integer = LastId + 1
        Return retval
    End Function

    Shared Function Delete(oMail As DTOCorrespondencia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oMail, oTrans)
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

    Shared Sub Delete(oMail As DTOCorrespondencia, ByRef oTrans As SqlTransaction)
        DocFileLoader.DeleteIfOrphan(oMail.DocFile, oTrans)
        DeleteContacts(oMail, oTrans)
        DeleteHeader(oMail, oTrans)
    End Sub

    Shared Sub DeleteContacts(oMail As DTOCorrespondencia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CrrCli WHERE MailGuid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oMail.Guid.ToString())
    End Sub

    Shared Sub DeleteHeader(oMail As DTOCorrespondencia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Crr WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oMail.Guid.ToString())
    End Sub

#End Region

End Class

Public Class CorrespondenciasLoader

    Shared Function All(oEmp As DTOEmp, Optional iYear As Integer = 0, Optional oContact As DTOContact = Nothing) As List(Of DTOCorrespondencia)
        Dim retval As New List(Of DTOCorrespondencia)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Crr.* ")
        sb.AppendLine("FROM Crr ")
        If oContact IsNot Nothing Then
            sb.AppendLine("INNER JOIN CrrCli ON Crr.Guid=MailGuid AND CrrCli.CliGuid = '" & oContact.Guid.ToString & "' ")
        End If
        sb.AppendLine("WHERE Crr.Emp = " & oEmp.Id & " ")
        If iYear > 0 Then
            sb.AppendLine("AND Crr.Yea = " & iYear & " ")
        End If
        sb.AppendLine("ORDER BY Crr.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCorrespondencia(oDrd("Guid"))
            With item
                .Emp = oEmp
                .Fch = oDrd("Fch")
                .Id = oDrd("Crr")
                .Subject = oDrd("Dsc")
                .Atn = oDrd("Atn")
                .Cod = oDrd("rt")
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile
                    With .DocFile
                        .Hash = oDrd("Hash")
                    End With
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oContact As DTOContact) As List(Of DTOCorrespondencia)
        Dim retval As New List(Of DTOCorrespondencia)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Crr.Guid, Crr.Crr, Crr.Fch, Crr.Dsc, Crr.Atn, Crr.Rt, Crr.Hash, Crr.UsrCreated, Crr.UsrLastEdited, Crr.FchCreated, Crr.FchLastEdited ")
        sb.AppendLine(", UCreated.Adr AS UCAdr, UCreated.Nickname AS UCNickname, ULastEdited.Adr AS ULAdr, ULastEdited.Nickname AS ULNickname ")
        sb.AppendLine(", BF.Size, BF.Mime, BF.Pags, BF.Width, BF.Height ")
        sb.AppendLine("FROM Crr ")
        sb.AppendLine("LEFT OUTER JOIN CrrCli ON CrrCli.MailGuid = Crr.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email AS UCreated ON Crr.UsrCreated = UCreated.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email AS ULastEdited ON Crr.UsrLastEdited = ULastEdited.Guid ")
        sb.AppendLine("LEFT OUTER JOIN DocFile BF ON Crr.Hash = BF.Hash ")
        sb.AppendLine("WHERE CrrCli.CliGuid='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Crr.Fch DESC, Crr.Crr DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCorrespondencia As New DTOCorrespondencia(oDrd("Guid"))
            With oCorrespondencia
                .Emp = oContact.Emp
                .Id = CInt(oDrd("Crr"))
                .Fch = oDrd("FCH")
                .Subject = oDrd("DSC")
                .Atn = oDrd("ATN")
                .Cod = oDrd("RT")
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash").ToString())
                    With .DocFile
                        .Mime = SQLHelper.GetIntegerFromDataReader(oDrd("Mime"))
                        .Pags = SQLHelper.GetIntegerFromDataReader(oDrd("Pags"))
                        .Size = New System.Drawing.Size(SQLHelper.GetIntegerFromDataReader(oDrd("Width")), SQLHelper.GetIntegerFromDataReader(oDrd("Height")))
                        .Length = SQLHelper.GetIntegerFromDataReader(oDrd("Size"))
                    End With
                End If
                .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd, UsrCreatedEmailAddressField:="UCAdr", UsrCreatedNickNameField:="UCNickname", UsrLastEditedEmailAddressField:="ULAdr", UsrLastEditedNickNameField:="ULNickname")

            End With
            retval.Add(oCorrespondencia)
        Loop

        oDrd.Close()
        Return retval
    End Function

End Class

