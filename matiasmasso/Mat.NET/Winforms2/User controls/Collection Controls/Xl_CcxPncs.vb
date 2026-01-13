Public Class Xl_CcxPncs

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPurchaseOrderItem)
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        clinom
        fch
        fchDeliveryMin
        pdd
        skunom
        qty
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
            retval = _Values.FindAll(Function(x) x.purchaseOrder.concept.ToLower.Contains(_Filter.ToLower) _
                Or DTOCustomer.refOrNomComercial(x.purchaseOrder.contact).ToLower.Contains(_Filter.ToLower) _
                Or x.sku.nomLlarg.Contains(_Filter.ToLower)
                )
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
        'MyBase.RowPurchaseOrderItem.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.clinom)
            .HeaderText = "Centre"
            .DataPropertyName = "CliNom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .MinimumWidth = 60
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fchDeliveryMin)
            .HeaderText = "Entrega"
            .DataPropertyName = "FchDeliveryMin"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.pdd)
            .HeaderText = "Comanda"
            .DataPropertyName = "Pdd"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .MinimumWidth = 60
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.skunom)
            .HeaderText = "Producte"
            .DataPropertyName = "SkuNom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .MinimumWidth = 60
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.qty)
            .HeaderText = "Pendent"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
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
            oContextMenu.Items.Add("albarà", Nothing, AddressOf Do_Ship)
            oContextMenu.Items.Add("-")
            Dim oMenu_PurchaseOrderItem As New Menu_PurchaseOrderItem(SelectedItems.First)
            AddHandler oMenu_PurchaseOrderItem.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PurchaseOrderItem.Range)
            oContextMenu.Items.Add("-")
            oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)
            oContextMenu.Items.Add("Eliminar linies seleccionades", Nothing, AddressOf Do_DeletePncs)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Excel()
        Dim oSheet As New MatHelper.Excel.Sheet("Pedidos pendientes")
        With oSheet
            .AddColumn("Centro", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Pedido", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Fecha", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Producto", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Unidades", MatHelper.Excel.Cell.NumberFormats.Integer)
        End With
        Dim items As ControlItems = SelectedControlItems()
        If items.Count <= 1 Then
            items = _ControlItems
        End If
        For Each Item As ControlItem In items
            Dim oRow As MatHelper.Excel.Row = oSheet.AddRow()
            With Item
                oRow.AddCell(.clinom)
                oRow.AddCell(.Pdd)
                oRow.AddCell(.fch)
                oRow.AddCell(.skunom)
                oRow.AddCell(.qty)
            End With
        Next

        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
    Private Sub Do_Ship()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oCustomer As DTOCustomer = oControlItem.Source.purchaseOrder.Contact
        root.NewCliAlbNew(oCustomer)
    End Sub

    Private Async Sub Do_DeletePncs()
        Dim oSelectedValues As List(Of DTOPurchaseOrderItem) = SelectedItems()
        Dim sPrompt As String = String.Format("eliminem les {0} linies sel.leccionades?", oSelectedValues.Count)
        Dim rc As MsgBoxResult = MsgBox(sPrompt, MsgBoxStyle.OkCancel, "M+O comandes pendents d'entrega")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.PurchaseOrderItems.Delete(exs, oSelectedValues) Then
                RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPurchaseOrderItem = CurrentControlItem.Source
            Dim oFrm As New Frm_PurchaseOrder(oSelectedValue.purchaseOrder)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOPurchaseOrderItem

        Property clinom As String
        Property fch As Date
        Property fchDeliveryMin As Nullable(Of Date)
        Property Pdd As String
        Property skunom As String
        Property qty As Integer

        Public Sub New(value As DTOPurchaseOrderItem)
            MyBase.New()
            _Source = value
            With value
                _clinom = DTOCustomer.RefOrNomComercial(.purchaseOrder.Contact)
                _fch = .purchaseOrder.fch
                If .purchaseOrder.fchDeliveryMin <> Nothing Then
                    _fchDeliveryMin = .purchaseOrder.fchDeliveryMin
                End If
                _Pdd = .purchaseOrder.concept
                _skunom = .sku.nomLlarg.Tradueix(Current.Session.Lang)
                _qty = .pending
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


