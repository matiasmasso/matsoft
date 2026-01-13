Public Class SurveyParticipantLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSurveyParticipant
        Dim retval As DTOSurveyParticipant = Nothing
        Dim oSurveyParticipant As New DTOSurveyParticipant(oGuid)
        If Load(oSurveyParticipant) Then
            retval = oSurveyParticipant
        End If
        Return retval
    End Function

    Shared Function FindOrNew(oSurvey As DTOSurvey, oUser As DTOUser) As DTOSurveyParticipant
        Dim retval As DTOSurveyParticipant = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SurveyParticipant.Guid ")
        sb.AppendLine("FROM SurveyParticipant ")
        sb.AppendLine("WHERE SurveyParticipant.Survey ='" & oSurvey.Guid.ToString & "' ")
        sb.AppendLine("AND SurveyParticipant.[User] = '" & oUser.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOSurveyParticipant(oDrd("Guid"))
        Else
            retval = New DTOSurveyParticipant
        End If
        oDrd.Close()

        retval.Survey = oSurvey
        retval.User = oUser
        Return retval
    End Function

    Shared Function Load(ByRef oSurveyParticipant As DTOSurveyParticipant) As Boolean
        If Not oSurveyParticipant.IsLoaded And Not oSurveyParticipant.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT SurveyParticipant.Survey, SurveyParticipant.[User], SurveyParticipant.Fch AS UserFch, SurveyParticipant.Obs AS UserObs ")
            sb.AppendLine(", SurveyResult.Answer ")
            sb.AppendLine(", SurveyAnswer.Guid AS AnswerGuid, SurveyAnswer.Value AS AnswerValue, SurveyAnswer.Text AS AnswerText ")
            sb.AppendLine(", SurveyQuestion.Guid AS QuestionGuid, SurveyQuestion.Text AS QuestionText ")
            sb.AppendLine(", SurveyStep.Guid AS StepGuid, SurveyStep.Text AS StepText ")
            sb.AppendLine(", Pdcs.Eur ")
            sb.AppendLine("FROM SurveyParticipant ")
            sb.AppendLine("LEFT OUTER JOIN SurveyResult ON SurveyParticipant.Guid = SurveyResult.Participant ")
            sb.AppendLine("LEFT OUTER JOIN SurveyAnswer ON SurveyResult.Answer = SurveyAnswer.Guid ")
            sb.AppendLine("LEFT OUTER JOIN SurveyQuestion ON SurveyAnswer.Parent = SurveyQuestion.Guid ")
            sb.AppendLine("LEFT OUTER JOIN SurveyStep ON SurveyQuestion.Parent = SurveyStep.Guid ")

            sb.AppendLine("LEFT OUTER JOIN (SELECT Email_Clis.EmailGuid, SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) AS Eur  ")
            sb.AppendLine("     FROM Email_Clis ")
            sb.AppendLine("     INNER JOIN Pdc ON Email_Clis.ContactGuid = Pdc.CliGuid ")
            sb.AppendLine("     INNER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
            sb.AppendLine("     WHERE Pdc.Fch>DATEADD(yy,-1,getdate()) ")
            sb.AppendLine("     GROUP BY Email_Clis.EmailGuid  ")
            sb.AppendLine("     ) Pdcs ON SurveyParticipant.[User] = Pdcs.EmailGuid ")

            sb.AppendLine("WHERE SurveyParticipant.Guid ='" & oSurveyParticipant.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY SurveyStep.Ord, SurveyQuestion.Ord ")

            Dim oStep As New DTOSurveyStep
            Dim oQuestion As New DTOSurveyQuestion
            oSurveyParticipant.Answers = New List(Of DTOSurveyAnswer)

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oSurveyParticipant.IsLoaded Then
                    With oSurveyParticipant
                        If .Survey Is Nothing Then .Survey = New DTOSurvey(oDrd("Survey"))
                        If .User Is Nothing Then .User = New DTOUser(CType(oDrd("User"), Guid))
                        .Fch = SQLHelper.GetFchFromDataReader(oDrd("UserFch"))
                        .Obs = SQLHelper.GetStringFromDataReader(oDrd("UserObs"))
                        .Eur = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                        .Answers = New List(Of DTOSurveyAnswer)
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("Answer")) Then
                    If Not oStep.Guid.Equals(oDrd("StepGuid")) Then
                        oStep = New DTOSurveyStep(oDrd("StepGuid"))
                        oStep.Text = SQLHelper.GetStringFromDataReader(oDrd("StepText"))
                        oStep.Parent = oSurveyParticipant.Survey
                        oSurveyParticipant.Fch = oDrd("UserFch")
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
                    oSurveyParticipant.Answers.Add(oAnswer)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oSurveyParticipant.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSurveyParticipant As DTOSurveyParticipant, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSurveyParticipant, oTrans)
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


    Shared Sub Update(oSurveyParticipant As DTOSurveyParticipant, ByRef oTrans As SqlTransaction)
        UpdateHeader(oSurveyParticipant, oTrans)
        If oSurveyParticipant.Answers IsNot Nothing Then
            UpdateItems(oSurveyParticipant, oTrans)
        End If
    End Sub

    Shared Sub UpdateHeader(oSurveyParticipant As DTOSurveyParticipant, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SurveyParticipant ")
        sb.AppendLine("WHERE Guid='" & oSurveyParticipant.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = Nothing
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSurveyParticipant.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        oRow("User") = SQLHelper.NullableBaseGuid(oSurveyParticipant.User)
        oRow("Survey") = SQLHelper.NullableBaseGuid(oSurveyParticipant.Survey)
        oRow("Fch") = SQLHelper.NullableFch(oSurveyParticipant.Fch)
        oRow("Obs") = SQLHelper.NullableString(oSurveyParticipant.Obs)

        oDA.Update(oDs)
    End Sub
    Shared Sub UpdateItems(oSurveyParticipant As DTOSurveyParticipant, ByRef oTrans As SqlTransaction)
        DeleteItems(oSurveyParticipant, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SurveyResult ")
        sb.AppendLine("WHERE Participant='" & oSurveyParticipant.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oAnswer As DTOSurveyAnswer In oSurveyParticipant.Answers
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Participant") = SQLHelper.NullableBaseGuid(oSurveyParticipant)
            oRow("Answer") = SQLHelper.NullableBaseGuid(oAnswer)
        Next

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oSurveyParticipant As DTOSurveyParticipant, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSurveyParticipant, oTrans)
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

    Shared Sub Delete(oSurveyParticipant As DTOSurveyParticipant, ByRef oTrans As SqlTransaction)
        DeleteItems(oSurveyParticipant, oTrans)
        DeleteHeader(oSurveyParticipant, oTrans)
    End Sub

    Shared Sub DeleteHeader(oSurveyParticipant As DTOSurveyParticipant, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SurveyParticipant WHERE Guid='" & oSurveyParticipant.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oSurveyParticipant As DTOSurveyParticipant, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SurveyResult WHERE Participant ='" & oSurveyParticipant.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class SurveyParticipantsLoader

    Shared Function Detall(oSurvey As DTOSurvey, Optional oBrand As DTOProductBrand = Nothing) As List(Of DTOSurveyParticipant)
        Dim retval As New List(Of DTOSurveyParticipant)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SurveyParticipant.Fch, email.adr, email.Nickname, Clx.Clx, X.Eur ")

        If oBrand IsNot Nothing Then sb.AppendLine(", X.Eur ")

        sb.AppendLine(", SUM(CASE WHEN SurveyStep.Ord=0 THEN SurveyAnswer.Value ELSE 0 END) ")
        sb.AppendLine(", SUM(CASE WHEN SurveyStep.Ord=1 THEN SurveyAnswer.Value ELSE 0 END) ")
        sb.AppendLine(", SUM(CASE WHEN SurveyStep.Ord=2 THEN SurveyAnswer.Value ELSE 0 END) ")
        sb.AppendLine(", SUM(CASE WHEN SurveyStep.Ord=3 THEN SurveyAnswer.Value ELSE 0 END) ")
        sb.AppendLine("  FROM SurveyResult ")
        sb.AppendLine("  INNER JOIN SurveyParticipant ON SurveyResult.Participant = SurveyParticipant.Guid ")
        sb.AppendLine("  INNER JOIN Email ON SurveyParticipant.[User] = email.Guid ")
        sb.AppendLine("  INNER JOIN Email_Clis ON Email.Guid=Email_Clis.EmailGuid ")
        sb.AppendLine("  LEFT OUTER JOIN Clx ON Email_Clis.ContactGuid=Clx.Guid ")
        sb.AppendLine("  INNER JOIN SurveyAnswer ON SurveyResult.Answer = SurveyAnswer.Guid ")
        sb.AppendLine("  INNER JOIN SurveyQuestion ON SurveyAnswer.Parent=SurveyQuestion.Guid ")
        sb.AppendLine("  INNER JOIN SurveyStep ON SurveyQuestion.Parent = SurveyStep.Guid ")

        If oBrand IsNot Nothing Then
            sb.AppendLine("  LEFT OUTER JOIN (SELECT Pdc.CliGuid , SUM(PNC.EUR*PNC.QTY*(100-PNC.DTO)/100) AS Eur FROM PNC  ")
            sb.AppendLine("		INNER JOIN PDC ON PNC.PDCGUID=Pdc.Guid  ")
            sb.AppendLine("		INNER JOIN Art ON Pnc.ArtGuid=Art.Guid INNER JOIN Stp ON Art.Category = Stp.Guid AND Stp.Brand = '" & oBrand.Guid.ToString & "' ")
            sb.AppendLine("		WHERE Pdc.Fch>=DATEADD(yy,-1,GETDATE()) GROUP BY Pdc.CliGuid) X ON Email_Clis.ContactGuid=X.CliGuid ")
        End If

        sb.AppendLine("  WHERE SurveyStep.Parent='" & oSurvey.Guid.ToString & "' ")
        sb.AppendLine("  GROUP BY SurveyParticipant.Fch, email.adr, email.Nickname, Clx.Clx, X.Eur ")
        If oBrand IsNot Nothing Then sb.AppendLine(", X.Eur ")

        sb.AppendLine("  ORDER BY X.Eur DESC, Clx.Clx ")

        'falta acabar
        Return retval
    End Function

    Shared Function Headers(oSurvey As DTOSurvey) As List(Of DTOSurveyParticipant)
        Dim retval As New List(Of DTOSurveyParticipant)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SurveyParticipant.Guid, SurveyParticipant.[User], SurveyParticipant.Fch ")
        sb.AppendLine(", SurveyParticipant.Obs ")
        sb.AppendLine(", Email.adr, Email.Nickname ")
        sb.AppendLine(", AnswerValue.Value, QuestionMaxSum.MaxValue, Pdcs.Eur, Contact.Guid AS ContactGuid, Contact.Clx AS ContactNom ")

        sb.AppendLine("FROM SurveyParticipant ")

        sb.AppendLine("INNER JOIN Email ON SurveyParticipant.[User] = Email.Guid ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT SurveyResult.Participant, SUM(SurveyAnswer.Value) AS Value  ")
        sb.AppendLine("     FROM SurveyResult ")
        sb.AppendLine("     INNER JOIN SurveyAnswer ON SurveyResult.Answer = SurveyAnswer.Guid ")
        sb.AppendLine("     GROUP BY SurveyResult.Participant ")
        sb.AppendLine("     ) AnswerValue ON SurveyParticipant.Guid = AnswerValue.Participant ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT SurveyResult.Participant, SUM(QuestionMaxValue.MaxValue) AS MaxValue  ")
        sb.AppendLine("     FROM SurveyResult ")
        sb.AppendLine("     INNER JOIN SurveyAnswer ON SurveyResult.Answer = SurveyAnswer.Guid ")
        sb.AppendLine("     LEFT OUTER JOIN (SELECT SurveyAnswer.Parent AS Question, MAX(SurveyAnswer.Value) AS MaxValue  ")
        sb.AppendLine("         FROM SurveyAnswer ")
        sb.AppendLine("         GROUP BY SurveyAnswer.Parent ")
        sb.AppendLine("         ) QuestionMaxValue ON SurveyAnswer.Parent = QuestionMaxValue.Question ")
        sb.AppendLine("     GROUP BY SurveyResult.Participant ")
        sb.AppendLine("     ) QuestionMaxSum ON SurveyParticipant.Guid = QuestionMaxSum.Participant ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT Email_Clis.EmailGuid, SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) AS Eur  ")
        sb.AppendLine("     FROM Email_Clis ")
        sb.AppendLine("     INNER JOIN Pdc ON Email_Clis.ContactGuid = Pdc.CliGuid ")
        sb.AppendLine("     INNER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("     WHERE Pdc.Fch>DATEADD(yy,-1,getdate()) ")
        sb.AppendLine("     GROUP BY Email_Clis.EmailGuid  ")
        sb.AppendLine("     ) Pdcs ON SurveyParticipant.[User] = Pdcs.EmailGuid ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT FirstContact.EmailGuid, Clx.Guid, Clx.Clx  ")
        sb.AppendLine("     FROM (SELECT Email_Clis.EmailGuid, MIN(Clx.Guid) AS ClxGuid FROM Email_Clis INNER JOIN Clx ON Email_Clis.ContactGuid=Clx.Guid AND Clx.Ex=0 GROUP BY Email_Clis.EmailGuid) FirstContact ")
        sb.AppendLine("     INNER JOIN Clx ON FirstContact.ClxGuid = Clx.Guid ")
        sb.AppendLine("     ) Contact ON SurveyParticipant.[User] = Contact.EmailGuid ")

        sb.AppendLine("WHERE SurveyParticipant.Survey = '" & oSurvey.Guid.ToString & "' ")

        'sb.AppendLine("GROUP BY SurveyParticipant.Guid, SurveyParticipant.[User], SurveyParticipant.Fch, (CASE WHEN SurveyParticipant.Obs IS NULL THEN 0 ELSE 1 END), Email.adr, Email.Nickname ")
        'sb.AppendLine(", AnswerValue.Value, QuestionMaxSum.MaxValue, Pdcs.Eur, Contact.Guid, Contact.Clx ")

        sb.AppendLine("ORDER BY SurveyParticipant.Fch DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSurveyParticipant(oDrd("Guid"))
            With item
                .Survey = oSurvey
                .User = New DTOUser(CType(oDrd("User"), Guid))
                .User.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                .User.NickName = SQLHelper.GetStringFromDataReader(oDrd("Nickname"))
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .Value = SQLHelper.GetIntegerFromDataReader(oDrd("Value"))
                .MaxValue = SQLHelper.GetIntegerFromDataReader(oDrd("MaxValue"))
                .Eur = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                '.Info = oDrd("Info") <> 0
                If Not IsDBNull(oDrd("ContactGuid")) Then
                    .Contact = New DTOContact(oDrd("ContactGuid"))
                    .Contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("ContactNom"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function MissingParticipants(oSurvey As DTOSurvey) As List(Of DTOSurveyParticipant)
        Dim retval As New List(Of DTOSurveyParticipant)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SurveyParticipant.Guid, email.Guid AS [User], email.Adr ")
        sb.AppendLine("FROM SurveyParticipant ")
        sb.AppendLine("INNER JOIN Email ON SurveyParticipant.[User] = email.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON Email.Guid=Email_Clis.EmailGuid ")
        sb.AppendLine("LEFT OUTER JOIN ( ")
        sb.AppendLine("	    SELECT  SurveyParticipant.Survey, Email_Clis.ContactGuid ")
        sb.AppendLine("	    FROM Email_Clis INNER JOIN SurveyParticipant ON Email_Clis.EmailGuid = SurveyParticipant.[User] ")
        sb.AppendLine("	    WHERE NOT SurveyParticipant.Fch IS NULL ")
        sb.AppendLine("	    GROUP BY SurveyParticipant.Survey, Email_Clis.ContactGuid ")
        sb.AppendLine("  ) X ON Email_Clis.ContactGuid = X.ContactGuid AND SurveyParticipant.Survey = X.Survey ")
        sb.AppendLine("WHERE SurveyParticipant.Survey='BAFCD7D7-2C9E-45E4-9D0F-28A9043BF78B' ")
        sb.AppendLine("AND SurveyParticipant.Fch IS NULL ")
        sb.AppendLine("AND X.ContactGuid IS NULL ")
        sb.AppendLine("GROUP BY SurveyParticipant.Guid, email.Guid, email.Adr ")
        sb.AppendLine("ORDER BY email.Adr ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSurveyParticipant(oDrd("Guid"))
            With item
                .Survey = oSurvey
                .User = New DTOUser(CType(oDrd("User"), Guid))
                .User.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function
End Class
