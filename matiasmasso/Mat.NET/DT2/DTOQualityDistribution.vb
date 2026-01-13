Public Class DTOQualityDistribution
    Property Customer As DTOCustomer
    Property Skus As List(Of DTOProductSku)

    Public Sub New()
        MyBase.New
        _Skus = New List(Of DTOProductSku)
    End Sub

    Shared Function ExcelSheet(items As List(Of DTOQualityDistribution)) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet
        With retval
            .AddColumn("product depth", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("customer")
        End With

        For Each item In items
            Dim oRow As New MatHelperStd.ExcelHelper.Row(retval)
            retval.Rows.Add(oRow)
            oRow.AddCell(item.Skus.Count)
            oRow.AddCell(item.Customer.FullNom)
        Next

        Return retval
    End Function

End Class
