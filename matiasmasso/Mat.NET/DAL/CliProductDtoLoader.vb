Public Class CliProductDtoLoader
End Class
Public Class CliProductDtosLoader

#Region "CRUD"

    Shared Function All(ByRef oCustomer As DTOCustomer) As List(Of DTOCliProductDto)
        Dim retval As New List(Of DTOCliProductDto)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliDto.Brand, CliDto.Dto ")
        sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        sb.AppendLine("FROM CliDto ")
        sb.AppendLine("INNER JOIN VwProductNom ON CliDto.Brand = VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON CliDto.Customer = VwCcxOrMe.Ccx ")
        sb.AppendLine("WHERE VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "'")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCliProductDto
            With item
                .Customer = oCustomer
                .Product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Brand"), oDrd("SkuNom"))
                .Dto = oDrd("Dto")
            End With
            retval.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oCustomer As DTOCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCustomer, oTrans)
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


    Shared Sub Update(oCustomer As DTOCustomer, ByRef oTrans As SqlTransaction)
        Delete(oCustomer, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliDto ")
        sb.AppendLine("WHERE CliDto.Customer = '" & oCustomer.Guid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOCliProductDto In oCustomer.ProductDtos
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With item
                oRow("Customer") = oCustomer.Guid
                oRow("Brand") = item.Product.Guid
                oRow("Dto") = item.Dto
            End With
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
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE CliDto ")
        sb.AppendLine("WHERE CliDto.Customer = '" & oCustomer.Guid.ToString & "'")
        Dim sql As String = sb.ToString
        SQLHelper.ExecuteNonQuery(sql, oTrans)
    End Sub

#End Region

End Class
