Public Class MiscController
    Inherits ApiController

    <HttpGet>
    <Route("api/helloWorld")>
    Public Function helloWorld() As String
        'Public Function helloWorld(emptyObj As Object) As String
        Dim retval As String = "Hello World!"
        Return retval
    End Function
End Class
