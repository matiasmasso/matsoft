Public Class Xl_PriceListItems_Customer
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToImport(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ArtNom
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
        SetContextMenu()
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
        Dim oSheet As New MatHelperStd.ExcelHelper.Sheet
        oSheet.AddRowWithCells("marca", "categoría", "PVP")
        For Each item As ControlItem In _ControlItems
            oSheet.addRowWithCells(item.Source.sku.category.brand.nom.Tradueix(Current.Session.Lang), item.Source.sku.category.nom.Tradueix(Current.Session.Lang), item.Source.sku.NomCurt, item.Retail)
        Next

        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadGrid()

        Dim oCurrentCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(DataGridView1)
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

        UIHelper.SetDataGridviewCurrentCell(DataGridView1, oCurrentCell)
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
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
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
        Property Retail As Decimal

        Public Sub New(item As DTOPricelistItemCustomer)
            MyBase.New()
            _Source = item
            With _Source
                _Nom = .sku.nomLlarg.Tradueix(Current.Session.Lang)
                If .Retail IsNot Nothing Then
                    _Retail = .Retail.Val
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class