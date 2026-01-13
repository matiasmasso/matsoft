Public Class SurveyLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSurvey
        Dim retval As DTOSurvey = Nothing
        Dim oSurvey As New DTOSurvey(oGuid)
        If Load(oSurvey) Then
            retval = oSurvey
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSurvey As DTOSurvey) As Boolean
        If Not oSurvey.IsLoaded And Not oSurvey.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Survey.Guid as SurveyGuid, Survey.Title as SurveyTitle, Survey.Text as SurveyText, Survey.FchFrom as SurveyFchFrom, Survey.FchTo AS SurveyFchTo ")
            sb.AppendLine(", SurveyStep.Guid as StepGuid, SurveyStep.Ord AS StepOrd, SurveyStep.Title as StepTitle ")
            sb.AppendLine(", SurveyQuestion.Guid as QuestionGuid, SurveyQuestion.Ord as QuestionOrd, SurveyQuestion.Text as QuestionText ")
            sb.AppendLine(", SurveyAnswer.Guid as AnswerGuid, SurveyAnswer.Ord as AnswerOrd, SurveyAnswer.Text as AnswerText, SurveyAnswer.Value as AnswerValue ")
            sb.AppendLine("FROM Survey ")
            sb.AppendLine("LEFT OUTER JOIN SurveyStep ON Survey.Guid = SurveyStep.Parent ")
            sb.AppendLine("LEFT OUTER JOIN SurveyQuestion ON SurveyStep.Guid = SurveyQuestion.Parent ")
            sb.AppendLine("LEFT OUTER JOIN SurveyAnswer ON SurveyQuestion.Guid = SurveyAnswer.Parent ")
            sb.AppendLine("WHERE Survey.Guid='" & oSurvey.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY SurveyStep.Ord, SurveyQuestion.Ord, SurveyAnswer.Ord ")

            Dim oSurveyStep As New DTOSurveyStep
            Dim oSurveyQuestion As New DTOSurveyQuestion

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oSurvey.IsLoaded Then
                    With oSurvey
                        .Title = SQLHelper.GetStringFromDataReader(oDrd("SurveyTitle"))
                        .Text = SQLHelper.GetStringFromDataReader(oDrd("SurveyText"))
                        .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("SurveyFchFrom"))
                        .FchTo = SQLHelper.GetFchFromDataReader(oDrd("SurveyFchTo"))
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("StepGuid")) Then
                    If Not oSurveyStep.Guid.Equals(oDrd("StepGuid")) Then
                        oSurveyStep = New DTOSurveyStep(oDrd("StepGuid"))
                        With oSurveyStep
                            .Parent = oSurvey
                            .Ord = SQLHelper.GetIntegerFromDataReader(oDrd("StepOrd"))
                            .Title = SQLHelper.GetStringFromDataReader(oDrd("StepTitle"))
                        End With
                        oSurvey.Steps.Add(oSurveyStep)
                    End If

                    If Not IsDBNull(oDrd("QuestionGuid")) Then
                        If Not oSurveyQuestion.Guid.Equals(oDrd("QuestionGuid")) Then
                            oSurveyQuestion = New DTOSurveyQuestion(oDrd("QuestionGuid"))
                            With oSurveyQuestion
                                .Parent = oSurveyStep
                                .Ord = SQLHelper.GetIntegerFromDataReader(oDrd("QuestionOrd"))
                                .Text = SQLHelper.GetStringFromDataReader(oDrd("QuestionText"))
                            End With
                            oSurveyStep.Questions.Add(oSurveyQuestion)
                        End If

                        Dim oSurveyAnswer As New DTOSurveyAnswer(oDrd("AnswerGuid"))
                        With oSurveyAnswer
                            .Parent = oSurveyQuestion
                            .Ord = SQLHelper.GetIntegerFromDataReader(oDrd("AnswerOrd"))
                            .Text = SQLHelper.GetStringFromDataReader(oDrd("AnswerText"))
                            .Value = SQLHelper.GetIntegerFromDataReader(oDrd("AnswerValue"))
                        End With
                        oSurveyQuestion.Answers.Add(oSurveyAnswer)
                    End If
                End If
            Loop

            oDrd.Close()
        End If
        Dim retval As Boolean = oSurvey.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSurvey As DTOSurvey, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSurvey, oTrans)
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


    Shared Sub Update(oSurvey As DTOSurvey, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Survey ")
        sb.AppendLine("WHERE Guid='" & oSurvey.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSurvey.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSurvey
            oRow("Title") = SQLHelper.NullableString(.Title)
            oRow("Text") = SQLHelper.NullableString(.Text)
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("FchFrom") = SQLHelper.NullableFch(.FchFrom)
            oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSurvey As DTOSurvey, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSurvey, oTrans)
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


    Shared Sub Delete(oSurvey As DTOSurvey, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Survey WHERE Guid='" & oSurvey.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function ResetScores(oSurvey As DTOSurvey, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim SQL As String = "UPDATE SurveyParticipant SET Fch=NULL, Obs=NULL WHERE Survey='" & oSurvey.Guid.ToString & "' "
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            Dim sb As New System.Text.StringBuilder
            sb.Append("DELETE SurveyResult WHERE Answer IN (")
            sb.Append("     SELECT SurveyAnswer.Guid ")
            sb.Append("     FROM SurveyAnswer")
            sb.Append("     INNER JOIN SurveyQuestion ON SurveyAnswer.Parent = SurveyQuestion.Guid ")
            sb.Append("     INNER JOIN SurveyStep ON SurveyQuestion.Parent = SurveyStep.Guid ")
            sb.Append("     WHERE SurveyStep.Parent = '" & oSurvey.Guid.ToString & "' ")
            sb.Append(" ) ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

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


End Class

Public Class SurveysLoader

    Shared Function Headers() As List(Of DTOSurvey)
        Dim retval As New List(Of DTOSurvey)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Survey ")
        sb.AppendLine("ORDER BY FchFrom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSurvey(oDrd("Guid"))
            With item
                .Title = SQLHelper.GetStringFromDataReader(oDrd("Title"))
                .Text = SQLHelper.GetStringFromDataReader(oDrd("Text"))
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
