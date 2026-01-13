Public Class Xl_ProductRepeticionsParent

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTORepeticio)
    Private _DefaultValue As DTORepeticio
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Orders
        Clients
        QuotClients
        Qty
        Eur
        QuotEur
    End Enum

    Public Shadows Sub Load(values As List(Of DTORepeticio), Optional oDefaultValue As DTORepeticio = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()

    End Sub

    Private Sub Refresca()
        _AllowEvents = False

        Dim Query = _Values.GroupBy(Function(g) New With {Key g.Orders}).
            Select(Function(group) New With {
            .Orders = group.Key.Orders,
            .Clients = group.Count,
            .Qty = group.Sum(Function(a) a.Qty),
            .Eur = group.Sum(Function(a) a.Eur)
                       })

        Dim TotalClients As Integer = Query.Sum(Function(x) x.Clients)
        Dim TotalQty As Integer = Query.Sum(Function(x) x.Qty)
        Dim TotalEur As Decimal = Query.Sum(Function(x) x.Eur)

        _ControlItems = New ControlItems
        Dim oControlItem As New ControlItem(0, TotalClients, TotalClients, TotalQty, TotalEur, TotalEur)
        _ControlItems.Add(oControlItem)

        For Each oItem In Query
            oControlItem = New ControlItem(oItem.Orders, oItem.Clients, TotalClients, oItem.Qty, oItem.Eur, TotalEur)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems

        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTORepeticio)
        Dim retval As List(Of DTORepeticio)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As Integer
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As Integer = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowRepeticio.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Orders)
            .HeaderText = "Comandes"
            .DataPropertyName = "Orders"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Clients)
            .HeaderText = "Clients"
            .DataPropertyName = "Clients"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.QuotClients)
            .HeaderText = "Quota"
            .DataPropertyName = "QuotClients"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
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
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.QuotEur)
            .HeaderText = "Quota"
            .DataPropertyName = "QuotEur"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
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

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
        End If
    End Sub



    Protected Class ControlItem
        Property Source As Integer

        Property Orders As Integer
        Property Clients As Integer
        Property QuotClients As Integer
        Property Qty As Integer
        Property Eur As Decimal
        Property QuotEur As Integer


        Public Sub New(Orders As Integer, Clients As Integer, TotalClients As Integer, Qty As Integer, Eur As Decimal, TotalEur As Decimal)
            MyBase.New()
            _Source = Orders

            _Clients = Clients
            _QuotClients = 100 * _Clients / TotalClients
            _Orders = Orders
            _Qty = Qty
            _Eur = Eur
            _QuotEur = 100 * _Eur / TotalEur
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


