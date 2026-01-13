

Public Class Frm_RepComsDescuadres
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private BlEnableLoad2 As Boolean

    Private Enum Errs
        Pncs
        Arcs
    End Enum


    Private Sub Frm_RepComsDescuadres_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ComboBoxErr.SelectedIndex = Errs.Arcs
    End Sub

    Private Sub LoadGrid()
        Select Case CType(ComboBoxErr.SelectedIndex, Errs)
            Case Errs.Pncs
            Case Errs.Arcs
                BlEnableLoad2 = False
                LoadGridArcs()
                BlEnableLoad2 = True
                LoadGrid2()
        End Select
    End Sub

    Private Sub LoadGrid2()
        Select Case CType(ComboBoxErr.SelectedIndex, Errs)
            Case Errs.Pncs
            Case Errs.Arcs
                LoadGridArc2()
        End Select
    End Sub

    Private Enum ACols
        PncRepGuid
        PncRepNom
        PncCom
        ArcRepId
        ArcRepNom
        ArcCom
        Lins
        MinYea
        MaxYea
    End Enum

    Private Sub LoadGridArcs()
        Dim SQL As String = "SELECT PNC.RepGuid AS PNCRepGuid, " _
        & "(CASE WHEN PR.ABR IS NULL THEN '(s/nom)' ELSE PR.ABR END) AS PNCREPNOM, " _
        & "PNC.COM AS PNCCOM, " _
        & "ARC.rep AS ARCREP, " _
        & "(CASE WHEN AR.ABR IS NULL THEN '(s/nom)' ELSE AR.ABR END) AS ARCREPNOM, " _
        & "ARC.Com AS ARCCOM, " _
        & "COUNT(PNC.lin) AS LINS, MIN(PNC.yea) AS MINYEA, MAX(PNC.yea) AS MAXYEA " _
        & "FROM PNC INNER JOIN " _
        & "Pdc ON Pnc.PdcGuid = Pdc.Guid INNER JOIN " _
        & "ARC ON Pnc.Guid = Arc.PncGuid LEFT OUTER JOIN " _
        & "CliRep AS PR ON PNC.RepGuid = CliRep.Guid LEFT OUTER JOIN " _
        & "CliRep as AR ON ARC.RepGuid=AR.AR.Guid " _
        & "WHERE NOT (PNC.RepGuid = ARC.RepGuid AND PNC.com = ARC.Com) " _
        & "GROUP BY PR.Abr, PNC.repGuid, PNC.com, ar.abr,ARC.rep, ARC.Com " _
        & "ORDER BY PNCREPNOM, ARCREPNOM"

        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
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
            .AllowUserToResizeRows = False
            .AllowDrop = True
            With .Columns(ACols.PncRepGuid)
                .Visible = False
            End With
            With .Columns(ACols.PncRepNom)
                .HeaderText = "rep a comanda"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ACols.PncCom)
                .HeaderText = "comisió"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0.0\%"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ACols.ArcRepId)
                .Visible = False
            End With
            With .Columns(ACols.ArcRepNom)
                .HeaderText = "rep a albará"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ACols.ArcCom)
                .HeaderText = "comisió"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#.0\%"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ACols.Lins)
                .HeaderText = "linies"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ACols.MinYea)
                .HeaderText = "desde"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ACols.MaxYea)
                .HeaderText = "fins"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

        End With


    End Sub

    Private Enum A2Cols
        PncYea
        PncPdc
        PdcFch
        ArcYea
        ArcAlb
        ArcFch
        CliNom
        ArtNom
    End Enum

    Private Sub LoadGridArc2()
        Dim oRow1 As DataGridViewRow = DataGridView1.CurrentRow

        Dim SQL As String = "SELECT Pdc.yea, Pdc.pdc, PDC.fch, ARC.ye1, ARC.alb, ARC.fch AS arcfch, CLX.clx, ART.myD " _
        & "FROM  PNC INNER JOIN " _
        & "ARC ON PNC.Guid = ARC.PncGuid INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid LEFT OUTER JOIN " _
        & "CLX ON PDC.CliGuid = CLX.Guid LEFT OUTER JOIN " _
        & "ART ON PNC.ArtGuid = ART.Guid " _
        & "WHERE  PNC.RepGuid =@PNCREP AND " _
        & "PNC.com =@PNCCOM AND " _
        & "ARC.rep =@ARCREP AND " _
        & "ARC.Com =@ARCCOM " _
        & "ORDER BY Pdc.yea, Pdc.pdc, PNC.lin, CLX.clx "

        Dim DcPncCom As Decimal = CType(oRow1.Cells(ACols.PncCom).Value, Decimal)
        Dim DcArcCom As Decimal = CType(oRow1.Cells(ACols.ArcCom).Value, Decimal)
        Dim oPncRepGuid As Guid = oRow1.Cells(ACols.PncRepGuid).Value
        Dim iArcRep As Integer = CInt(oRow1.Cells(ACols.ArcRepId).Value)
        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@EMP", mEmp.Id, "@PNCREP", oPncRepGuid.ToString, "@PNCCOM", DcPncCom, "@ARCREP", iArcRep, "@ARCCOM", DcArcCom)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView2
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = True
            With .Columns(A2Cols.PncYea)
                .Visible = False
            End With
            With .Columns(A2Cols.PncPdc)
                .HeaderText = "comanda"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(A2Cols.PdcFch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(A2Cols.ArcYea)
                .Visible = False
            End With
            With .Columns(A2Cols.ArcAlb)
                .HeaderText = "albará"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(A2Cols.ArcFch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(A2Cols.CliNom)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(A2Cols.ArtNom)
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With


    End Sub

    Private Sub ComboBoxErr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxErr.SelectedIndexChanged
        LoadGrid()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If BlEnableLoad2 Then
            LoadGrid2()
        End If
    End Sub
End Class