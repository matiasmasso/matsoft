Public Class DTOIpLookup
    Property Ip As String
    Property Coordenadas As GeoHelper.Coordenadas
    Property countryCode As String

    Shared Function IpLookup(ip As String) As DTOIpLookup
        'Dim url As String = String.Format("https://ipfind.co?ip={0}", ip) 'limit 100/day
        Dim url As String = String.Format("https://ip-api.com/json/{0}", ip) 'limit 150/minute
        Dim webClient As New System.Net.WebClient
        Dim jsonresult As String = webClient.DownloadString(url)

        Dim result As Object = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonresult)
        'Dim oCoordenadas As New googlemapshelper..GeoCoordenadas(result("latitude"), result("longitude"))
        Dim retval As New DTOIpLookup
        With retval
            .Ip = ip
            .Coordenadas = New GeoHelper.Coordenadas(result("lat"), result("lon"))
            .countryCode = result("countryCode")
        End With
        Return retval
    End Function
End Class
