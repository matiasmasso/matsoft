Public Class CustomerBrandDtoLoader

End Class

Public Class CustomerBrandDtosLoader
    Shared Function All(oCustomer As DTOCustomer) As List(Of DTOProductDto)
        Dim retval As New List(Of DTOProductDto)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliDto.Dto, CliDto.Brand AS SkuGuid ")
        sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        sb.AppendLine("FROM CliDto ")
        sb.AppendLine("INNER JOIN VwProductNom ON CliDto.Brand = VwProductNom.Guid ")
        sb.AppendLine("WHERE Customer = '" & oCustomer.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim item As New DTOProductDto
            With item
                .Product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("SkuGuid"), oDrd("SkuNom"))
                Select Case .Product.SourceCod
                    Case DTOProduct.SourceCods.Brand
                        .Product.Nom.Esp = oDrd("BrandNom")
                    Case DTOProduct.SourceCods.Category
                        .Product.Nom.Esp = oDrd("BrandNom") & "/" & oDrd("CategoryNom")
                    Case DTOProduct.SourceCods.Sku
                        .Product.Nom.Esp = oDrd("BrandNom") & "/" & oDrd("CategoryNom") & "/" & oDrd("SkuNom")
                End Select
                .Dto = oDrd("Dto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oCustomer As DTOCustomer, oDtos As List(Of DTOProductDto), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCustomer, oDtos, oTrans)
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

    Shared Sub Update(oCustomer As DTOCustomer, oDtos As List(Of DTOProductDto), oTrans As SqlTransaction)
        Delete(oCustomer, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliDto ")
        sb.AppendLine("WHERE Customer='" & oCustomer.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOProductDto In oDtos
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Customer") = oCustomer.Guid
            oRow("Brand") = item.Product.Guid
            oRow("Dto") = item.Dto
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCustomer As DTOCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCustomer, oTrans)
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

    Shared Sub Delete(oCustomer As DTOCustomer, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliDto WHERE Customer='" & oCustomer.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub



End Class
