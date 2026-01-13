Public Class Xl_GeoNamesPostalCode
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Paisos
        altres
        ES
        PT
        AD
    End Enum

    Public Sub New()
        MyBase.New
        InitializeComponent()
        ComboBoxCountry.SelectedIndex = Paisos.ES
    End Sub

    Public Shadows Sub Load(oZip As DTOZip)
        SetCountry(oZip)
        _AllowEvents = True
    End Sub

    Private Sub SetCountry(oZip As DTOZip)
        Dim oCountry = DTOZip.Country(oZip)
        Dim oPais As Paisos = Paisos.altres
        If [Enum].TryParse(Of Paisos)(oCountry.ISO, True, oPais) Then
            ComboBoxCountry.SelectedIndex = oPais
        Else
            ComboBoxCountry.Items.Add(oCountry.ISO)
            ComboBoxCountry.SelectedIndex = LastPaisIndex()
        End If
    End Sub

    Private Function LastPaisIndex() As Integer
        Return ComboBoxCountry.Items.Count - 1
    End Function

    Private Sub ComboBoxCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCountry.SelectedIndexChanged
        If _AllowEvents Then
            Select Case ComboBoxCountry.SelectedValue
                Case Paisos.altres
                    TextBoxZip.Visible = False
                    Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZip)
                    AddHandler oFrm.onItemSelected, AddressOf onZipSelected
                Case Else
                    TextBoxZip.Visible = True
            End Select
        End If
    End Sub

    Private Sub onZipSelected(sender As Object, e As MatEventArgs)
        Xl_Lookup_Zip1.Load(e.Argument)
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Async Sub TextBoxZip_Validated(sender As Object, e As EventArgs) Handles TextBoxZip.Validated
        Dim exs As New List(Of Exception)
        Dim sCountryIso = "ES" 'ComboBoxCountry.SelectedValue
        Dim sZipCod = TextBoxZip.Text
        If sZipCod = "" Then
            Xl_Lookup_Zip1.Clear()
        Else
            Dim oPostalCodes As List(Of DTO.Google.Geonames.postalCodeClass) = Await FEB.GeoNames.Locations(exs, sCountryIso, sZipCod)
            Select Case oPostalCodes.Count
                Case 0
                    UIHelper.WarnError("No s´ha trobat cap població amb el codi postal " & sZipCod)
                Case 1
                    Await loadZip(oPostalCodes.First)
                Case Else
                    loadPlaceNames(oPostalCodes)
            End Select

        End If
    End Sub

    Private Async Function loadZip(oPostalCode As DTO.Google.Geonames.postalCodeClass) As Task
        Dim exs As New List(Of Exception)
        Dim oCountry = Await CurrentCountry(exs)
        If exs.Count = 0 Then
            Dim sZipCod As String = oPostalCode.postalCode
            Dim oZips = Await FEB.Zips.All(exs, oCountry, sZipCod)
            If exs.Count = 0 Then

            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function CurrentCountry(exs As List(Of Exception)) As Task(Of DTOCountry)
        Dim retval As DTOCountry = Nothing
        Dim ISO = ComboBoxCountry.SelectedValue
        Select Case ComboBoxCountry.SelectedValue
            Case "ES"
                retval = DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)
            Case "PT"
                retval = DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal)
            Case "AD"
                retval = DTOCountry.Wellknown(DTOCountry.Wellknowns.Andorra)
            Case Else
                retval = Await FEB.Country.FromIso(ISO, exs)
        End Select
        Return retval
    End Function

    Private Sub loadPlaceNames(oPostalCodes As List(Of DTO.Google.Geonames.postalCodeClass))
        _AllowEvents = False
        Xl_Lookup_Zip1.Visible = False
        ComboBoxPlaceNames.Location = Xl_Lookup_Zip1.Location
        ComboBoxPlaceNames.Size = Xl_Lookup_Zip1.Size
        ComboBoxPlaceNames.Visible = True
        ComboBoxPlaceNames.Tag = oPostalCodes
        For Each PostalCode In oPostalCodes
            ComboBoxPlaceNames.Items.Add(PostalCode.placeName)
        Next
        ComboBoxPlaceNames.Items.Add("(tria una població)")
        ComboBoxPlaceNames.SelectedIndex = ComboBoxPlaceNames.Items.Count - 1
        _AllowEvents = True
    End Sub

    Private Async Sub ComboBoxPlaceNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPlaceNames.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        If _AllowEvents Then
            Dim oPostalCodes As List(Of DTO.Google.Geonames.postalCodeClass) = ComboBoxPlaceNames.Tag
            If ComboBoxPlaceNames.SelectedIndex = ComboBoxPlaceNames.Items.Count - 1 Then
                TextBoxZip.Visible = False
                Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZip)
                AddHandler oFrm.onItemSelected, AddressOf onZipSelected

            Else
                Dim oPostalCode = oPostalCodes(ComboBoxPlaceNames.SelectedIndex)
                Dim oZonas = Await FEB.Zonas.All(exs, oPostalCode)

            End If
        End If
    End Sub

End Class
