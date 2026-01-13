

Public Class Frm_InfoJobs
    Private mIdx As Integer
    Private mContactKeys As ContactKeys
    Private mTb As DataTable
    Private mStartPos As Point
    Private mArrastrando As Boolean
    Public Event SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Nom
        Ico
        Ex
    End Enum


    Private Sub Frm_InfoJobs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT ID,NOM,OBSOLETO FROM INFOJOB ORDER BY ID DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        mTb = oDs.Tables(0)

        'afegeix columna pdf
        Dim oCol As DataColumn = mTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

        With DataGridView1
            .DataSource = mTb
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Ex)
                .Visible = False
            End With
        End With

    End Sub

    Private Function CurrentJob() As InfoJob
        Dim oJob As InfoJob = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim JobId As Integer = oRow.Cells(Cols.Id).Value
        oJob = New InfoJob(JobId)
        Return oJob
    End Function

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Nom
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Ex).Value Then
                    e.CellStyle.BackColor = Color.LightGray
                End If
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Ex).Value = True Then
                    e.Value = My.Resources.del
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        zOOM()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                Zoom()
                e.Handled = True
        End Select
    End Sub

    Private Sub Zoom(Optional ByVal oJob As InfoJob = Nothing)
        If oJob Is Nothing Then oJob = CurrentJob()
        Dim oFrm As New Frm_InfoJob
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .InfoJob = oJob
            .Show()
        End With
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub ToolStripButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonAdd.Click
        Dim oJob As New InfoJob
        Zoom(oJob)
    End Sub

End Class