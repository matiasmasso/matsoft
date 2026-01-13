Public Class PromoController
    Inherits _BaseController

    <HttpPost>
    <Route("api/promos")>
    Public Function promos(user As DTOGuidNom) As List(Of DUI.Promo)
        Dim retval As List(Of DUI.Promo) = Promos(user, archive:=True)
        Return retval
    End Function

    <HttpPost>
    <Route("api/promos/active")>
    Public Function activePromos(user As DTOGuidNom) As List(Of DUI.Promo)
        Dim retval As List(Of DUI.Promo) = Promos(user, archive:=False)
        Return retval
    End Function

    Private Function Promos(user As DTOGuidNom, archive As Boolean) As List(Of DUI.Promo)
        Dim retval As New List(Of DUI.Promo)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        Dim BlFuturePromos As Boolean
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Marketing
                BlFuturePromos = archive
        End Select
        Dim oIncentius As List(Of DTOIncentiu) = BLLIncentius.All(oUser, archive, BlFuturePromos)
        For Each oIncentiu As DTOIncentiu In oIncentius

            Dim item As New DUI.Promo
            With item
                .Guid = oIncentiu.Guid
                .Nom = BLLIncentiu.Title(oIncentiu, oUser.Lang)
                If oIncentiu.Product IsNot Nothing Then
                    Dim duiProduct As New DUI.Product
                    .Product = New DUI.Product
                    .Product.Guid = oIncentiu.Product.Guid
                    .Product.Nom = oIncentiu.Product.Nom
                End If
                .FchFrom = MyBase.DateTimeFormat(oIncentiu.FchFrom)
                .FchTo = MyBase.DateTimeFormat(oIncentiu.FchTo)
                .Bases = BLLIncentiu.Bases(oIncentiu, oUser.Lang)
                If .Bases = "" Then .Bases = BLLIncentiu.Excerpt(oIncentiu, oUser.Lang)
                .OrdersCount = oIncentiu.PdcsCount
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/promo")>
    Public Function promo(duiPromo As DUI.UserGuidNom) As DUI.Promo
        Dim oUser As DTOUser = BLL.BLLUser.Find(duiPromo.User.Guid)
        Dim oIncentiu As DTOIncentiu = BLL.BLLIncentiu.Find(duiPromo.GuidNom.Guid)
        Dim retval As New DUI.Promo
        With retval
            .Guid = oIncentiu.Guid
            .Nom = BLLIncentiu.Title(oIncentiu, oUser.Lang)
            If oIncentiu.Product IsNot Nothing Then
                Dim duiProduct As New DUI.Product
                .Product = New DUI.Product
                .Product.Guid = oIncentiu.Product.Guid
                .Product.Nom = oIncentiu.Product.Nom
            End If
            .FchFrom = MyBase.DateTimeFormat(oIncentiu.FchFrom)
            .FchTo = MyBase.DateTimeFormat(oIncentiu.FchTo)
            .Bases = BLLIncentiu.Bases(oIncentiu, oUser.Lang)
        End With
        Return retval
    End Function

    <HttpPost>
    <Route("api/promo/purchaseorders")>
    Public Function promoPurchaseOrders(duiPromo As DUI.UserGuidNom) As List(Of DUI.PurchaseOrder)
        Dim oUser As DTOUser = BLL.BLLUser.Find(duiPromo.User.Guid)

        Dim oIncentiu As New DTOIncentiu(duiPromo.GuidNom.Guid)

        Dim retval As New List(Of DUI.PurchaseOrder)
        Dim items As List(Of DTOPurchaseOrder) = BLLPurchaseOrders.All(oIncentiu, oUser)
        For Each item As DTOPurchaseOrder In items
            Dim dui As New DUI.PurchaseOrder
            With dui
                .Guid = item.Guid
                .Nom = item.Concept
                .Fch = item.Fch
                .Id = item.Num
                .Eur = item.SumaDeImports.Eur
                .Customer = New DUI.Guidnom
                .Customer.Guid = item.Customer.Guid
                .Customer.Nom = item.Customer.FullNom
                If item.DocFile IsNot Nothing Then
                    .FileUrl = BLLDocFile.DownloadUrl(item.DocFile, True)
                    .ThumbnailUrl = BLLDocFile.ThumbnailUrl(item.DocFile, True)
                End If
                If item.Incentiu IsNot Nothing Then
                    .Promo = New DUI.Promo
                    .Promo.Guid = item.Incentiu.Guid
                    .Promo.Nom = BLLIncentiu.Title(oIncentiu, oUser.Lang)
                End If
            End With
            retval.Add(dui)
        Next
        Return retval
    End Function


End Class
