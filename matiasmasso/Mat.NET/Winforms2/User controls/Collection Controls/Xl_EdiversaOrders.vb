Public Class Xl_EdiversaOrders
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOEdiversaOrder)
    Private _Total As Decimal
    Private _DefaultValue As DTOVisaEmisor
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    'Private _Mode As Modes

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)


    'Public Enum Modes
    '    ProcessPending
    '    ConfirmationPending
    'End Enum

    Private Enum Cols
        Ico
        CliNom
        Fch
        OrderNum
        Amt
    End Enum

    Public Shadows Sub Load(values As List(Of DTOEdiversaOrder)) ', Optional oMode As Modes = Modes.ProcessPending)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        ' _Mode = oMode
        Refresca()
    End Sub

    Public ReadOnly Property Total As Decimal
        Get
            Return _Total
        End Get
    End Property
    Public ReadOnly Property Count As Integer
        Get
            Return _ControlItems.Count
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOEdiversaOrder) = FilteredValues()
        _ControlItems = New ControlItems
        _Total = 0
        For Each oItem As DTOEdiversaOrder In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
            _Total += oControlItem.Amt
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOEdiversaOrder)
        Dim retval As List(Of DTOEdiversaOrder) = Nothing
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.DocNum.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOEdiversaOrder
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOEdiversaOrder = oControlItem.Source
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



        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.CliNom)
            .HeaderText = "Client"
            .DataPropertyName = "CliNom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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
        With MyBase.Columns(Cols.OrderNum)
            .HeaderText = "Numero"
            .DataPropertyName = "OrderNum"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
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

    Private Function SelectedItems() As List(Of DTOEdiversaOrder)
        Dim retval As New List(Of DTOEdiversaOrder)
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
            Dim oMenu_EdiversaOrder As New Menu_EDiversaOrder(SelectedItems)
            AddHandler oMenu_EdiversaOrder.AfterUpdate, AddressOf RefreshRequest
            AddHandler oMenu_EdiversaOrder.RequestToToggleProgressBar, AddressOf ToggleProgressBarRequest
            oContextMenu.Items.AddRange(oMenu_EdiversaOrder.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("procesa totes les comandes validades", Nothing, AddressOf Do_ProcessAll)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Async Sub Do_ProcessAll()
        Dim exs As New List(Of Exception)
        If Await FEB.EdiversaOrders.ProcessAllValidated(Current.Session.User, exs) Then
            MsgBox("Comandes processades satisfactoriament", MsgBoxStyle.Information)
            RefreshRequest(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOEdiversaOrder = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_EDiversaOrder(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
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


    Private Sub Xl_EdiversaOrders_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Source.Exceptions.Count > 0 Then
                    If oControlItem.Source.Exceptions.Any(Function(x) x.Cod = DTOEdiversaException.Cods.DuplicatedOrder) Then
                        e.Value = My.Resources.duplicated
                    Else
                        e.Value = My.Resources.WarnRed16
                    End If
                ElseIf oControlItem.Source.Items.Exists(Function(x) x.Exceptions.Count > 0) Then
                    e.Value = My.Resources.warning
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub Xl_EdiversaOrders_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            If oRow IsNot Nothing Then
                Dim oControlitem As ControlItem = oRow.DataBoundItem
                Dim oOrder As DTOEdiversaOrder = oControlitem.Source
                e.ToolTipText = oOrder.Report
            End If
        End If
    End Sub

    Private Sub Xl_EdiversaOrders_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint

        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oEdiversaOrder As DTOEdiversaOrder = oControlItem.Source
        If oEdiversaOrder.EdiversaFile IsNot Nothing Then
            Select Case oEdiversaOrder.EdiversaFile.Result
                Case DTOEdiversaFile.Results.pending
                    oRow.DefaultCellStyle.BackColor = Color.White
                Case DTOEdiversaFile.Results.processed
                    oRow.DefaultCellStyle.BackColor = Color.LightBlue
                Case DTOEdiversaFile.Results.deleted
                    oRow.DefaultCellStyle.BackColor = Color.LightGray
            End Select
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOEdiversaOrder

        Property CliNom As String
        Property Fch As Date
        Property OrderNum As String
        Property Amt As Decimal


        Public Sub New(value As DTOEdiversaOrder)
            MyBase.New()
            _Source = value
            With value
                If .Comprador Is Nothing Then
                    _CliNom = "(comprador " & value.CompradorEAN.Value & " no registrat)"
                Else
                    _CliNom = .Comprador.FullNom
                End If
                _Fch = .FchDoc
                _OrderNum = .DocNum

                _Amt = .Items.Where(Function(x) x.Preu IsNot Nothing).Sum(Function(x) DTOAmt.FromQtyPriceDto(x.Qty, x.Preu, x.Dto).Eur)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


