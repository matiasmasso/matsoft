Public Class Xl_InvoiceReceivedItemsFactory
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPurchaseOrderItem)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Checked
        Nom
        QtyOrdered
        QtyInvoiced
        Eur
        Dto
        Amt
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrderItem))
        _Values = values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        Dim oPurchaseOrder As New DTOPurchaseOrder
        Dim oControlItem As ControlItem
        For Each oItem In _Values
            If oItem.PurchaseOrder.UnEquals(oPurchaseOrder) Then
                oPurchaseOrder = oItem.PurchaseOrder
                oControlItem = New ControlItem(oPurchaseOrder)
                _ControlItems.Add(oControlItem)
            End If
            oControlItem = New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Items As List(Of DTOInvoiceReceived.Item)
        Get
            Dim retval As New List(Of DTOInvoiceReceived.Item)
            Dim oCheckedControlItems = CheckedControlItems()
            For Each oControlItem In oCheckedControlItems
                Dim item As New DTOInvoiceReceived.Item
                With item
                    .Amount = oControlItem.Amt
                    .Dto = oControlItem.Dto
                    .Price = oControlItem.Preu
                    .Qty = oControlItem.QtyInvoiced
                    .PurchaseOrderItem = oControlItem.Source
                    .PurchaseOrder = DTOGuidNom.Factory(.PurchaseOrderItem.PurchaseOrder.Guid)
                    .PurchaseOrderId = .PurchaseOrderItem.PurchaseOrder.formattedId
                    .Sku = DTOGuidNom.Factory(.PurchaseOrderItem.Sku.Guid)
                    .SkuNom = oControlItem.Nom
                    .SkuRef = .PurchaseOrderItem.Sku.RefProveidor
                    .SkuEan = .PurchaseOrderItem.Sku.Ean13
                    .Lin = oCheckedControlItems.IndexOf(oControlItem) + 1
                End With
                retval.Add(item)
            Next
            Return retval
        End Get
    End Property

    Private Function CheckedControlItems() As List(Of ControlItem)
        Return _ControlItems.Where(Function(x) x.LinCod = ControlItem.LinCods.Item And x.Checked = True).ToList()
    End Function


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.35
        'MyBase.RowRol.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False

        MyBase.Columns.Add(New DataGridViewCheckBoxColumn)
        With DirectCast(MyBase.Columns(Cols.Checked), DataGridViewCheckBoxColumn)
            .DataPropertyName = "Checked"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 20
            '.DefaultCellStyle.NullValue = Nothing
        End With


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Concepte"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.QtyOrdered)
            .HeaderText = "Demanat"
            .DataPropertyName = "QtyOrdered"
            .Width = 55
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.QtyInvoiced)
            .HeaderText = "Facturat"
            .DataPropertyName = "QtyInvoiced"
            .Width = 55
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Preu"
            .DataPropertyName = "Preu"
            .Width = 50
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
            .DefaultCellStyle.Format = "#.##\%;-#.##\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Import"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .ReadOnly = True
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
            'Dim oMenu_Template As New Menu_Template(SelectedItems.First)
            'AddHandler oMenu_Template.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Template.Range)
            'oContextMenu.Items.Add("-")
        End If
        'oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            If TypeOf CurrentControlItem.Source Is DTOPurchaseOrder Then
                Dim oPdc As DTOPurchaseOrder = CurrentControlItem.Source
                Dim oFrm As New Frm_PurchaseOrder(oPdc)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            ElseIf TypeOf CurrentControlItem.Source Is DTOPurchaseOrderItem Then
                Dim oPnc As DTOPurchaseOrderItem = CurrentControlItem.Source
                Dim oFrm As New Frm_PurchaseOrder(oPnc.PurchaseOrder)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            End If

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Checked
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Select Case oControlItem.LinCod
                        Case ControlItem.LinCods.PurchaseOrder
                            Dim oPurchaseOrder As DTOPurchaseOrder = oControlItem.Source
                            Dim oControlItems = _ControlItems.Where(Function(x) x.LinCod = ControlItem.LinCods.Item AndAlso CType(x.Source, DTOPurchaseOrderItem).PurchaseOrder.Equals(oPurchaseOrder)).ToList()
                            For Each oItem In oControlItems
                                oItem.Check(oControlItem.Checked)
                            Next
                            MyBase.Refresh()
                        Case ControlItem.LinCods.Item
                            oControlItem.Check(oControlItem.Checked)
                            MyBase.Refresh()
                    End Select

                    RaiseEvent AfterUpdate(Me, New MatEventArgs())
                Case Cols.QtyInvoiced, Cols.Dto, Cols.Eur
                    MyBase.Refresh()
                    RaiseEvent AfterUpdate(Me, New MatEventArgs())
            End Select
        End If
    End Sub


    Public Function Total() As DTOAmt
        Dim retval As New DTOAmt
        For Each oControlItem In CheckedControlItems()
            retval.Add(oControlItem.Amt)
        Next
        Return retval
    End Function

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case MyBase.CurrentCell.ColumnIndex
            Case Cols.Checked
                MyBase.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem

        Select Case oControlItem.LinCod
            Case ControlItem.LinCods.PurchaseOrder
                oRow.DefaultCellStyle.BackColor = Color.LightBlue
            Case ControlItem.LinCods.Item
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Eur
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                'If oControlItem.Price?.Eur = 146.86 Then Stop
                'Dim oItem As DTOInvoiceReceived.Item = oControlItem.Source
                Dim oCur = oControlItem.Price?.Cur
                If oCur IsNot Nothing AndAlso Not oCur?.isEur Then
                    e.CellStyle.Format = oCur.formatString
                End If
                'Case Cols.ico
                'Dim oDocFile As DTODocFile = oItem.Docfile
                'If oDocFile IsNot Nothing Then
                'e.Value = IconHelper.GetIconFromMimeCod(oDocFile.Mime)
                'End If
        End Select
    End Sub



    Protected Class ControlItem
        Private _Preu As Decimal
        Private _Price As DTOAmt
        Private _Dto As Decimal
        Private _Amt As DTOAmt
        Private _QtyInvoiced As Integer

        Property Source As DTOBaseGuid
        Property Checked As Boolean = False
        Property LinCod As LinCods
        Property Nom As String
        Property QtyOrdered As Integer
        Property Import As Decimal

        Property Price As DTOAmt
            Get
                Return _Price
            End Get
            Set(value As DTOAmt)
                _Price = value
                _Preu = _Price.Val
            End Set
        End Property

        Property Preu As Decimal
            Get
                Return _Preu
            End Get
            Set(value As Decimal)
                _Preu = value
                Dim oCur = _Price.Cur
                _Price = DTOAmt.Factory(oCur, _Preu)
                Me.Amt = DTOAmt.import(_QtyInvoiced, _Price, _Dto)
            End Set
        End Property


        Property Dto As Decimal
            Get
                Return _Dto
            End Get
            Set(value As Decimal)
                _Dto = value
                Me.Amt = DTOAmt.import(_QtyInvoiced, _Price, _Dto)
            End Set
        End Property

        Property QtyInvoiced As Integer
            Get
                Return _QtyInvoiced
            End Get
            Set(value As Integer)

                _QtyInvoiced = value
                If value = 0 Then
                    Me.Amt = DTOAmt.Factory()
                Else
                    Me.Amt = DTOAmt.import(_QtyInvoiced, _Price, _Dto)
                End If
            End Set
        End Property

        Property Amt As DTOAmt
            Get
                Return _Amt
            End Get
            Set(value As DTOAmt)
                _Amt = value
                _Import = _Amt.Val
            End Set
        End Property

        Public Enum LinCods
            PurchaseOrder
            Item
        End Enum

        Public Sub New(value As DTOPurchaseOrder)
            MyBase.New()
            _Source = value
            With value
                _Nom = String.Format("Comanda {0} del {1:dd/MM/yy}", value.formattedId, value.Fch)
                _LinCod = LinCods.PurchaseOrder
            End With
        End Sub

        Public Sub New(value As DTOPurchaseOrderItem)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Sku.nomPrvAndRefOrMyd
                _QtyOrdered = .Qty
                _Dto = .Dto
                If .Price IsNot Nothing Then
                    Me.Price = .Price
                End If
                _LinCod = LinCods.Item
            End With
        End Sub

        Public Sub Check(active As Boolean)
            _Checked = active
            Select Case active
                Case True
                    Me.QtyInvoiced = _QtyOrdered
                Case False
                    Me.QtyInvoiced = 0
            End Select
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

