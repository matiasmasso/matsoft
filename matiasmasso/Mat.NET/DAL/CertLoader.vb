Public Class CertLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCert
        Dim retval As DTOCert = Nothing
        Dim oCert As New DTOCert(oGuid)
        If Load(oCert) Then
            retval = oCert
        End If
        Return retval
    End Function


    Shared Function Load(ByRef oCert As DTOCert) As Boolean
        If Not oCert.IsLoaded And Not oCert.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Pwd, Ext, Caduca, Stream ")
            sb.AppendLine(", (CASE WHEN Image IS NULL THEN 0 ELSE 1 END) AS ImageExists ")
            sb.AppendLine("FROM CliCert ")
            sb.AppendLine("WHERE Guid='" & oCert.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oCert
                    .Stream = oDrd("Stream")
                    .Pwd = oDrd("Pwd").ToString
                    .Ext = oDrd("Ext").ToString.ToUpper
                    .Caduca = oDrd("Caduca")
                    If oDrd("ImageExists") = 1 Then
                        .ImageUri = New Uri(MmoUrl.ApiUrl("Cert/Image", oCert.Guid.ToString()))
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCert.IsLoaded
        Return retval
    End Function

    Shared Function Image(ByRef oCert As DTOCert) As Byte()
        Dim retval As Byte() = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Image ")
        sb.AppendLine("FROM CliCert ")
        sb.AppendLine("WHERE Guid='" & oCert.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = oDrd("Image")
        End If

        oDrd.Close()

        Return retval
    End Function

    Shared Function Update(oCert As DTOCert, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCert, oTrans)
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


    Shared Sub Update(oCert As DTOCert, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliCert ")
        sb.AppendLine("WHERE Guid='" & oCert.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCert.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCert
            oRow("Stream") = .Stream
            oRow("Image") = .Image
            oRow("Pwd") = .Pwd
            oRow("Ext") = .Ext.ToUpper
            oRow("Caduca") = .Caduca
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCert As DTOCert, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCert, oTrans)
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


    Shared Sub Delete(oCert As DTOCert, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliCert WHERE Guid='" & oCert.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class
