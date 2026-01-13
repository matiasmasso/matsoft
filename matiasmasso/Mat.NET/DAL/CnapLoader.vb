Public Class CnapLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCnap
        Dim retval As DTOCnap = Nothing
        Dim oCnap As New DTOCnap(oGuid)
        If Load(oCnap) Then
            retval = oCnap
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCnap As DTOCnap) As Boolean
        If Not oCnap.IsLoaded And Not oCnap.IsNew Then

            Dim sb As New Text.StringBuilder
            sb.AppendLine("SELECT VwProductCnap.* ")
            sb.AppendLine(", Parent.Id AS ParentId, Parent.ShortNomEsp AS ParentShortEsp, Parent.LongNomEsp AS ParentLongEsp ")
            sb.AppendLine("FROM VwProductCnap ")
            sb.AppendLine("LEFT OUTER JOIN VwCnap AS Parent ON VwProductCnap.Parent = Parent.Guid ")
            sb.AppendLine("WHERE VwProductCnap.Guid='" & oCnap.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oCnap.IsLoaded Then
                    With oCnap
                        Dim oParent As DTOCnap = Nothing
                        If Not IsDBNull(oDrd("Parent")) Then
                            Dim oParentGuid As Guid = oDrd("Parent")
                            If oParentGuid <> Nothing Then
                                .Parent = New DTOCnap(oParentGuid)
                                With .Parent
                                    .Id = SQLHelper.GetStringFromDataReader(oDrd("ParentId"))
                                    SQLHelper.LoadLangTextFromDataReader(.NomShort, oDrd, "ParentShortEsp")
                                    SQLHelper.LoadLangTextFromDataReader(.NomLong, oDrd, "ParentLongEsp")
                                End With
                            End If
                        End If
                        .Id = SQLHelper.GetStringFromDataReader(oDrd("Id"))
                        SQLHelper.LoadLangTextFromDataReader(.NomShort, oDrd, "NomShort_ESP", "NomShort_CAT", "NomShort_ENG", "NomShort_POR")
                        SQLHelper.LoadLangTextFromDataReader(.NomLong, oDrd, "NomLong_ESP", "NomLong_CAT", "NomLong_ENG", "NomLong_POR")
                        .Tags = SQLHelper.GetStringFromDataReader(oDrd("Tags")).Split(",").ToList
                        .Products = New List(Of DTOProduct)
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("BrandGuid")) Then
                    Dim oProduct As DTOProduct = SQLHelper.GetProductFromDataReader(oDrd)
                    oCnap.Products.Add(oProduct)
                End If
            Loop
            oDrd.Close()
        End If

        Dim retval As Boolean = oCnap.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCnap As DTOCnap, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oCnap, oTrans)
            LangTextLoader.Update(oCnap.NomShort, oTrans)
            LangTextLoader.Update(oCnap.NomLong, oTrans)
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


    Shared Sub Update(oCnap As DTOCnap, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Cnap ")
        sb.AppendLine("WHERE Guid='" & oCnap.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCnap.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCnap
            oRow("Parent") = SQLHelper.NullableBaseGuid(.Parent)
            oRow("Id") = .Id
            oRow("Tags") = String.Join(",", .Tags)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCnap As DTOCnap, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            LangTextLoader.Delete(oCnap.NomShort, oTrans)
            'no need to delete NomLong since it deletes by BaseGuid
            Delete(oCnap, oTrans)
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


    Shared Sub Delete(oCnap As DTOCnap, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Cnap WHERE Guid='" & oCnap.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class CnapsLoader

    Shared Function All(Optional searchKey As String = "") As List(Of DTOCnap)
        Dim retval As New List(Of DTOCnap)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT * FROM VwProductCnap ")
        If searchKey > "" Then
            sb.AppendLine("WHERE (")
            sb.AppendLine("VwProductCnap.NomShort_ESP LIKE '%" & searchKey & "%' ")
            sb.AppendLine("OR VwProductCnap.Tags LIKE '%" & searchKey & "%' ")
            sb.AppendLine(") ")
        End If
        sb.AppendLine("ORDER BY VwProductCnap.Id ")
        Dim SQL As String = sb.ToString
        Dim oCnap As New DTOCnap()

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCnap.Guid.Equals(oDrd("Guid")) Then
                oCnap = New DTOCnap(oDrd("Guid"))
                With oCnap
                    Dim oParent As DTOCnap = Nothing
                    If Not IsDBNull(oDrd("Parent")) Then
                        Dim oParentGuid As Guid = oDrd("Parent")
                        If oParentGuid <> Nothing Then
                            .Parent = New DTOCnap(oParentGuid)
                        End If
                    End If
                    .Id = SQLHelper.GetStringFromDataReader(oDrd("Id"))
                    SQLHelper.LoadLangTextFromDataReader(.NomShort, oDrd, "NomShort_ESP", "NomShort_CAT", "NomShort_ENG", "NomShort_POR")
                    SQLHelper.LoadLangTextFromDataReader(.NomLong, oDrd, "NomLong_ESP", "NomLong_CAT", "NomLong_ENG", "NomLong_POR")
                    .Tags = SQLHelper.GetStringFromDataReader(oDrd("Tags")).Split(",").ToList
                    .Products = New List(Of DTOProduct)
                End With
                retval.Add(oCnap)
            End If

            If Not IsDBNull(oDrd("BrandGuid")) Then
                Dim oProduct As DTOProduct = SQLHelper.GetProductFromDataReader(oDrd)
                oCnap.Products.Add(oProduct)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
