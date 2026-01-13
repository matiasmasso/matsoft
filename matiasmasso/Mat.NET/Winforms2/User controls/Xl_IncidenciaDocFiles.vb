Public Class Xl_IncidenciaDocFiles
    Private _Incidencia As DTOIncidencia
    Private _Modes As DTOIncidencia.AttachmentCods()
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Thumbnail
    End Enum

    Public Shadows Sub Load(oIncidencia As DTOIncidencia, oModes As DTOIncidencia.AttachmentCods())
        _Incidencia = oIncidencia
        _Modes = oModes

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        Dim oFiles = _Incidencia.Attachments(_Modes)
        For Each oItem As DTODocFile In oFiles
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(DataGridView1)
        DataGridView1.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(DataGridView1, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Values As List(Of DTODocFile)
        Get
            Dim retval As New List(Of DTODocFile)
            For Each oControlItem As ControlItem In _ControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property


    Private Sub SetProperties()
        With DataGridView1
            With .RowTemplate
                .Height = 100
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = True
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = True
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Thumbnail), DataGridViewImageColumn)
                .DataPropertyName = "Thumbnail"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.NullValue = Nothing
                .ImageLayout = DataGridViewImageCellLayout.Zoom
            End With
        End With
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTODocFile)
        Dim retval As New List(Of DTODocFile)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Function CurrentDocFile() As DTODocFile
        Dim retval As DTODocFile = Nothing
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            retval = oControlItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oDocFile As DTODocFile = CurrentDocFile()

        If oDocFile IsNot Nothing Then
            oContextMenu.Items.Add("zoom", Nothing, AddressOf Do_ZoomDocFile)
            oContextMenu.Items.Add("fitxa", Nothing, AddressOf Do_Ficha)
            oContextMenu.Items.Add("exportar", Nothing, AddressOf Do_ExportDocFile)
            oContextMenu.Items.Add("copiar enllaç", Nothing, AddressOf Do_CopyLinkDocFile)
            oContextMenu.Items.Add("eliminar", Nothing, AddressOf Do_DeleteDocFile)
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNewDocFile)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentDocFileName(oMime As MimeCods) As String
        Dim oDocfile = CurrentControlItem().Source
        Dim retval = _Incidencia.DocfileName(oDocfile)
        Return retval
    End Function

    Private Async Sub Do_ExportDocFile()
        Dim exs As New List(Of Exception)
        Dim oDocFile As DTODocFile = CurrentDocFile()
        Dim oDlg As New SaveFileDialog
        With oDlg
            Dim oStream = Await FEB.DocFile.StreamOrDownload(oDocFile, exs)
            If exs.Count = 0 Then
                .FileName = CurrentDocFileName(oDocFile.mime)
                .Filter = "*" & MediaHelper.GetExtensionFromMime(oDocFile.mime) & "| tots els fitxers (*.*)"
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    If Not FileSystemHelper.SaveStream(oStream, exs, .FileName) Then
                        UIHelper.WarnError(exs, "no ha estat possible guardar el fitxer")
                    End If
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End With
    End Sub

    Private Sub Do_DeleteDocFile()
        Dim oControlItem As ControlItem = CurrentControlItem()
        _ControlItems.Remove(oControlItem)
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Async Sub Do_ZoomDocFile()
        Dim exs As New List(Of Exception)
        Dim oDocFile As DTODocFile = CurrentDocFile()
        If oDocFile Is Nothing Then
            UIHelper.WarnError("fitxer buit")
        ElseIf oDocFile.IsVideo Then
            Dim oFrm As New Frm_WindowsMediaPlayer(oDocFile)
            oFrm.Show()
        Else
            Dim sFilename = CurrentDocFileName(oDocFile.mime)
            If Not Await UIHelper.ShowStreamAsync(exs, oDocFile, sFilename) Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Do_Ficha()
        Dim oSelectedValue As DTODocFile = CurrentControlItem.Source
        Dim oFrm As New Frm_DocFile(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLinkDocFile()
        Dim oDocFile As DTODocFile = CurrentDocFile()
        'Dim sFilename As String = "incidencia " & mIncidencia.Id.ToString & "." & DataGridViewDocs.CurrentRow.Index & MediaHelper.GetExtensionFromMime(oDoc.MediaObject.Mime)
        Clipboard.SetDataObject(FEB.DocFile.DownloadUrl(oDocFile, True))
    End Sub

    Private Sub Do_AddNewDocFile()
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, "importar justificant", "Images(*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png|Videos|*.mov;*.mp4|*.Zip|*.Zip|*.pdf|*.pdf|tots els arxius|*.*") Then
            Select Case oDocFile.Mime
                Case MimeCods.Docx
                    Dim exs As New List(Of Exception)
                    oDocFile.Thumbnail = LegacyHelper.WordHelper.GetImgFromWordFirstPage(oDocFile.Stream, exs)
            End Select
            Dim oControlItem As New ControlItem(oDocFile)
            _ControlItems.Add(oControlItem)
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Do_ZoomDocFile()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()

    End Sub


    Private Sub DataGridView1_DragEnt(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles _
    DataGridView1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            '    or none of the above
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragOver
        Dim oPoint = DataGridView1.PointToClient(New System.Drawing.Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridView1.CurrentCell = oclickedCell
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        Dim exs As New List(Of Exception)
        Dim oTargetCell As DataGridViewCell = Nothing
        Dim oDocFiles As List(Of DTODocFile) = Nothing
        Dim oControlItems As New List(Of ControlItem)

        If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
            For Each oDocFile As DTODocFile In oDocFiles
                If oDocFile.mime = MimeCods.NotSet Then
                    Dim sFilename As String = FileSystemHelper.GetFilenameFromPath(oDocFile.filename)
                    Dim sMessage As String = String.Format("l'arxiu '{0}' no té un format admés", sFilename)
                    Dim ex As New Exception(sMessage)
                    exs.Add(ex)
                Else
                    Dim oControlItem As New ControlItem(oDocFile)
                    _ControlItems.Add(oControlItem)
                End If
            Next
        End If

        If exs.Count = 0 Then
            _ControlItems.ToList.AddRange(oControlItems)
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        Else
            MsgBox("error al importar documents" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTODocFile

        Property Thumbnail As System.Drawing.Image

        Public Sub New(oDocFile As DTODocFile)
            MyBase.New()
            _Source = oDocFile
            With oDocFile
                If .Thumbnail Is Nothing Then
                    Dim oImage = LegacyHelper.ImageHelper.FromBytes(.Stream)
                    _Thumbnail = LegacyHelper.ImageHelper.GetThumbnailToFill(oImage, 100, 100)
                Else
                    Dim oImage = LegacyHelper.ImageHelper.FromBytes(.Thumbnail)
                    _Thumbnail = LegacyHelper.ImageHelper.GetThumbnailToFill(oImage, 100, 100)
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

