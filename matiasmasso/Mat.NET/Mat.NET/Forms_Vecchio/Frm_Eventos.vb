

Public Class Frm_Eventos
    Private mAllowEvents As Boolean

    Private Enum Cols
        Id
        Nom
        FchFrom
        FchTo
    End Enum

    Private Sub Frm_Eventos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()

        Dim SQL As String = "SELECT ID,ESP,FCHFROM,FCHTO " _
        & "FROM EVENT " _
        & "ORDER BY FCHFROM DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "evento"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.FchFrom)
                .HeaderText = "desde"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.FchTo)
                .HeaderText = "hasta"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End With
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Function CurrentItem() As Evento
        Dim oRetVal As Evento = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = New Evento(CInt(oRow.Cells(Cols.Id).Value))
        End If
        Return oRetVal
    End Function

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oEvento As Evento = CurrentItem()

        If oEvento IsNot Nothing Then
            Dim oMenuItem As New ToolStripMenuItem("zoom", Nothing, AddressOf DoZoom)
            oContextMenu.Items.Add(oMenuItem)

            'oMenuItem = New ToolStripMenuItem("seguiment", Nothing, AddressOf DoFollowUp)
            'oContextMenu.Items.Add(oMenuItem)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DoZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Evento(CurrentItem)
        AddHandler oFrm.afterupdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        DoZoom(sender, e)
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub
End Class