Public Class Xl_EDiversaOrderItems
    Inherits DataGridView

    Private _Values As List(Of DTOEdiversaOrderItem)
    Private _DefaultValue As DTOEdiversaOrderItem
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _PropertiesSet As Boolean

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Ean
        RefClient
        RefProveidor
        Sku
        Qty
        Price
        Dto
        Amt
    End Enum

    Public Shadows Async Sub Load(values As List(Of DTOEdiversaOrderItem), Optional oDefaultValue As DTOEdiversaOrderItem = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        If Not _PropertiesSet Then
            SetProperties()
            _PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Await Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOEdiversaOrderItem)
        Get
            Return _Values
        End Get
    End Property

    Private Async Function Refresca() As Task
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOEdiversaOrderItem) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOEdiversaOrderItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCurrentCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        If _DefaultValue Is Nothing Then
            UIHelper.SetDataGridviewCurrentCell(Me, oCurrentCell)
        Else
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Ean)
            End If
        End If

        Await SetContextMenu()
        _AllowEvents = True
    End Function

    Private Function FilteredValues() As List(Of DTOEdiversaOrderItem)
        Dim retval As List(Of DTOEdiversaOrderItem)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Ean.Value.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function

    Public Async Function SetFilter(value As String) As Task
        _Filter = value
        If _Values IsNot Nothing Then
            Await Refresca()
        End If
    End Function


    Public Async Function ClearFilter() As Task
        If _Filter > "" Then
            _Filter = ""
            Await Refresca()
        End If
    End Function

    Public ReadOnly Property Value As DTOEdiversaOrderItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOEdiversaOrderItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ean)
            .HeaderText = "Ean"
            .DataPropertyName = "Ean"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.RefClient)
            .HeaderText = "Ref. Client"
            .DataPropertyName = "RefClient"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.RefProveidor)
            .HeaderText = "Ref. Proveidor"
            .DataPropertyName = "RefProveidor"
            .Width = 110
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "Producte"
            .DataPropertyName = "Sku"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Unitats"
            .DataPropertyName = "Qty"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Price)
            .HeaderText = "Preu"
            .DataPropertyName = "Price"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dto"
            .DataPropertyName = "Dto"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
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

    Private Function SelectedItems() As List(Of DTOEdiversaOrderItem)
        Dim retval As New List(Of DTOEdiversaOrderItem)
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

    Private Async Function SetContextMenu() As Task
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_EdiversaOrderItem As New Menu_EDiversaOrderItem(SelectedItems.First)
            AddHandler oMenu_EdiversaOrderItem.AfterUpdate, AddressOf onUpdateFromMenu
            Dim oRange = Await oMenu_EdiversaOrderItem.Range
            oContextMenu.Items.AddRange(oRange)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Function

    Private Sub onUpdateFromMenu(sender As Object, e As MatEventArgs)
        If TypeOf e.Argument Is DTOEdiversaOrder Then
            RefreshRequest(Me, e)
        ElseIf TypeOf e.Argument Is List(Of DTOEdiversaOrderItem) Then
            _Values = e.Argument
            Load(_Values, _DefaultValue, _SelectionMode)
            RaiseEvent AfterUpdate(Me, e)
        ElseIf TypeOf e.Argument Is DTOEdiversaOrderItem Then
            Dim oEditedItem As DTOEdiversaOrderItem = e.Argument
            Dim oOriginalItem As DTOEdiversaOrderItem = _Values.FirstOrDefault(Function(x) x.Equals(oEditedItem))
            Dim idx = _Values.IndexOf(oOriginalItem)
            _Values(idx) = oEditedItem
            Load(_Values, oEditedItem, _SelectionMode)
            RaiseEvent AfterUpdate(Me, e)
        Else
            UIHelper.WarnError("Objecte editat no reconegut")
        End If
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOEdiversaOrderItem = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    If oSelectedValue.Sku IsNot Nothing Then
                        Dim oFrm As New Frm_Art(oSelectedValue.Sku)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    End If
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Async Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            Await SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToRefresh(Me, e)
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim item As DTOEdiversaOrderItem = oControlItem.Source
        'If item.Lin = 10 Then Stop
        If item.SkipItemUser Is Nothing Then
            oRow.DefaultCellStyle.BackColor = Color.White
        Else
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub Xl_EDiversaOrderItems_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim item As DTOEdiversaOrderItem = oControlItem.Source
                Select Case item.Exceptions.Count
                    Case 0
                    Case Else
                        e.Value = My.Resources.warn
                End Select
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOEdiversaOrderItem

        Property Ean As String
        Property RefClient As String
        Property RefProveidor As String
        Property Sku As String
        Property Qty As Integer
        Property Price As Decimal
        Property Dto As Decimal
        Property Amt As Decimal

        Public Sub New(value As DTOEdiversaOrderItem)
            MyBase.New()
            _Source = value
            With value
                Dim oPrice = If(.Preu, .PreuNet)
                _Ean = .Ean.Value
                _RefClient = .RefClient
                _RefProveidor = .RefProveidor
                If .Sku IsNot Nothing Then
                    _Sku = .Sku.nomLlarg.Tradueix(Current.Session.Lang)
                End If
                _Qty = .Qty
                _Price = DTOAmt.EurOrDefault(oPrice)
                _Dto = .Dto
                _Amt = DTOAmt.EurOrDefault(DTOAmt.FromQtyPriceDto(.Qty, oPrice, .Dto))
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


