

Public Class Frm_Mgz_Stks

    Private mMgz As DTOMgz
    Private mStk As Integer
    Private mTot As Decimal
    Private mOut As Decimal
    Private mTotTpa As Decimal
    Private mOutTpa As Decimal
    Private mAllowEvents As Boolean


    Private Enum Cols
        Guid
        nom
        eur
        out
    End Enum

    Private Enum ColsArt
        Guid
        nom
        fch
        stk
        eur
        dias
        amt
        out
    End Enum

    Public Sub New(oMgz As DTOMgz)
        MyBase.New()
        Me.InitializeComponent()
        mMgz = oMgz
    End Sub

    Private Sub Frm_Mgz_Stks_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "STOCKS " & mMgz.Nom
        DateTimePicker1.Value = Today
        TextBoxDias50.Text = 90
        TextBoxDias100.Text = 180
        LoadMgzs()
        refresca()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        LoadTpas()
        LoadStps()
        LoadArts()
    End Sub

    Private Function DiesDesvaloritzacio50perCent() As Integer
        Dim RetVal As Integer = 0
        Dim tmp As String = TextBoxDias50.Text
        If IsNumeric(tmp) Then RetVal = CInt(tmp)
        Return RetVal
    End Function

    Private Function DiesDesvaloritzacio100perCent() As Integer
        Dim RetVal As Integer = 0
        Dim tmp As String = TextBoxDias100.Text
        If IsNumeric(tmp) Then RetVal = CInt(tmp)
        Return RetVal
    End Function

    Private Function CurrentFch() As Date
        Return DateTimePicker1.Value
    End Function

    Private Sub LoadMgzs()
        Dim oMgzs As List(Of DTOMgz) = BLL.BLLMgzs.Actius()
        With ComboBoxMgz
            .DataSource = oMgzs
            .SelectedItem = oMgzs.Find(Function(x) x.Equals(mMgz))
            .DisplayMember = "Nom"
        End With
    End Sub
   
#Region "Tpas"

    Private Sub LoadTpas()
        Cursor = Cursors.WaitCursor

        Dim DtFch As Date = CurrentFch()
        Dim sFch As String = Format(DtFch, "yyyyMMdd")
        Dim iYea As Integer = DtFch.Year
        Dim FLD_INVENTARI As String = "SUM((CAST(SUBSTRING(J.SKEY, CHARINDEX('STK=', J.SKEY) + 4, 8) AS INT) - 50000000) * (CAST(SUBSTRING(J.SKEY,CHARINDEX('PMC=', J.SKEY) + 4, 11) AS MONEY) - 50000000))"

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT  TPA.Guid, TPA.DSC, ")
        sb.AppendLine("SUM(CASE WHEN Y.DIAS <" & DiesDesvaloritzacio50perCent() & " THEN Y.STK * Eur WHEN DIAS >=" & DiesDesvaloritzacio100perCent() & " THEN 0 ELSE ROUND(Y.STK * Y.EUR / 2, 2) END) AS AMT, ")
        sb.AppendLine("SUM(CASE WHEN Y.DIAS <" & DiesDesvaloritzacio50perCent() & " THEN 0 WHEN DIAS >=" & DiesDesvaloritzacio100perCent() & " THEN Y.STK * Eur ELSE ROUND(Y.STK * Y.EUR / 2, 2) END) AS OUT ")
        sb.AppendLine("FROM (SELECT X.MgzGuid, X.Category, X.ArtGuid, X.STK, MAX(ARC_1.eur) AS Eur, DATEDIFF(d, X.LASTIN, '" & sFch & "') AS DIAS ")

        sb.AppendLine("FROM (SELECT ARC.MgzGuid, Art.Category, ARC.ArtGuid, SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) AS STK, ")
        sb.AppendLine("MAX(CASE WHEN COD < 50 AND EUR > 0 THEN FCH ELSE '01/01/2000' END) AS LASTIN, ")
        sb.AppendLine("MAX(CASE WHEN COD < 50 THEN EUR ELSE 0 END) AS COST ")
        sb.AppendLine("FROM ARC ")
        sb.AppendLine("INNER JOIN ART ON ARC.ArtGuid = ART.Guid ")
        sb.AppendLine("WHERE Art.NoMgz = 0 AND ARC.FCH<='" & sFch & "' ")
        sb.AppendLine("GROUP BY ARC.MgzGuid, Art.Category, ARC.ArtGuid ")
        sb.AppendLine("HAVING (SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) <> 0)) AS X ")

        sb.AppendLine("INNER JOIN ARC AS ARC_1 ON X.MgzGuid = ARC_1.MgzGuid AND X.ArtGuid = ARC_1.ArtGuid AND X.LASTIN = ARC_1.fch AND ARC_1.cod < 50 ")
        sb.AppendLine("GROUP BY X.MgzGuid, X.Category, X.ArtGuid, X.STK, X.LASTIN) AS Y ")

        sb.AppendLine("INNER JOIN Stp ON Y.Category = Stp.Guid ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand = Tpa.Guid ")
        sb.AppendLine("WHERE Y.MgzGuid ='" & mMgz.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY TPA.Guid, TPA.DSC, TPA.ORD ")
        sb.AppendLine("ORDER BY TPA.ORD")
        Dim SQL As String = sb.ToString

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        mTot = 0
        mOut = 0
        For Each oRow In oTb.Rows
            mTot += oRow(Cols.eur)
            mOut += oRow(Cols.out)
        Next

        oRow = oTb.NewRow
        oRow(Cols.Guid) = System.DBNull.Value
        oRow(Cols.eur) = mTot
        oRow(Cols.out) = mOut
        oRow(Cols.nom) = "totals"
        oTb.Rows.Add(oRow)

        mTotTpa = mTot
        mOutTpa = mOut

        With DataGridViewTpas
            With .RowTemplate
                .Height = DataGridViewTpas.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.nom)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.eur)
                .HeaderText = "inventari"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.out)
                .HeaderText = "obsolets"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.ForeColor = Color.Gray
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            If .Rows.Count > 0 Then
                .CurrentCell = .Rows(0).Cells(Cols.nom)
                With .Rows(.Rows.Count - 1)
                    .DefaultCellStyle.BackColor = Color.LightGray
                End With
            End If


            Cursor = Cursors.Default
        End With
    End Sub

    Private Function CurrentBrand() As DTOProductBrand
        Dim retval As DTOProductBrand = Nothing
        Dim oRow As DataGridViewRow = DataGridViewTpas.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            retval = New DTOProductBrand(oGuid)
        End If
        Return retval
    End Function

    Private Sub SetContextMenuTpas()
        Dim oContextMenu As New ContextMenuStrip
        Dim oBrand As DTOProductBrand = CurrentBrand()

        If oBrand IsNot Nothing Then
            Dim oMenu_Brand As New Menu_ProductBrand(oBrand)
            'AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Brand.Range)
        End If

        DataGridViewTpas.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _
    DataGridViewTpas.MouseDown, _
     DataGridViewStps.MouseDown, _
      DataGridViewArts.MouseDown
        mAllowEvents = True
    End Sub

    Private Sub DataGridViewTpas_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewTpas.SelectionChanged
        If mAllowEvents Then
            SetContextMenuTpas()
            LoadStps()
        End If
    End Sub

#End Region

#Region "Stps"

    Private Sub LoadStps()
        Cursor = Cursors.WaitCursor

        Dim oBrand As DTOProductBrand = CurrentBrand()
        Dim DtFch As Date = CurrentFch()
        Dim sFch As String = Format(DtFch, "yyyyMMdd")
        Dim iYea As Integer = DtFch.Year
        Dim FLD_INVENTARI As String = "SUM((CAST(SUBSTRING(J.SKEY, CHARINDEX('STK=', J.SKEY) + 4, 8) AS INT) - 50000000) * (CAST(SUBSTRING(J.SKEY,CHARINDEX('PMC=', J.SKEY) + 4, 11) AS MONEY) - 50000000))"

        Dim SQL As String = "SELECT  STP.Guid, STP.DSC, " _
        & "SUM(CASE WHEN Y.DIAS <" & DiesDesvaloritzacio50perCent() & " THEN Y.STK * Eur WHEN DIAS >=" & DiesDesvaloritzacio100perCent() & " THEN 0 ELSE ROUND(Y.STK * Y.EUR / 2, 2) END) AS AMT, " _
        & "SUM(CASE WHEN Y.DIAS <" & DiesDesvaloritzacio50perCent() & " THEN 0 WHEN DIAS >=" & DiesDesvaloritzacio100perCent() & " THEN Y.STK * Eur ELSE ROUND(Y.STK * Y.EUR / 2, 2) END) AS OUT " _
        & "FROM (SELECT X.MgzGuid, X.Category, X.ArtGuid, X.STK, MAX(ARC_1.eur) AS Eur, DATEDIFF(d, X.LASTIN, '" & sFch & "') AS DIAS " _
        & "FROM (SELECT ARC.MgzGuid, ART.Category, ARC.ArtGuid, SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) AS STK, " _
            & "MAX(CASE WHEN COD < 50 AND EUR > 0 THEN FCH ELSE '01/01/2000' END) AS LASTIN, " _
            & "MAX(CASE WHEN COD < 50 THEN EUR ELSE 0 END) AS COST " _
            & "FROM  ARC INNER JOIN ART ON ARC.ArtGuid = ART.Guid " _
            & "WHERE(Art.NoMgz = 0) AND ARC.FCH<='" & sFch & "' " _
            & "GROUP BY ARC.MgzGuid, ART.Category, ARC.ArtGuid " _
            & "HAVING (SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) <> 0)) AS X " _
        & "INNER JOIN ARC AS ARC_1 ON X.MgzGuid = ARC_1.MgzGuid AND X.ArtGuid = ARC_1.ArtGuid AND X.LASTIN = ARC_1.fch AND ARC_1.cod < 50 " _
        & "GROUP BY X.MgzGuid, X.Category, X.ArtGuid, X.STK, X.LASTIN) AS Y INNER JOIN " _
        & "STP ON Y.Category =STP.Guid " _
        & "WHERE Y.MgzGuid ='" & mMgz.Guid.ToString & "' AND " _
        & "Stp.Brand ='" & oBrand.Guid.ToString & "' " _
        & "GROUP BY STP.Guid, STP.DSC, STP.ORD " _
        & "ORDER BY STP.ORD"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        mTot = 0
        mOut = 0
        For Each oRow In oTb.Rows
            mTot += oRow(Cols.eur)
            mOut += oRow(Cols.out)
        Next

        oRow = oTb.NewRow
        oRow(Cols.Guid) = System.DBNull.Value
        oRow(Cols.eur) = mTot
        oRow(Cols.out) = mOut
        oRow(Cols.nom) = "totals"
        oTb.Rows.Add(oRow)

        With DataGridViewStps
            With .RowTemplate
                .Height = DataGridViewTpas.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.nom)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.eur)
                .HeaderText = "inventari"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.out)
                .HeaderText = "obsolets"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.ForeColor = Color.Gray
            End With
            If .Rows.Count > 0 Then
                .CurrentCell = .Rows(0).Cells(Cols.nom)
                With .Rows(.Rows.Count - 1)
                    .DefaultCellStyle.BackColor = Color.LightGray
                End With
            End If
        End With
        Cursor = Cursors.Default
    End Sub

    Private Function CurrentStp() As Stp
        Dim oStp As Stp = Nothing
        Dim oRow As DataGridViewRow = DataGridViewStps.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            oStp = New Stp(oGuid)
        End If
        Return oStp
    End Function

    Private Sub SetContextMenuStps()
        Dim oContextMenu As New ContextMenuStrip
        Dim oStp As Stp = CurrentStp()


        If oStp IsNot Nothing Then
            oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_StpExcel)
            oContextMenu.Items.Add("-")
            Dim oMenu_Stp As New Menu_ProductCategory(oStp)
            'AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Stp.Range)
        End If

        DataGridViewStps.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridViewStps_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewStps.SelectionChanged
        If mAllowEvents Then
            SetContextMenuStps()
            LoadArts()
        End If
    End Sub

    Private Sub Do_StpExcel(ByVal sender As Object, ByVal e As System.EventArgs)
        MatExcel.GetExcelFromDataGridView(DataGridViewArts).Visible = True
    End Sub
#End Region

#Region "Arts"

    Private Sub LoadArts()
        Cursor = Cursors.WaitCursor

        Dim oStp As Stp = CurrentStp()
        Dim sFch As String = Format(CurrentFch(), "yyyyMMdd")

        Dim SQL As String = "SELECT X.ArtGuid, X.ord, X.LASTIN, X.STK, MAX(ARC_1.eur) AS Eur, DATEDIFF(d, X.LASTIN, '" & sFch & "') AS DIAS, " _
        & "(CASE WHEN DATEDIFF(d, X.LASTIN, '" & sFch & "')<" & DiesDesvaloritzacio50perCent() & " THEN X.STK * MAX(ARC_1.eur) WHEN DATEDIFF(d, X.LASTIN, '" & sFch & "') >=" & DiesDesvaloritzacio100perCent() & " THEN 0 ELSE round(X.STK * MAX(ARC_1.eur) / 2, 2) END) AS AMT, " _
        & "(CASE WHEN DATEDIFF(d, X.LASTIN, '" & sFch & "')<" & DiesDesvaloritzacio50perCent() & " THEN 0 WHEN DATEDIFF(d, X.LASTIN, '" & sFch & "') >=" & DiesDesvaloritzacio100perCent() & " THEN X.STK * MAX(ARC_1.eur) ELSE round(X.STK * MAX(ARC_1.eur) / 2, 2) END) AS AMT " _
        & "FROM (SELECT ARC.MgzGuid, ART.Category, ARC.ArtGuid, ART.ORD, SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) AS STK, MAX(CASE WHEN COD < 50 AND EUR > 0 THEN FCH ELSE '01/01/2000' END) AS LASTIN, MAX(CASE WHEN COD < 50 THEN EUR ELSE 0 END) AS COST " _
        & "FROM ARC INNER JOIN " _
        & "ART ON ARC.ArtGuid = ART.Guid " _
        & "WHERE ART.nomgz = 0 AND ARC.FCH<='" & sFch & "' " _
        & "GROUP BY ARC.MgzGuid, ART.Category, ARC.artGuid, ART.ord " _
        & "HAVING (SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) <> 0)) AS X INNER JOIN " _
        & "ARC AS ARC_1 ON X.MgzGuid = ARC_1.MgzGuid AND X.artGuid = ARC_1.artGuid AND X.LASTIN = ARC_1.fch AND ARC_1.cod < 50 " _
        & "WHERE x.MgzGuid ='" & mMgz.Guid.ToString & "' AND " _
        & "x.Category ='" & oStp.Guid.ToString & "' " _
        & "GROUP BY X.ArtGuid, X.ord, X.LASTIN, X.STK"


        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        mTot = 0
        mStk = 0
        mOut = 0
        For Each oRow In oTb.Rows
            mTot += oRow(ColsArt.amt)
            mOut += oRow(ColsArt.out)
            mStk += oRow(ColsArt.stk)
        Next

        oRow = oTb.NewRow
        oRow(ColsArt.Guid) = System.DBNull.Value
        oRow(ColsArt.stk) = mStk
        oRow(ColsArt.eur) = 0
        oRow(ColsArt.amt) = mTot
        oRow(ColsArt.out) = mOut
        oRow(ColsArt.nom) = "totals"
        oTb.Rows.Add(oRow)

        With DataGridViewArts
            With .RowTemplate
                .Height = DataGridViewTpas.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(ColsArt.Guid)
                .Visible = False
            End With
            With .Columns(ColsArt.nom)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsArt.stk)
                .HeaderText = "stock"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsArt.eur)
                .HeaderText = "cost"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsArt.dias)
                .HeaderText = "dias"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsArt.amt)
                .HeaderText = "inventari"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsArt.out)
                .HeaderText = "obsolets"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.ForeColor = Color.Gray
            End With
            With .Columns(ColsArt.fch)
                .HeaderText = "ult.compra"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            If .Rows.Count > 0 Then
                .CurrentCell = .Rows(0).Cells(Cols.nom)
                With .Rows(.Rows.Count - 1)
                    .DefaultCellStyle.BackColor = Color.LightGray
                End With
            End If
        End With
        Cursor = Cursors.Default
    End Sub


    Private Function CurrentArt() As Art
        Dim oArt As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridViewArts.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            oArt = New Art(oGuid)
        End If
        Return oArt
    End Function

    Private Sub SetContextMenuArts()
        Dim oContextMenu As New ContextMenuStrip
        Dim oArt As Art = CurrentArt()

        If oArt IsNot Nothing Then
            Dim oMenu_Art As New Menu_Art(oArt)
            'AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Art.Range)
        End If

        DataGridViewArts.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridViewArts_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewArts.SelectionChanged
        If mAllowEvents Then
            SetContextMenuArts()
        End If
    End Sub

#End Region



    Private Sub RecuentoToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECUENTOToolStripButton.Click
        Dim sFch As String = Format(DateTimePicker1.Value, "yyyyMMdd")
        Dim oMgz As DTOMgz = BLL.BLLApp.Mgz
        Dim SQL As String = "SELECT X.tpa, X.stp, X.art, X.MYD,X.QTY, X.LASTIN, MAX(ARC_1.eur) AS Expr1, DATEDIFF(d, X.LASTIN, '" & sFch & "') AS DIAS " _
        & "FROM (SELECT ARC.emp, ART.tpa, ART.stp, ARC.art, ART.MYD, SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) AS QTY, MAX(CASE WHEN COD < 50 AND EUR > 0 THEN FCH ELSE '01/01/2000' END) AS LASTIN, MAX(CASE WHEN COD < 50 THEN EUR ELSE 0 END) AS COST " _
        & "FROM ARC INNER JOIN " _
        & "ART ON ARC.ArtGuid = ART.Guid " _
        & "WHERE (ARC.MgzGuid ='" & oMgz.Guid.ToString & "') AND (ART.nomgz = 0) AND ARC.FCH <= '" & sFch & "' " _
        & "GROUP BY ARC.emp, ART.tpa, ART.stp, ARC.art, ART.MYD " _
        & "HAVING (SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) <> 0)) AS X INNER JOIN " _
        & "ARC AS ARC_1 ON X.emp = ARC_1.emp AND X.art = ARC_1.art AND X.LASTIN = ARC_1.fch AND ARC_1.cod < 50 " _
        & "GROUP BY X.tpa, X.stp, X.art, X.MYD, X.QTY, X.LASTIN"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        MatExcel.GetExcelFromDataset(oDs).Visible = True
    End Sub

    Private Sub ButtonRefresca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRefresca.Click
        LoadTpas()
    End Sub

    Private Sub ButtonSaveCca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSaveCca.Click
        Dim DtFch As Date = DateTimePicker1.Value
        Dim oPlan As PgcPlan = PgcPlan.FromYear(DtFch.Year)

        Dim oCca As Cca = Nothing
        Dim oCtaDespeses As PgcCta = Nothing
        Dim oCtaMercancies As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.mercancies)
        Dim sTxt As String = ""
        Dim oDhDespeses As DTOCcb.DhEnum

        Dim oLastAmt As MaxiSrvr.Amt = oCtaMercancies.GetSaldo(BLL.BLLApp.Emp, DtFch)
        Dim oAmt As New maxisrvr.Amt(Math.Abs(mTotTpa + mOutTpa - oLastAmt.Eur))
        If oAmt.Eur <> 0 Then
            oCca = New Cca(BLL.BLLApp.Emp)
            oCtaDespeses = oPlan.Cta(DTOPgcPlan.Ctas.Inventari)
            oCtaMercancies = oPlan.Cta(DTOPgcPlan.Ctas.mercancies)
            sTxt = "inventari " & BLL.BLLLang.CAT.Mes(DtFch.Month).ToLower & " " & mMgz.Nom
            oDhDespeses = IIf((mTotTpa + mOutTpa) < oLastAmt.Eur, DTOCcb.DhEnum.Debe, DTOCcb.DhEnum.Haber)
            With oCca
                .Txt = sTxt
                .Ccd = DTOCca.CcdEnum.InventariMensual
                .Cdn = DtFch.Month
                .fch = DtFch
                .ccbs.Add(New Ccb(oCtaDespeses, Nothing, oAmt, oDhDespeses))
                .ccbs.Add(New Ccb(oCtaMercancies, Nothing, oAmt, IIf(oDhDespeses = DTOCcb.DhEnum.Debe, DTOCcb.DhEnum.Haber, DTOCcb.DhEnum.Debe)))
                Dim exs As New List(Of Exception)
                If Not .Update(exs) Then
                    MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If
            End With
        End If


        oLastAmt = oCtaMercancies.GetSaldo(BLL.BLLApp.Emp, DtFch).Times(-1)
        oAmt = New MaxiSrvr.Amt(Math.Abs(mOutTpa - oLastAmt.Eur))
        If oAmt.Eur <> 0 Then
            oCca = New Cca(BLL.BLLApp.Emp)
            oCtaDespeses = oPlan.Cta(DTOPgcPlan.Ctas.InventariDesvaloritzacio)
            oCtaMercancies = oPlan.Cta(DTOPgcPlan.Ctas.mercanciesObsoletes)
            sTxt = "desvalorització inventari " & BLL.BLLLang.CAT.Mes(DtFch.Month).ToLower & " " & mMgz.Nom
            oDhDespeses = IIf(mOutTpa > oLastAmt.Eur, DTOCcb.DhEnum.Debe, DTOCcb.DhEnum.Haber)
            With oCca
                .Txt = sTxt
                .Ccd = DTOCca.CcdEnum.InventariMensualDesvaloritzacio
                .Cdn = DtFch.Month
                .fch = DtFch
                .ccbs.Add(New Ccb(oCtaDespeses, Nothing, oAmt, oDhDespeses))
                .ccbs.Add(New Ccb(oCtaMercancies, Nothing, oAmt, IIf(oDhDespeses = DTOCcb.DhEnum.Debe, DTOCcb.DhEnum.Haber, DTOCcb.DhEnum.Debe)))
                Dim exs As New List(Of Exception)
                If Not .Update(exs) Then
                    MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If
            End With
        End If
    End Sub

    Private Sub ComboBoxMgz_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMgz.SelectedIndexChanged
        If mAllowEvents Then
            mMgz = ComboBoxMgz.SelectedItem
            refresca()
        End If
    End Sub
End Class
