Public Class Xl_Banks
    Private _ControlItems As ControlItems
    Private _DefaultBank As DTOBank
    Private _AllowEvents As Boolean
    Private _SelectionMode As DTO.Defaults.SelectionModes

    Private _FirstDisplayedScrollingRowIndex As Integer
    Private _LastRowIndex As Integer

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        id
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOBank), oDefaultBank As DTOBank, oSelectionMode As DTO.Defaults.SelectionModes)
        _SelectionMode = oSelectionMode
        _DefaultBank = oDefaultBank
        SetControlItems(values)
        LoadGrid()
        SetCurrentRow()
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOBank
        Get
            Dim retval As DTOBank = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Private Sub SetControlItems(oValues As List(Of DTOBank))
        _ControlItems = New ControlItems
        For Each oItem As DTOBank In oValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
    End Sub

    Private Sub LoadGrid()
        _AllowEvents = False
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
            With DirectCast(.Columns(Cols.ico), DataGridViewImageColumn)
                .DataPropertyName = "Ico"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.id)
                .HeaderText = "Codi"
                .DataPropertyName = "Id"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "Nom"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With



    End Sub

    Private Sub SetCurrentRow()
        If _DefaultBank Is Nothing Then
            If _LastRowIndex >= _ControlItems.Count Then
                _LastRowIndex = _ControlItems.Count - 1
            End If

            If _LastRowIndex >= 0 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(_LastRowIndex).Cells(Cols.Nom)
                DataGridView1.FirstDisplayedScrollingRowIndex = _FirstDisplayedScrollingRowIndex
            End If
        Else
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultBank))
            Dim Idx As Integer = _ControlItems.IndexOf(oControlItem)
            DataGridView1.CurrentCell = DataGridView1.Rows(Idx).Cells(Cols.Nom)
        End If
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

    Private Function SelectedItems() As List(Of DTOBank)
        Dim retval As New List(Of DTOBank)
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
            Dim oMenu_Bank_Object As New Menu_Bank(SelectedItems.First)
            AddHandler oMenu_Bank_Object.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Bank_Object.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOBank = CurrentControlItem.Source
        Select Case _SelectionMode
            Case DTO.Defaults.SelectionModes.Browse
                'Dim oFrm As New Frm_Location(oSelectedValue)
                'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                'oFrm.Show()
            Case DTO.Defaults.SelectionModes.Selection
                RaiseEvent OnItemSelected(Me, New MatEventArgs(Me.Value))
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oBank As DTOBank = oControlItem.Source
        If oBank.Obsoleto Then
            oRow.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        Else
            oRow.DefaultCellStyle.BackColor = Color.White
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                Dim oBank As DTOBank = oControlItem.Source
                RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            End If
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        _FirstDisplayedScrollingRowIndex = DataGridView1.FirstDisplayedScrollingRowIndex
        _LastRowIndex = _ControlItems.IndexOf(CurrentControlItem)
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOBank

        Property Ico As Image
        Property Id As String
        Property Nom As String

        Public Sub New(oBank As DTOBank)
            MyBase.New()
            _Source = oBank
            With oBank
                _Id = .Id
                _Nom = IIf(.NomComercial > "", .NomComercial, .RaoSocial)
                If .Obsoleto Then
                    _Ico = My.Resources.aspa
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

