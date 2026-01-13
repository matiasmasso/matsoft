Public Class GoogleMapsHelper
    Protected Shared _APIKey As String = "AIzaSyBwCj6irp55gfyydVdY_3bz2gnWO9FVeEs" ' "AIzaSyC3O2n2r1p1w-9JkC-f-yI7HWQfkst053I" ' "AIzaSyCUJivhLPW2XSvKRRr_K1Fdlw1UO5QuiU0"
    Protected Shared _StaticImageWidth As Integer = 640
    Protected Shared _StaticImageHeight As Integer = 320


    Shared Function Normalized(oCoordenadas As GeoCoordenadas) As String
        Dim sLatitud As String = NormalizedCoordenada(oCoordenadas.Latitud)
        Dim sLongitud As String = NormalizedCoordenada(oCoordenadas.Longitud)
        Dim retval = String.Format("{0},{1}", sLatitud, sLongitud)
        Return retval
    End Function

    Shared Function NormalizedCoordenada(src As Double) As String
        Dim retval As String = src.ToString.Replace(",", ".")
        Return retval
    End Function

    Shared Function Url(latitud As Double, longitud As Double) As String
        Dim sb As New System.Text.StringBuilder
        sb = New System.Text.StringBuilder
        Dim sLatitud As String = latitud.ToString.Replace(",", ".")
        Dim sLongitud As String = longitud.ToString.Replace(",", ".")
        sb.Append("http://maps.google.com/maps?q=")
        sb.Append(sLatitud)
        sb.Append(",")
        sb.Append(sLongitud)
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Function Url(sAddress As String, sLocationNom As String, sZipCod As String, Optional oGeoCoordenadas As GeoCoordenadas = Nothing) As String
        Dim retval As String = ""
        Dim sb As New System.Text.StringBuilder
        sb.Append("https://www.google.com/maps/place/")
        sb.Append(sAddress.Replace(" ", "+"))
        sb.Append("+" & sZipCod)
        sb.Append("+" & sLocationNom)

        If oGeoCoordenadas IsNot Nothing Then
            If oGeoCoordenadas.Latitud <> 0 Then
                retval = Url(oGeoCoordenadas.Latitud, oGeoCoordenadas.Longitud)
            Else
                retval = sb.ToString
            End If
        Else
            retval = sb.ToString
        End If
        Return retval
    End Function


    Shared Function Parameters(Optional coordenadas As GeoCoordenadas = Nothing, Optional zoom As Integer = 8, Optional size As System.Drawing.Size = Nothing) As List(Of String)
        Dim retval As New List(Of String)
        If coordenadas IsNot Nothing Then
            Dim sLatitud As String = NormalizedCoordenada(coordenadas.Latitud)
            Dim sLongitud As String = NormalizedCoordenada(coordenadas.Longitud)
            retval.Add(String.Format("center={0},{1}", sLatitud, sLongitud))
        End If
        retval.Add(String.Format("zoom={0}", zoom))
        If size <> Nothing Then
            retval.Add(String.Format("size={0}x{1}", size.Width, size.Height))
        End If
        Return retval
    End Function

    Shared Function Markers(oCoordenadas As List(Of GeoCoordenadas), Optional size As String = "mid", Optional color As String = "blue", Optional label As String = "S") As String
        Dim params As New List(Of String)
        If size > "" Then
            params.Add(String.Format("size:{0}", size))
        End If
        If color > "" Then
            params.Add(String.Format("color:{0}", color))
        End If
        If label > "" Then
            params.Add(String.Format("label:{0}", label))
        End If
        For Each oCoordenada In oCoordenadas
            Dim sLatitud As String = NormalizedCoordenada(oCoordenada.Latitud)
            Dim sLongitud As String = NormalizedCoordenada(oCoordenada.Longitud)
            params.Add(String.Format("{0},{1}", sLatitud, sLongitud))
        Next

        Dim sb As New Text.StringBuilder
        Dim splitchar As String = "%7C"
        sb.Append("markers=")
        For Each param In params
            If Not param.Equals(params.First) Then sb.Append(splitchar)
            sb.Append(param)
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function GeoCode(exs As List(Of Exception), googleText As String) As GeoCoordenadas
        Dim retval As GeoCoordenadas = Nothing
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim sUrl As String = "https://maps.googleapis.com/maps/api/geocode/json"

        Dim parameters As New Dictionary(Of String, String)
        parameters.Add("key", _APIKey)
        parameters.Add("address", googleText)

        Dim jsonOutput As String = ""

        If webapihelper.SendGetRequest(sUrl, parameters, jsonOutput, exs) Then
            Dim src As Object = JsonHelper.Deserialize(jsonOutput)
            Dim oGeoCode As New GlGeocode
            With oGeoCode
                .status = src("status")
                .results = New GlGeocodeResults
                With .results
                    .address_components = New List(Of GlGeocodeAddressComponent)
                    If src("results").length > 0 Then
                        .formatted_address = src("results")(0)("formatted_address")
                        .geometry = New GlGeocodeGeometry
                        With .geometry
                            .location = New GlGeocodeLocation
                            .location.lat = src("results")(0)("geometry")("location")("lat")
                            .location.lng = src("results")(0)("geometry")("location")("lng")
                        End With
                        retval = New GoogleMapsHelper.GeoCoordenadas(oGeoCode.results.geometry.location.lat, oGeoCode.results.geometry.location.lng)
                    Else
                        exs.Add(New Exception("results=0"))
                    End If
                End With
            End With


        End If


        Return retval
    End Function

    Shared Function IpLookupUrl(ip As String, imageSize As Size, Optional AppendApiKey As Boolean = False) As String
        'Dim url As String = String.Format("https://ipfind.co?ip={0}", ip) 'limit 100/day
        Dim url As String = String.Format("http://ip-api.com/json/{0}", ip) 'limit 150/minute
        Dim webClient As New System.Net.WebClient
        Dim jsonresult As String = webClient.DownloadString(url)

        Dim result As Object = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonresult)
        'Dim oCoordenadas As New googlemapshelper.GeoCoordenadas(result("latitude"), result("longitude"))
        Dim oCoordenadas As New GoogleMapsHelper.GeoCoordenadas(result("lat"), result("lon"))

        Dim retval As String = GoogleMapsHelper.StaticImageUrl(oCoordenadas, imageSize, AppendApiKey)
        Return retval
    End Function

    Shared Function StaticImageUrl(oCoordenadas As GoogleMapsHelper.GeoCoordenadas, oSize As Size, Optional AppendApiKey As Boolean = False) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("http://maps.googleapis.com/maps/api/staticmap")
        sb.Append("?center=" & Normalized(oCoordenadas))
        sb.Append("&zoom=8")
        sb.Append("&size=" & oSize.Width & "x" & oSize.Height)
        sb.Append("&markers=color:red%7C" & Normalized(oCoordenadas))
        sb.Append("&sensor=false")
        'If DTOApp.Current.Type <> DTOApp.AppTypes.MatNet Then
        If AppendApiKey Then
            If Not Debugger.IsAttached Then
                sb.Append("&key=" & _APIKey)
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function StaticImageUrl(sEncodedAddress As String) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("http://maps.googleapis.com/maps/api/staticmap")
        sb.Append("?center=" & sEncodedAddress)
        sb.Append("&zoom=14")
        sb.Append("&size=" & _StaticImageWidth & "x" & _StaticImageHeight)
        sb.Append("&markers=color:red%7C" & sEncodedAddress)
        sb.Append("&sensor=false")
        If Not Debugger.IsAttached Then
            sb.Append("&key=" & _APIKey)
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function StaticImageUrl(EncodedAddresses As List(Of String)) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("http://maps.googleapis.com/maps/api/staticmap")
        sb.Append(QueryString(EncodedAddresses))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Protected Shared Function QueryString(EncodedAddresses As List(Of String)) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("?size=" & _StaticImageWidth & "x" & _StaticImageHeight)
        Dim idx As Integer
        For Each sEncodedAddress In EncodedAddresses
            sb.Append("&markers=")
            sb.Append("color:red")
            sb.Append("%7C") 'separator
            sb.Append("label:" & Chr(idx + 65))
            sb.Append("%7C") 'separator
            sb.Append(sEncodedAddress)
            idx += 1
        Next
        sb.Append("&sensor=false")
        If Not Debugger.IsAttached Then
            sb.Append("&key=" & _APIKey)
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Function GetIpLookup(ip As String) As GoogleMapsHelper.IpLookUp
        'Dim url As String = String.Format("https://ipfind.co?ip={0}", ip) 'limit 100/day
        Dim url As String = String.Format("http://ip-api.com/json/{0}", ip) 'limit 150/minute
        Dim webClient As New System.Net.WebClient
        Dim jsonresult As String = webClient.DownloadString(url)

        Dim result As Object = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonresult)
        'Dim oCoordenadas As New googlemapshelper.GeoCoordenadas(result("latitude"), result("longitude"))
        Dim retval As New GoogleMapsHelper.IpLookUp
        With retval
            .Ip = ip
            .Coordenadas = New GoogleMapsHelper.GeoCoordenadas(result("lat"), result("lon"))
            .countryCode = result("countryCode")
        End With
        Return retval
    End Function


    Public Class GeoCoordenadas
        Property longitud As Double
        Property latitud As Double

        Public Sub New(DcLatitud As Double, DcLongitud As Double)
            MyBase.New
            _Longitud = DcLongitud
            _Latitud = DcLatitud
        End Sub

        Shared Function Factory(Lat As Object, Lng As Object) As GeoCoordenadas
            Dim retval As GeoCoordenadas = Nothing
            If IsDBNull(Lat) Or IsDBNull(Lng) Then
            Else
                retval = New GeoCoordenadas(Lat, Lng)
            End If
            Return retval
        End Function

        Shared Function Text(lat As Double, Lng As Double) As String
            Dim retval As String = String.Format("lat {0:#.000000}, lng {1:#.000000}", lat, Lng)
            Return retval
        End Function

        Public Function distance(ByVal oCoordFrom As GeoCoordenadas, oCoordTo As GeoCoordenadas) As Double
            Dim retval = distance(oCoordFrom.latitud, oCoordFrom.longitud, oCoordTo.latitud, oCoordTo.longitud)
            Return retval
        End Function

        Public Function distance(ByVal lat1 As Double, ByVal lon1 As Double, ByVal lat2 As Double, ByVal lon2 As Double, Optional ByVal unit As Char = "K") As Double
            If lat1 = lat2 And lon1 = lon2 Then
                Return 0
            Else
                Dim theta As Double = lon1 - lon2
                Dim dist As Double = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta))
                dist = Math.Acos(dist)
                dist = rad2deg(dist)
                dist = dist * 60 * 1.1515
                If unit = "K" Then
                    dist = dist * 1.609344
                ElseIf unit = "N" Then
                    dist = dist * 0.8684
                End If
                Return dist
            End If
        End Function

        Private Function deg2rad(ByVal deg As Double) As Double
            Return (deg * Math.PI / 180.0)
        End Function

        Private Function rad2deg(ByVal rad As Double) As Double
            Return rad / Math.PI * 180.0
        End Function

    End Class


    Public Class GlGeocode
        Property results As GlGeocodeResults
        Property status As String
    End Class

    Public Class GlGeocodeResults
        Property address_components As List(Of GlGeocodeAddressComponent)
        Property formatted_address As String
        Property geometry As GlGeocodeGeometry
        Property partial_match As Boolean
        Property place_id As String
        Property Types As List(Of String)

    End Class

    Public Class GlGeocodeAddressComponent
        Property long_name As String
        Property short_name As String
        Property types As List(Of String)

    End Class

    Public Class GlGeocodeGeometry
        Property location As GlGeocodeLocation
        Property location_type As String
        Property viewport As GlGeocodeViewport

    End Class

    Public Class GlGeocodeLocation
        Property lng As Double
        Property lat As Double

    End Class

    Public Class GlGeocodeViewport
        Property northeast As GlGeocodeLocation
        Property southwest As GlGeocodeLocation

    End Class



    Public Class IpLookUp
        Property Ip As String
        Property Coordenadas As GeoCoordenadas
        Property countryCode As String

        Shared Function IpLookup(ip As String) As IpLookUp
            'Dim url As String = String.Format("https://ipfind.co?ip={0}", ip) 'limit 100/day
            Dim url As String = String.Format("http://ip-api.com/json/{0}", ip) 'limit 150/minute
            Dim webClient As New System.Net.WebClient
            Dim jsonresult As String = webClient.DownloadString(url)

            Dim result As Object = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonresult)
            'Dim oCoordenadas As New GeoCoordenadas(result("latitude"), result("longitude"))
            Dim retval As New IpLookUp
            With retval
                .Ip = ip
                .Coordenadas = New GeoCoordenadas(result("lat"), result("lon"))
                .countryCode = result("countryCode")
            End With
            Return retval
        End Function
    End Class


End Class
