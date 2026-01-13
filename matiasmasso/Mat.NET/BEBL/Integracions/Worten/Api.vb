Namespace Integracions.Worten
    Public Class Api

        Shared Async Function GetRequest(Of T)(exs As List(Of Exception), urlSegment As String) As Task(Of T)
            Dim url = String.Format("{0}/api/{1}", DTO.Integracions.Worten.Globals.ApiUrl, urlSegment)
            Dim headers As New Dictionary(Of String, String)
            headers.Add("Authorization", DTO.Integracions.Worten.Globals.ApiKey)
            Return Await ApiHelper.Client.GetRequest(Of T)(exs, headers, url)
        End Function

        Shared Async Function PostRequest(Of U, T)(value As U, exs As List(Of Exception), urlSegment As String) As Task(Of T)
            Dim url = String.Format("{0}/api/{1}", DTO.Integracions.Worten.Globals.ApiUrl, urlSegment)
            Dim headers As New Dictionary(Of String, String)
            headers.Add("Authorization", DTO.Integracions.Worten.Globals.ApiKey)
            Return Await ApiHelper.Client.PostRequest(Of U, T)(value, exs, headers, url)
        End Function
    End Class

End Namespace
