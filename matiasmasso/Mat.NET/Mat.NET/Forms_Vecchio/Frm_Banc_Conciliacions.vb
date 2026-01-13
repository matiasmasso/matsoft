

Public Class Frm_Banc_Conciliacions
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDs2 As DataSet
    Private mAllowEvents As Boolean = False
    Private mCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.bancs)

    Private Enum Cols1
        Yea
        Mes
        Fch
    End Enum

    Private Enum Cols2
        Guid
        BancId
        BancNom
        NSdo
        SSdo
        Pdf
        IcoPdf
        Dif
        IcoWarn
    End Enum

    Private Enum Results
        NotSet
        Success
        Failed
    End Enum

    Private Sub Frm_Banc_Conciliacions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid1()
    End Sub

    Private Sub LoadGrid1()
        Dim SQL As String = "SELECT YEA,MONTH(FCH) FROM CCB WHERE EMP=1 AND CTA LIKE @CTA GROUP BY YEA,MONTH(FCH) ORDER BY YEA DESC, MONTH(FCH) DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@CTA", mCta.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oColFch As DataColumn = oTb.Columns.Add("FCH", System.Type.GetType("System.DateTime"))
        oColFch.SetOrdinal(Cols1.Fch)

        Dim DtFch As Date = Date.MinValue
        Dim iYea As Integer = 0
        Dim iMes As Integer = 0
        For Each oRow As DataRow In oTb.Rows
            iYea = CInt(oRow(Cols1.Yea))
            iMes = CInt(oRow(Cols1.Mes))
            DtFch = New Date(iYea, iMes, Date.DaysInMonth(iYea, iMes))
            oRow(Cols1.Fch) = DtFch
        Next

        mAllowEvents = False
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

            With .Columns(Cols1.Yea)
                .Visible = False
            End With
            With .Columns(Cols1.Mes)
                .Visible = False
            End With
            With .Columns(Cols1.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End With
        setcontextmenu1()
        mAllowEvents = True
    End Sub

    Private Function CurrentFch() As Date
        Dim DtFch As Date = Date.MinValue
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = oRow.Cells(Cols1.Yea).Value
            Dim iMes As Integer = oRow.Cells(Cols1.Mes).Value
            DtFch = New Date(iYea, iMes, 1).AddMonths(1).AddDays(-1)
        End If
        Return DtFch
    End Function

    Private Function CurrentItem() As BancConciliacio
        Dim oItem As BancConciliacio = Nothing
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            Dim oBanc As Banc = MaxiSrvr.Contact.FromNum(mEmp, CInt(oRow.Cells(Cols2.BancId).Value))
            oItem = BancConciliacio.GetFromBancFch(oBanc, CurrentFch)
        End If
        Return oItem
    End Function

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            setcontextmenu1()
            LoadGrid2()
        End If
    End Sub



    Private Sub LoadGrid2()
        Dim SQL As String = "SELECT H.Guid, CCB.cli, " _
        & "CliBnc.Abr, " _
        & "SUM(CASE WHEN (CCB.yea = YEAR(@FCH) AND CCB.fch <= @FCH AND DH = 1) THEN CCB.EUR WHEN (CCB.yea = YEAR(@FCH) AND CCB.fch <= @FCH AND DH = 2) THEN - CCB.EUR ELSE 0 END) AS NSDO, " _
        & "H.Eur AS SSDO, " _
        & "(CASE WHEN H.IMAGE IS NULL THEN 0 ELSE 1 END) AS PDF, " _
        & "0 AS DIF " _
        & "FROM CCB LEFT OUTER JOIN " _
        & "CliBnc ON CCB.Emp = CliBnc.emp AND CCB.cli = CliBnc.cli LEFT OUTER JOIN " _
        & "ConciliacionsBancsHeader H ON CCB.Emp = H.Emp AND CCB.cli = H.Banc AND H.Fch = @FCH " _
        & "WHERE CCB.Emp =@EMP AND CCB.cta LIKE @CTA and CCB.cli>0 " _
        & "GROUP BY H.Guid, CliBnc.Abr, CCB.cli, H.Eur, (CASE WHEN H.IMAGE IS NULL THEN 0 ELSE 1 END) " _
        & "HAVING (MIN(CCB.fch) < @FCH) AND (MAX(CCB.fch) > @FCH) " _
        & "ORDER BY CliBnc.Abr"

        Dim oCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.bancs)
        Dim sFch As String = Format(CurrentFch, "dd/MM/yyyy")
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@CTA", mCta.Id, "@FCH", sFch)
        mDs2 = oDs
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oColIcoPdf As DataColumn = oTb.Columns.Add("ICOPDF", System.Type.GetType("System.Byte[]"))
        oColIcoPdf.SetOrdinal(Cols2.IcoPdf)

        Dim oColIcoWarn As DataColumn = oTb.Columns.Add("ICOWARN", System.Type.GetType("System.Byte[]"))
        oColIcoWarn.SetOrdinal(Cols2.IcoWarn)

        mAllowEvents = False
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

            With .Columns(Cols2.BancId)
                .Visible = False
            End With
            With .Columns(Cols2.BancNom)
                .HeaderText = "banc"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols2.NSdo)
                .HeaderText = "sdo llibres"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols2.SSdo)
                .HeaderText = "sdo extracte"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols2.Pdf)
                .Visible = False
            End With
            With .Columns(Cols2.IcoPdf)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols2.Dif)
                .HeaderText = "diferencia"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols2.IcoWarn)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
        End With
        mAllowEvents = True
        SetContextMenu2()
    End Sub

    Private Sub DataGridView2_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        Select Case e.ColumnIndex
            Case Cols2.IcoPdf
                Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                If oRow.Cells(Cols2.Pdf).Value = 1 Then
                    e.Value = My.Resources.pdf
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols2.Dif
                Dim oRc As Results = Results.Failed
                Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Dim DcNSaldo As Decimal = 0
                Dim DcSSaldo As Decimal = 0
                If Not IsDBNull(oRow.Cells(Cols2.NSdo).Value) Then
                    DcNSaldo = oRow.Cells(Cols2.NSdo).Value
                End If
                If IsDBNull(oRow.Cells(Cols2.SSdo).Value) Then
                    oRc = Results.NotSet
                Else
                    DcSSaldo = oRow.Cells(Cols2.SSdo).Value
                End If
                Dim DcDif As Decimal = DcNSaldo - DcSSaldo
                e.Value = DcDif
                If e.Value = 0 Then oRc = Results.Success

                Select Case oRc
                    Case Results.Success
                        oRow.Cells(Cols2.IcoWarn).Value = My.Resources.Ok
                    Case Results.Failed
                        oRow.Cells(Cols2.IcoWarn).Value = My.Resources.warn
                    Case Results.NotSet
                        oRow.Cells(Cols2.IcoWarn).Value = My.Resources.empty
                End Select

        End Select
    End Sub

    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        Zoom(sender, e)
    End Sub

    Private Sub RefreshRequest2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols2.BancNom
        Dim oGrid As DataGridView = DataGridView2

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid2()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub SetContextMenu1()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItem As New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf Do_Excel)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub SetContextMenu2()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItem As New ToolStripMenuItem("zoom", My.Resources.prismatics, AddressOf Zoom)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("extracte", My.Resources.notepad, AddressOf Extracte)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("web", My.Resources.iExplorer, AddressOf Web)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView2.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Banc_Conciliacio(CurrentItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest2
        oFrm.Show()
    End Sub

    Private Sub Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CliCtasOld(CurrentItem.Ccd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest2
        oFrm.Show()
    End Sub

    Private Sub Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oBanc As Banc = CurrentItem.Banc
        Dim sUrl As String = oBanc.WebSite()
        Process.Start("IExplore.exe", sUrl)
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oExcel As New MatExcel
        Dim oHeader As New ArrayList()
        oHeader.Add("data")
        oHeader.Add("entitat")
        oHeader.Add("Saldo als llibres oficials")
        oHeader.Add("Saldo a l'extracte del banc")
        oExcel.AddRow(oHeader)
        oExcel.AddRow()

        For Each oRow As DataGridViewRow In DataGridView2.Rows
            Dim oGuid As Guid = oRow.Cells(Cols2.Guid).Value
            Dim oConciliacio As New BancConciliacio(oGuid)
            Dim oCells As New ArrayList
            oCells.Add(New DTOExcelCell(CurrentFch))
            oCells.Add(New DTOExcelCell(oConciliacio.Banc.AliasOrRaoSocial, oConciliacio.Url(True)))
            oCells.Add(New DTOExcelCell(oConciliacio.Ccd.GetSaldo(CurrentFch).Eur))
            oCells.Add(New DTOExcelCell(oConciliacio.Saldo.Eur))
            oExcel.AddRow(oCells)
        Next
        oExcel.Application.Visible = True
    End Sub

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.SelectionChanged
        If mAllowEvents Then
            SetContextMenu2()
        End If
    End Sub
End Class