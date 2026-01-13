
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_AdmDescuadrePendents

    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDs As DataSet
    Private mRol As Rols

    Private Enum Cols
        Cli
        Clx
        Sdo
        Cash
        Pnd
        Dif
    End Enum

    Public Enum Rols
        Clients
        Proveidors
    End Enum

    Public Sub New(ByVal oRol As Rols)
        MyBase.New()
        Me.InitializeComponent()
        mRol = oRol
        LoadGrid()
    End Sub


    Private Sub LoadGrid()
        'saldo del compte

        Dim oCta As PgcCta = Nothing
        Select Case mRol
            Case Rols.Clients
                oCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.clients)
            Case Rols.Proveidors
                oCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.proveidorsEur)
        End Select

        Dim sYea As String = Today.Year.ToString

        Dim SQLCCB As String = ""

        SQLCCB = "SELECT CCB.EMP, CCB.CTA, CCB.CLI, " _
        & "(CASE WHEN dh = @Dh THEN CCB.eur ELSE - ccb.eur END) AS SDO," _
        & "0 AS CASH," _
        & "0 as PNDEUR " _
        & "FROM CCB WHERE YEA=@Yea "


        'pendents
        Dim sSaldat As String = CInt(Pnd.StatusCod.saldat).ToString
        Dim SQLPND As String = "SELECT PND.EMP, PND.CTA, PND.CLI, " _
        & "0 AS SDO, " _
        & "0 AS CASH, " _
        & "PND.EUR AS PNDEUR " _
        & "FROM PND " _
        & "WHERE PND.CTA = @Cta AND PND.STATUS<" & sSaldat & " "


        Dim SQL As String = "SELECT U.CLI, CLX.CLX, SUM(U.SDO), 0 AS CASH, SUM(U.PNDEUR),SUM(U.SDO)-SUM(U.PNDEUR) AS DIF FROM " _
        & "(" & SQLCCB & " UNION ALL " & SQLPND & ") U INNER JOIN " _
        & "CLX ON U.EMP=CLX.EMP AND U.CLI=CLX.CLI " _
        & "WHERE U.EMP=@EMP AND U.CTA LIKE @Cta " _
        & "GROUP BY U.EMP,U.CLI,CLX.CLX " _
        & "HAVING SUM(U.SDO)<>SUM(U.PNDEUR) " _
        & "ORDER BY DIF DESC"

        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@yea", sYea, "@Dh", CInt(oCta.Act).ToString, "@Cta", oCta.Id)
        Dim oTb As DataTable = mDs.Tables(0)
        Me.Text = "DESCUADRE PENDENTS (" & oTb.Rows.Count & " CLIENTS)"

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

            With .Columns(Cols.Cli)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .Width = 50
                .HeaderText = "compte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Sdo)
                .Width = 70
                .HeaderText = "saldo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Cash)
                If mRol = Rols.Clients Then
                    .Width = 70
                    .HeaderText = "cash"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    .Visible = False
                End If
            End With
            With .Columns(Cols.Pnd)
                .HeaderText = "pendent"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Dif)
                .HeaderText = "diferencia"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub

    Private Function CurrentContact() As Contact
        Dim retval As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iId As Integer = DataGridView1.CurrentRow.Cells(Cols.Cli).Value
            retval = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, iId)
        End If
        Return retval
    End Function

    Private Sub MenuItem_Refresca(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadGrid()
    End Sub

    Private Sub MenuItem_Cuadra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.clients)
        Dim IntYea As Integer = Today.Year
        Dim oCcd As New Ccd(CurrentContact, IntYea, oCta)
        Dim oDs As DataSet = oCcd.CuadraDataset

        Dim oApp As Excel.Application = MatExcel.GetExcelFromDataset(oDs)
        Dim oSheet As Excel.Worksheet = oApp.ActiveSheet
        Dim oRange As Excel.Range

        oRange = oSheet.Columns("A:C")
        oRange.EntireColumn.AutoFit()

        oRange = oSheet.Columns("D:E")
        oRange.NumberFormat = "#.##0,00;#.##0,00;#"

        oRange = oSheet.Columns("F")
        oRange.Delete()

        oApp.Visible = True
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentContact()
        Dim oMenuItem As ToolStripMenuItem

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "refresca"
            .Image = My.Resources.refresca
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItem_Refresca
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "cuadra"
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItem_Cuadra
        oContextMenu.Items.Add(oMenuItem)

        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oContact)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)

            oContextMenu.Items.Add("-")
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Clx

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

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_CliCtas(CurrentContact)
        AddHandler oFrm.afterupdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub



    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        setcontextmenu()
    End Sub


End Class
