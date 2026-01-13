Public Class DTOExcelPOMiFarma
    Inherits MatHelper.Excel.Sheet

    Public Enum Cols
        Dsc
        Qty
        Ean
        Ref
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Shared Function PurchaseOrder(oSheet As MatHelper.Excel.Sheet, oCustomerSkus As List(Of DTOProductSku), oTarifaDtos As List(Of DTOCustomerTarifaDto), oCliProductDtos As List(Of DTOCliProductDto), oUser As DTOUser) As DTOPurchaseOrder
        oCustomerSkus = oCustomerSkus.Where(Function(x) x.Ean13 IsNot Nothing).ToList
        Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.miFarma)
        'Dim sConcept = oSheet.Filename.Replace(".xlsx", "")
        Dim sConcept As String = ""
        If oSheet.Rows.Count > 1 Then
            Dim oRow = oSheet.Rows(1)
            If oRow.Cells.Count > 3 Then
                Dim oCell = oRow.Cells(3)
                sConcept = oCell.Content
            End If
        End If

        Dim retval = DTOPurchaseOrder.Factory(oCustomer, oUser, DTO.GlobalVariables.Today(), DTOPurchaseOrder.Sources.eMail, sConcept)
        For Each oRow In oSheet.Rows
            If oSheet.Rows.IndexOf(oRow) > 0 Then
                Dim item = PurchaseOrderItem(oRow, retval, oCustomerSkus, oTarifaDtos, oCliProductDtos)
                retval.Items.Add(item)
            End If
        Next
        Return retval
    End Function

    Shared Function PurchaseOrderItem(oRow As MatHelper.Excel.Row, oOrder As DTOPurchaseOrder, oCustomerSkus As List(Of DTOProductSku), oTarifaDtos As List(Of DTOCustomerTarifaDto), oCliProductDtos As List(Of DTOCliProductDto)) As DTOPurchaseOrderItem
        Dim retval As New DTOPurchaseOrderItem()
        With retval
            .Sku = Sku(oRow, oCustomerSkus)
            .Qty = oRow.GetInt(Cols.Qty)
            .Pending = .Qty
            .Price = .Sku.Price()

            Dim oCost As DTOAmt = DTOProductSku.GetCustomerCost(.Sku, oTarifaDtos)
            If oCost Is Nothing Then
                .Price = DTOAmt.Factory(0)
                .Dto = 0
            Else
                .Price = oCost
                Dim oDto As DTOCliProductDto = .Sku.CliProductDto(oCliProductDtos)
                If oDto IsNot Nothing Then .Dto = oDto.Dto
            End If

        End With
        Return retval
    End Function

    Shared Function Sku(oRow As MatHelper.Excel.Row, oInventari As List(Of DTOProductSku)) As DTOProductSku
        Dim sEan As String = oRow.GetString(Cols.Ean)
        Dim retval = oInventari.FirstOrDefault(Function(x) x.Ean13.Value = sEan)
        If retval Is Nothing Then
            retval = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.UnknownSku)
            With retval
                .NomLlarg.Esp = String.Format("Ean {0} fora de cataleg ({1})", sEan, oRow.GetString(Cols.Dsc))
                .obsoleto = True
            End With
        End If
        Return retval
    End Function

End Class
