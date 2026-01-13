Public Class PgcEpg1Loader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPgcEpg1
        Dim retval As DTOPgcEpg1 = Nothing
        Dim oPgcEpg1 As New DTOPgcEpg1(oGuid)
        If Load(oPgcEpg1) Then
            retval = oPgcEpg1
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPgcEpg1 As DTOPgcEpg1) As Boolean
        If Not oPgcEpg1.IsLoaded And Not oPgcEpg1.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM PgcEpg1 ")
            sb.AppendLine("WHERE Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oPgcEpg1.Guid.ToString)
            If oDrd.Read Then
                With oPgcEpg1
                    .Parent = New DTOPgcEpg0(oDrd("Parent"))
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

        Dim retval As Boolean = oPgcEpg1.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPgcEpg1 As DTOPgcEpg1, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPgcEpg1, oTrans)
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


    Shared Sub Update(oPgcEpg1 As DTOPgcEpg1, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PgcEpg1 ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oPgcEpg1.Guid.ToString)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPgcEpg1.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPgcEpg1
            oRow("Parent") = SQLHelper.NullableBaseGuid(.Parent)
            oRow("NomEsp") = .NomEsp
            oRow("NomCat") = .NomCat
            oRow("NomEng") = .NomEng
            oRow("Cod") = .Cod
            oRow("Ordinal") = .Ordinal
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPgcEpg1 As DTOPgcEpg1, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPgcEpg1, oTrans)
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


    Shared Sub Delete(oPgcEpg1 As DTOPgcEpg1, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PgcEpg1 WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPgcEpg1.Guid.ToString)
    End Sub

#End Region

End Class
