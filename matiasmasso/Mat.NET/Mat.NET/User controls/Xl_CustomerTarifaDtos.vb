Public Class Xl_CustomerTarifaDtos
    Private _SelectionMode As BLL.Defaults.SelectionModes
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        Dto
        Product
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCustomerTarifaDto), Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        _SelectionMode = oSelectionMode
        _ControlItems = New ControlItems
        For Each oItem As DTOCustomerTarifaDto In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOCustomerTarifaDto
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCustomerTarifaDto = oControlItem.Source
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
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Dto)
                .HeaderText = "Dto"
                .DataPropertyName = "Dto"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#.00\%;-#.00\%;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Product)
                .HeaderText = "Producte"
                .DataPropertyName = "Product"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of DTOCustomerTarifaDto)
        Dim retval As New List(Of DTOCustomerTarifaDto)
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

        If oControlItem IsNot Nothing Then
            Dim oMenu_CustomerDto As New Menu_CustomerDto(SelectedItems.First)
            AddHandler oMenu_CustomerDto.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_CustomerDto.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick

        Select Case _selectionmode
            Case BLL.Defaults.SelectionModes.Selection
                Dim oControlItem As ControlItem = CurrentControlItem()
                If oControlItem IsNot Nothing Then
                    Dim oCustomerDto As DTOCustomerTarifaDto = SelectedItems.First
                    RaiseEvent onItemSelected(Me, New MatEventArgs(oCustomerDto))
                End If
            Case Else
                Dim oSelectedValue As DTOCustomerTarifaDto = CurrentControlItem.Source
                Dim oFrm As New Frm_CustomerDto(oSelectedValue)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If IsActive(oControlItem) Then
        Else
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Function IsActive(oControl As ControlItem) As Boolean
        Dim oDto As DTOCustomerTarifaDto = oControl.Source
        Dim oList As List(Of ControlItem) = _ControlItems.ToList

        Dim outdated As Boolean
        If oDto.Product Is Nothing Then
            outdated = oList.Exists(Function(x) x.fch > oDto.Fch And x.Source.Product Is Nothing)
        Else
            outdated = oList.Exists(Function(x) x.fch > oDto.Fch And (x.Source.Product Is Nothing Or oDto.Product.Equals(x.Source.Product)))
        End If
        Dim retval As Boolean = Not outdated
        Return retval
    End Function

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOCustomerTarifaDto

        Property fch As Date
        Property Dto As Decimal
        Property Product As String

        Public Sub New(value As DTOCustomerTarifaDto)
            MyBase.New()
            _Source = value
            With value
                _fch = .Fch
                _Dto = .Dto
                If .Product Is Nothing Then
                    _Product = "(totes les marques)"
                Else
                    _Product = BLL.BLLProduct.FullNom(.Product)
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

