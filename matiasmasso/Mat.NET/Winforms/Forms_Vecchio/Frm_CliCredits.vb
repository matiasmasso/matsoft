

Public Class Frm_CliCredits
    Private mAllowEvents As Boolean

    Private Enum Cols
        CliGuid
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

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Z.CliGuid, Z.Eur, Clx.clx, LastAlb.LastAlbFch, LastAlb.Eur AS Consum, (CASE WHEN IBAN.Mandato_Fch IS NULL THEN 1 ELSE 0 END) AS MANDATO ")
        sb.AppendLine("FROM  (SELECT Y.CliGuid, Y.Eur FROM (SELECT CliGuid, MAX(FchCreated) AS LastFch FROM CliCreditLog GROUP BY CliGuid) X ")
        sb.AppendLine("                                 INNER JOIN CliCreditLog Y ON X.CliGuid=Y.CliGuid AND X.LastFch=Y.FchCreated) Z ")
        sb.AppendLine("INNER JOIN Clx ON Z.CliGuid = Clx.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Clx.Guid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Iban ON Z.CliGuid = Iban.ContactGuid AND (IBAN.Caduca_Fch IS NULL OR IBAN.Caduca_Fch > GETDATE()) ")
        sb.AppendLine("LEFT OUTER JOIN  (SELECT Ccx.CcxGuid, MAX(ALB.fch) AS LastAlbFch, SUM(CASE WHEN Alb.Fch> GETDATE() - 185 THEN Alb.Eur ELSE 0 END) AS Eur ")
        sb.AppendLine("                 FROM Alb INNER JOIN Ccx ON Alb.CliGuid = Ccx.Guid GROUP BY Ccx.CcxGuid) LastAlb ON Z.CliGuid=LastAlb.CcxGuid ")
        sb.AppendLine("WHERE CliGral.Emp=" & BLLApp.Emp.Id & " AND Z.Eur>0 ")
        sb.AppendLine("GROUP BY Z.CliGuid, Z.Eur, Clx.clx, LastAlb.LastAlbFch, LastAlb.Eur, (CASE WHEN IBAN.Mandato_Fch IS NULL THEN 1 ELSE 0 END)")
        sb.AppendLine("ORDER BY LastAlb.LastAlbFch")

        Dim exs As New List(Of Exception)
        Dim SQL As String = sb.ToString
        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, exs)
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

            With .Columns(Cols.CliGuid)
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

    Private Function CurrentItm() As DTOContact
        Dim retval As DTOContact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.CliGuid).Value
            retval = New DTOContact(oGuid)
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oContact As DTOContact = CurrentItm()
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