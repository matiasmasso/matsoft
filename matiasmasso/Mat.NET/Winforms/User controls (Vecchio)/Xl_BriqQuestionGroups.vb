Public Class Xl_BriqQuestionGroups
    Private _Values As BriqQuestionGroups
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Title
    End Enum

    Public Shadows Sub Load(values As BriqQuestionGroups)
        _Values = values
        refresca()
    End Sub

    Private Sub refresca()
        _ControlItems = New ControlItems
        For Each oItem As BriqQuestionGroup In _Values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As BriqQuestionGroup
        Get
            Dim retval As BriqQuestionGroup = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If

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

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Title)
                .HeaderText = "Nom"
                .DataPropertyName = "Title"
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

    Private Function SelectedItems() As BriqQuestionGroups
        Dim retval As New BriqQuestionGroups
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
            'Dim oMenu_BriqQuestionGroup As New Menu_BriqQuestionGroup(SelectedItems.First)
            'AddHandler oMenu_BriqQuestionGroup.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_BriqQuestionGroup.Range)
        End If
        oContextMenu.Items.Add("afegir nou", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oGroup As BriqQuestionGroup = CurrentControlItem.Source
        Dim oFrm As New Frm_BriqQuestionGroup(oGroup)
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
        refresca()
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Public Property Source As BriqQuestionGroup

        Public Property Title As String

        Public Sub New(oBriqQuestionGroup As BriqQuestionGroup)
            MyBase.New()
            _Source = oBriqQuestionGroup
            With oBriqQuestionGroup
                _Title = .Title
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
