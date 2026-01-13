Public Class SurveyQuestionLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSurveyQuestion
        Dim retval As DTOSurveyQuestion = Nothing
        Dim oSurveyQuestion As New DTOSurveyQuestion(oGuid)
        If Load(oSurveyQuestion) Then
            retval = oSurveyQuestion
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSurveyQuestion As DTOSurveyQuestion) As Boolean
        If Not oSurveyQuestion.IsLoaded And Not oSurveyQuestion.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT SurveyQuestion.Parent as StepGuid, SurveyQuestion.Ord as QuestionOrd, SurveyQuestion.Text as QuestionText ")
            sb.AppendLine(", SurveyAnswer.Guid as AnswerGuid, SurveyAnswer.Ord as AnswerOrd, SurveyAnswer.Text as AnswerText ")
            sb.AppendLine("FROM SurveyQuestion ")
            sb.AppendLine("INNER JOIN SurveyAnswer ON SurveyQuestion.Guid = SurveyAnswer.Parent ")
            sb.AppendLine("WHERE SurveyQuestion.Guid='" & oSurveyQuestion.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY SurveyAnswer.Ord ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oSurveyQuestion.IsLoaded Then
                    With oSurveyQuestion
                        .Parent = New DTOSurveyStep(oDrd("StepGuid"))
                        .Ord = SQLHelper.GetIntegerFromDataReader(oDrd("QuestionOrd"))
                        .Text = SQLHelper.GetStringFromDataReader(oDrd("QuestionText"))
                        .IsLoaded = True
                    End With
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
        Dim retval As Boolean = oSurveyQuestion.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSurveyQuestion As DTOSurveyQuestion, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSurveyQuestion, oTrans)
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


    Shared Sub Update(oSurveyQuestion As DTOSurveyQuestion, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SurveyQuestion ")
        sb.AppendLine("WHERE Guid='" & oSurveyQuestion.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSurveyQuestion.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSurveyQuestion
            oRow("Parent") = .Parent.Guid
            oRow("Ord") = .Ord
            oRow("Text") = SQLHelper.NullableString(.Text)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSurveyQuestion As DTOSurveyQuestion, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSurveyQuestion, oTrans)
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


    Shared Sub Delete(oSurveyQuestion As DTOSurveyQuestion, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SurveyQuestion WHERE Guid='" & oSurveyQuestion.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class SurveyQuestionsLoader

    Shared Function Headers() As List(Of DTOSurveyQuestion)
        Dim retval As New List(Of DTOSurveyQuestion)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SurveyQuestion ")
        sb.AppendLine("ORDER BY Ord ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSurveyQuestion(oDrd("Guid"))
            With item
                .Parent = New DTOSurveyStep(oDrd("Parent"))
                .Text = SQLHelper.GetStringFromDataReader(oDrd("Text"))
                .Ord = SQLHelper.GetIntegerFromDataReader(oDrd("Ord"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

