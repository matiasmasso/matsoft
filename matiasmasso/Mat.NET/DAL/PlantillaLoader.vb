Public Class PlantillaLoader

    Shared Function Find(oGuid As Guid) As DTOPlantilla
        Dim retval As DTOPlantilla = Nothing
        Dim oPlantilla As New DTOPlantilla(oGuid)
        If Load(oPlantilla) Then
            retval = oPlantilla
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPlantilla As DTOPlantilla) As Boolean
        If Not oPlantilla.IsLoaded And Not oPlantilla.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Plantilla.Guid, VwDocFile.* ")
            sb.AppendLine("FROM Plantilla ")
            sb.AppendLine("LEFT OUTER JOIN VwDocfile ON Plantilla.Hash = VwDocfile.DocFileHash ")
            sb.AppendLine("WHERE Plantilla.Guid='" & oPlantilla.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oPlantilla
                    .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPlantilla.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPlantilla As DTOPlantilla, oEmp As DTOEmp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            DocFileLoader.Update(oPlantilla.DocFile, oTrans)
            Update(oPlantilla, oEmp, oTrans)
            oTrans.Commit()
            oPlantilla.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oPlantilla As DTOPlantilla, oEmp As DTOEmp, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Plantilla ")
        sb.AppendLine("WHERE Guid='" & oPlantilla.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPlantilla.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPlantilla
            oRow("Emp") = oEmp.Id
            oRow("Hash") = .DocFile.Hash
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPlantilla As DTOPlantilla, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            DocFileLoader.Delete(oPlantilla.DocFile, oTrans)
            Delete(oPlantilla, oTrans)
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


    Shared Sub Delete(oPlantilla As DTOPlantilla, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Plantilla WHERE Guid='" & oPlantilla.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class PlantillasLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOPlantilla)
        Dim retval As New List(Of DTOPlantilla)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Plantilla.Guid, VwDocFile.* ")
        sb.AppendLine("FROM Plantilla ")
        sb.AppendLine("LEFT OUTER JOIN VwDocfile ON Plantilla.Hash = VwDocfile.DocFileHash ")
        sb.AppendLine("WHERE Plantilla.Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY VwDocfile.DocFileNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPlantilla(oDrd("Guid"))
            With item
                .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

