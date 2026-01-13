Public Class CondicioLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCondicio
        Dim retval As DTOCondicio = Nothing
        Dim oCondicio As New DTOCondicio(oGuid)
        If Load(oCondicio) Then
            retval = oCondicio
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCondicio As DTOCondicio) As Boolean
        If Not oCondicio.IsLoaded And Not oCondicio.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * FROM VwCondicions ")
            sb.AppendLine("WHERE VwCondicions.CondGuid='" & oCondicio.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY VwCondicions.Ord ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oCondicio.IsLoaded Then
                    With oCondicio
                        SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                        SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                        .Capitols = New DTOCondicio.Capitol.Collection
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("CapitolGuid")) Then
                    Dim item As New DTOCondicio.Capitol(oDrd("CapitolGuid"))
                    With item
                        .Parent = oCondicio
                        SQLHelper.LoadLangTextFromDataReader(.Caption, oDrd, "CapitolEsp", "CapitolCat", "CapitolEng", "CapitolPor")
                        SQLHelper.LoadLangTextFromDataReader(.Text, oDrd, "TxtEsp", "TxtCat", "TxtEng", "TxtPor")
                        .Ord = oDrd("Ord")
                        .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
                    End With
                    oCondicio.Capitols.Add(item)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oCondicio.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCondicio As DTOCondicio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCondicio, oTrans)
            LangTextLoader.Update(oCondicio.Title, oTrans)
            LangTextLoader.Update(oCondicio.Excerpt, oTrans)

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

    Shared Sub Update(oCondicio As DTOCondicio, ByRef oTrans As SqlTransaction)
        UpdateHeader(oCondicio, oTrans)
        UpdateItems(oCondicio, oTrans)
    End Sub


    Shared Sub UpdateHeader(oCondicio As DTOCondicio, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cond.* ")
        sb.AppendLine("FROM Cond ")
        sb.AppendLine("WHERE Guid='" & oCondicio.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCondicio.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oCondicio As DTOCondicio, ByRef oTrans As SqlTransaction)
        DeleteItems(oCondicio, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CondCapitol ")
        sb.AppendLine("WHERE Parent='" & oCondicio.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim idx As Integer
        For Each item As DTOCondicio.Capitol In oCondicio.Capitols
            LangTextLoader.Update(item.Caption, oTrans)
            LangTextLoader.Update(item.Text, oTrans)

            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            idx += 1
            With item
                oRow("Guid") = .Guid
                oRow("Parent") = oCondicio.Guid
                oRow("Ord") = idx
            End With
        Next

        oDA.Update(oDs)

    End Sub

    Shared Function Delete(oCondicio As DTOCondicio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCondicio, oTrans)
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

    Shared Sub Delete(oCondicio As DTOCondicio, ByRef oTrans As SqlTransaction)
        DeleteItems(oCondicio, oTrans)
        DeleteHeader(oCondicio, oTrans)
    End Sub

    Shared Sub DeleteHeader(oCondicio As DTOCondicio, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Cond WHERE Guid='" & oCondicio.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oCondicio As DTOCondicio, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CondCapitol WHERE Parent='" & oCondicio.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class CondicionsLoader

    Shared Function Headers() As List(Of DTOCondicio)
        Dim retval As New List(Of DTOCondicio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CondGuid, MAX(FchLastEdited) AS FchLastEdited ")
        sb.AppendLine(", CAST(TitleEsp AS VARCHAR(MAX)) AS TitleEsp, CAST(TitleCat AS VARCHAR(MAX)) AS TitleCat, CAST(TitleEng AS VARCHAR(MAX)) AS TitleEng, CAST(TitlePor AS VARCHAR(MAX)) AS TitlePor ")
        sb.AppendLine("FROM VwCondicions ")
        sb.AppendLine("GROUP BY CondGuid, CAST(TitleEsp AS VARCHAR(MAX)), CAST(TitleCat AS VARCHAR(MAX)), CAST(TitleEng AS VARCHAR(MAX)), CAST(TitlePor AS VARCHAR(MAX)) ")
        sb.AppendLine("ORDER BY CAST(TitleEsp AS VARCHAR) ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCondicio(oDrd("CondGuid"))
            With item
                SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function



End Class

