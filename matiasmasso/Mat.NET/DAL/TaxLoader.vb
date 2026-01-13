Public Class TaxLoader

#Region "CRUD"


    Shared Function Find(oGuid As Guid) As DTOTax
        Dim retval As DTOTax = Nothing
        Dim oTax As New DTOTax(oGuid)
        If Load(oTax) Then
            retval = oTax
        End If
        Return retval
    End Function

    Shared Function Find(Codi As DTOTax.Codis, DtFch As Date) As DTOTax
        Dim retval As DTOTax = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Tax ")
        sb.AppendLine("WHERE Codi=" & CInt(Codi) & " ")
        sb.AppendLine("AND Fch='" & Format(DtFch, "yyyyMMdd") & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOTax(oDrd("Guid"))
            With retval
                .Codi = CInt(oDrd("Codi"))
                .Fch = CDate(oDrd("Fch"))
                .Tipus = CDec(oDrd("Tipus"))
                .IsLoaded = True
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Closest(Codi As DTOTax.Codis, DtFch As Date) As DTOTax
        Dim retval As DTOTax = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 * ")
        sb.AppendLine("FROM Tax ")
        sb.AppendLine("WHERE Codi=" & CInt(Codi) & " ")
        sb.AppendLine("AND Fch<='" & Format(DtFch, "yyyyMMdd") & "' ")
        sb.AppendLine("ORDER BY Fch DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOTax(oDrd("Guid"))
            With retval
                .Codi = CInt(oDrd("Codi"))
                .Fch = CDate(oDrd("Fch"))
                .Tipus = CDec(oDrd("Tipus"))
                .IsLoaded = True
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oTax As DTOTax) As Boolean
        If Not oTax.IsLoaded And Not oTax.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Tax ")
            sb.AppendLine("WHERE Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oTax.Guid.ToString())
            If oDrd.Read Then
                With oTax
                    .Codi = CInt(oDrd("Codi"))
                    .Fch = CDate(oDrd("Fch"))
                    .Tipus = CDec(oDrd("Tipus"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oTax.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTax As DTOTax, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTax, oTrans)
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


    Shared Sub Update(oTax As DTOTax, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Tax ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oTax.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oTax.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oTax
            oRow("Codi") = CInt(.Codi)
            oRow("Fch") = .Fch
            oRow("Tipus") = .Tipus
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oTax As DTOTax, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oTax, oTrans)
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


    Shared Sub Delete(oTax As DTOTax, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Tax WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oTax.Guid.ToString())
    End Sub

#End Region

    Shared Function Tipus(oCodi As DTOTax.Codis, Optional DtFch As Date = Nothing) As Decimal
        Dim retval As Decimal
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Tipus ")
        sb.AppendLine("FROM Tax ")
        sb.AppendLine("WHERE Codi = " & CInt(oCodi) & " ")
        sb.AppendLine("AND Fch<='" & Format(DtFch, "yyyyMMdd") & "' ")
        sb.AppendLine("ORDER BY Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = CDec(oDrd("Tipus"))
        End If

        oDrd.Close()
        Return retval
    End Function

End Class

Public Class TaxesLoader

    Shared Function All() As List(Of DTOTax)
        Dim retval As New List(Of DTOTax)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Tax ")
        sb.AppendLine("ORDER BY Fch DESC, Codi")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTax(oDrd("Guid"))
            With item
                .Codi = CInt(oDrd("Codi"))
                .Fch = CDate(oDrd("Fch"))
                .Tipus = CDec(oDrd("Tipus"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Closest(Optional DtFch As Date = Nothing) As List(Of DTOTax)
        Dim retval As New List(Of DTOTax)
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Dim sFch As String = Format(DtFch, "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tax.Guid, Tax.Codi, Tax.Fch, Tax.Tipus ")
        sb.AppendLine("FROM Tax ")
        sb.AppendLine("INNER JOIN ")
        sb.AppendLine("     ( ")
        sb.AppendLine("     SELECT Codi, MAX(Fch) AS LastFch ")
        sb.AppendLine("     FROM Tax ")
        sb.AppendLine("		WHERE Fch<=GETDATE() ")
        sb.AppendLine("		GROUP BY Codi ")
        sb.AppendLine("		) Closest ON Tax.Codi = Closest.Codi AND Tax.Fch=Closest.LastFch ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTax(oDrd("Guid"))
            With item
                .Codi = CInt(oDrd("Codi"))
                .Fch = CDate(oDrd("Fch"))
                .Tipus = CDec(oDrd("Tipus"))
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function


End Class
