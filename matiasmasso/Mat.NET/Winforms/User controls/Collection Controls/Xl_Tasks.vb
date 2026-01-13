Public Class Xl_Tasks
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOTask)
    Private _DefaultValue As DTOTask
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ItemCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Check
        Nom
        LastRun
        Ico
        NextRun
        TimeToNextRun
    End Enum

    Public Shadows Sub Load(values As List(Of DTOTask), Optional oDefaultValue As DTOTask = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOTask) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOTask In oFilteredValues
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

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOTask
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOTask = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Check), DataGridViewImageColumn)
            .DataPropertyName = "Check"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.LastRun)
            .HeaderText = "Torn anterior"
            .DataPropertyName = "LastRun"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.NextRun)
            .HeaderText = "Programat per"
            .DataPropertyName = "NextRun"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.TimeToNextRun)
            .HeaderText = "d'aquí a"
            .DataPropertyName = "TimeToNextRun"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        Dim oTimer As New Timers.Timer(30000) 'cada mig minut
        AddHandler oTimer.Elapsed, AddressOf HandleTimer
        oTimer.Start()

        _AllowEvents = True
    End Sub

    Private Async Sub HandleTimer(sender As Object, e As EventArgs)
        Await Task.Run(Sub()
                           RefreshNextRun()
                       End Sub)
    End Sub

    Private Delegate Sub DelegateRefreshNextRun(sFch As String, sTimeSpan As String)

    Private Sub RefreshNextRun()
        If InvokeRequired Then
            Invoke(Sub() MyBase.SuspendLayout())
        Else
            MyBase.SuspendLayout()
        End If

        For Each oControlItem As ControlItem In _ControlItems
            Dim item As DTOTask = oControlItem.Source
            oControlItem.NextRun = item.NextRun()
        Next

        If InvokeRequired Then
            Invoke(Sub() MyBase.Refresh())
            Invoke(Sub() MyBase.ResumeLayout(True))
        Else
            MyBase.Refresh()
            MyBase.ResumeLayout()
        End If


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

    Private Function SelectedItems() As List(Of DTOTask)
        Dim retval As New List(Of DTOTask)
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
            Dim oMenu_Task As New Menu_Task(SelectedItems)
            AddHandler oMenu_Task.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Task.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("executa totes les pendents", Nothing, AddressOf Do_ExecuteAll)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf onRefreshRequest)
        oContextMenu.Items.Add("-")
        oContextMenu.Items.Add("activa-ho tot", Nothing, AddressOf Do_CheckAll)
        oContextMenu.Items.Add("activa nomes la sel·lecció", Nothing, AddressOf Do_CheckSelection)
        oContextMenu.Items.Add("desactiva-ho tot", Nothing, AddressOf Do_CheckNone)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_CheckAll()
        For Each controlitem In _ControlItems
            controlitem.Checked = True
        Next
        MyBase.Refresh()
    End Sub

    Private Sub Do_CheckSelection()
        For Each controlitem In _ControlItems
            controlitem.Checked = False
        Next
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim controlItem As ControlItem = oRow.DataBoundItem
            controlItem.Checked = True
        Next
        MyBase.Refresh()
    End Sub

    Private Sub Do_CheckNone()
        For Each controlitem In _ControlItems
            controlitem.Checked = False
        Next
        MyBase.Refresh()
    End Sub
    Private Sub onRefreshRequest()
        MyBase.RefreshRequest(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Async Sub Do_ExecuteAll()
        Dim exs As New List(Of Exception)
        Dim oExecutedTasks = Await FEB2.Tasks.ExecuteAsync(Current.Session.User, exs)
        Dim failedTasks = oExecutedTasks.Where(Function(x) x.LastLog.ResultCod = DTOTask.ResultCods.Failed Or x.LastLog.ResultCod = DTOTask.ResultCods.DoneWithErrors)
        Dim sReport = DTOTask.Report(oExecutedTasks)
        If failedTasks.Count = 0 Then
            MsgBox(sReport, MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(sReport)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOTask = CurrentControlItem.Source
            Dim oFrm As New Frm_Task(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

#Region "Check"
    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MyBase.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem

                Dim oCurrentCheckState As CheckState = oControlItem.Checked
                Dim oNewCheckState As CheckState = IIf(oControlItem.Checked = CheckState.Checked, CheckState.Unchecked, CheckState.Checked)
                Dim oArgs As New ItemCheckEventArgs(e.RowIndex, oNewCheckState, oCurrentCheckState)
                RaiseEvent ItemCheck(Me, oArgs)
        End Select
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oControlItem As ControlItem = MyBase.Rows(e.RowIndex).DataBoundItem
                Select Case oControlItem.Checked
                    Case CheckState.Checked
                        e.Value = My.Resources.Checked13
                    Case CheckState.Unchecked
                        e.Value = My.Resources.UnChecked13
                    Case CheckState.Indeterminate
                        e.Value = My.Resources.CheckedGrayed13
                End Select
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oTask As DTOTask = oControlItem.Source
                If oTask.LastLog IsNot Nothing Then
                    Dim oIco As Image = IconHelper.TaskResult(oTask.lastLog)
                    e.Value = oIco
                End If

            Case Cols.LastRun
                Dim oControlItem As ControlItem = MyBase.Rows(e.RowIndex).DataBoundItem
                Dim DtFch As DateTimeOffset = oControlItem.LastRun
                e.Value = Format(DtFch.LocalDateTime, "dd/MM/yy HH:mm")

            Case Cols.NextRun
                Dim oControlItem As ControlItem = MyBase.Rows(e.RowIndex).DataBoundItem
                Dim DtFch As DateTimeOffset = oControlItem.NextRun
                e.Value = Format(DtFch.LocalDateTime, "dd/MM/yy HH:mm")

            Case Cols.TimeToNextRun
                Dim oControlItem As ControlItem = MyBase.Rows(e.RowIndex).DataBoundItem
                Dim DtFch As DateTimeOffset = oControlItem.NextRun
                Dim oTimeSpan As TimeSpan = DtFch - DateTimeOffset.Now
                If oTimeSpan.Minutes < 1 Then
                    e.Value = "inminent"
                Else
                    e.Value = String.Format("{0:00}:{1:00}:{2:00}", oTimeSpan.Days, oTimeSpan.Hours, oTimeSpan.Minutes)
                End If
        End Select
    End Sub

#End Region



    Protected Class ControlItem
        Property Source As DTOTask

        Property Checked As CheckState
        Property Nom As String
        Property LastRun As DateTimeOffset
        Property NextRun As DateTimeOffset

        Public Sub New(value As DTOTask)
            MyBase.New()
            _Source = value
            With value
                _Checked = IIf(.Enabled, CheckState.Checked, CheckState.Unchecked)
                _Nom = .nom
                If value.lastLog IsNot Nothing Then
                    _LastRun = value.lastLog.fch.LocalDateTime
                End If
                _NextRun = value.NextRun().LocalDateTime
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


