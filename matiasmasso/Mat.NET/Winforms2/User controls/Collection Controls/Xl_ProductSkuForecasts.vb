Public Class Xl_ProductSkuForecasts
    Inherits TabStopDataGridView

    Private _Values As DTOProductSkuForecast.Collection

    Private _FchTo As Date
    Private _ControlItems As ControlItems
    Private _Summary As ControlItem
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestForOrder(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Brand
        Category
        Sku
        Warn
        Optimitzat
        Proposal
        SecurityStock
        Stk
        Pn1
        Pn2
        Forecast
        Cost
        Dto
        Amt
        M3
        M3X
        MinPack
        OutPack
    End Enum

    Public Shadows Sub Load(Values As DTOProductSkuForecast.Collection, FchTo As Date)
        _FchTo = FchTo
        _Values = Values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then SetProperties()

        refresca()
    End Sub

    Public ReadOnly Property PurchaseOrderItems As List(Of DTOPurchaseOrderItem)
        Get
            Dim retval As New List(Of DTOPurchaseOrderItem)
            If _ControlItems IsNot Nothing Then
                For Each oControlItem As ControlItem In _ControlItems
                    If oControlItem.Optimitzat > 0 Then
                        Dim item As New DTOPurchaseOrderItem
                        With item
                            .Sku = oControlItem.Source
                            .Qty = oControlItem.Optimitzat
                            .Price = DTOAmt.Factory(oControlItem.Cost)
                            .Dto = oControlItem.Dto
                            .Pending = .Qty
                        End With
                        retval.Add(item)
                    End If
                Next
            End If
            Return retval
        End Get
    End Property

    Private Sub refresca()
        _ControlItems = New ControlItems
        _Summary = New ControlItem()
        _ControlItems.Add(_Summary)

        For Each oSku As DTOProductSkuForecast In _Values
            ' If oSku.Id = 23491 Then Stop '===================================
            Dim oControlItem As New ControlItem(oSku, _FchTo)
            If displayable(oControlItem) Then
                _ControlItems.Add(oControlItem)
            End If
        Next

        refrescaSummary()

        _AllowEvents = False
        Dim oBindingSource As New BindingSource
        oBindingSource.AllowNew = True
        oBindingSource.DataSource = _ControlItems
        MyBase.DataSource = oBindingSource

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function displayable(oControlItem As ControlItem) As Boolean
        Dim retval As Boolean
        With oControlItem
            If .Source.Obsoleto Or .Source.LastProduction Then
                If .Optimitzat > 0 Then retval = True
                If .Stk > 0 Then retval = True
                If .Pn2 > 0 Then retval = True
                If .Pn1 Then retval = True
            Else
                retval = True
            End If
        End With
        Return retval
    End Function

    Private Sub refrescaSummary()
        Dim items As List(Of ControlItem) = _ControlItems.Where(Function(y) y.Source IsNot Nothing).ToList

        With _Summary
            .Amt = items.Sum(Function(x) x.Amt)
            .M3X = items.Sum(Function(x) x.M3X)
        End With
        MyBase.Refresh()

    End Sub

    Private Sub SetProperties()
        MyBase.AutoGenerateColumns = False
        With MyBase.RowTemplate
            .Height = MyBase.Font.Height * 1.3
        End With
        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = True
        MyBase.MultiSelect = False
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowUserToDeleteRows = True
        MyBase.RowHeadersWidth = 25
        MyBase.ReadOnly = False
        MyBase.Columns.Clear()


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Brand)
            .HeaderText = "marca"
            .DataPropertyName = "Brand"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Category)
            .HeaderText = "categoría"
            .DataPropertyName = "Category"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "producte"
            .DataPropertyName = "Sku"
            .MinimumWidth = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Warn), DataGridViewImageColumn)
            .HeaderText = ""
            .ReadOnly = True
            .Width = 18
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.NullValue = Nothing
            .DefaultCellStyle.DataSourceNullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Optimitzat)
            .HeaderText = "comanda"
            .DataPropertyName = "Optimitzat"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = False
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Proposal)
            .HeaderText = "proposta"
            .DataPropertyName = "Proposal"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SecurityStock)
            .HeaderText = "stk seg."
            .DataPropertyName = "SecurityStock"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Stk)
            .HeaderText = "stock"
            .DataPropertyName = "Stk"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn1)
            .HeaderText = "proveidor"
            .DataPropertyName = "pn1"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn2)
            .HeaderText = "clients"
            .DataPropertyName = "pn2"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Forecast)
            .HeaderText = "forecast"
            .DataPropertyName = "Forecast"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cost)
            .HeaderText = "cost"
            .DataPropertyName = "Cost"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "dto"
            .DataPropertyName = "Dto"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "import"
            .DataPropertyName = "Amt"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M3)
            .HeaderText = "volum"
            .DataPropertyName = "M3"
            .ReadOnly = True
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0.000;-#,##0.000;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M3X)
            .HeaderText = "x volum"
            .DataPropertyName = "M3X"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0.000;-#,##0.000;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.MinPack)
            .HeaderText = "pack"
            .DataPropertyName = "MinPack"
            .ReadOnly = True
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0;-#,##0;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.BackColor = BCOLORBLANK
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.OutPack)
            .HeaderText = "master pack"
            .DataPropertyName = "OutPack"
            .ReadOnly = True
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0;-#,##0;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.BackColor = BCOLORBLANK
        End With

    End Sub

    Private Sub MatDataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Optimitzat
                    If e.RowIndex >= 0 Then
                        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                        Dim oControlItem As ControlItem = oRow.DataBoundItem
                        With oControlItem
                            .Amt = (DTOAmt.Factory(.Optimitzat * .Cost * (100 - .Dto) / 100)).Eur
                            .M3X = Math.Round(.Optimitzat * .M3 / .MinPack, 6, MidpointRounding.AwayFromZero) '0 .Optimitzat * .M3
                            '_M3X = Math.Round(_Optimitzat * _M3 / _MinPack, 6, MidpointRounding.AwayFromZero)
                        End With

                        refrescaSummary()
                    End If
            End Select
        End If
    End Sub

    Private Sub MatDataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.M3
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem IsNot Nothing Then
                    'e.CellStyle.ForeColor = Color.Gray
                End If
            Case Cols.Warn
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Source IsNot Nothing Then
                    Dim oSku As DTOProductSku = oControlItem.Source
                    If oSku.Obsoleto Then
                        If oControlItem.Optimitzat > 0 Then
                            e.Value = My.Resources.WarnRed16
                        Else
                            e.Value = My.Resources.WarnEmpty
                        End If
                    ElseIf oSku.LastProduction Then
                        If oControlItem.Proposal > 0 Then
                            e.Value = My.Resources.warn
                        Else
                            e.Value = My.Resources.WarnEmpty
                        End If
                    End If
                End If
            Case Cols.Proposal
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Proposal <= 0 Then
                    e.CellStyle.BackColor = Color.LightGray
                End If
        End Select
    End Sub

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
            Dim oSku As DTOProductSku = oControlItem.Source
            Dim oMenu As New Menu_ProductSku(oSku)
            oContextMenu.Items.AddRange(oMenu.Range)
            oContextMenu.Items.Add("-")
            'If oControlItem.Visibility = ControlItem.Visibilities.Expanded Then
            'oContextMenu.Items.Add("colapsa", Nothing, AddressOf Do_ExpandCollapse)
            'ElseIf oControlItem.Visibility = ControlItem.Visibilities.Collapsed Then
            'oContextMenu.Items.Add("expandeix", Nothing, AddressOf Do_ExpandCollapse)
            'End If
        End If
        oContextMenu.Items.Add("redacta comanda", Nothing, AddressOf Do_PO)
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_PO()
        RaiseEvent RequestForOrder(Me, MatEventArgs.Empty)
    End Sub



    Private Sub MatDataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then SetContextMenu()
    End Sub

    Private Sub Xl_ForecastOrderProposal_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Select Case e.ColumnIndex
                Case Cols.Warn
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    If oControlItem.Source IsNot Nothing Then
                        Dim oSku As DTOProductSku = oControlItem.Source
                        If oSku.Obsoleto Then
                            e.ToolTipText = "article obsolet"
                        ElseIf oSku.LastProduction Then
                            e.ToolTipText = "últimes unitats en producció"
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub Do_Excel()
        Dim exs As New List(Of Exception)
        Dim oSheet As New MatHelper.Excel.Sheet

        Dim query = _ControlItems.GroupBy(Function(g) New With {Key g.Brand, Key g.Category}).
         Select(Function(group) New With {
         .Brand = group.Key.Brand,
         .Category = group.Key.Category,
         .Optimitzat = group.Sum(Function(y) y.Optimitzat),
         .Amt = group.Sum(Function(z) z.Amt),
         .M3X = group.Sum(Function(z) z.M3X),
         .SecurityStock = group.Sum(Function(y) y.SecurityStock),
         .Stk = group.Sum(Function(z) z.Stk),
         .StkVal = group.Sum(Function(z) z.Stk * z.Cost * (100 - z.Dto) / 100),
         .Pn1 = group.Sum(Function(z) z.Pn1),
         .Pn1Val = group.Sum(Function(z) z.Pn1 * z.Cost * (100 - z.Dto) / 100),
         .Pn2 = group.Sum(Function(z) z.Pn2),
         .Pn2Val = group.Sum(Function(z) z.Pn2 * z.Cost * (100 - z.Dto) / 100),
         .Forecast = group.Sum(Function(z) z.Forecast),
         .FcastVal = group.Sum(Function(z) z.Forecast * z.Cost * (100 - z.Dto) / 100)
                    })

        With oSheet
            .DisplayTotals = True
            .AddColumn("Brand", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Category", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("To be ordered", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Cost", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Volume", MatHelper.Excel.Cell.NumberFormats.m3D2)
            .AddColumn("Safety Stock", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Stock", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Value", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Supplier", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Value", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Customers", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Value", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Forecast", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Value", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        For Each oItem In query
            If oItem.Optimitzat > 0 Then
                Dim oRow = oSheet.AddRow()
                With oItem
                    oRow.AddCell(.Brand)
                    oRow.AddCell(.Category)
                    'oRow.AddCell(.Sku)
                    oRow.AddCell(.Optimitzat)
                    oRow.AddCell(.Amt)
                    oRow.AddCell(.M3X)
                    oRow.AddCell(.SecurityStock)
                    oRow.AddCell(.Stk)
                    oRow.AddCell(.StkVal)
                    oRow.AddCell(.Pn1)
                    oRow.AddCell(.Pn1Val)
                    oRow.AddCell(.Pn2)
                    oRow.AddCell(.Pn2Val)
                    oRow.AddCell(.Forecast)
                    oRow.AddCell(.FcastVal)
                End With
            End If
        Next

        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOProductSku

        Property Brand As String
        Property Category As String
        Property Sku As String
        Property Warn As Boolean
        Property Optimitzat As Integer
        Property Proposal As Integer
        Property SecurityStock As Integer
        Property Stk As Integer
        Property Pn1 As Integer
        Property Pn2 As Integer
        Property Forecast As Integer
        Property Cost As Decimal
        Property Dto As Decimal
        Property Amt As Decimal

        Property M3 As Decimal
        Property M3X As Decimal
        Property MinPack As Integer
        Property OutPack As Integer

        Public Sub New()
            MyBase.New
            _Brand = "totals"
        End Sub

        Public Sub New(oSku As DTOProductSku, FchTo As Date)
            MyBase.New()
            _Source = oSku

            With _Source
                _Brand = .category.brand.nom.Tradueix(Current.Session.Lang)
                _Category = .category.nom.Tradueix(Current.Session.Lang)
                _Sku = .nom.Tradueix(Current.Session.Lang)
                _SecurityStock = .SecurityStock
                _Stk = .stock
                _Pn1 = .proveidors
                _Pn2 = .Clients
                If .Cost IsNot Nothing Then
                    _Cost = .Cost.Eur
                    _Dto = .CustomerDto
                End If
                _M3 = .VolumeM3
            End With

            _Warn = False
            _MinPack = DTOProductSku.InnerPackOrInherited(_Source)
            _OutPack = 0 '.outerpack

            SetFchTo(FchTo)
        End Sub

        Public Sub SetFchTo(FchTo As Date)
            If _Source IsNot Nothing Then
                With _Source
                    _Warn = False
                    _Forecast = DTOProductSkuForecast.Forecasted(_Source, FchTo)
                    _Proposal = DTOProductSkuForecast.Proposal(_Source, _Forecast)
                    _Optimitzat = DTOProductSkuForecast.OptimizedProposal(_Source, _Proposal)

                    If .Cost IsNot Nothing Then
                        _Amt = _Optimitzat * Cost * (100 - _Dto) / 100
                    End If
                    _M3X = _Optimitzat * DTOProductSku.VolumeM3OrInherited(_Source)

                End With
            End If
        End Sub


    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class
End Class
