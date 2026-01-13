Public Class CustomerTarifa
    Inherits _FeblBase

    Shared Async Function Load(exs As List(Of Exception), oUserOrCustomer As DTOBaseGuid, Optional DtFch As Date = Nothing) As Task(Of DTOCustomerTarifa)
        Dim retval = Await Api.Fetch(Of DTOCustomerTarifa)(exs, "CustomerTarifa/Load", OpcionalGuid(oUserOrCustomer), FormatFch(DtFch))
        For Each oBrand In retval.Brands
            For Each oCategory In oBrand.Categories
                oCategory.Brand = oBrand
                For Each oSku In oCategory.Skus
                    oSku.Category = oCategory
                Next
            Next
        Next
        Return retval
    End Function

    Shared Function LoadSync(exs As List(Of Exception), oUserOrCustomer As DTOBaseGuid, Optional DtFch As Date = Nothing, Optional oBrand As DTOProductBrand = Nothing) As DTOCustomerTarifa
        Dim retval = Api.FetchSync(Of DTOCustomerTarifa)(exs, "CustomerTarifa/Load", OpcionalGuid(oUserOrCustomer), FormatFch(DtFch))
        For Each oBrand In retval.Brands
            For Each oCategory In oBrand.Categories
                oCategory.Brand = oBrand
                For Each oSku In oCategory.Skus
                    oSku.Category = oCategory
                Next
            Next
        Next
        Return retval
    End Function

    Shared Function LoadSyncWithExcepts(exs As List(Of Exception), oUserOrCustomer As DTOBaseGuid, oLang As DTOLang, Optional DtFch As Date = Nothing) As DTOCustomerTarifa
        Dim retval = Api.FetchSync(Of DTOCustomerTarifa)(exs, "CustomerTarifa/LoadWithExcepts", OpcionalGuid(oUserOrCustomer), FormatFch(DtFch), oLang.Tag)
        For Each oBrand In retval.Brands
            For Each oCategory In oBrand.Categories
                oCategory.Brand = oBrand
                For Each oSku In oCategory.Skus
                    oSku.Category = oCategory
                Next
            Next
        Next
        Return retval
    End Function

    Shared Function CustomerCataleg(oTarifa As DTOCustomerTarifa) As DTOCustomerCataleg
        Dim retval As New DTOCustomerCataleg(oTarifa.Customer)
        For Each oBrand As DTOProductBrand In oTarifa.Brands
            For Each oCategory As DTOProductCategory In oBrand.Categories
                For Each oSku As DTOProductSku In oCategory.Skus
                    Dim item As New DTOCustomerCatalegItem
                    With item
                        .SkuGuid = oSku.Guid
                        .SkuId = oSku.Id
                        .Ref = oSku.RefProveidor
                        .Ean = oSku.Ean13.Value
                        .Brand = oBrand.nom.Esp
                        .Category = oCategory.nom.Esp
                        .Name = oSku.nom.Esp
                        If oTarifa.CostEnabled Then
                            .Cost = oSku.Price.Eur
                        End If
                        .RRPP = oSku.RRPP.Eur
                        If oSku.ImageExists Then
                            .Image = oSku.ImageUrl(True)
                        End If
                    End With
                    retval.Items.Add(item)
                Next
            Next
        Next
        Return retval
    End Function

    Shared Function Excel(oTarifa As DTOCustomerTarifa, oLang As DTOLang) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet
        Dim DtoVisible As Boolean = oTarifa.Skus.Exists(Function(x) x.customerDto <> 0)

        With retval
            .AddColumn("Ref.M+O")
            .AddColumn("Ref.Custom")
            .AddColumn(oLang.Tradueix("Ref.fabricante", "Ref.fabricant", "Ref.manufacturer"))
            .AddColumn("EAN producto")
            .AddColumn("EAN packaging")
            .AddColumn(oLang.Tradueix("Marca", "Marca", "Brand"), MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn(oLang.Tradueix("Categoría", "Categoria", "Category"), MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn(oLang.Tradueix("Producto", "Producte", "Product"), MatHelper.Excel.Cell.NumberFormats.W50)
            If oTarifa.CostEnabled Then
                .AddColumn(oLang.Tradueix("Coste", "Cost", "Cost"), MatHelper.Excel.Cell.NumberFormats.Euro)
                If DtoVisible Then
                    .AddColumn(oLang.Tradueix("Dto", "Dte", "Discount"), MatHelper.Excel.Cell.NumberFormats.Percent)
                End If
            End If
            .AddColumn(oLang.Tradueix("Venta", "Venda", "Retail"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oLang.Tradueix("Pedido mín.", "Comanda min.", "Moq"), MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn(oLang.Tradueix("Largo", "Llarg", "Length"), MatHelper.Excel.Cell.NumberFormats.mm)
            .AddColumn(oLang.Tradueix("Ancho", "Ample", "Width"), MatHelper.Excel.Cell.NumberFormats.mm)
            .AddColumn(oLang.Tradueix("Alto", "Alt", "Height"), MatHelper.Excel.Cell.NumberFormats.mm)
            .AddColumn(oLang.Tradueix("Peso", "Pes", "Weight"), MatHelper.Excel.Cell.NumberFormats.Kg)
            .AddColumn("Made In", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Codigo arancelario", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn(oLang.Tradueix("Imagen", "Imatge", "Image"), MatHelper.Excel.Cell.NumberFormats.PlainText)
        End With

        For Each oBrand As DTOProductBrand In oTarifa.Brands
            For Each oCategory As DTOProductCategory In oBrand.Categories
                For Each oSku As DTOProductSku In oCategory.Skus
                    Dim oRow As MatHelper.Excel.Row = retval.AddRow
                    oRow.AddCell(oSku.Id)
                    oRow.AddCell(oSku.RefCustomer)
                    'oRow.AddCell("'" & oSku.RefProveidor)
                    oRow.AddCell(oSku.RefProveidor)
                    oRow.AddCellEan(oSku.Ean13)
                    If oSku.PackageEan Is Nothing Then
                        oRow.AddCell()
                    Else
                        oRow.AddCell(oSku.PackageEan.Value)
                    End If
                    oRow.AddCell(oBrand.Nom.Esp)
                    oRow.AddCell(oCategory.Nom.Esp)
                    oRow.addCell(oSku.nom.Esp)

                    If oTarifa.CostEnabled Then
                        oRow.AddCellAmt(oSku.Price)
                        If DtoVisible Then
                            oRow.AddCell(oSku.CustomerDto)
                        End If
                    End If
                    oRow.AddCellAmt(oSku.RRPP)
                    oRow.AddCell(DTOProductSku.Moq(oSku))

                    oRow.AddCell(oSku.DimensionLOrInherited)
                    oRow.AddCell(oSku.DimensionWOrInherited)
                    oRow.AddCell(oSku.DimensionHOrInherited)
                    oRow.AddCell(oSku.WeightKgOrInherited)
                    oRow.AddCell(oSku.MadeInOrInheritedISO())
                    oRow.AddCell(oSku.CodiMercanciaIdOrInherited)

                    If oSku.ImageExists Then
                        Dim url As String = oSku.ImageUrl(True)
                        oRow.AddCell(url, url)
                    Else
                        oRow.AddCell()
                    End If
                Next

                'End If
            Next
            'End If
        Next
        Return retval
    End Function

    Shared Function Url(oTarifa As DTOCustomerTarifa, Optional AbsoluteUrl As Boolean = False)
        Dim retval As String = ""
        If oTarifa.Customer IsNot Nothing Then
            If oTarifa.Fch <> Nothing Then
                Dim iFch As Long = oTarifa.Fch.ToFileTime
                retval = UrlHelper.Factory(AbsoluteUrl, "tarifa", oTarifa.Customer.Guid.ToString, iFch.ToString())
            Else
                retval = UrlHelper.Factory(AbsoluteUrl, "tarifa", oTarifa.Customer.Guid.ToString())
            End If
        Else
            If oTarifa.Fch <> Nothing Then
                Dim iFch As Long = oTarifa.Fch.ToFileTime
                retval = UrlHelper.Factory(AbsoluteUrl, "tarifa", iFch.ToString())
            Else
                retval = UrlHelper.Factory(AbsoluteUrl, "tarifa")
            End If
        End If
        Return retval
    End Function


    Shared Function TarifaUrl(Optional AbsoluteUrl As Boolean = False, Optional DtFch As Date = Nothing)
        Dim retval As String = ""
        If DtFch = Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "tarifas")
        Else
            Dim encodedFch As String = TextHelper.EncodedDate(DtFch)
            retval = UrlHelper.Factory(AbsoluteUrl, "tarifa", encodedFch)
        End If
        Return retval
    End Function

    Shared Function ExcelUrl(oTarifa As DTOCustomerTarifa, Optional AbsoluteUrl As Boolean = False)
        Dim retval As String
        Dim DtFch As Date = oTarifa.Fch
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Dim sFch As Long = DtFch.ToFileTime.ToString

        If oTarifa.Customer Is Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "doc", DTODocFile.Cods.TarifaExcel, sFch)
        Else
            Dim oCustomerGuid As Guid = Guid.Empty
            If oTarifa.Customer IsNot Nothing Then oCustomerGuid = oTarifa.Customer.Guid
            retval = UrlHelper.Factory(AbsoluteUrl, "doc", DTODocFile.Cods.TarifaExcel, oCustomerGuid.ToString, sFch)
        End If

        Return retval
    End Function

End Class
