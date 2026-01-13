

Public Class Xl_Downloads
    Private mTpa As Tpa = Nothing
    Private mStp As Stp = Nothing
    Private mArt As Art = Nothing
    Private mTarget As Targets = Targets.NotSet
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Targets
        NotSet
        Tpa
        Stp
        Art
    End Enum

    Private Enum Cols
        Guid
        Src
        Img
        Text
        Status
        Obsoleto
    End Enum

    Public WriteOnly Property Tpa() As Tpa
        Set(ByVal value As Tpa)
            mTpa = value
            mTarget = Targets.Tpa
            refresca()
        End Set
    End Property

    Public WriteOnly Property Stp() As Stp
        Set(ByVal value As Stp)
            mStp = value
            mTarget = Targets.Stp
            refresca()
        End Set
    End Property


    Public WriteOnly Property Art() As Art
        Set(ByVal value As Art)
            mArt = value
            mTarget = Targets.Art
            refresca()
        End Set
    End Property

    Private Sub refresca()
        Dim SQL As String = ""
        Dim oDs As DataSet = Nothing

        Select Case mTarget
            Case Targets.Tpa
                SQL = "SELECT U.GUID,U.SRC,S.IMG, L.ESP AS NOM," & Download.StatusCodes.Propi & " AS STATUS, U.OBSOLETO " _
                & "FROM DOWNLOADS U INNER JOIN " _
                & "DOWNLOADSRCS S ON U.SRC=S.ID LEFT OUTER JOIN LANG_RESOURCE L " _
                & "ON L.CLAU LIKE S.LANGRESOURCEKEY " _
                & "WHERE U.EMP=@EMP AND U.TPA=@TPA AND U.STP=0 " _
                & "ORDER BY U.OBSOLETO, U.SRC"
                oDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mTpa.emp.Id, "@TPA", mTpa.Id)
            Case Targets.Stp
                SQL = "SELECT U.GUID,U.SRC,S.IMG, L.ESP AS NOM, " _
                & "(CASE WHEN U.STP=0 THEN " & Download.StatusCodes.Heretat & " ELSE " & Download.StatusCodes.Propi & " END) AS STATUS, U.OBSOLETO " _
                & "FROM DOWNLOADS U INNER JOIN " _
                & "DOWNLOADSRCS S ON U.SRC=S.ID LEFT OUTER JOIN LANG_RESOURCE L " _
                & "ON L.CLAU LIKE S.LANGRESOURCEKEY " _
                & "WHERE U.EMP=@EMP AND U.TPA=@TPA AND (U.STP=@STP OR U.STP=0) AND U.CTG=0 AND U.ART=0 " _
                & "ORDER BY U.OBSOLETO, U.SRC"
                oDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mStp.Tpa.emp.Id, "@TPA", mStp.Tpa.Id, "@STP", mStp.Id)
                 Case Targets.Art
                SQL = "SELECT U.GUID,U.SRC,S.IMG, L.ESP AS NOM, " _
                & "(CASE WHEN U.ART=0 THEN " & Download.StatusCodes.Heretat & " ELSE " & Download.StatusCodes.Propi & " END) AS STATUS, U.OBSOLETO " _
                & "FROM DOWNLOADS U INNER JOIN " _
                & "DOWNLOADSRCS S ON U.SRC=S.ID LEFT OUTER JOIN LANG_RESOURCE L " _
                & "ON L.CLAU LIKE S.LANGRESOURCEKEY " _
                & "WHERE U.EMP=@EMP AND " _
                & "U.TPA=@TPA AND " _
                & "(U.STP=@STP OR U.STP=0) AND " _
                & "(U.ART=@ART OR U.ART=0) " _
                & "ORDER BY U.OBSOLETO, U.SRC"
                oDs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@EMP", mArt.Emp.Id, "@TPA", mArt.Stp.Tpa.Id, "@STP", mArt.Stp.Id, "@ART", mArt.Id)
        End Select
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                '.Height = 48
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToAddRows = False
            .AllowDrop = True

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Src)
                .Visible = False
            End With
            With .Columns(Cols.Img)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Text)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Status)
                .Visible = False
            End With
            With .Columns(Cols.Obsoleto)
                .Visible = False
            End With

        End With

        mAllowEvents = True
        SetContextMenu()
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Text
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim BlObsoleto As Boolean = oRow.Cells(Cols.Obsoleto).Value
                If BlObsoleto Then
                    e.CellStyle.BackColor = Color.LightGray
                Else
                    Dim oStatus As Download.StatusCodes = oRow.Cells(Cols.Status).Value
                    Select Case oStatus
                        Case Download.StatusCodes.Buit
                            e.CellStyle.BackColor = Color.LightGray
                        Case Download.StatusCodes.Heretat
                            e.CellStyle.BackColor = Color.LightYellow
                        Case Download.StatusCodes.Propi
                            e.CellStyle.BackColor = Color.LightGreen
                    End Select
                End If
                e.CellStyle.WrapMode = DataGridViewTriState.True
        End Select
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Function CurrentDownload() As Download
        Dim oDownload As Download = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            oDownload = New Download(oGuid)
        End If
        Return oDownload
    End Function

    Private Function NewDownload() As Download
        Dim oDownload As Download = Nothing
        'Select Case mTarget
        '    Case Targets.Tpa
        'oDownload = New Download(mTpa)
        '    Case Targets.Stp
        'oDownload = New Download(mStp)
        '   Case Targets.Art
        'o() 'Download = New Download(mArt)
        'End Select

        'Dim oBigFile As New maxisrvr.BigFile(System.Guid.NewGuid)
        'oBigFile.SrcCod = maxisrvr.BigFile.SrcCods.UserManual

        'With oDownload
        '.BigFile = oBigFile
        '.Src = New DownloadSrc(0)
        'End With
        Return oDownload
    End Function

    Private Function CurrentDownloadStatus() As Download.StatusCodes
        Dim oStatus As Download.StatusCodes = Download.StatusCodes.Buit
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oStatus = oRow.Cells(Cols.Status).Value
        End If
        Return oStatus
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oDownload As Download = CurrentDownload()
        Dim oMenuItem As ToolStripMenuItem

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "zoom"
            .Image = My.Resources.prismatics
            .Enabled = (CurrentDownloadStatus() <> Download.StatusCodes.Buit)
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemDownloads_Zoom
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "document"
            .Image = My.Resources.pdf
            .Enabled = (CurrentDownloadStatus() <> Download.StatusCodes.Buit)
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemDownloads_Pdf
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "copiar enlace"
            .Image = My.Resources.Copy
            .Enabled = (CurrentDownloadStatus() <> Download.StatusCodes.Buit)
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemDownloads_CopyLink
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "importar nou"
            .Image = My.Resources.clip
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemDownloads_Importar
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "sustituir"
            .Image = My.Resources.REDO
            .Enabled = (CurrentDownloadStatus() = Download.StatusCodes.Propi)
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemDownloads_Sustituir
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub MenuItemDownloads_Zoom()
        Dim oDownload As Download = CurrentDownload()
        ImportaFile(oDownload)
    End Sub

    Private Sub MenuItemDownloads_Importar()
        Dim oDownload As Download = NewDownload()
        ImportaFile(oDownload)
    End Sub

    Private Sub MenuItemDownloads_Pdf()
        Dim oDownload As Download = CurrentDownload()
        UIHelper.ShowStream(oDownload.DocFile)
    End Sub

    Private Sub MenuItemDownloads_CopyLink()
        Dim oDownload As Download = CurrentDownload()
        UIHelper.CopyLink(oDownload.DocFile)
    End Sub

    Private Sub MenuItemDownloads_Sustituir()
        Dim oDownload As Download = CurrentDownload()
        ImportaFile(oDownload)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Text
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.CurrentRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        refresca()

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



    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        e.Effect = DragDropEffects.Copy
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
        Dim exs as New List(Of exception)
        Dim oTargetCell As DataGridViewCell = Nothing
        Dim oDocFiles As List(Of DTODocFile) = Nothing
        If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
            Dim oDownload As Download = CurrentDownload()
            If oDownload Is Nothing Then oDownload = NewDownload()
            oDownload.DocFile = oDocFiles(0)
            ImportaFile(oDownload)
        Else
            UIHelper.WarnError( exs, "error al arrossegar fitxer")
        End If

    End Sub

    Private Sub ImportaFile(ByVal oDownload As Download)
        Dim oFrm As New Frm_Download(oDownload)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

End Class
