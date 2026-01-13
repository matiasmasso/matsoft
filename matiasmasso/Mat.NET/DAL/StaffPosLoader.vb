Public Class StaffPosLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOStaffPos
        Dim retval As DTOStaffPos = Nothing
        Dim oStaffPos As New DTOStaffPos(oGuid)
        If Load(oStaffPos) Then
            retval = oStaffPos
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oStaffPos As DTOStaffPos) As Boolean
        If Not oStaffPos.IsLoaded And Not oStaffPos.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM StaffPos ")
            sb.AppendLine("WHERE Guid='" & oStaffPos.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oStaffPos
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng")
                    .Ord = oDrd("Ord")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oStaffPos.IsLoaded
        Return retval
    End Function

    Shared Function Update(oStaffPos As DTOStaffPos, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oStaffPos, oTrans)
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


    Shared Sub Update(oStaffPos As DTOStaffPos, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM StaffPos ")
        sb.AppendLine("WHERE Guid='" & oStaffPos.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oStaffPos.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oStaffPos
            SQLHelper.SetNullableLangText(.LangNom, oRow, "NomEsp", "NomCat", "NomEng")
            oRow("Ord") = .Ord
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oStaffPos As DTOStaffPos, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oStaffPos, oTrans)
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


    Shared Sub Delete(oStaffPos As DTOStaffPos, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE StaffPos WHERE Guid='" & oStaffPos.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class StaffPossLoader

    Shared Function All() As List(Of DTOStaffPos)
        Dim retval As New List(Of DTOStaffPos)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM StaffPos ")
        sb.AppendLine("ORDER BY Ord")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOStaffPos(oDrd("Guid"))
            With item
                .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng")
                .Ord = oDrd("Ord")
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oStaffPoss As List(Of DTOStaffPos), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffPossLoader.Delete(oStaffPoss, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE StaffPos ")
        sb.AppendLine("WHERE (")
        For Each item As DTOStaffPos In oStaffPoss
            If item.UnEquals(oStaffPoss.First) Then
                sb.Append("OR ")
            End If
            sb.Append("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Return retval
    End Function

End Class
