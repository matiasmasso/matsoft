Public Class AtlasController
    Inherits _BaseController

    <HttpPost>
    <Route("api/countries")>
    Public Function Countries(data As DTOBaseGuid) As List(Of DUI.Country)
        Dim retval As New List(Of DUI.Country)
        Dim oUser As DTOUser = BLLUser.Find(data.Guid)
        If oUser IsNot Nothing Then
            Dim oCountries As List(Of DTOCountry) = BLLContacts.Countries(oUser)
            For Each oCountry As DTOCountry In oCountries
                Dim DuiCountry As New DUI.Country
                With DuiCountry
                    .Guid = oCountry.Guid
                    .Nom = oCountry.Nom.Tradueix(oUser.Lang)
                End With
                retval.Add(DuiCountry)
            Next
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/countriesAndZonas")>
    Public Function CountriesAndZonas(data As DTOBaseGuid) As List(Of DUI.Country)
        Dim retval As New List(Of DUI.Country)
        Dim oUser As DTOUser = BLLUser.Find(data.Guid)
        If oUser IsNot Nothing Then
            Dim oCountries As List(Of DTOCountry) = BLLContacts.Countries(oUser)
            For Each oCountry As DTOCountry In oCountries
                Dim DuiCountry As New DUI.Country
                With DuiCountry
                    .Guid = oCountry.Guid
                    .Nom = oCountry.Nom.Tradueix(oUser.Lang)
                    .Zonas = New List(Of DUI.Zona)
                End With
                retval.Add(DuiCountry)
                For Each oZona As DTOZona In oCountry.Zonas
                    Dim duiZona As New DUI.Zona
                    With duiZona
                        .Guid = oZona.Guid
                        .Nom = oZona.Nom
                    End With
                    DuiCountry.Zonas.Add(duiZona)
                Next
            Next
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/zonas")>
    Public Function Zonas(data As DUI.UserCountry) As List(Of DUI.Zona)
        Dim retval As New List(Of DUI.Zona)
        Dim oUserGuid As Guid = data.User.Guid
        Dim oUser As DTOUser = BLLUser.Find(oUserGuid)
        Dim oCountryGuid As Guid = data.Country.Guid
        Dim oCountry As New DTOCountry(oCountryGuid)
        If oUser IsNot Nothing Then
            Dim oZonas As List(Of DTOZona) = BLLContacts.Zonas(oUser, oCountry)
            For Each oZona As DTOZona In oZonas
                Dim DuiZona As New DUI.Zona
                With DuiZona
                    .Guid = oZona.Guid
                    .Nom = oZona.Nom
                End With
                retval.Add(DuiZona)
            Next
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/locations")>
    Public Function Locations(data As DUI.UserZona) As List(Of DUI.Location)
        Dim retval As New List(Of DUI.Location)
        Dim oUserGuid As Guid = data.User.Guid
        Dim oUser As DTOUser = BLLUser.Find(oUserGuid)
        Dim oZonaGuid As Guid = data.Zona.Guid
        Dim oZona As New DTOZona(oZonaGuid)
        If oUser IsNot Nothing Then
            Dim oLocations As List(Of DTOLocation) = BLLContacts.Locations(oUser, oZona)
            For Each oLocation As DTOLocation In oLocations
                Dim DuiLocation As New DUI.Location
                With DuiLocation
                    .Guid = oLocation.Guid
                    .Nom = oLocation.Nom
                End With
                retval.Add(DuiLocation)
            Next
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/contacts")>
    Public Function Contacts(data As DUI.UserLocation) As List(Of DUI.Contact)
        Dim retval As New List(Of DUI.Contact)
        Dim oUserGuid As Guid = data.User.Guid
        Dim oUser As DTOUser = BLLUser.Find(oUserGuid)
        Dim oLocationGuid As Guid = data.Location.Guid
        Dim oLocation As New DTOLocation(oLocationGuid)
        If oUser IsNot Nothing Then
            Dim oContacts As List(Of DTOContact) = BLLContacts.all(oUser, oLocation)
            For Each oContact As DTOContact In oContacts
                Dim DuiContact As New DUI.Contact
                With DuiContact
                    .Guid = oContact.Guid
                    .Nom = oContact.FullNom
                    .Address = oContact.Address.Text
                    If oContact.Address.Coordenadas IsNot Nothing Then
                        .Latitude = oContact.Address.Coordenadas.Latitud
                        .Longitude = oContact.Address.Coordenadas.Longitud
                    End If
                End With
                retval.Add(DuiContact)
            Next
        End If
        Return retval
    End Function




    <HttpPost>
    <Route("api/atlas")>
    Public Function atlas(data As DTOBaseGuid) As List(Of DUI.Country)
        Dim retval As New List(Of DUI.Country)
        Dim oUser As DTOUser = BLLUser.Find(data.Guid)
        If oUser IsNot Nothing Then
            Dim oCountries As List(Of DTOCountry) = BLLContacts.Countries(oUser)
            For Each oCountry As DTOCountry In oCountries
                Dim DuiCountry As New DUI.Country
                With DuiCountry
                    .Guid = oCountry.Guid
                    .Nom = oCountry.Nom.Tradueix(oUser.Lang)
                    'If .Nom = "España" Then Stop
                    .Zonas = New List(Of DUI.Zona)
                End With
                retval.Add(DuiCountry)
                For Each oZona As DTOZona In oCountry.Zonas
                    Dim DuiZona As New DUI.Zona
                    With DuiZona
                        .Guid = oZona.Guid
                        .Nom = oZona.Nom
                        .Locations = New List(Of DUI.Location)
                    End With
                    DuiCountry.Zonas.Add(DuiZona)
                    For Each oLocation As DTOLocation In oZona.Locations
                        Dim DuiLocation As New DUI.Location
                        With DuiLocation
                            .Guid = oLocation.Guid
                            .Nom = oLocation.Nom
                            .Contacts = New List(Of DUI.Guidnom)
                        End With
                        DuiZona.Locations.Add(DuiLocation)
                        For Each oContact As DTOContact In oLocation.Contacts
                            Dim DuiContact As New DUI.Contact
                            With DuiContact
                                .Guid = oContact.Guid
                                .Nom = BLLContact.NomAndNomComercial(oContact)
                            End With
                            DuiLocation.Contacts.Add(DuiContact)
                        Next
                    Next

                Next
            Next
        End If

        Return retval
    End Function
End Class