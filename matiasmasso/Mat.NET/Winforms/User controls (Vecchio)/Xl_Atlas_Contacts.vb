Public Class Xl_Atlas_Contacts
    Private _Atlas As DTOAtlas2
    Private _SelectedArea As DTOArea
    Private _AllowEvents As Boolean

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oAtlas As DTOAtlas2, Optional oSelectedArea As DTOArea = Nothing)
        _Atlas = oAtlas
        _SelectedArea = oSelectedArea
        LoadCountries()
        _AllowEvents = True
    End Sub

    Private Sub LoadCountries()
        Dim oCountries As List(Of DTOCountry) = _Atlas.Countries
        Dim oCountry As DTOCountry = GlobalVariables.Emp.DefaultCountry
        Xl_Countries_Grid1.Load(oCountries, oCountry, DTO.Defaults.SelectionModes.Selection)
    End Sub

    Private Sub LoadZonas()
        Dim oCountry As DTOCountry = CurrentCountry()
        Dim oZonas As List(Of DTOZona) = oCountry.Zonas
        Xl_Zonas_Grid1.Load(oZonas, DTO.Defaults.SelectionModes.Selection)
    End Sub

    Private Sub LoadLocations()
        Dim oZona As DTOZona = CurrentZona()
        Dim oLocations As List(Of DTOLocation) = oZona.Locations
        Xl_Locations_Grid1.Load(oLocations, DTO.Defaults.SelectionModes.Selection)
    End Sub

    Private Sub LoadContacts()
        Dim oLocation As DTOLocation = CurrentLocation()
        Dim oContacts As List(Of DTOContact) = oLocation.Contacts
        Xl_Contacts_Grid1.Load(oContacts, DTO.Defaults.SelectionModes.Selection)
    End Sub

    Private Function CurrentCountry() As DTOCountry
        Dim retval As DTOCountry = Xl_Countries_Grid1.Value
        Return retval
    End Function

    Private Function CurrentZona() As DTOZona
        Dim retval As DTOZona = Xl_Zonas_Grid1.Value
        Return retval
    End Function

    Private Function CurrentLocation() As DTOLocation
        Dim retval As DTOLocation = Xl_Locations_Grid1.Value
        Return retval
    End Function

    Private Function CurrentContact() As DTOContact
        Dim retval As DTOContact = Xl_Contacts_Grid1.Value
        Return retval
    End Function

    Private Sub Xl_Countries_Grid1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Countries_Grid1.onItemSelected
        _SelectedArea = e.Argument
        RaiseEvent OnItemSelected(Me, New MatEventArgs(_SelectedArea))
    End Sub



    Private Sub Xl_Countries_Grid1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Countries_Grid1.ValueChanged
        _SelectedArea = e.Argument
        LoadZonas()
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Private Sub Xl_Zonas_Grid1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Zonas_Grid1.onItemSelected
        _SelectedArea = e.Argument
        RaiseEvent OnItemSelected(Me, New MatEventArgs(_SelectedArea))
    End Sub

    Private Sub Xl_Zonas_Grid1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Zonas_Grid1.ValueChanged
        _SelectedArea = e.Argument
        LoadLocations()
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Private Sub Xl_Locations_Grid1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Locations_Grid1.onItemSelected
        _SelectedArea = e.Argument
        RaiseEvent OnItemSelected(Me, New MatEventArgs(_SelectedArea))
    End Sub

    Private Sub Xl_Locations_Grid1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Locations_Grid1.ValueChanged
        _SelectedArea = e.Argument
        LoadContacts()
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Private Sub Xl_Contacts_Grid1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Contacts_Grid1.onItemSelected
        _SelectedArea = e.Argument
        RaiseEvent OnItemSelected(Me, New MatEventArgs(_SelectedArea))
    End Sub

    Private Sub Xl_Contacts_Grid1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Contacts_Grid1.ValueChanged
        _SelectedArea = e.Argument
        RaiseEvent ValueChanged(Me, e)
    End Sub
End Class
