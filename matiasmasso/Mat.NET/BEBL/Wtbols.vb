Public Class Wtbol
    Shared Function Model(oUser As DTOUser) As Models.Wtbol.Model
        Return WtbolLoader.Model(oUser)
    End Function
End Class

Public Class WtbolBasket
#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOWtbolBasket
        Dim retval As DTOWtbolBasket = WtbolBasketLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oWtbolBasket As DTOWtbolBasket) As Boolean
        Dim retval As Boolean = WtbolBasketLoader.Load(oWtbolBasket)
        Return retval
    End Function

    Shared Function Update(oWtbolBasket As DTOWtbolBasket, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolBasketLoader.Update(oWtbolBasket, exs)
        Return retval
    End Function

    Shared Function Delete(oWtbolBasket As DTOWtbolBasket, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolBasketLoader.Delete(oWtbolBasket, exs)
        Return retval
    End Function
#End Region

End Class

Public Class WtbolBaskets
    Shared Function All(Optional Site As DTOWtbolSite = Nothing) As List(Of DTOWtbolBasket)
        Dim retval = WtbolBasketsLoader.All(Site)
        Return retval
    End Function
End Class

