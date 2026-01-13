Public Class GeoNames

    Shared Async Function Locations(exs As List(Of Exception), IsoPais As String, ZipCod As String) As Task(Of List(Of DTO.Google.Geonames.postalCodeClass))
        Dim oRequest = Await Api.Fetch(Of DTO.Google.Geonames.request)(exs, DTO.Google.Geonames.postalCodesUrl(IsoPais, ZipCod))
        Dim retval = oRequest.postalCodes
        Return retval
    End Function
End Class
