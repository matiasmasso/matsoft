Public Class Xl_Leads
    Private _values As List(Of DTOUser)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Property SelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        text
    End Enum

    Public Shadows Sub Load(values As List(Of DTOUser), oMode As BLL.Defaults.SelectionModes)
        _values = values
        _SelectionMode = oMode
        _ControlItems = New ControlItems
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOUser
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOUser = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        Dim search As String = TextBoxSearch.Text
        _ControlItems = New ControlItems
        For Each oItem As DTOUser In _values
            If oItem.EmailAddress.ToString.Contains(search) Then
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            End If
        Next

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
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.text)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DataPropertyName = "text"
                .HeaderText = "email"
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

    Private Function SelectedItems() As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
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
            Dim oMenu_User As New Menu_User(SelectedItems.First)
            AddHandler oMenu_User.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_User.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOUser = CurrentControlItem.Source

        Select Case _SelectionMode
            Case BLL.Defaults.SelectionModes.Browse
                Dim oFrm As New Frm_User(oSelectedValue)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case bll.dEFAULTS.SelectionModes.Selection
                Dim oEventArgs As New MatEventArgs(oSelectedValue)
                RaiseEvent onItemSelected(Me, oEventArgs)
        End Select

    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Select Case oControlItem.Source.Rol.Id
            Case Rol.Ids.Cli
                oRow.DefaultCellStyle.BackColor = Color.LightGreen
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.White
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
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

        Property fch As Date
        Property text As String

        Public Sub New(oUser As DTOUser)
            MyBase.New()
            _Source = oUser
            With oUser
                _fch = .FchCreated
                _text = .EmailAddress
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        LoadGrid()
    End Sub


End Class

