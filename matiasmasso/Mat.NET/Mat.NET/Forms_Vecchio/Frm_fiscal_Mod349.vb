

Public Class Frm_fiscal_Mod349
    Private mLang As DTOLang = BLL.BLLApp.Lang
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Cols
        CliNum
        CliNom
        Nif
        Tot
        Mes
    End Enum

    Private Sub Frm_fiscal_Mod349_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadYeas()
        LoadGrid()
    End Sub

    Private Sub LoadYeas()
        Dim SQL As String = "SELECT YEA FROM INTRASTAT WHERE EMP=@EMP GROUP BY YEA ORDER BY YEA DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id)
        With ComboBoxYea
            .DataSource = oDs.Tables(0)
            .DisplayMember = "YEA"
            .ValueMember = "YEA"
        End With
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT CliGral.Cli,CliGral.RaoSocial, CliGral.NIF,SUM(CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) AS TOT "
        For iMes As Integer = 1 To 12
            SQL = SQL & ", SUM(CASE WHEN INTRASTAT.mes = " & iMes & " THEN (CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) ELSE 0 END) AS M" & Format(iMes, "00") & " "
        Next

        SQL = SQL & "FROM  INTRASTAT INNER JOIN " _
                & "IMPORTDTL ON INTRASTAT.Guid = IMPORTDTL.Intrastat INNER JOIN " _
                & "CCA ON IMPORTDTL.Guid = CCA.Guid INNER JOIN " _
                & "CCB ON Ccb.CcaGuid = Cca.Guid INNER JOIN " _
                & "CliGral ON CCB.Emp = CliGral.emp AND CCB.cli = CliGral.Cli " _
                & "WHERE INTRASTAT.EMP=@EMP AND INTRASTAT.yea = @YEA AND CCB.cta LIKE '600%' " _
                & "GROUP BY CliGral.Cli, CliGral.RaoSocial, CliGral.NIF " _
                & "ORDER BY CliGral.RaoSocial"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", ComboBoxYea.SelectedValue(0))
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.CliNum)
                .Visible = False
            End With
            With .Columns(Cols.CliNom)
                .HeaderText = "proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Nif)
                .HeaderText = "NIF"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Tot)
                .HeaderText = "total"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            For iMes As Integer = 1 To 12
                With .Columns(Cols.Mes + iMes - 1)
                    .HeaderText = mLang.MesAbr(iMes)
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            Next
        End With

    End Sub


    Private Sub ComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxYea.SelectedIndexChanged
        LoadGrid()
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub
End Class