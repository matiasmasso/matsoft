Public Class Xl_Extracte_Ccbs
    Inherits DataGridView

    Private _Extracte As DTOExtracte
    Private _ControlItems As ControlItems
    Private _Filter As String
    Private _AllowEvents As Boolean

    Private _IconPdf As Image = My.Resources.pdf

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Num
        Fch
        Ico
        Txt
        Deb
        Hab
        Sdo
    End Enum



    Public Shadows Sub Load(oExtracte As DTOExtracte, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Extracte = oExtracte
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
            If _Extracte IsNot Nothing Then refresca()
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

        If _ControlItems.Count > 0 Then
            MyBase.DataSource = _ControlItems
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
            SetContextMenu()
        End If

        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOCcb)
        Dim retval As List(Of DTOCcb)
        If _Filter = "" Then
            retval = _Extracte.Ccbs
        Else
            Dim LCaseFilter As String = _Filter.ToLower
            If IsNumeric(_Filter) Then
                Dim DcFilter As Decimal = CDec(_Filter)
                retval = _Extracte.Ccbs.FindAll(Function(x) x.Cca.Concept.ToLower.Contains(LCaseFilter) Or x.Amt.Eur = DcFilter)
            Else
                retval = _Extracte.Ccbs.FindAll(Function(x) x.Cca.Concept.ToLower.Contains(LCaseFilter)).ToList
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
        With CType(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
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
        Dim oControlItems As ControlItems = SelectedControlItems()

        If oControlItems.Count > 0 Then
            Dim oMenu_Ccbs As New Menu_Ccbs(SelectedItems)
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
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Filter = "Excel (*.xlsx)|*.xlsx"
            .DefaultExt = ".xlsx"
            If .ShowDialog Then
                Dim oWorkbook As ClosedXML.Excel.XLWorkbook = BLL.BLLExtracte.Excel(_Extracte)
                oWorkbook.SaveAs(.FileName)
            End If
        End With
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
        Dim oSelectedValue As DTOCcb = CurrentControlItem.Source

        Dim oCca As DTOCca = oSelectedValue.Cca
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.CurrentCellChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
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


