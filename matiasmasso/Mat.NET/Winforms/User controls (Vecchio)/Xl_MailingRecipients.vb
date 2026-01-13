Public Class Xl_MailingRecipients

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        Adr
        Fch
    End Enum

    Public Shadows Sub Load(value As MailingRecipients)
        _ControlItems = New ControlItems
        For Each oItem As MailingRecipient In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As MailingRecipient
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As MailingRecipient = oControlItem.Source
            Return retval
        End Get
    End Property

    Public ReadOnly Property Values As MailingRecipients
        Get
            Dim retval As New MailingRecipients
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

            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            '.MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            '.ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Adr)
                .HeaderText = "email"
                .DataPropertyName = "Adr"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "enviat"
                .DataPropertyName = "Fch"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub onBindingSourceAddingNew(sender As Object, e As System.ComponentModel.AddingNewEventArgs)
        Dim oMailingRecipient As New MailingRecipient()
        e.NewObject = New ControlItem(oMailingRecipient)
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

    Private Function SelectedItems() As MailingRecipients
        Dim retval As New MailingRecipients
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
            'Dim oMenu_MailingRecipient As New Menu_MailingRecipient(SelectedItems)
            'oContextMenu.Items.AddRange(oMenu_MailingRecipient.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        Dim sAdr As String = e.FormattedValue
        If sAdr > "" Then
            Dim oUser = BLLUser.FromEmail(sAdr)
            If oUser Is Nothing Then
                e.Cancel = True
            Else
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oMailingRecipient As New MailingRecipient
                oMailingRecipient.User = oUser
                oControlItem.Source = oMailingRecipient
            End If
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As MailingRecipient

        Public Property Adr As String
        Public Property Fch As DateTime

        Public Sub New(oMailingRecipient As MailingRecipient)
            MyBase.New()
            _Source = oMailingRecipient
            With oMailingRecipient
                If .User IsNot Nothing Then
                    _Adr = .User.EmailAddress
                    _Fch = .FchSent
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


