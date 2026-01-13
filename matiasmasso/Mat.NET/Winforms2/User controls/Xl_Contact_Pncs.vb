Public Class Xl_Contact_Pncs

    Inherits DataGridView

    Private _Values As List(Of DTOPurchaseOrderItem)
    Private _ControlItems As ControlItems
    Private _Filter As String
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _AllowEvents As Boolean

    Private _IconPdf As Image = My.Resources.pdf

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Num
        Txt
        Qty
        Preu
        Dto
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrderItem), Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        refresca()
    End Sub


    Public Property Filter As String
        Get
            Return _filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _filter > "" Then
            _filter = ""
            refresca()
        End If
    End Sub

    Private Sub refresca()
        _AllowEvents = False

        Dim oFilteredValues As List(Of DTOPurchaseOrderItem) = FilteredValues()
        _ControlItems = New ControlItems

        Dim oOrder As New DTOPurchaseOrder
        For Each oItem As DTOPurchaseOrderItem In oFilteredValues
            If oItem.PurchaseOrder.UnEquals(oOrder) Then
                oOrder = oItem.PurchaseOrder
                If _ControlItems.Count > 0 Then
                    _ControlItems.Add(New ControlItem)
                End If
                _ControlItems.Add(New ControlItem(oOrder))
                If oOrder.fchDeliveryMin <> Nothing Then
                    _ControlItems.Add(New ControlItem(oOrder.fchDeliveryMin))
                End If
            End If
            _ControlItems.Add(New ControlItem(oItem))
        Next

        If _ControlItems.Count > 0 Then
            MyBase.DataSource = _ControlItems
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
            SetContextMenu()
        End If

        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPurchaseOrderItem)
        Dim retval As List(Of DTOPurchaseOrderItem)
        If _Filter = "" Then
            retval = _Values
        Else
            Dim LCaseFilter As String = _Filter.ToLower
            If IsNumeric(_Filter) Then
                Dim iFilter As Integer = CInt(_Filter)
                retval = _Values.FindAll(Function(x) x.Sku.Id = iFilter Or x.PurchaseOrder.Num = iFilter Or x.PurchaseOrder.Concept.ToLower.Contains(_Filter) Or x.Sku.NomLlarg.Contains(_Filter))
            Else
                retval = _Values.FindAll(Function(x) x.purchaseOrder.concept.ToLower.Contains(LCaseFilter) Or x.sku.nomLlarg.Contains(LCaseFilter))
            End If
        End If
        Return retval
    End Function

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Num)
            .HeaderText = "Numero"
            .DataPropertyName = "Num"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Txt)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Quant"
            .DataPropertyName = "Qty"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Preu)
            .HeaderText = "Preu"
            .DataPropertyName = "Preu"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dto"
            .DataPropertyName = "Dto"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
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

        If retval.Count = 0 And TypeOf CurrentControlItem.Source Is DTOPurchaseOrderItem Then
            retval.Add(CurrentControlItem.Source)
        End If
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
        Dim oControlItems As ControlItems = SelectedControlItems()

        If oControlItems.Count > 0 Then
            Dim oControlItem As ControlItem = oControlItems.First
            If oControlItem IsNot Nothing Then

                Select Case oControlItem.LinCod
                    Case ControlItem.LinCods.comanda
                        Dim oOrders As New List(Of DTOPurchaseOrder)
                        oOrders.Add(DirectCast(oControlItem.Source, DTOPurchaseOrder))
                        Dim oMenu1 As New Menu_Pdc(oOrders)
                        AddHandler oMenu1.AfterUpdate, AddressOf RefreshRequest
                        oContextMenu.Items.AddRange(oMenu1.Range)
                        oContextMenu.Items.Add("-")
                    Case ControlItem.LinCods.item
                        Dim item As DTOPurchaseOrderItem = oControlItem.Source
                        Dim oMenu2 As New Menu_PurchaseOrderItem(item)
                        AddHandler oMenu2.AfterUpdate, AddressOf RefreshRequest
                        oContextMenu.Items.AddRange(oMenu2.Range)
                        oContextMenu.Items.Add("-")
                End Select
            End If


            oContextMenu.Items.Add("Excel (tots)", My.Resources.Excel_16, AddressOf Do_Excel)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub



    Private Sub Do_Excel()
        Dim exs As New List(Of Exception)
        Dim oContact As DTOContact = _Values.First.PurchaseOrder.Contact
        If FEB.Contact.Load(oContact, exs) Then
            Dim sCliNom As String = oContact.Nom
            Dim oLang As DTOLang = oContact.Lang

            Dim oValues As List(Of DTOPurchaseOrderItem) = _Values
            If SelectedItems.Count > 1 Then
                oValues = SelectedItems()
            End If

            Dim sTitle As String = oLang.Tradueix("M+O Pedidos pendientes de entrega", "M+O Comandes pendents de entrega", "M+O Open orders", "M+O encomendas pendentes de entrega") & " " & sCliNom
            Dim oSheet = FEB.PurchaseOrderItems.Excel(oValues, sTitle, sTitle)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Extracte_Pncs_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Txt
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.LinCod
                    Case ControlItem.LinCods.comanda
                        MyBase.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.AliceBlue
                        e.CellStyle.Font = New Font(MyBase.Font, FontStyle.Bold)
                    Case ControlItem.LinCods.dataEntrega
                        e.CellStyle.BackColor = Color.Orange
                    Case ControlItem.LinCods.item
                        e.CellStyle.Padding = New Padding(20, 0, 0, 0)
                End Select
            Case Cols.Qty
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.LinCod
                    Case ControlItem.LinCods.item
                        Dim oPnc As DTOPurchaseOrderItem = oControlItem.Source
                        If oPnc.Sku.Stock > (oPnc.Qty + oPnc.Sku.Clients) Then
                            e.CellStyle.BackColor = LegacyHelper.Defaults.COLOR_OK
                        ElseIf oPnc.Sku.Stock > oPnc.Qty Then
                            e.CellStyle.BackColor = Color.Yellow
                        Else
                            e.CellStyle.BackColor = LegacyHelper.Defaults.COLOR_NOTOK
                        End If
                End Select
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        Select Case _SelectionMode
            Case DTO.Defaults.SelectionModes.Browse
                Select Case oControlItem.LinCod
                    Case ControlItem.LinCods.comanda
                        Dim oOrder As DTOPurchaseOrder = oControlItem.Source
                        Dim oFrm As New Frm_PurchaseOrder(oOrder)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()

                    Case ControlItem.LinCods.item
                        Dim oItem As DTOPurchaseOrderItem = oControlItem.Source
                        Dim oFrm As New Frm_Art(oItem.Sku)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                End Select
            Case DTO.Defaults.SelectionModes.Selection
                If oControlItem.LinCod <> ControlItem.LinCods.blank Then
                    RaiseEvent onItemSelected(Me, New MatEventArgs(oControlItem.Source))
                End If
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.CurrentCellChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As Object

        Property Num As Integer
        Property Txt As String
        Property Qty As Decimal
        Property Preu As Decimal
        Property Dto As Decimal
        Property LinCod As LinCods

        Public Enum LinCods
            blank
            comanda
            dataEntrega
            item
        End Enum

        Public Sub New()
            MyBase.New()
            _LinCod = LinCods.blank
        End Sub

        Public Sub New(value As DTOPurchaseOrder)
            MyBase.New()
            _Source = value
            With value
                _Num = .Num
                _Txt = value.Caption(Lang(value))
                _LinCod = LinCods.comanda
            End With
        End Sub

        Public Sub New(value As DTOPurchaseOrderItem)
            MyBase.New()
            _Source = value
            With value
                _Num = .Sku.id
                _Txt = .sku.nomLlarg.Tradueix(Current.Session.Lang)
                _Qty = .Pending
                If .Price IsNot Nothing Then
                    _Preu = .Price.Eur
                End If
                _Dto = .Dto
                _LinCod = LinCods.item
            End With
        End Sub

        Public Sub New(value As Date)
            MyBase.New()
            _Source = value
            With value
                _Txt = "entrega " & value.ToShortDateString
                _LinCod = LinCods.dataEntrega
            End With
        End Sub

        Shared Function Lang(value As DTOPurchaseOrder) As DTOLang
            Dim exs As New List(Of Exception)
            Dim oContact As DTOContact = value.Customer
            If oContact Is Nothing Then oContact = value.Contact 'TO DEPRECATE
            FEB.Contact.Load(oContact, exs)
            Dim retval As DTOLang = oContact.Lang
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

    Private Sub Xl_Contact_Pncs_SelectionChanged(sender As Object, e As EventArgs) Handles Me.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub
End Class


