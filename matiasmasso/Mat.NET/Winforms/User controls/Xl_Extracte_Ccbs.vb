Public Class Xl_Extracte_Ccbs
    Inherits DataGridView

    Private _Values As List(Of DTOCcb)
    Private _ControlItems As ControlItems
    Private _Filter As String
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _AllowEvents As Boolean

    Private _LastMouseDownRectangle As Rectangle

    Private _IconPdf As Image = My.Resources.pdf

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Num
        Fch
        Ico
        Txt
        Deb
        Hab
        Sdo
    End Enum



    Public Shadows Sub Load(values As List(Of DTOCcb), Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        refresca()

    End Sub

    Public ReadOnly Property Value As DTOCcb
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCcb = oControlItem.Source
            Return retval
        End Get
    End Property

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

        Dim oFilteredValues As List(Of DTOCcb) = FilteredValues()
        _ControlItems = New ControlItems

        Dim DcSaldo As Decimal = 0
        For Each oItem As DTOCcb In oFilteredValues
            Dim oControlItem As New ControlItem(oItem, DcSaldo)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        MyBase.CurrentCell = MyBase.FirstDisplayedCell
            SetContextMenu()

        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOCcb)
        Dim retval As List(Of DTOCcb)
        If _Filter = "" Then
            retval = _Values
        Else
            Dim LCaseFilter As String = _Filter.ToLower
            If IsNumeric(_Filter) Then
                Dim DcFilter As Decimal = CDec(_Filter)
                retval = _Values.FindAll(Function(x) x.Cca.Concept.ToLower.Contains(LCaseFilter) Or x.Amt.Eur = DcFilter)
            Else
                retval = _Values.FindAll(Function(x) x.Cca.Concept.ToLower.Contains(LCaseFilter)).ToList
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

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Num)
            .HeaderText = "Numero"
            .DataPropertyName = "Num"
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
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Txt)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Deb)
            .HeaderText = "Deure"
            .DataPropertyName = "Deb"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Hab)
            .HeaderText = "Haver"
            .DataPropertyName = "Hab"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sdo)
            .HeaderText = "Saldo"
            .DataPropertyName = "Sdo"
            .Width = 80
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

    Private Function SelectedItems() As List(Of DTOCcb)
        Dim retval As New List(Of DTOCcb)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then
            Dim oCurrentControlItem As ControlItem = CurrentControlItem()
            If oCurrentControlItem IsNot Nothing Then
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
        Dim oControlItems As ControlItems = SelectedControlItems()

        If oControlItems.Count > 0 Then
            Dim oSelectedItems As List(Of DTOCcb) = SelectedItems()
            Dim oMenu_Ccbs As New Menu_Ccbs(oSelectedItems)
            AddHandler oMenu_Ccbs.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Ccbs.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("Excel", My.Resources.Excel_16, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Excel()
        Dim oSheet = FEB2.Extracte.Excel(_Values, Current.Session.Lang)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub Xl_Extracte_Ccbs_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oCcb As DTOCcb = oControlItem.Source
                If oCcb.Cca.DocFile IsNot Nothing Then
                    e.Value = _IconPdf
                End If
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick


        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOCcb = CurrentControlItem.Source
            Dim oCca As DTOCca = oSelectedValue.Cca
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Cca(oCca)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.CurrentCellChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                _LastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not _LastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(_LastMouseDownRectangle.X, _LastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    MyBase.CurrentCell = sender.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                    Dim oRow As DataGridViewRow = MyBase.CurrentRow
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oCcb As DTOCcb = oControlItem.Source
                    sender.DoDragDrop(oCcb, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub


    Protected Class ControlItem
        Property Source As DTOCcb

        Property Num As Integer
        Property Fch As Date
        Property Txt As String
        Property Deb As Decimal
        Property Hab As Decimal
        Property Sdo As Decimal

        Public Sub New(value As DTOCcb, ByRef DcSaldo As Decimal)
            MyBase.New()
            _Source = value
            With value
                _Num = .Cca.Id
                _Fch = .Cca.Fch
                _Txt = .Cca.Concept
                Select Case .Dh
                    Case DTOCcb.DhEnum.Debe
                        _Deb = .Amt.Eur
                    Case Else
                        _Hab = .Amt.Eur
                End Select
                If .Cta.Act = DTOPgcCta.Acts.Deutora Then
                    DcSaldo += _Deb - _Hab
                Else
                    DcSaldo += _Hab - _Deb
                End If
                _Sdo = DcSaldo
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

    Private Sub Xl_Extracte_Ccbs_SelectionChanged(sender As Object, e As EventArgs) Handles Me.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub
End Class


