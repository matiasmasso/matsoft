Public Class SubscriptionLoader

    Shared Function Find(oWellknown As DTOSubscription.Wellknowns) As DTOSubscription
        Dim retval As DTOSubscription = Nothing
        Dim oSubscription = DTOSubscription.Wellknown(oWellknown)
        If Load(oSubscription) Then
            retval = oSubscription
        End If
        Return retval
    End Function

    Shared Function Find(oGuid As Guid) As DTOSubscription
        Dim retval As DTOSubscription = Nothing
        Dim oSubscription As New DTOSubscription(oGuid)
        If Load(oSubscription) Then
            retval = oSubscription
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSubscription As DTOSubscription) As Boolean
        If Not oSubscription.IsLoaded Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Ssc.Emp, Ssc.Reverse ")
            sb.AppendLine(", SscRol.Rol ")
            sb.AppendLine(", LangTextNom.Esp AS NomEsp, LangTextNom.Cat AS NomCat, LangTextNom.Eng AS NomEng, LangTextNom.Por AS NomPor ")
            sb.AppendLine(", LangTextDsc.Esp AS DscEsp, LangTextDsc.Cat AS DscCat, LangTextDsc.Eng AS DscEng, LangTextDsc.Por AS DscPor ")
            sb.AppendLine("FROM Ssc ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTextNom ON Ssc.Guid = LangTextNom.Guid AND LangTextNom.Src = " & DTOLangText.Srcs.SubscriptionNom & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTextDsc ON Ssc.Guid = LangTextDsc.Guid AND LangTextDsc.Src = " & DTOLangText.Srcs.SubscriptionDsc & " ")
            sb.AppendLine("LEFT OUTER JOIN SscRol ON Ssc.Guid=SscRol.SscGuid ")
            sb.AppendLine("WHERE Ssc.Guid='" & oSubscription.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oSubscription.IsLoaded Then
                    With oSubscription
                        .Emp = New DTOEmp(oDrd("Emp"))
                        SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                        SQLHelper.LoadLangTextFromDataReader(.Dsc, oDrd, "DscEsp", "DscCat", "DscEng", "DscPor")
                        .Reverse = CBool(oDrd("Reverse"))
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("Rol")) Then
                    oSubscription.Rols.Add(New DTORol(oDrd("Rol")))
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oSubscription.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSubscription As DTOSubscription, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oSubscription, oTrans)
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

    Shared Sub Update(oSubscription As DTOSubscription, ByRef oTrans As SqlTransaction)
        UpdateHeader(oSubscription, oTrans)
        LangTextLoader.Update(oSubscription.Nom, oTrans)
        LangTextLoader.Update(oSubscription.Dsc, oTrans)
        UpdateRols(oSubscription, oTrans)
    End Sub

    Shared Sub UpdateHeader(oSubscription As DTOSubscription, ByRef oTrans As SqlTransaction)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Ssc ")
        sb.AppendLine("WHERE Ssc.Guid='" & oSubscription.Guid.ToString() & "' ")
        Dim SQL = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSubscription.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSubscription
            oRow("Emp") = .Emp.Id
            oRow("Reverse") = .Reverse
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateRols(oSubscription As DTOSubscription, ByRef oTrans As SqlTransaction)
        DeleteRols(oSubscription, oTrans)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT SscRol.* ")
        sb.AppendLine("FROM SscRol ")
        sb.AppendLine("WHERE SscRol.SscGuid='" & oSubscription.Guid.ToString() & "' ")
        Dim SQL = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRol As DTORol In oSubscription.Rols
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("SscGuid") = oSubscription.Guid
            oRow("Rol") = oRol.Id
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSubscription As DTOSubscription, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSubscription, oTrans)
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
    Shared Sub Delete(oSubscription As DTOSubscription, oTrans As SqlTransaction)
        DeleteRols(oSubscription, oTrans)
        DeleteHeader(oSubscription, oTrans)
    End Sub

    Shared Sub DeleteHeader(oSubscription As DTOSubscription, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE Ssc ")
        sb.AppendLine("WHERE Ssc.Guid ='" & oSubscription.Guid.ToString() & "' ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteRols(oSubscription As DTOSubscription, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE SscRol ")
        sb.AppendLine("WHERE SscRol.SscGuid ='" & oSubscription.Guid.ToString() & "' ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Function Log(oSubscription As DTOSubscription, ByVal oUser As DTOUser, ByVal ActivateSubscription As Boolean, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Log(oTrans, oSubscription, oUser, ActivateSubscription)
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


    Shared Sub Log(oTrans As SqlTransaction, oSubscriptors As List(Of DTOSubscriptor), ByVal ActivateSubscription As Boolean)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("INSERT INTO SscLog(Mail, SscGuid, Action) ")
        sb.AppendLine("VALUES ")
        For Each item In oSubscriptors
            If item.UnEquals(oSubscriptors.First) Then sb.Append(", ")
            sb.AppendLine("('" & item.Guid.ToString & "','" & item.Subscription.Guid.ToString() & "', " & IIf(ActivateSubscription, "1", "0") & ") ")
        Next

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub Log(oTrans As SqlTransaction, oSubscription As DTOSubscription, ByVal oUser As DTOUser, ByVal ActivateSubscription As Boolean)
        Dim SQL As String = "INSERT INTO SscLog(Mail, SscGuid, Action) VALUES (@Mail, @SscGuid, @Action)"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@MAIL", oUser.Guid.ToString, "@SscGuid", oSubscription.Guid.ToString(), "@Action", IIf(ActivateSubscription, "1", "0"))
        '.WarnWebMaster("SSC LOG " & IIf(ActivateSubscription, "SI ", "NO ") & Mid.ToString & " " & oEmail.Adr)
    End Sub


End Class




Public Class SubscriptionsLoader

    Shared Function All(oUser As DTOUser) As List(Of DTOSubscription)
        Dim retval As New List(Of DTOSubscription)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ssc.Guid AS SscGuid ")
        sb.AppendLine(", LangTextNom.Esp AS NomEsp, LangTextNom.Cat AS NomCat, LangTextNom.Eng AS NomEng, LangTextNom.Por AS NomPor ")
        sb.AppendLine(", LangTextDsc.Esp AS DscEsp, LangTextDsc.Cat AS DscCat, LangTextDsc.Eng AS DscEng, LangTextDsc.Por AS DscPor ")
        sb.AppendLine(", Ssc.Reverse ")
        sb.AppendLine(", SscEmail.FchCreated ")
        sb.AppendLine("FROM Ssc ")
        sb.AppendLine("INNER JOIN SscRol ON Ssc.Guid = SscRol.SscGuid ")
        sb.AppendLine("INNER JOIN Email ON Ssc.Emp = Email.Emp AND SscRol.Rol = Email.Rol ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTextNom ON Ssc.Guid = LangTextNom.Guid AND LangTextNom.Src = " & DTOLangText.Srcs.SubscriptionNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTextDsc ON Ssc.Guid = LangTextDsc.Guid AND LangTextDsc.Src = " & DTOLangText.Srcs.SubscriptionDsc & " ")
        sb.AppendLine("LEFT OUTER JOIN SscEmail ON Email.Guid = SscEmail.Email AND Ssc.Guid = SscEmail.SscGuid ")
        sb.AppendLine("WHERE Email.Guid = '" & oUser.Guid.ToString() & "' ")
        sb.AppendLine("ORDER BY Ssc.Ord ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSsc As New DTOSubscription(oDrd("SscGuid"))
            With oSsc
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                SQLHelper.LoadLangTextFromDataReader(.Dsc, oDrd, "DcsEsp", "DscCat", "DscEng", "DscPor")
                .Reverse = oDrd("Reverse")
                .FchSubscribed = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
            End With
            retval.Add(oSsc)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, Optional oUser As DTOUser = Nothing) As List(Of DTOSubscription)
        Dim retval As New List(Of DTOSubscription)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ssc.Guid ")
        sb.AppendLine(", LangTextNom.Esp AS NomEsp, LangTextNom.Cat AS NomCat, LangTextNom.Eng AS NomEng, LangTextNom.Por AS NomPor ")
        sb.AppendLine(", LangTextDsc.Esp AS DscEsp, LangTextDsc.Cat AS DscCat, LangTextDsc.Eng AS DscEng, LangTextDsc.Por AS DscPor ")
        sb.AppendLine(", Ssc.Reverse ")
        sb.AppendLine("FROM Ssc ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTextNom ON Ssc.Guid = LangTextNom.Guid AND LangTextNom.Src = " & DTOLangText.Srcs.SubscriptionNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTextDsc ON Ssc.Guid = LangTextDsc.Guid AND LangTextDsc.Src = " & DTOLangText.Srcs.SubscriptionDsc & " ")
        If oUser Is Nothing Then
            sb.AppendLine("WHERE Ssc.Emp=" & oEmp.Id & " ")
        Else
            sb.AppendLine("INNER JOIN (SELECT SscGuid FROM SscEmail WHERE SscEmail.Email = '" & oUser.Guid.ToString & "' GROUP BY SscGuid ) UsrSscs ")
            sb.AppendLine("ON Ssc.Guid= UsrSscs.SscGuid ")
        End If
        sb.AppendLine("ORDER BY Ssc.Ord ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim item As New DTOSubscription(oGuid)
            With item
                .Emp = oEmp
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                SQLHelper.LoadLangTextFromDataReader(.Dsc, oDrd, "DscEsp", "DscCat", "DscEng", "DscPor")
                .Reverse = CBool(oDrd("Reverse"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Public Shared Function All(oEmp As DTOEmp, oRol As DTORol) As List(Of DTOSubscription)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ssc.Guid ")
        sb.AppendLine(", LangTextNom.Esp AS NomEsp, LangTextNom.Cat AS NomCat, LangTextNom.Eng AS NomEng, LangTextNom.Por AS NomPor ")
        sb.AppendLine(", LangTextDsc.Esp AS DscEsp, LangTextDsc.Cat AS DscCat, LangTextDsc.Eng AS DscEng, LangTextDsc.Por AS DscPor ")
        sb.AppendLine(", Ssc.Reverse, SscRol.Rol ")
        sb.AppendLine("FROM Ssc ")
        sb.AppendLine("INNER JOIN SscRol ON Ssc.Guid = SscRol.SscGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTextNom ON Ssc.Guid = LangTextNom.Guid AND LangTextNom.Src = " & DTOLangText.Srcs.SubscriptionNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTextDsc ON Ssc.Guid = LangTextDsc.Guid AND LangTextDsc.Src = " & DTOLangText.Srcs.SubscriptionDsc & " ")
        sb.AppendLine("WHERE Ssc.Emp =" & oEmp.Id & " AND SscRol.Rol=" & CInt(oRol.id) & " ")
        sb.AppendLine("ORDER BY Ssc.Ord ")
        Dim SQL As String = sb.ToString


        Dim oRetVal As New List(Of DTOSubscription)
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSubscription(CType(oDrd("Guid"), Guid))
            With item
                .Emp = oEmp
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                SQLHelper.LoadLangTextFromDataReader(.Dsc, oDrd, "DscEsp", "DscCat", "DscEng", "DscPor")
                .Reverse = CBool(oDrd("Reverse"))
            End With
            oRetVal.Add(item)
        Loop
        oDrd.Close()
        Return oRetVal
    End Function


    Shared Function Update(oUser As DTOUser, ByVal oNewSscs As List(Of DTOSubscription), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oPreviousSscs As List(Of DTOSubscription) = All(oUser.Emp, oUser)

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Try
            Dim SQL As String = "DELETE SscEmail WHERE Email = '" & oUser.Guid.ToString & "' "
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            SQL = "SELECT * FROM SscEmail WHERE Email = '" & oUser.Guid.ToString & "' "
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow

            For Each item As DTOSubscription In oNewSscs
                oRow = oTb.NewRow
                oRow("SscGuid") = item.Guid
                oRow("Email") = oUser.Guid
                oTb.Rows.Add(oRow)
            Next

            oDA.Update(oDs)

            'registra les altes
            For Each oNewSsc As DTOSubscription In oNewSscs
                Dim BlFound As Boolean = False
                For Each oPreviousSsc As DTOSubscription In oPreviousSscs
                    If oNewSsc.Guid.Equals(oPreviousSsc.Guid) Then
                        BlFound = True
                        Exit For
                    End If
                Next

                If Not BlFound Then SubscriptionLoader.Log(oTrans, oNewSsc, oUser, Not oNewSsc.Reverse)
            Next

            'registra les baixes
            For Each oPreviousSsc As DTOSubscription In oPreviousSscs
                Dim BlFound As Boolean = False
                For Each oNewSsc As DTOSubscription In oNewSscs
                    If oNewSsc.Guid.Equals(oPreviousSsc.Guid) Then
                        BlFound = True
                        Exit For
                    End If
                Next

                If Not BlFound Then
                    SubscriptionLoader.Log(oTrans, oPreviousSsc, oUser, oPreviousSsc.Reverse)
                End If
            Next

            oTrans.Commit()
            retval = True

        Catch e As SqlException
            oTrans.Rollback()
            exs.Add(e)

        Finally
            oConn.Close()
            oConn = Nothing
        End Try

        Return retval
    End Function

End Class



Public Class SubscriptorsLoader

    Shared Function Recipients(oSubscription As DTOSubscription, Optional oContact As DTOContact = Nothing) As List(Of String)
        Dim retval As New List(Of String)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT Email.Adr ")
        sb.Append("FROM SscEmail ")
        sb.Append("INNER JOIN Email on SscEmail.Email = Email.Guid ")
        If oContact IsNot Nothing Then
            sb.Append("INNER JOIN Email_Clis on Email.Guid = Email_Clis.EmailGuid ")
        End If
        sb.Append("WHERE SscEmail.SscGuid = '" & oSubscription.Guid.ToString & "' ")
        sb.Append("AND Email.Obsoleto = 0 AND Email.BadMailGuid IS NULL ")
        If oContact IsNot Nothing Then
            sb.Append("AND Email_Clis.ContactGuid ='" & oContact.Guid.ToString & "' ")
        End If
        sb.Append("GROUP BY Email.Adr ")
        sb.Append("ORDER BY Email.Adr")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Adr"))
        Loop
        oDrd.Close()
        Return retval
    End Function



    Shared Function All(oSubscription As DTOSubscription) As List(Of DTOSubscriptor)
        Dim retval As New List(Of DTOSubscriptor)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT Email.Guid, Email.Rol, Email.Adr, Email.FchCreated, Email.Lang, Email.BadMailGuid, Email.Obsoleto ")
        sb.Append(", Email_Clis.ContactGuid, CliGral.FullNom, Email.DefaultContactGuid ")
        sb.Append("FROM SscEmail ")
        sb.Append("INNER JOIN Email on SscEmail.Email = Email.Guid ")
        sb.Append("LEFT OUTER JOIN Email_Clis on SscEmail.email=Email_Clis.EmailGuid ")
        sb.Append("LEFT OUTER JOIN CliGral on Email_Clis.ContactGuid = CliGral.Guid ")
        sb.Append("WHERE SscEmail.SscGuid = '" & oSubscription.Guid.ToString() & "' ")
        sb.Append("ORDER BY Email.Adr, SscEmail.Email, Email_Clis.ContactGuid")
        Dim SQL As String = sb.ToString
        Dim oSubscriptor As New DTOSubscriptor(Guid.NewGuid, oSubscription)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            If Not oSubscriptor.Guid.Equals(oDrd("Guid")) Then

                oSubscriptor = New DTOSubscriptor(oDrd("Guid"), oSubscription)
                With oSubscriptor
                    .emailAddress = oDrd("Adr")
                    .rol = New DTORol(CType(oDrd("Rol"), DTORol.Ids))
                    .lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                    If Not IsDBNull(oDrd("BadMailGuid")) Then .BadMail = New DTOCod(oDrd("BadMailGuid"))
                    .obsoleto = oDrd("Obsoleto")
                    .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
                    .contacts = New List(Of DTOContact)
                End With
                retval.Add(oSubscriptor)
            End If
            If Not IsDBNull(oDrd("ContactGuid")) Then
                Dim oContact As New DTOContact(oDrd("ContactGuid"))
                oContact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                If oContact.Guid.Equals(oDrd("DefaultContactGuid")) Then oSubscriptor.contact = oContact
                oSubscriptor.contacts.Add(oContact)
            End If


        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(oSubscription As DTOSubscription, oContact As DTOContact) As List(Of DTOSubscriptor)
        Dim retval As New List(Of DTOSubscriptor)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT Email.Guid, Email.Rol, Email.Adr, Email.FchCreated, Email.Lang, Email.BadMailGuid, Email.Obsoleto ")
        sb.Append("FROM SscEmail ")
        sb.Append("inner join Email_Clis on SscEmail.email=Email_Clis.EmailGuid ")
        sb.Append("inner join email on Email_Clis.Emailguid=Email.Guid ")
        sb.Append("WHERE SscEmail.SscGuid = '" & oSubscription.Guid.ToString & "' ")
        If oContact IsNot Nothing Then
            sb.Append("AND Email_Clis.ContactGuid = '" & oContact.Guid.ToString & "' ")
        End If
        sb.Append("GROUP BY Email.Guid, Email.Rol, Email.Adr, Email.FchCreated, Email.Lang, Email.BadMailGuid, Email.Obsoleto  ")
        sb.Append("ORDER BY Email.FchCreated DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSubscriptor As New DTOSubscriptor(oDrd("Guid"), oSubscription)
            With oSubscriptor
                .EmailAddress = oDrd("Adr")
                .rol = New DTORol(oDrd("Rol"))
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                If Not IsDBNull(oDrd("BadMailGuid")) Then .BadMail = New DTOCod(oDrd("BadMailGuid"))
                .Obsoleto = oDrd("Obsoleto")
                .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
            End With
            retval.Add(oSubscriptor)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function AllWithManufacturer(oSubscription As DTOSubscription) As List(Of DTOSubscriptor)
        Dim retval As New List(Of DTOSubscriptor)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT Email.Guid, Email.Adr, Email.Lang ")
        sb.Append(", Tpa.Proveidor, CliGral.RaoSocial ")
        sb.Append("FROM SscEmail ")
        sb.Append("INNER JOIN Email_Clis ON SscEmail.email=Email_Clis.EmailGuid ")
        sb.Append("INNER JOIN email ON Email_Clis.Emailguid=Email.Guid ")
        sb.Append("INNER JOIN Tpa ON Email_Clis.ContactGuid=Tpa.Proveidor ")
        sb.Append("INNER JOIN CliGral ON Email_Clis.ContactGuid=CliGral.Guid ")
        sb.Append("WHERE SscEmail.SscGuid = '" & oSubscription.Guid.ToString & "' ")
        sb.Append("GROUP BY Email.Guid, Email.Adr, Email.Lang, Tpa.Proveidor, CliGral.RaoSocial ")
        sb.Append("ORDER BY CliGral.RaoSocial, Tpa.Proveidor, Email.Adr ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSubscriptor As New DTOSubscriptor(DirectCast(oDrd("Guid"), Guid), oSubscription)
            With oSubscriptor
                .emp = oSubscription.Emp
                .emailAddress = oDrd("Adr")
                .lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                .contact = New DTOProveidor(oDrd("Proveidor"))
                .contact.emp = .emp
                .contact.nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
            End With
            retval.Add(oSubscriptor)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function IsSubscribed(oSubscriptor As DTOSubscriptor) As Boolean
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT * FROM SscEmail ")
        sb.AppendLine("WHERE SscGuid = '" & oSubscriptor.Subscription.Guid.ToString() & "' ")
        sb.AppendLine("AND Email = '" & oSubscriptor.Guid.ToString & "' ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function

    Shared Function Subscribe(oSubscriptors As List(Of DTOSubscriptor), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            SubscriptionLoader.Log(oTrans, oSubscriptors, True)
            For Each item In oSubscriptors
                Dim sb As New Text.StringBuilder
                sb.AppendLine("SELECT * FROM SscEmail ")
                sb.AppendLine("WHERE SscGuid = '" & item.Subscription.Guid.ToString() & "' ")
                sb.AppendLine("AND Email = '" & item.Guid.ToString & "' ")
                Dim SQL As String = sb.ToString
                Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
                Dim oDs As New DataSet
                oDA.Fill(oDs)
                Dim oTb As DataTable = oDs.Tables(0)
                If oTb.Rows.Count = 0 Then
                    Dim oRow As DataRow = oTb.NewRow
                    oTb.Rows.Add(oRow)
                    'oRow("Emp") = item.Subscription.Emp.Id
                    oRow("SscGuid") = item.Subscription.Guid
                    oRow("Email") = item.Guid.ToString
                End If
                oDA.Update(oDs)
            Next

            oTrans.Commit()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
            oTrans.Rollback()
        Finally
            oConn.Close()
        End Try
        Return retval
    End Function

    Shared Function Unsubscribe(oSubscriptors As List(Of DTOSubscriptor), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            SubscriptionLoader.Log(oTrans, oSubscriptors, False)

            Dim sb As New Text.StringBuilder
            sb.AppendLine("DELETE SscEmail ")
            sb.AppendLine("WHERE (")
            For Each item In oSubscriptors
                If item.UnEquals(oSubscriptors.First) Then sb.Append("OR ")
                sb.AppendLine("(SscGuid = '" & item.Subscription.Guid.ToString & "' AND Email = '" & item.Guid.ToString & "') ")
            Next
            sb.AppendLine(")")
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            oTrans.Commit()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
            oTrans.Rollback()
        Finally
            oConn.Close()
        End Try
        Return retval
    End Function
End Class


