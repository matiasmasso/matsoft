Public Class ProductUrlLoader

    Shared Function Search(url As String) As DTOProduct.ProductAndTab
        Dim retval As New DTOProduct.ProductAndTab
        Dim segments = url.Trim("/").Split("/").ToList

        retval.Tab = DTOProduct.Tab(segments.Last)
        If retval.Tab <> DTOProduct.Tabs.general Then
            segments.Remove(segments.Last)
        End If
        If segments.Count > 4 Then
            segments = segments.Take(4).ToList
        End If


        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwProductUrl.*, VwProductNom.* ")
        sb.AppendLine("FROM VwProductUrl ")
        sb.AppendLine("INNER JOIN VwProductNom ON VwProductUrl.Guid = VwProductNom.Guid ")
        sb.AppendLine("WHERE  ")
        sb.AppendLine("BrandSegment = '" & segments.First & "' ")



        Select Case segments.Count
            Case 1
                sb.AppendLine("AND DeptSegment IS NULL AND CategorySegment IS NULL AND SkuSegment IS NULL ")
            Case 2
                sb.AppendLine("AND ( ")
                sb.AppendLine("DeptSegment = '" & segments(1) & "' COLLATE SQL_Latin1_General_CP1_CI_AI AND CategorySegment IS NULL AND SkuSegment IS NULL OR ")
                sb.AppendLine("CategorySegment = '" & segments(1) & "' COLLATE SQL_Latin1_General_CP1_CI_AI AND SkuSegment IS NULL ")
                sb.AppendLine(") ")
            Case 3
                sb.AppendLine("AND ( ")
                sb.AppendLine("DeptSegment = '" & segments(1) & "' COLLATE SQL_Latin1_General_CP1_CI_AI AND CategorySegment = '" & segments(2) & "' COLLATE SQL_Latin1_General_CP1_CI_AI AND SkuSegment IS NULL OR ")
                sb.AppendLine("CategorySegment = '" & segments(1) & "' COLLATE SQL_Latin1_General_CP1_CI_AI AND SkuSegment = '" & segments(2) & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
                sb.AppendLine(") ")
            Case 4
                sb.AppendLine("AND DeptSegment = '" & segments(1) & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
                sb.AppendLine("AND CategorySegment = '" & segments(2) & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
                sb.AppendLine("AND SkuSegment = '" & segments(3) & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
        End Select
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval.Product = SQLHelper.GetProductFromDataReader(oDrd)
        End If
        oDrd.Close()
        Return retval
    End Function


End Class
