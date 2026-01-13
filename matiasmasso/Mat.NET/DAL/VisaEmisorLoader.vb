Public Class VisaEmisorLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOVisaEmisor
        Dim retval As DTOVisaEmisor = Nothing
        Dim oVisaOrg As New DTOVisaEmisor(oGuid)
        If Load(oVisaOrg) Then
            retval = oVisaOrg
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oVisaOrg As DTOVisaEmisor) As Boolean
        If Not oVisaOrg.IsLoaded And Not oVisaOrg.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM VisaEmisor ")
            sb.AppendLine("WHERE Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oVisaOrg.Guid.ToString())
            If oDrd.Read Then
                With oVisaOrg
                    .Nom = oDrd("Nom")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oVisaOrg.IsLoaded
        Return retval
    End Function

    Shared Function Update(oVisaOrg As DTOVisaEmisor, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oVisaOrg, oTrans)
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


    Shared Sub Update(oVisaOrg As DTOVisaEmisor, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM VisaEmisor ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oVisaOrg.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oVisaOrg.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oVisaOrg
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oVisaOrg As DTOVisaEmisor, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oVisaOrg, oTrans)
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


    Shared Sub Delete(oVisaOrg As DTOVisaEmisor, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE VisaEmisor WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oVisaOrg.Guid.ToString())
    End Sub

#End Region

End Class

Public Class VisaEmisorsLoader

    Shared Function All() As List(Of DTOVisaEmisor)
        Dim retval As New List(Of DTOVisaEmisor)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM VisaEmisor ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOVisaEmisor(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class