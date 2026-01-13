

Public Class Frm_Task
    Private mTask As MaxiSrvr.Task = Nothing
    Private mSchedules As TaskSchedules = Nothing
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        Enabled
        Nom
        NextRun
    End Enum

    Public Sub New(ByVal oTask As MaxiSrvr.Task)
        MyBase.New()
        Me.InitializeComponent()
        mTask = oTask
        mSchedules = oTask.Schedules

        With mTask
            TextBoxNom.Text = .Nom
            TextBoxDsc.Text = .Dsc
            CheckBoxEnabled.Checked = .Enabled
            If .LastRun = Date.MinValue Then
                LabelLastRun.Text = "primera vegada"
            Else
                LabelLastRun.Text = "ultima vegada " & Format(.LastRun, "dd/MM/yy")
            End If
            If .NotBefore > Date.MinValue Then
                CheckBoxNotBefore.Checked = True
                DateTimePickerNotBefore.Visible = True
                DateTimePickerNotBefore.Value = .NotBefore
            End If
            If .NotAfter < Date.MaxValue Then
                CheckBoxNotAfter.Checked = True
                DateTimePickerNotAfter.Visible = True
                DateTimePickerNotAfter.Value = .NotAfter
            End If
            LabelNextRun.Text = .TimeToNextRunFormated
        End With
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        mAllowEvents = False

        Dim oTb As New DataTable
        With oTb.Columns
            .Add(New DataColumn("Guid", System.Type.GetType("System.Guid")))
            .Add(New DataColumn("Check", System.Type.GetType("System.Boolean")))
            .Add(New DataColumn("Nom", System.Type.GetType("System.String")))
            .Add(New DataColumn("NextRun", System.Type.GetType("System.DateTime")))
        End With

        Dim oRow As DataRow
        For Each itm As TaskSchedule In mSchedules
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With itm
                oRow(Cols.Guid) = .Guid
                oRow(Cols.Enabled) = .Enabled
                oRow(Cols.Nom) = .Dsc
                oRow(Cols.NextRun) = TaskSchedule.NextRun(mTask, itm)
            End With
        Next

        With DataGridView1
            With .RowTemplate
                '.Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Enabled)
                .HeaderText = ""
                .Width = 20
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "dies"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.NextRun)
                .HeaderText = "Data"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End With

        mAllowEvents = True
        setcontextMenu()
    End Sub

    Private Function CurrentItem() As TaskSchedule
        Dim oRetVal As TaskSchedule = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = mSchedules(oRow.Index)
        End If
        Return oRetVal
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oSched As TaskSchedule = CurrentItem()
        If oSched IsNot Nothing Then
            oContextMenu.Items.Add("zoom", Nothing, AddressOf SchedZoom)
            oContextMenu.Items.Add("eliminar", My.Resources.del, AddressOf SchedDelete)
        End If
        oContextMenu.Items.Add("afegir", My.Resources.clip, AddressOf SchedAddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub SchedZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSched As TaskSchedule = CurrentItem()
        Dim oFrm As New Frm_TaskSchedule(oSched)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub SchedAddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSched As New TaskSchedule()
        Dim oFrm As New Frm_TaskSchedule(oSched)
        AddHandler oFrm.AfterUpdate, AddressOf AfterSchedUpdate
        oFrm.Show()
    End Sub

    Private Sub SchedDelete(ByVal sender As Object, ByVal e As System.EventArgs)
        mSchedules.Remove(DataGridView1.CurrentRow.Index)
        RefreshRequest(sender, e)
    End Sub

    Private Sub AfterSchedUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        mSchedules.Add(sender)
        RefreshRequest(sender, e)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Enabled

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
        LabelNextRun.Text = GetTaskFromForm.TimeToNextRunFormated
        ButtonOk.Enabled = True
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.NextRun

                Dim BlEnabled As Boolean = DataGridView1.Rows(e.RowIndex).Cells(Cols.Enabled).Value
                If BlEnabled Then
                    e.CellStyle.ForeColor = Color.Black
                Else
                    e.CellStyle.ForeColor = Color.Red
                End If
        End Select
    End Sub


    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Enabled
                If mAllowEvents Then
                    Dim oSched As TaskSchedule = CurrentItem()
                    oSched.Enabled = Not oSched.Enabled
                    RefreshRequest(sender, e)
                    ButtonOk.Enabled = True
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Enabled
                If mAllowEvents Then
                    DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        SchedZoom(sender, e)
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then SetContextMenu()
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNom.TextChanged, _
     TextBoxDsc.TextChanged, _
      CheckBoxEnabled.CheckedChanged

        If mAllowEvents Then
            LabelNextRun.Text = GetTaskFromForm.TimeToNextRunFormated
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        mTask = GetTaskFromForm()
        mTask.Update()
        RaiseEvent AfterUpdate(mTask, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Function GetTaskFromForm() As MaxiSrvr.Task
        Dim oTask As New MaxiSrvr.Task(mTask.Id, False)
        With oTask
            .Nom = TextBoxNom.Text
            .Dsc = TextBoxDsc.Text
            .Enabled = CheckBoxEnabled.Checked
            If CheckBoxNotBefore.Checked Then
                .NotBefore = DateTimePickerNotBefore.Value
            Else
                .NotBefore = Date.MinValue
            End If
            If CheckBoxNotAfter.Checked Then
                .NotAfter = DateTimePickerNotAfter.Value
            Else
                .NotAfter = Date.MaxValue
            End If
            .Schedules = mSchedules
        End With
        Return oTask
    End Function

    Private Sub CheckBoxNotBefore_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNotBefore.CheckedChanged
        If mAllowEvents Then
            DateTimePickerNotBefore.Visible = CheckBoxNotBefore.Checked
            LabelNextRun.Text = GetTaskFromForm.TimeToNextRunFormated
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxNotAfter_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNotAfter.CheckedChanged
        If mAllowEvents Then
            DateTimePickerNotAfter.Visible = CheckBoxNotAfter.Checked
            LabelNextRun.Text = GetTaskFromForm.TimeToNextRunFormated
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub DateTime_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DateTimePickerNotBefore.ValueChanged, _
     DateTimePickerNotAfter.ValueChanged
        If mAllowEvents Then
            LabelNextRun.Text = GetTaskFromForm.TimeToNextRunFormated
            ButtonOk.Enabled = True
        End If

    End Sub
End Class