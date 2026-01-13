Imports DTO.Models

Public Class Xl_ECIPurchaseOrders

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of ElCorteInglesOrderModel)
    Private _DefaultValue As DTOPurchaseOrder
    Private _Dept As String
    Private _MenuItemToggleShipped As ToolStripMenuItem
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Private _Filter As String
    Private _Depto As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        MmoNum
        Fch
        Depto
        ECINum
        Eur
        Ico
        Centre
    End Enum

    Public Shadows Sub Load(values As List(Of ElCorteInglesOrderModel))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values.OrderByDescending(Function(x) x.Fch).ThenBy(Function(y) y.Centre).ThenBy(Function(z) z.ECINum).ToList()
        _MenuItemToggleShipped = New ToolStripMenuItem("Ocultar servits", Nothing, AddressOf Refresca)
        _MenuItemToggleShipped.CheckOnClick = True
        Refresca()
    End Sub


    Public ReadOnly Property Depts As List(Of String)
        Get
            Dim retval As New List(Of String)
            For Each oControlItem As ControlItem In _ControlItems
                If Not retval.Contains(oControlItem.Depto) Then retval.Add(oControlItem.Depto)
            Next
            Return retval
        End Get
    End Property

    Public WriteOnly Property Dept()
        Set(value)
            _Dept = value
            Refresca()
        End Set
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim oFilteredValues = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            ' If Not (_MenuItemToggleShipped.Checked And oControlItem.ShippingStatus = DTOPurchaseOrder.ShippingStatusCods.fullyShipped) Then
            If oControlItem.ECINum > "" Then
                _ControlItems.Add(oControlItem)
            End If
            ' End If
        Next

        If _Dept <> "" Then
            For i As Integer = _ControlItems.Count - 1 To 0 Step -1
                If _ControlItems(i).Depto <> _Dept Then
                    _ControlItems.RemoveAt(i)
                End If
            Next
        End If



        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        If oCell IsNot Nothing Then
            'oCell.SortedColumn = Cols.ECINum
            'oCell.SortOrder = DTODatagridviewCell.SortOrders.Descending
        End If

        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        Cursor = Cursors.Default
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of ElCorteInglesOrderModel)
        Dim retval As List(Of ElCorteInglesOrderModel)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) (Not String.IsNullOrEmpty(x.ECINum) AndAlso x.ECINum.ToLower.Contains(_Filter.ToLower)) Or (Not String.IsNullOrEmpty(x.Centre) AndAlso x.Centre.ToLower.Contains(_Filter.ToLower)))
        End If
        If _Depto > "" Then
            retval = retval.FindAll(Function(x) Not String.IsNullOrEmpty(x.Depto) AndAlso x.Depto.ToLower.Contains(_Depto.ToLower))
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

    Public Property Depto As String
        Get
            Return _Depto
        End Get
        Set(value As String)
            _Depto = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOPurchaseOrder
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPurchaseOrder = oControlItem.PurchaseOrder
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
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
        With MyBase.Columns(Cols.MmoNum)
            .HeaderText = "n/num"
            .DataPropertyName = "MmoNum"
            .Width = 50
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
        With MyBase.Columns(Cols.Depto)
            .HeaderText = "Depto"
            .DataPropertyName = "Depto"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.ECINum)
            .HeaderText = "Eci"
            .DataPropertyName = "EciNum"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            '.DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Centre)
            .HeaderText = "Centre"
            .DataPropertyName = "Centre"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

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
            Dim oOrders As New List(Of DTOPurchaseOrder)
            oOrders.Add(oControlItem.PurchaseOrder)

            Dim oMenu_Pdc As New Menu_Pdc(oOrders)
            AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pdc.Range)
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add(_MenuItemToggleShipped)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPurchaseOrder = CurrentControlItem.PurchaseOrder
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.browse
                    Dim oFrm As New Frm_PurchaseOrder(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_ECIPurchaseOrders_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.ShippingStatus
                    Case DTOPurchaseOrder.ShippingStatusCods.emptyOrder
                        e.Value = My.Resources.aspa
                    Case DTOPurchaseOrder.ShippingStatusCods.unShipped
                        e.Value = My.Resources.harveyballEmpty
                    Case DTOPurchaseOrder.ShippingStatusCods.halfShipped
                        e.Value = My.Resources.harveyballHalf
                    Case DTOPurchaseOrder.ShippingStatusCods.fullyShipped
                        e.Value = My.Resources.harveyballFull
                End Select
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As ElCorteInglesOrderModel

        Property MmoNum As String
        Property Fch As Date
        Property Depto As String
        Property ECINum As String
        Property Eur As Decimal
        Property Centre As String
        Property ShippingStatus As DTOPurchaseOrder.ShippingStatusCods



        Public Sub New(value As ElCorteInglesOrderModel)
            MyBase.New()
            'If value.Num = 22537 Then Stop

            _Source = value
            With value
                _MmoNum = .Id
                _ECINum = .ECINum
                _Fch = .Fch
                _Centre = .Centre
                _Depto = .Depto
                _Eur = .Eur
                _ShippingStatus = .ShippingStatus
            End With
        End Sub

        Public Function PurchaseOrder() As DTOPurchaseOrder
            Dim retval As New DTOPurchaseOrder(Source.Guid)
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


