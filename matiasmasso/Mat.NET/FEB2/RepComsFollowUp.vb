Public Class RepComsFollowUp

    Shared Async Function Excel(oRep As DTORep, exs As List(Of Exception)) As Task(Of ExcelHelper.Sheet)
        Dim retval As New ExcelHelper.Sheet(oRep.NickName, String.Format("M+O Seguimiento comisiones {0}", oRep.NickName))
        With retval
            .AddColumn("Ejercicio")
            .AddColumn("Mes")
            .AddColumn("Dia")
            .AddColumn("Pedido", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Servido", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Facturado", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Liquidado", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Num.Registro")
            .AddColumn("Cliente")
            .AddColumn("Concepto")
        End With
        Dim oOrders = Await FEB2.Rep.Archive(oRep, exs)
        For Each oOrder In oOrders

            Dim oOrderedItems As List(Of DTOPurchaseOrderItem) = oOrder.Items
            Dim oDeliveredItems = oOrderedItems.SelectMany(Function(x) x.Deliveries).ToList
            Dim oInvoicedItems = oDeliveredItems.Where(Function(x) x.Delivery.Invoice IsNot Nothing).ToList
            Dim oLiquidatedItems = oInvoicedItems.Where(Function(x) x.RepLiq IsNot Nothing)

            Dim DcPedido As Decimal = oOrderedItems.Sum(Function(x) x.Amount.Eur)
            Dim DcServido As Decimal = oDeliveredItems.Sum(Function(x) x.Import.Eur)
            Dim DcFacturado As Decimal = oInvoicedItems.Sum(Function(x) x.Import.Eur)
            Dim DcLiquidado As Decimal = oLiquidatedItems.Sum(Function(x) x.Import.Eur)

            Dim oRow = retval.AddRow
            oRow.AddCell(oOrder.Fch.Year)
            oRow.AddCell(DTOLang.ESP.MesAbr(oOrder.Fch.Month))
            oRow.AddCell(oOrder.Fch.Day)
            oRow.AddCellAmt(DTOAmt.Factory(DcPedido))
            oRow.AddCellAmt(DTOAmt.Factory(DcServido))
            oRow.AddCellAmt(DTOAmt.Factory(DcFacturado))
            oRow.AddCellAmt(DTOAmt.Factory(DcLiquidado))
            oRow.AddCell(oOrder.FormattedId, FEB2.PurchaseOrder.Url(oOrder, True))
            oRow.AddCell(oOrder.Customer.FullNom)
            oRow.AddCell(oOrder.Concept)
        Next
        Return retval
    End Function
End Class
