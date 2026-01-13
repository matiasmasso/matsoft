Public Class DocfileSrcLoader

#Region "CRUD"


    Shared Function Load(oDocfileSrc As DTODocFileSrc) As Boolean
        Dim retval As Boolean
        If oDocfileSrc.Docfile IsNot Nothing Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM DocfileSrc ")
            sb.AppendLine("INNER JOIN VwDocfileThumbnail ON DocfileSrc.Hash = VwDocfileThumbnail.DocfileHash ")
            sb.AppendLine("WHERE DocfileSrc.Hash='" & oDocfileSrc.Docfile.Hash & "' ")
            sb.AppendLine("AND DocfileSrc.SrcCod=" & oDocfileSrc.Cod & " ")
            sb.AppendLine("AND DocfileSrc.SrcGuid='" & oDocfileSrc.Src.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oDocfileSrc
                    .Docfile = SQLHelper.GetDocFileFromDataReader(oDrd)
                    retval = True
                End With
            End If
            oDrd.Close()
        End If
        Return retval
    End Function

    Shared Function Update(oDocfileSrc As DTODocFileSrc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oDocfileSrc, oTrans)
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


    Shared Sub Update(oDocfileSrc As DTODocFileSrc, ByRef oTrans As SqlTransaction)
        DocFileLoader.Update(oDocfileSrc.Docfile, oTrans)
        UpdateHeader(oDocfileSrc, oTrans)
    End Sub

    Shared Sub UpdateHeader(oDocfileSrc As DTODocFileSrc, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM DocfileSrc ")
        sb.AppendLine("WHERE Hash='" & oDocfileSrc.Docfile.Hash & "' ")
        sb.AppendLine("AND SrcCod=" & oDocfileSrc.Cod & " ")
        sb.AppendLine("AND SrcGuid='" & oDocfileSrc.Src.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Hash") = oDocfileSrc.Docfile.Hash
            oRow("SrcCod") = oDocfileSrc.Cod
            oRow("SrcGuid") = oDocfileSrc.Src.Guid.ToString
        End If

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oDocfileSrc As DTODocFileSrc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oDocfileSrc, oTrans)
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


    Shared Sub Delete(oDocfileSrc As DTODocFileSrc, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE DocfileSrc ")
        sb.AppendLine("WHERE Hash='" & oDocfileSrc.Docfile.Hash & "' ")
        sb.AppendLine("AND SrcCod=" & oDocfileSrc.Cod & " ")
        sb.AppendLine("AND SrcGuid='" & oDocfileSrc.Src.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class DocfileSrcsLoader
    Shared Function All(oBaseGuid As DTOBaseGuid) As List(Of DTODocFileSrc)
        Dim retval As New List(Of DTODocFileSrc)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM DocfileSrc ")
        sb.AppendLine("INNER JOIN VwDocfile ON DocfileSrc.Hash = VwDocfile.DocfileHash ")
        sb.AppendLine("WHERE SrcGuid ='" & oBaseGuid.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY VwDocfile.DocfileFch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTODocFileSrc()
            With item
                .Docfile = SQLHelper.GetDocFileFromDataReader(oDrd)
                .Cod = oDrd("SrcCod")
                .Src = oBaseGuid
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
