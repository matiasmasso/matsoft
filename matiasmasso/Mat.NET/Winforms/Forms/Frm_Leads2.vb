Public Class Frm_Leads2
    Private _value As DTOLeadAreas
    Private _RolsValue As DTOLeadAreas
    Private Async Sub Frm_Leads2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _value = Await FEB2.LeadAreas.All(exs, Current.Session.Lang)
        Dim oRols = New List(Of DTORol)
        oRols.Add(New DTORol(DTORol.Ids.lead))
        _RolsValue = _value.ForRols(oRols)

        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            LoadCountries()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
    Private Sub LoadCountries()
        Dim items As New List(Of DTOCheckedGuidNom)
        For Each oCountry In _RolsValue.Countries
            Dim item As New DTOCheckedGuidNom
            With item
                .Guid = oCountry.Guid
                .Nom = oCountry.Nom
                .Tag = oCountry
                .Checked = False
            End With
            items.Add(item)
        Next
        Xl_CheckedGuidNomsCountries.Load(items, "Països")

    End Sub
    Private Sub LoadZonas()
        Dim oCountries = Xl_CheckedGuidNomsCountries.CheckedValues
        ' Dim oZonas = _value.RolsCountries(oRols)
        Dim items As New List(Of DTOCheckedGuidNom)
        For Each oCountry In oCountries
            Dim item As New DTOCheckedGuidNom
            With item
                .Guid = oCountry.Guid
                .Nom = oCountry.Nom
                .Tag = oCountry
                .Checked = False
            End With
            items.Add(item)
        Next
        Xl_CheckedGuidNomsCountries.Load(items, "Països")

    End Sub






    Private Sub Xl_CheckedGuidNomsCountries_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_CheckedGuidNomsCountries.ValueChanged
        Dim value As DTOCheckedGuidNom = e.Argument
        Dim country As DTOLeadAreas.Country = value.Tag
        Dim items As List(Of DTOCheckedGuidNom) = country.Zonas.ToCheckedGuidNoms()
        Xl_CheckedGuidNomsZonas.Load(items)
    End Sub
End Class