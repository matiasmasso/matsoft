

Public Class Xl_BigFile
    Private mBigFile As New BigFileNew
    Private mSuggestedFilename As String = ""
    Private mIsDirty As Boolean = False
    Public Event ShowLog(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property BigFile() As maxisrvr.BigFileNew
        Get
            Return mBigFile
        End Get
        Set(ByVal value As maxisrvr.BigFileNew)
            If value IsNot Nothing Then
                mBigFile = value
                Refresca()
            End If
        End Set
    End Property

    Public ReadOnly Property IsDirty() As Boolean
        Get
            Return mIsDirty
        End Get
    End Property

    Public WriteOnly Property SuggestedFilename() As String
        Set(ByVal value As String)
            mSuggestedFilename = value
        End Set
    End Property

    Private Sub Refresca()
        If mBigFile.Stream Is Nothing Then
            PictureBox1.Image = Nothing
            TextBox1.Clear()
            ZoomToolStripMenuItem.Enabled = False
            EliminarToolStripMenuItem.Enabled = False
            ExportarToolStripMenuItem.Enabled = False
            SeleccionarPaginesToolStripMenuItem.Enabled = False
        Else
            PictureBox1.Image = GetDisplayImage()
            TextBox1.Text = mBigFile.Features
            ZoomToolStripMenuItem.Enabled = True
            EliminarToolStripMenuItem.Enabled = True
            ExportarToolStripMenuItem.Enabled = True
            Select Case mBigFile.MimeCod
                Case DTOEnums.MimeCods.Pdf
                    SeleccionarPaginesToolStripMenuItem.Enabled = True
                Case Else
                    SeleccionarPaginesToolStripMenuItem.Enabled = False
            End Select
        End If

    End Sub


    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        If mBigFile.Img Is Nothing Then
            Dim oBigfileSrc As New MaxiSrvr.BigFileSrc(DTODocFile.Cods.NotSet, mBigFile.Guid, mBigFile)
            root.LoadBigFileFromDialog(oBigfileSrc)
            mIsDirty = True
            RaiseEvent AfterUpdate(mBigFile, New System.EventArgs)
            Refresca()
        Else
            root.ShowBigFile(mBigFile)
        End If
    End Sub

    Private Sub ImportarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportarToolStripMenuItem.Click
        Dim oBigfileSrc As New BigFileSrc(DTODocFile.Cods.NotSet, mBigFile.Guid, mBigFile)
        If root.LoadBigFileFromDialog(oBigfileSrc) Then
            GetImgFromStream()
            mIsDirty = True
            RaiseEvent AfterUpdate(mBigFile, New System.EventArgs)
            Refresca()
        End If
    End Sub

    Private Function GetImgFromStream() As Image
        Dim oImg As Image = Nothing
        Select Case mBigFile.MimeCod
            Case DTOEnums.MimeCods.Jpg, DTOEnums.MimeCods.Gif
                oImg = maxisrvr.GetImgFromByteArray(mBigFile.Stream)
                oImg = maxisrvr.GetThumbnail(oImg, PictureBox1.Width, PictureBox1.Height)
                mBigFile.Img = oImg
                SeleccionarPaginesToolStripMenuItem.Enabled = False
            Case DTOEnums.MimeCods.Pdf
                Dim oPdfRender As New PdfRender(mBigFile.Stream)
                oImg = oPdfRender.Thumbnail(PictureBox1.Width, PictureBox1.Height)
                mBigFile.Img = oImg
                SeleccionarPaginesToolStripMenuItem.Enabled = True
            Case DTOEnums.MimeCods.Zip
                oImg = My.Resources.Zip86
                SeleccionarPaginesToolStripMenuItem.Enabled = False
            Case DTOEnums.MimeCods.Xls
                oImg = My.Resources.Excel_Big
            Case DTOEnums.MimeCods.Xlsx
                Dim iExcelRows As Integer = 0
                Dim iExcelCols As Integer = 0
                oImg = ExcelHelper.GetImgFromExcelFirstPage(mBigFile.Stream, iExcelCols, iExcelRows)
                With mBigFile
                    .Img = oImg
                    .Width = iExcelCols
                    .Height = iExcelRows
                End With
            Case DTOEnums.MimeCods.Doc, DTOEnums.MimeCods.Docx
                oImg = WordHelper.GetImgFromWordFirstPage(mBigFile.Stream)
            Case DTOEnums.MimeCods.Mpg
                oImg = My.Resources.VideoFrame
            Case DTOEnums.MimeCods.Rtf
                oImg = My.Resources.word
        End Select
        Return oImg
    End Function


    Private Sub ZoomToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZoomToolStripMenuItem.Click
        root.ShowBigFile(mBigFile, mSuggestedFilename)
    End Sub

    Private Sub ToolStripMenuItemCopyLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemCopyLink.Click
        Dim sUrl As String = BaseGuid.UrlFromGuid(mBigFile.Guid, True)
        Clipboard.SetDataObject(SURL, True)
        MsgBox("enllaç copiat al portapapers:" & vbCrLf & sUrl, MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem.Click
        mBigFile.Clear()
        PictureBox1.Image = mBigFile.Img
        mIsDirty = True
        RaiseEvent AfterUpdate(mBigFile, New System.EventArgs)
        Refresca()
    End Sub

    Private Function GetDisplayImage() As Image
        Dim oImg As Image = mBigFile.Img
        If oImg Is Nothing Then
            oImg = GetImgFromStream()
        End If
        Return oImg
    End Function

    Private Sub Xl_BigFile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PictureBox1.AllowDrop = True
    End Sub

    Private Sub PictureBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(maxisrvr.BigFileNew))) Then
            e.Effect = DragDropEffects.Copy
        Else
            '    or none of the above
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub PictureBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        Dim fileNames() As String = Nothing

        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
                fileNames = e.Data.GetData(DataFormats.FileDrop)
                Dim sFilename As String = fileNames(0)
                ' get the actual raw file into memory
                Dim oFileStream As New System.IO.FileStream(sFilename, IO.FileMode.Open)
                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oFileStream)
                Dim oStream As Byte() = oBinaryReader.ReadBytes(oFileStream.Length)
                oBinaryReader.Close()
                root.LoadBigFile(mBigFile, oStream, sFilename)

                Refresca()
                mIsDirty = True
                RaiseEvent AfterUpdate(mBigFile, New System.EventArgs)

            ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
                '
                ' the first step here is to get the filename
                ' of the attachment and
                ' build a full-path name so we can store it 
                ' in the temporary folder
                '
                ' set up to obtain the FileGroupDescriptor 
                ' and extract the file name
                Dim theStream As System.IO.MemoryStream = e.Data.GetData("FileGroupDescriptor")
                Dim fileGroupDescriptor(512) As Byte
                theStream.Read(fileGroupDescriptor, 0, 512)

                ' used to build the filename from the FileGroupDescriptor block
                Dim sfilename As String = ""
                For i As Integer = 76 To 512
                    If fileGroupDescriptor(i) = 0 Then Exit For
                    sfilename = sfilename & Convert.ToChar(fileGroupDescriptor(i))
                Next
                theStream.Close()

                '
                ' Second step:  we have the file name.  
                ' Now we need to get the actual raw
                ' data for the attached file .
                '

                ' get the actual raw file into memory
                Dim oMemStream As System.IO.MemoryStream = e.Data.GetData("FileContents", True)
                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oMemStream)
                Dim oStream As Byte() = oBinaryReader.ReadBytes(oMemStream.Length)
                oBinaryReader.Close()
                root.LoadBigFile(mBigFile, oStream, sfilename)

                Refresca()
                mIsDirty = True
                RaiseEvent AfterUpdate(mBigFile, New System.EventArgs)

            ElseIf (e.Data.GetDataPresent(GetType(maxisrvr.BigFileNew))) Then
                mBigFile = e.Data.GetData(GetType(maxisrvr.BigFileNew))

                Refresca()
                mIsDirty = True
                RaiseEvent AfterUpdate(mBigFile, New System.EventArgs)

            Else
                MsgBox("format desconegut")
            End If
        Catch ex As Exception
            MsgBox("Error in DragDrop function: " + ex.Message)
        End Try
    End Sub

    Private Sub SeleccionarPaginesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeleccionarPaginesToolStripMenuItem.Click
        Dim oFrm As New Frm_PdfPageSelect
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .PdfStream = mBigFile.Stream
            .Show()
        End With
    End Sub

    Private Sub RefreshRequest(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oStream As Byte() = CType(sender, Byte())
        root.LoadBigFilePdfStream(mBigFile, oStream)
        mIsDirty = True
        Refresca()
        RaiseEvent AfterUpdate(mBigFile, New System.EventArgs)
    End Sub

    Private Sub ToolStripMenuItemCopyImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemCopyImg.Click
        Clipboard.SetDataObject(PictureBox1.Image, True)
        MsgBox("imatge copiada al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub ToolStripMenuItemImgExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemImgExport.Click
        Dim oDlg As New SaveFileDialog
        With oDlg
            If .ShowDialog = DialogResult.OK Then
                PictureBox1.Image.Save(.FileName)
            End If
        End With
    End Sub

    Private Sub ExportarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExportarToolStripMenuItem.Click
        Dim sMime As String = "*" & mBigFile.GetExtensionFromMime()
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Filter = sMime & "|" & sMime & "|tots els fitxers (*.*)|*.*"
            .DefaultExt = mBigFile.GetExtensionFromMime()
            If .ShowDialog = DialogResult.OK Then
                mBigFile.Save(.FileName)
            End If
        End With
    End Sub

    Private Sub LogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogToolStripMenuItem.Click
        'RaiseEvent ShowLog(mBigFile, EventArgs.Empty)
        Dim oFrm As New Frm_DownloadLog(mBigFile)
        oFrm.Show()
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            PictureBox1.DoDragDrop(mBigFile, DragDropEffects.Copy)
        End If
    End Sub

    Private Sub PictureBox1_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub
End Class
