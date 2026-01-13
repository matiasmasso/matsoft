Public Class Frm_DownloadLog
    Private mBigFile As maxisrvr.BigFileNew

    Private Enum Cols
        Fch
        Ip
        email
        cliNom
    End Enum

    Public Sub New(ByVal oBigFile As maxisrvr.BigFileNew)
        MyBase.New()
        Me.InitializeComponent()
        mBigFile = oBigFile
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT DG.Fch, DG.IP, EMAIL.Adr, MIN(CLX.clx) AS CliNom " _
                            & "FROM DOWNLOADLOG DG LEFT OUTER JOIN " _
                            & "EMAIL ON DG.EMAILGUID=EMAIL.GUID LEFT OUTER JOIN " _
                            & "EMAIL_CLIS ON EMAIL.Guid=EMAIL_CLIS.EmailGuid LEFT OUTER JOIN " _
                            & "CLX ON EMAIL_CLIS.EMP=CLX.EMP AND EMAIL_CLIS.CLI=CLX.CLI " _
                            & "WHERE DG.BIGFILE=@GUID " _
                            & "GROUP BY DG.FCH, DG.IP, EMAIL.ADR " _
                            & "ORDER BY DG.FCH DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@GUID", mBigFile.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)
        Me.Text = "DOWNLOAD LOG " & oTb.Rows.Count & " baixades"
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Ip)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
            End With
            With .Columns(Cols.email)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.cliNom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        LoadGrid()
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        Dim SQL As String = "SELECT MAX(DOWNLOADLOG.Fch) AS LASTFCH, EMAIL.Adr " _
        & "FROM EMAIL_CLIS INNER JOIN " _
        & "EMAIL ON EMAIL_CLIS.EmailGuid = EMAIL.Guid INNER JOIN " _
        & "CLX ON EMAIL_CLIS.emp = CLX.Emp AND EMAIL_CLIS.cli = CLX.cli INNER JOIN " _
        & "DOWNLOADLOG ON EMAIL.guid = DOWNLOADLOG.EmailGuid " _
        & "WHERE (DOWNLOADLOG.DocGuid LIKE @GUID) " _
        & "GROUP BY EMAIL.Adr " _
        & "ORDER BY LASTFCH DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@GUID", mBigFile.Guid.ToString)
        MatExcel.GetExcelFromDataset(oDs).Visible = True
    End Sub
End Class