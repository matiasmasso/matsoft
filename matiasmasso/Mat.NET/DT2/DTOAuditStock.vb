Public Class DTOAuditStock
    Inherits DTOBaseGuid

    Property Year As Integer
    Property Exercici As DTOExercici
    Property Ref As String
    Property Sku As DTOProductSku
    Property Dsc As String
    Property Qty As Integer
    Property Palet As String
    Property FchEntrada As Date
    Property Dias As Integer
    Property Entrada As String
    Property Procedencia As String
    Property Cost As Decimal

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
