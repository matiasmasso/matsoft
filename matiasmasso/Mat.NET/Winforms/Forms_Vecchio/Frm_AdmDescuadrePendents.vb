
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

        Dim oCta As DTOPgcCta = Nothing
        Select Case mRol
            Case Rols.Clients
                oCta = BLLPgcCta.FromCod(DTOPgcPlan.Ctas.Clients)
            Case Rols.Proveidors
                oCta = BLLPgcCta.FromCod(DTOPgcPlan.Ctas.ProveidorsEur)
        End Select

        Dim SQLCCB As String = ""
        Dim sYea As String = Today.Year.ToString
        Dim sDH As String = CInt(oCta.Act).ToString

        SQLCCB = "SELECT CCB.CtaGuid, CCB.ContactGuid, " _
        & "(CASE WHEN dh = '" & sDH & "' THEN CCB.eur ELSE - ccb.eur END) AS SDO," _
        & "0 AS CASH," _
        & "0 as PNDEUR " _
        & "FROM CCB WHERE YEA=" & sYea & " "


        'pendents
        Dim sSaldat As String = CInt(DTOPnd.StatusCod.saldat).ToString
        Dim SQLPND As String = "SELECT PND.CtaGuid, PND.ContactGuid, " _
        & "0 AS SDO, " _
        & "0 AS CASH, " _
        & "PND.EUR AS PNDEUR " _
        & "FROM PND " _
        & "WHERE PND.CtaGuid = '" & oCta.Guid.ToString & "' AND PND.STATUS<" & sSaldat & " "


        Dim SQL As String = "SELECT U.ContactGuid, CLX.CLX, SUM(U.SDO), 0 AS CASH, SUM(U.PNDEUR),SUM(U.SDO)-SUM(U.PNDEUR) AS DIF FROM " _
        & "(" & SQLCCB & " UNION ALL " & SQLPND & ") U INNER JOIN " _
        & "CLX ON U.ContactGuid=CLX.Guid " _
        & "INNER JOIN CliGral ON Clx.Guid = CliGral.Guid " _
        & "WHERE CliGral.EMP=" & CInt(BLLApp.Emp.Id) & " AND U.CtaGuid = '" & oCta.Guid.ToString & "'  " _
        & "GROUP BY U.ContactGuid, CLX.CLX " _
        & "HAVING SUM(U.SDO)<>SUM(U.PNDEUR) " _
        & "ORDER BY DIF DESC"


        mDs = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = mDs.Tables(0)

        If mRol = Rols.Clients Then
            Me.Text = "DESCUADRE PENDENTS (" & oTb.Rows.Count & " CLIENTS)"
        Else
            Me.Text = "DESCUADRE PENDENTS (" & oTb.Rows.Count & " PROVEIDORS)"
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

    Private Function CurrentContact() As DTOContact
        Dim retval As DTOContact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Cli).Value
            retval = New DTOContact(oGuid)
        End If
        Return retval
    End Function

    Private Sub MenuItem_Refresca(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadGrid()
    End Sub

    Private Sub MenuItem_Cuadra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCta As DTOPgcCta = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.Clients)
        Dim oExercici As DTOExercici = BLL.BLLExercici.FromYear(Today.Year)
        Dim oCcd As New DTOCcd(oExercici, oCta, CurrentContact)
        Dim oDs As DataSet = CuadraDataset(oCcd)
        Dim oSheet As DTOExcelSheet = BLLExcel.Sheet(oDs)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Public Function CuadraDataset(oCcd As DTOCcd) As DataSet
        Dim SQLDEB As String = "(CASE WHEN DH = 1 THEN EUR ELSE 0 END) AS DEB, "
        Dim SQLHAB As String = "(CASE WHEN DH = 2 THEN EUR ELSE 0 END) AS HAB, "

        Dim SQL As String = "SELECT CCA.cca, " _
        & "CCA.fch, " _
        & "CCA.txt, "

        Dim oActType As DTOPgcCta.Acts = oCcd.Cta.Act
        Select Case oActType
            Case DTOPgcCta.Acts.Deutora
                SQL = SQL & SQLDEB & SQLHAB
            Case DTOPgcCta.Acts.Creditora
                SQL = SQL & SQLDEB & SQLHAB
        End Select

        SQL = SQL & "CCD " _
        & "FROM CCB INNER JOIN " _
        & "CCA ON Ccb.CcaGuid = Cca.Guid " _
        & "WHERE CCB.CtaGuid ='" & oCcd.Cta.Guid.ToString & "' And " _
        & "Ccb.ContactGuid =" & oCcd.Contact.Guid.ToString & " " _
        & "ORDER BY CCA.YEA DESC, CCA.FCH desc, CCA.CCA desc"

        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oColDel As New DataColumn("DEL", System.Type.GetType("System.Boolean"))
        oTb.Columns.Add(oColDel)

        Dim oRowI As DataRow
        Dim oRowJ As DataRow
        Dim DblI As Decimal
        Dim DblJ As Decimal
        Dim i As Integer
        Dim j As Integer
        For i = 0 To oTb.Rows.Count - 2
            oRowI = oTb.Rows(i)
            DblI = IIf(oActType = DTOPgcCta.Acts.Deutora, oRowI("HAB"), oRowI("DEB"))
            If DblI <> 0 Then
                For j = i + 1 To oTb.Rows.Count - 1
                    oRowJ = oTb.Rows(j)
                    If IsDBNull(oRowJ("DEL")) Then
                        DblJ = IIf(oActType = DTOPgcCta.Acts.Deutora, oRowJ("DEB"), oRowJ("HAB"))
                        If DblI = DblJ Then
                            oTb.Rows(i)("DEL") = True
                            oTb.Rows(j)("DEL") = True
                            Exit For
                        End If
                    End If
                Next
            End If
        Next

        For i = oTb.Rows.Count - 1 To 0 Step -1
            If IsDBNull(oTb.Rows(i)("DEL")) Then
                If Not IsDBNull(oTb.Rows(i)("CCD")) Then
                    If oTb.Rows(i)("CCD") = DTOCca.CcdEnum.AperturaExercisi Then
                        oTb.Rows.RemoveAt(i)
                    End If
                End If
            Else
                oTb.Rows.RemoveAt(i)
            End If
        Next

        Return oDs
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As DTOContact = CurrentContact()
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
        Dim oFrm As New Frm_Extracte(CurrentContact)
        AddHandler oFrm.afterupdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub



    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        setcontextmenu()
    End Sub


End Class
