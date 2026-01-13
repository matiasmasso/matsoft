Imports System.Net.Http
Imports System.Net.Http.Formatting
Imports System.Net.Http.Headers
Imports System.Text
Imports Microsoft.Owin.Hosting
Module Module2

    Sub Main()
        Dim baseAddress As String = "http://localhost:9000/"

        ' Start OWIN host     
        Using WebApp.Start(Of Startup)(url:=baseAddress)
            Dim client = New HttpClient()
            Dim response = client.GetAsync(baseAddress & Convert.ToString("test")).Result
            Console.WriteLine(response)

            Console.WriteLine()

            Dim authorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes("rajeev:secretKey"))
            client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Basic", authorizationHeader)

            Dim form = New Dictionary(Of String, String)() From {
                {"grant_type", "password"},
                {"username", "matias"},
                {"password", "1234"}
            }

            Stop

            Dim tokenResponse = client.PostAsync(baseAddress & Convert.ToString("accesstoken"), New FormUrlEncodedContent(form)).Result
            'Dim oContent As HttpContent = New ObjectContent(GetType(Token), New Token, New JsonMediaTypeFormatter())
            Dim token = tokenResponse.Content.ReadAsAsync(Of Token)().Result

            Console.WriteLine("Token issued is: {0}", token.AccessToken)

            Console.WriteLine()

            client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", token.AccessToken)
            Dim authorizedResponse = client.GetAsync(baseAddress & Convert.ToString("test")).Result
            Console.WriteLine(authorizedResponse)
            Console.WriteLine(authorizedResponse.Content.ReadAsStringAsync().Result)
        End Using

        Console.ReadLine()
    End Sub


End Module
