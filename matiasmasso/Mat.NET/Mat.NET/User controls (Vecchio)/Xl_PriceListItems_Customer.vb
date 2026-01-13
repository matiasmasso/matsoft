Public Class Xl_PriceListItems_Customer
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToImport(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ArtNom
        TarifaA
        TarifaB
        Retail
        Fch
    End Enum


    Public Shadows Sub Load(values As List(Of DTOPricelistItemCustomer))
        _ControlItems = New ControlItems
        For Each oItem As DTOPricelistItemCustomer In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Items As List(Of DTOPricelistItemCustomer)
        Get
            Dim retval As New List(Of DTOPricelistItemCustomer)
            For Each item As ControlItem In _ControlItems
                retval.Add(item.Source)
            Next
            Return retval
        End Get
    End Property

    Public Sub ShowExcel()
        Dim oExcelSheet As New DTOExcelSheet
        oExcelSheet.AddRowWithCells("marca", "categoría", "tarifa", "tarifa B", "PVP")
        For Each item As ControlItem In _ControlItems
            oExcelSheet.AddRowWithCells(item.Source.Sku.Category.Brand.Nom, item.Source.Sku.Category.Nom, item.Source.Sku.NomCurt, item.TarifaA, item.TarifaB, item.Retail)
        Next

        UIHelper.ShowExcel(oExcelSheet)
    End Sub

    Private Sub LoadGrid()

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.ArtNom)
                .HeaderText = "producte"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.TarifaA)
                .HeaderText = "tarifa A"
                .DataPropertyName = "TarifaA"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.TarifaB)
                .HeaderText = "tarifa B"
                .DataPropertyName = "TarifaB"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Retail)
                .HeaderText = "PVP recomenat"
                .DataPropertyName = "Retail"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.ArtNom

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        'LoadGrid()

        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()

        If oCurrentControlItem IsNot Nothing Then
            Dim oDTOPricelistItemCustomer As DTOPricelistItemCustomer = oCurrentControlItem.Source
            Dim oMenu_PricelistItemCustomer As New Menu_PricelistItemCustomer(oDTOPricelistItemCustomer)
            AddHandler oMenu_PricelistItemCustomer.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PricelistItemCustomer.Range)
        End If

        oContextMenu.Items.Add("Afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("Importar ref/pvp", Nothing, AddressOf Do_Import)
        oContextMenu.Items.Add("Refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DoZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim item As DTOPricelistItemCustomer = CurrentControlItem.Source()
        Dim oFrm As New Frm_PricelistItemCustomer(item)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub Do_AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Import(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent RequestToImport(Me, MatEventArgs.Empty)
    End Sub

    Private Sub OnNewItemAdded(sender As Object, e As EventArgs)
        'Dim oItem As DTOPricelistItemCustomer = sender
        'Dim oItems As List(Of DTOPricelistItemCustomer) = DataGridView1.DataSource
        'oItems.Add(oItem)
        'RefreshRequest(sender, e)
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOPricelistItemCustomer

        Property Checked As Boolean
        Property Nom As String
        Property TarifaA As Decimal
        Property TarifaB As Decimal
        Property Retail As Decimal

        Public Sub New(item As DTOPricelistItemCustomer)
            MyBase.New()
            _Source = item
            With _Source
                _Nom = .Sku.NomLlarg
                If .TarifaA IsNot Nothing Then
                    _TarifaA = .TarifaA.Eur
                End If
                If .TarifaB IsNot Nothing Then
                    _TarifaB = .TarifaB.Eur
                End If
                _Retail = .Retail.Eur
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class