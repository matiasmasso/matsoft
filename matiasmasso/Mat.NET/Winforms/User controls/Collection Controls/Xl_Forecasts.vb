Public Class Xl_Forecasts

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As DTOProductSkuForecast.Collection
    Private _Year As Integer

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)


    Private Enum Cols
        Nom
        ico
        Tot
        M1
        M2
        M3
        M4
        M5
        M6
        M7
        M8
        M9
        M10
        M11
        M12
    End Enum

    Public Shadows Sub Load(values As DTOProductSkuForecast.Collection, iYear As Integer)
        _Values = values
        _Year = iYear

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems

        Dim oControlItem As ControlItem = Nothing
        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory

        For Each oSku As DTOProductSkuForecast In _Values

            If displayable(oSku) Then

                If oSku.Category.unEquals(oCategory) Then
                    If oSku.Category.Brand.unEquals(oBrand) Then
                        oBrand = oSku.Category.Brand
                        oControlItem = New ControlItem(ControlItem.LinCods.Forecast, oBrand, _Year, _Values)
                        _ControlItems.Add(oControlItem)
                        oControlItem = New ControlItem(ControlItem.LinCods.Sales, oBrand, _Year, _Values)
                        _ControlItems.Add(oControlItem)
                    End If
                    oCategory = oSku.Category
                    oControlItem = New ControlItem(ControlItem.LinCods.Forecast, oCategory, _Year, _Values)
                    _ControlItems.Add(oControlItem)
                    oControlItem = New ControlItem(ControlItem.LinCods.Sales, oCategory, _Year, _Values)
                    _ControlItems.Add(oControlItem)
                End If
                oControlItem = New ControlItem(ControlItem.LinCods.Forecast, oSku, _Year, _Values)
                _ControlItems.Add(oControlItem)
                oControlItem = New ControlItem(ControlItem.LinCods.Sales, oSku, _Year, _Values)
                _ControlItems.Add(oControlItem)
            End If

        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function displayable(oSku As DTOProductSkuForecast) As Boolean
        Dim isForecasted = _Values.Where(Function(x) x.Guid.Equals(oSku.Guid)).SelectMany(Function(y) y.Forecasts).Any(Function(z) z.YearMonth.year = _Year)
        Dim retval As Boolean = True
        If oSku.obsoleto Then retval = False
        If oSku.Category.obsoleto Then retval = False
        If isForecasted Then retval = True
        If oSku.IsBundle Then retval = False
        Return retval
    End Function

    Public ReadOnly Property Value As DTOProduct
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProduct = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowForecast.DefaultCellStyle.BackColor = Color.Transparent


        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        Dim oLang As DTOLang = Current.Session.Lang

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        For i As Integer = 0 To 12
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.Tot + i)
                .HeaderText = IIf(i = 0, "total", oLang.MesAbr(i))
                .DataPropertyName = IIf(i = 0, "Tot", String.Format("M{0:00}", i))
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
        Next

    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of ControlItem)
        Dim retval As New List(Of ControlItem)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_Product As New Menu_Product(SelectedItems.First.Source)
            AddHandler oMenu_Product.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Product.Range)
            oContextMenu.Items.Add("-")
            If oControlItem.LinCod = ControlItem.LinCods.Forecast Then
                oContextMenu.Items.Add("pegar", Nothing, AddressOf Do_Paste)
            End If
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOProduct = CurrentControlItem.Source
            'Dim oFrm As New Frm_Art(oSelectedValue)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_Forecasts_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem.LinCod = ControlItem.LinCods.Sales Then
            oRow.DefaultCellStyle.ForeColor = Color.Red
        End If

        Dim oProduct As DTOProduct = oControlItem.Source
        If TypeOf oProduct Is DTOProductBrand Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        ElseIf TypeOf oProduct Is DTOProductCategory Then
            oRow.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        End If
    End Sub

    Private Sub Xl_Forecasts_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control And e.KeyCode = Keys.V Then
            If Clipboard.ContainsText() Then
                Do_Paste()
            End If
        End If
    End Sub

    Private Async Sub Do_Paste()
        Dim oTable As DataTable = MyBase.ExcelPastedCells
        Dim oCells As DataGridViewSelectedCellCollection = MyBase.SelectedCells
        Dim oPastedForecasts As DTOProductSkuForecast.Collection = PastedForecasts(oTable, oCells)
        Dim exs As New List(Of Exception)
        If Await FEB2.Forecasts.Insert(oPastedForecasts, exs) Then
            For Each oSku In oPastedForecasts
                Dim pSku = _Values.FirstOrDefault(Function(x) x.Equals(oSku))
                If pSku IsNot Nothing Then
                    For Each oForecast In oSku.Forecasts
                        Dim pForecast = pSku.Forecasts.FirstOrDefault(Function(x) x.YearMonth.Equals(oForecast.YearMonth))
                        If pForecast Is Nothing Then
                            pSku.Forecasts.Add(oForecast)
                            pSku.Forecasts = pSku.Forecasts.OrderBy(Function(x) x.YearMonth.Tag).ToList
                        Else
                            pForecast.Target = oForecast.Target
                        End If
                    Next
                End If
            Next
            Refresca()
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Values))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function PastedForecasts(tblExcelData As DataTable, oCells As DataGridViewSelectedCellCollection) As DTOProductSkuForecast.Collection
        Dim retval As New DTOProductSkuForecast.Collection

        Dim iRowMax As Integer = 0
        Dim iRowMin As Integer = Integer.MaxValue
        Dim iColMax As Integer = 0
        Dim iColMin As Integer = Integer.MaxValue

        Dim oCell As DataGridViewCell
        For Each oCell In oCells
            If oCell.RowIndex < iRowMin Then iRowMin = oCell.RowIndex
            If oCell.RowIndex > iRowMax Then iRowMax = oCell.RowIndex
            If oCell.ColumnIndex < iColMin Then iColMin = oCell.ColumnIndex
            If oCell.ColumnIndex > iColMax Then iColMax = oCell.ColumnIndex
        Next

        Dim oUser As DTOUser = Current.Session.User
        Dim iYear As Integer = _Year

        For iTbRow As Integer = 0 To tblExcelData.Rows.Count - 1
            If iTbRow + iRowMin < MyBase.Rows.Count Then
                Dim oRow As DataGridViewRow = MyBase.Rows(iTbRow + iRowMin)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If TypeOf oControlItem.Source Is DTOProductSku Then
                    If oControlItem.LinCod = ControlItem.LinCods.Forecast Then
                        Dim oSku As New DTOProductSkuForecast(oControlItem.Source.Guid)
                        retval.Add(oSku)
                        For iTbCol As Integer = 0 To tblExcelData.Columns.Count - 1
                            Dim iMes As Integer = (iColMin - Cols.M1) + iTbCol + 1
                            If iMes > 0 AndAlso iMes < 13 Then
                                If IsNumeric(tblExcelData.Rows(iTbRow)(iTbCol)) Then
                                    Dim oForecast As New DTOProductSkuForecast.Forecast
                                    With oForecast
                                        .YearMonth = New DTOYearMonth(iYear, iMes)
                                        .Target = tblExcelData.Rows(iTbRow)(iTbCol)
                                        .UserCreated = oUser.Trim
                                        .FchCreated = Now
                                    End With
                                    oSku.Forecasts.Add(oForecast)
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        Next

        Return retval
    End Function

    Private Sub Xl_Forecasts_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oControlItem As ControlItem = _ControlItems(e.RowIndex)
                If oControlItem.LinCod = ControlItem.LinCods.Forecast And TypeOf (oControlItem.Source) Is DTOProductSku Then
                    Dim oSku As DTOProductSku = oControlItem.Source
                    If oSku.obsoleto Or oSku.LastProduction Then
                        e.Value = My.Resources.aspa
                    End If
                End If
            Case Cols.M1 To Cols.M12
                Dim iCurrentMonth As Integer = e.ColumnIndex - Cols.M1 + 1
                Dim formatcell As Boolean = isActiveMonth(_Year, iCurrentMonth) And isSalesLine(e.RowIndex) And isSku(e.RowIndex)

                If formatcell Then
                    Dim oControlItemForecast As ControlItem = _ControlItems(e.RowIndex - 1)
                    Dim iForecast As Integer = oControlItemForecast.MonthQty(iCurrentMonth)
                    Dim iSold As Integer = e.Value

                    Dim daysFactor As Decimal = 1
                    If _Year = Today.Year And iCurrentMonth = Today.Month Then
                        daysFactor = Today.Day / 31
                    End If

                    If iSold > iForecast * daysFactor * 1.2 Then
                        e.CellStyle.BackColor = Color.LightGreen
                    ElseIf iSold < iForecast * daysFactor * 0.8 Then
                        e.CellStyle.BackColor = Color.LightSalmon
                    End If
                End If
        End Select
    End Sub

    Private Function isActiveMonth(iYear As Integer, iMonth As Integer) As Boolean
        Dim retval As Boolean
        Select Case iYear
            Case < Today.Year
                retval = True
            Case = Today.Year
                retval = iMonth <= Today.Month
            Case > Today.Year
                retval = False
        End Select
        Return retval
    End Function

    Private Function isSalesLine(iRowIndex As Integer) As Boolean
        Dim oControlItem As ControlItem = _ControlItems(iRowIndex)
        Dim retval As Boolean = oControlItem.LinCod = ControlItem.LinCods.Sales
        Return retval
    End Function

    Private Function isSku(iRowIndex As Integer) As Boolean
        Dim oControlItem As ControlItem = _ControlItems(iRowIndex)
        Dim retval As Boolean = TypeOf oControlItem.Source Is DTOProductSku
        Return retval
    End Function

    Protected Class ControlItem
        Property Source As DTOProduct

        Property LinCod As LinCods
        Property Nom As String
        Property Tot As Integer
        Property M01 As Integer
        Property M02 As Integer
        Property M03 As Integer
        Property M04 As Integer
        Property M05 As Integer
        Property M06 As Integer
        Property M07 As Integer
        Property M08 As Integer
        Property M09 As Integer
        Property M10 As Integer
        Property M11 As Integer
        Property M12 As Integer

        Public Enum LinCods
            Forecast
            Sales
        End Enum

        Public Enum ProductCods
            Brand
            Category
            Sku
        End Enum

        Public Sub New(oLincod As LinCods, oProduct As DTOProduct, iYear As Integer, allValues As DTOProductSkuForecast.Collection)
            MyBase.New()
            _LinCod = oLincod
            _Source = oProduct
            Dim oSkus As New DTOProductSkuForecast.Collection
            If TypeOf oProduct Is DTOProductBrand Then
                Dim oBrand As DTOProductBrand = oProduct
                oSkus.AddRange(allValues.Where(Function(x) x.Category.Brand.Equals(oProduct)))
                If _LinCod = LinCods.Forecast Then _Nom = oBrand.Nom.Tradueix(Current.Session.Lang)
            ElseIf TypeOf oProduct Is DTOProductCategory Then
                Dim oCategory As DTOProductCategory = oProduct
                oSkus.AddRange(allValues.Where(Function(x) x.Category.Equals(oProduct)))
                If _LinCod = LinCods.Forecast Then _Nom = New String(" ", 8) & oCategory.Nom.Tradueix(Current.Session.Lang)
            ElseIf TypeOf oProduct Is DTOProductSku Then
                oSkus.AddRange(allValues.Where(Function(x) x.Equals(oProduct)))
                If _LinCod = LinCods.Forecast Then _Nom = New String(" ", 16) & oSkus.First.nom.Tradueix(Current.Session.Lang)
            End If

            Dim oValues = oSkus.SelectMany(Function(x) x.Forecasts).
                    Where(Function(y) y.YearMonth.Year = 0 Or y.YearMonth.Year = iYear).
                    ToList

            SetFigures(oLincod, oValues)
        End Sub

        Private Sub SetFigures(oLincod As LinCods, oValues As List(Of DTOProductSkuForecast.Forecast))
            Select Case oLincod
                Case LinCods.Forecast
                    _Tot = oValues.Sum(Function(y) y.Target)
                    _M01 = oValues.Where(Function(x) x.YearMonth.Month = 1).Sum(Function(y) y.Target)
                    _M02 = oValues.Where(Function(x) x.YearMonth.Month = 2).Sum(Function(y) y.Target)
                    _M03 = oValues.Where(Function(x) x.YearMonth.Month = 3).Sum(Function(y) y.Target)
                    _M04 = oValues.Where(Function(x) x.YearMonth.Month = 4).Sum(Function(y) y.Target)
                    _M05 = oValues.Where(Function(x) x.YearMonth.Month = 5).Sum(Function(y) y.Target)
                    _M06 = oValues.Where(Function(x) x.YearMonth.Month = 6).Sum(Function(y) y.Target)
                    _M07 = oValues.Where(Function(x) x.YearMonth.Month = 7).Sum(Function(y) y.Target)
                    _M08 = oValues.Where(Function(x) x.YearMonth.Month = 8).Sum(Function(y) y.Target)
                    _M09 = oValues.Where(Function(x) x.YearMonth.Month = 9).Sum(Function(y) y.Target)
                    _M10 = oValues.Where(Function(x) x.YearMonth.Month = 10).Sum(Function(y) y.Target)
                    _M11 = oValues.Where(Function(x) x.YearMonth.Month = 11).Sum(Function(y) y.Target)
                    _M12 = oValues.Where(Function(x) x.YearMonth.Month = 12).Sum(Function(y) y.Target)
                Case LinCods.Sales
                    _Tot = oValues.Sum(Function(y) y.Sold)
                    _M01 = oValues.Where(Function(x) x.YearMonth.Month = 1).Sum(Function(y) y.Sold)
                    _M02 = oValues.Where(Function(x) x.YearMonth.Month = 2).Sum(Function(y) y.Sold)
                    _M03 = oValues.Where(Function(x) x.YearMonth.Month = 3).Sum(Function(y) y.Sold)
                    _M04 = oValues.Where(Function(x) x.YearMonth.Month = 4).Sum(Function(y) y.Sold)
                    _M05 = oValues.Where(Function(x) x.YearMonth.Month = 5).Sum(Function(y) y.Sold)
                    _M06 = oValues.Where(Function(x) x.YearMonth.Month = 6).Sum(Function(y) y.Sold)
                    _M07 = oValues.Where(Function(x) x.YearMonth.Month = 7).Sum(Function(y) y.Sold)
                    _M08 = oValues.Where(Function(x) x.YearMonth.Month = 8).Sum(Function(y) y.Sold)
                    _M09 = oValues.Where(Function(x) x.YearMonth.Month = 9).Sum(Function(y) y.Sold)
                    _M10 = oValues.Where(Function(x) x.YearMonth.Month = 10).Sum(Function(y) y.Sold)
                    _M11 = oValues.Where(Function(x) x.YearMonth.Month = 11).Sum(Function(y) y.Sold)
                    _M12 = oValues.Where(Function(x) x.YearMonth.Month = 12).Sum(Function(y) y.Sold)
            End Select

        End Sub

        Public Function MonthQty(iMes As Integer) As Integer
            Dim retval As Integer = Choose(iMes, _M01, _M02, _M03, _M04, _M05, _M06, _M07, _M08, _M09, _M10, _M11, _M12)
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


