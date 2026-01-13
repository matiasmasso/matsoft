Public Class Xl_Contact_Pncs

    Inherits DataGridView

    Private _Values As List(Of DTOPurchaseOrderItem)
    Private _ControlItems As ControlItems
    Private _Filter As String
    Private _AllowEvents As Boolean

    Private _IconPdf As Image = My.Resources.pdf

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Num
        Txt
        Qty
        Preu
        Dto
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrderItem), Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
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
                retval = _Values.FindAll(Function(x) x.Sku.Id = iFilter Or x.PurchaseOrder.Id = iFilter Or x.PurchaseOrder.Concept.ToLower.Contains(_Filter) Or x.Sku.NomLlarg.Contains(_Filter))
            Else
                retval = _Values.FindAll(Function(x) x.PurchaseOrder.Concept.ToLower.Contains(LCaseFilter) Or x.Sku.NomLlarg.ToLower.Contains(LCaseFilter))
            End If
        End If
        Return retval
    End Function

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

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
            .DefaultCellStyle.Format = "#\%;-#\%;#"
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
        Dim oControlItems As ControlItems = SelectedControlItems()

        If oControlItems.Count > 0 Then
            Dim oControlItem As ControlItem = oControlItems.First
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.comanda
                    Dim oMenu1 As New Menu_PurchaseOrder(oControlItem.Source)
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

            oContextMenu.Items.Add("Excel", My.Resources.Excel_16, AddressOf Do_Excel)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub



    Private Sub Do_Excel()
        Dim oContact As DTOContact = _Values.First.PurchaseOrder.Customer
        BLL.BLLContact.Load(oContact)
        Dim sCliNom As String = oContact.Nom
        Dim oLang As DTOLang = oContact.Lang

        Dim oDlg As New SaveFileDialog
        With oDlg
            .Filter = "Excel (*.xlsx)|*.xlsx"
            .DefaultExt = ".xlsx"
            .FileName = oLang.Tradueix("M+O Pedidos pendientes de entrega", "M+O Comandes pendents de entrega", "M+O Open orders") & " " & sCliNom & ".xlsx"
            If .ShowDialog Then
                If .FileName > "" Then
                    Dim oWorkbook As ClosedXML.Excel.XLWorkbook = BLL.BLLPurchaseOrderItems.Excel(_Values)
                    oWorkbook.SaveAs(.FileName)
                End If
            End If
        End With
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
                    Case ControlItem.LinCods.item
                        e.CellStyle.Padding = New Padding(20, 0, 0, 0)
                End Select
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        Select Case oControlItem.LinCod
            Case ControlItem.LinCods.comanda
                Dim oOrder As DTOPurchaseOrder = oControlItem.Source
                Dim oFrm As New Frm_PurchaseOrder(oOrder)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case ControlItem.LinCods.item
                Dim oSku As DTOProductSku = oControlItem.Source
                Dim oArt As New Art(oSku.Guid)
                Dim oFrm As New Frm_Art(oArt)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
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
                _Num = .Id
                _Txt = BLL.BLLPurchaseOrder.Caption(value, Lang(value))
                _LinCod = LinCods.comanda
            End With
        End Sub

        Public Sub New(value As DTOPurchaseOrderItem)
            MyBase.New()
            _Source = value
            With value
                _Num = .Sku.Id
                _Txt = .Sku.NomLlarg
                _Qty = .Pending
                If .Price IsNot Nothing Then
                    _Preu = .Price.Eur
                End If
                _Dto = .Dto
                _LinCod = LinCods.item
            End With
        End Sub

        Shared Function Lang(value As DTOPurchaseOrder) As DTOLang
            Dim oContact As DTOContact = value.Customer
            BLL.BLLContact.Load(oContact)
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


