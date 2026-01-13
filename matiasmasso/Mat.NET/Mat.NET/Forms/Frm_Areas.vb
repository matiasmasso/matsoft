Public Class Frm_Areas
    Private _Area As DTOArea
    Private _SelMode As SelModes
    Private _CountryMode As BLL.Defaults.SelectionModes
    Private _ZonaMode As BLL.Defaults.SelectionModes
    Private _LocationMode As BLL.Defaults.SelectionModes
    Private _ZipMode As BLL.Defaults.SelectionModes
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Public Enum SelModes
        Browse
        SelectArea
        SelectCountry
        SelectZona
        SelectLocation
        SelectZip
    End Enum

    Public Sub New(Optional oArea As DTOArea = Nothing, Optional oSelMode As SelModes = SelModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        If oArea Is Nothing Then
            _Area = BLL.BLLApp.Org.Address.Zip
        Else
            _Area = oArea
        End If
        SetSelectionModes(oSelMode)
    End Sub

    Private Sub Frm_Areas_Load(sender As Object, e As EventArgs) Handles Me.Load
        RefrescaCountries()
        _AllowEvents = True
    End Sub

    Private Sub RefrescaCountries()
        Dim oLang As DTOLang = Session.User.Lang
        Dim oCountries As List(Of DTOCountry) = BLL.BLLCountries.All(oLang)
        Dim oCountry As DTOCountry = BLL.BLLArea.Country(_Area)
        Xl_Countries1.Load(oCountries, oCountry, _CountryMode)
        RefrescaZonas()
    End Sub

    Private Sub RefrescaZonas()
        Dim oCountry As DTOCountry = Xl_Countries1.Value
        If oCountry Is Nothing Then
            Xl_Zonas1.Clear()
            Xl_Locations1.Clear()
            Xl_Zips1.Clear()
        Else
            Dim oZonas As List(Of DTOZona) = BLL.BLLZonas.All(oCountry)
            Dim oZona As DTOZona = BLL.BLLArea.Zona(_Area)
            Xl_Zonas1.Load(oZonas, oZona, _ZonaMode)
            RefrescaLocations()
        End If
    End Sub

    Private Sub RefrescaLocations()
        Dim oZona As DTOZona = Xl_Zonas1.Value
        If oZona Is Nothing Then
            Xl_Locations1.Clear()
            Xl_Zips1.Clear()
        Else
            Dim oLocations As List(Of DTOLocation) = BLL.BLLLocations.FromZona(oZona)
            Dim oLocation As DTOLocation = BLL.BLLArea.Location(_Area)
            Xl_Locations1.Load(oLocations, oLocation, _LocationMode)
            RefrescaZips()
        End If
    End Sub

    Private Sub RefrescaZips()
        Dim oLocation As DTOLocation = Xl_Locations1.Value
        If oLocation Is Nothing Then
            Xl_Zips1.Clear()
        Else
            Dim oZips As List(Of DTOZip) = BLL.BLLZips.FromLocation(oLocation)
            Dim oZip As DTOZip = BLL.BLLArea.Zip(_Area)
            Xl_Zips1.Load(oZips, oZip, _ZipMode)
        End If
    End Sub

    Public ReadOnly Property Value As DTOArea
        Get
            Dim retval As DTOArea = Nothing
            If Xl_Zips1.Value IsNot Nothing Then
                retval = Xl_Zips1.Value
            ElseIf Xl_Locations1.Value IsNot Nothing Then
                retval = Xl_Locations1.Value
            ElseIf Xl_Zonas1.Value IsNot Nothing Then
                retval = Xl_Zonas1.Value
            ElseIf Xl_Countries1.Value IsNot Nothing Then
                retval = Xl_Countries1.Value
            End If
            Return retval
        End Get
    End Property

    Private Sub Xl_onItemSelected(sender As Object, e As MatEventArgs) Handles _
        Xl_Countries1.onItemSelected, _
         Xl_Zonas1.onItemSelected, _
          Xl_Locations1.onItemSelected, _
           Xl_Zips1.onItemSelected

        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Countries1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Countries1.RequestToAddNew
        Dim oCountry As New DTOCountry
        Dim oFrm As New Frm_Country(oCountry)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaCountries
        oFrm.Show()
    End Sub

    Private Sub Xl_Countries1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Countries1.RequestToRefresh
        _Area = Me.Value
        RefrescaCountries()
    End Sub

    Private Sub Xl_Countries1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Countries1.ValueChanged
        If _AllowEvents Then
            RefrescaZonas()
            RaiseEvent ValueChanged(Me, e)
        End If
    End Sub

    Private Sub Xl_Zonas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.RequestToAddNew
        Dim oCountry As DTOCountry = Xl_Countries1.Value
        Dim oZona As DTOZona = BLL.BLLZona.NewFromCountry(oCountry)
        Dim oFrm As New Frm_Zona(oZona)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaZonas
        oFrm.Show()
    End Sub

    Private Sub Xl_Zonas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.RequestToRefresh
        _Area = Me.Value
        RefrescaZonas()
    End Sub

    Private Sub Xl_Zonas1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.ValueChanged
        If _AllowEvents Then
            RefrescaLocations()
            RaiseEvent ValueChanged(Me, e)
        End If
    End Sub

    Private Sub Xl_Locations1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToAddNew
        Dim oZona As DTOZona = Xl_Zonas1.Value
        Dim oLocation As DTOLocation = BLL.BLLLocation.NewFromZona(oZona)
        Dim oFrm As New Frm_Location(oLocation)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaLocations
        oFrm.Show()
    End Sub

    Private Sub Xl_Locations1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToRefresh
        _Area = Me.Value
        RefrescaLocations()
    End Sub


    Private Sub Xl_Locations1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Locations1.ValueChanged
        If _AllowEvents Then
            RefrescaZips()
            RaiseEvent ValueChanged(Me, e)
        End If
    End Sub

    Private Sub Xl_Zips1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Zips1.RequestToAddNew
        Dim oLocation As DTOLocation = Xl_Locations1.Value
        Dim oZip As DTOZip = BLL.BLLZip.NewFromLocation(oLocation)
        Dim oFrm As New Frm_Zip(oZip)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaZips
        oFrm.Show()
    End Sub

    Private Sub Xl_Zips1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Zips1.RequestToRefresh
        _Area = Me.Value
        RefrescaZips()
    End Sub

    Private Sub Xl_Zips1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Zips1.ValueChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, e)
        End If
    End Sub

    Private Sub SetSelectionModes(oSelMode As SelModes)
        _SelMode = oSelMode
        Select Case _SelMode
            Case SelModes.SelectCountry
                _CountryMode = BLL.Defaults.SelectionModes.Selection
            Case SelModes.SelectZona
                _ZonaMode = BLL.Defaults.SelectionModes.Selection
            Case SelModes.SelectLocation
                _LocationMode = BLL.Defaults.SelectionModes.Selection
            Case SelModes.SelectZip
                _ZipMode = BLL.Defaults.SelectionModes.Selection
            Case SelModes.SelectArea
                _CountryMode = BLL.Defaults.SelectionModes.Selection
                _ZonaMode = BLL.Defaults.SelectionModes.Selection
                _LocationMode = BLL.Defaults.SelectionModes.Selection
                _ZipMode = BLL.Defaults.SelectionModes.Selection
        End Select
    End Sub


End Class