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
    'Private _reloadable As Boolean
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oSelectMode As DTOArea.SelectModes = DTOArea.SelectModes.Browse, Optional oArea As DTOArea = Nothing, Optional oCountries As List(Of DTOCountry) = Nothing)
        MyBase.New()
        Me.InitializeComponent()

        _Area = IIf(oArea Is Nothing, Current.Session.Emp.Org.Address.Zip, oArea)
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
    End Sub

    Private Async Function LoadData() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Countries = Await FEB2.Zips.Tree(exs, Current.Session.Lang)
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
        LoadFilter(e.Argument)
        refresca(_Area)
        _AllowEvents = True
    End Sub

    Private Async Sub Xl_Countries1_RequestToDelete(sender As Object, e As MatEventArgs) Handles Xl_Countries1.RequestToDelete
        Dim exs As New List(Of Exception)
        Dim oCountry As DTOCountry = e.Argument
        If Await FEB2.Country.Delete(oCountry, exs) Then
            _Countries.Remove(oCountry)
            Reload()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_Locations1_RequestToDelete(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToDelete
        Dim exs As New List(Of Exception)
        Dim oLocation As DTOLocation = e.Argument
        If Await FEB2.Location.Delete(exs, oLocation) Then
            _Locations.Remove(oLocation)
            Reload()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub GeonamesExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GeonamesExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet As New MatHelperStd.ExcelHelper.Sheet
        If UIHelper.LoadExcelSheetDialog(oSheet, "Importar códigos postales de geonames.org") Then
            Dim oZips = DT2.Google.Geonames.ReadFromExcel(oSheet)
            Dim sCountryISOs = oZips.GroupBy(Function(x) x.CountryISO).Select(Function(y) y.First.CountryISO).ToList
            Dim oAllCountriesTree = Await FEB2.Zips.Tree(exs)
            Dim oCountryTree = oAllCountriesTree.Where(Function(x) x.ISO = sCountryISOs.First)
            Dim sAllZipCods = oCountryTree.SelectMany(Function(x) x.Zonas).SelectMany(Function(y) y.Locations).SelectMany(Function(z) z.Zips).Select(Function(p) p.ZipCod).ToList
            Dim oMissingGeonameZips As List(Of DT2.Google.Geonames.Zip) = oZips.Where(Function(x) Not sAllZipCods.Any(Function(y) y = x.ZipCod)).ToList
            For Each oGeonameZip In oMissingGeonameZips

            Next
        End If
    End Sub
End Class