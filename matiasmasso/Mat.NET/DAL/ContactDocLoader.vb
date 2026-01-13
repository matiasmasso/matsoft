Public Class ContactDocLoader


    Shared Function Find(oGuid As Guid) As DTOContactDoc
        Dim retval As DTOContactDoc = Nothing
        Dim oContactDoc As New DTOContactDoc(oGuid)
        If Load(oContactDoc) Then
            retval = oContactDoc
        End If
        Return retval
    End Function

    Shared Function Load(ByRef value As DTOContactDoc) As Boolean
        If Not value.IsLoaded And Not value.IsNew Then

            Dim SQL As String = "SELECT CliDoc.Guid, CliDoc.Contact, CliDoc.Fch, CliDoc.Type, CliDoc.Ref, CliDoc.Obsoleto, CliDoc.Hash, CliGral.FullNom FROM CliDoc INNER JOIN CliGral ON CliDoc.Contact = CliGral.Guid WHERE CliDoc.Guid='" & value.Guid.ToString() & "'"
            Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With value
                    .Contact = New DTOContact(DirectCast(oDrd("Contact"), Guid))
                    .Contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Type = oDrd("Type")
                    .Fch = oDrd("Fch")
                    .Ref = oDrd("Ref")
                    .Obsoleto = oDrd("Obsoleto")
                    If Not IsDBNull(oDrd("Hash")) Then
                        .DocFile = New DTODocFile(oDrd("Hash").ToString())
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = value.IsLoaded
        Return retval
    End Function

    Shared Function Update(oContactDoc As DTOContactDoc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oContactDoc, oTrans)
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

    Shared Sub Update(oContactDoc As DTOContactDoc, oTrans As SqlTransaction)
        DocFileLoader.Update(oContactDoc.DocFile, oTrans)
        UpdateHeader(oContactDoc, oTrans)
    End Sub

    Shared Sub UpdateHeader(oContactDoc As DTOContactDoc, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM CliDoc WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oContactDoc.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oContactDoc.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oContactDoc
            oRow("Contact") = .Contact.Guid
            oRow("Type") = .Type
            oRow("Fch") = .Fch
            oRow("Ref") = .Ref
            oRow("Obsoleto") = .Obsoleto

            oRow("Hash") = SQLHelper.NullableDocFile(oContactDoc.DocFile)
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oContactDoc As DTOContactDoc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oContactDoc, oTrans)
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

    Shared Sub Delete(oContactDoc As DTOContactDoc, ByRef oTrans As SqlTransaction)
        DocFileLoader.Delete(oContactDoc.DocFile, oTrans)
        DeleteHeader(oContactDoc, oTrans)
    End Sub

    Shared Sub DeleteHeader(oContactDoc As DTOContactDoc, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliDoc WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@GUID", oContactDoc.Guid.ToString())
    End Sub

End Class


Public Class ContactDocsLoader
    Shared Function All(oContact As DTOContact, Optional iYea As Integer = 0, Optional oType As DTOContactDoc.Types = DTOContactDoc.Types.NotSet) As List(Of DTOContactDoc)
        Dim retval As New List(Of DTOContactDoc)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CliDoc.* ")
        sb.AppendLine(", VwDocfile.* ")
        sb.AppendLine("FROM CliDoc ")
        sb.AppendLine("LEFT OUTER JOIN VwDocfile ON CliDoc.Hash = VwDocfile.DocfileHash ")
        sb.AppendLine("WHERE CliDoc.Contact = '" & oContact.Guid.ToString & "' ")
        If oType <> DTOContactDoc.Types.NotSet Then
            sb.AppendLine("AND CliDoc.Type = " & CInt(oType).ToString & " ")
        End If
        If iYea > 0 Then
            sb.AppendLine("AND Year(CliDoc.Fch) = " & iYea.ToString & " ")
        End If
        sb.AppendLine("ORDER BY CliDoc.Obsoleto, CliDoc.Fch Desc, CliDoc.Ref")
        Dim SQL = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oContactDoc As New DTOContactDoc(oDrd("Guid"))
            With oContactDoc
                .Contact = oContact
                .Type = oDrd("Type")
                .Fch = oDrd("Fch")
                .Ref = oDrd("Ref")
                .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                .Obsoleto = oDrd("Obsoleto")
                .IsLoaded = True

            End With
            retval.Add(oContactDoc)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oUser As DTOUser, Optional oType As DTOContactDoc.Types = DTOContactDoc.Types.NotSet) As List(Of DTOContactDoc)
        Dim retval As New List(Of DTOContactDoc)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CliDoc.* ")
        sb.AppendLine(", VwDocfile.* ")
        sb.AppendLine("FROM CliDoc ")
        sb.AppendLine("LEFT OUTER JOIN VwDocfile ON CliDoc.Hash = VwDocfile.DocfileHash ")
        sb.AppendLine("INNER JOIN Email_Clis ON CliDoc.Contact = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString() & "' ")
        sb.AppendLine("WHERE CliDoc.Type = " & CInt(oType).ToString & " ")
        sb.AppendLine("ORDER BY CliDoc.Obsoleto, CliDoc.Fch Desc, CliDoc.Ref")
        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oContactDoc As New DTOContactDoc(oDrd("Guid"))
            With oContactDoc
                .Type = oDrd("Type")
                .Fch = oDrd("Fch")
                .Ref = oDrd("Ref")
                .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                .Obsoleto = oDrd("Obsoleto")
                .IsLoaded = True
            End With
            retval.Add(oContactDoc)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Mod145s(oExercici As DTOExercici) As List(Of DTOContactDoc)
        Dim retval As New List(Of DTOContactDoc)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CliDoc.Guid, CliDoc.Contact, CliGral.RaoSocial, CliDoc.Fch, CliDoc.Hash ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine("FROM (SELECT Contact, MAX(Fch)AS LastFch, CliDoc.Type FROM CliDoc ")
        sb.AppendLine("WHERE CliDoc.Type=8 ")
        sb.AppendLine("AND Year(CliDoc.Fch)<='" & oExercici.Year & "' ")
        sb.AppendLine("GROUP BY Contact, Type) X ")
        sb.AppendLine("INNER JOIN CliDoc ON X.Contact=CliDoc.Contact AND X.LastFch=CliDoc.Fch AND CliDoc.Type=X.Type ")
        sb.AppendLine("INNER JOIN CliGral ON CliDoc.Contact=CliGral.Guid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOContactDoc(oDrd("Guid"))
            With item
                .Contact = New DTOContact(oDrd("Contact"))
                With .Contact
                    .Nom = oDrd("RaoSocial")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                End With
                .Fch = oDrd("Fch")
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Retencions(oContacts As List(Of DTOContact)) As List(Of DTOContactDoc)
        Dim retval As New List(Of DTOContactDoc)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CliDoc.Guid, CliDoc.Hash, CliDoc.Fch, CliDoc.Contact FROM CliDoc ")
        sb.AppendLine("WHERE CliDoc.Type = " & CInt(DTOContactDoc.Types.Retencions).ToString & " AND (")
        For Each oContact As DTOContact In oContacts
            If oContact.UnEquals(oContacts.First) Then sb.AppendLine("OR ")
            sb.AppendLine("CliDoc.Contact='" & oContact.Guid.ToString & "' ")
        Next
        sb.AppendLine(") ")
        sb.AppendLine("ORDER BY Fch DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("Hash")) Then
                Dim oContactDoc As New DTOContactDoc(DirectCast(oDrd("Guid"), Guid))
                With oContactDoc
                    .Contact = New DTOContact(oDrd("Contact"))
                    .Type = DTOContactDoc.Types.Retencions
                    .Fch = oDrd("Fch")
                    .DocFile = New DTODocFile(oDrd("Hash").ToString())
                End With
                retval.Add(oContactDoc)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

