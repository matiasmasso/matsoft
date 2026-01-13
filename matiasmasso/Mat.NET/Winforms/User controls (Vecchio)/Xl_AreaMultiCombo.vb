Public Class Xl_AreaMultiCombo

    Public Shadows Sub Load(oCountries As List(Of DTOCountry), Optional oSelectedArea As DTOArea = Nothing)
        Xl_CountriesCombo1.Load(oCountries, oSelectedArea)
        If oSelectedArea IsNot Nothing Then
            Dim oCountry As DTOCountry = oSelectedArea
            Dim oZonas As List(Of DTOZona) = oCountry.Zonas
            Xl_ZonasCombo1.Load(oZonas, oSelectedArea)
            If TypeOf oSelectedArea Is DTOZona Or TypeOf oSelectedArea Is DTOLocation Or TypeOf oSelectedArea Is DTOZip Then
                Dim oZona As DTOZona = oSelectedArea
                Xl_LocationsCombo1.Load(oZona.Locations, oSelectedArea)
            End If

        End If
    End Sub

    Private Sub Xl_CountriesCombo1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_CountriesCombo1.AfterUpdate
        Dim oCountry As DTOCountry = e.Argument
        Xl_ZonasCombo1.Load(oCountry.Zonas)
    End Sub

    Private Sub Xl_ZonasCombo1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ZonasCombo1.AfterUpdate
        Dim oZona As DTOZona = e.Argument
        Xl_LocationsCombo1.Load(oZona.Locations)
    End Sub

    Private Sub Xl_LocationsCombo1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LocationsCombo1.AfterUpdate
        Dim oLocation As DTOLocation = e.Argument
        Xl_ContactsCombo1.Load(oLocation.Contacts)
    End Sub
End Class
