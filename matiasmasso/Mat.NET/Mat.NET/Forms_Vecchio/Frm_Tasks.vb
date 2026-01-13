

Public Class Frm_Tasks
    Private mAllowEvents As Boolean

    Private Enum Cols
        Id
        Enabled
        Nom
        TimeToNextRun
    End Enum


    Private Sub Frm_Tasks_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadGrid()
    End Sub


    Private Sub LoadGrid()
        mAllowEvents = False

        Dim oTb As New DataTable
        With oTb.Columns
            .Add(New DataColumn("Id", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("Check", System.Type.GetType("System.Boolean")))
            .Add(New DataColumn("Nom", System.Type.GetType("System.String")))
            .Add(New DataColumn("TimeToNextRun", System.Type.GetType("System.String")))
        End With

        Dim oRow As DataRow
        For Each itm As MaxiSrvr.Task In Tasks.AllTasks
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)

            With itm
                oRow(Cols.Id) = .Id
                oRow(Cols.Enabled) = .Enabled
                oRow(Cols.Nom) = .Nom
                oRow(Cols.TimeToNextRun) = .TimeToNextRunFormated
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
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Enabled)
                .HeaderText = ""
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .ReadOnly = True
            End With
            With .Columns(Cols.TimeToNextRun)
                .HeaderText = "propera vegada"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .ReadOnly = True
            End With
        End With

        mAllowEvents = True
        SetContextMenu()
    End Sub


    Private Function CurrentItem() As MaxiSrvr.Task
        Dim oRetVal As MaxiSrvr.Task = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oid As MaxiSrvr.Task.Ids = oRow.Cells(Cols.Id).Value
            oRetVal = New MaxiSrvr.Task(oid)
        End If
        Return oRetVal
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oTask As MaxiSrvr.Task = CurrentItem()
        If oTask IsNot Nothing Then
            oContextMenu.Items.Add("zoom", Nothing, AddressOf Zoom)
            oContextMenu.Items.Add("executa", Nothing, AddressOf Exec)
        End If
        oContextMenu.Items.Add("executa les tasques vençudes", Nothing, AddressOf DoExecDueTasks)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Task(CurrentItem)
        AddHandler oFrm.AfterUpdate, AddressOf refreshRequest
        oFrm.Show()
    End Sub

    Private Sub Exec(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs as new list(Of Exception)
        Dim oTask As MaxiSrvr.Task = CurrentItem()

        Dim oResult As EventLogEntryType = oTask.Execute( exs)
        RefreshRequest(sender, e)
        MsgBox(oTask.Nom & vbCrLf & BLL.Defaults.ExsToMultiline(exs), IIf(oResult = EventLogEntryType.Information, MsgBoxStyle.Information, MsgBoxStyle.Exclamation), "MAT.NET")
    End Sub

    Private Sub DoExecDueTasks(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iCount As Integer = Tasks.ExecuteDueTasks()
        RefreshRequest(sender, e)
        MsgBox("tasques executades", MsgBoxStyle.Information, "MAT.NET")
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
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.TimeToNextRun

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
                    Dim oTask As MaxiSrvr.Task = CurrentItem()
                    oTask.Enabled = Not oTask.Enabled
                    oTask.Update()
                    RefreshRequest(sender, e)
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

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then SetContextMenu()
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom(sender, e)
    End Sub

End Class