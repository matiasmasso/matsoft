Public Class SurveyStepLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSurveyStep
        Dim retval As DTOSurveyStep = Nothing
        Dim oSurveyStep As New DTOSurveyStep(oGuid)
        If Load(oSurveyStep) Then
            retval = oSurveyStep
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSurveyStep As DTOSurveyStep) As Boolean
        If Not oSurveyStep.IsLoaded And Not oSurveyStep.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT SurveyStep.Parent AS SurveyGuid, SurveyStep.Guid as StepGuid, SurveyStep.Title as StepTitle, SurveyStep.Text as StepText, SurveyStep.Ord as StepOrd ")
            sb.AppendLine(", SurveyQuestion.Guid as QuestionGuid, SurveyQuestion.Ord as QuestionOrd, SurveyQuestion.Text as QuestionText ")
            sb.AppendLine(", SurveyAnswer.Guid as AnswerGuid, SurveyAnswer.Ord as AnswerOrd, SurveyAnswer.Text as AnswerText ")
            sb.AppendLine("FROM SurveyStep ")
            sb.AppendLine("INNER JOIN SurveyQuestion ON SurveyStep.Guid = SurveyQuestion.Parent ")
            sb.AppendLine("INNER JOIN SurveyAnswer ON SurveyQuestion.Guid = SurveyAnswer.Parent ")
            sb.AppendLine("WHERE SurveyStep.Guid='" & oSurveyStep.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY SurveyQuestion.Ord, SurveyAnswer.Ord ")

            Dim oSurveyQuestion As New DTOSurveyQuestion

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oSurveyStep.IsLoaded Then
                    With oSurveyStep
                        .Parent = New DTOSurvey(oDrd("SurveyGuid"))
                        .Title = SQLHelper.GetStringFromDataReader(oDrd("StepTitle"))
                        .Text = SQLHelper.GetStringFromDataReader(oDrd("StepText"))
                        .Ord = SQLHelper.GetIntegerFromDataReader(oDrd("StepOrd"))
                        .IsLoaded = True
                    End With
                End If

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
                End With
                oSurveyQuestion.Answers.Add(oSurveyAnswer)

            Loop

            oDrd.Close()
        End If
        Dim retval As Boolean = oSurveyStep.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSurveyStep As DTOSurveyStep, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSurveyStep, oTrans)
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


    Shared Sub Update(oSurveyStep As DTOSurveyStep, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SurveyStep ")
        sb.AppendLine("WHERE Guid='" & oSurveyStep.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSurveyStep.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSurveyStep
            oRow("Parent") = .Parent.Guid
            oRow("Ord") = .Ord
            oRow("Title") = .Title
            oRow("Text") = SQLHelper.NullableString(.Text)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSurveyStep As DTOSurveyStep, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSurveyStep, oTrans)
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


    Shared Sub Delete(oSurveyStep As DTOSurveyStep, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SurveyStep WHERE Guid='" & oSurveyStep.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class SurveyStepsLoader

    Shared Function Headers() As List(Of DTOSurveyStep)
        Dim retval As New List(Of DTOSurveyStep)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SurveyStep ")
        sb.AppendLine("ORDER BY Ord ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSurveyStep(oDrd("Guid"))
            With item
                .Title = SQLHelper.GetStringFromDataReader(oDrd("Title"))
                .Text = SQLHelper.GetStringFromDataReader(oDrd("Text"))
            End With
            retval.Add(item)
            Loop
            oDrd.Close()
            Return retval
        End Function
    End Class
