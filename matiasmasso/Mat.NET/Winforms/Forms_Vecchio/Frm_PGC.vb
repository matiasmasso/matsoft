
Imports System.Data.SqlClient

Public Class Frm_PGC
    Private mAllowEvents As Boolean
    Private mTbEpg As DataTable
    Private mCleanTab(20) As Boolean

    Private Enum Tabs
        Ctas
        Balanç
        Explotacio
        CashFlow
    End Enum

    Private Enum ColsPlan
        Id
        Nom
    End Enum

    Private Enum ColsCta
        LinCod
        Id
        Nom
    End Enum

    Private Enum ColsEpg
        Id
        Nivel
        Nom
    End Enum

    Private Enum LinCods
        Grup
        Cta
    End Enum

    Private Enum Filtres
        None
        OnlyPgcCtas
        OnlyCcbs
    End Enum

    Private Sub Frm_PGC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Xl_Langs1.Lang = BLL.BLLSession.Current.Lang
        Refresca()
    End Sub

    Private Sub Refresca()
        mAllowEvents = False
        CheckOrfes()
        LoadPlans()
        ComboBoxFiltre.SelectedIndex = GetDefaultFiltre()
        LoadCtas()
        'LoadGridEpgs()
        mAllowEvents = True

    End Sub

    Private Sub CheckOrfes()
        Dim oArrayOrfes As ArrayList = PgcPlan.FromToday.CodisHorfes
        If oArrayOrfes.Count > 0 Then
            Dim s As String = "els següents codis no estan assignats a cap compte:" & vbCrLf
            For Each oCod as DTOPgcPlan.ctas In oArrayOrfes
                s = s & CInt(oCod) & ": " & oCod.ToString & vbCrLf
            Next
            MsgBox(s, MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

#Region "PGCPLANS"
    Private Sub ComboboxPlan_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPlan.DoubleClick
        If CurrentPlan() Is Nothing Then
            AddNewPlan()
        Else
            ZoomPlan()
        End If
    End Sub



    Private Sub ComboBoxPlan_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPlan.SelectedValueChanged
        If mAllowEvents Then
            SetPlanContextMenu()
            For i As Integer = 0 To TabControl1.TabPages.Count - 1
                mCleanTab(i) = False
            Next
            Select Case TabControl1.SelectedTab.Text
                Case TabPageCtas.Text
                    LoadCtas()
                Case TabPageBal.Text, TabPageExplot.Text, TabPageCFlow.Text
                    LoadGridEpgs()
            End Select
        End If
    End Sub

    Private Sub SetPlanContextMenu()
        If ComboBoxPlan.SelectedIndex = -1 Then
            PlanToolStripMenuItemZoom.Enabled = False
        Else
            PlanToolStripMenuItemZoom.Enabled = True
        End If
    End Sub

    Private Function CurrentPlan() As PgcPlan
        Dim oPlan As PgcPlan = Nothing
        Dim PlanId As Integer = ComboBoxPlan.SelectedValue
        oPlan = New PgcPlan(PlanId)
        Return oPlan
    End Function

    Private Sub PlanToolStripMenuItemZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PlanToolStripMenuItemZoom.Click
        ZoomPlan()
    End Sub

    Private Sub ZoomPlan()
        Dim oFrm As New Frm_PgcPlan_Old
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestPlans
        With oFrm
            .PgcPlan = CurrentPlan()
            .Show()
        End With
    End Sub

    Private Sub AddNewPlan()
        Dim oPlan As New PgcPlan
        With oPlan
            If ComboBoxPlan.Items.Count > 0 Then
                .LastPlan = New PgcPlan(ComboBoxPlan.Items(ComboBoxPlan.Items.Count - 1).Value)
                If .LastPlan.YearTo > 0 Then
                    .YearFrom = .LastPlan.YearTo + 1
                    .Nom = "PGC " & .YearFrom
                End If
            End If
        End With

        Dim oFrm As New Frm_PgcPlan_Old
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestPlans
        With oFrm
            .PgcPlan = oPlan
            .Show()
        End With
    End Sub

    Private Sub RefreshRequestPlans(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadPlans()
    End Sub

    Private Sub LoadPlans()
        mAllowEvents = False
        Dim SQL As String = "SELECT ID,NOM FROM PGCPLAN ORDER BY YEARFROM DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With ComboBoxPlan
            .DataSource = oTb
            .DisplayMember = "NOM"
            .ValueMember = "ID"
            .SelectedIndex = 0
        End With
        mAllowEvents = True
    End Sub

    Private Sub PlanToolStripMenuItemAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PlanToolStripMenuItemAdd.Click
        AddNewPlan()
    End Sub
#End Region


    Private Sub LoadCtas()
        mAllowEvents = False

        Dim sGrupField As String = Xl_Langs1.Lang.Tradueix("PGCGRUP.ESP", "(CASE WHEN PGCGRUP.CAT='' THEN PGCGRUP.ESP ELSE PGCGRUP.CAT END)", "(CASE WHEN PGCGRUP.ENG='' THEN PGCGRUP.ESP ELSE PGCGRUP.ENG END)")
        Dim sCtaField As String = Xl_Langs1.Lang.Tradueix("PGCCTA.ESP", "(CASE WHEN PGCCTA.CAT='' THEN PGCCTA.ESP ELSE PGCCTA.CAT END)", "(CASE WHEN PGCCTA.ENG='' THEN PGCCTA.ESP ELSE PGCCTA.ENG END)")

        Dim SQL As String = "SELECT 0 AS LINCOD, PGCGRUP.ID," & sGrupField & " AS PCGRUPNOM FROM PGCGRUP "

        Select Case ComboBoxFiltre.SelectedIndex
            Case Filtres.None
            Case Filtres.OnlyPgcCtas
                SQL = SQL & " INNER JOIN PGCCTA ON PGCGRUP.PGCPLAN=PGCCTA.PGCPLAN AND PGCCTA.ID LIKE PGCGRUP.ID+'%' "
            Case Filtres.OnlyCcbs
                SQL = SQL & " INNER JOIN (SELECT PGCPLAN, CTA FROM CCB GROUP BY PGCPLAN,CTA) B ON PGCGRUP.PGCPLAN=B.PGCPLAN AND B.CTA LIKE PGCGRUP.ID+'%'"
        End Select

        SQL = SQL & "WHERE PGCGRUP.PGCPLAN=" & CurrentPlan.Id & " "
        SQL = SQL & "UNION " _
        & "SELECT 1 AS LINCOD, PGCCTA.ID," & sCtaField & " AS CTANOM FROM PGCCTA " _
        & "WHERE PGCCta.PgcPlan=" & CurrentPlan.Id & " " _
        & "ORDER BY PGCGRUP.ID"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridViewCta
            With .RowTemplate
                .Height = DataGridViewCta.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(ColsCta.LinCod)
                .Visible = False
            End With
            With .Columns(ColsCta.Id)
                .Visible = False
            End With
            With .Columns(ColsCta.Nom)
                '.HeaderText = "Quadre de Comptes"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
        SetCtaContextMenu()
        mCleanTab(TabControl1.SelectedIndex) = True
        mAllowEvents = True
    End Sub

    Private Function CurrentGrup() As PgcGrup
        Dim oGrup As PgcGrup = Nothing
        Dim oRow As DataGridViewRow = DataGridViewCta.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(ColsCta.Id).Value) Then
                oGrup = New PgcGrup(CurrentPlan, oRow.Cells(ColsCta.Id).Value)
            End If
        End If
        Return oGrup
    End Function

    Private Function CurrentCta() As pgccta
        Dim oCta As PgcCta = Nothing
        Dim oRow As DataGridViewRow = DataGridViewCta.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(ColsCta.Id).Value) Then
                oCta = MaxiSrvr.PgcCta.FromNum(CurrentPlan, oRow.Cells(ColsCta.Id).Value)
            End If
        End If
        Return oCta
    End Function

    Private Sub DataGridViewCta_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewCta.CellFormatting
        Dim oGrid As DataGridView = sender
        Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
        Dim sId As String = oRow.Cells(ColsCta.Id).Value
        Try
            Dim iLevel As Integer = sId.Length
            Dim sPad As New String(" ", 4 * iLevel)
            e.Value = sPad & sId & " " & e.Value
            Select Case iLevel
                Case 1
                    e.CellStyle.BackColor = Color.LightGray
                    e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
                Case 2
                    e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
                Case Is >= 3
            End Select

        Catch ex As Exception

        End Try


    End Sub

    Private Function CurrentLinCod() As LinCods
        Dim oLinCod As LinCods = LinCods.Grup
        Dim oRow As DataGridViewRow = DataGridViewCta.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(ColsCta.Id).Value) Then
                oLinCod = oRow.Cells(ColsCta.LinCod).Value
            End If
        End If
        Return oLinCod
    End Function

    Private Sub DataGridViewCtas_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewCta.DoubleClick
        Select Case CurrentLinCod()
            Case LinCods.Grup
                If CurrentGrup() Is Nothing Then
                    AddNewGrup()
                Else
                    ShowGrup()
                End If
            Case LinCods.Cta
                ZoomCta()
        End Select
    End Sub

    Private Sub ShowGrup()
        Dim oFrm As New Frm_PgcGrup
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestCtas
        With oFrm
            .PgcGrup = CurrentGrup()
            .Show()
        End With
    End Sub

    Private Sub ZoomCta()
        Dim oFrm As New Frm_PgcCta_Old(CurrentCta)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestCtas
        oFrm.Show()
    End Sub

    Private Sub SetCtaContextMenu()
        If DataGridViewCta.CurrentRow Is Nothing Then
            CtasToolStripMenuItemZoom.Enabled = False
            CtasToolStripMenuItemDel.Enabled = False
        Else
            Select Case CurrentLinCod()
                Case LinCods.Grup
                    CtasToolStripMenuItemZoom.Enabled = True
                    CtasToolStripMenuItemDel.Enabled = True
                Case LinCods.Cta
                    CtasToolStripMenuItemZoom.Enabled = True
                    CtasToolStripMenuItemDel.Enabled = True
            End Select
        End If
    End Sub

    Private Sub SetEpgContextMenu()
        Dim oEpg As PgcEpg = CurrentEpg()
        If oEpg Is Nothing Then
            EpgToolStripMenuItemZoom.Enabled = False
            EpgToolStripMenuItemAddNew.Enabled = True
            EpgToolStripMenuItemDel.Enabled = False
        Else
            EpgToolStripMenuItemZoom.Enabled = True
            Dim sErr As String = ""
            EpgToolStripMenuItemAddNew.Enabled = oEpg.allowAddChildren(sErr)
            EpgToolStripMenuItemAddNew.ToolTipText = sErr
            sErr = ""
            EpgToolStripMenuItemDel.Enabled = oEpg.AllowDelete(sErr)
        End If

    End Sub

    Private Sub AddNewGrup()

        Dim oFrm As New Frm_PgcGrup
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestCtas
        With oFrm
            .PgcGrup = CurrentPlan.NewGrup
            .Show()
        End With
    End Sub

    Private Sub AddNewSubGrup()
        Dim oFrm As New Frm_PgcGrup
        Dim oGrup As PgcGrup = CurrentGrup()
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestCtas
        With oFrm
            .ParentGrup = oGrup
            .Show()
        End With
    End Sub

    Private Sub AddNewCta()
        Dim oFrm As New Frm_PgcCta_Old(CurrentGrup.NewCta)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestCtas
        oFrm.Show()
    End Sub

    Private Sub RefreshRequestCtas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oGrid As DataGridView = DataGridViewCta
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsCta.Nom

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadCtas()

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

    Private Sub CtasToolStripMenuItemDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtasToolStripMenuItemDel.Click
        Dim rc As MsgBoxResult
        Dim sErr As String = ""
        Select Case CurrentLinCod()
            Case LinCods.Grup
                Dim oGrup As PgcGrup = CurrentGrup()
                rc = MsgBox("eliminem el Grup " & oGrup.Id & " ?", MsgBoxStyle.Information, "MAT.NET")
                If rc = MsgBoxResult.Ok Then
                    If oGrup.Delete(sErr) Then
                        RefreshRequestCtas(sender, e)
                    Else
                        MsgBox("error:" & vbCrLf & sErr, MsgBoxStyle.Exclamation)
                    End If
                End If
            Case LinCods.Cta
                Dim oCta As PgcCta = CurrentCta()
                rc = MsgBox("eliminem el compte " & oCta.Id & " ?", MsgBoxStyle.Information, "MAT.NET")
                If rc = MsgBoxResult.Ok Then
                    Dim exs as New List(Of exception)
                    If PgcCtaLoader.Delete(oCta, exs) Then
                        RefreshRequestCtas(sender, e)
                    Else
                        MsgBox("error al eliminar compte:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                    End If
                End If
        End Select
    End Sub

    Private Sub DataGridViewCta_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewCta.SelectionChanged
        If mAllowEvents Then
            SetCtaContextMenu()
        End If
    End Sub

    Private Sub CtasToolStripMenuItemAddGrup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtasToolStripMenuItemAddGrup.Click
        AddNewGrup()
    End Sub

    Private Sub CtasToolStripMenuItemAddSubgrup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtasToolStripMenuItemAddSubgrup.Click
        AddNewSubGrup()
    End Sub

    Private Sub CtasToolStripMenuItemAddCta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtasToolStripMenuItemAddCta.Click
        AddNewCta()
    End Sub

    Private Function CurrentEpgCod() As PgcEpg.BalCods
        Dim oCod As PgcEpg.BalCods
        Select Case TabControl1.SelectedTab.Text
            Case TabPageBal.Text
                oCod = PgcEpg.BalCods.Balanç
            Case TabPageExplot.Text
                oCod = PgcEpg.BalCods.Explotacio
            Case TabPageCFlow.Text
                oCod = PgcEpg.BalCods.CashFlow
        End Select
        Return oCod
    End Function

    Private Function CurrentGrid() As DataGridView
        Dim oGrid As DataGridView = Nothing
        Select Case TabControl1.SelectedTab.Text
            Case TabPageBal.Text
                oGrid = DataGridViewEpgBal
            Case TabPageExplot.Text
                oGrid = DataGridViewExplot
            Case TabPageCFlow.Text
                oGrid = DataGridViewCFlow
        End Select
        Return oGrid
    End Function

    Private Sub LoadGridEpgs()

        Dim oTb As New DataTable
        With oTb.Columns
            .Add("ID", System.Type.GetType("System.Int32"))
            .Add("NIVEL", System.Type.GetType("System.Int32"))
            .Add("NOM", System.Type.GetType("System.String"))
        End With
        Dim oRootEpg As New PgcEpg(CurrentEpgCod)
        WriteEpg(oRootEpg, oTb)

        If oTb.Rows.Count > 0 Then

            Dim oGrid As DataGridView = CurrentGrid()
            With oGrid
                With .RowTemplate
                    .Height = DataGridViewCta.Font.Height * 1.3
                    '.DefaultCellStyle.BackColor = Color.Transparent
                End With
                .DataSource = oTb
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ColumnHeadersVisible = True
                .RowHeadersVisible = False
                .MultiSelect = False
                .AllowUserToResizeRows = False

                With .Columns(ColsEpg.Id)
                    .Visible = False
                End With
                With .Columns(ColsEpg.Nivel)
                    .Visible = False
                End With
                With .Columns(ColsCta.Nom)
                    .HeaderText = "epigrafs comptes anuals"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                End With
            End With
        End If
        SetEpgContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub WriteEpg(ByVal oParent As PgcEpg, ByRef oTb As DataTable, Optional ByVal iLevel As Integer = 0)
        Dim sEpgNomField As String = Xl_Langs1.Lang.Tradueix("ESP", "(CASE WHEN CAT='' THEN ESP ELSE CAT END)", "(CASE WHEN ENG='' THEN ESP ELSE ENG END)")

        Dim SQL As String = "SELECT ID," & sEpgNomField & " AS EPGNOM FROM PGCEPG WHERE " _
        & "BALCOD=" & CurrentEpgCod() & " AND " _
        & "PARENT=" & oParent.Id

        Dim iChildId As Integer
        Dim oRow As DataRow
        Dim oDrd As SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi)
        Do While oDrd.Read
            iChildId = oDrd("ID")
            oRow = oTb.NewRow
            oRow("ID") = iChildId
            oRow("NIVEL") = iLevel
            oRow("NOM") = oDrd("EPGNOM")
            oTb.Rows.Add(oRow)
            WriteEpg(New PgcEpg(iChildId), oTb, iLevel + 1)
        Loop
        oDrd.Close()
    End Sub

    Private Sub EpgToolStripMenuItemAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EpgToolStripMenuItemAddNew.Click
        AddNewEpg()
    End Sub

    Private Sub DataGridViewEpg_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles _
    DataGridViewEpgBal.CellFormatting, _
    DataGridViewExplot.CellFormatting, _
    DataGridViewCFlow.CellFormatting

        Dim oGrid As DataGridView = sender
        Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
        Dim iLevel As Integer = oRow.Cells(ColsEpg.Nivel).Value
        Dim sPad As New String(" ", 4 * iLevel)
        Select Case iLevel
            Case 0
                e.CellStyle.BackColor = Color.Gray
                e.CellStyle.ForeColor = Color.White
            Case 1
                e.CellStyle.BackColor = Color.LightGray
                e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
                e.Value = sPad & e.Value
            Case 2
                e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
                e.Value = sPad & e.Value
            Case 3
                e.Value = sPad & e.Value
            Case 4
                e.Value = sPad & e.Value
        End Select
    End Sub

    Private Sub DataGridViewEpg_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DataGridViewEpgBal.DoubleClick, _
     DataGridViewExplot.DoubleClick, _
      DataGridViewCFlow.DoubleClick
        ZoomEpg()
    End Sub

    Private Sub DataGridViewEpgBal_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DataGridViewEpgBal.SelectionChanged, _
     DataGridViewExplot.SelectionChanged, _
      DataGridViewCFlow.SelectionChanged
        If mAllowEvents Then
            SetEpgContextMenu()
        End If
    End Sub

    Private Sub EpgToolStripMenuItemZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EpgToolStripMenuItemZoom.Click
        ZoomEpg()
    End Sub

    Private Function CurrentEpg() As PgcEpg
        Dim oEpg As PgcEpg = Nothing
        Dim oGrid As DataGridView = CurrentGrid()
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            Dim EpgId As Integer = oRow.Cells(ColsEpg.Id).Value
            oEpg = New PgcEpg(EpgId)
        End If
        Return oEpg
    End Function

    Private Sub AddNewEpg()
        Dim oGrid As DataGridView = CurrentGrid()
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        Dim oEpgParent As PgcEpg
        If oRow Is Nothing Then
            oEpgParent = New PgcEpg(CurrentEpgCod)
        Else
            Dim EpgId As Integer = oRow.Cells(ColsEpg.Id).Value
            oEpgParent = New PgcEpg(EpgId)
        End If

        Dim oFrm As New Frm_PgcEpg
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestEpgs
        With oFrm
            .DefaultPlan = CurrentPlan()
            .Epg = New PgcEpg(oEpgParent)
            .Show()
        End With
    End Sub

    Private Sub ZoomEpg()
        Dim oGrid As DataGridView = CurrentGrid()
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        Dim EpgId As Integer = 0
        If oRow IsNot Nothing Then EpgId = oRow.Cells(ColsEpg.Id).Value
        Dim oEpg As New PgcEpg(EpgId)
        If EpgId = 0 Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.Balanç
                    oEpg.BalCod = PgcEpg.BalCods.Balanç
                Case Tabs.Explotacio
                    oEpg.BalCod = PgcEpg.BalCods.Explotacio
                Case Tabs.CashFlow
                    oEpg.BalCod = PgcEpg.BalCods.CashFlow
            End Select
        End If

        Dim oFrm As New Frm_PgcEpg
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestEpgs
        With oFrm
            .DefaultPlan = CurrentPlan()
            .Epg = oEpg
            .Show()
        End With
    End Sub


    Private Sub RefreshRequestEpgs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oGrid As DataGridView = CurrentGrid()
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsEpg.Nom

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridEpgs()

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


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If mAllowEvents Then
            Select Case TabControl1.SelectedTab.Text
                Case TabPageCtas.Text
                    If Not mCleanTab(TabControl1.SelectedIndex) Then
                        LoadCtas()
                    End If
                Case TabPageBal.Text, TabPageExplot.Text, TabPageCFlow.Text
                    If Not mCleanTab(TabControl1.SelectedIndex) Then
                        LoadGridEpgs()
                    End If
            End Select
        End If

    End Sub

    Private Sub EpgToolStripMenuItemDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EpgToolStripMenuItemDel.Click
        Dim oEpg As PgcEpg = CurrentEpg()
        Dim sErr As String = ""
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest epigraf?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            If oEpg.Delete(sErr) Then
                RefreshRequestEpgs(sender, e)
            Else
                MsgBox(sErr, MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub CtasToolStripMenuItemZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtasToolStripMenuItemZoom.Click
        Select Case CurrentLinCod()
            Case LinCods.Grup
                ShowGrup()
            Case LinCods.Cta
                ZoomCta()
        End Select
    End Sub

    Private Sub ComboBoxFiltre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxFiltre.SelectedIndexChanged
        LoadCtas()
        SaveSetting("MATSOFT", "MAT.NET", "PGC Ctas Filtre", ComboBoxFiltre.SelectedIndex)
    End Sub

    Private Function GetDefaultFiltre() As Integer
        Dim RetVal As Integer = 0
        Dim sFiltre As String = GetSetting("MATSOFT", "MAT.NET", "PGC Ctas Filtre")
        If IsNumeric(sFiltre) Then
            Dim iFiltre As Integer = CInt(sFiltre)
            If iFiltre >= 0 And iFiltre < ComboBoxFiltre.Items.Count Then
                RetVal = iFiltre
            End If
        End If
        Return RetVal
    End Function

    Private Sub ToolStripButtonSortEpg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonSortEpg.Click

    End Sub


    Private Sub Xl_Langs1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Langs1.AfterUpdate
        If mAllowEvents Then
            Refresca()
        End If
    End Sub
End Class