Public Class Vivace

    Shared Function ExcelRefs() As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("M+O Referencias")
        With retval
            .AddColumn("ref. M+O")
            .AddColumn("EAN")
            .AddColumn("ref. Proveedor")
            .AddColumn("nombre internacional")
            .AddColumn("nombre nacional")
            .AddColumn("marca")
        End With

        Dim oSkus As List(Of DTOProductSku) = VivaceLoader.Refs
        For Each oSku As DTOProductSku In oSkus

            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            With oSku
                oRow.AddCell(.id)
                oRow.AddCell(DTOEan.EanValue(.ean13))
                oRow.AddCell(.refProveidor)
                oRow.AddCell(.nomProveidor)
                oRow.addCell(.nomLlarg.Esp)
                oRow.AddCell(.BrandNom)
            End With
        Next
        Return retval

    End Function
End Class
