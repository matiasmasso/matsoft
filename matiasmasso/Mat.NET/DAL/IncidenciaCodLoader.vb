Public Class IncidenciaCodLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOIncidenciaCod
        Dim retval As DTOIncidenciaCod = Nothing
        Dim oIncidenciaCod As New DTOIncidenciaCod(oGuid)
        If Load(oIncidenciaCod) Then
            retval = oIncidenciaCod
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oIncidenciaCod As DTOIncidenciaCod) As Boolean
        If Not oIncidenciaCod.IsLoaded Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM IncidenciesCods ")
            sb.AppendLine("WHERE Guid = '" & oIncidenciaCod.Guid.ToString & "'")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oIncidenciaCod
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng", "Por")
                    .Cod = oDrd("Cod")
                    .ReposicionParcial = oDrd("ReposicionParcial")
                    .ReposicionTotal = oDrd("ReposicionTotal")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oIncidenciaCod.IsLoaded
        Return retval
    End Function

    Shared Function Update(oIncidenciaCod As DTOIncidenciaCod, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oIncidenciaCod, oTrans)
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


    Shared Sub Update(oIncidenciaCod As DTOIncidenciaCod, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM IncidenciesCods ")
        sb.AppendLine("WHERE Guid = '" & oIncidenciaCod.Guid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oIncidenciaCod.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oIncidenciaCod
            oRow("Esp") = .Nom.Esp
            oRow("Cat") = .Nom.Cat
            oRow("Eng") = .Nom.Eng
            oRow("Por") = .Nom.Por
            oRow("ReposicionParcial") = .ReposicionParcial
            oRow("ReposicionTotal") = .ReposicionTotal
            oRow("Cod") = CInt(.Cod)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oIncidenciaCod As DTOIncidenciaCod, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oIncidenciaCod, oTrans)
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


    Shared Sub Delete(oIncidenciaCod As DTOIncidenciaCod, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE ")
        sb.AppendLine("FROM IncidenciesCods ")
        sb.AppendLine("WHERE Guid = '" & oIncidenciaCod.Guid.ToString & "'")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class IncidenciaCodsLoader

    Shared Function All(Optional oCod As DTOIncidenciaCod.cods = DTOIncidenciaCod.cods.NotSet) As List(Of DTOIncidenciaCod)
        Dim retval As New List(Of DTOIncidenciaCod)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM IncidenciesCods ")
        If oCod <> DTOIncidenciaCod.cods.NotSet Then
            sb.AppendLine("WHERE Cod = " & oCod & " ")
        End If
        sb.AppendLine("ORDER BY Esp")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOIncidenciaCod(oDrd("Guid"))
            With item
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng", "Por")
                .Cod = oDrd("Cod")
                .ReposicionParcial = oDrd("ReposicionParcial")
                .ReposicionTotal = oDrd("ReposicionTotal")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

