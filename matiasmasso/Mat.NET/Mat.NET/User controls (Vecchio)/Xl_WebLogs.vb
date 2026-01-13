Public Class Xl_WebLogs

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToReset(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        email
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOWebLogBrowse))
        Dim iRowIndex As Integer = DataGridView1.CurrentCell.RowIndex
        Dim iColIndex As Integer = DataGridView1.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = DataGridView1.FirstDisplayedScrollingRowIndex

        _ControlItems = New ControlItems
        For Each oItem As DTOWebLogBrowse In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()

        If iRowIndex >= DataGridView1.Rows.Count Then
            iRowIndex = DataGridView1.Rows.Count - 1
        End If
        DataGridView1.FirstDisplayedScrollingRowIndex = iFirstRow
        DataGridView1.CurrentCell = DataGridView1.Rows(iRowIndex).Cells(iColIndex)

    End Sub

    Public ReadOnly Property Value As DTOWebLogBrowse
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOWebLogBrowse = oControlItem.Source
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
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.email)
                .HeaderText = "email"
                .DataPropertyName = "email"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "Nom"
                .DataPropertyName = "Nom"
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
            Dim oMenu_Log As New Menu_WebLogBrowse(CurrentControlItem.Source)
            AddHandler oMenu_Log.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Log.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)
        oContextMenu.Items.Add("reset", Nothing, AddressOf Do_Reset)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Reset()
        Dim rc As MsgBoxResult = MsgBox("eliminem tots els logs d'aquest post des del principi?", MsgBoxStyle.YesNoCancel)
        If rc = MsgBoxResult.Yes Then
            RaiseEvent RequestToReset(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOWebLogBrowse = CurrentControlItem.Source
        'Dim oFrm As New Frm_User(oSelectedValue.User)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Select Case oControlItem.Source.User.Rol.Id
            Case Rol.Ids.Admin, Rol.Ids.SalesManager, Rol.Ids.Accounts
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            Case Rol.Ids.Cli
                oRow.DefaultCellStyle.BackColor = Color.LightGreen
            Case Rol.Ids.Rep, Rol.Ids.Comercial
                oRow.DefaultCellStyle.BackColor = Color.Yellow
            Case Rol.Ids.Manufacturer
                oRow.DefaultCellStyle.BackColor = Color.LightSalmon
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
        Property Source As DTOWebLogBrowse

        Property Fch As Date
        Property email As String
        Property Nom As String

        Public Sub New(oWebLog As DTOWebLogBrowse)
            MyBase.New()
            _Source = oWebLog
            With oWebLog
                _Fch = .Fch
                _email = .User.EmailAddress
                _Nom = .User.Nom
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
