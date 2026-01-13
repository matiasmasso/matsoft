
Imports System.Windows.Forms

Public Class Frm_MediaResource

    Private _MediaResource As DTOMediaResource
    Private _DirtyStream As Boolean
    Private _DirtyThumbnail As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oMediaResource As DTOMediaResource)
        MyBase.New
        InitializeComponent()
        _MediaResource = oMediaResource
    End Sub

    Private Sub Frm_MediaResource_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim exs As New List(Of Exception)
        If FEB2.MediaResource.Load(exs, _MediaResource, LoadThumbnail:=True) Then
            With _MediaResource
                UIHelper.LoadComboFromEnum(ComboBoxCods, GetType(DTOMediaResource.Cods), "(sel.leccionar codi)", .Cod)
                Xl_LookupProduct1.Product = .Product
                TextBoxNom.Text = .Nom
                Xl_Langs1.Value = .Lang
                CheckBoxObsoleto.Checked = .Obsolet
                PictureBox1.Image = LegacyHelper.ImageHelper.Converter(.Thumbnail)
                PictureBox1.ContextMenuStrip = New ContextMenuStrip
                PictureBox1.ContextMenuStrip.Items.Add("canviar miniatura", Nothing, AddressOf Do_ChangeThumbnail)
                PictureBox1.ContextMenuStrip.Items.Add("exportar miniatura", Nothing, AddressOf Do_ExportThumbnail)
                LabelFeatures.Text = .Features()
                Xl_UsrLog1.Load(.UsrLog)
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
            ButtonOk.Enabled = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_ChangeThumbnail()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Obrir nova miniatura"
            .Filter = UIHelper.SavefileDialogFilter(MimeCods.Jpg)
            If .ShowDialog Then
                Dim oImage As Image = Nothing
                Dim exs As New List(Of Exception)
                If DTOMediaResource.validateThumbnail(.FileName, oImage, exs) Then
                    PictureBox1.Image = LegacyHelper.ImageHelper.Converter(oImage)
                    _DirtyThumbnail = True
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Sub Do_ExportThumbnail()
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "Exportar miniatura"
            .FileName = "Miniatura " & TextBoxNom.Text & ".jpg"
            If .ShowDialog = DialogResult.OK Then
                PictureBox1.Image.Save(.FileName)
            End If
        End With
    End Sub

    Private Sub ButtonFileSearch_Click(sender As Object, e As EventArgs) Handles ButtonFileSearch.Click
        Dim oDlg As New OpenFileDialog()
        With oDlg
            .Title = "Importar nou recurs"
            If .ShowDialog Then
                Dim exs As New List(Of Exception)
                Dim tmp = MediaResourcesHelper.Factory(Current.Session.User, .FileName, exs)
                If exs.Count = 0 Then
                    _MediaResource.Stream = tmp.Stream
                    _MediaResource.UsrLog.usrLastEdited = Current.Session.User
                    PictureBox1.Image = LegacyHelper.ImageHelper.Converter(_MediaResource.Thumbnail)
                    LabelFeatures.Text = _MediaResource.Features()
                    TextBoxFile.Text = .FileName
                    TextBoxNom.Text = System.IO.Path.GetFileName(.FileName)
                    _DirtyStream = True
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        If PictureBox1.Image Is Nothing Then
            UIHelper.WarnError("falta triar recurs")
        ElseIf TextBoxNom.Text.Contains(",") Then
            UIHelper.WarnError("mo s'admeten comes als nom de la imatge")
        Else
            With _MediaResource
                .Nom = TextBoxNom.Text
                .Product = Xl_LookupProduct1.Product
                .Lang = Xl_Langs1.Value
                .Cod = ComboBoxCods.SelectedValue
                .Obsolet = CheckBoxObsoleto.Checked
                If _DirtyThumbnail Then .Thumbnail = LegacyHelper.ImageHelper.Converter(PictureBox1.Image)
            End With

            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB2.MediaResource.Update(_MediaResource, exs) Then
                UIHelper.ToggleProggressBar(Panel1, False)
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest recurs?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.MediaResource.Delete(_MediaResource, exs) Then
                Try
                    'Dim oFtp = MediaResourceFtpHelper.FtpClient
                    'Dim sTargetFilename As String = DTOMediaResource.TargetFilename(_MediaResource)
                    'oFtp.FtpDelete(sTargetFilename)
                    'RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                    'Me.Close()
                    MsgBox("No impementado")
                Catch ex As Exception
                    FEB2.MediaResource.UpdateSync(_MediaResource, exs)
                    UIHelper.WarnError(exs)
                End Try
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        CheckBoxObsoleto.CheckedChanged,
         ComboBoxCods.SelectedIndexChanged,
          TextBoxFile.TextChanged,
           TextBoxNom.TextChanged,
            Xl_Langs1.AfterUpdate,
             Xl_LookupProduct1.AfterUpdate

        If _AllowEvents Then
            _MediaResource.UsrLog.usrLastEdited = Current.Session.User
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CopyGuidToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyGuidToolStripMenuItem.Click
        UIHelper.CopyToClipboard(_MediaResource.Guid.ToString)
    End Sub

    Private Sub CopyLinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyLinkToolStripMenuItem.Click
        Dim sUrl As String = FEB2.MediaResource.Url(_MediaResource, True)
        UIHelper.CopyLink(sUrl)
    End Sub

    Private Async Sub DownloadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New SaveFileDialog()
        With oDlg
            .Title = "Exportar recurs"
            .Filter = UIHelper.SavefileDialogFilter(_MediaResource.Mime)
            .FileName = DTOMediaResource.FriendlyName(_MediaResource)
            If .ShowDialog Then
                UIHelper.ToggleProggressBar(Panel1, True)
                Dim url = FEB2.MediaResource.Url(_MediaResource, True)
                Dim oImage = Await MatHelperStd.ImageHelper.DownloadFromWebsiteAsync(exs, url)
                If exs.Count = 0 Then
                    If oImage Is Nothing Then
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError("la imatge es buida")
                    Else
                        Try
                            Select Case _MediaResource.Mime
                                Case MimeCods.Jpg
                                    Await oImage.SaveAsJpegAsync(.FileName)
                                Case MimeCods.Png
                                    Await oImage.SaveAsPngAsync(.FileName)
                                Case MimeCods.Gif
                                    Await oImage.SaveAsGifAsync(.FileName)
                            End Select
                            UIHelper.ToggleProggressBar(Panel1, False)

                        Catch ex As Exception
                            UIHelper.ToggleProggressBar(Panel1, False)
                            UIHelper.WarnError(exs)
                        End Try
                    End If
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs)
                End If
            End If
        End With

    End Sub


    Private Sub TextBoxNom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxNom.KeyPress
        Dim forbiddenChars As String = "\/:,;""*?<>|"
        If forbiddenChars.Contains(e.KeyChar) Then
            e.KeyChar = " "
        End If
    End Sub
End Class