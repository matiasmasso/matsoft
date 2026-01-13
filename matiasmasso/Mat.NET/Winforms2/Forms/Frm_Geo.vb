Public Class Frm_Geo
    Private _Area As DTOArea

    Private _Countries As List(Of DTOCountry)
    Private _Zonas As List(Of DTOZona)
    Private _Locations As List(Of DTOLocation)
    Private _Zips As List(Of DTOZip)

    Private _FilteredCountries As List(Of DTOCountry)
    Private _FilteredZonas As List(Of DTOZona)
    Private _FilteredLocations As List(Of DTOLocation)
    Private _FilteredZips As List(Of DTOZip)

    Private _SelectMode As DTOArea.SelectModes
    Private _XlSearchResults As Xl_GeoSearch
    'Private _reloadable As Boolean
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oSelectMode As DTOArea.SelectModes = DTOArea.SelectModes.Browse, Optional oArea As DTOArea = Nothing, Optional oCountries As List(Of DTOCountry) = Nothing)
        MyBase.New()
        Me.InitializeComponent()

        If oArea Is Nothing Then
            If Current.Session.Emp.Org IsNot Nothing AndAlso Current.Session.Emp.Org.Address IsNot Nothing Then
                _Area = Current.Session.Emp.Org.Address.Zip
            Else
                _Area = DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)
            End If
        Else
            _Area = oArea
        End If

        _SelectMode = oSelectMode
        _Countries = oCountries
        '_reloadable = (oCountries Is Nothing)
    End Sub


    Private Async Sub Frm_Locations_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        SetProperties()
        If _Countries Is Nothing Then
            Await LoadData()
        Else
            _Zonas = _Countries.SelectMany(Function(x) x.zonas).Select(Function(y) y).ToList
            _Locations = _Zonas.SelectMany(Function(x) x.locations).Select(Function(y) y).ToList
            _Zips = _Locations.SelectMany(Function(x) x.Zips).Select(Function(y) y).ToList
        End If

        Reload()

        _XlSearchResults = New Xl_GeoSearch
        _XlSearchResults.Visible = False
        _XlSearchResults.Dock = DockStyle.Fill
        Panel1.Controls.Add(_XlSearchResults)
        AddHandler _XlSearchResults.OnItemSelected, AddressOf OnSearchResultSelected
        _XlSearchResults.BringToFront()
    End Sub

    Private Sub onSearchResultSelected(sender As Object, e As MatEventArgs)
        Dim oArea As DTOArea = e.Argument
        Select Case _SelectMode
            Case SelModes.Browse
                Select Case oArea.Cod
                    Case DTOArea.Cods.Country
                        Dim oFrm As New Frm_Country(oArea)
                        AddHandler oFrm.AfterUpdate, AddressOf Reload
                    Case DTOArea.Cods.Zona
                        Dim oFrm As New Frm_Zona(oArea)
                        AddHandler oFrm.AfterUpdate, AddressOf Reload
                    Case DTOArea.Cods.Location
                        Dim oFrm As New Frm_Location(oArea)
                        AddHandler oFrm.AfterUpdate, AddressOf Reload
                    Case DTOArea.Cods.Zip
                        Dim oFrm As New Frm_Zip(oArea)
                        AddHandler oFrm.AfterUpdate, AddressOf Reload
                End Select
            Case DTOArea.SelectModes.SelectAny,
                 DTOArea.SelectModes.SelectCountry And oArea.Cod = DTOArea.Cods.Country,
                 DTOArea.SelectModes.SelectZona And oArea.Cod = DTOArea.Cods.Zona,
                 DTOArea.SelectModes.SelectLocation And oArea.Cod = DTOArea.Cods.Location,
                 DTOArea.SelectModes.SelectZip And oArea.Cod = DTOArea.Cods.Zip
                RaiseEvent onItemSelected(Me, e)
                Me.Close()
        End Select

    End Sub

    Private Async Function LoadData() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Countries = Await FEB.Zips.Tree(exs, Current.Session.Lang)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            _Zonas = _Countries.SelectMany(Function(x) x.zonas).Select(Function(y) y).ToList
            _Locations = _Zonas.SelectMany(Function(x) x.locations).Select(Function(y) y).ToList
            _Zips = _Locations.SelectMany(Function(x) x.Zips).Select(Function(y) y).ToList
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Function


#Region "Data and Properties"


    Private Sub Reload()

        If _Zips.Count = 0 Then
            SplitContainerLocations.Panel2Collapsed = True
            If _Locations.Count = 0 Then
                SplitContainerZonas.Panel2Collapsed = True
                If _Zonas.Count = 0 Then
                    SplitContainerCountries.Panel2Collapsed = True
                End If
            End If
        End If

        LoadFilter()
        refresca(_Area)
        _AllowEvents = True

    End Sub

    Private Sub LoadFilter(Optional searchKey As String = "")
        If searchKey = "" Then
            _FilteredZips = _Zips
            _FilteredLocations = _Locations
            _FilteredZonas = _Zonas
            _FilteredCountries = _Countries
        Else
            searchKey = searchKey.ToLower
            _FilteredZips = _Zips.Where(Function(x) x.ZipCod.ToLower.Contains(searchKey) Or
                                     x.location.nom.ToLower.Contains(searchKey) Or
                                     x.location.Zona.nom.ToLower.Contains(searchKey) Or
                                     x.location.Zona.Country.LangNom.esp.Contains(searchKey) Or
                                     x.location.Zona.Country.ISO.Contains(searchKey)).ToList

            _FilteredLocations = _Locations.Where(Function(x) x.nom.ToLower.Contains(searchKey) Or
                                     x.Zona.nom.ToLower.Contains(searchKey) Or
                                     x.Zona.Country.LangNom.esp.Contains(searchKey) Or
                                     x.Zona.Country.ISO.Contains(searchKey)).ToList

            _FilteredZonas = _Zonas.Where(Function(x) x.nom.ToLower.Contains(searchKey) Or
                                     x.Country.LangNom.esp.Contains(searchKey) Or
                                     x.Country.ISO.Contains(searchKey)).ToList

            _FilteredCountries = _Countries.Where(Function(x) x.LangNom.esp.Contains(searchKey) Or
                                     x.ISO.Contains(searchKey)).ToList

        End If
    End Sub

    Private Sub SetProperties()
        Select Case _SelectMode
            Case DTOArea.SelectModes.SelectAny, DTOArea.SelectModes.SelectZip
            Case DTOArea.SelectModes.SelectLocation
                Me.Text = "Poblacions"
                SplitContainerLocations.Panel2Collapsed = True
                SplitContainerCountries.SplitterDistance = 180
                SplitContainerZonas.SplitterDistance = 360
                MyBase.Size = New Size(620, MyBase.Height)
            Case DTOArea.SelectModes.SelectZona
                Me.Text = "Zones"
                SplitContainerZonas.Panel2Collapsed = True
                SplitContainerCountries.SplitterDistance = 180
                MyBase.Size = New Size(400, MyBase.Height)
            Case DTOArea.SelectModes.SelectCountry
                SplitContainerCountries.Panel2Collapsed = True
                Me.Text = "Paisos"
                MyBase.Size = New Size(250, MyBase.Height)
        End Select

    End Sub

#End Region

#Region "Refresca"

    Private Sub refresca(oArea As DTOArea)
        If RefrescaCountries(oArea) Then
            If RefrescaZonas(oArea) Then
                If RefrescaLocations(oArea) Then
                    RefrescaZips(oArea)
                End If
            End If
        End If
    End Sub

    Private Function RefrescaCountries(oArea As DTOArea) As Boolean
        Dim retval As Boolean
        Dim oCountries = _FilteredCountries
        If oCountries.Count = 0 Then
            Xl_Countries1.Clear()
            Xl_Zonas1.Clear()
            Xl_Locations1.Clear()
            Xl_Zips1.Clear()
        Else
            Dim oCountry = DTOArea.Country(oArea)
            Dim oSelMode As SelModes = IIf(_SelectMode = DTOArea.SelectModes.SelectCountry Or _SelectMode = DTOArea.SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Countries1.Load(oCountries, oCountry, oSelMode)
            retval = True
        End If
        Return retval
    End Function

    Private Function RefrescaZonas(Optional oArea As DTOArea = Nothing) As Boolean
        Dim retval As Boolean
        Dim oCountry As DTOCountry = Xl_Countries1.Value
        Dim oZonas = _FilteredZonas.Where(Function(x) x.Country.Equals(oCountry))
        If oZonas.Count = 0 Then
            Xl_Zonas1.Clear()
            Xl_Locations1.Clear()
            Xl_Zips1.Clear()
        Else
            Dim oZona = IIf(oArea Is Nothing, oZonas.First, DTOArea.Zona(oArea))
            Dim oSelMode As SelModes = IIf(_SelectMode = DTOArea.SelectModes.SelectZona Or _SelectMode = DTOArea.SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            Xl_Zonas1.Load(oZonas, oZona, oSelMode)
            retval = True
        End If
        Return retval
    End Function

    Private Function RefrescaLocations(Optional oArea As DTOArea = Nothing) As Boolean
        Dim retval As Boolean
        Dim oZona As DTOZona = Xl_Zonas1.Value
        If oZona Is Nothing Then
            Xl_Locations1.Clear()
            Xl_Zips1.Clear()
        Else
            Dim oLocations = _FilteredLocations.Where(Function(x) x.Zona.Equals(oZona))
            If oLocations.Count = 0 Then
                Xl_Locations1.Clear()
                Xl_Zips1.Clear()
            Else
                Dim oLocation = IIf(oArea Is Nothing, oLocations.First, DTOArea.Location(oArea))
                Dim oSelMode As SelModes = IIf(_SelectMode = DTOArea.SelectModes.SelectLocation Or _SelectMode = DTOArea.SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
                Xl_Locations1.Load(oLocations, oLocation, oSelMode)
                retval = True
            End If
        End If
        Return retval
    End Function

    Private Function RefrescaZips(Optional oArea As DTOArea = Nothing) As Boolean
        Dim retval As Boolean
        Dim oLocation As DTOLocation = Xl_Locations1.Value
        If oLocation Is Nothing Then
            Xl_Zips1.Clear()
        Else
            Dim oZips = _FilteredZips.Where(Function(x) x.location IsNot Nothing AndAlso x.location.Equals(oLocation)).ToList
            Dim oSelMode As SelModes = IIf(_SelectMode = DTOArea.SelectModes.SelectZip Or _SelectMode = DTOArea.SelectModes.SelectAny, SelModes.Selection, SelModes.Browse)
            If oZips.Count = 0 Then
                Xl_Zips1.Clear()
            Else
                Dim oZip = IIf(oArea Is Nothing, oZips.First, DTOArea.Zip(oArea))
                Xl_Zips1.Load(oZips, oZip, oSelMode)
                retval = True
            End If
        End If
        Return retval
    End Function

    Private Function CurrentArea() As DTOArea
        Dim retval As DTOArea = Xl_Zips1.Value
        If retval Is Nothing Then
            retval = Xl_Locations1.Value
            If retval Is Nothing Then
                retval = Xl_Zonas1.Value
                If retval Is Nothing Then
                    retval = Xl_Countries1.Value
                End If
            End If
        End If
        Return retval
    End Function

#End Region

#Region "SelectionChanged"

    Private Sub Xl_Countries1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Countries1.onItemSelected
        Select Case _SelectMode
            Case SelModes.Browse
                Dim oFrm As New Frm_Country(e.Argument)
                AddHandler oFrm.AfterUpdate, AddressOf Reload
            Case DTOArea.SelectModes.SelectAny, DTOArea.SelectModes.SelectCountry
                RaiseEvent onItemSelected(Me, e)
                Me.Close()
        End Select
    End Sub

    Private Sub Xl_Zonas1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.onItemSelected
        Select Case _SelectMode
            Case SelModes.Browse
                Dim oFrm As New Frm_Zona(e.Argument)
                AddHandler oFrm.AfterUpdate, AddressOf Reload
            Case DTOArea.SelectModes.SelectAny, DTOArea.SelectModes.SelectZona
                RaiseEvent onItemSelected(Me, e)
                Me.Close()
        End Select
    End Sub

    Private Sub Xl_Locations1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Locations1.onItemSelected
        Select Case _SelectMode
            Case SelModes.Browse
                Dim oFrm As New Frm_Location(e.Argument)
                AddHandler oFrm.AfterUpdate, AddressOf Reload
            Case DTOArea.SelectModes.SelectAny, DTOArea.SelectModes.SelectLocation
                RaiseEvent onItemSelected(Me, e)
                Me.Close()
        End Select

    End Sub

    Private Sub Xl_Zips1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Zips1.onItemSelected
        Select Case _SelectMode
            Case SelModes.Browse
                Dim oFrm As New Frm_Zip(e.Argument)
                AddHandler oFrm.AfterUpdate, AddressOf Reload
            Case DTOArea.SelectModes.SelectAny, DTOArea.SelectModes.SelectZip
                RaiseEvent onItemSelected(Me, e)
                Me.Close()
        End Select

    End Sub


#End Region

#Region "AddNew"
    Private Sub Xl_Countries1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Countries1.RequestToAddNew
        Dim oCountry As New DTOCountry
        Dim oFrm As New Frm_Country(oCountry)
        AddHandler oFrm.AfterUpdate, AddressOf AfterAddNewCountry
        oFrm.Show()
    End Sub

    Private Sub Xl_Zonas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.RequestToAddNew
        Dim oCountry As DTOCountry = Xl_Countries1.Value
        If oCountry IsNot Nothing Then
            Dim oZona As DTOZona = DTOZona.Factory(oCountry)
            Dim oFrm As New Frm_Zona(oZona)
            AddHandler oFrm.AfterUpdate, AddressOf AfterAddNewZona
            oFrm.Show()
        End If
    End Sub

    Private Sub Xl_Locations1_requestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToAddNew
        Dim oZona As DTOZona = Xl_Zonas1.Value
        If oZona IsNot Nothing Then
            Dim oLocation As New DTOLocation
            oLocation.Zona = oZona
            Dim oFrm As New Frm_Location(oLocation)
            AddHandler oFrm.AfterUpdate, AddressOf AfterAddNewLocation
            oFrm.Show()
        End If
    End Sub

    Private Sub Xl_Zips1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Zips1.RequestToAddNew
        Dim oLocation As DTOLocation = Xl_Locations1.Value
        If oLocation IsNot Nothing Then
            Dim oZip As New DTOZip
            oZip.Location = oLocation
            Dim oFrm As New Frm_Zip(oZip)
            AddHandler oFrm.AfterUpdate, AddressOf AfterAddNewZip
            oFrm.Show()
        End If
    End Sub

    Private Sub AfterAddNewCountry(sender As Object, e As MatEventArgs)
        _Area = e.Argument
        _Countries.Add(_Area)
        Reload()
    End Sub

    Private Sub AfterAddNewZona(sender As Object, e As MatEventArgs)
        _Area = e.Argument
        _Zonas.Add(_Area)
        Reload()
    End Sub

    Private Sub AfterAddNewLocation(sender As Object, e As MatEventArgs)
        _Area = e.Argument
        _Locations.Add(_Area)
        Reload()
    End Sub

    Private Sub AfterAddNewZip(sender As Object, e As MatEventArgs)
        _Area = e.Argument
        _Zips.Add(_Area)
        Reload()
    End Sub

#End Region


    Private Async Sub Xl_RequestToRefresh(sender As Object, e As MatEventArgs) Handles _
        Xl_Countries1.RequestToRefresh,
        Xl_Zonas1.RequestToRefresh,
        Xl_Locations1.RequestToRefresh,
        Xl_Zips1.RequestToRefresh

        _Area = CurrentArea()
        Await LoadData()
        Reload()
    End Sub

    Private Sub Xl_Countries1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Countries1.ValueChanged
        If _AllowEvents Then
            If RefrescaZonas() Then
                If RefrescaLocations() Then
                    RefrescaZips()
                End If
            End If
        End If
    End Sub

    Private Sub Xl_Zonas1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.ValueChanged
        If _AllowEvents Then
            If RefrescaLocations() Then
                RefrescaZips()
            End If
        End If
    End Sub

    Private Sub Xl_Locations1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Locations1.ValueChanged
        If _AllowEvents Then
            RefrescaZips()
        End If
    End Sub


    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Dim searchKey = e.Argument
        Dim oSearchResults = SearchResults(searchKey, _Countries)
        _XlSearchResults.Visible = Not String.IsNullOrEmpty(searchKey)
        _XlSearchResults.Load(oSearchResults, Nothing, _SelectMode)
        _AllowEvents = True
    End Sub

    Private Function SearchResults(searchKey As String, oCountries As List(Of DTOCountry)) As List(Of DTOArea)
        Dim retval As New List(Of DTOArea)
        Select Case _SelectMode
            Case DTOArea.SelectModes.SelectZip
                retval.AddRange(ZipResults(searchKey, oCountries))
            Case DTOArea.SelectModes.SelectLocation
                retval.AddRange(LocationResults(searchKey, oCountries))
            Case DTOArea.SelectModes.SelectZona
                retval.AddRange(ZonaResults(searchKey, oCountries))
            Case DTOArea.SelectModes.SelectCountry
                retval.AddRange(CountryResults(searchKey, oCountries))
            Case Else
                retval.AddRange(CountryResults(searchKey, oCountries))
                retval.AddRange(ZonaResults(searchKey, oCountries))
                retval.AddRange(LocationResults(searchKey, oCountries))
                retval.AddRange(ZipResults(searchKey, oCountries))
        End Select
        Return retval
    End Function

    Private Function ZipResults(searchKey As String, oCountries As List(Of DTOCountry)) As List(Of DTOArea)
        Dim retval As New List(Of DTOArea)
        retval.AddRange(oCountries.Where(Function(c) c.Matches(searchKey)).SelectMany(Function(z) z.Zonas).SelectMany(Function(l) l.Locations).SelectMany(Function(p) p.Zips))
        retval.AddRange(oCountries.SelectMany(Function(c) c.Zonas).Where(Function(z) z.Matches(searchKey)).SelectMany(Function(z) z.Locations).SelectMany(Function(l) l.Zips))
        retval.AddRange(oCountries.SelectMany(Function(c) c.Zonas).SelectMany(Function(z) z.Locations).Where(Function(l) l.Matches(searchKey)).SelectMany(Function(p) p.Zips))
        retval.AddRange(oCountries.SelectMany(Function(c) c.Zonas).SelectMany(Function(z) z.Locations).SelectMany(Function(p) p.Zips).Where(Function(x) x.Matches(searchKey)))
        retval = retval.GroupBy(Function(x) CType(x, DTOZip).Guid).Select(Function(y) y.First()).
                    OrderBy(Function(c) CType(c, DTOZip).ZipCod).
                    OrderBy(Function(c) CType(c, DTOZip).Location.Nom).
                    OrderBy(Function(c) CType(c, DTOZip).Location.Zona.Nom).
                    OrderBy(Function(c) CType(c, DTOZip).CountryNom(Current.Session.Lang)).ToList()
        Return retval
    End Function

    Private Function LocationResults(searchKey As String, oCountries As List(Of DTOCountry)) As List(Of DTOArea)
        Dim retval As New List(Of DTOArea)
        retval.AddRange(oCountries.Where(Function(c) c.Matches(searchKey)).SelectMany(Function(z) z.Zonas).SelectMany(Function(l) l.Locations))
        retval.AddRange(oCountries.SelectMany(Function(c) c.Zonas).Where(Function(z) z.Matches(searchKey)).SelectMany(Function(z) z.Locations))
        retval.AddRange(oCountries.SelectMany(Function(c) c.Zonas).SelectMany(Function(z) z.Locations).Where(Function(l) l.Matches(searchKey)))
        retval = retval.GroupBy(Function(x) CType(x, DTOLocation).Guid).Select(Function(y) y.First()).
                    OrderBy(Function(c) CType(c, DTOLocation).Nom).
                    OrderBy(Function(c) CType(c, DTOLocation).Zona.Nom).
                    OrderBy(Function(c) CType(c, DTOLocation).Zona.CountryNom(Current.Session.Lang)).ToList()
        Return retval
    End Function

    Private Function ZonaResults(searchKey As String, oCountries As List(Of DTOCountry)) As List(Of DTOArea)
        Dim retval As New List(Of DTOArea)
        retval.AddRange(oCountries.Where(Function(c) c.Matches(searchKey)).SelectMany(Function(z) z.Zonas))
        retval.AddRange(oCountries.SelectMany(Function(c) c.Zonas).Where(Function(z) z.Matches(searchKey)))
        retval = retval.GroupBy(Function(x) CType(x, DTOZona).Guid).Select(Function(y) y.First()).
                    OrderBy(Function(c) CType(c, DTOZona).Nom).
                    OrderBy(Function(c) CType(c, DTOZona).Country.LangNom.Tradueix(Current.Session.Lang)).ToList()
        Return retval
    End Function

    Private Function CountryResults(searchKey As String, oCountries As List(Of DTOCountry)) As List(Of DTOArea)
        Dim retval As New List(Of DTOArea)
        retval.AddRange(oCountries.Where(Function(c) c.Matches(searchKey)))
        retval = retval.
                    OrderBy(Function(c) CType(c, DTOCountry).LangNom.Tradueix(Current.Session.Lang)).ToList()
        Return retval
    End Function

    Private Async Sub Xl_Countries1_RequestToDelete(sender As Object, e As MatEventArgs) Handles Xl_Countries1.RequestToDelete
        Dim exs As New List(Of Exception)
        Dim oCountry As DTOCountry = e.Argument
        If Await FEB.Country.Delete(oCountry, exs) Then
            _Countries.Remove(oCountry)
            Reload()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_Locations1_RequestToDelete(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToDelete
        Dim exs As New List(Of Exception)
        Dim oLocation As DTOLocation = e.Argument
        If Await FEB.Location.Delete(exs, oLocation) Then
            _Locations.Remove(oLocation)
            Reload()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub GeonamesExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GeonamesExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet As New MatHelper.Excel.Sheet
        If UIHelper.LoadExcelSheetDialog(oSheet, "Importar códigos postales de geonames.org") Then
            Dim oZips = DTO.Google.Geonames.ReadFromExcel(oSheet)
            Dim sCountryISOs = oZips.GroupBy(Function(x) x.CountryISO).Select(Function(y) y.First.CountryISO).ToList
            Dim oAllCountriesTree = Await FEB.Zips.Tree(exs)
            Dim oCountryTree = oAllCountriesTree.Where(Function(x) x.ISO = sCountryISOs.First)
            Dim sAllZipCods = oCountryTree.SelectMany(Function(x) x.Zonas).SelectMany(Function(y) y.Locations).SelectMany(Function(z) z.Zips).Select(Function(p) p.ZipCod).ToList
            Dim oMissingGeonameZips As List(Of DTO.Google.Geonames.Zip) = oZips.Where(Function(x) Not sAllZipCods.Any(Function(y) y = x.ZipCod)).ToList
            For Each oGeonameZip In oMissingGeonameZips

            Next
        End If
    End Sub
End Class