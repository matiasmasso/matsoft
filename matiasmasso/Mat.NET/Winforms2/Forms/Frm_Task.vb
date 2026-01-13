Public Class Frm_Task

    Private _Task As DTOTask
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Schedule
        Logs
    End Enum

    Public Sub New(value As DTOTask)
        MyBase.New()
        Me.InitializeComponent()
        _Task = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Task.Load(_Task, exs) Then
            UIHelper.LoadComboFromEnum(ComboBoxCod, GetType(DTOTask.Cods), , _Task.Cod)
            LoadForm()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadForm()
        With _Task
            TextBoxNom.Text = .Nom
            TextBoxDsc.Text = .Dsc
            If .LastLog Is Nothing Then
                TextBoxLastLogFch.Clear()
                PictureBoxLastLogResult.Image = Nothing
                TextBoxResult.Text = ""
            Else
                TextBoxLastLogFch.Text = Format(.LastLog.Fch.LocalDateTime, "dd/MM/yy hh:mm")
                PictureBoxLastLogResult.Image = IconHelper.TaskResult(.lastLog)
                TextBoxResult.Text = .lastLog.resultMsg
            End If

            Dim DtNextRun As DateTimeOffset = _Task.NextRun
            If DtNextRun <> Nothing Then
                TextBoxNextRunFch.Text = Format(DtNextRun.LocalDateTime, "dd/MM/yy hh:mm")
                Dim oTimeSpan As TimeSpan = _Task.TimeToNextRun()
                Dim sTimeSpan As String = DTOTaskSchedule.SpanText(DtNextRun, Current.Session.Lang)
                LabelTimeSpan.Text = sTimeSpan

            End If

            CheckBoxNotBefore.Checked = (.NotBefore <> Nothing)
            CheckBoxNotAfter.Checked = (.NotAfter <> Nothing)
            CheckBoxEnabled.Checked = .Enabled
            If .NotBefore <> Nothing Then
                DateTimePickerNotBefore.Visible = True
                DateTimePickerNotBefore.Value = .NotBefore.LocalDateTime
            End If
            If .NotAfter <> Nothing Then
                DateTimePickerNotAfter.Visible = True
                DateTimePickerNotAfter.Value = .NotAfter.LocalDateTime
            End If

            SetNextRun()
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew

            Dim oMenuTask As New Menu_Task(_Task)
            AddHandler oMenuTask.AfterUpdate, AddressOf RefrescaTask
            ArxiuToolStripMenuItem.DropDownItems.Clear()
            ArxiuToolStripMenuItem.DropDownItems.AddRange(oMenuTask.Range)
            ArxiuToolStripMenuItem.DropDownItems.Add("-")
            ArxiuToolStripMenuItem.DropDownItems.Add("refresca", Nothing, AddressOf ReloadRequest)
        End With

        _AllowEvents = True
    End Sub


    Private Sub SetNextRun()
        UpdateWithFormValues()
        Dim DtNextRun As DateTimeOffset = _Task.NextRun
        If DtNextRun = Nothing Then
            TextBoxNextRunFch.Clear()
            LabelTimeSpan.Text = ""
        Else
            TextBoxNextRunFch.Text = Format(DtNextRun.LocalDateTime, "dd/MM/yy HH:mm")
            Dim sTimeSpan As String = DTOTaskSchedule.SpanText(DtNextRun, Current.Session.Lang)
            LabelTimeSpan.Text = sTimeSpan
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxDsc.TextChanged,
          DateTimePickerNotBefore.ValueChanged,
           DateTimePickerNotAfter.ValueChanged,
            CheckBoxEnabled.CheckedChanged

        If _AllowEvents Then
            SetNextRun()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxNotBefore_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxNotBefore.CheckedChanged
        If _AllowEvents Then
            DateTimePickerNotBefore.Visible = CheckBoxNotBefore.Checked
            ButtonOk.Enabled = True
            SetNextRun()
        End If
    End Sub
    Private Sub CheckBoxNotAfter_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxNotAfter.CheckedChanged
        If _AllowEvents Then
            DateTimePickerNotAfter.Visible = CheckBoxNotAfter.Checked
            ButtonOk.Enabled = True
            SetNextRun()
        End If
    End Sub

    Private Sub UpdateWithFormValues()
        With _Task
            .Nom = TextBoxNom.Text
            .Cod = ComboBoxCod.SelectedValue
            .Dsc = TextBoxDsc.Text
            If CheckBoxNotBefore.Checked Then
                Dim DtFch = DateTimePickerNotBefore.Value
                Dim DtFchOffset As New DateTimeOffset(DtFch.Year, DtFch.Month, DtFch.Day, 0, 0, 0, TimeZoneInfo.Local.GetUtcOffset(DtFch))
                .NotBefore = DtFchOffset
            Else
                .NotBefore = Nothing
            End If
            If CheckBoxNotAfter.Checked Then
                Dim DtFch = DateTimePickerNotAfter.Value
                Dim DtFchOffset As New DateTimeOffset(DtFch.Year, DtFch.Month, DtFch.Day, 0, 0, 0, TimeZoneInfo.Local.GetUtcOffset(DtFch))
                .NotAfter = DtFchOffset
            Else
                .NotAfter = Nothing
            End If
            .Enabled = CheckBoxEnabled.Checked
        End With
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UpdateWithFormValues()
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.Task.Update(_Task, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Task))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta tasca?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.Task.Delete(_Task, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Task))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la tasca")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Schedule
                RefrescaTaskSchedules()
            Case Tabs.Logs
                RefrescaTaskLogs()
        End Select
    End Sub

    Private Sub Xl_TaskSchedules1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_TaskSchedules1.RequestToAddNew
        Dim oSched As New DTOTaskSchedule
        oSched.Task = _Task

        Dim oFrm As New Frm_TaskSchedule(oSched)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaTaskSchedules
        oFrm.Show()
    End Sub


    Private Async Sub RefrescaTaskSchedules()
        Dim exs As New List(Of Exception)
        _Task.Schedules = Await FEB.TaskSchedules.All(_Task, exs)
        If exs.Count = 0 Then
            Xl_TaskSchedules1.Load(_Task)
            SetNextRun()
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Task))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub RefrescaTaskLogs()
        Dim exs As New List(Of Exception)
        Dim oLogs = Await FEB.TaskLogs.All(_Task, exs)
        If exs.Count = 0 Then
            Xl_TaskLogs1.Load(oLogs)
            Dim oLog As DTOTaskLog = Xl_TaskLogs1.Value
            If oLog IsNot Nothing Then
                TextBoxMsg.Text = oLog.ResultMsg
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub RefrescaTask(sender As Object, e As MatEventArgs)
        _Task = e.Argument
        LoadForm()
    End Sub

    Private Sub ReloadRequest()
        _Task.IsLoaded = False
        Dim exs As New List(Of Exception)
        If FEB.Task.Load(_Task, exs) Then
            LoadForm()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_TaskLogs1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_TaskLogs1.ValueChanged
        Dim oLog As DTOTaskLog = e.Argument
        Dim exs As New List(Of Exception)
        oLog = Await FEB.TaskLog.Find(oLog.Guid, exs)
        If exs.Count = 0 Then
            TextBoxMsg.Text = oLog.ResultMsg
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_TaskSchedules1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_TaskSchedules1.RequestToRefresh
        RefrescaTaskSchedules()
    End Sub

    Private Sub Xl_TaskLogs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_TaskLogs1.RequestToRefresh
        RefrescaTaskLogs()
    End Sub

    Private Async Sub ButtonExecuteNow_Click(sender As Object, e As EventArgs)
        _Task.LastLog = Nothing
        Dim exs As New List(Of Exception)
        If Await FEB.Task.Update(_Task, exs) Then
            LoadForm()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class


