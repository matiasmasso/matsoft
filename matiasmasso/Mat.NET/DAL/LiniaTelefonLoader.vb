Public Class LiniaTelefonLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOLiniaTelefon
        Dim retval As DTOLiniaTelefon = Nothing
        Dim oLiniaTelefon As New DTOLiniaTelefon(oGuid)
        If Load(oLiniaTelefon) Then
            retval = oLiniaTelefon
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oLiniaTelefon As DTOLiniaTelefon) As Boolean
        If Not oLiniaTelefon.IsLoaded And Not oLiniaTelefon.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT LiniaTelefon.*, CliGral.FullNom ")
            sb.AppendLine("FROM LiniaTelefon ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON LiniaTelefon.Contact=CliGral.Guid ")
            sb.AppendLine("WHERE LiniaTelefon.Guid='" & oLiniaTelefon.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oLiniaTelefon
                    .Num = oDrd("Num")
                    .Alias = SQLHelper.GetStringFromDataReader(oDrd("Alias"))
                    .Icc = SQLHelper.GetStringFromDataReader(oDrd("Icc"))
                    .Imei = SQLHelper.GetStringFromDataReader(oDrd("Imei"))
                    .Puk = SQLHelper.GetStringFromDataReader(oDrd("Puk"))
                    .Alta = SQLHelper.GetFchFromDataReader(oDrd("Alta"))
                    .Baixa = SQLHelper.GetFchFromDataReader(oDrd("Baixa"))
                    .Privat = SQLHelper.GetBooleanFromDatareader(oDrd("Privat"))
                    If Not IsDBNull(oDrd("Contact")) Then
                        .Contact = New DTOContact(oDrd("Contact"))
                        .contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oLiniaTelefon.IsLoaded
        Return retval
    End Function

    Shared Function Update(oLiniaTelefon As DTOLiniaTelefon, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oLiniaTelefon, oTrans)
            oTrans.Commit()
            oLiniaTelefon.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oLiniaTelefon As DTOLiniaTelefon, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM LiniaTelefon ")
        sb.AppendLine("WHERE Guid='" & oLiniaTelefon.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oLiniaTelefon.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oLiniaTelefon
            oRow("Num") = .Num
            oRow("Contact") = SQLHelper.NullableBaseGuid(.Contact)
            oRow("ICC") = SQLHelper.NullableString(.Icc)
            oRow("Imei") = SQLHelper.NullableString(.Imei)
            oRow("Puk") = SQLHelper.NullableString(.Puk)
            oRow("Alias") = SQLHelper.NullableString(.Alias)
            oRow("Alta") = SQLHelper.NullableFch(.Alta)
            oRow("Baixa") = SQLHelper.NullableFch(.Baixa)
            oRow("Privat") = .Privat
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oLiniaTelefon As DTOLiniaTelefon, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oLiniaTelefon, oTrans)
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


    Shared Sub Delete(oLiniaTelefon As DTOLiniaTelefon, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE LiniaTelefon WHERE Guid='" & oLiniaTelefon.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class LiniaTelefonsLoader

    Shared Function All() As List(Of DTOLiniaTelefon)
        Dim retval As New List(Of DTOLiniaTelefon)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT LiniaTelefon.*, CliGral.FullNom ")
        sb.AppendLine("FROM LiniaTelefon ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON LiniaTelefon.Contact=CliGral.Guid ")
        sb.AppendLine("ORDER BY Num")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOLiniaTelefon(oDrd("Guid"))
            With item
                .Num = oDrd("Num")
                .Alias = SQLHelper.GetStringFromDataReader(oDrd("Alias"))
                .Alta = SQLHelper.GetFchFromDataReader(oDrd("Alta"))
                .Baixa = SQLHelper.GetFchFromDataReader(oDrd("Baixa"))
                .Privat = SQLHelper.GetBooleanFromDatareader(oDrd("Privat"))
                If Not IsDBNull(oDrd("Contact")) Then
                    .Contact = New DTOContact(oDrd("Contact"))
                    .contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
