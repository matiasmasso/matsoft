Public Class Frm_PdfSign

    Private Sub importStream(oByteArray As Byte())
        Dim exs As New List(Of Exception)
        Dim oStream As New System.IO.MemoryStream(oByteArray)
        Dim oPdf = LegacyHelper.GhostScriptHelper.Rasterize(exs, oStream)
        If exs.Count = 0 Then
            PictureBox1.Image = LegacyHelper.ImageHelper.Converter(oPdf.Thumbnail)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ImportarPdfToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarPdfToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog
        With oDlg
            If .ShowDialog Then
                Dim oByteArray As Byte() = Nothing
                If FileSystemHelper.GetStreamFromFile(.FileName, oByteArray, exs) Then
                    Dim oStream As New System.IO.MemoryStream(oByteArray)
                    Dim oPdf = LegacyHelper.GhostScriptHelper.Rasterize(exs, oStream)
                    PictureBox1.Image = LegacyHelper.ImageHelper.Converter(oPdf.Thumbnail)
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Sub PictureBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        Dim oDocfiles As New List(Of DTODocFile)
        Dim exs As New List(Of Exception)
        Dim oTargetCell As DataGridViewCell = Nothing

        If DragDropHelper.GetDroppedDocFiles(e, oDocfiles, exs) Then
            If oDocfiles.Count > 0 Then
                Dim oDocfile = oDocfiles.First

            End If
        Else
            UIHelper.WarnError(exs, "error al importar fitxers")
        End If
    End Sub
End Class