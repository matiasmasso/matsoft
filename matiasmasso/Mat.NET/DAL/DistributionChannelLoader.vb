Public Class DistributionChannelLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTODistributionChannel
        Dim retval As DTODistributionChannel = Nothing
        Dim oDistributionChannel As New DTODistributionChannel(oGuid)
        If Load(oDistributionChannel) Then
            retval = oDistributionChannel
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oDistributionChannel As DTODistributionChannel) As Boolean
        If Not oDistributionChannel.IsLoaded And Not oDistributionChannel.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT DistributionChannel.Guid AS ChannelGuid, DistributionChannel.NomEsp AS ChannelEsp, DistributionChannel.NomCat AS ChannelCat, DistributionChannel.NomEng AS ChannelEng  ")
            sb.AppendLine(", DistributionChannel.Ord, DistributionChannel.ConsumerPriority ")
            sb.AppendLine(", ContactClass.Guid AS ClassGuid ")
            sb.AppendLine(", ContactClass.Esp AS ClassEsp ")
            sb.AppendLine(", ContactClass.Cat AS ClassCat ")
            sb.AppendLine(", ContactClass.Eng AS ClassEng ")
            sb.AppendLine(", ContactClass.Por AS ClassPor ")
            sb.AppendLine("FROM DistributionChannel ")
            sb.AppendLine("INNER JOIN ContactClass ON DistributionChannel.Guid = ContactClass.DistributionChannel ")
            sb.AppendLine("WHERE DistributionChannel.Guid='" & oDistributionChannel.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            oDistributionChannel.ContactClasses = New List(Of DTOContactClass)
            Do While oDrd.Read
                If oDistributionChannel.IsLoaded = False Then
                    With oDistributionChannel
                        .LangText = New DTOLangText(oDrd("ChannelEsp"), SQLHelper.GetStringFromDataReader(oDrd("ChannelCat")), SQLHelper.GetStringFromDataReader(oDrd("ChannelEng")))
                        .Ord = oDrd("Ord")
                        .ConsumerPriority = SQLHelper.GetIntegerFromDataReader(oDrd("ConsumerPriority"))
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("ClassGuid")) Then
                    Dim oClass As New DTOContactClass(oDrd("ClassGuid"))
                    oClass.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "ClassEsp", "ClassCat", "ClassEng", "ClassPor")
                    oDistributionChannel.ContactClasses.Add(oClass)
                End If
            Loop
            oDrd.Close()
        End If


        Dim retval As Boolean = oDistributionChannel.IsLoaded
        Return retval
    End Function

    Shared Function Update(oDistributionChannel As DTODistributionChannel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oDistributionChannel, oTrans)
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

    Shared Sub Update(oDistributionChannel As DTODistributionChannel, ByRef oTrans As SqlTransaction)
        UpdateHeader(oDistributionChannel, oTrans)
        UpdateDetail(oDistributionChannel, oTrans)
    End Sub


    Shared Sub UpdateHeader(oDistributionChannel As DTODistributionChannel, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM DistributionChannel ")
        sb.AppendLine("WHERE Guid='" & oDistributionChannel.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oDistributionChannel.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oDistributionChannel
            oRow("NomEsp") = SQLHelper.NullableString(.LangText.Esp)
            oRow("NomCat") = SQLHelper.NullableString(.LangText.Cat)
            oRow("NomEng") = SQLHelper.NullableString(.LangText.Eng)
            oRow("Ord") = SQLHelper.NullableInt(.Ord)
            oRow("ConsumerPriority") = SQLHelper.NullableInt(.ConsumerPriority)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateDetail(oDistributionChannel As DTODistributionChannel, ByRef oTrans As SqlTransaction)
        DeleteDetails(oDistributionChannel, oTrans)

        For Each item As DTOContactClass In oDistributionChannel.ContactClasses
            Dim SQL As String = "UPDATE ContactClass SET DistributionChannel='" & oDistributionChannel.Guid.ToString & "' WHERE Guid ='" & item.Guid.ToString & "' "
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Next
    End Sub

    Shared Function Delete(oDistributionChannel As DTODistributionChannel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oDistributionChannel, oTrans)
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

    Shared Sub Delete(oDistributionChannel As DTODistributionChannel, ByRef oTrans As SqlTransaction)
        DeleteDetails(oDistributionChannel, oTrans)
        DeleteHeader(oDistributionChannel, oTrans)
    End Sub


    Shared Sub DeleteDetails(oDistributionChannel As DTODistributionChannel, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE ContactClass SET DistributionChannel = NULL WHERE DistributionChannel='" & oDistributionChannel.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oDistributionChannel As DTODistributionChannel, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE DistributionChannel WHERE Guid='" & oDistributionChannel.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class DistributionChannelsLoader

    Shared Function AllWithContacts(oEmp As DTOEmp) As List(Of DTODistributionChannel)
        Dim retval As New List(Of DTODistributionChannel)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT  X.ChannelGuid, X.ChannelEsp, X.ChannelCat, X.ChannelEng, X.ChannelPor ")
        sb.AppendLine(", X.ClassGuid, X.ClassEsp, X.ClassCat, X.ClassEng, X.ClassPor ")
        sb.AppendLine(", X.ContactGuid, X.ContactNom FROM ( ")
        sb.AppendLine("     SELECT DistributionChannel.Guid AS ChannelGuid ")
        sb.AppendLine("     , DistributionChannel.Ord as ChannelOrd ")
        sb.AppendLine("     , DistributionChannel.NomEsp as ChannelEsp ")
        sb.AppendLine("     , DistributionChannel.NomCat as ChannelCat ")
        sb.AppendLine("     , DistributionChannel.NomEng as ChannelEng ")
        sb.AppendLine("     , DistributionChannel.NomPor as ChannelPor ")
        sb.AppendLine("     , ContactClass.Guid AS ClassGuid ")
        sb.AppendLine("     , ContactClass.Esp AS ClassEsp ")
        sb.AppendLine("     , ContactClass.Cat AS ClassCat ")
        sb.AppendLine("     , ContactClass.Eng AS ClassEng ")
        sb.AppendLine("     , ContactClass.Por AS ClassPor ")
        sb.AppendLine("     , CliGral.Guid AS ContactGuid, CliGral.FullNom AS ContactNom ")
        sb.AppendLine("     FROM DistributionChannel ")
        sb.AppendLine("     LEFT OUTER JOIN ContactClass ON DistributionChannel.Guid = ContactClass.DistributionChannel ")
        sb.AppendLine("     LEFT OUTER JOIN CliGral ON ContactClass.Guid = CliGral.ContactClass ")

        sb.AppendLine("     UNION ")
        sb.AppendLine("     SELECT NULL,NULL,NULL,NULL,NULL,NULL ")
        sb.AppendLine("     , ContactClass.Guid AS ClassGuid ")
        sb.AppendLine("     , ContactClass.Esp AS ClassEsp ")
        sb.AppendLine("     , ContactClass.Cat AS ClassCat ")
        sb.AppendLine("     , ContactClass.Eng AS ClassEng ")
        sb.AppendLine("     , ContactClass.Por AS ClassPor ")
        sb.AppendLine("     , CliGral.Guid AS ContactGuid, CliGral.FullNom AS ContactNom ")
        sb.AppendLine("     FROM ContactClass ")
        sb.AppendLine("     LEFT OUTER JOIN CliGral ON ContactClass.Guid = CliGral.ContactClass ")
        sb.AppendLine("     WHERE ContactClass.DistributionChannel IS NULL ")

        sb.AppendLine("     UNION ")
        sb.AppendLine("     SELECT NULL,NULL,NULL,NULL,NULL,NULL ")
        sb.AppendLine("     , NULL,NULL,NULL,NULL,NULL ")
        sb.AppendLine("     , CliGral.Guid AS ContactGuid, CliGral.FullNom AS ContactNom ")
        sb.AppendLine("     FROM CliGral ")
        sb.AppendLine("     WHERE CliGral.ContactClass IS NULL AND CliGral.Obsoleto = 0 ")
        sb.AppendLine("     )  X ")
        sb.AppendLine("ORDER BY X.ChannelEsp, X.ChannelGuid, X.ClassEsp, X.ClassGuid, X.ContactNom")
        Dim SQL As String = sb.ToString

        Dim oChannel As New DTODistributionChannel(Guid.Empty)
        oChannel.LangText = New DTOLangText("(diversos)")
        retval.Add(oChannel)

        Dim oClass As New DTOContactClass(Guid.Empty)
        oClass.Nom = New DTOLangText("(per classificar)", "(per classificar)", "(per classificar)", "(per classificar)")
        oChannel.ContactClasses.Add(oClass)

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("ChannelGuid")) Then
                If Not oChannel.Guid.Equals(oDrd("ChannelGuid")) Then
                    oChannel = New DTODistributionChannel(oDrd("ChannelGuid"))
                    With oChannel
                        .LangText = New DTOLangText(oDrd("ChannelEsp"), SQLHelper.GetStringFromDataReader(oDrd("ChannelCat")), SQLHelper.GetStringFromDataReader(oDrd("ChannelEng")))
                        .ContactClasses = New List(Of DTOContactClass)
                    End With
                    retval.Add(oChannel)
                End If
            End If

            If Not IsDBNull(oDrd("ClassGuid")) Then
                If Not oClass.Guid.Equals(oDrd("ClassGuid")) Then
                    oClass = New DTOContactClass(oDrd("ClassGuid"))
                    oClass.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "ClassEsp", "ClassCat", "ClassEng", "ClassPor")
                    oChannel.ContactClasses.Add(oClass)
                End If
            End If

            If Not IsDBNull(oDrd("ContactGuid")) Then
                Dim oContact As New DTOContact(oDrd("ContactGuid"))
                oContact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("ContactNom"))
                oClass.Contacts.Add(oContact)
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Headers(oLang As DTOLang) As List(Of DTODistributionChannel)
        Dim retval As New List(Of DTODistributionChannel)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM DistributionChannel ")
        sb.AppendLine("ORDER BY " & oLang.Tradueix("NomEsp", "NomCat", "NomEng") & " ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oChannel As New DTODistributionChannel(oDrd("Guid"))
            oChannel.LangText = New DTOLangText(oDrd("NomEsp"), SQLHelper.GetStringFromDataReader(oDrd("NomCat")), SQLHelper.GetStringFromDataReader(oDrd("NomEng")))
            retval.Add(oChannel)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTODistributionChannel)
        Dim retval As New List(Of DTODistributionChannel)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT DistributionChannel.Guid, DistributionChannel.NomEsp ")
        sb.AppendLine(", DistributionChannel.NomCat, DistributionChannel.NomEng, DistributionChannel.NomPor ")
        sb.AppendLine("FROM DistributionChannel ")
        Select Case oUser.Rol.Id
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                sb.AppendLine("INNER JOIN RepProducts ON DistributionChannel.Guid = RepProducts.DistributionChannel ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select
        sb.AppendLine("GROUP BY DistributionChannel.Guid, DistributionChannel.NomEsp ")
        sb.AppendLine(", DistributionChannel.NomCat, DistributionChannel.NomEng, DistributionChannel.NomPor ")
        sb.AppendLine("ORDER BY NomEsp ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oChannel As New DTODistributionChannel(oDrd("Guid"))
            oChannel.LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomEsp", "NomEsp", "NomEsp")
            retval.Add(oChannel)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oIncentiu As DTOIncentiu, oLang As DTOLang) As List(Of DTODistributionChannel)
        Dim retval As New List(Of DTODistributionChannel)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT DistributionChannel.* ")
        sb.AppendLine("FROM Incentiu_Channels ")
        sb.AppendLine("INNER JOIN DistributionChannel ON Incentiu_Channels.DistributionChannel = DistributionChannel.Guid ")
        sb.AppendLine("WHERE Incentiu_Channels.Incentiu = '" & oIncentiu.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oChannel As New DTODistributionChannel(oDrd("Guid"))
            oChannel.LangText = New DTOLangText(oDrd("NomEsp"), SQLHelper.GetStringFromDataReader(oDrd("NomCat")), SQLHelper.GetStringFromDataReader(oDrd("NomEng")))
            retval.Add(oChannel)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
