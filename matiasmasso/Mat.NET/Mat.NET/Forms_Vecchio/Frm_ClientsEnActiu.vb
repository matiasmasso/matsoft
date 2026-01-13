

Public Class Frm_ClientsEnActiu

    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Cols
        TpaNum = 0
        TpaNom = 1
        Mesos2 = 2
        Anys1 = 3
    End Enum

    Private Sub Frm_ClientsEnActiu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim sFaDosMesos As String = Format(Today.AddMonths(-2), "yyyyMMdd")
        Dim sFaUnAny As String = Format(Today.AddYears(-1), "yyyyMMdd")
        Dim SQL As String = "SELECT X.TPA, TPA.DSC, COUNT(DISTINCT X.MESES2) AS MESES2, COUNT(DISTINCT X.YEARS1) AS YEARS1 " _
        & "FROM ( " _
        & "SELECT ART.emp, ART.tpa, PDC.cli AS MESES2, 0 AS YEARS1 " _
        & "FROM            PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid " _
        & "WHERE  PDC.fch > '" & sFaDosMesos & "' AND PDC.cod = 2 " _
        & "UNION " _
        & "SELECT        Pdc.Emp, 0 AS TPA, PDC.cli AS MESES2, 0 AS YEARS1 " _
        & "FROM             PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid " _
        & "WHERE  PDC.fch > '" & sFaDosMesos & "' AND PDC.cod = 2 " _
        & "UNION " _
        & "SELECT ART.emp, ART.tpa, 0 MESES2, PDC.cli AS YEARS1 " _
        & "FROM            PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid " _
        & "WHERE  PDC.fch > '" & sFaUnAny & "' AND PDC.cod = 2 " _
        & "UNION " _
        & "SELECT        Pdc.Emp, 0 AS TPA, 0 AS MESES2, PDC.cli AS YEARS1 " _
        & "FROM             PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid " _
        & "WHERE  PDC.fch > '" & sFaUnAny & "' AND PDC.cod = 2 ) AS X LEFT OUTER JOIN " _
        & "TPA ON X.emp = TPA.EMP AND X.tpa = TPA.TPA " _
        & "WHERE X.EMP=@EMP " _
        & "GROUP BY X.tpa, TPA.DSC, TPA.ORD, TPA.OBSOLETO " _
        & "ORDER BY TPA.OBSOLETO, TPA.ORD"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.ID)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            '.SelectionMode = DataGridViewSelectionMode.CellSelect
            .DataSource = oTb
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToDeleteRows = False
            .AllowDrop = False
            .BackgroundColor = Color.White

            With .Columns(Cols.TpaNum)
                .Visible = False
                .DefaultCellStyle.DataSourceNullValue = 0
            End With
            With .Columns(Cols.TpaNom)
                .HeaderText = "marca comercial"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Mesos2)
                .HeaderText = "2 ultims mesos"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Anys1)
                .HeaderText = "ultim any"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub
End Class