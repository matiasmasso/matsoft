Public Class VivaceLoader


    Shared Function Refs() As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("WHERE VwSkuNom.Emp=1 AND VwSkuNom.Obsoleto=0 AND BrandNom<>'varios' ")
        sb.AppendLine("ORDER BY VwSkuNom.SkuId ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku = SQLHelper.GetProductFromDataReader(oDrd)
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
