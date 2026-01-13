Public Class Xl_Proveidors_Pnds
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPnd)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToExcel(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Txt
        Amt
        Fra
        Fch
        Obs
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPnd))
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
        Dim oFilteredValues As List(Of DTOPnd) = FilteredValues()
        Dim DtVto As Date = Nothing
        _ControlItems = New ControlItems
        For Each oItem As DTOPnd In oFilteredValues
            If oItem.Vto <> DtVto Then
                DtVto = oItem.Vto
                Dim DcEur As Decimal = _Values.Sum(Function(x) x.Amt.Eur)
                If _ControlItems.Count > 0 Then
                    _ControlItems.Add(New ControlItem)
                End If
                _ControlItems.Add(New ControlItem(DtVto, DcEur))
            End If
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPnd)
        Dim retval As List(Of DTOPnd)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Contact.FullNom.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOPnd
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPnd = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Txt)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .SortMode = DataGridViewColumnSortMode.NotSortable
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
            .SortMode = DataGridViewColumnSortMode.NotSortable
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fra)
            .HeaderText = "Factura"
            .DataPropertyName = "Fra"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .Width = 90
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
            .SortMode = DataGridViewColumnSortMode.NotSortable
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Obs)
            .HeaderText = "Observacions"
            .DataPropertyName = "Obs"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .SortMode = DataGridViewColumnSortMode.NotSortable
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

    Private Function SelectedItems() As List(Of DTOPnd)
        Dim retval As New List(Of DTOPnd)
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
            If oControlItem.Lincod = ControlItem.LinCods.Item Then
                Dim oMenu_Pnd As New Menu_Pnd(SelectedItems.First)
                AddHandler oMenu_Pnd.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Pnd.Range)
                oContextMenu.Items.Add("-")
            End If
        End If
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Excel()
        RaiseEvent RequestToExcel(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPnd = CurrentControlItem.Source
            Dim oFrm As New Frm_Contact_Pnd(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlitem As ControlItem = oRow.DataBoundItem
        Select Case oControlitem.Lincod
            Case ControlItem.LinCods.Vto
                UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.AliceBlue)
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.White
        End Select
    End Sub


    Protected Class ControlItem
        Property Source As DTOPnd

        Property Lincod As LinCods
        Property Txt As String
        Property Amt As Decimal
        Property Fra As String
        Property Fch As Nullable(Of Date)
        Property Obs As String

        Public Enum LinCods
            Blank
            Vto
            Item
        End Enum

        Public Sub New()
            MyBase.New()
            _Lincod = LinCods.Blank
        End Sub

        Public Sub New(DtVto As Date, Amt As Decimal)
            MyBase.New()
            _Lincod = LinCods.Vto
            _Txt = String.Format("Vto.{0:dd/MM/yy}", DtVto)
            _Amt = Amt
        End Sub

        Public Sub New(value As DTOPnd)
            MyBase.New()
            _Source = value
            With value
                _Lincod = LinCods.Item
                _Txt = .Contact.Nom
                _Fch = .Fch
                _Fra = .FraNum
                _Amt = .Amt.Eur
                _Obs = .Fpg
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

