

Public Class Xl_Pnd_Select


    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Private mPnds As Pnds
    Private mImpagats As Impagats
    Private mPndIdx As Integer = 0
    Private mImpagatsIdx As Integer = 0
    Private mTb As DataTable
    Private mCodi As Pnd.Codis = Pnd.Codis.NotSet
    Private mShowDivisas As Boolean
    Private mDefaultCur As maxisrvr.Cur = maxisrvr.DefaultCur
    Private mSum As maxisrvr.Amt

    Public Enum Totals
        All
        AllChecked
        CheckedPendents
        CheckedImpagats
    End Enum

    Private Enum Cols
        Chk
        Vto
        Amt
        Eur
        Cur
        Fra
        Fch
        Cta
        Obs
        Dh
        Idx
        Xec
        LinCod
        PndGuid
    End Enum

    Private Enum LinCods
        pendent
        impagat
    End Enum

    Public Property Pnds() As Pnds
        Get
            Dim oPnds As New Pnds
            If mTb IsNot Nothing Then
                For Each oRow As DataRow In mTb.Rows
                    If oRow(Cols.Chk) Then
                        If CType(oRow(Cols.LinCod), LinCods) = LinCods.pendent Then
                            oPnds.Add(mPnds(oRow(Cols.Idx)))
                        End If
                    End If
                Next
            End If
            Return oPnds
        End Get
        Set(ByVal Value As Pnds)
            If Not Value Is Nothing Then
                mPnds = Value
                LoadGrid()
                SetTotals()
            End If
        End Set
    End Property

    Public Property Impagats() As Impagats
        Get
            Dim oImpagats As New Impagats
            If mTb IsNot Nothing Then
                For Each oRow As DataRow In mTb.Rows
                    If oRow(Cols.Chk) Then
                        If CType(oRow(Cols.LinCod), LinCods) = LinCods.impagat Then
                            oImpagats.Add(mImpagats(oRow(Cols.Idx)))
                        End If
                    End If
                Next
            End If
            Return oImpagats
        End Get
        Set(ByVal oImpagats As Impagats)
            If Not oImpagats Is Nothing Then
                mImpagats = oImpagats
                LoadGrid()
                SetTotals()
            End If
        End Set
    End Property

    Public Sub SetCheckedItems(oPndsToCheck As Pnds)
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            Dim oGuid As Guid = oRow.Cells(Cols.PndGuid).Value
            If oGuid <> Nothing Then
                Dim BlCheck As Boolean = oPndsToCheck.Exists(Function(x) x.Guid.Equals(oGuid))
                oRow.Cells(Cols.Chk).Value = BlCheck
            End If
        Next
    End Sub

    Public ReadOnly Property Amt() As maxisrvr.Amt
        Get
            If mSum.Cur Is Nothing Then
                Return New Amt(mDefaultCur)
            Else
                Return mSum
            End If
        End Get
    End Property

    Public WriteOnly Property Codi() As Pnd.Codis
        Set(ByVal Value As Pnd.Codis)
            mCodi = Value
        End Set
    End Property

    Private Sub CreateDatatable()
        If mTb Is Nothing Then
            mTb = New DataTable("PNDS")
            mTb.Columns.Add("CHK", System.Type.GetType("System.Boolean"))
            mTb.Columns.Add("VTO", System.Type.GetType("System.DateTime"))
            mTb.Columns.Add("AMT", System.Type.GetType("System.Decimal"))
            mTb.Columns.Add("EUR", System.Type.GetType("System.Decimal"))
            mTb.Columns.Add("CUR", System.Type.GetType("System.String"))
            mTb.Columns.Add("FRA", System.Type.GetType("System.String"))
            mTb.Columns.Add("FCH", System.Type.GetType("System.DateTime"))
            mTb.Columns.Add("CTA", System.Type.GetType("System.String"))
            mTb.Columns.Add("OBS", System.Type.GetType("System.String"))
            mTb.Columns.Add("DH", System.Type.GetType("System.Int32"))
            mTb.Columns.Add("IDX", System.Type.GetType("System.Int32"))
            mTb.Columns.Add("XEC", System.Type.GetType("System.Int32"))
            mTb.Columns.Add("LINCOD", System.Type.GetType("System.Int32"))
            mTb.Columns.Add("PndGuid", System.Type.GetType("System.Guid"))
        End If
        mTb.Rows.Clear()
    End Sub


    Private Sub LoadGrid()

        CreateDatatable()
        Dim oRow As DataRow

        mPndIdx = 0
        If mPnds IsNot Nothing Then
            Dim oPnd As Pnd
            For Each oPnd In mPnds
                oRow = mTb.NewRow()
                If SetDataRowFromPnd(oRow, oPnd) Then
                    mTb.Rows.Add(oRow)
                    'If oPnd.Cod = mCodi Then
                    'oSum.Add(oPnd.Amt)
                    'Else
                    'oSum.Substract(oPnd.Amt)
                    'End If
                End If
            Next
        End If

        mImpagatsIdx = 0
        If mImpagats IsNot Nothing Then
            Dim oImpagat As Impagat
            For Each oImpagat In mImpagats
                oRow = mTb.NewRow()
                If SetDataRowFromImpagat(oRow, oImpagat) Then
                    mTb.Rows.Add(oRow)
                    'oSum.Add(oImpagat.Pendent)
                End If
            Next
        End If

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
            End With
            .DataSource = mTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(Cols.Chk)
                .HeaderText = ""
                .Width = 20
                .DefaultCellStyle.SelectionBackColor = Color.White
            End With
            With .Columns(Cols.Vto)
                .HeaderText = "venciment"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "Import"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Eur)
                If mShowDivisas Then
                    .HeaderText = "Euros"
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    .Visible = False
                End If
            End With
            With .Columns(Cols.Cur)
                .Visible = False
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "factura"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Cta)
                .HeaderText = "compte"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Obs)
                .HeaderText = "Observacions"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Dh)
                .Visible = False
            End With
            With .Columns(Cols.Idx)
                .Visible = False
            End With
            With .Columns(Cols.Xec)
                .Visible = False
            End With
            With .Columns(Cols.LinCod)
                .Visible = False
            End With
            With .Columns(Cols.PndGuid)
                .Visible = False
            End With
        End With


        TextBoxTot.Text = GetTotal(Totals.All).CurFormat
        TextBoxSel.Text = ""

    End Sub

    Public Function GetTotal(oTotalMode As Totals)
        Dim retval As New maxisrvr.Amt()
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            Dim BlChecked As Boolean = oRow.Cells(Cols.Chk).Value
            Dim oLinCod As LinCods = CType(oRow.Cells(Cols.LinCod).Value, LinCods)

            Dim BlProceed As Boolean = False
            Select Case oTotalMode
                Case Totals.All
                    BlProceed = True
                Case Totals.AllChecked
                    BlProceed = BlChecked
                Case Totals.CheckedPendents
                    BlProceed = BlChecked And oLinCod = LinCods.pendent
                Case Totals.CheckedImpagats
                    BlProceed = BlChecked And oLinCod = LinCods.impagat
            End Select

            If BlProceed Then
                Dim DcDivisa As Decimal = CDec(oRow.Cells(Cols.Amt).Value)
                Dim DcEur As Decimal = CDec(oRow.Cells(Cols.Eur).Value)
                Dim sCur As String = oRow.Cells(Cols.Cur).Value.ToString
                Dim oCur As Cur = Current.Cur(sCur)
                Dim oAmt As New maxisrvr.Amt(DcEur, oCur, DcDivisa)
                retval.Add(oAmt)
            End If

        Next
        Return retval
    End Function

    Private Function SetDataRowFromPnd(ByRef oRow As DataRow, ByVal oPnd As Pnd) As Boolean
        Dim oAmt As maxisrvr.Amt = oPnd.Amt
        If oPnd.Cod <> mCodi Then
            oAmt = oAmt.Times(-1)
        End If

        oRow(Cols.Chk) = False
        oRow(Cols.Vto) = CDate(oPnd.Vto)
        oRow(Cols.Amt) = oAmt.Val
        oRow(Cols.Eur) = oAmt.Eur
        oRow(Cols.Cur) = oAmt.Cur.Id.ToString
        oRow(Cols.Fra) = oPnd.FraNum
        oRow(Cols.Fch) = oPnd.Fch
        oRow(Cols.Cta) = oPnd.Cta.Id
        oRow(Cols.Obs) = oPnd.Fpg
        oRow(Cols.Dh) = oPnd.Cod
        oRow(Cols.Idx) = mPndIdx
        oRow(Cols.PndGuid) = oPnd.guid
        'If oPnd.Xec Is Nothing Then
        'oRow(Cols.Xec) = 0
        'Else
        'oRow(Cols.Xec) = oPnd.Xec.Id
        'End If
        oRow(Cols.LinCod) = LinCods.pendent
        If oPnd.Amt.Cur.Id <> mDefaultCur.Id Then mShowDivisas = True
        mPndIdx += 1
        Return True
    End Function

    Private Function SetDataRowFromImpagat(ByRef oRow As DataRow, ByVal oImpagat As Impagat) As Boolean
        Dim oPlan As PgcPlan = PgcPlan.FromToday
        Dim oCtaImpagats As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.impagats)
        With oImpagat
            oRow(Cols.Chk) = False
            oRow(Cols.Vto) = .Csb.Vto
            oRow(Cols.Amt) = .Pendent.Eur
            oRow(Cols.Eur) = .Pendent.Eur
            oRow(Cols.Cur) = .Pendent.Cur.Id.ToString

            Dim oFra As Fra = .GuessFra
            If oFra IsNot Nothing Then
                oRow(Cols.Fra) = oFra.Id
                oRow(Cols.Fch) = oFra.Fch
            End If

            oRow(Cols.Cta) = oCtaImpagats.Id
            oRow(Cols.Obs) = .Obs
            oRow(Cols.Dh) = Pnd.Codis.Deutor
            oRow(Cols.Idx) = mImpagatsIdx
            oRow(Cols.LinCod) = LinCods.impagat
            mImpagatsIdx += 1
            Return True
        End With
    End Function

    Private Sub SetTotals()
        Dim oSum As maxisrvr.Amt = GetTotal(Totals.AllChecked)

        Dim s As String = ""
        If oSum.Cur IsNot Nothing Then
            s = oSum.CurFormat
            If oSum.Cur.Id <> mDefaultCur.Id Then
                s = s & " (" & Format(oSum.Eur, "#,###.00 EUR") & ")"
            End If

        End If
        TextBoxSel.Text = s
        mSum = oSum
    End Sub

    Private Function CurrentPnd(oRow As DataGridViewRow) As Pnd
        Dim oPnd As Pnd = Nothing
        Try
            If oRow IsNot Nothing Then
                If Not IsDBNull(oRow.Cells(Cols.Idx).Value) Then
                    oPnd = mPnds(oRow.Cells(Cols.Idx).Value)
                End If
            End If

        Catch ex As Exception

        End Try
        Return oPnd
    End Function

    Private Function CurrentImpagat(oRow As DataGridViewRow) As Impagat
        Dim oImpagat As Impagat = Nothing
        Try
            If oRow IsNot Nothing Then
                If Not IsDBNull(oRow.Cells(Cols.Idx).Value) Then
                    oImpagat = mImpagats(oRow.Cells(Cols.Idx).Value)
                End If
            End If

        Catch ex As Exception

        End Try
        Return oImpagat
    End Function

    Private Function CurrentLinCod(oRow As DataGridViewRow) As LinCods
        Dim oLinCod As LinCods = LinCods.pendent
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.LinCod).Value) Then
                oLinCod = oRow.Cells(Cols.LinCod).Value
            End If
        End If
        Return oLinCod
    End Function

    Private Sub SetContextMenu(oRow As DataGridViewRow)
        Dim oContextMenu As New ContextMenuStrip

        Select Case CurrentLinCod(oRow)
            Case LinCods.pendent
                Dim oPnd As Pnd = CurrentPnd(oRow)

                If oPnd IsNot Nothing Then
                    Dim oMenu_Pnd As New Menu_Pnd(oPnd)
                    AddHandler oMenu_Pnd.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Pnd.Range)
                End If
            Case LinCods.impagat
                Dim oImpagat As Impagat = CurrentImpagat(oRow)

                If oImpagat IsNot Nothing Then
                    Dim oMenu_Impagat As New Menu_Impagat(oImpagat)
                    AddHandler oMenu_Impagat.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Impagat.Range)
                End If
        End Select

        DataGridView1.ContextMenuStrip = oContextMenu
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
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Amt
                Try
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim oCur As maxisrvr.Cur = mPnds(oRow.Cells(Cols.Idx).Value).Amt.Cur
                    Dim sFormat As String = ""
                    Select Case oCur.Id
                        Case "EUR"
                            sFormat = "#,###0.00 €;-#,###0.00 €;#"
                        Case "GBP"
                            sFormat = "£ #,###0.00;£ -#,###0.00;#"
                        Case "USD"
                            sFormat = "$ #,###0.00;$ -#,###0.00;#"
                        Case Else
                            sFormat = "#,###0.00;-#,###0.00;#"
                    End Select
                    e.Value = Format(CDbl(e.Value), sFormat)

                Catch ex As Exception

                End Try
        End Select
    End Sub


    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        If Not IsDBNull(oRow.Cells(Cols.Xec).Value) Then
            If CInt(oRow.Cells(Cols.Xec).Value) > 0 Then
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            End If
        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        'SetContextMenu()
        'canviat a DataGridView1_MouseUp
    End Sub

    Private Sub DataGridView1_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim oGrid As DataGridView = CType(sender, DataGridView)
            Dim ClickedInfo As System.Windows.Forms.DataGridView.HitTestInfo = oGrid.HitTest(e.X, e.Y)
            If ClickedInfo.RowIndex >= 0 Then
                Dim oRow As DataGridViewRow = oGrid.Rows(ClickedInfo.RowIndex)
                SetContextMenu(oRow)
            End If
        End If
    End Sub


    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Chk
                SetTotals()
                If e.RowIndex >= 0 Then
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Select Case CurrentLinCod(oRow)
                        Case LinCods.pendent
                            RaiseEvent AfterUpdate(CurrentPnd(oRow), EventArgs.Empty)
                        Case LinCods.impagat
                            RaiseEvent AfterUpdate(CurrentImpagat(oRow), EventArgs.Empty)
                    End Select
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub
End Class
