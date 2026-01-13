Public Class Xl_MailingParameters

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        Name
        Value
    End Enum

    Public Shadows Sub Load(value As MailingParameters)
        _ControlItems = New ControlItems
        For Each oItem As MailingParameter In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As MailingParameter
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As MailingParameter = oControlItem.Source
            Return retval
        End Get
    End Property

    Public ReadOnly Property Values As MailingParameters
        Get
            Dim retval As New MailingParameters
            For Each oControlItem As ControlItem In _ControlItems
                retval.Add(oControlItem.Source)
            Next
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

            Dim oBindingSource As New BindingSource
            AddHandler oBindingSource.AddingNew, AddressOf onBindingSourceAddingNew
            oBindingSource.AllowNew = True
            oBindingSource.DataSource = _ControlItems
            .DataSource = oBindingSource

            .SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            '.MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            '.ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Name)
                .HeaderText = "Nom"
                .DataPropertyName = "Name"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Value)
                .HeaderText = "Valor"
                .DataPropertyName = "Value"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub onBindingSourceAddingNew(sender As Object, e As System.ComponentModel.AddingNewEventArgs)
        Dim oMailingParameter As New MailingParameter()
        e.NewObject = New ControlItem(oMailingParameter)
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

    Private Function SelectedItems() As MailingParameters
        Dim retval As New MailingParameters
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
            'Dim oMenu_MailingParameter As New Menu_MailingParameter(SelectedItems)
            'oContextMenu.Items.AddRange(oMenu_MailingParameter.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView1.RowValidating
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim sName As String = oRow.Cells(Cols.Name).Value
        Dim sValue As String = oRow.Cells(Cols.Value).Value

        If sName > "" And sValue > "" Then
            Dim oMailingParameter As New MailingParameter
            With oMailingParameter
                .Name = sName
                .Value = sValue
            End With
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            oControlItem.Source = oMailingParameter
        ElseIf sName = "" Xor sValue = "" Then
            e.Cancel = True
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As MailingParameter

        Public Property Name As String
        Public Property Value As String

        Public Sub New(oMailingParameter As MailingParameter)
            MyBase.New()
            _Source = oMailingParameter
            With oMailingParameter
                _Name = .Name
                _Value = .Value
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

