Public Class Xl_StockAvailable
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOStockAvailable)
    Private _DefaultValue As DTOStockAvailable
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Nom
        Pendent
        StockOriginal
        Sortit
        Clients
    End Enum

    Public Shadows Sub Load(values As List(Of DTOStockAvailable), Optional oDefaultValue As DTOStockAvailable = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        Dim previousAllowEvents As Boolean = _AllowEvents
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOStockAvailable) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOStockAvailable In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        _ControlItems.Insert(0, ControlItem.TotalItem(_ControlItems))

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = previousAllowEvents
    End Sub


    Public ReadOnly Property Value As DTOStockAvailable
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOStockAvailable = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowStockAvailable.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pendent)
            .HeaderText = "Pendent"
            .DataPropertyName = "Pendent"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.StockOriginal)
            .HeaderText = "Stock"
            .DataPropertyName = "Stock"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sortit)
            .HeaderText = "Sortides"
            .DataPropertyName = "Sortit"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Clients)
            .HeaderText = "Clients"
            .DataPropertyName = "Clients"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###"
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

    Private Function SelectedItems() As List(Of DTOStockAvailable)
        Dim retval As New List(Of DTOStockAvailable)
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
            If oControlItem.Source IsNot Nothing Then
                If oControlItem IsNot Nothing Then
                    Dim oProductSku As DTOProductSku = oControlItem.Source.Sku
                    Dim oMenu_ProductSku As New Menu_ProductSku(oProductSku)
                    AddHandler oMenu_ProductSku.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_ProductSku.Range)
                    oContextMenu.Items.Add("-")
                End If
            End If
        End If

        oContextMenu.Items.Add(New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf Do_Excel))

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Excel()
        Dim oCsv As New DTOCsv("El Corte Ingles productes pendents de entrega.csv")
        Dim oRow = oCsv.AddRow()
        oRow.addcell("producte")
        oRow.addcell("pendent")
        oRow.addcell("stock")
        oRow.addcell("sortit")
        oRow.addcell("clients")
        For Each oControlItem As ControlItem In _ControlItems
            oRow = oCsv.AddRow()
            oRow.addcell(oControlItem.Nom)
            oRow.addcell(oControlItem.Pendent)
            oRow.addcell(oControlItem.Stock)
            oRow.addcell(oControlItem.Sortit)
            oRow.addcell(oControlItem.Clients)
        Next
        UIHelper.SaveCsvDialog(oCsv, "desar pendents de entrega El Corte Ingles")
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oSelectedValue As DTOStockAvailable = CurrentControlItem.Source
        Dim oFrm As New Frm_Art(oSelectedValue.Sku)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_StockAvailable_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlitem As ControlItem = oRow.DataBoundItem
                If oControlitem IsNot Nothing Then
                    Dim item As DTOStockAvailable = oControlitem.Source
                    If item IsNot Nothing Then
                        If item.Sku.LastProduction Or item.Sku.Obsoleto Then
                            e.Value = My.Resources.aspa
                        End If
                    End If

                End If
        End Select
    End Sub

    Protected Class ControlItem
        Public Property Source As DTOStockAvailable

        Public Property Nom As String
        Public Property Pendent As Integer
        Public Property Stock As Integer
        Public Property Sortit As Integer
        Public Property Clients As Integer

        Public Sub New(oStockAvailable As DTOStockAvailable)
            MyBase.New()
            _Source = oStockAvailable
            With oStockAvailable
                _Nom = _Source.Sku.nomLlarg.Tradueix(Current.Session.Lang)
                _Pendent = _Source.Pendent
                _Stock = .OriginalStock
                _Sortit = .OriginalStock - .AvailableStock
                _Clients = .Clients
            End With
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Shared Function TotalItem(oControlItems As ControlItems)
            Dim retval As New ControlItem
            With retval
                .Nom = "Totals"
                .Pendent = oControlItems.Sum(Function(x) x.Pendent)
                .Stock = oControlItems.Sum(Function(x) x.Stock)
                .Sortit = oControlItems.Sum(Function(x) x.Sortit)
                .Clients = oControlItems.Sum(Function(x) x.Clients)
            End With
            Return retval
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


