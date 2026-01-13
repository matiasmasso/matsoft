

Public Class Frm_Impagats_Old
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean
    Private mDirtyDeutors As Boolean
    Private mDirtyStat As Boolean
    Private mDirtyInsolvencies As Boolean
    Private mDirtyMxf As Boolean = True
    Private mPlan As PgcPlan = PgcPlan.FromToday

    Private Enum ColsImp
        Yea
        Csa
        Csb
        Cli
        Quadra
        QuadraToolTip
        QuadraIco
        Vto
        Nom
        Txt
        Eur
        Gts
        Deute
        ACompte
        Pendent
        AFP
        LastMem
        Status
        StatusIco
    End Enum

    Private Enum ColsIns
        Id
        Cli
        CliNom
        Nominal
        PagatACompte
        Presentacio
        Admisio
        Liquidacio
        Deute
        Rehabilitacio
    End Enum

    Private Enum ColsDeutors
        Ico
        Cli
        Nom
        Saldo
        Impagats
        Inolvencies
    End Enum

    Private Enum ColsStat
        Fch
        Deb
        Hab
        Sdo
    End Enum

    Private Enum ColsMxf
        Emps
        Mmo
        BbA
        Ind
        Fad
        IcoMmo
        IcoBbA
        IcoInd
        IcoFad
        Nif
        Nom
        Fch
        Eur
    End Enum

    Private Sub Frm_Impagats_New_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadImpagats()
        mDirtyDeutors = True
        mDirtyInsolvencies = True
        mDirtyStat = True
        SetContextMenuImpagats()
    End Sub

    Private Sub LoadImpagats()
        mAllowEvents = False

        Dim SQL As String = "SELECT (CASE WHEN CTAS.CLI IS NULL THEN IMPS.CLI ELSE CTAS.CLI END) AS CLIENT, " _
        & "IMPS.SUM AS IMPSSUM, " _
        & "CTAS.SUM AS CTASSUM " _
        & "FROM (SELECT CSB.Emp, CSB.cli, SUM(CSB.eur-IMPAGATS.PAGATACOMPTE) AS SUM " _
        & "FROM IMPAGATS INNER JOIN CSB ON IMPAGATS.Emp = CSB.Emp AND IMPAGATS.Yea = CSB.yea AND IMPAGATS.Csa = CSB.Csb AND IMPAGATS.Csb = CSB.Doc " _
        & "WHERE(Impagats.Status = 1) " _
        & "GROUP BY CSB.Emp, CSB.cli) AS IMPS FULL OUTER JOIN " _
        & "(SELECT Emp, cli, SUM(CASE WHEN DH = 1 THEN EUR ELSE - EUR END) AS SUM " _
        & "FROM Ccb " _
        & "WHERE yea = YEAR(GETDATE()) AND (cta LIKE '" & mPlan.Cta(DTOPgcPlan.ctas.impagats).Id & "') " _
        & "GROUP BY Emp, cli " _
        & "HAVING SUM(CASE WHEN DH = 1 THEN EUR ELSE - EUR END) <> 0) AS CTAS " _
        & "ON IMPS.Emp = CTAS.Emp AND IMPS.cli = CTAS.cli " _
        & "WHERE IMPS.EMP=" & mEmp.Id & " AND " _
        & "(Ctas.SUM <> IMPS.SUM) " _
        & "ORDER BY CLIENT"
        Dim oDsCheck As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTbCheck As DataTable = oDsCheck.Tables(0)

        SQL = "SELECT IMPAGATS.Yea, IMPAGATS.Csa, IMPAGATS.Csb, CSB.cli, 0 as QUADRA, '' AS QUADRATOOLTIP, " _
        & "CSB.vto, CLX.clx, CSB.txt, CSB.eur, IMPAGATS.Gastos, CSB.eur+IMPAGATS.Gastos, IMPAGATS.PagatACompte, CSB.eur+IMPAGATS.Gastos-IMPAGATS.PagatACompte, " _
        & "IMPAGATS.FchAFP, LASTMEM, IMPAGATS.Status " _
        & "FROM IMPAGATS INNER JOIN " _
        & "CSB ON IMPAGATS.Emp = CSB.Emp AND IMPAGATS.Yea = CSB.yea AND IMPAGATS.Csa = CSB.Csb AND IMPAGATS.Csb = CSB.Doc INNER JOIN " _
        & "CLX ON CSB.Emp = CLX.Emp AND CSB.cli = CLX.cli LEFT OUTER JOIN " _
        & "(SELECT EMP,CLI,MAX(FCH) AS LASTMEM FROM MEM GROUP BY EMP,CLI) MM ON MM.EMP=CSB.EMP AND MM.CLI=CSB.CLI " _
        & "WHERE CSB.EMP=" & mEmp.Id & " "

        If CheckBoxHideSaldats.Checked Then
            SQL = SQL & "AND STATUS <>" & Impagat.StatusCodes.Saldat & " "
        End If

        If CheckBoxHideInsolvencias.Checked Then
            SQL = SQL & "AND STATUS <>" & Impagat.StatusCodes.Insolvencia & " "
        End If

        SQL = SQL & "ORDER BY VTO DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oCol1 As DataColumn = oTb.Columns.Add("QUADRAICO", System.Type.GetType("System.Byte[]"))
        oCol1.SetOrdinal(ColsImp.QuadraIco)

        Dim oCol2 As DataColumn = oTb.Columns.Add("STATUSICO", System.Type.GetType("System.Byte[]"))
        oCol2.SetOrdinal(ColsImp.StatusIco)

        Dim CliId As Integer = 0
        Dim DecTot As Decimal = 0
        Dim BlFound As Boolean
        For Each oRow As DataRow In oTb.Rows
            CliId = oRow(ColsImp.Cli)
            DecTot += CDec(oRow(ColsImp.Eur)) - CDec(oRow(ColsImp.ACompte))
            For Each oRowCheck As DataRow In oTbCheck.Rows
                Select Case CInt(oRowCheck("Client"))
                    Case Is = CliId
                        oRow(ColsImp.Quadra) = 1
                        oRow(ColsImp.QuadraToolTip) = "saldo impagats: " & Format(CDbl(oRowCheck("CTASSUM")), "#,###.00 €") & " suma de impagos: " & Format(CDbl(oRowCheck("IMPSSUM")), "#,###.00 €")
                        Exit For
                    Case Is > CliId
                        Exit For
                End Select
            Next
            If BlFound Then
            Else
            End If
        Next

        TextBoxImpagats.Text = "total " & oTb.Rows.Count & " impagats per " & Format(DecTot, "#,###.00 €") & " (despeses no incloses)"
        With DataGridViewImpagats
            With .RowTemplate
                .Height = DataGridViewImpagats.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(ColsImp.Yea)
                .Visible = False
            End With
            With .Columns(ColsImp.Csa)
                .Visible = False
            End With
            With .Columns(ColsImp.Csb)
                .Visible = False
            End With
            With .Columns(ColsImp.Cli)
                .Visible = False
            End With
            With .Columns(ColsImp.Quadra)
                .Visible = False
            End With
            With .Columns(ColsImp.QuadraToolTip)
                .Visible = False
            End With
            With .Columns(ColsImp.QuadraIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsImp.Vto)
                .HeaderText = "venciment"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsImp.Nom)
                .HeaderText = "deutor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsImp.Txt)
                .HeaderText = "concepte"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsImp.Eur)
                .HeaderText = "nominal"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsImp.Gts)
                .HeaderText = "despeses"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsImp.Deute)
                .HeaderText = "deute"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsImp.ACompte)
                .HeaderText = "a cte"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsImp.Pendent)
                .HeaderText = "pendent"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsImp.AFP)
                .HeaderText = "AFP"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsImp.LastMem)
                .HeaderText = "ult.memo"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsImp.Status)
                .Visible = False
            End With
            With .Columns(ColsImp.StatusIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
        End With
        mAllowEvents = True
    End Sub


    Private Sub LoadMxf()
        mAllowEvents = False

        Dim oDs As DataSet = MxfLobby.RankingDataset
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oColIcoMmo As DataColumn = oTb.Columns.Add("ICOMMO", System.Type.GetType("System.Byte[]"))
        oColIcoMmo.SetOrdinal(ColsMxf.IcoMmo)
        Dim oColIcoBba As DataColumn = oTb.Columns.Add("ICOBBA", System.Type.GetType("System.Byte[]"))
        oColIcoBba.SetOrdinal(ColsMxf.IcoBbA)
        Dim oColIcoInd As DataColumn = oTb.Columns.Add("ICOIND", System.Type.GetType("System.Byte[]"))
        oColIcoInd.SetOrdinal(ColsMxf.IcoInd)
        Dim oColIcoFad As DataColumn = oTb.Columns.Add("ICOFAD", System.Type.GetType("System.Byte[]"))
        oColIcoFad.SetOrdinal(ColsMxf.IcoFad)

        With DataGridViewMxf
            With .RowTemplate
                .Height = DataGridViewMxf.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(ColsMxf.Emps)
                .Visible = False
            End With
            With .Columns(ColsMxf.Mmo)
                .Visible = False
            End With
            With .Columns(ColsMxf.BbA)
                .Visible = False
            End With
            With .Columns(ColsMxf.Ind)
                .Visible = False
            End With
            With .Columns(ColsMxf.Fad)
                .Visible = False
            End With
            With .Columns(ColsMxf.IcoMmo)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsMxf.IcoBbA)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsMxf.IcoInd)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsMxf.IcoFad)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsMxf.Nif)
                .Visible = False
            End With
            With .Columns(ColsMxf.Nom)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsMxf.Fch)
                .HeaderText = "ult.data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsMxf.Eur)
                .HeaderText = "import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Sub DataGridViewImpagats_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridViewImpagats.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridViewImpagats.Rows(e.RowIndex)
        Dim DtVto As DateTime = oRow.Cells(ColsImp.Vto).Value
        If DtVto.AddMonths(4) <= Today Then
            PaintGradientRowBackGround(e, maxisrvr.COLOR_NOTOK)
        ElseIf DtVto.AddMonths(2) <= Today Then
            PaintGradientRowBackGround(e, Color.Yellow)
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            Me.DataGridViewImpagats.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            Me.DataGridViewImpagats.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub


    Private Sub LoadInsolvencies()
        mAllowEvents = False

        Dim SQL As String = "SELECT ID,INSOLVENCIAS.CLI,CLX,NOMINAL,PAGATACOMPTE,FCHPRESENTACIO,FCHADMISIO,FCHLIQUIDACIO," _
        & "NOMINAL+GASTOS+COMISIO-PAGATACOMPTE, " _
        & "FCHREHABILITACIO " _
        & "FROM INSOLVENCIAS INNER JOIN " _
        & "CLX ON INSOLVENCIAS.EMP=CLX.EMP AND INSOLVENCIAS.CLI=CLX.CLI " _
        & "WHERE INSOLVENCIAS.EMP=" & mEmp.Id & " "

        If CheckBoxHideLiquidats.Checked Then
            SQL = SQL & " AND FCHLIQUIDACIO IS NULL "
        End If

        SQL = SQL & "ORDER BY CLX"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridViewInsolvencies
            With .RowTemplate
                .Height = DataGridViewImpagats.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(ColsIns.Id)
                .Visible = False
            End With
            With .Columns(ColsIns.Cli)
                .Visible = False
            End With
            With .Columns(ColsIns.CliNom)
                .HeaderText = "deutor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsIns.Nominal)
                .HeaderText = "nominal"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsIns.Presentacio)
                .HeaderText = "presentacio"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsIns.Admisio)
                .HeaderText = "Admisio"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            If CheckBoxHideLiquidats.Checked Then
                With .Columns(ColsIns.Liquidacio)
                    .Visible = False
                End With
                With .Columns(ColsIns.PagatACompte)
                    .Visible = False
                End With
                With .Columns(ColsIns.Deute)
                    .Visible = False
                End With
                With .Columns(ColsIns.Rehabilitacio)
                    .Visible = False
                End With
            Else
                With .Columns(ColsIns.Liquidacio)
                    .HeaderText = "Liquidacio"
                    .Width = 65
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "dd/MM/yy"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Visible = True
                End With
                With .Columns(ColsIns.PagatACompte)
                    .HeaderText = "anticipades"
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(ColsIns.Deute)
                    .HeaderText = "deute"
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
                With .Columns(ColsIns.Rehabilitacio)
                    .HeaderText = "Rehabilitacio"
                    .Width = 65
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "dd/MM/yy"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Visible = True
                End With
            End If
        End With
        mAllowEvents = True
    End Sub

    Private Sub LoadDeutors()
        mAllowEvents = False

        Dim SQL As String = "SELECT SDO.cli, SDO.clx, SDO.SDO, IMPS.EUR AS IMPAGATS, INS.EUR AS INSOLVENCIES " _
        & "FROM (SELECT CCB.Emp, CCB.cli, CLX.clx, SUM(CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) AS SDO " _
        & "FROM CCB INNER JOIN " _
        & "CLX ON CCB.Emp = CLX.Emp AND CCB.cli = CLX.cli " _
        & "WHERE (CCB.yea = YEAR(GETDATE())) AND (CCB.cta LIKE '" & mPlan.Cta(DTOPgcPlan.ctas.impagats).Id & "') " _
        & "GROUP BY CCB.Emp, CCB.cli, CLX.clx " _
        & "HAVING (SUM(CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) <> 0)) AS SDO LEFT OUTER JOIN " _
        & "(SELECT  IMPAGATS.Emp, CSB.cli, SUM(CSB.eur-IMPAGATS.PAGATACOMPTE) AS EUR " _
        & "FROM IMPAGATS INNER JOIN " _
        & "CSB ON IMPAGATS.Emp = CSB.Emp AND IMPAGATS.Yea = CSB.yea AND IMPAGATS.Csa = CSB.Csb AND IMPAGATS.Csb = CSB.Doc " _
        & "WHERE(Impagats.Status < 3) " _
        & "GROUP BY IMPAGATS.Emp, CSB.cli) AS IMPS ON SDO.Emp = IMPS.Emp AND SDO.cli = IMPS.cli LEFT OUTER JOIN " _
        & "(SELECT  Emp, Cli, SUM(Nominal-pagatacompte) AS EUR " _
        & "FROM INSOLVENCIAS " _
        & "WHERE FchLiquidacio Is NULL " _
        & "GROUP BY Emp, Cli) AS INS ON SDO.Emp = INS.Emp AND SDO.cli = INS.Cli " _
        & "WHERE SDO.Emp = 1 " _
        & "ORDER BY SDO.CLX"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("QUADRAICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(ColsDeutors.Ico)

        Dim DecTot As Decimal = 0
        For Each oRow As DataRow In oTb.Rows
            DecTot += oRow("SDO")
        Next
        TextBoxDeutors.Text = "total " & oTb.Rows.Count & " deutors " & Format(DecTot, "#,###.00 €")

        With DataGridViewDeutors
            With .RowTemplate
                .Height = DataGridViewImpagats.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(ColsDeutors.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsDeutors.Cli)
                .Visible = False
            End With
            With .Columns(ColsDeutors.Nom)
                .HeaderText = "deutor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsDeutors.Saldo)
                .HeaderText = "saldo"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDeutors.Impagats)
                .HeaderText = "impagats"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDeutors.Inolvencies)
                .HeaderText = "insolvencies"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Sub DataGridViewImpagats_CellToolTipTextNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles DataGridViewImpagats.CellToolTipTextNeeded
        Select Case e.ColumnIndex
            Case ColsImp.QuadraIco
                Dim iRow As Integer = e.RowIndex
                If iRow >= 0 Then
                    e.ToolTipText = DataGridViewImpagats.Rows(iRow).Cells(ColsImp.QuadraToolTip).Value
                End If
        End Select
    End Sub

    Private Sub DataGridViewImpagats_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewImpagats.DoubleClick
        Zoom()
    End Sub

    Private Function CurrentImpagat() As Impagat
        Dim oImpagat As Impagat = Nothing
        Dim oRow As DataGridViewRow = DataGridViewImpagats.CurrentRow
        If oRow IsNot Nothing Then
            Dim oCsa As Csa = MaxiSrvr.Csa.FromNum(mEmp, oRow.Cells(ColsImp.Yea).Value, oRow.Cells(ColsImp.Csa).Value)
            Dim oCsb As New Csb(oCsa, oRow.Cells(ColsImp.Csb).Value)
            oImpagat = New Impagat(oCsb)
        End If
        Return oImpagat
    End Function

    Private Function CurrentInsolvencia() As Insolvencia
        Dim oInsolvencia As Insolvencia = Nothing
        Dim oRow As DataGridViewRow = DataGridViewInsolvencies.CurrentRow
        If oRow IsNot Nothing Then
            oInsolvencia = New Insolvencia(mEmp, oRow.Cells(ColsIns.Id).Value)
        End If
        Return oInsolvencia
    End Function

    Private Function CurrentImpagats() As Impagats
        Dim oImpagats As New Impagats
        Dim oRow As DataGridViewRow
        If DataGridViewImpagats.SelectedRows.Count = 0 Then
            Dim oImpagat As Impagat = CurrentImpagat()
            If oImpagat IsNot Nothing Then
                oImpagats.Add(CurrentImpagat)
            End If
        Else
            For i As Integer = 0 To DataGridViewImpagats.SelectedRows.Count - 1
                oRow = DataGridViewImpagats.SelectedRows(i)
                Dim oCsa As Csa = MaxiSrvr.Csa.FromNum(mEmp, oRow.Cells(ColsImp.Yea).Value, oRow.Cells(ColsImp.Csa).Value)
                Dim oCsb As New Csb(oCsa, oRow.Cells(ColsImp.Csb).Value)
                oImpagats.Add(New Impagat(oCsb))
            Next
        End If
        Return oImpagats
    End Function

    Private Function CurrentDeutor() As Contact
        Dim oContact As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridViewDeutors.CurrentRow
        If oRow IsNot Nothing Then
            oContact = MaxiSrvr.Contact.FromNum(mEmp, oRow.Cells(ColsDeutors.Cli).Value)
        End If
        Return oContact
    End Function


    Private Sub Zoom()
        Dim oFrm As New Frm_Impagat
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestImpagats
        With oFrm
            .Impagat = CurrentImpagat()
            .Show()
        End With
    End Sub

    Private Sub ZoomInsolvencia()
        Dim oFrm As New Frm_Insolvencia
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestInsolvencies
        With oFrm
            .Insolvencia = CurrentInsolvencia()
            .Show()
        End With
    End Sub

    Private Sub Extracte()
        Dim oCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.impagats)
        Dim oContact As Contact = CurrentImpagat.Csb.Client
        Dim oExercici As New Exercici(oContact.Emp, Today.Year)
        Dim oFrm As New Frm_CliCtas(oContact, oCta, oExercici)
        oFrm.Show()
    End Sub

    Private Sub RefreshRequestImpagats(ByVal sender As Object, ByVal e As System.EventArgs)
        mDirtyDeutors = True
        mDirtyStat = True

        Dim i As Integer
        Dim iFirstRow As Integer
        Dim oGrid As DataGridView = DataGridViewImpagats
        If oGrid.CurrentRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
        End If
        Dim j As Integer = ColsImp.Nom
        iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        LoadImpagats()
        If iFirstRow >= 0 And iFirstRow < oGrid.Rows.Count Then
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(j)
            End If
        End If

    End Sub


    Private Sub RefreshRequestInsolvencies(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim iFirstRow As Integer
        Dim oGrid As DataGridView = DataGridViewInsolvencies
        If oGrid.CurrentRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
        End If
        Dim j As Integer = ColsIns.CliNom
        iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        LoadInsolvencies()
        If iFirstRow >= 0 And iFirstRow < oGrid.Rows.Count Then
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(j)
            End If
        End If
    End Sub

    Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    CheckBoxHideInsolvencias.CheckedChanged, CheckBoxHideSaldats.CheckedChanged
        If mAllowEvents Then
            RefreshRequestImpagats(sender, e)
        End If
    End Sub


    Private Function GetImpagatFromRow(ByVal oRow As DataGridViewRow) As Impagat
        Dim oCsa As Csa = MaxiSrvr.Csa.FromNum(mEmp, oRow.Cells(ColsImp.Yea).Value, oRow.Cells(ColsImp.Csa).Value)
        Dim oCsb As New Csb(oCsa, oRow.Cells(ColsImp.Csb).Value)
        Dim oImpagat As New Impagat(oCsb)
        Return oImpagat
    End Function


    Private Sub DataGridViewImpagats_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewImpagats.CellFormatting
        Select Case e.ColumnIndex
            Case ColsImp.QuadraIco
                Dim oRow As DataGridViewRow = DataGridViewImpagats.Rows(e.RowIndex)
                Select Case CInt(oRow.Cells(ColsImp.Quadra).Value)
                    Case 0
                        e.Value = My.Resources.empty
                    Case 1
                        e.Value = My.Resources.warn
                End Select
            Case ColsImp.StatusIco
                Dim oRow As DataGridViewRow = DataGridViewImpagats.Rows(e.RowIndex)
                Select Case CType(oRow.Cells(ColsImp.Status).Value, Impagat.StatusCodes)
                    Case Impagat.StatusCodes.EnNegociacio
                        e.Value = My.Resources.tel
                    Case Impagat.StatusCodes.Conveni
                        e.Value = My.Resources.Outlook_16
                    Case Impagat.StatusCodes.Saldat
                        e.Value = My.Resources.Ok
                    Case Impagat.StatusCodes.Insolvencia
                        e.Value = My.Resources.wrong
                    Case Else
                        e.Value = My.Resources.empty
                End Select
        End Select
    End Sub

    Private Sub DataGridViewDeutors_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewDeutors.CellFormatting
        Select Case e.ColumnIndex
            Case ColsDeutors.Ico
                Dim oRow As DataGridViewRow = DataGridViewDeutors.Rows(e.RowIndex)
                Dim DecSdo As Decimal = CDec(oRow.Cells(ColsDeutors.Saldo).Value)

                Dim DecImpagats As Decimal = 0
                If IsNumeric(oRow.Cells(ColsDeutors.Impagats).Value) Then
                    DecImpagats = CDec(oRow.Cells(ColsDeutors.Impagats).Value)
                End If

                Dim DecInsolvencies As Decimal = 0
                If IsNumeric(oRow.Cells(ColsDeutors.Inolvencies).Value) Then
                    DecInsolvencies = CDec(oRow.Cells(ColsDeutors.Inolvencies).Value)
                End If

                If DecSdo = DecImpagats + DecInsolvencies Then
                    e.Value = My.Resources.empty
                Else
                    e.Value = My.Resources.warn
                End If
        End Select
    End Sub

    Private Sub DataGridViewMxf_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewMxf.CellFormatting
        Dim oGrid As DataGridView = DataGridViewMxf
        Select Case e.ColumnIndex
            Case ColsMxf.IcoMmo
                Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
                If oRow.Cells(ColsMxf.Mmo).Value > 0 Then e.Value = My.Resources.Mxf_matias Else e.Value = My.Resources.empty
            Case ColsMxf.IcoBbA
                Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
                If oRow.Cells(ColsMxf.BbA).Value > 0 Then e.Value = My.Resources.Mxf_xavi Else e.Value = My.Resources.empty
            Case ColsMxf.IcoInd
                Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
                If oRow.Cells(ColsMxf.Ind).Value > 0 Then e.Value = My.Resources.Mxf_michael Else e.Value = My.Resources.empty
            Case ColsMxf.IcoFad
                Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
                If oRow.Cells(ColsMxf.Fad).Value > 0 Then e.Value = My.Resources.Mxf_fadi Else e.Value = My.Resources.empty
        End Select
    End Sub


    Private Sub DataGridViewImpagats_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewImpagats.SelectionChanged
        If mAllowEvents Then
            SetContextMenuImpagats()
        End If
    End Sub

    Private Sub DataGridViewDeutors_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewDeutors.SelectionChanged
        If mAllowEvents Then
            SetContextMenuDeutors()
        End If
    End Sub

    Private Sub SetContextMenuImpagats()
        Dim oContextMenu As New ContextMenuStrip
        Dim oImpagats As Impagats = CurrentImpagats()


        If oImpagats.Count > 0 Then
            Dim oMenu_Impagat As New Menu_Impagat(oImpagats)
            AddHandler oMenu_Impagat.AfterUpdate, AddressOf RefreshRequestImpagats
            oContextMenu.Items.AddRange(oMenu_Impagat.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim oMenuItemRefresca As New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequestImpagats)
        oContextMenu.Items.Add(oMenuItemRefresca)

        DataGridViewImpagats.ContextMenuStrip = oContextMenu

    End Sub

    Private Sub SetContextMenuInsolvencies()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItemRefresca As New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequestInsolvencies)
        oContextMenu.Items.Add(oMenuItemRefresca)

        Dim oInsolvencia As Insolvencia = CurrentInsolvencia()

        If oInsolvencia IsNot Nothing Then
            Dim oMenuItemZoom As New ToolStripMenuItem("zoom", My.Resources.prismatics, AddressOf ZoomInsolvencia)
            oContextMenu.Items.Add(oMenuItemZoom)
            Dim oMenuItemContact As New ToolStripMenuItem("client...", My.Resources.People_Blue)
            oContextMenu.Items.Add(oMenuItemContact)
            Dim oMenu_Contact As New Menu_Contact(oInsolvencia.Contact)
            oMenuItemContact.DropDownItems.AddRange(oMenu_Contact.Range)
        End If

        DataGridViewInsolvencies.ContextMenuStrip = oContextMenu

    End Sub

    Private Sub SetContextMenuDeutors()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentDeutor()
        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(CurrentDeutor)
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
        End If
        DataGridViewDeutors.ContextMenuStrip = oContextMenu

    End Sub

    Private Sub Do_Cobrar()
        Dim oFrm As New Frm_Cobrament
        AddHandler oFrm.afterupdate, AddressOf RefreshRequestImpagats
        With oFrm
            .impagats = CurrentImpagats()
            .Show()
        End With
        oFrm.Show()
    End Sub

    Private Sub Do_Insolvencia()
        Dim oContact As Contact = Nothing
        Dim oImpagat As Impagat = Nothing
        Dim oNominal As New maxisrvr.Amt
        Dim oGastos As New maxisrvr.Amt

        Dim oImpagats As New Impagats
        For Each oRow As DataGridViewRow In DataGridViewImpagats.SelectedRows
            oImpagat = GetImpagatFromRow(oRow)
            oNominal.Add(oImpagat.pendent)
            oGastos.Add(oImpagat.Gastos)
            oImpagats.Add(oImpagat)
        Next

        Dim oTmp As maxisrvr.Amt = oNominal.Clone
        oTmp.Add(oGastos)
        Dim oComisio As maxisrvr.Amt = oTmp.Percent(20)

        oContact = oImpagats(0).Csb.Client

        Dim oInsolvencia As New Insolvencia(oContact)
        With oInsolvencia
            .Nominal = oNominal
            .Gastos = oGastos
            .Comisio = oComisio
            .Impagats = oImpagats
            .FchPresentacio = Today
        End With

        Dim oFrm As New Frm_Insolvencia
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestAll
        With oFrm
            .Insolvencia = oInsolvencia
            .Show()
        End With

    End Sub

    Private Sub RefreshRequestAll(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.RefreshRequestImpagats(sender, e)
        Me.RefreshRequestInsolvencies(sender, e)
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

        Select Case TabControl1.SelectedTab.Name
            Case TabPageImpagats.Name

            Case TabPageInsolvencias.Name
                If mDirtyInsolvencies Then
                    LoadInsolvencies()
                    SetContextMenuInsolvencies()
                    mDirtyInsolvencies = False
                End If

            Case TabPageDeutors.Name
                If mDirtyDeutors Then
                    LoadDeutors()
                    SetContextMenuDeutors()
                    mDirtyDeutors = False
                End If

            Case TabPageStat.Name
                Static BlLoadedYeas As Boolean
                If mDirtyStat Then
                    mAllowEvents = False
                    If Not BlLoadedYeas Then
                        LoadStatYeas()
                        BlLoadedYeas = True
                    End If
                    PictureBoxGraph.Image = StatImg()
                    mDirtyStat = False
                    mAllowEvents = True
                End If

            Case TabPageMxf.Name
                If mDirtyMxf Then
                    LoadMxf()
                    mDirtyMxf = False
                End If
        End Select

    End Sub

    Private Sub DataGridViewInsolvencies_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewInsolvencies.DoubleClick
        ZoomInsolvencia()
    End Sub

    Private Sub DataGridViewInsolvencies_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewInsolvencies.SelectionChanged
        SetContextMenuInsolvencies()
    End Sub

    Private Sub CheckBoxHideLiquidats_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxHideLiquidats.CheckedChanged
        If mAllowEvents Then
            RefreshRequestInsolvencies(sender, e)
        End If
    End Sub

    Private Function StatImg() As Bitmap
        Dim iMargeInf As Integer = 20
        Dim iYea As Integer = CurrentStatYea()
        Dim oPlan As PgcPlan = PgcPlan.FromYear(iYea)
        Dim FchLast As DateTime = IIf(Today.Year = iYea, Today, New Date(iYea, 12, 31))
        Dim FchFirst As New DateTime(FchLast.Year, 1, 1)
        Dim iDias As Integer = FchLast.DayOfYear
        Dim DecTmp As Decimal = 0

        Dim SQL As String = "SELECT YEA,SUM(CASE WHEN DH = 1 THEN EUR ELSE -EUR END) AS EUR " _
        & "FROM  CCB " _
        & "WHERE CCB.Emp =" & mEmp.Id & " AND " _
        & "CCB.yea >2000 AND " _
        & "CCB.cta LIKE '" & oPlan.Cta(DTOPgcPlan.ctas.impagats).Id & "' " _
        & "GROUP BY CCB.YEA, CCB.fch " _
        & "ORDER BY CCB.fch"

        Dim DecMaxVal As Decimal = 0
        Dim DecSdo As Integer
        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Do While oDrd.Read
            If oDrd("YEA") <> iYea Then
                iYea = oDrd("YEA")
                DecSdo = 0
            End If
            DecSdo += oDrd("EUR")
            If DecSdo > DecMaxVal Then DecMaxVal = decsdo
        Loop
        oDrd.Close()

        SQL = "SELECT CCB.fch, " _
        & "SUM(CASE WHEN DH = 1 THEN EUR ELSE 0 END) AS DEB, " _
        & "SUM(CASE WHEN DH = 2 THEN EUR ELSE 0 END) AS HAB " _
        & "FROM  CCB INNER JOIN " _
        & "CCA ON Ccb.CcaGuid = Cca.Guid " _
        & "WHERE CCB.Emp =" & mEmp.Id & " AND " _
        & "CCB.yea =" & CurrentStatYea() & " AND " _
        & "CCB.cta LIKE '" & oPlan.Cta(DTOPgcPlan.ctas.impagats).Id & "' " _
        & "GROUP BY CCB.fch " _
        & "ORDER BY CCB.fch DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("SALDO", System.Type.GetType("System.Decimal"))

        Dim oRowTot As DataRow = oTb.NewRow

        Dim i, j As Integer
        Dim oRow As DataRow = oTb.Rows(oTb.Rows.Count - 1)

        Dim Idx As Integer = 0
        Dim DecDeb As Decimal
        Dim DecHab As Decimal
        Dim DecVal(FchLast.DayOfYear) As Decimal
        Dim DecXDeb As Decimal
        Dim DecXHab As Decimal

        Dim iGraphWidth As Integer = 366
        Dim iGraphHeight As Integer = 250
        Dim SngFactor As Decimal = iGraphHeight / DecMaxVal

        Dim Fch_i As Date
        Dim Fch_j As Date

        DecSdo = 0
        For i = oTb.Rows.Count - 1 To 0 Step -1
            oRow = oTb.Rows(i)
            Fch_i = oRow(ColsStat.Fch)
            For j = Idx To FchLast.DayOfYear - 1
                Fch_j = FchFirst.AddDays(j)
                If Fch_j >= Fch_i Then
                    Idx = j
                    Exit For
                End If
                DecVal(j) = DecSdo * SngFactor
            Next
            DecDeb = oRow(ColsStat.Deb)
            DecHab = oRow(ColsStat.Hab)
            DecSdo = DecSdo + DecDeb - DecHab
            DecXDeb += DecDeb
            DecXHab += DecHab
            oRow(ColsStat.Sdo) = DecSdo
        Next

        If Fch_j >= Fch_i Then
            DecVal(j) = DecSdo * SngFactor
        End If


        Dim oBitmap As New Bitmap(iGraphWidth, iGraphHeight)
        Dim e As Graphics = Graphics.FromImage(oBitmap)
        Dim oPen As New Pen(Color.Black)
        Dim X1 As Integer
        Dim X2 As Integer
        Dim Y1 As Integer
        Dim Y2 As Integer
        For i = 2 To iDias - 1
            X1 = i - 1
            Y1 = iGraphHeight - DecVal(X1)
            X2 = i
            Y2 = iGraphHeight - DecVal(X2)
            e.DrawLine(oPen, X1, Y1, X2, Y2)
            Y1 = Y2
        Next

        Dim Y As Integer
        Idx = 0
        oPen = New Pen(Color.Gray)
        Do
            Idx += 1
            Y = iGraphHeight - Idx * 10000 * SngFactor
            If Y < 0 Then Exit Do
            e.DrawLine(oPen, 0, Y, iGraphWidth, Y)
        Loop

        oRowTot(ColsStat.Deb) = DecXDeb
        oRowTot(ColsStat.Hab) = DecXHab
        oTb.Rows.InsertAt(oRowTot, 0)

        With DataGridViewStat
            With .RowTemplate
                .Height = DataGridViewImpagats.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            .Rows(0).DefaultCellStyle.BackColor = maxisrvr.COLOR_NOTOK

            With .Columns(ColsStat.Fch)
                .HeaderText = "data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsStat.Deb)
                .HeaderText = "impagos"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsStat.Hab)
                .HeaderText = "recobros"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsStat.Sdo)
                .HeaderText = "saldo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With

        Return oBitmap
    End Function

    Private Function CurrentStatYea() As Integer
        Dim iYea As Integer
        If ComboBoxStatYea.SelectedIndex >= 0 Then
            iYea = ComboBoxStatYea.SelectedValue
        Else
            iYea = Today.Year
        End If
        Return iYea
    End Function

    Private Sub LoadStatYeas()
        Dim SQL As String = "SELECT  CCB.yea " _
        & "FROM CCB INNER JOIN " _
        & "PGCCTA ON CCB.PgcPlan = PGCCTA.PgcPlan AND PGCCTA.Cod =" & DTOPgcPlan.ctas.impagats & " " _
        & "WHERE EMP=" & mEmp.Id & " AND " _
        & "YEA>2000 " _
        & "GROUP BY CCB.yea " _
        & "ORDER BY CCB.yea DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With ComboBoxStatYea
            .DataSource = oDs.Tables(0)
            .ValueMember = "YEA"
            .DisplayMember = "YEA"
            If oDs.Tables(0).Rows.Count > 0 Then
                .SelectedIndex = 0
            End If
        End With
    End Sub

    Private Function CreateStatTable() As DataTable
        Dim oTb As New DataTable
        With oTb
            .Columns.Add("FCH", System.Type.GetType("System.DateTime"))
            .Columns.Add("EUR", System.Type.GetType("System.Decimal"))
        End With
        Return oTb
    End Function



    Private Sub ComboBoxStatYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxStatYea.SelectedIndexChanged
        If mAllowEvents Then
            PictureBoxGraph.Image = StatImg()
            mDirtyStat = False
        End If
    End Sub
End Class