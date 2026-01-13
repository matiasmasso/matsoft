Public Class CustomerProduct
#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOCustomerProduct
        Dim retval As DTOCustomerProduct = CustomerProductLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Find(oCustomer As DTOCustomer, oSku As DTOProductSku, sRef As String) As DTOCustomerProduct
        Dim retval As DTOCustomerProduct = CustomerProductLoader.Find(oCustomer, oSku, sRef)
        Return retval
    End Function

    Shared Function Update(oCustomerProduct As DTOCustomerProduct, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CustomerProductLoader.Update(oCustomerProduct, exs)
        Return retval
    End Function

    Shared Function Delete(oCustomerProduct As DTOCustomerProduct, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CustomerProductLoader.Delete(oCustomerProduct, exs)
        Return retval
    End Function
#End Region

    Shared Function UpdateElCorteIngles(exs As List(Of Exception), item As DTO.Integracions.ElCorteIngles.Cataleg) As Boolean
        Return CustomerProductLoader.UpdateElCorteIngles(exs, item)
    End Function

    Shared Function SaveIfMissing(oCustomer As DTOCustomer, oSku As DTOProductSku, sRef As String, exs As List(Of Exception))
        If oSku IsNot Nothing Then
            Dim oCustomerProduct As DTOCustomerProduct = CustomerProduct.Find(oCustomer, oSku, sRef)
            If oCustomerProduct Is Nothing Then
                oCustomerProduct = New DTOCustomerProduct
                With oCustomerProduct
                    .Customer = oCustomer
                    .Sku = oSku
                    .Ref = sRef
                End With
                CustomerProduct.Update(oCustomerProduct, exs)
            End If
        End If
        Return exs.Count = 0
    End Function

    Shared Function Dun14(oCustomerProduct As DTOCustomerProduct) As String
        Dim retval As String = ""
        If oCustomerProduct IsNot Nothing Then
            retval = oCustomerProduct.DUN14
        End If
        Return retval
    End Function
End Class


Public Class CustomerProducts

    Shared Function All(Optional oCustomer As DTOCustomer = Nothing, Optional oSku As DTOProductSku = Nothing, Optional sRef As String = "") As List(Of DTOCustomerProduct)
        Dim retval As List(Of DTOCustomerProduct) = CustomerProductsLoader.All(oCustomer, oSku, sRef)
        Return retval
    End Function

    Shared Function Compact(oCustomer As DTOCustomer) As List(Of DTOCustomerProduct.Compact)
        Dim retval As List(Of DTOCustomerProduct.Compact) = CustomerProductsLoader.Compact(oCustomer)
        Return retval
    End Function

    Shared Function SaveIfMissing(items As List(Of DTOCustomerProduct), exs As List(Of Exception)) As Boolean
        Return CustomerProductsLoader.SaveIfMissing(items, exs)
    End Function


    Shared Function Delete(exs As List(Of Exception), oGuids As List(Of Guid)) As Boolean
        Return CustomerProductsLoader.Delete(exs, oGuids)
    End Function


    Shared Function Excel(items As List(Of DTOCustomerProduct)) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("catalogo M+O")
        With retval
            .AddColumn("EAN", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("ref.cliente", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("ref.proveedor", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("ref.fabricante", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("marca", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("categoria", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("producto", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("obsoleto", MatHelper.Excel.Cell.NumberFormats.W50)
        End With

        For Each item In items
            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            If item.Sku.ean13 Is Nothing Then
                oRow.AddCell()
            Else
                oRow.AddCell(item.Sku.ean13.value)
            End If

            oRow.AddCell(item.Ref)
            oRow.AddCell(item.Sku.id)
            oRow.AddCell(item.Sku.refProveidor)
            oRow.addCell(item.sku.category.brand.nom.Esp)
            oRow.addCell(item.sku.category.nom.Esp)
            oRow.addCell(item.sku.nomLlarg.Esp)
            oRow.AddCell(IIf(item.Sku.obsoleto, "X", ""))
        Next
        Return retval
    End Function

End Class