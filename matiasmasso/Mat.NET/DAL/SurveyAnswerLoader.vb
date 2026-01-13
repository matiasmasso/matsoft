Public Class SurveyAnswerLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSurveyAnswer
        Dim retval As DTOSurveyAnswer = Nothing
        Dim oSurveyAnswer As New DTOSurveyAnswer(oGuid)
        If Load(oSurveyAnswer) Then
            retval = oSurveyAnswer
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSurveyAnswer As DTOSurveyAnswer) As Boolean
        If Not oSurveyAnswer.IsLoaded And Not oSurveyAnswer.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT SurveyAnswer.Guid as AnswerGuid, SurveyAnswer.Parent AS QuestionGuid, SurveyAnswer.Ord as AnswerOrd, SurveyAnswer.Text as AnswerText, SurveyAnswer.Value as AnswerValue ")
            sb.AppendLine("FROM SurveyAnswer ")
            sb.AppendLine("WHERE SurveyAnswer.Guid='" & oSurveyAnswer.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oSurveyAnswer
                    .Parent = New DTOSurveyQuestion(oDrd("QuestionGuid"))
                    .Ord = SQLHelper.GetIntegerFromDataReader(oDrd("AnswerOrd"))
                    .Text = SQLHelper.GetStringFromDataReader(oDrd("AnswerText"))
                    .Value = SQLHelper.GetIntegerFromDataReader(oDrd("AnswerValue"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oSurveyAnswer.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSurveyAnswer As DTOSurveyAnswer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSurveyAnswer, oTrans)
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


    Shared Sub Update(oSurveyAnswer As DTOSurveyAnswer, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SurveyAnswer ")
        sb.AppendLine("WHERE Guid='" & oSurveyAnswer.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSurveyAnswer.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSurveyAnswer
            oRow("Parent") = .Parent.Guid
            oRow("Ord") = .Ord
            oRow("Text") = SQLHelper.NullableString(.Text)
            oRow("Value") = SQLHelper.NullableInt(.Value)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSurveyAnswer As DTOSurveyAnswer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSurveyAnswer, oTrans)
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


    Shared Sub Delete(oSurveyAnswer As DTOSurveyAnswer, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SurveyAnswer WHERE Guid='" & oSurveyAnswer.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class SurveyAnswersLoader

    Shared Function Scores(oSurvey As DTOSurvey) As List(Of DTOSurveyAnswer)
        Dim retval As New List(Of DTOSurveyAnswer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SurveyAnswer.Parent, SurveyQuestion.Parent AS StepGuid, SurveyAnswer.Value, SurveyResult.Answer, SA.MaxValue ")
        sb.AppendLine(", COUNT(DISTINCT SurveyResult.Participant) AS ParticipantsCount ")
        sb.AppendLine("FROM SurveyResult ")
        sb.AppendLine("INNER JOIN SurveyAnswer ON SurveyResult.Answer = SurveyAnswer.Guid ")
        sb.AppendLine("INNER JOIN SurveyQuestion ON SurveyAnswer.Parent=SurveyQuestion.Guid ")
        sb.AppendLine("INNER JOIN SurveyStep ON SurveyQuestion.Parent=SurveyStep.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT SurveyAnswer.Parent AS Question, MAX(SurveyAnswer.Value) AS MaxValue FROM SurveyAnswer GROUP BY SurveyAnswer.Parent) SA ON SurveyAnswer.Parent = SA.Question ")
        sb.AppendLine("WHERE SurveyStep.Parent = '" & oSurvey.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY SurveyAnswer.Parent, SurveyQuestion.Parent, SurveyAnswer.Value, SurveyResult.Answer, SurveyStep.Ord, SurveyQuestion.Ord, SurveyAnswer.Ord, SA.MaxValue ")
        sb.AppendLine("ORDER BY SurveyStep.Ord, SurveyQuestion.Ord, SurveyAnswer.Ord ")

        Dim oStep As New DTOSurveyStep
        Dim oQuestion As New DTOSurveyQuestion
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oStep.Guid.Equals(oDrd("StepGuid")) Then
                oStep = New DTOSurveyStep(oDrd("StepGuid"))
            End If
            If Not oQuestion.Guid.Equals(oDrd("Parent")) Then
                oQuestion = New DTOSurveyQuestion(oDrd("Parent"))
                oQuestion.Parent = oStep
                oQuestion.MaxValue = SQLHelper.GetIntegerFromDataReader(oDrd("MaxValue"))
            End If
            Dim item As New DTOSurveyAnswer(oDrd("Answer"))
            With item
                .ParticipantsCount = oDrd("ParticipantsCount")
                .Value = SQLHelper.GetIntegerFromDataReader(oDrd("Value"))
                .Parent = oQuestion
                .Parent.ParticipantsCount += .ParticipantsCount
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

