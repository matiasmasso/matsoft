Imports MatHelperStd

Public Class Xl_NominasFactory

    Inherits _Xl_ReadOnlyDatagridview

    Private _allValues As List(Of DTONomina)
    Private _selectedValues As List(Of DTONomina)

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Chk
        Ico
        Nom
        Devengat
        Dietas
        SegSoc
        Irpf
        Deutes
        Liquid
    End Enum

    Public Function CheckedValues() As List(Of DTONomina)
        Dim oCheckedControlItems = _ControlItems.ToList.Where(Function(x) x.Checked)
        Dim retval As List(Of DTONomina) = oCheckedControlItems.Select(Function(y) y.Source).ToList
        Return retval
    End Function

    Public Shadows Sub Load(allValues As List(Of DTONomina), selectedValues As List(Of DTONomina))
        _allValues = allValues
        _selectedValues = selectedValues

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each Value In _allValues
            Dim oControlitem As New ControlItem(Value)
            oControlitem.Checked = _selectedValues.Any(Function(x) x.Equals(oControlitem.Source))
            _ControlItems.Add(oControlitem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.35
        'MyBase.RowRol.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False

        MyBase.Columns.Add(New DataGridViewCheckBoxColumn(False))
        With DirectCast(MyBase.Columns(Cols.Chk), DataGridViewCheckBoxColumn)
            .HeaderText = ""
            .DataPropertyName = "Checked"
            .Width = 20
            .DefaultCellStyle.SelectionBackColor = Color.White
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
        With MyBase.Columns(Cols.Nom)
            .DataPropertyName = "Nom"
            .HeaderText = "treballador"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Devengat)
            .DataPropertyName = "Devengat"
            .HeaderText = "devengat"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dietas)
            .DataPropertyName = "Dietas"
            .HeaderText = "dietes"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SegSoc)
            .DataPropertyName = "SegSoc"
            .HeaderText = "Seg.Social"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Irpf)
            .DataPropertyName = "Irpf"
            .HeaderText = "Irpf"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Deutes)
            .DataPropertyName = "Deutes"
            .HeaderText = "Deutes"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Liquid)
            .DataPropertyName = "Liquid"
            .HeaderText = "liquid"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
    End Sub

    Private Function SelectedItems() As List(Of DTONomina)
        Dim retval As New List(Of DTONomina)
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
            'Dim oMenu_Nomina As New Menu_Nomina(SelectedItems)
            'AddHandler oMenu_Nomina.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Nomina.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            'Dim oSelectedValue As DTONomina = CurrentControlItem.Source
            'Dim oFrm As New Frm_Nomina(oSelectedValue)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Chk
                    RaiseEvent AfterUpdate(sender, New MatEventArgs(CurrentControlItem.Source))
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case MyBase.CurrentCell.ColumnIndex
            Case Cols.Chk
                MyBase.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub


    Protected Class ControlItem
        Public Property Source As DTONomina

        Public Property Checked As Boolean
        Public Property Ico As Image = Nothing
        Public Property Nom As String
        Public Property Devengat As Decimal
        Public Property Dietas As Decimal
        Public Property SegSoc As Decimal
        Public Property Irpf As Decimal
        Public Property Deutes As Decimal
        Public Property Liquid As Decimal

        Public Sub New(oNomina As DTONomina)
            MyBase.New()
            _Source = oNomina
            _Checked = True

            With oNomina
                _Nom = DTOStaff.AliasOrNom(.Staff)
                If .Devengat IsNot Nothing Then _Devengat = .Devengat.Eur
                If .Dietes IsNot Nothing Then _Dietas = .Dietes.Eur
                If .SegSocial IsNot Nothing Then _SegSoc = .SegSocial.Eur
                If .Irpf IsNot Nothing Then _Irpf = .Irpf.Eur
                If .Embargos IsNot Nothing Then _Deutes = .Embargos.Eur
                If .Deutes IsNot Nothing Then _Deutes += .Deutes.Eur
                If .Anticips IsNot Nothing Then _Deutes += .Anticips.Eur
                If .Liquid IsNot Nothing Then _Liquid = .Liquid.Eur

                If (_Devengat) <> (_SegSoc + _Irpf + _Deutes + _Liquid) Then
                    _Ico = My.Resources.warn
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class
