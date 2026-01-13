Public Class Frm_Geo
    Private _Area As DTOArea
    Private _SelectMode As SelectModes
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Enum SelectModes
        Browse
        SelectAny
        SelectCountry
        SelectZona
        SelectLocation
        SelectZip
    End Enum

    Public Sub New(Optional oSelectMode As SelectModes = SelectModes.Browse)
        MyBase.New()
        Me.InitializeComponent()

        _Area = BLL.BLLApp.Org.Address.Zip
        _SelectMode = oSelectMode
        _AllowEvents = True
    End Sub

    Public Sub New(oCountry As DTOCountry, Optional oSelectMode As SelectModes = SelectModes.Browse)
        MyBase.New()
        Me.InitializeComponent()

        _Area = oCountry
        _SelectMode = oSelectMode
        _AllowEvents = True
    End Sub

    Public Sub New(oZona As DTOZona, Optional oSelectMode As SelectModes = SelectModes.Browse)
        MyBase.New()
        Me.InitializeComponent()

        _Area = oZona
        _SelectMode = oSelectMode
        _AllowEvents = True
    End Sub

    Public Sub New(oLocation As DTOLocation, Optional oSelectMode As SelectModes = SelectModes.Browse)
        MyBase.New()
        Me.InitializeComponent()

        _Area = oLocation
        _SelectMode = oSelectMode
        _AllowEvents = True
    End Sub

    Public Sub New(oZip As DTOZip, Optional oSelectMode As SelectModes = SelectModes.Browse)
        MyBase.New()
        Me.InitializeComponent()

        _Area = oZip
        _SelectMode = oSelectMode
        _AllowEvents = True
    End Sub

    Private Sub Frm_Locations_Load(sender As Object, e As EventArgs) Handles Me.Load
        If TypeOf _Area Is DTOCountry Then
            Dim oCountry As DTOCountry = _Area
            Dim oCountries As List(Of DTOCountry) = BLL.BLLCountries.All(BLL.BLLSession.Current.User.Lang)
            Dim oSelMode As SelModes = IIf(_SelectMode = SelectModes.SelectCountry Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Countries1.Load(oCountries, oCountry, oSelMode)

            Dim oZonas As List(Of DTOZona) = BLL.BLLZonas.All(oCountry)
            oSelMode = IIf(_SelectMode = SelectModes.SelectZona Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Zonas1.Load(oZonas, Nothing, oSelMode)

            If oZonas.Count > 0 Then
                Dim oLocations As List(Of DTOLocation) = BLL.BLLLocations.FromZona(oZonas(0))
                oSelMode = IIf(_SelectMode = SelectModes.SelectLocation Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
                Xl_Zonas1.Load(oZonas, Nothing, oSelMode)
            End If
        ElseIf TypeOf _Area Is DTOZona Then
            Dim oZona As DTOZona = _Area
            Dim oCountries As List(Of DTOCountry) = BLL.BLLCountries.All(BLL.BLLSession.Current.User.Lang)
            Dim oSelMode As SelModes = IIf(_SelectMode = SelectModes.SelectCountry Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Countries1.Load(oCountries, oZona.Country, oSelMode)

            Dim oZonas As List(Of DTOZona) = BLL.BLLZonas.All(oZona.Country)
            oSelMode = IIf(_SelectMode = SelectModes.SelectZona Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Zonas1.Load(oZonas, Nothing, oSelMode)

            Dim oLocations As List(Of DTOLocation) = BLL.BLLLocations.FromZona(oZona)
            oSelMode = IIf(_SelectMode = SelectModes.SelectLocation Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Zonas1.Load(oZonas, Nothing, oSelMode)

        ElseIf TypeOf _Area Is DTOLocation Then
            Dim oLocation As DTOLocation = _Area
            If oLocation Is Nothing Then oLocation = BLL.BLLApp.Org.Address.Zip.Location
            BLL.BLLLocation.Load(oLocation)

            Dim oCountries As List(Of DTOCountry) = BLL.BLLCountries.All(BLL.BLLSession.Current.User.Lang)
            Dim oSelMode As SelModes = IIf(_SelectMode = SelectModes.SelectCountry Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Countries1.Load(oCountries, oLocation.Zona.Country, oSelMode)

            Dim oZonas As List(Of DTOZona) = BLL.BLLZonas.All(oLocation.Zona.Country)
            oSelMode = IIf(_SelectMode = SelectModes.SelectZona Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Zonas1.Load(oZonas, oLocation.Zona, oSelMode)

            Dim oLocations As List(Of DTOLocation) = BLL.BLLLocations.FromZona(oLocation.Zona)
            oSelMode = IIf(_SelectMode = SelectModes.SelectLocation Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Locations1.Load(oLocations, oLocation, oSelMode)

            Dim oZips As List(Of DTOZip) = BLL.BLLZips.FromLocation(oLocation)
            oSelMode = IIf(_SelectMode = SelectModes.SelectLocation Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Zips1.Load(oZips, Nothing, oSelMode)

        ElseIf TypeOf _Area Is DTOZip Then
            Dim oZip As DTOZip = _Area
            If oZip Is Nothing Then oZip = BLL.BLLApp.Org.Address.Zip
            BLL.BLLZip.Load(oZip)

            Dim oCountries As List(Of DTOCountry) = BLL.BLLCountries.All(BLL.BLLSession.Current.User.Lang)
            Dim oSelMode As SelModes = IIf(_SelectMode = SelectModes.SelectCountry Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Countries1.Load(oCountries, oZip.Location.Zona.Country, oSelMode)

            Dim oZonas As List(Of DTOZona) = BLL.BLLZonas.All(oZip.Location.Zona.Country)
            oSelMode = IIf(_SelectMode = SelectModes.SelectZona Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Zonas1.Load(oZonas, oZip.Location.Zona, oSelMode)

            Dim oLocations As List(Of DTOLocation) = BLL.BLLLocations.FromZona(oZip.Location.Zona)
            oSelMode = IIf(_SelectMode = SelectModes.SelectLocation Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Locations1.Load(oLocations, oZip.Location, oSelMode)

            Dim oZips As List(Of DTOZip) = BLL.BLLZips.FromLocation(oZip.Location)
            oSelMode = IIf(_SelectMode = SelectModes.SelectZip Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Zips1.Load(oZips, oZip, oSelMode)
        End If


        Select Case _SelectMode
            Case SelectModes.SelectAny, SelectModes.SelectZip
            Case SelectModes.SelectLocation
                Me.Text = "Poblacions"
                SplitContainerLocations.Panel2Collapsed = True
                SplitContainerCountries.SplitterDistance = 180
                SplitContainerZonas.SplitterDistance = 360
                MyBase.Size = New Size(620, MyBase.Height)
            Case SelectModes.SelectZona
                Me.Text = "Zones"
                SplitContainerZonas.Panel2Collapsed = True
                SplitContainerCountries.SplitterDistance = 180
                MyBase.Size = New Size(400, MyBase.Height)
            Case SelectModes.SelectCountry
                SplitContainerCountries.Panel2Collapsed = True
                Me.Text = "Paisos"
                MyBase.Size = New Size(250, MyBase.Height)
        End Select
        _AllowEvents = True
    End Sub


    Private Sub Xl_Countries1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Countries1.onItemSelected
        Select Case _SelectMode
            Case SelModes.Browse
                Dim oFrm As New Frm_Country(e.Argument)
                AddHandler oFrm.AfterUpdate, AddressOf RefrescaCountries
            Case SelectModes.SelectAny, SelectModes.SelectCountry
                RaiseEvent onItemSelected(Me, e)
                Me.Close()
        End Select
    End Sub

    Private Sub Xl_Zonas1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.onItemSelected
        Select Case _SelectMode
            Case SelModes.Browse
                Dim oFrm As New Frm_Zona(e.Argument)
                AddHandler oFrm.AfterUpdate, AddressOf RefrescaZonas
            Case SelectModes.SelectAny, SelectModes.SelectZona
                RaiseEvent onItemSelected(Me, e)
                Me.Close()
        End Select
    End Sub

    Private Sub Xl_Locations1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Locations1.onItemSelected
        Select Case _SelectMode
            Case SelModes.Browse
                Dim oFrm As New Frm_Location(e.Argument)
                AddHandler oFrm.AfterUpdate, AddressOf RefrescaLocations
            Case SelectModes.SelectAny, SelectModes.SelectLocation
                RaiseEvent onItemSelected(Me, e)
                Me.Close()
        End Select

    End Sub

    Private Sub Xl_Countries1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Countries1.RequestToAddNew
        Dim oCountry As New DTOCountry
        Dim oFrm As New Frm_Country(oCountry)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaCountries
        oFrm.Show()
    End Sub

    Private Sub Xl_Countries1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Countries1.RequestToRefresh
        RefrescaCountries()
    End Sub

    Private Sub Xl_Countries1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Countries1.ValueChanged
        If _AllowEvents Then
            RefrescaZonas()
            RefrescaLocations()
            RefrescaZips()
        End If
    End Sub

    Private Sub Xl_Zonas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.RequestToAddNew
        Dim oCountry As DTOCountry = Xl_Countries1.Value
        If oCountry IsNot Nothing Then
            Dim oZona As New DTOZona
            oZona.Country = oCountry
            Dim oFrm As New Frm_Zona(oZona)
            AddHandler oFrm.AfterUpdate, AddressOf RefrescaZonas
            oFrm.Show()
        End If
    End Sub

    Private Sub Xl_Zonas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.RequestToRefresh
        RefrescaZonas()
    End Sub

    Private Sub Xl_Zonas1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.ValueChanged
        If _AllowEvents Then
            RefrescaLocations()
            RefrescaZips()
        End If
    End Sub

    Private Sub Xl_Locations1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Locations1.ValueChanged
        If _AllowEvents Then
            RefrescaZips()
        End If
    End Sub

    Private Sub Xl_Locations1_requestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToAddNew
        Dim oZona As DTOZona = Xl_Zonas1.Value
        If oZona IsNot Nothing Then
            Dim oLocation As New DTOLocation
            oLocation.Zona = oZona
            Dim oFrm As New Frm_Location(oLocation)
            AddHandler oFrm.AfterUpdate, AddressOf RefrescaLocations
            oFrm.Show()
        End If
    End Sub

    Private Sub RefrescaCountries()
        Dim oCountries As List(Of DTOCountry) = BLL.BLLCountries.All(BLL.BLLSession.Current.User.Lang)
        Dim oSelMode As SelModes = IIf(_SelectMode = SelectModes.SelectCountry Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
        Xl_Countries1.Load(oCountries, Nothing, oSelMode)
    End Sub

    Private Sub RefrescaZonas()
        Dim oCountry As DTOCountry = Xl_Countries1.Value
        Dim oZonas As List(Of DTOZona) = BLL.BLLZonas.All(oCountry)
        If oZonas.Count = 0 Then
            Xl_Zonas1.Clear()
        Else
            Dim oSelMode As SelModes = IIf(_SelectMode = SelectModes.SelectZona Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Zonas1.Load(oZonas, oZonas(0), oSelMode)
        End If
    End Sub

    Private Sub RefrescaLocations()
        Dim oZona As DTOZona = Xl_Zonas1.Value
        If oZona Is Nothing Then
            Xl_Locations1.Clear()
        Else
            Dim oLocations As List(Of DTOLocation) = BLL.BLLLocations.FromZona(oZona)
            If oLocations.Count = 0 Then
                Xl_Locations1.Clear()
            Else
                Dim oSelMode As SelModes = IIf(_SelectMode = SelectModes.SelectLocation Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
                Xl_Locations1.Load(oLocations, oLocations(0), oSelMode)
            End If
        End If
    End Sub

    Private Sub RefrescaZips()
        Dim oLocation As DTOLocation = Xl_Locations1.Value
        If oLocation Is Nothing Then
            Xl_Zips1.Clear()
        Else
            Dim oZips As List(Of DTOZip) = BLL.BLLZips.FromLocation(oLocation)
            Dim oSelMode As SelModes = IIf(_SelectMode = SelectModes.SelectZip Or _SelectMode = SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            If oZips.Count = 0 Then
                Xl_Zips1.Clear()
            Else
                Xl_Zips1.Load(oZips, oZips(0), oSelMode)
            End If
        End If
    End Sub


    Private Sub Xl_Locations1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToRefresh
        RefrescaLocations()
    End Sub

    Private Sub Xl_Zips1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Zips1.RequestToAddNew
        Dim oLocation As DTOLocation = Xl_Locations1.Value
        If oLocation IsNot Nothing Then
            Dim oZip As New DTOZip
            oZip.Location = oLocation
            Dim oFrm As New Frm_Zip(oZip)
            AddHandler oFrm.AfterUpdate, AddressOf RefrescaZips
            oFrm.Show()
        End If
    End Sub

    Private Sub Xl_Zips1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Zips1.RequestToRefresh
        RefrescaZips()
    End Sub
End Class