Public Class Xl_ProductSkuPncs
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPurchaseOrderItem)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean


    Private Enum Cols
        Pdc
        Qty
        Fch
        Nom
        Preu
        Dto
        Shipment
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrderItem))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOPurchaseOrderItem) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOPurchaseOrderItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPurchaseOrderItem)
        Dim retval As List(Of DTOPurchaseOrderItem)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.PurchaseOrder.Contact.FullNom.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOPurchaseOrderItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPurchaseOrderItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Pdc)
            .HeaderText = "Comanda"
            .DataPropertyName = "Pdc"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Quant"
            .DataPropertyName = "Qty"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Preu)
            .HeaderText = "Preu"
            .DataPropertyName = "Preu"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dto"
            .DataPropertyName = "Dto"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Shipment)
            .HeaderText = "Entrega"
            .DataPropertyName = "Shipment"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
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

    Private Function SelectedItems() As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
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
            Dim oMenu_PurchaseOrderItem As New Menu_PurchaseOrderItem(SelectedItems.First)
            AddHandler oMenu_PurchaseOrderItem.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PurchaseOrderItem.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("refresca", Nothing, AddressOf Refresca)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPurchaseOrderItem = CurrentControlItem.Source
            Dim oFrm As New Frm_PurchaseOrder(oSelectedValue.PurchaseOrder)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            'RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_ProductSkuPncs_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim item As DTOPurchaseOrderItem = oControlItem.Source
        If item.PurchaseOrder.Pot Then
            UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.Yellow)
        ElseIf item.PurchaseOrder.BlockStock Then
            UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.GreenYellow)
        ElseIf item.PurchaseOrder.FchDeliveryMin <> Nothing AndAlso item.purchaseOrder.fchDeliveryMin > DTO.GlobalVariables.Today() Then
            UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.Orange)
        Else
            oRow.DefaultCellStyle.BackColor = Color.White
        End If
    End Sub

    Private Sub Xl_ProductSkuPncs_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Control) Then
            Dim oRow As DataGridViewRow = MyBase.CurrentRow
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Dim item As DTOPurchaseOrderItem = oControlItem.Source
            Dim oOrder As DTOPurchaseOrder = item.PurchaseOrder
            Dim oCustomer As DTOCustomer = oOrder.Customer
            root.NewCliAlbNew(oCustomer)
            e.SuppressKeyPress = True
        End If

    End Sub

    Protected Class ControlItem
        Property Source As DTOPurchaseOrderItem

        Property Pdc As Integer
        Property Qty As Integer
        Property Fch As Date
        Property Nom As String
        Property Preu As Decimal
        Property Dto As Decimal
        Property Shipment As Nullable(Of Date)


        Public Sub New(value As DTOPurchaseOrderItem)
            MyBase.New()
            _Source = value
            With value
                _Pdc = .PurchaseOrder.Num
                _Qty = .Pending
                _Fch = .PurchaseOrder.Fch
                _Nom = .PurchaseOrder.Contact.FullNom
                _Preu = .Price.Eur
                _Dto = .Dto
                If .PurchaseOrder.Cod = DTOPurchaseOrder.Codis.Proveidor Then
                    If .ETD <> Nothing Then
                        _Shipment = .ETD
                    End If
                Else
                    If .purchaseOrder.fchDeliveryMin <> Nothing Then
                        _Shipment = .purchaseOrder.fchDeliveryMin
                    End If
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


