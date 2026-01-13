Public Class SiiLogLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSiiLog
        Dim retval As DTOSiiLog = Nothing
        Dim oSiiLog As New DTOSiiLog(oGuid)
        If Load(oSiiLog) Then
            retval = oSiiLog
        End If
        Return retval
    End Function

    Shared Function Find(sCsv As String) As DTOSiiLog
        Dim retval As DTOSiiLog = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SiiLog ")
        sb.AppendLine("WHERE Csv='" & sCsv & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOSiiLog(oDrd("Guid"))
            With retval
                .Entorno = oDrd("Entorno")
                .Fch = oDrd("Fch")
                .Contingut = oDrd("Contingut")
                .TipoDeComunicacion = oDrd("TipoDeComunicacion")
                .Csv = SQLHelper.GetStringFromDataReader(oDrd("Csv"))
                .Result = SQLHelper.GetIntegerFromDataReader(oDrd("Result"))
                .IsLoaded = True
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oSiiLog As DTOSiiLog) As Boolean
        If Not oSiiLog.IsLoaded And Not oSiiLog.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM SiiLog ")
            sb.AppendLine("WHERE Guid='" & oSiiLog.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oSiiLog
                    .Entorno = oDrd("Entorno")
                    .Fch = oDrd("Fch")
                    .Contingut = oDrd("Contingut")
                    .TipoDeComunicacion = oDrd("TipoDeComunicacion")
                    .Csv = SQLHelper.GetStringFromDataReader(oDrd("Csv"))
                    .Result = SQLHelper.GetIntegerFromDataReader(oDrd("Result"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oSiiLog.IsLoaded
        Return retval
    End Function
    Shared Function FromCsv(sCsv As String) As DTOSiiLog
        Dim retval As DTOSiiLog = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid ")
        sb.AppendLine("FROM SiiLog ")
        sb.AppendLine("WHERE Csv='" & sCsv & "' ")

        Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOSiiLog(oDrd("Guid"))
            With retval
                .Csv = sCsv
            End With
        End If

        oDrd.Close()

        Return retval
    End Function

    Shared Function Update(oSiiLog As DTOSiiLog, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSiiLog, oTrans)
            oTrans.Commit()
            oSiiLog.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oSiiLog As DTOSiiLog, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SiiLog ")
        sb.AppendLine("WHERE Guid='" & oSiiLog.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSiiLog.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSiiLog
            oRow("Entorno") = .Entorno
            oRow("Fch") = .Fch
            oRow("TipoDeComunicacion") = .TipoDeComunicacion
            oRow("Contingut") = .Contingut
            oRow("Csv") = SQLHelper.NullableString(.Csv)
            oRow("Result") = SQLHelper.NullableInt(.Result)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSiiLog As DTOSiiLog, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSiiLog, oTrans)
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


    Shared Sub Delete(oSiiLog As DTOSiiLog, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SiiLog WHERE Guid='" & oSiiLog.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class SiiLogsLoader

    Shared Function All() As List(Of DTOSiiLog)
        Dim retval As New List(Of DTOSiiLog)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SiiLog ")
        sb.AppendLine("ORDER BY Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSiiLog(oDrd("Guid"))
            With item
                .Entorno = oDrd("Entorno")
                .Fch = oDrd("Fch")
                .Contingut = oDrd("Contingut")
                .TipoDeComunicacion = oDrd("TipoDeComunicacion")
                .Csv = SQLHelper.GetStringFromDataReader(oDrd("Csv"))
                .Result = SQLHelper.GetIntegerFromDataReader(oDrd("Result"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

