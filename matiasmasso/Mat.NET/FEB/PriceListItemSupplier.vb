
Public Class PriceListItemSupplier
    Inherits _FeblBase


    Shared Async Function Update(exs As List(Of Exception), oPriceListItemSupplier As DTOPriceListItem_Supplier) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPriceListItem_Supplier)(oPriceListItemSupplier, exs, "PriceListItemSupplier")
        oPriceListItemSupplier.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oPriceListItemSupplier As DTOPriceListItem_Supplier) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPriceListItem_Supplier)(oPriceListItemSupplier, exs, "PriceListItemSupplier")
    End Function

    Shared Async Function FromSku(exs As List(Of Exception), oSku As DTOProductSku, Optional DtFch As Date = Nothing) As Task(Of DTOPriceListItem_Supplier)
        Dim retval As DTOPriceListItem_Supplier = Nothing
        If oSku IsNot Nothing Then
            If oSku.RefProveidor > "" And oSku.Category IsNot Nothing Then
                If oSku.Category.Brand IsNot Nothing Then
                    If oSku.Category.Brand.Proveidor IsNot Nothing Then
                        retval = Await FromSku(exs, oSku.Category.Brand.Proveidor, oSku.RefProveidor, DtFch)
                    End If
                End If
            End If
        End If
        Return retval
    End Function

    Shared Async Function FromSku(exs As List(Of Exception), oProveidor As DTOProveidor, sRef As String, Optional DtFch As Date = Nothing) As Task(Of DTOPriceListItem_Supplier)
        Return Await Api.Execute(Of String, DTOPriceListItem_Supplier)(sRef, exs, "PriceListItemSupplier/fromSku", oProveidor.Guid.ToString, FormatFch(DtFch))
    End Function

    Shared Async Function GetPreusDeCost(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of DTOPriceListItem_Supplier)
        Dim oBrand = Product.Brand(exs, oSku)

        Dim oProveidor As DTOProveidor = oBrand.Proveidor
        Dim DtFch As Date = DTO.GlobalVariables.Today()
        Dim retval = Await PriceListItemSupplier.FromSku(exs, oProveidor, oSku.RefProveidor, DtFch)
        Return retval
    End Function
End Class

Public Class PriceListItemsSupplier
    Inherits _FeblBase

    Shared Async Function Vigent(exs As List(Of Exception), oProveidor As DTOProveidor) As Task(Of List(Of DTOPriceListItem_Supplier))
        Return Await Api.Fetch(Of List(Of DTOPriceListItem_Supplier))(exs, "PriceListItemsSupplier/Vigent", oProveidor.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oProductSku As DTOProductSku) As Task(Of List(Of DTOPriceListItem_Supplier))
        Return Await Api.Fetch(Of List(Of DTOPriceListItem_Supplier))(exs, "PriceListItemsSupplier", oProductSku.Guid.ToString())
    End Function

    Shared Async Function Delete(exs As List(Of Exception), values As List(Of DTOPriceListItem_Supplier)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTOPriceListItem_Supplier))(values, exs, "PriceListItemsSupplier")
    End Function

    Shared Async Function FromExcelSheet(exs As List(Of Exception), oPriceList As DTOPriceListSupplier, oSheet As MatHelper.Excel.Sheet) As Task(Of List(Of DTOPriceListItem_Supplier))
        'REF / EAN / DESCRIPTION / PRICE / RETAIL / INNER PACK
        Dim retval As New List(Of DTOPriceListItem_Supplier)
        'Dim oProveidorSkus As List(Of DTOProductSku) = ProductSkus.All(Proveidor:=oPriceList.Proveidor)
        Dim oProveidorSkus = Await ProductSkus.All(exs, oPriceList.Proveidor)

        Dim IsHeaderRow As Boolean
        If oSheet.Rows.Count > 0 Then
            If oSheet.Rows(0).Cells.Count > 3 Then
                IsHeaderRow = Not TextHelper.VbIsNumeric(oSheet.rows(0).cells(3).content)
            End If
        End If

        For Each oRow As MatHelper.Excel.Row In oSheet.Rows
            If IsHeaderRow Then
                IsHeaderRow = False
            Else
                If oRow.Cells.Count > 3 Then
                    If oRow.Cells(0).Content <> Nothing Then
                        Dim sRef As String = oRow.Cells(0).Content.Trim
                        If sRef > "" Then

                            Dim oItem As New DTOPriceListItem_Supplier
                            With oItem
                                .Parent = oPriceList
                                .Ref = sRef
                                .EAN = oRow.Cells(1).Content
                                .Description = oRow.Cells(2).content
                                If TextHelper.VbIsNumeric(oRow.cells(3).content) Then
                                    .Price = CDec(oRow.cells(3).content)
                                Else
                                    .Price = 0
                                End If

                                .Sku = oProveidorSkus.FirstOrDefault(Function(x) x.RefProveidor = sRef)
                                If oRow.Cells.Count > 4 Then
                                    If TextHelper.VbIsNumeric(oRow.cells(3 + 1).content) Then
                                        .Retail = CDec(oRow.cells(3 + 1).content)
                                    Else
                                        .Retail = 0
                                    End If
                                End If
                                If oRow.Cells.Count > 5 Then
                                    If TextHelper.VbIsNumeric(oRow.cells(4 + 1).content) Then
                                        .InnerPack = CInt(oRow.cells(4 + 1).content)
                                    Else
                                        .InnerPack = 0
                                    End If
                                End If
                            End With
                            If oItem.Price = 0 And oItem.Retail = 0 Then
                            Else
                                retval.Add(oItem)
                            End If
                        End If
                    End If
                End If
            End If
        Next
        Return retval
    End Function


End Class

