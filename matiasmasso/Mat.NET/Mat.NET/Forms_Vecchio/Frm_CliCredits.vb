

Public Class Frm_CliCredits
    Private mAllowEvents As Boolean

    Private Enum Cols
        CliId
        Clasf
        CliNom
        LastFch
        Consum
        Mandato
        Warn
    End Enum

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT  CC.Cli, CC.Eur, CLX.clx, X.LASTFCH, X.EUR AS Consum, (CASE WHEN IBAN.Mandato_Fch IS NULL THEN 1 ELSE 0 END) AS MANDATO " _
        & "FROM            Credit_ClassificacioClients AS CC INNER JOIN " _
        & "CLX ON CC.Emp = CLX.Emp AND CC.Cli = CLX.cli LEFT OUTER JOIN " _
        & "IBAN ON CC.Emp = IBAN.EMP AND CC.Cli = IBAN.CLI AND (IBAN.Caduca_Fch IS NULL OR IBAN.Caduca_Fch > GETDATE()) LEFT OUTER JOIN " _
        & "(SELECT        CCX.EMP, CCX.CCX, MAX(ALB.fch) AS LASTFCH, SUM(ALB.eur) AS EUR " _
        & "FROM            ALB INNER JOIN " _
        & "CCX ON ALB.CliGuid = CCX.Guid " _
        & "WHERE        (ALB.fch > GETDATE() - 185) " _
        & "GROUP BY CCX.EMP, CCX.CCX) AS X ON CC.Emp = X.EMP AND CC.Cli = X.CCX " _
        & "WHERE        (CC.Emp = 1) " _
        & "GROUP BY CC.Cli, CC.Eur, CLX.clx, X.LASTFCH, X.EUR, IBAN.Mandato_Fch " _
        & "ORDER BY X.LASTFCH"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("WARN", System.Type.GetType("System.Byte[]"))
        'oCol.SetOrdinal(Cols.Warn)

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

            With .Columns(Cols.CliId)
                .Visible = False
            End With

            With .Columns(Cols.Clasf)
                .HeaderText = "Classificació"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            With .Columns(Cols.CliNom)
                .HeaderText = "Client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.LastFch)
                .HeaderText = "ultim albará"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 70
            End With

            With .Columns(Cols.Consum)
                .HeaderText = "Consum 6 mesos"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            With .Columns(Cols.Mandato)
                .Visible = False
            End With

            With .Columns(Cols.Warn)
                .HeaderText = "mandato"
                .Width = 60
            End With
        End With
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Warn
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case oRow.Cells(Cols.Mandato).Value
                    Case 0
                        e.Value = My.Resources.empty
                    Case 1
                        e.Value = My.Resources.warn
                End Select
        End Select
    End Sub

    Private Function CurrentItm() As Contact
        Dim oItm As Object = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iCliId As Integer = CInt(oRow.Cells(Cols.CliId).Value.ToString)
            oItm = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, iCliId)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oContact As Contact = CurrentItm()
        If oContact IsNot Nothing Then
            Dim oMenuContact As New Menu_Contact(oContact)
            oContextMenuStrip.Items.AddRange(oMenuContact.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub


    Private Sub Zoom()
        Dim oFrm As New Frm_Contact(CurrentItm)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

  
    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.CliNom
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