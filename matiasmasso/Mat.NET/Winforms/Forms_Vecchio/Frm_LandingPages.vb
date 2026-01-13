Public Class Frm_LandingPages

    Public Sub New(oClient As Client)
        MyBase.New()
        InitializeComponent()
        Dim oLandingpages As List(Of EShopLandingpage) = EShopLandingpagesLoader.FromCustomer(oClient)
        Xl_EShopLandingpages1.Load(oLandingpages, EShopLandingpage.Modes.ShopProducts)
    End Sub

    Public Sub New(oProduct As DTOProduct0)
        MyBase.New()
        InitializeComponent()
        Dim oLandingpages As List(Of EShopLandingpage) = EShopLandingpagesLoader.FromProduct(oProduct)
        Xl_EShopLandingpages1.Load(oLandingpages, EShopLandingpage.Modes.ProductShops)
    End Sub
End Class