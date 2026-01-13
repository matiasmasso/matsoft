Public Class TopNavBar
    Inherits DTONavbar

    Public Property Product As DTOProduct

    Private _WebSession As MVC.WebSession

    Public Sub New(oWebSession As MVC.WebSession, Optional oProduct As DTOProduct = Nothing)
        MyBase.New()
        _WebSession = oWebSession
        _Product = oProduct
        MyBase.Items = New List(Of DTONavbarItem)

        BLLNavbar.LoadProducts(oWebSession, Me, _Product)
        BLLNavbar.LoadLangs(oWebSession, Me)

        If _WebSession.IsAuthenticated Then
            BLLNavbar.LoadRolMenus(oWebSession, Me)
        Else
            BLLNavbar.LoadSignUp(oWebSession, Me)
            BLLNavbar.LoadLogButton(oWebSession, Me)
        End If
    End Sub




End Class
