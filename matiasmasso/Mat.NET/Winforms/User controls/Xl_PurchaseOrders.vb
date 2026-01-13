Public Class Xl_PurchaseOrders
    Inherits _Xl_ReadOnlyDatagridview

    Private _values As List(Of DTOPurchaseOrder)
    Private _Mode As Modes
    Private _Codi As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.Client
    Private _DefaultValue As DTOPurchaseOrder
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

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

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrder), Optional oDefaultValue As DTOPurchaseOrder = Nothing, Optional oMode As Modes = Modes.MultipleCustomers, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _values = values
        If _values.Count > 0 Then
            _Codi = _values.First.Cod
        End If

        _Mode = oMode
        _SelectionMode = oSelectionMode

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If


        Refresca()
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

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOPurchaseOrder) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOPurchaseOrder In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems

        If _DefaultValue Is Nothing Then
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        Else
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.id)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPurchaseOrder)
        Dim retval As List(Of DTOPurchaseOrder)
        If _Filter = "" Then
            retval = _values
        Else
            retval = _values.FindAll(Function(x) x.Contact.FullNom.ToLower.Contains(_Filter.ToLower) Or
                                         x.Concept.ToLower.Contains(_Filter.ToLower) Or
                                         x.Num.ToString = _Filter)
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _values IsNot Nothing Then Refresca()
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
            Dim retval As DTOPurchaseOrder = oControlItem.Source
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
        With MyBase.Columns(Cols.id)
            .HeaderText = "Numero"
            .DataPropertyName = "Id"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.pdf), DataGridViewImageColumn)
            .DataPropertyName = "pdf"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.clinom)
            Select Case _Mode
                Case Modes.MultipleCustomers
                    .HeaderText = IIf(_Codi = DTOPurchaseOrder.Codis.Client, "Client", "Proveidor")
                    .DataPropertyName = "clinom"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Case Modes.SingleCustomer
                    .Visible = False
            End Select
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.concept)
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
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.src), DataGridViewImageColumn)
            .DataPropertyName = "src"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.user)
            .HeaderText = "Usuari"
            .DataPropertyName = "user"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
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

    Private Function SelectedItems() As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 AndAlso _ControlItems.Count > 0 Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval.Add(CurrentControlItem.Source)
            End If
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
        Dim oOrders As List(Of DTOPurchaseOrder) = SelectedItems()
        If oControlItem IsNot Nothing Then
            'Dim oOrder As DTOPurchaseOrder = SelectedItems.First
            'Dim oOrders As List(Of DTOPurchaseOrder) = SelectedItems()

            Dim oMenu_Pdc As New Menu_Pdc(oOrders)
            AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pdc.Range)

            'Dim oMenu_PurchaseOrder As New Menu_PurchaseOrder(oOrder)
            'AddHandler oMenu_PurchaseOrder.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_PurchaseOrder.Range)

            oContextMenu.Items.Add("-")
        End If

        If oOrders.Count > 1 Then
            Dim tot As Decimal = oOrders.Sum(Function(x) x.SumaDeImports.Eur)
            oContextMenu.Items.Add(String.Format("{0} seleccionats per {1:#,###0.00 €}", oOrders.Count, tot))
        End If

        oContextMenu.Items.Add("Excel comandes", Nothing, AddressOf Do_ExcelPdcs)
        oContextMenu.Items.Add("Excel linies", Nothing, AddressOf Do_ExcelLinies)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf onRefreshRequest)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub onRefreshRequest()
        MyBase.RefreshRequest(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_ExcelLinies()
        Dim exs As New List(Of Exception)
        Dim oSheet = FEB2.PurchaseOrders.ExcelLinies(exs, _values)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_ExcelPdcs()
        Dim oSheet = FEB2.PurchaseOrders.ExcelPdcs(Me.SelectedItems)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPurchaseOrder = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim exs As New List(Of Exception)
                    If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oSelectedValue.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder(oSelectedValue)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.src
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oPurchaseOrder As DTOPurchaseOrder = oControlItem.Source
                e.Value = IconHelper.PurchaseSrcIcon(oPurchaseOrder.source)
            Case Cols.pdf
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oPurchaseOrder As DTOPurchaseOrder = oControlItem.Source
                e.Value = IIf(oPurchaseOrder.DocFile Is Nothing, My.Resources.empty, My.Resources.pdf)
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oPurchaseOrder As DTOPurchaseOrder = oControlItem.Source
        If oPurchaseOrder.IsOpenOrder Then
            oRow.DefaultCellStyle.BackColor = Color.Yellow
            'PaintGradientRowBackGround(e, Color.Yellow)
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle(
            0, e.RowBounds.Top,
            MyBase.Columns.GetColumnsWidth(
            DataGridViewElementStates.Visible) -
            MyBase.HorizontalScrollingOffset + 1,
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

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub




    Protected Class ControlItem
        Property Source As DTOPurchaseOrder

        Property id As Integer
        Property fch As Date
        Property clinom As String
        Property amt As String
        Property concept As String
        Property user As String

        Public Sub New(oPurchaseOrder As DTOPurchaseOrder)
            MyBase.New()
            _Source = oPurchaseOrder
            With oPurchaseOrder
                _id = .Num
                _fch = .Fch
                _clinom = .Contact.FullNom
                _amt = DTOAmt.CurFormatted(.SumaDeImports)

                'provisional ---------------------------------
                'Dim oEur = DTOCur.Eur()
                'If .SumaDeImports.Cur.unEquals(oEur) Then Stop

                _concept = .Concept
                _user = DTOUser.NicknameOrElse(.UsrLog.usrCreated)
            End With
        End Sub


    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


