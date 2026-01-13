Public Class Xl_IncidenciaDocFiles
    Private _Incidencia As DTOIncidencia
    Private _Mode As Modes
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Modes
        Images
        PurchaseTicket
    End Enum

    Private Enum Cols
        Thumbnail
    End Enum

    Public Shadows Sub Load(oIncidencia As DTOIncidencia, oMode As Modes)
        _Incidencia = oIncidencia
        _Mode = oMode
        _ControlItems = New ControlItems
        Dim oFiles As List(Of DTODocFile) = IIf(_Mode = Modes.Images, oIncidencia.DocFileImages, oIncidencia.PurchaseTickets)
        For Each oItem As DTODocFile In oFiles
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
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


    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = 100
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems.ToList.FindAll(Function(x) x.Source.PendingOp <> DTODocFile.PendingOps.Delete)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = True
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = True
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.Thumbnail), DataGridViewImageColumn)
                .DataPropertyName = "Thumbnail"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.NullValue = Nothing
            End With
        End With
        SetContextMenu()
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
            oContextMenu.Items.Add("exportar", Nothing, AddressOf Do_ExportDocFile)
            oContextMenu.Items.Add("copiar enllaç", Nothing, AddressOf Do_CopyLinkDocFile)
            oContextMenu.Items.Add("eliminar", Nothing, AddressOf Do_DeleteDocFile)
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNewDocFile)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentDocFileName(oMime As DTOEnums.MimeCods) As String
        Dim retval As String = "incidencia " & _Incidencia.Id.ToString & ".doc " & DataGridView1.CurrentRow.Index & BLL.MediaHelper.GetExtensionFromMime(oMime)
        Return retval
    End Function

    Private Sub Do_ExportDocFile()
        Dim oDocFile As DTODocFile = CurrentDocFile()
        Dim oDlg As New SaveFileDialog
        With oDlg
            BLL.BLLDocFile.Load(oDocFile, True)
            .FileName = CurrentDocFileName(oDocFile.Mime)
            .Filter = "*" & BLL.MediaHelper.GetExtensionFromMime(oDocFile.Mime) & "| tots els fitxers (*.*)"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim exs As New List(Of Exception)
                If Not BLL.FileSystemHelper.SaveStream(oDocFile.Stream, exs, .FileName) Then
                    UIHelper.WarnError(exs, "no ha estat possible guardar el fitxer")
                End If
            End If
        End With
    End Sub

    Private Sub Do_DeleteDocFile()
        Dim oControlItem As ControlItem = CurrentControlItem()
        oControlItem.Source.PendingOp = DTODocFile.PendingOps.Delete
        LoadGrid()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_ZoomDocFile()
        Dim oDocFile As DTODocFile = CurrentDocFile()

        UIHelper.ShowStream(oDocFile, CurrentDocFileName(oDocFile.Mime))
    End Sub

    Private Sub Do_CopyLinkDocFile()
        Dim oDocFile As DTODocFile = CurrentDocFile()
        'Dim sFilename As String = "incidencia " & mIncidencia.Id.ToString & "." & DataGridViewDocs.CurrentRow.Index & MediaHelper.GetExtensionFromMime(oDoc.MediaObject.Mime)
        Clipboard.SetDataObject(BLL.BLLDocFile.DownloadUrl(oDocFile, True))
    End Sub

    Private Sub Do_AddNewDocFile()
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, "importar justificant", "Images(*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png|*.zip|*.zip|*.pdf|*.pdf|tots els arxius|*.*") Then
            Select Case oDocFile.Mime
                Case DTOEnums.MimeCods.Docx
                    oDocFile.Thumbnail = WordHelper.GetImgFromWordFirstPage(oDocFile.Stream)
            End Select
            oDocFile.PendingOp = DTODocFile.PendingOps.Insert
            Dim oControlItem As New ControlItem(oDocFile)
            _ControlItems.Add(oControlItem)
            LoadGrid()
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTODocFile = CurrentControlItem.Source
        Dim oFrm As New Frm_DocFile(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
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
        Dim oPoint = DataGridView1.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridView1.CurrentCell = oclickedCell
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        Dim exs As New List(Of exception)
        Dim oTargetCell As DataGridViewCell = Nothing
        Dim oDocFiles As List(Of DTODocFile) = Nothing
        Dim oControlItems As New List(Of ControlItem)

        If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
            For Each oDocFile As DTODocFile In oDocFiles
                If oDocFile.Mime = DTOEnums.MimeCods.NotSet Then
                    Dim sFilename As String = BLL.FileSystemHelper.GetFilenameFromPath(oDocFile.Filename)
                    Dim sMessage As String = String.Format("l'arxiu '{0}' no té un format admés", sFilename)
                    Dim ex As New Exception(sMessage)
                    exs.Add(ex)
                Else
                    oDocFile.PendingOp = DTODocFile.PendingOps.Insert
                    Dim oControlItem As New ControlItem(oDocFile)
                    _ControlItems.Add(oControlItem)
                End If
            Next
        End If

        If exs.Count = 0 Then
            _ControlItems.ToList.AddRange(oControlItems)
            LoadGrid()
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        Else
            MsgBox("error al importar documents" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTODocFile

        Property Thumbnail As Image

        Public Sub New(oDocFile As DTODocFile)
            MyBase.New()
            _Source = oDocFile
            With oDocFile
                _Thumbnail = BLL.ImageHelper.GetThumbnailToFill(.Thumbnail, 100, 100)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

