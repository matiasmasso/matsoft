

Public Class Xl_Contact_Pnds_Old
    Private mContact As Contact
    Private mDs As DataSet
    Private mArrayCtas As ArrayList
    Private mArraySdos As ArrayList
    Private mShowDivisas As Boolean
    Private mAllowEvents As Boolean
    Private mTot As maxisrvr.Amt
    Private mMenuInclouSaldats As ToolStripMenuItem
    Private mMenuCapDAny As ToolStripMenuItem
    Private WithEvents mPnd As Pnd
    Private mCsb As Csb

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum LineModes
        Pnd
        Csb
        Impagat
        Insolvencia
        Reembols
        TranfPrevia
    End Enum

    Private Enum Cols
        Id
        Status
        IcoStatus
        Vto
        Val
        Cur
        Eur
        Cuadra
        IcoCuadra
        Cta
        Fra
        FraFch
        Txt
        Ad
        CsbYea
        CsbId
        Doc
        Guid
    End Enum

    Public WriteOnly Property Contact() As Contact
        Set(ByVal value As Contact)
            If value IsNot Nothing Then
                mContact = value
                Me.Text = Me.Text & " " & mContact.Clx

                mMenuInclouSaldats = New ToolStripMenuItem("inclou saldats")
                mMenuInclouSaldats.CheckOnClick = True
                AddHandler mMenuInclouSaldats.CheckedChanged, AddressOf RefreshRequest

                Dim DtCapDAny As New Date(Today.Year - 1, 12, 31)
                mMenuCapDAny = New ToolStripMenuItem("retroactiu " & Format(DtCapDAny, "dd/MM/yy"))
                mMenuCapDAny.CheckOnClick = True
                AddHandler mMenuCapDAny.CheckedChanged, AddressOf RefreshRequest

                LoadGrid()
                Calcula()
                'CheckSaldos()

                SetContextMenu()
                mAllowEvents = True
            End If
        End Set
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Id, Status, vto, pts AS VAL, Div AS CUR, eur, '' AS WARN, Cta, fra, fch, fpg AS TXT, ad, CsaYea, CsaId, CsaDoc, NEWID() " _
        & "FROM Pnd " _
        & "WHERE Emp =" & mContact.Emp.Id & " AND cli =" & mContact.Id & " "

        If mMenuCapDAny.Checked Then
            Dim DtCapDAny As New Date(Today.Year, 1, 1)
            Dim sCapDAny As String = Format(DtCapDAny, "yyyyMMdd")
            SQL = SQL & "AND PND.FCH< '" & sCapDAny & "' AND " _
            & "(Status < " & CInt(Pnd.StatusCod.saldat).ToString & " OR PND.VTO >'" & sCapDAny & "') "
        ElseIf Not mMenuInclouSaldats.Checked Then
            SQL = SQL & "AND Status < " & CInt(Pnd.StatusCod.saldat).ToString & " "
        End If

        ', CSB.ROWGUID " _

        SQL = SQL & "UNION " _
        & "SELECT 0 AS Id, 98 AS Status, CSB.vto, (CSB.eur+IMP.Gastos-IMP.PagatACompte) AS Val, 'EUR' AS Cur, (CSB.eur+IMP.Gastos-IMP.PagatACompte) AS Eur,'' AS warn, '4315' AS CTA, '' AS FRA, CSB.VTO, CAST(IMP.Obs AS VARCHAR) AS TXT, 'D' AS AD, IMP.Yea, IMP.Csa, IMP.Csb, NEWID() " _
        & "FROM IMPAGATS AS IMP INNER JOIN CSB ON IMP.Emp = CSB.Emp AND IMP.Yea = CSB.yea AND IMP.Csa = CSB.Csb AND IMP.Csb = CSB.Doc " _
        & "WHERE IMP.Emp =" & mContact.Emp.Id & " AND CSB.cli =" & mContact.Id & " AND IMP.Status <" & Impagat.StatusCodes.Saldat & " " _
        & "UNION " _
        & "SELECT Id, 99 AS STATUS, FchPresentacio, (Nominal+Gastos+Comisio-PagatACompte) AS VAL, 'EUR' AS CUR, (Nominal+Gastos+Comisio-PagatACompte) AS EUR,'' AS WARN, '4315' AS CTA, '' AS FRA, FchLiquidacio, '' AS TXT, 'D' AS AD, 0 AS CSAYEA, 0 AS CSAId, 0 AS CSADOC, NEWID() " _
        & "FROM INSOLVENCIAS " _
        & "WHERE FchRehabilitacio IS NULL AND Emp =" & mContact.Emp.Id & " AND Cli =" & mContact.Id & " " _
        & "ORDER BY VTO"

        'Dim SQLCSB As String = "SELECT '1' AS BLOCKED, CSB.VTO, CSB.VAL, CSB.CUR, CSB.EUR, '' as WARN, '430' AS CTA, cast(CSB.FRA as varchar), FRA.FCH," _
        '& "(cast(CSB.YEA as VARCHAR)+'.'+CAST(CSB.CSB AS VARCHAR)+'.'+CAST(Csb.DOC AS VARCHAR)+' '+CLIBNC.ABR+' '+CAST(DATEPART(dd,CSA.FCH)AS VARCHAR)+'/'+CAST(DATEPART(mm,CSA.FCH)AS VARCHAR)+'/'+CAST(DATEPART(yy,CSA.FCH)AS VARCHAR)) AS TXT, 'D' AS AD, CSB.yea AS CSBYEA, CSB.CSB, Csb.Id " _
        '& "FROM CSB LEFT OUTER JOIN FRA ON CSB.EMP=FRA.EMP AND CSB.YEF=FRA.YEA AND CSB.FRA=FRA.FRA INNER JOIN " _
        '& "CSA ON CSB.EMP = CSA.EMP AND CSB.YEA = CSA.YEA AND CSB.CSB = CSA.CSB LEFT OUTER JOIN " _
        '& "CLIBNC ON CSB.EMP=CLIBNC.EMP AND CSA.BNC=CLIBNC.CLI " _
        '& "WHERE CSB.EMP=" & mContact.Emp.Id & " AND CSB.CLI=" & mContact.Id & " AND CSB.CCAVTOCCA=0 "

        'Dim SQL As String = "SELECT * FROM (" & SQLPND & " UNION " & SQLCSB & ") DERIVEDTBL " _
        '& "ORDER BY AD, CTA, VTO"
        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)


        Dim oTb As DataTable = mDs.Tables(0)

        Dim oColStatus As DataColumn = oTb.Columns.Add("ICOSTATUS", System.Type.GetType("System.Byte[]"))
        oColStatus.SetOrdinal(Cols.IcoStatus)
        Dim oColCuadra As DataColumn = oTb.Columns.Add("ICOCUADRA", System.Type.GetType("System.Byte[]"))
        oColCuadra.SetOrdinal(Cols.IcoCuadra)

        Dim oClient As Client = mContact.Client
        Dim oRow As DataRow
        If oClient.Exists Then
            Dim sCta As String = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.clients).Id
            Dim oAlbsPagats As Albs = oClient.AlbaransPagatsPendentsDeFacturar
            For Each oalb As Alb In oAlbsPagats
                oRow = oTb.NewRow
                Dim oCash As maxisrvr.Amt = oalb.TotalCash
                oRow(Cols.Eur) = -oCash.Eur
                oRow(Cols.Val) = -oCash.Val
                oRow(Cols.Cur) = oCash.Cur.Id
                oRow(Cols.Cta) = sCta
                oRow(Cols.Ad) = "D"
                oRow(Cols.Cuadra) = 0
                Select Case oalb.CashCod
                    Case DTO.DTOCustomer.CashCodes.Reembols
                        oRow(Cols.Txt) = "alb." & oalb.Id & " cobrat contra reembols"
                        oRow(Cols.Vto) = oalb.FchCobroReembolso
                        oRow(Cols.Status) = 101
                        oRow(Cols.Guid) = oalb.Guid.ToString
                    Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia
                        oRow(Cols.Txt) = "alb." & oalb.Id & " transferencia previa"
                        oRow(Cols.Vto) = oalb.Fch
                        oRow(Cols.Status) = 102
                        oRow(Cols.Guid) = oalb.Guid.ToString
                    Case DTO.DTOCustomer.CashCodes.Visa
                        oRow(Cols.Txt) = "alb." & oalb.Id & " visa"
                        oRow(Cols.Vto) = oalb.Fch
                        oRow(Cols.Status) = 102
                        oRow(Cols.Guid) = oalb.Guid.ToString
                End Select
                oTb.Rows.Add(oRow)
            Next

        End If
        If oTb.Rows.Count = 0 Then
            'ZoomToolStripButton.Enabled = False
            'ExtracteToolStripButton.Enabled = False
            'ExcelToolStripButton.Enabled = False
        End If


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
            .AllowDrop = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Status)
                .Visible = False
            End With
            With .Columns(Cols.IcoStatus)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Vto)
                .HeaderText = "venciment"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            With .Columns(Cols.Val)
                .Visible = mShowDivisas
                If mShowDivisas Then
                    .HeaderText = "divisas"
                    .Width = 90
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            End With
            With .Columns(Cols.Cur)
                .Visible = False
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Cuadra)
                .Visible = False
            End With
            With .Columns(Cols.IcoCuadra)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Cta)
                .HeaderText = "compte"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "factura"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.FraFch)
                .HeaderText = "data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Txt)
                .HeaderText = "observacions"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Ad)
                .Visible = False
            End With
            With .Columns(Cols.CsbYea)
                .Visible = False
            End With
            With .Columns(Cols.CsbId)
                .Visible = False
            End With
            With .Columns(Cols.Doc)
                .Visible = False
            End With
            With .Columns(Cols.Guid)
                .Visible = False
            End With

        End With


    End Sub

    Private Sub Calcula()
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim oDeutor As maxisrvr.Amt = New maxisrvr.Amt(0, Nothing, 0)
        Dim oCreditor As maxisrvr.Amt = New maxisrvr.Amt(0, Nothing, 0)
        Dim oAmt As maxisrvr.Amt
        Dim oCur As maxisrvr.Cur
        Dim sCta As String = ""
        mArrayCtas = New ArrayList
        mArraySdos = New ArrayList
        mShowDivisas = False
        Dim Idx As Integer
        For Each oRow In oTb.Rows
            oCur = Current.Cur(oRow(Cols.Cur))
            If oCur.Id <> "EUR" Then mShowDivisas = True
            oAmt = New maxisrvr.Amt(oRow(Cols.Val), oCur, oRow(Cols.Eur))

            Select Case CStr(oRow(Cols.Ad)).ToUpper
                Case "A"
                    oCreditor.Add(oAmt)
                    'PagarToolStripButton.Enabled = True
                Case "D"
                    oDeutor.Add(oAmt)
                    'CobrarToolStripButton.Enabled = True
            End Select

            If sCta <> oRow(Cols.Cta) Then
                sCta = oRow(Cols.Cta)
                mArrayCtas.Add(sCta)
                mArraySdos.Add(0)
                Idx = mArrayCtas.Count - 1
            End If
            mArraySdos(Idx) = mArraySdos(Idx) + oAmt.Eur
        Next


        If oDeutor.Val = 0 Then
            LabelDeutor.Text = ""
        Else
            LabelDeutor.Text = "total deutor: " & oDeutor.CurFormat
        End If

        If oCreditor.Val = 0 Then
            LabelCreditor.Text = ""
        Else
            LabelCreditor.Text = "total creditor: " & oCreditor.CurFormat
        End If

        If oDeutor.Val > oCreditor.Val Then
            mTot = oDeutor.Clone
            mTot.Substract(oCreditor)
        Else
            mTot = oCreditor.Clone
            mTot.Substract(oDeutor)
        End If
    End Sub

    Private Sub CheckSaldos()
        CheckSdo("43000")
        'CheckSdo("4315")
        CheckSdo("40000")
        CheckSdo("41000")
        CheckSdo("44000")
    End Sub

    Private Sub CheckSdo(ByVal sCta As String)
        Dim oPlan As PgcPlan = PgcPlan.FromToday
        Dim oCta As PgcCta = MaxiSrvr.PgcCta.FromNum(oPlan, sCta)
        Dim oCcd As New Ccd(mContact, Today.Year, oCta)
        Dim Idx As Integer
        Dim oSdo As maxisrvr.Amt = Nothing
        For Idx = 0 To mArrayCtas.Count - 1
            If mArrayCtas(Idx) = sCta Then
                If oCcd.Saldo(Today).Eur <> Math.Round(mArraySdos(Idx), 2) Then
                    WarnCta(sCta)
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub WarnCta(ByVal sCta As String)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            If oRow(Cols.Cta) = sCta Then
                oRow(Cols.Cuadra) = "1"
            End If
        Next
    End Sub

    Private Sub ShowCurrentItm()
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Select Case CurrentLineMode(oRow)
            Case LineModes.Csb
                ShowCsb()
            Case LineModes.Pnd
                ShowPnd()
            Case LineModes.Impagat
                Dim oFrm As New Frm_Impagat
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                With oFrm
                    .Impagat = CurrentImpagat()
                    .Show()
                End With
            Case LineModes.Insolvencia
                Dim oFrm As New Frm_Insolvencia
                With oFrm
                    .Insolvencia = currentinsolvencia
                    .Show()
                End With
        End Select
    End Sub

    Private Sub ShowPnd()
        Dim oFrm As New Frm_Contact_Pnd(CurrentPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub ShowCsb()
        Dim oFrm As New Frm_Csb
        AddHandler oFrm.AfterUpdate, AddressOf refreshrequest
        With oFrm
            .Csb = CurrentCsb
            .Show()
        End With
    End Sub

    Private Function CurrentLineMode(ByVal oRow As DataGridViewRow) As LineModes
        Dim oLineMode As LineModes = LineModes.Pnd
        If oRow IsNot Nothing Then
            Select Case oRow.Cells(Cols.Status).Value
                Case 98
                    oLineMode = LineModes.Impagat
                Case 99
                    oLineMode = LineModes.Insolvencia
                Case 101
                    oLineMode = LineModes.Reembols
                Case 102
                    oLineMode = LineModes.TranfPrevia
                Case Else
                    If oRow.Cells(Cols.CsbId).Value > 0 Then
                        oLineMode = LineModes.Csb
                    Else
                        oLineMode = LineModes.Pnd
                    End If
            End Select
        End If
        Return oLineMode
    End Function



    Private Function CurrentCsb() As Csb
        Dim oCsb As Csb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oCsa As Csa = MaxiSrvr.Csa.FromNum(mContact.Emp, oRow.Cells(Cols.CsbYea).Value, oRow.Cells(Cols.CsbId).Value)
            oCsb = New Csb(oCsa, oRow.Cells(Cols.Doc).Value)
        End If
        Return oCsb
    End Function

    Private Function CurrentPnd() As Pnd
        Dim oPnd As Pnd = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oPnd = New Pnd(oRow.Cells(Cols.Id).Value)
        End If
        Return oPnd
    End Function

    Private Function CurrentInsolvencia() As Insolvencia
        Dim oInsolvencia As Insolvencia = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oInsolvencia = New Insolvencia(mContact.Emp, oRow.Cells(Cols.Id).Value)
        End If
        Return oInsolvencia
    End Function

    Private Function CurrentImpagat() As Impagat
        Dim oImpagat As Impagat = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oImpagat = New Impagat(New Csb(MaxiSrvr.Csa.FromNum(mContact.Emp, oRow.Cells(Cols.CsbYea).Value, oRow.Cells(Cols.CsbId).Value), oRow.Cells(Cols.Doc).Value))
        End If
        Return oImpagat
    End Function

    Private Function CurrentImpagats() As Impagats
        Dim oImpagats As New Impagats
        If DataGridView1.SelectedRows.Count = 0 Then
            oImpagats.Add(CurrentImpagat)
        Else
            Dim oEmp As DTOEmp = mContact.Emp
            Dim oCsa As Csa
            Dim oCsb As Csb
            For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
                oCsa = MaxiSrvr.Csa.FromNum(oEmp, oRow.Cells(Cols.CsbYea).Value, oRow.Cells(Cols.CsbId).Value)
                oCsb = New Csb(oCsa, oRow.Cells(Cols.CsbId).Value)
                oImpagats.Add(New Impagat(oCsb))
            Next
        End If
        Return oImpagats
    End Function

    Private Function CurrentPnds() As Pnds
        Dim oPnds As New Pnds

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim LngId As Integer
            Dim oPnd As Pnd
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                LngId = oRow.Cells(Cols.Id).Value
                oPnd = New Pnd(LngId)
                oPnds.Add(oPnd)
            Next
        Else
            Dim oPnd As Pnd = CurrentPnd()
            If oPnd IsNot Nothing Then
                oPnds.Add(CurrentPnd)
            End If
        End If
        Return oPnds
    End Function


    Private Sub ExtracteToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles ExtracteToolStripButton.Click
        Dim IntYea As Integer = Today.Year
        Dim oPlan As PgcPlan = PgcPlan.FromToday
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim oCta As PgcCta = MaxiSrvr.PgcCta.FromNum(oPlan, oRow.Cells(Cols.Cta).Value)
        Dim oFrm As New Frm_CliCtas(mContact, oCta)
        oFrm.Show()
    End Sub

    Private Sub CobrarToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles CobrarToolStripButton.Click
        root.WzCobrament(mContact)
    End Sub

    Private Sub PagarToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles PagarToolStripButton.Click
        root.WzPagament(mContact)
    End Sub

    Private Sub AfegirToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles AfegirToolStripButton.Click
        Dim oPlan As PgcPlan = PgcPlan.FromToday
        Dim oPnd As New Pnd
        With oPnd
            .Contact = mContact
            .Fch = Today
            .Vto = Today
            .Status = Pnd.StatusCod.pendent
            If .Contact.Proveidor.Exists Then
                .Cod = Pnd.Codis.Creditor
                .Cta = oPlan.Cta(PgcPlan.GetCtaProveedors(mContact.Proveidor.DefaultCur))
                .Amt = New Amt(mContact.Proveidor.DefaultCur)
                .Cfp = mContact.Proveidor.FormaDePago.Cod
            Else
                .Cod = Pnd.Codis.Deutor
                .Cta = oPlan.Cta(DTOPgcPlan.ctas.clients)
                .Amt = maxisrvr.DefaultAmt
                .Cfp = mContact.Client.FormaDePago.Cod
            End If
        End With

        Dim oFrm As New Frm_Contact_Pnd(oPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub ExcelToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles ExcelToolStripButton.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoStatus
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case oRow.Cells(Cols.Status).Value
                    Case Pnd.StatusCod.pendent
                        e.Value = My.Resources.empty
                    Case Pnd.StatusCod.enCirculacio
                        e.Value = My.Resources.candau
                    Case 98
                        e.Value = My.Resources.pirata_rojo
                    Case 99
                        e.Value = My.Resources.cyc
                    Case 101
                        e.Value = My.Resources.DollarBlue
                    Case 102
                        e.Value = My.Resources.DollarOrange2
                    Case Else
                        e.Value = My.Resources.empty
                End Select
            Case Cols.IcoCuadra
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case oRow.Cells(Cols.Cuadra).Value
                    Case "1"
                        e.Value = My.Resources.warn
                    Case Else
                        e.Value = My.Resources.empty
                End Select
            Case Cols.Eur

        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowCurrentItm()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)

        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Eur

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()
        Calcula()
        CheckSaldos()

        If DataGridView1.Rows.Count >= iFirstRow And DataGridView1.Rows.Count > 0 Then
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

        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then

            Select Case CurrentLineMode(oRow)
                Case LineModes.Csb
                    Dim oMenu_Csb As New Menu_Csb(CurrentCsb)
                    AddHandler oMenu_Csb.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Csb.Range)
                Case LineModes.Pnd
                    Dim oMenu_Pnd As New Menu_Pnd(CurrentPnds)
                    AddHandler oMenu_Pnd.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Pnd.Range)
                Case LineModes.Impagat
                    Dim oMenu_Impagat As New Menu_Impagat(CurrentImpagat)
                    AddHandler oMenu_Impagat.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Impagat.Range)
                Case LineModes.Insolvencia
                Case LineModes.TranfPrevia, LineModes.Reembols
                    Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                    Dim oAlb As New Alb(oGuid)
                    Dim oMenu_Alb As New Menu_Alb(oAlb)
                    AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Alb.Range)
            End Select
        End If



        Dim oMenuItem As New ToolStripMenuItem("afegir", My.Resources.clip, AddressOf AddNew)
        oContextMenu.Items.Add(oMenuItem)

        oContextMenu.Items.Add(mMenuInclouSaldats)
        oContextMenu.Items.Add(mMenuCapDAny)

        oMenuItem = New ToolStripMenuItem("Excel", My.Resources.Excel, AddressOf DoExcel)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequest)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub AddNew()
        Dim oPnd As New Pnd()
        With oPnd
            .Contact = mContact
            .Fch = Today
            .Vto = Today
            .Amt = New maxisrvr.Amt(0, MaxiSrvr.Cur.Eur, 0)
        End With

        Dim oFrm As New Frm_Contact_Pnd(oPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DoExcel()
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub
End Class
