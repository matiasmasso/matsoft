Public Class DTOProductRanking
    Property User As DTOUser
    Property FchFrom As Date
    Property FchTo As Date
    Property AreaGuid As Guid
    Property BrandGuid As Guid
    Property Rep As DTORep
    Property ProductParentGuid As Nullable(Of Guid)
    Property Unit As Units
    Property Items As Dictionary(Of DTOProduct, Decimal)
    Property Zonas As List(Of DTOGuidNom)
    Property Brands As List(Of DTOProductBrand)

    Public Enum Units
        Qty
        Eur
    End Enum

    Shared Function Excel(value As DTOProductRanking) As MatHelperStd.ExcelHelper.Sheet
        Dim sFilename As String = String.Format("M+O product ranking {0:yyyy.MM.dd}-{1:yyyy.MM.dd}", value.FchFrom, value.FchTo)
        Dim retval As New MatHelperStd.ExcelHelper.Sheet("ranking", sFilename)
        With retval
            .AddColumn("Category", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Volume", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Rate", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Percent)
        End With

        Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow()
        oRow.AddCell("total")
        oRow.AddFormula("SUM(R[1]C:R[" & value.Items.Count & "]C)")
        oRow.AddFormula("100*RC[-1]/R2C[-1]")

        For Each item As KeyValuePair(Of DTOProduct, Decimal) In value.Items
            oRow = retval.AddRow()
            oRow.AddCell(item.Key.nom)
            oRow.AddCell(item.Value)
            oRow.AddFormula("100*RC[-1]/R2C[-1]")
        Next
        Return retval
    End Function

End Class

