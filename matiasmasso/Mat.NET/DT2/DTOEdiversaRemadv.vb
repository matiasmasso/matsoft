Public Class DTOEdiversaRemadv
    Inherits DTOBaseGuid
    Public Property DocNum As String
    Public Property FchDoc As Date
    Public Property FchEmision As Date
    Public Property FchVto As Date
    Public Property DocRef As String
    Public Property MedioDePago As MediosDePago
    Public Property EmisorPago As DTOContact
    Public Property ReceptorPago As DTOContact
    Public Property Amt As DTOAmt
    Public Property Result As Guid
    Public Property Items As List(Of DTOEdiversaRemadvItem)
    Public Property Exceptions As List(Of Exception)

    Public Enum MediosDePago
        Cheque = 20
        Transferencia = 31
        CtaBancaria = 42
        Pagare = 60
    End Enum

    Public Sub New(value As Guid)
        MyBase.New(value)
        _Items = New List(Of DTOEdiversaRemadvItem)
    End Sub

    Public Sub New()
        MyBase.New()
        _Items = New List(Of DTOEdiversaRemadvItem)
    End Sub

    Public Function Cuadra() As Boolean
        Dim retval As Boolean = True
        For Each oItem As DTOEdiversaRemadvItem In _Items
            If oItem.Pnd Is Nothing Then
                retval = False
                Exit For
            End If
        Next
        Return retval
    End Function
End Class



Public Class DTOEdiversaRemadvItem
    Public Property Parent As DTOEdiversaRemadv
    Public Property Type As Types
    Public Property Nom As String
    Public Property Num As String
    Public Property Fch As Date
    Public Property Amt As DTOAmt
    Public Property Pnd As DTOPnd
    Public Property Idx As Integer

    Public Enum Types
        Relacion_de_facturas = 49
        Nota_de_credito = 83
        Nota_de_debito = 84
        Factura_Comercial = 380
        Nota_de_abono = 381
        Nota_de_cargo = 383
        Factura_rectificativa = 384
        Factura_consolidada = 385
        Factura_de_anticipo = 386
        Autofactura = 389
    End Enum

    Public Sub New(value As DTOEdiversaRemadv)
        MyBase.New()
        _Parent = value
    End Sub
End Class

