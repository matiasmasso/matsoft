Public Class PgcEpg2Loader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPgcEpg2
        Dim retval As DTOPgcEpg2 = Nothing
        Dim oPgcEpg2 As New DTOPgcEpg2(oGuid)
        If Load(oPgcEpg2) Then
            retval = oPgcEpg2
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPgcEpg2 As DTOPgcEpg2) As Boolean
        If Not oPgcEpg2.IsLoaded And Not oPgcEpg2.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM PgcEpg2 ")
            sb.AppendLine("WHERE Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oPgcEpg2.Guid.ToString)
            If oDrd.Read Then
                With oPgcEpg2
                    .Parent = New DTOPgcEpg1(oDrd("Parent"))
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

        Dim retval As Boolean = oPgcEpg2.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPgcEpg2 As DTOPgcEpg2, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPgcEpg2, oTrans)
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


    Shared Sub Update(oPgcEpg2 As DTOPgcEpg2, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PgcEpg2 ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oPgcEpg2.Guid.ToString)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPgcEpg2.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPgcEpg2
            oRow("Parent") = SQLHelper.NullableBaseGuid(.Parent)
            oRow("NomEsp") = .NomEsp
            oRow("NomCat") = .NomCat
            oRow("NomEng") = .NomEng
            oRow("Cod") = .Cod
            oRow("Ordinal") = .Ordinal
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPgcEpg2 As DTOPgcEpg2, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPgcEpg2, oTrans)
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


    Shared Sub Delete(oPgcEpg2 As DTOPgcEpg2, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PgcEpg2 WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPgcEpg2.Guid.ToString)
    End Sub

#End Region




End Class
Public Class PgcEpg2sLoader
    Shared Function Tree() As List(Of DTOPgcEpgBase)
        Dim retval As New List(Of DTOPgcEpgBase)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PgcEpg0.Guid as Epg0, PgcEpg0.NomEsp as Esp0 ")
        sb.AppendLine(", PgcEpg1.Guid as Epg1, PgcEpg1.NomEsp as Esp1 ")
        sb.AppendLine(", PgcEpg2.Guid As Epg2, PgcEpg2.NomEsp as Esp2 ")
        sb.AppendLine("From PgcEpg0 ")
        sb.AppendLine("LEFT OUTER JOIN PgcEpg1 ON PgcEpg0.Guid=PgcEpg1.Parent ")
        sb.AppendLine("LEFT OUTER JOIN PgcEpg2 ON PgcEpg1.Guid=PgcEpg2.Parent ")
        sb.AppendLine("ORDER BY PgcEpg0.Ordinal, PgcEpg1.Ordinal, PgcEpg2.Ordinal")
        Dim SQL As String = sb.ToString

        Dim Epg0 As New DTOPgcEpg0
        Dim Epg1 As New DTOPgcEpg1

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not Epg0.Guid.Equals(oDrd("Epg0")) Then
                Epg0 = New DTOPgcEpg0(oDrd("Epg0"))
                With Epg0
                    .NomEsp = oDrd("Esp0")
                End With
                retval.Add(Epg0)
            End If
            If Not IsDBNull(oDrd("Epg1")) Then
                If Not Epg1.Guid.Equals(oDrd("Epg1")) Then
                    Epg1 = New DTOPgcEpg1(oDrd("Epg1"))
                    With Epg1
                        .NomEsp = oDrd("Esp1")
                    End With
                    Epg0.Children.Add(Epg1)
                End If
                If Not IsDBNull(oDrd("Epg2")) Then
                    Dim Epg2 As New DTOPgcEpg2(oDrd("Epg2"))
                    With Epg2
                        .NomEsp = oDrd("Esp2")
                    End With
                    Epg1.Children.Add(Epg2)
                End If

            End If
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
