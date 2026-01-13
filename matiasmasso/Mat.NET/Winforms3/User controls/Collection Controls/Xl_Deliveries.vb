Public Class Xl_Deliveries
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTODelivery)
    Private _Purpose As Purposes
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse
    Private _Filter As String
    Private _FilterList As List(Of Integer)
    Private _LastMouseDownRectangle As Rectangle
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRemove(sender As Object, e As MatEventArgs)
    Public Event RequestToInvoice(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Enum Purposes
        MultipleCustomers
        SingleCustomer
        Transmisio
        Facturacio
        PendentsDeCobro
        PortsAltres
    End Enum

    Private Enum Cols
        Id
        Fch
        IcoEtq
        Clx
        Eur
        Cash
        Facturable
        Transm
        Fra
        Usr
        Trp
    End Enum

    Public Shadows Sub Load(values As List(Of DTODelivery), oPurpose As Purposes, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Values = values
        _Purpose = oPurpose
        _SelectionMode = oSelectionMode

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Public ReadOnly Property Values() As List(Of DTODelivery)
        Get
            Return _Values
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTODelivery) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTODelivery In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        If _ControlItems.All(Function(x) x.Usr = "") Then MyBase.Columns(Cols.Usr).Visible = False
        If _ControlItems.All(Function(x) x.Trp = "") Then MyBase.Columns(Cols.Trp).Visible = False
        If _ControlItems.All(Function(x) x.Source.Facturable = True) Then MyBase.Columns(Cols.Facturable).Visible = False
        If _ControlItems.All(Function(x) x.Source.CashCod = DTOCustomer.CashCodes.credit) Then MyBase.Columns(Cols.Cash).Visible = False

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery)
        If _Filter = "" And _FilterList Is Nothing Then
            retval = _Values
        Else
            If _FilterList IsNot Nothing Then
                retval = _Values.FindAll(Function(x) _FilterList.Any(Function(y) y = x.Id)).ToList
            ElseIf IsNumeric(_Filter) Then
                retval = _Values.FindAll(Function(x) Not String.IsNullOrEmpty(x.Contact.Nom) AndAlso x.Contact.Nom.ToLower.Contains(_Filter.ToLower) Or x.Id.ToString.Contains(_Filter) Or Not String.IsNullOrEmpty(x.Contact.FullNom) AndAlso x.Contact.FullNom.ToLower.Contains(_Filter.ToLower))
            Else
                retval = _Values.FindAll(Function(x) Not String.IsNullOrEmpty(x.Contact.Nom) AndAlso x.Contact.Nom.ToLower.Contains(_Filter.ToLower) Or Not String.IsNullOrEmpty(x.Contact.FullNom) AndAlso x.Contact.FullNom.ToLower.Contains(_Filter.ToLower))
            End If
        End If

        Return retval
    End Function


    Public Property FilterList As List(Of Integer)
        Get
            Return _FilterList
        End Get
        Set(value As List(Of Integer))
            _FilterList = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

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

    Public ReadOnly Property Value As DTODelivery
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTODelivery = oControlItem.Source
            Return retval
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Dim retval As Integer = 0
            If _ControlItems IsNot Nothing Then
                retval = _ControlItems.Count
            End If
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        Dim sameCustomer As Boolean = _Values.All(Function(x) x.Contact.Equals(_Values.First.Contact))

        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Id)
            .HeaderText = "Albará"
            .DataPropertyName = "Id"
            .Width = 45
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#"
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .Width = 65
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "dd/MM/yy"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.IcoEtq), DataGridViewImageColumn)
            If _Purpose <> Purposes.Transmisio Then .Visible = False
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Clx)
            If _Purpose = Purposes.SingleCustomer Then .Visible = False
            .HeaderText = "client"
            .DataPropertyName = "Clx"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Cash), DataGridViewImageColumn)
            If _Purpose = Purposes.Transmisio Then .Visible = False
            If _Purpose = Purposes.PendentsDeCobro Then .Visible = False
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Facturable), DataGridViewImageColumn)
            If _Purpose = Purposes.Transmisio Then .Visible = False
            If _Purpose = Purposes.PendentsDeCobro Then .Visible = False
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Transm)
            If _Purpose = Purposes.Transmisio Then .Visible = False
            If _Purpose = Purposes.PendentsDeCobro Then .Visible = False
            If _Purpose = Purposes.PortsAltres Then .Visible = False
            .HeaderText = "Transm"
            .DataPropertyName = "Transm"
            .Width = 45
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fra)
            If _Purpose = Purposes.Facturacio Then .Visible = False
            If _Purpose = Purposes.PendentsDeCobro Then .Visible = False
            If _Purpose = Purposes.PortsAltres Then .Visible = False
            .HeaderText = "Factura"
            .DataPropertyName = "Fra"
            .Width = 45
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Usr)
            If _Purpose = Purposes.Transmisio Then .Visible = False
            .HeaderText = "Usuari"
            .DataPropertyName = "Usr"
            Select Case _Purpose
                Case Purposes.SingleCustomer
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Case Else
                    .Width = 100
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End Select
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Trp)
            If _Purpose = Purposes.Transmisio Then .Visible = False
            If _Purpose = Purposes.PendentsDeCobro Then .Visible = False
            If _Purpose = Purposes.PortsAltres Then .Visible = False
            .HeaderText = "Transport"
            .DataPropertyName = "Trp"
            If sameCustomer Then
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Else
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End If
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
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

    Private Function SelectedItems() As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)
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
            Dim oMenu_Delivery As New Menu_Delivery(SelectedItems)
            AddHandler oMenu_Delivery.AfterUpdate, AddressOf RefreshRequest
            AddHandler oMenu_Delivery.RequestToToggleProgressBar, AddressOf ToggleProgressBarRequest
            oContextMenu.Items.AddRange(oMenu_Delivery.Range)
            oContextMenu.Items.Add("-")
        End If

        Select Case _Purpose
            Case Purposes.Transmisio
                oContextMenu.Items.Add("retirar de la transmisio", Nothing, AddressOf Do_Remove)
            Case Purposes.Facturacio
                oContextMenu.Items.Add("facturar", Nothing, AddressOf Do_Facturar)
            Case Else
                oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        End Select
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)


        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Facturar()
        RaiseEvent requestToInvoice(Me, New MatEventArgs(SelectedItems))
    End Sub
    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Remove()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oDelivery As DTODelivery = oControlItem.Source
        RaiseEvent RequestToRemove(Me, New MatEventArgs(oDelivery))
    End Sub


    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oSelectedValue As DTODelivery = CurrentControlItem.Source
        Select Case _SelectionMode
            Case DTO.Defaults.SelectionModes.Browse
                Dim oDelivery As DTODelivery = oSelectedValue
                Dim oContact As DTOContact = oDelivery.Contact
                Dim exs As New List(Of Exception)
                If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oContact, DTOAlbBloqueig.Codis.ALB, exs) Then
                    Dim oFrm As New Frm_AlbNew2(oDelivery)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If
            Case DTO.Defaults.SelectionModes.Selection
                RaiseEvent onItemSelected(Me, New MatEventArgs(oSelectedValue))
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_Deliveries_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem IsNot Nothing Then
            'If oControlItem.Source.Id = 17593 Then Stop

            Select Case e.ColumnIndex
                Case Cols.IcoEtq
                    Dim oDelivery As DTODelivery = oControlItem.Source
                    If oDelivery.EtiquetesTransport IsNot Nothing Then
                        e.Value = My.Resources.label16
                    End If
                Case Cols.Facturable
                    'If oControlItem.Source.Id = 17593 Then Stop
                    If MyBase.Columns(Cols.Facturable).Visible Then
                        Dim oDelivery As DTODelivery = oControlItem.Source
                        If oDelivery.Facturable = False Then
                            e.Value = My.Resources.NoPark
                        End If
                    End If
                Case Cols.Cash
                    If MyBase.Columns(Cols.Cash).Visible Then
                        Dim oDelivery As DTODelivery = oControlItem.Source
                        Select Case oDelivery.CashCod
                            Case DTOCustomer.CashCodes.credit
                            Case DTOCustomer.CashCodes.reembols
                                e.Value = My.Resources.DollarBlue
                            Case DTOCustomer.CashCodes.transferenciaPrevia
                                e.Value = My.Resources.DollarOrange2
                            Case DTOCustomer.CashCodes.visa
                                e.Value = My.Resources.visa1
                        End Select
                    End If
            End Select
        End If
    End Sub

    Private Sub Xl_Transmisions_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Select Case e.ColumnIndex
                Case Cols.IcoEtq
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oDelivery As DTODelivery = oControlItem.Source
                    If oDelivery.EtiquetesTransport IsNot Nothing Then
                        e.ToolTipText = "albarà amb etiquetes de transport personalitzades"
                    End If
                Case Cols.Facturable
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    If oControlItem.Source.Facturable = False Then
                        e.ToolTipText = "no facturable"
                    End If
                Case Cols.Cash
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Select Case oControlItem.Source.CashCod
                        Case DTOCustomer.CashCodes.Visa
                            e.ToolTipText = "tarjeta de credit"
                        Case DTOCustomer.CashCodes.TransferenciaPrevia
                            e.ToolTipText = "transferencia previa"
                        Case DTOCustomer.CashCodes.Diposit
                            e.ToolTipText = "diposit"
                    End Select
            End Select
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem IsNot Nothing Then
            Dim oDelivery As DTODelivery = oControlItem.Source
            Select Case oDelivery.Cod
                Case DTOPurchaseOrder.Codis.Client
                    Dim DblEur As Decimal = oRow.Cells(Cols.Eur).Value
                    If DblEur < 0 Then
                        UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.LightSalmon)
                    Else
                        oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
                    End If
                Case DTOPurchaseOrder.Codis.Proveidor
                    UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.GreenYellow)
                'oRow.DefaultCellStyle.BackColor = Color.GreenYellow
                Case DTOPurchaseOrder.Codis.Reparacio
                    UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.Pink)
                'oRow.DefaultCellStyle.BackColor = Color.LightPink
                Case DTOPurchaseOrder.Codis.Traspas
                    UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Me.BackColor)
                    'oRow.DefaultCellStyle.BackColor = Me.BackColor
                Case Else
                    oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
            End Select
        End If
    End Sub

#Region "Drag"
    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                _LastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not _LastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(_LastMouseDownRectangle.X, _LastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    Dim oDeliveries As List(Of DTODelivery) = SelectedItems()
                    sender.DoDragDrop(oDeliveries, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub
#End Region

    Protected Class ControlItem
        Property Source As DTODelivery

        Property Id As Integer
        Property Fch As Date
        Property Clx As String
        Property Eur As Decimal
        Property Transm As Integer
        Property Fra As Integer
        Property Usr As String
        Property Trp As String

        Public Sub New(value As DTODelivery)
            MyBase.New()
            _Source = value
            With value
                _Id = .Id
                _Fch = .Fch
                _Clx = .Contact.Nom
                If _Clx = "" Then _Clx = .Contact.FullNom
                If .Liquid IsNot Nothing Then
                    _Eur = .Liquid.Eur
                End If
                If .Transmisio IsNot Nothing Then
                    _Transm = .Transmisio.Id
                End If
                If .Invoice IsNot Nothing Then
                    _Fra = .Invoice.Num
                End If
                If .UsrLog IsNot Nothing AndAlso .UsrLog.UsrCreated IsNot Nothing Then
                    _Usr = .UsrLog.UsrCreated.Nom
                End If
                Select Case .PortsCod
                    Case DTOCustomer.PortsCodes.reculliran
                        _Trp = "(reculliran)"
                    Case DTOCustomer.PortsCodes.entregatEnMa
                        _Trp = "(entregat en ma)"
                    Case DTOCustomer.PortsCodes.altres
                        _Trp = "(altres)"
                    Case Else
                        If .Transportista IsNot Nothing Then
                            _Trp = .Transportista.Nom
                        End If
                End Select
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


