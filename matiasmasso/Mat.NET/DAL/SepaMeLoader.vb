Public Class SepaMeLoader

    Shared Function Find(oGuid As Guid) As DTOSepaMe
        Dim retval As DTOSepaMe = Nothing
        Dim oSepaMe As New DTOSepaMe(oGuid)
        If Load(oSepaMe) Then
            retval = oSepaMe
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSepaMe As DTOSepaMe) As Boolean
        If Not oSepaMe.IsLoaded And Not oSepaMe.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT SepaMe.* ")
            sb.AppendLine(", Banc.FullNom AS BancNom")
            sb.AppendLine(", Lliurador.FullNom AS LliuradorNom")
            sb.AppendLine(", VwDocfile.* ")
            sb.AppendLine(", UsrCreated.Nom AS UsrCreatedNickname, UsrLastEdited.Nom AS UsrLastEditedNickname ")
            sb.AppendLine("FROM SepaMe ")
            sb.AppendLine("LEFT OUTER JOIN CliGral Banc ON SepaMe.Banc = Banc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral Lliurador ON SepaMe.Lliurador = Lliurador.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwDocfile ON SepaMe.Hash = VwDocfile.DocfileHash ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname AS UsrCreated ON SepaMe.UsrCreated = UsrCreated.guid ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname AS UsrLastEdited ON SepaMe.UsrLastEdited = UsrLastEdited.guid ")
            sb.AppendLine("WHERE SepaMe.Guid='" & oSepaMe.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oSepaMe
                    .Banc = New DTOBanc(oDrd("Banc"))
                    .Banc.FullNom = SQLHelper.GetStringFromDataReader(oDrd("BancNom"))
                    .Lliurador = New DTOContact(oDrd("Lliurador"))
                    .Lliurador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("LliuradorNom"))
                    .FchFrom = oDrd("FchFrom")
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                    .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                    .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oSepaMe.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSepaMe As DTOSepaMe, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSepaMe, oTrans)
            oTrans.Commit()
            oSepaMe.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oSepaMe As DTOSepaMe, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SepaMe ")
        sb.AppendLine("WHERE Guid='" & oSepaMe.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSepaMe.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSepaMe
            oRow("Banc") = .Banc.Guid
            oRow("LLiurador") = SQLHelper.NullableBaseGuid(.Lliurador)
            oRow("FchFrom") = SQLHelper.NullableFch(.FchFrom)
            oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
            oRow("Ref") = SQLHelper.NullableString(.Ref)
            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)
            SQLHelper.SetUsrLog(.UsrLog, oRow)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSepaMe As DTOSepaMe, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSepaMe, oTrans)
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


    Shared Sub Delete(oSepaMe As DTOSepaMe, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SepaMe WHERE Guid='" & oSepaMe.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class SepaMesLoader

    Shared Function All() As List(Of DTOSepaMe)
        Dim retval As New List(Of DTOSepaMe)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SepaMe.* ")
        sb.AppendLine(", Banc.FullNom AS BancNom")
        sb.AppendLine(", CliBnc.Abr")
        sb.AppendLine(", Lliurador.FullNom AS LliuradorNom")
        sb.AppendLine("FROM SepaMe ")
        sb.AppendLine("LEFT OUTER JOIN CliBnc ON SepaMe.Banc = CliBnc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral Banc ON SepaMe.Banc = Banc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral Lliurador ON SepaMe.Lliurador = Lliurador.Guid ")
        sb.AppendLine("ORDER BY FchFrom DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSepaMe(oDrd("Guid"))
            With item
                .Banc = New DTOBanc(oDrd("Banc"))
                .Banc.Abr = oDrd("Abr")
                .Banc.FullNom = SQLHelper.GetStringFromDataReader(oDrd("BancNom"))
                .Lliurador = New DTOContact(oDrd("Lliurador"))
                .Lliurador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("LliuradorNom"))
                .FchFrom = oDrd("FchFrom")
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

