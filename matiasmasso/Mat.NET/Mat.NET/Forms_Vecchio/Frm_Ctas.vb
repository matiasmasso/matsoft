

Public Class Frm_Ctas
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDs As DataSet
    Private mFch As Date = Today
    Private mAllowEvents As Boolean = False

    Private Enum Tabs
        Sumas
        Saldos
    End Enum

    Private Enum Cols1
        Plan
        Cta
        Dsc
        Cli
        Nom
        Sdo1
        Deb
        Hab
        Sdo2
    End Enum

    Private Enum Cols2
        Plan
        Cta
        Dsc
        Deb
        Hab
        XDeb
        XHab
    End Enum

    Public WriteOnly Property Fch() As Date
        Set(ByVal value As Date)
            mFch = value
        End Set
    End Property

    Private Sub Frm_Ctas_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DateTimePicker1.Value = mFch
        Me.Show()
        LoadGrid1()
        mAllowEvents = True
    End Sub


    Private Sub LoadGrid1()
        Cursor = Cursors.WaitCursor
        Dim iYea As Integer = CurrentYea()
        Dim DtFch As Date = CurrentFch()
        Dim SQL As String = "SELECT CCB.PGCPLAN, CCB.cta, PGCCTA.ESP, CCB.cli, " _
        & "(CASE WHEN CLX.CLX IS NULL THEN '' ELSE CLX.CLX END) AS SUBCOMPTE, " _
        & "SUM(CASE WHEN CCA.CCD = 1 THEN (CASE WHEN DH = 1 THEN EUR ELSE - EUR END) ELSE 0 END) AS SDOINICIAL, " _
        & "SUM(CASE WHEN DH = 1 AND CCA.CCD <> 1 THEN EUR ELSE 0 END) AS DEBE, " _
        & "SUM(CASE WHEN DH = 2 AND CCA.CCD <> 1 THEN EUR ELSE 0 END) AS HABER, " _
        & "SUM(CASE WHEN DH = 1 THEN EUR ELSE - EUR END) AS SDOFINAL " _
        & "FROM CCB INNER JOIN " _
        & "CCA ON Ccb.CcaGuid = Cca.Guid LEFT OUTER JOIN " _
        & "CLX ON CCB.Emp = CLX.Emp AND CCB.cli = CLX.cli LEFT OUTER JOIN " _
        & "PGCCTA ON Ccb.CtaGuid = PgcCta.Guid " _
        & "WHERE CCB.EMP=" & mEmp.Id & " AND " _
        & "CCB.yea =" & CurrentYea() & " AND " _
        & "CCB.FCH<='" & Format(DtFch, "yyyyMMdd") & "' " _
        & "GROUP BY CCB.PGCPLAN, CCB.cta, PGCCTA.ESP, CCB.cli, CLX.clx " _
        & "HAVING SUM(CASE WHEN CCA.CCD = 1 THEN (CASE WHEN DH = 1 THEN EUR ELSE - EUR END) ELSE 0 END) <> 0 OR " _
        & "SUM(CASE WHEN DH = 1 AND CCA.CCD <> 1 THEN EUR ELSE 0 END) <> 0 OR " _
        & "SUM(CASE WHEN DH = 2 AND CCA.CCD <> 1 THEN EUR ELSE 0 END) <> 0 " _
        & "ORDER BY CCB.PGCPLAN, CCB.cta, CLX.clx"

        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim oSum As DataRow = oTb.NewRow
        oSum(Cols1.Nom) = "totals"
        For iCol As Integer = Cols1.Sdo1 To Cols1.Sdo2
            oSum(iCol) = 0
        Next
        For Each oRow In oTb.Rows
            For iCol As Integer = Cols1.Sdo1 To Cols1.Sdo2
                Try
                    oSum(iCol) += oRow(iCol)
                Catch ex As Exception
                    Stop
                End Try
            Next
        Next

        'Dim BlDescuadre As Boolean = oSum(Cols1.Deb) <> oSum(Cols1.Hab)
        'If oSum(Cols1.Debx) <> oSum(Cols1.Habx) Then BlDescuadre = True
        'ToolStripButtonDescuadre.Enabled = BlDescuadre

        oTb.Rows.InsertAt(oSum, 0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols1.Plan)
                .Visible = False
            End With
            With .Columns(Cols1.Cli)
                .Visible = False
            End With
            With .Columns(Cols1.Cta)
                .HeaderText = "Compte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols1.Dsc)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols1.Nom)
                .HeaderText = "Subcompte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols1.Sdo1)
                .HeaderText = "Sdo.inicial"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols1.Deb)
                .HeaderText = "Debe"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols1.Hab)
                .HeaderText = "Haber"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols1.Sdo2)
                .HeaderText = "Sdo.final"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        Cursor = Cursors.Default


    End Sub




    Private Sub LoadGrid2()
        Cursor = Cursors.WaitCursor
        Dim iYea As Integer = CurrentYea()
        Dim DtFch As Date = CurrentFch()
        Dim SQL As String = "SELECT  PgcPlan, cta, Esp, DEB, HAB, " _
        & "(CASE WHEN DEB > HAB THEN DEB - HAB ELSE 0 END) AS XDEB, " _
        & "(CASE WHEN HAB > DEB THEN HAB - DEB ELSE 0 END) AS XHAB " _
        & "FROM (SELECT CCB.PgcPlan, CCB.cta, PGCCTA.Esp, " _
        & "SUM(CASE WHEN DH = 1 THEN EUR ELSE 0 END) AS DEB, " _
        & "SUM(CASE WHEN DH = 2 THEN EUR ELSE 0 END) AS HAB " _
        & "FROM CCB LEFT OUTER JOIN " _
        & "PGCCTA ON CCB.PgcPlan = PGCCta.PgcPlan AND CCB.cta = PGCCTA.Id " _
        & "WHERE CCB.EMP=" & mEmp.Id & " AND " _
        & "CCB.yea =" & CurrentYea() & " AND " _
        & "CCB.FCH<='" & Format(DtFch, "yyyyMMdd") & "' " _
        & "GROUP BY CCB.PgcPlan, CCB.cta, PGCCTA.Esp) AS X " _
        & "ORDER BY PgcPlan, cta"

        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim oSum As DataRow = oTb.NewRow
        oSum(Cols2.Dsc) = "totals"
        For iCol As Integer = Cols2.Deb To Cols2.XHab
            oSum(iCol) = 0
        Next
        For Each oRow In oTb.Rows
            For iCol As Integer = Cols2.Deb To Cols2.XHab
                Try
                    oSum(iCol) += oRow(iCol)
                Catch ex As Exception
                    Stop
                End Try
            Next
        Next

        'Dim BlDescuadre As Boolean = oSum(Cols1.Deb) <> oSum(Cols1.Hab)
        'If oSum(Cols1.Debx) <> oSum(Cols1.Habx) Then BlDescuadre = True
        'ToolStripButtonDescuadre.Enabled = BlDescuadre

        oTb.Rows.InsertAt(oSum, 0)
        With DataGridView2
            With .RowTemplate
                .Height = DataGridView2.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols2.Plan)
                .Visible = False
            End With
            With .Columns(Cols2.Cta)
                .HeaderText = "Compte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols2.Dsc)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols2.Deb)
                .HeaderText = "Debe"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols2.Hab)
                .HeaderText = "Haber"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols2.XDeb)
                .HeaderText = "Sdo.deutor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols2.XHab)
                .HeaderText = "Sdo.creditor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        Cursor = Cursors.Default


    End Sub


    Private Function CurrentYea() As Integer
        Return DateTimePicker1.Value.Year
    End Function

    Private Function CurrentFch() As Date
        Return DateTimePicker1.Value
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DataGridView1.DoubleClick, DataGridView2.DoubleClick
        Dim oCce As Cce = CurrentCce(sender)

        If oCce IsNot Nothing Then
            Dim FromFch As Date = "1/1/" & CurrentFch.Year.ToString
            root.ShowCceCcds(oCce, FromFch, CurrentFch)
        End If
    End Sub


    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke
        'If (e.State And DataGridViewElementStates.Selected) = _
        'DataGridViewElementStates.Selected Then
        'oBgColor = DataGridView1.DefaultCellStyle.SelectionBackColor
        'End If

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            Me.DataGridView1.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            Me.DataGridView1.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        'System.Drawing.Drawing2D.LinearGradientBrush(rowBounds, _
        'e.InheritedRowStyle.BackColor, _
        'oColor, _
        'System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub


    Private Sub ButtonRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRefresh.Click
        Select Case TabControl1.SelectedIndex
            Case 0
                LoadGrid1()
            Case 1
                LoadGrid2()
        End Select
    End Sub

    Private Sub SetContextMenu(ByVal oGrid As DataGridView)
        Dim oContextMenu As New ContextMenuStrip
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols1.Cta).Value) Then
                Dim iYea As Integer = DateTimePicker1.Value.Year
                Dim sCta As String = oRow.Cells(Cols1.Cta).Value
                Dim oCta As PgcCta = MaxiSrvr.PgcCta.FromNum(PgcPlan.FromYear(iYea), sCta)
                Dim oCce As New Cce(BLL.BLLApp.Emp, oCta, iYea)

                Dim iCli As Integer = oRow.Cells(Cols1.Cli).Value
                Dim oContact As Contact = Nothing
                If iCli = 0 Then
                    Dim oMenu_Cce As New Menu_Cce(oCce)
                    oContextMenu.Items.AddRange(oMenu_Cce.Range)
                Else
                    oContact = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, iCli)
                    Dim oCcd As New Ccd(oCce, oContact)
                    Dim oMenu_Ccd As New Menu_Ccd(oCcd, BLL.BLLApp.Emp)
                    oContextMenu.Items.AddRange(oMenu_Ccd.Range)
                End If
            End If
        End If
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentCce(ByVal oGrid As DataGridView) As Cce
        Dim oCce As Cce = Nothing
        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If Not IsDBNull(oRow.Cells(Cols1.Cta).Value) Then
            Dim iPlan As Integer = oRow.Cells(Cols1.Plan).Value
            Dim sCta As String = oRow.Cells(Cols1.Cta).Value
            If sCta > "" Then
                Dim oCta As PgcCta = MaxiSrvr.PgcCta.FromNum(iPlan, sCta)
                oCce = New Cce(mEmp, oCta, CurrentYea)
            End If
        End If
        Return oCce
    End Function

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mallowevents Then
            SetContextMenu(sender)
        End If
    End Sub

    Private Sub ToolStripButtonDescuadre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonDescuadre.Click
        root.ShowCcaDescuadres(CurrentYea)
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        Select Case TabControl1.TabIndex
            Case 0
                LoadGrid1()
            Case 1
                LoadGrid2()
        End Select
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedTab.Text
            Case TabPage1.Text
            Case TabPage2.Text
                Static BlDone2 As Boolean
                If Not BlDone2 Then
                    LoadGrid2()
                    BlDone2 = True
                End If
        End Select

    End Sub

    Private Sub TabControl1_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.TabIndexChanged
    End Sub
End Class