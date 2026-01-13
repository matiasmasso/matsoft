Public Class DTOQtyDto
    Property Qty As Integer
    Property Dto As Decimal
    Property FreeUnits As Integer

    Public Sub New(Optional ByVal iQty As Integer = 0, Optional ByVal DcDto As Decimal = 0, Optional ByVal iFreeUnits As Integer = 0)
        MyBase.New()
        _Qty = iQty
        _Dto = DcDto
        _FreeUnits = iFreeUnits
    End Sub
End Class
