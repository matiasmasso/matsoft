Public Class Xl_PurchaseOrdersOld
    Private _values As List(Of DTOPurchaseOrder)
    Private _DefaultValue As DTOPurchaseOrder
    Private _Mode As Modes
    Private _filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Public Enum Modes
        MultipleCustomers
        SingleCustomer
    End Enum


    Private Enum Cols
        id
        fch
        pdf
        clinom
        amt
        concept
        src
        user
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrder), oMode As Modes, Optional oDefaultValue As DTOPurchaseOrder = Nothing)
        _values = values
        _DefaultValue = oDefaultValue
        _Mode = oMode

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        refresca()
    End Sub

    Public ReadOnly Property Count As Integer
        Get
            Dim retval As Integer = 0
            If _ControlItems IsNot Nothing Then
                retval = _ControlItems.Count
            End If
            Return retval
        End Get
    End Property

    Public Property Filter As String
        Get
            Return _filter
        End Get
        Set(value As String)
            _filter = value
            If _AllowEvents Then refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _filter > "" Then
            _filter = ""
            refresca()
        End If
    End Sub

    Private Sub refresca()
        Dim iCurrentRowIndex As Integer
        Dim iTopRow As Integer = DataGridView1.FirstDisplayedScrollingRowIndex
        Dim oCurrentRow As DataGridViewRow = DataGridView1.CurrentRow
        If oCurrentRow IsNot Nothing Then
            iCurrentRowIndex = oCurrentRow.Index
        End If

        _ControlItems = New ControlItems
        For Each oItem As DTOPurchaseOrder In _values
            Dim blProcede As Boolean
            If _filter = "" Then
                blProcede = True
            Else
                blProcede = (oItem.Concept.ToUpper.Contains(_filter.ToUpper) Or oItem.Customer.FullNom.ToUpper.Contains(_filter.ToUpper))
            End If
            If blProcede Then
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            End If
        Next

        _AllowEvents = False
        DataGridView1.DataSource = _ControlItems

        If iCurrentRowIndex > 0 Then
            If iCurrentRowIndex > _ControlItems.Count - 1 Then
                iCurrentRowIndex = _ControlItems.Count
            End If
            DataGridView1.CurrentCell = DataGridView1.Rows(iCurrentRowIndex).Cells(Cols.clinom)
            DataGridView1.FirstDisplayedScrollingRowIndex = iTopRow
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Sub SetProperties()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.id)
                .HeaderText = "Numero"
                .DataPropertyName = "Id"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.pdf), DataGridViewImageColumn)
                .DataPropertyName = "pdf"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.clinom)
                Select Case _Mode
                    Case Modes.MultipleCustomers
                        .HeaderText = "Client"
                        .DataPropertyName = "clinom"
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    Case Modes.SingleCustomer
                        .Visible = False
                End Select
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.amt)
                .HeaderText = "Import"
                .DataPropertyName = "Amt"
                .Width = 70
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.concept)
                .HeaderText = "Concepte"
                .DataPropertyName = "concept"
                Select Case _Mode
                    Case Modes.MultipleCustomers
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                        .Width = 200
                    Case Modes.SingleCustomer
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End Select
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.src), DataGridViewImageColumn)
                .DataPropertyName = "src"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.user)
                .HeaderText = "Usuari"
                .DataPropertyName = "user"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With
        End With
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.src
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oPurchaseOrder As DTOPurchaseOrder = oControlItem.Source
                e.Value = IconHelper.PurchaseSrcIcon(oPurchaseOrder.source)
            Case Cols.pdf
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oPurchaseOrder As DTOPurchaseOrder = oControlItem.Source
                e.Value = IIf(oPurchaseOrder.DocFile Is Nothing, My.Resources.empty, My.Resources.pdf)
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oPurchaseOrder As DTOPurchaseOrder = oControlItem.Source
        If oPurchaseOrder.IsOpenOrder Then
            PaintGradientRowBackGround(e, Color.Yellow)
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle(
            0, e.RowBounds.Top,
            Me.DataGridView1.Columns.GetColumnsWidth(
            DataGridViewElementStates.Visible) -
            Me.DataGridView1.HorizontalScrollingOffset + 1,
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush(
        rowBounds,
        oColor,
        oBgColor,
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            'Dim oOrder As DTOPurchaseOrder = SelectedItems.First
            Dim oOrders As List(Of DTOPurchaseOrder) = SelectedItems()

            Dim oMenu_Pdc As New Menu_Pdc(oOrders)
            AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pdc.Range)

            'Dim oMenu_PurchaseOrder As New Menu_PurchaseOrder(oOrder)
            'AddHandler oMenu_PurchaseOrder.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_PurchaseOrder.Range)

            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOPurchaseOrder = CurrentControlItem.Source

        Dim exs As New List(Of Exception)
        If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oSelectedValue.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
            Dim oFrm As New Frm_PurchaseOrder(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub



    Protected Class ControlItem
        Property Source As DTOPurchaseOrder

        Property id As Integer
        Property fch As Date
        Property clinom As String
        Property amt As Decimal
        Property concept As String
        Property user As String

        Public Sub New(oPurchaseOrder As DTOPurchaseOrder)
            MyBase.New()
            _Source = oPurchaseOrder
            With oPurchaseOrder
                _id = .Num
                _fch = .Fch
                _clinom = .Customer.FullNom
                _amt = .SumaDeImports.Eur
                _concept = .Concept
                _user = DTOUser.NicknameOrElse(.UsrLog.UsrCreated)
            End With
        End Sub


    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

