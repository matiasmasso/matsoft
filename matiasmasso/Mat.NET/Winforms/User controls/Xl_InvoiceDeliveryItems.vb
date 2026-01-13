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

    Public Function BaseImponible() As DTOAmt
        Dim DcEur = _ControlItems.Select(Function(x) x.Amt).Sum()
        Return DTOAmt.Factory(DcEur)
    End Function

    Public Shadows Sub Load(values As List(Of DTODelivery), oLang As DTOLang, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        _ControlItems = New ControlItems

        If values IsNot Nothing Then
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

                oControlItem = New ControlItem(oDelivery, oLang)
                _ControlItems.Add(oControlItem)

                Dim oLastSpv As New DTOSpv

                For Each oItem As DTODeliveryItem In oDelivery.Items
                    If oDelivery.Cod = DTOPurchaseOrder.Codis.reparacio Then
                        If oItem.Spv IsNot Nothing Then
                            If oItem.Spv.unEquals(oLastSpv) Then
                                oLastSpv = oItem.Spv
                                For Each sLine As String In oLastSpv.lines(oLang)
                                    oControlItem = New ControlItem(oItem.Spv, sLine)
                                    _ControlItems.Add(oControlItem)
                                Next
                            End If
                        End If
                    Else
                        If oItem.PurchaseOrderItem.PurchaseOrder IsNot Nothing Then
                            If oItem.PurchaseOrderItem.PurchaseOrder.unEquals(oPurchaseOrder) Then
                                oPurchaseOrder = oItem.PurchaseOrderItem.PurchaseOrder
                                oControlItem = New ControlItem(oPurchaseOrder, oLang)
                                _ControlItems.Add(oControlItem)
                            End If
                        End If
                    End If

                    oControlItem = New ControlItem(oItem, oLang)
                    _ControlItems.Add(oControlItem)

                    If oItem.Bundle.Count > 0 Then
                        oControlItem = New ControlItem(oLang.Tradueix("compuesto de los siguientes elementos:", "compost dels següents elements", "composed of the following elements:"))
                        _ControlItems.Add(oControlItem)
                        For Each oChildItem In oItem.Bundle
                            oControlItem = New ControlItem("        " & oChildItem.Sku.NomLlarg.Tradueix(oLang))
                            _ControlItems.Add(oControlItem)
                        Next
                    End If
                Next
            Next
        End If
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
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
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
                    Dim oOrders As New List(Of DTOPurchaseOrder)
                    oOrders.Add(oControlItem.Source)
                    Dim oMenu_Pdc As New Menu_Pdc(oOrders)
                    AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Pdc.Range)
                Case ControlItem.LinCods.Alb
                    Dim oDeliveries As New List(Of DTODelivery)
                    oDeliveries.Add(oControlItem.Source)
                    Dim oMenu_Alb As New Menu_Delivery(oDeliveries)
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
        Dim oFrm As New Frm_AlbNew2(oSelectedValue.Delivery)
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
            Spv
            Sku
        End Enum

        Public Sub New()
            MyBase.New()
            _LinCod = LinCods.Empty
        End Sub

        Public Sub New(value As String)
            MyBase.New()
            _Source = value
            _LinCod = LinCods.Empty
            _Concepte = "    " & value
        End Sub

        Public Sub New(value As DTOPurchaseOrder, oLang As DTOLang)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Pdc
                _Concepte = value.caption(oLang)
            End With
        End Sub

        Public Sub New(value As DTOSpv, sLine As String)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Spv
                _Concepte = sLine
            End With
        End Sub

        Public Sub New(value As DTODelivery, oLang As DTOLang)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Alb
                _Concepte = DTODelivery.caption(value, oLang)
            End With
        End Sub

        Public Sub New(value As DTODeliveryItem, oLang As DTOLang)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Sku
                Dim oSku As DTOProductSku
                If .PurchaseOrderItem Is Nothing Then
                    oSku = .Sku
                Else
                    oSku = .PurchaseOrderItem.Sku
                End If
                _Concepte = oSku.NomLlarg.Tradueix(oLang)
                _Qty = .Qty
                If .Price IsNot Nothing Then
                    _Price = .Price.Eur
                End If
                _Dto = .Dto
                _Amt = value.import.Eur
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


