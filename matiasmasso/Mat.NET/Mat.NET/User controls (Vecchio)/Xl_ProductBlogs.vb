

Public Class Xl_ProductBlogs
    Private mProduct As Product
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Fch
        Blogger
        Title
    End Enum

    Public WriteOnly Property Product As product
        Set(value As product)
            mProduct = value
            If mProduct IsNot Nothing Then
                LoadGrid()
                mAllowEvents = True
            End If
        End Set
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Guid, Fch, Blogger, Title From ProductBlogs WHERE Product=@Product ORDER BY FCH DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Product", mProduct.Guid.ToString)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oDs.Tables(0).DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With

            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With

            With .Columns(Cols.Blogger)
                .HeaderText = "blogger"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With

            With .Columns(Cols.Title)
                .HeaderText = "titol"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With

        SetContextMenu()
    End Sub

    Private Function CurrentItm() As ProductBlog
        Dim oItm As ProductBlog = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New Guid(oRow.Cells(Cols.Guid).Value.ToString)
            oItm = New ProductBlog(oGuid)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As ProductBlog = CurrentItm()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("display", My.Resources.iExplorer, AddressOf Display)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("copiar enllaç", My.Resources.iExplorer, AddressOf CopyLink)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("copiar Guid", My.Resources.iExplorer, AddressOf CopyGuid)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("eliminar", My.Resources.binoculares, AddressOf Delete)
            oMenuItem.Enabled = oItm.AllowDelete
            oContextMenuStrip.Items.Add(oMenuItem)

        End If

        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oProductBlog As New ProductBlog
        oProductBlog.Product = mProduct

        Dim oFrm As New Frm_ProductBlog(oProductBlog)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_ProductBlog(CurrentItm())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Display()
        UIHelper.ShowHtml(CurrentItm.UrlFromGuid(True))
    End Sub

    Private Sub CopyLink()
        Clipboard.SetDataObject(CurrentItm.UrlFromGuid(True), True)
    End Sub

    Private Sub CopyGuid()
        Clipboard.SetDataObject(CurrentItm.Guid.ToString, True)
    End Sub

    Private Sub Delete()
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta entrada de blog?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = vbOK Then
            Dim oItm As ProductBlog = CurrentItm()
            oItm.Delete()

        End If
        RefreshRequest(Nothing, System.EventArgs.Empty)
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Title
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.CurrentRow IsNot Nothing Then
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

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

End Class
