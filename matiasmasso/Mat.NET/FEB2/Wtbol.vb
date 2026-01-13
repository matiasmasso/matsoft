Public Class Wtbol

    Shared Async Function Model(exs As List(Of Exception), oUser As DTOUser) As Task(Of Models.Wtbol.Model)
        Return Await Api.Fetch(Of Models.Wtbol.Model)(exs, "wtbol/model", oUser.Guid.ToString())
    End Function

    Shared Async Function Baskets(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTOWtbolBasket))
        Return Await Api.Fetch(Of List(Of DTOWtbolBasket))(exs, "wtbol/baskets", oUser.Guid.ToString())
    End Function
End Class
