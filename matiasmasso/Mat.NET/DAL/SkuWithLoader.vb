Public Class SkuWithsLoader

    Shared Function Find(oSkuParent As DTOProductSku, oMgz As DTOMgz, DtFch As Date) As List(Of DTOSkuWith)
        Dim retval As New List(Of DTOSkuWith)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SkuWith.Child AS ProductGuid, SkuWith.Qty, VwSkuRetail.Retail ")
        sb.AppendLine(", Art.Art, Stk.Stock, Pnx.Pn2, Art.Obsoleto ")
        sb.AppendLine(", VwProductNom.Cod AS ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine("FROM SkuWith ")
        sb.AppendLine("INNER JOIN VwProductNom ON SkuWith.Child = VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN Art ON SkuWith.Child = Art.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT MgzGuid, ArtGuid, SUM(CASE WHEN Cod < 50 THEN Qty ELSE - Qty END) AS Stock FROM Arc GROUP BY MgzGuid, ArtGuid) Stk ON Stk.MgzGuid ='" & oMgz.Guid.ToString & "' AND Stk.ArtGuid = Art.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT ArtGuid, SUM(Pn2) AS Pn2 FROM PNC INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid WHERE Pdc.Cod = 2 GROUP BY ArtGuid HAVING SUM(Pn2) <> 0) Pnx ON Pnx.ArtGuid = Art.Guid ")

        sb.AppendLine("LEFT OUTER JOIN VwSkuRetail ON VwSkuRetail.SkuGuid = SkuWith.Child ")
        sb.AppendLine("WHERE SkuWith.Parent = '" & oSkuParent.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY SkuWith.Child, SkuWith.Qty, Art.Art, Stk.Stock, Pnx.Pn2, VwSkuRetail.Retail, Art.Obsoleto ")
        sb.AppendLine(", VwProductNom.Cod , VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine("ORDER BY Art.Obsoleto, VwProductNom.SkuNomLlarg ")


        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSkuChild As DTOProductSku = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ProductGuid"), oDrd("SkuNom"), oDrd("SkuNomLlarg"))
            With oSkuChild
                .Id = oDrd("Art")
                .Obsoleto = oDrd("Obsoleto")
                .Stock = Defaults.IntOrZeroIfNull(oDrd("Stock"))
                .Clients = Defaults.IntOrZeroIfNull(oDrd("Pn2"))
                If Not IsDBNull(oDrd("Retail")) Then
                    .RRPP = DTOAmt.Factory(CDec(oDrd("Retail")))
                End If
            End With
            Dim item As New DTOSkuWith
            With item
                .Parent = oSkuParent
                .Child = oSkuChild
                .Qty = oDrd("Qty")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(oParent As Guid, oChildren As List(Of GuidQty), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oParent, oChildren, oTrans)
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

    Shared Sub Update(oParent As Guid, oChildren As List(Of GuidQty), ByRef oTrans As SqlTransaction)
        Delete(oParent, oTrans)

        If oChildren.Count > 0 Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM SkuWith ")
            sb.AppendLine("WHERE SkuWith.Parent = '" & oParent.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            For Each item In oChildren
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Parent") = oParent
                With item
                    oRow("Child") = .Guid
                    oRow("Qty") = .Qty
                End With
            Next

            oDA.Update(oDs)
        End If
    End Sub


    Shared Function Delete(oParent As Guid, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oParent, oTrans)
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


    Shared Sub Delete(oParent As Guid, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE SkuWith ")
        sb.AppendLine("WHERE SkuWith.Parent = '" & oParent.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class
