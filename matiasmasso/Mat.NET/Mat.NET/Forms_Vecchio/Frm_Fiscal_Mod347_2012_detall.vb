

Public Class Frm_Fiscal_Mod347_2012_detall

    Private mContact As Contact
    Private mYea As Integer
    Private mCod As IOcods

    Private Enum Cols
        Guid
        Fra
        Fch
        Base
        Iva
        Liq
        T1
        T2
        T3
        T4
    End Enum

    Public Sub New(oContact As Contact, iYea As Integer, oCod As IOcods)
        MyBase.New()
        Me.InitializeComponent()
        mContact = oContact
        mYea = iYea
        mCod = oCod
        If mContact IsNot Nothing Then
            Me.Text = "model 347 (" & iYea.ToString & ") " & mContact.Nom
        End If
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = ""

        Select Case mCod
            Case IOcods.Output
                SQL = "SELECT Guid, fra, fch, EurBase, EurLiq - EurBase AS Iva, EurLiq " _
                & ",(CASE WHEN DATEPART(Q,FCH)=1 THEN EURLIQ ELSE 0 END) AS T1 " _
                & ",(CASE WHEN DATEPART(Q,FCH)=2 THEN EURLIQ ELSE 0 END) AS T2 " _
                & ",(CASE WHEN DATEPART(Q,FCH)=3 THEN EURLIQ ELSE 0 END) AS T3 " _
                & ",(CASE WHEN DATEPART(Q,FCH)=4 THEN EURLIQ ELSE 0 END) AS T4 " _
                & "FROM Fra " _
                & "WHERE Emp = @Emp AND yea = @Yea AND cli = @Cli " _
                & "ORDER BY fra"
            Case IOcods.Input
                SQL = "SELECT (CASE WHEN CCA.FileDocument IS NULL THEN NULL ELSE CCA.Guid END) AS Guid, " _
                    & "0 AS CCA, " _
                    & "CCA.fch, " _
                    & "(CASE WHEN BASE.DH=1 THEN BASE.eur ELSE -BASE.EUR END) AS BASE, " _
                    & "(CASE WHEN IVA.DH=1 THEN IVA.eur ELSE -IVA.EUR END) AS IVA, " _
                    & "(CASE WHEN BASE.DH=1 THEN BASE.eur ELSE -BASE.EUR END)+(CASE WHEN IVA.DH=1 THEN IVA.eur ELSE -IVA.EUR END) as EurLiq " _
                & ",(CASE WHEN DATEPART(Q,BASE.FCH)=1 THEN (CASE WHEN BASE.DH=1 THEN BASE.eur ELSE -BASE.EUR END)+(CASE WHEN IVA.DH=1 THEN IVA.eur ELSE -IVA.EUR END) ELSE 0 END) AS T1 " _
                & ",(CASE WHEN DATEPART(Q,BASE.FCH)=2 THEN (CASE WHEN BASE.DH=1 THEN BASE.eur ELSE -BASE.EUR END)+(CASE WHEN IVA.DH=1 THEN IVA.eur ELSE -IVA.EUR END) ELSE 0 END) AS T2 " _
                & ",(CASE WHEN DATEPART(Q,BASE.FCH)=3 THEN (CASE WHEN BASE.DH=1 THEN BASE.eur ELSE -BASE.EUR END)+(CASE WHEN IVA.DH=1 THEN IVA.eur ELSE -IVA.EUR END) ELSE 0 END) AS T3 " _
                & ",(CASE WHEN DATEPART(Q,BASE.FCH)=4 THEN (CASE WHEN BASE.DH=1 THEN BASE.eur ELSE -BASE.EUR END)+(CASE WHEN IVA.DH=1 THEN IVA.eur ELSE -IVA.EUR END) ELSE 0 END) AS T4 " _
                    & "FROM CCA INNER JOIN " _
                    & "CCB IVA ON CCA.emp = IVA.Emp AND CCA.yea = IVA.yea AND CCA.cca = IVA.Cca LEFT OUTER JOIN " _
                    & "(SELECT  B.Emp, B.yea, B.Cca, MAX(B.eur) AS Eur FROM CCB B INNER JOIN " _
                    & "PGCCTA C ON B.PgcPlan = C.PgcPlan AND B.cta LIKE C.Id " _
                    & "WHERE C.IsBaseImponibleIVA = 1 " _
                    & "GROUP BY B.Emp, B.yea, B.Cca) AS XBASE " _
                    & "ON CCA.emp = XBASE.Emp AND CCA.yea = XBASE.yea AND CCA.cca = XBASE.Cca LEFT OUTER JOIN " _
                    & "CCB AS BASE ON BASE.Emp = XBASE.Emp AND BASE.yea = XBASE.yea AND BASE.Cca = XBASE.Cca AND BASE.eur = XBASE.Eur LEFT OUTER JOIN " _
                    & "CliGral ON BASE.Emp = CliGral.emp AND BASE.cli = CliGral.Cli LEFT OUTER JOIN " _
                    & "PGCCTA AS CTABASE ON BASE.PGCPLAN=CTABASE.PGCPLAN AND BASE.CTA LIKE CTABASE.ID " _
                    & "WHERE CCA.emp = @EMP AND CCA.yea =@YEA AND IVA.cta LIKE '47200' AND CCA.CCD<>81 AND base.CLI=@Cli " _
                    & "ORDER BY CCA.auxCca, CCA.fch, CCA.cca"
        End Select

        Dim iContact As Integer = 0
        Dim oEmp as DTOEmp
        If mContact Is Nothing Then
            oEmp =BLL.BLLApp.Emp
        Else
            oEmp = mContact.Emp
            iContact = mContact.Id
        End If

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Emp", oEmp.Id, "@Yea", CurrentYea, "@Cli", iContact)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oDataRowSum As DataRow = oTb.NewRow
        For j As Integer = Cols.Base To Cols.T4
            oDataRowSum(j) = 0
        Next
        For Each oDataRow As DataRow In oTb.Rows
            For j As Integer = Cols.Base To Cols.T4
                If Not IsDBNull(oDataRow(j)) Then
                    oDataRowSum(j) += oDataRow(j)
                End If
            Next
        Next
        oTb.Rows.Add(oDataRowSum)

        With DataGridView1
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With

            With .Columns(Cols.Fra)
                .HeaderText = "Factura"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            With .Columns(Cols.Base)
                .HeaderText = "Base"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
                .Width = 80
            End With

            With .Columns(Cols.Iva)
                .HeaderText = "Iva"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
                .Width = 80
            End With

            With .Columns(Cols.Liq)
                .HeaderText = "Total"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
                .Width = 80
            End With

            With .Columns(Cols.T1)
                .HeaderText = "T1"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
                .DefaultCellStyle.BackColor = Color.LightGray
                .Width = 70
            End With

            With .Columns(Cols.T2)
                .HeaderText = "T2"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
                .DefaultCellStyle.BackColor = Color.LightGray
                .Width = 80
            End With

            With .Columns(Cols.T3)
                .HeaderText = "T3"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
                .DefaultCellStyle.BackColor = Color.LightGray
                .Width = 80
            End With

            With .Columns(Cols.T4)
                .HeaderText = "T4"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
                .DefaultCellStyle.BackColor = Color.LightGray
                .Width = 80
            End With

            With .Rows(.Rows.Count - 1)
                .DefaultCellStyle.BackColor = Color.LightGray
            End With
        End With

    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oFras As Fras = CurrentFras()

        If oFras.Count > 0 Then
            Dim oMenu_Fra As New Menu_Fra(oFras)
            AddHandler oMenu_Fra.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Fra.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Function CurrentYea() As Integer
        Return mYea
    End Function

    Private Function CurrentFra() As Fra
        Dim oFra As Fra = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim LngId As Long = DataGridView1.CurrentRow.Cells(Cols.Fra).Value
            oFra = Fra.FromNum(BLL.BLLApp.Emp, CurrentYea, LngId)
        End If
        Return oFra
    End Function

    Private Function CurrentFras() As Fras
        Dim oFras As New Fras

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim iYea As Integer = CurrentYea()
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                Dim iFra As Integer = oRow.Cells(Cols.Fra).Value
                Dim oFra As Fra = Fra.FromNum(mContact.Emp, iYea, iFra)
                oFras.Add(oFra)
            Next
            oFras.Sort()
        Else
            Dim oFra As Fra = CurrentFra()
            If oFra IsNot Nothing Then
                oFras.Add(CurrentFra)
            End If
        End If
        Return oFras
    End Function

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Fch
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
End Class