

Public Class Frm_CceCcds
    Private mCce As MaxiSrvr.Cce
    Private mActType As PgcCta.Acts
    Private mDs As DataSet
    Private mXDeb As Decimal
    Private mXHab As Decimal
    Private mXSdo As Decimal
    Private mFchFrom As Date = Date.MinValue
    Private BlHideSaldoZero As Boolean = True
    Private mAllowEvents As Boolean

    Private Enum Cols
        Cli
        Clx
        Deb
        Hab
        Sdo
    End Enum

    Public Sub New(oCce As Cce, Optional DtFchFrom As Date = Nothing, Optional DtFchTo As Date = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mCce = oCce
        Me.Text = mCce.Cta.FullNom
        mFchFrom = DtFchFrom
        If DtFchTo <> Nothing Then
            DateTimePicker1.Value = DtFchTo
        End If
        mActType = mCce.Cta.Act
        RefreshRequest(Nothing, New System.EventArgs)
        mAllowEvents = True
    End Sub



    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Clx
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

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT CCB.CLI, CLX.CLX, " _
        & "SUM(CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE 0 END) AS DEB, " _
        & "SUM(CASE WHEN CCB.DH =2 THEN CCB.EUR ELSE 0 END) AS HAB, "

        Dim BlReverseSaldo As Boolean
        Select Case mCce.Cta.Bal
            Case PgcGrup.BalCods.balanç
                Select Case mCce.Cta.Act
                    Case PgcCta.Acts.creditora
                        BlReverseSaldo = True
                End Select
            Case PgcGrup.BalCods.explotacio
                Select Case mCce.Cta.Act
                    Case PgcCta.Acts.creditora
                        BlReverseSaldo = True
                End Select
        End Select

        'If BlReverseSaldo Then
        'SQL = SQL & "SUM(CASE WHEN CCB.DH = 2 THEN CCB.EUR ELSE - CCB.EUR END) AS SDO "
        'Else
        SQL = SQL & "SUM(CASE WHEN CCB.DH = PGCCTA.ACT THEN CCB.EUR ELSE - CCB.EUR END) AS SDO "
        'End If

        SQL = SQL & "FROM CCB INNER JOIN " _
        & "PGCCTA ON Ccb.CtaGuid = PgcCta.Guid LEFT OUTER JOIN " _
        & "CLX ON CCB.Emp = CLX.Emp AND CCB.cli = CLX.cli " _
        & "WHERE CCB.EMP =" & BLLApp.Emp.Id & " AND " _
        & "CCB.YEA =" & mCce.Yea & " AND " _
        & "PgcCta.Id ='" & mCce.Cta.Id & "' AND "
        If mFchFrom = Date.MinValue Then
            SQL = SQL & "CCB.FCH<='" & Format(DateTimePicker1.Value, "yyyyMMdd") & "' "
        Else
            SQL = SQL & "CCB.FCH BETWEEN '" & Format(mFchFrom, "yyyyMMdd") & "' AND '" & Format(DateTimePicker1.Value, "yyyyMMdd") & "' "
        End If
        SQL = SQL & "GROUP BY PGCCTA.ACT, CCB.CLI, CLX "

        If BlHideSaldoZero Then
            SQL = SQL & "HAVING SUM(CASE WHEN CCB.DH = 2 THEN CCB.EUR ELSE - CCB.EUR END)<>0 "
        End If
        SQL = SQL & "ORDER BY CLX"

        mXDeb = 0
        mXHab = 0
        mXSdo = 0
        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            mXDeb += oRow(Cols.Deb)
            mXHab += oRow(Cols.Hab)
            mXSdo += oRow(Cols.Sdo)
        Next
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = mDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Cli)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "subcompte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Deb)
                .HeaderText = "Debe"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Hab)
                .HeaderText = "Haber"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Sdo)
                .HeaderText = "Saldo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With

    End Sub

    Private Function CurrentCcd() As Ccd
        Dim oCcd As Ccd = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim CliId As Long = oRow.Cells(Cols.Cli).Value
            Dim oContact As Contact = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, CliId)
            oCcd = New Ccd(mCce, oContact)
        End If
        Return oCcd
    End Function


    Private Sub Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub


    Private Sub ExcelToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        If mAllowEvents Then
            refreshrequest(sender, e)
        End If
    End Sub

    Private Sub RefrescaToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        refreshrequest(sender, e)
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oCcd As Ccd = CurrentCcd()
        If oCcd IsNot Nothing Then
            Dim oContact As New DTOContact(oCcd.Contact.Guid)
            Dim oCta As New DTOPgcCta(oCcd.Cta.Guid)
            Dim oExercici As New DTOExercici(BLL.BLLApp.Emp, oCcd.Yea)

            Dim oFrm As New Frm_Extracte(oContact, oCta, oExercici)
            oFrm.Show()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCcd As Ccd = CurrentCcd()
        Dim oMenuItem As ToolStripMenuItem

        If oCcd IsNot Nothing Then
            Dim oMenu_Ccd As New Menu_Ccd(oCcd, mCce.Emp)
            AddHandler oMenu_Ccd.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Ccd.Range)

            oContextMenu.Items.Add("-")
        End If

        If CurrentCcd() IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem
            With oMenuItem
                .Text = "oculta comptes saldades"
                .Checked = BlHideSaldoZero
                .CheckOnClick = True
            End With
            AddHandler oMenuItem.Click, AddressOf MenuItem_HideSaldoZero
            oContextMenu.Items.Add(oMenuItem)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub MenuItem_HideSaldoZero(ByVal sender As Object, ByVal e As System.EventArgs)
        BlHideSaldoZero = Not BlHideSaldoZero
        refreshrequest(sender, e)
    End Sub
End Class