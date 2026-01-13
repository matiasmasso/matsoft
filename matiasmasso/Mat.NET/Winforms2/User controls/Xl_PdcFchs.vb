Public Class Xl_PdcFchs

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPurchaseOrder)
    Private _Lang As DTOLang
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Hora
        Total
        Quota
        Dilluns
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrder), oLang As DTOLang)
        _Values = values
        _Lang = oLang

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False

        Dim oFilteredValues As List(Of DTOPurchaseOrder) = _Values
        _ControlItems = New ControlItems
        For i As Integer = 0 To 23
            Dim hour As Integer = i
            Dim oValues As List(Of DTOPurchaseOrder) = _Values.Where(Function(x) x.UsrLog.FchCreated.Hour = hour).ToList
            Dim quota As Decimal
            If _Values.Count > 0 Then
                quota = oValues.Count / _Values.Count
            End If
            Dim oControlItem As New ControlItem(oValues, hour, quota)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


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
        With MyBase.Columns(Cols.Hora)
            .HeaderText = "Hora"
            .DataPropertyName = "Hora"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Total)
            .HeaderText = "Total"
            .DataPropertyName = "Total"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Quota)
            .HeaderText = "Quota"
            .DataPropertyName = "Quota"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
        End With

        For dia As Integer = 1 To 7
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.Quota + dia)
                .HeaderText = _Lang.WeekDay(dia)
                .DataPropertyName = String.Format("D{0}", dia)
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
            'Dim oMenu_Template As New Menu_Template(SelectedItems.First)
            'AddHandler oMenu_Template.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Template.Range)
            'oContextMenu.Items.Add("-")
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Property Source As List(Of DTOPurchaseOrder)

        Property Hora
        Property Total
        Property Quota
        Property D1
        Property D2
        Property D3
        Property D4
        Property D5
        Property D6
        Property D7


        Public Sub New(values As List(Of DTOPurchaseOrder), hour As Integer, quota As Decimal)
            MyBase.New()
            _Source = values
            _Hora = Format(hour, "00")
            _Total = values.Count
            _Quota = quota
            _D1 = values.Where(Function(x) x.Fch.DayOfWeek = DayOfWeek.Monday).Count
            _D2 = values.Where(Function(x) x.Fch.DayOfWeek = DayOfWeek.Tuesday).Count
            _D3 = values.Where(Function(x) x.Fch.DayOfWeek = DayOfWeek.Wednesday).Count
            _D4 = values.Where(Function(x) x.Fch.DayOfWeek = DayOfWeek.Thursday).Count
            _D5 = values.Where(Function(x) x.Fch.DayOfWeek = DayOfWeek.Friday).Count
            _D6 = values.Where(Function(x) x.Fch.DayOfWeek = DayOfWeek.Saturday).Count
            _D7 = values.Where(Function(x) x.Fch.DayOfWeek = DayOfWeek.Sunday).Count
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

