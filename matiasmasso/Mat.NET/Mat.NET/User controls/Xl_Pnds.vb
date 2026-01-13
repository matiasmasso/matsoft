Public Class Xl_Pnds
    Inherits DataGridView

    Private _ControlItems As ControlItems
    Private _Values As List(Of DTOPnd)
    Private _Filter As String
    Private _ShowDivisas As Boolean
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        IcoStatus
        Vto
        Div
        Eur
        Cta
        Fra
        FraFch
        Txt
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPnd), Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        _Values = values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        MyBase.Columns(Cols.Div).Visible = values.Any(Function(x) x.Amt.Cur.Id <> DTOCur.Ids.EUR)

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

        Dim oFilteredValues As List(Of DTOPnd) = FilteredValues()
        _ControlItems = New ControlItems

        For Each oItem As DTOPnd In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.Columns(Cols.Div).Visible = IsDivisaVisible(oFilteredValues)
        MyBase.DataSource = _ControlItems
        MyBase.CurrentCell = MyBase.FirstDisplayedCell

        SetContextMenu()

        _AllowEvents = True
    End Sub

    Private Function IsDivisaVisible(oFilteredValues As List(Of DTOPnd))
        Dim retval As Boolean
        If oFilteredValues.Count > 0 Then
            Dim FirstCur As DTOCur = oFilteredValues.First.Amt.Cur
            Dim BlDistinctCurs As Boolean = oFilteredValues.Any(Function(x) x.Amt.Cur.Id <> FirstCur.Id)
            retval = BlDistinctCurs Or (FirstCur.Id <> DTOCur.Ids.EUR)
        End If
        Return retval
    End Function

    Private Function FilteredValues() As List(Of DTOPnd)
        Dim retval As New List(Of DTOPnd)
        If _Filter = "" Then
            retval = _Values
        Else
            Dim LCaseFilter As String = _Filter.ToLower
            Dim DcFilter As Decimal = CDec(_Filter)
            retval = _Values.FindAll(Function(x) x.FraNum.ToString.Contains(_Filter) Or x.Amt.Eur = DcFilter)
        End If
        Return retval
    End Function

    Public ReadOnly Property Value As DTOPnd
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPnd = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then
            Dim oCurrentControlItem As ControlItem = CurrentControlItem()
            If oCurrentControlItem IsNot Nothing Then retval.Add(CurrentControlItem)
        End If
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOPnd)
        Dim retval As New List(Of DTOPnd)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then
            Dim oCurrentControlItem As ControlItem = CurrentControlItem()
            If oCurrentControlItem IsNot Nothing Then
                retval.Add(oCurrentControlItem.Source)
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

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With CType(MyBase.Columns(Cols.IcoStatus), DataGridViewImageColumn)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Vto)
            .DataPropertyName = "vto"
            .HeaderText = "venciment"
            .Width = 65
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "dd/MM/yy"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Div)
            .DataPropertyName = "div"
            .HeaderText = "divisas"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .DataPropertyName = "eur"
            .HeaderText = "import"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cta)
            .DataPropertyName = "cta"
            .HeaderText = "compte"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fra)
            .DataPropertyName = "fra"
            .HeaderText = "factura"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FraFch)
            .DataPropertyName = "fch"
            .HeaderText = "data"
            .Width = 65
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "dd/MM/yy"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Txt)
            .DataPropertyName = "obs"
            .HeaderText = "observacions"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Excel()
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Filter = "Excel (*.xlsx)|*.xlsx"
            .DefaultExt = ".xlsx"
            If .ShowDialog Then
                Dim oWorkbook As ClosedXML.Excel.XLWorkbook = BLL.BLLPnds.Excel(_Values, BLL.BLLSession.Current.User.Lang)
                oWorkbook.SaveAs(.FileName)
            End If
        End With
    End Sub

    Private Sub Xl_Pnds_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoStatus
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oPnd As DTOPnd = oControlItem.Source
                Select Case oPnd.Status
                    Case DTOPnd.StatusCod.enCirculacio
                        e.Value = My.Resources.candau
                    Case DTOPnd.StatusCod.enCartera
                        e.Value = My.Resources.candau_edit
                End Select
            Case Cols.Div
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oPnd As DTOPnd = oControlItem.Source
                e.Value = oPnd.Amt.CurFormatted
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oSelectedValue As DTOPnd = CurrentControlItem.Source
        Dim oOldPnd As New Pnd(oSelectedValue.Id)
        Dim oFrm As New Frm_Contact_Pnd(oOldPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            'RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItems As ControlItems = SelectedControlItems()

        If oControlItems.Count > 0 Then
            Dim oMenu_Pnds As New Menu_Pnds(SelectedItems)
            AddHandler oMenu_Pnds.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pnds.Range)
            oContextMenu.Items.Add("-")
            oContextMenu.Items.Add("Excel", My.Resources.Excel_16, AddressOf Do_Excel)
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOPnd

        Property IcoStatus As DTOPnd.StatusCod
        Property Vto As Date
        Property Div As Decimal
        Property Eur As Decimal
        Property Cta As String
        Property Fra As String
        Property Fch As Date
        Property Obs As String

        Public Sub New(value As DTOPnd)
            MyBase.New()
            _Source = value
            With value
                _IcoStatus = .Status
                _Vto = .Vto
                _Div = .Amt.Val
                _Eur = .Amt.Eur
                _Cta = BLL.BLLPgcCta.FullNom(.Cta, BLL.BLLSession.Current.User.Lang)
                _Fra = .FraNum
                _Fch = .Fch
                _Obs = .Fpg
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


