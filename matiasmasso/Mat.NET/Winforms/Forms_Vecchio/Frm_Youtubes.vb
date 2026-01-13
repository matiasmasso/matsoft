

Public Class Frm_Youtubes
    Private mAllowEvents As Boolean
    Private mSelectionMode As BLL.Defaults.SelectionModes

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        YoutubeId
        Fch
        Nom
        Obsoleto
    End Enum

    Public Sub New(Optional ByVal oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        mSelectionMode = oSelectionMode
    End Sub

    Private Sub Frm_Youtubes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT GUID,YOUTUBEID,FchCreated,NOM, Obsoleto FROM YOUTUBE ORDER BY FchCreated DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)

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
            With .Columns(Cols.Obsoleto)
                .Visible = False
            End With

            With .Columns(Cols.YoutubeId)
                .HeaderText = "Id"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With

            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With

            With .Columns(Cols.Nom)
                .HeaderText = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
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

            oMenuItem = New ToolStripMenuItem("display", My.Resources.iExplorer, AddressOf Display)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("copiar enllaç", My.Resources.iExplorer, AddressOf CopyYoutubeLink)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("copiar Guid", My.Resources.iExplorer, AddressOf CopyGuid)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("eliminar", My.Resources.binoculares, AddressOf Delete)
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

    Private Sub Display()
        Dim oYouTube As DTOYouTubeMovie = CurrentItm()
        Dim sUrl As String = BLL.BLLYouTubeMovie.Url_YouTubeSite(oYouTube)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub CopyYoutubeLink()
        Dim oYouTube As DTOYouTubeMovie = CurrentItm()
        Dim sUrl As String = BLL.BLLYouTubeMovie.Url_YouTubeSite(oYouTube)
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub CopyGuid()
        Clipboard.SetDataObject(CurrentItm.Guid.ToString, True)
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
            MsgBox("cal buidar-lo primer de productes", MsgBoxStyle.Exclamation)
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

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If mSelectionMode = BLL.Defaults.SelectionModes.Selection Then
            Dim oYoutube As DTOYouTubeMovie = CurrentItm()
            RaiseEvent AfterSelect(oYoutube, EventArgs.Empty)
            Me.Close()
        Else
            Zoom()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim BlObsoleto As Boolean = oRow.Cells(Cols.Obsoleto).Value
        If BlObsoleto Then
            oRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray
        End If
    End Sub
End Class