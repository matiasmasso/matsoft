Public Class Xl_leadsChecked

    Inherits DataGridView

    Private _DefaultValue As DTOLeadChecked
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ItemCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Property AllValues As List(Of DTOLeadChecked)

    Private Enum Cols
        Check
        Nom
        Domain
    End Enum

    Public Shadows Sub Load(AllValues As List(Of DTOLeadChecked), Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _AllValues = AllValues
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Public ReadOnly Property CheckedItems() As List(Of DTOLeadChecked)
        Get
            Dim retval As New List(Of DTOLeadChecked)
            If _ControlItems IsNot Nothing Then
                For Each oControlItem As ControlItem In _ControlItems
                    If oControlItem.CheckState = CheckState.Checked Then
                        retval.Add(oControlItem.Source)
                    End If
                Next
            End If
            Return retval
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False


        Dim oFilteredValues As List(Of DTOLeadChecked) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOLeadChecked In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        MyBase.Sort(MyBase.Columns(Cols.Domain), System.ComponentModel.ListSortDirection.Ascending)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Function FilteredValues() As List(Of DTOLeadChecked)
        Dim retval As List(Of DTOLeadChecked)
        If _Filter = "" Then
            retval = _AllValues
        Else
            retval = _AllValues.FindAll(Function(x) x.EmailAddress.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _AllValues IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub


    Public ReadOnly Property Value As DTOLeadChecked
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOLeadChecked = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()
        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowUserToResizeColumns = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Check), DataGridViewImageColumn)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Area"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Domain)
            .DataPropertyName = "Domain"
            .Visible = False
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

    Private Function SelectedItems() As List(Of DTOLeadChecked)
        Dim retval As New List(Of DTOLeadChecked)
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
            If oControlItem IsNot Nothing Then
                Dim oMenuUser As New Menu_User(oControlItem.Source)
                oContextMenu.Items.AddRange(oMenuUser.Range)
                oContextMenu.Items.Add("-")
            End If

            oContextMenu.Items.Add("Seleccionar", My.Resources.Checked13, AddressOf Do_Check)
            oContextMenu.Items.Add("Deseleccionar", My.Resources.UnChecked13, AddressOf Do_UnCheck)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Check(sender As Object, e As System.EventArgs)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlitem As ControlItem = oRow.DataBoundItem
            oControlitem.CheckState = CheckState.Checked
            oControlitem.Source.Checked = True
            MyBase.NotifyCurrentCellDirty(True)
        Next
        MyBase.Refresh()
    End Sub
    Private Sub Do_UnCheck(sender As Object, e As System.EventArgs)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlitem As ControlItem = oRow.DataBoundItem
            oControlitem.CheckState = CheckState.Unchecked
            oControlitem.Source.Checked = False
            MyBase.NotifyCurrentCellDirty(True)
        Next
        MyBase.Refresh()

    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOLeadChecked = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    'Dim oFrm As New Frm_LeadChecked(oSelectedValue)
                    'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    'oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    'RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oControlItem As ControlItem = MyBase.Rows(e.RowIndex).DataBoundItem
                Select Case oControlItem.CheckState
                    Case CheckState.Checked
                        e.Value = My.Resources.Checked13
                    Case CheckState.Unchecked
                        e.Value = My.Resources.UnChecked13
                    Case CheckState.Indeterminate
                        e.Value = My.Resources.CheckedGrayed13
                End Select
        End Select
    End Sub




    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MyBase.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oCurrentCheckState As CheckState = oControlItem.CheckState
                Dim oNewCheckState As CheckState = IIf(oControlItem.CheckState = CheckState.Checked, CheckState.Unchecked, CheckState.Checked)
                Dim oLead As DTOLeadChecked = oControlItem.Source
                oLead.Checked = oNewCheckState = CheckState.Checked
                'MyBase.Refresh()
                Dim oArgs As New ItemCheckEventArgs(e.RowIndex, oNewCheckState, oCurrentCheckState)
                RaiseEvent ItemCheck(Me, oArgs)
        End Select
    End Sub


    Protected Class ControlItem
        Property Source As DTOLeadChecked
        Property CheckState As CheckState
        Property Nom As String
        Property Domain As String


        Public Sub New(value As DTOLeadChecked)
            MyBase.New()
            _Source = value
            With value
                _CheckState = IIf(value.Checked, CheckState.Checked, CheckState.Unchecked)
                _Nom = .EmailAddress
                _Domain = .EmailAddress.Substring(.EmailAddress.IndexOf("@"))
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

