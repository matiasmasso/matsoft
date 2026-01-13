

Public Class Frm_Diari_ResumMensual
    Private mYea As Integer
    Private mMes As Integer
    Private mFchFrom As Date
    Private mFchTo As Date
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Cols
        Plan
        Cta
        Dsc
        Act
        Deb
        Hab
        Sdo
    End Enum

    Public WriteOnly Property Yea() As Integer
        Set(ByVal value As Integer)
            mYea = value
            If mMes > 0 Then LoadGrid()
        End Set
    End Property

    Public WriteOnly Property Mes() As Integer
        Set(ByVal value As Integer)
            mMes = value
            If mYea > 0 Then LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        mFchFrom = New Date(mYea, mMes, 1)
        mFchTo = New Date(mYea, mMes, Date.DaysInMonth(mYea, mMes))
        Me.Text = "RESUM MENSUAL " & BLL.BLLApp.Lang.Mes(mMes) & " " & mYea

        Dim SQL As String = "SELECT CCB.PGCPLAN, CCB.CTA, CTAS.DSC, CTAS.ACT, " _
        & "SUM(CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE 0 END) AS CARRECS, " _
        & "SUM(CASE WHEN CCB.DH = 2 THEN CCB.EUR ELSE 0 END) AS ABONAMENTS, " _
        & "SUM(CASE WHEN CCB.DH = CTAS.ACT THEN CCB.EUR ELSE -CCB.EUR END) AS SDO " _
        & "FROM CCB LEFT OUTER JOIN " _
        & "(SELECT CTA,DSC,ACT FROM CCC WHERE YEA <=" & mYea & " AND (YEZ = 0 OR YEZ >=" & mYea & ")) AS CTAS ON CTAS.CTA LIKE CCB.CTA " _
        & "WHERE CCB.CTA > '6' AND CCB.EMP = 1 AND CCB.YEA =" & mYea & " AND MONTH(CCB.FCH) = " & mMes & " " _
        & "GROUP BY CCB.CTA, CTAS.DSC, CTAS.ACT " _
        & "ORDER BY CCB.CTA"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Plan)
                .Visible = False
            End With
            With .Columns(Cols.Cta)
                .HeaderText = "compte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
            End With
            With .Columns(Cols.Dsc)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Act)
                .Visible = False
            End With
            With .Columns(Cols.Deb)
                .HeaderText = "carrecs"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Hab)
                .HeaderText = "abonaments"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Sdo)
                .HeaderText = "saldo"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub

    Private Function CurrentCta() As PgcCta
        Dim oCta As PgcCta = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iPlan As Integer = DataGridView1.CurrentRow.Cells(Cols.Plan).Value
            Dim sCta As String = DataGridView1.CurrentRow.Cells(Cols.Cta).Value
            oCta = MaxiSrvr.PgcCta.FromNum(iPlan, sCta)
        End If
        Return oCta
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oCta As PgcCta = CurrentCta()
        Dim oCce As New Cce(mEmp, oCta, mYea)
        root.ShowCceCcds(oCce, mFchFrom, mFchTo)
    End Sub
End Class