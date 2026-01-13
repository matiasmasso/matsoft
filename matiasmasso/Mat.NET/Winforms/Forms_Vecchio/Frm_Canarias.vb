

Public Class Frm_Canarias
    Private mAllowEvents As Boolean

    Private Enum Cols
        Cli
        Clx
        Fch
        ArtNom
        Qty
    End Enum

    Private Sub Frm_Canarias_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim oMgz As DTOMgz = BLL.BLLApp.Mgz
        Dim SQL As String = "SELECT  dbo.PDC.Cli,dbo.CLX.clx, dbo.PDC.fch, dbo.ART.myD, dbo.PNC.Pn2 " _
        & "FROM            dbo.PNC INNER JOIN " _
                         & "dbo.ART ON dbo.PNC.ArtGuid = dbo.ART.Guid LEFT OUTER JOIN " _
                         & "ArtPn2NoPn3 ON Art.Guid=ArtPn2NoPn3.ArtGuid INNER JOIN " _
                         & "dbo.PDC ON dbo.PNC.PdcGuid = dbo.PDC.Guid INNER JOIN " _
                         & "ArtStock ON ART.Guid = ArtStock.ArtGuid AND ArtStock.MgzGuid = @MgzGuid INNER JOIN " _
                         & "dbo.CliAdr ON dbo.PDC.Emp = dbo.CliAdr.emp AND dbo.PDC.cli = dbo.CliAdr.cli AND dbo.CliAdr.cod = 1 INNER JOIN " _
& "Zip ON CliAdr.Zip=Zip.Guid INNER JOIN " _
& "Location ON Zip.Location=Location.Guid INNER JOIN " _
& "Zona ON Location.Zona=Zona.Guid INNER JOIN " _
& "Provincia ON Zona.Provincia=Provincia.Guid INNER JOIN " _
& "Reg ON Provincia.Regio=Reg.Guid INNER JOIN " _
                         & "dbo.CLX ON dbo.PDC.CliGuid = dbo.CLX.Guid " _
        & "WHERE        (dbo.REG.REGION LIKE 'CANARIAS') AND (dbo.Pdc.Emp = 1) AND (ArtStock.Stock > 0) AND (PNC.PN2 > 0) " _
        & "ORDER BY dbo.CLX.clx, dbo.PDC.fch, dbo.PDC.pdc, dbo.PNC.lin"

        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@EMP", BLLApp.Emp.Id, "@MgzGuid", oMgz.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            .DataSource = oTb
            .RowHeadersVisible = False
            .RowTemplate.Height = .Font.Height * 1.3
            .ColumnHeadersVisible = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AllowUserToResizeRows = False


            With .Columns(Cols.Cli)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Qty)
                .HeaderText = "quant"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Width = 50
            End With
        End With
        mAllowEvents = True
        SetContextMenu()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oContact As Contact = CurrentContact()
        If oContact IsNot Nothing Then
            root.ShowContact(oContact)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Function CurrentContact() As contact
        Dim oContact As contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim CliId As Integer = oRow.Cells(Cols.Cli).Value
            oContact = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, CliId)
        End If
        Return oContact
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentContact

        Dim oMenuItemRefresca As New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequest)
        oContextMenu.Items.Add(oMenuItemRefresca)

        If oContact IsNot Nothing Then
            oContextMenu.Items.Add("-")
            Dim oMenu_Contact As New Menu_Contact(oContact)
            ' AddHandler oMenu_Impagat.AfterUpdate, AddressOf RefreshRequestImpagats
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
        End If


        DataGridView1.ContextMenuStrip = oContextMenu

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


End Class