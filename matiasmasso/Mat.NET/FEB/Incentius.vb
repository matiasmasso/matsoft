Public Class Incentiu
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOIncentiu)
        Return Await Api.Fetch(Of DTOIncentiu)(exs, "Incentiu", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oIncentiu As DTOIncentiu) As Boolean
        If Not oIncentiu.IsLoaded And Not oIncentiu.IsNew Then
            Dim pIncentiu = Api.FetchSync(Of DTOIncentiu)(exs, "Incentiu", oIncentiu.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOIncentiu)(pIncentiu, oIncentiu, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oIncentiu As DTOIncentiu) As Task(Of Boolean)
        Return Await Api.Update(Of DTOIncentiu)(oIncentiu, exs, "Incentiu")
        oIncentiu.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oIncentiu As DTOIncentiu) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOIncentiu)(oIncentiu, exs, "Incentiu")
    End Function

    Shared Async Function PurchaseOrders(exs As List(Of Exception), oIncentiu As DTOIncentiu, oUser As DTOUser) As Task(Of List(Of DTOPurchaseOrder))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrder))(exs, "Incentiu/PurchaseOrders", oIncentiu.Guid.ToString, oUser.Guid.ToString())
    End Function

    Shared Function PurchaseOrdersSync(exs As List(Of Exception), oIncentiu As DTOIncentiu, oUser As DTOUser) As List(Of DTOPurchaseOrder)
        Return Api.FetchSync(Of List(Of DTOPurchaseOrder))(exs, "Incentiu/PurchaseOrders", oIncentiu.Guid.ToString, oUser.Guid.ToString())
    End Function

    Shared Function ParticipantsSync(exs As List(Of Exception), oIncentiu As DTOIncentiu) As List(Of DTOContact)
        Return Api.FetchSync(Of List(Of DTOContact))(exs, "Incentiu/Participants", oIncentiu.Guid.ToString())
    End Function

    Shared Async Function PncProducts(exs As List(Of Exception), oIncentiu As DTOIncentiu) As Task(Of List(Of DTOProductSkuQtyEur))
        Return Await Api.Fetch(Of List(Of DTOProductSkuQtyEur))(exs, "Incentiu/PncProducts", oIncentiu.Guid.ToString())
    End Function

    Shared Function Url(oIncentiu As DTOIncentiu, Optional AbsoluteUrl As Boolean = False)
        Return UrlHelper.Factory(AbsoluteUrl, "promo", oIncentiu.Guid.ToString())
    End Function

    Shared Function UrlParticipants(oIncentiu As DTOIncentiu, Optional AbsoluteUrl As Boolean = False)
        Return UrlHelper.Factory(AbsoluteUrl, "promo", "salepoints", oIncentiu.Guid.ToString())
    End Function

    Shared Function ThumbnailUrl(oIncentiu As DTOIncentiu, Optional AbsoluteUrl As Boolean = False)
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.Incentiu, oIncentiu.Guid, AbsoluteUrl)
    End Function

    Shared Async Function ExcelFiresLocals(exs As List(Of Exception), oProveidor As DTOProveidor, oUser As DTOUser) As Task(Of MatHelper.Excel.Sheet)
        Dim oIncentiu = DTOIncentiu.Wellknown(DTOIncentiu.Wellknowns.FeriasLocales)
        Dim oOrders = Await Incentiu.PurchaseOrders(exs, oIncentiu, oUser)
        oOrders = oOrders.Where(Function(x) x.Items.Any(Function(y) DTOProduct.proveidor(y.Sku).Equals(oProveidor))).
            OrderByDescending(Function(z) z.Fch)

        Dim sTitle As String = "Colaboracions Fires Locals"
        Dim retval As New MatHelper.Excel.Sheet(sTitle)
        With retval
            .AddColumn("Data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Comanda", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Client", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Import", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        For Each oOrder In oOrders
            Dim DcEur As Decimal = oOrder.
                Items.
                Where(Function(x) DTOProduct.proveidor(x.Sku).Equals(oProveidor)).
                Sum(Function(y) y.Qty * y.Price.Eur * y.Dto / 100)

            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            oRow.AddCell(oOrder.Fch)
            oRow.AddCell(oOrder.Num, PurchaseOrder.Url(oOrder, True))
            oRow.AddCell(oOrder.Customer.FullNom)
            oRow.AddCell(DcEur)
        Next
        Return retval
    End Function

    Shared Async Function ExcelResults(exs As List(Of Exception), oIncentiu As DTOIncentiu) As Task(Of MatHelper.Excel.Sheet)
        Return Await Api.Fetch(Of MatHelper.Excel.Sheet)(exs, "Incentiu/ExcelResults", oIncentiu.Guid.ToString())
    End Function

    Shared Async Function ExcelDetall(exs As List(Of Exception), oIncentiu As DTOIncentiu, oUser As DTOUser) As Task(Of MatHelper.Excel.Sheet)
        Dim sTitle As String = oIncentiu.Title.Esp
        Dim retval As New MatHelper.Excel.Sheet("lineas de pedido", sTitle)
        With retval
            .AddColumn("Fecha", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Pedido", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Cliente", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Producto", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Cantidad", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Precio", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Descuento", MatHelper.Excel.Cell.NumberFormats.Percent)
            .AddColumn("Importe", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        Dim oOrders = Await Incentiu.PurchaseOrders(exs, oIncentiu, oUser)
        If exs.Count = 0 Then
            For Each oOrder In oOrders
                For Each item In oOrder.Items
                    Dim oRow As MatHelper.Excel.Row = retval.AddRow
                    oRow.AddCell(oOrder.Fch)
                    oRow.AddCell(oOrder.Num)
                    oRow.AddCell(oOrder.Customer.FullNom)
                    oRow.AddCell(item.Sku.RefYNomLlarg.Esp)
                    oRow.AddCell(item.Qty)
                    oRow.AddCellAmt(item.Price)
                    oRow.AddCell(item.Dto)
                    oRow.AddFormula("RC[-3]*RC[-2]*(100-RC[-1])/100")
                Next
            Next
        End If
        Return retval
    End Function

    Shared Async Function ExcelDeliveryAddresses(exs As List(Of Exception), oIncentiu As DTOIncentiu, oUser As DTOUser) As Task(Of MatHelper.Excel.Sheet)
        Return Await Api.Fetch(Of MatHelper.Excel.Sheet)(exs, "Incentiu/DeliveryAddresses", oIncentiu.Guid.ToString())
    End Function
End Class


Public Class Incentius
    Inherits _FeblBase

    Shared Function AllSync(exs As List(Of Exception), oUser As DTOUser, Optional BlIncludeObsolets As Boolean = False, Optional BlIncludeFutureIncentius As Boolean = False) As List(Of DTOIncentiu)
        Return Api.FetchSync(Of List(Of DTOIncentiu))(exs, "Incentius", oUser.Guid.ToString, OpcionalBool(BlIncludeObsolets), OpcionalBool(BlIncludeFutureIncentius))
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oUser As DTOUser, Optional BlIncludeObsolets As Boolean = False, Optional BlIncludeFutureIncentius As Boolean = False) As Task(Of List(Of DTOIncentiu))
        Return Await Api.Fetch(Of List(Of DTOIncentiu))(exs, "Incentius/Headers", oUser.Guid.ToString, OpcionalBool(BlIncludeObsolets), OpcionalBool(BlIncludeFutureIncentius))
    End Function

End Class
