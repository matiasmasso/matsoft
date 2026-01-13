Public Class DTOEdiversaSalesReport
    Inherits DTOBaseGuid
    Property Id As String
    Property Fch As Date
    Property Customer As DTOCustomer
    Property Cur As DTOCur
    Property Items As List(Of Item)
    Property Exceptions As List(Of DTOEdiversaException)


    Public Sub New()
        MyBase.New()
        _Items = New List(Of Item)
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of Item)
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

    Public Sub AddException(oCod As DTOEdiversaException.Cods, sMsg As String, Optional oTagCod As DTOEdiversaException.TagCods = DTOEdiversaException.TagCods.NotSet, Optional oTag As DTOBaseGuid = Nothing)
        Dim oException = DTOEdiversaException.Factory(oCod, oTag, sMsg)
        oException.TagCod = oTagCod
        _Exceptions.Add(oException)
    End Sub

    Public Function amount() As DTOAmt
        Dim eur = _Items.Sum(Function(x) x.Eur)
        Return DTOAmt.Factory(eur)
    End Function

    Public Class Item

        Property customer As DTOCustomer
        Property sku As DTOProductSku

        Property fch As Date
        Property dept As String
        Property centro As String
        Property qty As Integer
        Property qtyBack As Integer

        Property eur As Decimal
        Property retail As Decimal

    End Class
End Class
