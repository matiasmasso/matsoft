Public Class SellOutController
    Inherits _BaseController


    <HttpPost>
    <Route("api/compact/sellout/purchaseorders")>
    Public Function PurchaseOrders(user As DUI.User) As List(Of DTOCompactPurchaseOrder)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        'Dim oUser As DTOUser = BLLUser.WellKnown(BLLUser.WellKnowns.toni)
        BLLUser.Load(oUser)
        Dim retval As List(Of DTOCompactPurchaseOrder) = BLLCompactSellOut.PurchaseOrders(oUser)
        Return retval
    End Function

    <HttpPost>
    <Route("api/compact/sellout/contacts")>
    Public Function Contacts(user As DUI.User) As List(Of DTOCompactContact)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        'Dim oUser As DTOUser = BLLUser.WellKnown(BLLUser.WellKnowns.toni)
        BLLUser.Load(oUser)
        Dim retval As List(Of DTOCompactContact) = BLLCompactSellOut.Contacts(oUser)
        Return retval
    End Function

    <HttpPost>
    <Route("api/compact/sellout/skus")>
    Public Function Skus(user As DUI.User) As List(Of DTOCompactSku)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        Dim retval As List(Of DTOCompactSku) = BLLCompactSellOut.Skus(oUser)
        Return retval
    End Function


    <HttpGet>
    <Route("api/compact/sellout/purchaseorders")>
    Public Function PurchaseOrdersGet() As List(Of DTOCompactPurchaseOrder)
        Dim oUser As DTOUser = BLLUser.WellKnown(BLLUser.WellKnowns.toni)
        'Dim oUser As DTOUser = BLLUser.WellKnown(BLLUser.WellKnowns.toni)
        BLLUser.Load(oUser)

        Dim retval As List(Of DTOCompactPurchaseOrder) = BLLCompactSellOut.PurchaseOrders(oUser)
        Return retval
    End Function

    <HttpGet>
    <Route("api/compact/sellout/contacts/{user}")>
    Public Function ContactsGet(user As Guid) As List(Of DTOCompactContact)
        Dim oUser As DTOUser = BLLUser.Find(user)
        'Dim oUser As DTOUser = BLLUser.WellKnown(BLLUser.WellKnowns.toni)
        'BLLUser.Load(oUser)
        Dim retval As List(Of DTOCompactContact) = BLLCompactSellOut.Contacts(oUser)
        Return retval
    End Function

    <HttpGet>
    <Route("api/compact/sellout/skus")>
    Public Function SkusGet() As List(Of DTOCompactSku)
        Dim oUser As DTOUser = BLLUser.WellKnown(BLLUser.WellKnowns.toni)
        BLLUser.Load(oUser)
        Dim retval As List(Of DTOCompactSku) = BLLCompactSellOut.Skus(oUser)
        Return retval
    End Function

End Class
