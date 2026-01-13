Imports Winforms

Public Class Frm_EdiversaFiles
    Private _Files As List(Of DTOEdiversaFile)
    Private _ServerPendingFiles As List(Of DTOEdiversaFile)
    Private _IncludeClosed As Boolean

    Private Enum Tabs
        Inputs
        Outputs
        ServerPending
    End Enum

    Private Async Sub Frm_EdiversaFiles_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        With NumericUpDownYear
            .Maximum = Today.Year
            .Value = Today.Year
        End With

        If Await RefrescaTags(exs) Then
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function RefrescaTags(exs As List(Of Exception)) As Task(Of Boolean)
        Dim ioCod = CurrentIoCod()
        Dim tags = Await FEB2.EdiversaFiles.Tags(exs, GlobalVariables.Emp, CurrentYear, ioCod)
        Select Case ioCod
            Case DTOEdiversaFile.IOcods.inbox
                Xl_EdiversaFileTagsIn.Load(tags)
            Case DTOEdiversaFile.IOcods.outbox
                Xl_EdiversaFileTagsOut.Load(tags)
        End Select
        Return exs.Count = 0
    End Function


    Private Function CurrentYear() As Integer
        Return NumericUpDownYear.Value
    End Function

    Private Function CurrentIoCod() As DTOEdiversaFile.IOcods
        Dim retval As DTOEdiversaFile.IOcods = DTOEdiversaFile.IOcods.notSet
        Select Case TabControl1.SelectedIndex
            Case Tabs.Inputs
                retval = DTOEdiversaFile.IOcods.inbox
            Case Tabs.Outputs
                retval = DTOEdiversaFile.IOcods.outbox
        End Select
        Return retval
    End Function

    Private Async Function refresca() As Task
        ProgressBar1.Visible = True
        Dim exs As New List(Of Exception)
        Dim ioCod = CurrentIoCod()
        _Files = Await FEB2.EdiversaFiles.All(exs, GlobalVariables.Emp, NumericUpDownYear.Value, CurrentIoCod, CurrentTag)
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            Select Case ioCod
                Case DTOEdiversaFile.IOcods.inbox
                    Dim sTag As String = Xl_EdiversaFileTagsIn.Value
                    If sTag > "" Then Xl_EdiversaFilesIn.Load(_Files)
                Case DTOEdiversaFile.IOcods.outbox
                    Dim sTag As String = Xl_EdiversaFileTagsOut.Value
                    If sTag > "" Then Xl_EdiversaFilesOut.Load(_Files)
            End Select
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Function CurrentTag() As String
        Dim retval As String = ""
        Select Case TabControl1.SelectedIndex
            Case Tabs.Inputs
                retval = Xl_EdiversaFileTagsIn.Value
            Case Tabs.Outputs
                retval = Xl_EdiversaFileTagsOut.Value
        End Select
        Return retval
    End Function

    Private Async Sub Xl_EdiversaFileTags_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_EdiversaFileTagsIn.ValueChanged, Xl_EdiversaFileTagsOut.ValueChanged
        Await refresca()
    End Sub


    Private Sub Xl_EdiversaFileTagsServerPending_ValueChanged(sender As Object, e As MatEventArgs)
        Dim tag As String = e.Argument
        refrescaServerPending(tag)
    End Sub


    Private Sub refrescaServerPending(tag As String)
        'Dim oFiles As List(Of DTOEdiversaFile) = _ServerPendingFiles.Where(Function(x) x.Tag = tag).ToList
        'Xl_EdiversaFilesServerPending.Load(oFiles)
    End Sub


    Private Async Sub IncludeClosedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncludeClosedToolStripMenuItem.Click
        _IncludeClosed = IncludeClosedToolStripMenuItem.Checked
        Await refresca()
    End Sub

    Private Async Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        Await refresca()
    End Sub

    Private Async Sub Xl_EdiversaFiles_RequestToRefresh(sender As Object, e As MatEventArgs) Handles _
        Xl_EdiversaFilesIn.RequestToRefresh,
         Xl_EdiversaFilesOut.RequestToRefresh

        Await refresca()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_EdiversaFilesIn.Filter = e.Argument
        Xl_EdiversaFilesOut.Filter = e.Argument
        'Xl_EdiversaFilesServerPending.Filter = e.Argument
    End Sub

    Private Sub Xl_EdiversaFiles_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles _
        Xl_EdiversaFilesIn.RequestToToggleProgressBar,
        Xl_EdiversaFilesOut.RequestToToggleProgressBar

        ProgressBar1.Visible = e.Argument
    End Sub

    Private Async Sub Xl_EdiversaFilesIn_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_EdiversaFilesIn.RequestToAddNew
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar fitxer Ediversa"
            If .ShowDialog = DialogResult.OK Then
                Dim oEdiversaFile = DTOEdiversaFile.Factory(.FileName)
                Dim exs As New List(Of Exception)
                If Await FEB2.EdiversaFileSystem.SaveInboxFile(oEdiversaFile, exs) Then
                    Await refresca()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Async Sub NumericUpDownYear_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownYear.ValueChanged
        Dim exs As New List(Of Exception)
        If Await RefrescaTags(exs) Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.Inputs
                    Xl_EdiversaFileTagsOut.clear()
                Case Tabs.Outputs
                    Xl_EdiversaFileTagsIn.Clear()
            End Select
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        If Await RefrescaTags(exs) Then
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class