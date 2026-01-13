Public Class FeedbackSourceLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOFeedback.SourceClass
        Dim retval As DTOFeedback.SourceClass = Nothing
        Dim oFeedbackSource As New DTOFeedback.SourceClass(oGuid)
        If Load(oFeedbackSource) Then
            retval = oFeedbackSource
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oFeedbackSource As DTOFeedback.SourceClass) As Boolean
        If Not oFeedbackSource.isLoaded And Not oFeedbackSource.isNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT FeedbackSource.Emp, FeedbackSource.Cod, FeedbackSource.Nom ")
            sb.AppendLine(", SUM(CASE WHEN Feedback.Score = 1 THEN 1 ELSE 0 END) AS ScorePositive ")
            sb.AppendLine(", SUM(CASE WHEN Feedback.Score = 2 THEN 1 ELSE 0 END) AS ScoreNeutral ")
            sb.AppendLine(", SUM(CASE WHEN Feedback.Score = 3 THEN 1 ELSE 0 END) AS ScoreNegative ")
            sb.AppendLine("FROM FeedbackSource ")
            sb.AppendLine("LEFT OUTER JOIN Feedback ON FeedbackSource.Guid = Feedback.Source ")
            sb.AppendLine("WHERE FeedbackSource.Guid='" & oFeedbackSource.Guid.ToString & "' ")
            sb.AppendLine("GROUP BY FeedbackSource.Emp, FeedbackSource.Cod, FeedbackSource.Nom ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oFeedbackSource
                    .Emp = New DTOEmp(oDrd("Emp"))
                    .Cod = oDrd("Cod")
                    .Nom = oDrd("Nom")
                    .ScorePositive = SQLHelper.GetDecimalFromDataReader(oDrd("ScorePositive"))
                    .ScoreNeutral = SQLHelper.GetDecimalFromDataReader(oDrd("ScoreNeutral"))
                    .ScoreNegative = SQLHelper.GetDecimalFromDataReader(oDrd("ScoreNegative"))
                    .isLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oFeedbackSource.isLoaded
        Return retval
    End Function

    Shared Function Update(oFeedbackSource As DTOFeedback.SourceClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oFeedbackSource, oTrans)
            oTrans.Commit()
            oFeedbackSource.isNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oFeedbackSource As DTOFeedback.SourceClass, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM FeedbackSource ")
        sb.AppendLine("WHERE Guid='" & oFeedbackSource.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oFeedbackSource.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oFeedbackSource
            oRow("Emp") = .Emp.id
            oRow("Cod") = .Cod
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oFeedbackSource As DTOFeedback.SourceClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oFeedbackSource, oTrans)
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


    Shared Sub Delete(oFeedbackSource As DTOFeedback.SourceClass, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE FeedbackSource WHERE Guid='" & oFeedbackSource.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class FeedbackSourcesLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOFeedback.SourceClass)
        Dim retval As New List(Of DTOFeedback.SourceClass)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT FeedbackSource.Guid, FeedbackSource.Cod, FeedbackSource.Nom ")
        sb.AppendLine(", SUM(CASE WHEN Feedback.Score = 1 THEN 1 ELSE 0 END) AS ScorePositive ")
        sb.AppendLine(", SUM(CASE WHEN Feedback.Score = 2 THEN 1 ELSE 0 END) AS ScoreNeutral ")
        sb.AppendLine(", SUM(CASE WHEN Feedback.Score = 3 THEN 1 ELSE 0 END) AS ScoreNegative ")
        sb.AppendLine("FROM FeedbackSource ")
        sb.AppendLine("LEFT OUTER JOIN Feedback ON FeedbackSource.Guid = Feedback.Source ")
        sb.AppendLine("WHERE FeedbackSource.Emp = " & oEmp.id & " ")
        sb.AppendLine("GROUP BY FeedbackSource.Guid, FeedbackSource.Cod, FeedbackSource.Nom ")
        sb.AppendLine("ORDER BY FeedbackSource.Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOFeedback.SourceClass(oDrd("Guid"))
            With item
                .Emp = oEmp
                .Cod = oDrd("Cod")
                .Nom = oDrd("Nom")
                .ScorePositive = SQLHelper.GetDecimalFromDataReader(oDrd("ScorePositive"))
                .ScoreNeutral = SQLHelper.GetDecimalFromDataReader(oDrd("ScoreNeutral"))
                .ScoreNegative = SQLHelper.GetDecimalFromDataReader(oDrd("ScoreNegative"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
