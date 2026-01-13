Public Class StatQuotasOnline
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oProveidor As DTOProveidor) As Task(Of List(Of DTOStatQuotaOnline))
        Return Await Api.Fetch(Of List(Of DTOStatQuotaOnline))(exs, "StatQuotasOnline", oProveidor.Guid.ToString())
    End Function

End Class
