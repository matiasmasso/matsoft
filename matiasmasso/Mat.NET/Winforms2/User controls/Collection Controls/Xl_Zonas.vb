Public Class Xl_Zonas
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As IEnumerable(Of DTOZona)
    Private _DefaultValue As DTOZona
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _ControlItems As ControlItems
    Private _AdvancedMode As Boolean 'crea columnes amb CEE, Lang...
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)


    Private Enum Cols
        nom
        exportCod
        export
        lang
    End Enum

    Public Shadows Sub Load(values As IEnumerable(Of DTOZona), Optional oDefaultValue As DTOZona = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode

        Refresca()
    End Sub



    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each oItem As DTOZona In _Values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Sub Clear()
        _Values = New List(Of DTOZona)
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOZona)
        Get
            Return _Values
        End Get
    End Property
    Public ReadOnly Property Value As DTOZona
        Get
            Dim retval As DTOZona = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

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
            .HeaderText = "Zones"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.exportCod)
            .HeaderText = "export"
            .DataPropertyName = "exportCod"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 46
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Visible = _AdvancedMode
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.export), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
            .Visible = _AdvancedMode
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.lang)
            .HeaderText = "llengua"
            .DataPropertyName = "lang"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 45
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Visible = _AdvancedMode
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

    Private Function SelectedItems() As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)
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
            Dim oMenu_Zona As New Menu_Zona(SelectedItems.First)
            AddHandler oMenu_Zona.AfterUpdate, AddressOf RefreshRequest
            AddHandler oMenu_Zona.AfterDelete, AddressOf DeleteRequest
            oContextMenu.Items.AddRange(oMenu_Zona.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim oMenuItemAdvanced As New ToolStripMenuItem("avançat", Nothing, AddressOf Do_ToggleAdvanced)
        oMenuItemAdvanced.CheckOnClick = True
        oMenuItemAdvanced.Checked = _AdvancedMode
        oContextMenu.Items.Add(oMenuItemAdvanced)

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ToggleAdvanced(sender As Object, e As EventArgs)
        _AdvancedMode = Not _AdvancedMode
        SetProperties()
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOZona = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Zona(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_Countries_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.export
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oCountry As DTOZona = oControlItem.Source
                Select Case oCountry.ExportCod
                    Case DTOInvoice.ExportCods.Nacional
                        e.Value = My.Resources.harveyballEmpty
                    Case DTOInvoice.ExportCods.Intracomunitari
                        e.Value = My.Resources.harveyballHalf
                    Case DTOInvoice.ExportCods.Extracomunitari
                        e.Value = My.Resources.harveyballFull
                End Select
            Case Cols.lang
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oCountry As DTOZona = oControlItem.Source
                Select Case oCountry.Lang.Id
                    Case DTOLang.Ids.CAT
                        e.CellStyle.BackColor = Color.LightBlue
                    Case DTOLang.Ids.ESP
                        e.CellStyle.BackColor = Color.Yellow
                End Select

        End Select
    End Sub

    Private Sub Xl_Zonas_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        Select Case e.ColumnIndex
            Case Cols.export, Cols.exportCod
                If e.RowIndex >= 0 Then
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oZona As DTOZona = oControlItem.Source

                    Select Case oZona.ExportCod
                        Case DTOInvoice.ExportCods.Nacional
                            e.ToolTipText = "nacional"
                        Case DTOInvoice.ExportCods.Intracomunitari
                            e.ToolTipText = "Intracomunitari"
                        Case DTOInvoice.ExportCods.Extracomunitari
                            e.ToolTipText = "Extracomunitari"
                    End Select
                End If
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOZona

        Property Nom As String
        Property exportCod As Integer

        Public Sub New(value As DTOZona)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Nom
                _exportCod = .ExportCod
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


