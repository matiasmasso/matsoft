
Imports System.Data.SqlClient

Public Class Frm_Geo_Fras
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Cols
        Id
        Nom
        Eur
        Pct
    End Enum


    Private Sub Frm_Geo_Fras_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadYeas()
        LoadPaisos()
        LoadRegions()

    End Sub

    Private Sub LoadRegions()
        Dim SQL As String = "SELECT Reg.Guid AS ID, Reg.Region as NOM, SUM(CASE WHEN DH = 2 THEN EUR ELSE - EUR END) AS EUR, 0 AS PCT " _
        & "FROM CliAdr INNER JOIN " _
& "Zip ON CliAdr.Zip=Zip.Guid INNER JOIN " _
& "Location ON Zip.Location=Location.Guid INNER JOIN " _
& "Zona ON Location.Zona=Zona.Guid LEFT OUTER JOIN " _
& "Provincia ON Zona.Provincia=Provincia.Guid LEFT OUTER JOIN " _
& "Reg ON Provincia.Regio=Reg.Guid INNER JOIN " _
        & "CCB ON CliAdr.emp = CCB.Emp AND CliAdr.cli = CCB.cli AND CliAdr.cod = 1 " _
        & "INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid " _
        & "WHERE CCB.Emp =@EMP AND " _
        & "PgcCta.Id LIKE '70%' AND " _
        & "CCB.yea =@YEA AND " _
        & "CIT.ISOpais LIKE @PAIS " _
        & "GROUP BY Reg.Guid, REG.REGION " _
        & "ORDER BY NOM"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", CurrentYea, "@PAIS", CURRENTCountry.ISO)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        Dim DcTot As Decimal
        For Each oRow In oTb.Rows
            DcTot += oRow(Cols.Eur)
        Next
        For Each oRow In oTb.Rows
            oRow(Cols.Pct) = 100 * oRow(Cols.Eur) / DcTot
        Next

        With DataGridView2
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Eur)
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Pct)
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0.0\%"
            End With
        End With

    End Sub

    Private Sub LoadPaisos()
        Dim SQL As String = "SELECT CIT.ISOpais, '' AS NOM, SUM(CASE WHEN DH = 2 THEN EUR ELSE - EUR END) AS EUR, 0 AS PCT " _
        & "FROM CIT INNER JOIN " _
        & "CliAdr ON CIT.Id = CliAdr.CitNum RIGHT OUTER JOIN " _
        & "CCB ON CliAdr.emp = CCB.Emp AND CliAdr.cli = CCB.cli AND CliAdr.cod = 1 " _
        & "INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid " _
        & "WHERE CCB.Emp =@EMP AND " _
        & "PgcCta.Id LIKE '70%' AND " _
        & "CCB.yea = @YEA " _
        & "GROUP BY CIT.ISOpais " _
        & "ORDER BY CIT.ISOpais"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", CurrentYea)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        Dim DcTot As Decimal
        For Each oRow In oTb.Rows
            DcTot += oRow(Cols.Eur)
        Next
        For Each oRow In oTb.Rows
            oRow(Cols.Nom) = BLLCountry.Find(oRow(Cols.Id).ToString).Nom(BLL.BLLApp.Lang)
            oRow(Cols.Pct) = 100 * oRow(Cols.Eur) / DcTot
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Eur)
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Pct)
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0.0\%"
            End With
        End With

    End Sub


    Private Sub LoadYeas()
        Dim SQL As String = "SELECT YEA FROM CCB " _
        & "WHERE EMP=@EMP AND " _
        & "CTA LIKE '70%' " _
        & "GROUP BY YEA " _
        & "ORDER BY YEA DESC"

        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id)
        With ToolStripComboBoxYea
            Do While oDrd.Read
                .Items.Add(oDrd("YEA"))
            Loop
            If .Items.Count = 0 Then
                .Items.Add(Today.Year)
            End If
            .SelectedIndex = 0
        End With
    End Sub

    Private Function CurrentYea() As Integer
        Dim iYea As Integer = ToolStripComboBoxYea.SelectedItem
        Return iYea
    End Function

    Private Function CurrentCountry() As DTOCountry
        Dim oCountry As DTOCountry = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oCountry = BLLCountry.Find(oRow.Cells(Cols.Id).Value.ToString)
        End If
        Return oCountry
    End Function

    Private Sub AnyanteriorToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnyanteriorToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Idx = Idx + 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub AnysegüentToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnysegüentToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Idx = Idx - 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub EnableYeaButtons()
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = ToolStripComboBoxYea.Items.Count
        AnyanteriorToolStripButton.Enabled = (Idx < iYeas - 1)
        AnysegüentToolStripButton.Enabled = (Idx > 0)
    End Sub

    Private Sub ToolStripComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxYea.SelectedIndexChanged
        LoadPaisos()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        LoadRegions()
    End Sub
End Class