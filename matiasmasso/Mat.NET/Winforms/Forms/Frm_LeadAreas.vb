Public Class Frm_LeadAreas
    Private _AllAreas As List(Of DTOLeadAreas)
    Private _Areas As List(Of DTOLeadAreas)
    Private _Allowevents As Boolean

    Private Async Sub Frm_LeadAreas_Load(sender As Object, e As EventArgs) Handles Me.Load
        _AllAreas = Await LoadAreasFromFactory()
        _Areas = _AllAreas

        refresca()
        _Allowevents = True
    End Sub

    Private Async Function LoadAreasFromFactory() As Task(Of List(Of DTOLeadAreas))
        Dim retval As List(Of DTOLeadAreas)
        Me.Cursor = Cursors.WaitCursor

        ProgressBar1.Visible = True
        PictureBoxSaveFile.Visible = False
        LabelLeadsCount.Visible = False
        ArxiuToolStripMenuItem.Visible = False
        SplitContainer1.Visible = False
        CheckBoxHideShops.Visible = False


        Application.DoEvents()

        Dim exs As New List(Of Exception)
        retval = Await FEB2.LeadAreas.All(exs, CheckBoxHideShops.Checked)
        If exs.Count = 0 Then
            ProgressBar1.Visible = False
            PictureBoxSaveFile.Visible = True
            LabelLeadsCount.Visible = True
            ArxiuToolStripMenuItem.Visible = True
            SplitContainer1.Visible = True
            CheckBoxHideShops.Visible = True
            Me.Cursor = Cursors.Default
        Else
            Me.Cursor = Cursors.Default
            UIHelper.WarnError(exs)
        End If
        Return retval
    End Function

    Private Sub refresca()
        RefreshCount()
        Xl_LeadAreasCountry.Load(_Areas)

        Dim oLeadCountry As DTOLeadAreas = Xl_LeadAreasCountry.Value
        Xl_LeadAreasZona.Load(_AllAreas, oLeadCountry)

        Dim oLeadLocation As DTOLeadAreas = Xl_LeadAreasZona.Value
        Xl_LeadAreasLocation.Load(_AllAreas, oLeadLocation)
    End Sub

    Private Sub RefreshCount()
        Dim iCheckedCount As Integer = CheckedCount()
        LabelLeadsCount.Text = String.Format("{1:#,0} sel.leccionats sobre un total de {0:#,0} leads", ProgressBar1.Maximum, iCheckedCount)
        If iCheckedCount = 0 Then
            PictureBoxSaveFile.Image = My.Resources.SaveFilex48disabled
        Else
            PictureBoxSaveFile.Image = My.Resources.SaveFilex48
        End If
    End Sub

    Private Function CheckedCount() As Integer
        Dim retval As Integer
        For Each oArea As DTOLeadAreas In _AllAreas
            retval += oArea.Leads.Where(Function(x) x.Checked = True).Count
        Next
        Return retval
    End Function

    Public Sub Do_Progress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByVal label As String, ByRef CancelRequest As Boolean)
        With ProgressBar1
            .Minimum = min
            .Maximum = max
            .Value = value + 1
        End With
        Application.DoEvents()
    End Sub

    Private Function LeadCountries() As List(Of DTOLeadAreas)
        Dim retval As New List(Of DTOLeadAreas)
        Dim oCountry As New DTOCountry
        Dim oLeadCountry As New DTOLeadArea(oCountry)
        For Each item As DTOLeadAreas In _AllAreas
            If DirectCast(item.Area, DTOLocation).Zona.Country.UnEquals(oLeadCountry.Area) Then
                oLeadCountry = New DTOLeadArea(DirectCast(item.Area, DTOLocation).Zona.Country)
                retval.Add(oLeadCountry)
            End If
            oLeadCountry.Leads.AddRange(item.Leads)
        Next
        Return retval
    End Function

    Private Function LeadZonas(oLeadCountry As DTOLeadAreas) As List(Of DTOLeadAreas)
        Dim retval As New List(Of DTOLeadAreas)
        Dim oParent As DTOCountry = oLeadCountry.Area
        Dim oAreas As List(Of DTOLeadAreas) = _AllAreas.FindAll(Function(x) DirectCast(x.Area, DTOLocation).Zona.Country.Equals(oParent))
        Dim oZona As New DTOZona
        Dim oLeadZona As New DTOLeadArea(oZona)
        For Each item As DTOLeadAreas In oAreas
            If DirectCast(item.Area, DTOLocation).Zona.UnEquals(oLeadZona.Area) Then
                oLeadZona = New DTOLeadArea(DirectCast(item.Area, DTOLocation).Zona)
                retval.Add(oLeadZona)
            End If
            oLeadZona.Leads.AddRange(item.Leads)
        Next
        Return retval
    End Function

    Private Function LeadLocations(oLeadZona As DTOLeadAreas) As List(Of DTOLeadAreas)
        Dim oParent As DTOZona = oLeadZona.Area
        Dim retval As List(Of DTOLeadAreas) = _AllAreas.FindAll(Function(x) DirectCast(x.Area, DTOLocation).Zona.Equals(oParent))
        Return retval
    End Function

    Private Sub Xl_LeadAreasCountry_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_LeadAreasCountry.ValueChanged
        Dim oLeadCountry As DTOLeadAreas = e.Argument

        Xl_LeadAreasZona.Load(_AllAreas, oLeadCountry)

        Dim oLeadLocation As DTOLeadAreas = Xl_LeadAreasZona.Value
        Xl_LeadAreasLocation.Load(_AllAreas, oLeadLocation)
    End Sub

    Private Sub Xl_LeadAreasZona_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_LeadAreasZona.ValueChanged
        Dim oLeadZona As DTOLeadAreas = e.Argument
        Xl_LeadAreasLocation.Load(_AllAreas, oLeadZona)
    End Sub

    Private Sub DesarCsvToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesarCsvToolStripMenuItem.Click
        Dim oCsv As DTOCsv = DTOLeadAreas.Csv(_AllAreas)
        UIHelper.SaveCsvDialog(oCsv, "Desar fitxer Csv de emails de consumidor")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBoxSaveFile.Click
        Dim oCsv As DTOCsv = DTOLeadAreas.Csv(_AllAreas)
        UIHelper.SaveCsvDialog(oCsv, "Desar fitxer Csv de emails de consumidor")
    End Sub

    Private Sub Xl_LeadAreasCountry_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Xl_LeadAreasCountry.ItemCheck
        If sender.Equals(Xl_LeadAreasCountry) Then
            Dim checkedValue As Boolean = (e.NewValue = CheckState.Checked)
            Dim oLeadCountry As DTOLeadAreas = Xl_LeadAreasCountry.FilteredValues(e.Index)
            Dim oLeadLocations As List(Of DTOLeadAreas) = _AllAreas.FindAll(Function(x) DirectCast(x.Area, DTOLocation).Zona.Country.Equals(oLeadCountry.Area)).ToList
            For Each oLeadLocation As DTOLeadAreas In oLeadLocations
                For Each oLead As DTOLeadChecked In oLeadLocation.Leads
                    oLead.Checked = checkedValue
                Next
            Next

            Xl_LeadAreasZona.Refresh()
            Xl_LeadAreasLocation.Refresh()
            RefrescaLeads()
            RefreshCount()
        End If
    End Sub

    Private Sub Xl_LeadAreasZona_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Xl_LeadAreasZona.ItemCheck
        If sender.Equals(Xl_LeadAreasZona) Then
            Dim checkedValue As Boolean = (e.NewValue = CheckState.Checked)
            Dim oLeadZona As DTOLeadAreas = Xl_LeadAreasZona.FilteredValues(e.Index)
            Dim oLeadLocations As List(Of DTOLeadAreas) = _AllAreas.FindAll(Function(x) DirectCast(x.Area, DTOLocation).Zona.Equals(oLeadZona.Area)).ToList
            For Each oLeadLocation As DTOLeadAreas In oLeadLocations
                For Each oLead As DTOLeadChecked In oLeadLocation.Leads
                    oLead.Checked = checkedValue
                Next
            Next

            Xl_LeadAreasCountry.Refresh()
            Xl_LeadAreasLocation.Refresh()
            RefrescaLeads()
            RefreshCount()
        End If
    End Sub

    Private Sub Xl_LeadAreasLocation_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Xl_LeadAreasLocation.ItemCheck
        If sender.Equals(Xl_LeadAreasLocation) Then
            Dim checkedValue As Boolean = (e.NewValue = CheckState.Checked)
            Dim oLeadLocation As DTOLeadAreas = Xl_LeadAreasLocation.FilteredValues(e.Index)
            For Each oLead As DTOLeadChecked In oLeadLocation.Leads
                oLead.Checked = checkedValue
            Next

            Xl_LeadAreasCountry.Refresh()
            Xl_LeadAreasZona.Refresh()
            RefrescaLeads()
            RefreshCount()
        End If
    End Sub

    Private Async Sub CheckBoxHideShops_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxHideShops.CheckedChanged
        If _Allowevents Then
            _AllAreas = Await LoadAreasFromFactory()
            _Areas = _AllAreas
            refresca()
        End If
    End Sub

    Private Sub RefrescaLeads()
        Xl_leadsChecked1.Load(Leads)
    End Sub

    Private Function Leads() As List(Of DTOLeadChecked)
        Dim retval As New List(Of DTOLeadChecked)
        For Each value As DTOLeadAreas In _AllAreas
            For Each item As DTOLeadChecked In value.Leads.FindAll(Function(x) x.Checked = True)
                retval.Add(item)
            Next
        Next
        Return retval
    End Function
End Class