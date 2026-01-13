
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Xl_Ccd_Extracte
    Private mCcd As MaxiSrvr.Ccd
    Private mActType As PgcCta.Acts
    Private mShowDivisas As Boolean = False
    Private mMesDeDuesDivisas As Boolean = False
    Private mShadowToDate As Date = Date.MinValue
    Private mCur As DTOCur
    Private mDs As DataSet
    Private mAEB43LastFch As Date = Date.MinValue
    Private mSelectionMode As Modes
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event NavigateYear(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Enum Modes
        NotSet
        ForSelection
        ForBrowsing
    End Enum

    Private Enum Cols
        Pdf
        Ico
        Assentament
        Data
        Concepte
        DivDeb
        DivHab
        DivSdo
        EurDeb
        EurHab
        EurSdo
        Ccd
        Cur
        Guid
        AEB43Fch
    End Enum

    Public WriteOnly Property ShadowToDate() As Date
        Set(ByVal value As Date)
            mShadowToDate = value
        End Set
    End Property

    Public WriteOnly Property Ccd() As MaxiSrvr.Ccd
        Set(ByVal Value As MaxiSrvr.Ccd)
            mCcd = Value
            Dim sTitle As String = mCcd.Cta.FullNom
            If mCcd.Contact IsNot Nothing Then
                sTitle = sTitle & " " & mCcd.Contact.Clx
                If mCcd.Cta.Cod = DTOPgcPlan.ctas.bancs Then
                    'Dim oBanc As Banc = MaxiSrvr.Banc.FromNum(mCcd.Contact.Emp, mCcd.Contact.Id)
                    'mAEB43LastFch = oBanc.Q43LastFchValidated
                End If
            End If
            Me.Text = sTitle
            EnableYearNavigation()
            mActType = mCcd.Cta.Act


            'If Not mCcd.Cuadra Then
            'ToolStripButtonPnd.Image = My.Resources.warn
            'End If
            LoadGrid()
        End Set
    End Property

    Public WriteOnly Property SelectionMode() As Modes
        Set(ByVal value As Modes)
            mSelectionMode = value
        End Set
    End Property

    Private Sub EnableYearNavigation()
        ToolStripButtonPreviousYear.Enabled = False
        ToolStripButtonNextYear.Enabled = False
        Select Case mCcd.YearStat
            Case MaxiSrvr.Ccd.YearStats.IsYearInBetween
                ToolStripButtonPreviousYear.Enabled = True
                ToolStripButtonNextYear.Enabled = True
            Case MaxiSrvr.Ccd.YearStats.IsFirstYear
                ToolStripButtonNextYear.Enabled = True
            Case MaxiSrvr.Ccd.YearStats.IsLastYear
                ToolStripButtonPreviousYear.Enabled = True
        End Select
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Assentament

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


    Private Sub LoadGrid()
        Dim SQL As String = "SELECT BF.MIME AS PDF, " _
        & "0 AS ICO, " _
        & "CCA.cca, " _
        & "CCA.fch, " _
        & "(CASE WHEN PND.FRA IS NULL THEN CCA.TXT ELSE CCA.TXT+' fra.'+PND.FRA END) AS TXT, " _
        & "(CASE WHEN DH = 1 THEN CCB.PTS ELSE 0 END) AS DIVDEB, " _
        & "(CASE WHEN DH = 2 THEN CCB.PTS ELSE 0 END) AS DIVHAB, " _
        & "CAST (0 AS MONEY) AS DIVSDO, " _
        & "(CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE 0 END) AS EURDEB, " _
        & "(CASE WHEN CCB.DH = 2 THEN CCB.EUR ELSE 0 END) AS EURHAB, " _
        & "CAST (0 AS MONEY) AS EURSDO, " _
        & "CCA.CCD, CCB.CUR, CCA.GUID "

        If mAEB43LastFch > Date.MinValue Then
            'SQL = SQL & ", Q43.FCH "
        End If

        SQL = SQL & "FROM CCB INNER JOIN " _
        & "CCA ON Ccb.CcaGuid = Cca.Guid LEFT OUTER JOIN " _
        & "PND ON CCB.PND=PND.ID "

        SQL = SQL & "LEFT OUTER JOIN BIGFILESRC BS ON CCA.GUID=BS.GUID AND BS.SRC=" & CInt(DTODocFile.Cods.Assentament).ToString & " LEFT OUTER JOIN " _
            & "BIGFILE BF ON BS.BIGFILE=BF.GUID "

        'If mAEB43LastFch > Date.MinValue Then
        'Dim sLastFch As String = Format(mAEB43LastFch, "yyyyMMdd")
        'SQL = SQL & " LEFT OUTER JOIN " _
        '& "(SELECT EMP, BANC, EUR, MIN(FCHOPERACIO) AS FCH FROM AEB43 WHERE FCHOPERACIO > '" & sLastFch & "' GROUP BY EMP, BANC, EUR) AS Q43 ON Q43.EMP=CCB.EMP AND Q43.BANC=CCB.CLI AND Q43.EUR=(CASE WHEN CCB.DH=1 THEN CCB.EUR ELSE -CCB.EUR END) AND CCB.FCH > '" & sLastFch & "' "
        'End If

        SQL = SQL & "WHERE CCA.YEA =@YEA And CCB.CtaGuid LIKE @CtaGuid "

        If mCcd.Contact Is Nothing Then
            SQL = SQL & "AND CCA.Emp=" & App.Current.Emp.Id & " AND CCB.ContactGuid IS NULL "
        Else
            SQL = SQL & "AND CCB.ContactGuid = '" & mCcd.Contact.Guid.ToString & "' "

        End If
        SQL = SQL & "ORDER BY CCA.FCH, CCA.CCD, CCA.CDN, TXT, CCA.CCA"

        mDs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@YEA", mCcd.Yea, "@CtaGuid", mCcd.Cta.Guid.ToString)
        Dim oTb As DataTable = mDs.Tables(0)

        Dim oRow As DataRow
        Dim DblDeb As Decimal
        Dim DblHab As Decimal
        Dim DblDivSdo As Decimal
        Dim DblEurSdo As Decimal
        Dim EurId As String = DTOCur.Eur.Tag
        Dim CurId As String = ""
        Dim iFirstRow As Integer = 0
        Dim i As Integer
        For i = 0 To oTb.Rows.Count - 1
            oRow = oTb.Rows(i)

            If mShadowToDate > Date.MinValue Then
                If oRow("FCH") > mShadowToDate Then
                Else
                    iFirstRow = i
                End If
            End If

            Select Case oRow("CUR").ToString
                Case EurId
                Case CurId
                Case Else
                    If CurId = "" Then
                        mShowDivisas = True
                        CurId = oRow("CUR").ToString
                        mCur = BLLApp.GetCur(CurId)
                    Else
                        mMesDeDuesDivisas = True
                    End If
            End Select

            If mShowDivisas And oRow("CUR") = CurId Then
                DblDeb = oRow("DIVDEB")
                DblHab = oRow("DIVHAB")
                Select Case mActType
                    Case MaxiSrvr.PgcCta.Acts.deutora
                        DblDivSdo = DblDivSdo + DblDeb - DblHab
                    Case MaxiSrvr.PgcCta.Acts.creditora
                        DblDivSdo = DblDivSdo + DblHab - DblDeb
                End Select
            End If
            oRow(Cols.DivSdo) = DblDivSdo

            DblDeb = oRow("EURDEB")
            DblHab = oRow("EURHAB")
            Select Case mActType
                Case MaxiSrvr.PgcCta.Acts.deutora
                    DblEurSdo = DblEurSdo + DblDeb - DblHab
                Case MaxiSrvr.PgcCta.Acts.creditora
                    DblEurSdo = DblEurSdo + DblHab - DblDeb
            End Select
            oRow(Cols.EurSdo) = DblEurSdo
        Next

        'afegeix columna pdf
        oTb.Columns.RemoveAt(Cols.Ico)
        Dim oCol As DataColumn = oTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = mDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = True

            If iFirstRow > 0 Then
                .FirstDisplayedScrollingRowIndex() = iFirstRow
            End If

            With .Columns(Cols.Assentament)
                .HeaderText = "registre"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Data)
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
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Concepte)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            If mShowDivisas Then
                'Me.Width = FORMSHORTWIDTH + 90 * 3
                With .Columns(Cols.DivDeb)
                    .HeaderText = "debe"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 90
                    .DefaultCellStyle.BackColor = Color.LightYellow
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols.DivHab)
                    .HeaderText = "haber"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 90
                    .DefaultCellStyle.BackColor = Color.LightYellow
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With

                If mMesDeDuesDivisas Then
                    'hi ha mes de una divises diferents de l'Euro
                    With .Columns(Cols.DivSdo)
                        .Visible = False
                    End With
                Else
                    With .Columns(Cols.DivSdo)
                        .HeaderText = "saldo"
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                        .Width = 90
                        .DefaultCellStyle.BackColor = Color.LightYellow
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    End With
                End If
            Else
                With .Columns(Cols.DivDeb)
                    .Visible = False
                End With
                With .Columns(Cols.DivHab)
                    .Visible = False
                End With
                With .Columns(Cols.DivSdo)
                    .Visible = False
                End With
            End If

            With .Columns(Cols.EurDeb)
                .HeaderText = "debe"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.EurHab)
                .HeaderText = "haber"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.EurSdo)
                .HeaderText = "saldo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Ccd)
                .Visible = False
            End With
            With .Columns(Cols.Cur)
                .Visible = False
            End With
            With .Columns(Cols.Guid)
                .Visible = False
            End With
            If mAEB43LastFch > Date.MinValue Then
                'With .Columns(Cols.AEB43Fch)
                '.Visible = False
                'End With
            End If
        End With
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If IsDBNull(oRow.Cells(Cols.Pdf).Value) Then
                    e.Value = My.Resources.empty
                Else
                    Dim oMime As DTOEnums.MimeCods = CType(oRow.Cells(Cols.Pdf).Value, DTOEnums.MimeCods)
                    Dim oIco As Image = root.GetIcoFromMime(oMime)
                    e.Value = oIco
                End If
                'If oRow.Cells(Cols.Pdf).Value = 1 Then
                ' e.Value = My.Resources.pdf
                'Else
                'e.Value = My.Resources.empty
                'End If
            Case Cols.Data
                'If mAEB43LastFch > Date.MinValue Then
                'Dim DtFch As Date = e.Value
                'If DtFch > mAEB43LastFch Then
                ' Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                ' If IsDBNull(oRow.Cells(Cols.AEB43Fch).Value) Then
                ' e.CellStyle.BackColor = Color.LightSalmon
                ' Else
                ' If CDate(oRow.Cells(Cols.AEB43Fch).Value) <> DtFch Then
                ' e.CellStyle.BackColor = Color.LightYellow
                ' End If
                ' End If
                ' End If
                ' End If

            Case Cols.DivDeb, Cols.DivHab, Cols.DivSdo
                If mShowDivisas Then
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim sCur As String = oRow.Cells(Cols.Cur).Value
                    Select Case sCur
                        Case "EUR"
                            e.Value = Format(CDbl(e.Value), "#,###.00 €;-#,###.00 €;#")
                        Case "GBP"
                            e.Value = Format(CDbl(e.Value), "£ #,###.00;£ -#,###.00;#")
                        Case "USD"
                            e.Value = Format(CDbl(e.Value), "$ #,###.00;$ -#,###.00;#")
                        Case Else
                            e.Value = Format(CDbl(e.Value), "#,###.00 " & sCur & ";-#,###.00 " & sCur & ";#")
                    End Select
                End If
        End Select
    End Sub

    Private Function CurrentCca() As Cca
        Dim oCca As Cca = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            oCca = New Cca(oGuid)
        End If
        Return oCca
    End Function

    Private Function CurrentCcas() As Ccas
        Dim retval As New Ccas

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                Dim oCca As Cca = New Cca(oGuid)
                retval.Add(oCca)
            Next
        Else
            Dim oCca As Cca = CurrentCca()
            If oCca IsNot Nothing Then
                retval.Add(oCca)
            End If
        End If
        Return retval
    End Function


    Private Sub DoCuadra(sender As Object, e As System.EventArgs)
        Dim SQL As String = "SELECT CCA.cca, CCB.fch, CCA.txt, (CASE WHEN DH = 1 THEN CCB.EUR ELSE -CCB.EUR END) AS EUR, 0 AS SDO, Ccb.Pnd, 0 as DEL " _
        & "FROM CCB INNER JOIN " _
        & "CCA ON Ccb.CcaGuid = Cca.Guid " _
        & "WHERE  CCA.ccd<>1 AND " _
        & "CCB.cli = " & mCcd.Contact.Id & " AND " _
        & "CCB.Emp = " & mCcd.Contact.Emp.Id & " AND " _
        & "CCB.cta LIKE '" & mCcd.Cta.Id & "' "

        Dim BlFinsCapDAny As Boolean = Today.Year > mCcd.Yea
        If BlFinsCapDAny Then
            SQL = SQL & " AND CCB.YEA<=" & mCcd.Yea.ToString & " "
        End If

        SQL = SQL & "ORDER BY CCA.fch, CCA.ccd, CCA.cdn"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRows As DataRowCollection = oTb.Rows
        Dim oRow As DataRow

        'omple els saldos
        Dim DcSdo As Decimal = 0
        For Each oRow In oRows
            DcSdo += CDec(oRow("EUR"))
            oRow("SDO") = DcSdo
        Next

        'Busca l'ultim saldo 0 i esborra les linies anteriors
        Dim Saldat As Boolean
        For i As Integer = oRows.Count - 1 To 0 Step -1
            oRow = oRows(i)
            If Saldat Then
                oRows.RemoveAt(i)
            Else
                Saldat = (oRow("SDO") = 0)
            End If
        Next

        'marca per eliminar les que coincideixen
        For i As Integer = 0 To oRows.Count - 1
            If oRows(i)("DEL") = 0 Then
                For j As Integer = i + 1 To oRows.Count - 2
                    If oRows(j)("DEL") = 0 Then
                        If oRows(i)("EUR") = -oRows(j)("EUR") Then
                            oRows(i)("DEL") = 1
                            oRows(j)("DEL") = 1
                            Exit For
                        End If
                    End If
                Next
            End If
        Next

        'redacta excel
        Dim oExcel As New MatExcel
        For Each oRow In oRows
            If oRow("DEL") = 0 Then
                Dim oCells As New ArrayList
                oCells.Add(oRow("CCA").ToString)
                oCells.Add(CDate(oRow("FCH")).ToShortDateString)
                oCells.Add(oRow("TXT").ToString)
                Dim DcEur As Decimal = CDec(oRow("EUR"))
                If DcEur > 0 Then
                    oCells.Add(Math.Abs(DcEur))
                    oCells.Add(0)
                Else
                    oCells.Add(0)
                    oCells.Add(Math.Abs(DcEur))
                End If
                oCells.Add(CDec(oRow("SDO")))
                oExcel.AddRow(oCells)
            End If
        Next

        oExcel.Application.Visible = True
    End Sub

    Private Sub DoExcel(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")


        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        Dim oRow As DataGridViewRow
        Dim i As Integer
        Dim VGap As Integer = 5
        Dim oTb As DataTable = mDs.Tables(0)
        Dim sFormula As String

        Select Case mCcd.Cta.Act
            Case PgcCta.Acts.deutora
                sFormula = "=R[-1]C+RC[-2]-RC[-1]"
                'sFormula = "=F[-1]C+FC[-2]-FC[-1]"
            Case Else
                sFormula = "=R[-1]C+RC[-1]-RC[-2]"
                'sFormula = "=F[-1]C+FC[-1]-FC[-2]"
        End Select

        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iLastRow As Integer


        If DataGridView1.SelectedRows.Count > 1 Then
            Dim DblSdo As Decimal = 0

            iFirstRow = DataGridView1.SelectedRows(0).Index
            iLastRow = DataGridView1.SelectedRows(DataGridView1.SelectedRows.Count - 1).Index
            If iFirstRow > iLastRow Then
                Dim iTmp As Integer = iFirstRow
                iFirstRow = iLastRow
                iLastRow = iTmp
            End If

            Dim oAct As PgcCta.Acts = mCcd.Cta.Act
            For i = 0 To iFirstRow - 1
                oRow = DataGridView1.Rows(i)
                Select Case oAct
                    Case PgcCta.Acts.deutora
                        DblSdo = DblSdo + oRow.Cells(Cols.DivDeb).Value - oRow.Cells(Cols.DivHab).Value
                    Case Else
                        DblSdo = DblSdo + oRow.Cells(Cols.DivHab).Value - oRow.Cells(Cols.DivDeb).Value
                End Select
            Next

            j = VGap
            If DblSdo <> 0 Then
                oSheet.Cells(j, 3) = "Suma anterior"
                oSheet.Cells(j, 6) = DblSdo
                j = j + 1
            End If

        Else
            j = VGap
            iFirstRow = 0
            iLastRow = mDs.Tables(0).Rows.Count - 1
        End If

        Dim RowFirst As Integer = j

        Dim sTxt As String = ""
        Dim sUrl As String = ""
        Dim oRange As Excel.Range
        Dim oGuid As System.Guid
        For i = iFirstRow To iLastRow
            oRow = DataGridView1.Rows(i)
            oSheet.Cells(j, 1) = oRow.Cells(Cols.Assentament).Value
            oSheet.Cells(j, 2) = oRow.Cells(Cols.Data).Value
            sTxt = oRow.Cells(Cols.Concepte).Value
            Try
                oGuid = oRow.Cells(Cols.Guid).Value
                Dim oCca As New Cca(oGuid)
                If oCca.DocExists Then
                    sUrl = BLL.BLLDocFile.DownloadUrl(oCca.DocFile, True)
                    oRange = oSheet.Cells(j, 3)
                    oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)
                Else
                    oSheet.Cells(j, 3) = sTxt
                End If
                'oRange.FormulaR1C1 = "=HYPERLINK(" & Chr(34) & sUrl & Chr(34) & ";" & Chr(34) & sTxt & Chr(34) & ")"
            Catch ex As Exception
                oSheet.Cells(j, 3) = sTxt
            End Try
            oSheet.Cells(j, 4) = oRow.Cells(Cols.DivDeb).Value
            oSheet.Cells(j, 5) = oRow.Cells(Cols.DivHab).Value
            j = j + 1
        Next

        Dim ColSdo As Integer = 6
        Dim RowLast As Integer = j - 1
        Dim oFirstCell As Excel.Range = oSheet.Cells(RowFirst, ColSdo)
        Dim oLastCell As Excel.Range = oSheet.Cells(RowLast, ColSdo)
        Dim oRangeSdo As Excel.Range = oSheet.Range(oFirstCell, oLastCell)

        Try

            'oRangeSdo.FormulaR1C1Local = sFormula
            oRangeSdo.FormulaR1C1 = sFormula
        Catch ex As Exception
            sFormula = sFormula.Replace("R", "F")
            'sFormula = sFormula.Replace("F", "R")
            oRangeSdo.FormulaR1C1 = sFormula
        End Try

        With oWb.Styles.Add(Name:="Epigrafs")
            .Font.ColorIndex = 2
            .Font.Bold = True
            .Interior.ColorIndex = 15
            .Interior.Pattern = 1
        End With

        With oWb.Styles.Add(Name:="Extracte")
            .NumberFormatLocal = "#,##0.00;#,##0.00;"
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        End With

        oSheet.Columns("D:F").Style = "Extracte"

        oSheet.Columns("A:A").ColumnWidth = 7
        oSheet.Columns("A:A").HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        oSheet.Columns("B:B").ColumnWidth = 7
        oSheet.Columns("B:B").NumberFormatLocal = "dd/mm/aa"
        oSheet.Columns("B:B").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        oSheet.Columns("C:C").ColumnWidth = 30
        oSheet.Range("D1:F1").HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        oSheet.Columns("D:F").NumberFormat = "#,##0.00;-#,##0.00;#"

        i = 1
        oSheet.Cells(i, 1) = mCcd.Cta.FullNom & " " & mCcd.Contact.Clx
        oSheet.Cells(i, 1).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

        i = 3
        oSheet.Cells(i, 1) = "Registre"
        oSheet.Cells(i, 2) = "data"
        oSheet.Cells(i, 3) = "Concepte"
        oSheet.Cells(i, 4) = "Debe"
        oSheet.Cells(i, 5) = "Haber"
        oSheet.Cells(i, 6) = "Saldo"
        oSheet.Range("A" & i & ":F" & i).Style = "Epigrafs"
        oSheet.Cells(i, 4).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        oSheet.Cells(i, 5).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        oSheet.Cells(i, 6).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight

        oSheet.Cells.Font.Size = 9

        oApp.Visible = True

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

    End Sub






    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        If DataGridView1.SelectedRows.Count > 1 Then
            Dim DcTot As Decimal = 0
            Dim DcDeb As Decimal = 0
            Dim DcHab As Decimal = 0
            For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
                DcDeb = oRow.Cells(Cols.EurDeb).Value
                DcHab = oRow.Cells(Cols.EurHab).Value
                If mCcd.Cta.Act = PgcCta.Acts.deutora Then
                    DcTot += DcDeb - DcHab
                Else
                    DcTot += DcHab - DcDeb
                End If
            Next
            oMenuItem = New ToolStripMenuItem("total " & Format(DcTot, "#,###.00 €"))
            oContextMenu.Items.Add(oMenuItem)
        End If

        Try
            Dim oCcas As Ccas = CurrentCcas()

            If oCcas.Count > 0 Then
                Dim oMenu_Cca As New Menu_Cca(oCcas)
                AddHandler oMenu_Cca.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Cca.Range)
            End If

            oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf DoExcel)
            oContextMenu.Items.Add("Cuadra", Nothing, AddressOf DoCuadra)

            If oCcas.Count = 1 And mCcd.Contact IsNot Nothing Then
                oMenuItem = New ToolStripMenuItem("pendents")
                oContextMenu.Items.Add(oMenuItem)
                Dim oCca As Cca = oCcas(0)
                Dim oPnds As Pnds = Pnds.FromCcb(oCca, mCcd.Cta, mCcd.Contact)
                If oPnds.Count = 0 Then
                    Dim oSubMenuitem As New ToolStripMenuItem("afegir", Nothing, AddressOf Do_AddPnd)
                    oMenuItem.DropDownItems.Add(oSubMenuitem)
                Else
                    For Each oPnd As Pnd In oPnds
                        Dim sText As String = FormatPnd(oPnd)
                        Dim oSubMenuitem As New ToolStripMenuItem(sText, Nothing, AddressOf Do_ZoomPnd)
                        oMenuItem.DropDownItems.Add(oSubMenuitem)
                    Next
                End If
            End If

            oMenuItem = New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequest)
            oContextMenu.Items.Add(oMenuItem)
        Catch ex As Exception
            BLL.MailHelper.MailErr("error al contextmenu de Xl_ccdextracte" & vbCrLf & ex.Message & vbCrLf & ex.StackTrace)
        End Try

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function FormatPnd(oPnd As Pnd) As String
        Dim retval As String = oPnd.Vto.ToShortDateString & " " & oPnd.Amt.CurFormatted
        Return retval
    End Function

    Private Sub Do_AddPnd(sender As Object, e As System.EventArgs)
        Dim oCca As Cca = CurrentCca()
        Dim oAmt As DTOAmt = Nothing
        For Each oCcb As Ccb In oCca.ccbs
            If oCcb.Cta.Equals(mCcd.Cta) And oCcb.Contact.Equals(oCcb.Contact) Then
                oAmt = oCcb.Amt
                Exit For
            End If
        Next
        Dim oPnd As New Pnd()
        With oPnd
            .Contact = mCcd.Contact
            .Yef = oCca.yea
            .Cta = mCcd.Cta
            .Fch = oCca.fch
            .Vto = Today
            .Amt = oAmt
            .Cca = oCca
            .Status = Pnd.StatusCod.pendent
            .Fpg = "afegit a ma per " & Current.Login & " a " & Today.ToShortDateString
            .Cfp = DTOCustomer.FormasDePagament.aNegociar
            .Cod = IIf(mCcd.Cta.Act = PgcCta.Acts.deutora, Pnd.Codis.Deutor, Pnd.Codis.Creditor)
            .FraNum = oCca.Cdn

            Select Case .Cod
                Case Pnd.Codis.Creditor
                    .Cfp = (New Proveidor(.Contact.Guid)).FormaDePago.Cod
                Case Pnd.Codis.Deutor
                    .Cfp = (New Client(.Contact.Guid)).FormaDePago.Cod
            End Select
        End With
        Dim oFrm As New Frm_Contact_Pnd(oPnd.ToDTO)
        AddHandler oFrm.AfterUpdate, AddressOf onPndUpdated
        oFrm.Show()
    End Sub

    Private Sub onPndUpdated(sender As Object, e As System.EventArgs)
        SetContextMenu()
    End Sub

    Private Sub Do_ZoomPnd(sender As Object, e As System.EventArgs)
        Dim oMenuitem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim oPnds As Pnds = Pnds.FromCcb(CurrentCca, mCcd.Cta, mCcd.Contact)
        For Each oPnd As Pnd In oPnds
            Dim sTest As String = FormatPnd(oPnd)
            If sTest = oMenuitem.Text Then
                Dim oFrm As New Frm_Contact_Pnd(oPnd.ToDTO)
                AddHandler oFrm.AfterUpdate, AddressOf onPndUpdated
                oFrm.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oCca As Cca = CurrentCca()
        If oCca IsNot Nothing Then
            Select Case mSelectionMode
                Case Modes.ForSelection
                    RaiseEvent AfterSelect(oCca, EventArgs.Empty)
                Case Else
                    Dim oFrm As New Frm_Cca(oCca)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.show()
            End Select
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

#Region "DragDrop"

    Private mLastMouseDownRectangle As System.Drawing.Rectangle

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
            'PictureBox1.BackColor = Color.SeaShell
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            'PictureBox1.BackColor = Color.LemonChiffon
            '    or none of the above
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragOver
        Dim oPoint = DataGridView1.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridView1.CurrentCell = oclickedCell
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        Dim oDocFiles As New List(Of DTODocFile)
        Dim exs as New List(Of exception)
        Dim oTargetCell As DataGridViewCell = Nothing
        If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
            Dim oCca As Cca = CurrentCca()
            Dim oDocFile As DTODocFile = oDocFiles.First
            Dim rc As MsgBoxResult = MsgBox("importem fitxer " & BLL_DocFile.FileNameOrDefault(oDocFile) & vbCrLf & BLL_DocFile.Features(oDocFile) & vbCrLf & "al assentament " & oCca.Id & " del " & oCca.fch.ToShortDateString & vbCrLf & oCca.Txt & "?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                oCca.DocFile = oDocFile
                If oCca.Update( exs) Then
                    RefreshRequest(Me, MatEventArgs.Empty)
                Else
                    MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If
            Else
                MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                mLastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not mLastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(mLastMouseDownRectangle.X, mLastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    DataGridView1.CurrentCell = sender.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                    Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
                    Dim oCca As Cca = CurrentCca()
                    sender.DoDragDrop(oCca, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub

#End Region


    Private Sub ToolStripButtonPreviousYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonPreviousYear.Click
        Dim oCcd As Ccd = mCcd.PreviousYearCcd
        RaiseEvent NavigateYear(oCcd, EventArgs.Empty)
    End Sub

    Private Sub ToolStripButtonNextYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonNextYear.Click
        Dim oCcd As Ccd = mCcd.NextYearCcd
        RaiseEvent NavigateYear(oCcd, EventArgs.Empty)
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        If mShadowToDate > Date.MinValue Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim DtFch As Date = oRow.Cells(Cols.Data).Value
            If DtFch > mShadowToDate Then
            Else
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            End If
        End If

    End Sub

    Private Sub DataGridView1_CellToolTipTextNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles DataGridView1.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Select Case e.ColumnIndex
                Case Cols.Data
                    If mAEB43LastFch > Date.MinValue Then

                        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                        If oRow.Cells(Cols.Data).Value > mAEB43LastFch Then

                            'If IsDBNull(oRow.Cells(Cols.AEB43Fch).Value) Then
                            'e.ToolTipText = "aquest assentament no hi surt a l'extracte del banc"
                            'Else
                            'e.ToolTipText = "al banc hi surt en data " & CDate(oRow.Cells(Cols.AEB43Fch).Value).ToShortDateString
                            'End If
                        End If
                    End If
            End Select
        End If
    End Sub
End Class
