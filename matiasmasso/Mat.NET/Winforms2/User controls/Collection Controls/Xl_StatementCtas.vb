Public Class Xl_StatementCtas
    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As DTOStatement
    Private _DefaultValue As DTOBaseGuid
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse
    Private _ShowSaldosMenuItem As ToolStripMenuItem

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Amt
    End Enum

    Public Shadows Sub Load(value As DTOStatement, Optional oDefaultValue As DTOPgcCta = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        _Value = value
        _SelectionMode = oSelectionMode
        _DefaultValue = oDefaultValue

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False

        Dim Query = _Value.Items.GroupBy(Function(x) x.CtaGuid).Select(Function(y) y.First.CtaGuid).ToList()
        _ControlItems = New ControlItems
        For Each oCtaGuid In Query
            Dim oCta = _Value.Ctas.FirstOrDefault(Function(x) x.Guid.Equals(oCtaGuid))
            Dim items = _Value.Items.Where(Function(x) x.CtaGuid.Equals(oCtaGuid))
            Dim deb = items.Where(Function(x) x.Dh = DTOCcb.DhEnum.debe).Sum(Function(y) y.Amt.Eur)
            Dim hab = items.Where(Function(x) x.Dh = DTOCcb.DhEnum.haber).Sum(Function(y) y.Amt.Eur)
            Dim oControlItem As New ControlItem(oCta, deb, hab)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOPgcCta
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPgcCta = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        _ShowSaldosMenuItem = New ToolStripMenuItem("mostrar saldos")
        _ShowSaldosMenuItem.CheckOnClick = True

        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowPgcCta.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 70
            .Visible = _ShowSaldosMenuItem.Checked
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

    Private Function SelectedItems() As List(Of DTOPgcCta)
        Dim retval As New List(Of DTOPgcCta)
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
            Dim oMenu_PgcCta As New Menu_PgcCta(SelectedItems.First)
            AddHandler oMenu_PgcCta.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PgcCta.Range)
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add(_ShowSaldosMenuItem)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPgcCta = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.browse
                    Dim oFrm As New Frm_PgcCta(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.selection
                    RaiseEvent OnItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Property Source As DTOPgcCta

        Property Nom As String
        Property Eur As Decimal

        Public Sub New(oCta As DTOPgcCta, deb As Decimal, hab As Decimal)
            _Source = oCta
            _Nom = String.Format("{0} {1}", oCta.Id, oCta.Nom.Tradueix(Current.Session.Lang))
            _Eur = IIf(oCta.Cod = oCta.Act, deb - hab, hab - deb)
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


