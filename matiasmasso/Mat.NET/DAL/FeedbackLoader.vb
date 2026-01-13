Public Class FeedbackLoader

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOFeedback
        Dim retval As DTOFeedback = Nothing
        Dim oFeedback As New DTOFeedback(oGuid)
        If Load(oFeedback) Then
            retval = oFeedback
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oFeedback As DTOFeedback) As Boolean
        If Not oFeedback.IsLoaded And Not oFeedback.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Feedback.Target, Feedback.Fch, Feedback.UserOrCustomer, Feedback.UserOrCustomerCod, Feedback.Cod ")
            sb.AppendLine(", CliGral.FullNom ")
            sb.AppendLine(", Email.Adr, Email.Nickname, Email.Nom, Email.Cognoms ")
            sb.AppendLine("FROM Feedback ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON Feedback.UserOrCustomer = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email ON Feedback.UserOrCustomer = Email.Guid ")
            sb.AppendLine("WHERE Feedback.Guid='" & oFeedback.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oFeedback
                    .Target = New DTOBaseGuid(oDrd("Target"))
                    .Fch = oDrd("Fch")
                    .UserOrCustomerCod = oDrd("UserOrCustomerCod")
                    Select Case .UserOrCustomerCod
                        Case DTOFeedback.UserOrCustomerCods.User
                            Dim oUser As New DTOUser(oDrd("UserOrCustomer"))
                            With oUser
                                .adr = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                                .nickName = SQLHelper.GetStringFromDataReader(oDrd("nickName"))
                                .nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                                .cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                            End With
                            .UserOrCustomer = oUser.ToGuidNom()
                        Case DTOFeedback.UserOrCustomerCods.Customer
                            .UserOrCustomer = New DTOGuidNom(oDrd("UserOrCustomer"), SQLHelper.GetStringFromDataReader(oDrd("FullNom")))
                    End Select
                    .Cod = oDrd("Cod")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oFeedback.IsLoaded
        Return retval
    End Function

    Shared Function Update(oFeedback As DTOFeedback, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oFeedback, oTrans)
            oTrans.Commit()
            oFeedback.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oFeedback As DTOFeedback, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Feedback ")
        sb.AppendLine("WHERE Guid='" & oFeedback.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oFeedback.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oFeedback
            oRow("Target") = .Target.Guid
            oRow("Fch") = .Fch
            oRow("UserOrCustomerCod") = .UserOrCustomerCod
            oRow("UserOrCustomer") = SQLHelper.NullableBaseGuid(.UserOrCustomer)
            oRow("Cod") = .Cod
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oFeedback As DTOFeedback, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oFeedback, oTrans)
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


    Shared Sub Delete(oFeedback As DTOFeedback, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Feedback WHERE Guid='" & oFeedback.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class FeedbacksLoader

    Shared Function All(oTarget As DTOBaseGuid) As List(Of DTOFeedback)
        Dim retval As New List(Of DTOFeedback)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Feedback.Guid ")
        sb.AppendLine(", Feedback.Fch, Feedback.UserOrCustomer, Feedback.UserOrCustomerCod, Feedback.Cod ")
        sb.AppendLine(", CliGral.FullNom ")
        sb.AppendLine(", Email.Adr, Email.Nickname, Email.Nom, Email.Cognoms ")
        sb.AppendLine("FROM Feedback ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Feedback.UserOrCustomer = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email ON Feedback.UserOrCustomer = Email.Guid ")
        sb.AppendLine("WHERE Feedback.Target='" & oTarget.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Feedback.Fch DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOFeedback(oDrd("Guid"))
            With item
                .Target = oTarget
                .Fch = oDrd("Fch")
                .UserOrCustomerCod = oDrd("UserOrCustomerCod")
                Select Case .UserOrCustomerCod
                    Case DTOFeedback.UserOrCustomerCods.User
                        Dim oUser As New DTOUser(oDrd("UserOrCustomer"))
                        With oUser
                            .adr = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                            .nickName = SQLHelper.GetStringFromDataReader(oDrd("nickName"))
                            .nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                            .cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                        End With
                        .UserOrCustomer = oUser.ToGuidNom()
                    Case DTOFeedback.UserOrCustomerCods.Customer
                        .UserOrCustomer = New DTOGuidNom(oDrd("UserOrCustomer"), SQLHelper.GetStringFromDataReader(oDrd("FullNom")))
                End Select
                .Cod = oDrd("Cod")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Model(oTarget As DTOBaseGuid, oUser As DTOUser) As DTOFeedback.Model
        Dim retval As New DTOFeedback.Model
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SUM(CASE WHEN Cod = " & DTOFeedback.Cods.Like & " THEN 1 ELSE 0 END) AS Likes ")
        sb.AppendLine(", SUM(CASE WHEN Cod = " & DTOFeedback.Cods.Share & " THEN 1 ELSE 0 END) AS Shares ")
        sb.AppendLine(", SUM(CASE WHEN Cod = " & DTOFeedback.Cods.Like & " AND UserOrCustomer='" & oUser.Guid.ToString & "' THEN 1 ELSE 0 END) AS HasLiked ")
        sb.AppendLine(", SUM(CASE WHEN Cod = " & DTOFeedback.Cods.Share & " AND UserOrCustomer='" & oUser.Guid.ToString & "' THEN 1 ELSE 0 END) AS HasShared ")
        sb.AppendLine("FROM Feedback ")
        sb.AppendLine("WHERE Feedback.Target='" & oTarget.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With retval
                .Likes = SQLHelper.GetIntegerFromDataReader(oDrd("Likes"))
                .Shares = SQLHelper.GetIntegerFromDataReader(oDrd("Shares"))
                .HasLiked = SQLHelper.GetIntegerFromDataReader(oDrd("HasLiked"))
                .HasShared = SQLHelper.GetIntegerFromDataReader(oDrd("HasShared"))
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

End Class
