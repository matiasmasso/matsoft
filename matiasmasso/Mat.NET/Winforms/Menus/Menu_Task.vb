Public Class Menu_Task
    Inherits Menu_Base

    Private _Tasks As List(Of DTOTask)
    Private _Task As DTOTask


    Public Sub New(ByVal oTasks As List(Of DTOTask))
        MyBase.New()
        _Tasks = oTasks
        If _Tasks.Count > 0 Then
            _Task = _Tasks.First
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oTask As DTOTask)
        MyBase.New()
        _Task = oTask
        _Tasks = New List(Of DTOTask)
        _Tasks.Add(_Task)
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Execute())
        MyBase.AddMenuItem(MenuItem_Advanced())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = (_Tasks.Count = 1)
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Advanced() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avançat"
        oMenuItem.Visible = Current.Session.Rol.IsSuperAdmin
        oMenuItem.DropDownItems.Add(MenuItem_CopyGuid)
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyGuid() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar Guid"
        oMenuItem.Enabled = (_Tasks.Count = 1)
        AddHandler oMenuItem.Click, AddressOf Do_CopyGuid
        Return oMenuItem
    End Function


    Private Function MenuItem_Execute() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Executa"
        Select Case _Task.Cod
            Case DTOTask.Cods.EdiReadFromInbox, DTOTask.Cods.EdiWriteToOutbox
                oMenuItem.Enabled = False
        End Select
        AddHandler oMenuItem.Click, AddressOf Do_Execute
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Task(_Tasks.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Async Sub Do_Execute(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oTask As DTOTask = _Tasks.First

        Dim oTaskResult = Await FEB2.Task.Execute(oTask, Current.Session.User, exs)
        If exs.Count = 0 Then
            FEB2.Task.Load(oTask, exs)
            If exs.Count = 0 Then
                Select Case oTask.LastLog.ResultCod
                    Case DTOTask.ResultCods.Success
                        MsgBox(oTask.LastLog.ResultMsg, MsgBoxStyle.Information)
                    Case DTOTask.ResultCods.Empty
                        MsgBox(oTask.LastLog.ResultMsg, MsgBoxStyle.Exclamation)
                    Case DTOTask.ResultCods.DoneWithErrors, DTOTask.ResultCods.Failed
                        UIHelper.WarnError(exs, oTask.LastLog.ResultMsg)
                End Select
                MyBase.RefreshRequest(Me, New MatEventArgs(oTask))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyGuid(sender As Object, e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        oMenuItem.Image = My.Resources.Copy
        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, _Tasks.First.Guid.ToString())
        Clipboard.SetDataObject(data_object, True)
        oMenuItem.Image = Nothing
    End Sub




End Class

