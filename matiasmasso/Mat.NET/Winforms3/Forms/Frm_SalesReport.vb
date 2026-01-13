

Public Class Frm_SalesReport
    Private _SalesReport As DTOSalesReport
    Private _AllowEvents As Boolean

    Public Sub New(oSalesReport As DTOSalesReport)
        MyBase.New
        InitializeComponent()
        _SalesReport = oSalesReport
    End Sub

    Private Async Sub Frm_SalesReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        ComboBoxFormat.SelectedIndex = DTOSalesReport.Formats.Eur
        Dim oYears = Await FEB.EdiversaSalesReports.Years(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            LoadConcepts()
            Xl_Years1.Load(oYears, _SalesReport.SelectedExercici.Year)
            If _SalesReport.IsLoaded Then
                ProgressBar1.Visible = False
                refresca()
                _AllowEvents = True
            Else
                If Await LoadData(exs) Then
                    refresca()
                    _AllowEvents = True
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadData(exs As List(Of Exception)) As Task(Of Boolean)
        ProgressBar1.Visible = True
        With _SalesReport
            .SelectedExercici = New DTOExercici(GlobalVariables.Emp, Xl_Years1.Value)
            .Items = New List(Of DTOSalesReport.Item)
        End With
        _SalesReport = Await FEB.EdiversaSalesReportItems.SalesReport(exs, _SalesReport)
        ProgressBar1.Visible = False
        Return exs.Count = 0
    End Function


    Private Sub refresca()
        Dim previousAllowEvents = _AllowEvents
        _AllowEvents = False
        loadCentros()
        LoadDepts()
        Dim oProducts As New List(Of DTOProduct)
        If _SalesReport.SelectedProduct IsNot Nothing Then oProducts.Add(_SalesReport.SelectedProduct)
        Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny, True)
        Xl_SalesReportItems1.Load(_SalesReport)
        _AllowEvents = previousAllowEvents
    End Sub


    Private Sub LoadConcepts()
        For Each nom In [Enum].GetNames(GetType(DTOSalesReport.Concepts))
            ComboBoxConcept.Items.Add(nom)
        Next
        ComboBoxConcept.SelectedIndex = _SalesReport.SelectedConcept
    End Sub

    Private Sub LoadDepts()
        ComboBoxDepts.DisplayMember = "Nom"
        Dim sNoDept = Current.Session.Tradueix("(todos los depts)", "(tots els depts)", "(all depts)")
        ComboBoxDepts.Items.Add(sNoDept)

        For Each sDept In _SalesReport.Depts
            ComboBoxDepts.Items.Add(sDept)
        Next

        If _SalesReport.SelectedDept > "" Then
            ComboBoxDepts.SelectedItem = _SalesReport.SelectedDept
        Else
            ComboBoxDepts.SelectedIndex = 0
        End If
    End Sub

    Private Sub loadCentros()
        ComboBoxCentros.DisplayMember = "Nom"
        Dim oNoCentro As New DTOSalesReport.Centro()
        With oNoCentro
            .guid = Guid.Empty
            .Nom = Current.Session.Tradueix("(todos los centros)", "(tots els centres)", "(all centers)")
        End With
        ComboBoxCentros.Items.Clear()
        ComboBoxCentros.Items.Add(oNoCentro)

        Dim oSortedCentros = _SalesReport.Centros.OrderBy(Function(x) x.Nom).ToList

        For Each oCentro In oSortedCentros
            ComboBoxCentros.Items.Add(oCentro)
        Next

        If _SalesReport.SelectedCentro Is Nothing Then
            ComboBoxCentros.SelectedIndex = 0
        Else
            ComboBoxCentros.SelectedIndex = _SalesReport.Centros.IndexOf(_SalesReport.SelectedCentro) + 1
        End If
    End Sub


    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Dim exs As New List(Of Exception)
        If _AllowEvents Then
            If Await LoadData(exs) Then
                refresca()
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub ComboBoxConcept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxConcept.SelectedIndexChanged
        If _AllowEvents Then
            _SalesReport.SelectedConcept = ComboBoxConcept.SelectedIndex
            refresca()
        End If
    End Sub

    Private Sub ComboBoxFormat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFormat.SelectedIndexChanged
        If _AllowEvents Then
            _SalesReport.SelectedFormat = ComboBoxFormat.SelectedIndex
            refresca()
        End If
    End Sub

    Private Sub Xl_LookupProduct1_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.RequestToLookup
        Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectAny, e.Argument, IncludeObsoletos:=True, oCustomCatalog:=_SalesReport.Catalog)
        AddHandler oFrm.OnItemSelected, AddressOf Xl_LookupProduct1_AfterUpdate
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        Dim oProducts As New List(Of DTOProduct)
        If e.Argument IsNot Nothing Then oProducts.Add(e.Argument)
        Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
        _SalesReport.SelectedProduct = e.Argument
        refresca()
    End Sub

    Private Sub ComboBoxCentros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCentros.SelectedIndexChanged
        If _AllowEvents Then
            If ComboBoxCentros.SelectedIndex = 0 Then
                _SalesReport.SelectedCentro = Nothing
            Else
                _SalesReport.SelectedCentro = ComboBoxCentros.SelectedItem
            End If
            refresca()
        End If
    End Sub

    Private Sub ComboBoxDepts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxDepts.SelectedIndexChanged
        If _AllowEvents Then
            If ComboBoxDepts.SelectedIndex = 0 Then
                _SalesReport.SelectedDept = ""
            Else
                _SalesReport.SelectedDept = ComboBoxDepts.SelectedItem
            End If
            refresca()
        End If
    End Sub

    Private Sub CheckBoxProduct_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProduct.CheckedChanged
        Xl_LookupProduct1.Visible = CheckBoxProduct.Checked
        If Not CheckBoxProduct.Checked Then
            Xl_LookupProduct1.Clear()
            _SalesReport.SelectedProduct = Nothing
            refresca()
        End If
    End Sub

    Private Sub ButtonExcel_Click(sender As Object, e As EventArgs) Handles ButtonExcel.Click
        Dim exs As New List(Of Exception)
        Dim oSheet = _SalesReport.Excel
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class