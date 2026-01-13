

Public Class frm_Aeat_Mod349
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDs As DataSet

    Private Enum Cols
        Cli
        Pais
        Nif
        Nom
        Eur
        M1
        M2
        M3
    End Enum

    Private Sub frm_Aeat_Mod349_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadYeas()
        LoadGrid()
    End Sub

    Private Sub LoadYeas()
        Dim DtFch As Date = Today.AddMonths(-3) 'trimestre anterior
        Dim iYea As Integer
        With ToolStripComboBoxYea
            .Items.Clear()
            For i As Integer = 0 To 4
                iYea = Today.Year - i
                .Items.Add(iYea.ToString)
                If iYea = DtFch.Year Then ToolStripComboBoxYea.SelectedIndex = i
            Next
        End With
        Dim iQuarter As Integer = maxisrvr.GetQuarterFromFch()
        ToolStripComboBoxQ.SelectedIndex = iQuarter - 1
    End Sub

    Private Function CurrentYear() As Integer
        Return ToolStripComboBoxYea.Text
    End Function

    Private Function CurrentQuarter() As Integer
        Return ToolStripComboBoxQ.SelectedIndex + 1
    End Function

    Private Sub LoadGrid()
        Dim iQM3 As Integer = 3 * CurrentQuarter()
        Dim oLang As DTOLang = BLL.BLLApp.Lang

        Dim sql As String = "SELECT IMPORTHDR.prv, IMPORTHDR.PaisOrigen, SUBSTRING(CliGral.NIF, 3, 100) AS Expr1, CliGral.RaoSocial, " _
        & "0 AS TOT, " _
        & "SUM(CASE WHEN INTRASTAT.MES=@QM3-2 THEN (CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) ELSE 0 END) AS QM1, " _
        & "SUM(CASE WHEN INTRASTAT.MES=@QM3-1 THEN (CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) ELSE 0 END) AS QM2, " _
        & "SUM(CASE WHEN INTRASTAT.MES=@QM3 THEN (CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) ELSE 0 END) AS QM3 " _
        & "FROM IMPORTHDR INNER JOIN " _
        & "IMPORTDTL ON IMPORTHDR.emp = IMPORTDTL.emp AND IMPORTHDR.Yea = IMPORTDTL.Yea AND IMPORTHDR.Id = IMPORTDTL.Id INNER JOIN " _
        & "CCA ON IMPORTDTL.Guid = CCA.Guid INNER JOIN " _
        & "CCB ON Ccb.CcaGuid = Cca.Guid AND CCB.cta LIKE '6%' INNER JOIN " _
        & "CliGral ON IMPORTHDR.emp = CliGral.emp AND IMPORTHDR.prv = CliGral.Cli INNER JOIN " _
        & "INTRASTAT ON IMPORTDTL.Intrastat = INTRASTAT.Guid " _
        & "WHERE INTRASTAT.EMP=@EMP AND INTRASTAT.yea=@YEA AND (INTRASTAT.MES BETWEEN @QM3-2 AND @QM3) " _
        & "GROUP BY IMPORTHDR.prv, IMPORTHDR.PaisOrigen, CliGral.NIF, CliGral.RaoSocial " _
        & "ORDER BY CliGral.RaoSocial"


        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", CurrentYear, "@QM3", iQM3)
        mDs = oDs
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = Nothing

        Dim DcM1, DcM2, DcM3 As Decimal
        For Each oRow In oTb.Rows
            oRow(Cols.Eur) = oRow(Cols.M1) + oRow(Cols.M2) + oRow(Cols.M3)
            DcM1 += oRow(Cols.M1)
            DcM2 += oRow(Cols.M2)
            DcM3 += oRow(Cols.M3)
        Next

        oRow = oTb.NewRow
        oRow(Cols.Nom) = "total " & oTb.Rows.Count & " operadors:"
        oRow(Cols.M1) = DcM1
        oRow(Cols.M2) = DcM2
        oRow(Cols.M3) = DcM3
        oRow(Cols.Eur) = DcM1 + DcM2 + DcM3
        oTb.Rows.Add(oRow)

        With DataGridView1
            .DataSource = oTb
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True

            With .Columns(Cols.Cli)
                .Visible = False
            End With
            With .Columns(Cols.Pais)
                .HeaderText = "pais"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Nif)
                .HeaderText = "NIF"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Totals"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.M1)
                .HeaderText = oLang.Mes(iQM3 - 2)
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.M2)
                .HeaderText = oLang.Mes(iQM3 - 1)
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.M3)
                .HeaderText = oLang.Mes(iQM3)
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With


    End Sub

    Private Sub ToolStripComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxYea.SelectedIndexChanged
        LoadGrid()
    End Sub

    Private Sub ToolStripComboBoxQ_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxQ.SelectedIndexChanged
        LoadGrid()
    End Sub


    Private Sub ToolStripButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).visible = True
    End Sub
End Class