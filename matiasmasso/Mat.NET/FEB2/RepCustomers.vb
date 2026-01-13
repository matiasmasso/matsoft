Public Class RepCustomers
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser, Optional oArea As DTOArea = Nothing) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "RepCustomers", oUser.Guid.ToString, OpcionalGuid(oArea))
    End Function

    Shared Async Function Atlas(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOAtlas.Country))
        Return Await Api.Fetch(Of List(Of DTOAtlas.Country))(exs, "RepCustomers/atlas", oUser.Guid.ToString())
    End Function

End Class
