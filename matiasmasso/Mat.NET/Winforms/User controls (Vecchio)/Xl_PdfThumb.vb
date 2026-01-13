

Public Class Xl_PdfThumb
    Private mBigFile As BigFileSrc
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property BigFile() As BigFileSrc
        Get
            Return mBigFile
        End Get
        Set(ByVal value As BigFileSrc)
            If value IsNot Nothing Then
                mBigFile = value
                Refresca()
            End If
        End Set
    End Property

    Private Sub Refresca()
        If mBigFile.IsEmpty Then
            ZoomToolStripMenuItem.Enabled = False
            EliminarToolStripMenuItem.Enabled = False
            ExportarToolStripMenuItem.Enabled = False
        Else
            PictureBox1.Image = BLL.GetThumbnailToFit(mBigFile.BigFile.Img, PictureBox1.Width, PictureBox1.Height)
            ZoomToolStripMenuItem.Enabled = True
            EliminarToolStripMenuItem.Enabled = True
            ExportarToolStripMenuItem.Enabled = True
            PictureBox1.Text = mBigFile.BigFile.Features
        End If
    End Sub


    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        If mBigFile.BigFile.Img Is Nothing Then
            root.LoadBigFileFromDialog(mBigFile)
            RaiseEvent AfterUpdate(mBigFile, New System.EventArgs)
            Refresca()
        Else
            root.ShowBigFile(mBigFile.BigFile)
        End If
    End Sub

    Private Sub ImportarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportarToolStripMenuItem.Click
        If root.LoadBigFileFromDialog(mBigFile) Then
            GetImgFromStream()
            RaiseEvent AfterUpdate(mBigFile, New System.EventArgs)
            Refresca()
        End If
    End Sub

    Private Function GetImgFromStream() As Image
        Dim oImg As Image = Nothing
        With mBigFile.BigFile
            Select Case .MimeCod
                Case DTOEnums.MimeCods.Jpg, DTOEnums.MimeCods.Gif
                    oImg = maxisrvr.GetImgFromByteArray(.Stream)
                    oImg = BLL.GetThumbnailToFit(oImg, MaxiSrvr.BigFileNew.THUMB_WIDTH, MaxiSrvr.BigFileNew.THUMB_HEIGHT)
                    mBigFile.BigFile.Img = oImg
                Case DTOEnums.MimeCods.Pdf
                    Dim oPdfRender As New PdfRender(.Stream)
                    oImg = oPdfRender.Thumbnail(maxisrvr.BigFileNew.THUMB_WIDTH, maxisrvr.BigFileNew.THUMB_HEIGHT)
                    mBigFile.BigFile.Img = oImg
                Case DTOEnums.MimeCods.Zip
                    oImg = My.Resources.Zip86
                Case DTOEnums.MimeCods.Rtf
                    oImg = My.Resources.word
                Case DTOEnums.MimeCods.Xls, DTOEnums.MimeCods.Xlsx
                    oImg = My.Resources.Excel_Big
            End Select
        End With
        Return oImg
    End Function


    Private Sub ZoomToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZoomToolStripMenuItem.Click
        root.ShowBigFile(mBigFile.BigFile)
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem.Click
        mBigFile.BigFile.Clear()
        PictureBox1.Image = mBigFile.BigFile.Img
        RaiseEvent AfterUpdate(mBigFile, New System.EventArgs)
        Refresca()
    End Sub

    Private Sub CopyLinkToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyLinkToolStripMenuItem.Click
        Dim strURL As String = mBigFile.RoutingUrl(True)

        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, strURL)
        Clipboard.SetDataObject(data_object, True)
        MsgBox("enllaç copiat al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub
End Class
