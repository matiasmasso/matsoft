Public Class PgcEpg0Loader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPgcEpg0
        Dim retval As DTOPgcEpg0 = Nothing
        Dim oPgcEpg0 As New DTOPgcEpg0(oGuid)
        If Load(oPgcEpg0) Then
            retval = oPgcEpg0
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPgcEpg0 As DTOPgcEpg0) As Boolean
        If Not oPgcEpg0.IsLoaded And Not oPgcEpg0.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM PgcEpg0 ")
            sb.AppendLine("WHERE Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oPgcEpg0.Guid.ToString)
            If oDrd.Read Then
                With oPgcEpg0
                    .NomEsp = oDrd("NomEsp")
                    .NomCat = oDrd("NomCat")
                    .NomEng = oDrd("NomEng")
                    .Cod = oDrd("Cod")
                    .Ordinal = oDrd("Ordinal")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPgcEpg0.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPgcEpg0 As DTOPgcEpg0, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPgcEpg0, oTrans)
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


    Shared Sub Update(oPgcEpg0 As DTOPgcEpg0, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PgcEpg0 ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oPgcEpg0.Guid.ToString)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPgcEpg0.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPgcEpg0
            oRow("NomEsp") = .NomEsp
            oRow("NomCat") = .NomCat
            oRow("NomEng") = .NomEng
            oRow("Cod") = .Cod
            oRow("Ordinal") = .Ordinal
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPgcEpg0 As DTOPgcEpg0, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPgcEpg0, oTrans)
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


    Shared Sub Delete(oPgcEpg0 As DTOPgcEpg0, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PgcEpg0 WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPgcEpg0.Guid.ToString)
    End Sub

#End Region

End Class
