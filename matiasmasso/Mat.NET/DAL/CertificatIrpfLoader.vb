Public Class CertificatIrpfLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCertificatIrpf
        Dim retval As DTOCertificatIrpf = Nothing
        Dim oCertificatIrpf As New DTOCertificatIrpf(oGuid)
        If Load(oCertificatIrpf) Then
            retval = oCertificatIrpf
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCertificatIrpf As DTOCertificatIrpf) As Boolean
        If Not oCertificatIrpf.IsLoaded And Not oCertificatIrpf.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CertificatIrpf.* ")
            sb.AppendLine(", CliGral.FullNom ")
            sb.AppendLine("FROM CertificatIrpf ")
            sb.AppendLine("INNER JOIN CliGral ON CertificatIrpf.Contact = CliGral.Guid ")
            sb.AppendLine("WHERE CertificatIrpf.Guid='" & oCertificatIrpf.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oCertificatIrpf
                    .Contact = New DTOContact(oDrd("Contact"))
                    .Contact.FullNom = oDrd("FullNom")
                    .Year = oDrd("Year")
                    .Period = oDrd("Period")
                    .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCertificatIrpf.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCertificatIrpf As DTOCertificatIrpf, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            DocFileLoader.Update(oCertificatIrpf.DocFile, oTrans)
            Update(oCertificatIrpf, oTrans)
            oTrans.Commit()
            oCertificatIrpf.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oCertificatIrpf As DTOCertificatIrpf, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CertificatIrpf ")
        sb.AppendLine("WHERE Guid='" & oCertificatIrpf.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCertificatIrpf.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCertificatIrpf
            oRow("Contact") = SQLHelper.NullableBaseGuid(.Contact)
            oRow("Year") = .Year
            oRow("Period") = .Period
            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCertificatIrpf As DTOCertificatIrpf, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            DocFileLoader.DeleteIfOrphan(oCertificatIrpf.DocFile, oTrans)
            Delete(oCertificatIrpf, oTrans)
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


    Shared Sub Delete(oCertificatIrpf As DTOCertificatIrpf, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CertificatIrpf WHERE Guid='" & oCertificatIrpf.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class CertificatIrpfsLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOCertificatIrpf)
        Dim retval As New List(Of DTOCertificatIrpf)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CertificatIrpf.* ")
        sb.AppendLine(", CliGral.RaoSocial ")
        sb.AppendLine(", DocFile.Hash AS DocfileHash, Docfile.Mime AS DocfileMime, Docfile.Nom AS DocfileNom, Docfile.Size AS DocfileSize ")
        sb.AppendLine(", Docfile.Width AS DocfileWidth, Docfile.Height AS DocfileHeight ")
        sb.AppendLine("FROM CertificatIrpf ")
        sb.AppendLine("INNER JOIN CliGral ON CertificatIrpf.Contact = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Docfile ON CertificatIrpf.Hash = Docfile.Hash ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY CertificatIrpf.Year DESC, CertificatIrpf.Period DESC, CliGral.RaoSocial")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCertificatIrpf(oDrd("Guid"))
            With item
                .Contact = New DTOContact(oDrd("Contact"))
                .Contact.nom = oDrd("RaoSocial")
                .Year = oDrd("Year")
                .Period = oDrd("Period")
                .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTOCertificatIrpf)
        Dim retval As New List(Of DTOCertificatIrpf)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CertificatIrpf.*, CliGral.FullNom ")
        sb.AppendLine(", DocFile.Hash AS DocfileHash, Docfile.Mime AS DocfileMime, Docfile.Nom AS DocfileNom, Docfile.Size AS DocfileSize ")
        sb.AppendLine(", Docfile.Width AS DocfileWidth, Docfile.Height AS DocfileHeight ")
        sb.AppendLine("FROM CertificatIrpf ")
        sb.AppendLine("INNER JOIN Docfile ON CertificatIrpf.Hash = Docfile.Hash ")
        sb.AppendLine("INNER JOIN CliGral ON CertificatIrpf.Contact = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON CertificatIrpf.Contact = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid = Email.Guid ")
        sb.AppendLine("WHERE Email.Adr = '" & oUser.EmailAddress & "' ")
        sb.AppendLine("ORDER BY CertificatIrpf.Year DESC, CertificatIrpf.Period DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCertificatIrpf(oDrd("Guid"))
            With item
                .Contact = New DTOContact(oDrd("Contact"))
                .Contact.FullNom = oDrd("FullNom")
                .Year = oDrd("Year")
                .Period = oDrd("Period")
                .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(oContact As DTOContact) As List(Of DTOCertificatIrpf)
        Dim retval As New List(Of DTOCertificatIrpf)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CertificatIrpf.* ")
        sb.AppendLine(", DocFile.Hash AS DocfileHash, Docfile.Mime AS DocfileMime, Docfile.Nom AS DocfileNom, Docfile.Size AS DocfileSize ")
        sb.AppendLine(", Docfile.Width AS DocfileWidth, Docfile.Height AS DocfileHeight ")
        sb.AppendLine("FROM CertificatIrpf ")
        sb.AppendLine("INNER JOIN Docfile ON CertificatIrpf.Hash = Docfile.Hash ")
        sb.AppendLine("WHERE CertificatIrpf.Contact = '" & oContact.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY CertificatIrpf.Year DESC, CertificatIrpf.Period DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCertificatIrpf(oDrd("Guid"))
            With item
                .Contact = oContact
                .Year = oDrd("Year")
                .Period = oDrd("Period")
                .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
