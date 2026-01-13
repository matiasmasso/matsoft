Public Class NewRepLiqControlItem

    Public Property Source As DTORepComLiquidable

    Public Property Checked As Boolean
    Public Property RepAbr As String
    Public Property Fra As Integer
    Public Property Fch As Date
    Public Property Base As Decimal
    Public Property Comisio As Decimal
    Public Property Client As String
    Public Property Obs As String
    Public Property Codi As Codis
    Public Property Descarta As Boolean

    Public Enum Codis
        Impagat
        Insolvent
        CashOk
        CashPending
        BancPending
        BancOk
        NoBancPnd
        NoBancNoPnd
    End Enum

    Public Sub New(oRepComLiquidable As DTORepComLiquidable, BlLiquidable As Boolean, oCodi As Codis)
        MyBase.New()
        _Source = oRepComLiquidable
        With oRepComLiquidable
            _Checked = BlLiquidable
            _RepAbr = .Rep.NickName
            _Fra = .Fra.Num
            _Fch = .Fra.fch
            _Base = .baseFras.eur
            _Comisio = .Comisio.Eur
            _Client = .Fra.Customer.FullNom
            _Obs = .Fra.Fpg
            _Codi = oCodi
        End With
    End Sub

End Class

Public Class NewRepLiqControlItems
    Inherits SortableBindingList(Of NewRepLiqControlItem)
End Class

