Public Class Xl_Marges
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTODeliveryItem)
    Private _Mode As Modes
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Sales
        Cost
        Mrg
        Bnf
    End Enum

    Public Enum Modes
        NotSet
        Brands
        Categories
        Customers
        Ccx
    End Enum

    Public Shadows Sub Load(values As List(Of DTODeliveryItem), Optional oMode As Modes = Modes.NotSet)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _Mode = oMode
        Refresca()
    End Sub

    Private Sub Refresca()
        Dim exs As New List(Of Exception)
        _AllowEvents = False

        Dim query As IEnumerable(Of ControlItem) = Nothing

        Select Case _Mode
            Case Modes.Brands
                query = _Values.
                         GroupBy(Function(g) New With {Key g.sku.category.brand.Guid, g.sku.category.brand.nom}).
                         Select(Function(group) New ControlItem With {
                            .Guid = group.Key.Guid,
                            .Nom = group.Key.Nom.Tradueix(Current.Session.Lang),
                            .Sales = group.Sum(Function(a) DTOAmt.import(a.qty, a.price, a.dto).Eur),
                            .Cost = group.Sum(Function(c) c.qty * c.pmc)})
            Case Modes.Categories
                query = _Values.
                       GroupBy(Function(g) New With {Key g.sku.category.Guid, g.sku.category.nom}).
                       Select(Function(group) New ControlItem With {
                          .Guid = group.Key.Guid,
                          .Nom = group.Key.Nom.Tradueix(Current.Session.Lang),
                            .Sales = group.Sum(Function(a) DTOAmt.import(a.qty, a.price, a.dto).Eur),
                          .Cost = group.Sum(Function(c) c.qty * c.pmc)})
            Case Modes.Customers
                query = _Values.
                       GroupBy(Function(g) New With {Key g.Delivery.Customer.Guid, g.Delivery.Customer.FullNom}).
                       Select(Function(group) New ControlItem With {
                          .Guid = group.Key.Guid,
                          .Nom = group.Key.FullNom,
                            .Sales = group.Sum(Function(a) DTOAmt.Import(a.Qty, a.Price, a.Dto).Eur),
                          .Cost = group.Sum(Function(c) c.Qty * c.Pmc)})
            Case Modes.Ccx
                query = _Values.
                       GroupBy(Function(g) New With {Key FEB2.Customer.CcxOrMe(exs, g.Delivery.Customer).Guid, FEB2.Customer.CcxOrMe(exs, g.Delivery.Customer).FullNom}).
                       Select(Function(group) New ControlItem With {
                          .Guid = group.Key.Guid,
                          .Nom = group.Key.FullNom,
                            .Sales = group.Sum(Function(a) DTOAmt.Import(a.Qty, a.Price, a.Dto).Eur),
                          .Cost = group.Sum(Function(c) c.Qty * c.Pmc)})
        End Select


        _ControlItems = New ControlItems
        Dim oTotal As New ControlItem
        oTotal.Nom = "totals"
        _ControlItems.Add(oTotal)
        For Each oItem In query
            _ControlItems.Add(oItem)
            oTotal.Sales += oItem.Sales
            oTotal.Cost += oItem.Cost
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub



    Public ReadOnly Property Value As DTODeliveryItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTODeliveryItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowDeliveryItem.DefaultCellStyle.BackColor = Color.Transparent

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
            .HeaderText = "Marca comercial"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sales)
            .HeaderText = "Vendes"
            .DataPropertyName = "Sales"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cost)
            .HeaderText = "Cost"
            .DataPropertyName = "Cost"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Mrg)
            .HeaderText = "Marge"
            .DataPropertyName = "Mrg"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Bnf)
            .HeaderText = "Benefici brut"
            .DataPropertyName = "Bnf"
            .Width = 90
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

    Private Function SelectedItems() As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function SelectedCustomers() As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Dim oCustomer As New DTOCustomer(oControlItem.Guid)
            oCustomer.FullNom = oControlItem.Nom
            retval.Add(oCustomer)
        Next

        If retval.Count = 0 Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim oCustomer As New DTOCustomer(oControlItem.Guid)
            oCustomer.FullNom = oControlItem.Nom
            retval.Add(oCustomer)
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
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Select Case _Mode
                Case Modes.Customers
                    Dim oSelectedCustomers As List(Of DTOCustomer) = SelectedCustomers()
                    If oSelectedCustomers.Count > 0 Then
                        Dim oMenuItem As ToolStripMenuItem = oContextMenu.Items.Add("client")
                        Dim oMenu_Contact As New Menu_Contact(oSelectedCustomers.First)
                        oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
                        oContextMenu.Items.Add(oMenuItem)
                        oContextMenu.Items.Add("Detall", Nothing, AddressOf Do_CustomerDrillDown)
                    End If
            End Select
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_CustomerDrillDown()
        Dim oCustomer As DTOCustomer = SelectedCustomers.First
        Dim oFrm As New Frm_Margins(oCustomer)
        oFrm.Show()
    End Sub

    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTODeliveryItem = CurrentControlItem.Source
            If oSelectedValue IsNot Nothing Then
                Dim oDelivery As DTODelivery = oSelectedValue.Delivery
                Dim oCustomer As DTOCustomer = oDelivery.Contact
                Dim exs As New List(Of Exception)
                If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
                    Dim oFrm As New Frm_AlbNew2(oDelivery)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
            Dim oCurrentControlItem As ControlItem = CurrentControlItem()
            If oCurrentControlItem Is Nothing Then
                RaiseEvent ValueChanged(Me, New MatEventArgs(Guid.Empty))
            Else
                RaiseEvent ValueChanged(Me, New MatEventArgs(oCurrentControlItem.Guid))
            End If
        End If
    End Sub

    Private Sub Xl_Marges_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem IsNot Nothing Then
            If oControlItem.Guid = Nothing Then
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            End If
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTODeliveryItem

        Property Guid As Guid
        Property Nom As String
        Property Sales As Decimal
        Property Cost As Decimal

        Public ReadOnly Property Mrg As Decimal
            Get
                Dim retval As Decimal
                If _Cost <> 0 Then
                    retval = 100 * (Sales - Cost) / Cost
                End If
                Return retval
            End Get
        End Property

        Public ReadOnly Property Bnf As Decimal
            Get
                Dim retval As Decimal = Sales - Cost
                Return retval
            End Get
        End Property


    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

