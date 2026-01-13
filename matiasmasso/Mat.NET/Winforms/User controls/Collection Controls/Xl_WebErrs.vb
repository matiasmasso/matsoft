Public Class Xl_WebErrs
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOWebErr)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        Usr
        Url
        Referrer

    End Enum

    Public Shadows Sub Load(values As List(Of DTOWebErr))
        _Values = values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOWebErr) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOWebErr In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOWebErr)
        Dim retval As List(Of DTOWebErr)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Url.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOWebErr
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOWebErr = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowWebErr.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Usr)
            .HeaderText = "Usr"
            .DataPropertyName = "Usr"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Url)
            .HeaderText = "Url"
            .DataPropertyName = "Url"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Referrer)
            .HeaderText = "Referrer"
            .DataPropertyName = "Referrer"
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

    Private Function SelectedItems() As List(Of DTOWebErr)
        Dim retval As New List(Of DTOWebErr)
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
            oContextMenu.Items.Add("Navegar", Nothing, AddressOf Do_Browse)

            Dim oMenuItemReferrer As New ToolStripMenuItem("Navegar a referrer", Nothing, AddressOf Do_BrowseReferrer)
            oMenuItemReferrer.Enabled = Not String.IsNullOrEmpty(SelectedItems.First.Referrer)
            oContextMenu.Items.Add(oMenuItemReferrer)

            oContextMenu.Items.Add("Copiar enllaç", Nothing, AddressOf Do_CopyLink)
            oContextMenu.Items.Add("Copiar enllaç a referrer", Nothing, AddressOf Do_CopyLinkReferrer)

            Dim userMenuItem = New ToolStripMenuItem("Usuari")
            oContextMenu.Items.Add(userMenuItem)
            If SelectedItems.First.User Is Nothing Then
                userMenuItem.Enabled = False
            Else
                Dim oMenu_User As New Menu_User(SelectedItems.First.User)
                AddHandler oMenu_User.AfterUpdate, AddressOf RefreshRequest
                userMenuItem.DropDownItems.AddRange(oMenu_User.Range)
            End If

        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Do_Browse()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_Browse()
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        Dim oSelectedValue As DTOWebErr = CurrentControlItem.Source
        Dim url = oSelectedValue.Url
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub Do_BrowseReferrer()
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        Dim oSelectedValue As DTOWebErr = CurrentControlItem.Source
        Dim url = oSelectedValue.Referrer
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub Do_CopyLink()
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        Dim oSelectedValue As DTOWebErr = CurrentControlItem.Source
        Dim url = oSelectedValue.Url
        UIHelper.CopyLink(url)
    End Sub

    Private Sub Do_CopyLinkReferrer()
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        Dim oSelectedValue As DTOWebErr = CurrentControlItem.Source
        Dim url = oSelectedValue.Referrer
        UIHelper.CopyLink(url)
    End Sub


    Protected Class ControlItem
        Property Source As DTOWebErr

        Property Fch As Nullable(Of Date)
        Property Usr As String
        Property Url As String
        Property Referrer As String

        Public Sub New(value As DTOWebErr)
            MyBase.New()
            _Source = value
            With value
                _Fch = .Fch
                If .User IsNot Nothing Then
                    _Usr = .User.NicknameOrElse
                End If
                _Url = .Url
                _Referrer = .Referrer
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


