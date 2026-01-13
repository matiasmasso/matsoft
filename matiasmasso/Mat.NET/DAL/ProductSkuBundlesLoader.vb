Public Class ProductSkuBundlesLoader
    Shared Function All(oEmp As DTOEmp) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SkuBundle.Bundle AS BundleGuid, SkuBundle.Sku  ")
        sb.AppendLine(", SkuBundle.Qty, VwSkuRetail.Retail, Art.BundleDto ")
        sb.AppendLine("FROM SkuBundle ")
        sb.AppendLine("INNER JOIN Art ON SkuBundle.Bundle = Art.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuRetail ON SkuBundle.Sku = VwSkuRetail.SkuGuid ")
        sb.AppendLine("WHERE Art.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY SkuBundle.Bundle, SkuBundle.Ord ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oBundle As New DTOProductSku
        Do While oDrd.Read
            If Not oBundle.Guid.Equals(oDrd("BundleGuid")) Then
                oBundle = New DTOProductSku(oDrd("BundleGuid"))
                retval.Add(oBundle)
            End If
            Dim item As New DTOSkuBundle
            item.Sku = New DTOProductSku(oDrd("Sku"))
            item.Qty = oDrd("Qty")
            If Not IsDBNull(oDrd("Retail")) Then
                Dim DcRrpp = SQLHelper.GetDecimalFromDataReader(oDrd("Retail"))
                Dim DcDto = SQLHelper.GetDecimalFromDataReader(oDrd("BundleDto"))
                item.Rrpp = DcRrpp - Math.Round(DcRrpp * DcDto / 100, 2, MidpointRounding.AwayFromZero)
            End If
            oBundle.BundleSkus.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oSku As DTOProductSku, oCustomer As DTOCustomer) As List(Of DTOSkuBundle)
        'falta lligar el price, dto del client i repCom------------------------------------------------------------
        Dim retval As New List(Of DTOSkuBundle)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.*, SkuBundle.Qty, VwSkuRetail.Retail, Art.BundleDto ")
        sb.AppendLine("FROM SkuBundle ")
        sb.AppendLine("INNER JOIN VwSkuNom ON SkuBundle.Sku = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN Art ON SkuBundle.Bundle = Art.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuRetail ON SkuBundle.Sku = VwSkuRetail.SkuGuid ")
        sb.AppendLine("WHERE SkuBundle.Bundle = '" & oSku.Guid.ToString() & "' ")
        sb.AppendLine("ORDER BY SkuBundle.Ord ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSkuBundle
            item.Sku = SQLHelper.GetProductFromDataReader(oDrd)
            item.Qty = oDrd("Qty")
            If Not IsDBNull(oDrd("Retail")) Then
                Dim oRetail As DTOAmt = DTOAmt.Factory(oDrd("Retail"))
                item.Sku.rrpp = oRetail.deductPercent(SQLHelper.GetDecimalFromDataReader(oDrd("BundleDto")))
            End If
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
