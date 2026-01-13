Public Class AreaRegioLoader
#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOAreaRegio
        Dim retval As DTOAreaRegio = Nothing
        Dim oAreaRegio As New DTOAreaRegio
        oAreaRegio.Guid = oGuid
        If Load(oAreaRegio) Then
            retval = oAreaRegio
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oAreaRegio As DTOAreaRegio) As Boolean
        If Not oAreaRegio.IsLoaded And Not oAreaRegio.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Regio.*, Country.* ")
            sb.AppendLine("FROM Regio ")
            sb.AppendLine("INNER JOIN Country ON Regio.Country=Country.Guid ")
            sb.AppendLine("WHERE Regio.Guid='" & oAreaRegio.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oAreaRegio
                    .Country = SQLHelper.GetCountryFromDataReader(oDrd)
                    .Nom = oDrd("Nom")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oAreaRegio.IsLoaded
        Return retval
    End Function

    Shared Function Update(oAreaRegio As DTOAreaRegio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oAreaRegio, oTrans)
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


    Shared Sub Update(oAreaRegio As DTOAreaRegio, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Regio ")
        sb.AppendLine("WHERE Guid='" & oAreaRegio.Guid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oAreaRegio.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oAreaRegio
            oRow("Country") = SQLHelper.NullableBaseGuid(.Country)
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oAreaRegio As DTOAreaRegio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oAreaRegio, oTrans)
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


    Shared Sub Delete(oAreaRegio As DTOAreaRegio, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Regio WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oAreaRegio.Guid.ToString())
    End Sub

#End Region

End Class

Public Class AreaRegionsLoader

    Shared Function All(oCountry As DTOCountry) As List(Of DTOAreaRegio)
        Dim retval As New List(Of DTOAreaRegio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Regio ")
        sb.AppendLine("WHERE Country='" & oCountry.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOAreaRegio(oDrd("Guid"))
            With item
                .Country = oCountry
                .Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
