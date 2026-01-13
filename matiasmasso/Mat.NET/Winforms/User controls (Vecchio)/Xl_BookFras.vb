Public Class Xl_BookFras
    Private _Values As List(Of BookFra)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Fch
        Nom
        Nif
        Cta
        Fra
        Base
        IvaAmt
        IvaPct
        IrpfAmt
        IrpfPct
        Liq
    End Enum



    Public Shadows Sub Load(values As List(Of BookFra))
        Dim iCurrentRow As Integer
        Dim iFirstDisplayedRow As Integer
        If DataGridView1.CurrentRow IsNot Nothing Then
            iCurrentRow = DataGridView1.CurrentRow.Index
            iFirstDisplayedRow = DataGridView1.FirstDisplayedScrollingRowIndex
        End If

        _Values = values
        _ControlItems = New ControlItems
        Dim DcBases As Decimal
        Dim DcIvas As Decimal
        Dim DcIrpfs As Decimal

        For Each oItem As BookFra In values
            DcBases += oItem.Base.Eur
            DcIvas += oItem.Iva.Eur
            DcIrpfs += oItem.Irpf.Eur

            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oTotals As New ControlItem(DcBases, DcIvas, DcIrpfs)
        _ControlItems.Insert(0, oTotals)

        LoadGrid()

        If DataGridView1.Rows.Count > iCurrentRow Then
            DataGridView1.CurrentCell = DataGridView1.Rows(iCurrentRow).Cells(Cols.Fch)
        Else
            DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(Cols.Fch)
        End If

        If DataGridView1.Rows.Count > iFirstDisplayedRow Then
            DataGridView1.FirstDisplayedScrollingRowIndex = iFirstDisplayedRow
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.Rows.Count - 1
        End If
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Values As List(Of BookFra)
        Get
            Return _Values
        End Get
    End Property

    Public ReadOnly Property Value As BookFra
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As BookFra = oControlItem.Source
            Return retval
        End Get
    End Property


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

            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.ico), DataGridViewImageColumn)
                .DataPropertyName = "Ico"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
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
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "Nom"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nif)
                .HeaderText = "NIF"
                .DataPropertyName = "Nif"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Cta)
                .HeaderText = "Compte"
                .DataPropertyName = "Cta"
                .Width = 120
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fra)
                .HeaderText = "Factura"
                .DataPropertyName = "Fra"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Base)
                .HeaderText = "Base"
                .DataPropertyName = "Base"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.IvaAmt)
                .HeaderText = "Iva"
                .DataPropertyName = "IvaAmt"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.IvaPct)
                .HeaderText = "Tipus"
                .DataPropertyName = "IvaPct"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#\%;-#\%;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.IrpfAmt)
                .HeaderText = "Irpf"
                .DataPropertyName = "IrpfAmt"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.IrpfPct)
                .HeaderText = "Tipus"
                .DataPropertyName = "IrpfPct"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#\%;-#\%;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Liq)
                .HeaderText = "Liquid"
                .DataPropertyName = "Liq"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
        End With
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oGrid As DataGridView = CType(sender, DataGridView)
        Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oLin As ControlItem.lins = oControlItem.lin
        Select Case oLin
            Case ControlItem.lins.Standard
            Case ControlItem.lins.Totals
                oRow.DefaultCellStyle.BackColor = Color.LightBlue
                oRow.DefaultCellStyle.Font = New Font(DataGridView1.Font, FontStyle.Bold)
        End Select
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Dim oGrid As DataGridView = CType(sender, DataGridView)
        Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oLin As ControlItem.lins = oControlItem.lin
        Select Case oLin
            Case ControlItem.lins.Standard
                Select Case e.ColumnIndex
                    Case Cols.Nif
                        If e.Value = "" Then
                            e.CellStyle.BackColor = MaxiSrvr.COLOR_NOTOK
                        End If
                    Case Cols.Fra
                        If e.Value = "" Then
                            e.CellStyle.BackColor = MaxiSrvr.COLOR_NOTOK
                        End If
                    Case Cols.IvaPct
                        If e.Value > 0 And (e.Value < 17.5 Or e.Value > 22) Then
                            e.CellStyle.BackColor = MaxiSrvr.COLOR_NOTOK
                        End If
                    Case Cols.IrpfPct
                        If e.Value > 0 And (e.Value < 14.5 Or e.Value > 22) Then
                            e.CellStyle.BackColor = MaxiSrvr.COLOR_NOTOK
                        End If
                End Select
        End Select
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

    Private Function SelectedItems() As List(Of BookFra)
        Dim retval As New List(Of BookFra)
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
            Dim oMenu_BookFra As New Menu_BookFra(SelectedItems.First)
            AddHandler oMenu_BookFra.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_BookFra.Range)
        End If
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOBookFra = CurrentControlItem.Source
        Dim oFrm As New Frm_BookFra(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
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
        Public Property Source As BookFra
        Property lin As lins
        Property ico As Image
        Property Fch As Nullable(Of Date)
        Property Nom As String
        Property Nif As String
        Property Cta As String
        Property Fra As String
        Property Base As Decimal
        Property IvaAmt As Decimal
        Property IvaPct As Decimal
        Property IrpfAmt As Decimal
        Property IrpfPct As Decimal
        Property Liq As Decimal

        Public Enum lins
            Standard
            Totals
        End Enum

        Public Sub New(oBookFra As BookFra)
            MyBase.New()
            _Source = oBookFra
            With oBookFra
                _lin = lins.Standard
                _ico = IIf(.Cca.DocFile Is Nothing, Nothing, My.Resources.pdf)
                _Fch = .Cca.fch
                If .Contact IsNot Nothing Then
                    _Nom = .Contact.Nom
                    _Nif = .Contact.NIF
                End If
                If .Contact IsNot Nothing Then
                    _Cta = .Cta.FullNom(BLL.BLLSession.Current.Lang)
                End If
                _Fra = .FraNum
                _Base = .Base.Eur
                _IvaAmt = .Iva.Eur
                _IrpfAmt = .Irpf.Eur
                If _Base <> 0 Then
                    _IvaPct = 100 * _IvaAmt / _Base
                    _IrpfPct = 100 * _IrpfAmt / _Base
                End If
                _Liq = _Base + _IvaAmt - _IrpfAmt
            End With
        End Sub

        Public Sub New(DcBases As Decimal, DcIvas As Decimal, DcIrpfs As Decimal)
            MyBase.New()
            _lin = lins.Totals
            _Nom = "totals"
            _Base = DcBases
            _IvaAmt = DcIvas
            _IrpfAmt = DcIrpfs
                If _Base <> 0 Then
                    _IvaPct = 100 * _IvaAmt / _Base
                    _IrpfPct = 100 * _IrpfAmt / _Base
                End If
            _Liq = _Base + _IvaAmt - _IrpfAmt
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

