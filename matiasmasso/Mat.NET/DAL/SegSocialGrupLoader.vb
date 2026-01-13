Public Class SegSocialGrupLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSegSocialGrup
        Dim retval As DTOSegSocialGrup = Nothing
        Dim oSegSocialGrup As New DTOSegSocialGrup(oGuid)
        If Load(oSegSocialGrup) Then
            retval = oSegSocialGrup
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSegSocialGrup As DTOSegSocialGrup) As Boolean
        If Not oSegSocialGrup.IsLoaded And Not oSegSocialGrup.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM SegSocialGrups ")
            sb.AppendLine("WHERE Guid='" & oSegSocialGrup.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oSegSocialGrup
                    .Id = oDrd("Id")
                    .Nom = oDrd("Nom")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oSegSocialGrup.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSegSocialGrup As DTOSegSocialGrup, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSegSocialGrup, oTrans)
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


    Shared Sub Update(oSegSocialGrup As DTOSegSocialGrup, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SegSocialGrups ")
        sb.AppendLine("WHERE Guid='" & oSegSocialGrup.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSegSocialGrup.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSegSocialGrup
            oRow("Id") = .Id
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSegSocialGrup As DTOSegSocialGrup, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSegSocialGrup, oTrans)
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


    Shared Sub Delete(oSegSocialGrup As DTOSegSocialGrup, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SegSocialGrup WHERE Guid='" & oSegSocialGrup.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class SegSocialGrupsLoader

    Shared Function All() As List(Of DTOSegSocialGrup)
        Dim retval As New List(Of DTOSegSocialGrup)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SegSocialGrups ")
        sb.AppendLine("ORDER BY Id, Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSegSocialGrup(oDrd("Guid"))
            With item
                .Id = oDrd("Id")
                .Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
