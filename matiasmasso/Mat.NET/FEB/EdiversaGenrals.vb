Public Class EdiversaGenrals

    Shared Async Function Search(searchkey As String, exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaGenral))
        Return Await Api.Execute(Of String, List(Of DTOEdiversaGenral))(searchkey, exs, "EdiversaGenrals/search")
    End Function

End Class
