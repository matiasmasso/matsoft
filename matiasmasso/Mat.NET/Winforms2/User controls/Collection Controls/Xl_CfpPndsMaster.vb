Public Class Xl_CfpPndsMaster

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPnd)
    Private _DefaultValue As DTOPnd
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        fpg
        VtoFrom
        VtoTo
        Eur
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPnd), Optional oDefaultValue As DTOPnd = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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

        Dim query = _Values.GroupBy(Function(x) New With {Key x.Cfp}) _
            .Select(Function(group) New With {
            .cfp = group.Key.Cfp,
            .total = group.Sum(Function(a) a.Amt.Eur),
            .vtoFrom = group.Min(Function(b) b.Vto),
            .vtoTo = group.Max(Function(c) c.Vto)})

        _ControlItems = New ControlItems
        For Each Group In query
            Dim oControlItem As New ControlItem(Group.cfp, Group.vtoFrom, Group.vtoTo, Group.total)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOPnd.FormasDePagament
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPnd.FormasDePagament = oControlItem.Source
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

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fpg)
            .HeaderText = "Forma de Pago"
            .DataPropertyName = "Fpg"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.VtoFrom)
            .HeaderText = "Des de"
            .DataPropertyName = "VtoFrom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.VtoTo)
            .HeaderText = "Fins"
            .DataPropertyName = "VtoTo"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 80
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
        Property Source As DTOPnd.FormasDePagament

        Property fpg As String
        Property VtoFrom As Date
        Property VtoTo As Date
        Property Clis As Integer
        Property Eur As Decimal


        Public Sub New(value As DTOPnd.FormasDePagament, VtoFrom As Date, VtoTo As Date, Eur As Decimal)
            MyBase.New()
            _Source = value
            With value
                _fpg = DTOPnd.GetFpg(value)
                _VtoFrom = VtoFrom
                _VtoTo = VtoTo
                _Eur = Eur
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

