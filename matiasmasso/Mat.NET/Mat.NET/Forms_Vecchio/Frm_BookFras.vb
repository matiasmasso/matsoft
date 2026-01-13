

Public Class Frm_BookFras
    Private mAllowEvents As Boolean

    Private Enum Tabs
        Fras
        Ctas
        Mod347
    End Enum

    Private Enum ColsCta
        Plan
        Cta
        CtaNom
        Cli
        CliNom
        Bas
        Sdo
    End Enum

    Private Enum Cols347
        Cli
        Nom
        Nif
        IdProvincia
        NomProvincia
        Q1
        Q2
        Q3
        Q4
        Tot
    End Enum

    Private Sub Frm_BookFras_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim DtFch As Date = Today.AddMonths(-1)
        NumericUpDownYea.Value = DtFch.Year
        ComboBoxBookFraMode.SelectedIndex = BookFrasLoader.Modes.OnlyIva
        Dim oLang as DTOLang = BLL.BLLSession.Current.Lang
        For i = 1 To 12
            ComboBoxMes.Items.Add(oLang.MesAbr(i))
        Next
        ComboBoxMes.SelectedIndex = DtFch.Month - 1
        LoadBookFras()
        mAllowEvents = True
    End Sub

    Private Sub LoadBookFras()
        Dim oBookFras As List(Of BookFra) = BookFrasLoader.All(CurrentMode, CurrentExercici, CurrentMes)
        Xl_BookFras1.Load(oBookFras)
    End Sub

    Private Function CurrentMode() As BookFrasLoader.Modes
        Dim retval As BookFrasLoader.Modes = ComboBoxBookFraMode.SelectedIndex
        Return retval
    End Function

    Private Function CurrentYea() As Integer
        Return NumericUpDownYea.Value
    End Function

    Private Function CurrentExercici() As Exercici
        Dim retval As New Exercici(BLL.BLLApp.Emp, CurrentYea)
        Return retval
    End Function

    Private Function CurrentMes() As Integer
        Dim retval As Integer
        If CheckBoxMes.Checked Then
            retval = ComboBoxMes.SelectedIndex + 1
        End If
        Return retval
    End Function


    Private Sub RefreshRequestCtas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsCta.CliNom
        Dim oGrid As DataGridView = DataGridViewCtas

        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadCtas()

        If oGrid.CurrentRow Is Nothing Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        refrescaTab()
    End Sub

    Private Sub refrescaTab()
        Select Case TabControl1.SelectedIndex
            Case Tabs.Fras
                Static FrasDone As Boolean
                Static iYeaFras As Integer
                If Not (FrasDone And iYeaFras = CurrentYea()) Then
                    LoadBookFras()
                    iYeaFras = CurrentYea()
                    FrasDone = True
                End If
            Case Tabs.Ctas
                Static CtasDone As Boolean
                Static iYea As Integer
                If Not (CtasDone And iYea = CurrentYea()) Then
                    LoadCtas()
                    iYea = CurrentYea()
                    CtasDone = True
                End If
            Case Tabs.Mod347
                Static iYea347 As Integer
                Static Mod347Done As Boolean
                If Not (Mod347Done And iYea347 = CurrentYea()) Then
                    Load347()
                    iYea347 = CurrentYea()
                    Mod347Done = True
                End If
        End Select
    End Sub


    Private Sub LoadCtas()
        Dim SQL As String = "SELECT X.PgcPlan, X.Cta, PGCCTA.Esp, X.Cli, Clx.Clx, SUM(X.Base) AS BASE, SUM(X.SALDO) AS SALDO " _
        & "FROM            (SELECT emp, yea, PgcPlan, Cta, Contact as Cli, Base, 0 AS SALDO " _
                            & "FROM BookFras " _
                            & "UNION " _
                            & "SELECT Emp, yea, PgcPlan, cta, Cli, 0 AS BASE, (CASE WHEN ccb.dh = 1 THEN ccb.eur ELSE - ccb.eur END) AS SALDO " _
                            & "FROM            CCB) AS X INNER JOIN " _
                            & "PGCCTA ON X.PgcPlan = PGCCTA.PgcPlan AND X.Cta = PGCCTA.Id LEFT OUTER JOIN " _
                            & "CLX ON X.EMP=CLX.EMP AND X.Cli=CLX.Cli " _
        & "WHERE        (X.emp = @EMP) AND (X.yea = @YEA) AND (PGCCTA.IsBaseImponibleIVA = 1) " _
        & "GROUP BY X.PgcPlan, X.Cta, PGCCTA.Esp, X.Cli, Clx.Clx " _
        & "HAVING SUM(X.BASE)<>0 OR SUM(X.SALDO)<>0 " _
        & "ORDER BY X.PgcPlan, X.Cta, Clx.Clx"


        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Emp", App.Current.Emp.Id, "@Yea", CurrentYea)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridViewCtas
            With .RowTemplate
                .Height = DataGridViewCtas.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(ColsCta.Plan)
                .Visible = False
            End With

            With .Columns(ColsCta.Cta)
                .HeaderText = "compte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
            End With

            With .Columns(ColsCta.CtaNom)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(ColsCta.Cli)
                .Visible = False
            End With

            With .Columns(ColsCta.CliNom)
                .HeaderText = "proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(ColsCta.Bas)
                .HeaderText = "base imponible"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            With .Columns(ColsCta.Sdo)
                .HeaderText = "sdo.compte"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
        End With
    End Sub

    Private Sub Load347()
        Dim SQL As String = "SELECT BookFras.Contact, CliGral.RaoSocial, CliGral.NIF, PROVINCIA.Mod347, PROVINCIA.Nom, "
        For Q As Integer = 1 To 4
            SQL = SQL & "SUM(CASE WHEN DATEPART(Q, CCA.FCH) = " & Q.ToString & " THEN BookFras.Base + BookFras.IVA ELSE 0 END) AS Q" & Q.ToString & ", "
        Next
        SQL = SQL & "SUM(BookFras.Base + BookFras.Iva) AS TOT " _
        & "FROM            CCA INNER JOIN " _
                                 & "BookFras ON CCA.emp = BookFras.emp AND CCA.yea = BookFras.yea AND CCA.cca = BookFras.cca LEFT OUTER JOIN " _
                                 & "CliGral ON BookFras.emp = CliGral.emp AND BookFras.Contact = CliGral.Cli INNER JOIN " _
                                 & "CliAdr ON CliGral.Guid = CliAdr.SrcGuid AND CliAdr.Cod=1 INNER JOIN " _
                                 & "Zip ON CliAdr.Zip=Zip.Guid INNER JOIN " _
                                 & "Location ON Zip.Location=Location.Guid INNER JOIN " _
                                 & "Zona ON Location.Zona=Zona.Guid INNER JOIN " _
                                 & "PROVINCIA ON Zona.Provincia = PROVINCIA.Guid " _
        & "WHERE BookFras.Irpf=0 " _
        & "GROUP BY BookFras.Contact, CliGral.RaoSocial, CliGral.NIF, PROVINCIA.Mod347, PROVINCIA.Nom " _
        & "HAVING(SUM(BookFras.Base + BookFras.Iva) > @MinValue AND BookFras.Contact <> 0) " _
        & "ORDER BY CliGral.RaoSocial, CliGral.NIF, PROVINCIA.Nom"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@MinValue", CInt(Mod347.MinValue))
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView347
            With .RowTemplate
                .Height = DataGridView347.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols347.Cli)
                .Visible = False
            End With

            With .Columns(Cols347.Nom)
                .HeaderText = "proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols347.Nif)
                .HeaderText = "NIF"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
            End With

            With .Columns(Cols347.IdProvincia)
                .HeaderText = "zip"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
            End With

            With .Columns(Cols347.NomProvincia)
                .HeaderText = "provincia"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
            End With

            With .Columns(Cols347.Q1)
                .HeaderText = "trimestre 1"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            With .Columns(Cols347.Q2)
                .HeaderText = "trimestre 2"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            With .Columns(Cols347.Q3)
                .HeaderText = "trimestre 3"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            With .Columns(Cols347.Q4)
                .HeaderText = "trimestre 4"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            With .Columns(Cols347.Tot)
                .HeaderText = "liquid"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
        End With
    End Sub

    Private Sub DataGridView347_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView347.CellFormatting
        Select Case e.ColumnIndex
            Case Cols347.Nif
                If e.Value = "" Then
                    e.CellStyle.BackColor = maxisrvr.COLOR_NOTOK
                End If
        End Select
    End Sub

    Private Sub DataGridView347_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridView347.SelectionChanged
        If mAllowEvents Then
            SetContextMenu347()
        End If
    End Sub

    Private Function Current347() As Contact
        Dim oItm As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView347.CurrentRow
        If oRow IsNot Nothing Then
            oItm = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, CInt(oRow.Cells(Cols347.Cli).Value))
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu347()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As Contact = Current347()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("proveidor")
            oContextMenuStrip.Items.Add(oMenuItem)
            Dim oMenuContact As New Menu_Contact(oItm)
            oMenuItem.DropDownItems.AddRange(oMenuContact.Range)
        End If

        oMenuItem = New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf Do347_excel)
        oContextMenuStrip.Items.Add(oMenuItem)
        oMenuItem = New ToolStripMenuItem("refresca", Nothing, AddressOf Load347)
        oContextMenuStrip.Items.Add(oMenuItem)

        DataGridView347.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub Do347_Excel()
        MatExcel.GetExcelFromDataGridView(DataGridView347).Visible = True
    End Sub


    Private Sub DataGridViewCtas_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewCtas.CellFormatting
        Select Case e.ColumnIndex
            Case ColsCta.Bas
                Dim oRow As DataGridViewRow = DataGridViewCtas.Rows(e.RowIndex)
                If oRow.Cells(ColsCta.Sdo).Value <> e.Value Then
                    e.CellStyle.BackColor = maxisrvr.COLOR_NOTOK
                End If
        End Select
    End Sub


    Private Sub DataGridViewCtas_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridViewCtas.DoubleClick
        Dim oCcd As Ccd = CurrentCta()
        Dim oContact As Contact = oCcd.Contact
        Dim oCta As PgcCta = oCcd.Cta
        Dim oExercici As New Exercici(BLL.BLLApp.Emp, oCcd.Yea)

        Dim oFrm As New Frm_CliCtas(oContact, oCta, oExercici)
        oFrm.Show()
    End Sub

    Private Sub DataGridViewCtas_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridViewCtas.SelectionChanged
        If mAllowEvents Then
            SetContextMenuCta()
        End If
    End Sub

    Private Function CurrentCta() As Ccd
        Dim oItm As Ccd = Nothing
        Dim oRow As DataGridViewRow = DataGridViewCtas.CurrentRow
        If oRow IsNot Nothing Then
            Dim oPlan As New PgcPlan(CInt(oRow.Cells(ColsCta.Plan).Value))
            Dim oCta As PgcCta = MaxiSrvr.PgcCta.FromNum(oPlan, oRow.Cells(ColsCta.Cta).Value)
            Dim oCce As New Cce(BLL.BLLApp.Emp, oCta, CurrentYea)
            Dim oContact As Contact = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, oRow.Cells(ColsCta.Cli).Value)
            oItm = New Ccd(oCce, oContact)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenuCta()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As Ccd = CurrentCta()
        If oItm IsNot Nothing Then
            Dim oMenu_Ccd As New Menu_Ccd(CurrentCta, BLL.BLLApp.Emp)
            AddHandler oMenu_Ccd.AfterUpdate, AddressOf RefreshRequestCtas
            oContextMenuStrip.Items.AddRange(oMenu_Ccd.Range)
        End If

        oMenuItem = New ToolStripMenuItem("refresca", Nothing, AddressOf RefreshRequestCtas)
        oContextMenuStrip.Items.Add(oMenuItem)

        'oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        DataGridViewCtas.ContextMenuStrip = oContextMenuStrip
    End Sub


    Private Sub NumericUpDownYea_ValueChanged(sender As Object, e As System.EventArgs) Handles NumericUpDownYea.ValueChanged
        If mAllowEvents Then
            refrescaTab()
        End If
    End Sub


    Private Sub CheckBoxMes_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxMes.CheckedChanged
        If mAllowEvents Then
            ComboBoxMes.Enabled = CheckBoxMes.Checked
            LoadBookFras()
        End If
    End Sub

    Private Sub BookFras_ControlChanged(sender As Object, e As EventArgs) Handles _
        ComboBoxMes.SelectedIndexChanged, _
         ComboBoxBookFraMode.SelectedIndexChanged
        LoadBookFras()
    End Sub

    Private Sub Xl_BookFras1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BookFras1.RequestToRefresh
        LoadBookFras()
    End Sub

    Private Sub ButtonExcel_Click(sender As Object, e As EventArgs) Handles ButtonExcel.Click
        Dim oExcelSheet As DTOExcelSheet = BLL_BookFra.Excel(Xl_BookFras1.Values)
        UIHelper.ShowExcel(oExcelSheet)
    End Sub
End Class