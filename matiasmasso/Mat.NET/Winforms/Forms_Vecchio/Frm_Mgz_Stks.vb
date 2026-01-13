

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
        Xl_MatDateTimePicker1.Value = Today
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
        Return Xl_MatDateTimePicker1.Value
    End Function

    Private Sub LoadMgzs()
        Dim oMgzs As List(Of DTOMgz) = BLL.BLLMgzs.Actius(Xl_MatDateTimePicker1.Value)
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
        sb.AppendLine("SUM(CASE WHEN Y.DIAS <" & DiesDesvaloritzacio50perCent() & " THEN Y.STK * Eur WHEN DIAS >=" & DiesDesvaloritzacio100perCent() & " THEN 0 ELSE ROUND(Y.STK * Y.EUR / 2, 2) END) As DTOAmt, ")
        sb.AppendLine("SUM(CASE WHEN Y.DIAS <" & DiesDesvaloritzacio50perCent() & " THEN 0 WHEN DIAS >=" & DiesDesvaloritzacio100perCent() & " THEN Y.STK * Eur ELSE ROUND(Y.STK * Y.EUR / 2, 2) END) AS OUT ")
        sb.AppendLine("FROM (SELECT X.MgzGuid, X.Category, X.ArtGuid, X.STK, MAX(ARC_1.eur*(100-Arc_1.Dto)/100) AS Eur, DATEDIFF(d, X.LASTIN, '" & sFch & "') AS DIAS ")

        sb.AppendLine("FROM (SELECT ARC.MgzGuid, Art.Category, ARC.ArtGuid, SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) AS STK, ")
        sb.AppendLine("MAX(CASE WHEN COD < 50 AND Arc.Eur > 0 THEN FCH ELSE '01/01/2000' END) AS LASTIN, ")
        sb.AppendLine("MAX(CASE WHEN COD < 50 THEN Arc.Eur*(100-Arc.Dto)/100 ELSE 0 END) AS Cost ")
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

        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
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
            If Not IsDBNull(oRow.Cells(Cols.Guid).Value) Then
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                retval = New DTOProductBrand(oGuid)
            End If
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
    DataGridViewTpas.MouseDown,
     DataGridViewStps.MouseDown,
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

        If oBrand Is Nothing Then
            DataGridViewStps.DataSource = Nothing
        Else

            Dim DtFch As Date = CurrentFch()
            Dim sFch As String = Format(DtFch, "yyyyMMdd")
            Dim iYea As Integer = DtFch.Year
            Dim FLD_INVENTARI As String = "SUM((CAST(SUBSTRING(J.SKEY, CHARINDEX('STK=', J.SKEY) + 4, 8) AS INT) - 50000000) * (CAST(SUBSTRING(J.SKEY,CHARINDEX('PMC=', J.SKEY) + 4, 11) AS MONEY) - 50000000))"

            Dim SQL As String = "SELECT  STP.Guid, STP.DSC, " _
        & "SUM(CASE WHEN Y.DIAS <" & DiesDesvaloritzacio50perCent() & " THEN Y.STK * Eur WHEN DIAS >=" & DiesDesvaloritzacio100perCent() & " THEN 0 ELSE ROUND(Y.STK * Y.EUR / 2, 2) END) As DTOAmt, " _
        & "SUM(CASE WHEN Y.DIAS <" & DiesDesvaloritzacio50perCent() & " THEN 0 WHEN DIAS >=" & DiesDesvaloritzacio100perCent() & " THEN Y.STK * Eur ELSE ROUND(Y.STK * Y.EUR / 2, 2) END) AS OUT " _
        & "FROM (SELECT X.MgzGuid, X.Category, X.ArtGuid, X.STK, MAX(ARC_1.eur*(100-Arc_1.Dto)/100) AS Eur, DATEDIFF(d, X.LASTIN, '" & sFch & "') AS DIAS " _
        & "FROM (SELECT ARC.MgzGuid, ART.Category, ARC.ArtGuid, SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) AS STK, " _
            & "MAX(CASE WHEN COD < 50 AND EUR > 0 THEN FCH ELSE '01/01/2000' END) AS LASTIN, " _
            & "MAX(CASE WHEN COD < 50 THEN Arc.EUR*(100-Arc.Dto)/100 ELSE 0 END) AS COST " _
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

            Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
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
        End If
    End Sub

    Private Function CurrentCategory() As DTOProductCategory
        Dim retval As DTOProductCategory = Nothing
        Dim oRow As DataGridViewRow = DataGridViewStps.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Guid).Value) Then
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                retval = New DTOProductCategory(oGuid)
            End If
        End If
        Return retval
    End Function

    Private Sub SetContextMenuStps()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCategory = CurrentCategory()


        If oCategory IsNot Nothing Then
            oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_StpExcel)
            oContextMenu.Items.Add("-")
            Dim oMenu_Stp As New Menu_ProductCategory(oCategory)
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
        Dim oSheet As DTOExcelSheet = UIHelper.GetExcelFromDataGridView(DataGridViewArts)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
#End Region

#Region "Arts"

    Private Sub LoadArts()
        Cursor = Cursors.WaitCursor

        Dim oCategory = CurrentCategory()

        If oCategory Is Nothing Then
            DataGridViewArts.DataSource = Nothing
        Else
            Dim sFch As String = Format(CurrentFch(), "yyyyMMdd")

            Dim SQL As String = "SELECT X.ArtGuid, X.ord, X.LASTIN, X.STK, MAX(ARC_1.eur*(100-Arc_1.Dto)/100) AS Eur, DATEDIFF(d, X.LASTIN, '" & sFch & "') AS DIAS, " _
        & "(CASE WHEN DATEDIFF(d, X.LASTIN, '" & sFch & "')<" & DiesDesvaloritzacio50perCent() & " THEN X.STK * MAX(ARC_1.eur*(100-Arc_1.Dto)/100) WHEN DATEDIFF(d, X.LASTIN, '" & sFch & "') >=" & DiesDesvaloritzacio100perCent() & " THEN 0 ELSE round(X.STK * MAX(ARC_1.eur*(100-Arc_1.Dto)/100) / 2, 2) END) As DTOAmt, " _
        & "(CASE WHEN DATEDIFF(d, X.LASTIN, '" & sFch & "')<" & DiesDesvaloritzacio50perCent() & " THEN 0 WHEN DATEDIFF(d, X.LASTIN, '" & sFch & "') >=" & DiesDesvaloritzacio100perCent() & " THEN X.STK * MAX(ARC_1.eur*(100-Arc_1.Dto)/100) ELSE round(X.STK * MAX(ARC_1.eur*(100-Arc_1.Dto)/100) / 2, 2) END) As DTOAmt " _
        & "FROM (SELECT ARC.MgzGuid, ART.Category, ARC.ArtGuid, ART.ORD, SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) AS STK, MAX(CASE WHEN COD < 50 AND EUR > 0 THEN FCH ELSE '01/01/2000' END) AS LASTIN, MAX(CASE WHEN COD < 50 THEN EUR ELSE 0 END) AS COST " _
        & "FROM ARC INNER JOIN " _
        & "ART ON ARC.ArtGuid = ART.Guid " _
        & "WHERE ART.nomgz = 0 AND ARC.FCH<='" & sFch & "' " _
        & "GROUP BY ARC.MgzGuid, ART.Category, ARC.artGuid, ART.ord " _
        & "HAVING (SUM(CASE WHEN COD < 50 THEN QTY ELSE - QTY END) <> 0)) AS X INNER JOIN " _
        & "ARC AS ARC_1 ON X.MgzGuid = ARC_1.MgzGuid AND X.artGuid = ARC_1.artGuid AND X.LASTIN = ARC_1.fch AND ARC_1.cod < 50 " _
        & "WHERE x.MgzGuid ='" & mMgz.Guid.ToString & "' AND " _
        & "x.Category ='" & oCategory.Guid.ToString & "' " _
        & "GROUP BY X.ArtGuid, X.ord, X.LASTIN, X.STK"


            Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
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
        End If

    End Sub


    Private Function CurrentSku() As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim oRow As DataGridViewRow = DataGridViewArts.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Guid).Value) Then
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                retval = New DTOProductSku(oGuid)
            End If
        End If
        Return retval
    End Function

    Private Sub SetContextMenuArts()
        Dim oContextMenu As New ContextMenuStrip
        Dim oSku As DTOProductSku = CurrentSku()

        If oSku IsNot Nothing Then
            Dim oMenu_Sku As New Menu_ProductSku(oSku)
            'AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Sku.Range)
        End If

        DataGridViewArts.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridViewArts_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewArts.SelectionChanged
        If mAllowEvents Then
            SetContextMenuArts()
        End If
    End Sub

#End Region



    Private Sub ButtonRefresca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRefresca.Click
        LoadMgzs()
        LoadTpas()
    End Sub

    Private Sub ButtonSaveCca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSaveCca.Click
        Dim DtFch As Date = Xl_MatDateTimePicker1.Value

        Dim oCtaMercancies As DTOPgcCta = BLLPgcCta.FromCod(DTOPgcPlan.Ctas.mercancies)
        Dim oLastAmt As DTOAmt = BLLPgcCta.Saldo(oCtaMercancies, , DtFch).ReverseSign
        Dim oAmt As DTOAmt = BLLApp.GetAmt(Math.Abs(mTotTpa + mOutTpa - oLastAmt.Eur))
        If oAmt.Eur <> 0 Then
            Dim oCca As DTOCca = BLLCca.Factory(DtFch, BLLSession.Current.User, DTOCca.CcdEnum.InventariMensual)
            With oCca
                .Concept = "inventari " & BLL.BLLLang.CAT.Mes(DtFch.Month).ToLower & " " & mMgz.Nom
                .Cdn = DtFch.Month
            End With

            Dim increment As Boolean = (mTotTpa + mOutTpa) < oLastAmt.Eur
            If increment Then
                BLLCca.AddDebit(oCca, oAmt, DTOPgcPlan.Ctas.Inventari)
            Else
                BLLCca.AddCredit(oCca, oAmt, DTOPgcPlan.Ctas.Inventari)
            End If
            BLLCca.AddSaldo(oCca, DTOPgcPlan.Ctas.mercancies)

            Dim exs As New List(Of Exception)
            If BLLCca.Update(oCca, exs) Then
                Dim sb As New Text.StringBuilder
                sb.AppendLine(String.Format("{0} de stocks per {1}", IIf(increment, "Increment", "Reducció"), oAmt.CurFormatted))
                sb.AppendLine(String.Format("{0} a l'assentament {1} del {2}", IIf(increment, "registrat", "registrada"), oCca.Id, Format(oCca.Fch, "dd/MM/yy")))
                MsgBox(sb.ToString, MsgBoxStyle.Information)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub ComboBoxMgz_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMgz.SelectedIndexChanged
        If mAllowEvents Then
            mMgz = ComboBoxMgz.SelectedItem
            refresca()
        End If
    End Sub


End Class
