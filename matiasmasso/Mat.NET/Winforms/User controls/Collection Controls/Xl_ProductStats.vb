Public Class Xl_ProductStats

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOProductStat)
    Private _Min As DTOYearMonth
    Private _Max As DTOYearMonth

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Qty
        Share
    End Enum

    Public Shadows Sub Load(values As List(Of DTOProductStat), oMin As DTOYearMonth, oMax As DTOYearMonth)
        _Values = values
        _Min = oMin
        _Max = oMax

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
        Dim iTotalQty = _Values.SelectMany(Function(x) x.items).Where(Function(y) y.IsGreaterOrEqualThan(_Min) And y.IsLowerOrEqualThan(_Max)).Sum(Function(z) z.Eur)

        Dim oSummary = ControlItem.Summary(iTotalQty)
        _ControlItems.Add(oSummary)

        For Each oItem As DTOProductStat In _Values
            Dim oControlItem = ControlItem.Factory(oItem, _Min, _Max, iTotalQty)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        MyBase.Sort(MyBase.Columns(Cols.Qty), System.ComponentModel.ListSortDirection.Descending)
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOProductStat
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductStat = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Producte"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Unitats"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,##0"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Share)
            .HeaderText = "Quota"
            .DataPropertyName = "Share"
            .Width = 70
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

    Private Function SelectedItems() As List(Of DTOProductStat)
        Dim retval As New List(Of DTOProductStat)
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
            'Dim oMenu_ProductStat As New Menu_ProductStat(SelectedItems.First)
            'AddHandler oMenu_ProductStat.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_ProductStat.Range)
            'oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Excel()
        Dim oSheet As New MatHelperStd.ExcelHelper.Sheet()

        Dim i As Integer
        For Each oControlItem In _ControlItems
            i += 1
            Dim oRow = oSheet.AddRow()
            oRow.AddCell(oControlItem.Nom)
            oRow.AddCell(oControlItem.Qty)
            'oRow.AddFormula("RC[-1]/R[-" & i & "]C[-1]")
        Next

        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOProductStat = CurrentControlItem.Source
            ' Dim oFrm As New Frm_ProductSku(oSelectedValue)
            ' AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            ' oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub




    Protected Class ControlItem
        Property Source As DTOProductStat

        Property Nom As String
        Property Qty As Integer
        Property Share As Decimal

        Shared Function Factory(value As DTOProductStat, oMin As DTOYearMonth, oMax As DTOYearMonth, iTotalQty As Integer) As ControlItem
            Dim retval As New ControlItem
            With retval
                .Source = value
                .Nom = value.nom.Tradueix(Current.Session.Lang)
                .Qty = value.items.Where(Function(x) x.IsGreaterOrEqualThan(oMin) And x.IsLowerOrEqualThan(oMax)).Sum(Function(y) y.Eur)
                .Share = 100 * retval.Qty / iTotalQty
            End With
            Return retval
        End Function

        Shared Function Summary(iTotalQty As Integer) As ControlItem
            Dim retval As New ControlItem
            With retval
                .Nom = "totals"
                .Qty = iTotalQty
                .Share = 100
            End With
            Return retval
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


