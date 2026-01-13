Public Class Xl_Users
    Inherits DataGridView

    Private _Values As List(Of DTOUser)
    Private _DefaultValue As DTOUser
    Private _SelectionMode As DTO.Defaults.SelectionModes

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ItemSelected(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        emailAddress
    End Enum

    Public Shadows Sub Load(values As List(Of DTOUser), Optional oDefaultValue As DTOUser = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOUser)
        Get
            Return _Values
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOUser) = FilteredValues()
        _ControlItems = New ControlItems
        If oFilteredValues IsNot Nothing Then
            For Each oItem As DTOUser In oFilteredValues
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Next
        End If

        MyBase.DataSource = _ControlItems
        MyBase.CurrentCell = MyBase.FirstDisplayedCell
        If _ControlItems.Count > 0 Then
            SetContextMenu()
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOUser)
        Dim retval As List(Of DTOUser)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.EmailAddress.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOUser
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOUser = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = False
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.emailAddress)
            .HeaderText = "email"
            .DataPropertyName = "EmailAddress"
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
            Dim oMenu_User As New Menu_User(SelectedItems.First)
            AddHandler oMenu_User.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_User.Range)
            oContextMenu.Items.Add("-")
        End If

        If _ControlItems.Count > 0 Then
            oContextMenu.Items.Add("csv", Nothing, AddressOf Do_Csv)
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Csv()
        Dim oCsv As New DTOCsv
        For Each Item As DTOUser In _Values
            Dim oRow As DTOCsvRow = oCsv.AddRow()
            oRow.addcell(Item.EmailAddress)
        Next

        UIHelper.SaveCsvDialog(oCsv, "mailing")
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oSelectedValue As DTOUser = CurrentControlItem.Source
        Select Case _SelectionMode
            Case Defaults.SelectionModes.browse
                Dim oFrm As New Frm_User(oSelectedValue)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case Defaults.SelectionModes.selection
                RaiseEvent ItemSelected(Me, New MatEventArgs(oSelectedValue))
        End Select
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

    Protected Class ControlItem
        Property Source As DTOUser

        Property EmailAddress As String

        Public Sub New(value As DTOUser)
            MyBase.New()
            _Source = value
            With value

                If .NickName = "" Then
                    _EmailAddress = .EmailAddress
                Else
                    _EmailAddress = String.Format("{0} <{1}>", .NickName, .EmailAddress)
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

