
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_Fiscal_Mod347
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean = False
    Private MINVALUE As Decimal = 0

    Private Enum Cols
        Cli
        Conform
        ConformIco
        Nif
        Nom
        Provincia
        Eur
        ObsExist
        ObsIco
    End Enum

    Private Sub Frm_Fiscal_Mod347_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MINVALUE = BLL.BLLDefault.EmpValue(DTODefault.Codis.Min347)
        LoadYeas()
        LoadOps()
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT CONTACT,CONFORMITAT,NIF,MOD347.NOM AS CLINOM,PROVINCIA.NOM AS PROVINCIA,EUR,(CASE WHEN MOD347.OBS IS NULL THEN 0 ELSE 1 END) AS OBSEXIST " _
        & "FROM MOD347 LEFT OUTER JOIN " _
        & "PROVINCIA ON MOD347.PAIS LIKE PROVINCIA.PAIS AND MOD347.PROVINCIA=PROVINCIA.ID " _
        & "WHERE EMP=@EMP AND OP=@OP AND YEA=@YEA AND EUR>0 ORDER BY CLINOM"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", CurrentYea, "@OP", CInt(CurrentOp()))
        Dim oTb As DataTable = oDs.Tables(0)

        'afegeix icono conforme client
        Dim oColCfm As DataColumn = oTb.Columns.Add("CONFORMICO", System.Type.GetType("System.Byte[]"))
        oColCfm.SetOrdinal(Cols.ConformIco)

        'afegeix icono observacions
        Dim oColObs As DataColumn = oTb.Columns.Add("OBSICO", System.Type.GetType("System.Byte[]"))
        oColObs.SetOrdinal(Cols.ObsIco)

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

            With .Columns(Cols.Cli)
                .Visible = False
            End With
            With .Columns(Cols.Conform)
                .Visible = False
            End With
            With .Columns(Cols.ConformIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nif)
                .HeaderText = "NIF"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "declarat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Provincia)
                .HeaderText = "provincia"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.ObsExist)
                .Visible = False
            End With
            With .Columns(Cols.ObsIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

        End With
        mAllowEvents = True
        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentItm.Contact
        Dim oMenuItem As ToolStripMenuItem = Nothing
        If oContact IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.prismatics, AddressOf Do_Zoom)
            oContextMenu.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("extracte", My.Resources.notepad, AddressOf Do_Extracte)
            oContextMenu.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("contacte...")
            oContextMenu.Items.Add(oMenuItem)
            Dim oMenu_Contact As New Menu_Contact(oContact)
            oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentMod347() As Mod347
        Dim RetVal As New Mod347(mEmp, CurrentYea)
        Return RetVal
    End Function

    Private Function CurrentContact() As Contact
        Dim oContact As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oContact = MaxiSrvr.Contact.FromNum(mEmp, CInt(oRow.Cells(Cols.Cli).Value))
        End If
        Return oContact
    End Function

    Private Function CurrentItm() As Mod347Itm
        Dim oMod347 As New Mod347(mEmp, CurrentYea)
        Dim RetVal As New Mod347Itm(oMod347, CurrentContact, CurrentOp)
        Return RetVal
    End Function

    Private Function CurrentYea() As Integer
        Dim iYea As Integer = CInt(ToolStripComboBoxYea.Text)
        Return iYea
    End Function

    Private Function CurrentOp() As Mod347Itm.Ops
        Dim RetVal As Mod347Itm.Ops = CType(ToolStripComboBoxOp.SelectedIndex, Mod347Itm.Ops)
        Return RetVal
    End Function

    Private Sub LoadYeas()
        Dim SQL As String = "SELECT YEA FROM MOD347 WHERE EMP=@EMP GROUP BY YEA ORDER BY YEA DESC"
        Dim oDrd = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", BLLApp.Emp.Id)
        Do While oDrd.read
            ToolStripComboBoxYea.Items.Add(oDrd("YEA").ToString)
        Loop
        ToolStripComboBoxYea.SelectedIndex = 0
    End Sub

    Private Sub LoadOps()

        Dim i As Integer = 0
        Dim s As String '= [Enum].Parse(GetType(test), test.uno)
        For Each s In [Enum].GetNames(GetType(Mod347Itm.Ops))
            If s = "NotSet" Then
                ToolStripComboBoxOp.Items.Add("(seleccionar codi)")
            Else
                ToolStripComboBoxOp.Items.Add(s)
            End If
            i = i + 1
        Next
        ToolStripComboBoxOp.SelectedIndex = 2
    End Sub

    Private Sub ToolStripButtonNewYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonNewYear.Click
        Dim oMod347 As New Mod347(mEmp, Today.Year - 1)

        Dim sWarn As String = ""
        If oMod347.ExistItems Then
            sWarn = "Precaució:" & vbCrLf & "aixó esborrará els registres de " & oMod347.Yea.ToString & " amb les observacions que hagin deixat els clients a la web." & vbCrLf & "i carregará les dades un altre cop"
        Else
            sWarn = "Carreguem els registres de " & oMod347.Yea & "?"
        End If

        Dim rc As MsgBoxResult = MsgBox(sWarn, MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            ToolStripProgressBar1.Visible = True
            DeleteLog(oMod347)
            LoadCompres(oMod347)
            LoadVendes(oMod347)
            ToolStripComboBoxYea.Items.Insert(0, oMod347.Yea)
            ToolStripComboBoxYea.SelectedIndex = 0
            If ToolStripComboBoxOp.Items.Count >= CInt(Mod347Itm.Ops.Ventas) - 1 Then
                ToolStripComboBoxOp.SelectedIndex = CInt(Mod347Itm.Ops.Ventas)
            End If
            RefreshRequest()
            ToolStripProgressBar1.Visible = False
        End If
    End Sub



    Private Sub DeleteLog(ByVal oMod347 As Mod347)
        Dim SQL As String = "DELETE FROM MOD347 WHERE EMP=@EMP AND YEA=@YEA"
        maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi, "@EMP", oMod347.Emp.Id, "@YEA", oMod347.Yea)
    End Sub

    Private Function DataSetCompres(ByVal oMod347 As Mod347) As DataSet
        Dim oPlan As PgcPlan = PgcPlan.FromYear(oMod347.Yea)
        Dim sCtaIva As String = oPlan.Cta(DTOPgcPlan.ctas.IvaSoportat).Id.Substring(0, 3) & "%"
        Dim sCtaIrpf As String = oPlan.Cta(DTOPgcPlan.ctas.Irpf).Id.Substring(0, 4) & "%"
        Dim sCtaRetencions As String = oPlan.Cta(DTOPgcPlan.ctas.RetencionsInteresos).Id.Substring(0, 4) & "%"

        Dim SQL As String = "SELECT CCB_BASE.CLI, " _
        & "SUM(CASE WHEN CCB_BASE.DH=1 THEN CCB_BASE.eur ELSE -CCB_BASE.eur END) AS BASE, " _
        & "SUM(CASE WHEN CCB_IVA.DH=1 THEN CCB_IVA.eur ELSE -CCB_IVA.EUR END) AS IVA " _
        & "FROM CCB AS CCB_BASE INNER JOIN " _
        & "CCA ON CCB_BASE.Emp = CCA.emp AND CCB_BASE.yea = CCA.yea AND CCB_BASE.Cca = CCA.cca AND (CCB_BASE.cta LIKE '6%' OR CCB_BASE.cta LIKE '2%') LEFT OUTER JOIN " _
        & "CCB AS CCB_IVA ON CCA.emp = CCB_IVA.Emp AND CCA.yea = CCB_IVA.yea AND CCA.cca = CCB_IVA.Cca AND CCB_IVA.cta LIKE @CTAIVA LEFT OUTER JOIN " _
        & "CCB AS CCB_IRPF ON CCA.emp = CCB_IRPF.Emp AND CCA.yea = CCB_IRPF.yea AND CCA.cca = CCB_IRPF.Cca AND (CCB_IRPF.cta LIKE @CTAIRPF OR CCB_IRPF.cta LIKE @CTARETENCIONS) LEFT OUTER JOIN " _
        & "CLX ON CCB_BASE.EMP=CLX.EMP AND CCB_BASE.CLI=CLX.CLI " _
        & "WHERE CCA.emp =@EMP AND CCA.yea =@YEA AND CCB_BASE.CLI>0 AND CCB_IRPF.CLI IS NULL " _
        & "GROUP BY CCB_BASE.CLI, CLX.CLX " _
        & "ORDER BY CLX.CLX"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", oMod347.Yea, "@CTAIVA", sCtaIva, "@CTAIRPF", sCtaIrpf, "@CTARETENCIONS", sCtaRetencions)
        Return oDs
    End Function

    Private Function DataSetVendes(ByVal oMod347 As Mod347) As DataSet
        Dim oPlan As PgcPlan = PgcPlan.FromYear(oMod347.Yea)
        Dim sCtaVendes As String = "7%"
        Dim sCtaIVA As String = oPlan.Cta(DTOPgcPlan.ctas.IvaRepercutit).Id
        Dim sCtaRED As String = oPlan.Cta(DTOPgcPlan.ctas.IvaReduit).Id
        Dim sCtaREQ As String = oPlan.Cta(DTOPgcPlan.ctas.IvaRecarrecEquivalencia).Id
        Dim SQL As String = "SELECT CCB_BASE.CLI, " _
        & "SUM(CASE WHEN CCB_BASE.DH=2 THEN CCB_BASE.eur ELSE -CCB_BASE.eur END) AS BASE, " _
        & "SUM(CASE WHEN CCB_IVA.DH=2 THEN CCB_IVA.eur ELSE -CCB_IVA.eur END) AS IVA, " _
        & "SUM(CASE WHEN CCB_RED.DH=2 THEN CCB_RED.eur ELSE -CCB_RED.eur END) AS RED, " _
        & "SUM(CASE WHEN CCB_REQ.DH=2 THEN CCB_REQ.eur ELSE -CCB_REQ.eur END) AS REQ " _
        & "FROM CCB AS CCB_BASE INNER JOIN " _
        & "CCA ON CCB_BASE.Emp = CCA.emp AND CCB_BASE.yea = CCA.yea AND CCB_BASE.Cca = CCA.cca AND CCB_BASE.cta LIKE @CTAVENDES LEFT OUTER JOIN " _
        & "CCB AS CCB_IVA ON CCA.emp = CCB_IVA.Emp AND CCA.yea = CCB_IVA.yea AND CCA.cca = CCB_IVA.Cca AND CCB_IVA.cta LIKE @CTAIVA LEFT OUTER JOIN " _
        & "CCB AS CCB_RED ON CCA.emp = CCB_RED.Emp AND CCA.yea = CCB_RED.yea AND CCA.cca = CCB_RED.Cca AND CCB_RED.cta LIKE @CTARED LEFT OUTER JOIN " _
        & "CCB AS CCB_REQ ON CCA.emp = CCB_REQ.Emp AND CCA.yea = CCB_REQ.yea AND CCA.cca = CCB_REQ.Cca AND CCB_REQ.cta LIKE @CTAREQ LEFT OUTER JOIN " _
        & "CLX ON CCB_BASE.EMP=CLX.EMP AND CCB_BASE.CLI=CLX.CLI " _
        & "WHERE CCA.emp =@EMP AND CCA.yea =@YEA AND CCB_BASE.CLI>0 " _
        & "GROUP BY CCB_BASE.CLI, CLX.CLX " _
        & "ORDER BY CLX.CLX"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", oMod347.Yea, "@CTAVENDES", sCtaVendes, "@CTAIVA", sCtaIVA, "@CTARED", sCtaRED, "@CTAREQ", sCtaREQ)
        Return oDs
    End Function

    Private Sub LoadCompres(ByVal oMod347 As Mod347)
        Dim oDs As DataSet = DataSetCompres(oMod347)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oItm As Mod347Itm = Nothing
        Dim oOp As Mod347Itm.Ops = Mod347Itm.Ops.Compras
        Dim oRow As DataRow = Nothing
        With ToolStripProgressBar1
            .Value = 0
            .Maximum = oTb.Rows.Count
        End With
        For Each oRow In oTb.Rows
            oItm = GetItemFromDataRow(oRow, oMod347, oOp)
            oItm.Update()
            ToolStripProgressBar1.Increment(1)
        Next

    End Sub

    Private Sub LoadVendes(ByVal oMod347 As Mod347)
        Dim oDs As DataSet = DataSetVendes(oMod347)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oItm As Mod347Itm = Nothing
        Dim oOp As Mod347Itm.Ops = Mod347Itm.Ops.Ventas
        Dim oRow As DataRow = Nothing
        With ToolStripProgressBar1
            .Value = 0
            .Maximum = oTb.Rows.Count
        End With

        For Each oRow In oTb.Rows
            oItm = GetItemFromDataRow(oRow, oMod347, oOp)
            oItm.Update()
            ToolStripProgressBar1.Increment(1)
        Next
    End Sub

    Private Function GetItemFromDataRow(ByVal oRow As DataRow, ByVal oMod347 As Mod347, ByVal oOp As Mod347Itm.Ops) As Mod347Itm
        Dim oContact As Contact = MaxiSrvr.Contact.FromNum(mEmp, CInt(oRow("CLI")))
        Dim oItm As New Mod347Itm(oMod347, oContact, oOp)
        Dim oZip As Zip = oContact.GetAdr(Adr.Codis.Fiscal).Zip


        Dim DcEur As Decimal = CDec(oRow("BASE"))
        If Not IsDBNull(oRow("IVA")) Then
            DcEur += CDec(oRow("IVA"))
        End If

        If oOp = Mod347Itm.Ops.Ventas Then
            If Not IsDBNull(oRow("RED")) Then
                DcEur += CDec(oRow("RED"))
            End If
            If Not IsDBNull(oRow("REQ")) Then
                DcEur += CDec(oRow("REQ"))
            End If
        End If

        With oItm
            .Nom = oContact.Nom
            .NIF = New DTONif(oContact.NIF)
            If oZip.GetProvincia Is Nothing Then
                .Provincia = New Provincia(oZip.Country)
            Else
                .Provincia = oZip.GetProvincia
            End If
            .Eur = DcEur
        End With
        Return oItm
    End Function

    Private Sub RefreshRequest()
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
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

    Private Sub ToolStripComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxYea.SelectedIndexChanged
        LoadGrid()
    End Sub

    Private Sub ToolStripComboBoxOp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxOp.SelectedIndexChanged
        LoadGrid()
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ConformIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case CType(oRow.Cells(Cols.Conform).Value, Mod347Itm.Conformitats)
                    Case Mod347Itm.Conformitats.NotSet
                        e.Value = My.Resources.empty
                    Case Mod347Itm.Conformitats.Llegit
                        e.Value = My.Resources.info
                    Case Mod347Itm.Conformitats.Ok
                        e.Value = My.Resources.Ok
                    Case Mod347Itm.Conformitats.Ko
                        e.Value = My.Resources.warn
                End Select
            Case Cols.ObsIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.ObsExist).Value = 1 Then
                    e.Value = My.Resources.help
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Zoom()
    End Sub

    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Fiscal_Mod347_Cli(CurrentItm)
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_Fiscal_Mod347Itm(CurrentItm)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        If oRow.Cells(Cols.Eur).Value < MINVALUE Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        Else
            oRow.DefaultCellStyle.BackColor = Color.White
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub ToolStripButtonMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonMail.Click
        Dim SQL As String = "SELECT E.EMAIL " _
        & "FROM MOD347 INNER JOIN " _
        & "EMAILSCOMPTABILITAT E ON MOD347.Emp = E.emp AND MOD347.Contact = E.cli " _
        & "WHERE MOD347.Emp =@EMP AND MOD347.yea =@YEA AND MOD347.EUR>=@MINEUR " _
        & "GROUP BY E.EMAIL ORDER BY E.EMAIL"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", CurrentYea, "@MINEUR", MINVALUE)
        MatExcel.GetExcelFromDataset(oDs).Visible = True
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        Dim oMod347 As Mod347 = CurrentMod347()
        Dim oItm As Mod347Itm = Nothing
        Dim iLin As Integer = 0
        Dim oDs As DataSet = DataSetCompres(oMod347)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = Nothing


        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        With oSheet
            .Cells.Font.Size = 9
        End With

        iLin += 1
        oSheet.Cells(iLin, 2) = "COMPRES"
        iLin += 1

        oDs = DataSetCompres(oMod347)
        oTb = oDs.Tables(0)
        For Each oRow In oTb.Rows
            oItm = GetItemFromDataRow(oRow, oMod347, Mod347Itm.Ops.Compras)
            If oItm.Declarable Then
                iLin += 1
                oSheet.Cells(iLin, 1) = oItm.Eur
                oSheet.Cells(iLin, 2) = oItm.NIF
                oSheet.Cells(iLin, 3) = oItm.Provincia.Nom
                oSheet.Cells(iLin, 4) = oItm.Nom
            End If
        Next

        iLin += 2
        oSheet.Cells(iLin, 2) = "VENDES"
        iLin += 1

        oDs = DataSetVendes(oMod347)
        oTb = oDs.Tables(0)
        For Each oRow In oTb.Rows
            oItm = GetItemFromDataRow(oRow, oMod347, Mod347Itm.Ops.Ventas)
            If oItm.Declarable Then
                iLin += 1
                oSheet.Cells(iLin, 1) = oItm.Eur
                oSheet.Cells(iLin, 2) = oItm.NIF
                oSheet.Cells(iLin, 3) = oItm.Provincia.Nom
                oSheet.Cells(iLin, 4) = oItm.Nom
            End If
        Next

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        oApp.Visible = True
        oApp = Nothing
    End Sub

    Private Sub ToolStripButtonFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonFile.Click
        Dim oOrg As DTOContact = BLLApp.Org
        Dim sContacte As String = ""
        'If oOrg.SubContacts.Count > 0 Then
        ' sContacte = oOrg.SubContacts(0)
        ' End If
        Dim oFile347 As New MaxiSrvr.MatFileAEAT347(CurrentYea, oOrg.Nif, oOrg.Nom, sContacte, BLLContact.Tel(oOrg))

        Dim oMod347 As Mod347 = CurrentMod347()
        Dim oItm As Mod347Itm = Nothing
        Dim iLin As Integer = 0
        Dim oDs As DataSet = DataSetCompres(oMod347)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = Nothing


        oDs = DataSetCompres(oMod347)
        oTb = oDs.Tables(0)
        For Each oRow In oTb.Rows
            oItm = GetItemFromDataRow(oRow, oMod347, Mod347Itm.Ops.Compras)
            If oItm.Declarable Then
                Dim oProvincia As Provincia = oItm.Provincia
                oFile347.AddRegistre(Maxisrvr.MatFileAEAT347.ClausOperacio.Compres, oItm.NIF.Value, oItm.Nom, oProvincia.Mod347, oProvincia.Country.ISO, oItm.Eur)
            End If
        Next

        oDs = DataSetVendes(oMod347)
        oTb = oDs.Tables(0)
        For Each oRow In oTb.Rows
            oItm = GetItemFromDataRow(oRow, oMod347, Mod347Itm.Ops.Ventas)
            If oItm.Declarable Then
                Dim oProvincia As Provincia = oItm.Provincia
                oFile347.AddRegistre(Maxisrvr.MatFileAEAT347.ClausOperacio.Vendes, oItm.NIF.Value, oItm.Nom, oProvincia.Mod347, oProvincia.Country.ISO, oItm.Eur)
            End If
        Next

        Dim oDlg As New SaveFileDialog
        With oDlg
            .DefaultExt = ".txt"
            .FileName = oFile347.DefaultFileName()
            If .ShowDialog = DialogResult.OK Then
                oFile347.SaveAs(.FileName)
            End If
        End With

    End Sub
End Class