Public Class Xl_EmailsContacts
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOContact)
    Private _DefaultValue As DTOUser

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        country
        zona
        location
        clinom
        email
    End Enum

    Public Shadows Sub Load(values As List(Of DTOContact), Optional oDefaultValue As DTOUser = Nothing)
        _Values = values
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
        Dim oFilteredValues As List(Of DTOContact) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oContact As DTOContact In oFilteredValues
            For Each oEmail As DTOUser In oContact.Emails
                Dim oControlItem As New ControlItem(oContact, oEmail)
                _ControlItems.Add(oControlItem)
            Next
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

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

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Values As List(Of String)
        Get
            Dim retval = _ControlItems.ToList.Select(Function(x) x.User.EmailAddress).Distinct
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowUser.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Country)
            .HeaderText = "Pais"
            .DataPropertyName = "country"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 50
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Zona)
            .HeaderText = "Zona"
            .DataPropertyName = "zona"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.location)
            .HeaderText = "Població"
            .DataPropertyName = "Location"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.clinom)
            .HeaderText = "Rao Social"
            .DataPropertyName = "Clinom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.email)
            .HeaderText = "Email"
            .DataPropertyName = "Email"
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

    Private Function SelectedItems() As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.User)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.User)
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
            Dim oMenu_User As New Menu_User(SelectedItems.First)
            AddHandler oMenu_User.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_User.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOUser = CurrentControlItem.User
            Dim oFrm As New Frm_User(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.User))
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Property Contact As DTOContact
        Property User As DTOUser
        Property Country As String
        Property Zona As String
        Property Location As String
        Property CliNom As String
        Property Email As String

        Public Sub New(oContact As DTOContact, oUser As DTOUser)
            MyBase.New()
            _Contact = oContact
            _User = oUser
            _Country = oContact.Address.Zip.Location.Zona.Country.ISO
            _Zona = oContact.Address.Zip.Location.Zona.Nom
            _Location = oContact.Address.Zip.Location.Nom
            _CliNom = oContact.NomAndNomComercial()
            _Email = oUser.EmailAddress
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

