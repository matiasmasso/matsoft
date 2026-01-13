Public Class UrlSegmentsLoader

    Shared Function All(target As DTOBaseGuid) As DTOUrlSegment.Collection
        Dim retval As New DTOUrlSegment.Collection
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Target, Segment, Lang, Canonical ")
        sb.AppendLine("FROM UrlSegment ")
        sb.AppendLine("WHERE Target = '" & target.Guid.ToString & "'")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOUrlSegment
            With item
                .Segment = oDrd("Segment")
                .Canonical = oDrd("Canonical")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Sub Delete(target As DTOBaseGuid, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE ")
        sb.AppendLine("FROM UrlSegment ")
        sb.AppendLine("WHERE Target='" & target.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub Update(target As DTOBaseGuid, segments As DTOUrlSegment.Collection, ByRef oTrans As SqlTransaction)
        Delete(target, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM UrlSegment ")
        sb.AppendLine("WHERE Target='" & target.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each segment In segments
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Target") = target.Guid
            oRow("Segment") = segment.Segment
            oRow("Lang") = SQLHelper.NullableLang(segment.Lang)
            oRow("Canonical") = segment.Canonical
        Next

        oDA.Update(oDs)
    End Sub
End Class
