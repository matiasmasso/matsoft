

Public Class Frm_MediaResource

    Private _MediaResource As DTOMediaResource
    Private _Cache As Models.ClientCache
    Private _Product As DTOProduct
    Private _Stream As Byte()
    Private _DirtyStream As Boolean
    Private _DirtyThumbnail As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oProduct As DTOProduct)
        'adding new media resource
        MyBase.New
        InitializeComponent()
        _Product = oProduct
    End Sub

    Public Sub New(Optional oMediaResource As DTOMediaResource = Nothing)
        'editing current media resource
        MyBase.New
        InitializeComponent()
        _MediaResource = oMediaResource
    End Sub

    Private Async Sub Frm_MediaResource_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        _Cache = Await FEB.Cache.Fetch(exs, Current.Session.User)
        If exs.Count = 0 Then
            PictureBox1.AllowDrop = True
            LabelPictureboxInfo.AllowDrop = True
            UIHelper.LoadComboFromEnum(ComboBoxCods, GetType(DTOMediaResource.Cods), "(sel.leccionar codi)")

            If _MediaResource Is Nothing Then
                LoadEmptyResource()
                UIHelper.ToggleProggressBar(Panel1, False)
            Else
                If FEB.MediaResource.Load(exs, _MediaResource, LoadThumbnail:=True) Then
                    LoadExistingResource()
                    UIHelper.ToggleProggressBar(Panel1, False)
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs)
                End If
            End If
            _AllowEvents = True

        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadEmptyResource()
        Dim oUsrLog = DTOUsrLog.Factory(Current.Session.User)
        Dim oProducts As New List(Of DTOProduct)
        oProducts.Add(_Product)
        Xl_LookupProduct1.Load(New List(Of DTOProduct), DTOProduct.SelectionModes.SelectMany)
        Xl_Products1.Load(oProducts)
        Xl_LangSet1.Load(New DTOLang.Set("1111"))
        Xl_UsrLog1.Load(oUsrLog)
        ButtonDel.Enabled = False
        EnableMenus()
    End Sub

    Private Sub LoadExistingResource()
        With _MediaResource
            ComboBoxCods.SelectedValue = .Cod
            Xl_LookupProduct1.Load(New List(Of DTOProduct), DTOProduct.SelectionModes.SelectMany)
            TextBoxNom.Text = .Nom
            Xl_Langs1.Value = .Lang
            Xl_LangSet1.Load(.LangSet)
            CheckBoxObsoleto.Checked = .Obsolet
            LabelPictureboxInfo.Visible = False
            PictureBox1.Image = LegacyHelper.ImageHelper.FromBytes(.Thumbnail)
            TextBoxFeatures.Text = .Features()
            TextBoxEsp.Text = .Description.Text(DTOLang.ESP())
            TextBoxCat.Text = .Description.Text(DTOLang.CAT())
            TextBoxEng.Text = .Description.Text(DTOLang.ENG())
            TextBoxPor.Text = .Description.Text(DTOLang.POR())
            EnableMenus()

            Dim oProducts = Xl_Products1.Values 'add to exisiting products so we don't lose previous data
            For Each item In .Products
                Dim oProduct = _Cache.ProductOrSelf(item.Guid)
                oProducts.Add(oProduct)
            Next
            Xl_Products1.Load(oProducts)

            Xl_UsrLog1.Load(.UsrLog)
            ButtonDel.Enabled = Not .IsNew
            ButtonOk.Enabled = True
        End With

    End Sub

    Private Sub EnableMenus()
        Dim enabled = PictureBox1.Image IsNot Nothing
        DownloadToolStripMenuItem.Enabled = enabled
        ExportarImatgeToolStripMenuItem.Enabled = enabled
        CopiarLinkToolStripMenuItem.Enabled = enabled
        CopyLinkToolStripMenuItem.Enabled = enabled
        CopyGuidToolStripMenuItem.Enabled = enabled
        CopiarHashToolStripMenuItem.Enabled = enabled

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
                .Products = Xl_Products1.Values.Select(Function(x) New Models.Base.GuidNom(x.Guid, x.FullNom())).ToList()
                .Lang = Xl_Langs1.Value
                .LangSet = Xl_LangSet1.Value
                .Cod = ComboBoxCods.SelectedValue
                .Obsolet = CheckBoxObsoleto.Checked
                .Description.Load(TextBoxEsp.Text, TextBoxCat.Text, TextBoxEng.Text, TextBoxPor.Text)
                If _DirtyThumbnail Then
                    .Thumbnail = PictureBox1.Image.Bytes()
                    .Stream = _Stream
                End If
                .UsrLog.UsrLastEdited = Current.Session.User
            End With

            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB.MediaResource.Update(_MediaResource, exs) Then
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
            If Await FEB.MediaResource.Delete(_MediaResource, exs) Then
                Try
                    'Dim oFtp = MediaResourceFtpHelper.FtpClient
                    'Dim sTargetFilename As String = DTOMediaResource.TargetFilename(_MediaResource)
                    'oFtp.FtpDelete(sTargetFilename)
                    'RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                    'Me.Close()
                    MsgBox("No impementado")
                Catch ex As Exception
                    FEB.MediaResource.UpdateSync(_MediaResource, exs)
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
           TextBoxNom.TextChanged,
            Xl_Langs1.AfterUpdate,
             Xl_LangSet1.AfterUpdate,
             TextBoxEsp.TextChanged,
              TextBoxCat.TextChanged,
               TextBoxEng.TextChanged,
                TextBoxPor.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub



    Private Sub TextBoxNom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxNom.KeyPress
        Dim forbiddenChars = "\/:,;""*?<>|"
        If forbiddenChars.Contains(e.KeyChar) Then
            e.KeyChar = " "
        End If
    End Sub

    Private Sub ButtonAddProduct_Click(sender As Object, e As EventArgs) Handles ButtonAddProduct.Click
        Dim oProductsToAdd = Xl_LookupProduct1.Products
        Dim oCurrentProducts = Xl_Products1.Values
        oCurrentProducts.AddRange(oProductsToAdd)
        Xl_Products1.Load(oCurrentProducts)
        Xl_LookupProduct1.Clear()
        ButtonAddProduct.Enabled = False
        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        ButtonAddProduct.Enabled = True
    End Sub

    Private Async Sub ImportarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarToolStripMenuItem.Click
        Await ImportFile()
    End Sub

    Private Async Sub PictureBox1_DragDrop(sender As Object, e As DragEventArgs) Handles PictureBox1.DragDrop, LabelPictureboxInfo.DragDrop
        Dim exs As New List(Of Exception)

        Dim oFile = DragDropHelper.GetDroppedFile(exs, e)
        If exs.Count = 0 Then
            Dim sFilename = System.IO.Path.GetFileName(oFile.filename)
            Await ImportFile(exs, oFile.ByteArray, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If

        'Dim files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())

        'If files.Length <> 0 Then
        'Try
        'Await ImportFile(exs, files(0))

        'Catch ex As Exception
        'exs.Add(ex)
        'End Try
        'If exs.Count > 0 Then
        'UIHelper.WarnError(exs)
        'End If
        'End If
    End Sub

    Private Sub Picture_DragEnter(sender As Object, e As DragEventArgs) Handles PictureBox1.DragEnter, LabelPictureboxInfo.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Or e.Data.GetDataPresent("FileGroupDescriptor") Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub CopyGuidToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyGuidToolStripMenuItem.Click, CopiarHashToolStripMenuItem.Click
        If _MediaResource IsNot Nothing Then
            UIHelper.CopyToClipboard(_MediaResource.Hash)
        End If
    End Sub

    Private Sub CopyLinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyLinkToolStripMenuItem.Click, CopiarLinkToolStripMenuItem.Click
        Dim sUrl As String = FEB.MediaResource.Url(_MediaResource, True)
        UIHelper.CopyLink(sUrl)
    End Sub

    Private Async Sub DownloadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadToolStripMenuItem.Click, ExportarImatgeToolStripMenuItem.Click
        Await ExportFile()
    End Sub


    Private Async Sub Importar_Click(sender As Object, e As EventArgs) Handles ImportarImatgeToolStripMenuItem.Click, ImportarToolStripMenuItem.Click
        Await ImportFile()
    End Sub


    Private Async Function ImportFile() As Task
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog()
        With oDlg
            .Title = "Importar nou recurs"
            If .ShowDialog = DialogResult.OK Then
                Dim oByteArray As Byte() = Nothing
                If FileSystemHelper.GetStreamFromFile(.FileName, oByteArray, exs) Then
                    'Await ImportFile(exs, .FileName)
                    Await ImportFile(exs, oByteArray, .FileName)
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Function

    Private Async Function ImportFile(exs As List(Of Exception), oByteArray As Byte(), filename As String) As Task
        UIHelper.ToggleProggressBar(Panel1, True)
        If MediaResourcesHelper.LoadFromStream(Current.Session.User, oByteArray, _MediaResource, exs, filename) Then
            Dim tmp = Await FEB.MediaResource.FromHash(exs, _MediaResource.Hash)
            If exs.Count = 0 Then
                If tmp Is Nothing Then
                    _Stream = oByteArray
                    LabelPictureboxInfo.Visible = False
                    PictureBox1.Image = LegacyHelper.ImageHelper.FromBytes(_MediaResource.Thumbnail)
                    _DirtyThumbnail = True

                    TextBoxFeatures.Text = _MediaResource.Features()
                    TextBoxNom.Text = System.IO.Path.GetFileName(filename)
                    _DirtyStream = True
                Else
                    _MediaResource.Products = tmp.Products
                    _MediaResource.Cod = tmp.Cod
                    _MediaResource.Lang = tmp.Lang
                    _MediaResource.Obsolet = tmp.Obsolet

                    LoadExistingResource()
                End If
                UIHelper.ToggleProggressBar(Panel1, False)
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
            EnableMenus()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If

    End Function
    Private Async Function ImportFile(exs As List(Of Exception), filename As String) As Task
        UIHelper.ToggleProggressBar(Panel1, True)
        _MediaResource = MediaResourcesHelper.Factory(Current.Session.User, filename, exs)
        If exs.Count = 0 Then
            Dim tmp = Await FEB.MediaResource.FromHash(exs, _MediaResource.Hash)
            If exs.Count = 0 Then
                If tmp Is Nothing Then
                    _Stream = _MediaResource.Stream
                    LabelPictureboxInfo.Visible = False
                    Dim oThumbnail = LegacyHelper.ImageHelper.FromBytes(_MediaResource.Thumbnail)
                    PictureBox1.Image = oThumbnail
                    _DirtyThumbnail = True

                    TextBoxFeatures.Text = _MediaResource.Features()
                    TextBoxNom.Text = System.IO.Path.GetFileName(filename)
                    _DirtyStream = True
                Else
                    _MediaResource.Guid = tmp.Guid
                    _MediaResource.Nom = tmp.Nom
                    _MediaResource.Products = tmp.Products
                    _MediaResource.Cod = tmp.Cod
                    _MediaResource.Ord = tmp.Ord
                    _MediaResource.Description = tmp.Description
                    _MediaResource.Lang = tmp.Lang
                    _MediaResource.LangSet = tmp.LangSet
                    _MediaResource.Obsolet = tmp.Obsolet

                    LoadExistingResource()
                End If
                UIHelper.ToggleProggressBar(Panel1, False)
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
            EnableMenus()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Async Function ExportFile() As Task
        Dim exs As New List(Of Exception)
        Dim oDlg As New SaveFileDialog()
        With oDlg
            .Title = "Exportar recurs"
            .Filter = UIHelper.SavefileDialogFilter(_MediaResource.Mime)
            .FileName = DTOMediaResource.FriendlyName(_MediaResource)
            If .ShowDialog Then
                UIHelper.ToggleProggressBar(Panel1, True)
                Dim url = FEB.MediaResource.Url(_MediaResource, True)
                Dim oImageBytes = Await FEB.FetchImage(exs, url)
                If exs.Count = 0 Then
                    If oImageBytes Is Nothing Then
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError("la imatge es buida")
                    Else
                        Try
                            Dim oImage = LegacyHelper.ImageHelper.FromBytes(oImageBytes)
                            oImage.Save(.FileName)
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

    End Function

End Class