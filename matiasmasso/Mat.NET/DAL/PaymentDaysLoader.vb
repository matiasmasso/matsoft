Public Class PaymentDaysLoader
    Shared Function All(oContact As DTOContact, oTipus As DTOContact.Tipus) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Dpg ")
        sb.AppendLine("FROM CliDpgs")
        sb.AppendLine("WHERE Contact='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Cod=" & CInt(oTipus) & " ")
        sb.AppendLine("ORDER BY Dpg")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Dpg"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Sub Update(oContact As DTOContact, oTipus As DTOContact.Tipus, oDays As List(Of Integer), ByRef oTrans As SqlTransaction)
        Delete(oContact, oTipus, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliDpgs ")
        sb.AppendLine("WHERE Contact='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Cod=" & CInt(oTipus) & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each iDay As Integer In oDays
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Contact") = oContact.Guid
            oRow("Cod") = CInt(oTipus)
            oRow("Dpg") = iDay
        Next
        oDA.Update(oDs)
    End Sub

    Shared Sub Delete(oContact As DTOContact, oTipus As DTOContact.Tipus, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE CliDpgs ")
        sb.AppendLine("WHERE Contact='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Cod=" & CInt(oTipus) & " ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class
