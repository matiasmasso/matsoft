Public Class KeyWords

    Shared Async Function All(oSrcGuid As Guid, exs As List(Of Exception)) As Task(Of List(Of String))
        Return Await Api.Fetch(Of List(Of String))(exs, "KeyWords", oSrcGuid.ToString())
    End Function

End Class
