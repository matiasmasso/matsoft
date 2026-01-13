
Imports System.Data

Public Class Frm_Export_Main
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Fra
        Fch
        Eur
        Nom
        Cee
        Pais
        Pdf
        PdfIco
    End Enum

    Private Sub Frm_Export_Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_Yea1.Yea = Today.Year
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()

        Dim SQL As String = "SELECT Fra.Guid, FRA.fra, FRA.fch, FRA.EurLiq, Cli_Geo3.RaoSocial, " _
                          & "(CASE WHEN Country.CEE = 0 THEN '' ELSE 'CEE' END) AS CEE, Country.Nom_ESP, " _
                          & "(CASE WHEN BIGFILESRC.BIGFILE IS NULL THEN 0 ELSE 1 END) AS PDF " _
        & "FROM            FRA INNER JOIN " _
                                 & "Cli_Geo3 ON FRA.Emp = Cli_Geo3.emp AND FRA.cli = Cli_Geo3.Cli INNER JOIN " _
                                 & "Country ON Cli_Geo3.ISOpais = Country.ISO LEFT OUTER JOIN " _
                                 & "BIGFILESRC ON FRA.Guid = BIGFILESRC.GUID AND BIGFILESRC.SRC =@DocSrc " _
        & "WHERE        (FRA.yea =@Yea ) AND (Cli_Geo3.ISOpais <> 'ES') " _
        & "ORDER BY FRA.fra DESC"

        Dim sDocSrc As String = CInt(DTODocFile.Cods.Dua).ToString
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@DocSrc", sDocSrc, "@Yea", CurrentYea.ToString)
        Dim oTb As DataTable = oDs.Tables(0)


        Dim oColPdfIco As DataColumn = oTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oColPdfIco.SetOrdinal(Cols.PdfIco)

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

            With .Columns(Cols.Guid)
                .Visible = False
            End With

            With .Columns(Cols.Fra)
                .HeaderText = "factura"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Width = 50
            End With

            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
                .Width = 70
            End With

            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            With .Columns(Cols.Nom)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Cee)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
            End With

            With .Columns(Cols.Pais)
                .HeaderText = "pais"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With

            With .Columns(Cols.Pdf)
                .Visible = False
            End With

            With .Columns(Cols.PdfIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .ReadOnly = True
            End With

        End With
    End Sub

    Private Function CurrentYea() As Integer
        Return Xl_Yea1.Yea
    End Function

    Private Function CurrentItm() As Fra
        Dim retval As Fra = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = CType(oRow.Cells(Cols.Guid).Value, Guid)
            retval = New Fra(oGuid)
        End If
        Return retval
    End Function

    Private Function DUA_exists(oGuid As Guid) As Boolean
        Dim SQL As String = "SELECT Bigfile FROM BigfileSrc WHERE Guid=@Guid and Src=@Src "
        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@Guid", oGuid.ToString, "@Src", CInt(DTODocFile.Cods.Dua).ToString)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As Fra = CurrentItm()
        If oItm IsNot Nothing Then

            Dim oMenuItemFra As New ToolStripMenuItem("factura")
            Dim oMenuFra As New Menu_Fra(oItm)
            oMenuItemFra.DropDownItems.AddRange(oMenuFra.Range)
            oContextMenuStrip.Items.Add(oMenuItemFra)

            Dim oMenuItemDua As New ToolStripMenuItem("Dua")
            Dim BlDua As Boolean = DUA_exists(oItm.Guid)
            oContextMenuStrip.Items.Add(oMenuItemDua)

            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.pdf, AddressOf Do_BrowseDUA)
            oMenuItem.Enabled = BlDua
            oMenuItemDua.DropDownItems.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("copiar enllaç", My.Resources.Copy, AddressOf Do_CopyLinkDUA)
            oMenuItem.Enabled = BlDua
            oMenuItemDua.DropDownItems.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("importar", My.Resources.download, AddressOf Do_ImportDUA)
            oMenuItemDua.DropDownItems.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("eliminar", My.Resources.del, AddressOf Do_DeleteDUA)
            oMenuItem.Enabled = BlDua
            oMenuItemDua.DropDownItems.Add(oMenuItem)

        End If

        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.PdfIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Pdf).Value = 0 Then
                    e.Value = My.Resources.empty
                Else
                    e.Value = My.Resources.pdf
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Do_ZoomFra()
    End Sub

    Private Sub Do_ZoomFra()
        Dim oFra As Fra = CurrentItm()
        Dim oFrm As New Frm_Fra(oFra)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_BrowseDUA()
        'Dim oFra As Fra = CurrentItm()
        'Dim oBigfile As New BigFileSrc(DTODocFile.Cods.Dua, oFra.Guid)
        'root.ShowBigFile(oBigfile.BigFile, "DUA " & oFra.FileName)
    End Sub

    Private Sub Do_CopyLinkDUA()
        Dim oFra As Fra = CurrentItm()
        Dim oBigfile As New BigFileSrc(DTODocFile.Cods.Dua, oFra.Guid)
        Dim sUrl As String = oBigfile.RoutingUrl(True)
        Clipboard.SetDataObject(sUrl, True)
        MsgBox("adreça copiada al portapapers:" & vbCrLf & sUrl)
    End Sub

    Private Sub Do_ImportDUA()
        Dim oFra As Fra = CurrentItm()
        Dim oBigFile As New BigFileSrc(DTODocFile.Cods.Dua, oFra.Guid)
        If root.LoadBigFileFromDialog(oBigFile) Then
            oBigFile.Update()
            RefreshRequest(Nothing, EventArgs.Empty)
        End If
    End Sub

    Private Sub Do_DeleteDUA()
        Dim oFra As Fra = CurrentItm()
        Dim oBigfile As New BigFileSrc(DTODocFile.Cods.Dua, oFra.Guid)
        oBigfile.Delete()
        RefreshRequest(Nothing, EventArgs.Empty)
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            i = oRow.Index
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
        SetContextMenu()
    End Sub


    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragOver
        Dim oPoint = DataGridView1.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridView1.CurrentCell = oclickedCell
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        Dim sFileName As String = ""
        Dim oBigfile As New maxisrvr.BigFileNew
        Dim exs as new list(Of Exception)

        If root.GetBigfileFromDatagridDrop(DataGridView1, e, oBigfile, sFileName, exs) Then
            Dim oFra As Fra = CurrentItm()
            Dim rc As MsgBoxResult = MsgBox("importem justificant " & sFileName & vbCrLf & oBigfile.Features & vbCrLf & "a la factura " & oFra.Id & " de " & oFra.Client.AliasOrRaoSocial & "?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                Dim oBigfileSrc As New BigFileSrc(DTODocFile.Cods.Dua, oFra.Guid, oBigfile)
                oBigfileSrc.Update()
                RefreshRequest(sender, e)
            End If
        Else
            MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
        End If

    End Sub

    Private Sub Xl_Yea1_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_Yea1.AfterUpdate
        If mAllowEvents Then
            mAllowEvents = False
            LoadGrid()
            SetContextMenu()
            mAllowEvents = True
        End If
    End Sub


End Class