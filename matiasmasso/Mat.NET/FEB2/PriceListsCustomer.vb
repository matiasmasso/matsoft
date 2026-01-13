Public Class PriceListCustomer
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOPricelistCustomer)
        Return Await Api.Fetch(Of DTOPricelistCustomer)(exs, "PriceListCustomer", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oPriceListCustomer As DTOPricelistCustomer, Optional ForceReload As Boolean = False) As Boolean

        If Not oPriceListCustomer.IsNew Then
            If (ForceReload Or Not oPriceListCustomer.IsLoaded) And Not oPriceListCustomer.IsNew Then
                Dim pPriceListCustomer = Api.FetchSync(Of DTOPricelistCustomer)(exs, "PriceListCustomer", oPriceListCustomer.Guid.ToString())
                If exs.Count = 0 Then
                    DTOBaseGuid.CopyPropertyValues(Of DTOPricelistCustomer)(pPriceListCustomer, oPriceListCustomer, exs)
                    For Each item In oPriceListCustomer.Items
                        item.Parent = oPriceListCustomer
                    Next
                End If
            End If

        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oPriceListCustomer As DTOPricelistCustomer) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPricelistCustomer)(oPriceListCustomer, exs, "PriceListCustomer")
        oPriceListCustomer.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oPriceListCustomer As DTOPricelistCustomer) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPricelistCustomer)(oPriceListCustomer, exs, "PriceListCustomer")
    End Function


End Class

Public Class PriceListsCustomer
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOPricelistCustomer))
        Return Await Api.Fetch(Of List(Of DTOPricelistCustomer))(exs, "PriceListsCustomer")
    End Function

    Shared Async Function All(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOPricelistCustomer))
        Return Await Api.Fetch(Of List(Of DTOPricelistCustomer))(exs, "PriceListsCustomer", oCustomer.Guid.ToString)
    End Function


    Shared Async Function Customers(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "PriceListsCustomer/Customers", oSku.Guid.ToString())
    End Function

    Shared Async Function Delete(exs As List(Of Exception), values As List(Of DTOPricelistCustomer)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTOPricelistCustomer))(values, exs, "PriceListCustomer")
    End Function

    Shared Async Function ExcelCompareSheet(exs As List(Of Exception), value As DTOPricelistCustomer, Optional oLang As DTOLang = Nothing) As Task(Of ExcelHelper.Sheet)
        Dim retval As New ExcelHelper.Sheet
        Dim oDomain = DTOWebDomain.Factory(oLang)
        If FEB2.PriceListCustomer.Load(exs, value) Then

            Dim currents = Await FEB2.PriceListItemsCustomer.Vigent(exs, DtFch:=value.Fch)
            currents = currents.OrderBy(Function(x) x.sku.id).ToList

            Dim previous = Await FEB2.PriceListItemsCustomer.Vigent(exs, DtFch:=value.Fch.AddDays(-1))
            previous = previous.OrderBy(Function(x) x.sku.id).ToList

            Dim items As New List(Of Tuple(Of DTOProductSku, DTOPricelistItemCustomer, DTOPricelistItemCustomer))
            Dim item As Tuple(Of DTOProductSku, DTOPricelistItemCustomer, DTOPricelistItemCustomer) = Nothing

            Dim idx, previousIdx As Integer
            Dim lastId As Integer = Math.Max(currents.Last.sku.id, previous.Last.sku.id)
            Do
                If previousIdx = previous.Count OrElse currents(idx).sku.id < previous(previousIdx).sku.id Then
                    item = New Tuple(Of DTOProductSku, DTOPricelistItemCustomer, DTOPricelistItemCustomer)(currents(idx).sku, currents(idx), Nothing)
                    If idx < currents.Count Then idx += 1
                ElseIf idx = currents.Count OrElse currents(idx).sku.id > previous(previousIdx).sku.id Then
                    item = New Tuple(Of DTOProductSku, DTOPricelistItemCustomer, DTOPricelistItemCustomer)(previous(previousIdx).sku, Nothing, previous(previousIdx))
                    If previousIdx < previous.Count Then previousIdx += 1
                ElseIf currents(idx).sku.id = previous(previousIdx).sku.id Then
                    item = New Tuple(Of DTOProductSku, DTOPricelistItemCustomer, DTOPricelistItemCustomer)(currents(idx).sku, currents(idx), previous(previousIdx))
                    If idx < currents.Count Then idx += 1
                    If previousIdx < previous.Count Then previousIdx += 1
                End If
                items.Add(item)
                If item.Item1.id = lastId Then Exit Do
            Loop

            items = items.OrderBy(Function(a) a.Item1.nom.Esp).OrderBy(Function(b) b.Item1.category.ord).OrderBy(Function(c) c.Item1.category.nom.Esp).OrderBy(Function(X) X.Item1.category.codi).OrderBy(Function(e) e.Item1.category.brand.nom.Esp).ToList

            With retval
                .addColumn("id", ExcelHelper.Sheet.NumberFormats.W50)
                .addColumn("marca", ExcelHelper.Sheet.NumberFormats.W50)
                .addColumn("categoria", ExcelHelper.Sheet.NumberFormats.W50)
                .addColumn("producte", ExcelHelper.Sheet.NumberFormats.W50)
                .addColumn("Pvp", ExcelHelper.Sheet.NumberFormats.Euro)
                .addColumn("Pvp anterior", ExcelHelper.Sheet.NumberFormats.Euro)
                .addColumn("diferencia", ExcelHelper.Sheet.NumberFormats.Percent)
            End With

            For Each item In items
                Dim oRow As ExcelHelper.Row = retval.AddRow
                oRow.AddCell(item.Item1.Id, item.Item1.GetUrl(oLang, DTOProduct.Tabs.general, True))
                oRow.addCell(item.Item1.category.brand.nom.Esp)
                oRow.addCell(item.Item1.category.nom.Esp)
                oRow.addCell(item.Item1.nom.Esp)
                If item.Item2 Is Nothing Then
                    oRow.addCell(0)
                Else
                    oRow.AddCellAmt(item.Item2.retail)
                End If
                If item.Item3 Is Nothing Then
                    oRow.addCell(0)
                Else
                    oRow.AddCellAmt(item.Item3.retail)
                End If
                'oRow.AddFormula("IF(ISBLANK(RC[-1]);0;100*RC[-2]/RC[-1]-100)")
                oRow.addFormula("100*RC[-2]/RC[-1]-100")
            Next

        End If
        Return retval
    End Function

End Class
