Public Class CliProductBlocked
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oContact As DTOContact, oProduct As DTOProduct) As Task(Of DTOCliProductBlocked)
        Dim retval = Await Api.Fetch(Of DTOCliProductBlocked)(exs, "CliProductBlocked", oContact.Guid.ToString, oProduct.Guid.ToString())
        If retval IsNot Nothing Then
            retval.RestoreObjects()
        End If
        Return retval
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oCliProductBlocked As DTOCliProductBlocked) As Boolean
        If Not oCliProductBlocked.IsLoaded And Not oCliProductBlocked.IsNew Then
            Dim pCliProductBlocked = Api.FetchSync(Of DTOCliProductBlocked)(exs, "CliProductBlocked", oCliProductBlocked.contact.Guid.ToString, OpcionalGuid(CType(oCliProductBlocked.product, DTOProduct)))
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCliProductBlocked)(pCliProductBlocked, oCliProductBlocked, exs)
                oCliProductBlocked.RestoreObjects()
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oCliProductBlocked As DTOCliProductBlocked) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCliProductBlocked)(oCliProductBlocked, exs, "CliProductBlocked")
        oCliProductBlocked.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oCliProductBlocked As DTOCliProductBlocked) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCliProductBlocked)(oCliProductBlocked, exs, "CliProductBlocked")
    End Function

    Shared Async Function AltresEnExclusiva(exs As List(Of Exception), oContact As DTOContact, oProduct As DTOProduct) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "CliProductBlocked/AltresEnExclusiva", oContact.Guid.ToString, oProduct.Guid.ToString())
    End Function

End Class

Public Class CliProductsBlocked
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOCliProductBlocked))
        Dim retval = Await Api.Fetch(Of List(Of DTOCliProductBlocked))(exs, "CliProductsBlocked", oCustomer.Guid.ToString())
        For Each item In retval
            item.RestoreObjects()
        Next
        Return retval
    End Function

    Shared Function AllSync(exs As List(Of Exception), oCustomer As DTOCustomer) As List(Of DTOCliProductBlocked)
        Dim retval = Api.FetchSync(Of List(Of DTOCliProductBlocked))(exs, "CliProductsBlocked", oCustomer.Guid.ToString())
        For Each item In retval
            item.RestoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function DistribuidorsOficialsActiveEmails(exs As List(Of Exception), oBrand As DTOProductBrand) As Task(Of List(Of DTOEmail))
        Return Await Api.Fetch(Of List(Of DTOEmail))(exs, "CliProductsBlocked/DistribuidorsOficialsActiveEmails", oBrand.Guid.ToString())
    End Function


    Shared Function ExcelRankingLastPdc(values As List(Of DTOCliProductBlocked)) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("ranking de remolones")
        retval.AddColumn("ranking", MatHelper.Excel.Cell.NumberFormats.PlainText)
        retval.AddColumn("ultima comanda", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
        retval.AddColumn("client", MatHelper.Excel.Cell.NumberFormats.PlainText)
        retval.AddColumn("producte", MatHelper.Excel.Cell.NumberFormats.PlainText)
        retval.AddColumn("modalitat", MatHelper.Excel.Cell.NumberFormats.PlainText)
        retval.AddColumn("observacions", MatHelper.Excel.Cell.NumberFormats.PlainText)
        For Each item As DTOCliProductBlocked In values
            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            With item
                oRow.addCell(TextHelper.VbFormat(values.IndexOf(item) + 1, "000"))
                oRow.addCell(TextHelper.VbFormat(item.lastFch, "dd/MM/yy"))
                oRow.addCell(item.contact.FullNom)
                oRow.AddCell(CType(item.product, DTOProduct).Nom.Esp)
                oRow.addCell(item.cod.ToString())
                oRow.addCell(item.obs)
            End With
        Next
        Return retval
    End Function



    Shared Async Function Delete(exs As List(Of Exception), oCliProductsBlocked As List(Of DTOCliProductBlocked)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTOCliProductBlocked))(oCliProductsBlocked, exs, "CliProductsBlocked")
    End Function


End Class
