Public Class Xl_AreaMultiCombo

    Public Shadows Sub Load(oCountries As List(Of Country), Optional oSelectedArea As area = Nothing)
        Xl_CountriesCombo1.Load(oCountries, oSelectedArea)
        If oSelectedArea IsNot Nothing Then
            Dim oCountry As Country = oSelectedArea.Country
            Dim oZonas As List(Of Zona) = oCountry.Zonas
            Xl_ZonasCombo1.Load(oZonas, oSelectedArea)
            Select Case oSelectedArea.ValueType
                Case area.ValueTypes.Zona, area.ValueTypes.Location, area.ValueTypes.Zip
                    Dim oZona As Zona = oSelectedArea.Zona
                    Xl_LocationsCombo1.Load(oZona.Locations, oSelectedArea)
            End Select

        End If
    End Sub

    Private Sub Xl_CountriesCombo1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_CountriesCombo1.AfterUpdate
        Dim oCountry As Country = e.Argument
        Xl_ZonasCombo1.Load(oCountry.Zonas)
    End Sub

    Private Sub Xl_ZonasCombo1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ZonasCombo1.AfterUpdate
        Dim oZona As Zona = e.Argument
        Xl_LocationsCombo1.Load(oZona.Locations)
    End Sub

    Private Sub Xl_LocationsCombo1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LocationsCombo1.AfterUpdate
        Dim oLocation As Location = e.Argument
        Xl_ContactsCombo1.Load(oLocation.Contacts)
    End Sub
End Class
