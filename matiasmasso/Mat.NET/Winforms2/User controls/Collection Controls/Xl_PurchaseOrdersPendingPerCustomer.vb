Public Class Xl_PurchaseOrdersPendingPerCustomer

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPurchaseOrder)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Amt
        FchDeliveryMin
        FchDeliveryMax
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrder))
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
        Dim query = _Values.GroupBy(Function(g) New With {Key g.contact}).
            Select(Function(group) New With {.customer = group.Key.Contact,
                                            .Eur = group.SelectMany(Function(x) x.items).Sum(Function(y) DTOAmt.import(y.pending, y.preuNet, y.dto).Eur),
                                            .FchDeliveryMin = group.Min(Function(m) m.fch),
                                            .FchDeliveryMax = group.Max(Function(n) n.fch)})

        Dim oFilteredValues As List(Of DTOPurchaseOrder) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem In query
            Dim oControlItem As New ControlItem(oItem.customer, oItem.Eur, oItem.FchDeliveryMin, oItem.FchDeliveryMax)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPurchaseOrder)
        Dim retval As List(Of DTOPurchaseOrder)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values '.FindAll(Function(x) x.Nom.ToLower.Contains(_Filter.ToLower))
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


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchDeliveryMin)
            .HeaderText = "Des de"
            .DataPropertyName = "FchFirst"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchDeliveryMax)
            .HeaderText = "Fins"
            .DataPropertyName = "FchLast"
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


    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Async Function SetContextMenu() As Task
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oContactMenu = Await FEB.ContactMenu.Find(exs, oControlItem.Source)
            Dim oMenu_Contact As New Menu_Contact(oControlItem.Source, oContactMenu)
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Function

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            'Dim oSelectedValue As DTOPurchaseOrder = CurrentControlItem.Source
            'Select Case _SelectionMode
            ' Case DTO.Defaults.SelectionModes.Browse
            ' Dim oFrm As New Frm_Template(oSelectedValue)
            ' AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            ' oFrm.Show()
            ' Case DTO.Defaults.SelectionModes.Selection
            ' RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            ' End Select

        End If
    End Sub

    Private Async Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            Await SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Property Source As DTOContact

        Property Nom As String
        Property Eur As Decimal
        Property FchDeliveryMin As Date
        Property FchDeliveryMax As Date

        Public Sub New(oContact As DTOContact, DcEur As Decimal, DtFchDeliveryMin As Date, DtFchDeliveryMax As Date)
            MyBase.New()
            _Source = oContact
            _Nom = oContact.FullNom
            _Eur = DcEur
            _FchDeliveryMin = DtFchDeliveryMin
            _FchDeliveryMax = DtFchDeliveryMax
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

