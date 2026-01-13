Public Class Xl_InvoiceDeliveryItems
    Inherits DataGridView

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Concepte
        Qty
        Price
        Dto
        Amt
    End Enum

    Public Shadows Sub Load(values As List(Of DTODelivery), Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        _ControlItems = New ControlItems

        Dim Firstrec As Boolean = True
        Dim oPurchaseOrder As New DTOPurchaseOrder
        Dim oControlItem As ControlItem

        For Each oDelivery In values
            If Firstrec Then
                Firstrec = False
            Else
                oControlItem = New ControlItem
                _ControlItems.Add(oControlItem)
            End If

            oControlItem = New ControlItem(oDelivery)
            _ControlItems.Add(oControlItem)

            For Each oItem As DTODeliveryItem In oDelivery.Items
                If oItem.PurchaseOrderItem.PurchaseOrder IsNot Nothing Then
                    If oItem.PurchaseOrderItem.PurchaseOrder.UnEquals(oPurchaseOrder) Then
                        oPurchaseOrder = oItem.PurchaseOrderItem.PurchaseOrder
                        oControlItem = New ControlItem(oPurchaseOrder)
                        _ControlItems.Add(oControlItem)
                    End If
                End If
                oControlItem = New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Next
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTODeliveryItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTODeliveryItem = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Concepte)
            .HeaderText = "Concepte"
            .DataPropertyName = "Concepte"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Quantitat"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Price)
            .HeaderText = "Preu"
            .DataPropertyName = "Price"
            .Width = 70
            .Width = 65
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dto"
            .DataPropertyName = "Dto"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 70
            .Width = 65
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With

        SetContextMenu()
        _AllowEvents = True
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

    Private Function SelectedItems() As List(Of Object)
        Dim retval As New List(Of Object)
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
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Sku
                    Dim oMenu_DeliveryItem As New Menu_DeliveryItem(oControlItem.Source)
                    AddHandler oMenu_DeliveryItem.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_DeliveryItem.Range)
                Case ControlItem.LinCods.Pdc
                    Dim oMenu_Pdc As New Menu_Pdc(CType(oControlItem.Source, DTOPurchaseOrder))
                    AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Pdc.Range)
                Case ControlItem.LinCods.Alb
                    Dim oMenu_Alb As New Menu_Alb(New Alb(CType(oControlItem.Source, DTODelivery).Guid))
                    AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Alb.Range)
            End Select
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_PurchaseOrderDeliveryItems_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Concepte
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.LinCod
                    Case ControlItem.LinCods.Alb
                        e.CellStyle.Font = New Font(MyBase.Font, FontStyle.Bold)
                    Case ControlItem.LinCods.Pdc
                        e.CellStyle.Font = New Font(MyBase.Font, FontStyle.Italic)
                        e.CellStyle.Padding = New Padding(20, 0, 0, 0)
                    Case Else
                        e.CellStyle.Font = MyBase.Font
                        e.CellStyle.Padding = New Padding(40, 0, 0, 0)
                End Select
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oSelectedValue As DTODeliveryItem = CurrentControlItem.Source
        Dim oAlb As New Alb(oSelectedValue.Delivery.Guid)
        Dim oFrm As New Frm_AlbNew2(oAlb)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As Object

        Property Concepte As String
        Property Qty As Integer
        Property Price As Decimal
        Property Dto As Decimal
        Property Amt As Decimal
        Property LinCod As LinCods

        Public Enum LinCods
            Empty
            Alb
            Pdc
            Sku
        End Enum

        Public Sub New()
            MyBase.New()
            _LinCod = LinCods.Empty
        End Sub

        Public Sub New(value As DTOPurchaseOrder)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Pdc
                _Concepte = BLL.BLLPurchaseOrder.Caption(value)
            End With
        End Sub

        Public Sub New(value As DTODelivery)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Alb
                _Concepte = BLL.BLLDelivery.Caption(value)
            End With
        End Sub

        Public Sub New(value As DTODeliveryItem)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Sku
                _Concepte = .PurchaseOrderItem.Sku.NomLlarg
                _Qty = .Qty
                If .Price IsNot Nothing Then
                    _Price = .Price.Eur
                End If
                _Dto = .Dto
                _Amt = .Amt.Eur
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


