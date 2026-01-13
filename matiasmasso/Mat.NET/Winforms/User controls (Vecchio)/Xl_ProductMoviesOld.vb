

Public Class Xl_ProductMoviesOld

    Private mProduct As DTOProduct
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        YoutubeId
        Nom
    End Enum

    Public WriteOnly Property Product As DTOProduct
        Set(ByVal value As DTOProduct)
            mProduct = value
            LoadGrid()
        End Set
    End Property

    Public Sub LoadAllMovies()
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        mAllowEvents = False

        Dim SQL As String = "SELECT Y.GUID,Y.YOUTUBEID,Y.NOM FROM YOUTUBE Y INNER JOIN " _
                            & "YOUTUBEPRODUCTS P ON Y.GUID=P.YOUTUBEGUID "
        If mProduct IsNot Nothing Then
            SQL = SQL & " WHERE P.PRODUCTGUID='" & mProduct.Guid.ToString & "' "
        End If
        SQL = SQL & "ORDER BY Y.NOM"

        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))

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

            With .Columns(Cols.YoutubeId)
                .HeaderText = "Id"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With

            With .Columns(Cols.Nom)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With

        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Function CurrentItm() As DTOYouTubeMovie
        Dim oItm As DTOYouTubeMovie = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New Guid(oRow.Cells(Cols.Guid).Value.ToString)
            oItm = New DTOYouTubeMovie(oGuid)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As DTOYouTubeMovie = CurrentItm()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("eliminar", My.Resources.binoculares, AddressOf Delete)
            oMenuItem.Enabled = BLL.BLLYouTubeMovie.AllowDelete(oItm)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("visualitzar a YouTube", Nothing, AddressOf Play)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("copiar enllaç", Nothing, AddressOf CopyLink)
            oContextMenuStrip.Items.Add(oMenuItem)

        End If

        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
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

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oYouTube As New DTOYouTubeMovie
        Dim oFrm As New Frm_Youtube(oYouTube)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_Youtube(CurrentItm())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Play()
        Dim oItm As DTOYouTubeMovie = CurrentItm()
        Dim sUrl As String = BLL.BLLYouTubeMovie.Url_YouTubeSite(oItm)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub CopyLink()
        Dim oItm As DTOYouTubeMovie = CurrentItm()
        Dim sUrl As String = BLL.BLLYouTubeMovie.Url_YouTubeSite(oItm)
        Clipboard.SetDataObject(sUrl, True)
        MsgBox("adreça copiada al portapapers:" & vbCrLf & sUrl, MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub Delete()
        Dim oItm As DTOYouTubeMovie = CurrentItm()
        If BLL.BLLYouTubeMovie.AllowDelete(oItm) Then
            Dim rc As MsgBoxResult = MsgBox("eliminem " & oItm.Nom & "?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                Dim exs As New List(Of Exception)
                If BLL.BLLYouTubeMovie.Delete(oItm, exs) Then
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
        Dim oFrm As New Frm_Youtube(CurrentItm())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
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

