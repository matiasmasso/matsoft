Public Class Xl_Contacts_Editable

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOContact)
    Private _PropertiesSet As Boolean

    Private _DefaultValue As DTOContact
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Async Sub Load(values As List(Of DTOContact), Optional oDefaultValue As DTOContact = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        If Not _PropertiesSet Then
            SetProperties()
            _PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Await Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOContact)
        Get
            Dim retval As New List(Of DTOContact)
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.Source IsNot Nothing Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

    Private Async Function Refresca() As Task
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOContact) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOContact In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems

        If _DefaultValue Is Nothing Then
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        Else
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        Await SetContextMenu()
        _AllowEvents = True
    End Function

    Private Function FilteredValues() As List(Of DTOContact)
        Dim retval As List(Of DTOContact)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public ReadOnly Property Value As DTOContact
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOContact = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = True
        MyBase.RowHeadersWidth = 35
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False
        MyBase.AllowUserToAddRows = True
        MyBase.AllowUserToDeleteRows = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                retval.Add(oControlItem.Source)
            End If
        Next

        If retval.Count = 0 AndAlso CurrentControlItem()?.Source IsNot Nothing Then retval.Add(CurrentControlItem.Source)
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

    Private Async Function SetContextMenu() As Task
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oContactMenu = Await FEB.ContactMenu.Find(exs, SelectedItems.First)
            If oContactMenu IsNot Nothing And SelectedItems()?.Count > 0 Then
                Dim oMenu_Contact As New Menu_Contact(SelectedItems.First, oContactMenu)
                AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Contact.Range)
                oContextMenu.Items.Add("-")
            End If
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Function

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oFrm As New Frm_Contact(oCurrentControlItem.Source)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Async Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            Await SetContextMenu()
        End If
    End Sub


    Private Sub Xl_Contacts_Editable_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles Me.CellValidating
        Select Case e.ColumnIndex
            Case Cols.Nom
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem

                Dim procesa As Boolean = oControlItem Is Nothing And e.FormattedValue > ""
                If oControlItem IsNot Nothing Then
                    procesa = oControlItem.Nom <> e.FormattedValue
                End If

                If procesa Then
                    Dim exs As New List(Of Exception)
                    Dim oContact = Finder.FindContact(exs, Current.Session.User, e.FormattedValue)
                    If exs.Count = 0 Then
                        If oContact Is Nothing Then
                            e.Cancel = True
                        Else
                            oControlItem.Source = oContact
                            oControlItem.Nom = oContact.FullNom
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

    Private Sub Xl_Contacts_Editable_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellValidated
        Select Case e.ColumnIndex
            Case Cols.Nom
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem IsNot Nothing Then
                    Dim oContact As DTOContact = oControlItem.Source
                    If oContact IsNot Nothing Then
                        If oControlItem.Nom <> oContact.FullNom Then
                            oControlItem.Nom = oContact.FullNom
                            RaiseEvent AfterUpdate(Me, New MatEventArgs(Values))
                        End If
                    End If
                End If
        End Select

    End Sub

    Private Sub Xl_Contacts_Editable_UserDeletedRow(sender As Object, e As DataGridViewRowEventArgs) Handles Me.UserDeletedRow
        RaiseEvent AfterUpdate(Me, New MatEventArgs(Values))
    End Sub

    Protected Class ControlItem
        Property Source As DTOContact

        Property Nom As String

        Public Sub New(value As DTOContact)
            MyBase.New()
            _Source = value
            With value
                _Nom = .FullNom
            End With
        End Sub

        Public Sub New() 'obligatori per editable grid
            MyBase.New
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


