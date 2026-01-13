

Public Class Frm_PgcCta
    Private mPgcCta As PgcCta
    Private mAllowEvents As Boolean
    Private mCleanTab(10) As Boolean
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mCce As Cce

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        Gral
        Ccds
        Special
    End Enum

    Private Enum ColsCcds
        Cli
        Nom
        Deb
        Hab
        Sdo
    End Enum

    Public Sub New(value As PgcCta)
        MyBase.New()
        Me.InitializeComponent()
        mPgcCta = value
        Refresca()
        mAllowEvents = True
    End Sub

    Public Sub New(value As Cce)
        MyBase.New()
        Me.InitializeComponent()
        mCce = value
        mPgcCta = mCce.Cta

        LoadCcdYeas(mCce.Yea)
        LoadGridCcds()
        TabControl1.SelectedIndex = Tabs.Ccds

        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mPgcCta
            If .IsNew Then
                Me.Text = "NOU COMPTE"
                ButtonDel.Enabled = False
                TextBoxId.ReadOnly = False
            Else
                Me.Text = "COMPTE " & .Id
                ButtonDel.Enabled = True
                TextBoxId.Text = .Id
                TextBoxId.ReadOnly = True
            End If

            TextBoxPgcPlan.Text = .Plan.Nom
            RefreshRequestPgcGrups(Nothing, New System.EventArgs)

            TextBoxEsp.Text = .Esp
            TextBoxCat.Text = .Cat
            TextBoxEng.Text = .Eng
            TextBoxDsc.Text = .Dsc

            ComboBoxSigne.Items.Clear()
            ComboBoxSigne.Items.Add("(seleccionar signe)")
            Select Case .Bal
                Case PgcGrup.BalCods.balanç
                    ComboBoxSigne.Items.Add("deutor")
                    ComboBoxSigne.Items.Add("creditor")
                Case PgcGrup.BalCods.explotacio
                    ComboBoxSigne.Items.Add("despeses")
                    ComboBoxSigne.Items.Add("ingressos")
            End Select
            ComboBoxSigne.SelectedIndex = .Act
            CheckBoxIsBaseImponibleIVA.Checked = .IsBaseImponibleIva

            UIHelper.LoadComboFromEnum(ComboBoxCod, GetType(DTOPgcPlan.Ctas))
            ComboBoxCod.SelectedValue = .Cod
        End With
    End Sub


    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxEsp.TextChanged, _
    TextBoxCat.TextChanged, _
    TextBoxEng.TextChanged, _
    TextBoxDsc.TextChanged, _
    ComboBoxSigne.SelectedValueChanged, _
    ComboBoxCod.SelectedValueChanged, _
    Xl_Cta_PGC_next.AfterUpdate, _
    Xl_Cta_PGC_previous.AfterUpdate, _
     CheckBoxIsBaseImponibleIVA.CheckedChanged

        If mAllowEvents Then
            Dim blEnable As Boolean = True
            If CurrentAct() = MaxiSrvr.PgcCta.Acts.notset Then blEnable = False
            ButtonOk.Enabled = blEnable
        End If
    End Sub

    Private Function CurrentAct() As PgcCta.Acts
        Dim oAct As PgcCta.Acts = ComboBoxSigne.SelectedIndex
        Return oAct
    End Function

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mPgcCta
            .Id = TextBoxId.Text
            .Esp = TextBoxEsp.Text
            .Cat = TextBoxCat.Text
            .Eng = TextBoxEng.Text
            .Dsc = TextBoxDsc.Text
            .Cod = ComboBoxCod.SelectedValue
            .Act = CurrentAct()
            .IsBaseImponibleIva = CheckBoxIsBaseImponibleIVA.Checked
        End With

        Dim exs as New List(Of exception)
        If PgcCtaLoader.Update(mPgcCta, exs) Then
            RaiseEvent AfterUpdate(mPgcCta, e)
            Me.Close()
        Else
            MsgBox("error al desat el compte" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        End If


    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim sErr As String = ""
        Dim rc As MsgBoxResult = MsgBox("eliminem el compte " & mPgcCta.Id & " ?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then

            Dim exs as New List(Of exception)
            If PgcCtaLoader.Delete(mPgcCta, exs) Then
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                MsgBox("error al eliminar el compte" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If

        End If
    End Sub


    Private Sub TextBoxGrup1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxGrup1.DoubleClick
        ShowParent(1)
    End Sub

    Private Sub TextBoxGrup2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxGrup2.DoubleClick
        ShowParent(2)
    End Sub

    Private Sub TextBoxGrup3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxGrup3.DoubleClick
        ShowParent(3)
    End Sub

    Private Sub TextBoxGrup4_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxGrup4.DoubleClick
        ShowParent(4)
    End Sub

    Private Sub ShowParent(ByVal iLevel As Integer)
        Dim oGrup As PgcGrup = mPgcCta.Parent(iLevel)
        Dim oFrm As New Frm_PgcGrup
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestPgcGrups
        With oFrm
            .PgcGrup = oGrup
            .Show()
        End With
    End Sub

    Private Sub RefreshRequestPgcGrups(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oParent As PgcGrup = Nothing
        With mPgcCta
            oParent = .Parent(1)
            If oParent IsNot Nothing Then
                TextBoxGrup1.Text = oParent.FullNom
            End If
            oParent = .Parent(2)
            If oParent IsNot Nothing Then
                TextBoxGrup2.Text = oParent.FullNom
            End If
            oParent = .Parent(3)
            If oParent IsNot Nothing Then
                TextBoxGrup3.Text = oParent.FullNom
            End If
            oParent = .Parent(4)
            If oParent IsNot Nothing Then
                TextBoxGrup4.Text = oParent.FullNom
            End If
        End With
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Gral
                Refresca()
            Case Tabs.Ccds
                If Not mCleanTab(Tabs.Ccds) Then
                    LoadCcdYeas()
                    LoadGridCcds()
                End If
            Case Tabs.Special
                LoadSpecialMrt()
        End Select
    End Sub

    Private Sub LoadCcdYeas(Optional ByVal iYea As Integer = 0)
        Static BlLoaded As Boolean
        If Not BlLoaded Then
            mAllowEvents = False
            Dim SQL As String = "SELECT CCB.YEA " _
            & "FROM CCB " _
            & "WHERE CCB.EMP=" & mEmp.Id & " AND " _
            & "CCB.PGCPLAN=" & mPgcCta.Plan.Id & " AND " _
            & "CCB.CTA LIKE '" & mPgcCta.Id & "' " _
            & "GROUP BY CCB.YEA"

            Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
            Dim oTb As DataTable = oDs.Tables(0)

            With ComboBoxCcdYeas
                .DataSource = oTb
                .DisplayMember = "YEA"
                .ValueMember = "YEA"
                If oTb.Rows.Count > 0 Then
                    If iYea = 0 Then
                        .SelectedIndex = 0
                    Else
                        .SelectedValue = iYea
                    End If
                End If
            End With

        End If

        mAllowEvents = True

    End Sub

    Private Function CurrentCcdYea() As Integer
        Dim iYea As Integer
        If ComboBoxCcdYeas.Items.Count > 0 Then
            iYea = ComboBoxCcdYeas.SelectedValue
        End If
        Return iYea
    End Function

    Private Function CurrentCcds() As Ccds
        Dim oContact As Contact
        Dim oCcd As Ccd
        Dim oCcds As New Ccds
        Select Case DataGridViewCcds.SelectedRows.Count
            Case Is <= 1
                oCcds.Add(CurrentCcd)
            Case Else
                For Each oRow As DataGridViewRow In DataGridViewCcds.SelectedRows
                    oContact = MaxiSrvr.Contact.FromNum(mEmp, oRow.Cells(ColsCcds.Cli).Value)
                    oCcd = New Ccd(mCce, oContact)
                    oCcds.Add(oCcd)
                Next
        End Select
        Return oCcds
    End Function

    Private Sub ComboBoxCcdYeas_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxCcdYeas.SelectedValueChanged
        If mAllowEvents Then
            LoadGridCcds()
        End If
    End Sub

    Private Function CcdsDataset() As DataSet
        Dim oAct As PgcCta.Acts = mPgcCta.Act
        If oAct = MaxiSrvr.PgcCta.Acts.notset Then oAct = MaxiSrvr.PgcCta.Acts.deutora
        Dim sDebHeader As String = ""
        Dim sHabHeader As String = ""

        Dim oBal As PgcGrup.BalCods = mPgcCta.Bal
        If oBal = PgcGrup.BalCods.NotSet Then oBal = PgcGrup.BalCods.balanç
        Dim SQL As String = "SELECT CLX.CLI, CLX.CLX, "
        Select Case oBal
            Case PgcGrup.BalCods.balanç
                sDebHeader = "deure"
                sHabHeader = "haver"
                SQL = SQL & "SUM(CASE WHEN DH=" & oAct & " THEN EUR ELSE 0 END) AS " & sDebHeader & ", " _
                & "SUM(CASE WHEN DH<>" & oAct & " THEN EUR ELSE 0 END) AS " & sHabHeader & ", "
            Case PgcGrup.BalCods.explotacio
                sDebHeader = "ingresos"
                sHabHeader = "despeses"
                SQL = SQL & "SUM(CASE WHEN DH=" & oAct & " THEN EUR ELSE 0 END) AS " & sHabHeader & ", " _
                & "SUM(CASE WHEN DH<>" & oAct & " THEN EUR ELSE 0 END) AS " & sHabHeader & ", "
        End Select

        SQL = SQL & "SUM(CASE WHEN DH=" & oAct & " THEN EUR ELSE -EUR END) AS SDO " _
        & "FROM CCB LEFT OUTER JOIN CLX ON CCB.EMP=CLX.EMP AND CCB.CLI=CLX.CLI " _
        & "WHERE CCB.EMP=" & mEmp.Id & " AND " _
        & "CCB.PGCPLAN=" & mPgcCta.Plan.Id & " AND " _
        & "CCB.CTA LIKE '" & mPgcCta.Id & "' AND " _
        & "CCB.YEA=" & CurrentCcdYea() & " " _
        & "GROUP BY CLX.CLI, CLX.CLX "

        If CheckBoxOcultarComptesSaldats.Checked Then
            SQL = SQL & " HAVING SUM(CASE WHEN DH=1 THEN EUR ELSE -EUR END)<>0 "
        End If

        SQL = SQL & " ORDER BY CLX.CLX"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Return oDs
    End Function

    Private Sub LoadGridCcds()
        Dim oGrid As DataGridView = DataGridViewCcds

        Dim oTb As DataTable = CcdsDataset.Tables(0)

        With oGrid
            With .RowTemplate
                .Height = oGrid.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(ColsCcds.Cli)
                .Visible = False
            End With
            With .Columns(ColsCcds.Nom)
                .HeaderText = "titular"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsCcds.Deb)
                .Width = 100
                '.HeaderText = sDebHeader
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsCcds.Hab)
                .Width = 100
                '.HeaderText = sHabHeader
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsCcds.Sdo)
                .Width = 100
                .HeaderText = "saldo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mAllowEvents = True

    End Sub

    Private Sub DataGridViewCcds_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewCcds.DoubleClick
        ZoomCcd()
    End Sub

    Private Sub RefreshRequestCcds(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadGridCcds()
    End Sub


    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(CcdsDataset).Visible = True
    End Sub

    Private Function CurrentCcd() As Ccd
        Dim oRow As DataGridViewRow = DataGridViewCcds.CurrentRow
        Dim CliId As Integer = 0
        If Not IsDBNull(oRow.Cells(ColsCcds.Cli).Value) Then
            CliId = oRow.Cells(ColsCcds.Cli).Value
        End If
        Dim oContact As Contact = MaxiSrvr.Contact.FromNum(mEmp, CliId)
        Dim oCcd As New Ccd(oContact, CurrentCcdYea, mPgcCta)
        Return oCcd
    End Function

    Private Sub ZoomCcd()
        Dim oCcd As Ccd = CurrentCcd()
        Dim oFrm As New Frm_CliCtasOld(oCcd)
        AddHandler oFrm.afterupdate, AddressOf RefreshRequestCcds
        oFrm.Show()
    End Sub


    Private Sub CheckBoxOcultarComptesSaldats_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxOcultarComptesSaldats.CheckedChanged
        If mAllowEvents Then
            RefreshRequestCcds(sender, e)
        End If
    End Sub

#Region "Special"

    Private Enum ColsSpecial
        Id
        Pdf
        PdfIco
        Fch
        Nom
        Val
        Acum
        Inv
        Dot
    End Enum

    Private Sub LoadSpecialMrt()
        mAllowEvents = False
        Dim iYea As Integer = CurrentCcdYea()
        Dim SQL As String = "SELECT MRT.Itm, " _
        & "(CASE WHEN BF.GUID IS NULL THEN 0 ELSE 1 END) AS PDF, " _
        & "MRT.FCH, MRT.dsc, MRT.eur AS VALOR, SUM(MR2.eur) AS AMORTACUMULADA, " _
        & "MRT.eur - SUM(MR2.eur) AS INVENTARI, " _
        & "SUM(CASE WHEN YEAR(MR2.FCH)=" & iYea & " THEN MR2.EUR ELSE 0 END) AS DOTACIO " _
        & "FROM MRT INNER JOIN " _
        & "MR2 ON MRT.Emp = MR2.emp AND MRT.Itm = MR2.itm LEFT OUTER JOIN " _
        & "CCA ON MRT.Emp = CCA.emp AND MRT.AltaYea = CCA.yea AND MRT.AltaCca = CCA.cca LEFT OUTER JOIN " _
        & "BIGFILE AS BF ON CCA.Guid LIKE BF.GUID " _
        & "WHERE MRT.Emp =" & mEmp.Id & " AND " _
        & "MRT.PgcPlan LIKE '" & mPgcCta.Plan.Id & "' AND " _
        & "MRT.cta LIKE '" & mPgcCta.Id & "' AND " _
        & "YEAR(MR2.fch) <=" & iYea & " " _
        & "GROUP BY MRT.Itm, MRT.FCH, MRT.dsc, MRT.eur, BF.GUID " _
        & "HAVING(SUM(Mr2.eur) < Mrt.eur) " _
        & "ORDER BY MRT.FCH DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(ColsSpecial.PdfIco)

        Dim oRowSum As DataRow = oTb.NewRow
        oRowSum(ColsSpecial.Nom) = "totals"
        oRowSum(ColsSpecial.Pdf) = 0
        oRowSum(ColsSpecial.Val) = 0
        oRowSum(ColsSpecial.Acum) = 0
        oRowSum(ColsSpecial.Inv) = 0
        oRowSum(ColsSpecial.Dot) = 0
        For Each oRow As DataRow In oTb.Rows
            If Not IsDBNull(oRow(ColsSpecial.Val)) Then
                oRowSum(ColsSpecial.Val) += oRow(ColsSpecial.Val)
            End If
            If Not IsDBNull(oRow(ColsSpecial.Acum)) Then
                oRowSum(ColsSpecial.Acum) += oRow(ColsSpecial.Acum)
            End If
            If Not IsDBNull(oRow(ColsSpecial.Inv)) Then
                oRowSum(ColsSpecial.Inv) += oRow(ColsSpecial.Inv)
            End If
            If Not IsDBNull(oRow(ColsSpecial.Dot)) Then
                oRowSum(ColsSpecial.Dot) += oRow(ColsSpecial.Dot)
            End If
        Next
        oTb.Rows.InsertAt(oRowSum, 0)

        With DataGridViewSpecial
            With .RowTemplate
                .Height = DataGridViewSpecial.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(ColsSpecial.Id)
                .Visible = False
            End With
            With .Columns(ColsSpecial.Pdf)
                .Visible = False
            End With
            With .Columns(ColsSpecial.PdfIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsSpecial.Fch)
                .HeaderText = "Data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsSpecial.Nom)
                .HeaderText = "Concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsSpecial.Val)
                .HeaderText = "valor de adquisició"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsSpecial.Acum)
                .HeaderText = "Amortització acumulada"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsSpecial.Inv)
                .HeaderText = "Valor de inventari"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsSpecial.Dot)
                .HeaderText = "Dotacio exerc." & iYea.ToString
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Function CurrentMrt() As Mrt
        Dim oGrid As DataGridView = DataGridViewSpecial
        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        Dim oMrt As Mrt = Nothing
        If oRow IsNot Nothing Then
            Dim iId As Integer = oRow.Cells(ColsSpecial.Id).Value
            oMrt = New Mrt(mEmp, iId)
        End If
        Return oMrt
    End Function

    Private Sub SetContextMenuCcds()
        Dim oGrid As DataGridView = DataGridViewSpecial
        Dim oContextMenu As New ContextMenuStrip
        Dim oCcds As Ccds = CurrentCcds()

        Dim oMenu_Ccd As New Menu_Ccd(oCcds)
        AddHandler oMenu_Ccd.AfterUpdate, AddressOf RefreshRequestCcds
        oContextMenu.Items.AddRange(oMenu_Ccd.Range)

        oGrid.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub SetContextMenuMrt()
        Dim oGrid As DataGridView = DataGridViewSpecial
        Dim oContextMenu As New ContextMenuStrip
        Dim oMrt As Mrt = CurrentMrt()

        If oMrt IsNot Nothing Then
            Dim oMenu_Mrt As New Menu_Mrt(oMrt)
            AddHandler oMenu_Mrt.AfterUpdate, AddressOf RefreshRequestMrts
            oContextMenu.Items.AddRange(oMenu_Mrt.Range)
        End If

        oGrid.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshrequestMrts(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadSpecialMrt()
    End Sub

    Private Sub ShowMrt()
        Dim oMrt As Mrt = CurrentMrt()
        If oMrt IsNot Nothing Then
            Dim oFrm As New Frm_Mrt
            AddHandler oFrm.AfterUpdate, AddressOf RefreshrequestMrts
            With oFrm
                .Mrt = oMrt
                .Show()
            End With
        End If
    End Sub

    Private Sub DataGridViewSpecial_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewSpecial.CellFormatting
        Select Case e.ColumnIndex
            Case ColsSpecial.PdfIco
                Dim oRow As DataGridViewRow = CType(sender, DataGridView).CurrentRow
                If oRow IsNot Nothing Then

                    Dim iPdfCod As Integer = oRow.Cells(ColsSpecial.Pdf).Value
                    If iPdfCod = 1 Then
                        e.Value = My.Resources.pdf
                    Else
                        e.Value = My.Resources.empty
                    End If
                End If
        End Select
    End Sub

    Private Sub DataGridViewSpecial_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewSpecial.DoubleClick
        ShowMrt()
    End Sub

    Private Sub DataGridViewSpecial_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewSpecial.SelectionChanged
        If mAllowEvents Then
            SetContextMenuMrt()
        End If
    End Sub
#End Region

 
End Class