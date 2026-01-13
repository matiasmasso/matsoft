Public Class Xl_PriceLists_Suppliers
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean


    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        Concepte
        Discount_OnInvoice
        Discount_OffInvoice
        Currency
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPriceList_Supplier))
        _ControlItems = New ControlItems
        For Each oItem As DTOPriceList_Supplier In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOPriceList_Supplier
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPriceList_Supplier = oControlItem.Source
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
                .DataPropertyName = "Fch"
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
                .Width = 70
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Concepte)
                .DataPropertyName = "Concepte"
                .HeaderText = "Concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Discount_OnInvoice)
                .DataPropertyName = "Discount_OnInvoice"
                .HeaderText = "dte.en fra"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0%;-0%;"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Discount_OffInvoice)
                .DataPropertyName = "Discount_OffInvoice"
                .HeaderText = "dte.fora fra"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0%;-0%;"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Currency)
                .DataPropertyName = "Currency"
                .HeaderText = "Divisa"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 35
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
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

    Private Function SelectedItems() As List(Of DTOPriceList_Supplier)
        Dim retval As New List(Of DTOPriceList_Supplier)
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
            Dim oMenu As New Menu_PriceList_Supplier(SelectedItems.First)
            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu.Range)
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOPriceList_Supplier = CurrentControlItem.Source
        Dim oFrm As New Frm_PriceList_Supplier(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub



    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Dim oDragDropResult As DragDropEffects
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                Dim oItem As DTOPriceList_Supplier = CurrentControlItem.Source
                If oItem IsNot Nothing Then
                    oDragDropResult = DataGridView1.DoDragDrop(oItem, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOPriceList_Supplier

        Property Fch As Date
        Property Concepte As String
        Property Discount_OnInvoice As Decimal
        Property Discount_OffInvoice
        Property Currency As String

        Public Sub New(oPriceList As DTOPriceList_Supplier)
            MyBase.New()
            _Source = oPriceList
            With oPriceList
                _Fch = .Fch
                _Concepte = .Concepte
                _Discount_OnInvoice = .Discount_OnInvoice / 100
                _Discount_OffInvoice = .Discount_OffInvoice / 100
                _Currency = .Cur.Id
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

