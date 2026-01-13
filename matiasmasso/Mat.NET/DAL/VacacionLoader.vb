Public Class VacacionLoader

    Shared Sub Update(oContact As DTOContact, oTipus As DTOContact.Tipus, oVacaciones As List(Of DTOVacacion), ByRef oTrans As SqlTransaction)
        Delete(oContact, oTipus, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliVacs ")
        sb.AppendLine("WHERE Contact='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Cod=" & CInt(oTipus) & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOVacacion In oVacaciones
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Contact") = oContact.Guid
            oRow("Cod") = CInt(oTipus)
            oRow("FromMes") = item.MonthDayFrom.Month
            oRow("FromDia") = item.MonthDayFrom.Day
            oRow("UntilMes") = item.MonthDayTo.Month
            oRow("UntilDia") = item.MonthDayTo.Day
            oRow("ForwardMes") = item.MonthDayResult.Month
            oRow("ForwardDia") = item.MonthDayResult.Day
        Next
        oDA.Update(oDs)
    End Sub

    Shared Sub Delete(oContact As DTOContact, oTipus As DTOContact.Tipus, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE CliVacs ")
        sb.AppendLine("WHERE Contact='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Cod=" & CInt(oTipus) & " ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class

Public Class VacacionesLoader
    Shared Function All(oContact As DTOContact, oTipus As DTOContact.Tipus) As List(Of DTOVacacion)
        Dim retval As New List(Of DTOVacacion)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliVacs")
        sb.AppendLine("WHERE Contact='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Cod=" & CInt(oTipus) & " ")
        sb.AppendLine("ORDER BY FromMes, FromDia")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOVacacion
            With item
                .MonthDayFrom = New DTOMonthDay(oDrd("FromMes"), oDrd("FromDia"))
                .MonthDayTo = New DTOMonthDay(oDrd("UntilMes"), oDrd("UntilDia"))
                .MonthDayResult = New DTOMonthDay(oDrd("ForwardMes"), oDrd("ForwardDia"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
