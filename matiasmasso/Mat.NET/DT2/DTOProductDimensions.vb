Public Class DTOProductDimensions
    Property Hereda As Boolean
    Property NoDimensions As Boolean
    Property KgNet As Decimal
    Property KgBrut As Decimal
    Property M3 As Decimal
    Property DimensionAlto As Decimal
    Property DimensionAncho As Decimal
    Property DimensionLargo As Decimal
    Property InnerPack As Integer
    Property OuterPack As Integer
    Property ForzarInnerPack As Boolean
    Property CodiMercancia As DTOCodiMercancia
    Property PackageEan As DTOEan


    Shared Function DimensionLess() As DTOProductDimensions
        Dim oRetVal As New DTOProductDimensions()
        With oRetVal
            .NoDimensions = True
            .Hereda = False
        End With
        Return oRetVal
    End Function


End Class
