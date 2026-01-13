Public Class Xl_AeatMod347Ccas

    Private _Values As List(Of DTOAeatMod347Cca)
    Private _ControlItems As ControlItems
    Private _filter As String

    Private _AllowEvents As Boolean
    Private COLOR_DISABLED As System.Drawing.Color = Color.FromArgb(210, 210, 210)

    Private Enum Cols
        Id
        Fch
        Pdf
        Concept
        Base
        IVA
        Total
        T1
        T2
        T3
        T4
    End Enum

    Public Shadows Sub Load(values As List(Of DTOAeatMod347Cca))
        If values IsNot Nothing Then
            _Values = values
            refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOAeatMod347Cca
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOAeatMod347Cca = oControlItem.Source
            Return retval
        End Get
    End Property

    Public Property Filter As String
        Get
            Return _filter
        End Get
        Set(value As String)
            _filter = value
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
        _ControlItems = New ControlItems
        For Each oItem As DTOAeatMod347Cca In _Values
            Dim blProcede As Boolean
            If _filter = "" Then
                blProcede = True
            Else
                blProcede = (oItem.Cca.Concept.ToUpper.Contains(_filter.ToUpper))
            End If
            If blProcede Then
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            End If
        Next

        Dim DtFchSum As Date
        If _ControlItems.Count > 0 Then
            Dim oItem As DTOAeatMod347Cca = _ControlItems(0).Source
            Dim iYear As Integer = oItem.Cca.Fch.Year
            DtFchSum = New Date(iYear, 12, 31)
        End If

        Dim oSum As New ControlItem(Nothing)
        With oSum
            .Fch = DtFchSum
            .Concept = "Totals"
            .Total = _ControlItems.Sum(Function(x) x.Total)
            .T1 = _ControlItems.Sum(Function(x) x.T1)
            .T2 = _ControlItems.Sum(Function(x) x.T2)
            .T3 = _ControlItems.Sum(Function(x) x.T3)
            .T4 = _ControlItems.Sum(Function(x) x.T4)
        End With
        _ControlItems.Add(oSum)
        LoadGrid()

    End Sub

    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Id)
                .HeaderText = "Numero"
                .DataPropertyName = "Id"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Pdf), DataGridViewImageColumn)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Concept)
                .HeaderText = "Concepte"
                .DataPropertyName = "Concept"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Base)
                .HeaderText = "Base"
                .DataPropertyName = "Base"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.IVA)
                .HeaderText = "IVA"
                .DataPropertyName = "IVA"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Total)
                .HeaderText = "Total"
                .DataPropertyName = "Total"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.t1)
                .HeaderText = "T1"
                .DataPropertyName = "T1"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.t2)
                .HeaderText = "T2"
                .DataPropertyName = "T2"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.t3)
                .HeaderText = "T3"
                .DataPropertyName = "T3"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.t4)
                .HeaderText = "T4"
                .DataPropertyName = "T4"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
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
        Dim oMenuItem As ToolStripMenuItem

        If oControlItem IsNot Nothing Then
            Dim oAeatMod347Cca As DTOAeatMod347Cca = oControlItem.Source
            If oAeatMod347Cca IsNot Nothing Then
                Dim oCca As DTOCca = oAeatMod347Cca.Cca

                oMenuItem = New ToolStripMenuItem("assentament...")
                oContextMenu.Items.Add(oMenuItem)
                If oCca Is Nothing Then
                    oMenuItem.Enabled = False
                Else
                    Dim oMenuCca As New Menu_Cca(oCca)
                    oMenuItem.DropDownItems.AddRange(oMenuCca.Range)
                End If
            End If
        End If

        oMenuItem = New ToolStripMenuItem("Excel", Nothing, AddressOf Do_Excel)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu

    End Sub

    Private Sub Do_Excel()
        Dim oSheet = DTOAeatMod347.ExcelSheet(_Values)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Pdf
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Source IsNot Nothing Then
                    Dim oAeatMod347Cca As DTOAeatMod347Cca = oControlItem.Source
                    Dim oCca As DTOCca = oAeatMod347Cca.Cca
                    If oCca.DocFile Is Nothing Then
                        e.Value = My.Resources.empty
                    Else
                        e.Value = My.Resources.pdf
                    End If

                End If
        End Select
    End Sub




    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Property Source As DTOAeatMod347Cca

        Property Id As Integer
        Property Fch As Date
        Property Concept As String
        Property Base As Decimal
        Property IVA As Decimal
        Property Total As Decimal
        Property T1 As Decimal
        Property T2 As Decimal
        Property T3 As Decimal
        Property T4 As Decimal

        Public Sub New(item As DTOAeatMod347Cca)
            MyBase.New()
            If item IsNot Nothing Then
                _Source = item
                With item
                    _Id = .Cca.Id
                    _Fch = .Cca.Fch
                    _Concept = .Cca.Concept
                    _Base = .Base.Eur
                    _IVA = .Iva.Eur
                    _Total = _Base + _IVA
                    Dim Quarter As Integer = DatePart(DateInterval.Quarter, _Fch)
                    _T1 = IIf(Quarter = 1, _Total, 0)
                    _T2 = IIf(Quarter = 2, _Total, 0)
                    _T3 = IIf(Quarter = 3, _Total, 0)
                    _T4 = IIf(Quarter = 4, _Total, 0)
                End With

            End If
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


