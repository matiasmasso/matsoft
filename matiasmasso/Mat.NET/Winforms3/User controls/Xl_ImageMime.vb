

Public Class Xl_ImageMime
    Inherits PictureBox
    Private _ImageMime As ImageMime

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oByteArray As Byte(), oMime As MimeCods)
        Dim oImageMime = ImageMime.Factory(oByteArray, oMime)
        Load(oImageMime)
    End Sub

    Public Shadows Sub Load(oImageMime As ImageMime)
        _ImageMime = oImageMime
        Dim oLegacyImage = LegacyHelper.ImageHelper.GetImgFromByteArray(oImageMime.ByteArray)
        MyBase.Image = oLegacyImage
        MyBase.SizeMode = PictureBoxSizeMode.Zoom
        SetContextMenu()
    End Sub

    Public Function Value() As ImageMime
        Return _ImageMime
    End Function

    Private Sub SetContextMenu()
        If MyBase.ContextMenuStrip Is Nothing Then

            Dim oContextMenu As New ContextMenuStrip
            Dim oMenuItem As ToolStripMenuItem = Nothing
            oMenuItem = New ToolStripMenuItem("importar", Nothing, AddressOf Do_Import)
            oContextMenu.Items.Add(oMenuItem)
            oMenuItem = New ToolStripMenuItem("exportar", Nothing, AddressOf Do_Export)
            oContextMenu.Items.Add(oMenuItem)
            oMenuItem = New ToolStripMenuItem("copiar", Nothing, AddressOf Do_Copy)
            oContextMenu.Items.Add(oMenuItem)
            oMenuItem = New ToolStripMenuItem("borrar", Nothing, AddressOf Do_Clear)
            oContextMenu.Items.Add(oMenuItem)
            MyBase.ContextMenuStrip = oContextMenu
        End If

    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim oImg As System.Drawing.Image = MyBase.Image
            If Not oImg Is Nothing Then
                MyBase.DoDragDrop(oImg, System.Windows.Forms.DragDropEffects.Copy)
            End If
        End If
    End Sub

    Private Sub PictureBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        e.Effect = DragDropHelper.DragEnterFilePresentEffect(e)
    End Sub

    Private Sub PictureBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop
        Dim exs As New List(Of Exception)

        Dim oFile = DragDropHelper.GetDroppedFile(exs, e)
        If exs.Count = 0 Then
            _ImageMime = ImageMime.Factory(oFile.ByteArray)
            Load(_ImageMime)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ImageMime))
        End If
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.DoubleClick
        Do_Import()
    End Sub

    Private Sub Do_Clear()
        _ImageMime = Nothing
        Load(_ImageMime)
    End Sub

    Private Sub Do_Import()
        Dim oDlg As New System.Windows.Forms.OpenFileDialog
        Dim oResult As System.Windows.Forms.DialogResult

        With oDlg
            .Title = "importar imatge"
            .Filter = "Totes les imatges |*.gif;*.jpg;*.jpeg;*.png;*.bmp;*.ico|Tots els fitxers (*.*)|*.*"
            .FilterIndex = 4
            oResult = .ShowDialog
            Select Case oResult
                Case System.Windows.Forms.DialogResult.OK
                    Dim exs As New List(Of Exception)
                    Dim oByteArray As Byte() = Nothing
                    If FileSystemHelper.GetStreamFromFile(.FileName, oByteArray, exs) Then
                        Dim oMime = MimeHelper.GetMimeFromExtension(.FileName)
                        _ImageMime = ImageMime.Factory(oByteArray, oMime)
                        Load(_ImageMime)
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(_ImageMime))
                    Else
                        MsgBox("error al importar la imatge" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
                    End If
            End Select
        End With
    End Sub

    Private Sub Do_Copy(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oImg As System.Drawing.Image = MyBase.Image
        System.Windows.Forms.Clipboard.SetDataObject(oImg, True)
    End Sub

    Private Sub Do_Export(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New System.Windows.Forms.SaveFileDialog
        Dim oResult As System.Windows.Forms.DialogResult
        Dim fileExtension = MimeHelper.GetExtensionFromMime(_ImageMime.Mime)
        With oDlg
            .Title = "Guardar imatge de producte"
            .FileName = String.Format("imatge.{0}", fileExtension)
            .Filter = String.Format("*imatges {0}|*.{0}", fileExtension)
            oResult = .ShowDialog
            Select Case oResult
                Case System.Windows.Forms.DialogResult.OK
                    Dim ms As New IO.MemoryStream(_ImageMime.ByteArray)
                    Dim oImage = Image.FromStream(ms)
                    oImage.Save(.FileName)
            End Select
        End With
    End Sub
End Class
