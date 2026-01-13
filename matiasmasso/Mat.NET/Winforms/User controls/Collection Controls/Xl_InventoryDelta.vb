Public Class Xl_InventoryDelta

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTODeliveryItem)
    Private _Mode As Modes
    Private _Lang As DTOLang
    Private _DefaultValue As DTODeliveryItem
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Enum Modes
        NotSet
        Months
        Days
        Deliveries
        Items
    End Enum

    Private Enum Cols
        nom
        qty
        price
        dto
        entrades
        sortides
        deltaGoods
        vendes
        result
        margin
    End Enum

    Public Shadows Sub Load(oMode As Modes, values As List(Of DTODeliveryItem), Optional oDefaultValue As DTODeliveryItem = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Mode = oMode
        _Lang = Current.Session.Lang
        _Values = values
        _SelectionMode = oSelectionMode

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False

        Select Case _Mode
            Case Modes.Items
                If _Values IsNot Nothing Then
                    If _Values.First.Delivery.Cod = DTOPurchaseOrder.Codis.Proveidor Then
                        MyBase.Columns(Cols.entrades).Visible = True
                        MyBase.Columns(Cols.sortides).Visible = False
                    Else
                        MyBase.Columns(Cols.entrades).Visible = False
                        MyBase.Columns(Cols.sortides).Visible = True
                    End If
                End If
            Case Else
                MyBase.Columns(Cols.qty).Visible = False
                MyBase.Columns(Cols.price).Visible = False
                MyBase.Columns(Cols.dto).Visible = False
        End Select

        _ControlItems = New ControlItems

        If _Values.Count > 0 Then
            Dim oTotalItem As New ControlItem(_Mode, _Values, _Lang)
            _ControlItems.Add(oTotalItem)

            Dim query As Object = DataSource()
            For Each oItem In query
                Dim oControlItem As New ControlItem(_Mode, oItem, _Lang)
                oTotalItem.Entrades += oItem.Entrades
                oTotalItem.Sortides += oItem.Sortides
                oTotalItem.Vendes += oItem.Vendes
                _ControlItems.Add(oControlItem)
            Next

            Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
            MyBase.DataSource = _ControlItems
            If oCell Is Nothing Then
                If _ControlItems.Count > 0 Then
                    MyBase.CurrentCell = MyBase.Rows(1).Cells(Cols.nom)
                End If
            Else
                UIHelper.SetDataGridviewCurrentCell(Me, oCell)
            End If

            SetContextMenu()

        End If
        _AllowEvents = True
    End Sub

    Private Shadows Function DataSource() As Object
        Dim retval As Object = Nothing
        Select Case _Mode
            Case Modes.Months
                retval = _Values.
                   GroupBy(Function(g) New With {Key g.Delivery.Fch.Month}).
                   Select(Function(group) New With {
                      .Month = group.Key.Month,
                      .Entrades = group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Proveidor, DTOAmt.Import(a.Qty, a.Price, a.Dto).Eur, 0)),
                      .Sortides = group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Client, DTOAmt.Import(a.Qty, DTOAmt.Factory(a.Pmc), 0).Eur, 0)),
                      .Vendes = group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Client, DTOAmt.Import(a.Qty, a.Price, a.Dto).Eur, 0))
                      })
            Case Modes.Days
                retval = _Values.
                   GroupBy(Function(g) New With {Key g.Delivery.Fch}).
                   Select(Function(group) New With {
                      .Fch = group.Key.Fch,
                      .Entrades = group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Proveidor, DTOAmt.Import(a.Qty, a.Price, a.Dto).Eur, 0)),
                      .Sortides = group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Client, DTOAmt.Import(a.Qty, DTOAmt.Factory(a.Pmc), 0).Eur, 0)),
                      .Vendes = group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Client, DTOAmt.Import(a.Qty, a.Price, a.Dto).Eur, 0))
                      })
            Case Modes.Deliveries
                retval = _Values.
                   GroupBy(Function(g) New With {Key g.Delivery.Guid, g.Delivery.Id, g.Delivery.Customer.FullNom, g.Delivery.Cod}).
                   Select(Function(group) New With {
                      .Guid = group.Key.Guid,
                      .Delivery = group.Key.Id,
                      .Customer = group.Key.FullNom,
                      .Cod = group.Key.Cod,
                      .Entrades = group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Proveidor, DTOAmt.Import(a.Qty, a.Price, a.Dto).Eur, 0)),
                      .Sortides = group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Client, DTOAmt.Import(a.Qty, DTOAmt.Factory(a.Pmc), 0).Eur, 0)),
                      .Vendes = group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Client, DTOAmt.Import(a.Qty, a.Price, a.Dto).Eur, 0))
                      })
            Case Modes.Items
                retval = _Values.
                   GroupBy(Function(g) New With {Key g.Sku.Guid, g.Sku.Nom, g.Qty, g.Price.Eur, g.Dto, g.Delivery.Cod}).
                   Select(Function(group) New With {
                      .Guid = group.Key.Guid,
                      .Nom = group.Key.Nom,
                      .Qty = group.Key.Qty,
                      .Eur = group.Key.Eur,
                      .Dto = group.Key.Dto,
                      .Cod = group.Key.Cod,
                      .Entrades = CDec(group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Proveidor, DTOAmt.Import(a.Qty, a.Price, a.Dto).Eur, 0))),
                      .Sortides = CDec(group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Client, DTOAmt.Import(a.Qty, DTOAmt.Factory(a.Pmc), 0).Eur, 0))),
                      .Vendes = CDec(group.Sum(Function(a) IIf(a.Delivery.Cod = DTOPurchaseOrder.Codis.Client, DTOAmt.Import(a.Qty, a.Price, a.Dto).Eur, 0)))
                      })
        End Select
        Return retval
    End Function

    Public ReadOnly Property Value As Object
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As Object = Nothing
            If oControlItem IsNot Nothing Then retval = oControlItem.Source
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
        With MyBase.Columns(Cols.nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.qty)
            .HeaderText = "Quant"
            .DataPropertyName = "Qty"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.price)
            .HeaderText = "Preu"
            .DataPropertyName = "Price"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.dto)
            .HeaderText = "Dte"
            .DataPropertyName = "Dto"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.entrades)
            .HeaderText = "Entrades"
            .DataPropertyName = "Entrades"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.sortides)
            .HeaderText = "sortides"
            .DataPropertyName = "sortides"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.deltaGoods)
            .HeaderText = "incr.inventari"
            .DataPropertyName = "deltaGoods"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.vendes)
            .HeaderText = "vendes"
            .DataPropertyName = "vendes"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.result)
            .HeaderText = "Resultat"
            .DataPropertyName = "Result"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.margin)
            .HeaderText = "Marge"
            .DataPropertyName = "Margin"
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
                Case Modes.Deliveries
                    Dim oGuid As Guid = CurrentControlItem.Source
                    Dim oDelivery As New DTODelivery(oGuid)
                    Dim oDeliveries As New List(Of DTODelivery)
                    oDeliveries.Add(oDelivery)
                    Dim oMenu_Delivery As New Menu_Delivery(oDeliveries)
                    oContextMenu.Items.AddRange(oMenu_Delivery.Range)
                Case Modes.Items
                    Dim oGuid As Guid = CurrentControlItem.Source
                    Dim oSku As New DTOProductSku(oGuid)
                    Dim oMenu_Sku As New Menu_ProductSku(oSku)
                    oContextMenu.Items.AddRange(oMenu_Sku.Range)
            End Select

        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim exs As New List(Of Exception)
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Select Case _Mode
                Case Modes.Deliveries
                    Dim oGuid As Guid = CurrentControlItem.Source
                    Dim oDelivery = Await FEB2.Delivery.Find(oGuid, exs)
                    If exs.Count = 0 Then
                        Dim oCustomer As DTOCustomer = oDelivery.Contact
                        If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
                            Dim oFrm As New Frm_AlbNew2(oDelivery)
                            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            oFrm.Show()
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If

                Case Modes.Items
                    Dim oGuid As Guid = CurrentControlItem.Source
                    Dim oSku As New DTOProductSku(oGuid)
                    Dim oFrm As New Frm_Art(oSku)
                    oFrm.Show()
            End Select

        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_InventoryDelta_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        If e.RowIndex = 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            oRow.DefaultCellStyle.BackColor = Color.LightBlue
        End If
    End Sub

    Protected Class ControlItem
        Property Source As Object

        Property Nom As String
        Property Qty As Integer
        Property Price As Decimal
        Property Dto As Decimal
        Property Entrades As Decimal
        Property Sortides As Decimal
        Property Vendes As Decimal
        ReadOnly Property DeltaGoods As Decimal
            Get
                Return _Entrades - _Sortides
            End Get
        End Property
        ReadOnly Property Result As Decimal
            Get
                Return _Vendes - _Sortides
            End Get
        End Property
        ReadOnly Property Margin As Decimal
            Get
                Dim retval As Decimal
                If _Sortides <> 0 Then
                    retval = 100 * (_Vendes - _Sortides) / _Sortides
                End If
                Return retval
            End Get
        End Property

        Public Sub New(oMode As Modes, item As Object, oLang As DTOLang)
            MyBase.New()
            Select Case oMode
                Case Modes.Months
                    _Source = item.Month
                    _Nom = Current.Session.Lang.MesAbr(item.Month)
                Case Modes.Days
                    Dim DtFch As Date = item.fch
                    _Source = DtFch
                    _Nom = String.Format("{0:00} {1}", DtFch.Day, oLang.WeekDay(DtFch))
                Case Modes.Deliveries
                    _Source = item.guid
                    _Nom = String.Format("{0:00000} {1}", item.delivery, item.customer)
                Case Modes.Items
                    _Source = item.guid
                    _Nom = item.nom
                    _Qty = item.qty
                    _Price = item.eur
                    _Dto = item.dto
            End Select
            _Entrades = item.Entrades
            _Sortides = item.Sortides
            _Vendes = item.Vendes
        End Sub

        Public Sub New(oMode As Modes, values As List(Of DTODeliveryItem), oLang As DTOLang)
            MyBase.New()
            Select Case oMode
                Case Modes.Months
                    Dim DtFch As Date = values.First.Delivery.Fch
                    _Nom = String.Format("total {0}", DtFch.Year)
                Case Modes.Days
                    Dim DtFch As Date = values.First.Delivery.Fch
                    _Nom = String.Format("total {0} {1}", oLang.Mes(DtFch.Month), DtFch.Year)
                Case Modes.Deliveries
                    Dim DtFch As Date = values.First.Delivery.Fch
                    _Nom = String.Format("total {0} {1:dd/MM/yy}", oLang.WeekDay(DtFch), DtFch)
                Case Modes.Items
                    Dim oDelivery As DTODelivery = values.First.Delivery
                    _Nom = String.Format("total albará {0} de {1}", oDelivery.Id, oDelivery.Customer.FullNom)
            End Select
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


