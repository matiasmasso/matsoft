Public Class CustomerProduct

#Region "CRUD"
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCustomerProduct)
        Return Await Api.Fetch(Of DTOCustomerProduct)(exs, "CustomerProduct", oGuid.ToString())
    End Function

    Shared Function FindSync(oCustomer As DTOCustomer, oSku As DTOProductSku, sRef As String, exs As List(Of Exception)) As DTOCustomerProduct
        Return Api.FetchSync(Of DTOCustomerProduct)(exs, "CustomerProduct", oCustomer.Guid.ToString, oSku.Guid.ToString, sRef)
    End Function

    Shared Function Load(ByRef oCustomerProduct As DTOCustomerProduct, exs As List(Of Exception)) As Boolean
        If Not oCustomerProduct.IsLoaded And Not oCustomerProduct.IsNew Then
            Dim pCustomerProduct = Api.FetchSync(Of DTOCustomerProduct)(exs, "CustomerProduct", oCustomerProduct.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCustomerProduct)(pCustomerProduct, oCustomerProduct, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCustomerProduct As DTOCustomerProduct, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCustomerProduct)(oCustomerProduct, exs, "CustomerProduct")
        oCustomerProduct.IsNew = False
    End Function

    Shared Async Function Delete(oCustomerProduct As DTOCustomerProduct, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCustomerProduct)(oCustomerProduct, exs, "CustomerProduct")
    End Function

#End Region

    Shared Async Function SaveIfMissing(oCustomer As DTOCustomer, oSku As DTOProductSku, ref As String, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval = Await Api.Execute(Of String, Boolean)(ref, exs, "CustomerProduct/SaveIfMissing", oCustomer.Guid.ToString, oSku.Guid.ToString)
        Return retval
    End Function

    Shared Async Function UpdateElCorteIngles(exs As List(Of Exception), item As DTO.Integracions.ElCorteIngles.Cataleg) As Task(Of Boolean)
        Return Await Api.Execute(Of DTO.Integracions.ElCorteIngles.Cataleg)(item, exs, "CustomerProduct/UpdateElCorteIngles")
    End Function
End Class

Public Class CustomerProducts
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), Optional oCustomer As DTOCustomer = Nothing, Optional oSku As DTOProductSku = Nothing, Optional ref As String = "") As Task(Of List(Of DTOCustomerProduct))
        If ref = "" Then
            Return Await Api.Fetch(Of List(Of DTOCustomerProduct))(exs, "CustomerProducts", OpcionalGuid(oCustomer), OpcionalGuid(oSku))
        Else
            Return Await Api.Fetch(Of List(Of DTOCustomerProduct))(exs, "CustomerProducts", OpcionalGuid(oCustomer), OpcionalGuid(oSku), ref)
        End If
    End Function

    Shared Async Function Compact(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOCustomerProduct.Compact))
        Return Await Api.Fetch(Of List(Of DTOCustomerProduct.Compact))(exs, "CustomerProducts/Compact", oCustomer.Guid.ToString)
    End Function

    Shared Async Function FromRef(exs As List(Of Exception), oCustomer As DTOCustomer, ref As String) As Task(Of List(Of DTOCustomerProduct))
        Return Await Api.Execute(Of String, List(Of DTOCustomerProduct))(ref, exs, "CustomerProducts", oCustomer.Guid.ToString)
    End Function


    Shared Function AllSync(exs As List(Of Exception), Optional oCustomer As DTOCustomer = Nothing, Optional oSku As DTOProductSku = Nothing, Optional Ref As String = "") As List(Of DTOCustomerProduct)
        If Ref = "" Then
            Return Api.FetchSync(Of List(Of DTOCustomerProduct))(exs, "CustomerProducts", OpcionalGuid(oCustomer), OpcionalGuid(oSku))
        Else
            Return Api.FetchSync(Of List(Of DTOCustomerProduct))(exs, "CustomerProducts", OpcionalGuid(oCustomer), OpcionalGuid(oSku), Ref)
        End If
    End Function

    Shared Function Excel(items As List(Of DTOCustomerProduct)) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet("catalogo M+O")
        With retval
            .AddColumn("EAN", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("ref.cliente", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("ref.proveedor", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("ref.fabricante", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("marca", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("categoria", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("producto", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("obsoleto", ExcelHelper.Sheet.NumberFormats.W50)
        End With

        For Each item In items
            Dim oRow As ExcelHelper.Row = retval.AddRow()
            If item.Sku.Ean13 Is Nothing Then
                oRow.AddCell()
            Else
                oRow.AddCell(item.Sku.Ean13.Value)
            End If

            oRow.AddCell(item.Ref)
            oRow.AddCell(item.Sku.Id)
            oRow.AddCell(item.Sku.RefProveidor)
            oRow.addCell(item.sku.category.brand.nom.Esp)
            oRow.addCell(item.sku.category.nom.Esp)
            oRow.addCell(item.sku.nomLlarg.Esp)
            oRow.addCell(If(item.sku.obsoleto, "X", ""))
        Next
        Return retval
    End Function



End Class
