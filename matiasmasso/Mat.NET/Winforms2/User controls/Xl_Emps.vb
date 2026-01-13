Public Class Xl_Emps
    Private _ControlItems As ControlItems
    Private _SelectionMode As DTO.Defaults.SelectionModes
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event SelectedItemChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOEmp), selectedvalue As DTOEmp, Optional SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _SelectionMode = SelectionMode
        _ControlItems = New ControlItems
        For Each oItem As DTOEmp In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()

        selectedvalue = values.Find(Function(x) x.Id = selectedvalue.Id)
        If selectedvalue IsNot Nothing Then
            Dim iRowIdx As Integer = values.IndexOf(selectedvalue)
            If DataGridView1.Rows.Count > iRowIdx Then
                DataGridView1.CurrentCell = DataGridView1.Rows(iRowIdx).Cells(Cols.Nom)
            End If
        End If
    End Sub

    Public ReadOnly Property value as DTOEmp
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval as DTOEmp = oControlItem.Source
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
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "Nom"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of DTOEmp)
        Dim retval As New List(Of DTOEmp)
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
            Dim oMenu_Emp As New Menu_Emp(SelectedItems.First)
            AddHandler oMenu_Emp.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Emp.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub
    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        OnItemSelected()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            OnItemSelected()
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            If _SelectionMode = DTO.Defaults.SelectionModes.Browse Then
                RaiseEvent SelectedItemChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            End If
            SetContextMenu()
        End If
    End Sub

    Private Sub OnItemSelected()
        Dim oSelectedvalue as DTOEmp = CurrentControlItem.Source
        Select Case _SelectionMode
            Case DTO.Defaults.SelectionModes.Browse
                Dim oFrm As New Frm_Emp(oSelectedValue)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case DTO.Defaults.SelectionModes.Selection
                RaiseEvent SelectedItemChanged(Me, New MatEventArgs(oSelectedvalue))
        End Select
    End Sub

    Private Sub RefreshRequest()

    End Sub

    Protected Class ControlItem
        Public Property Source As DTOEmp

        Public Property Nom As String

        Public Sub New(oEmp as DTOEmp)
            MyBase.New()
            _Source = oEmp
            With oEmp
                _Nom = .Nom
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
