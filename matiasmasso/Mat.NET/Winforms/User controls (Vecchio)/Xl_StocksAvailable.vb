Public Class Xl_StocksAvailable
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Pendent
        StockOriginal
        Sortit
        Clients
    End Enum

    Public Shadows Sub Load(value As List(Of DTOStockAvailable))
        _ControlItems = New ControlItems
        For Each oItem As DTOStockAvailable In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        _ControlItems.Insert(0, ControlItem.TotalItem(_ControlItems))
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOStockAvailable
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOStockAvailable = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True



            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "Nom"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Pendent)
                .HeaderText = "Pendent"
                .DataPropertyName = "Pendent"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.StockOriginal)
                .HeaderText = "Stock"
                .DataPropertyName = "Stock"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Sortit)
                .HeaderText = "Sortides"
                .DataPropertyName = "Sortit"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Clients)
                .HeaderText = "Clients"
                .DataPropertyName = "Clients"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With

        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOStockAvailable)
        Dim retval As New List(Of DTOStockAvailable)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem.Source IsNot Nothing Then
            If oControlItem IsNot Nothing Then
                Dim oProductSku As DTOProductSku = oControlItem.Source.Sku
                Dim oMenu_ProductSku As New Menu_ProductSku(oProductSku)
                AddHandler oMenu_ProductSku.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_ProductSku.Range)
                oContextMenu.Items.Add("-")
            End If
        End If

        oContextMenu.Items.Add(New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf Do_Excel))

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOStockAvailable = CurrentControlItem.Source
        Dim oFrm As New Frm_Art(oSelectedValue.Sku)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            If CurrentControlItem.Source IsNot Nothing Then
                RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source.Sku))
            End If
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()

    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint

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
                _Nom = _Source.Sku.NomLlarg
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

