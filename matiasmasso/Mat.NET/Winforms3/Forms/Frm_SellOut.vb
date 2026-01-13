Public Class Frm_SellOut
    Private _value As DTOSellOut
    Private _AllowEvents As Boolean

    Public Sub New(Optional oSellOut As DTOSellOut = Nothing)
        MyBase.New
        InitializeComponent()

        _value = oSellOut
    End Sub

    Private Async Sub Frm_SellOut2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _value Is Nothing Then
            _value = Await FEB.SellOut.Factory(exs, Current.Session.User)
        End If
        If exs.Count = 0 Then
            LoadConceptTypes()
            ComboBoxConceptType.SelectedIndex = _value.ConceptType
            ComboBoxFormat.SelectedIndex = _value.Format
            If Await LoadYears() Then
                Await refresca()
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Application.DoEvents()

        LoadFilters()
        Dim oSellout = Await FEB.SellOut.Load(exs, _value)
        If exs.Count = 0 Then
            _value = oSellout
            Xl_SellOut1.Load(_value)

            ProgressBar1.Visible = False
            Application.DoEvents()
            _AllowEvents = True
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub LoadFilters()
        Xl_SelloutFilters1.Load(_value)
        _value.IsBundle = BundlesToolStripMenuItem.Checked
    End Sub

    Private Async Function LoadYears() As Task(Of Boolean)
        Dim exs As New List(Of Exception)
        Dim iYears As List(Of Integer) = Await FEB.SellOut.Years(exs, _value)
        If exs.Count = 0 Then
            Dim iYear As Integer = _value.YearMonths.Last.Year
            If Xl_Years1.IsEmpty Then Xl_Years1.Load(iYears, iYear)
        Else
            UIHelper.WarnError(exs)
        End If
        Return exs.Count = 0
    End Function


    Private Sub LoadConceptTypes()
        Dim oLang As DTOLang = Current.Session.Lang
        Dim items As New List(Of String)
        For Each cod In [Enum].GetValues(GetType(DTOSellOut.ConceptTypes))
            ComboBoxConceptType.Items.Add(DTOSellOut.ConceptTypeNom(cod, oLang))
        Next
        ComboBoxConceptType.SelectedIndex = _value.ConceptType
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        If _AllowEvents Then
            Dim iYear As Integer = Xl_Years1.Value
            Dim oYearMonthTo As New DTOYearMonth(iYear, 12)
            _value.YearMonths = oYearMonthTo.Last12YearMonths()
            Await refresca()
        End If
    End Sub

    Private Async Sub ComboBoxConceptType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxConceptType.SelectedIndexChanged
        If _AllowEvents Then
            _value.ConceptType = ComboBoxConceptType.SelectedIndex
            Await refresca()
        End If
    End Sub

    Private Sub onProductFilter(sender As Object, e As MatEventArgs)
        Dim oProduct As DTOProduct = e.Argument
        Dim value As New DTOGuidNom(oProduct.Guid, oProduct.FullNom(Current.Session.Lang))
        AddFilterValue(value, DTOSellOut.Filter.Cods.Product)
    End Sub

    Private Sub onAtlasFilter(sender As Object, e As MatEventArgs)
        Dim oArea As DTOArea = e.Argument
        Dim value As New DTOGuidNom(oArea.Guid, DTOArea.FullNom(oArea, Current.Session.Lang))
        AddFilterValue(value, DTOSellOut.Filter.Cods.Atlas)
    End Sub

    Private Async Sub onChannelFilter(sender As Object, e As MatEventArgs)
        Dim values As New List(Of DTOGuidNom)
        For Each oChannel As DTODistributionChannel In e.Argument
            Dim value As New DTOGuidNom(oChannel.Guid, DTODistributionChannel.Nom(oChannel, Current.Session.Lang))
            values.Add(value)
        Next
        Await AddFilterValues(values, DTOSellOut.Filter.Cods.Channel)
    End Sub

    Private Sub onCustomerFilter(sender As Object, e As MatEventArgs)
        Dim oCustomer As DTOCustomer = e.Argument
        Dim value As New DTOGuidNom(oCustomer.Guid, oCustomer.FullNom)
        AddFilterValue(value, DTOSellOut.Filter.Cods.Customer)
    End Sub

    Private Async Sub onProviderFilter(sender As Object, e As MatEventArgs)
        Dim values As New List(Of DTOGuidNom)
        For Each oProveidor In e.Argument
            Dim value As New DTOGuidNom(oProveidor.Guid, oProveidor.nom)
            values.Add(value)
        Next
        Await AddFilterValues(values, DTOSellOut.Filter.Cods.Provider)
    End Sub

    Private Async Sub onRepFilter(sender As Object, e As MatEventArgs)
        Dim values As New List(Of DTOGuidNom)
        For Each oRep In e.Argument
            Dim value As New DTOGuidNom(oRep.Guid, oRep.NicknameOrNom())
            values.Add(value)
        Next
        Await AddFilterValues(values, DTOSellOut.Filter.Cods.Rep)
    End Sub

    Private Async Function AddFilterValues(oValues As IEnumerable(Of DTOGuidNom), oCod As DTOSellOut.Filter.Cods) As Task
        If oValues IsNot Nothing Then
            Dim oFilter As DTOSellOut.Filter = _value.Filters.FirstOrDefault(Function(x) x.Cod = oCod)
            If oFilter IsNot Nothing Then
                oFilter.Values = oValues
                Await refresca()
            End If
        End If
    End Function

    Private Async Sub AddFilterValue(oValue As DTOGuidNom, oCod As DTOSellOut.Filter.Cods)
        Dim exs As New List(Of Exception)
        If oValue IsNot Nothing Then
            Dim oFilter As DTOSellOut.Filter = _value.Filters.FirstOrDefault(Function(x) x.Cod = oCod)
            If oFilter IsNot Nothing Then
                oFilter.Values.Add(oValue)
                LoadFilters()
                Dim oSellOut = Await FEB.SellOut.Load(exs, _value)
                If exs.Count = 0 Then
                    _value = oSellOut
                    Xl_SellOut1.Load(_value)
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End If
    End Sub

    Private Async Sub Xl_SelloutFilters1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_SelloutFilters1.AfterUpdate
        Dim exs As New List(Of Exception)
        _value.Filters = e.Argument
        LoadFilters()
        Dim oSellOut = Await FEB.SellOut.Load(exs, _value)
        If exs.Count = 0 Then
            Xl_SellOut1.Load(_value)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_SelloutFilters1_RequestToFilter(sender As Object, e As MatEventArgs) Handles Xl_SelloutFilters1.RequestToFilter
        Dim exs As New List(Of Exception)
        Dim oFilter As DTOSellOut.Filter = e.Argument
        If oFilter IsNot Nothing Then
            Select Case oFilter.Cod
                Case DTOSellOut.Filter.Cods.Product
                    Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectAny)
                    AddHandler oFrm.onItemSelected, AddressOf onProductFilter
                    oFrm.Show()
                Case DTOSellOut.Filter.Cods.Atlas
                    Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectAny)
                    AddHandler oFrm.onItemSelected, AddressOf onAtlasFilter
                    oFrm.Show()
                Case DTOSellOut.Filter.Cods.Channel
                    Dim oAllValues = Await FEB.SellOut.Channels(exs, _value.Clone)
                    If exs.Count = 0 Then
                        Dim oSelectedValues = oFilter.Values
                        Dim oFrm As New Frm_BaseGuids_Checklist(oAllValues.ToArray, oSelectedValues.ToArray)
                        AddHandler oFrm.AfterUpdate, AddressOf onChannelFilter
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case DTOSellOut.Filter.Cods.Customer
                    Dim oFrm As New Frm_ContactSearch
                    AddHandler oFrm.itemSelected, AddressOf onCustomerFilter
                    oFrm.Show()
                Case DTOSellOut.Filter.Cods.Provider
                    Dim oAllValues = Await FEB.SellOut.Proveidors(exs, _value.Clone)
                    If exs.Count = 0 Then
                        Dim oSelectedValues = oFilter.Values
                        Dim oFrm As New Frm_BaseGuids_Checklist(oAllValues.ToArray, oSelectedValues.ToArray)
                        AddHandler oFrm.AfterUpdate, AddressOf onProviderFilter
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case DTOSellOut.Filter.Cods.Rep
                    Dim oAllValues = Await FEB.SellOut.Reps(exs, _value.Clone)
                    If exs.Count = 0 Then
                        Dim oSelectedValues = oFilter.Values
                        Dim oFrm As New Frm_BaseGuids_Checklist(oAllValues.ToArray, oSelectedValues.ToArray)
                        AddHandler oFrm.AfterUpdate, AddressOf onRepFilter
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
            End Select
        End If
    End Sub

    Private Async Sub ComboBoxFormat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFormat.SelectedIndexChanged

        If _AllowEvents Then
            _value.Format = ComboBoxFormat.SelectedIndex
            Await refresca()
        End If
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet = DTOSellOut.Excel(exs, _value)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub BundlesToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles BundlesToolStripMenuItem.CheckedChanged
        If _AllowEvents Then
            _value.IsBundle = BundlesToolStripMenuItem.Checked
            Await refresca()
        End If
    End Sub
End Class