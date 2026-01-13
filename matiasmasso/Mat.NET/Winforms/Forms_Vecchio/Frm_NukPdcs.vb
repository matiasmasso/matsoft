

Public Class Frm_NukPdcs

    Private NUK_ID As Integer = 104
    Private mAllowEvents As Boolean

    Private Enum Cols
        Yea
        Pdc
        FchPdc
        alb
        FchAlb
        Clx
    End Enum


    Private Sub Frm_NukPdcs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT PDC.YEA, PDC.pdc, PDC.fch, MIN(CAST(ARC.ye1 AS VARCHAR) + '/' + CAST(ARC.alb AS VARCHAR)) AS ALB, MIN(ARC.fch) AS FchAlb, CLX.clx " _
        & "FROM PDC INNER JOIN " _
        & "CLX ON PDC.Emp = CLX.Emp AND PDC.cli = CLX.cli INNER JOIN " _
        & "PNC ON PDC.Guid = PNC.PdcGuid INNER JOIN " _
        & "ART ON PNC.ArtGuid = Art.Guid LEFT OUTER JOIN " _
        & "ARC ON Arc.PdcGuid = PNC.PdcGuid " _
        & "WHERE ART.emp = @EMP AND ART.tpa = @NUK AND PNC.Cod = 2 "

        If CheckboxHideShipped.Checked Then
            SQL = SQL & " AND ARC.ALB IS NULL "
        End If

        SQL = SQL & "GROUP BY PDC.yea, PDC.pdc, PDC.fch, CLX.clx " _
        & "ORDER BY PDC.yea DESC, PDC.pdc DESC"

        mAllowEvents = False
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@NUK", NUK_ID)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oDs.Tables(0).DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = True

            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Pdc)
                .HeaderText = "comanda"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.FchPdc)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.alb)
                If CheckboxHideShipped.Checked Then
                    .Visible = False
                Else
                    .Visible = True
                    .HeaderText = "albará"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 75
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Format = "#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            End With
            With .Columns(Cols.FchAlb)
                If CheckboxHideShipped.Checked Then
                    .Visible = False
                Else
                    .Visible = True
                    .HeaderText = "data"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 65
                    .DefaultCellStyle.Format = "dd/MM/yy"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Function CurrentPdc() As Pdc
        Dim oPdc As Pdc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = DataGridView1.CurrentRow.Cells(Cols.Yea).Value
            Dim iPdc As Integer = DataGridView1.CurrentRow.Cells(Cols.Pdc).Value
            oPdc = Pdc.FromNum(BLL.BLLApp.Emp, iYea, iPdc)
        End If
        Return oPdc
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oPdc As Pdc = CurrentPdc()

        If oPdc IsNot Nothing Then
            Dim oMenu_Pdc As New Menu_Pdc(oPdc)
            AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pdc.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mAllowEvents = False
        Dim i As Integer = DataGridView1.CurrentRow.Index
        Dim j As Integer = DataGridView1.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = DataGridView1.FirstDisplayedScrollingRowIndex()
        LoadGrid()
        DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
        mAllowEvents = True

        If i > DataGridView1.Rows.Count - 1 Then
            DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
        Else
            DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(j)
        End If
    End Sub

    Private Sub CheckboxHideShipped_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckboxHideShipped.CheckedChanged
        If mAllowEvents Then
            RefreshRequest(sender, e)
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub
End Class