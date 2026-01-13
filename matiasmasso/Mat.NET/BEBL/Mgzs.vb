Public Class Mgz
    Shared Function ExcelStocks(oEmp As DTOEmp, oUser As DTOUser, oMgz As DTOMgz) As MatHelper.Excel.Sheet
        Dim retval As MatHelper.Excel.Sheet = Nothing

        If oUser.contacts.Any(Function(x) x.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Carrefour))) Then
            retval = Carrefour.ExcelStocks(oMgz)
        Else
            Dim oLang As DTOLang = oUser.lang
            Dim sTitle As String = "M+O Stocks " & Format(Now, "yyyy.MM.dd.hh.mm.ss")
            retval = New MatHelper.Excel.Sheet(sTitle, sTitle)
            With retval
                .AddColumn("Ref M+O", MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn("EAN", MatHelper.Excel.Cell.NumberFormats.Integer)
                .AddColumn(oLang.Tradueix("Marca comercial", "Marca comercial", "Brand"), MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn(oLang.Tradueix("Categoría", "Categoria", "Category"), MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn(oLang.Tradueix("Producto", "Producte", "Product"), MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn("Stock", MatHelper.Excel.Cell.NumberFormats.Integer)
            End With

            Dim items As List(Of DTOProductSku) = BEBL.Mgz.Stocks(oEmp, oUser, oMgz)
            For Each item In items
                Dim oRow = retval.AddRow
                oRow.AddCell(item.id)
                oRow.AddCell(DTOProductSku.Ean(item))
                oRow.AddCell(item.BrandNom)
                oRow.AddCell(item.CategoryNom())
                oRow.addCell(item.Nom.Tradueix(oLang))
                oRow.AddCell(item.StockAvailable())
            Next
        End If

        Return retval
    End Function

    Shared Function Stocks(oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = MgzLoader.Stocks(oMgz)
        Return retval
    End Function

    Shared Function InvRpt(oCustomer As DTOCustomer, oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim oTarifa = BEBL.CustomerTarifa.Load(oCustomer)
        Dim oStocks As List(Of DTOProductSku) = MgzLoader.Stocks(oMgz)
        For Each oSku In oTarifa.Skus
            Dim oSkuStock As DTOProductSku = oStocks.FirstOrDefault(Function(x) x.Guid.Equals(oSku.Guid))
            If oSkuStock IsNot Nothing Then
                oSku.stock = oSkuStock.StockAvailable()
            End If
            If oSku.price IsNot Nothing And oSku.ean13 IsNot Nothing Then
                retval.Add(oSku.ToProductSku())
            End If
        Next
        Return retval
    End Function

    Shared Function Inventari(oProveidor As DTOProveidor, oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductStocksLoader.FromProveidor(oProveidor, oMgz)
        Return retval
    End Function

    Shared Function Inventory(oMgz As DTOMgz, Optional DtFch As Date = Nothing) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = MgzLoader.Inventory(oMgz, DtFch)
        Return retval
    End Function


    Shared Function Skus(oMgz As DTOMgz) As List(Of DTOProductSku)
        Return MgzLoader.Skus(oMgz)
    End Function


    Shared Function SetPrecioMedioCoste(oSku As DTOProductSku, oMgz As DTOMgz, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = MgzLoader.SetPrecioMedioCoste(oMgz, oSku, exs)
        Return retval
    End Function

    Shared Function DeliveryItems(oMgz As DTOMgz, iYear As Integer) As List(Of DTODeliveryItem)
        Dim retval As List(Of DTODeliveryItem) = MgzLoader.DeliveryItems(oMgz, iYear)
        Return retval
    End Function

    Shared Function Stocks(oEmp As DTOEmp, oUser As DTOUser, oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductStocksLoader.FromUserOrCustomer(oEmp, oUser, oMgz)
        Return retval
    End Function

    Shared Function StocksCustom(oCustomer As DTOCustomer, oMgz As DTOMgz) As List(Of DTOCustomerProduct)
        Dim retval As List(Of DTOCustomerProduct) = ProductStocksLoader.Custom(oCustomer, oMgz)
        Return retval
    End Function

    Shared Function SetPrecioMedioCoste(oMgz As DTOMgz, exs As List(Of Exception)) As Boolean
        Try

            Dim oSkus As List(Of DTOProductSku) = BEBL.Mgz.Skus(oMgz)
            Dim iCount As Integer = oSkus.Count
            Dim idx = 0
            For Each oSku In oSkus
                idx += 1
                BEBL.Mgz.SetPrecioMedioCoste(oSku, oMgz, exs)
            Next
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return exs.Count = 0
    End Function

End Class

Public Class Mgzs

    Shared Function All(oEmp As DTOEmp) As List(Of DTOMgz)
        Dim retval As List(Of DTOMgz) = MgzsLoader.All(oEmp)
        Return retval
    End Function

    Shared Function All(oSku As DTOProductSku) As List(Of DTOMgz)
        Dim retval As List(Of DTOMgz) = MgzsLoader.All(oSku)
        Return retval
    End Function

    Shared Function Actius(oEmp As DTOEmp, DtFch As Date) As List(Of DTOMgz)
        Dim retval As List(Of DTOMgz) = MgzsLoader.Actius(oEmp,, DtFch)
        Return retval
    End Function

End Class
