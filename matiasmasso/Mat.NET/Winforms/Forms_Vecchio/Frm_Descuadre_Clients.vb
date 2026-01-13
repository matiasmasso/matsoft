

Public Class Frm_Descuadre_Clients

    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Cols
        s430
        pnd
        s5208
        dif
        cli
        Clx
    End Enum

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Refresca()
    End Sub

    Private Sub Refresca()
        BindGrid()
    End Sub

    Private Sub BindGrid()
        Dim SQL As String = "SELECT  CCD_SALDOS.SALDO, " _
        & "SUM(PND.eur) AS PNDEUR, " _
        & "(CASE WHEN GIRAT.SALDO IS NULL THEN 0 ELSE GIRAT.SALDO END) AS GIR, " _
        & "CCD_SALDOS.SALDO - SUM(PND.eur) - (CASE WHEN GIRAT.SALDO IS NULL THEN 0 ELSE GIRAT.SALDO END) AS DIF, " _
        & "CCD_SALDOS.CLI, CLX.CLX " _
        & "FROM CCD_SALDOS INNER JOIN " _
        & "PND ON CCD_SALDOS.Emp = PND.Emp AND CCD_SALDOS.cta = PND.Cta AND CCD_SALDOS.cli = PND.cli LEFT OUTER JOIN " _
        & "CLX ON PND.ContactGuid=CLX.Guid INNER JOIN " _
        & "GIRAT ON CCD_SALDOS.Emp = GIRAT.Emp AND CCD_SALDOS.yea = GIRAT.yea AND CCD_SALDOS.cli = GIRAT.CLI " _
        & "WHERE PND.Emp =" & mEmp.Id & " AND " _
        & "CCD_SALDOS.yea =" & Today.Year & " AND " _
        & "CCD_SALDOS.cta LIKE '" & BLLPgcCta.FromCod(DTOPgcPlan.Ctas.Clients).Id & "' " _
        & "GROUP BY CCD_SALDOS.SALDO, GIRAT.SALDO, CCD_SALDOS.CLI " _
        & "ORDER BY CCD_SALDOS.SALDO - SUM(PND.eur) - (CASE WHEN GIRAT.SALDO IS NULL THEN 0 ELSE GIRAT.SALDO END)"
        mDs =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = mDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(Cols.s430)
                .HeaderText = "430"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.pnd)
                .HeaderText = "pendent"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.s5208)
                .HeaderText = "girat"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.dif)
                .HeaderText = "descuadre"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.cli)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub



    Private Sub FetchRowStyle()
        'D() 'im oRow As DataRow = mDs.Tables(0).Rows(e.Row)
        'Dim oCli As New Client(mEmp, oRow("CLI"))
        'If oCli.CashCod = DTO.DTOCustomer.CashCodes.Reembols Then
        'e.CellStyle.BackColor = System.Drawing.Color.LightBlue
        'End If
    End Sub
End Class