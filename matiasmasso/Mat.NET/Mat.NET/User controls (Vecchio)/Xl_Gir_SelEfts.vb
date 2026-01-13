

Public Class Xl_Gir_SelEfts
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mCountry As Country
    Private mDs As DataSet
    Private _Tot As Decimal
    Private mCsbs As Csbs
    Private mDblSelAmt As Decimal
    Private mBlOnlyNoDomiciliats As Boolean
    Private mMode As Modes
    Private mAllowEvents As Boolean
    Private COLOR_DISABLED As Color = Color.FromArgb(200, 200, 200)

    Public Event Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Enum Modes
        SEPAB2B
        Norma58
        NoDomiciliats
    End Enum

    Private Enum Cols
        Id
        Chk
        Eur
        WarnQuadra
        IcoWarnQuadra
        WarnJoin
        IcoWarnJoin
        Vto
        CliGuid
        Clx
        WarnCcc
        Ccc
        Sepa
    End Enum

    Public Sub LoadData(ByVal oCountry As Country, ByVal oMode As Modes)
        mCountry = oCountry
        mMode = oMode

        Dim oBackgroundWorker As New System.ComponentModel.BackgroundWorker
        oBackgroundWorker.WorkerReportsProgress = True
        AddHandler oBackgroundWorker.DoWork, AddressOf onDoWork
        AddHandler oBackgroundWorker.ProgressChanged, AddressOf onProgressChanged
        AddHandler oBackgroundWorker.RunWorkerCompleted, AddressOf onRunWorkerCompleted
        oBackgroundWorker.RunWorkerAsync()
        'LoadGrid()
    End Sub

    Private Sub onProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
        Me.ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub onDoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim oWorker As System.ComponentModel.BackgroundWorker = sender
        Dim SQL As String = ""
        Select Case mMode
            Case Modes.SEPAB2B, Modes.Norma58
                SQL = "SELECT PND.ID,  cast(0 as bit) as CHECKED, PND.EUR, " _
                & "'' AS WARNQUADRA, " _
                & "'' AS WARNJOIN, " _
                & "PND.VTO, Clx.Guid as CliGuid, CLX.CLX, " _
                & "'' AS WARNCCC, " _
                & "IBAN.CCC, 0 AS SEPA " _
                & "FROM PND INNER JOIN " _
                & "CLX ON PND.ContactGuid = Clx.Guid INNER JOIN " _
                & "IBAN ON IBAN.ContactGuid=PND.ContactGuid AND IBAN.COD = 2 AND SUBSTRING(IBAN.CCC,1,2) = '" & mCountry.ISO & "' AND (IBAN.Caduca_Fch IS NULL OR IBAN.Caduca_Fch > GETDATE()) " _
                & "WHERE " _
                & "PND.EMP=" & mEmp.Id & " AND " _
                & "PND.STATUS=" & Pnd.StatusCod.pendent & " AND " _
                & "PND.ad LIKE 'D' "

                SQL = SQL & " AND (PND.cfp=" & DTOCustomer.FormasDePagament.DomiciliacioBancaria & " " _
                    & "or PND.cfp= " & DTOCustomer.FormasDePagament.Xerocopia & " " _
                    & "or PND.cfp=" & DTOCustomer.FormasDePagament.EfteAndorra & ") "
            Case Modes.NoDomiciliats
                SQL = "SELECT PND.ID,  cast(0 as bit) as CHECKED, PND.EUR, " _
                            & "'' AS WARNQUADRA, " _
                            & "'' AS WARNJOIN, " _
                            & "PND.VTO, Clx.Guid as CliGuid, CLX.CLX, " _
                            & "'' AS WARNCCC, " _
                            & "'' AS CCC, 0 AS SEPA " _
                            & "FROM PND INNER JOIN " _
                            & "CLX ON PND.ContactGuid = Clx.Guid " _
                            & "WHERE " _
                            & "PND.EMP=" & mEmp.Id & " AND " _
                            & "PND.STATUS=" & Pnd.StatusCod.pendent & " AND " _
                            & "PND.ad LIKE 'D' "

                SQL = SQL & " AND (PND.cfp=" & DTOCustomer.FormasDePagament.Xerocopia & ") "
        End Select

        SQL = SQL & " ORDER BY PND.vto, PND.fra"

        mDs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)

        Dim oColIcoWarnQuadra As DataColumn = oTb.Columns.Add("IcoWarnQuadra", System.Type.GetType("System.Byte[]"))
        oColIcoWarnQuadra.SetOrdinal(Cols.IcoWarnQuadra)

        Dim oColIcoWarnJoin As DataColumn = oTb.Columns.Add("IcoWarnJoin", System.Type.GetType("System.Byte[]"))
        oColIcoWarnJoin.SetOrdinal(Cols.IcoWarnJoin)

        Dim oRow As DataRow
        Dim DblEur As Decimal
        Dim i As Integer
        Dim j As Integer
        Dim oCli As Contact
        Dim sIban As String = ""
        For i = 0 To oTb.Rows.Count - 1
            oRow = oTb.Rows(i)
            Dim oCliGuid As Guid = oRow(Cols.CliGuid)
            oCli = New Contact(oCliGuid)
            oRow(Cols.WarnQuadra) = IIf(Cuadra(oCli), "", "1")

            sIban = oRow(Cols.Ccc).ToString
            oRow(Cols.Sepa) = IIf(BLL.BLLIban.Is_SEPAB2B_Enabled(sIban), True, False)

            If Not Bll.BllIban.Validated(sIban) Then oRow(Cols.WarnCcc) = "1"
            If oRow(Cols.Eur) < 0 Then
                For j = 0 To oTb.Rows.Count - 1
                    If i <> j Then
                        If oTb.Rows(j)(Cols.CliGuid).Equals(oCliGuid) Then
                            oTb.Rows(j)(Cols.WarnJoin) = "1"
                            oRow(Cols.WarnJoin) = "1"
                        End If
                    End If
                Next
            End If
            DblEur += oRow(Cols.Eur)

            Dim iProgress As Integer = 100 * i / oTb.Rows.Count
            oWorker.ReportProgress(iProgress)
        Next
        _Tot = DblEur
    End Sub

    Private Sub onRunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        ProgressBar1.Visible = False
        LoadGrid()
        mAllowEvents = True
    End Sub



    Public WriteOnly Property SepaMode As Modes
        Set(value As Modes)
            mMode = value
            DataGridView1.Refresh()
        End Set
    End Property

    Public ReadOnly Property Csbs() As Csbs
        Get
            Dim oPnd As Pnd
            Dim oCsbs As New Csbs
            Dim oCsb As Csb
            Dim DtVto As Date

            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.Rows
                If oRow.Cells(Cols.Chk).Value Then
                    oPnd = New Pnd(oRow.Cells(Cols.Id).Value)
                    DtVto = oRow.Cells(Cols.Vto).Value
                    If DtVto <> oPnd.Vto Then
                        oPnd.Vto = DtVto
                        Dim exs as New List(Of exception)
                        If Not oPnd.Update( exs) Then
                            MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                        End If
                    End If
                    oCsb = oPnd.GetNewCsb(mMode = Modes.SEPAB2B)
                    oCsbs.Add(oCsb)
                End If
            Next
            Return oCsbs
        End Get
    End Property


    Private Sub LoadGrid()

        TextBoxTot.Text = Format(_Tot, "#,##0.00")

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Chk)
                .HeaderText = ""
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.SelectionBackColor = Color.White
            End With
            With .Columns(Cols.WarnQuadra)
                .Visible = False
            End With
            With .Columns(Cols.IcoWarnQuadra)
                .ReadOnly = True
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Eur)
                .ReadOnly = True
                .HeaderText = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.WarnJoin)
                .Visible = False
            End With
            With .Columns(Cols.IcoWarnJoin)
                .ReadOnly = True
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Vto)
                .ReadOnly = True
                .HeaderText = "Venciment"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.CliGuid)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .ReadOnly = True
                .HeaderText = "Client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.WarnCcc)
                .ReadOnly = True
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Ccc)
                .ReadOnly = True
                .HeaderText = "Banc"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Sepa)
                .Visible = False
            End With
        End With

    End Sub

    Public Function Cuadra(ByVal oCli As Contact) As Boolean
        Dim DblPndEur As Decimal = 0
        Dim DblSdo430 As Decimal = 0
        Dim DblSdo5208 As Decimal = 0
        Dim oPlan As PgcPlan = PgcPlan.FromToday

        Dim SQL As String = "SELECT SUM(CASE WHEN AD LIKE 'D' THEN EUR ELSE - EUR END) AS EUR " _
        & "FROM PND " _
        & "WHERE EMP=" & mEmp.Id & " AND " _
        & "Cta LIKE '" & oPlan.Cta(DTOPgcPlan.ctas.clients).Id & "' AND " _
        & "cli =" & oCli.Id & " AND " _
        & "STATUS<" & Pnd.StatusCod.saldat

        Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi)
        oDrd.Read()
        If Not IsDBNull(oDrd("EUR")) Then
            DblPndEur = Math.Round(CDbl(oDrd("EUR")), 2)
        End If
        oDrd.Close()

        Dim oCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.clients)
        Dim oCcd As New Ccd(oCli, Today.Year, oCta)

        SQL = "SELECT SUM(CASE WHEN DH=1 THEN EUR ELSE -EUR END) AS EUR FROM CCB WHERE " _
        & "EMP=" & mEmp.Id & " AND " _
        & "YEA=" & Today.Year & " AND " _
        & "CLI=" & oCli.Id & " AND " _
        & "CTA LIKE '" & oPlan.Cta(DTOPgcPlan.ctas.clients).Id & "'"
        oDrd = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi)
        oDrd.Read()
        If Not IsDBNull(oDrd("EUR")) Then
            DblSdo430 = oDrd("EUR")
        End If
        oDrd.Close()

        Dim BlCuadra As Boolean = (DblSdo430 = DblPndEur)
        Return BlCuadra
    End Function

    Private Sub SetTotals()
        Dim oRow As DataGridViewRow
        Dim DblEur As Decimal
        For Each oRow In DataGridView1.Rows
            If oRow.Cells(Cols.Chk).Value Then
                DblEur += oRow.Cells(Cols.Eur).Value
            End If
        Next
        TextBoxSel.Text = Format(DblEur, "#,###0.00 €;-#,###0.00 €;#")
    End Sub


    Private Function CurrentPnd() As Pnd
        Dim oPnd As Pnd = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iPnd As Integer = oRow.Cells(Cols.Id).Value
            oPnd = New Pnd(iPnd)
        End If
        Return oPnd
    End Function


    Private Sub RefrescaToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefrescaToolStripButton.Click
        LoadGrid()
    End Sub


    Private Function MenuItem_Pnds() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "altres partides"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf ShowContactPnds
        Return oMenuItem
    End Function

    Private Sub ShowContactPnds(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPnd As Pnd = CurrentPnd()
        root.ShowContactPnds(oPnd.Contact)
    End Sub


    Private Sub ToolStripButtonAddBanc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonAddBanc.Click
        Dim s As String = InputBox("Seleccionar tots els efectes del següent banc:", "GIRS", "0000")
        If s > "" Then
            Dim oTb As DataTable = mDs.Tables(0)
            Dim oRow As DataRow
            Dim DblEur As Decimal
            Dim DblSum As Decimal
            Dim DblSel As Decimal = mDblSelAmt
            Dim i As Integer

            Dim oCountry As New Country("ES")
            'Dim oBankToAdd As New Bank(oCountry, s)
            'If Not oBankToAdd.Exists Then
            ' MsgBox("Codi bancari " & s & " desconegut", MsgBoxStyle.Exclamation, "MAT.NET")
            ' Exit Sub
            'End If
            'Dim oBankGroup As BankGroup = oBankToAdd.Group
            'Dim oBank As Bank

            'Dim oIban As DTOIban
            'Dim sBankId As String
            'For i = 0 To oTb.Rows.Count - 1
            'oRow = oTb.Rows(i)
            'oIban = New Iban(oRow(Cols.Bnc))
            'sBankId = oIban.Bank.Id
            'DblEur = oRow(Cols.Eur)
            'For Each oBank In oBankGroup.Banks
            ' If sBankId = oBank.Id Then
            ' oRow(Cols.Chk) = True
            ' Exit For
            ' End If
            ' Next

            '    If oRow(Cols.Chk) = True Then
            'DblSum += DblEur
            'End If
            'Next
            '
            DblSum = mDblSelAmt
            TextBoxSel.Text = Format(DblSum, "#,###0.00 €;-#,###0.00 €;#")
            RaiseEvent Changed(Me, New System.EventArgs)
            SetTotals()
        End If
    End Sub

    Private Sub ToolStripButtonDelBanc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonDelBanc.Click
        Dim s As String = InputBox("retirar tots els efectes del següent banc:", "GIRS", "0000")
        If s > "" Then
            'Dim oTb As DataTable = mDs.Tables(0)
            'Dim oRow As DataRow
            'Dim DblEur As Decimal
            'Dim DblSum As Decimal
            'Dim DblSel As Decimal = mDblSelAmt
            'Dim i As Integer
            '
            '            Dim oCountry As New Country("ES")
            '            Dim oBankToAdd As New Bank(oCountry, s)
            '            If Not oBankToAdd.Exists Then
            ' MsgBox("Codi bancari " & s & " desconegut", MsgBoxStyle.Exclamation, "MAT.NET")
            ' Exit Sub
            'End  If
            'Dim oBank As Bank
            'Dim oBankGroup As BankGroup = oBankToAdd.Group

            'Dim oIban As Iban
            'Dim sBankId As String
            'For i = 0 To oTb.Rows.Count - 1
            ' oRow = oTb.Rows(i)
            ' oIban = New Iban(oRow(Cols.Bnc))
            ' sBankId = oIban.Bank.Id
            ' DblEur = oRow(Cols.Eur)
            ' For Each oBank In oBankGroup.Banks
            ' If sBankId = oBank.Id Then
            ' oRow(Cols.Chk) = False
            ' Exit For
            ' End If
            '    Next

            'If oRow(Cols.Chk) = True Then
            ' DblSum += DblEur
            'End If
            '   Next

            'mDblSelAmt = DblSum
            'TextBoxSel.Text = Format(DblSum, "#,###0.00 €;-#,###0.00 €;#")
            'RaiseEvent Changed(Me, New System.EventArgs)
            'SetTotals()
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Chk

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
            MsgBox("no hi han factures registrades!", MsgBoxStyle.Exclamation)
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oPnd As Pnd = CurrentPnd()

        If oPnd IsNot Nothing Then
            Dim oMenu_Pnd As New Menu_Pnd(oPnd)
            AddHandler oMenu_Pnd.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pnd.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoWarnJoin
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim sCod As String = CStr(oRow.Cells(Cols.WarnJoin).Value)
                If sCod = "1" Then
                    e.Value = My.Resources.clip
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.IcoWarnQuadra
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim sCod As String = CStr(oRow.Cells(Cols.WarnQuadra).Value)
                If sCod = "1" Then
                    e.Value = My.Resources.warn
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oPnd As Pnd = CurrentPnd()
        If oPnd IsNot Nothing Then
            Dim oFrm As New Frm_Contact_Pnd(oPnd)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim BlDisable As Boolean = False
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

        If oRow.Cells(Cols.Eur).Value < 0 Then
            PaintGradientRowBackGround(e, MaxiSrvr.COLOR_NOTOK)
        Else

            Select Case mMode
                Case Modes.SEPAB2B
                    Select Case CBool(oRow.Cells(Cols.Sepa).Value)
                        Case False
                            BlDisable = True
                    End Select
                Case Modes.Norma58, Modes.NoDomiciliats
                    Select Case CBool(oRow.Cells(Cols.Sepa).Value)
                        Case True
                            BlDisable = True
                    End Select
            End Select

            If BlDisable Then
                oRow.DefaultCellStyle.BackColor = COLOR_DISABLED
                oRow.ReadOnly = mMode <> Modes.Norma58 ' True
            Else
                oRow.ReadOnly = False
                If CDate(oRow.Cells(Cols.Vto).Value) <= Today Then
                    PaintGradientRowBackGround(e, Color.Yellow)
                Else
                    oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
                End If
            End If
        End If
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
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub TextBoxSel_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxSel.Validated
        Dim DblEur As Decimal
        Dim DblSum As Decimal
        Dim DblSel As Decimal = mDblSelAmt

        Dim oRow As DataGridViewRow
        For Each oRow In DataGridView1.Rows
            DblEur = oRow.Cells(Cols.Eur).Value
            If DblEur > 0 And oRow.Cells(Cols.WarnQuadra).Value = "" Then
                Dim ActivateCheck As Boolean = Not oRow.ReadOnly
                If DblSum + DblEur > DblSel Then ActivateCheck = False


                If ActivateCheck Then
                    DblSum += DblEur
                    oRow.Cells(Cols.Chk).Value = True
                Else
                    oRow.Cells(Cols.Chk).Value = False
                End If
            End If
        Next

        mDblSelAmt = DblSum
        TextBoxSel.Text = Format(DblSum, "#,###0.00 €;-#,###0.00 €;#")
        RaiseEvent Changed(Me, New System.EventArgs)
    End Sub

    Private Sub TextBoxSel_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBoxSel.Validating
        Dim s As String = TextBoxSel.Text
        If s = "" Then s = "0"
        If IsNumeric(s) Then
            mDblSelAmt = CDbl(s)
        Else
            MsgBox("només s'accepten valors numerics!", MsgBoxStyle.Exclamation, "MAT.NET")
            e.Cancel = True
        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Chk
                SetTotals()
                RaiseEvent Changed(Me, New System.EventArgs)
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Sub ToolStripButtonMinVtos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonMinVtos.Click
        Dim oRow As DataGridViewRow
        Dim DtVto As Date
        Dim DtMin As Date = Today.AddDays(5)
        For Each oRow In DataGridView1.Rows
            DtVto = oRow.Cells(Cols.Vto).Value
            If DtVto < DtMin Then
                oRow.Cells(Cols.Vto).Value = DtMin
            End If
        Next
    End Sub
End Class
