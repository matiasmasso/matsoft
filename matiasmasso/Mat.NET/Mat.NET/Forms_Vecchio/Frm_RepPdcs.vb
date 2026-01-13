

Public Class Frm_RepPdcs
    Private mYea As Integer = Today.Year
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Cols
        Guid
        Nom
        Tot
        Ene
    End Enum

    Private Sub Frm_RepPdcs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT PNC.RepGuid, CliRep.Abr, COUNT(DISTINCT Pdc.pdc) AS TOT "
        For i As Integer = 1 To 12
            SQL = SQL & ", COUNT(DISTINCT CASE WHEN MONTH(PDC.FCH) =" & i.ToString & " THEN Pdc.pdc ELSE NULL END) AS M" & Format(i, "00") & " "
        Next
        SQL = SQL & " FROM PNC INNER JOIN " _
        & "CliRep ON PNC.RepGuid = CliRep.Guid INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid " _
        & "WHERE  Pdc.EMP=@EMP AND Pdc.yea =@YEA AND (PDC.src =4 OR PDC.src = 5 OR PDC.SRC = 7 ) " _
        & "GROUP BY PNC.RepGuid, CliRep.Abr " _
        & "ORDER BY CliRep.Abr"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@YEA", mYea)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowDrop = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "representant"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Tot)
                .HeaderText = "total"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            For iCol As Integer = 1 To 12
                With .Columns(iCol + Cols.Ene - 1)
                    .HeaderText = BLL.BLLApp.Lang.MesAbr(iCol)
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 40
                    .DefaultCellStyle.Format = "#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            Next
        End With

    End Sub
End Class