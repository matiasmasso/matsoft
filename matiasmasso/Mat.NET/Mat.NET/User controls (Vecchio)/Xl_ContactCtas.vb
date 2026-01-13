

Public Class Xl_ContactCtas
    Private mContact As Contact = Nothing
    Private mCtas() As PgcCtas = Nothing
    Private mAllowEvents As Boolean = False

    Public Shadows Event DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        CtaGuid
        Plan
        Id
        Nom
        Esp
        Cat
        Eng
        Deb
        Hab
        Sdo
        Cod
    End Enum

    Public WriteOnly Property Contact() As Contact
        Set(ByVal value As Contact)
            mContact = value
            LoadYeas()
            LoadCtas()
        End Set
    End Property

    Private Sub LoadYeas()

        Dim SQL As String = "SELECT Cca.Yea FROM Ccb INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid "
        If mContact IsNot Nothing Then
            SQL = SQL & "WHERE ContactGuid='" & mContact.Guid.ToString & "' "
        End If
        SQL = SQL & "GROUP BY Cca.Yea ORDER BY Cca.Yea DESC"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False

            With .Columns(0)
                .HeaderText = "any"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
        mAllowEvents = True
    End Sub

    Private Sub LoadCtas()
        Dim SQL As String = "SELECT Ccb.CtaGuid,CCB.PGCPLAN,CCB.CTA,'',PGCCTA.ESP,PGCCTA.CAT,PGCCTA.ENG, " _
        & "SUM(CASE WHEN CCB.DH=1 THEN EUR ELSE 0 END) AS DEB, " _
        & "SUM(CASE WHEN CCB.DH=2 THEN EUR ELSE 0 END) AS HAB, " _
        & "SUM(CASE WHEN CCB.DH=PGCCTA.ACT THEN EUR ELSE -EUR END) AS SDO, " _
        & "PGCCTA.COD " _
        & "FROM CCB INNER JOIN " _
        & "PGCCTA ON Ccb.CtaGuid = PgcCta.Guid " _
        & "WHERE CCB.YEA=@YEA "

        If mContact Is Nothing Then
            SQL = SQL & "AND ContactGuid IS NULL "
        Else
            SQL = SQL & "AND ContactGuid = '" & mContact.Guid.ToString & "' "
        End If

        SQL = SQL & "GROUP BY Ccb.CtaGuid,CCB.PGCPLAN, CCB.CTA,PGCCTA.ESP,PGCCTA.CAT,PGCCTA.ENG,PGCCTA.COD " _
        & "ORDER BY CCB.PGCPLAN DESC, CCB.CTA"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@YEA", CurrentYea)
        Dim oTb As DataTable = oDs.Tables(0)

        mAllowEvents = False
        With DataGridView2
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToResizeRows = False

            With .Columns(Cols.CtaGuid)
                .Visible = False
            End With
            With .Columns(Cols.Plan)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "compte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
            End With
            With .Columns(Cols.Nom)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Esp)
                .Visible = False
            End With
            With .Columns(Cols.Cat)
                .Visible = False
            End With
            With .Columns(Cols.Eng)
                .Visible = False
            End With
            With .Columns(Cols.Deb)
                .HeaderText = "deure"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.BackColor = Color.LightGray
            End With
            With .Columns(Cols.Hab)
                .HeaderText = "haver"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.BackColor = Color.LightGray
            End With
            With .Columns(Cols.Sdo)
                .HeaderText = "saldo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Cod)
                .Visible = False
            End With

        End With
        mAllowEvents = True
        SetContextMenu()
    End Sub


    Private Sub DataGridView2_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Nom
                Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Dim oLang As DTOLang = BLL.BLLApp.Lang
                e.Value = oLang.Tradueix(oRow.Cells(Cols.Esp).Value, oRow.Cells(Cols.Cat).Value, oRow.Cells(Cols.Eng).Value)
            Case Cols.Sdo
                Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Dim oCod As DTOPgcPlan.Ctas = CType(oRow.Cells(Cols.Cod).Value, DTOPgcPlan.Ctas)
                Select Case oCod
                    Case DTOPgcPlan.ctas.Vendes
                        e.CellStyle.BackColor = Color.LightGreen
                    Case DTOPgcPlan.ctas.impagats
                        e.CellStyle.BackColor = Color.Salmon

                End Select
        End Select
    End Sub

    Private Function CurrentYea() As Integer
        Dim RetVal As Integer = 0
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            RetVal = oRow.Cells(0).Value
        End If
        Return RetVal
    End Function

    Private Function CurrentCta() As PgcCta
        Dim RetVal As PgcCta = Nothing
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            Dim oCtaGuid As Guid = (oRow.Cells(Cols.CtaGuid).Value)
            RetVal = New PgcCta(oCtaGuid)
        End If
        Return RetVal
    End Function

    Private Function CurrentCcd() As Ccd
        Dim oCcd As Ccd = Nothing
        Dim oCta As PgcCta = CurrentCta()
        If oCta IsNot Nothing Then
            oCcd = New Ccd(mContact, CurrentYea, CurrentCta)
        End If
        Return oCcd
    End Function

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            LoadCtas()
        End If
    End Sub

    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        Dim oCcd As Ccd = CurrentCcd()
        If oCcd IsNot Nothing Then
            RaiseEvent DoubleClick(oCcd, EventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCcd As Ccd = CurrentCcd()

        If oCcd IsNot Nothing Then
            Dim oMenu_Ccd As New Menu_Ccd(oCcd, mContact.Emp)
            'AddHandler oMenu_Ccd.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Ccd.Range)
        End If

        DataGridView2.ContextMenuStrip = oContextMenu
    End Sub
End Class
