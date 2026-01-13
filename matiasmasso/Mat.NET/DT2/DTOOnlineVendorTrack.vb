Public Class DTOOnlineVendorTrack
    Inherits DTOBaseGuid
    Property Serp As DTOOnlineVendorSerp
    Property Vendor As DTOOnlineVendor
    Property Pos As Integer
    Property Clicked As Integer
    Property Eur As Decimal

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class





