Public Class Xl_UserTaskLogs

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOUserTaskLog)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Fch
        Message
        UsrCompleted
        FchCompleted
    End Enum

    Public Shadows Sub Load(values As List(Of DTOUserTaskLog))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems

        MyBase.Columns(Cols.FchCompleted).Visible = _Values.Any(Function(x) x.FchCompleted <> Nothing)
        MyBase.Columns(Cols.UsrCompleted).Visible = MyBase.Columns(Cols.FchCompleted).Visible

        For Each oItem As DTOUserTaskLog In _Values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOUserTaskLog
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOUserTaskLog = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowUserTaskLog.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Message)
            .HeaderText = "Tasca"
            .DataPropertyName = "Message"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.UsrCompleted)
            .HeaderText = "Usuari"
            .DataPropertyName = "UsrCompleted"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchCompleted)
            .HeaderText = "Completada"
            .DataPropertyName = "FchCompleted"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
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

    Private Function SelectedItems() As List(Of DTOUserTaskLog)
        Dim retval As New List(Of DTOUserTaskLog)
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
            Dim oMenu_UserTaskLog As New Menu_UserTaskLog(SelectedItems.First)
            AddHandler oMenu_UserTaskLog.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_UserTaskLog.Range)
            oContextMenu.Items.Add("-")
        End If
        MyBase.AddMenuItemObsolets(oContextMenu)
        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim exs As New List(Of Exception)
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oUserTaskLog As DTOUserTaskLog = CurrentControlItem.Source
            Dim oUserTaskId As DTOUserTaskId = oUserTaskLog.UserTaskId
            Select Case oUserTaskId.Id
                Case DTOUserTaskId.Ids.CheckCustomerPurchaseOrder
                    Dim oOrder = Await FEB2.PurchaseOrder.Find(oUserTaskLog.Ref.Guid, exs)
                    If oOrder Is Nothing Then
                        Dim rc As MsgBoxResult = MsgBox("Ja no existeix aquesta comanda. Donem la tasca per completada?", MsgBoxStyle.OkCancel)
                        If rc = MsgBoxResult.Ok Then
                            Await CompleteTask(oUserTaskLog)
                        End If
                    Else
                        Dim oFrm As New Frm_PurchaseOrder(oOrder)
                        oFrm.Show()
                        Await CompleteTask(oUserTaskLog)
                    End If
                Case Else
                    Dim oFrm As New Frm_UserTaskLog(oUserTaskLog)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
            End Select
        End If
    End Sub

    Private Async Function CompleteTask(oUserTaskLog As DTOUserTaskLog) As Task
        Dim oUser As DTOUser = Current.Session.User
        Dim exs As New List(Of Exception)
        If Not Await FEB2.UserTaskLog.ToggleComplete(exs, oUserTaskLog, oUser) Then
            UIHelper.WarnError(exs, "No s'ha pogut marcar la tasca com a completada")
        End If
    End Function

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_UserTaskLogs_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem.Source.IsCompleted Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub Xl_UserTaskLogs_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oUserTaskLog As DTOUserTaskLog = oControlItem.Source
                e.Value = Icon(oUserTaskLog.UserTaskId)
            Case Cols.FchCompleted
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.FchCompleted = Nothing Then
                    e.Value = String.Empty
                End If
        End Select
    End Sub

    Function Icon(oUserTaskId As DTOUserTaskId) As Image
        Dim retval As Image = Nothing
        Select Case oUserTaskId.Ico
            Case DTOUserTaskId.Icons.Info
                retval = My.Resources.info
            Case DTOUserTaskId.Icons.Warn
                retval = My.Resources.warn
            Case DTOUserTaskId.Icons.Exception
                retval = My.Resources.wrong
            Case DTOUserTaskId.Icons.Check
                retval = My.Resources.Checked13
        End Select
        Return retval
    End Function

    Protected Class ControlItem
        Property Source As DTOUserTaskLog

        Property Fch As Date
        Property FchCompleted As Date
        Property UsrCompleted As String
        Property Message As String

        Public Sub New(value As DTOUserTaskLog)
            MyBase.New()
            _Source = value
            With value
                _Fch = .FchCreated
                _UsrCompleted = DTOUser.NicknameOrElse(.UsrCompleted)
                _FchCompleted = .FchCompleted
                _Message = .Message
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


