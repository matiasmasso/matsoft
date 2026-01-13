Public Class ProductStocks
    Shared Async Function FromUserOrCustomer(exs As List(Of Exception), oEmp As DTOEmp, oUserOrCustomer As DTOBaseGuid, oMgz As DTOMgz) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "ProductStocks/FromUserOrCustomer", oEmp.Id, oUserOrCustomer.Guid.ToString, oMgz.Guid.ToString())
    End Function

    Shared Async Function Custom(exs As List(Of Exception), oEmp As DTOEmp, oCustomer As DTOCustomer) As Task(Of List(Of DTOCustomerProduct))
        Return Await Api.Fetch(Of List(Of DTOCustomerProduct))(exs, "ProductStocks/Custom", oEmp.Id, oCustomer.Guid.ToString())
    End Function

    Shared Async Function Skus(exs As List(Of Exception), oMgz As DTOMgz) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "ProductStocks/Skus", oMgz.Guid.ToString())
    End Function

    Shared Async Function Excel(exs As List(Of Exception), oEmp As DTOEmp, oUserOrCustomer As DTOBaseGuid) As Task(Of ExcelHelper.Sheet)
        Dim sTitle As String = "M+O Stocks " & TextHelper.VbFormat(DateTime.Now, "yyyy.MM.dd.hh.mm.ss")
        Dim retval As New ExcelHelper.Sheet(sTitle, sTitle)
        Dim oMgz As DTOMgz = oEmp.Mgz

        Dim oCustomers As New List(Of DTOCustomer)
        Dim oLang As DTOLang = DTOApp.current.lang
        If TypeOf oUserOrCustomer Is DTOCustomer Then
            oCustomers.Add(oUserOrCustomer)
            oLang = If(oCustomers.First.Lang, DTOApp.current.lang)
        ElseIf TypeOf oUserOrCustomer Is DTOUser Then
            Dim oUser As DTOUser = oUserOrCustomer
            oCustomers = Await FEB2.User.GetCustomers(oUser, exs)
            oLang = If(oCustomers.First.Lang, DTOApp.current.lang)
        End If

        If oCustomers.Any(Function(x) x.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Carrefour))) Then
            retval = Await FEB2.Carrefour.ExcelStocks(oEmp.Mgz, exs)
        Else
            Dim oSkus = Await FEB2.ProductStocks.FromUserOrCustomer(exs, oEmp, oUserOrCustomer, oMgz)
            With retval
                .AddColumn("Ref M+O", ExcelHelper.Sheet.NumberFormats.W50)
                .AddColumn("EAN", ExcelHelper.Sheet.NumberFormats.Integer)
                .AddColumn(oLang.Tradueix("Marca comercial", "Marca comercial", "Brand"), ExcelHelper.Sheet.NumberFormats.W50)
                .AddColumn(oLang.Tradueix("Categoría", "Categoria", "Category"), ExcelHelper.Sheet.NumberFormats.W50)
                .AddColumn(oLang.Tradueix("Producto", "Producte", "Product"), ExcelHelper.Sheet.NumberFormats.W50)
                .AddColumn("Stock", ExcelHelper.Sheet.NumberFormats.Integer)
            End With

            For Each item In oskus
                Dim oRow = retval.AddRow
                oRow.AddCell(item.Id)
                oRow.AddCell(DTOProductSku.Ean(item))
                oRow.AddCell(DTOProduct.BrandNom(item))
                oRow.AddCell(DTOProduct.CategoryNom(item))
                oRow.AddCell(item.Nom)
                oRow.AddCell(DTOProductSku.StockAvailable(item))
            Next
        End If

        Return retval
    End Function

End Class
