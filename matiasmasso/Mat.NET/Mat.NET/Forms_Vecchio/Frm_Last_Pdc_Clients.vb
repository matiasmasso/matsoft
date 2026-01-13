
Imports System.Drawing

Public Class Frm_Last_Pdc_Clients
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDsYeas As DataSet
    Private mAllowEvents As Boolean
    Private mCod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.NotSet
    Private mRep As Rep

    Private Enum Cols
        Guid
        Id
        Fch
        Pdf
        PdfIco
        Cli
        Clx
        Eur
        Pdd
        Src
        Ico
        Usr
        Pn2
    End Enum

    Public Sub New(ByVal oRep As Rep)
        MyBase.New()
        Me.InitializeComponent()
        mRep = oRep
        SetYeas()
        Refresca()
        EnableYeaButtons()
        mAllowEvents = True
    End Sub

    Public Sub New()
        MyBase.new()
        InitializeComponent()
    End Sub

    Private Sub Frm_Last_Pdcs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetYeas()
        Refresca()
        EnableYeaButtons()
        mAllowEvents = True
    End Sub

    Public WriteOnly Property Cod() As DTOPurchaseOrder.Codis
        Set(ByVal value As DTOPurchaseOrder.Codis)
            mCod = value
        End Set
    End Property

    Private Sub Refresca()
        LoadGrid()
        SetContextMenu()
    End Sub

    Private Sub LoadGrid()

        Dim SQL As String = "SELECT PDC.Guid,PDC.PDC,PDC.FCH," _
        & "(CASE WHEN PDC.Hash IS NULL THEN 0 ELSE 1 END) AS PDF," _
        & "PDC.CLI,CLX.CLX,PDC.EUR,PDC.PDD, " _
        & "PDC.SRC, " _
        & "(CASE WHEN USR.LOGIN IS NULL THEN CAST(PDC.USRCREATED AS VARCHAR) ELSE USR.LOGIN END) AS USR, " _
        & "(CASE WHEN MAX(PNC.Pn2) > 0 THEN 1 ELSE 0 END) AS PN2 " _
        & "FROM PDC INNER JOIN " _
        & "CLX ON Pdc.CliGuid=CLX.Guid LEFT OUTER JOIN " _
        & "PNC ON PDC.Guid = PNC.PdcGuid LEFT OUTER JOIN " _
        & "EMPUSR ON Pdc.UsrCreatedGuid=EMPUSR.ContactGuid LEFT OUTER JOIN " _
        & "USR ON EmpUsr.UsrGuid = Usr.Guid "

        If CheckBoxZona.Checked Then
            Dim oZona As DTOZona = Xl_Lookup_Zona1.Zona
            If oZona IsNot Nothing Then
                SQL = SQL & "INNER JOIN CLI_GEO3 ON PDC.EMP=CLI_GEO3.EMP AND PDC.CLI=CLI_GEO3.CLI " _
                    & "AND CLI_GEO3.ZonId = '" & oZona.Guid.ToString & "' "
            End If
        End If

        SQL = SQL & "WHERE PDC.EMP=" & App.Current.Emp.Id & " AND " _
        & "PDC.YEA=" & CurrentYea() & " AND " _
        & "PDC.COD=" & DTOPurchaseOrder.Codis.client & " "

        If mRep IsNot Nothing Then
            SQL = SQL & " AND PNC.RepGuid='" & mRep.Guid.ToString & "' "
        End If

        If CheckBoxZona.Checked Then
            Dim oZona As DTOZona = Xl_Lookup_Zona1.Zona
            If oZona IsNot Nothing Then

            End If
        End If

        SQL = SQL & "GROUP BY PDC.Guid,PDC.PDC,PDC.FCH,PDC.CLI,CLX.CLX,PDC.EUR,PDC.PDD,PDC.SRC,PDC.Hash,PDC.USRCREATED,USR.LOGIN " _
        & "ORDER BY PDC.PDC DESC"

        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)

        Dim oColPdf As DataColumn = oTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oColPdf.SetOrdinal(Cols.PdfIco)

        Dim oColSrc As DataColumn = oTb.Columns.Add("SRCICO", System.Type.GetType("System.Byte[]"))
        oColSrc.SetOrdinal(Cols.Ico)


        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "Comanda"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Pdf)
                .Visible = False
            End With
            With .Columns(Cols.PdfIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Cli)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Pdd)
                .HeaderText = "Concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 150
            End With
            With .Columns(Cols.Src)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Usr)
                .HeaderText = "Usuari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Pn2)
                .Visible = False
            End With
        End With
    End Sub


    Private Sub SetYeas()
        Dim SQL As String = ""

        If mRep Is Nothing Then
            SQL = "SELECT YEA FROM PDC " _
            & "WHERE EMP=" & mEmp.Id & " AND " _
            & "COD=" & DTOPurchaseOrder.Codis.client & " " _
            & "GROUP BY YEA " _
            & "ORDER BY YEA DESC"
        Else
            SQL = "SELECT PDC.YEA FROM PDC INNER JOIN " _
            & "PNC ON PDC.Guid=PNC.PdcGuid " _
            & "WHERE PDC.COD=" & DTOPurchaseOrder.Codis.client & " AND " _
            & "PNC.RepGuid='" & mRep.Guid.ToString & "' " _
            & "GROUP BY PDC.YEA " _
            & "ORDER BY PDC.YEA DESC"
        End If

        mDsYeas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsYeas.Tables(0)
        Dim oRow As DataRow
        With ToolStripComboBoxYea
            .BeginUpdate()
            For Each oRow In oTb.Rows
                .Items.Add(oRow("YEA"))
            Next
            .EndUpdate()
            If oTb.Rows.Count > 0 Then .SelectedIndex = 0
        End With
    End Sub

    Private Function CurrentYea() As Integer
        Dim iYea As Integer = ToolStripComboBoxYea.SelectedItem
        Return iYea
    End Function

    Private Function CurrentPdc() As Pdc
        Dim oPdc As Pdc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Guid).Value
            oPdc = New Pdc(oGuid)
        End If
        Return oPdc
    End Function

    Private Function CurrentPdcs() As Pdcs
        Dim oPdcs As New Pdcs

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim oPdc As Pdc
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                oPdc = New Pdc(oGuid)
                oPdcs.Add(oPdc)
            Next
            oPdcs.Sort()
        Else
            Dim oPdc As Pdc = CurrentPdc()
            If oPdc IsNot Nothing Then
                oPdcs.Add(CurrentPdc)
            End If
        End If
        Return oPdcs
    End Function


    Private Sub EnableYeaButtons()
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = mDsYeas.Tables(0).Rows.Count
        AnyanteriorToolStripButton.Enabled = (Idx < iYeas - 1)
        AnysegüentToolStripButton.Enabled = (Idx > 0)
    End Sub

    Private Sub AnyanteriorToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnyanteriorToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = mDsYeas.Tables(0).Rows.Count
        Idx = Idx + 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub AnysegüentToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnysegüentToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Idx = Idx - 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oPdcs As Pdcs = CurrentPdcs()

        If oPdcs.Count > 0 Then
            Dim oMenu_Pdc As New Menu_Pdc(oPdcs)
            AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pdc.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mAllowEvents = False
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer

        Try

            Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
            If oRow IsNot Nothing Then
                i = DataGridView1.CurrentRow.Index
                j = DataGridView1.CurrentCell.ColumnIndex
                iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
            End If

            Refresca()
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
            mAllowEvents = True

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(j)
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'Exit Sub
        Select Case e.ColumnIndex
            Case Cols.PdfIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Pdf).Value = 1 Then
                    e.Value = My.Resources.pdf
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oCod As DTOPurchaseOrder.Codis = CType(oRow.Cells(Cols.Src).Value, DTOPurchaseOrder.Codis)
                e.Value = BLL.BLLPurchaseOrder.SrcIcon(oCod)
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowPdc()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ShowPdc()
            e.Handled = True
        End If
    End Sub

    Private Sub ShowPdc()
        Dim oPdc As Pdc = CurrentPdc()

        If oPdc IsNot Nothing Then
            Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(oPdc.Guid)
            Dim exs As New List(Of Exception)
            If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                UIHelper.WarnError(exs)
            Else
                Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            End If
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        If oRow.Cells(Cols.Pn2).Value = 1 Then
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
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxYea.SelectedIndexChanged
        If mAllowEvents Then
            EnableYeaButtons()
            Refresca()
        End If
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        Refresca()
    End Sub

    Private Sub CheckBoxZona_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxZona.CheckedChanged
        Xl_Lookup_Zona1.Visible = CheckBoxZona.Checked
    End Sub

    Private Sub Xl_Lookup_Zona1_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_Lookup_Zona1.AfterUpdate
        Refresca()
    End Sub
End Class