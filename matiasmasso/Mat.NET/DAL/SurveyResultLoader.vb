Public Class SurveyResultLoader

#Region "CRUD"

    Shared Function Find(oSurvey As DTOSurvey, oUser As DTOUser) As DTOSurveyResult
        Dim retval As DTOSurveyResult = Nothing
        Dim oSurveyResult As New DTOSurveyResult(oSurvey, oUser)
        If Load(oSurveyResult) Then
            retval = oSurveyResult
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSurveyResult As DTOSurveyResult) As Boolean
        If Not oSurveyResult.IsLoaded And Not oSurveyResult.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT SurveyAnswer.Guid AS AnswerGuid, SurveyAnswer.Value AS AnswerValue, SurveyAnswer.Text AS AnswerText ")
            sb.AppendLine(", SurveyQuestion.Guid AS QuestionGuid, SurveyQuestion.Text AS QuestionText ")
            sb.AppendLine(", SurveyStep.Guid AS StepGuid, SurveyStep.Text AS StepText ")
            sb.AppendLine(", SurveyResult.Fch ")
            sb.AppendLine("FROM SurveyResult ")
            sb.AppendLine("INNER JOIN SurveyAnswer ON SurveyResult.Answer = SurveyAnswer.Guid ")
            sb.AppendLine("INNER JOIN SurveyQuestion ON SurveyAnswer.Parent = SurveyQuestion.Guid ")
            sb.AppendLine("INNER JOIN SurveyStep ON SurveyQuestion.Parent = SurveyStep.Guid ")
            sb.AppendLine("WHERE SurveyStep.Parent ='" & oSurveyResult.Survey.Guid.ToString & "' ")
            sb.AppendLine("AND SurveyResult.[User] = '" & oSurveyResult.User.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY SurveyStep.Ord, SurveyQuestion.Ord ")

            Dim oStep As New DTOSurveyStep
            Dim oQuestion As New DTOSurveyQuestion
            oSurveyResult.Answers = New List(Of DTOSurveyAnswer)

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oStep.Guid.Equals(oDrd("StepGuid")) Then
                    oStep = New DTOSurveyStep(oDrd("StepGuid"))
                    oStep.Text = oDrd("StepText")
                    oStep.Parent = oSurveyResult.Survey
                    oSurveyResult.Fch = oDrd("Fch")
                End If
                If Not oQuestion.Guid.Equals(oDrd("QuestionGuid")) Then
                    oQuestion = New DTOSurveyQuestion(oDrd("QuestionGuid"))
                    oQuestion.Text = oDrd("QuestionText")
                    oQuestion.Parent = oStep
                End If
                Dim oAnswer As New DTOSurveyAnswer(oDrd("AnswerGuid"))
                With oAnswer
                    .Parent = oQuestion
                    .Value = SQLHelper.GetIntegerFromDataReader(oDrd("AnswerValue"))
                    .Text = oDrd("AnswerText")
                End With
                oSurveyResult.Answers.Add(oAnswer)
            Loop

            oDrd.Close()
            oSurveyResult.IsLoaded = True
        End If

        Dim retval As Boolean = oSurveyResult.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSurveyResult As DTOSurveyResult, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSurveyResult, oTrans)
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


    Shared Sub Update(oSurveyResult As DTOSurveyResult, ByRef oTrans As SqlTransaction)
        Delete(oSurveyResult, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SurveyResult ")
        sb.AppendLine("WHERE User='" & oSurveyResult.User.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oAnswer As DTOSurveyAnswer In oSurveyResult.Answers
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("User") = oSurveyResult.User.Guid
            oRow("Fch") = oSurveyResult.Fch
            oRow("Answer") = oAnswer.Guid
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSurveyResult As DTOSurveyResult, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSurveyResult, oTrans)
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


    Shared Sub Delete(oSurveyResult As DTOSurveyResult, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE SurveyResult ")
        sb.AppendLine("WHERE SurveyResult.[User] ='" & oSurveyResult.User.Guid.ToString & "' ")
        sb.AppendLine("AND SurveyResult.Answer IN (SELECT SurveyAnswer.Guid FROM SurveyAnswer ")
        sb.AppendLine("                             INNER JOIN SurveyQuestion ON SurveyAnswer.Parent = SurveyQuestion.Guid")
        sb.AppendLine("                             INNER JOIN SurveyStep ON SurveyQuestion.Parent = SurveyStep.Guid AND SurveyStep.Parent = '" & oSurveyResult.Survey.Guid.ToString & "') ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class SurveyResultsLoader

    Shared Function All(oAnswer As DTOSurveyAnswer) As List(Of DTOSurveyResult)
        Dim retval As New List(Of DTOSurveyResult)
        Dim oSurvey As DTOSurvey = oAnswer.Parent.Parent.Parent
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT User, Fch ")
        sb.AppendLine("FROM SurveyResult ")
        sb.AppendLine("WHERE Answer = '" & oAnswer.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(CType(oDrd("User"), Guid))
            Dim item As New DTOSurveyResult(oSurvey, oUser)
            item.Fch = oDrd("Fch")
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
