
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_Fiscal_Mod347_Cli
    Private mMod347Itm As Mod347Itm
    Private mDs As DataSet

    Private Enum Cols
        Cca
        Fch
        Txt
        Bas
        Iva
        Red
        Req
        Irpf
        Liq
    End Enum

    Public Sub New(ByVal oMod347Itm As Mod347Itm)
        MyBase.New()
        Me.InitializeComponent()
        mMod347Itm = oMod347Itm
        Me.Text = "Mod.347 VENDES " & mMod347Itm.Parent.Yea.ToString & " " & mMod347Itm.Contact.Clx
        ToolStripComboBoxOps.SelectedIndex = mMod347Itm.Op
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Select Case CurrentOp()
            Case Mod347Itm.Ops.Compras
                Dim oPlan As PgcPlan = mMod347Itm.Parent.PgcPlan
                Dim sCtaCompres As String = "6%"
                Dim sCtaInmovilitzat As String = "2%"
                Dim sCtaIva As String = oPlan.Cta(DTOPgcPlan.ctas.IvaSoportat).Id.Substring(0, 3) & "%"
                Dim sCtaIrpf As String = oPlan.Cta(DTOPgcPlan.ctas.Irpf).Id.Substring(0, 4) & "%"
                Dim sCtaRetencions As String = oPlan.Cta(DTOPgcPlan.ctas.RetencionsInteresos).Id.Substring(0, 4) & "%"

                Dim SQL As String = "SELECT CCA.CCA, CCA.FCH, CCA.TXT, " _
                 & "SUM(CASE WHEN CCB_BASE.DH=1 THEN CCB_BASE.eur ELSE -CCB_BASE.eur END) AS BASE, " _
                 & "SUM(CASE WHEN CCB_IVA.DH=1 THEN CCB_IVA.eur ELSE -CCB_IVA.EUR END) AS IVA, " _
                 & "0 as RED, 0 AS REQ, " _
                 & "SUM(CASE WHEN CCB_IRPF.DH=1 THEN CCB_IRPF.eur ELSE -CCB_IRPF.EUR END) AS IRPF " _
                 & "FROM CCB AS CCB_BASE INNER JOIN " _
                 & "CCA ON CCB_BASE.Emp = CCA.emp AND CCB_BASE.yea = CCA.yea AND CCB_BASE.Cca = CCA.cca AND (CCB_BASE.cta LIKE @CTACOMPRES OR CCB_BASE.cta LIKE @CTAINMOVILITZAT) LEFT OUTER JOIN " _
                 & "CCB AS CCB_IVA ON CCA.emp = CCB_IVA.Emp AND CCA.yea = CCB_IVA.yea AND CCA.cca = CCB_IVA.Cca AND CCB_IVA.cta LIKE @CTAIVA LEFT OUTER JOIN " _
                 & "CCB AS CCB_IRPF ON CCA.emp = CCB_IRPF.Emp AND CCA.yea = CCB_IRPF.yea AND CCA.cca = CCB_IRPF.Cca AND (CCB_IRPF.cta LIKE @CTAIRPF OR CCB_IRPF.cta LIKE @CTARETENCIONS) " _
                 & "WHERE CCA.emp =@EMP AND CCA.yea =@YEA AND CCB_BASE.CLI=@CONTACT " _
                 & "GROUP BY CCA.CCA, CCA.FCH, CCA.TXT " _
                 & "ORDER BY CCA.FCH, CCA.CCA"

                mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mMod347Itm.Parent.Emp.Id, "@YEA", mMod347Itm.Parent.Yea, "@CONTACT", mMod347Itm.Contact.Id, "@CTACOMPRES", sCtaCompres, "@CTAINMOVILITZAT", sCtaInmovilitzat, "@CTAIVA", sCtaIva, "@CTAIRPF", sCtaIrpf, "@CTARETENCIONS", sCtaRetencions)
            Case Else
                Dim oPlan As PgcPlan = mMod347Itm.Parent.PgcPlan
                Dim sCtaVendes As String = "7%"
                Dim sCtaIVA As String = oPlan.Cta(DTOPgcPlan.ctas.IvaRepercutit).Id
                Dim sCtaRED As String = oPlan.Cta(DTOPgcPlan.ctas.IvaReduit).Id
                Dim sCtaREQ As String = oPlan.Cta(DTOPgcPlan.ctas.IvaRecarrecEquivalencia).Id

                Dim SQL As String = "SELECT CCA.CCA, CCA.FCH, CCA.TXT, " _
                & "SUM(CASE WHEN CCB_BASE.DH=2 THEN CCB_BASE.eur ELSE -CCB_BASE.eur END) AS BASE, " _
                & "SUM(CASE WHEN CCB_IVA.DH=2 THEN CCB_IVA.eur ELSE -CCB_IVA.eur END) AS IVA, " _
                & "SUM(CASE WHEN CCB_RED.DH=2 THEN CCB_RED.eur ELSE -CCB_RED.eur END) AS RED, " _
                & "SUM(CASE WHEN CCB_REQ.DH=2 THEN CCB_REQ.eur ELSE -CCB_REQ.eur END) AS REQ, " _
                & "0 AS IRPF " _
                & "FROM CCB AS CCB_BASE INNER JOIN " _
                & "CCA ON CCB_BASE.Emp = CCA.emp AND CCB_BASE.yea = CCA.yea AND CCB_BASE.Cca = CCA.cca AND CCB_BASE.cta LIKE @CTAVENDES LEFT OUTER JOIN " _
                & "CCB AS CCB_IVA ON CCA.emp = CCB_IVA.Emp AND CCA.yea = CCB_IVA.yea AND CCA.cca = CCB_IVA.Cca AND CCB_IVA.cta LIKE @CTAIVA LEFT OUTER JOIN " _
                & "CCB AS CCB_RED ON CCA.emp = CCB_RED.Emp AND CCA.yea = CCB_RED.yea AND CCA.cca = CCB_RED.Cca AND CCB_RED.cta LIKE @CTARED LEFT OUTER JOIN " _
                & "CCB AS CCB_REQ ON CCA.emp = CCB_REQ.Emp AND CCA.yea = CCB_REQ.yea AND CCA.cca = CCB_REQ.Cca AND CCB_REQ.cta LIKE @CTAREQ " _
                & "WHERE CCA.emp =@EMP AND CCA.yea =@YEA AND CCB_BASE.CLI=@CONTACT " _
                & "GROUP BY CCA.CCA, CCA.FCH, CCA.TXT " _
                & "ORDER BY CCA.FCH, CCA.CCA"

                mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mMod347Itm.Parent.Emp.Id, "@YEA", mMod347Itm.Parent.Yea, "@CONTACT", mMod347Itm.Contact.Id, "@CTAVENDES", sCtaVendes, "@CTAIVA", sCtaIVA, "@CTARED", sCtaRED, "@CTAREQ", sCtaREQ)
        End Select
        Dim oTb As DataTable = mDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("LIQ", System.Type.GetType("System.Decimal"))

        Dim oRow As DataRow = Nothing
        Dim DcBas As Decimal
        Dim DcIva As Decimal
        Dim DcRed As Decimal
        Dim DcReq As Decimal
        Dim DcIrpf As Decimal
        Dim DcLiq As Decimal
        Dim DcTmp As Decimal

        Dim BlRedExists As Boolean = False
        Dim BlReqExists As Boolean = False
        Dim BlIrpfExists As Boolean = False

        For Each oRow In oTb.Rows

            DcTmp = oRow(Cols.Bas)
            DcBas += oRow(Cols.Bas)
            If Not IsDBNull(oRow(Cols.Iva)) Then
                DcTmp += oRow(Cols.Iva)
                DcIva += oRow(Cols.Iva)
            End If
            If Not IsDBNull(oRow(Cols.Red)) Then
                If oRow(Cols.Red) <> 0 Then
                    BlRedExists = True
                    DcTmp += oRow(Cols.Red)
                    DcRed += oRow(Cols.Red)
                End If
            End If
            If Not IsDBNull(oRow(Cols.Req)) Then
                If oRow(Cols.Req) <> 0 Then
                    BlReqExists = True
                    DcTmp += oRow(Cols.Req)
                    DcReq += oRow(Cols.Req)
                End If
            End If
            If Not IsDBNull(oRow(Cols.Irpf)) Then
                If oRow(Cols.Irpf) <> 0 Then
                    BlIrpfExists = True
                    DcTmp = DcTmp - oRow(Cols.Irpf)
                    DcIrpf += oRow(Cols.Irpf)
                End If
            End If
            oRow(Cols.Liq) = DcTmp
            DcLiq += DcTmp
        Next

        oRow = oTb.NewRow
        oRow(Cols.Txt) = "totals"
        oRow(Cols.Bas) = DcBas
        oRow(Cols.Iva) = DcIva
        oRow(Cols.Red) = DcRed
        oRow(Cols.Req) = DcReq
        oRow(Cols.Irpf) = DcIrpf
        oRow(Cols.Liq) = DcLiq
        oTb.Rows.InsertAt(oRow, 0)


        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Cca)
                .HeaderText = "assentament"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Txt)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Bas)
                .HeaderText = "Base"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Iva)
                .HeaderText = "Iva"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Red)
                If BlRedExists Then
                    .Visible = True
                    .HeaderText = "Iva Red."
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    .Visible = False
                End If
            End With
            With .Columns(Cols.Req)
                If BlReqExists Then
                    .Visible = True
                    .HeaderText = "Rec.Equival"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    .Visible = False
                End If
            End With
            With .Columns(Cols.Irpf)
                If BlIrpfExists Then
                    .Visible = True
                    .HeaderText = "Irpf"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    .Visible = False
                End If
            End With
            With .Columns(Cols.Liq)
                .HeaderText = "Liquid"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            Dim oSumRow As DataGridViewRow = .Rows(.RowCount - 1)
            oSumRow.DefaultCellStyle.BackColor = Color.Yellow
        End With
    End Sub

    Private Function CurrentOp() As Mod347Itm.Ops
        Dim iOp As Integer = ToolStripComboBoxOps.SelectedIndex
        Return CType(iOp, Mod347Itm.Ops)
    End Function


    Private Function CurrentCcas() As Ccas
        Dim oCcas As New Ccas
        Dim IntYea As Integer
        Dim LngId As Integer
        Dim oCca As Cca

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                IntYea = mMod347Itm.Parent.Yea
                LngId = oRow.Cells(Cols.Cca).Value
                oCca = MaxiSrvr.Cca.FromNum(mMod347Itm.Parent.Emp, IntYea, LngId)
                oCcas.Add(oCca)
            Next
        Else
            oCcas.Add(CurrentCca)
        End If
        Return oCcas
    End Function


    Private Function CurrentCca() As Cca
        Dim oCca As Cca = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iCca As Integer = oRow.Cells(Cols.Cca).Value
            oCca = MaxiSrvr.Cca.FromNum(mMod347Itm.Parent.Emp, mMod347Itm.Parent.Yea, iCca)
        End If
        Return oCca
    End Function

    
    Private Sub ExcelToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExcelToolStripButton.Click
        Cursor = Cursors.WaitCursor
        MatExcel.GetExcelFromDataset(mDs).Visible = True
        Cursor = Cursors.Default
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oCca As Cca = CurrentCca()
        root.ShowCca(oCca)
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        LoadGrid()
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Try
            Dim oCcas As Ccas = CurrentCcas()

            If oCcas.Count > 0 Then
                Dim oMenu_Cca As New Menu_Cca(oCcas)
                AddHandler oMenu_Cca.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Cca.Range)
            End If
        Catch ex As Exception

        End Try

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Txt

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If mDs.Tables(0).Rows.Count = 0 Then
            MsgBox("any buit", MsgBoxStyle.Exclamation)
        Else
            Try
                DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

                If i > DataGridView1.Rows.Count - 1 Then
                    DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
                Else
                    DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
                End If

            Catch ex As Exception

            End Try

        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripComboBoxOps_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxOps.SelectedIndexChanged
        mMod347Itm = New Mod347Itm(mMod347Itm.Parent, mMod347Itm.Contact, CurrentOp)
        LoadGrid()
    End Sub
End Class