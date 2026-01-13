

Public Class Frm_Ctas_Select_Old
    Private mCta As PgcCta
    Private mPgcPlan As PgcPlan
    Private mClosed As Boolean
    Private mFontGrup As Font
    Private mFontCta As Font

    Private Enum Cols
        LinCod
        Id
        Esp
        Cat
        Eng
    End Enum

    Private Enum LinCods
        Grup
        Cta
    End Enum

    Public Property Cta() As PgcCta
        Get
            Return mCta
        End Get
        Set(ByVal value As PgcCta)
            mPgcPlan = value.Plan
            LoadGrid(value.Id)
        End Set
    End Property

    Private Sub LoadGrid(ByVal sSearchKey As String)
        mFontCta = DataGridView1.Font
        mFontGrup = New Font(mFontCta, FontStyle.Italic)

        Dim SQL As String = "SELECT 0 AS LINCOD, PGCGRUP.ID, " _
        & "PGCGRUP.ESP,PGCGRUP.CAT,PGCGRUP.ENG FROM PGCGRUP " _
        & "WHERE PGCGRUP.PGCPLAN=" & mPgcPlan.Id & " AND " _
        & "PGCGRUP.ID LIKE '" & sSearchKey & "%' " _
        & "UNION " _
        & "SELECT 1 AS LINCOD, PGCCTA.ID, " _
        & "PGCCTA.ESP,PGCCTA.CAT,PGCCTA.ENG FROM PGCCTA " _
        & "WHERE PGCCta.PgcPlan=" & mPgcPlan.Id & " AND " _
        & "PGCCTA.ID LIKE '" & sSearchKey & "%' " _
        & "ORDER BY ID"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.LinCod)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Esp)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Cat)
                .Visible = False
            End With
            With .Columns(Cols.Eng)
                .Visible = False
            End With
        End With
        Cursor = Cursors.Default

    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Esp
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim sId As String = oRow.Cells(Cols.Id).Value
                Dim sEsp As String = oRow.Cells(Cols.Esp).Value
                Dim sCat As String = oRow.Cells(Cols.Cat).Value
                Dim sEng As String = oRow.Cells(Cols.Eng).Value
                Dim sTxt As String = BLL.BLLApp.Lang.Tradueix(sEsp, sCat, sEng)
                Dim iLevel As Integer = sId.Trim.Length
                Select Case iLevel
                    Case 1
                        e.Value = sId & ". " & sTxt
                        e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
                        e.CellStyle.Font = mFontGrup
                    Case 2
                        e.Value = New String(" ", 4) & sId & ". " & sTxt
                        e.CellStyle.BackColor = Color.FromArgb(210, 210, 210)
                        e.CellStyle.Font = mFontGrup
                    Case 3
                        e.Value = New String(" ", 8) & sId & ". " & sTxt
                        e.CellStyle.BackColor = Color.FromArgb(220, 220, 220)
                        e.CellStyle.Font = mFontGrup
                    Case 4
                        e.Value = New String(" ", 12) & sId & ". " & sTxt
                        e.CellStyle.BackColor = Color.FromArgb(230, 230, 230)
                        e.CellStyle.Font = mFontGrup
                    Case 5
                        e.Value = New String(" ", 16) & sId & " " & sTxt
                        e.CellStyle.BackColor = Color.White
                        e.CellStyle.Font = mFontCta
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Me.Close()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            mClosed = True
            Me.Close()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If Not mClosed Then
            mCta = CurrentCta()
        End If
    End Sub

    Private Function CurrentCta() As PgcCta
        Dim oCta As PgcCta = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If oRow.Cells(Cols.LinCod).Value = LinCods.Cta Then
                Dim sCtaId As String = oRow.Cells(Cols.Id).Value
                oCta = MaxiSrvr.PgcCta.FromNum(mPgcPlan, sCtaId)
            End If
        End If
        Return oCta
    End Function
End Class