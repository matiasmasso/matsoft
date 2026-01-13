Public Class Mgz
    Shared Async Function SetPrecioMedioCoste(oSku As DTOProductSku, oMgz As DTOMgz, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "mgz/SetPrecioMedioCoste", oMgz.Guid.ToString, oSku.Guid.ToString())
    End Function

    Shared Async Function DeliveryItems(oMgz As DTOMgz, year As Integer, exs As List(Of Exception)) As Task(Of List(Of DTODeliveryItem))
        Return Await Api.Fetch(Of List(Of DTODeliveryItem))(exs, "mgz/DeliveryItems", oMgz.Guid.ToString, year)
    End Function

    Shared Async Function Skus(oMgz As DTOMgz, DtFch As Date, exs As List(Of Exception)) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "mgz/inventory", oMgz.Guid.ToString, DtFch.ToString("yyyy-MM-dd"))
    End Function

    Shared Async Function Inventari(exs As List(Of Exception), oMgz As DTOMgz, oProveidor As DTOProveidor) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "mgz/inventari/fromProveidor", oMgz.Guid.ToString, oProveidor.Guid.ToString())
    End Function


    Shared Async Function Inventory(exs As List(Of Exception), oMgz As DTOMgz, oUser As DTOUser, Optional DtFch As Date = Nothing) As Task(Of List(Of DTOProductSku))
        If DtFch = Nothing Then DtFch = DateTime.Now
        Dim retval = Await Skus(oMgz, DtFch, exs)
        If exs.Count = 0 Then
            Dim oBrands As New List(Of DTOProductBrand)
            Dim oCategories As New List(Of DTOProductCategory)
            For Each item In retval
                Dim oCategory = oCategories.FirstOrDefault(Function(x) x.Equals(item.Category))
                If oCategory Is Nothing Then
                    oCategory = item.Category
                    Dim oBrand = oBrands.FirstOrDefault(Function(x) x.Equals(item.Category.Brand))
                    If oBrand Is Nothing Then
                        oBrand = item.Category.Brand
                        oBrands.Add(oBrand)
                    Else
                        oCategory.Brand = oBrand
                    End If
                    oCategories.Add(oCategory)
                Else
                    item.Category = oCategory
                End If
                'If item.Id = 20559 Then Stop '===========================================================
            Next
        End If
        Return retval
    End Function

    Shared Async Function ExcelStocks(oUser As DTOUser, oMgz As DTOMgz, exs As List(Of Exception)) As Task(Of ExcelHelper.Sheet)
        Dim retval As ExcelHelper.Sheet = Nothing

        If oUser.Contacts.Any(Function(x) x.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Carrefour))) Then
            retval = Await Carrefour.ExcelStocks(oMgz, exs)
        Else
            Dim oLang As DTOLang = oUser.lang
            Dim sTitle As String = "M+O Stocks " & TextHelper.VbFormat(DateTime.Now, "yyyy.MM.dd.hh.mm.ss")
            retval = New ExcelHelper.Sheet(sTitle, sTitle)
            With retval
                .AddColumn("Ref M+O", ExcelHelper.Sheet.NumberFormats.W50)
                .AddColumn("EAN", ExcelHelper.Sheet.NumberFormats.Integer)
                .AddColumn(oLang.Tradueix("Marca comercial", "Marca comercial", "Brand"), ExcelHelper.Sheet.NumberFormats.W50)
                .AddColumn(oLang.Tradueix("Categoría", "Categoria", "Category"), ExcelHelper.Sheet.NumberFormats.W50)
                .AddColumn(oLang.Tradueix("Producto", "Producte", "Product"), ExcelHelper.Sheet.NumberFormats.W50)
                .AddColumn("Stock", ExcelHelper.Sheet.NumberFormats.Integer)
            End With

            Dim items As List(Of DTOProductSku) = Await FEB2.Mgzs.Stocks(oUser, oMgz, exs)
            If exs.Count = 0 Then
                For Each item In items
                    Dim oRow = retval.AddRow
                    oRow.AddCell(item.Id)
                    oRow.AddCell(item.Ean13)
                    oRow.AddCell(item.BrandNom)
                    oRow.AddCell(item.CategoryNom())
                    oRow.AddCell(item.Nom)
                    oRow.AddCell(item.StockAvailable())
                Next
            End If
        End If

        Return retval
    End Function

    Shared Async Function ExcelInventari(exs As List(Of Exception), oMgz As DTOMgz, oUser As DTOUser, Optional DtFch As Date = Nothing) As Task(Of ExcelHelper.Sheet)
        Dim retval As ExcelHelper.Sheet = Nothing
        If DtFch = Nothing Then DtFch = DateTime.Now
        Dim oLang As DTOLang = oUser.lang
        Dim sTitle As String = "M+O Stocks " & TextHelper.VbFormat(DtFch, "yyyy.MM.dd.hh.mm.ss")
        retval = New ExcelHelper.Sheet(sTitle, sTitle)
        With retval
            .AddColumn("Ref M+O", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("EAN", ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn(oLang.Tradueix("Marca comercial", "Marca comercial", "Brand"), ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn(oLang.Tradueix("Categoría", "Categoria", "Category"), ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn(oLang.Tradueix("Producto", "Producte", "Product"), ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Stock", ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Valor", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Import", ExcelHelper.Sheet.NumberFormats.Euro)
        End With

        Dim items As List(Of DTOProductSku) = Await FEB2.Mgz.Inventory(exs, oMgz, oUser, DtFch)
        If exs.Count = 0 Then
            For Each item In items
                Dim oRow = retval.AddRow
                oRow.AddCell(item.Id)
                oRow.AddCell(item.Ean13)
                oRow.AddCell(item.BrandNom)
                oRow.AddCell(item.CategoryNom())
                oRow.AddCell(item.Nom)
                oRow.AddCell(item.StockAvailable())
                oRow.AddCell(item.Pmc())
                oRow.AddFormula("R[C-2]*R[C-1]")
            Next
        End If

        Return retval
    End Function

End Class
Public Class Mgzs

    Shared Async Function Stocks(oUser As DTOUser, oMgz As DTOMgz, exs As List(Of Exception)) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "mgz/stocks", oUser.Guid.ToString, oMgz.Guid.ToString())
    End Function

    Shared Async Function CustomStocks(oCustomer As DTOCustomer, oMgz As DTOMgz, exs As List(Of Exception)) As Task(Of List(Of DTOCustomerProduct))
        Return Await Api.Fetch(Of List(Of DTOCustomerProduct))(exs, "mgz/stocks/custom", oCustomer.Guid.ToString, oMgz.Guid.ToString())
    End Function

    Shared Async Function SetPrecioMedioCoste(oMgz As DTOMgz, exs As List(Of Exception)) As Task(Of DTOTaskResult)
        Return Await Api.Fetch(Of DTOTaskResult)(exs, "mgz/SetPrecioMedioCoste", oMgz.Guid.ToString())
    End Function

    Shared Async Function All(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOMgz))
        Return Await Api.Fetch(Of List(Of DTOMgz))(exs, "Mgzs", oEmp.Id)
    End Function

    Shared Async Function All(oSku As DTOProductSku, exs As List(Of Exception)) As Task(Of List(Of DTOMgz))
        Return Await Api.Fetch(Of List(Of DTOMgz))(exs, "Mgzs/FromSku", oSku.Guid.ToString())
    End Function

    Shared Async Function Actius(oEmp As DTOEmp, DtFch As Date, exs As List(Of Exception)) As Task(Of List(Of DTOMgz))
        Return Await Api.Fetch(Of List(Of DTOMgz))(exs, "Mgzs/Actius", oEmp.Id, DtFch.ToString("yyyy-MM-dd"))
    End Function

End Class
