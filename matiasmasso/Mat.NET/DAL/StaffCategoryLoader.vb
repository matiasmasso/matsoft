Public Class StaffCategoryLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOStaffCategory
        Dim retval As DTOStaffCategory = Nothing
        Dim oStaffCategory As New DTOStaffCategory(oGuid)
        If Load(oStaffCategory) Then
            retval = oStaffCategory
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oStaffCategory As DTOStaffCategory) As Boolean
        If Not oStaffCategory.IsLoaded And Not oStaffCategory.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT StaffCategory.Nom ")
            sb.AppendLine(", StaffCategory.SegSocialGrup, SegSocialGrups.Id AS SsId, SegSocialGrups.Nom AS SsNom ")
            sb.AppendLine("FROM StaffCategory ")
            sb.AppendLine("LEFT OUTER JOIN SegSocialGrups ON StaffCategory.SegSocialGrup = SegSocialGrups.Guid ")
            sb.AppendLine("WHERE StaffCategory.Guid='" & oStaffCategory.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oStaffCategory
                    .Nom = oDrd("Nom")
                    If Not IsDBNull(oDrd("SegSocialGrup")) Then
                        .SegSocialGrup = New DTOSegSocialGrup(oDrd("SegSocialGrup"))
                        With .SegSocialGrup
                            .Id = SQLHelper.GetIntegerFromDataReader(oDrd("SsId"))
                            .Nom = SQLHelper.GetStringFromDataReader(oDrd("SsNom"))
                        End With
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oStaffCategory.IsLoaded
        Return retval
    End Function

    Shared Function Update(oStaffCategory As DTOStaffCategory, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oStaffCategory, oTrans)
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


    Shared Sub Update(oStaffCategory As DTOStaffCategory, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM StaffCategory ")
        sb.AppendLine("WHERE Guid='" & oStaffCategory.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oStaffCategory.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oStaffCategory
            oRow("Nom") = .Nom
            oRow("SegSocialGrup") = SQLHelper.NullableBaseGuid(.SegSocialGrup)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oStaffCategory As DTOStaffCategory, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oStaffCategory, oTrans)
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


    Shared Sub Delete(oStaffCategory As DTOStaffCategory, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE StaffCategory WHERE Guid='" & oStaffCategory.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class StaffCategoriesLoader

    Shared Function All() As List(Of DTOStaffCategory)
        Dim retval As New List(Of DTOStaffCategory)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT StaffCategory.Guid, StaffCategory.Nom ")
        sb.AppendLine(", StaffCategory.SegSocialGrup, SegSocialGrups.Id AS SsId, SegSocialGrups.Nom AS SsNom ")
        sb.AppendLine("FROM StaffCategory ")
        sb.AppendLine("LEFT OUTER JOIN SegSocialGrups ON StaffCategory.SegSocialGrup = SegSocialGrups.Guid ")
        sb.AppendLine("ORDER BY SegSocialGrups.Id, StaffCategory.Ord")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOStaffCategory(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
                If Not IsDBNull(oDrd("SegSocialGrup")) Then
                    .SegSocialGrup = New DTOSegSocialGrup(oDrd("SegSocialGrup"))
                    With .SegSocialGrup
                        .Id = SQLHelper.GetIntegerFromDataReader(oDrd("SsId"))
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("SsNom"))
                    End With
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval

    End Function


End Class

