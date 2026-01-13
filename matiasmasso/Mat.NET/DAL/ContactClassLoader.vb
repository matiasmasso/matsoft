Public Class ContactClassLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOContactClass
        Dim retval As DTOContactClass = Nothing
        Dim oContactClass As New DTOContactClass(oGuid)
        If Load(oContactClass) Then
            retval = oContactClass
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oContactClass As DTOContactClass) As Boolean
        If Not oContactClass.IsLoaded And Not oContactClass.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ContactClass.Guid, ContactClass.DistributionChannel, ContactClass.SalePoint, ContactClass.Raffles, ContactClass.Ord ")
            sb.AppendLine(", ContactClass.Esp, ContactClass.Cat, ContactClass.Eng, ContactClass.Por ")
            sb.AppendLine(", DistributionChannel.NomEsp AS ChannelEsp ")
            sb.AppendLine(", DistributionChannel.NomCat AS ChannelCat ")
            sb.AppendLine(", DistributionChannel.NomEng AS ChannelEng ")
            sb.AppendLine(", DistributionChannel.NomPor AS ChannelPor ")
            sb.AppendLine(", DistributionChannel.Ord AS ChannelOrd ")
            sb.AppendLine("FROM ContactClass ")
            sb.AppendLine("INNER JOIN DistributionChannel ON ContactClass.DistributionChannel = DistributionChannel.Guid ")
            sb.AppendLine("WHERE ContactClass.Guid='" & oContactClass.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oContactClass
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng", "Por")
                    .SalePoint = oDrd("SalePoint")
                    .Raffles = oDrd("Raffles")
                    .Ord = oDrd("Ord")
                    If Not IsDBNull(oDrd("DistributionChannel")) Then
                        .DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                        .DistributionChannel.Ord = oDrd("ChannelOrd")
                        .DistributionChannel.LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ChannelEsp", "ChannelCat", "ChannelEng", "ChannelPor")
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oContactClass.IsLoaded
        Return retval
    End Function

    Shared Function Update(oContactClass As DTOContactClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oContactClass, oTrans)
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


    Shared Sub Update(oContactClass As DTOContactClass, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ContactClass ")
        sb.AppendLine("WHERE Guid='" & oContactClass.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oContactClass.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oContactClass
            oRow("Esp") = SQLHelper.NullableLangText(.Nom, DTOLang.ESP)
            oRow("Cat") = SQLHelper.NullableLangText(.Nom, DTOLang.CAT)
            oRow("Eng") = SQLHelper.NullableLangText(.Nom, DTOLang.ENG)
            oRow("Por") = SQLHelper.NullableLangText(.Nom, DTOLang.POR)
            oRow("SalePoint") = .SalePoint
            oRow("Raffles") = .Raffles
            oRow("Ord") = .Ord
            oRow("DistributionChannel") = SQLHelper.NullableBaseGuid(.DistributionChannel)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oContactClass As DTOContactClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oContactClass, oTrans)
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


    Shared Sub Delete(oContactClass As DTOContactClass, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ContactClass WHERE Guid='" & oContactClass.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class ContactClassesLoader

    Shared Function All() As List(Of DTOContactClass)
        Dim retval As New List(Of DTOContactClass)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ContactClass.Guid, ContactClass.DistributionChannel, ContactClass.SalePoint, ContactClass.Raffles, ContactClass.Ord ")
        sb.AppendLine(", ContactClass.Esp, ContactClass.Cat, ContactClass.Eng, ContactClass.Por ")
        sb.AppendLine(", DistributionChannel.NomEsp AS ChannelEsp ")
        sb.AppendLine(", DistributionChannel.NomCat AS ChannelCat ")
        sb.AppendLine(", DistributionChannel.NomEng AS ChannelEng ")
        sb.AppendLine(", DistributionChannel.NomPor AS ChannelPor ")
        sb.AppendLine("FROM ContactClass ")
        sb.AppendLine("LEFT OUTER JOIN DistributionChannel ON ContactClass.DistributionChannel = DistributionChannel.Guid ")
        sb.AppendLine("ORDER BY ContactClass.Ord, ContactClass.Esp")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOContactClass(oDrd("Guid"))
            With item
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng", "Por")
                .SalePoint = oDrd("SalePoint")
                .Raffles = oDrd("Raffles")
                .Ord = oDrd("Ord")
                If Not IsDBNull(oDrd("DistributionChannel")) Then
                    .DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                    .DistributionChannel.LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ChannelEsp", "ChannelCat", "ChannelEng", "ChannelPor")
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function AllWithContacts(oEmp As DTOEmp) As List(Of DTOContactClass)
        Dim retval As New List(Of DTOContactClass)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ContactClass.Guid, ContactClass.DistributionChannel, ContactClass.SalePoint, ContactClass.Raffles, ContactClass.Ord ")
        sb.AppendLine(", ContactClass.Esp, ContactClass.Cat, ContactClass.Eng, ContactClass.Por ")
        sb.AppendLine(", CliGral.Guid AS ContactGuid, CliGral.FullNom ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("LEFT OUTER JOIN ContactClass ON CliGral.ContactClass = ContactClass.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND CliGral.Obsoleto = 0 ")
        sb.AppendLine("ORDER BY ContactClass.Esp, CliGral.FullNom ")
        Dim SQL As String = sb.ToString

        Dim oClass As New DTOContactClass
        oClass.Nom = New DTOLangText("(por clasificar)", "(per classificar)", "(class pending)", "(por clasificar)")
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If IsDBNull(oDrd("Guid")) Then
                If oClass.Contacts.Count = 0 Then retval.Add(oClass)
            ElseIf Not oClass.Guid.Equals(oDrd("Guid")) Then
                oClass = New DTOContactClass(oDrd("Guid"))
                With oClass
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng", "Por")
                    .SalePoint = oDrd("SalePoint")
                    .Raffles = oDrd("Raffles")
                    .Ord = oDrd("Ord")
                End With
                retval.Add(oClass)
            End If
            Dim oContact As New DTOContact(oDrd("ContactGuid"))
            oContact.Emp = oEmp
            oContact.FullNom = oDrd("FullNom")
            oContact.ContactClass = oClass
            oClass.Contacts.Add(oContact)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
