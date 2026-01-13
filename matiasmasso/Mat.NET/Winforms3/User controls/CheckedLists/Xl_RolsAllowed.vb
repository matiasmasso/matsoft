Public Class Xl_RolsAllowed
    Inherits _Xl_ReadOnlyDatagridview

    Private _allValues As List(Of DTORol)
    Private _selectedValues As List(Of DTORol)

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Chk
        Nom
    End Enum

    Public Function CheckedValues() As List(Of DTORol)
        Dim retval As New List(Of DTORol)
        If _ControlItems IsNot Nothing Then
            Dim oCheckedControlItems = _ControlItems.ToList.Where(Function(x) x.Checked)
            retval = oCheckedControlItems.Select(Function(y) y.Source).ToList
        End If
        Return retval
    End Function

    Public Shadows Sub Load(allValues As List(Of DTORol), Optional selectedValues As List(Of DTORol) = Nothing)
        _allValues = allValues
        _selectedValues = selectedValues
        If _selectedValues Is Nothing Then _selectedValues = New List(Of DTORol)

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
            If _selectedValues IsNot Nothing Then
                oControlitem.Checked = _selectedValues.Any(Function(x) x.Equals(oControlitem.Source))
            End If
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

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Function SelectedItems() As List(Of DTORol)
        Dim retval As New List(Of DTORol)
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
            Dim oMenu_Rol As New Menu_Rol(SelectedItems)
            AddHandler oMenu_Rol.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Rol.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTORol = CurrentControlItem.Source
            Dim oFrm As New Frm_Rol(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
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
        Property Source As DTORol

        Property Checked As Boolean
        Property Nom As String

        Public Sub New(value As DTORol)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Nom.Tradueix(Current.Session.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
