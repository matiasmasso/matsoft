Imports System.Data.SqlClient

Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_Bal
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mLang As DTOLang = BLL.BLLApp.Lang
    Private IsCleanTab(20) As Boolean
    Private mBal As Balance
    Private mAllowEvents As Boolean

    Private Enum Tabs
        Actiu
        Passiu
        Explotacio
        CashFlow
        Errors
    End Enum

    Private Enum LinCods
        NotSet
        Cta
        Epg
        Grup
        Sumatori
    End Enum

    Private Enum Cols
        LinCod
        SortKey
        Plan
        Id
        Level
        Nom
        NomCat
        NomEng
        Ye2
        Ye1
        Suma
    End Enum

    Private Enum ColsSS
        PlanId
        PlanNom
        Cta
        Esp
        Cat
        Eng
        Deb
        Hab
        SdoDeb
        SdoHab
    End Enum

    Private Sub Frm_Bal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sFch As String = GetSetting("MAT.NET", "Accounts", "FchLastBalance")
        Dim DtFch As Date
        If IsDate(sFch) Then
            DtFch = CDate(sFch)
        Else
            DtFch = Today
            SaveSetting("MAT.NET", "Accounts", "FchLastBalance", DtFch.ToShortDateString)
        End If
        'CheckErrs()
        'LoadActiu()
    End Sub



    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetFch()
        CheckPgcCta()
        LoadGrid()
        CheckEpgs()
    End Sub



    Private Sub LoadGrid()
        Cursor = Cursors.WaitCursor

        mBal = New Balance(mEmp, CurrentDoc, CurrentFch, mLang)
        Dim oDs As DataSet = mBal.Dataset(CurrentDoc)
        Dim oTb As DataTable = oDs.Tables(0)

        With CurrentGrid()
            With .RowTemplate
                .Height = DataGridViewActiu.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.LinCod)
                .Visible = False
            End With
            With .Columns(Cols.SortKey)
                .Visible = False
            End With
            With .Columns(Cols.Plan)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Level)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "Concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.NomCat)
                .Visible = False
            End With
            With .Columns(Cols.NomEng)
                .Visible = False
            End With
            With .Columns(Cols.Ye2)
                .HeaderText = CurrentYea()
                .Width = 120
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Ye1)
                .HeaderText = CurrentYea() - 1
                .Width = 120
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Suma)
                .Visible = False
            End With
        End With
        IsCleanTab(CurrentTab) = True
        Cursor = Cursors.Default
        mAllowEvents = True
    End Sub


    Private Function CurrentTab() As Tabs
        Dim oTab As Tabs = TabControl1.SelectedIndex
        Return oTab
    End Function

    Private Function CurrentYea() As Integer
        Return CurrentFch.Year
    End Function

    Private Function CurrentFch() As Date
        Return DateTimePicker1.Value
    End Function

    Private Function CurrentDoc() As Balance.DocCods
        Dim oDoc As Balance.DocCods = Nothing
        Select Case CurrentTab()
            Case Tabs.Actiu
                oDoc = Balance.DocCods.Actiu
            Case Tabs.Passiu
                oDoc = Balance.DocCods.Passiu
            Case Tabs.Explotacio
                oDoc = Balance.DocCods.Explotacio
        End Select
        Return oDoc
    End Function

    Private Function CurrentBal() As PgcEpg.BalCods
        Dim oBal As PgcEpg.BalCods = PgcEpg.BalCods.NotSet
        Select Case CurrentDoc()
            Case Balance.DocCods.Actiu, Balance.DocCods.Passiu
                oBal = PgcEpg.BalCods.Balanç
            Case Balance.DocCods.Explotacio
                oBal = PgcEpg.BalCods.Explotacio
        End Select
        Return oBal
    End Function

    Private Function CurrentAct() As PgcCta.Acts
        Dim oAct As PgcCta.Acts = PgcCta.Acts.notset
        Select Case CurrentDoc()
            Case Balance.DocCods.Actiu
                oAct = PgcCta.Acts.deutora
            Case Balance.DocCods.Passiu
                oAct = PgcCta.Acts.creditora
        End Select
        Return oAct
    End Function

    Private Function CurrentGrid() As DataGridView
        Dim oGrid As DataGridView = Nothing
        Select Case CurrentTab()
            Case Tabs.Actiu
                oGrid = DataGridViewActiu
            Case Tabs.Passiu
                oGrid = DataGridViewPassiu
            Case Tabs.Explotacio
                oGrid = DataGridViewExplotacio
        End Select
        Return oGrid
    End Function

    Private Function CurrentRow() As DataGridViewRow
        Dim oGrid As DataGridView = CurrentGrid()
        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        Return oRow
    End Function


    Private Sub CheckErrs()
        CheckPgcCta()
        CheckEpgs()
    End Sub

    Private Sub CheckPgcCta()
        Dim SQL As String = "SELECT B.ctaGuid, C.Id " _
        & "FROM CCB AS B LEFT OUTER JOIN " _
        & "PGCCTA AS C ON B.ctaGuid = C.Guid " _
        & "WHERE B.Emp =" & mEmp.Id & " AND " _
        & "B.yea =" & CurrentYea() & " AND " _
        & "C.Id IS NULL " _
        & "GROUP BY B.CtaGuid, C.Id"

        Dim sErr As String = ""
        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Do While oDrd.Read
            If sErr = "" Then sErr = "els següents comptes no tenen descripció;" & vbCrLf
            sErr += oDrd("CTA").ToString & vbCrLf
        Loop
        oDrd.Close()
        If sErr > "" Then MsgBox(sErr)
    End Sub

    Private Sub CheckEpgs()
        Dim SQL As String = "SELECT B.cta " _
        & "FROM PGCEPG AS E RIGHT OUTER JOIN " _
        & "PGCEPGCTAS AS X ON E.Id = X.Epg RIGHT OUTER JOIN " _
        & "CCB AS B ON X.PgcPLan = B.PgcPlan AND B.cta LIKE X.Cta + '%' " _
        & "WHERE B.Emp =" & mEmp.Id & " AND " _
        & "B.yea =" & CurrentYea() & " AND " _
        & "E.Id IS NULL " _
        & "GROUP BY B.CTA " _
        & "ORDER BY B.CTA"

        Dim sErr As String = ""
        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Do While oDrd.Read
            If sErr = "" Then sErr = "els següents comptes no tenen descripció;" & vbCrLf
            sErr += oDrd("CTA").ToString & vbCrLf
        Loop
        oDrd.Close()
        If sErr > "" Then MsgBox(sErr)
    End Sub

    Private Sub DataGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles _
    DataGridViewActiu.CellFormatting, _
     DataGridViewPassiu.CellFormatting, _
      DataGridViewExplotacio.CellFormatting

        Dim oGrid As DataGridView = sender
        Select Case e.ColumnIndex
            Case Cols.Nom
                Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
                Dim iLevel As Integer = oRow.Cells(Cols.Level).Value
                Dim oLevel As New PgcEpgLevel(CurrentBal, iLevel)
                e.Value = mLang.Tradueix(oRow.Cells(Cols.Nom).Value, oRow.Cells(Cols.NomCat).Value, oRow.Cells(Cols.NomEng).Value)
                If oLevel.Indent > 0 Then
                    e.CellStyle.Padding = New System.Windows.Forms.Padding(oLevel.Indent * 8, 0, 0, 0)
                End If
                If oLevel.Level <> PgcEpgLevel.Levels.Compte Then e.CellStyle.BackColor = Color.FromArgb(240, 240, 240)
                If oLevel.FontBold Then
                    e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
                End If
            Case Cols.Ye1, Cols.Ye2
                Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
                Dim iLevel As Integer = oRow.Cells(Cols.Level).Value
                Dim oLevel As New PgcEpgLevel(CurrentBal, iLevel)

                Dim BlDisplayValues As Boolean = (oLevel.DisplayAmt = PgcEpgLevel.DisplayAmts.Always)
                If oRow.Cells(Cols.LinCod).Value <= 2 Then BlDisplayValues = True
                If Not BlDisplayValues Then
                    e.Value = 0
                End If
                If oLevel.Level = PgcEpgLevel.Levels.Compte Then
                    e.CellStyle.Padding = New System.Windows.Forms.Padding(0, 0, 42, 0)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(240, 240, 240)
                    e.CellStyle.ForeColor = Color.FromArgb(100, 100, 100)
                End If
        End Select
    End Sub

    Private Sub SetFch()
        Dim sFch As String = GetSetting("MAT.NET", "Accounts", "FchLastBalance")
        If IsDate(sFch) Then
            DateTimePicker1.Value = CDate(sFch)
        Else
            DateTimePicker1.Value = Today
            SaveSetting("MAT.NET", "Accounts", "FchLastBalance", DateTimePicker1.Value.ToShortDateString)
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        For i As Integer = 0 To TabControl1.TabCount - 1
            IsCleanTab(i) = False
        Next
        ButtonRefresca.Enabled = True
        DataGridViewActiu.BackgroundColor = Color.LightGray
    End Sub

    Private Sub ButtonRefresca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRefresca.Click
        ButtonRefresca.Enabled = False
        SaveSetting("MAT.NET", "Accounts", "FchLastBalance", DateTimePicker1.Value.ToShortDateString)
        LoadGrid()
        DataGridViewActiu.BackgroundColor = Color.White
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If Not IsCleanTab(CurrentTab) Then LoadGrid()
    End Sub

    Private Sub DataGridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DataGridViewActiu.DoubleClick, _
    DataGridViewPassiu.DoubleClick, _
    DataGridViewExplotacio.DoubleClick

        Dim oRow As DataGridViewRow = CurrentRow()
        Dim ItmId As Integer = oRow.Cells(Cols.Id).Value
        Dim oLinCod As LinCods = oRow.Cells(Cols.LinCod).Value

        If Not IsDBNull(oRow.Cells(Cols.Plan).Value) Then
            Dim oPlan As New PgcPlan(oRow.Cells(Cols.Plan).Value)
            Select Case oLinCod
                Case LinCods.Cta
                    Dim oCta As PgcCta = MaxiSrvr.PgcCta.FromNum(oPlan, ItmId.ToString)
                    ShowCce(oCta)
            End Select
        End If
    End Sub

    Private Sub ShowCce(ByVal oCta As PgcCta)
        Dim oCce As New Cce(mEmp, oCta, CurrentYea)
        Dim oFrm As New Frm_PgcCta_Old(oCce)
        oFrm.Show()
    End Sub

    Private Sub ToolStripButtonPdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonPdf.Click
        Dim oBal As New Balance(mEmp, Balance.DocCods.FullBook, CurrentFch, CurrentLang)
        root.ShowPdf(oBal.Pdf)
    End Sub

    Private Function CurrentLang() As DTOLang
        Dim oLang As New DTOLang(ComboBoxLang.SelectedItem.ToString)
        Return oLang
    End Function


    Private Sub ToolStripButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        GetExcel.Visible = True
    End Sub

    Public Function GetExcel() As Excel.Application
        Dim oLang As DTOLang = CurrentLang()
        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()

        AddExcelSheet(oWb, Balance.DocCods.SumasySaldos, "SUMES I SALDOS")
        AddExcelSheet(oWb, Balance.DocCods.Explotacio, "COMPTE DE RESULTATS")
        AddExcelSheet(oWb, Balance.DocCods.Passiu, "PASSIU")
        AddExcelSheet(oWb, Balance.DocCods.Actiu, "ACTIU")

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Return oApp
    End Function

    Private Sub AddExcelSheet(ByRef oWb As Excel.Workbook, ByVal oDoc As Balance.DocCods, ByVal sSheetName As String)
        Dim oLang As DTOLang = CurrentLang()
        Dim oBal As New Balance(mEmp, oDoc, CurrentFch, oLang)
        Dim oDs As DataSet = oBal.Dataset(oDoc)
        Dim oSheet As Excel.Worksheet = oWb.Sheets.Add()
        Dim oColsAutofit As Excel.Range = Nothing
        Dim oColsNum As Excel.Range = Nothing
        Dim i As Integer = 1

        Select Case oDoc
            Case Balance.DocCods.Actiu, Balance.DocCods.Passiu, Balance.DocCods.Explotacio
                For Each oRow As DataRow In oDs.Tables(0).Rows
                    If oRow(Cols.Ye2) <> 0 Then
                        Select Case CType(oRow(Cols.LinCod), LinCods)
                            Case LinCods.Cta
                                oSheet.Cells(i, 4) = oLang.Tradueix(oRow(Cols.Nom), oRow(Cols.NomCat), oRow(Cols.NomEng))
                                oSheet.Cells(i, 5) = oRow(Cols.Ye2)
                            Case LinCods.Epg
                                oSheet.Cells(i, 3) = oLang.Tradueix(oRow(Cols.Nom), oRow(Cols.NomCat), oRow(Cols.NomEng))
                                oSheet.Cells(i, 6) = oRow(Cols.Ye2)
                            Case LinCods.Grup
                                oSheet.Cells(i, 2) = oLang.Tradueix(oRow(Cols.Nom), oRow(Cols.NomCat), oRow(Cols.NomEng))
                                oSheet.Cells(i, 7) = oRow(Cols.Ye2)
                            Case LinCods.Sumatori
                                oSheet.Cells(i, 1) = oLang.Tradueix(oRow(Cols.Nom), oRow(Cols.NomCat), oRow(Cols.NomEng))
                                oSheet.Cells(i, 8) = oRow(Cols.Ye2)

                                'Dim oRange As New Excel.Range(oSheet.Cells(i, 1), oSheet.Cells(i, 8))
                                'oRange.Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous
                                'oRange.Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
                        End Select
                        i += 1
                    End If
                Next
                oColsAutofit = oSheet.Columns("D")
                oColsNum = oSheet.Columns("E:H")

            Case Balance.DocCods.SumasySaldos
                i = 1
                oSheet.Cells(i, 1) = "PLA"
                oSheet.Cells(i, 2) = "COMPTE"
                oSheet.Cells(i, 3) = "SUMES DEUTORES"
                oSheet.Cells(i, 4) = "SUMES CREDITORES"
                oSheet.Cells(i, 5) = "SALDOS DEUTORS"
                oSheet.Cells(i, 6) = "SALDOS CREDITORS"
                i += 2
                For Each oRow As DataRow In oDs.Tables(0).Rows
                    If oRow(Cols.Ye2) <> 0 Then
                        oSheet.Cells(i, 1) = oRow(ColsSS.PlanNom)
                        oSheet.Cells(i, 2) = oRow(ColsSS.Cta) & " " & mLang.Tradueix(oRow(ColsSS.Esp), oRow(ColsSS.Cat), oRow(ColsSS.Eng))
                        oSheet.Cells(i, 3) = oRow(ColsSS.Deb)
                        oSheet.Cells(i, 4) = oRow(ColsSS.Hab)
                        oSheet.Cells(i, 5) = oRow(ColsSS.SdoDeb)
                        oSheet.Cells(i, 6) = oRow(ColsSS.SdoHab)
                        i += 1
                    End If
                Next
                oColsAutofit = oSheet.Columns("B")
                oColsNum = oSheet.Columns("C:F")
        End Select
        With oSheet
            .Name = sSheetName & " " & CurrentYea()
            .Cells.Font.Size = 11
        End With
        oColsAutofit.AutoFit()
        oColsNum.NumberFormat = "#,###.00;-#,###.00;#"
        oColsNum.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight

    End Sub

    Private Sub SetContextMenu(ByVal oGrid As DataGridView)
        Dim oContextMenu As New ContextMenuStrip

        Dim oRow As DataGridViewRow = CurrentRow()
        If oRow IsNot Nothing Then

            Dim oLin As LinCods = oRow.Cells(Cols.LinCod).Value
            Select Case oLin
                Case LinCods.Cta
                    If Not IsDBNull(oRow.Cells(Cols.Plan).Value) Then
                        Dim iPlan As Integer = oRow.Cells(Cols.Plan).Value
                        Dim sCta As String = oRow.Cells(Cols.Id).Value
                        Dim oCta As PgcCta = MaxiSrvr.PgcCta.FromNum(New PgcPlan(iPlan), sCta)
                        Dim oCce As New Cce(mEmp, oCta, CurrentYea)

                        Dim oMenu_Cce As New Menu_Cce(oCce)
                        oContextMenu.Items.AddRange(oMenu_Cce.Range)
                    End If
            End Select
        End If

        oGrid.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DataGridViewExplotacio.SelectionChanged, _
    DataGridViewActiu.SelectionChanged, _
    DataGridViewPassiu.SelectionChanged

        SetContextMenu(sender)
    End Sub

    Private Sub ComboBoxLang_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxLang.SelectedIndexChanged
        If mAllowEvents Then
            mLang = New DTOLang(CType(ComboBoxLang.SelectedIndex + 1, DTOLang.Ids))
            LoadGrid()
        End If
    End Sub
End Class