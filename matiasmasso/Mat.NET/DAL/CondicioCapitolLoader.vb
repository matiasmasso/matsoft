Public Class CondicioCapitolLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCondicio.Capitol
        Dim retval As DTOCondicio.Capitol = Nothing
        Dim oCondicioCapitol As New DTOCondicio.Capitol(oGuid)
        If Load(oCondicioCapitol) Then
            retval = oCondicioCapitol
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCondicioCapitol As DTOCondicio.Capitol) As Boolean
        If Not oCondicioCapitol.IsLoaded And Not oCondicioCapitol.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT VwCondicions.* ")
            sb.AppendLine("FROM VwCondicions ")
            sb.AppendLine("WHERE VwCondicions.CapitolGuid='" & oCondicioCapitol.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oCondicio As New DTOCondicio(oDrd("CondGuid"))
                With oCondicio
                    SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                    SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                End With
                With oCondicioCapitol
                    .Parent = oCondicio
                    SQLHelper.LoadLangTextFromDataReader(.Caption, oDrd, "CapitolEsp", "CapitolCat", "CapitolEng", "CapitolPor")
                    SQLHelper.LoadLangTextFromDataReader(.Text, oDrd, "TxtEsp", "TxtCat", "TxtEng", "TxtPor")
                    .Ord = oDrd("Ord")
                    .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCondicioCapitol.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCondicioCapitol As DTOCondicio.Capitol, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCondicioCapitol, oTrans)
            LangTextLoader.Update(oCondicioCapitol.Caption, oTrans)
            LangTextLoader.Update(oCondicioCapitol.Text, oTrans)
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


    Shared Sub Update(oCondicioCapitol As DTOCondicio.Capitol, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CondCapitol ")
        sb.AppendLine("WHERE Guid='" & oCondicioCapitol.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCondicioCapitol.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCondicioCapitol
            oRow("Parent") = .Parent.Guid
            oRow("Ord") = .Ord
            oRow("UsrCreated") = SQLHelper.NullableBaseGuid(.UsrLog.UsrCreated)
            oRow("UsrLastEdited") = SQLHelper.NullableBaseGuid(.UsrLog.UsrLastEdited)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCondicioCapitol As DTOCondicio.Capitol, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCondicioCapitol, oTrans)
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


    Shared Sub Delete(oCondicioCapitol As DTOCondicio.Capitol, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CondCapitol WHERE Guid='" & oCondicioCapitol.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class CondicioCapitolsLoader
    Shared Function Headers(oCondicio As DTOCondicio) As DTOCondicio.Capitol.Collection
        Dim retval As New DTOCondicio.Capitol.Collection
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwCondicions.* ")
        sb.AppendLine("FROM VwCondicions ")
        sb.AppendLine("WHERE VwCondicions.CondGuid ='" & oCondicio.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY VwCondicions.Ord ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCondicio.Capitol(oDrd("CapitolGuid"))
            With item
                .Parent = oCondicio
                SQLHelper.LoadLangTextFromDataReader(.Caption, oDrd, "CapitolEsp", "CapitolCat", "CapitolEng", "CapitolPor")
                SQLHelper.LoadLangTextFromDataReader(.Text, oDrd, "TxtEsp", "TxtCat", "TxtEng", "TxtPor")
                .Ord = oDrd("Ord")
                .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function
End Class

