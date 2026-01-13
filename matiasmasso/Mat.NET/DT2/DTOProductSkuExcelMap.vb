Public Class DTOProductSkuExcelMap
    Property sheetCol As Integer
    Property colHeader As String
    Property skuField As SkuFields

    Public Enum SkuFields
        Ref_proveidor
        Descripcio_proveidor
        Ean_producte
        Ean_packaging
        Amplada_mm
        Longitut_mm
        Alçada_mm
        Pes_net_grams
        Pes_brut_grams
        Moq
    End Enum

    Shared Function fieldName(oField As SkuFields) As String
        Return oField.ToString.Replace("_", " ")
    End Function

    Shared Function Factory(sheetcol As Integer, colheader As String, skufield As SkuFields) As DTOProductSkuExcelMap
        Dim retval As New DTOProductSkuExcelMap
        With retval
            .sheetCol = sheetcol
            .colHeader = colheader
            .skuField = skufield
        End With
        Return retval
    End Function
End Class