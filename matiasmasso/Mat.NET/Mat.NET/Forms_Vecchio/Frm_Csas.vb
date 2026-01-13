

Public Class Frm_Csas
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mBanc As Banc

    Private mContextMenu As ContextMenuStrip = Nothing
    Private mMenuItemRemesa As ToolStripMenuItem = Nothing
    Private mMenuItemModeCobro As ToolStripMenuItem = Nothing
    Private mMenuItemModeDescompte As ToolStripMenuItem = Nothing
    Private mMenuItemFullMode As ToolStripMenuItem = Nothing

    Private mAllowEvents As Boolean

    Private Enum Cols
        Ico
        Yea
        Id
        Fch
        BancId
        BancNom
        Efts
        Amt
        AmtMig
        MinVto
        MaxVto
        Dies
        Despeses
        Tae
        Descomptat
        FileFormat
    End Enum

    Private Enum Modes
        AlCobro
        AlDescompte
        Tots
    End Enum

    Public Sub New(Optional ByVal oBanc As Banc = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mBanc = oBanc
        LoadContextMenu()
        LoadGrid()
    End Sub

    Private Sub LoadContextMenu()
        mContextMenu = New ContextMenuStrip
        mMenuItemRemesa = New ToolStripMenuItem("remesa...")
        mContextMenu.Items.Add(mMenuItemRemesa)
        mContextMenu.Items.Add("-")

        mMenuItemModeCobro = New ToolStripMenuItem("nomes al cobro", Nothing, AddressOf onModeClick)
        mContextMenu.Items.Add(mMenuItemModeCobro)

        mMenuItemModeDescompte = New ToolStripMenuItem("nomes al descompte", Nothing, AddressOf onModeClick)
        mContextMenu.Items.Add(mMenuItemModeDescompte)

        mMenuItemFullMode = New ToolStripMenuItem("totes les remeses", Nothing, AddressOf onModeClick)
        mMenuItemFullMode.Checked = True
        mContextMenu.Items.Add(mMenuItemFullMode)
        DataGridView1.ContextMenuStrip = mContextMenu
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT 0 as ICO,CSA.YEA,CSA.CSB,CSA.FCH,CSA.BNC, " _
        & "(CASE WHEN CLIGRAL.NOMCOM>'' THEN CLIGRAL.NOMCOM ELSE CLIGRAL.RAOSOCIAL END) AS NOM, " _
        & "CSA.EFTS,CSA.EUR,(CASE WHEN EFTS = 0 THEN 0 ELSE CSA.eur / CSA.efts END)," _
        & "MIN(CSB.VTO),MAX(CSB.VTO), " _
        & "CSA.DIAS,CSA.GTS," _
        & "(CASE WHEN (CSA.EUR=0 OR CSA.DIAS = 0 OR CSA.DESCOMPTAT=0) THEN 0 ELSE (CSA.gts * 360) / (CSA.eur * CSA.dias) END) AS TAE, " _
        & "CSA.DESCOMPTAT,CSA.FILEFORMAT " _
        & "FROM CSA INNER JOIN " _
        & "CSB ON CSA.EMP=CSB.EMP AND CSA.YEA=CSB.YEA AND CSA.CSB=CSB.CSB LEFT OUTER JOIN " _
        & "CLIGRAL ON CSA.EMP=CLIGRAL.EMP AND CSA.BNC=CLIGRAL.CLI " _
        & "WHERE CSA.EMP=@EMP "

        If mBanc IsNot Nothing Then
            SQL = SQL & " AND CSA.BNC=" & mBanc.Id & " "
        End If

        Select Case CurrentMode()
            Case Modes.AlCobro
                SQL = SQL & " AND CSA.DESCOMPTAT=0 "
            Case Modes.AlDescompte
                SQL = SQL & " AND CSA.DESCOMPTAT=1 "
            Case Modes.Tots
        End Select
        SQL = SQL & "GROUP BY CSA.YEA,CSA.CSB,CSA.FCH,CSA.BNC,CLIGRAL.NOMCOM,CLIGRAL.RAOSOCIAL,CSA.EFTS,CSA.EUR,CSA.DIAS,CSA.GTS,CSA.DESCOMPTAT,CSA.FILEFORMAT "
        SQL = SQL & "ORDER BY CSA.YEA DESC, CSA.CSB DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        'afegeix columna Ico
        oTb.Columns.RemoveAt(Cols.Ico)
        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "Remesa"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.BancId)
                .Visible = False
            End With
            With .Columns(Cols.BancNom)
                .HeaderText = "Banc"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Efts)
                .HeaderText = "Efectes"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "Import"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.AmtMig)
                .HeaderText = "Import mig"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.MinVto)
                .HeaderText = "min.vto"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.MaxVto)
                .HeaderText = "max.vto"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Dies)
                .HeaderText = "dies"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.0;-#,##0.0;#"
            End With
            With .Columns(Cols.Despeses)
                .HeaderText = "Despeses"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Tae)
                .HeaderText = "Tae"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0.00 %;-0.00 %;#"
            End With
            With .Columns(Cols.Descomptat)
                .Visible = False
            End With
            With .Columns(Cols.FileFormat)
                .Visible = False
            End With
        End With
        mAllowEvents = True
        SetContextMenu()
    End Sub

    Private Function CurrentCsa() As Csa
        Dim oCsa As Csa = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oCsa = MaxiSrvr.Csa.FromNum(mEmp, oRow.Cells(Cols.Yea).Value, oRow.Cells(Cols.Id).Value)
        End If
        Return oCsa
    End Function

    Private Function SelectedCsas() As Csas
        Dim oCsas As New Csas
        Dim IntYea As Integer
        Dim LngId As Integer
        Dim oCsa As Csa
        Dim oRow As DataGridViewRow

        If DataGridView1.SelectedRows.Count > 0 Then
            For Each oRow In DataGridView1.SelectedRows
                IntYea = oRow.Cells(Cols.Yea).Value
                LngId = oRow.Cells(Cols.Id).Value
                oCsa = MaxiSrvr.Csa.FromNum(mEmp, IntYea, LngId)
                oCsas.Add(oCsa)
            Next
        Else
            oCsas.Add(CurrentCsa)
        End If
        Return oCsas
    End Function


    Private Sub ToolStripButtonRefresca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        LoadGrid()
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Id

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
            MsgBox("no hi han remeses registrades!", MsgBoxStyle.Exclamation)
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub onModeClick(ByVal sender As Object, ByVal e As System.EventArgs)
        mMenuItemModeCobro.Checked = False
        mMenuItemModeDescompte.Checked = False
        mMenuItemFullMode.Checked = False
        CType(sender, ToolStripMenuItem).Checked = True
        LoadGrid()
    End Sub

    Private Sub SetContextMenu()
        Dim oCsa As Csa = CurrentCsa()

        mMenuItemRemesa.DropDownItems.Clear()
        If oCsa IsNot Nothing Then
            Dim oMenu_Csa As New Menu_Csa(oCsa)
            AddHandler oMenu_Csa.AfterUpdate, AddressOf RefreshRequest
            mMenuItemRemesa.DropDownItems.AddRange(oMenu_Csa.Range)
        End If
    End Sub

    Private Function CurrentMode() As Modes
        Dim oMode As Modes = Modes.Tots
        If mMenuItemModeCobro.Checked Then
            oMode = Modes.AlCobro
        ElseIf mMenuItemModeDescompte.Checked Then
            oMode = Modes.AlDescompte
        End If
        Return oMode
    End Function

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oFileFormat As DTOCsa.FileFormats = CType(oRow.Cells(Cols.FileFormat).Value, DTOCsa.FileFormats)
                Select Case oFileFormat
                    Case DTOCsa.FileFormats.SepaB2b
                        e.Value = My.Resources.star
                    Case Else
                        Dim BlDescomptat As Boolean = oRow.Cells(Cols.Descomptat).Value
                        If BlDescomptat Then
                            e.Value = My.Resources.star_red
                        Else
                            e.Value = My.Resources.star_blue
                        End If
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_Csa
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Csa = CurrentCsa()
            .Show()
        End With
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

End Class