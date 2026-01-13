Public Class DTOOnlineVendorSerp 'Search Engine Results Page
    Inherits DTOBaseGuid
    Property Fch As Date
    Property Sku As DTOProductSku

    Property Tracks As List(Of DTOOnlineVendorTrack)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class