Public Class Xl_ForecastFollowUp
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As DTOProductSkuForecast.Collection
    Private _fch As Date

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Concept
        FcastQty
        SoldQty
        FcastAmt
        SoldAmt
        Gap
        Status
    End Enum

    Public Shadows Sub Load(values As DTOProductSkuForecast.Collection, fch As Date)
        _Values = values
        _fch = fch

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oLang = Current.Session.Lang
        Dim oFilteredValues As DTOProductSkuForecast.Collection = _Values
        _ControlItems = New ControlItems
        Dim oControlItem As ControlItem = Nothing

        Dim oBrands = _Values.GroupBy(Function(x) x.Category.Brand.Guid).Select(Function(y) y.First).Select(Function(z) z.Category.Brand).ToList()
        For Each oBrand In oBrands
            Dim oFollowUp = _Values.GetFollowUp(oBrand, _fch)
            oControlItem = New ControlItem(oFollowUp, oLang)
            _ControlItems.Add(oControlItem)
            Dim oCategories = _Values.Where(Function(a) a.Category.Brand.Equals(oBrand)).GroupBy(Function(x) x.Category.Guid).Select(Function(y) y.First).Select(Function(z) z.Category).ToList()
            For Each oCategory In oCategories
                oFollowUp = _Values.GetFollowUp(oCategory, _fch)
                oControlItem = New ControlItem(oFollowUp, oLang)
                _ControlItems.Add(oControlItem)
            Next
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOProductSkuForecast.FollowUp
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductSkuForecast.FollowUp = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowProductSkuForecast.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Concept)
            .HeaderText = "Concept"
            .DataPropertyName = "Concept"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FcastQty)
            .HeaderText = "Forecast"
            .DataPropertyName = "FcastQty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,####"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SoldQty)
            .HeaderText = "Sold"
            .DataPropertyName = "SoldQty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,####"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FcastAmt)
            .HeaderText = "Forecast"
            .DataPropertyName = "FcastAmt"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SoldAmt)
            .HeaderText = "Sold"
            .DataPropertyName = "SoldAmt"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Gap)
            .HeaderText = "Gap"
            .DataPropertyName = "Gap"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Status)
            .HeaderText = "Status"
            .DataPropertyName = "Status"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
        End With
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

    Private Function SelectedItems() As List(Of DTOProductSkuForecast.FollowUp)
        Dim retval As New List(Of DTOProductSkuForecast.FollowUp)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
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
            'Dim oMenu_ProductSkuForecast As New Menu_ProductSkuForecast(SelectedItems.First)
            'AddHandler oMenu_ProductSkuForecast.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_ProductSkuForecast.Range)
            'oContextMenu.Items.Add("-")
        End If
        'oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOProductSkuForecast.FollowUp = CurrentControlItem.Source

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Gap
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                'If oControlItem.Gap < 0 Then e.Style
                'End If
        End Select
    End Sub


    Protected Class ControlItem
        Property Source As DTOProductSkuForecast.FollowUp

        Property Concept As String
        Property FcastQty As Integer
        Property SoldQty As Integer
        Property FcastAmt As Decimal
        Property SoldAmt As Decimal
        Property Gap As Decimal
        Property Status As Decimal

        Public Sub New(oFollowUp As DTOProductSkuForecast.FollowUp, oLang As DTOLang)
            MyBase.New()
            _Source = oFollowUp
            With oFollowUp
                _Concept = .Product.Nom.Tradueix(oLang)
                _FcastQty = .FcastQty
                _SoldQty = .SoldQty
                _FcastAmt = .FcastAmt.Eur
                _SoldAmt = .SoldAmt.Eur
                _Gap = _SoldAmt - _FcastAmt
                If _FcastAmt <> 0 Then
                    _Status = 100 * _SoldAmt / _FcastAmt - 100
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

