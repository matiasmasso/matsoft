Public Class ApiRequest


    Shared Async Function Execute(Of T, U)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of U)
        Return Await ApiClient.Execute(Of T, U)(value, exs, urlSegments)
    End Function

End Class
