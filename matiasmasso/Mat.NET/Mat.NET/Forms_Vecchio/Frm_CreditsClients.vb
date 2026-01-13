

Public Class Frm_CreditsClients
    Private mAllowEvents As Boolean

    Private Enum Cols
        Cli
        Nom
        Concedit
        Risc
        Disposat
        Iban
        Dies
        Fch
    End Enum

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT CLX.cli, CLX.clx, CCC.Eur, X.RISC,(CASE WHEN CCC.EUR>0 THEN X.RISC/CCC.Eur ELSE 0 END) AS DISPOSAT, IBAN.CCC, IBAN.Mandato_DiesDevolucio, IBAN.Mandato_Fch " _
        & "FROM            CLX LEFT OUTER JOIN " _
        & "Credit_ClassificacioClients AS CCC ON CLX.Emp = CCC.Emp AND CLX.cli = CCC.Cli LEFT OUTER JOIN " _
        & "IBAN ON CCC.Emp = IBAN.EMP AND CCC.Cli = IBAN.CLI AND IBAN.COD = 2 AND (IBAN.Caduca_Fch IS NULL OR IBAN.Caduca_Fch > GETDATE()) LEFT OUTER JOIN " _
        & "(SELECT        Emp, cli, SUM(CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) AS RISC " _
        & "FROM Ccb " _
        & "WHERE cta LIKE '43000' AND yea = YEAR(GETDATE()) " _
        & "GROUP BY Emp, cli) AS X ON X.Emp = CLX.Emp AND X.cli = CLX.cli " _
        & "WHERE  CLX.EMP=@EMP AND  (CCC.Eur <> 0 OR X.RISC <> 0) " _
        & "ORDER BY RISC DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim iCredits As Integer = 0
        Dim iClients As Integer = oTb.Rows.Count
        Dim iMandatos As Integer = 0
        Dim DcConcedit As Decimal = 0
        Dim DcRisc As Decimal = 0

        Dim oRow As DataRow = Nothing
        For Each oRow In oTb.Rows
            If Not IsDBNull(oRow(Cols.Risc)) Then
                DcRisc += CDec(oRow(Cols.Risc))
            End If
            If Not IsDBNull(oRow(Cols.Concedit)) Then
                iCredits += 1
                DcConcedit += CDec(oRow(Cols.Concedit))
                If Not IsDBNull(oRow(Cols.Dies)) Then
                    iMandatos += 1
                End If
            End If
        Next

        LabelCredits.Text = Format(iCredits, "#,##0") & " credits concedits per " & Format(DcConcedit, "#,##0.00") & "€ , disposats per " & Format(DcRisc, "#,##0.00")
        LabelMandatos.Text = Format(iMandatos, "#,##0") & " mandatos signats sobre " & Format(iCredits, "#,##0") & " credits concedits (" & Format(100 * iMandatos / iCredits, "0.0") & "%)"
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
            With .Columns(Cols.Nom)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Concedit)
                .HeaderText = "concedit"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Risc)
                .HeaderText = "risc"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Disposat)
                .HeaderText = "disposat"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#%;-#%;#"
            End With
            With .Columns(Cols.Iban)
                .HeaderText = "iban"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 160
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Dies)
                .HeaderText = "dies"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 70
            End With

        End With
    End Sub

    Private Function CurrentContact() As Contact
        Dim oContact As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iId As Integer = DataGridView1.CurrentRow.Cells(Cols.Cli).Value
            oContact = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, iId)
        End If
        Return oContact
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentContact()
        'Dim oMenuItem As ToolStripMenuItem

        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oContact)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)

        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
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

End Class