Public Class DTOCommercialMargin
    Public Property CostNet As Decimal
    'Public Property CostToTarifaA As Decimal
    'Public Property TarifaAtoTarifaB As Decimal
    'Public Property TarifaAtoRetail As Decimal
    Public Property CostToRetail As Decimal


    'Public Function GetTarifaA() As Decimal
    ' Dim rawvalue As Decimal = CostNet * (1 + CostToTarifaA / 100)
    ' Dim retval As Decimal = Math.Round(rawvalue, 0, MidpointRounding.AwayFromZero)
    '     Return retval
    ' End Function

    'Public Function GetTarifaB(Optional DcTarifaA As Decimal = 0) As Decimal
    '    If DcTarifaA = 0 Then DcTarifaA = GetTarifaA()
    'Dim rawvalue As Decimal = DcTarifaA * (1 + TarifaAtoTarifaB / 100)
    'Dim retval As Decimal = Math.Round(rawvalue, 2, MidpointRounding.AwayFromZero)
    '    Return retval
    'End Function

    Public Function GetRetail(Optional DcCost As Decimal = 0) As Decimal
        Dim rawvalue As Decimal = CostNet * (1 + CostToRetail / 100)
        Dim retval As Decimal = Math.Round(rawvalue, 0, MidpointRounding.AwayFromZero)
        Return retval
    End Function

End Class
