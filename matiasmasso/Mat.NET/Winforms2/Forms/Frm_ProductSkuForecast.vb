Public Class Frm_ProductSkuForecast
    Private _Proveidor As DTOProveidor
    Private _Product As DTOProduct
    Private _Forecasts As DTOProductSkuForecast.Collection
    Private _allowEvents As Boolean

    Private Enum Tabs
        Forecast
        Proposal
    End Enum

    Public Sub New(oProveidor As DTOProveidor)
        MyBase.New
        InitializeComponent()
        _Proveidor = oProveidor
    End Sub

    Public Sub New(oProduct As DTOProduct)
        MyBase.New
        InitializeComponent()
        _Product = oProduct
    End Sub

    Private Async Sub Frm_ProductSkuForecast_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Forecasts = Await FEB.Forecasts.All(Current.Session.Emp.Mgz, exs, _Proveidor, _Product)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            LoadYears()
            Xl_Forecasts1.Load(_Forecasts, Xl_Years1.Value)
            _allowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadYears()
        Dim iYears As List(Of Integer) = _Forecasts.SelectMany(Function(x) x.Forecasts).Select(Function(y) y.YearMonth.Year).Distinct.ToList

        For i As Integer = DTO.GlobalVariables.Today().Year To DTO.GlobalVariables.Today().Year + 5
            If Not iYears.Contains(i) Then
                iYears.Add(i)
            End If
        Next

        Xl_Years1.Load(iYears, DTO.GlobalVariables.Today().Year)
    End Sub

    Private Sub Xl_ProductCategoriesCheckedList1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ProductCategoriesCheckedList1.AfterUpdate
        If _allowEvents Then refresca()
    End Sub

    Private Sub refresca()
        Xl_ProductSkuForecasts1.Load(FilteredValues(), DateTimePicker1.Value)
    End Sub

    Private Function FilteredValues() As DTOProductSkuForecast.Collection
        Dim retval As New DTOProductSkuForecast.Collection
        Dim oCategories = Xl_ProductCategoriesCheckedList1.CheckedValues
        retval.addrange(_Forecasts.Where(Function(x) oCategories.Any(Function(y) y.Equals(x.Category))))
        Return retval
    End Function

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If _allowEvents Then refresca()
    End Sub

    Private Sub NumericUpDownM3_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownM3.ValueChanged
        If _allowEvents Then
            _allowEvents = False
            Dim volume As Decimal = 0
            Dim target As Decimal = NumericUpDownM3.Value
            Dim oFilteredValues = FilteredValues()
            Dim maxTag = oFilteredValues.SelectMany(Function(x) x.Forecasts).Max(Function(y) y.YearMonth.Tag)
            Dim maxYearMonth = DTOYearMonth.FromTag(maxTag)
            DateTimePicker1.Value = DTO.GlobalVariables.Today()
            Application.DoEvents()
            Do Until volume >= target 'endavant dia a dia
                If maxYearMonth.IsLowerThan(DTOYearMonth.FromFch(DateTimePicker1.Value.AddDays(1))) Then
                    Stop
                    Exit Do
                End If
                DateTimePicker1.Value = DateTimePicker1.Value.AddDays(1)
                Application.DoEvents()
                volume = DTOProductSkuForecast.Volume(oFilteredValues, DateTimePicker1.Value)
            Loop
            Dim DtFch As Date = DateTimePicker1.Value
            For hours = 1 To 23 'enrera hora a hora
                DtFch = DtFch.AddHours(-1)
                volume = DTOProductSkuForecast.Volume(oFilteredValues, DtFch)
                If volume < target Then Exit For
            Next
            For minutes = 1 To 59 'endavant minut a minut
                DtFch = DtFch.AddMinutes(1)
                volume = DTOProductSkuForecast.Volume(oFilteredValues, DtFch)
                If volume > target Then
                    Exit For
                End If
            Next
            DateTimePicker1.Value = DtFch
            refresca()
            _allowEvents = True
        End If
    End Sub

    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Xl_Forecasts1.Load(_Forecasts, Xl_Years1.Value)
    End Sub

    Private Sub Xl_Forecasts1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Forecasts1.AfterUpdate
        _Forecasts = e.Argument
    End Sub

    Private Async Sub ComandaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ComandaToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        If FEB.Proveidor.Load(_Proveidor, exs) Then
            Dim oOrder = DTOPurchaseOrder.Factory(GlobalVariables.Emp, _Proveidor, Current.Session.User)
            With oOrder
                .Concept = "regular"
                .Source = DTOPurchaseOrder.Sources.eMail
                .Items = Xl_ProductSkuForecasts1.PurchaseOrderItems()
            End With

            If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, _Proveidor, DTOAlbBloqueig.Codis.PDC, exs) Then
                Dim oFrm As New Frm_PurchaseOrder_Proveidor(oOrder)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Proposal
                Dim oCategories = DTOProductSku.Categories(_Forecasts).Where(Function(x) DisplayableCategory(x)).ToList
                Xl_ProductCategoriesCheckedList1.Load(oCategories, oCategories)
                refresca()
        End Select
    End Sub

    Private Function DisplayableCategory(oCategory As DTOProductCategory) As Boolean
        Dim retval As Boolean = True
        Dim oSkus = _Forecasts.Where(Function(x) x.category.Equals(oCategory)).ToList
        Dim isBundle = oSkus.All(Function(x) x.isBundle)
        If oCategory.obsoleto Then retval = False
        If isBundle Then retval = False
        Return retval
    End Function

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet = DTOProductSkuForecast.Excel(_Forecasts, DTOLang.ENG)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub EstimationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EstimationToolStripMenuItem.Click

    End Sub

    Private Sub ExcelComandaPerCategoriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelComandaPerCategoriesToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim items = Xl_ProductSkuForecasts1.PurchaseOrderItems()
        'Dim query = items.GroupBy(Function(g) New With {Key g.Sku.Category.Brand, Key g.Sku.Category}).
        'Select Case (Function(group) New With {.Brand = group.Key.Category.Brand, .Category = group.Key.Category, .Qty = group.Sum(Function(y) y.Qty), .Eur = group.Sum(Function(z) z.Qty * z.amount.Eur * (100 - z.Dto) / 100)})

        Dim oSheet As New MatHelper.Excel.Sheet
        For Each item In items 'query
            Dim oRow = oSheet.AddRow()
            oRow.AddCell(item.Sku.Category.Brand.Nom.Tradueix(DTOLang.ENG))
            oRow.AddCell(item.Sku.Category.Nom.Tradueix(DTOLang.ENG))
            oRow.AddCell(item.Sku.NomLlarg.Tradueix(DTOLang.ENG))
            oRow.AddCell(item.Qty)
            oRow.AddCell(item.preuNet.Eur)
        Next

        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class