Public Class Xl_OrderConfirmations
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Fch
        Proveidor
        DocumentNumber
        CustomerOrder
        Amt
    End Enum

    Public Shadows Sub Load(value As OrderConfirmations)
        _ControlItems = New ControlItems
        For Each oItem As OrderConfirmation In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As OrderConfirmation
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As OrderConfirmation = oControlItem.Source
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

            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.ico), DataGridViewImageColumn)
                .DataPropertyName = "Ico"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Proveidor)
                .HeaderText = "Proveidor"
                .DataPropertyName = "Proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.documentNumber)
                .HeaderText = "Confirmació"
                .DataPropertyName = "documentNumber"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.CustomerOrder)
                .HeaderText = "Comanda"
                .DataPropertyName = "CustomerOrder"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Amt)
                .HeaderText = "Import"
                .DataPropertyName = "Amt"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
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

    Private Function SelectedItems() As OrderConfirmations
        Dim retval As New OrderConfirmations
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
            Dim oMenu_OrderConfirmation As New Menu_OrderConfirmation(CurrentControlItem.Source)
            AddHandler oMenu_OrderConfirmation.AfterUpdate, AddressOf refreshRequest
            oContextMenu.Items.AddRange(oMenu_OrderConfirmation.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oOrderConfirmation As OrderConfirmation = oControlItem.Source
        Dim oFrm As New Frm_OrderConfirmation(oOrderConfirmation)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub refreshRequest(sender As Object, e As EventArgs)
        RaiseEvent RequestToRefresh(Me, e)
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As OrderConfirmation

        Public Property ico As Image
        Public Property Fch As Date
        Public Property Proveidor As String
        Public Property DocumentNumber As String
        Public Property CustomerOrder As String
        Public Property Amt As Decimal

        Public Sub New(oOrderConfirmation As OrderConfirmation)
            MyBase.New()
            _Source = oOrderConfirmation
            With oOrderConfirmation
                _ico = Nothing
                _Fch = .Fch
                If .SupplierContact IsNot Nothing Then
                    _Proveidor = .SupplierContact.Clx
                End If
                _DocumentNumber = .DocumentNumber
                _CustomerOrder = .CustomerOrderRef
                _Amt = .Total.eur
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

