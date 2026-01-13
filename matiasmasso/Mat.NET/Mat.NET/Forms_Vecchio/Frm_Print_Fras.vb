

Public Class Frm_Print_Fras
    Private mAllowEvents As Boolean
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mFras As Fras

    'Private Event ShowProgress(ByVal sText As String, ByVal iCount As Integer)
    'Private Event IncrementProgress(ByVal sender As Object, ByVal e As EventArgs)

    Private Enum Cols
        Chk
        Mode
        Ico
        Yea
        Fra
        Fch
        Eur
        Clx
    End Enum

    Private Sub Frm_Print_Fras_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadFchs()
        LoadGrid()
        mAllowEvents = True
    End Sub

   
    Private Sub LoadGrid()
        mAllowEvents = False
        Dim SQL As String = "SELECT CAST (0 AS BIT) AS CHK, " _
        & "(CASE WHEN EFRAS.CLI IS NULL THEN 0 ELSE 1 END) AS MODE,  " _
        & "FRA.YEA, FRA.FRA, FRA.FCH, FRA.EURLIQ, CLX.CLX " _
        & "FROM  FRA INNER JOIN " _
        & "CLX ON FRA.EMP=CLX.EMP AND FRA.CLI=CLX.CLI LEFT OUTER JOIN " _
        & "(SELECT EMAIL_CLIS.EMP, EMAIL_CLIS.CLI FROM EMAIL_CLIS INNER JOIN " _
        & "EMAIL ON EMAIL_CLIS.EmailGuid = EMAIL.Guid INNER JOIN " _
        & "SSCEMAIL ON EMAIL.GUID LIKE SSCEMAIL.EMAIL AND SSCEMAIL.SSC=" & CInt(DTOSubscription.Ids.Facturacio).ToString & " " _
        & "GROUP BY EMAIL_CLIS.EMP, EMAIL_CLIS.CLI) EFRAS " _
        & "ON EFRAS.EMP=FRA.EMP AND EFRAS.CLI=FRA.CLI " _
        & "WHERE FRA.Emp =" & mEmp.Id & " AND " _
        & "FRA.yea >= 2006 AND " _
        & "FRA.NOPRINT=0 "

        If IsDate(ComboBoxFchs.SelectedValue) Then
            Dim DtFch As Date = ComboBoxFchs.SelectedValue
            Dim sFch As String = Format(DtFch, "yyyyMMdd HH:mm:ss")
            SQL = SQL & " AND FRA.FchLastPrinted =CAST('" & sFch & "' AS DATETIME) "
        Else
            SQL = SQL & " AND FRA.FchLastPrinted IS NULL "
        End If

        If Not CheckBoxPrint.Checked Then
            SQL = SQL & " AND EFRAS.CLI IS NOT NULL "
        End If

        If Not CheckBoxEfras.Checked Then
            SQL = SQL & " AND EFRAS.CLI IS NULL "
        End If

        If CheckBoxCliFilter.Checked Then
            Dim oContact As Contact = Xl_Contact1.Contact
            If oContact.Exists Then
                SQL += " AND FRA.CLI=" & oContact.Id & " "
            End If
        End If

        SQL = SQL & "GROUP BY FRA.yea, FRA.fra, FRA.fch, FRA.EURLIQ, CLX.clx, EFRAS.CLI " _
        & "ORDER BY FRA.yea, FRA.fra"
        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)
        ButtonOk.Enabled = oTb.Rows.Count > 0

        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Chk)
                .HeaderText = ""
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.SelectionBackColor = Color.White
            End With
            With .Columns(Cols.Mode)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "factura"
                .Width = 45
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Sub LoadFchs()
        Dim SQL As String = "SELECT FCHLASTPRINTED AS SQLFCH, " _
        & "CAST(DATEPART(dd,FCHLASTPRINTED) AS VARCHAR)+'/'+" _
        & "CAST(DATEPART(MM,FCHLASTPRINTED) AS VARCHAR)+'/'+" _
        & "CAST(DATEPART(yy,FCHLASTPRINTED) AS VARCHAR)+' '+" _
        & "CAST(DATEPART(HH,FCHLASTPRINTED) AS VARCHAR)+':'+" _
        & "CAST(DATEPART(mm,FCHLASTPRINTED) AS VARCHAR)+':'+" _
        & "CAST(DATEPART(ss,FCHLASTPRINTED) AS VARCHAR) AS MYFCH " _
        & "FROM FRA " _
        & "WHERE EMP=" & mEmp.Id & " AND " _
        & "YEA>=2006 AND " _
        & "YEA>" & Today.Year - 2 & " " _
        & "GROUP BY FCHLASTPRINTED " _
        & "ORDER BY FCHLASTPRINTED DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        oRow("MYFCH") = "(pendents)"
        oTb.Rows.InsertAt(oRow, 0)
        With ComboBoxFchs
            .DataSource = oTb
            .DisplayMember = "MYFCH"
            .ValueMember = "SQLFCH"
        End With
    End Sub

    Private Sub ComboBoxFchs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxFchs.SelectedIndexChanged
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub

    Private Sub CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    CheckBoxEfras.CheckStateChanged, _
    CheckBoxPrint.CheckStateChanged
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub



    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        ButtonOk.Enabled = False
        'Dim oBgWorker As New System.ComponentModel.BackgroundWorker
        'AddHandler oBgWorker.DoWork, AddressOf DoWork
        'AddHandler oBgWorker.RunWorkerCompleted, AddressOf WorkerCompleted
        'oBgWorker.RunWorkerAsync()
        'End Sub

        'Private Sub WorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        'Me.Close()
        'End Sub

        'Private Sub DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        'ButtonOk.Enabled = False

        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim oFra As Fra
        Dim DtFchPrinted As Date = Now
        mFras = New Fras

        'RaiseEvent ShowProgress("(1/3) redactant factures...", oTb.Rows.Count)

        Dim iCount As Integer
        For Each oRow In oTb.Rows
            If CBool(oRow(Cols.Chk)) Then iCount += 1
        Next


        ShowProgressBar("(1/3) redactant factures...", iCount)
        Application.DoEvents()



        For Each oRow In oTb.Rows
            If CBool(oRow(Cols.Chk)) Then
                oFra = Fra.FromNum(mEmp, oRow(Cols.Yea), oRow(Cols.Fra))
                If oFra.Client.EfrasEnabled Then
                    root.Email_Efra(oFra, DtFchPrinted)
                Else
                    mFras.Add(oFra)
                    'root.print_fraOriginal(oFra)
                End If
            End If
            ProgressBar1.Increment(1)
            Application.DoEvents()
        Next

        If mFras.Count > 0 Then
            'RaiseEvent ShowProgress("(2/3) enviant factures a la impresora...", mFras.Count)
            ShowProgressBar("(2/3) enviant factures a la impresora...", mFras.Count)
            Application.DoEvents()

            Dim oRpt As maxisrvr.DocRpt = mFras.DocRpt(maxisrvr.DocRpt.FuentePapel.Original)
            Dim oFrm As New Rpt_Docs

            ProgressBar1.Increment(1)
            Application.DoEvents()
            oFrm.DocRpt = oRpt
            oFrm.print(oRpt.Papel, maxisrvr.DocRpt.Ensobrados.Sencillo)

            'root.PrintDoc(oRpt)
        End If

        For Each oFra In mFras
            'RaiseEvent ShowProgress("(3/3) retirant factures de pendents de imprimir...", mFras.Count)
            ShowProgressBar("(3/3) retirant factures de pendents de imprimir...", mFras.Count)
            Application.DoEvents()

            Dim SQL As String = "UPDATE FRA SET " _
            & "FCHLASTPRINTED='" & Format(DtFchPrinted, "yyyyMMdd HH:mm") & "', " _
            & "USRLASTPRINTED=" & root.Usuari.Id & " " _
            & " WHERE " _
            & "EMP=" & CInt(mEmp.Id).ToString & " AND " _
            & "YEA=" & oFra.Yea.ToString & " AND " _
            & "FRA=" & oFra.Id.ToString
            maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi)

            ProgressBar1.Increment(1)
            'RaiseEvent IncrementProgress(Nothing, EventArgs.Empty)
            Application.DoEvents()

        Next
        HideProgressBar()
        Me.Close()
    End Sub


    Private Sub ShowProgressBar(ByVal TxtStatus As String, ByVal iMax As Integer)
        LabelStatus.Text = TxtStatus
        With ProgressBar1
            .Maximum = iMax
            .Value = 0
            .Visible = True
        End With
        ButtonOk.Visible = False
    End Sub

    Private Sub HideProgressBar()
        LabelStatus.Text = " "
        With ProgressBar1
            .Visible = False
        End With
        ButtonOk.Visible = True
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case oRow.Cells(Cols.Mode).Value
                    Case "0"
                        e.Value = My.Resources.printer
                    Case "1"
                        e.Value = My.Resources.iExplorer
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oFra As Fra = CurrentFra()

        If oFra IsNot Nothing Then

            oContextMenu.Items.Add("seleccionar", My.Resources.PLUS, AddressOf MenuItemFra_Seleccionar)
            oContextMenu.Items.Add("deseleccionar", My.Resources.minus, AddressOf MenuItemFra_Deseleccionar)
            oContextMenu.Items.Add("descartar", My.Resources.del, AddressOf MenuItemFra_Descartar)

            oContextMenu.Items.Add("-")

            Dim oMenu_Fra As New Menu_Fra(oFra)
            AddHandler oMenu_Fra.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Fra.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Function CurrentFra() As Fra
        Dim oFra As Fra = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = DataGridView1.CurrentRow.Cells(Cols.Yea).Value
            Dim iFra As Integer = DataGridView1.CurrentRow.Cells(Cols.Fra).Value
            oFra = Fra.FromNum(BLL.BLLApp.Emp, iYea, iFra)
        End If
        Return oFra
    End Function

    Private Function CurrentFras() As Fras
        Dim oFras As New Fras

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim iYea As Integer
            Dim iFra As Integer
            Dim oFra As Fra
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                iYea = oRow.Cells(Cols.Yea).Value
                iFra = oRow.Cells(Cols.Fra).Value
                oFra = Fra.FromNum(mEmp, iYea, iFra)
                oFras.Add(oFra)
            Next
            oFras.Sort()
        Else
            Dim oFra As Fra = CurrentFra()
            If oFra IsNot Nothing Then
                oFras.Add(CurrentFra)
            End If
        End If
        Return oFras
    End Function


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Fra
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

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

    Private Sub MenuItemFra_Seleccionar(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            oRow.Cells(Cols.Chk).Value = True
        Next
    End Sub

    Private Sub MenuItemFra_Deseleccionar(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            oRow.Cells(Cols.Chk).Value = False
        Next
    End Sub

    Private Sub MenuItemFra_Descartar(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each oFra As Fra In CurrentFras()
            Dim SQL As String = "UPDATE FRA SET NOPRINT=1 WHERE " _
            & "EMP=@EMP AND " _
            & "YEA=@YEA AND " _
            & "FRA=@FRA"
            MaxiSrvr.ExecuteNonQuery(SQL, MaxiSrvr.Databases.Maxi, "@EMP", oFra.Client.Emp.Id, "@YEA", oFra.Yea, "@FRA", oFra.Id)
        Next
        LoadGrid()
    End Sub

    Private Sub ButtonCheckAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCheckAll.Click
        Dim oRow As DataGridViewRow
        For Each oRow In DataGridView1.Rows
            oRow.Cells(Cols.Chk).Value = True
        Next
    End Sub

    Private Sub ButtonCheckNone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCheckNone.Click
        Dim oRow As DataGridViewRow
        For Each oRow In DataGridView1.Rows
            oRow.Cells(Cols.Chk).Value = False
        Next
    End Sub

    Private Sub CheckBoxCliFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxCliFilter.CheckedChanged
        Xl_Contact1.Visible = CheckBoxCliFilter.Checked
    End Sub

End Class