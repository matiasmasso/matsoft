Public Class ImportValidacioLoader

End Class

Public Class ImportValidacionsLoader

    Shared Sub Load(oImportacio As DTOImportacio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ImportValidacio.Guid, ImportValidacio.Sku, ImportValidacio.Lin, ImportValidacio.Qty, ImportValidacio.Cfm, Art.Art ")
        sb.AppendLine(", Product2.Cod as ProductCod, Product2.BrandGuid, Product2.BrandNom, Product2.CategoryGuid, Product2.CategoryNom, Product2.SkuNom, Product2.SkuMyd ")
        sb.AppendLine("FROM ImportValidacio ")
        sb.AppendLine("INNER JOIN Product2 ON ImportValidacio.Sku = Product2.Guid ")
        sb.AppendLine("INNER JOIN Art ON ImportValidacio.Sku = Art.Guid ")
        sb.AppendLine("WHERE Importacio='" & oImportacio.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ImportValidacio.Lin ")

        oImportacio.Validacions = New List(Of DTOImportValidacio)
        Dim oOrder As New DTOPurchaseOrder
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOImportValidacio(oDrd("Guid"))
            With item
                .Lin = oDrd("Lin")
                .Qty = oDrd("Qty")
                .Cfm = oDrd("Cfm")
                .Sku = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), CType(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Sku"), oDrd("SkuNom"), oDrd("SkuMyd"))
                .Sku.Id = oDrd("Art")
            End With
            oImportacio.Validacions.Add(item)
        Loop

        oDrd.Close()

    End Sub

    Shared Function Update(oImportacio As DTOImportacio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oImportacio, oTrans)
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


    Shared Sub Update(oImportacio As DTOImportacio, ByRef oTrans As SqlTransaction)
        Delete(oImportacio, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ImportValidacio ")
        sb.AppendLine("WHERE Importacio='" & oImportacio.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Lin ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item As DTOImportValidacio In oImportacio.Validacions
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            If item.Lin = 0 Then
                Dim iMaxLin As Integer = oImportacio.Validacions.Max(Function(x) x.Lin)
                item.Lin = iMaxLin + 1
            End If
            oRow("Guid") = item.Guid
            oRow("Importacio") = oImportacio.Guid
            oRow("lin") = item.Lin
            oRow("Qty") = item.Qty
            oRow("Sku") = item.Sku.Guid
            oRow("Cfm") = item.Cfm
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oValidacions As List(Of DTOImportValidacio), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oValidacions, oTrans)
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


    Shared Sub Delete(oValidacions As List(Of DTOImportValidacio), ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE ImportValidacio ")
        sb.AppendLine("WHERE ( ")
        For Each Item As DTOImportValidacio In oValidacions
            If Item.UnEquals(oValidacions.First) Then sb.Append(" OR ")
            sb.AppendLine("Guid='" & Item.Guid.ToString & "' ")
        Next
        sb.AppendLine(" ) ")

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Delete(oImportacio As DTOImportacio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oImportacio, oTrans)
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


    Shared Sub Delete(oImportacio As DTOImportacio, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE ImportValidacio ")
        sb.AppendLine("WHERE Importacio='" & oImportacio.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class
